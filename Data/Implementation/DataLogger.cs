using System.Collections.Concurrent;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Data.Implementation;

public class DataLogger
{
        private readonly ConcurrentQueue<JObject> _ballsConcurrentQueue;
        private readonly string _pathToFile;
        private readonly object _writeLock = new();
        private readonly object _queueLock = new();
        private Task _logerTask;
        private const int Capacity = 1000;


        public DataLogger() 
        {
            string tempPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string loggersDirectory = Path.Combine(tempPath, "Loggs");
            _pathToFile = Path.Combine(loggersDirectory, "DataBallLog.json");
            _ballsConcurrentQueue = new ConcurrentQueue<JObject>();

            if (!Directory.Exists(loggersDirectory))
            {
                Directory.CreateDirectory(loggersDirectory);
                FileStream file = File.Create(_pathToFile);
                file.Close();
            }
        }

        public void AddBall(LoggerBall ball)
        {
            lock (_queueLock)
            {
                if (_logerTask is { IsCompleted: false }) return;

                if (_ballsConcurrentQueue.Count < Capacity)
                {
                    _logerTask = Task.Run(() => SaveDataToLog(ball));
                }
                else
                {
                    _logerTask = Task.Run(() => SaveDataToLog(null));
                }
            }
           
        }
        
        private void SaveDataToLog(LoggerBall ball)
        {
            JObject log = [];

            if (ball is null)
            {
                log["Time"] = DateTime.Now;
                log["Message"] = "Max capacity has been reached!!!";
            }
            else
            {
                log = JObject.FromObject(ball);
            }


            _ballsConcurrentQueue.Enqueue(log);

            lock (_writeLock)
            {
                // Check if the buffer is empty
                if (_ballsConcurrentQueue.IsEmpty) return;

                while (_ballsConcurrentQueue.TryDequeue(out JObject entry))
                {
                    try
                    {
                        File.AppendAllText(_pathToFile, JsonConvert.SerializeObject(entry, Formatting.Indented));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
        
}
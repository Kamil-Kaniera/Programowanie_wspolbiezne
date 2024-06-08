using System.Collections.Concurrent;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Data.Implementation;

public class DataLogger
{
    
        private readonly ConcurrentQueue<JObject> _ballsConcurrentQueue;
        private readonly JArray _logArray = [];
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

        public void AddBall(Ball ball, string time)
        {
            lock (_queueLock)
            {
                if (_logerTask is { IsCompleted: false }) return;

                if (_ballsConcurrentQueue.Count < Capacity)
                {
                    _logerTask = Task.Run(() => SaveDataToLog(ball, time));
                }
                else
                {
                    _logerTask = Task.Run(() => SaveDataToLog(null, time));
                }
            }
           
        }
        
        private void SaveDataToLog(Ball ball, string time)
        {
            JObject log = [];

            if (ball is null)
            {
                log["Time"] = time;
                log["Message"] = "Max capacity has been reached!!!";
            }
            else
            {
                log = JObject.FromObject(ball);
                log["Time"] = time;
            }


            _ballsConcurrentQueue.Enqueue(log);

            lock (_writeLock)
            {
                while (_ballsConcurrentQueue.TryDequeue(out JObject b)) 
                {
                    _logArray.Add(b);
                }

                // Check if the buffer is empty
                if (!_logArray.HasValues) return;

                string diagnosticData = JsonConvert.SerializeObject(_logArray, Formatting.Indented);
                
                try
                {
                    File.WriteAllText(_pathToFile, diagnosticData);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
            }
           
        }
        
}
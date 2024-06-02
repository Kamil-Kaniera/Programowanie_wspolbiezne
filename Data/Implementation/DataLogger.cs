using System.Collections.Concurrent;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Data.Implementation;

public class DataLogger
{
    
        private ConcurrentQueue<JObject> _ballsConcurrentQueue;
        private JArray _logArray;
        private string _pathToFile;
        private readonly object _writeLock = new();
        private Task _logerTask;
        private int _capacity;


        public DataLogger() 
        {
            string tempPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string loggersDirectory = Path.Combine(tempPath, "Loggs");
            _pathToFile = Path.Combine(loggersDirectory, "DataBallLog.json");
            _ballsConcurrentQueue = new ConcurrentQueue<JObject>();
            _capacity = 1000; 

            
            if (File.Exists(_pathToFile))
            {
                try
                {
                    string input = File.ReadAllText(_pathToFile);
                    _logArray = JArray.Parse(input);
                }
                catch(Exception ex)
                {
                    _logArray = new JArray();
                }
                
            }
            else
            {
                _logArray = new JArray();
                FileStream file = File.Create(_pathToFile);
                file.Close();
            }
        }
        public void AddBall(LoggerBall ball)
        {
            if (_ballsConcurrentQueue.Count == _capacity - 1)
            {
                JObject log = new();
                log["Time"] = DateTime.Now.ToString("HH:mm:ss.fff");
                log["Message"] = "Max capacity has been reached!!!";
                _ballsConcurrentQueue.Enqueue(log);    
                if (_logerTask == null || _logerTask.IsCompleted)
                {
                    _logerTask = Task.Run(SaveDataToLog);
                } 
            } 
            else
            {
                JObject log = JObject.FromObject(ball);
                _ballsConcurrentQueue.Enqueue(log);    
                if (_logerTask == null || _logerTask.IsCompleted)
                {
                    _logerTask = Task.Run(SaveDataToLog);
                }
            }            
            
        }
        
        private void SaveDataToLog()
        {
            lock (_writeLock)
            {
                while (_ballsConcurrentQueue.TryDequeue(out JObject ball)) 
                {
                    _logArray.Add(ball);
                }
                String diagnosticData = JsonConvert.SerializeObject(_logArray, Formatting.Indented);
                {
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
        
}
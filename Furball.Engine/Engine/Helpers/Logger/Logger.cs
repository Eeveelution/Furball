using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Furball.Engine.Engine.Helpers.Logger {
    public static class Logger {
        private static Queue<LoggerLine> _LoggerLines = new();
        private static List<ILogger>     _Loggers     = new();
        
        private static       double            _UpdateDeltaTime = 0;
        
        public static List<ILogger> Loggers => _Loggers;

        /// <summary>
        /// The time in seconds before each logger update, defaults to half a second
        /// </summary>
        public static double UpdateRate = 0.1d;
        
        public static async void Update(GameTime time) {
            _UpdateDeltaTime += time.ElapsedGameTime.TotalSeconds;

            if (_UpdateDeltaTime >= UpdateRate) {
                _UpdateDeltaTime = 0d;
                
                await Task.Run(
                delegate {
                    if (_LoggerLines.Count == 0) return;

                    do {
                        LoggerLine lineToSend = _LoggerLines.Dequeue();

                        foreach (ILogger logger in _Loggers)
                            logger.Send(lineToSend);
                    } while (_LoggerLines.Count > 0);
                }
                );
            }
        }

        public static void AddLogger(ILogger logger) {
            logger.Initialize();
            _Loggers.Add(logger);
        }

        public static void RemoveLogger(ILogger logger) {
            logger.Dispose();
            _Loggers.Remove(logger);
        }

        public static void RemoveLogger(Type type) {
            for (var i = 0; i < _Loggers.Count; i++) 
                if (_Loggers[i].GetType() == type) 
                    RemoveLogger(_Loggers[i]);
        }

        public static void Log(LoggerLine line) {
            line.LineData = line.LineData.Replace("\r", "");
            line.LineData = line.LineData.Replace("\n", " ");
            _LoggerLines.Enqueue(line);
        }

        public static void Log(string data, LoggerLevel level = LoggerLevel.Unknown) {
            Log(new LoggerLine{Level = level, LineData = data});
        }
    }
}

using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App.Logger
{
    public class Logger : ILogger
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public void Log(LogMessage message)
        {
            var stringMessage = CreateMessage(message);
            switch (message.Level)
            {
                case Level.Info:
                    _logger.Log(LogLevel.Info, stringMessage);
                    break;
                case Level.Warn:
                    _logger.Log(LogLevel.Warn, stringMessage);
                    break;
                case Level.Error:
                    _logger.Log(LogLevel.Error, stringMessage);
                    break;
                case Level.Fatal:
                    _logger.Log(LogLevel.Fatal, stringMessage);
                    break;
                default:
                    throw new InvalidEnumArgumentException($"Unhandled level: {message.Level.ToString()}");
            }
        }

        private string CreateMessage(LogMessage message)
        { 
            return $"Level-{message?.Level.ToString()}|Place-{message?.Place}|StackTrace-{message?.StackTrace}|Date-{message?.Date}|Message-{message?.Message}";
        }
    }
}

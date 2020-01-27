using System;
using System.Collections.Generic;
using System.Text;

namespace App.Logger
{
    public class LogMessage
    {
        public string Message { get; set; }
        public string Place { get; set; }
        public string StackTrace { get; set; }
        public DateTime Date { get; set; }
        public Level Level { get; set; }
    }

    public enum Level
    { 
        Info,
        Warn,
        Error,
        Fatal
    }
}

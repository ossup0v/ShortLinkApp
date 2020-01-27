using System;
using System.Collections.Generic;
using System.Text;

namespace App.Logger
{
    interface ILogger
    {
        void Log(LogMessage message);
    }
}

using System;
using Grach.Core.Enums;

namespace Grach.Core.Models
{
    public class Log
    {
        public Log()
        {
            CreatedDateTime = DateTime.UtcNow;
        }

        public string Message { get; set; }

        public LoggingLevels Level { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
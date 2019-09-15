using System;
using System.Collections.Generic;

namespace SamDevs.InfrastructureCore.ExceptionHandling
{
    public class ExceptionInfo
    {
        public DateTime Date { get; internal set; } = DateTime.UtcNow;
        public string Module { get; set; }
        public string Action { get; set; }

        public string Message { get; internal set; }

        /// <summary>
        /// Json string data
        /// </summary>
        public IEnumerable<object> Data { get; set; }
    }
}

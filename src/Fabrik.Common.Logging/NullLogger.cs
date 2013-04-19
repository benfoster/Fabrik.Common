using System;

namespace Fabrik.Common.Logging
{
    /// <summary>
    /// A logger that does nothing.
    /// </summary>
    public class NullLogger : ILogger
    {
        public void Trace(string message)
        {
        }

        public void Trace(string message, params object[] args)
        {
        }

        public void Debug(string message)
        {
        }

        public void Debug(string message, params object[] args)
        {
        }

        public void Info(string message)
        {
        }

        public void Info(string message, params object[] args)
        {
        }

        public void Warning(string message)
        {
        }

        public void Warning(string message, params object[] args)
        {
        }

        public void Error(string message)
        {
        }

        public void Error(string message, params object[] args)
        {
        }

        public void Error(Exception exception, string message)
        {
        }

        public void Error(Exception exception, string message, params object[] args)
        {
        }

        public void Fatal(string message)
        {
        }

        public void Fatal(string message, params object[] args)
        {
        }

        public void Fatal(Exception exception, string message)
        {
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
        }
    }
}

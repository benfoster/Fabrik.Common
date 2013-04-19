using NLog;
using System;

namespace Fabrik.Common.Logging.NLog
{
    public class NLogLogger : ILogger
    {
        private readonly Logger logger;

        public NLogLogger(Type loggerType)
        {
            Ensure.Argument.NotNull(loggerType, "loggerType");
            logger = LogManager.GetLogger(loggerType.FullName);
        }

        public void Trace(string message)
        {
            Ensure.Argument.NotNullOrEmpty(message, "message");
            logger.Trace(message);
        }

        public void Trace(string message, params object[] args)
        {
            Ensure.Argument.NotNullOrEmpty(message, "message");
            Ensure.Argument.NotNull(args, "args");
            logger.Trace(message, args);
        }

        public void Debug(string message)
        {
            Ensure.Argument.NotNullOrEmpty(message, "message");
            logger.Debug(message);
        }

        public void Debug(string message, params object[] args)
        {
            Ensure.Argument.NotNullOrEmpty(message, "message");
            Ensure.Argument.NotNull(args, "args");
            logger.Debug(message, args);
        }

        public void Info(string message)
        {
            Ensure.Argument.NotNullOrEmpty(message, "message");
            logger.Info(message);
        }

        public void Info(string message, params object[] args)
        {
            Ensure.Argument.NotNullOrEmpty(message, "message");
            Ensure.Argument.NotNull(args, "args");
            logger.Info(message, args);
        }

        public void Warning(string message)
        {
            Ensure.Argument.NotNullOrEmpty(message, "message");
            logger.Warn(message);
        }

        public void Warning(string message, params object[] args)
        {
            Ensure.Argument.NotNullOrEmpty(message, "message");
            Ensure.Argument.NotNull(args, "args");
            logger.Warn(message, args);
        }

        public void Error(string message)
        {
            Ensure.Argument.NotNullOrEmpty(message, "message");
            logger.Error(message);
        }

        public void Error(string message, params object[] args)
        {
            Ensure.Argument.NotNullOrEmpty(message, "message");
            Ensure.Argument.NotNull(args, "args");
            logger.Error(message, args);
        }

        public void Error(Exception exception, string message)
        {
            Ensure.Argument.NotNull(exception, "exception");
            Ensure.Argument.NotNullOrEmpty(message, "message");
            logger.ErrorException(message, exception);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            Ensure.Argument.NotNull(exception, "exception");
            Ensure.Argument.NotNullOrEmpty(message, "message");
            Ensure.Argument.NotNull(args, "args");
            logger.ErrorException(message.FormatWith(args), exception);
        }

        public void Fatal(string message)
        {
            Ensure.Argument.NotNullOrEmpty(message, "message");
            logger.Fatal(message);
        }

        public void Fatal(string message, params object[] args)
        {
            Ensure.Argument.NotNullOrEmpty(message, "message");
            Ensure.Argument.NotNull(args, "args");
            logger.Fatal(message, args);
        }

        public void Fatal(Exception exception, string message)
        {
            Ensure.Argument.NotNull(exception, "exception");
            Ensure.Argument.NotNullOrEmpty(message, "message");
            logger.FatalException(message, exception);
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            Ensure.Argument.NotNull(exception, "exception");
            Ensure.Argument.NotNullOrEmpty(message, "message");
            Ensure.Argument.NotNull(args, "args");
            logger.FatalException(message.FormatWith(args), exception);
        }
    }
}

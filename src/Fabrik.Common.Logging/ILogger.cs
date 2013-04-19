using System;

namespace Fabrik.Common.Logging
{
    /// <summary>
    /// An interface for loggers
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes the diagnostic message at the trace level.
        /// </summary>
        /// <param name="message">Log message</param>
        void Trace(string message);

        /// <summary>
        /// Writes the diagnostic message at the Trace level using the specified parameters.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Trace(string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Debug(string message);

        /// <summary>
        /// Writes the diagnostic message at the Debug level using the specified parameters.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the Info level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Info(string message);

        /// <summary>
        /// Writes the diagnostic message at the Info level using the specified parameters.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="message">Writes the diagnostic message at the Warn level.</param>
        void Warning(string message);
        
        /// <summary>
        /// Writes the diagnostic message at the Warn level using the specified parameters.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Warning(string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the Error level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Error(string message);

        /// <summary>
        /// Writes the diagnostic message at the Error level using the specified parameters.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Error(string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message and exception at the Error level.
        /// </summary>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        void Error(Exception exception, string message);
        
        /// <summary>
        /// Writes the diagnostic message and exception at the Error level using the specified parameters.
        /// </summary>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Error(Exception exception, string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Fatal(string message);

        /// <summary>
        /// Writes the diagnostic message at the Fatal level using the specified parameters.
        /// </summary>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Fatal(string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message and exception at the Fatal level.
        /// </summary>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string to be written.</param>
        void Fatal(Exception exception, string message);

        /// <summary>
        /// Writes the diagnostic message and exception at the Fatal level using the specified parameters.
        /// </summary>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="message">A string containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Fatal(Exception exception, string message, params object[] args);
    }
}

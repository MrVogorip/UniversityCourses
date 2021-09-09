using System;
using System.Diagnostics;
using System.Linq;
using GameStore.Domain.Interfaces;

namespace GameStore.Logging.LoggerExtensions
{
    public static class ExceptionLoggerExtension
    {
        public static void LogException(this IGameStoreLogger logger, Exception e)
        {
            var stackTrace = new StackTrace(e, true);
            foreach (var frame in stackTrace.GetFrames())
            {
                if (!(frame is null) && string.IsNullOrEmpty(frame.GetFileName()))
                {
                    string message = string.IsNullOrEmpty(e.Message) ? "No message" : e.Message;

                    logger.Error(
                        $"Message: {message}; " +
                        $"Type: {frame.GetType().Name}; " +
                        $"File:{frame.GetFileName()}; " +
                        $"Method:{frame.GetMethod().Name}; " +
                        $"Line: {frame.GetFileLineNumber()}; ");
                }
            }
        }

        public static void LogExceptionWithParameters(this IGameStoreLogger logger, Exception e)
        {
            var stackTrace = new StackTrace(e, true);
            foreach (var frame in stackTrace.GetFrames())
            {
                if (!(frame is null) && string.IsNullOrEmpty(frame.GetFileName()))
                {
                    var parametersList = frame.GetMethod().GetParameters().Select(x => string.Join(':', x.Name)).ToList();
                    string parameters = parametersList.Count != 0 ? string.Join(',', parametersList) : "No parameters";
                    string message = string.IsNullOrEmpty(e.Message) ? "No message" : e.Message;

                    logger.Error(
                        $"Message: {message}; " +
                        $"Type: {frame.GetType().Name}; " +
                        $"File:{frame.GetFileName()}; " +
                        $"Method:{frame.GetMethod().Name}; " +
                        $"Parameters {parameters}" +
                        $"Line: {frame.GetFileLineNumber()}; ");
                }
            }
        }
    }
}

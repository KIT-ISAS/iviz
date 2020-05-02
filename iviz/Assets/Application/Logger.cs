
using System.Runtime.CompilerServices;

namespace Iviz.App
{
    public static class Logger
    {
        public static void Info<T>(T t, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            UnityEngine.Debug.Log(t);
            ConnectionManager.Instance.LogMessage(ConnectionManager.LogLevel.Debug, t.ToString(), file, line);
        }

        public static void Error<T>(T t, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            UnityEngine.Debug.LogError(t);
            ConnectionManager.Instance.LogMessage(ConnectionManager.LogLevel.Error, t.ToString(), file, line);
        }

        public static void Warn<T>(T t, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            UnityEngine.Debug.LogWarning(t);
            ConnectionManager.Instance.LogMessage(ConnectionManager.LogLevel.Warn, t.ToString(), file, line);
        }

        public static void Debug<T>(T t, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            UnityEngine.Debug.Log(t);
            ConnectionManager.Instance.LogMessage(ConnectionManager.LogLevel.Debug, t.ToString(), file, line);
        }
    }
}

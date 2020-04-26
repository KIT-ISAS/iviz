
namespace Iviz.App
{
    public static class Logger
    {
        public static void Info<T>(T t)
        {
            UnityEngine.Debug.Log(t);
            ConnectionManager.Instance.LogMessage(ConnectionManager.LogLevel.Debug, t.ToString());
        }

        public static void Error<T>(T t)
        {
            UnityEngine.Debug.LogError(t);
            ConnectionManager.Instance.LogMessage(ConnectionManager.LogLevel.Error, t.ToString());
        }

        public static void Warn<T>(T t)
        {
            UnityEngine.Debug.LogWarning(t);
            ConnectionManager.Instance.LogMessage(ConnectionManager.LogLevel.Warn, t.ToString());
        }

        public static void Debug<T>(T t)
        {
            UnityEngine.Debug.Log(t);
            ConnectionManager.Instance.LogMessage(ConnectionManager.LogLevel.Debug, t.ToString());
        }
    }
}

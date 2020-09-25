using System.Threading.Tasks;

namespace Iviz.XmlRpc
{
    public static class TaskUtils
    {
        public static async Task<bool> WaitFor<T>(this Task<T> task, int timeout) where T : class
        {
            async Task<T> Timeout()
            {
                await Task.Delay(timeout);
                return null;
            }

            Task<T> result = await Task.WhenAny(task, Timeout());
            return result == task;
        }
        
        public static async Task<bool> WaitFor(this Task task, int timeout)
        {
            Task result = await Task.WhenAny(task, Task.Delay(timeout));
            return result == task;
        }        
    }
}
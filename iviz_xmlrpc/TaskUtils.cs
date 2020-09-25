using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Iviz.XmlRpc
{
    public static class TaskUtils
    {
        public static async Task<bool> WaitFor<T>(this Task<T> task, int timeout) where T : class
        {
            async Task<T> Timeout()
            {
                await Task.Delay(timeout).Caf();
                return null;
            }

            Task<T> result = await Task.WhenAny(task, Timeout()).Caf();
            return result == task;
        }
        
        public static async Task<bool> WaitFor(this Task task, int timeout)
        {
            Task result = await Task.WhenAny(task, Task.Delay(timeout)).Caf();
            return result == task;
        }

        public static ConfiguredTaskAwaitable Caf(this Task task)
        {
            return task.ConfigureAwait(false);
        }
        
        public static ConfiguredTaskAwaitable<T> Caf<T>(this Task<T> task)
        {
            return task.ConfigureAwait(false);
        }
    }
}
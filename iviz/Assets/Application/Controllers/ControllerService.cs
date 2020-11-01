using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Iviz.App;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;

namespace Iviz.Controllers
{
    public class ControllerService
    {
        static RosConnection Connection => ConnectionManager.Connection;
        
        static readonly SemaphoreSlim signal = new SemaphoreSlim(0, 1);

        public ControllerService()
        {
            Connection.AdvertiseService<AddModuleFromTopic>("add_module_from_topic", Callback);
        }

        static void Callback(AddModuleFromTopic srv)
        {
            var (success, message) = TryCreateModuleFromTopic(srv.Request.Topic);
            srv.Response.Success = success;
            srv.Response.Message = message;
        }

        static (bool success, string message) TryCreateModuleFromTopic(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                return (false, "Invalid topic name");
            }

            if (ModuleListPanel.Instance.ModuleDatas.Any(module => module.Topic == topic))
            {
                return (true, "A module with that topic already exists");
            }

            ReadOnlyCollection<BriefTopicInfo> topics = Connection.GetSystemPublishedTopics(RequestType.CachedOnly);
            string type = topics.FirstOrDefault(topicInfo => topicInfo.Topic == topic)?.Type;

            if (type == null)
            {
                topics = Connection.GetSystemPublishedTopics(RequestType.WaitForRequest);
                type = topics.FirstOrDefault(topicInfo => topicInfo.Topic == topic)?.Type;
            }

            if (type == null)
            {
                return (false, $"Failed to find topic '{topic}'");
            }


            if (!Resource.ResourceByRosMessageType.TryGetValue(type, out Resource.Module resource))
            {
                return (false, $"Type '{type}' is unsupported");
            }

            GameThread.Post(() =>
            {
                ModuleListPanel.Instance.CreateModule(resource, topic, type);
                signal.Release();
            });

            signal.Wait();
            return (true, "Ok");
        }
    }
}
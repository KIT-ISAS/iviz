using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.App;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using Newtonsoft.Json;

namespace Iviz.Controllers
{
    public class ControllerService
    {
        static RosConnection Connection => ConnectionManager.Connection;

        public ControllerService()
        {
            Connection.AdvertiseService<AddModuleFromTopic>("add_module_from_topic", AddModuleCallback);
            Connection.AdvertiseService<UpdateModule>("update_module", UpdateModuleCallback);
        }

        static void AddModuleCallback(AddModuleFromTopic srv)
        {
            var (id, success, message ) = TryCreateModuleFromTopic(srv.Request.Topic);
            srv.Response.Success = success;
            srv.Response.Message = message;
            srv.Response.Id = id ?? "";
        }

        static (string id, bool success, string message) TryCreateModuleFromTopic(string topic)
        {
            (string id, bool success, string message) result = default;

            if (string.IsNullOrWhiteSpace(topic))
            {
                result.message = "Invalid topic name";
                return result;
            }

            if (ModuleListPanel.Instance.ModuleDatas.Any(module => module.Topic == topic))
            {
                result.message = "A module with that topic already exists";
                result.success = true;
                return result;
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
                return ("", false, $"Failed to find topic '{topic}'");
            }


            if (!Resource.ResourceByRosMessageType.TryGetValue(type, out Resource.Module resource))
            {
                result.message = $"Type '{type}' is unsupported";
            }

            CancellationTokenSource ts = new CancellationTokenSource();
            
            string id = null;
            GameThread.Post(() =>
            {
                try
                {
                    id = ModuleListPanel.Instance.CreateModule(resource, topic, type).Configuration.Id;
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.message = $"An exception was raised: {e.Message}";
                }
                finally
                {
                    ts.Cancel();
                }
            });

            try
            {
                Task.Delay(5000, ts.Token);
            }
            catch (OperationCanceledException) { }

            return ts.IsCancellationRequested ? result : ("", false, "Request timed out!");
        }

        static void UpdateModuleCallback(UpdateModule srv)
        {
            var (success, message) = TryUpdateModule(srv.Request.Id, srv.Request.Fields, srv.Request.Config);
            srv.Response.Success = success;
            srv.Response.Message = message;
        }

        static (bool success, string message) TryUpdateModule(string id, string[] fields, string config)
        {
            (bool success, string message) result = default;

            if (string.IsNullOrWhiteSpace(id))
            {
                result.message = "Empty topic name";
                return result;
            }

            if (string.IsNullOrWhiteSpace(config))
            {
                result.message = "Empty configuration";
                return result;
            }

            CancellationTokenSource ts = new CancellationTokenSource();

            GameThread.Post(() =>
            {
                try
                {
                    ModuleData module =
                        ModuleListPanel.Instance.ModuleDatas.FirstOrDefault(mod => mod.Configuration.Id == id);

                    if (module == null)
                    {
                        result.success = false;
                        result.message = "There is no module with that id";
                        return;
                    }

                    module.UpdateConfiguration(config, fields);
                }
                catch (JsonException e)
                {
                    result.success = false;
                    result.message = $"Error parsing JSON config: {e.Message}";
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.message = $"An exception was raised: {e.Message}";
                }
                finally
                {
                    ts.Cancel();
                }
            });

            try
            {
                Task.Delay(5000, ts.Token);
            }
            catch (OperationCanceledException) { }

            return ts.IsCancellationRequested ? result : (false, "Request timed out!");
        }
    }
}
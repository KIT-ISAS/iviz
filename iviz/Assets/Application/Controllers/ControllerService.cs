using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Iviz.App;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    public class ControllerService
    {
        [NotNull] static RosConnection Connection => ConnectionManager.Connection;

        static readonly (Resource.ModuleType module, string name)[] ModuleNames =
            typeof(Resource.ModuleType).GetEnumValues()
                .Cast<Resource.ModuleType>()
                .Select(module => (module, module.ToString()))
                .ToArray();


        public ControllerService()
        {
            Connection.AdvertiseService<AddModule>("add_module", AddModuleCallback);
            Connection.AdvertiseService<AddModuleFromTopic>("add_module_from_topic", AddModuleFromTopicCallback);
            Connection.AdvertiseService<UpdateModule>("update_module", UpdateModuleCallback);
            Connection.AdvertiseService<GetModules>("get_modules", GetModulesCallback);
        }

        static void AddModuleCallback([NotNull] AddModule srv)
        {
            var (id, success, message) = TryCreateModule(srv.Request.ModuleType, srv.Request.Id);
            srv.Response.Success = success;
            srv.Response.Message = message;
            srv.Response.Id = id ?? "";
        }

        static Resource.ModuleType ModuleTypeFromString(string moduleName)
        {
            return ModuleNames.FirstOrDefault(entry => entry.name == moduleName).module;
        }

        static (string id, bool success, string message) TryCreateModule([NotNull] string moduleTypeStr,
            [NotNull] string requestedId)
        {
            (string id, bool success, string message) result = ("", false, "");

            if (string.IsNullOrWhiteSpace(moduleTypeStr))
            {
                result.message = "EE Invalid module type";
                return result;
            }

            Resource.ModuleType moduleType = ModuleTypeFromString(moduleTypeStr);

            if (moduleType == Resource.ModuleType.Invalid)
            {
                result.message = "EE Invalid module type";
                return result;
            }

            SemaphoreSlim signal = new SemaphoreSlim(0, 1);

            GameThread.Post(() =>
            {
                try
                {
                    if (requestedId.Length != 0)
                    {
                        ModuleData module =
                            ModuleListPanel.Instance.ModuleDatas.FirstOrDefault(
                                data => data.Configuration.Id == requestedId);
                        if (module != null)
                        {
                            if (module.ModuleType != moduleType)
                            {
                                result.message = "EE Another module of the same name already exists, but it has type " +
                                                 module.ModuleType;
                            }
                            else
                            {
                                result.success = true;
                                result.message = "** Module already exists";
                            }

                            return;
                        }
                    }
                    
                    var moduleData = ModuleListPanel.Instance.CreateModule(moduleType,
                        requestedId: requestedId.Length != 0 ? requestedId : null);
                    result.id = moduleData.Configuration.Id;
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.message = $"EE An exception was raised: {e.Message}";
                }
                finally
                {
                    signal.Release();
                }
            });

            return signal.Wait(2000) ? result : ("", false, "EE Request timed out!");
        }

        static void AddModuleFromTopicCallback([NotNull] AddModuleFromTopic srv)
        {
            var (id, success, message) = TryCreateModuleFromTopic(srv.Request.Topic);
            srv.Response.Success = success;
            srv.Response.Message = message;
            srv.Response.Id = id ?? "";
        }

        static (string id, bool success, string message) TryCreateModuleFromTopic([NotNull] string topic)
        {
            (string id, bool success, string message) result = ("", false, "");

            if (string.IsNullOrWhiteSpace(topic))
            {
                result.message = "EE Invalid topic name";
                return result;
            }

            if (ModuleListPanel.Instance.ModuleDatas.Any(module => module.Topic == topic))
            {
                result.message = "EE A module with that topic already exists";
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
                return ("", false, $"EE Failed to find topic '{topic}'");
            }


            if (!Resource.ResourceByRosMessageType.TryGetValue(type, out Resource.ModuleType resource))
            {
                result.message = $"EE Type '{type}' is unsupported";
            }

            SemaphoreSlim signal = new SemaphoreSlim(0, 1);

            GameThread.Post(() =>
            {
                try
                {
                    result.id = ModuleListPanel.Instance.CreateModule(resource, topic, type).Configuration.Id;
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.message = $"EE An exception was raised: {e.Message}";
                }
                finally
                {
                    signal.Release();
                }
            });

            return signal.Wait(2000) ? result : ("", false, "EE Request timed out!");
        }

        static void UpdateModuleCallback([NotNull] UpdateModule srv)
        {
            var (success, message) = TryUpdateModule(srv.Request.Id, srv.Request.Fields, srv.Request.Config);
            srv.Response.Success = success;
            srv.Response.Message = message;
        }

        static (bool success, string message) TryUpdateModule([NotNull] string id, string[] fields, string config)
        {
            (bool success, string message) result = (false, "");

            if (string.IsNullOrWhiteSpace(id))
            {
                result.message = "EE Empty configuration id!";
                return result;
            }

            if (string.IsNullOrWhiteSpace(config))
            {
                result.message = "EE Empty configuration text!";
                return result;
            }

            SemaphoreSlim signal = new SemaphoreSlim(0, 1);

            GameThread.Post(() =>
            {
                try
                {
                    ModuleData module =
                        ModuleListPanel.Instance.ModuleDatas.FirstOrDefault(data => data.Configuration.Id == id);

                    if (module == null)
                    {
                        result.success = false;
                        result.message = "EE There is no module with that id";
                        return;
                    }

                    module.UpdateConfiguration(config, fields);
                    result.success = true;
                }
                catch (JsonException e)
                {
                    result.success = false;
                    result.message = $"EE Error parsing JSON config: {e.Message}";
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.message = $"EE An exception was raised: {e.Message}";
                }
                finally
                {
                    signal.Release();
                }
            });

            return signal.Wait(2000) ? result : (false, "EE Request timed out!");
        }

        static void GetModulesCallback([NotNull] GetModules srv)
        {
            string[] result = GetModules();
            srv.Response.Configs = result;
        }

        [NotNull]
        static string[] GetModules()
        {
            SemaphoreSlim signal = new SemaphoreSlim(0, 1);

            string[] result = default;
            GameThread.Post(() =>
            {
                try
                {
                    IConfiguration[] configurations =
                        ModuleListPanel.Instance.ModuleDatas.Select(data => data.Configuration).ToArray();
                    result = configurations.Select(JsonConvert.SerializeObject).ToArray();
                }
                catch (JsonException e)
                {
                    Logger.External(LogLevel.Error,
                        $"ControllerService: Unexpected JSON exception in GetModules: {e.Message}");
                    Debug.LogWarning(e);
                }
                catch (Exception e)
                {
                    Logger.External(LogLevel.Error,
                        $"ControllerService: Unexpected exception in GetModules: {e.Message}");
                    Debug.LogWarning(e);
                }
                finally
                {
                    signal.Release();
                }
            });

            if (!signal.Wait(2000))
            {
                Logger.External(LogLevel.Error, "ControllerService: Unexpected timeout in GetModules");
            }

            return result;
        }
    }
}
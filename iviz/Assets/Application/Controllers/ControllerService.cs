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
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    public class ControllerService
    {
        [NotNull] static RosConnection Connection => ConnectionManager.Connection;

        public ControllerService()
        {
            Connection.AdvertiseService<AddModule>("add_module", AddModuleCallback);
            Connection.AdvertiseService<AddModuleFromTopic>("add_module_from_topic", AddModuleFromTopicCallback);
            Connection.AdvertiseService<UpdateModule>("update_module", UpdateModuleCallback);
            Connection.AdvertiseService<GetModules>("get_modules", GetModulesCallback);
        }

        static void AddModuleCallback([NotNull] AddModule srv)
        {
            var (id, success, message) = TryCreateModule(srv.Request.ModuleType);
            srv.Response.Success = success;
            srv.Response.Message = message;
            srv.Response.Id = id ?? "";
        }

        static Resource.ModuleType ModuleTypeFromString(string moduleType)
        {
            switch (moduleType)
            {
                case nameof(Resource.ModuleType.Grid):
                    return Resource.ModuleType.Grid;
                case nameof(Resource.ModuleType.TF):
                    return Resource.ModuleType.TF;
                case nameof(Resource.ModuleType.PointCloud):
                    return Resource.ModuleType.PointCloud;
                case nameof(Resource.ModuleType.Image):
                    return Resource.ModuleType.Image;
                case nameof(Resource.ModuleType.Marker):
                    return Resource.ModuleType.Marker;
                case nameof(Resource.ModuleType.InteractiveMarker):
                    return Resource.ModuleType.InteractiveMarker;
                case nameof(Resource.ModuleType.JointState):
                    return Resource.ModuleType.JointState;
                case nameof(Resource.ModuleType.DepthCloud):
                    return Resource.ModuleType.DepthCloud;
                case nameof(Resource.ModuleType.LaserScan):
                    return Resource.ModuleType.LaserScan;
                case nameof(Resource.ModuleType.AugmentedReality):
                    return Resource.ModuleType.AugmentedReality;
                case nameof(Resource.ModuleType.Magnitude):
                    return Resource.ModuleType.Magnitude;
                case nameof(Resource.ModuleType.OccupancyGrid):
                    return Resource.ModuleType.OccupancyGrid;
                case nameof(Resource.ModuleType.Joystick):
                    return Resource.ModuleType.Joystick;
                case nameof(Resource.ModuleType.Path):
                    return Resource.ModuleType.Path;
                case nameof(Resource.ModuleType.GridMap):
                    return Resource.ModuleType.GridMap;
                case nameof(Resource.ModuleType.Robot):
                    return Resource.ModuleType.Robot;
                default:
                    return Resource.ModuleType.Invalid;
            }            
        } 
        
        static (string id, bool success, string message) TryCreateModule([NotNull] string moduleType)
        {
            (string id, bool success, string message) result = ("", false, "");

            if (string.IsNullOrWhiteSpace(moduleType))
            {
                result.message = "Invalid module type";
                return result;
            }

            Resource.ModuleType resource = ModuleTypeFromString(moduleType);

            if (resource == Resource.ModuleType.Invalid)
            {
                result.message = "Invalid module type";
                result.success = false;
                return result;
            }

            SemaphoreSlim signal = new SemaphoreSlim(0, 1);

            GameThread.Post(() =>
            {
                try
                {
                    result.id = ModuleListPanel.Instance.CreateModule(resource).Configuration.Id;
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.message = $"An exception was raised: {e.Message}";
                }
                finally
                {
                    signal.Release();
                }
            });

            return signal.Wait(5000) ? result : ("", false, "Request timed out!");
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


            if (!Resource.ResourceByRosMessageType.TryGetValue(type, out Resource.ModuleType resource))
            {
                result.message = $"Type '{type}' is unsupported";
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
                    result.message = $"An exception was raised: {e.Message}";
                }
                finally
                {
                    signal.Release();
                }
            });

            return signal.Wait(5000) ? result : ("", false, "Request timed out!");
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
                result.message = "Empty configuration id!";
                return result;
            }

            if (string.IsNullOrWhiteSpace(config))
            {
                result.message = "Empty configuration text!";
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
                        result.message = "There is no module with that id";
                        return;
                    }

                    module.UpdateConfiguration(config, fields);
                    result.success = true;
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
                    signal.Release();
                }
            });

            return signal.Wait(5000) ? result : (false, "Request timed out!");
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

            if (!signal.Wait(5000))
            {
                Logger.External(LogLevel.Error, "ControllerService: Unexpected timeout in GetModules");
            }

            return result;
        }
    }
}
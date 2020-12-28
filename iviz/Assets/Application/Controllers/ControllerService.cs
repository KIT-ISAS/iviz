using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.App;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Logger = Iviz.Core.Logger;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;

namespace Iviz.Controllers
{
    public sealed class ControllerService
    {
        const int DefaultTimeoutInMs = 4500;

        [NotNull] static RoslibConnection Connection => ConnectionManager.Connection;
        [NotNull] static IReadOnlyList<ModuleData> ModuleDatas => ModuleListPanel.Instance.ModuleDatas;

        static readonly (Resource.ModuleType module, string name)[] ModuleNames =
            typeof(Resource.ModuleType).GetEnumValues()
                .Cast<Resource.ModuleType>()
                .Select(module => (module, module.ToString()))
                .ToArray();


        public ControllerService()
        {
            Connection.AdvertiseService<AddModule>("add_module", AddModuleAsync);
            Connection.AdvertiseService<AddModuleFromTopic>("add_module_from_topic", AddModuleFromTopicAsync);
            Connection.AdvertiseService<UpdateModule>("update_module", UpdateModuleAsync);
            Connection.AdvertiseService<GetModules>("get_modules", GetModulesAsync);
            Connection.AdvertiseService<SetFixedFrame>("set_fixed_frame", SetFixedFrameAsync);
            Connection.AdvertiseService<GetFramePose>("get_frame_poses", GetFramePoseAsync);
        }

        static async Task AddModuleAsync([NotNull] AddModule srv)
        {
            var (id, success, message) = await TryAddModuleAsync(srv.Request.ModuleType, srv.Request.Id);
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
            srv.Response.Id = id ?? "";
        }

        static Resource.ModuleType ModuleTypeFromString(string moduleName)
        {
            return ModuleNames.FirstOrDefault(tuple => tuple.name == moduleName).module;
        }

        static async Task<(string id, bool success, string message)> TryAddModuleAsync([NotNull] string moduleTypeStr,
            [NotNull] string requestedId)
        {
            (string id, bool success, string message) result = default;

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

            if (moduleType != Resource.ModuleType.Grid &&
                moduleType != Resource.ModuleType.DepthCloud &&
                moduleType != Resource.ModuleType.AugmentedReality &&
                moduleType != Resource.ModuleType.Joystick &&
                moduleType != Resource.ModuleType.Robot)
            {
                result.message = "EE Cannot create module of that type, use AddModuleFromTopic instead";
                return result;
            }

            ModuleData moduleData;
            if (requestedId.Length != 0 &&
                (moduleData = ModuleDatas.FirstOrDefault(data => data.Configuration.Id == requestedId)) != null)
            {
                if (moduleData.ModuleType != moduleType)
                {
                    result.message =
                        $"EE Another module of the same id already exists, but it has type {moduleData.ModuleType}";
                }
                else
                {
                    result.success = true;
                    result.id = requestedId;
                    result.message = "** Module already exists";
                }

                return result;
            }


            SemaphoreSlim signal = new SemaphoreSlim(0);

            GameThread.Post(() =>
            {
                try
                {
                    Logger.External($"Creating module of type {moduleType}");
                    var newModuleData = ModuleListPanel.Instance.CreateModule(moduleType,
                        requestedId: requestedId.Length != 0 ? requestedId : null);
                    result.id = newModuleData.Configuration.Id;
                    result.success = true;
                    Logger.External("Done!");
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
            return await signal.WaitAsync(DefaultTimeoutInMs) ? result : ("", false, "EE Request timed out!");
        }

        static async Task AddModuleFromTopicAsync([NotNull] AddModuleFromTopic srv)
        {
            var (id, success, message) = await TryAddModuleFromTopicAsync(srv.Request.Topic, srv.Request.Id);
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
            srv.Response.Id = id ?? "";
        }

        static async Task<(string id, bool success, string message)> TryAddModuleFromTopicAsync([NotNull] string topic,
            [NotNull] string requestedId)
        {
            (string id, bool success, string message) result = default;
            if (string.IsNullOrWhiteSpace(topic))
            {
                result.message = "EE Invalid topic name";
                return result;
            }

            var data = ModuleDatas.FirstOrDefault(module => module.Topic == topic);
            if (data != null)
            {
                result.message = requestedId == data.Configuration.Id
                    ? "** Module already exists"
                    : "WW A module with that topic but different id already exists";
                result.id = data.Configuration.Id;
                result.success = true;
                return result;
            }

            if (requestedId.Length != 0 && ModuleDatas.Any(module => module.Configuration.Id == requestedId))
            {
                result.message = "EE There is already another module with that id";
                return result;
            }

            ReadOnlyCollection<BriefTopicInfo> topics = Connection.GetSystemTopicTypes(RequestType.CachedOnly);

            string type = topics.FirstOrDefault(topicInfo => topicInfo.Topic == topic)?.Type;
            if (type == null)
            {
                topics = await Connection.GetSystemTopicTypesAsync(DefaultTimeoutInMs);
                if (topics == null)
                {
                    return ("", false, "EE Failed to retrieve updated list of topics due to timeout");
                }

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

            SemaphoreSlim signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    //Logger.Debug(Time.time + ": Adding topic " + topic);
                    result.id = ModuleListPanel.Instance.CreateModule(resource, topic, type,
                        requestedId: requestedId.Length != 0 ? requestedId : null).Configuration.Id;
                    result.success = true;
                    //Logger.Debug(Time.time + ": Done!");
                }
                catch (Exception e)
                {
                    result.message = $"EE An exception was raised: {e.Message}";
                    Logger.Error("Exception raised in TryAddModuleFromTopicAsync", e);
                }
                finally
                {
                    signal.Release();
                }
            });

            return await signal.WaitAsync(DefaultTimeoutInMs) ? result : ("", false, "EE Request timed out!");
        }

        static async Task UpdateModuleAsync([NotNull] UpdateModule srv)
        {
            var (success, message) = await TryUpdateModuleAsync(srv.Request.Id, srv.Request.Fields, srv.Request.Config);
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
        }

        static async Task<(bool success, string message)> TryUpdateModuleAsync([NotNull] string id, string[] fields,
            string config)
        {
            (bool success, string message) result = default;
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

            SemaphoreSlim signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                Logger.Debug(Time.time + ": Updating module!");

                try
                {
                    ModuleData module = ModuleDatas.FirstOrDefault(data => data.Configuration.Id == id);
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
                    Logger.External("Error:", e);
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.message = $"EE An exception was raised: {e.Message}";
                    Logger.External("Error:", e);
                }
                finally
                {
                    Logger.Debug(Time.time + ": Done!");
                    signal.Release();
                }
            });
            return await signal.WaitAsync(DefaultTimeoutInMs) ? result : (false, "EE Request timed out!");
        }

        static async Task GetModulesAsync([NotNull] GetModules srv)
        {
            string[] result = await GetModulesAsync();
            srv.Response.Configs = result;
        }

        [ItemNotNull]
        static async Task<string[]> GetModulesAsync()
        {
            SemaphoreSlim signal = new SemaphoreSlim(0);
            string[] result = Array.Empty<string>();
            GameThread.Post(() =>
            {
                try
                {
                    IConfiguration[] configurations = ModuleDatas.Select(data => data.Configuration).ToArray();
                    result = configurations.Select(JsonConvert.SerializeObject).ToArray();
                }
                catch (JsonException e)
                {
                    Logger.Error("ControllerService: Unexpected JSON exception in GetModules", e);
                }
                catch (Exception e)
                {
                    Logger.Error("ControllerService: Unexpected exception in GetModules", e);
                }
                finally
                {
                    signal.Release();
                }
            });
            if (!await signal.WaitAsync(DefaultTimeoutInMs))
            {
                Logger.Error("Timeout in GetModules");
            }

            return result;
        }

        static async Task SetFixedFrameAsync([NotNull] SetFixedFrame srv)
        {
            (bool success, string message) = await TrySetFixedFrame(srv.Request.Id);
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
        }

        static async Task<(bool success, string message)> TrySetFixedFrame(string id)
        {
            (bool success, string message) result = default;

            SemaphoreSlim signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    TfListener.FixedFrameId = id;
                }
                finally
                {
                    signal.Release();
                }
            });

            if (!await signal.WaitAsync(DefaultTimeoutInMs))
            {
                Logger.Error("ControllerService: Unexpected timeout in TrySetFixedFrame");
                return result;
            }

            return (true, "");
        }
        
        static async Task GetFramePoseAsync([NotNull] GetFramePose srv)
        {
            (bool[] success, Pose[] poses) = await TryGetFramePoseAsync(srv.Request.Frames);
            srv.Response.Poses = poses;
            srv.Response.IsValid = success;
        }

        static async Task<(bool[] success, Pose[] poses)> TryGetFramePoseAsync(string[] ids)
        {
            SemaphoreSlim signal = new SemaphoreSlim(0);
            bool[] success = null;
            Pose[] poses = null;

            GameThread.Post(() =>
            {
                try
                {
                    List<bool> successList = new List<bool>();
                    List<Pose> posesList = new List<Pose>();
                    foreach (string id in ids)
                    {
                        if (!TfListener.TryGetFrame(id, out var frame))
                        {
                            successList.Add(false);
                            posesList.Add(Pose.Identity);
                        }
                        else
                        {
                            successList.Add(true);
                            posesList.Add(frame.WorldPose.Unity2RosPose());
                        }
                    }

                    success = successList.ToArray();
                    poses = posesList.ToArray();
                }
                finally
                {
                    signal.Release();
                }
            });

            if (!await signal.WaitAsync(DefaultTimeoutInMs))
            {
                Logger.Error("ControllerService: Unexpected timeout in TryGetFramePoseAsync");
                return (new bool[ids.Length], new Pose[ids.Length]);
            }

            return (success, poses);
        }        
    }
}
#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BigGustave;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays.XR;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.Roscpp;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using LaunchDialog = Iviz.Msgs.IvizCommonMsgs.LaunchDialog;
using Logger = Iviz.Msgs.Roscpp.Logger;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;
using UpdateRobot = Iviz.Msgs.IvizCommonMsgs.UpdateRobot;

namespace Iviz.Controllers
{
    internal static class ControllerService
    {
        const int DefaultTimeoutInMs = 5000;

        static RoslibConnection Connection => RosManager.Connection;
        static IEnumerable<ModuleData> ModuleDatas => ModuleListPanel.Instance.ModuleDatas;

        static readonly Dictionary<ModuleType, string> ModuleNames =
            typeof(ModuleType).GetEnumValues()
                .Cast<ModuleType>()
                .Select(module => (key: module, value: module.ToString()))
                .ToDictionary(entry => entry.key, entry => entry.value);

        static readonly Dictionary<string, ModuleType> ModuleTypeFromName =
            ModuleNames.ToDictionary(pair => pair.Value, pair => pair.Key);

        static readonly string[] LogLevelNames = { "debug", "info", "warn", "error", "fatal" };

        static string TryGetName(ModuleType type) =>
            ModuleNames.TryGetValue(type, out string name) ? name : "[" + (int)type + "]";

        public static void Start()
        {
            var connection = RosManager.Connection;

            connection.AdvertiseService<GetLoggers>("~get_loggers", GetLoggers);
            connection.AdvertiseService<SetLoggerLevel>("~set_logger_level", SetLoggerLevel);

            connection.AdvertiseService<AddModule>("~add_module", AddModuleAsync);
            connection.AdvertiseService<AddModuleFromTopic>("~add_module_from_topic", AddModuleFromTopicAsync);
            connection.AdvertiseService<UpdateModule>("~update_module", UpdateModuleAsync);
            connection.AdvertiseService<ResetModule>("~reset_module", ResetModuleAsync);
            connection.AdvertiseService<GetModules>("~get_modules", GetModulesAsync);
            connection.AdvertiseService<SetFixedFrame>("~set_fixed_frame", SetFixedFrameAsync);
            //connection.AdvertiseService<GetFramePose>("get_frame_poses", GetFramePoseAsync);
            //connection.AdvertiseService<GetCaptureResolutions>("get_capture_resolutions", GetCaptureResolutions);
            //connection.AdvertiseService<StartCapture>("start_capture", StartCaptureAsync);
            //connection.AdvertiseService<StopCapture>("stop_capture", StopCaptureAsync);
            //connection.AdvertiseService<CaptureScreenshot>("capture_screenshot", CaptureScreenshotAsync);

            //connection.AdvertiseService<UpdateRobot>("update_robot", UpdateRobotAsync);
            //connection.AdvertiseService<LaunchDialog>("launch_dialog", LaunchDialogAsync);
        }

        static void GetLoggers(GetLoggers srv)
        {
            srv.Response.Loggers = new[]
            {
                new Logger("ros.iviz",
                    LogLevelNames[ConsoleDialogData.IndexFromLevel(RosManager.Logger.MinLogLevel)])
            };
        }

        static void SetLoggerLevel(SetLoggerLevel srv)
        {
            if (srv.Request.Logger != "ros.iviz")
            {
                return;
            }

            foreach (int i in ..LogLevelNames.Length)
            {
                if (LogLevelNames[i] == srv.Request.Level)
                {
                    RosManager.Logger.MinLogLevel = ConsoleDialogData.LevelFromIndex(i);
                    break;
                }
            }
        }

        static async ValueTask<T?> AwaitAndLog<T>(this ValueTask<T> task, string name)
        {
            try
            {
                return await task;
            }
            catch (Exception e)
            {
                RosLogger.Error($"{nameof(ControllerService)}: Error in {name}!", e);
                return default;
            }
        }

        static async ValueTask AddModuleAsync(AddModule srv)
        {
            var (id, success, message) = await TryAddModuleAsync(srv.Request.ModuleType, srv.Request.Id)
                .AwaitAndLog(nameof(AddModuleAsync));
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
            srv.Response.Id = id ?? "";
        }

        static async ValueTask<(string id, bool success, string? message)>
            TryAddModuleAsync(string moduleTypeStr, string requestedId)
        {
            (string id, bool success, string? message) result = default;

            if (string.IsNullOrWhiteSpace(moduleTypeStr))
            {
                result.message = "Invalid module type";
                return result;
            }

            if (!ModuleTypeFromName.TryGetValue(moduleTypeStr, out var moduleType)
                || moduleType == ModuleType.Invalid)
            {
                result.message = "Invalid module type";
                return result;
            }

            if (moduleType is not
                (ModuleType.Grid
                or ModuleType.DepthCloud
                or ModuleType.AR
                or ModuleType.Joystick
                or ModuleType.Robot))
            {
                result.message = $"Cannot create module of that type, use {nameof(AddModuleFromTopic)} instead";
                return result;
            }

            ModuleData? moduleData;
            if (requestedId.Length != 0 &&
                (moduleData = ModuleDatas.FirstOrDefault(data => data.Configuration.Id == requestedId)) != null)
            {
                if (moduleData.ModuleType != moduleType)
                {
                    result.message = "Another module of the same id already exists, " +
                                     $"but it has type {TryGetName(moduleData.ModuleType)}";
                }
                else
                {
                    result.success = true;
                    result.id = requestedId;
                    result.message = "** Module already exists";
                }

                return result;
            }

            using var signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    RosLogger.Debug($"{nameof(ControllerService)}: Creating module of type {TryGetName(moduleType)}");
                    var newModuleData = ModuleListPanel.Instance.CreateModule(moduleType,
                        requestedId: requestedId.Length != 0 ? requestedId : null);
                    result.id = newModuleData.Configuration.Id;
                    result.success = true;
                    RosLogger.Debug($"{nameof(ControllerService)}: Done!");
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
            return await signal.WaitAsync(DefaultTimeoutInMs) ? result : ("", false, "Request timed out!");
        }

        static async ValueTask AddModuleFromTopicAsync(AddModuleFromTopic srv)
        {
            var (id, success, message) =
                await TryAddModuleFromTopicAsync(srv.Request.Topic, srv.Request.Id)
                    .AwaitAndLog(nameof(AddModuleFromTopicAsync));
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
            srv.Response.Id = id ?? "";
        }

        static async ValueTask<(string id, bool success, string? message)>
            TryAddModuleFromTopicAsync(string topic, string requestedId)
        {
            (string id, bool success, string? message) result = default;
            if (string.IsNullOrWhiteSpace(topic))
            {
                result.message = "Invalid topic name";
                return result;
            }

            var data = ModuleDatas.OfType<ListenerModuleData>().FirstOrDefault(module => module.Topic == topic);
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
                result.message = "There is already another module with that id";
                return result;
            }

            string? GetCachedPublishedType() =>
                Connection.GetSystemPublishedTopicTypes(RequestType.CachedOnly)
                    .FirstOrDefault(topicInfo => topicInfo.Topic == topic)?.Type;

            async ValueTask<string?> GetPublishedTypeFromServer() =>
                (await Connection.GetSystemPublishedTopicTypesAsync(DefaultTimeoutInMs))
                .FirstOrDefault(topicInfo => topicInfo.Topic == topic)?.Type;

            string? type = GetCachedPublishedType() ?? await GetPublishedTypeFromServer();
            if (type == null)
            {
                return ("", false, $"Failed to find topic '{topic}'");
            }

            if (!Resource.ResourceByRosMessageType.TryGetValue(type, out ModuleType resource))
            {
                return ("", false, $"Type '{type}' is unsupported");
            }

            using var signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    result.id = ModuleListPanel.Instance.CreateModule(resource, topic, type,
                        requestedId: requestedId.Length != 0 ? requestedId : null).Configuration.Id;
                    result.message = "";
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.message = $"An exception was raised: {e.Message}";
                    RosLogger.Error($"{nameof(ControllerService)}: Failed to create module for topic '{topic}'", e);
                }
                finally
                {
                    signal.Release();
                }
            });

            return await signal.WaitAsync(DefaultTimeoutInMs) ? result : ("", false, "Request timed out!");
        }

        static async ValueTask UpdateModuleAsync(UpdateModule srv)
        {
            var (success, message) = await TryUpdateModuleAsync(srv.Request.Id, srv.Request.Fields, srv.Request.Config)
                .AwaitAndLog(nameof(UpdateModuleAsync));
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
        }

        static async ValueTask<(bool success, string? message)>
            TryUpdateModuleAsync(string id, string[] fields, string config)
        {
            (bool success, string? message) result = default;
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

            ModuleType moduleType;
            string[] validatedFields;
            try
            {
                var moduleInfo = JsonConvert.DeserializeObject<GenericModule>(config);
                if (moduleInfo.ModuleType is not { } type)
                {
                    result.success = false;
                    result.message = "JSON config does not contain a ModuleType field";
                    return result;
                }

                moduleType = type;

                validatedFields = fields.Length != 0 ? fields : GetFields(config);
            }
            catch (JsonException e)
            {
                result.success = false;
                result.message = $"Error parsing JSON config: {e.Message}";
                RosLogger.Error($"{nameof(ControllerService)}: Error in {nameof(TryUpdateModuleAsync)}", e);
                return result;
            }

            using var signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    var module = ModuleDatas.FirstOrDefault(data => data.Configuration.Id == id);
                    if (module == null)
                    {
                        result.success = false;
                        result.message = $"There is no module with id '{id}'";
                        return;
                    }

                    if (module.ModuleType != moduleType)
                    {
                        result.success = false;
                        result.message = $"Given ModuleType field '{ModuleNames[moduleType]}' does not match " +
                                         $"existing type '{ModuleNames[module.ModuleType]}'";
                        return;
                    }

                    module.UpdateConfiguration(config, validatedFields);
                    result.success = true;
                }
                catch (JsonException e)
                {
                    result.success = false;
                    result.message = $"Error parsing JSON config: {e.Message}";
                    RosLogger.Error($"{nameof(ControllerService)}: Error in {nameof(TryUpdateModuleAsync)}", e);
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.message = $"An exception was raised: {e.Message}";
                    RosLogger.Error($"{nameof(ControllerService)}: Error in {nameof(TryUpdateModuleAsync)}", e);
                }
                finally
                {
                    signal.Release();
                }
            });
            return await signal.WaitAsync(DefaultTimeoutInMs) ? result : (false, "Request timed out!");
        }

        sealed class GenericModule
        {
            [UsedImplicitly] public ModuleType? ModuleType { get; set; }
        }

        static string[] GetFields(string json)
        {
            return JObject.Parse(json)
                .Select((KeyValuePair<string, JToken?> pair) => pair.Key)
                .ToArray();
        }

        static async ValueTask ResetModuleAsync(ResetModule srv)
        {
            var (success, message) = await TryResetModuleAsync(srv.Request.Id).AwaitAndLog(nameof(ResetModuleAsync));
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
        }

        static async ValueTask<(bool success, string? message)> TryResetModuleAsync(string id)
        {
            (bool success, string? message) result = default;
            if (string.IsNullOrWhiteSpace(id))
            {
                result.message = "Empty configuration id!";
                return result;
            }

            using var signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    var module = ModuleDatas.FirstOrDefault(data => data.Configuration.Id == id);
                    if (module == null)
                    {
                        result.success = false;
                        result.message = "There is no module with that id";
                        return;
                    }

                    module.ResetController();
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.message = $"An exception was raised: {e.Message}";
                    RosLogger.Error($"{nameof(ControllerService)}: Error in {nameof(TryResetModuleAsync)}", e);
                }
                finally
                {
                    signal.Release();
                }
            });
            return await signal.WaitAsync(DefaultTimeoutInMs) ? result : (false, "Request timed out!");
        }

        static async ValueTask GetModulesAsync(GetModules srv)
        {
            srv.Response.Configs =
                await GetModulesAsync().AwaitAndLog(nameof(GetModulesAsync)) ?? Array.Empty<string>();
        }

        static async ValueTask<string[]> GetModulesAsync()
        {
            using var signal = new SemaphoreSlim(0);
            string[] result = Array.Empty<string>();
            GameThread.Post(() =>
            {
                try
                {
                    var configurations = ModuleDatas.Select(data => data.Configuration).ToArray();
                    result = configurations.Select(JsonConvert.SerializeObject).ToArray();
                }
                catch (JsonException e)
                {
                    RosLogger.Error(
                        $"{nameof(ControllerService)}: Unexpected JSON exception in {nameof(GetModulesAsync)}", e);
                }
                catch (Exception e)
                {
                    RosLogger.Error(
                        $"{nameof(ControllerService)}: Unexpected exception in {nameof(GetModulesAsync)}", e);
                }
                finally
                {
                    signal.Release();
                }
            });
            if (!await signal.WaitAsync(DefaultTimeoutInMs))
            {
                RosLogger.Error($"{nameof(ControllerService)}: Timeout in {nameof(GetModulesAsync)}");
            }

            return result;
        }

        static async ValueTask SetFixedFrameAsync(SetFixedFrame srv)
        {
            (bool success, string? message) =
                await TrySetFixedFrameAsync(srv.Request.Id).AwaitAndLog(nameof(SetFixedFrameAsync));
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
        }

        static async ValueTask<(bool success, string? message)> TrySetFixedFrameAsync(string id)
        {
            (bool success, string? message) result = default;

            using (var signal = new SemaphoreSlim(0))
            {
                GameThread.Post(() =>
                {
                    try
                    {
                        TfModule.FixedFrameId = id;
                    }
                    finally
                    {
                        signal.Release();
                    }
                });

                if (!await signal.WaitAsync(DefaultTimeoutInMs))
                {
                    RosLogger.Error(
                        $"{nameof(ControllerService)}: Unexpected timeout in {nameof(TrySetFixedFrameAsync)}");
                    return result;
                }
            }

            return (true, "");
        }

        static async ValueTask GetFramePoseAsync(GetFramePose srv)
        {
            (bool[] success, Pose[] poses) = await TryGetFramePoseAsync(srv.Request.Frames);
            srv.Response.Poses = poses;
            srv.Response.IsValid = success;
        }

        static async ValueTask<(bool[] success, Pose[] poses)> TryGetFramePoseAsync(string[] ids)
        {
            bool[] success = Array.Empty<bool>();
            Pose[] poses = Array.Empty<Pose>();

            using (var signal = new SemaphoreSlim(0))
            {
                GameThread.Post(() =>
                {
                    try
                    {
                        var successList = new List<bool>();
                        var posesList = new List<Pose>();
                        foreach (string id in ids)
                        {
                            if (!TfModule.TryGetFrame(id, out var frame))
                            {
                                successList.Add(false);
                                posesList.Add(Pose.Identity);
                            }
                            else
                            {
                                successList.Add(true);
                                posesList.Add(frame.FixedWorldPose.Unity2RosPose());
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
                    RosLogger.Error(
                        $"{nameof(ControllerService)}: Unexpected timeout in {nameof(TryGetFramePoseAsync)}");
                    return (new bool[ids.Length], new Pose[ids.Length]);
                }
            }

            return (success, poses);
        }

        static void GetCaptureResolutions(GetCaptureResolutions srv)
        {
            if (Settings.ScreenCaptureManager == null)
            {
                srv.Response.Success = false;
                srv.Response.Message = "No screenshot manager has been set for this platform";
                return;
            }

            srv.Response.Success = true;
            srv.Response.Resolutions = Settings.ScreenCaptureManager.GetResolutions()
                .Select(resolution => new Vector2i(resolution.width, resolution.height))
                .ToArray();
        }

        static async ValueTask StartCaptureAsync(StartCapture srv)
        {
            if (Settings.ScreenCaptureManager == null)
            {
                srv.Response.Success = false;
                srv.Response.Message = "No screenshot manager has been set for this platform";
                return;
            }

            string? errorMessage = null;
            using (var signal = new SemaphoreSlim(0))
            {
                GameThread.Post(async () =>
                {
                    try
                    {
                        await Settings.ScreenCaptureManager.StartAsync(
                            srv.Request.ResolutionX, srv.Request.ResolutionY, srv.Request.WithHolograms);
                    }
                    catch (Exception e)
                    {
                        errorMessage = e.Message;
                    }
                    finally
                    {
                        signal.Release();
                    }
                });

                if (!await signal.WaitAsync(DefaultTimeoutInMs))
                {
                    RosLogger.Error($"{nameof(ControllerService)}: Unexpected timeout in {nameof(StartCaptureAsync)}");
                    srv.Response.Success = false;
                    srv.Response.Message = "Request timed out";
                    return;
                }
            }

            if (errorMessage != null)
            {
                srv.Response.Success = false;
                srv.Response.Message = errorMessage;
                return;
            }

            srv.Response.Success = true;
        }

        static async ValueTask StopCaptureAsync(StopCapture srv)
        {
            if (Settings.ScreenCaptureManager == null)
            {
                srv.Response.Success = false;
                srv.Response.Message = "No screenshot manager has been set for this platform";
                return;
            }

            string? errorMessage = null;
            using (var signal = new SemaphoreSlim(0))
            {
                GameThread.Post(async () =>
                {
                    try
                    {
                        await Settings.ScreenCaptureManager.StopAsync();
                    }
                    catch (Exception e)
                    {
                        errorMessage = e.Message;
                    }
                    finally
                    {
                        signal.Release();
                    }
                });

                if (!await signal.WaitAsync(DefaultTimeoutInMs))
                {
                    RosLogger.Error($"{nameof(ControllerService)}: Unexpected timeout in {nameof(StopCaptureAsync)}");
                    srv.Response.Success = false;
                    srv.Response.Message = "Request timed out";
                    return;
                }
            }

            if (errorMessage != null)
            {
                srv.Response.Success = false;
                srv.Response.Message = errorMessage;
                return;
            }

            srv.Response.Success = true;
        }

        static uint screenshotSeq;

        static async ValueTask CaptureScreenshotAsync(CaptureScreenshot srv)
        {
            if (Settings.ScreenCaptureManager == null)
            {
                srv.Response.Success = false;
                srv.Response.Message = "No screenshot manager has been set for this platform";
                return;
            }

            string? errorMessage = null;
            Screenshot? ss = null;
            Pose? pose = null;
            using (var signal = new SemaphoreSlim(0))
            {
                GameThread.Post(async () =>
                {
                    try
                    {
                        var screenshot = Resource.Extras.AppAssetHolder.Screenshot;
                        AudioSource.PlayClipAtPoint(screenshot, Settings.MainCamera.transform.position);

                        ss = await Settings.ScreenCaptureManager.CaptureColorAsync();
                        pose = ss != null
                            ? TfModule.RelativeToFixedFrame(ss.CameraPose).Unity2RosPose().ToCameraFrame()
                            : null;
                    }
                    catch (Exception e)
                    {
                        errorMessage = e.Message;
                    }
                    finally
                    {
                        signal.Release();
                    }
                });

                if (!await signal.WaitAsync(DefaultTimeoutInMs))
                {
                    RosLogger.Error(
                        $"{nameof(ControllerService)}: Unexpected timeout in {nameof(CaptureScreenshotAsync)}");
                    srv.Response.Success = false;
                    srv.Response.Message = "Request timed out";
                    return;
                }
            }

            if (errorMessage != null)
            {
                srv.Response.Success = false;
                srv.Response.Message = errorMessage;
                return;
            }

            if (ss == null)
            {
                srv.Response.Success = false;
                srv.Response.Message = "Captured failed for unknown reason";
                return;
            }

            srv.Response.Success = true;
            srv.Response.Width = ss.Width;
            srv.Response.Height = ss.Height;
            srv.Response.Bpp = ss.Bpp;
            srv.Response.Header = (screenshotSeq++, ss.Timestamp, TfModule.FixedFrameId);
            srv.Response.Intrinsics = ss.Intrinsic.ToArray();
            srv.Response.Pose = pose ?? Pose.Identity;
            srv.Response.Data = await CompressAsync(ss);
        }


        static Task<byte[]> CompressAsync(Screenshot ss)
        {
            return Task.Run(() =>
            {
                (int bpp, bool flipRb) = ss.Format switch
                {
                    ScreenshotFormat.Bgra => (bpp: 4, flipRb: true),
                    ScreenshotFormat.Rgb => (bpp: 3, flipRb: false),
                    _ => throw new InvalidOperationException("Unknown screenshot format")
                };

                var builder = new PngBuilder(ss.Bytes, bpp == 4, ss.Width, ss.Height, bpp, flipRb);
                return builder.Save();
            });
        }

        static ValueTask UpdateRobotAsync(UpdateRobot srv)
        {
            switch (srv.Request.Operation)
            {
                case OperationType.Remove:
                    return RemoveRobotAsync(srv);
                case OperationType.AddOrUpdate:
                    return AddRobotAsync(srv);
                default:
                    srv.Response.Success = false;
                    srv.Response.Message = "Unknown operation";
                    return default;
            }
        }

        static async ValueTask RemoveRobotAsync(UpdateRobot srv)
        {
            string id = srv.Request.Id;
            if (id.Length == 0)
            {
                srv.Response.Success = false;
                srv.Response.Message = "Id field is empty";
                return;
            }

            ModuleData? moduleData;
            if ((moduleData = ModuleDatas.FirstOrDefault(data => data.Configuration.Id == id)) == null)
            {
                srv.Response.Success = true;
                srv.Response.Message = $"WW There is no node with name '{id}'";
                return;
            }

            if (moduleData.ModuleType != ModuleType.Robot)
            {
                srv.Response.Success = false;
                srv.Response.Message = "Another module of the same id already exists, " +
                                       $"but it has type {TryGetName(moduleData.ModuleType)}";
                return;
            }

            using var signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    RosLogger.Info($"{nameof(ControllerService)}: Removing robot");
                    ModuleListPanel.Instance.RemoveModule(moduleData);
                }
                catch (Exception e)
                {
                    srv.Response.Success = false;
                    srv.Response.Message = $"An exception was raised: {e.Message}";
                }
                finally
                {
                    signal.Release();
                }
            });
            if (!await signal.WaitAsync(DefaultTimeoutInMs))
            {
                srv.Response.Success = false;
                srv.Response.Message = "Request timed out!";
                return;
            }

            if (string.IsNullOrEmpty(srv.Response.Message))
            {
                srv.Response.Success = true;
            }
        }

        static async ValueTask AddRobotAsync(UpdateRobot srv)
        {
            string id = srv.Request.Id;

            ModuleData? moduleData;
            if (id.Length != 0)
            {
                moduleData = ModuleDatas.FirstOrDefault(data => data.Configuration.Id == id);
                if (moduleData != null && moduleData.ModuleType != ModuleType.Robot)
                {
                    srv.Response.Success = false;
                    srv.Response.Message = "Another module of the same id already exists, " +
                                           $"but it has type {TryGetName(moduleData.ModuleType)}";
                    return;
                }
            }
            else
            {
                moduleData = null;
            }

            using var signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    //Logger.Info($"ControllerService: Creating robot");
                    var newModuleData = moduleData ?? ModuleListPanel.Instance.CreateModule(
                        ModuleType.Robot, requestedId: id.Length != 0 ? id : null);
                    srv.Response.Success = true;
                    ((SimpleRobotModuleData)newModuleData).UpdateConfiguration(
                        srv.Request.Configuration,
                        srv.Request.ValidFields);
                }
                catch (Exception e)
                {
                    srv.Response.Success = false;
                    srv.Response.Message = $"An exception was raised: {e.Message}";
                    RosLogger.Error($"{nameof(ControllerService)}: Failed to create robot", e);
                }
                finally
                {
                    signal.Release();
                }
            });
            if (!await signal.WaitAsync(DefaultTimeoutInMs))
            {
                srv.Response.Success = false;
                srv.Response.Message = "Request timed out!";
                return;
            }

            if (string.IsNullOrEmpty(srv.Response.Message))
            {
                srv.Response.Success = true;
            }
        }

        static async ValueTask LaunchDialogAsync(LaunchDialog srv)
        {
            if (string.IsNullOrEmpty(srv.Request.Dialog.Id))
            {
                srv.Response.Success = false;
                srv.Response.Message = "Dialog Id is null or empty";
                return;
            }

            if (Math.Abs(srv.Request.Dialog.Scale) < 1e-8)
            {
                srv.Response.Success = false;
                srv.Response.Message = "Cannot launch dialog with scale 0";
                return;
            }

            Feedback feedback = new();
            using SemaphoreSlim signal = new(0);
            GameThread.Post(() =>
            {
                try
                {
                    RosLogger.Info($"{nameof(ControllerService)}: Creating dialog");

                    bool overrideExpired = false;
                    string id = srv.Request.Dialog.Id;
                    var dialog = GuiWidgetListener.DefaultHandler.AddDialog(srv.Request.Dialog);
                    if (dialog == null)
                    {
                        TryRelease(signal);
                        return;
                    }

                    if (dialog is IDialogCanBeClicked canBeClicked)
                    {
                        canBeClicked.Clicked += TriggerButton;
                    }

                    //dialog.MenuEntryClicked += TriggerMenu;
                    dialog.Expired += OnExpired;

                    void TriggerButton(int buttonId)
                    {
                        feedback.VizId = RosManager.MyId ?? "";
                        feedback.Id = id;
                        feedback.Type = (byte)FeedbackType.ButtonClick;
                        feedback.EntryId = buttonId;
                        overrideExpired = true;

                        TryRelease(signal);
                    }

                    /*
                    void TriggerMenu(int buttonId)
                    {
                        feedback.VizId = RosManager.MyId ?? "";
                        feedback.Id = id;
                        feedback.Type = (byte)FeedbackType.MenuEntryClick;
                        feedback.EntryId = buttonId;
                        overrideExpired = true;

                        TryRelease(signal);
                    }
                    */

                    void OnExpired()
                    {
                        // ReSharper disable once AccessToModifiedClosure
                        if (overrideExpired)
                        {
                            return;
                        }

                        feedback.VizId = RosManager.MyId ?? "";
                        feedback.Id = id;
                        feedback.Type = (byte)FeedbackType.Expired;
                        feedback.EntryId = 0;

                        TryRelease(signal);
                    }
                }
                catch (Exception e)
                {
                    srv.Response.Success = false;
                    srv.Response.Message = $"An exception was raised: {e.Message}";
                    TryRelease(signal);
                }
            });

            await signal.WaitAsync();

            if (string.IsNullOrEmpty(srv.Response.Message))
            {
                srv.Response.Success = true;
                srv.Response.Feedback = feedback;
            }
        }

        static void TryRelease(SemaphoreSlim signal)
        {
            try
            {
                signal.Release();
            }
            catch (ObjectDisposedException)
            {
            }
        }
    }
}
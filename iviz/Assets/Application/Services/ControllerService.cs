#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.App;
using Iviz.App.ARDialogs;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.Roscpp;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using Newtonsoft.Json;
using UnityEngine;
using Feedback = Iviz.Msgs.IvizCommonMsgs.Feedback;
using LaunchDialog = Iviz.Msgs.IvizCommonMsgs.LaunchDialog;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;
using UpdateRobot = Iviz.Msgs.IvizCommonMsgs.UpdateRobot;

namespace Iviz.Controllers
{
    internal static class ServiceFunctions
    {
        const int DefaultTimeoutInMs = 5000;

        static RoslibConnection Connection => ConnectionManager.Connection;
        static IEnumerable<ModuleData> ModuleDatas => ModuleListPanel.Instance.ModuleDatas;

        static readonly (ModuleType module, string name)[] ModuleNames =
            typeof(ModuleType).GetEnumValues()
                .Cast<ModuleType>()
                .Select(module => (module, module.ToString()))
                .ToArray();


        static readonly string[] LogLevelNames = { "debug", "info", "warn", "error", "fatal" };

        public static void Start()
        {
            RoslibConnection connection = ConnectionManager.Connection;

            connection.AdvertiseService<GetLoggers>("get_loggers", GetLoggers);
            connection.AdvertiseService<SetLoggerLevel>("set_logger_level", SetLoggerLevel);

            connection.AdvertiseService<AddModule>("add_module", AddModuleAsync);
            connection.AdvertiseService<AddModuleFromTopic>("add_module_from_topic", AddModuleFromTopicAsync);
            connection.AdvertiseService<UpdateModule>("update_module", UpdateModuleAsync);
            connection.AdvertiseService<GetModules>("get_modules", GetModulesAsync);
            connection.AdvertiseService<SetFixedFrame>("set_fixed_frame", SetFixedFrameAsync);
            connection.AdvertiseService<GetFramePose>("get_frame_poses", GetFramePoseAsync);
            connection.AdvertiseService<GetCaptureResolutions>("get_capture_resolutions", GetCaptureResolutions);
            connection.AdvertiseService<StartCapture>("start_capture", StartCaptureAsync);
            connection.AdvertiseService<StopCapture>("stop_capture", StopCaptureAsync);
            connection.AdvertiseService<CaptureScreenshot>("capture_screenshot", CaptureScreenshotAsync);

            connection.AdvertiseService<UpdateRobot>("update_robot", UpdateRobotAsync);
            connection.AdvertiseService<LaunchDialog>("launch_dialog", LaunchDialogAsync);
        }

        static void GetLoggers(GetLoggers srv)
        {
            srv.Response.Loggers = new[]
            {
                new Msgs.Roscpp.Logger("ros.iviz",
                    LogLevelNames[ConsoleDialogData.IndexFromLevel(ConnectionManager.MinLogLevel)])
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
                    ConnectionManager.MinLogLevel = ConsoleDialogData.LevelFromIndex(i);
                    break;
                }
            }
        }

        static async ValueTask AddModuleAsync(AddModule srv)
        {
            var (id, success, message) = await TryAddModuleAsync(srv.Request.ModuleType, srv.Request.Id);
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
            srv.Response.Id = id ?? "";
        }

        static ModuleType ModuleTypeFromString(string moduleName)
        {
            return ModuleNames.FirstOrDefault(tuple => tuple.name == moduleName).module;
        }

        static async ValueTask<(string id, bool success, string message)> TryAddModuleAsync(string moduleTypeStr,
            string requestedId)
        {
            (string id, bool success, string message) result = default;

            if (string.IsNullOrWhiteSpace(moduleTypeStr))
            {
                result.message = "EE Invalid module type";
                return result;
            }

            ModuleType moduleType = ModuleTypeFromString(moduleTypeStr);

            if (moduleType == ModuleType.Invalid)
            {
                result.message = "EE Invalid module type";
                return result;
            }

            if (moduleType != ModuleType.Grid &&
                moduleType != ModuleType.DepthCloud &&
                moduleType != ModuleType.AugmentedReality &&
                moduleType != ModuleType.Joystick &&
                moduleType != ModuleType.Robot)
            {
                result.message = "EE Cannot create module of that type, use AddModuleFromTopic instead";
                return result;
            }

            ModuleData? moduleData;
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

            using var signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    RosLogger.Info($"Creating module of type {moduleType}");
                    var newModuleData = ModuleListPanel.Instance.CreateModule(moduleType,
                        requestedId: requestedId.Length != 0 ? requestedId : null);
                    result.id = newModuleData.Configuration.Id;
                    result.success = true;
                    RosLogger.Info("Done!");
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

        static async ValueTask AddModuleFromTopicAsync(AddModuleFromTopic srv)
        {
            var (id, success, message) = await TryAddModuleFromTopicAsync(srv.Request.Topic, srv.Request.Id);
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
            srv.Response.Id = id ?? "";
        }

        static async ValueTask<(string id, bool success, string message)> TryAddModuleFromTopicAsync(
            string topic,
            string requestedId)
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

            var topics = Connection.GetSystemPublishedTopicTypes(RequestType.CachedOnly);
            string? type = topics.FirstOrDefault(topicInfo => topicInfo.Topic == topic)?.Type;

            if (type == null)
            {
                topics = await Connection.GetSystemPublishedTopicTypesAsync(DefaultTimeoutInMs);
                type = topics.FirstOrDefault(topicInfo => topicInfo.Topic == topic)?.Type;
            }

            if (type == null)
            {
                return ("", false, $"EE Failed to find topic '{topic}'");
            }

            if (!Resource.ResourceByRosMessageType.TryGetValue(type, out ModuleType resource))
            {
                return ("", false, $"EE Type '{type}' is unsupported");
            }

            using var signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    result.id = ModuleListPanel.Instance.CreateModule(resource, topic, type,
                        requestedId: requestedId.Length != 0 ? requestedId : null).Configuration.Id;
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.message = $"EE An exception was raised: {e.Message}";
                    RosLogger.Error("Exception raised in TryAddModuleFromTopicAsync", e);
                }
                finally
                {
                    signal.Release();
                }
            });

            return await signal.WaitAsync(DefaultTimeoutInMs) ? result : ("", false, "EE Request timed out!");
        }

        static async ValueTask UpdateModuleAsync(UpdateModule srv)
        {
            var (success, message) = await TryUpdateModuleAsync(srv.Request.Id, srv.Request.Fields, srv.Request.Config);
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
        }

        static async ValueTask<(bool success, string message)> TryUpdateModuleAsync(string id,
            string[] fields,
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

            using var signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    var module = ModuleDatas.FirstOrDefault(data => data.Configuration.Id == id);
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
                    RosLogger.Error("Error:", e);
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.message = $"EE An exception was raised: {e.Message}";
                    RosLogger.Error("Error:", e);
                }
                finally
                {
                    signal.Release();
                }
            });
            return await signal.WaitAsync(DefaultTimeoutInMs) ? result : (false, "EE Request timed out!");
        }

        static async ValueTask GetModulesAsync(GetModules srv)
        {
            srv.Response.Configs = await GetModulesAsync();
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
                    RosLogger.Error("ControllerService: Unexpected JSON exception in GetModules", e);
                }
                catch (Exception e)
                {
                    RosLogger.Error("ControllerService: Unexpected exception in GetModules", e);
                }
                finally
                {
                    signal.Release();
                }
            });
            if (!await signal.WaitAsync(DefaultTimeoutInMs))
            {
                RosLogger.Error("Timeout in GetModules");
            }

            return result;
        }

        static async ValueTask SetFixedFrameAsync(SetFixedFrame srv)
        {
            (bool success, string message) = await TrySetFixedFrame(srv.Request.Id);
            srv.Response.Success = success;
            srv.Response.Message = message ?? "";
        }

        static async ValueTask<(bool success, string message)> TrySetFixedFrame(string id)
        {
            (bool success, string message) result = default;

            using (SemaphoreSlim signal = new SemaphoreSlim(0))
            {
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
                    RosLogger.Error("ControllerService: Unexpected timeout in TrySetFixedFrame");
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
                            if (!TfListener.TryGetFrame(id, out var frame))
                            {
                                successList.Add(false);
                                posesList.Add(Pose.Identity);
                            }
                            else
                            {
                                successList.Add(true);
                                posesList.Add(frame.OriginWorldPose.Unity2RosPose());
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
                    RosLogger.Error("ControllerService: Unexpected timeout in TryGetFramePoseAsync");
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
                    RosLogger.Error("ControllerService: Unexpected timeout in StartCaptureAsync");
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
                    RosLogger.Error("ControllerService: Unexpected timeout in StopCaptureAsync");
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
                        var assetHolder = UnityEngine.Resources
                            .Load<GameObject>("App Asset Holder")
                            .GetComponent<AppAssetHolder>();
                        AudioSource.PlayClipAtPoint(assetHolder.Screenshot, Settings.MainCamera.transform.position);

                        ss = await Settings.ScreenCaptureManager.CaptureColorAsync();
                        pose = ss != null
                            ? TfListener.RelativeToFixedFrame(ss.CameraPose).Unity2RosPose().ToCameraFrame()
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
                    RosLogger.Error("ControllerService: Unexpected timeout in CaptureScreenshotAsync");
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
            srv.Response.Header = (screenshotSeq++, ss.Timestamp, TfListener.FixedFrameId);
            srv.Response.Intrinsics = ss.Intrinsic.ToArray();
            srv.Response.Pose = pose ?? Pose.Identity;
            srv.Response.Data = await CompressAsync(ss);
        }


        static Task<byte[]> CompressAsync(Screenshot ss)
        {
            return Task.Run(() =>
            {
                int bpp;
                bool flipRb;

                switch (ss.Format)
                {
                    case ScreenshotFormat.Bgra:
                        bpp = 4;
                        flipRb = true;
                        break;
                    case ScreenshotFormat.Rgb:
                        bpp = 3;
                        flipRb = false;
                        break;
                    default:
                        throw new InvalidOperationException("Unknown screenshot format");
                }

                var builder = new BigGustave.PngBuilder(ss.Bytes, bpp == 4, ss.Width, ss.Height, bpp, flipRb);
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
                    srv.Response.Message = "EE Unknown operation";
                    return default;
            }
        }

        static async ValueTask RemoveRobotAsync(UpdateRobot srv)
        {
            string id = srv.Request.Id;
            if (id.Length == 0)
            {
                srv.Response.Success = false;
                srv.Response.Message = "EE Id field is empty";
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
                srv.Response.Message =
                    $"EE Another module of the same id already exists, but it has type {moduleData.ModuleType}";
                return;
            }

            using var signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    RosLogger.Info($"ControllerService: Removing robot");
                    ModuleListPanel.Instance.RemoveModule(moduleData);
                }
                catch (Exception e)
                {
                    srv.Response.Success = false;
                    srv.Response.Message = $"EE An exception was raised: {e.Message}";
                }
                finally
                {
                    signal.Release();
                }
            });
            if (!await signal.WaitAsync(DefaultTimeoutInMs))
            {
                srv.Response.Success = false;
                srv.Response.Message = "EE Request timed out!";
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
                    srv.Response.Message =
                        $"EE Another module of the same id already exists, but it has type {moduleData.ModuleType}";
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
                    srv.Response.Message = $"EE An exception was raised: {e.Message}";
                }
                finally
                {
                    signal.Release();
                }
            });
            if (!await signal.WaitAsync(DefaultTimeoutInMs))
            {
                srv.Response.Success = false;
                srv.Response.Message = "EE Request timed out!";
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

            Feedback feedback = new Feedback();
            bool overrideExpired = false;
            using var signal = new SemaphoreSlim(0);
            GameThread.Post(() =>
            {
                try
                {
                    RosLogger.Info($"ControllerService: Creating dialog");

                    var dialog = GuiWidgetListener.DefaultHandler.AddDialog(srv.Request.Dialog);
                    if (dialog == null)
                    {
                        TryRelease(signal);
                        return;
                    }

                    dialog.ButtonClicked += TriggerButton;
                    dialog.MenuEntryClicked += TriggerMenu;
                    dialog.Expired += Expired;

                    void TriggerButton(ARDialog mDialog, int buttonId)
                    {
                        mDialog.ButtonClicked -= TriggerButton;
                        mDialog.MenuEntryClicked -= TriggerMenu;
                        mDialog.Expired -= Expired;

                        feedback.VizId = ConnectionManager.MyId ?? "";
                        feedback.Id = mDialog.Id ?? "";
                        feedback.FeedbackType = FeedbackType.ButtonClick;
                        feedback.EntryId = buttonId;
                        overrideExpired = true;

                        TryRelease(signal);
                    }

                    void TriggerMenu(ARDialog mDialog, int buttonId)
                    {
                        mDialog.ButtonClicked -= TriggerButton;
                        mDialog.MenuEntryClicked -= TriggerMenu;
                        mDialog.Expired -= Expired;

                        feedback.VizId = ConnectionManager.MyId ?? "";
                        feedback.Id = mDialog.Id ?? "";
                        feedback.FeedbackType = FeedbackType.MenuEntryClick;
                        feedback.EntryId = buttonId;
                        overrideExpired = true;

                        TryRelease(signal);
                    }

                    void Expired(ARDialog mDialog)
                    {
                        // ReSharper disable once AccessToModifiedClosure
                        if (overrideExpired)
                        {
                            return;
                        }

                        mDialog.ButtonClicked -= TriggerButton;
                        mDialog.MenuEntryClicked -= TriggerMenu;
                        mDialog.Expired -= Expired;

                        feedback.VizId = ConnectionManager.MyId ?? "";
                        feedback.Id = mDialog.Id ?? "";
                        feedback.FeedbackType = FeedbackType.Expired;
                        feedback.EntryId = 0;

                        TryRelease(signal);
                    }
                }
                catch (Exception e)
                {
                    srv.Response.Success = false;
                    srv.Response.Message = $"EE An exception was raised: {e.Message}";
                    signal.Release();
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
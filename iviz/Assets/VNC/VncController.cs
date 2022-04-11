#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.XR;
using Iviz.Displays;
using Iviz.Displays.XR;
using Iviz.Msgs;
using Iviz.Tools;
using MarcusW.VncClient;
using MarcusW.VncClient.Protocol;
using MarcusW.VncClient.Protocol.SecurityTypes;
using Newtonsoft.Json;
using Nito.AsyncEx;
using UnityEngine;
using UnityEngine.InputSystem;
using Color = UnityEngine.Color;

namespace VNC
{
    public class VncController : MonoBehaviour
    {
        [SerializeField] XRPlainDialog? plainDialog;
        [SerializeField] VncConnectionDialog? connectionDialog;
        [SerializeField] VncPasswordDialog? passwordDialog;
        [SerializeField] VncScreen? screen;
        [SerializeField] PanelHolder? panelHolder;

        [SerializeField] GameObject? leftController;
        [SerializeField] GameObject? rightController;

        IXRController? leftControllerComp;
        IXRController? rightControllerComp;

        TfModule? tfModule;

        string lastHostname = "";
        string lastPort = "5900";
        string lastPassword = "";
        bool hasConfirmedPassword;

        readonly CancellationTokenSource tokenSource = new();
        CancellationToken Token => tokenSource.Token;

        XRPlainDialog PlainDialog => plainDialog.AssertNotNull(nameof(plainDialog));
        VncConnectionDialog ConnectionDialog => connectionDialog.AssertNotNull(nameof(connectionDialog));
        VncPasswordDialog PasswordDialog => passwordDialog.AssertNotNull(nameof(passwordDialog));
        PanelHolder PanelHolder => panelHolder.AssertNotNull(nameof(panelHolder));
        public VncClient Client { get; }
        public VncScreen Screen => screen.AssertNotNull(nameof(screen));

        public IXRController LeftController =>
            leftController.EnsureHasComponent(ref leftControllerComp, nameof(leftController));

        public IXRController RightController =>
            rightController.EnsureHasComponent(ref rightControllerComp, nameof(rightController));

        public VncController()
        {
            Client = new VncClient(Token);
        }

        void Awake()
        {
#if UNITY_EDITOR
            // clear static stuff in case domain reloading is disabled
            Settings.ClearResources();
            Iviz.Resources.Resource.ClearResources();
            ResourcePool.ClearResources();
#endif

            tfModule = new TfModule(id => new TfFrameDisplay(id));
            SetBackgroundColor(Color.black);

            foreach (var dialog in new XRDialog[] { PlainDialog, ConnectionDialog, PasswordDialog })
            {
                dialog.BindingType = BindingType.User;
                dialog.LocalDisplacement = new Vector3(0, -0.3f, 0.6f);
                dialog.Scale = 0.45f;
                dialog.PositionDamping = 0.01f;
            }
        }

        void Start()
        {
            _ = RunAsync();
        }

        async Task RunAsync()
        {
            PanelHolder.Visible = false;

            LoadConfiguration();

            while (!Token.IsCancellationRequested)
            {
                hasConfirmedPassword = false;

                if (!await TryToConnect())
                {
                    continue;
                }

                await SaveConfiguration();

                ShowMainPanel();

                await foreach (var connectionState in ProcessConnectionChanges(Token))
                {
                    await ProcessConnectionChange(connectionState);
                }
            }
        }

        async Task<bool> TryToConnect()
        {
            try
            {
                await Client.StartAsync(this);
                return true;
            }
            catch (HandshakeFailedNoCommonSecurityException)
            {
                await LaunchPlainDialog(
                    "Error",
                    "<b>Authentication failed!</b>\n" +
                    "Make sure that the server supports the \"VNC\" authentication type.");
            }
            catch (HandshakeFailedAuthenticationException e) when (e.Result == SecurityResult.FailedTooManyAttempts)
            {
                await LaunchPlainDialog(
                    "Error",
                    "<b>Authentication failed!</b>\n" +
                    "The server is not accepting more connections. Too many authentication attempts failed.");
            }
            catch (HandshakeFailedAuthenticationException)
            {
                await LaunchPlainDialog(
                    "Error",
                    "<b>Authentication failed!</b>\n" +
                    "Make sure the password is correct.");
            }
            catch (SocketException e)
            {
                //Debug.Log(e.SocketErrorCode);
                string message = e.SocketErrorCode switch
                {
                    SocketError.ConnectionRefused =>
                        "Connection refused. Make sure that the VNC server is running and the " +
                        "hostname and port are correct.",
                    SocketError.HostNotFound =>
                        "Host not found. Make sure that the hostname of the VNC server is correct.",
                    _ => e.Message
                };
                await LaunchPlainDialog(
                    "Error",
                    "<b>Connection failed!</b>\n" +
                    message);
            }
            catch (TimeoutException)
            {
                await LaunchPlainDialog(
                    "Error",
                    "<b>Connection failed!</b>\n" +
                    "The connection timed out. Make sure the VNC server is reachable.");
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            return false;
        }

        async IAsyncEnumerable<ConnectionState> ProcessConnectionChanges(
            [EnumeratorCancellation] CancellationToken token)
        {
            var queue = new AsyncProducerConsumerQueue<ConnectionState>();

            void OnConnectionStateChanged(ConnectionState connectionState)
            {
                queue.Enqueue(connectionState, default);
            }

            Client.ConnectionStateChanged += OnConnectionStateChanged;

            while (!token.IsCancellationRequested)
            {
                ConnectionState newState;
                try
                {
                    newState = await queue.DequeueAsync(token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                yield return newState;
            }

            Client.ConnectionStateChanged -= OnConnectionStateChanged;
        }

        Task<bool> ProcessConnectionChange(ConnectionState state)
        {
            switch (state)
            {
                case ConnectionState.ReconnectFailed:
                    return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public async ValueTask<(string, int)> RequestServerAsync()
        {
            while (true)
            {
                var (hostname, testPort) = await LaunchConnectionDialog();
                if (hostname.Length == 0)
                {
                    await LaunchPlainDialog(
                        "Error",
                        "<b>Invalid hostname!</b>\n" +
                        "The hostname cannot be empty.");
                    continue;
                }

                if (Uri.CheckHostName(hostname) == UriHostNameType.Unknown)
                {
                    await LaunchPlainDialog(
                        "Error",
                        "<b>Invalid hostname!</b>\n" +
                        "Make sure that the VNC server name is correct.");
                    continue;
                }

                if (testPort.Length == 0)
                {
                    await LaunchPlainDialog(
                        "Error",
                        "<b>Invalid port!</b>\n" +
                        "The port cannot be empty.");
                    continue;
                }

                if (!int.TryParse(testPort, out int port) || port is < 0 or >= 65536)
                {
                    await LaunchPlainDialog(
                        "Error",
                        "<b>Invalid port!</b>\n" +
                        "Make sure the port is numeric and between 0 and 65536.");
                    continue;
                }

                return (hostname, port);
            }
        }

        Task LaunchPlainDialog(string title, string caption)
        {
            PlainDialog.Title = title;
            PlainDialog.Caption = caption;
            PlainDialog.ButtonSetup = ButtonSetup.Ok;
            PlainDialog.Visible = true;
            PlainDialog.Initialize();

            FAnimator.Spawn(Token, 0.1f, t => PlainDialog.Scale = Mathf.Sqrt(t) * 0.45f);

            var ts = TaskUtils.CreateCompletionSource();

            void OnClicked(int _)
            {
                ts.TrySetResult();
                PlainDialog.Visible = false;
                PlainDialog.Clicked -= OnClicked;
            }

            PlainDialog.Clicked += OnClicked;
            return ts.Task;
        }

        async Task<(string hostname, string port)> LaunchConnectionDialog()
        {
            ConnectionDialog.Hostname = lastHostname;
            ConnectionDialog.Port = lastPort;
            ConnectionDialog.Visible = true;
            ConnectionDialog.Initialize();

            //FAnimator.Spawn(Token, 0.1f, t => ConnectionDialog.Scale = t * 0.45f);

            var ts = TaskUtils.CreateCompletionSource();

            void OnClicked(int _)
            {
                ts.TrySetResult();
                ConnectionDialog.Visible = false;
                ConnectionDialog.Clicked -= OnClicked;
            }

            ConnectionDialog.Clicked += OnClicked;
            await ts.Task;

            lastHostname = ConnectionDialog.Hostname;
            lastPort = ConnectionDialog.Port;

            return (lastHostname, lastPort);
        }

        public Task<string> RequestPasswordAsync()
        {
            return hasConfirmedPassword 
                ? Task.FromResult(lastPassword) 
                : GameThread.PostAsync(DoRequestPasswordAsync);
        }

        async ValueTask<string> DoRequestPasswordAsync()
        {
            while (true)
            {
                string password = await LaunchPasswordDialog();
                if (password.Length == 0)
                {
                    await LaunchPlainDialog(
                        "Error",
                        "<b>Invalid password!</b>\n" +
                        "The password cannot be empty.");
                    continue;
                }

                hasConfirmedPassword = true;
                return password;
            }
        }

        async Task<string> LaunchPasswordDialog()
        {
            PasswordDialog.Password = lastPassword;
            PasswordDialog.Visible = true;
            PasswordDialog.Initialize();

            FAnimator.Spawn(Token, 0.1f, t => ConnectionDialog.Scale = t * 0.45f);

            var ts = TaskUtils.CreateCompletionSource();

            void OnClicked(int _)
            {
                ts.TrySetResult();
                PasswordDialog.Visible = false;
                PasswordDialog.Clicked -= OnClicked;
            }

            PasswordDialog.Clicked += OnClicked;
            await ts.Task;

            lastPassword = PasswordDialog.Password;
            return lastPassword;
        }

        void ShowMainPanel()
        {
            var cameraPosition = Settings.MainCameraTransform.position;
            var cameraForward = Settings.MainCameraTransform.forward;

            var position = (cameraPosition + 1.5f * cameraForward).WithY(cameraPosition.y - 0.6f);
            PanelHolder.Transform.position = position;
            PanelHolder.RotateToFront();
            PanelHolder.Visible = true;
        }

        static string ConfigPath => $"{Settings.VncFolder}/config.json";

        async Task SaveConfiguration()
        {
            var config = new VncConfiguration
            {
                Hostname = lastHostname,
                Port = int.Parse(lastPort),
                Password = lastPassword
            };

            string path = ConfigPath;

            try
            {
                RosLogger.Internal("Saving VNC config file...");
                Directory.CreateDirectory(Settings.VncFolder);
                string text = BuiltIns.ToJsonString(config);
                await FileUtils.WriteAllTextAsync(path, text, default);
                RosLogger.Internal("Done.");
            }
            catch (Exception e)
            {
                RosLogger.Internal("Error saving VNC config", e);
                return;
            }

            RosLogger.Debug($"{this}: Writing VNC config to {path}");
        }

        void LoadConfiguration()
        {
            string path = ConfigPath;

            try
            {
                if (!File.Exists(path))
                {
                    return;
                }

                RosLogger.Debug($"{this}: Reading VNC config from {path}");

                string text = File.ReadAllText(path);
                var config = JsonConvert.DeserializeObject<VncConfiguration?>(text);
                if (config == null)
                {
                    return; // empty text
                }

                lastHostname = config.Hostname;
                lastPort = config.Port.ToString();
                lastPassword = config.Password;

                return;
            }
            catch (Exception e) when
                (e is IOException or SecurityException or JsonException)
            {
                RosLogger.Debug($"{this}: Error loading VNC config", e);
                // pass through
            }

            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                RosLogger.Debug($"{this}: Failed to reset VNC config", e);
            }
        }

        static void SetBackgroundColor(Color value)
        {
            Color colorToUse = Settings.IsHololens ? Color.black : value;
            Settings.MainCamera.backgroundColor = colorToUse.WithAlpha(0);

            RenderSettings.ambientSkyColor = value.WithAlpha(0);

            Color.RGBToHSV(value, out float h, out float s, out float v);
            var equatorColor = Color.HSVToRGB(h, Math.Min(s, 0.3f), v * 0.5f);
            RenderSettings.ambientEquatorColor = equatorColor;
        }

        void OnDestroy()
        {
            tokenSource.Cancel();
            tfModule?.Dispose();
            Client.Dispose();
        }

        [DataContract]
        sealed class VncConfiguration
        {
            [DataMember] public string Hostname { get; set; } = "";
            [DataMember] public int Port { get; set; } = 5090;
            [DataMember] public string Password { get; set; } = "";
        }
    }
}
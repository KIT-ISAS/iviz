#nullable enable

using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XR;
using MarcusW.VncClient.Protocol;
using UnityEngine;

namespace VNC
{
    public class VncController : MonoBehaviour
    {
        [SerializeField] XRPlainDialog? plainDialog;
        [SerializeField] VncScreen? screen;

        VncScreen Screen => screen.AssertNotNull(nameof(screen));
        XRPlainDialog PlainDialog => plainDialog.AssertNotNull(nameof(plainDialog));
        public VncClient Client { get; } = new();

        TfModule? tfModule;
        bool fpsClamped;

        void Awake()
        {
            tfModule = new TfModule(id => new TfFrameDisplay(id));

            // clear static stuff in case domain reloading is disabled
            // this is only really needed in the editor
            Settings.ClearResources();
            Iviz.Resources.Resource.ClearResources();
            ResourcePool.ClearResources();
        }

        void Start()
        {
            RunAsync();
        }

        async void RunAsync()
        {
            try
            {
                await Client.StartAsync(Screen);
            }
            catch (HandshakeFailedNoCommonSecurityException e)
            {
                await LaunchDialog(
                    "Error",
                    "<b>Authentication failed!</b>\n" +
                    "Make sure that the server supports the \"VNC\" authentication type.");
                //Debug.Log("Theirs: " + string.Join(", ", e.RemoteSecurityTypeIds));
                //Debug.Log("Mine: " + string.Join(", ", e.LocalSecurityTypes));
            }
            catch (HandshakeFailedAuthenticationException e)
            {
                Debug.Log("Authentication failed: " + (e.Reason ?? "Wrong credentials"));
            }
            catch (SocketException e)
            {
                string message = e.SocketErrorCode switch
                {
                    SocketError.ConnectionRefused =>
                        "Make sure that the VNC server is running and the hostname and port are correct.",
                    _ => e.Message
                };
                //Debug.Log(e.SocketErrorCode);
                await LaunchDialog(
                    "Error",
                    "<b>Connection failed!</b>\n" +
                    message);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            Debug.Log("All right!");
        }

        Task LaunchDialog(string title, string caption)
        {
            PlainDialog.Title = title;
            PlainDialog.Caption = caption;
            PlainDialog.ButtonSetup = XRButtonSetup.Ok;
            PlainDialog.Visible = true;

            var ts = new TaskCompletionSource();
            PlainDialog.Clicked += _ =>
            {
                ts.TrySetResult();
                PlainDialog.Visible = false;
            };
            return ts.Task;
        }

        /*
        void Update()
        {
            if (Time.deltaTime > 1f / 15 && fpsClamped)
            {
                Debug.Log("Unclamping FPS!");
                Settings.SettingsManager.TargetFps = -1;
                fpsClamped = false;
            }
            else if (Time.deltaTime < 1f / 120 && !fpsClamped)
            {
                Debug.Log("Clamping FPS to 120.");
                Settings.SettingsManager.TargetFps = 120;
                fpsClamped = true;
            }
        }
        */

        void OnDestroy()
        {
            tfModule?.Dispose();
            Client.Dispose();
        }
    }
}
using System;
using System.Threading;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ARSidePanel : MonoBehaviour
    {
        [NotNull]
        static ARFoundationController Controller =>
            ARController.Instance ?? throw new NullReferenceException("AR Controller is not set");

        [CanBeNull] RectTransform mTransform;
        [NotNull] RectTransform Transform => mTransform != null ? mTransform : (mTransform = (RectTransform)transform);

        [SerializeField] Button divider;
        [SerializeField] Button toggle; 
        [SerializeField] ARSideButton arVisible;
        [SerializeField] ARSideButton move;
        [SerializeField] ARSideButton reposition;
        [SerializeField] ARSideButton reset;
        [SerializeField] ARSideButton meshEnabled;
        [SerializeField] ARSideButton meshReset;
        [SerializeField] ARSideButton occlusionEnabled;
        [SerializeField] ARSideButton tfVisible;

        [SerializeField] float shiftLeft = -30;
        [SerializeField] float shiftRight = 40;

        [CanBeNull] CancellationTokenSource tokenSource;

        bool active;
        bool visible;

        bool Active
        {
            get => active;
            set
            {
                if (active == value)
                {
                    return;
                }

                tokenSource?.Cancel();
                tokenSource = new CancellationTokenSource();

                active = value;

                FAnimator.Spawn(tokenSource.Token, 0.15f, t =>
                {
                    float scaledT = Mathf.Sqrt(t);
                    float x = Mathf.Lerp(shiftLeft, shiftRight, active ? scaledT : (1 - scaledT));
                    var parent = (RectTransform)Transform.parent;
                    parent.anchoredPosition = parent.anchoredPosition.WithX(x);
                });
            }
        }

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                if (!value)
                {
                    gameObject.SetActive(false);
                    divider.gameObject.SetActive(false);
                    toggle.gameObject.SetActive(false);
                    return;
                }

                if (ARController.Instance == null)
                {
                    return;
                }

                gameObject.SetActive(true);
                divider.gameObject.SetActive(true);
                toggle.gameObject.SetActive(true);
                Setup();
            }
        }

        void Setup()
        {
            arVisible.Enabled = Controller.Visible;
            move.Enabled = Controller.ShowARJoystick;
            meshEnabled.Enabled = Controller.EnableMeshing;
            occlusionEnabled.Enabled = Controller.OcclusionQuality != OcclusionQualityType.Off;
            tfVisible.Enabled = TfListener.Instance.Visible;
        }

        void Awake()
        {
            ARController.ARStateChanged += OnArEnabledChanged;

            toggle.onClick.AddListener(() =>
            {
                if (ARController.Instance != null)
                {
                    ModuleListPanel.Instance.ShowARPanel();
                }
            });

            divider.onClick.AddListener(OnDividerClicked);

            arVisible.Clicked += () => Controller.Visible = (arVisible.Enabled = !arVisible.Enabled);
            move.Clicked += () =>
            {
                if (ARController.Instance is { SetupModeEnabled: false })
                {
                    ToggleARJoystick();
                }
                else if (move.Enabled)
                {
                    Controller.ShowARJoystick = false;
                    move.Enabled = false;
                }
            };
            reposition.Clicked += () => Controller.ResetSetupMode();
            meshEnabled.Clicked += () => Controller.EnableMeshing = (meshEnabled.Enabled = !meshEnabled.Enabled);
            reset.Clicked += () => Controller.ResetSession();
            meshReset.Clicked += () =>
            {
                Controller.EnableMeshing = false;
                Controller.EnableMeshing = true;
            };
            occlusionEnabled.Clicked += () =>
            {
                occlusionEnabled.Enabled = !occlusionEnabled.Enabled;
                Controller.OcclusionQuality =
                    occlusionEnabled.Enabled ? OcclusionQualityType.Fast : OcclusionQualityType.Off;
            };

            tfVisible.Clicked += () => TfListener.Instance.Visible = (tfVisible.Enabled = !tfVisible.Enabled);

            Active = true;
        }

        public void ToggleARJoystick()
        {
            Controller.ShowARJoystick = (move.Enabled = !move.Enabled);
        }

        void OnDestroy()
        {
            ARController.ARStateChanged -= OnArEnabledChanged;
            tokenSource?.Cancel();
        }

        void OnArEnabledChanged(bool _)
        {
            Visible = Visible;
        }

        void OnDividerClicked()
        {
            Active = !Active;
        }
    }
}
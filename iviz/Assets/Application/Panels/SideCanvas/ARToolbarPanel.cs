#nullable enable

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
    public sealed class ARToolbarPanel : MonoBehaviour
    {
        [NotNull]
        static ARFoundationController Controller =>
            ARController.Instance ?? throw new NullReferenceException("AR Controller is not set");

        RectTransform? mTransform;
        RectTransform Transform => this.EnsureHasTransform(ref mTransform);

        [SerializeField] Button? divider;
        [SerializeField] Button? toggle; 
        [SerializeField] ARSideButton? arVisible;
        [SerializeField] ARSideButton? move;
        [SerializeField] ARSideButton? reposition;
        [SerializeField] ARSideButton? reset;
        [SerializeField] ARSideButton? meshEnabled;
        [SerializeField] ARSideButton? meshReset;
        [SerializeField] ARSideButton? occlusionEnabled;
        [SerializeField] ARSideButton? tfVisible;

        [SerializeField] float shiftLeft = -30;
        [SerializeField] float shiftRight = 40;

        CancellationTokenSource? tokenSource;

        bool active;
        bool visible;

        Button Divider => divider.AssertNotNull(nameof(divider));
        Button Toggle => toggle.AssertNotNull(nameof(toggle));
        ARSideButton ArVisible => arVisible.AssertNotNull(nameof(arVisible));
        ARSideButton Move => move.AssertNotNull(nameof(move));
        ARSideButton Reposition => reposition.AssertNotNull(nameof(reposition));
        ARSideButton Reset => reset.AssertNotNull(nameof(reset));
        ARSideButton MeshEnabled => meshEnabled.AssertNotNull(nameof(meshEnabled));
        ARSideButton MeshReset => meshReset.AssertNotNull(nameof(meshReset));
        ARSideButton OcclusionEnabled => occlusionEnabled.AssertNotNull(nameof(occlusionEnabled));
        ARSideButton TfVisible => tfVisible.AssertNotNull(nameof(tfVisible));

        
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

                var parent = (RectTransform)Transform.parent;
                FAnimator.Spawn(tokenSource.Token, 0.15f, t =>
                {
                    float scaledT = Mathf.Sqrt(t);
                    float x = Mathf.Lerp(shiftLeft, shiftRight, active ? scaledT : (1 - scaledT));
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
                    Divider.gameObject.SetActive(false);
                    Toggle.gameObject.SetActive(false);
                    return;
                }

                if (ARController.Instance == null)
                {
                    return;
                }

                gameObject.SetActive(true);
                Divider.gameObject.SetActive(true);
                Toggle.gameObject.SetActive(true);
                Setup();
            }
        }

        void Setup()
        {
            ArVisible.Enabled = Controller.Visible;
            Move.Enabled = Controller.ShowARJoystick;
            MeshEnabled.Visible = Controller.ProvidesMesh;
            MeshEnabled.Enabled = Controller.EnableMeshing;
            OcclusionEnabled.Visible = Controller.ProvidesOcclusion;
            OcclusionEnabled.Enabled = Controller.OcclusionQuality != OcclusionQualityType.Off;
            TfVisible.Enabled = TfModule.Instance.Visible;
        }

        void Awake()
        {
            ARController.ARStateChanged += OnArEnabledChanged;
            ARFoundationController.SetupModeChanged += OnSetupModeChanged;

            Toggle.onClick.AddListener(() =>
            {
                if (ARController.Instance != null)
                {
                    ModuleListPanel.Instance.ShowARPanel();
                }
            });

            Divider.onClick.AddListener(OnDividerClicked);

            ArVisible.Clicked += () => Controller.Visible = (ArVisible.Enabled = !ArVisible.Enabled);
            Move.Clicked += () =>
            {
                if (ARController.Instance is { SetupModeEnabled: false })
                {
                    ToggleARJoystick();
                }
                else if (Move.Enabled)
                {
                    Controller.ShowARJoystick = false;
                    Move.Enabled = false;
                }
            };
            Reposition.Clicked += () => Controller.ResetSetupMode();
            MeshEnabled.Clicked += () => Controller.EnableMeshing = (MeshEnabled.Enabled = !MeshEnabled.Enabled);
            Reset.Clicked += () => _ = Controller.ResetSessionAsync();
            MeshReset.Clicked += () =>
            {
                Controller.EnableMeshing = false;
                Controller.EnableMeshing = true;
            };
            OcclusionEnabled.Clicked += () =>
            {
                OcclusionEnabled.Enabled = !OcclusionEnabled.Enabled;
                Controller.OcclusionQuality =
                    OcclusionEnabled.Enabled ? OcclusionQualityType.Fast : OcclusionQualityType.Off;
            };

            TfVisible.Clicked += () => TfModule.Instance.Visible = (TfVisible.Enabled = !TfVisible.Enabled);

            Active = true;
        }

        public void ToggleARJoystick()
        {
            Controller.ShowARJoystick = (Move.Enabled = !Move.Enabled);
        }

        void OnDestroy()
        {
            ARController.ARStateChanged -= OnArEnabledChanged;
            ARFoundationController.SetupModeChanged -= OnSetupModeChanged;
            tokenSource?.Cancel();
        }

        void OnArEnabledChanged(bool _)
        {
            Visible = Visible;
        }
        
        void OnSetupModeChanged(bool value)
        {
            Move.Interactable = !value;
        }

        void OnDividerClicked()
        {
            Active = !Active;
        }
    }
}
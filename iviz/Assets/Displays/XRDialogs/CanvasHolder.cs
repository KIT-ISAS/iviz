#nullable enable

using System;
using System.Threading;
using Iviz.Core;
using Iviz.Resources;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class CanvasHolder : XRHolder, IDisplay, IRecyclable
    {
        const float HeaderHeight = 0.075f;

        [SerializeField] Canvas? canvas;
        [SerializeField] BoxCollider? boxCollider;
        [SerializeField] FixedDistanceDraggable? draggable;
        [SerializeField] TMP_Text? label;
        [SerializeField] Vector2 canvasSize = new Vector2(800, 600);
        [SerializeField] bool followsCamera;

        RoundedPlaneDisplay? background;
        SelectionFrame? frame;

        BoxCollider BoxCollider => boxCollider.AssertNotNull(nameof(boxCollider));
        RoundedPlaneDisplay Background => ResourcePool.RentChecked(ref background, Holder);
        FixedDistanceDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        Transform Holder => Draggable.Transform;
        TMP_Text Label => label.AssertNotNull(nameof(label));

        public string Title
        {
            get => Label.text;
            set => Label.text = value;
        }

        public Canvas Canvas => canvas.AssertNotNull(nameof(canvas));

        public Vector2 CanvasSize
        {
            get => canvasSize;
            set
            {
                canvasSize = value;
                var canvasTransform = (RectTransform)Canvas.transform;
                canvasTransform.sizeDelta = canvasSize;
                var (sizeX, sizeY) = Vector2.Scale(canvasTransform.rect.size, canvasTransform.localScale);
                Background.Size = new Vector2(sizeX, HeaderHeight);
                BoxCollider.size = new Vector3(sizeX, HeaderHeight, 0.005f);
                Draggable.Transform.localPosition = (sizeY + HeaderHeight + 0.01f) / 2 * Vector3.up;
            }
        }

        public bool FollowsCamera
        {
            set => enabled = value;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void Awake()
        {
            if (Canvas.worldCamera == null)
            {
                Canvas.worldCamera = Settings.MainCamera;
            }

            Background.Radius = 0.01f;
            Background.Color = Resource.Colors.TooltipBackground;
            Background.EnableShadows = false;
            Background.Layer = LayerType.Clickable;

            Draggable.ForwardScale = 5f;
            Draggable.StateChanged += () =>
            {
                bool visible = Draggable.IsDragging || Draggable.IsHovering;
                if (visible)
                {
                    if (frame == null)
                    {
                        frame = ResourcePool.RentDisplay<SelectionFrame>(Holder);
                        frame.Size = BoxCollider.size;
                    }

                    frame.Color = Draggable.IsDragging ? Color.cyan : Color.white;
                    frame.EmissiveColor = Draggable.IsDragging ? Color.blue : Color.black;
                    frame.ColumnWidth = Draggable.IsDragging ? 0.002f : 0.001f;
                }
                else
                {
                    var oldFrame = frame;
                    frame = null;
                    
                    // may happen during a disable, this may cause an error so post on next frame
                    GameThread.Post(oldFrame.ReturnToPool); 
                }                
            };

            Draggable.StartDragging += OnStartDragging;
            Draggable.EndDragging += OnEndDragging;

            CanvasSize = CanvasSize;

            if (Title.Length == 0)
            {
                Title = "Main";
            }

            FollowsCamera = followsCamera;
        }

        protected override Pose ProcessPose(in Pose pose)
        {
            var canvasTransform = (RectTransform)Canvas.transform;
            float sizeY = canvasTransform.rect.size.y * canvasTransform.localScale.y;

            var processedPose = pose;
            processedPose.position.y = Mathf.Max(processedPose.position.y, sizeY / 2 + 0.5f);

            return processedPose;
        }

        void OnStartDragging()
        {
            isDragging = true;
        }
        
        void OnEndDragging()
        {
            isDragging = false;
            
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();
            
            var mainCameraPose = new Pose(
                Settings.MainCameraPose.position,
                Quaternion.Euler(0, Settings.MainCameraPose.rotation.eulerAngles.y, 0)
            );
            var currentPose = Transform.AsPose();

            const float minZ = 1.0f;
            const float maxZ = 2.0f;

            cameraZ = Mathf.Clamp(mainCameraPose.InverseMultiply(currentPose).position.z, minZ, maxZ);

            var start = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            FAnimator.Spawn(tokenSource.Token, 0.25f,
                t => transform.rotation = Quaternion.Lerp(start, CalculateOrientationToCamera(), t));
        }

        public void ReturnToPool()
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            FAnimator.Spawn(tokenSource.Token, 0.15f, t =>
            {
                float scale = Mathf.Sqrt(1 - t);
                transform.localScale = scale * Vector3.one;
            }, () =>
            {
                transform.localScale = Vector3.one;
                ResourceUtils.ReturnToPool(this);
            });
        }

        public void Suspend()
        {
            tokenSource?.Cancel();
            frame.ReturnToPool();
            frame = null;
        }

        public void SplitForRecycle()
        {
            frame.ReturnToPool();
            background.ReturnToPool();
        }
    }
}
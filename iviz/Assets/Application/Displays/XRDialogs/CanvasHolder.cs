#nullable enable

using System.Threading;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public class CanvasHolder : MonoBehaviour, IDisplay, IRecyclable
    {
        const float HeaderHeight = 0.075f;

        [SerializeField] Canvas? canvas;
        [SerializeField] BoxCollider? boxCollider;
        [SerializeField] FixedDistanceDraggable? draggable;
        [SerializeField] TMP_Text? label;
        CancellationTokenSource? tokenSource;

        RoundedPlaneResource? background;
        SelectionFrame? frame;
        Transform? mTransform;
        Vector2 canvasSize = new Vector2(800, 600);

        BoxCollider BoxCollider => boxCollider.AssertNotNull(nameof(boxCollider));
        
        RoundedPlaneResource Background =>
            background != null
                ? background
                : (background = ResourcePool.RentDisplay<RoundedPlaneResource>(Holder));

        FixedDistanceDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        Transform Holder => Draggable.Transform;
        TMP_Text Label => label.AssertNotNull(nameof(label));
        
        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

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
            set
            {
                enabled = value;
            }
        } 

        void Awake()
        {
            if (Canvas.worldCamera == null)
            {
                Canvas.worldCamera = Settings.MainCamera;
            }

            Background.Radius = 0.01f;
            Background.Color = Resource.Colors.HighlighterBackground;
            Background.ShadowsEnabled = false;
            Background.Layer = LayerType.Clickable;

            Draggable.Damping = 0.2f;
            Draggable.ForwardScale = 5f;
            Draggable.StateChanged += () =>
            {
                bool visible = Draggable.IsDragging || Draggable.IsHovering;
                if (visible && frame == null)
                {
                    frame = ResourcePool.RentDisplay<SelectionFrame>(Holder);
                    frame.Size = BoxCollider.size;
                    frame.Color = Draggable.IsDragging ? Color.cyan : Color.white;
                    frame.EmissiveColor = Draggable.IsDragging ? Color.blue : Color.black;
                    frame.ColumnWidth = Draggable.IsDragging ? 0.01f : 0.005f;
                }
                else if (!visible && frame != null)
                {
                    frame.ReturnToPool();
                    frame = null;
                }
            };

            Draggable.EndDragging += OnEndDragging;

            CanvasSize = CanvasSize;

            if (Title is "")
            {
                Title = "Main";
            }

            FollowsCamera = false;
        }

        public void InitializePose()
        {
            var canvasTransform = (RectTransform)Canvas.transform;
            float sizeY = canvasTransform.rect.size.y * canvasTransform.localScale.y;

            var position = Settings.MainCameraTransform.position + Settings.MainCameraTransform.forward * 1.5f;
            position.y = Mathf.Max(position.y, sizeY / 2 + 0.5f);

            transform.position = position;
            transform.rotation = CalculateOrientationToCamera();
            
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            FAnimator.Spawn(tokenSource.Token, 0.15f, t =>
            {
                float scale = Mathf.Sqrt(t);
                transform.localScale = scale * Vector3.one;
            });
        }

        void OnEndDragging()
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            var start = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            FAnimator.Spawn(tokenSource.Token, 0.25f, t =>
            {
                transform.rotation = Quaternion.Lerp(start, CalculateOrientationToCamera(), t);
            });
        }

        Quaternion CalculateOrientationToCamera()
        {
            return Quaternion.LookRotation((transform.position - Settings.MainCameraTransform.position).WithY(0));
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

        public Bounds? Bounds => new Bounds(BoxCollider.center, BoxCollider.size);

        public int Layer { get; set; }

        void Update()
        {
            const float minDistance = 1.0f;
            const float maxDistance = 1.5f;
            const float damping = 0.05f;
            
            var mainCameraPose = new Pose(
                Settings.MainCameraTransform.position,
                Quaternion.Euler(0, Settings.MainCameraTransform.eulerAngles.y, 0)
            );

            var (currentPosition, currentRotation) = Transform.AsPose();
            var currentPositionLocal = mainCameraPose.Inverse().Multiply(currentPosition);
            var targetPositionLocal = Clamp(
                currentPositionLocal, 
                new Vector3(-0.5f, -0.3f, minDistance),
                new Vector3(0.5f, 0.1f, maxDistance));

            var targetPosition = mainCameraPose.Multiply(targetPositionLocal);
            var targetRotation = mainCameraPose.rotation;
            
            Transform.SetPositionAndRotation(
                Vector3.Lerp(currentPosition, targetPosition, damping), 
                Quaternion.Lerp(currentRotation, targetRotation, damping));
        }

        static Vector3 Clamp(in Vector3 value, in Vector3 min, in Vector3 max)
        {
            return Vector3.Min(Vector3.Max(value, min), max);
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
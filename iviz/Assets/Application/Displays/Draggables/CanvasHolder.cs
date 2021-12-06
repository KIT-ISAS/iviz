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

        BoxCollider BoxCollider => boxCollider.AssertNotNull(nameof(boxCollider));

        RoundedPlaneResource Background =>
            background != null
                ? background
                : (background = ResourcePool.RentDisplay<RoundedPlaneResource>(Holder));

        FixedDistanceDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        Transform Holder => Draggable.Transform;

        TMP_Text Label => label.AssertNotNull(nameof(label));

        public string Title
        {
            get => Label.text;
            set => Label.text = value;
        }

        public Canvas Canvas => canvas.AssertNotNull(nameof(canvas));

        Vector2 canvasSize = new Vector2(800, 600);

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
        }

        void OnEndDragging()
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            var start = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            FAnimator.Spawn(tokenSource.Token, 0.25f, t =>
            {
                var (x, _, z) = transform.position - Settings.MainCameraTransform.position;
                var end = quaternion.Euler(0, Mathf.PI / 2 - Mathf.Atan2(z, x), 0);
                transform.rotation = Quaternion.Lerp(start, end, t);
            });
        }

        public Bounds? Bounds => new Bounds(BoxCollider.center, BoxCollider.size);

        public int Layer { get; set; }

        public void Suspend()
        {
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
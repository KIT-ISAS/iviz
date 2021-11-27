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
    public class CanvasHolder : MonoBehaviour
    {
        [SerializeField] Canvas? canvas;
        [SerializeField] RoundedPlaneResource? background;
        [SerializeField] SelectionFrame? frame;
        [SerializeField] FixedDistanceDraggable? draggable;
        [SerializeField] TMP_Text? label;
        CancellationTokenSource? tokenSource;
        
        RoundedPlaneResource Background => background.AssertNotNull(nameof(background));
        FixedDistanceDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        SelectionFrame Frame => frame.AssertNotNull(nameof(frame));
        TMP_Text Label => label.AssertNotNull(nameof(label));

        public string Title
        {
            set => Label.text = value;
        }

        void Awake()
        {
            var mCanvas = canvas.AssertNotNull(nameof(canvas));
            if (mCanvas.worldCamera == null)
            {
                mCanvas.worldCamera = Settings.MainCamera;
            }

            var canvasTransform = (RectTransform) mCanvas.transform;
            var (sizeX, sizeY) = Vector2.Scale(canvasTransform.rect.size, canvasTransform.localScale);

            const float headerHeight = 0.1f;
            
            Background.Size = new Vector2(sizeX, headerHeight);
            Background.Radius = 0.01f;
            Background.Color = Resource.Colors.HighlighterBackground;
            Background.ShadowsEnabled = false;
            
            Frame.Size = new Vector3(sizeX, headerHeight, 0.005f);

            Draggable.Transform.localPosition = (sizeY + headerHeight + 0.01f) / 2 * Vector3.up; 
            Draggable.Damping = 0.2f;
            Draggable.StateChanged += () =>
            {
                Frame.Visible = Draggable.IsDragging || Draggable.IsHovering;
                Frame.EmissiveColor = Draggable.IsDragging ? Color.blue : Color.black;
            };
            
            Draggable.EndDragging += OnEndDragging;

            Title = "Main";
        }

        void OnEndDragging()
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            Quaternion start = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            FAnimator.Spawn(tokenSource.Token, 0.25f, t =>
            {
                var (x, _, z) = transform.position - Settings.MainCameraTransform.position;
                Quaternion end = quaternion.Euler(0, Mathf.PI / 2 - Mathf.Atan2(z, x), 0);
                transform.rotation = Quaternion.Lerp(start, end, t);
            });
        }
    }
}
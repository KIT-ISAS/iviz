#nullable enable

using System;
using System.Threading;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XR;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class PositionDisc3D : MonoBehaviour, IRecyclable, IWidgetWithColor, IWidgetCanBeMoved,
        IWidgetWithCaption
    {
        [SerializeField] XRButton? button;
        [SerializeField] MeshMarkerDisplay? anchor;
        [SerializeField] XRScreenDraggable? draggable;
        [SerializeField] MeshMarkerDisplay? disc;
        [SerializeField] MeshMarkerDisplay? glow;
        [SerializeField] float linkWidth = 0.04f;
        [SerializeField] float buttonDistance = 1f;
        
        LineDisplay? line;
        Transform? mTransform;

        CancellationTokenSource? tokenSource;
        string mainButtonCaption = "Send!";
        
        Color color = new(0, 1f, 0.6f);
        Color secondaryColor = Color.white;

        readonly LineWithColor[] lineBuffer = new LineWithColor[3];

        MeshMarkerDisplay Anchor => anchor.AssertNotNull(nameof(anchor));
        MeshMarkerDisplay Disc => disc.AssertNotNull(nameof(disc));
        MeshMarkerDisplay Glow => glow.AssertNotNull(nameof(glow));
        XRScreenDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        LineDisplay Line => ResourcePool.RentChecked(ref line, Transform);
        Transform Transform => this.EnsureHasTransform(ref mTransform);
        XRButton Button => button.AssertNotNull(nameof(button));

        public event Action<Vector3>? Moved;

        public string Caption
        {
            get => mainButtonCaption;
            set
            {
                mainButtonCaption = value;
                Button.Caption = value;
            }
        }

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                Disc.Color = value.WithValue(0.5f);
                Glow.Color = value.WithAlpha(0.8f);
                Glow.EmissiveColor = value;
            }
        }

        public Color SecondaryColor
        {
            get => secondaryColor;
            set
            {
                secondaryColor = value;
                Anchor.Color = value;
            }
        }

        public bool Interactable
        {
            set => Draggable.enabled = value;
        }
        
        void Awake()
        {
            Line.ElementScale = linkWidth;
            Line.Visible = false;

            Color = Color;
            SecondaryColor = SecondaryColor;
            
            Glow.Visible = false;            
            
            Button.Icon = XRIcon.Ok;
            Button.Caption = "Send!";
            Button.Visible = false;
            Button.Transform.SetParentLocal(Transform);
            Caption = Caption;

            Draggable.StartDragging += () =>
            {
                Button.Visible = false;
                tokenSource?.Cancel();
                Line.Visible = true;
                Disc.EmissiveColor = Color;
                Glow.Visible = true;                
            };
            Draggable.Moved += () =>
            {
                var discPosition = Draggable.Transform.localPosition;

                var localToFixed = TfModule.RelativeToFixedFrame(Transform);
                var fixedToLocal = localToFixed.Inverse();

                var start = localToFixed.position;
                var end = localToFixed.Multiply(discPosition);
                var (_, diffY, diffZ) = end - start;

                var p0 = Vector3.zero;
                var p1 = fixedToLocal.Multiply(start + new Vector3(0, diffY, 0));
                var p2 = fixedToLocal.Multiply(start + new Vector3(0, diffY, diffZ));
                var p3 = discPosition;

                lineBuffer[0] = new LineWithColor(p0, p1, Color.blue.WithAlpha(0.5f));
                lineBuffer[1] = new LineWithColor(p1, p2, Color.red.WithAlpha(0.5f));
                lineBuffer[2] = new LineWithColor(p2, p3, Color.green.WithAlpha(0.5f));
                Line.Set(lineBuffer, true);           
            };            
            Draggable.EndDragging += () =>
            {
                Disc.EmissiveColor = Color.black;
                Glow.Visible = false;
                
                //Button.Transform.SetLocalPose(Draggable.Transform.AsLocalPose().Multiply(BaseButtonPose));
                //Button.Transform.localPosition = Draggable.Transform.TransformPoint(new Vector3(-0.8f, 0, 0));
                Button.Transform.localPosition = Draggable.Transform.localPosition + buttonDistance * Vector3.up;
                Button.Visible = true;
                
                //button.Transform.SetLocalPose(disc.Transform.AsLocalPose().Multiply(BaseButtonPose));
                //button.Visible = true;
            };
            Button.Clicked += () =>
            {
                Button.Visible = false;
                Line.Visible = false;

                tokenSource?.Cancel();
                tokenSource = new CancellationTokenSource();

                var startPosition = Draggable.Transform.localPosition;
                FAnimator.Spawn(tokenSource.Token, 0.1f, t =>
                {
                    Draggable.Transform.localPosition = (1 - Mathf.Sqrt(t)) * startPosition;
                });                
                
                Moved?.Invoke(startPosition);                
                /*
                button.Visible = false;
                Debug.Log("click");
                Moved?.Invoke(disc.Transform.localPosition);
                */
            };
        }

        /*
        static Pose BaseButtonPose => new(
            new Vector3(-0.8f, 0, 0),
            Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(180, Vector3.up)
        );

        protected override void Update()
        {
            base.Update();

            anchor.transform.localRotation = disc.Transform.localRotation; // copy billboard
            if (button.Visible)
            {
                button.Transform.SetLocalPose(disc.Transform.AsLocalPose().Multiply(BaseButtonPose));
            }

            var discPosition = disc.Transform.localPosition;

            if (line.Visible)
            {
                Pose localToFixed = TfModule.RelativeToFixedFrame(Transform);
                Pose fixedToLocal = localToFixed.Inverse();

                Vector3 start = localToFixed.Multiply(Vector3.zero);
                Vector3 end = localToFixed.Multiply(discPosition);
                Vector3 diff = end - start;

                Vector3 p0 = Vector3.zero;
                Vector3 p1 = fixedToLocal.Multiply(start + new Vector3(0, diff.y, 0));
                Vector3 p2 = fixedToLocal.Multiply(start + new Vector3(0, diff.y, diff.z));
                Vector3 p3 = discPosition;

                lineBuffer[0] = new LineWithColor(p0, p1, Color.blue.WithAlpha(0.5f));
                lineBuffer[1] = new LineWithColor(p1, p2, Color.red.WithAlpha(0.5f));
                lineBuffer[2] = new LineWithColor(p2, p3, Color.green.WithAlpha(0.5f));
                line.Set(lineBuffer, true);
            }

            float discDistance = discPosition.Magnitude();
            if (discDistance < 0.005f)
            {
                if (dragBack)
                {
                    disc.Transform.localPosition = Vector3.zero;
                    dragBack = false;
                    line.Visible = false;
                }

                return;
            }

            if (dragBack)
            {
                disc.Transform.localPosition = 0.9f * discPosition;
            }
        }
        */

        public void Suspend()
        {
            Button.Transform.parent = Transform;
            Draggable.Transform.localPosition = Vector3.zero;
            Line.Visible = false;
            Moved = null;            
        }

        public void SplitForRecycle()
        {
            line.ReturnToPool();
        }
    }
}
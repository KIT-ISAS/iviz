#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Iviz.Displays.XR
{
    public sealed class TrajectoryDisc : MonoBehaviour, IRecyclable, IWidgetProvidesTrajectory,
        IWidgetWithCaption, IWidgetWithColor, IWidgetCanBeClicked
    {
        [SerializeField] XRButton? send;
        [SerializeField] XRButton? back;
        [SerializeField] XRButton? reset;
        [SerializeField] GameObject? holder;
        [SerializeField] XRScreenDraggable? disc;
        [SerializeField] MeshMarkerDisplay? sphere;
        [SerializeField] MeshMarkerDisplay? glow;
        [SerializeField] float period = 0.1f;
        [SerializeField] Transform? mTransform;

        readonly List<Vector3> positions = new(8);
        readonly List<LineWithColor> lineBuffer = new(8);

        Color secondColor;
        LineDisplay? lines;
        LineDisplay? segment;
        readonly List<MeshMarkerDisplay> corners = new();

        LineDisplay Lines => ResourcePool.RentChecked(ref lines, Transform);
        LineDisplay Segments => ResourcePool.RentChecked(ref segment, Transform);
        XRButton Send => send.AssertNotNull(nameof(send));
        XRButton Back => back.AssertNotNull(nameof(back));
        XRButton Reset => reset.AssertNotNull(nameof(reset));
        GameObject Holder => holder.AssertNotNull(nameof(holder));
        Transform Transform => this.EnsureHasTransform(ref mTransform);
        XRScreenDraggable Disc => disc.AssertNotNull(nameof(disc));
        MeshMarkerDisplay Sphere => sphere.AssertNotNull(nameof(sphere));
        MeshMarkerDisplay Glow => glow.AssertNotNull(nameof(glow));

        public event Action<List<Vector3>, float>? ProvidedTrajectory;
        public event Action<int>? Clicked;

        public bool Interactable
        {
            set => Send.Interactable = value;
        }

        public string Caption
        {
            set => Send.Caption = string.IsNullOrWhiteSpace(value) ? "Send" : value;
        }

        public Color Color
        {
            get => Sphere.Color;
            set => Sphere.Color = value;
        }

        public Color SecondColor
        {
            get => secondColor;
            set
            {
                secondColor = value;
                Lines.Tint = value;
                Segments.Tint = value.WithValue(0.5f);
                Glow.Color = value.WithAlpha(0.8f);
                Glow.EmissiveColor = value;
                foreach (var corner in corners)
                {
                    corner.Color = value;
                    corner.EmissiveColor = value;
                }
            }
        }


        void Awake()
        {
            Disc.StartDragging += StartWriting;
            Disc.EndDragging += StopWriting;

            Send.Clicked += SendTrajectory;
            Reset.Clicked += ResetTrajectory;
            Back.Clicked += UndoTrajectory;

            Lines.ElementScale = 0.08f;
            Lines.RenderType = LineDisplay.LineRenderType.AlwaysCapsule;

            Segments.ElementScale = 0.08f;
            Segments.RenderType = LineDisplay.LineRenderType.AlwaysCapsule;

            Send.Icon = XRIcon.Ok;
            Caption = "Send";

            Back.Icon = XRIcon.Backward;
            Back.Caption = "Back";

            Reset.Icon = XRIcon.Cross;
            Reset.Caption = "Reset";

            Color = new Color(0, 0.6f, 1f);
            SecondColor = Color.cyan;
        }

        void StartWriting()
        {
            //startTime = Time.time;
            Holder.SetActive(false);
            Glow.Visible = true;
            Sphere.EmissiveColor = Color;

            GameThread.EveryFrame += UpdateSegment;

            //positions.Clear();
            if (positions.Count == 0)
            {
                positions.Add(Disc.Transform.localPosition);
            }
        }

        void StopWriting()
        {
            Holder.SetActive(true);
            Glow.Visible = false;
            Sphere.EmissiveColor = Color.black;

            GameThread.EveryFrame -= UpdateSegment;

            var currentPosition = Disc.Transform.localPosition;
            positions.Add(currentPosition);
            
            LineUtils.AddLineStipple(lineBuffer, positions[^2], currentPosition, stippleLength: 0.25f);
            Lines.Set(lineBuffer.AsReadOnlySpan(), false);

            ResetCorners();
            
            Segments.Reset();

            ProvidedTrajectory?.Invoke(positions, period);
        }

        void SendTrajectory()
        {
            positions.Clear();
            lineBuffer.Clear();
            Lines.Reset();
            ResetCorners();

            Disc.Transform.localPosition = Vector3.zero;
            Holder.SetActive(false);

            Clicked?.Invoke(0);
        }

        void ResetTrajectory()
        {
            positions.Clear();
            lineBuffer.Clear();
            Lines.Reset();
            ResetCorners();

            Disc.Transform.localPosition = Vector3.zero;
            Holder.SetActive(false);
            ProvidedTrajectory?.Invoke(positions, period);
        }

        void ResetCorners()
        {
            int cornersCount = corners.Count;
            int positionsCount = positions.Count;
            int min = Mathf.Min(cornersCount, positionsCount);
            for (int i = 0; i < min; i++)
            {
                corners[i].Transform.localPosition = positions[i];
            }
            
            if (cornersCount < positionsCount)
            {
                for (int i = cornersCount; i < positionsCount; i++)
                {
                    corners.Add(CreateCorner(positions[i], Transform, SecondColor));
                }
            }
            else
            {
                for (int i = positionsCount; i < cornersCount; i++)
                {
                    corners[i].ReturnToPool(Resource.Displays.Cube);
                }
                
                corners.RemoveRange(positionsCount, cornersCount - positionsCount);
            }
        }
        
        static MeshMarkerDisplay CreateCorner(Vector3 position, Transform transform, Color color)
        {
            var newCorner = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Cube, transform);
            newCorner.Color = color;
            newCorner.EmissiveColor = color;
            newCorner.Transform.localPosition = position;
            newCorner.Transform.localScale = Vector3.one * (0.1f * 3);
            newCorner.EnableShadows = false;
            return newCorner;
        }
        
        void UndoTrajectory()
        {
            if (positions.Count == 0)
            {
                return;
            }

            positions.RemoveAt(positions.Count - 1);

            lineBuffer.Clear();
            for (int i = 0; i < positions.Count - 1; i++)
            {
                LineUtils.AddLineStipple(lineBuffer, positions[i], positions[i + 1], stippleLength: 0.25f);
            }

            Lines.Set(lineBuffer.AsReadOnlySpan(), false);
            Segments.Reset();
            ResetCorners();

            Disc.Transform.localPosition = positions[^1];
            ProvidedTrajectory?.Invoke(positions, period);
        }

        void UpdateSegment()
        {
            var currentPosition = Disc.Transform.localPosition;
            ReadOnlySpan<LineWithColor> line = stackalloc LineWithColor[]
            {
                new LineWithColor(positions[^1], currentPosition)
            };
            Segments.Set(line);
        }

        public void Suspend()
        {
            ProvidedTrajectory = null;
            Lines.Reset();
            Segments.Reset();
            ResetCorners();
            lineBuffer.Clear();
            //startTime = null;
            Send.Transform.parent = Transform;
            Lines.Transform.parent = Transform;
            GameThread.EveryFrame -= UpdateSegment;

            Holder.SetActive(false);
            Glow.Visible = false;
            Sphere.EmissiveColor = Color.black;
        }

        public void SplitForRecycle()
        {
            lines.ReturnToPool();
        }

        public override string ToString() => $"[{nameof(TrajectoryDisc)}]";
    }
}
using System;
using System.Collections.Generic;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XR;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class TrajectoryDisc : ARWidget, IRecyclable
    {
        [SerializeField] XRButton button = null;
        [SerializeField] float period = 0.1f;

        BillboardDiscDisplay disc;

        float? startTime;
        readonly List<Vector3> positions = new();
        readonly NativeList<LineWithColor> lineBuffer = new();

        [CanBeNull] LineDisplay lines;
        [NotNull] LineDisplay Lines => ResourcePool.RentChecked(ref lines, TfModule.RootFrame.Transform);

        public event Action<Vector3[], float> Moved;

        string mainButtonCaption = "Send!";
        public string MainButtonCaption
        {
            get => mainButtonCaption;
            set
            {
                mainButtonCaption = value;
                button.Caption = value;
            }
        }
        
        protected override void Awake()
        {
            base.Awake();
            disc = GetComponent<BillboardDiscDisplay>();
            disc.StartDragging += StartWriting;
            disc.EndDragging += StopWriting;

            button.Clicked += SendTrajectory;

            Lines.ElementScale = 0.02f;
            Lines.RenderType = LineDisplay.LineRenderType.AlwaysCapsule;

            button.Icon = XRIcon.Ok;
            button.Visible = false;
            button.Transform.parent = Transform.parent;
            MainButtonCaption = mainButtonCaption;
        }

        void StartWriting()
        {
            startTime = Time.time;
            positions.Clear();
            Lines.Reset();
            button.Visible = false;
        }

        void StopWriting()
        {
            startTime = null;
            if (positions.Count > 2)
            {
                button.Visible = true;
                button.Transform.SetLocalPose(Transform.AsLocalPose().Multiply(BaseButtonPose));
            }
        }

        void SendTrajectory()
        {
            Debug.Log($"{this}: Sending trajectory");
            Moved?.Invoke(positions.ToArray(), period);
        }

        public override void Initialize()
        {
            base.Initialize();
            Lines.Transform.parent = Transform.parent;
            button.Transform.parent = Transform.parent;
        }

        static readonly Pose BaseButtonPose = new(
            new Vector3(-0.5f, 0, -0.5f),
            Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(180, Vector3.up)
        );

        protected override void Update()
        {
            base.Update();
            if (button.Visible)
            {
                button.Transform.SetLocalPose(Transform.AsLocalPose().Multiply(BaseButtonPose));
            }

            if (startTime == null)
            {
                return;
            }

            float time = Time.time;
            float expectedTime = startTime.Value + positions.Count * period;
            Vector3 currentPosition = Transform.localPosition;
            if (time < expectedTime && (positions.Count != 0 &&
                                        Vector3.Distance(positions[^1], currentPosition) < 0.1f))
            {
                return;
            }

            positions.Add(currentPosition);
            lineBuffer.Clear();
            for (int i = 0; i < positions.Count - 1; i++)
            {
                lineBuffer.Add(new LineWithColor(positions[i], positions[i + 1]));
            }

            Lines.Set(lineBuffer, false);
        }

        public override void Suspend()
        {
            base.Suspend();
            Moved = null;
            Lines.Reset();
            lineBuffer.Clear();
            startTime = null;
            button.Transform.parent = Transform;
            Lines.Transform.parent = Transform;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            lineBuffer.Dispose();
        }

        void IRecyclable.SplitForRecycle()
        {
            lines.ReturnToPool();
        }
    }
}
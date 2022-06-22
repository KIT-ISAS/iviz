#nullable enable

using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class BoundaryLinkDisplay : MonoBehaviour, IDisplay, ISupportsColor, IRecyclable
    {
        [SerializeField] BoxCollider? startCollider;
        [SerializeField] BoxCollider? endCollider;
        MeshMarkerDisplay? line;
        SelectionFrame? frame;
        Tooltip? tooltip;
        Color color = Color.red;

        Transform? mTransform;

        Transform Transform => this.EnsureHasTransform(ref mTransform);
        MeshMarkerDisplay Line => ResourcePool.RentChecked(ref line, Resource.Displays.Cube, Transform);
        SelectionFrame Frame => ResourcePool.RentChecked(ref frame, Transform);
        Tooltip Tooltip => ResourcePool.RentChecked(ref tooltip, Transform);

        void Awake()
        {
            Color = color;
            Tooltip.Scale = 0.03f;
            Frame.ColumnWidth = 0.001f;

            if (startCollider != null && endCollider != null)
            {
                Set(startCollider, endCollider);
            }
        }

        public void Set(BoxCollider a, BoxCollider b)
        {
            startCollider = a;
            endCollider = b;
            Transform.SetParentLocal(TfModule.RootFrame.Transform);

            Frame.Transform.parent = b.gameObject.transform;
            Frame.SetBounds(b.GetLocalBounds());

            GameThread.EveryFrame += UpdateLink;
            UpdateLink();
        }

        void UpdateLink()
        {
            if (startCollider == null || endCollider == null)
            {
                return;
            }

            float distance = DistanceTo(startCollider, endCollider, out var start, out var end);
            var mid = (start + end) / 2;

            if (distance > 0)
            {
                Line.Transform.localPosition = mid;
                Line.Transform.localRotation = Quaternion.LookRotation(end - start);
                Line.Transform.localScale = new Vector3(0.02f, 0.02f, distance);
            }

            if (distance < 0.5f)
            {
                Tooltip.Visible = false;
                return;
            }

            Tooltip.Visible = true;
            Tooltip.Transform.localPosition = mid + (Settings.MainCameraPose.position - mid).normalized * 0.25f;
            Tooltip.Caption = distance.ToString("#,0.###", UnityUtils.Culture);
        }

        public void Suspend()
        {
            startCollider = null;
            endCollider = null;
            Frame.Transform.parent = Transform;
            GameThread.EveryFrame -= UpdateLink;
        }

        public Color Color
        {
            set
            {
                color = value;
                Line.Color = value;
                Line.EmissiveColor = value;
                Tooltip.Color = (value / 2).WithAlpha(1);
                Frame.Color = value;
                Frame.EmissiveColor = value;
            }
        }

        public Color EmissiveColor
        {
            set => Line.EmissiveColor = value;
        }

        public void SplitForRecycle()
        {
            tooltip.ReturnToPool();
            frame.ReturnToPool();
            line.ReturnToPool(Resource.Displays.Cube);
        }

        public static float DistanceTo(BoxCollider a, BoxCollider b, out Vector3 start, out Vector3 end)
        {
            end = b.ClosestPoint(a.bounds.center);
            start = a.ClosestPoint(end);

            end = TfModule.RootFrame.Transform.InverseTransformPoint(end);
            start = TfModule.RootFrame.Transform.InverseTransformPoint(start);

            return Vector3.Distance(start, end);
        }
    }
}
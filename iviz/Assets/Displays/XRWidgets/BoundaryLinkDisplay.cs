#nullable enable

using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public class BoundaryLinkDisplay : MonoBehaviour, IDisplay, ISupportsColor, IRecyclable
    {
        [SerializeField] BoxCollider? startCollider; 
        [SerializeField] BoxCollider? endCollider;
        MeshMarkerDisplay? line;
        SelectionFrame? frame;
        Tooltip? tooltip;
        Color color = Color.red;

        Transform? mTransform;

        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        MeshMarkerDisplay Line =>
            line != null ? line : line = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Cube, Transform);

        SelectionFrame Frame =>
            frame != null ? frame : frame = ResourcePool.RentDisplay<SelectionFrame>(Transform);

        Tooltip Tooltip => tooltip != null ? tooltip : tooltip = ResourcePool.RentDisplay<Tooltip>(Transform);

        void Awake()
        {
            Color = color;
            Tooltip.Scale = 0.03f;
            Frame.ColumnWidth = 0.02f;
            
            if (startCollider != null && endCollider != null)
            {
                Set(startCollider, endCollider);
            }
        }
        
        public void Set(BoxCollider a, BoxCollider b)
        {
            startCollider = a;
            endCollider = b;
            Transform.SetParentLocal(TfModule.UnityFrameTransform);
            
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

            float distance = startCollider.DistanceTo(endCollider, out var start, out var end);
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
            Tooltip.Transform.localPosition = mid + (Settings.MainCameraTransform.position - mid).normalized * 0.25f;
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
    }
}
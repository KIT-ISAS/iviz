using System.Collections.Generic;
using System.Reflection;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public abstract class DataPanelContents : MonoBehaviour
    {
        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        List<IWidget> cachedWidgets;

        public void ClearSubscribers()
        {
            if (cachedWidgets == null)
            {
                SetWidgets();
            }
            cachedWidgets.ForEach(w => w.ClearSubscribers());
        }

        void SetWidgets()
        {
            cachedWidgets = new List<IWidget>();
            PropertyInfo[] properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                if (typeof(IWidget).IsAssignableFrom(property.PropertyType))
                {
                    cachedWidgets.Add((IWidget)property.GetValue(this));
                }
            }
        }

        public static DataPanelContents AddTo(GameObject o, Resource.Module resource)
        {
            switch (resource)
            {
                case Resource.Module.TF: return o.AddComponent<TFPanelContents>();
                case Resource.Module.PointCloud: return o.AddComponent<PointCloudPanelContents>();
                case Resource.Module.Grid: return o.AddComponent<GridPanelContents>();
                case Resource.Module.Image: return o.AddComponent<ImagePanelContents>();
                case Resource.Module.Robot: return o.AddComponent<RobotPanelContents>();
                case Resource.Module.Marker: return o.AddComponent<MarkerPanelContents>();
                case Resource.Module.InteractiveMarker: return o.AddComponent<InteractiveMarkerPanelContents>();
                case Resource.Module.JointState: return o.AddComponent<JointStatePanelContents>();
                case Resource.Module.DepthImageProjector: return o.AddComponent<DepthImageProjectorPanelContents>();
                case Resource.Module.LaserScan: return o.AddComponent<LaserScanPanelContents>();
                case Resource.Module.AR: return o.AddComponent<ARPanelContents>();
                case Resource.Module.Odometry: return o.AddComponent<OdometryPanelContents>();
                default: return o.AddComponent<DefaultPanelContents>();
            }
        }
    }
    public abstract class ListenerPanelContents : DataPanelContents
    {
        public ListenerWidget Listener { get; protected set; }
    }
}
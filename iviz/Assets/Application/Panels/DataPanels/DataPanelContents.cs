using System;
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
                FindWidgets();
            }
            cachedWidgets.ForEach(w => w.ClearSubscribers());
        }

        void FindWidgets()
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
                //case Resource.Module.LegacyRobot: return o.AddComponent<RobotPanelContents>();
                case Resource.Module.Robot: return o.AddComponent<SimpleRobotPanelContents>();
                case Resource.Module.Marker: return o.AddComponent<MarkerPanelContents>();
                case Resource.Module.InteractiveMarker: return o.AddComponent<InteractiveMarkerPanelContents>();
                case Resource.Module.JointState: return o.AddComponent<JointStatePanelContents>();
                case Resource.Module.DepthCloud: return o.AddComponent<DepthCloudPanelContents>();
                case Resource.Module.LaserScan: return o.AddComponent<LaserScanPanelContents>();
                case Resource.Module.AugmentedReality: return o.AddComponent<ARPanelContents>();
                case Resource.Module.Magnitude: return o.AddComponent<MagnitudePanelContents>();
                case Resource.Module.OccupancyGrid: return o.AddComponent<OccupancyGridPanelContents>();
                case Resource.Module.Joystick: return o.AddComponent<JoystickPanelContents>();
                case Resource.Module.Path: return o.AddComponent<PathPanelContents>();
                case Resource.Module.GridMap: return o.AddComponent<GridMapPanelContents>();
                default: throw new ArgumentException(nameof(resource));
            }
        }
    }
}
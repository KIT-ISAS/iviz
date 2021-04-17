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
            cachedWidgets.ForEach(widget => widget.ClearSubscribers());
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

        public static DataPanelContents AddTo(GameObject o, Resource.ModuleType resource)
        {
            switch (resource)
            {
                case Resource.ModuleType.TF: return o.AddComponent<TfPanelContents>();
                case Resource.ModuleType.PointCloud: return o.AddComponent<PointCloudPanelContents>();
                case Resource.ModuleType.Grid: return o.AddComponent<GridPanelContents>();
                case Resource.ModuleType.Image: return o.AddComponent<ImagePanelContents>();
                case Resource.ModuleType.Robot: return o.AddComponent<SimpleRobotPanelContents>();
                case Resource.ModuleType.Marker: return o.AddComponent<MarkerPanelContents>();
                case Resource.ModuleType.InteractiveMarker: return o.AddComponent<InteractiveMarkerPanelContents>();
                case Resource.ModuleType.JointState: return o.AddComponent<JointStatePanelContents>();
                case Resource.ModuleType.DepthCloud: return o.AddComponent<DepthCloudPanelContents>();
                case Resource.ModuleType.LaserScan: return o.AddComponent<LaserScanPanelContents>();
                case Resource.ModuleType.AugmentedReality: return o.AddComponent<ARPanelContents>();
                case Resource.ModuleType.Magnitude: return o.AddComponent<MagnitudePanelContents>();
                case Resource.ModuleType.OccupancyGrid: return o.AddComponent<OccupancyGridPanelContents>();
                case Resource.ModuleType.Joystick: return o.AddComponent<JoystickPanelContents>();
                case Resource.ModuleType.Path: return o.AddComponent<PathPanelContents>();
                case Resource.ModuleType.GridMap: return o.AddComponent<GridMapPanelContents>();
                case Resource.ModuleType.Octomap: return o.AddComponent<OctomapPanelContents>();
                case Resource.ModuleType.ARGuiSystem: return o.AddComponent<ARGuiPanelContents>();
                default: throw new ArgumentException(nameof(resource));
            }
        }
    }
}
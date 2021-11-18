using System;
using System.Collections.Generic;
using System.Reflection;
using Iviz.Common;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public abstract class DataPanelContents : MonoBehaviour
    {
        public ToggleButtonWidget HideButton { get; protected set; }
        
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

        public static DataPanelContents AddTo(GameObject o, ModuleType resource)
        {
            switch (resource)
            {
                case ModuleType.TF: return o.AddComponent<TfPanelContents>();
                case ModuleType.PointCloud: return o.AddComponent<PointCloudPanelContents>();
                case ModuleType.Grid: return o.AddComponent<GridPanelContents>();
                case ModuleType.Image: return o.AddComponent<ImagePanelContents>();
                case ModuleType.Robot: return o.AddComponent<SimpleRobotPanelContents>();
                case ModuleType.Marker: return o.AddComponent<MarkerPanelContents>();
                case ModuleType.InteractiveMarker: return o.AddComponent<InteractiveMarkerPanelContents>();
                case ModuleType.JointState: return o.AddComponent<JointStatePanelContents>();
                case ModuleType.DepthCloud: return o.AddComponent<DepthCloudPanelContents>();
                case ModuleType.LaserScan: return o.AddComponent<LaserScanPanelContents>();
                case ModuleType.AugmentedReality: return o.AddComponent<ARPanelContents>();
                case ModuleType.Magnitude: return o.AddComponent<MagnitudePanelContents>();
                case ModuleType.OccupancyGrid: return o.AddComponent<OccupancyGridPanelContents>();
                case ModuleType.Joystick: return o.AddComponent<JoystickPanelContents>();
                case ModuleType.Path: return o.AddComponent<PathPanelContents>();
                case ModuleType.GridMap: return o.AddComponent<GridMapPanelContents>();
                case ModuleType.Octomap: return o.AddComponent<OctomapPanelContents>();
                case ModuleType.GuiDialog: return o.AddComponent<GuiDialogPanelContents>();
                default: throw new ArgumentException(nameof(resource));
            }
        }
    }
}
#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iviz.Common;
using Iviz.Core;
using UnityEngine;

namespace Iviz.App
{
    public abstract class DataPanelContents : MonoBehaviour
    {
        List<IWidget>? cachedWidgets;
        ToggleButtonWidget? hideButton;

        public ToggleButtonWidget HideButton
        {
            get => hideButton != null ? hideButton : throw new NullReferenceException("Hide button has not been set!");
            protected set => hideButton = value.CheckedNull() ?? throw new ArgumentNullException(nameof(value));
        }

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            cachedWidgets ??= FindWidgets();
            foreach (var widget in cachedWidgets)
            {
                widget.ClearSubscribers();
            }
        }

        List<IWidget> FindWidgets()
        {
            return GetType().GetProperties()
                .Where(property => typeof(IWidget).IsAssignableFrom(property.PropertyType))
                .Select(property => (IWidget)property.GetValue(this))
                .ToList();
        }

        public static DataPanelContents AddTo(GameObject o, ModuleType resource)
        {
            return resource switch
            {
                ModuleType.TF => o.AddComponent<TfPanelContents>(),
                ModuleType.PointCloud => o.AddComponent<PointCloudPanelContents>(),
                ModuleType.Grid => o.AddComponent<GridPanelContents>(),
                ModuleType.Image => o.AddComponent<ImagePanelContents>(),
                ModuleType.Robot => o.AddComponent<SimpleRobotPanelContents>(),
                ModuleType.Marker => o.AddComponent<MarkerPanelContents>(),
                ModuleType.InteractiveMarker => o.AddComponent<InteractiveMarkerPanelContents>(),
                ModuleType.JointState => o.AddComponent<JointStatePanelContents>(),
                ModuleType.DepthCloud => o.AddComponent<DepthCloudPanelContents>(),
                ModuleType.LaserScan => o.AddComponent<LaserScanPanelContents>(),
                ModuleType.AugmentedReality => o.AddComponent<ARPanelContents>(),
                ModuleType.Magnitude => o.AddComponent<MagnitudePanelContents>(),
                ModuleType.OccupancyGrid => o.AddComponent<OccupancyGridPanelContents>(),
                ModuleType.Joystick => o.AddComponent<JoystickPanelContents>(),
                ModuleType.Path => o.AddComponent<PathPanelContents>(),
                ModuleType.GridMap => o.AddComponent<GridMapPanelContents>(),
                ModuleType.Octomap => o.AddComponent<OctomapPanelContents>(),
                ModuleType.GuiDialog => o.AddComponent<GuiDialogPanelContents>(),
                ModuleType.XR => o.AddComponent<XRPanelContents>(),
                ModuleType.Camera => o.AddComponent<CameraPanelContents>(),
                ModuleType.TFPublisher => o.AddComponent<TfPublisherPanelContents>(),
                _ => throw new ArgumentException($"Failed to find panel contents for module type '{resource}'",
                    nameof(resource))
            };
        }
    }
}
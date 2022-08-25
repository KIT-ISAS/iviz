#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Common;
using Iviz.Core;
using UnityEngine;

namespace Iviz.App
{
    public abstract class ModulePanel : MonoBehaviour
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

        public static ModulePanel AddResourceToDataPanel(GameObject o, ModuleType resource)
        {
            return resource switch
            {
                ModuleType.Invalid => throw new ArgumentException("Cannot create module panel for resource Invalid",
                    nameof(resource)),
                ModuleType.TF => o.AddComponent<TfModulePanel>(),
                ModuleType.PointCloud => o.AddComponent<PointCloudModulePanel>(),
                ModuleType.Grid => o.AddComponent<GridModulePanel>(),
                ModuleType.Image => o.AddComponent<ImageModulePanel>(),
                ModuleType.Robot => o.AddComponent<SimpleRobotModulePanel>(),
                ModuleType.Marker => o.AddComponent<MarkerModulePanel>(),
                ModuleType.InteractiveMarker => o.AddComponent<InteractiveMarkerModulePanel>(),
                ModuleType.JointState => o.AddComponent<JointStateModulePanel>(),
                ModuleType.DepthCloud => o.AddComponent<DepthCloudModulePanel>(),
                ModuleType.LaserScan => o.AddComponent<LaserScanModulePanel>(),
                ModuleType.AR => o.AddComponent<ARModulePanel>(),
                ModuleType.Magnitude => o.AddComponent<MagnitudeModulePanel>(),
                ModuleType.OccupancyGrid => o.AddComponent<OccupancyGridModulePanel>(),
                ModuleType.Joystick => o.AddComponent<JoystickModulePanel>(),
                ModuleType.Path => o.AddComponent<PathModulePanel>(),
                ModuleType.GridMap => o.AddComponent<GridMapModulePanel>(),
                ModuleType.Octomap => o.AddComponent<OctomapModulePanel>(),
                ModuleType.VizWidget => o.AddComponent<VizWidgetModulePanel>(),
                ModuleType.XR => o.AddComponent<XRModulePanel>(),
                ModuleType.Camera => o.AddComponent<CameraPanel>(),
                ModuleType.TFPublisher => o.AddComponent<PublishedFramePanel>(),
                _ => throw new IndexOutOfRangeException()
            };
        }
    }
}
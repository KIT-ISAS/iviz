#nullable enable

using UnityEngine;
using System;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Displays;
using UnityEngine.Rendering;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed class GridController : IController
    {
        readonly FrameNode node;
        readonly ReflectionProbe reflectionProbe;
        readonly GridResource grid;
        readonly GridConfiguration config = new();

        public GridConfiguration Config
        {
            get => config;
            private set
            {
                Orientation = value.Orientation;
                Visible = value.Visible;
                GridColor = value.GridColor;
                InteriorColor = value.InteriorColor;
                InteriorVisible = value.InteriorVisible;
                FollowCamera = value.FollowCamera;
                HideInARMode = value.HideInARMode;
                Offset = value.Offset;
            }
        }

        GridOrientation Orientation
        {
            get => config.Orientation;
            set
            {
                config.Orientation = value;
                grid.Orientation = value;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;

                bool arEnabled = ARController.Instance is { Visible: true };
                bool gridVisible = value && (arEnabled && !HideInARMode || !arEnabled);
                if (!gridVisible)
                {
                    reflectionProbe.transform.parent = TfListener.DefaultFrame.Transform;
                    grid.Visible = false;
                }
                else
                {
                    grid.Visible = true;
                    reflectionProbe.transform.parent = grid.Transform;
                    reflectionProbe.transform.localPosition = new Vector3(0, 0, -2);
                }

                reflectionProbe.RenderProbe();
            }
        }

        public Color GridColor
        {
            get => config.GridColor;
            set
            {
                config.GridColor = value;
                grid.GridColor = value;
                UpdateMesh();
            }
        }

        public Color InteriorColor
        {
            get => config.InteriorColor;
            set
            {
                config.InteriorColor = value;
                grid.InteriorColor = value;
                UpdateMesh();
            }
        }

        public bool InteriorVisible
        {
            get => config.InteriorVisible;
            set
            {
                config.InteriorVisible = value;
                grid.ShowInterior = value;
                UpdateMesh();
            }
        }

        public bool FollowCamera
        {
            get => config.FollowCamera;
            set
            {
                config.FollowCamera = value;
                grid.FollowCamera = value;
            }
        }

        public bool HideInARMode
        {
            get => config.HideInARMode;
            set
            {
                config.HideInARMode = value;
                Visible = Visible; // update
            }
        }

        public Vector3 Offset
        {
            get => config.Offset;
            set
            {
                config.Offset = value;
                node.transform.localPosition = value.Ros2Unity();
                UpdateMesh();
            }
        }

        public GridController(GridConfiguration? config)
        {
            grid = ResourcePool.RentDisplay<GridResource>();
            grid.name = "Grid";
            grid.Layer = LayerType.Collider;

            node = FrameNode.Instantiate("GridNode");
            grid.transform.SetParentLocal(node.transform);

            reflectionProbe = new GameObject().AddComponent<ReflectionProbe>();
            reflectionProbe.gameObject.name = "Grid Reflection Probe";
            reflectionProbe.transform.SetParentLocal(grid.transform);
            reflectionProbe.transform.localPosition = new Vector3(0, 2.0f, 0);
            reflectionProbe.nearClipPlane = 0.5f;
            reflectionProbe.farClipPlane = 100f;
            reflectionProbe.backgroundColor = Settings.MainCamera.backgroundColor;
            reflectionProbe.mode = ReflectionProbeMode.Realtime;

            if (Settings.IsMobile)
            {
                reflectionProbe.enabled = false;
            }
            else
            {
                reflectionProbe.timeSlicingMode = ReflectionProbeTimeSlicingMode.IndividualFaces;
                reflectionProbe.refreshMode = ReflectionProbeRefreshMode.EveryFrame;
                reflectionProbe.clearFlags = ReflectionProbeClearFlags.SolidColor;
            }

            UpdateMesh();

            Config = config ?? new GridConfiguration();
        }

        void UpdateMesh()
        {
            const int numberOfGridCells = 90;
            const float gridCellSize = 1;
            const float totalSize = numberOfGridCells * gridCellSize;

            reflectionProbe.size = new Vector3(totalSize * 2, 20f, totalSize * 2);
            reflectionProbe.RenderProbe();
        }

        public void Dispose()
        {
            grid.ReturnToPool();
            node.DestroySelf();
            UnityEngine.Object.Destroy(reflectionProbe.gameObject);
        }

        public void ResetController()
        {
        }

        public void OnSettingsChanged()
        {
            reflectionProbe.backgroundColor = Settings.SettingsManager.BackgroundColor;
        }
    }
}
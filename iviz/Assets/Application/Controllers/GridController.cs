using UnityEngine;
using System;
using System.Runtime.Serialization;
using Iviz.App;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Ros;
using JetBrains.Annotations;
using UnityEngine.Rendering;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    [DataContract]
    public class GridConfiguration : IConfiguration
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Grid;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public GridOrientation Orientation { get; set; } = GridOrientation.XY;
        [DataMember] public SerializableColor GridColor { get; set; } = GridController.DefaultColor * 0.25f;

        [DataMember] public SerializableColor InteriorColor { get; set; } = GridController.DefaultColor;

        //[DataMember] public float GridLineWidth { get; set; } = 0.025f;
        //[DataMember] public float GridCellSize { get; set; } = 1;
        //[DataMember] public int NumberOfGridCells { get; set; } = 90;
        [DataMember] public bool InteriorVisible { get; set; } = true;
        [DataMember] public bool FollowCamera { get; set; } = true;
        [DataMember] public bool PublishLongTapPosition { get; set; } = false;
        [DataMember] public string TapTopic { get; set; } = "~clicked_point";
        [DataMember] public bool HideInARMode { get; set; } = true;
        [DataMember] public SerializableVector3 Offset { get; set; } = Vector3.zero;
    }

    public sealed class GridController : IController
    {
        //public static readonly Color DefaultColor = new Color(0.12f, 0.14f, 0.19f); 
        public static readonly Color DefaultColor = (0.6f * Color.white).WithAlpha(1);

        const int ProbeRefreshTimeInSec = 5;

        readonly FrameNode node;
        readonly ReflectionProbe reflectionProbe;
        readonly GridResource grid;

        Point? lastTapPosition;
        uint clickedSeq;

        public IModuleData ModuleData { get; }

        readonly GridConfiguration config = new GridConfiguration();

        public Sender<PointStamped> SenderPoint { get; private set; }

        public GridConfiguration Config
        {
            get => config;
            set
            {
                Orientation = value.Orientation;
                Visible = value.Visible;
                GridColor = value.GridColor;
                InteriorColor = value.InteriorColor;
                /*
                GridLineWidth = value.GridLineWidth;
                GridCellSize = value.GridCellSize;
                NumberOfGridCells = value.NumberOfGridCells;
                */
                InteriorVisible = value.InteriorVisible;
                FollowCamera = value.FollowCamera;
                HideInARMode = value.HideInARMode;
                PublishLongTapPosition = value.PublishLongTapPosition;
                Offset = value.Offset;
            }
        }

        public GridOrientation Orientation
        {
            get => config.Orientation;
            set
            {
                config.Orientation = value;
                grid.Orientation = value;
                //reflectionProbe.transform.localPosition = new Vector3(0, 0, -2);
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;

                bool arEnabled = ARController.InstanceVisible;
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

        /*
        public float GridLineWidth
        {
            get => config.GridLineWidth;
            set
            {
                config.GridLineWidth = value;
                grid.GridLineWidth = value;
                UpdateMesh();
            }
        }
        */

        /*
        public float GridCellSize
        {
            get => config.GridCellSize;
            set
            {
                config.GridCellSize = value;
                grid.GridCellSize = value;
                UpdateMesh();
            }
        }
        */

        /*
        public int NumberOfGridCells
        {
            get => config.NumberOfGridCells;
            set
            {
                config.NumberOfGridCells = value;
                grid.NumberOfGridCells = value;
                UpdateMesh();
            }
        }
        */

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

        public SerializableVector3 Offset
        {
            get => config.Offset;
            set
            {
                config.Offset = value;
                node.transform.localPosition = ((Vector3) value).Ros2Unity();
                UpdateMesh();
            }
        }

        public bool PublishLongTapPosition
        {
            get => config.PublishLongTapPosition;
            set
            {
                if (value && SenderPoint == null)
                {
                    SenderPoint = new Sender<PointStamped>(TapTopic);
                    GuiInputModule.Instance.LongClick += OnLongClick;
                }
                else if (!value && SenderPoint != null)
                {
                    SenderPoint.Stop();
                    SenderPoint = null;
                    GuiInputModule.Instance.LongClick -= OnLongClick;
                }

                config.PublishLongTapPosition = value;
            }
        }

        [NotNull]
        public string TapTopic
        {
            get => config.TapTopic;
            set
            {
                config.TapTopic = value;
                if (SenderPoint != null && SenderPoint.Topic != value)
                {
                    SenderPoint.Stop();
                    SenderPoint = new Sender<PointStamped>(value);
                }
            }
        }

        [NotNull]
        public string LastTapPositionString =>
            lastTapPosition == null
                ? "<b>Last Tap Position:</b>\n<i>None</i>"
                : $"<b>Last Tap Position:</b>\n[X:{lastTapPosition.Value.X:N3} Y:{lastTapPosition.Value.Y:N3} Z:{lastTapPosition.Value.Z:N3}]";


        //void Awake()
        public GridController([NotNull] IModuleData moduleData)
        {
            grid = ResourcePool.RentDisplay<GridResource>();
            grid.name = "Grid";

            node = FrameNode.Instantiate("GridNode");
            grid.transform.SetParentLocal(node.transform);

            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

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

            //GameThread.EverySecond += CheckProbeUpdate;

            Config = new GridConfiguration();
        }

        void OnLongClick(Vector2 cameraPoint)
        {
            if (SenderPoint != null && grid.TryRaycast(cameraPoint, out Vector3 hit))
            {
                lastTapPosition = TfListener.RelativePositionToOrigin(hit).Unity2RosPoint();
                SenderPoint.Publish(new PointStamped
                {
                    Header = (clickedSeq++, TfListener.FixedFrameId),
                    Point = lastTapPosition.Value
                });
                ModuleData.ResetPanel();
            }
        }

        void UpdateMesh()
        {
            const int numberOfGridCells = 90;
            const float gridCellSize = 1;

            float totalSize = numberOfGridCells * gridCellSize;
            reflectionProbe.size = new Vector3(totalSize * 2, 20f, totalSize * 2);
            reflectionProbe.RenderProbe();
        }

        public void StopController()
        {
            grid.ReturnToPool();
            node.DestroySelf();
            UnityEngine.Object.Destroy(reflectionProbe.gameObject);

            GuiInputModule.Instance.LongClick -= OnLongClick;
            //GameThread.EverySecond -= CheckProbeUpdate;
        }

        public void ResetController()
        {
            /*
            if (reflectionProbe)
            {
                reflectionProbe.RenderProbe();
            }
            */
        }

        public void OnSettingsChanged()
        {
            if (Settings.SettingsManager != null)
            {
                reflectionProbe.backgroundColor = Settings.SettingsManager.BackgroundColor;
            }
        }


        /*
        int ticks;

        void CheckProbeUpdate()
        {
            ticks++;
            if (ticks == ProbeRefreshTimeInSec)
            {
                if (reflectionProbe != null)
                {
                    reflectionProbe.backgroundColor = Settings.SettingsManager.BackgroundColor;
                    reflectionProbe.RenderProbe();
                }

                ticks = 0;
            }
        }
                 */
    }
}
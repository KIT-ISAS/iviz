using UnityEngine;
using Iviz.Roslib;
using System.Runtime.Serialization;
using System;
using Iviz.App;
using Iviz.Resources;
using Iviz.Displays;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class ARConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.AugmentedReality;
        [DataMember] public bool Visible { get; set; } = true;
        /* NonSerializable */ public float WorldScale { get; set; } = 1.0f;
        /* NonSerializable */ public SerializableVector3 WorldOffset { get; set; } = ARController.DefaultWorldOffset;
        /* NonSerializable */ public float WorldAngle { get; set; } = 0;
        [DataMember] public bool SearchMarker { get; set; } = false;
        [DataMember] public bool MarkerHorizontal { get; set; } = true;
        [DataMember] public int MarkerAngle { get; set; } = 0;
        [DataMember] public string MarkerFrame { get; set; } = "";
        [DataMember] public SerializableVector3 MarkerOffset { get; set; } = Vector3.zero;

        /* NonSerializable */ public bool ShowRootMarker { get; set; }
        /* NonSerializable */ public bool PinRootMarker { get; set; }
    }
    
    [DataContract]
    public sealed class ARSessionInfo : JsonToString
    {
        [DataMember] public float WorldScale { get; set; } = 1.0f;
        [DataMember] public SerializableVector3 WorldOffset { get; set; } = Vector3.zero;
        [DataMember] public float WorldAngle { get; set; } = 0;
        [DataMember] public bool ShowRootMarker { get; set; }
        [DataMember] public bool PinRootMarker { get; set; }
    }

    public abstract class ARController : MonoBehaviour, IController, IHasFrame
    {
        public static readonly Vector3 DefaultWorldOffset = new Vector3(0.5f, 0, -0.2f);

        public static ARController Instance { get; private set; }
        public static bool ARModeEnabled => Instance != null && Instance.Visible;

        public static event Action<bool> ARModeChanged;

        public enum RootMover
        {
            ModuleData,
            Anchor,
            ImageMarker,
            ControlMarker,
            Configuration,
        }
        
        
        static ARSessionInfo savedSessionInfo;
        
        protected Canvas canvas;
        protected DisplayClickableNode node;

        public IModuleData ModuleData { get; set; }

        readonly ARConfiguration config = new ARConfiguration();
        public ARConfiguration Config
        {
            get => config;
            set
            {
                Visible = value.Visible;
                WorldScale = value.WorldScale;
                UseMarker = value.SearchMarker;
                MarkerHorizontal = value.MarkerHorizontal;
                MarkerAngle = value.MarkerAngle;
                MarkerFrame = value.MarkerFrame;
                MarkerOffset = value.MarkerOffset;
            }
        }
        
        public Vector3 WorldPosition
        {
            get => config.WorldOffset;
            private set => config.WorldOffset = value;
        }
        
        public float WorldAngle
        {
            get => config.WorldAngle;
            private set => config.WorldAngle = value;
        }

        public Pose WorldPose { get; private set; } = Pose.identity;
    
        public event Action<RootMover> WorldPoseChanged;

        void UpdateWorldPose(in Pose pose, RootMover mover)
        {
            WorldPose = pose;
            if (Visible)
            {
                TFListener.RootFrame.transform.SetPose(pose);
            }

            WorldPoseChanged?.Invoke(mover);
        }

        protected void SetWorldPose(in Pose unityPose, RootMover mover)
        {
            float angle = unityPose.rotation.eulerAngles.y;
            if (angle > 180)
            {
                angle -= 360;
            }
            
            WorldPosition = unityPose.position;
            WorldAngle = angle;
            UpdateWorldPose(unityPose, mover);
        }

        void SetWorldPose(in Vector3 unityPosition, float angle, RootMover mover)
        {
            WorldPosition = unityPosition;
            WorldAngle = angle;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            UpdateWorldPose(new Pose(unityPosition, rotation), mover);
        }
        
        public void SetWorldPosition(in Vector3 unityPosition, RootMover mover)
        {
            WorldPosition = unityPosition;
            UpdateWorldPose(new Pose(unityPosition, WorldPose.rotation), mover);
        }

        public void SetWorldAngle(float angle, RootMover mover)
        {
            WorldAngle = angle;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            UpdateWorldPose(new Pose(WorldPosition, rotation), mover);
        }

        public float WorldScale
        {
            get => config.WorldScale;
            set
            {
                config.WorldScale = value;
                TfRootScale = value;
            }
        }

        public virtual bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                TFListener.RootFrame.transform.SetPose(value ? WorldPose : Pose.identity);
                ARModeChanged?.Invoke(value);
            }
        }

        public virtual bool UseMarker
        {
            get => config.SearchMarker;
            set => config.SearchMarker = value;
        }

        public virtual bool MarkerHorizontal
        {
            get => config.MarkerHorizontal;
            set => config.MarkerHorizontal = value;
        }

        public virtual int MarkerAngle
        {
            get => config.MarkerAngle;
            set => config.MarkerAngle = value;
        }

        public virtual string MarkerFrame
        {
            get => config.MarkerFrame;
            set => config.MarkerFrame = value;
        }    
        
        public virtual Vector3 MarkerOffset
        {
            get => config.MarkerOffset;
            set => config.MarkerOffset = value;
        }        
        
        public TFFrame Frame => node.Parent;

        static float TfRootScale
        {
            set => TFListener.RootFrame.transform.localScale = value * Vector3.one;
        }          
        
        public virtual bool PinRootMarker
        {
            get => config.PinRootMarker;
            set => config.PinRootMarker = value;
        }
        
        public bool ShowRootMarker
        {
            get => config.ShowRootMarker;
            set => config.ShowRootMarker = value;
        }
        
        protected virtual void Awake()
        {
            Instance = this;
            
            if (canvas == null)
            {
                canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            }

            node = DisplayClickableNode.Instantiate("AR Node");
            
            if (savedSessionInfo != null)
            {
                SetWorldPose(savedSessionInfo.WorldOffset, savedSessionInfo.WorldAngle, RootMover.Configuration);
                WorldScale = savedSessionInfo.WorldScale;
            }
            
            TFListener.RootMarker.SetTargetPoseUpdater(pose => SetWorldPose(pose, RootMover.ControlMarker));
        }
        
        public virtual bool FindClosest(in Vector3 position, out Vector3 anchor, out Vector3 normal)
        {
            Vector3 origin = position + 0.05f * Vector3.up;
            Ray ray = new Ray(origin, Vector3.down);
            return FindRayHit(ray, out anchor, out normal);
        }

        protected abstract bool FindRayHit(in Ray ray, out Vector3 anchor, out Vector3 normal);

        public virtual void Stop()
        {
            savedSessionInfo = new ARSessionInfo()
            {
                WorldAngle = WorldAngle,
                WorldOffset = WorldPosition,
                WorldScale = WorldScale
            };
            
            Visible = false;
            WorldScale = 1;

            TFListener.RootMarker.SetTargetPoseUpdater(pose => TFListener.RootFrame.transform.SetPose(pose));
            
            Instance = null;
        }
        
        public static Pose RelativePoseToWorld(in Pose unityPose)
        {
            //Debug.Log(Instance.WorldPose);
            return Instance == null ? unityPose : Instance.WorldPose.Inverse().Multiply(unityPose);
        }

        public virtual void Reset()
        {
        }
    }
}

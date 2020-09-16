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
        
        protected static Transform TfRoot => TFListener.RootFrame.transform;

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

        public Vector3 WorldOffset
        {
            get => config.WorldOffset;
            set
            {
                config.WorldOffset = value;
                TfRoot.SetPose(RootPose);
            }
        }
        
        public float WorldAngle
        {
            get => config.WorldAngle;
            set
            {
                config.WorldAngle = value;
                TfRoot.SetPose(RootPose);
            }
        }

        public float WorldScale
        {
            get => config.WorldScale;
            set
            {
                config.WorldScale = value;
                TfRoot.localScale = value * Vector3.one;
            }
        }

        public virtual bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;

                TfRoot.SetPose(value ? RootPose : Pose.identity);
                ModuleListPanel.Instance.OnARModeChanged(value);
                foreach (var module in ModuleListPanel.Instance.ModuleDatas)
                {
                    module.OnARModeChanged(value);
                }
                
                TFListener.MapFrame.UpdateAnchor(value);
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

        protected Pose RegisteredPose { get; set; } = Pose.identity;

        protected Pose RootPose
        {
            get
            {
                Pose offsetPose = new Pose(
                    WorldOffset.Ros2Unity(),
                    Quaternion.AngleAxis(WorldAngle, Vector3.up)
                    );
                return RegisteredPose.Multiply(offsetPose);
            }
        }

        public bool PinRootMarker
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
                WorldAngle = savedSessionInfo.WorldAngle;
                WorldOffset = savedSessionInfo.WorldOffset;
                WorldScale = savedSessionInfo.WorldScale;
            }
        }
        
        protected bool forceAnchorRebuild;


        public virtual bool FindClosest(in Vector3 position, out Vector3 anchor, out Vector3 normal)
        {
            Vector3 origin = position + 0.25f * Vector3.up;
            Ray ray = new Ray(origin, Vector3.down);
            return FindRayHit(ray, out anchor, out normal);
        }

        public abstract bool FindRayHit(in Ray ray, out Vector3 anchor, out Vector3 normal);
        
        void UpdateAnchors()
        {
            Vector3? projection = TFListener.MapFrame.UpdateAnchor(forceAnchorRebuild);
            if (PinRootMarker && projection.HasValue)
            {
                TFListener.RootFrame.transform.position = projection.Value;
                ((ARModuleData)ModuleData).CopyControlMarkerPoseToPanel();
            }
            forceAnchorRebuild = false;
        }
        
        public void Update()
        {
            UpdateAnchors();
        }
        
        public void Stop()
        {
            savedSessionInfo = new ARSessionInfo()
            {
                WorldAngle = WorldAngle,
                WorldOffset = WorldOffset,
                WorldScale = WorldScale
            };
            
            Visible = false;

            WorldScale = 1;
            TfRoot.SetPose(Pose.identity);

            Instance = null;
        }

        public virtual void Reset()
        {
        }
    }
}

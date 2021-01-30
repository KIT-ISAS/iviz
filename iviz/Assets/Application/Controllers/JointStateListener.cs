using UnityEngine;
using System.Collections.Generic;
using System;
using Iviz.Msgs.SensorMsgs;
using System.Runtime.Serialization;
using Iviz.Roslib;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.XmlRpc;
using JetBrains.Annotations;

namespace Iviz.Controllers
{
    public interface IJointProvider
    {
        string Name { get; }
        event Action Stopped;
        bool TryWriteJoint(string joint, float value);
        bool AttachedToTf { get; }
    }

    [DataContract]
    public sealed class JointStateConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.JointState;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string RobotName { get; set; } = "";
        [DataMember] public string MsgJointPrefix { get; set; } = "";
        [DataMember] public string MsgJointSuffix { get; set; } = "";
        [DataMember] public int MsgTrimFromEnd { get; set; } = 0;
    }

    public sealed class JointStateListener : ListenerController
    {
        public override IModuleData ModuleData { get; }

        public override TfFrame Frame => TfListener.MapFrame;

        readonly JointStateConfiguration config = new JointStateConfiguration();

        public JointStateConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.RobotName = value.RobotName;
                MsgJointPrefix = value.MsgJointPrefix;
                MsgJointSuffix = value.MsgJointSuffix;
                MsgTrimFromEnd = value.MsgTrimFromEnd;
            }
        }

        public string RobotName
        {
            get => config.RobotName;
            set => config.RobotName = value;
        }

        public bool Visible
        {
            get => config.Visible;
            set => config.Visible = value;
        }

        IJointProvider robot;

        public IJointProvider Robot
        {
            get => robot;
            set
            {
                if (robot != null)
                {
                    robot.Stopped -= OnRobotStopped;
                }

                robot = value;
                warnNotFound.Clear();
                if (robot != null)
                {
                    robot.Stopped += OnRobotStopped;
                }
            }
        }

        public string MsgJointPrefix
        {
            get => config.MsgJointPrefix;
            set => config.MsgJointPrefix = value;
        }

        public string MsgJointSuffix
        {
            get => config.MsgJointSuffix;
            set => config.MsgJointSuffix = value;
        }

        public int MsgTrimFromEnd
        {
            get => config.MsgTrimFromEnd;
            set => config.MsgTrimFromEnd = value;
        }

        readonly HashSet<string> warnNotFound = new HashSet<string>();

        public JointStateListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
        }

        void OnRobotStopped()
        {
            Robot = null;
        }

        public override void StartListening()
        {
            Listener = new Listener<JointState>(config.Topic, Handler);
        }

        public override void StopController()
        {
            base.StopController();
            Robot = null;
            warnNotFound.Clear();
        }

        void Handler(JointState msg)
        {
            if (Robot is null || Robot.AttachedToTf)
            {
                return;
            }

            if (msg.Name.Length != msg.Position.Length)
            {
                Debug.Log($"{this}: Names and positions have different lengths!");
                return;
            }

            foreach (var (name, position) in msg.Name.Zip(msg.Position))
            {
                if (double.IsNaN(position))
                {
                    Debug.Log($"JointStateListener: Joint for '{name}' is NaN!");
                    continue;
                }

                string msgJoint = name;
                if (MsgTrimFromEnd != 0 && msgJoint.Length >= MsgTrimFromEnd)
                {
                    msgJoint = msgJoint.Substring(0, msgJoint.Length - MsgTrimFromEnd);
                }

                msgJoint = $"{MsgJointPrefix}{msgJoint}{MsgJointSuffix}";
                if (Robot.TryWriteJoint(msgJoint, (float) position))
                {
                    continue;
                }

                if (!warnNotFound.Contains(msgJoint))
                {
                    Debug.Log("JointStateListener for '" + config.Topic + "': Cannot find joint '" + msgJoint +
                              "' (original: '" + name + "')");
                    warnNotFound.Add(msgJoint);
                }
            }
        }
    }
}
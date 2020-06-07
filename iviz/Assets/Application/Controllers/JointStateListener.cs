using UnityEngine;
using System.Collections.Generic;
using System;
using Iviz.Msgs.SensorMsgs;
using System.Runtime.Serialization;
using Iviz.RoslibSharp;
using Iviz.Resources;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class JointStateConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.JointState;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string RobotName { get; set; } = "";
        [DataMember] public string MsgJointPrefix { get; set; } = "";
        [DataMember] public string MsgJointSuffix { get; set; } = "";
        [DataMember] public int MsgTrimFromEnd { get; set; } = 0;
    }

    public class JointStateListener : TopicListener
    {
        public override DisplayData DisplayData { get; set; }

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
            set
            {
                config.RobotName = value;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
            }
        }

        Robot robot;
        public Robot Robot
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
            set
            {
                config.MsgJointPrefix = value;
            }
        }

        public string MsgJointSuffix
        {
            get => config.MsgJointSuffix;
            set
            {
                config.MsgJointSuffix = value;
            }
        }

        public int MsgTrimFromEnd
        {
            get => config.MsgTrimFromEnd;
            set
            {
                config.MsgTrimFromEnd = value;
            }
        }

        readonly HashSet<string> warnNotFound = new HashSet<string>();


        void OnRobotStopped()
        {
            Robot = null;
        }

        public override void StartListening()
        {
            base.StartListening();
            Listener = new RosListener<JointState>(config.Topic, Handler);
        }

        public override void Stop()
        {
            base.Stop();
            Robot = null;
            warnNotFound.Clear();
        }

        void Handler(JointState msg)
        {
            if (Robot == null || Robot.AttachToTF)
            {
                return;
            }

            for (int i = 0; i < msg.Name.Length; i++)
            {
                if (double.IsNaN(msg.Position[i]))
                {
                    Debug.Log($"JointStateListener: Joint {i} is NaN!");
                    continue;
                }
                string msgJoint = msg.Name[i];
                if (MsgTrimFromEnd != 0 && msgJoint.Length >= MsgTrimFromEnd) 
                {
                    msgJoint = msgJoint.Substring(0, msgJoint.Length - MsgTrimFromEnd);
                }
                msgJoint = $"{MsgJointPrefix}{msgJoint}{MsgJointSuffix}";
                if (Robot.JointWriters.TryGetValue(msgJoint, out JointInfo writer))
                {
                    writer.Write((float)msg.Position[i]);
                }
                else
                {
                    if (!warnNotFound.Contains(msgJoint))
                    {
                        Debug.Log("JointStateListener for " + name + ": Cannot find joint '" + msgJoint + "' (original: '" + msg.Name[i] + "')");
                        warnNotFound.Add(msgJoint);
                    }
                }
            }
        }
    }

}
using UnityEngine;
using System.Collections.Generic;
using System;
using Iviz.Msgs.sensor_msgs;

namespace Iviz.App
{
    public class JointStateListener : DisplayableListener
    {
        [Serializable]
        public class Configuration
        {
            public Resource.Module module => Resource.Module.JointState;
            public string topic = "";
            public string robotName = "";
            public string msgJointPrefix = "";
            public string msgJointSuffix = "";
            public int msgTrimFromEnd = 0;
        }

        readonly Configuration config = new Configuration();
        public Configuration Config
        {
            get => config;
            set
            {
                config.topic = value.topic;
                config.robotName = value.robotName;
                MsgJointPrefix = value.msgJointPrefix;
                MsgJointSuffix = value.msgJointSuffix;
                MsgTrimFromEnd = value.msgTrimFromEnd;
            }
        }

        public string RobotName
        {
            get => config.robotName;
            set
            {
                config.robotName = value;
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
            get => config.msgJointPrefix;
            set
            {
                config.msgJointPrefix = value;
            }
        }

        public string MsgJointSuffix
        {
            get => config.msgJointSuffix;
            set
            {
                config.msgJointSuffix = value;
            }
        }

        public int MsgTrimFromEnd
        {
            get => config.msgTrimFromEnd;
            set
            {
                config.msgTrimFromEnd = value;
            }
        }

        readonly HashSet<string> warnNotFound = new HashSet<string>();


        void OnRobotStopped()
        {
            Robot = null;
        }

        public override void StartListening()
        {
            Topic = config.topic;
            Listener = new RosListener<JointState>(config.topic, Handler);
            GameThread.EverySecond += UpdateStats;
        }

        public override void Unsubscribe()
        {
            GameThread.EverySecond -= UpdateStats;
            Listener?.Stop();
            Listener = null;
            Robot = null;
            warnNotFound.Clear();
        }

        void Handler(JointState msg)
        {
            if (Robot == null || Robot.AttachToTF)
            {
                return;
            }

            for (int i = 0; i < msg.name.Length; i++)
            {
                string msgJoint = msg.name[i];
                if (MsgTrimFromEnd != 0 && msgJoint.Length >= MsgTrimFromEnd) 
                {
                    msgJoint = msgJoint.Substring(0, msgJoint.Length - MsgTrimFromEnd);
                }
                msgJoint = $"{MsgJointPrefix}{msgJoint}{MsgJointSuffix}";
                if (Robot.JointWriters.TryGetValue(msgJoint, out JointInfo writer))
                {
                    writer.Write((float)msg.position[i]);
                }
                else
                {
                    if (!warnNotFound.Contains(msgJoint))
                    {
                        Debug.Log("JointStateListener for " + name + ": Cannot find joint '" + msgJoint + "' (original: '" + msg.name[i] + "')");
                        warnNotFound.Add(msgJoint);
                    }
                }
            }
        }
    }

}
﻿using System;
using System.Collections.Generic;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    public interface IJointProvider
    {
        string Name { get; }
        event Action Stopped;
        bool TryWriteJoint(string joint, float value);
        bool AttachedToTf { get; }
    }

    public sealed class JointStateListener : ListenerController
    {
        [NotNull] public override TfFrame Frame => TfModule.DefaultFrame;

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

        public override bool Visible
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

        [CanBeNull] Listener listener;
        [CanBeNull] public override Listener Listener => listener;
        
        void OnRobotStopped()
        {
            Robot = null;
        }

        public void StartListening()
        {
            listener = new Listener<JointState>(config.Topic, Handle);
        }

        public override void Dispose()
        {
            base.Dispose();
            Robot = null;
            warnNotFound.Clear();
        }

        void Handle(JointState msg)
        {
            if (Robot is null || Robot.AttachedToTf)
            {
                return;
            }

            if (msg.Name.Length != msg.Position.Length)
            {
                RosLogger.Debug($"{this}: Names and positions have different lengths!");
                return;
            }

            foreach (var (name, position) in msg.Name.Zip(msg.Position))
            {
                if (double.IsNaN(position))
                {
                    RosLogger.Debug($"{this}: Joint for '{name}' is NaN!");
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
                    RosLogger.Debug($"{this}: Cannot find joint '{msgJoint}' (original: '{name}')");
                    warnNotFound.Add(msgJoint);
                }
            }
        }
    }
}
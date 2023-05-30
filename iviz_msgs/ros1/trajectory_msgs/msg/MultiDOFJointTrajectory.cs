/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [DataContract]
    public sealed class MultiDOFJointTrajectory : IHasSerializer<MultiDOFJointTrajectory>, IMessage
    {
        // The header is used to specify the coordinate frame and the reference time for the trajectory durations
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A representation of a multi-dof joint trajectory (each point is a transformation)
        // Each point along the trajectory will include an array of positions/velocities/accelerations
        // that has the same length as the array of joint names, and has the same order of joints as 
        // the joint names array.
        [DataMember (Name = "joint_names")] public string[] JointNames;
        [DataMember (Name = "points")] public MultiDOFJointTrajectoryPoint[] Points;
    
        public MultiDOFJointTrajectory()
        {
            JointNames = EmptyArray<string>.Value;
            Points = EmptyArray<MultiDOFJointTrajectoryPoint>.Value;
        }
        
        public MultiDOFJointTrajectory(in StdMsgs.Header Header, string[] JointNames, MultiDOFJointTrajectoryPoint[] Points)
        {
            this.Header = Header;
            this.JointNames = JointNames;
            this.Points = Points;
        }
        
        public MultiDOFJointTrajectory(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            JointNames = b.DeserializeStringArray();
            {
                int n = b.DeserializeArrayLength();
                MultiDOFJointTrajectoryPoint[] array;
                if (n == 0) array = EmptyArray<MultiDOFJointTrajectoryPoint>.Value;
                else
                {
                    array = new MultiDOFJointTrajectoryPoint[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new MultiDOFJointTrajectoryPoint(ref b);
                    }
                }
                Points = array;
            }
        }
        
        public MultiDOFJointTrajectory(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            JointNames = b.DeserializeStringArray();
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                MultiDOFJointTrajectoryPoint[] array;
                if (n == 0) array = EmptyArray<MultiDOFJointTrajectoryPoint>.Value;
                else
                {
                    array = new MultiDOFJointTrajectoryPoint[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new MultiDOFJointTrajectoryPoint(ref b);
                    }
                }
                Points = array;
            }
        }
        
        public MultiDOFJointTrajectory RosDeserialize(ref ReadBuffer b) => new MultiDOFJointTrajectory(ref b);
        
        public MultiDOFJointTrajectory RosDeserialize(ref ReadBuffer2 b) => new MultiDOFJointTrajectory(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(JointNames.Length);
            b.SerializeArray(JointNames);
            b.Serialize(Points.Length);
            foreach (var t in Points)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(JointNames.Length);
            b.SerializeArray(JointNames);
            b.Align4();
            b.Serialize(Points.Length);
            foreach (var t in Points)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(JointNames, nameof(JointNames));
            BuiltIns.ThrowIfNull(Points, nameof(Points));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetArraySize(JointNames);
                foreach (var msg in Points) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, JointNames);
            size = WriteBuffer2.Align4(size);
            size += 4; // Points.Length
            foreach (var msg in Points) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "trajectory_msgs/MultiDOFJointTrajectory";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ef145a45a5f47b77b7f5cdde4b16c942";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WS4/bNhC+81cM4EO8hdeLJkUOC/RQIE27BYKmjdFLEBi0NJKYSKRCUuuovz7fUC/b" +
                "mz6ANmv4IJHznm++0Yp2FVPFOmdPJlAXOKfoKLScmaKniNvMOZ8bqyNT4XXDpG2eLjwX7NlmTNHguHA+" +
                "HUev33MWne8p77yOxtmgfh5cDJ6UWtEPUG89B7YxiZArSFPT1dFc53h+74yNp7bWrLOK2nSMSLXc2QCn" +
                "TdK/gs0fFwldO1tehnM0dU3GZnWXSxakvde9OG5dMCnOm3uuXYZnDjc6y7jmKYEVjOlIlQ7JapBC1GzL" +
                "WNF4NFsbQreQCJtUrDMlFBOFmMSCaCfjfKo3GNsqFaI3tnz7brjcp0v1Sqr04teXv8jZbs7vtbxCNFUg" +
                "KPX9//xTr978dEsh5vsmlOFmaClifxORpPY5NRx1rqNOUKhMWbG/rhklhZJuWkAr3ca+5bCF4q5CI/Ev" +
                "2aLOdd3P+Mtc03TWZAI6AdeZPjQNmket9tFkXa39A4yKdfwDf+wSQO9e3ELGBs66aBBQLzDwrAOKi0tS" +
                "HUr27Kko0Ire/u7Ct+/Uand01zjnEg2boxhwgKj5k+BXAtbhFs6+GbLcwgmqxHCXB1qnsz1ewxXBG2Lh" +
                "1gGma6Twuo8VkC+9v9fe6EPNYjhDKWD1iSg9uTqxbJNpq62bzA8WFx//xqyd7UpO1xWaV0sZQlfqRAKt" +
                "d/cmh+hhJIDaYEqpNgevfa/SsCeXavUyEUKUPqbWyGCGgAlCJ3LMW6xGBA9t2Zv8a8FyGfMBnX83IxNV" +
                "XLJNBk6YmO+EX2jdtQLK5wR7V6pkB6BPfnaTFAZv1ggqoRvsmGweQA40Mks/ejCoz8SYzpvSJAZcaODS" +
                "zdEEme2Fnx66wEycUNZ/83NOfmoi8gSZfeFdswcCfPxa3fyLGk+cMa+OMFH82KkDxyMzwjy6B5wQhDUK" +
                "zwBvqzNQhPojYeLZoF+nBNVvHRS8lVy9G1bT4yQ5BvOFFAU7cncRvxDcXWIiZ0FoDWtZmG7RhGJuPFSR" +
                "w3bACorEGzKRcod6WCej0OgPMMmgBdHWbVvP6K/HpjtRWfO23G7oWKG+SUrGOrFx4m+TkcArv9jLySaN" +
                "yW0oFk+HNZxiHpyhhTAyVftqS3cF9a6joySEBz+uDScon+JKrBad28jOGE2cFzSNOsoSgi5BgDZELCx0" +
                "vaidjs+/o0/zUz8//fkorV4w9qVuW5nT+bPorOfy9nEBqBT5HxOano6PNKtCIFNa04oMC/ud53Pw7gNL" +
                "kgliATvGMpaQfDRpW6bVLlseXwvTrI4iy/sop9RnJhydHMwKAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MultiDOFJointTrajectory> CreateSerializer() => new Serializer();
        public Deserializer<MultiDOFJointTrajectory> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MultiDOFJointTrajectory>
        {
            public override void RosSerialize(MultiDOFJointTrajectory msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MultiDOFJointTrajectory msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MultiDOFJointTrajectory msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MultiDOFJointTrajectory msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MultiDOFJointTrajectory msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MultiDOFJointTrajectory>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MultiDOFJointTrajectory msg) => msg = new MultiDOFJointTrajectory(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MultiDOFJointTrajectory msg) => msg = new MultiDOFJointTrajectory(ref b);
        }
    }
}

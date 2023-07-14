/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [DataContract]
    public sealed class MultiDOFJointTrajectoryPoint : IHasSerializer<MultiDOFJointTrajectoryPoint>, IMessage
    {
        // Each multi-dof joint can specify a transform (up to 6 DOF)
        [DataMember (Name = "transforms")] public GeometryMsgs.Transform[] Transforms;
        // There can be a velocity specified for the origin of the joint 
        [DataMember (Name = "velocities")] public GeometryMsgs.Twist[] Velocities;
        // There can be an acceleration specified for the origin of the joint 
        [DataMember (Name = "accelerations")] public GeometryMsgs.Twist[] Accelerations;
        [DataMember (Name = "time_from_start")] public duration TimeFromStart;
    
        public MultiDOFJointTrajectoryPoint()
        {
            Transforms = EmptyArray<GeometryMsgs.Transform>.Value;
            Velocities = EmptyArray<GeometryMsgs.Twist>.Value;
            Accelerations = EmptyArray<GeometryMsgs.Twist>.Value;
        }
        
        public MultiDOFJointTrajectoryPoint(GeometryMsgs.Transform[] Transforms, GeometryMsgs.Twist[] Velocities, GeometryMsgs.Twist[] Accelerations, duration TimeFromStart)
        {
            this.Transforms = Transforms;
            this.Velocities = Velocities;
            this.Accelerations = Accelerations;
            this.TimeFromStart = TimeFromStart;
        }
        
        public MultiDOFJointTrajectoryPoint(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Transform[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Transform>.Value;
                else
                {
                    array = new GeometryMsgs.Transform[n];
                    b.DeserializeStructArray(array);
                }
                Transforms = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Twist[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Twist>.Value;
                else
                {
                    array = new GeometryMsgs.Twist[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new GeometryMsgs.Twist(ref b);
                    }
                }
                Velocities = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Twist[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Twist>.Value;
                else
                {
                    array = new GeometryMsgs.Twist[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new GeometryMsgs.Twist(ref b);
                    }
                }
                Accelerations = array;
            }
            b.Deserialize(out TimeFromStart);
        }
        
        public MultiDOFJointTrajectoryPoint(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Transform[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Transform>.Value;
                else
                {
                    array = new GeometryMsgs.Transform[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Transforms = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Twist[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Twist>.Value;
                else
                {
                    array = new GeometryMsgs.Twist[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new GeometryMsgs.Twist(ref b);
                    }
                }
                Velocities = array;
            }
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Twist[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Twist>.Value;
                else
                {
                    array = new GeometryMsgs.Twist[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new GeometryMsgs.Twist(ref b);
                    }
                }
                Accelerations = array;
            }
            b.Align4();
            b.Deserialize(out TimeFromStart);
        }
        
        public MultiDOFJointTrajectoryPoint RosDeserialize(ref ReadBuffer b) => new MultiDOFJointTrajectoryPoint(ref b);
        
        public MultiDOFJointTrajectoryPoint RosDeserialize(ref ReadBuffer2 b) => new MultiDOFJointTrajectoryPoint(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Transforms.Length);
            b.SerializeStructArray(Transforms);
            b.Serialize(Velocities.Length);
            foreach (var t in Velocities)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Accelerations.Length);
            foreach (var t in Accelerations)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(TimeFromStart);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Transforms.Length);
            b.Align8();
            b.SerializeStructArray(Transforms);
            b.Serialize(Velocities.Length);
            foreach (var t in Velocities)
            {
                t.RosSerialize(ref b);
            }
            b.Align4();
            b.Serialize(Accelerations.Length);
            foreach (var t in Accelerations)
            {
                t.RosSerialize(ref b);
            }
            b.Align4();
            b.Serialize(TimeFromStart);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Transforms, nameof(Transforms));
            BuiltIns.ThrowIfNull(Velocities, nameof(Velocities));
            BuiltIns.ThrowIfNull(Accelerations, nameof(Accelerations));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 20;
                size += 56 * Transforms.Length;
                size += 48 * Velocities.Length;
                size += 48 * Accelerations.Length;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Transforms.Length
            size = WriteBuffer2.Align8(size);
            size += 56 * Transforms.Length;
            size += 4; // Velocities.Length
            size = WriteBuffer2.Align8(size);
            size += 48 * Velocities.Length;
            size += 4; // Accelerations.Length
            size = WriteBuffer2.Align8(size);
            size += 48 * Accelerations.Length;
            size += 8; // TimeFromStart
            return size;
        }
    
        public const string MessageType = "trajectory_msgs/MultiDOFJointTrajectoryPoint";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3ebe08d1abd5b65862d50e09430db776";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UTY/TQAy9z6+wtJddqQQJ0B6QuPGhPSBArLggVE0nTjrsZBw8Dtnw6/EkabJdFXEA" +
                "tVIlT2s/+73nmQt4Y90emi6If1JSBd/JRwFnI6QWna8GsCBsY6qIG7jsWhCCa3j94e2VqZEaFB62TarT" +
                "09tD1tdva0Uy5gJu98g4Yu5Q4X5iIOdlmDt4LEFTQfYIxL72EXSOfJpmedym90m0xYzi8USLCNY5DMhW" +
                "PMV/6/MQSVuV3QwqvsFtxdRsk1gWY1795495//ndS/iDxiNln4CxZUwYJY1MVqd2KD2ijtkTOCIufbSC" +
                "ULFtMIFyrxhRlbEOC2O+oBPi51N9GAmaT50WcMxcmWT67Swk52FOUMy7k/97ND9o6o2A5lIMAzRo1U5d" +
                "06VSC0vPWqocimlXVCTcgBcoSfWIJIrR2DuFxJgwV9u2Dcv2h9l0yiWXWNTFBvq96jtm+VhroiLUGJG9" +
                "g7xe5erGUmxhJrcBqZ5B70OYZp6aqYUKclD7qoCbCgbqoM+ENGAordgMtMNlLrsLeV7aQJcHHyGOBf04" +
                "7rf6nmyNql0StKW6XgWycv0C7pdoWKJfZ7F63bFTbsd8TzWc5DvyPJ9+rAuaRf4roUPUn+mu5gfkQAvv" +
                "M62kq7a8fsd8dkx3mEmOK5Yg+IiWVYNSv3UXNG71mUnFclfnlPU85xnzG339a7jVBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MultiDOFJointTrajectoryPoint> CreateSerializer() => new Serializer();
        public Deserializer<MultiDOFJointTrajectoryPoint> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MultiDOFJointTrajectoryPoint>
        {
            public override void RosSerialize(MultiDOFJointTrajectoryPoint msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MultiDOFJointTrajectoryPoint msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MultiDOFJointTrajectoryPoint msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MultiDOFJointTrajectoryPoint msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MultiDOFJointTrajectoryPoint msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MultiDOFJointTrajectoryPoint>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MultiDOFJointTrajectoryPoint msg) => msg = new MultiDOFJointTrajectoryPoint(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MultiDOFJointTrajectoryPoint msg) => msg = new MultiDOFJointTrajectoryPoint(ref b);
        }
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [DataContract]
    public sealed class MultiDOFJointTrajectoryPoint : IDeserializable<MultiDOFJointTrajectoryPoint>, IMessage
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
            Transforms = System.Array.Empty<GeometryMsgs.Transform>();
            Velocities = System.Array.Empty<GeometryMsgs.Twist>();
            Accelerations = System.Array.Empty<GeometryMsgs.Twist>();
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
            unsafe
            {
                int n = b.DeserializeArrayLength();
                Transforms = n == 0
                    ? System.Array.Empty<GeometryMsgs.Transform>()
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Transforms[0]), n * 56);
                }
            }
            {
                int n = b.DeserializeArrayLength();
                Velocities = n == 0
                    ? System.Array.Empty<GeometryMsgs.Twist>()
                    : new GeometryMsgs.Twist[n];
                for (int i = 0; i < n; i++)
                {
                    Velocities[i] = new GeometryMsgs.Twist(ref b);
                }
            }
            {
                int n = b.DeserializeArrayLength();
                Accelerations = n == 0
                    ? System.Array.Empty<GeometryMsgs.Twist>()
                    : new GeometryMsgs.Twist[n];
                for (int i = 0; i < n; i++)
                {
                    Accelerations[i] = new GeometryMsgs.Twist(ref b);
                }
            }
            b.Deserialize(out TimeFromStart);
        }
        
        public MultiDOFJointTrajectoryPoint(ref ReadBuffer2 b)
        {
            b.Align4();
            unsafe
            {
                int n = b.DeserializeArrayLength();
                Transforms = n == 0
                    ? System.Array.Empty<GeometryMsgs.Transform>()
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Transforms[0]), n * 56);
                }
            }
            {
                int n = b.DeserializeArrayLength();
                Velocities = n == 0
                    ? System.Array.Empty<GeometryMsgs.Twist>()
                    : new GeometryMsgs.Twist[n];
                for (int i = 0; i < n; i++)
                {
                    Velocities[i] = new GeometryMsgs.Twist(ref b);
                }
            }
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                Accelerations = n == 0
                    ? System.Array.Empty<GeometryMsgs.Twist>()
                    : new GeometryMsgs.Twist[n];
                for (int i = 0; i < n; i++)
                {
                    Accelerations[i] = new GeometryMsgs.Twist(ref b);
                }
            }
            b.Deserialize(out TimeFromStart);
        }
        
        public MultiDOFJointTrajectoryPoint RosDeserialize(ref ReadBuffer b) => new MultiDOFJointTrajectoryPoint(ref b);
        
        public MultiDOFJointTrajectoryPoint RosDeserialize(ref ReadBuffer2 b) => new MultiDOFJointTrajectoryPoint(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
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
        
        public void RosValidate()
        {
            if (Transforms is null) BuiltIns.ThrowNullReference();
            if (Velocities is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Velocities.Length; i++)
            {
                if (Velocities[i] is null) BuiltIns.ThrowNullReference(nameof(Velocities), i);
                Velocities[i].RosValidate();
            }
            if (Accelerations is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Accelerations.Length; i++)
            {
                if (Accelerations[i] is null) BuiltIns.ThrowNullReference(nameof(Accelerations), i);
                Accelerations[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += 56 * Transforms.Length;
                size += 48 * Velocities.Length;
                size += 48 * Accelerations.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Transforms length
            c = WriteBuffer2.Align8(c);
            c += 56 * Transforms.Length;
            c += 4; // Velocities length
            c = WriteBuffer2.Align8(c);
            c += 48 * Velocities.Length;
            c += 4; // Accelerations length
            c = WriteBuffer2.Align8(c);
            c += 48 * Accelerations.Length;
            c += 8; // TimeFromStart
            return c;
        }
    
        public const string MessageType = "trajectory_msgs/MultiDOFJointTrajectoryPoint";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3ebe08d1abd5b65862d50e09430db776";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
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
    }
}

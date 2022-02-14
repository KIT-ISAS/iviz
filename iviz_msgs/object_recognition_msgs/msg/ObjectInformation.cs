/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectInformation : IDeserializable<ObjectInformation>, IMessage
    {
        //############################################# VISUALIZATION INFO ######################################################
        //################## THIS INFO SHOULD BE OBTAINED INDEPENDENTLY FROM THE CORE, LIKE IN AN RVIZ PLUGIN ###################
        // The human readable name of the object
        [DataMember (Name = "name")] public string Name;
        // The full mesh of the object: this can be useful for display purposes, augmented reality ... but it can be big
        // Make sure the type is MESH
        [DataMember (Name = "ground_truth_mesh")] public ShapeMsgs.Mesh GroundTruthMesh;
        // Sometimes, you only have a cloud in the DB
        // Make sure the type is POINTS
        [DataMember (Name = "ground_truth_point_cloud")] public SensorMsgs.PointCloud2 GroundTruthPointCloud;
    
        /// Constructor for empty message.
        public ObjectInformation()
        {
            Name = "";
            GroundTruthMesh = new ShapeMsgs.Mesh();
            GroundTruthPointCloud = new SensorMsgs.PointCloud2();
        }
        
        /// Explicit constructor.
        public ObjectInformation(string Name, ShapeMsgs.Mesh GroundTruthMesh, SensorMsgs.PointCloud2 GroundTruthPointCloud)
        {
            this.Name = Name;
            this.GroundTruthMesh = GroundTruthMesh;
            this.GroundTruthPointCloud = GroundTruthPointCloud;
        }
        
        /// Constructor with buffer.
        public ObjectInformation(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            GroundTruthMesh = new ShapeMsgs.Mesh(ref b);
            GroundTruthPointCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ObjectInformation(ref b);
        
        public ObjectInformation RosDeserialize(ref ReadBuffer b) => new ObjectInformation(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            GroundTruthMesh.RosSerialize(ref b);
            GroundTruthPointCloud.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (GroundTruthMesh is null) throw new System.NullReferenceException(nameof(GroundTruthMesh));
            GroundTruthMesh.RosValidate();
            if (GroundTruthPointCloud is null) throw new System.NullReferenceException(nameof(GroundTruthPointCloud));
            GroundTruthPointCloud.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.GetStringSize(Name);
                size += GroundTruthMesh.RosMessageLength;
                size += GroundTruthPointCloud.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectInformation";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "921ec39f51c7b927902059cf3300ecde";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71XbU8bRxD+3PsVo/hDIAJXQJoiKlSR2ASrxkZgIjUostZ3a9+2d7uX3T3g8uv7zO6d" +
                "7aRUaqRSixd7b+eZ92fGvd73vOjD6Ob2bDz6eDYbTSc0mpxP6bsA1q/kKfTZxegmgt5cTG/HA3o7pOnb" +
                "2dloMhzgfDC8GuLPZDb+nc6vp5e4P6R30+vhHo1Hvw1xg84mdP1h9JGuxrfv8fEpxQn05JLyuhSarBSZ" +
                "WBSStCglmSV5PDKLP2TqE+et0qvwpBNa1kVBpXT511dP8F45SgG4kFQ7iXu0NJYy5apCNFTVtjJOuj0S" +
                "9aqU2suMVRfKN9Tv92lRe1K+A1ioFfRdij8ludrKoMg3lSTouBzeXCQuF5Wcl27lfrxkY1bW1Dqbe1v7" +
                "fM7msb03ppRelay0MTUZXTSUi3tJgtLC1BkpHZAHb/9R2dV0NJndJE5qZ2zUd2WU9u9Y/vBrtRU/mAfk" +
                "JDn9j1/J5c37E/rGbZg9kEullVdGc0IEdb4XyvmQIquEXhXS/RL8UjqTj3Qvilo6xH8pLXlDyEyAcByR" +
                "e2m9SqW7+5SwjlkLcPdpg8UKGE2kvhbFWgJnwlPJgayrcCFYs5KcB9tshQ9gndD/FKrOjSdC1rn10m2M" +
                "qmHk0eHdUbRTPs4RuOe09okYhYbjnjLaC6VdCGiXqWh5qDjO2dJKFG8lUpksCyP8m9f0uH7XrN99ebZg" +
                "P90fnQsoAydWYBxTZI67zxQFSKP1Y7KfoUm1w0cUU/AJHfuQqzRHMTUAaUNAIsuC97imNNilFAHD1bgp" +
                "HGk+KSALBMbzzR5Jn/aZuIASo5UJL7iznTcWHCTYnoXSwja0KMwCwt4RGMuAkDLpUqsWuLZoOPytKWAv" +
                "19Hfi6WScOoFCWtF0+9oMuqKLBM0whEmNmNXQqsvQDzMaEeViMp+of6Uu3hCBxmkd2q4kUnYttunqw2M" +
                "25KF0RAP0q5DrqzJ6jSYymaCsq2AA5XP2+y4dZycB7xhjcyO+2a5vyzUKvfRehXHQBSKxov0c61i4YG/" +
                "dRY8Tw3sROA8hoLl2TEa0A5T/lHWBdvt9pMLzBewTB7+sYLDAQywNajDrufNVrj6NIpnLUU7WgdkDyhs" +
                "Jw6BcxAseVAZPFSxOwqpVz5/CrRt6Bag+xSEk0AJMdERJs2F1rJwnavKdgXRDoy2XkJsuGj6SUjUOZcC" +
                "qC2WRJIsjCkIL+XmGGgSDILp1qORi8MyincPfu2MinbPkaQKsj0ar53aavhF49csRWTNQ3f/Wwk82tzv" +
                "scAxLAyqu1ePziKPbxpkjxwKjcO602G/iqHbTbYxfmgB7q7RE59aj+FuhuKRG/yZrQEWsoKcC/xqA6sw" +
                "hlTWVsqzMZPPIi3FOuStwCOtwmZgJS9CILhoczgn7X4h72UBIVFWsu1cXgVcv6My/CBh6K0CywT2nIzn" +
                "Z2rKstYq5V4IC8e2PCSZuqgSPFvqQti/tQ6j48fJz7XUKXfSCRONk2ntFQxqgJBiW3K8jaHNusRDIOnN" +
                "Hsw+E95K2o3yOIphrHysLNg3kMYJdLyKzvWBzbQNLWCWnXA2x0e3i8yzCbIyIIsdWH7V+NzEur8XGJW8" +
                "K4ZVDxye0UsWerm7hawDtBbadPARcaPj38DqNW7gKHRkVrD3rl4hgLgIvrtX2Zqa0ecKNYi9Z2HRmwlL" +
                "RZVJ7zzQ06bn8F84Z1IleAt9UOCAdtMN2Zir59vfvp2TgTKeHpPsVBxAVTcpje5YDa7apqUjiG/vpB1K" +
                "nI+R+Y7xzWB2zM14SgftyW17dEqHmzsHb8LJ0dYdPjql15s7zDg4+WnrDh+d0pv25Hw8PeOjU/p5+wQL" +
                "yCkdJ9vfKjqCmLTfPQJzdtVtlksnfbwwje+X1pScVRt22xiKOExaRSHBYXmH0KB7L3XN4zDOL4dVSSzM" +
                "vez0pNjhfWvIBegS34oakoUsw5xvKT9alvwFsZeyWS0OAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

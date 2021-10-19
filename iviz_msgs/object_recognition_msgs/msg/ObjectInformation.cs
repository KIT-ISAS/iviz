/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectInformation")]
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
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectInformation()
        {
            Name = string.Empty;
            GroundTruthMesh = new ShapeMsgs.Mesh();
            GroundTruthPointCloud = new SensorMsgs.PointCloud2();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectInformation(string Name, ShapeMsgs.Mesh GroundTruthMesh, SensorMsgs.PointCloud2 GroundTruthPointCloud)
        {
            this.Name = Name;
            this.GroundTruthMesh = GroundTruthMesh;
            this.GroundTruthPointCloud = GroundTruthPointCloud;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectInformation(ref Buffer b)
        {
            Name = b.DeserializeString();
            GroundTruthMesh = new ShapeMsgs.Mesh(ref b);
            GroundTruthPointCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectInformation(ref b);
        }
        
        ObjectInformation IDeserializable<ObjectInformation>.RosDeserialize(ref Buffer b)
        {
            return new ObjectInformation(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            GroundTruthMesh.RosSerialize(ref b);
            GroundTruthPointCloud.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(Name);
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
                "H4sIAAAAAAAAE71XXW/bNhR916+4aB6aFImHJF0XZAiGtHYaY44dNE6BtSgMWqItrjKpklQS9dfvXFK0" +
                "3S4DVmCZ4MQSxXvu97n0zs6PXPR+eHN7Php+OJ8OJ2Maji8m9EMA6yt7DH16ObyJoDeXk9tRn14PaPJ6" +
                "ej4cD/pY7w+uB/g3no7+oIt3kyvsH9CbybvBPo2Gvw+wg87H9O798ANdj27f4vExxRn0lJLKZiU0WSkK" +
                "Ma8kabGSZBbk8crM/5S5z5y3Si/DmyS0aKqKVtKV3249xb1ylANwLqlxEvtoYSwVytWVaKlubG2cdPsk" +
                "muVKai8LVl0p31Kv16N540n5BDBXS+i7Ep8lucbKoMi3tSTouBrcXGauFLWcrdzS/XTFxiytaXQx87bx" +
                "5YzNY3tvzEp6tWKlrWnI6KqlUtxJEpRXpilI6YDcf/2Pyq4nw/H0JnNSO2OjvmujtH/D8kffqq35xSwg" +
                "Z9nZf3xlVzdvT+k7t2F2Xy6UVl4ZzQkRlHyvlPMhRVYJvayk+zX4pXQhH+hOVI10iP9CWvKGkJkA4Tgi" +
                "d9J6lUv38VPGOqYdwMdPGyxWwGgi942o1hJYE55WHMimDhuCNUvJebDtVvgAloT+p1AlNx4JWXLrudsY" +
                "1cDI46OPx9FO+TBD4J7S2kdiFBqOe8poL5R2IaApU9HyUHGcs4WVKN5a5DJbVEb4Vy/pYX3Xru++Plmw" +
                "H++P5ALKwIklGMdUhePuM1UF0uj8GB8UaFLt8IhiCj6hY+9LlZcophYgXQhIFEXwHtuUBrusRMBwDXYK" +
                "R5pXKsgCgfF8u0/S5z0mLqDEaBXCC+5s540FBwm2Z660sC3NKzOHsHcExjIgpEK63Ko5ts1bDn9nCtjL" +
                "Jfp7tlASTj0jYa1oe4kmo67IMkEjHGFiM3YptPoKxKOCdtUKUTmo1Ge5hzd0WEB6t4EbhYRtez263sC4" +
                "LVkYDfEg7RJybU3R5MFUNhOUbQUcqH3ZZcet4+Q84A1rZHY8MIuDRaWWpY/WqzgGolA0XuRfGhULD/yt" +
                "i+B5bmAnAucxFCzPjmGfdpnyj4sUbLfXyy4xX8AyZfhiBUd9GGAbUIddz5utcPVoGNc6ina0Dsg+UNhO" +
                "LALnMFhyrwp4qGJ3VFIvffkYaNfQHUB6CsJZoISY6AiTl0JrWbnkqrKpILqB0dVLiA0XTS8LibrgUgC1" +
                "xZLIsrkxFeFSboaBJsEgmG47NHRxWEbx9OK3ZFS0e4Yk1ZDdodHaqa2Gn7d+zVJE1tyn/d9L4NW3+09g" +
                "YNCcrh06jzS+6Y99cqgzjupugn4RI7fXeQWXChSI3IBMbQOJEHnkVeBPG2jGqFFFVw1Pxj6+iNQTa40n" +
                "v0fqhC3APF4Eb7kwS3gg7UEl72QFIbGqZdedPO5dL9EVPkgK+qfCgQFnmYJnZG5Wq0arnOs9HCq25SHJ" +
                "9ES14PnRVML+rT0YHR8nvzRS59wtp0wmTuaNVzCoBUKOE5HjExdaKSUXAtnO9N4cMKktpd0oj+MWxsqH" +
                "2oJhAzGcQseL6FwP2EzN0AL22A1rMzy6PaSXTZC1ASHswvLr1pcm1vadwDjk82A4zoGnC3rOQs/3tpB1" +
                "gNZCmwQfETc6/g2sXuMGHkLXFRV775olAoiN4LQ7VazpF72swL0428wt+i9jqagy27kIFLTpK3wL50yu" +
                "BJ807xX6vDvNhmzM1NOd0b6fhYEWHh+F7FQcMnWahkYn5oKrtu0oB+Lb586EEmdgZLcTnP6nJ9yMZ3TY" +
                "rdx2S2d0tNlz+CqsHG/t4aUzernZw6yClZ+39vDSGb3qVi5Gk3NeOqNftldwyDijk2z7l0MiiHH3+yKw" +
                "Y6pus1g46eOGSbxfWLPirNpwfo2hiAOjUxQSHA7oEOqne6kbHnlxRjkch8Tc3MmkJ8c53XeGXIIS8cun" +
                "JVnJVZjlHa1Hy7K/AHLgCOURDgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

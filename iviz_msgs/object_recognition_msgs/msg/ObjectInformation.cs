/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [DataContract]
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
            b.DeserializeString(out Name);
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
            if (Name is null) BuiltIns.ThrowNullReference();
            if (GroundTruthMesh is null) BuiltIns.ThrowNullReference();
            GroundTruthMesh.RosValidate();
            if (GroundTruthPointCloud is null) BuiltIns.ThrowNullReference();
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "object_recognition_msgs/ObjectInformation";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "921ec39f51c7b927902059cf3300ecde";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XXW/bNhR9nn7FRfPQpEg8JOm6IIMxpLXTGEvsIHYKrEFh0BJtcZNIlaSSqL9+55KS" +
                "7XYZsALLBCeWKd5zv8+ldna+56IPo+nt2eXo49lsNBnTaHw+oe8CWF/JU+izi9E0gk4vJreXA3o7pMnb" +
                "2dloPBxgfTC8HuLfeHb5O53fTK6wf0jvJjfDfboc/TbEDjob082H0Ue6vrx9j59PKU6gJ5eU16XQZKXI" +
                "xKKQpEUpySzJ45FZ/CFTnzhvlV6FJ53Qsi4KKqXLv956invlKAXgQlLtJPbR0ljKlKsK0VBV28o46fZJ" +
                "1KtSai8zVl0o31Cv16NF7Un5DmChVtB3Jf6U5GorgyLfVJKg42o4vUhcLio5L93K/XjFxqysqXU297b2" +
                "+ZzNY3unppRelay0MTUZXTSUi3tJgtLC1BkpHZAHb/9R2fVkNJ5NEye1MzbquzZK+3csf/S12oofzANy" +
                "kvT/4yu5mr4/pW/chtkDuVRaeWU0J0RQ53uhnA8pskroVSHdL8EvpTP5SPeiqKVD/JfSkjeEzAQIxxG5" +
                "l9arVLq7TwnrmLUAd582WKyA0UTqa1GsJbAmPJUcyLoKG4I1K8l5sM1W+ADWCf1PoerceCJknVsv3cao" +
                "GkYeH90dRzvl4xyBe05rn4hRaDjuKaO9UNqFgHaZipaHiuOcLa1E8VYilcmyMMK/eU2P67tmfffl2YL9" +
                "dH90LqAMnFiBcUyROe4+UxQgjdaP8UGGJtUOP1FMwSd07EOu0hzF1ACkDQGJLAveY5vSYJdSBAxXY6dw" +
                "pHmlgCwQGM83+yR92mPiAkqMVia84M523lhwkGB7FkoL29CiMAsIe0dgLANCyqRLrVpg26Lh8LemgL1c" +
                "R38vlkrCqRckrBVNr6PJqCuyTNAIR5jYjF0Jrb4A8SijXVUiKgeF+lPu4QkdZpDereFGJmHbXo+uNzBu" +
                "SxZGQzxIuw65siar02AqmwnKtgIOVD5vs+PWcXIe8IY1MjsemOXBslCr3EfrVRwDUSgaL9LPtYqFB/7W" +
                "WfA8NbATgfMYCpZnx2hAu0z5x1kXbLfXSy4wX8AyefhiBUcDGGBrUIddz5utcPVoFNdaina0Dsg+UNhO" +
                "LALnMFjyoDJ4qGJ3FFKvfP4UaNvQLUD3KwgngRJioiNMmgutZeE6V5XtCqIdGG29hNhw0fSSkKhzLgVQ" +
                "WyyJJFkYUxAu5eYYaBIMgum2QyMXh2UU7x782hkV7Z4jSRVkd+hy7dRWwy8av2YpImseuv3fSuDR1/tP" +
                "YCBr/qHdfHeDcv5EZ5HKNz2yTw61xpHd7eBfxejttZ7BrQxFIqm9UDu2hkSIPnIr8KcNtGPcqKytiGdj" +
                "IJ9F+on1xtPfI33CZmAfL0KsuThzeCDtQSHvZQEhUVay7VAe+a7XURY+SAx6qMChAeeZjOdkasqy1irl" +
                "mg8Hi215SDJFUSV4htSFsH9rEUbHx8nPtdQpd8wpE4qTae0VDGqAkOJU5PjUhXbqEgyBZGf2YA6Y2FbS" +
                "bpTHkQtj5WNlwbKBHE6h41V0rgdspmdoAYPshrU5fro9pJdNkJUBKezC8uvG5ybW973ASOQzYTjSgasz" +
                "eslCL/e2kHWA1kKbDj4ibnT8G1i9xg1chM7LCvbe1SsEEBvBa/cqW1Mw+lmhYHG+WVj0YMJSUWWycx5o" +
                "aNNb+BbOmVQJPm0+KPR6e6IN2Zir5zunfTsPAzU8PQ7ZqThoqm4iGt2xF1y1TUs7EN8+e3YocQ5GhjvB" +
                "G8DshJuxT4ftym271KejzZ7DN2HleGsPL/Xp9WYPMwtWftraw0t9etOunF9OznipTz9vr+Cg0aeTZPvt" +
                "oSOIcfuOERiyq26zXDrp44ZJvF9aU3JWbTjDxlDEodEqCgkOh3QIDbp7qWsee3FOORyJxMLcy05PirO6" +
                "bw25AC3i7achWcgyzPOW2qNlyV+ZSSlSFQ4AAA==";
                
    
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}

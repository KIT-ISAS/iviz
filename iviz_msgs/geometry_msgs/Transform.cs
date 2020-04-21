
namespace Iviz.Msgs.geometry_msgs
{
    public struct Transform : IMessage 
    {
        // This represents the transform between two coordinate frames in free space.
        
        public Vector3 translation;
        public Quaternion rotation;

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Transform";

        public IMessage Create() => new Transform();

        public int GetLength() => 56;

        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeStruct(out this, ref ptr, end);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeStruct(this, ref ptr, end);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ac9eff44abf714214112b05d54a3cf9b";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAAE71SwUrDQBC971cM9KIQIqh4EDxLD4KieJVpMkkXk504OzXGr3e2aROrghcxp5fNvJc3" +
            "7+0CHtY+glAnFCloBF0TqGCIFUsLK9KeKID2DAWzlD6gElSCLUXwwRARxA4Lyp17pEJZzkZ+g+o5uLuN" +
            "ESQYBGEdz9zVHz/u5v76EmrillSGpzbW8WRnxi2+rYjwuv32xT/Y6FLBZjk0A7SEQUF5Zhqx9GJU2yE3" +
            "VRKykCgDr1Cy5RFYTaPFZ5OkECmxsetMDD9nko6NckR5nWfQry3f7ZQPtQ2aQk2BxBcgvvbl3MZERtgt" +
            "l4FWp9D7phk9jz+zCk1kn/ZxDssKBt5AnxYyIFCiYhJa0eQLV03yyxlskvGtxGGgt+yNb71HrMmyi0pY" +
            "WutVw6gX5/A2oWFC7/9S9XzHfmo7AIs3OMZ30Hl6e5kvaAr514X2qHfuA/8+ZJE+AwAA";

    }
}

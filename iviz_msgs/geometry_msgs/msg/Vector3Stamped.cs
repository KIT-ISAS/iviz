/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Vector3Stamped")]
    public sealed class Vector3Stamped : IDeserializable<Vector3Stamped>, IMessage
    {
        // This represents a Vector3 with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "vector")] public Vector3 Vector { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Vector3Stamped()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Vector3Stamped(StdMsgs.Header Header, in Vector3 Vector)
        {
            this.Header = Header;
            this.Vector = Vector;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Vector3Stamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Vector = new Vector3(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Vector3Stamped(ref b);
        }
        
        Vector3Stamped IDeserializable<Vector3Stamped>.RosDeserialize(ref Buffer b)
        {
            return new Vector3Stamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Vector.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 24;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Vector3Stamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7b324c7325e683bf02a9b14b01090ec7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwWrbQBC9C/QPAzkkKY4KSenB0Ftpm0MhkNBrGEsjaam0q+6O7Khf3zer2GnopYfW" +
                "2Ei29r158+aNz+ihd4miTFGSeE3E9E1qDfGGDk57PGkliq+F6hBi4zyrUBt5FGLfkLpRkvI4lcUX4UYi" +
                "9flSFkeWfb6WRVl8+Mevsvh6/3lLSZvHMXXp7SqgLM7oXqGNY0OjKDesTG2AMtf1Eq8G2ctAWbQ0lJ/q" +
                "MkmqDJndwLsTL5GHYaE54ZQGtD+Os3e19X/q+khgUOfh3cRRXT0PHP/wK/PbJ8mPOTt6+3GLUz5JPauD" +
                "qAUcdRROznd4iMOz83pzbQgAHw7hCt+lg8snBaQ9qymWJxuhieW0tTJv1h4r0MMkQaEm0UX+7RFf0yWh" +
                "DlTIFOqeLiD/btE+eDAK7Tk63g1izDV8AO25gc4vf6c26Vvy7MORf6V8KfI3vP6F2Nq66jG8wSxIcwcf" +
                "cXKKYe8anN0tmaUeHLJKg9tFjktZGGwtCpJPOZxqg8yzwZVTCrXDJJoc6rJIGq1Ansuja/5jOjsJCGFc" +
                "1og+L8Upaa/2bt0Uy1EbBf1MXEuVE3ObJxw8EjIKo3Pk8QQFsnERWBd8BVqsK9IuG3JKTZBEPqiRjPwd" +
                "pAK3Dc7TBDZEP7JPAxvYfgbmQqqu2tChFwTaTplTa8TzVriaousclsKgKDWe0Kf/jg1pew2vh2FVvVbD" +
                "6IwlBs2Iy4puW1rCTAfrCTfxeR0D7SDyWVmOi4awsVU8cry29S5gL2BNStwhWj4p/gqwb2XRDoH1/Tt6" +
                "erlFXo63P8viF1CtIKEBBQAA";
                
    }
}

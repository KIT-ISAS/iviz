/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [Preserve, DataContract (Name = "grid_map_msgs/GridMapInfo")]
    public sealed class GridMapInfo : IDeserializable<GridMapInfo>, IMessage
    {
        // Header (time and frame)
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Resolution of the grid [m/cell].
        [DataMember (Name = "resolution")] public double Resolution;
        // Length in x-direction [m].
        [DataMember (Name = "length_x")] public double LengthX;
        // Length in y-direction [m].
        [DataMember (Name = "length_y")] public double LengthY;
        // Pose of the grid map center in the frame defined in `header` [m].
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
    
        /// <summary> Constructor for empty message. </summary>
        public GridMapInfo()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public GridMapInfo(in StdMsgs.Header Header, double Resolution, double LengthX, double LengthY, in GeometryMsgs.Pose Pose)
        {
            this.Header = Header;
            this.Resolution = Resolution;
            this.LengthX = LengthX;
            this.LengthY = LengthY;
            this.Pose = Pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GridMapInfo(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Resolution = b.Deserialize<double>();
            LengthX = b.Deserialize<double>();
            LengthY = b.Deserialize<double>();
            b.Deserialize(out Pose);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GridMapInfo(ref b);
        }
        
        GridMapInfo IDeserializable<GridMapInfo>.RosDeserialize(ref Buffer b)
        {
            return new GridMapInfo(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Resolution);
            b.Serialize(LengthX);
            b.Serialize(LengthY);
            b.Serialize(Pose);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 80 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "grid_map_msgs/GridMapInfo";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "43ee5430e1c253682111cb6bedac0ef9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWsbMRC9C/wfBnyIU2oH2tKDoYdC6Qe04Da5heLI0nhXoJU2ktb29tf3SRuvY3pI" +
                "D23M4tVKb97Mmw9N6TNLzYFmyTRM0mnaBtnwpXjYr8tLiCn94Ohtl4x35LeUaqYqGE23zZVia38uxNZ6" +
                "md6+oTACs9lXdlWqyTg6zLUJrArDbfPIwBbI+nAO75+A9xm+8pHPwmlkS4pdQujgyNtFD2neGsc6b94N" +
                "mu4G1op9wyn06yZW8arwtfibiHf/+DcR364/LSkmPbgaEjyBiOuEvMuA4DlJLZOkrUfmTVVzmFvesYWV" +
                "bFqEX05T33JcwPCmNpHwVOw4SGt76iJAyZPyTdM5o2RiypU9s4cl0iCplSEZ1VkZgPdBG5fhJV+ZHU/k" +
                "+46dYvryYQmMi6xQWATUg0EFltG4CockOuPS61fZQExv9n6OT65QhNE5aiFTDpYPLTokxynjEj5eDOIW" +
                "4EZ2GF50pFnZW+MzXhKcIARuvapphshXfar9UNydDEZuLGdihQyA9SIbXVw+Ys5hL8lJ54/0A+PJx9/Q" +
                "upE3a5rXqJnN6mNXIYEAtsHvjAZ00xcSZQ0akazZBBl6UQasuBTTj6UnUy5fqQjeMkavDAqgaW9SLWIK" +
                "mb1UY220+G8N+ecA5J58jzHOdYICeZz5PBe5c7aBoaSVil/mRsvb+uHcFGy+RnwwR9sFiZVHQ4wA8b2D" +
                "0OAK7wn3fBoRzOQ4P+iIJI2LpWajBMjBgJSozxSL4y10GFf9uPr1XApO+RtljOVCK51l9Tz+/HV/yj4u" +
                "mmYhnhB1XO0h7ze7rc8vMQYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

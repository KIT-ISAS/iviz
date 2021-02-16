/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/WorkspaceParameters")]
    public sealed class WorkspaceParameters : IDeserializable<WorkspaceParameters>, IMessage
    {
        // This message contains a set of parameters useful in
        // setting up the volume (a box) in which the robot is allowed to move.
        // This is useful only when planning for mobile parts of 
        // the robot as well.
        // Define the frame of reference for the box corners
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // The minumum corner of the box, with respect to the robot starting pose
        [DataMember (Name = "min_corner")] public GeometryMsgs.Vector3 MinCorner { get; set; }
        // The maximum corner of the box, with respect to the robot starting pose
        [DataMember (Name = "max_corner")] public GeometryMsgs.Vector3 MaxCorner { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public WorkspaceParameters()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public WorkspaceParameters(in StdMsgs.Header Header, in GeometryMsgs.Vector3 MinCorner, in GeometryMsgs.Vector3 MaxCorner)
        {
            this.Header = Header;
            this.MinCorner = MinCorner;
            this.MaxCorner = MaxCorner;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public WorkspaceParameters(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            MinCorner = new GeometryMsgs.Vector3(ref b);
            MaxCorner = new GeometryMsgs.Vector3(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new WorkspaceParameters(ref b);
        }
        
        WorkspaceParameters IDeserializable<WorkspaceParameters>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            MinCorner.RosSerialize(ref b);
            MaxCorner.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/WorkspaceParameters";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d639a834e7b1f927e9f1d6c30e920016";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VVsW7bQAzd9RUEMjQpHBdIig4BugVtMxQIkKBrQEuUdOjpqN6dbKtf38eTo6RAhw6t" +
                "F8s6vsdH8vF8Ro+9SzRIStwJ1Royu5CIKUkmbWnkyINkiYmmJO3kyYXqzE6zCx1NI+VeaK9+GoTOmXZ6" +
                "vEAIHXpX9+Us6k4zIQl7rwdpKCsNupctaEpyt1Jr8DOQEmj0HIIlaDUieue8mJScTBOAL8Sc6CDebyu8" +
                "vZXWBSmHrcm24CitRAm1FCo7gkQUGgNqqr4INxKpL19VUSQ0uDAN03AKMpITbEMHl3tQplHqbIW86EgZ" +
                "8kzxqEmqThRdi/PTkLr07huiNV4b8dNCuqbio/s/qfi4pvr4jz/V14fPN1DRLCmXHqKgh8yh4djATpkb" +
                "zlxa3ruul3jpZS/epA8jPFBO8zxKem2DTqAXNpnNEMUotQ7DFFzNGWN1sOlrPJBwGhdjuHryHBGvsXHB" +
                "wosDjL3Y9cdUPHB3e2MmT1JP2UHQDIY6Cidr590tVZML+frKANXZ40Ev8VM6TGZNjjlwsbMcR0zHdHK6" +
                "QY63S3FbcKM5gixNovPy7gk/0wUhCSTIqNiMcyi/n3OvYVkgjo53MDmIa3QArG8M9ObiFXMo1IGDPtMv" +
                "jC85/oY2rLxW02WPmXmrPk0dGojAMereNQjdzYWk9k5CJu92keNcGWpJWZ19KluWbXxlIrblKWntMICm" +
                "OLhKOZY9tsgn1/wvN/5xC56tFcVGhSLsYtuXM3NOGwWVjFzL1kxyV8Za7qBBOJSlW5EANi4C6jRsbXVx" +
                "r2iUDblMjUqioBkcA38HpaDHhuZxBBmMHjkkz4a114Ccy7bbbpa7rkRZj4qjyw64mqLrXLMgkWhYwUyn" +
                "4jaU2yv02J/uzSUZBgaSqLkALrZ019KsEx2sIDzE0+op7WTVVSySVTe2dyeK3xt6r1iE9W8C/xAZS487" +
                "t/XK+cN7Oq5P8/r0s/oF+N16rV4GAAA=";
                
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [Preserve, DataContract (Name = "diagnostic_msgs/DiagnosticArray")]
    public sealed class DiagnosticArray : IDeserializable<DiagnosticArray>, IMessage
    {
        // This message is used to send diagnostic information about the state of the robot
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } //for timestamp
        [DataMember (Name = "status")] public DiagnosticStatus[] Status { get; set; } // an array of components being reported on
    
        /// <summary> Constructor for empty message. </summary>
        public DiagnosticArray()
        {
            Header = new StdMsgs.Header();
            Status = System.Array.Empty<DiagnosticStatus>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public DiagnosticArray(StdMsgs.Header Header, DiagnosticStatus[] Status)
        {
            this.Header = Header;
            this.Status = Status;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public DiagnosticArray(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = b.DeserializeArray<DiagnosticStatus>();
            for (int i = 0; i < Status.Length; i++)
            {
                Status[i] = new DiagnosticStatus(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DiagnosticArray(ref b);
        }
        
        DiagnosticArray IDeserializable<DiagnosticArray>.RosDeserialize(ref Buffer b)
        {
            return new DiagnosticArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Status, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            for (int i = 0; i < Status.Length; i++)
            {
                if (Status[i] is null) throw new System.NullReferenceException($"{nameof(Status)}[{i}]");
                Status[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var i in Status)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "diagnostic_msgs/DiagnosticArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "60810da900de1dd6ddd437c3503511da";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UUU/bMBB+j5T/cFIfgEnABm+V+oAE2xDbQAWxh2lCTnwkFomd2U67/Pt957QpIE3a" +
                "w7aqre3k7ru7777zjO5qE6jlEFTFhG0fWFN0FNhq0kZV1oVoSjL20flWReMsqcL1kWLNFKKKTO4xHbwr" +
                "XMyzj6w0e6rHZQY3igYRomq7PDufIG/h24dv3xNIH2hGCtDeq0EAS9d2zrKNgQo2tiLPnfMRyTmbZ4u/" +
                "/Mmzz7cf5shEP7ShCsdjDXk2I2RptfIaHEWlVVQkBdWmqtkfNrziRvJvOySW3sah43AknolZfCu27FXT" +
                "DBO3qK3trSmFu4maLYC4GhBBnfJgqW+Uh4Pz2lixf/Sq5YQvv8A/erYl0+X5HFY2cNlHg6QGYJSeVRDq" +
                "Ls9h3BsbT0/EA453a3eIM1fo0JQBmqiiZMw/Ow9FICMV5hLmzVjjEeBBEiOQDrSfnj3gGA4IcZAFWlTW" +
                "tI/0b4ZYQykii5XyRhVNUlcJHgC7J057B8+hJfU5WQVtbPBHyF2QP8G1O2Ap67BG8xqhIPQVeIRl593K" +
                "aNgWQ0IpGwORUWMKr/yQZ+I2BgXIeyEbZvBLvcGqQnClQSc0rU2s8yxELwFSXx6MzrN/ps7dNI4ifT1K" +
                "k+i241y7Bjxu5xQjhrnCjBmrDTjoVbObshczvJGX/N+4EIyQnKSeEFwHOcs9gIDFAEleXy3ebrZfz5Zf" +
                "Fu82h4vl8nq5ONmcbu/OPl0sTgU2ncfZmW3W57jEtm9lLwIs3IppYtlKP3BRkOZQetMl803qESo+3hU0" +
                "3hdwmpy3tPzGf+Rosq4x9GvlpafJY3smTC6GjkazPLvi4V41PeMiW8n6+iJbjQ9f6eZFxP+omW22U5lP" +
                "PCDhtUw+bqZGFWhGEnzKGy/Y0srw+jmR4yshZdzBMXpVPhF6Nd4nUtEvVPmFNlwGAAA=";
                
    }
}

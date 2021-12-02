/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Octomap : IDeserializable<Octomap>, IMessage
    {
        // A 3D map in binary format, as Octree
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Flag to denote a binary (only free/occupied) or full occupancy octree (.bt/.ot file)
        [DataMember (Name = "binary")] public bool Binary;
        // Class id of the contained octree 
        [DataMember (Name = "id")] public string Id;
        // Resolution (in m) of the smallest octree nodes
        [DataMember (Name = "resolution")] public double Resolution;
        // binary serialization of octree, use conversions.h to read and write octrees
        [DataMember (Name = "data")] public sbyte[] Data;
    
        /// Constructor for empty message.
        public Octomap()
        {
            Id = string.Empty;
            Data = System.Array.Empty<sbyte>();
        }
        
        /// Explicit constructor.
        public Octomap(in StdMsgs.Header Header, bool Binary, string Id, double Resolution, sbyte[] Data)
        {
            this.Header = Header;
            this.Binary = Binary;
            this.Id = Id;
            this.Resolution = Resolution;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Octomap(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Binary = b.Deserialize<bool>();
            Id = b.DeserializeString();
            Resolution = b.Deserialize<double>();
            Data = b.DeserializeStructArray<sbyte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Octomap(ref b);
        
        Octomap IDeserializable<Octomap>.RosDeserialize(ref Buffer b) => new Octomap(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Binary);
            b.Serialize(Id);
            b.Serialize(Resolution);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 17;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Id);
                size += Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "octomap_msgs/Octomap";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9a45536b45c5e409cd49f04bb2d9999f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1TTW8TMRC9+1eMlEMT1CQSRQhF4oCICj0gEO2tQtXEnuxa8tqL7U1Zfj1vdpsCNw5E" +
                "kfZr3pt5740X9I6u9tRxTz7SwUfOIx1T7rheEhf6bGsWMR+FnWRqp4sxC7oO3FBN5CSmKsRn6DLFAAJg" +
                "tsnaoffiVpQyHYcQaHrD0Y64U1pabg51u0mVjj7IyhxSCk9E2uN94FLIO0pHqq2QTbGyj+LOcFNq9rFB" +
                "iZZ/lZLCUH2KtISWbnXGlY5DkFLPsJicFHMMievrV5SfYUryJKNI9hz8T57owDNDL2ko0xwnyQVfyqZV" +
                "EzJsIY6OHrOHGXNtMT7WN/ffyHFlY97+55/5dPthR6W6h640ZTsHBAG3FYNwdtRJZW2taVLrm1byOshJ" +
                "AkDc9XBx+lrHXsoGwLvWw+xCjUTJMGxUrU7l2dR1Q/SWoa36Tv7CAwmzmXrO1dshcEZ9yg4+ovyYuRNl" +
                "x7/I90GiFbrZ79TDIha2Y6ARDBYeFs3yZk9mgHNXLxVgFnePaY1HabB+z80RK1cdVn70yE/n5LJDjxez" +
                "uA24YY6giyu0nN494LGsCE0wgvTJttOafBlri4h1T06M0A9BlNjqyji6UNDF6g9mHXtHkWM608+Mv3v8" +
                "C62yzLyqad0is6Dqy9DAQBT2OZ28Q+lhnHc/eImVgj9kPR2KmluaxbV6jCKgpkRwxblJ1iMArKSv7fmc" +
                "TGk86Gn5BW5gtlP3AwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

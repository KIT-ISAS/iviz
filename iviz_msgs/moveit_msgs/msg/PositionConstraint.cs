/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PositionConstraint")]
    public sealed class PositionConstraint : IDeserializable<PositionConstraint>, IMessage
    {
        // This message contains the definition of a position constraint.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The robot link this constraint refers to
        [DataMember (Name = "link_name")] public string LinkName;
        // The offset (in the link frame) for the target point on the link we are planning for
        [DataMember (Name = "target_point_offset")] public GeometryMsgs.Vector3 TargetPointOffset;
        // The volume this constraint refers to 
        [DataMember (Name = "constraint_region")] public BoundingVolume ConstraintRegion;
        // A weighting factor for this constraint (denotes relative importance to other constraints. Closer to zero means less important)
        [DataMember (Name = "weight")] public double Weight;
    
        /// <summary> Constructor for empty message. </summary>
        public PositionConstraint()
        {
            LinkName = string.Empty;
            ConstraintRegion = new BoundingVolume();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PositionConstraint(in StdMsgs.Header Header, string LinkName, in GeometryMsgs.Vector3 TargetPointOffset, BoundingVolume ConstraintRegion, double Weight)
        {
            this.Header = Header;
            this.LinkName = LinkName;
            this.TargetPointOffset = TargetPointOffset;
            this.ConstraintRegion = ConstraintRegion;
            this.Weight = Weight;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PositionConstraint(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            LinkName = b.DeserializeString();
            b.Deserialize(out TargetPointOffset);
            ConstraintRegion = new BoundingVolume(ref b);
            Weight = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PositionConstraint(ref b);
        }
        
        PositionConstraint IDeserializable<PositionConstraint>.RosDeserialize(ref Buffer b)
        {
            return new PositionConstraint(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(LinkName);
            b.Serialize(TargetPointOffset);
            ConstraintRegion.RosSerialize(ref b);
            b.Serialize(Weight);
        }
        
        public void RosValidate()
        {
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
            if (ConstraintRegion is null) throw new System.NullReferenceException(nameof(ConstraintRegion));
            ConstraintRegion.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(LinkName);
                size += ConstraintRegion.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PositionConstraint";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c83edf208d87d3aa3169f47775a58e6a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1X227bRhB9J+B/WMAPllpGTeOgKFz4IYmdWkCTuLEb5IJAWJEjchGKy+wuZStf3zO7" +
                "y4scO+lD48SAeNk5M3Pmyn1xWSor1mStLEhkunZS1Va4kkROK1Urp3Qt9EpI0Wgb7nDKOoNzbpYkZyRz" +
                "MqL0P0myD0ASRi+1E5WqPwEJ+IOEMLQiAwU6wRNVF/7UopZr6oT1amXJiYmqvRkeZWVwYCpW2vhnTpoC" +
                "RxrNiHp07oqENCSaStY1g0MgKUivyZntYm0L+8sbypw2hxFi4SEWQWVnwEZX7ZrutlwkT3Vb58B/E04O" +
                "hxaGClDESE9gjCpK582QrDSav4s6yanWjizgK+nUhoRaN9o4WWcwQQsN38xIwM7Es0pbPMPLL2Q0gicR" +
                "sQoh7EXdNFlVWrrfHkcjkr3k+H/+t5e8uPjzSFiXB2JDIuzB8wtYkEuTwzInc+mkd7yEHWQeVLShClJy" +
                "3VAu/Fu3bcjOPPngBn8F1WRkVW1Fa3EIjmZ6vW5rlUkHUhTSdSwPSeQKElQap7K2ksyXNggQH/epw+j4" +
                "s/S5JSZ2fnLkOaWsZc6hSdWZIWk5WvMTkbSg+vARCyT7l1f6AW6pYNI75QikdGwsXTcG3MMYaY+g46fg" +
                "3AzYYIegJbdi4p8tcGunAkpgAjU6K32Wn29dGZN4I42SywpJgCQBA0A9YKGD6QiZzT4Stax1Bx8QBx3/" +
                "BbbucdmnByVihhIqhG0LEIiDjdEblePocutBskoRErZSSyPNNmGpoDLZf84ch9T2EcGvtFZnCgHIxZVy" +
                "ZVftPhoLlf+4hLy13Pe67DLE0YIfsFBs/EtOnpUhONPIjGacJ3MfWV0jL7i6HKdgLwnBXBmIotBn3DDQ" +
                "GLShVCgnco1KRj0DYy0/AZJAM0vLpgEYct2gWLnQOTKaRSY0K2apuCqpDqeYJp/UvgxUJowqVB4koWjd" +
                "C0sRvUuFWz0CzVUVbA7KEDOAGO28wHQm5iux1a24YodwYWL1abGk3i6fJU7rlEsvQuwyeu6bbjcxMCwc" +
                "Ch+DoGs41/3Vtr/68sOivdYbUi5YttuVOeQnPMIwErqWjkgfnoTmzCMGY83qCtxGD8F1Y9QaU26DMPoC" +
                "91FsudqHuaDDaCQxkZxF6EugN7GlbCgYcsGg5x3Sh48j1G7GYJhCBRRclQptgOFHqnmGVZq7XX5jfJ1D" +
                "bgy48ECMOkf65HmY0DBxQEs5WCVQM1lzqG1DmVqp0Fghwj+sfxnp6/ycyAIDJ0Ul3+LiC0DCjoD8Daei" +
                "6u86xOeiLz9uVN0VoVGqLPV1Coa4qFORbdERMdRwpfGO8wYl5mGCSyEPPIOlxOiGx8oMRAKMaUfPABzm" +
                "g+EX4mGK/ygYHjG/i6ev3h7/Gq8vzs9OX58eP4q3z979NX95cvr6+LB78Orl6fHjjm0em5zCTLO3KZ7i" +
                "50l3KEeXri2iZ3ePxsoE88OJToZbHps/FhgdOxIkEV2/knArCSSEnZHpuuYy45uDQeYAzhu5ZQ3P4w4H" +
                "x72pqb97m4p3SDXQ835sM5PslzuqC1d2FmXaoBU3GH0wk8c6xlTvH0ifDdwu3h4/HN2967nmu/egemxS" +
                "4D9a5Tsphx27B1Rx48dM4IUk2InWXcQ+gYVX5qplE3icMmc+gzo7Au7i9ZOT+T8XsGesswuyx+QAh3Uo" +
                "sBJSh1dbP7q14QnMmVRp7zifeS/ktcJWOEyiHdzF2en8z7NLMWHseDMdfAIIeBsxPvhU+s2x5zzWgphw" +
                "LUyDPkj3eoJ3UU+4Gem5SwtPqI67ED6JuXOnzmccEGaqewX5G31zVJNodpkyfiP0gxqbTjPkkOeU5Xmg" +
                "cb63TRqYFT9HUrsivUFmn1I3nEdyjSr1q8MDMTh4T/sPt1dub0+G/SXsD6CB2+3u8pOGgPEyG96HccKE" +
                "h9zzsliRwhLQHUj+btHZDQ+J8bn78xHG9Bvezjds70L8gmWrdzz+9tpyXx4M/N26qO6wums/330e2Of9" +
                "8Hu72PBpeC+jljeFfsCOgsETnxthpWxoMvhEqQt8xv4xmiQbWbX+2xif3n6rifHEZ2KN9Z2/98h++Jiw" +
                "kssIgJnWYyWxSeL7u5VVL/H1buetuSWtANYJ3RtbnSO3sdZ5dmAHu8K36ofDYCpdL8BdNPhfT4aw8OAR" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

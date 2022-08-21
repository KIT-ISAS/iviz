/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class SoftBiometrics : IDeserializable<SoftBiometrics>, IMessage
    {
        // This message describes soft biometrics (age and gender)
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "age")] public byte Age;
        [DataMember (Name = "age_confidence")] public float AgeConfidence;
        public const byte UNDEFINED = 0;
        public const byte FEMALE = 1;
        public const byte MALE = 2;
        public const byte OTHER = 3;
        /// <summary> One of UNDEFINED, FEMALE, MALE, OTHER </summary>
        [DataMember (Name = "gender")] public byte Gender;
        [DataMember (Name = "gender_confidence")] public float GenderConfidence;
    
        public SoftBiometrics()
        {
        }
        
        public SoftBiometrics(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Age);
            b.Deserialize(out AgeConfidence);
            b.Deserialize(out Gender);
            b.Deserialize(out GenderConfidence);
        }
        
        public SoftBiometrics(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Age);
            b.Deserialize(out AgeConfidence);
            b.Deserialize(out Gender);
            b.Deserialize(out GenderConfidence);
        }
        
        public SoftBiometrics RosDeserialize(ref ReadBuffer b) => new SoftBiometrics(ref b);
        
        public SoftBiometrics RosDeserialize(ref ReadBuffer2 b) => new SoftBiometrics(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Age);
            b.Serialize(AgeConfidence);
            b.Serialize(Gender);
            b.Serialize(GenderConfidence);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Age);
            b.Serialize(AgeConfidence);
            b.Serialize(Gender);
            b.Serialize(GenderConfidence);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 10 + Header.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c += 1; // Age
            c = WriteBuffer2.Align4(c);
            c += 4; // AgeConfidence
            c += 1; // Gender
            c = WriteBuffer2.Align4(c);
            c += 4; // GenderConfidence
            return c;
        }
    
        public const string MessageType = "hri_msgs/SoftBiometrics";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "791877398420c10707c4d9a832b0e6ad";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61TQW7bMBC88xUL+JC4iNMmuQQGfChguwnQpEXinorCWJMriYBEqiTlVL/viLIdH3uo" +
                "QIhacmd2OEtNaFPZSI3EyKWQkaiD3Umk6ItEO+sbScHqSJfDNjtDpTgjYaoehDFTlSelOuvSPSFJFbXn" +
                "dHc7fG+1d4U14rQcM348L1frx+fVcvHpsLJePX3+ulrcHMIc3B6Cb5uH1cvi7ggea9OEvBPyxTvZ1YHl" +
                "isZ3xp2UjLAzMaSUWvznRz29fplTTGbbxDJ+HO1RE3pNcI2DgceJDSemwsM2W1YSZrXspQaIm1YM5d3U" +
                "txKvAcyNwYB6CVzXPXURScmT9k3TOas5CSWL3p3jgbSOmFoOyequ5oB8H4x1Q3oRuJGBHSPK7y678bic" +
                "I8dF0V2yENSDQQfhaF2JTcruw0cA4P3PFx9vfqnJ5s3PsC4lOnJSQaniNKiWP23ApYIqjnMU+zCe8hpF" +
                "4JKgnMGlymtbhHFKqAYt0npd0SWO8L1PlXcgFNpzsLyrZSDWsAKsFwPoYnrG7DK1Y+eP9CPje41/oXUn" +
                "3uFMswrNqwcbYlfCSSS2we9xiwzt+kyiaysuUW13gUOvBtRYUk3Wg9lIAiq3BjPH6LVFJwy92VSpiL8L" +
                "7LktW2uU+gvOqjdbkQMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

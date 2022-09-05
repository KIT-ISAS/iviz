/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class FacialActionUnits : IDeserializable<FacialActionUnits>, IHasSerializer<FacialActionUnits>, IMessage
    {
        // This message the intensity of each actions unit (AU), with their confidence, for a specific face.
        //
        // It follows the naming convention of the  Facial Action Coding System (FACS) developed by Ekman.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // List taken from https://en.wikipedia.org/wiki/Facial_Action_Coding_System
        // Main codes
        /// <summary> Neutral face </summary>
        public const byte NEUTRAL_FACE = 0;
        /// <summary> Inner brow raiser </summary>
        public const byte INNER_BROW_RAISER = 1;
        /// <summary> Outer brow raiser </summary>
        public const byte OUTER_BROW_RAISER = 2;
        /// <summary> Brow lowerer </summary>
        public const byte BROW_LOWERER = 4;
        /// <summary> Upper lid raiser </summary>
        public const byte UPPER_LID_RAISER = 5;
        /// <summary> Cheek raiser </summary>
        public const byte CHEEK_RAISER = 6;
        /// <summary> Lid tightener </summary>
        public const byte LID_TIGHTENER = 7;
        /// <summary> Lips toward each other </summary>
        public const byte LIPS_TOWARD_EACH_OTHER = 8;
        /// <summary> Nose wrinkler </summary>
        public const byte NOSE_WRINKLER = 9;
        /// <summary> Upper lip raiser </summary>
        public const byte UPPER_LIP_RAISER = 10;
        /// <summary> Nasolabial deepener </summary>
        public const byte NASOLABIAL_DEEPENER = 11;
        /// <summary> Lip corner puller </summary>
        public const byte LIP_CORNER_PULLER = 12;
        /// <summary> Sharp lip puller </summary>
        public const byte SHARP_LIP_PULLER = 13;
        /// <summary> Dimpler </summary>
        public const byte DIMPLER = 14;
        /// <summary> Lip corner depressor </summary>
        public const byte LIP_CORNER_DEPRESSOR = 15;
        /// <summary> Lower lip depressor </summary>
        public const byte LOWER_LIP_DEPRESSOR = 16;
        /// <summary> Chin raiser </summary>
        public const byte CHIN_RAISER = 17;
        /// <summary> Lip pucker </summary>
        public const byte LIP_PUCKER = 18;
        /// <summary> Tongue show </summary>
        public const byte TONGUE_SHOW = 19;
        /// <summary> Lip stretcher </summary>
        public const byte LIP_STRETCHER = 20;
        /// <summary> Neck tightener </summary>
        public const byte NECK_TIGHTENER = 21;
        /// <summary> Lip funneler </summary>
        public const byte LIP_FUNNELER = 22;
        /// <summary> Lip tightener </summary>
        public const byte LIP_TIGHTENER = 23;
        /// <summary> Lip pressor </summary>
        public const byte LIP_PRESSOR = 24;
        /// <summary> Lips part </summary>
        public const byte LIPS_PART = 25;
        /// <summary> Jaw drop </summary>
        public const byte JAW_DROP = 26;
        /// <summary> Mouth stretch </summary>
        public const byte MOUTH_STRETCH = 27;
        /// <summary> Lip suck </summary>
        public const byte LIP_SUCK = 28;
        // Head movement codes
        /// <summary> Head turn left </summary>
        public const byte HEAD_TURN_LEFT = 51;
        /// <summary> Head turn right </summary>
        public const byte HEAD_TURN_RIGHT = 52;
        /// <summary> Head up </summary>
        public const byte HEAD_UP = 53;
        /// <summary> Head down </summary>
        public const byte HEAD_DOWN = 54;
        /// <summary> Head tilt left </summary>
        public const byte HEAD_TILT_LEFT = 55;
        /// <summary> Head tilt right </summary>
        public const byte HEAD_TILT_RIGHT = 56;
        /// <summary> Head forward </summary>
        public const byte HEAD_FORWARD = 57;
        /// <summary> Head back </summary>
        public const byte HEAD_BACK = 58;
        // Eye movement codes
        /// <summary> Eyes turn left </summary>
        public const byte EYES_TURN_LEFT = 61;
        /// <summary> Eyes turn right </summary>
        public const byte EYES_TURN_RIGHT = 62;
        /// <summary> Eyes up </summary>
        public const byte EYES_UP = 63;
        /// <summary> Eyes down </summary>
        public const byte EYES_DOWN = 64;
        /// <summary> Walleye </summary>
        public const byte WALLEYE = 65;
        /// <summary> Cross-eye </summary>
        public const byte CROSS_EYE = 66;
        /// <summary> Eyes positioned to look at other person : The 4, 5, or 7, alone or in combination, occurs while the eye position is fixed on the other person in the conversation. </summary>
        public const byte EYES_POSITIONED_TO_LOOK_AT_OTHER_PERSON = 69;
        // Visibility codes
        /// <summary> Brows and forehead not visible </summary>
        public const byte BROWS_AND_FOREHEAD_NOT_VISIBLE = 70;
        /// <summary> Eyes not visible </summary>
        public const byte EYES_NOT_VISIBLE = 71;
        /// <summary> Lower face not visible </summary>
        public const byte LOWER_FACE_NOT_VISIBLE = 72;
        /// <summary> Entire face not visible </summary>
        public const byte ENTIRE_FACE_NOT_VISIBLE = 73;
        /// <summary> Unsociable </summary>
        public const byte UNSOCIABLE = 74;
        // Gross behavior codes
        /// <summary> Jaw thrust </summary>
        public const byte JAW_THRUST = 29;
        /// <summary> Jaw sideways </summary>
        public const byte JAW_SIDEWAYS = 30;
        /// <summary> Jaw clencher : masseter </summary>
        public const byte JAW_CLENCHER = 31;
        /// <summary> [Lip] bite </summary>
        public const byte LIP_BITE = 32;
        /// <summary> [Cheek] blow </summary>
        public const byte CHEEK_BLOW = 33;
        /// <summary> [Cheek] puff </summary>
        public const byte CHEEK_PUFF = 34;
        /// <summary> [Cheek] suck </summary>
        public const byte CHEEK_SUCK = 35;
        /// <summary> [Tongue] bulge </summary>
        public const byte TONGUE_BULGE = 36;
        /// <summary> Lip wipe </summary>
        public const byte LIP_WIPE = 37;
        /// <summary> Nostril dilator : nasalis (pars alaris) </summary>
        public const byte NOSTRIL_DILATOR = 38;
        /// <summary> Nostril compressor : nasalis (pars transversa) and depressor septi nasi </summary>
        public const byte NOSTRIL_COMPRESSOR = 39;
        /// <summary> Sniff </summary>
        public const byte SNIFF = 40;
        /// <summary> Lid droop : Levator palpebrae superioris (relaxation) </summary>
        public const byte LID_DROOP = 41;
        /// <summary> Slit : Orbicularis oculi muscle </summary>
        public const byte SLIT = 42;
        /// <summary> Eyes closed : Relaxation of Levator palpebrae superioris </summary>
        public const byte EYES_CLOSED = 43;
        /// <summary> Squint : Corrugator supercilii and orbicularis oculi muscle </summary>
        public const byte SQUINT = 44;
        /// <summary> Blink : Relaxation of Levator palpebrae superioris; contraction of orbicularis oculi (pars palpebralis) </summary>
        public const byte BLINK = 45;
        /// <summary> Wink : orbicularis oculi </summary>
        public const byte WINK = 46;
        /// <summary> Speech </summary>
        public const byte SPEECH = 50;
        /// <summary> Swallow </summary>
        public const byte SWALLOW = 80;
        /// <summary> Chewing </summary>
        public const byte CHEWING = 81;
        /// <summary> Shoulder shrug </summary>
        public const byte SHOULDER_SHRUG = 82;
        /// <summary> Head shake back and forth </summary>
        public const byte HEAD_SHAKE_BACK_AND_FORTH = 84;
        /// <summary> Head nod up and down </summary>
        public const byte HEAD_NOD_UP_AND_DOWN = 85;
        /// <summary> Flash </summary>
        public const byte FLASH = 91;
        /// <summary> Partial flash </summary>
        public const byte PARTIAL_FLASH = 92;
        /// <summary> Shiver/tremble </summary>
        public const byte SHIVER_TREMBLE = 97;
        /// <summary> Fast up-down look </summary>
        public const byte FAST_UP_DOWN_LOOK = 98;
        [DataMember] public float[] FAU;
        /// <summary> An array of 99 floats, one per AU. Use the constant defined above to access one specific AU. </summary>
        [DataMember (Name = "intensity")] public float[] Intensity;
        /// <summary> An array of 99 floats, one per AU. Use the constant defined above to access one specific AU. </summary>
        [DataMember (Name = "confidence")] public float[] Confidence;
    
        public FacialActionUnits()
        {
            FAU = EmptyArray<float>.Value;
            Intensity = EmptyArray<float>.Value;
            Confidence = EmptyArray<float>.Value;
        }
        
        public FacialActionUnits(in StdMsgs.Header Header, float[] FAU, float[] Intensity, float[] Confidence)
        {
            this.Header = Header;
            this.FAU = FAU;
            this.Intensity = Intensity;
            this.Confidence = Confidence;
        }
        
        public FacialActionUnits(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                FAU = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Intensity = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Confidence = array;
            }
        }
        
        public FacialActionUnits(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                FAU = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Intensity = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Confidence = array;
            }
        }
        
        public FacialActionUnits RosDeserialize(ref ReadBuffer b) => new FacialActionUnits(ref b);
        
        public FacialActionUnits RosDeserialize(ref ReadBuffer2 b) => new FacialActionUnits(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(FAU);
            b.SerializeStructArray(Intensity);
            b.SerializeStructArray(Confidence);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(FAU);
            b.SerializeStructArray(Intensity);
            b.SerializeStructArray(Confidence);
        }
        
        public void RosValidate()
        {
            if (FAU is null) BuiltIns.ThrowNullReference();
            if (Intensity is null) BuiltIns.ThrowNullReference();
            if (Confidence is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += Header.RosMessageLength;
                size += 4 * FAU.Length;
                size += 4 * Intensity.Length;
                size += 4 * Confidence.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // FAU length
            c += 4 * FAU.Length;
            c += 4; // Intensity length
            c += 4 * Intensity.Length;
            c += 4; // Confidence length
            c += 4 * Confidence.Length;
            return c;
        }
    
        public const string MessageType = "hri_msgs/FacialActionUnits";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "7872c889685bdf665183ddf7f24a1dd9";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VYW2/iSBp951eUlIfprLqTDpfcVn5wwARPCGZt06jValmFKaAUY3tcduj8+zlfGRtD" +
                "sqNdaabVUoA636W+67HPmL+Rim2FUnwtWL4RTMa5iJXM31iyYoKHG8bDXCaxYkUsc/bJnJ1/ZjuZbwgt" +
                "MxYm8UouRRyKz2yVZIwzlYpQrmTIVjwUF62z1hmzc5xFUbJT2kbMtzJek+iriEk52aIDNuSh5BEztUnW" +
                "T5aE895ULrbs09Dse+dsKV5FlKRiyRZvzHrZ8vii1RoJvhQZ2+g/LVgcS5WznL+ImK2yZMs2eZ6q+8tL" +
                "EV/s5IuEuOQXSba+pG+XpdmgNBuUZoPSLCl75jKGt0uhWgXic8sm1sx3zXEAjyzjK2NnbCKKPIPndGe2" +
                "R9mTieUGD64zD1zT9izXuCKoHcdwdZElO5ZxqfB5j3dm/gm+TXinyD/Ga+TYmVsuoF2CPhAIYRbZATWb" +
                "TqF1bA8qpT1CztIUkEguT3T2R5b1VCGvCdnfCPFygiJtvv048i3c0Lgh2BiqcrneoHqauKkX+M7cdAeB" +
                "ZfZHgeOPIHBbCqSohmTHs2VZZwkqoJacOJ4VzF178jSGwJ0OcaIE22Uyfone325ah/hr83rpieMT03PG" +
                "5oON3A0sa6rdv9JZmXCVRHxB1bcUIj25RdB3XErmdDYmf67a+xugKjJCpkXUcMobme5UO1XhO4T3NjxL" +
                "tVPH8IH9PNUoncOB3KbRh8YH1tS1PM8BsndifynSDE2cHMSoLLQLDSmdzzGVh/binVB/ZE/qON6UyUfl" +
                "n+aertV/IkyVSFwofDlAfGfyOLMCb+TMjSudOz+J14VgaoP6bOjxfNfy+1QS7a+VKpVnIg+bpWD1nxrV" +
                "1i7TJcKXD+stGM7QdhTOdp2kVYGWO4lpQ2Gnwn2or4peu1tf9iTUVORT0/WNdq8u7JRneQX43ZwHA9eZ" +
                "Gm2dgN/5ji2zJK2On9H3oyoURlvH/TkpMGH3oTiKGCJvtOu4K8Sd0YiiAci2yavYYqIezaqRZaJZZ+4k" +
                "GFtD3+jp8Gl4XmQxi8Qqf4d0KThGr30MzSg8TexsavQ6NaZIm2cDZz4xet36dJns4iM79tjfe9Q7mJFR" +
                "/t4jQu49uj6GvvNo6Lg0bIzeTQ3EUqIZ00Q9mAhi77aGLHj4QkG03sTHMbS+W14jhtc6hkCrdzE8IEuP" +
                "r9vH0KbHGosYXndqTB1DfaZjeN2tTxsxnJsYLN8t41oHb84xT95E1cau43mBPi0neJYo9eVwrnVPHc/2" +
                "bWdiIcAOtojzFJh+OZ8DjFTPgeW72nKagBJgN2Lr5gk2TPLCeL6f2Ji0Csv6HlxCsO5n1vvM0Bw3nxmP" +
                "IECf9fbcLmTMSQeOw7DIFNttZFSSDvhWm2AgJCv5C5bwmQ6PrMjyN00dMqX1XVDqvkklFzIi3tJMHK1I" +
                "LzAnujIsnfyJ4wffbM9+GFvGzddqayrGY10rgjgEi5OcvZLK6ChoR7KHIniPLscv0YNjmfZhAmum8IGd" +
                "iW+71geiZZGALmXiv8nOJp7Tt00N11Uzi1UCZkMIxOiR6oAtxIa/yiQ7ihONKH/kzjwMsbtqSOWbrFB5" +
                "A+HZA2tufveMztcKo8D9dvytqac/tiZ6pneuKlQYgSBSFu/ZlislQGgaM+3B9i2jo0PzA0PtJ1vIvK5l" +
                "zUceEE+joyPwQ/MRYEBzjjDT2XBodLpNTFqsVkcYPT07vSaGJujx2nqYjR/hj26dH+XigrkiWouGz3N7" +
                "CsxNNYd3YJQH5uK7NviFPTZ97I3O7Z685JkEt5ARzxMKRMwVj1Drn7AqUH0Rz6Q6P9HRd56r9dO5a6pB" +
                "O1Vb6FQTaGisdHOc65o+bHkl0lwSWlY8ZWIjZl2dTC+WdbCI3mFhYWN1rypuh42FlXXPxuJVXyDlUSoW" +
                "GcdSL9CbKCjyIBMR/6W7srqKN7Z9o6uT66E9ocHJFjIs9H0xCYpIsm2hwuNG64/B/wZG9zAZwwj0bwlx" +
                "tzZBTw1/5U7lwX9m9gQ+6NLw/qAfoaafZFmx1rJaIsTwkDpgyV/79zAGKTW6uogeIrDR/8unf9PoQorC" +
                "CvzeWpnGSjg6VMVcG9Z1OS/tvhOurjy1LHCJXpnZVIhwU53Q4kAz3ZZHOx4d9RFMPBq3V3vev8NjUE1p" +
                "ndl4gJnmYUoAUiZ0kxQRPXcpTIoKqYcsGPCTpfdsNXz9kXF7YANqg0czvXeruZtvmvITh9iFltVL8PbA" +
                "EeKEqEZZ2od9OByb3si4054PI64qbcTLiO3vz7XbU3Az4vmrBs4b2d9wO5CwZxqfdzfl/SQa6RIsbHsY" +
                "skPT88k38kvvTeNOd/iQ44mzSL+QU3pDtlqrKOF5p/3jJ4RmjW+HZ+wz3IPxLOP6cfvujmmQworE5qRH" +
                "GHN2wWZKVEtP5RzVuxQrSauYL0BWaCHzMESLa6H66RuSDZOHx/R/2marZfzN/1rP3uM9yPAy2Kq1uiyf" +
                "9bHPPDi2pMfHrcj5kudcv4DYgGGJ7EtErwkgxLf0qkCf5m+pUBcQ1C898H9NXB8d8MYKVTIbzNVtEcuQ" +
                "57ij3IojeUiCfnDN7cu2Az7JlsRrsJMzvqXXHQRT4o9Cx9oe3OsQirDIUUuwJOMwExjB8RqHJbPH6oMA" +
                "bRs3UVc/W2f+LvlCNbJGMmovkA5QLngtfumBTrlQ9zD2r/KWFzCCKAmYW2IS698CfFXnWNLki0gTPEt8" +
                "whWmb/lmT65eMTuIH5DikFjkkv1GQr+dNzTHWnXM46RSX2o82Phf1Ma1XrrTlw2SF1EYFMZwRsA0S15R" +
                "o/rFjq69SBIRjyTGYPbWIqnSZOtsSMEGCFI6NfgLXkFcJ4c8vZ5q0aKEdp2WQC5brT8BN7T9kO0SAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<FacialActionUnits> CreateSerializer() => new Serializer();
        public Deserializer<FacialActionUnits> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<FacialActionUnits>
        {
            public override void RosSerialize(FacialActionUnits msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(FacialActionUnits msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(FacialActionUnits msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(FacialActionUnits msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<FacialActionUnits>
        {
            public override void RosDeserialize(ref ReadBuffer b, out FacialActionUnits msg) => msg = new FacialActionUnits(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out FacialActionUnits msg) => msg = new FacialActionUnits(ref b);
        }
    }
}

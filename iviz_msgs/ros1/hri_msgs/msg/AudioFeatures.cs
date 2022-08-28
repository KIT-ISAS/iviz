/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class AudioFeatures : IDeserializable<AudioFeatures>, IMessage
    {
        // This message encodes the 16 audio features selected
        // in the INTERSPEECH2009 challenge.
        // 
        // Reference: Schuller, Steidl, Batliner, The INTERSPEECH 2009 Emotion Challenge
        // 
        // They can be extract using the OpenSMILE toolkit with the present
        // IS09_emotion.conf
        /// <summary> Zero-crossing rate from the time signal </summary>
        [DataMember] public float ZCR;
        /// <summary> Root mean square frame energy </summary>
        [DataMember] public float RMS;
        /// <summary> Pitch frequency (normalised to 500 Hz) </summary>
        [DataMember (Name = "pitch")] public float Pitch;
        /// <summary> Harmonics-to-noise ratio by autocorrelation function </summary>
        [DataMember] public float HNR;
        /// <summary> Mel-frequency cepstral coefficients 1 to 12 </summary>
        [DataMember] public float[] MFCC;
    
        public AudioFeatures()
        {
            MFCC = EmptyArray<float>.Value;
        }
        
        public AudioFeatures(ref ReadBuffer b)
        {
            b.Deserialize(out ZCR);
            b.Deserialize(out RMS);
            b.Deserialize(out Pitch);
            b.Deserialize(out HNR);
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
                MFCC = array;
            }
        }
        
        public AudioFeatures(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out ZCR);
            b.Deserialize(out RMS);
            b.Deserialize(out Pitch);
            b.Deserialize(out HNR);
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
                MFCC = array;
            }
        }
        
        public AudioFeatures RosDeserialize(ref ReadBuffer b) => new AudioFeatures(ref b);
        
        public AudioFeatures RosDeserialize(ref ReadBuffer2 b) => new AudioFeatures(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(ZCR);
            b.Serialize(RMS);
            b.Serialize(Pitch);
            b.Serialize(HNR);
            b.SerializeStructArray(MFCC);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(ZCR);
            b.Serialize(RMS);
            b.Serialize(Pitch);
            b.Serialize(HNR);
            b.SerializeStructArray(MFCC);
        }
        
        public void RosValidate()
        {
            if (MFCC is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 20 + 4 * MFCC.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // ZCR
            c += 4; // RMS
            c += 4; // Pitch
            c += 4; // HNR
            c += 4; // MFCC length
            c += 4 * MFCC.Length;
            return c;
        }
    
        public const string MessageType = "hri_msgs/AudioFeatures";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "1942f4cd8b6bb147f1ccb9aded9b6535";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1WR0UvDMBDG3/tXHOxFYRvdREEfLZUN3JR1T4pIll7aYJrrkiu6/fVeOzYRAgl33/3y" +
                "5csItrWN0GCMqkJAr6nECFwjzO5AdaUlMKi4C1KN6FAzlskIrB80y/U23xSveZ4t5ml6D7pWzqGvcCoa" +
                "WRs0GASKD1DoupNeGEPBaEs3hkfFzvq+sv2PgoGVN8SWPGRn5gkp2gNo5WEndn84KM3QReurwdBLi75Y" +
                "LZ9zYCL3ZRm+LddDq5UnoGdBLIv0/hNP+Kkmb5LEOFJ8M4e3bAMjOGKgiQ4UB25QjGACNQOGbYMQbeWV" +
                "u0xtVoVMBSKWJMVa3Hcq9DOq6TPFUB0u2tayrkV92k3AfScBHeDKU2iUsxFL8Q63aQqL4/VlbLHujdUq" +
                "NOStjhOmiScR9+7kj3YH+SwmTSGgU0NupvO6P5wR7x+wesoyoTToJn8Xa2yjxOhAExpjtZWQIsx6E7N5" +
                "kvwCsvCCuyICAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

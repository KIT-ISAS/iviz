/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/TimeReference")]
    public sealed class TimeReference : IDeserializable<TimeReference>, IMessage
    {
        // Measurement from an external time source not actively synchronized with the system clock.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // stamp is system time for which measurement was valid
        // frame_id is not used 
        [DataMember (Name = "time_ref")] public time TimeRef { get; set; } // corresponding time from this external source
        [DataMember (Name = "source")] public string Source { get; set; } // (optional) name of time source
    
        /// <summary> Constructor for empty message. </summary>
        public TimeReference()
        {
            Header = new StdMsgs.Header();
            Source = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public TimeReference(StdMsgs.Header Header, time TimeRef, string Source)
        {
            this.Header = Header;
            this.TimeRef = TimeRef;
            this.Source = Source;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TimeReference(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            TimeRef = b.Deserialize<time>();
            Source = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TimeReference(ref b);
        }
        
        TimeReference IDeserializable<TimeReference>.RosDeserialize(ref Buffer b)
        {
            return new TimeReference(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(TimeRef);
            b.Serialize(Source);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Source is null) throw new System.NullReferenceException(nameof(Source));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Source);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/TimeReference";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "fded64a0265108ba86c3d38fb11c0c16";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1TwYrbQAy9G/wPghw2W8gW2lugt6XtHhYKu/egzCj20PGMq5GTdb++0jhJs7ceOjgY" +
                "O9J7T+/JK3gmLBPTQEngwHkATEBvQpwwgoSBoOSJHUHKAugkHCnOUObkes4p/CYPpyA9SK+VcxEawMXs" +
                "fj60Tdt8J/TE0C83PSsogsMIoVyKK8UhM5z64HoYbuScsMARY/BtY73vzkrF4kC74A3LtE1FpRhpRYQK" +
                "vGM6WK3LzFTGnHxI3ZnSZpVem6/TLoO2TRG2svPclWydRwlZi+4hKS3kw603xvrlP5+2eX75tlW7/G4o" +
                "Xfm4WNk2K3gRTB7Zq1eCHgWrfX3oeuJNJM1nMVntqP/KPFLROFbwauPq1VEixqg5VtMkq0HDMKXgUKgO" +
                "9g7AWkMChBFZgpsisjZkVjOtvgZR8e1X6NdESX17etxqVSrkpvPShORY4zVvnx61eApJPn+yDm18PeWN" +
                "PlOni3JVoAGhmGJ6GzVAE4tlazQflhkfFF5NIiXyBdb13U4fyz0oj6qgMetWrVX+j1n6nOqeHpED7iMZ" +
                "slMfFPbOmu7ub6FN+lbjTvmCv0D+JfkXXEM5A9tYm17Di3W9pk591MqR8zF4rd3PFcXFYNsfw56R5/M+" +
                "V1IF+WpmL3tbs9E7lpJd0CSWL/G6v5cPxLbzD0XTbbzoAwAA";
                
    }
}

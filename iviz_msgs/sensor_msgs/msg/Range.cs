/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/Range")]
    public sealed class Range : IDeserializable<Range>, IMessage
    {
        // Single range reading from an active ranger that emits energy and reports
        // one range reading that is valid along an arc at the distance measured. 
        // This message is  not appropriate for laser scanners. See the LaserScan
        // message if you are working with a laser scanner.
        // This message also can represent a fixed-distance (binary) ranger.  This
        // sensor will have min_range===max_range===distance of detection.
        // These sensors follow REP 117 and will output -Inf if the object is detected
        // and +Inf if the object is outside of the detection range.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // timestamp in the header is the time the ranger
        // returned the distance reading
        // Radiation type enums
        // If you want a value added to this list, send an email to the ros-users list
        public const byte ULTRASOUND = 0;
        public const byte INFRARED = 1;
        [DataMember (Name = "radiation_type")] public byte RadiationType { get; set; } // the type of radiation used by the sensor
        // (sound, IR, etc) [enum]
        [DataMember (Name = "field_of_view")] public float FieldOfView { get; set; } // the size of the arc that the distance reading is
        // valid for [rad]
        // the object causing the range reading may have
        // been anywhere within -field_of_view/2 and
        // field_of_view/2 at the measured range. 
        // 0 angle corresponds to the x-axis of the sensor.
        [DataMember (Name = "min_range")] public float MinRange { get; set; } // minimum range value [m]
        [DataMember (Name = "max_range")] public float MaxRange { get; set; } // maximum range value [m]
        // Fixed distance rangers require min_range==max_range
        [DataMember (Name = "range")] public float Range_ { get; set; } // range data [m]
        // (Note: values < range_min or > range_max
        // should be discarded)
        // Fixed distance rangers only output -Inf or +Inf.
        // -Inf represents a detection within fixed distance.
        // (Detection too close to the sensor to quantify)
        // +Inf represents no detection within the fixed distance.
        // (Object out of range)
    
        /// <summary> Constructor for empty message. </summary>
        public Range()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Range(StdMsgs.Header Header, byte RadiationType, float FieldOfView, float MinRange, float MaxRange, float Range_)
        {
            this.Header = Header;
            this.RadiationType = RadiationType;
            this.FieldOfView = FieldOfView;
            this.MinRange = MinRange;
            this.MaxRange = MaxRange;
            this.Range_ = Range_;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Range(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            RadiationType = b.Deserialize<byte>();
            FieldOfView = b.Deserialize<float>();
            MinRange = b.Deserialize<float>();
            MaxRange = b.Deserialize<float>();
            Range_ = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Range(ref b);
        }
        
        Range IDeserializable<Range>.RosDeserialize(ref Buffer b)
        {
            return new Range(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(RadiationType);
            b.Serialize(FieldOfView);
            b.Serialize(MinRange);
            b.Serialize(MaxRange);
            b.Serialize(Range_);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 17;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/Range";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c005c34273dc426c67a020a87bc24148";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1WXW/bNhR9N+D/cAE/1Fljt+keNhjzgAJuNgNtWtjpU1AYtERZ3CjSJSl/7NfvXFKW" +
                "7SR1t2FCEEsk77nnfvL2aK7MSktywqzwX4oc31Q4W5EwJLKgNs2mo1CKQLJSwZM00q32OJJDZm1d8N1O" +
                "j6x5DBRFlKeN0ConoS3WGNdlhI1QSsqVD8JkkiopfO1kPiSGui8hVUnvBdDwSsYGEuu1s2unRJBUWEda" +
                "eNDymTCg44c0lzJivuf1OZYZqQUpaG9rqJa0te5PZrdVoSRxDjPsdp7oF9pbwjbb6qSXBlSoUDuZD1r6" +
                "/aUywu2vGmcNKUIwFM57kN0qrakUcGelzCKeGo/Hldi17y2WLSiXQcL51gwTG2htgDxM19puafbuE93c" +
                "/BSDEMFtHdZ1oMHUFGwte8Iu/wAMOzABypzhWOLls6cA4VUeGcTgHFgkq6Jvfkdo4a4y/RyfHgUFhwVR" +
                "rUmZKN6cAS5/8XZ8SR7qdk6Ez54e/BxqZ2R+niFNUqUAzfAuIrWwX0skZF1Fb09TmLciBgl5VyN+ec5Y" +
                "FnDgooF3zc5EQhrks1A67UGD9YMayZAOdTu1MuFn+vz+fvZ2/vHz3WT8+rA2vbudvZ29m4xvmE5acwdK" +
                "i0gp+YQN5y94tN0n6MhpuY+7KaqXvNH3tjb5NU1n1yRDdkUPbO0XVlxoK8KPb5CMUucLWyw2Sm5bxV79" +
                "1YaSSy5W43MuRYguEUjVyxX3ABu+XDp6kk6ZqH3qAY+7QiX2sRIuAS2lRJ8w+20puWJRqUiqwZmdr95w" +
                "Jl8CeXI8mX9oNU1W0yWI19DBDTKzDqW/tib3h2zZDcSOa6Y4iePwNCptobdoWFFVXTX+SNn5wKFsRQ79" +
                "4CgCJc+KfJvzLbemkyDHevNw/9daubP+06o7pX2qPwGmlVwE8T3V/Tsb5CjR9PRLklxAIyF7fj18it0l" +
                "DF/aWqM+Yp5mwqF4r/6Dudbo/VlPBAPuesNLWPFg2+Y9WsixBzZJWJypu4jWn7TCweIK0RZtvMme5lrA" +
                "19cazUoV+4tGvnxEzNinzBj237D7mAoVPkrtCX4DB9xE/+vT7XyY/zYiH/JF5Vf+VbpAuFfPwTFHfFGQ" +
                "QcTs4h5TqhVqfqDlRmqK9wkbxLvcSH1zIaLw8LfiQURoBDq2VDgzs1VVG5XxjNBeSAcAFoWfBK2FCyqr" +
                "tXAQsA5dKc4UTlTssl486FEukhNqOhnhlPEyq3kcgjJlMvSy2N2mE0r9H4UDCQjeb+0A3zKOTO2VeJiE" +
                "5I5DyGSFH7GaH5KNQ8DDSTKLHaYf1xb49Fdo48wCY1ZWUh/0P+1DyRmFaG8EpqEl2hOQM/gBsC9Y6AXi" +
                "eIRm6iMywiD9En6CPCr5J7iM0gCzWYMSwdPsAl+v4EecxHS2wfTQXm2ZVjwrabV0GI26nTgBRKUAuWVn" +
                "pxs5xga/wnub8XTHA00oux0fXBpHcXSh0Oq7nb8Bu4+ZULIKAAA=";
                
    }
}

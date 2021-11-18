/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/GripperTranslation")]
    public sealed class GripperTranslation : IDeserializable<GripperTranslation>, IMessage
    {
        // defines a translation for the gripper, used in pickup or place tasks
        // for example for lifting an object off a table or approaching the table for placing
        // the direction of the translation
        [DataMember (Name = "direction")] public GeometryMsgs.Vector3Stamped Direction;
        // the desired translation distance
        [DataMember (Name = "desired_distance")] public float DesiredDistance;
        // the min distance that must be considered feasible before the
        // grasp is even attempted
        [DataMember (Name = "min_distance")] public float MinDistance;
    
        /// <summary> Constructor for empty message. </summary>
        public GripperTranslation()
        {
            Direction = new GeometryMsgs.Vector3Stamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GripperTranslation(GeometryMsgs.Vector3Stamped Direction, float DesiredDistance, float MinDistance)
        {
            this.Direction = Direction;
            this.DesiredDistance = DesiredDistance;
            this.MinDistance = MinDistance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GripperTranslation(ref Buffer b)
        {
            Direction = new GeometryMsgs.Vector3Stamped(ref b);
            DesiredDistance = b.Deserialize<float>();
            MinDistance = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GripperTranslation(ref b);
        }
        
        GripperTranslation IDeserializable<GripperTranslation>.RosDeserialize(ref Buffer b)
        {
            return new GripperTranslation(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Direction.RosSerialize(ref b);
            b.Serialize(DesiredDistance);
            b.Serialize(MinDistance);
        }
        
        public void RosValidate()
        {
            if (Direction is null) throw new System.NullReferenceException(nameof(Direction));
            Direction.RosValidate();
        }
    
        public int RosMessageLength => 8 + Direction.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/GripperTranslation";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b53bc0ad0f717cdec3b0e42dec300121";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VwW7UQAy9R+o/WOqBFm0XCRCHStwqoAckpFZcK2/GSYYmM2HGaRu+nufJbkoFQhyA" +
                "1UrJztjPfvaz95icND5IJiZNHHLP6mOgJibSTqhNfhwlbWjK4sgHGn19O42E67HnWkg53+bquDjIAw9j" +
                "L+W994360BIHirsvUivFprEgvIMFDHgcU+S6MyOLtFw0e2CcVkC1C+cT3C2p2CyWj3lWrcRBNM03Q27z" +
                "i88wjOnVlSIPpLt6rlCSceSeMHU+K4daqqaPrK9eHoxu1ou98wD2hzMcsNIwZaWdUB1D9k4MuRHO3ojs" +
                "BFTMTuDeJs4j+UxyJ4FYVYZRxa0hAf0Y7qh6+5c/R9XHq/fn9LtaHSHL6w4ZJhmTZAlqitjb0L3XDjcN" +
                "KBr5OsbkfGBFvxIPgiajpn4QMBjG6oMwikFdeVQHjLvy/HfssrqF2BLeCIFbcJwcgTY7Vi7y6nzbSTrr" +
                "0YseXnup2K3Oo+TtoRL4thIkcd/Pi/w1gvowTMHXxn1lfPCHJzTCNHJSX089p59KZej4Zvk6lVJeXpwX" +
                "+Ug9qUdCMxDqZCLCWFxeUDX5YAqBQ3V8fR/P8FNaVHcNvkjRtPVgnbM8OZ8jxvOF3BbYqI4gist0Us5u" +
                "8DOfEoIgBRlj3dEJMv80a4eJMLHfcfJlIAFcowJAfWZOz05/QLa0zylwiAf4BfExxp/AhhXXOJ116Flv" +
                "7PPUooAwxJ64w3g52s0FpO499IkNs0uc5sq8lpDV8bsiR7X2lY7gyTnH2qMBrsi4ypoMvXTjxrv/PG6/" +
                "nrNlNkw8TRKQGbFZt6aTy9LZGKCLQRikIcHVE47rhtsCFdNpO2dDXslFrPQQFRgD3wJSUGaCN9YuwJ4u" +
                "exzD5US27XZD952tKLMq69uyKGPga0q+9fvliUDD6rwuig1p8xJl7vsl5yXYsgNT1OJwuqXLhuY40b0R" +
                "wkvaT1+0XXrIq6hEYyz/PIc1+qSinyJmAWXJmVsIKmTF4G+rZam+eU0P69u8vn07qr4DMjuu9PIGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/JoyFeedbackArray")]
    public sealed class JoyFeedbackArray : IDeserializable<JoyFeedbackArray>, IMessage
    {
        // This message publishes values for multiple feedback at once. 
        [DataMember (Name = "array")] public JoyFeedback[] Array { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public JoyFeedbackArray()
        {
            Array = System.Array.Empty<JoyFeedback>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public JoyFeedbackArray(JoyFeedback[] Array)
        {
            this.Array = Array;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public JoyFeedbackArray(ref Buffer b)
        {
            Array = b.DeserializeArray<JoyFeedback>();
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] = new JoyFeedback(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new JoyFeedbackArray(ref b);
        }
        
        JoyFeedbackArray IDeserializable<JoyFeedbackArray>.RosDeserialize(ref Buffer b)
        {
            return new JoyFeedbackArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Array, 0);
        }
        
        public void RosValidate()
        {
            if (Array is null) throw new System.NullReferenceException(nameof(Array));
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i] is null) throw new System.NullReferenceException($"{nameof(Array)}[{i}]");
                Array[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 6 * Array.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/JoyFeedbackArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cde5730a895b1fc4dee6f91b754b213d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1S0UrDMBR9L/QfDux11G4yEFlfZFUmDkTdgxORtL1Zg2kyknTav/e228S9m5fknpuc" +
                "c3KSEV5q5dGQ92JL2LWFVr4mj73QLU/SOjStDmqnCZKoKkT5CRFgTUkJ4ujedrdH+O0dwjnRxVH2zyOO" +
                "Vs931/BkvHUfjd/6iz+6cTTCgkotHMFKhJoQut2wPjmOo1aZcIWX18f84yFfgEeG9Ax+Wq9uHnKGJ2fw" +
                "zXqzyZ8YnsbRqdHT99XokN6X0hq11RWEgapg2qYgN2RHoqx/3QyFPFpK+uP5t2g42fFgWirnAzRV+LIt" +
                "kxXEZFl6aHoqrTnv/PpU1cHM0gROSIXuFMNJawzpbIM0SREsJglzKlPq1qs9PyKWEhXtVcmsvucRZWiF" +
                "1h0KZYTrxqgcb3Tw9aAeHPEHSOfZ9zxNZhCe5eSY2Wc9lE0GxPD9pLYiXE5Z6+irtxlHP0ZTPRd0AgAA";
                
    }
}

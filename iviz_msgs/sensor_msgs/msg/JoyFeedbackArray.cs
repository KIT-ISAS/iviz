/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class JoyFeedbackArray : IDeserializable<JoyFeedbackArray>, IMessage
    {
        // This message publishes values for multiple feedback at once. 
        [DataMember (Name = "array")] public JoyFeedback[] Array;
    
        /// Constructor for empty message.
        public JoyFeedbackArray()
        {
            Array = System.Array.Empty<JoyFeedback>();
        }
        
        /// Explicit constructor.
        public JoyFeedbackArray(JoyFeedback[] Array)
        {
            this.Array = Array;
        }
        
        /// Constructor with buffer.
        internal JoyFeedbackArray(ref Buffer b)
        {
            Array = b.DeserializeArray<JoyFeedback>();
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] = new JoyFeedback(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new JoyFeedbackArray(ref b);
        
        JoyFeedbackArray IDeserializable<JoyFeedbackArray>.RosDeserialize(ref Buffer b) => new JoyFeedbackArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Array);
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
    
        public int RosMessageLength => 4 + 6 * Array.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/JoyFeedbackArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "cde5730a895b1fc4dee6f91b754b213d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1R0UrDMBR9z1cc2GupnSKIrC9ilYmC6PbgREba3q7BNBlJOte/97a2Mt+9L0nOTc45" +
                "OXeGVa08GvJe7gj7NtfK1+RxkLrlpbIOTauD2mtCRVTmsviEDLCmoBjiwXZ3I/r+Aemc7ET6zyWeXu+v" +
                "4cl467aN3/mzE1Uxwy0VWjqCrRBqQuj2w35yK1plwhVWb8/Z9jG7BVeK5BR9WT/dPGaMzk/Rm/Vmk70w" +
                "ei5GuGcWYozsS2mN2uoS0kCVMG2TkxsCI1nUvzaoP0xeYn6dHWXDaUaD2Uo5H6CpxJdtmSsn5kqTn6an" +
                "wpq/ncmhKnsjSxM4FRW66e+TToTK2QZJnCBYzGMmVKbQrVcHnhqWFUo6qIIpPdPIIrRS6w65MtJ1EUrH" +
                "9xx8PSgHRzzwZJEeF0l8CelZrYqY/LKH0vmAmFhU2spwcc5KoyshxDd2StFkYAIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

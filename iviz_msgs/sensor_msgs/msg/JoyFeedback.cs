
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class JoyFeedback : IMessage
    {
        // Declare of the type of feedback
        public const byte TYPE_LED = 0;
        public const byte TYPE_RUMBLE = 1;
        public const byte TYPE_BUZZER = 2;
        
        public byte type;
        
        // This will hold an id number for each type of each feedback.
        // Example, the first led would be id=0, the second would be id=1
        public byte id;
        
        // Intensity of the feedback, from 0.0 to 1.0, inclusive.  If device is
        // actually binary, driver should treat 0<=x<0.5 as off, 0.5<=x<=1 as on.
        public float intensity;
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/JoyFeedback";
    
        public IMessage Create() => new JoyFeedback();
    
        public int GetLength() => 6;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out type, ref ptr, end);
            BuiltIns.Deserialize(out id, ref ptr, end);
            BuiltIns.Deserialize(out intensity, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(type, ref ptr, end);
            BuiltIns.Serialize(id, ref ptr, end);
            BuiltIns.Serialize(intensity, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "f4dcd73460360d98f36e55ee7f2e46f1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAClWPQWvDIBiG78L+wwu9hpB0FHZoLqU5FDoYpT2sl2H0k8iMFjVt8+9nsmZ0nvT59Hlf" +
                "F9iSMNwTnEJsCXG4THtFJBsuvlmvbXzD8fOj/trXW6RVoXimh9P7Zl8nWj7Tzel8rg+JLtkDj2bGFji2" +
                "OuCmjUHrjAS30BK27xryUM6DuGj/atB4mLvk6XV9593FUDaVVdqHCEMSN9cnV0PJVRW/w0DC2f+TuaGW" +
                "Y5GdjWSDjsP89zkng/KuQ5EXiA5lnoTaCtMHfaUc2ClIumqRlCFpuIg9N2ZAoy33Qwbp0z2P0E7J0ROP" +
                "KNbVfV3kK/CQ0lSW5KsRVeVEbM6UcTy+LlPSoxVjL+wHXvepM58BAAA=";
                
    }
}


namespace Iviz.Msgs.sensor_msgs
{
    public sealed class JoyFeedbackArray : IMessage
    {
        // This message publishes values for multiple feedback at once. 
        public JoyFeedback[] array;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/JoyFeedbackArray";
    
        public IMessage Create() => new JoyFeedbackArray();
    
        public int GetLength()
        {
            int size = 4;
            size += 6 * array.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public JoyFeedbackArray()
        {
            array = new JoyFeedback[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeArray(out array, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeArray(array, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "cde5730a895b1fc4dee6f91b754b213d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1R0UrDMBR9D+wfDux11G4yELEvsiqTDWTqgxMZaXu7BtNkJOm2/r13dZX57n1Jcm5y" +
                "zsm5Q7xWyqMm7+WWsGsyrXxFHnupG15K61A3OqidJpRERSbzL8gAa3KKIJ5s+3BGPz4hnZPtQCT/XAOx" +
                "fHm8hSfjrdvUfuuvLnQHYogZ5Vo6gi0RKkJod92+NywaZcINXt+f080inYErQXyJrt6W94uU0fElev+2" +
                "XqcrRifiDJ+YhTindlBao7K6gDRQBUxTZ+S6zEjm1a8NOh16LxG/To+y5kBHndlSOR+gqcDBNsyVEXMl" +
                "8U/TU27N307vUBUnI3MTOBYV2v7vvc4IpbM14ihGsBhHTKhMrhuv9jw4zEsUtFc5U3qmkXlopNYtMmWk" +
                "a0coHN9z8FWnHBzxzOO75HgXR1NIz2rliMmnJygZd4iJRKmtDNcTVjq7EmIgvgE0QAWKZAIAAA==";
                
    }
}

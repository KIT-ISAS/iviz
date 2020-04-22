
namespace Iviz.Msgs.actionlib_msgs
{
    public sealed class GoalID : IMessage
    {
        // The stamp should store the time at which this goal was requested.
        // It is used by an action server when it tries to preempt all
        // goals that were requested before a certain time
        public time stamp;
        
        // The id provides a way to associate feedback and
        // result message with specific goal requests. The id
        // specified must be unique.
        public string id;
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib_msgs/GoalID";
    
        public IMessage Create() => new GoalID();
    
        public int GetLength()
        {
            int size = 12;
            size += id.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public GoalID()
        {
            id = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out stamp, ref ptr, end);
            BuiltIns.Deserialize(out id, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(stamp, ref ptr, end);
            BuiltIns.Serialize(id, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "302881f31927c1df708a2dbab0e80ee8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACj2PTW7EIAyF90hzhyfNPveYfS/ggBOsJpBiM9HcviaNuuTxfj4/8ZUZarQf0Fz7lvxR" +
                "G8NcNtkZZDizxOyKKNZKG05SNP7prMZpCk+8DP7XlRPmD6iAokktUG5vbp7nAjFYE1ZYxdGY98NA2+bp" +
                "0elyHkvs0//VmHkZLITIzUjKRRQurAs5eHrwS/LK+pbk9eR4nzFCqjUKGWNhTjPFbydLnmisfTPsrEor" +
                "4xTL0IOjLBL/DrwJdLrbPXQbHGrvak6GXsRdU1A/q6zDFR7hFzUY0x9QAQAA";
                
    }
}

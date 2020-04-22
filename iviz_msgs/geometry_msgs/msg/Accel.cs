
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Accel : IMessage
    {
        // This expresses acceleration in free space broken into its linear and angular parts.
        public Vector3 linear;
        public Vector3 angular;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Accel";
    
        public IMessage Create() => new Accel();
    
        public int GetLength() => 48;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            linear.Deserialize(ref ptr, end);
            angular.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            linear.Serialize(ref ptr, end);
            angular.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1Ry0rEQBC8D+QfCvaisERQ8SB4lj0IguJVepNOdtjJTOjpdTd+vZ0HEe8GBjqZquqq" +
                "ygbvB5/Bl144Z86gquLAQupThI9ohBm5p4qxl3Tk8aMmeM0IPjIJKNZ22lOwuSfRXLoPrjTJHRbI7/uC" +
                "c4V7+uencC9vz49oOXWsMnx2uc03y97CbeaYwmNMjmae8DVd/s1YwqA7hWFTDAM6pqiwvCvTiLUXo1o/" +
                "pamycJOEt9YI6mQFxqSm0dHRJDlmHtnU9yZGUKGYw9ztVCKuuGzLLc4HK3ZC+dga0BRajiy+gvjW1zPT" +
                "FnUrmbCk20KbW5x9CLPneZke2EQk6US4LrFrMKQTzmMgGwQ1qTlK2JvFxRftw+g3bXEajU8Sfxt9Tfb7" +
                "rZacqWXrLitTXTrXhET6cI/LOg3r9F24H4Euk0NnAgAA";
                
    }
}

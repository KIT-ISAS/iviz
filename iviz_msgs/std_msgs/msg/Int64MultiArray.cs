
namespace Iviz.Msgs.std_msgs
{
    public sealed class Int64MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public long[] data; // array of data
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Int64MultiArray";
    
        public IMessage Create() => new Int64MultiArray();
    
        public int GetLength()
        {
            int size = 4;
            size += layout.GetLength();
            size += 8 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Int64MultiArray()
        {
            layout = new MultiArrayLayout();
            data = new long[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            layout.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            layout.Serialize(ref ptr, end);
            BuiltIns.Serialize(data, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "54865aa6c65be0448113a2afc6a49270";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR9N+Q/XJKXNkuzfJSyFvIQKOylhUEHZYRQVOs6VmJbQZKbdb9+R/Jn2tcx" +
                "YbB9P885utKIfmQsLFOm9YGEI5cyPZaZU2tjxPuDeNelo5ytFTsmyYkqlFO6oESbaERSx2XOhRPBhkdk" +
                "GeU+Xfh0O42iT8Uoq9/VGpE9cqwSFddFEpLCiToqUoW7ud5sm2is4G3XiEKnJi2KBtHqH69B9Pj0/Y6s" +
                "ky+53dmvHxkNIMRPyNbxhlBxJgxbErTjgo2KK++VVJDLgqfIOuACBY7COBWXyKoIuvcjT4num3iUMkza" +
                "SDYsKTE6J7RmQ7m2DvlOkyqK+v9M9rYEVER7KLZuFWtcdDT6yEDANioh+XIRULzoJLHc26qjkFIVO+KM" +
                "/bYDlPNYCtfpj/JxjHnRxpJNdZlJWj88r3890SvTySjnuABUAvbcnoOwzijJqCAK2UwFyAaeV55XLzZR" +
                "xvMcEZ5O+As12U8Ol7QKYDZ9Dl988kvVYjPfjtW5ZbEd72E5bKORpwAsACGMnNDyKk4FpM3o5nr2+/rb" +
                "jFTuD8NJuRREgA0n6A04Y51pQ3WwRZVTYA/aHRdh70IDdN7MttNMvKIu4A5TVrvUDTuXVX8Ymq8IHXvW" +
                "gBbW5Rhoxh7Nim4X85vZjOii0I7ryFpMUpb2JZQL5aB2wH5ZF5z3EZyUdOmw87QA0KhnPQOA9/x20bgX" +
                "/XK1DsPO1xZc9mxtuSDL5500nDAmCePtbyYvudGnCe3xAb3LvJiEaTn4/6rj9L9eAffNRA4izwVno5YA" +
                "p6X6gug79Yahb4e3OWK1IP4KrHfnQyBd+IOCm4BKXLv2sk2sVPOJ1dfn1EH0Fzlf0RfbBQAA";
                
    }
}

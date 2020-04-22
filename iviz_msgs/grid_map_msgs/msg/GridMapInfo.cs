
namespace Iviz.Msgs.grid_map_msgs
{
    public sealed class GridMapInfo : IMessage
    {
        // Header (time and frame)
        public std_msgs.Header header;
        
        // Resolution of the grid [m/cell].
        public double resolution;
        
        // Length in x-direction [m].
        public double length_x;
        
        // Length in y-direction [m].
        public double length_y;
        
        // Pose of the grid map center in the frame defined in `header` [m].
        public geometry_msgs.Pose pose;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "grid_map_msgs/GridMapInfo";
    
        public IMessage Create() => new GridMapInfo();
    
        public int GetLength()
        {
            int size = 80;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public GridMapInfo()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out resolution, ref ptr, end);
            BuiltIns.Deserialize(out length_x, ref ptr, end);
            BuiltIns.Deserialize(out length_y, ref ptr, end);
            pose.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(resolution, ref ptr, end);
            BuiltIns.Serialize(length_x, ref ptr, end);
            BuiltIns.Serialize(length_y, ref ptr, end);
            pose.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "43ee5430e1c253682111cb6bedac0ef9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UTWsbMRC9C/wfBnyIU2oH2tKDoYdC6Qe04Da5heLI0nhXoJU2ktb29tf3SRuvY3pI" +
                "D23M4tVKb97Mmw9N6TNLzYFmyTRM0mnaBtnwpXjYr8tLiCn94Ohtl4x35LeUaqYqGE23zZVia38uxNZ6" +
                "md6+oTACs9lXdlWqyTg6zLUJrArDbfPIwBbI+nAO75+A9xm+8pHPwmlkS4pdQujgyNtFD2neGsc6b94N" +
                "mu4G1op9wyn06yZW8arwtfibiHf/+DcR364/LSkmPbgaEjyBiOuEvMuA4DlJLZOkrUfmTVVzmFvesYWV" +
                "bFqEX05T33JcwPCmNpHwVOw4SGt76iJAyZPyTdM5o2RiypU9s4cl0iCplSEZ1VkZgPdBG5fhJV+ZHU/k" +
                "+46dYvryYQmMi6xQWATUg0EFltG4CockOuPS61fZQExv9n6OT65QhNE5aiFTDpYPLTokxynjEj5eDOIW" +
                "4EZ2GF50pFnZW+MzXhKcIARuvapphshXfar9UNydDEZuLGdihQyA9SIbXVw+Ys5hL8lJ54/0A+PJx9/Q" +
                "upE3a5rXqJnN6mNXIYEAtsHvjAZ00xcSZQ0akazZBBl6UQasuBTTj6UnUy5fqQjeMkavDAqgaW9SLWIK" +
                "mb1UY220+G8N+ecA5J58jzHOdYICeZz5PBe5c7aBoaSVil/mRsvb+uHcFGy+RnwwR9sFiZVHQ4wA8b2D" +
                "0OAK7wn3fBoRzOQ4P+iIJI2LpWajBMjBgJSozxSL4y10GFf9uPr1XApO+RtljOVCK51l9Tz+/HV/yj4u" +
                "mmYhnhB1XO0h7ze7rc8vMQYAAA==";
                
    }
}

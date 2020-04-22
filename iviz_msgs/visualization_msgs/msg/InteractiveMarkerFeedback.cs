
namespace Iviz.Msgs.visualization_msgs
{
    public sealed class InteractiveMarkerFeedback : IMessage
    {
        // Time/frame info.
        public std_msgs.Header header;
        
        // Identifying string. Must be unique in the topic namespace.
        public string client_id;
        
        // Feedback message sent back from the GUI, e.g.
        // when the status of an interactive marker was modified by the user.
        
        // Specifies which interactive marker and control this message refers to
        public string marker_name;
        public string control_name;
        
        // Type of the event
        // KEEP_ALIVE: sent while dragging to keep up control of the marker
        // MENU_SELECT: a menu entry has been selected
        // BUTTON_CLICK: a button control has been clicked
        // POSE_UPDATE: the pose has been changed using one of the controls
        public const byte KEEP_ALIVE = 0;
        public const byte POSE_UPDATE = 1;
        public const byte MENU_SELECT = 2;
        public const byte BUTTON_CLICK = 3;
        
        public const byte MOUSE_DOWN = 4;
        public const byte MOUSE_UP = 5;
        
        public byte event_type;
        
        // Current pose of the marker
        // Note: Has to be valid for all feedback types.
        public geometry_msgs.Pose pose;
        
        // Contains the ID of the selected menu entry
        // Only valid for MENU_SELECT events.
        public uint menu_entry_id;
        
        // If event_type is BUTTON_CLICK, MOUSE_DOWN, or MOUSE_UP, mouse_point
        // may contain the 3 dimensional position of the event on the
        // control.  If it does, mouse_point_valid will be true.  mouse_point
        // will be relative to the frame listed in the header.
        public geometry_msgs.Point mouse_point;
        public bool mouse_point_valid;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/InteractiveMarkerFeedback";
    
        public IMessage Create() => new InteractiveMarkerFeedback();
    
        public int GetLength()
        {
            int size = 98;
            size += header.GetLength();
            size += client_id.Length;
            size += marker_name.Length;
            size += control_name.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerFeedback()
        {
            header = new std_msgs.Header();
            client_id = "";
            marker_name = "";
            control_name = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out client_id, ref ptr, end);
            BuiltIns.Deserialize(out marker_name, ref ptr, end);
            BuiltIns.Deserialize(out control_name, ref ptr, end);
            BuiltIns.Deserialize(out event_type, ref ptr, end);
            pose.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out menu_entry_id, ref ptr, end);
            mouse_point.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out mouse_point_valid, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(client_id, ref ptr, end);
            BuiltIns.Serialize(marker_name, ref ptr, end);
            BuiltIns.Serialize(control_name, ref ptr, end);
            BuiltIns.Serialize(event_type, ref ptr, end);
            pose.Serialize(ref ptr, end);
            BuiltIns.Serialize(menu_entry_id, ref ptr, end);
            mouse_point.Serialize(ref ptr, end);
            BuiltIns.Serialize(mouse_point_valid, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ab0f1eee058667e28c19ff3ffc3f4b78";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1W227bRhB9J+B/GMAPucBW2jgpCgF5cG0lEeKLCkvtI7Eih9TC5C6zu7Sjfn3PLC+m" +
                "6qLoQxtDgKjlzJk5Zy7rY1rrmt8UTtVM2hR2lnxmlbOjXfxKkmNa5myCLvbalOSDw9eMrlsfaMvUGv21" +
                "FU8KO6ZgG52RAZZvVMazpDOnrNKASHUucB+Z863K7glWXpVMHu8onhTO1hHo02Z5QjwrZ7B/3HGH7oMK" +
                "rSdbkDKIGNipLOgHplq5e2T8qDzVNteF5py2++jTenYziXrXcCZvPPB0tvs7f2VyyqwJzlbw1X5M0HHB" +
                "zoPdwKdzSIXoSLFz7M4Qb71vWFKVJPgBDHH2ZbFYpedXy98W8440UqmYcqfKUjCCpXvmhtpmzKNH6AIC" +
                "4npxs0nvFleLi/WcFDI0LQHJ7WkH9luGVJ4rzgLnsP5ls17f3qQXV8uLL2K+bUOwZgQfXVCf7D56rG7v" +
                "FulmdXm+Ro4SubGeJ4Y7ZUqo23rJ15qRYg/pkxbC/jxhSh/oh/5wgo3TH/vTCSOcvu1Pp5nj+CwZrG83" +
                "ALm8/f0Gp+8ODjcrHL0fDKPmaUAVpBoXrXMieGTzV01vbOA5fQZHFAA9/aAqnVNh0RFVRcXQroLlZ0nJ" +
                "tmYInta+9G9WAiioMQpEUNr4CL+8HAINBZlUC8a3ptpPQk1liLkjlDA5exvd0ujWT9CymNAjNOpUrZOJ" +
                "RickyL06JxgOjEPaWMACpVb7WDZkHNM8oxyrwHhtjaqEkw54POhhVFx+wLmv94wkGR0ot+wPAqQdtUcN" +
                "CaFpcC3D+DCD4aXjSsVBhP4Sq9tGlfYiWp9dt46eyw+kA9StRWM/yyM5Sj78x39HyfXdJ8xxyLtUurV5" +
                "JKsmYJMoJ/UOKldBxQLvdLljd1pByEpWWd2AXHzbNxZ2hiwdfEo22E0V+gM0clEls3WNVZupAJFQpQN/" +
                "eEIkRY1yQWdtpRzsrcu1EfMopqDj4xnL2mTSnHMpoeesFeERSZvMsYpjjc4dWg8OyfH60Z7KuiyxI8fg" +
                "KIoKkix/axz2JJJRfo4YrztyM2DLlkOU3NPLeJbip39FCIIUuLFYwy+R+Wofdl1jYR6cVlvsRABnUACo" +
                "L8TpxasJsqQ9xy1j7ADfIT7F+DewZsQVTqdYbHkVb7i2hIAwbJx90PnTVdJdYmjLrVOYYPHqQibHH2PD" +
                "xjsjVgTfynubaRRAZiDshmsiViP9Pxvy+X6SnjzHkEmdwEANYx2XIYQqHIOJXNgn0mhynPfvuxUgF6N1" +
                "evCdUdLN3WCQ/NqCqDMR98nu+3FEMkfD/GTTJTzdYhiQmPUB46SorAo/vaNv49N+fPrjezF40m+kMZYL" +
                "rXSg6mH+8uvrk/pYNDX+3flnUsPTI+j9CRMLpKUACgAA";
                
    }
}

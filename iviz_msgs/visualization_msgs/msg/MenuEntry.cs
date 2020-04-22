
namespace Iviz.Msgs.visualization_msgs
{
    public sealed class MenuEntry : IMessage
    {
        // MenuEntry message.
        
        // Each InteractiveMarker message has an array of MenuEntry messages.
        // A collection of MenuEntries together describe a
        // menu/submenu/subsubmenu/etc tree, though they are stored in a flat
        // array.  The tree structure is represented by giving each menu entry
        // an ID number and a "parent_id" field.  Top-level entries are the
        // ones with parent_id = 0.  Menu entries are ordered within their
        // level the same way they are ordered in the containing array.  Parent
        // entries must appear before their children.
        
        // Example:
        // - id = 3
        //   parent_id = 0
        //   title = "fun"
        // - id = 2
        //   parent_id = 0
        //   title = "robot"
        // - id = 4
        //   parent_id = 2
        //   title = "pr2"
        // - id = 5
        //   parent_id = 2
        //   title = "turtle"
        //
        // Gives a menu tree like this:
        //  - fun
        //  - robot
        //    - pr2
        //    - turtle
        
        // ID is a number for each menu entry.  Must be unique within the
        // control, and should never be 0.
        public uint id;
        
        // ID of the parent of this menu entry, if it is a submenu.  If this
        // menu entry is a top-level entry, set parent_id to 0.
        public uint parent_id;
        
        // menu / entry title
        public string title;
        
        // Arguments to command indicated by command_type (below)
        public string command;
        
        // Command_type stores the type of response desired when this menu
        // entry is clicked.
        // FEEDBACK: send an InteractiveMarkerFeedback message with menu_entry_id set to this entry's id.
        // ROSRUN: execute "rosrun" with arguments given in the command field (above).
        // ROSLAUNCH: execute "roslaunch" with arguments given in the command field (above).
        public const byte FEEDBACK = 0;
        public const byte ROSRUN = 1;
        public const byte ROSLAUNCH = 2;
        public byte command_type;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/MenuEntry";
    
        public IMessage Create() => new MenuEntry();
    
        public int GetLength()
        {
            int size = 17;
            size += title.Length;
            size += command.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public MenuEntry()
        {
            title = "";
            command = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out id, ref ptr, end);
            BuiltIns.Deserialize(out parent_id, ref ptr, end);
            BuiltIns.Deserialize(out title, ref ptr, end);
            BuiltIns.Deserialize(out command, ref ptr, end);
            BuiltIns.Deserialize(out command_type, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(id, ref ptr, end);
            BuiltIns.Serialize(parent_id, ref ptr, end);
            BuiltIns.Serialize(title, ref ptr, end);
            BuiltIns.Serialize(command, ref ptr, end);
            BuiltIns.Serialize(command_type, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "b90ec63024573de83b57aa93eb39be2d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACp1UTW/bMAy9B9h/INLDVqBfSzdgKJBD16ZbsbUbuvUcyDYTC7ElT5LT5t/vUbITZz0U" +
                "2Mmi/PhIPpI6oDs27cwEt6GavVdLPhmNDmim8pJuTWCn8qDXfKfcil0PoVJ5UoaUc2pDdvGSxJ+A5JJy" +
                "W1UMAmuGKM2egl1yKEFZsM+dzpgUPGogTn2b9d/+yCGn4JiPKJS2XZb48AbhmXywjgvSSIYWlQogiVmd" +
                "EP0uOToB49o8tEBrT44bx55RWkHZhpZ6rc2SWOqVUIQ/biMshm6vybR1hiSVKcA/bhDRhLkuxrTQXBUS" +
                "xDbHFa+5io5SmWSF9EBhDcwnHUraOtKUzuAlSuw5WFew1CFo1AJ/7cCQmGGRVzXTE9TeVt67JDikNkFp" +
                "I8X0AvyMUUHTR6pbH0g1DStHGS+gXIpEeamrAuDU+2dVNxVf4HhMMeVzHGm/iHgTdKgY1njRmvEOPnkN" +
                "7mxmw8DhwwuHyb5D4yYD+MfX4Og1TvDA9RdMLzROzY3jUOmV1K29VAhOJJ8OMa3IBAMh+2OiE2kwERgh" +
                "1Y8FFPx3cqS5ojIGujX6T8uDloJAuuRsdRQnymOWq4IMmiz9wGSMWm3C+QRldtGwNdLcVGuykMAu3BHp" +
                "BemQsuq2BSncJmC3UQmaMGFvXuHvOQykDHaQxfZ61BOddlRR6RH2SsYtGbLvbtkCFmS7UWldS5HaFDpX" +
                "3bZ1l/OwaZjeZVzZp8OepvsnRFdDWNxwUMo2iw0RYDfWeJbHQ8e1KdnspOkmPlacVzpfcSHP0c1sdv35" +
                "8urbBWqWfTYvH7gb5iJT+Wr70MXtFc55ZJxDIhEM9cVo8fKtR78kwMOPXw+P9xfEz5y3gWXOvcNmJBa1" +
                "VQdvDtLd7m3SKT4o9E5lds2HHdv3y8f7q6/7hJVqTV7+F6e09dNWhulZd5HSnr7fmSnudNLdDLs2ejP6" +
                "C8VQ0aM0BgAA";
                
    }
}

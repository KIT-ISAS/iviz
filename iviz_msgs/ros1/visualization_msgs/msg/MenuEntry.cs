/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract]
    public sealed class MenuEntry : IHasSerializer<MenuEntry>, IMessage
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
        [DataMember (Name = "id")] public uint Id;
        // ID of the parent of this menu entry, if it is a submenu.  If this
        // menu entry is a top-level entry, set parent_id to 0.
        [DataMember (Name = "parent_id")] public uint ParentId;
        // menu / entry title
        [DataMember (Name = "title")] public string Title;
        // Arguments to command indicated by command_type (below)
        [DataMember (Name = "command")] public string Command;
        // Command_type stores the type of response desired when this menu
        // entry is clicked.
        // FEEDBACK: send an InteractiveMarkerFeedback message with menu_entry_id set to this entry's id.
        // ROSRUN: execute "rosrun" with arguments given in the command field (above).
        // ROSLAUNCH: execute "roslaunch" with arguments given in the command field (above).
        public const byte FEEDBACK = 0;
        public const byte ROSRUN = 1;
        public const byte ROSLAUNCH = 2;
        [DataMember (Name = "command_type")] public byte CommandType;
    
        public MenuEntry()
        {
            Title = "";
            Command = "";
        }
        
        public MenuEntry(ref ReadBuffer b)
        {
            b.Deserialize(out Id);
            b.Deserialize(out ParentId);
            Title = b.DeserializeString();
            Command = b.DeserializeString();
            b.Deserialize(out CommandType);
        }
        
        public MenuEntry(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out Id);
            b.Deserialize(out ParentId);
            Title = b.DeserializeString();
            b.Align4();
            Command = b.DeserializeString();
            b.Deserialize(out CommandType);
        }
        
        public MenuEntry RosDeserialize(ref ReadBuffer b) => new MenuEntry(ref b);
        
        public MenuEntry RosDeserialize(ref ReadBuffer2 b) => new MenuEntry(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
            b.Serialize(ParentId);
            b.Serialize(Title);
            b.Serialize(Command);
            b.Serialize(CommandType);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Id);
            b.Serialize(ParentId);
            b.Serialize(Title);
            b.Align4();
            b.Serialize(Command);
            b.Serialize(CommandType);
        }
        
        public void RosValidate()
        {
            if (Title is null) BuiltIns.ThrowNullReference(nameof(Title));
            if (Command is null) BuiltIns.ThrowNullReference(nameof(Command));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 17;
                size += WriteBuffer.GetStringSize(Title);
                size += WriteBuffer.GetStringSize(Command);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Id
            size += 4; // ParentId
            size = WriteBuffer2.AddLength(size, Title);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Command);
            size += 1; // CommandType
            return size;
        }
    
        public const string MessageType = "visualization_msgs/MenuEntry";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b90ec63024573de83b57aa93eb39be2d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE51Uy27bMBC8+ysWzqEJkFedFigC+JAmThu0Tou0OQeUtLYIS6RKUk78950lJVtuDgF6" +
                "Epeand3ZBw9ozqadmeA2VLP3asmno9EBzVRe0p0J7FQe9Jrnyq3Y9RAqlSdlSDmnNmQXr0n8KUiuKLdV" +
                "xSCwZojS7CnYJYcSlAX73OmMScGjBuLMt1n/7Y8ccgqO+ZhCadtliQ9vEJ7JB+u4II1kaFGpAJKY1SnR" +
                "75KjEzCuzUMLtPbkuHHsGdIKyja01GttlsSiV0IRiwxhMXR3Q6atMySpTAH+cYOIJjzpYkwLzVUhQWxz" +
                "UvGaq+goyiQrpAcKa2A+61DS1pGmdA6veR+pd7CuYNEhaGiBv3ZgSMywyKua6RnV3irvXRIcpTZBaSNi" +
                "+gL8jFFB00eqWx9INQ0rRxkvbMpUO8pLXRUAp96/qLqp+BLHE4opX+BI+yLiTdChYljjRWvGO/jkLbiz" +
                "mQ0Dhw+vHCb7Do2bDOAf34Kj1zjBA9dfML2ocWpuHIdKr0S39qIQnEg+HWJakQkGQvbHRCelwURoIevG" +
                "AhX8d3KkuVJlDHRr9J+WBy0FgXTJ2eo4TpTHLFcFGTRZ+oHJGLXahIsJZHbRsDXS3KQ1WUhgF+6Y9IJ0" +
                "SFl124IU7hKw26gETZiwN6/w9xwGpQx2kMX2etQTnXVUsdIj7JWMWzJk392yBSzIdkNpXYtIbQqdq27b" +
                "usunsGmYDjOu7PNRT9P9E6LrISxuuI9liDaKALuxxrM8HjquTclmV5pu4qPivNL5igt5jm5ns5vPV9ff" +
                "LqFZ9tm8fuBumYtM5avtQxe3VzifIqOUSAoGfTFavHzn0S8J8PDj18Pj/SXxC+dtYJlz77AZiUVtq4M3" +
                "B+lu9zbVKT4odKgyu+ajju371eP99dd9wkq1Ji//i1Pa+mlbhul5d5HSnr7fmSnudNLdDLs2Gv0F7VRe" +
                "/DMGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MenuEntry> CreateSerializer() => new Serializer();
        public Deserializer<MenuEntry> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MenuEntry>
        {
            public override void RosSerialize(MenuEntry msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MenuEntry msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MenuEntry msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MenuEntry msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MenuEntry msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MenuEntry>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MenuEntry msg) => msg = new MenuEntry(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MenuEntry msg) => msg = new MenuEntry(ref b);
        }
    }
}

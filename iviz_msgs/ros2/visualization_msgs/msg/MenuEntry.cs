/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.VisualizationMsgs
{
    [DataContract]
    public sealed class MenuEntry : IDeserializableRos2<MenuEntry>, IMessageRos2
    {
        // MenuEntry message.
        //
        // Each InteractiveMarker message has an array of MenuEntry messages.
        // A collection of MenuEntries together describe a
        // menu/submenu/subsubmenu/etc tree, though they are stored in a flat
        // array.  The tree structure is represented by giving each menu entry
        // an ID number and a "parent_id" field.  Top-level entries are the
        // ones with parent_id = 0.  Menu entries are ordered within their
        // level the same way they are ordered in the containing array.  Parent
        // entries must appear before their children.
        //
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
    
        /// Constructor for empty message.
        public MenuEntry()
        {
            Title = "";
            Command = "";
        }
        
        /// Constructor with buffer.
        public MenuEntry(ref ReadBuffer2 b)
        {
            b.Deserialize(out Id);
            b.Deserialize(out ParentId);
            b.DeserializeString(out Title);
            b.DeserializeString(out Command);
            b.Deserialize(out CommandType);
        }
        
        public MenuEntry RosDeserialize(ref ReadBuffer2 b) => new MenuEntry(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Id);
            b.Serialize(ParentId);
            b.Serialize(Title);
            b.Serialize(Command);
            b.Serialize(CommandType);
        }
        
        public void RosValidate()
        {
            if (Title is null) BuiltIns.ThrowNullReference();
            if (Command is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Id);
            WriteBuffer2.AddLength(ref c, ParentId);
            WriteBuffer2.AddLength(ref c, Title);
            WriteBuffer2.AddLength(ref c, Command);
            WriteBuffer2.AddLength(ref c, CommandType);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/MenuEntry";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}

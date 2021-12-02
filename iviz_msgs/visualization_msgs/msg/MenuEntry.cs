/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MenuEntry : IDeserializable<MenuEntry>, IMessage
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
    
        /// Constructor for empty message.
        public MenuEntry()
        {
            Title = string.Empty;
            Command = string.Empty;
        }
        
        /// Explicit constructor.
        public MenuEntry(uint Id, uint ParentId, string Title, string Command, byte CommandType)
        {
            this.Id = Id;
            this.ParentId = ParentId;
            this.Title = Title;
            this.Command = Command;
            this.CommandType = CommandType;
        }
        
        /// Constructor with buffer.
        internal MenuEntry(ref Buffer b)
        {
            Id = b.Deserialize<uint>();
            ParentId = b.Deserialize<uint>();
            Title = b.DeserializeString();
            Command = b.DeserializeString();
            CommandType = b.Deserialize<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MenuEntry(ref b);
        
        MenuEntry IDeserializable<MenuEntry>.RosDeserialize(ref Buffer b) => new MenuEntry(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Id);
            b.Serialize(ParentId);
            b.Serialize(Title);
            b.Serialize(Command);
            b.Serialize(CommandType);
        }
        
        public void RosValidate()
        {
            if (Title is null) throw new System.NullReferenceException(nameof(Title));
            if (Command is null) throw new System.NullReferenceException(nameof(Command));
        }
    
        public int RosMessageLength => 17 + BuiltIns.GetStringSize(Title) + BuiltIns.GetStringSize(Command);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "visualization_msgs/MenuEntry";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "b90ec63024573de83b57aa93eb39be2d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACp1UTW/bMAy951cQ6WEt0K+lGzAUyKFr063Y2g3deg5km4mF2JInyWnz7/co2YmzHgrs" +
                "ZFF+fCQfSR3QPZt2ZoLbUM3eqyWfjkYHNFN5SXcmsFN50Gu+V27FrodQqTwpQ8o5tSG7eE3iT0FyRbmt" +
                "KgaBNUOUZk/BLjmUoCzY505nTAoeNRBnvs36b3/kkFNwzMcUStsuS3x4g/BMPljHBWkkQ4tKBZDErE6J" +
                "fpccnYBxbR5aoLUnx41jzyitoGxDS73WZkks9Uoowh+3ERZDdzdk2jpDksoU4B83iGjCXBdjWmiuCgli" +
                "m5OK11xFR6lMskJ6oLAG5rMOJW0daUrn8BIl9hysK1jqEDRqgb92YEjMsMirmukZam8r710SHFKboLSR" +
                "YnoBfsaooOkj1a0PpJqGlaOMF1AuRaK81FUBcOr9i6qbii9xPKGY8gWOtF9EvAk6VAxrvGjNeAefvAV3" +
                "NrNh4PDhlcNk36FxkwH841tw9BoneOD6C6YXGqfmxnGo9Erq1l4qBCeST4eYVmSCgZD9MdGJNJgIjJDq" +
                "xwIK/js50lxRGQPdGv2n5UFLQSBdcrY6jhPlMctVQQZNln5gMkatNuFigjK7aNgaaW6qNVlIYBfumPSC" +
                "dEhZdduCFO4SsNuoBE2YsDev8PccBlIGO8hiez3qic46qqj0CHsl45YM2Xe3bAELst2otK6lSG0Knatu" +
                "27rLedg0TIcZV/b5qKfp/gnR9RAWNxyUss1iQwTYjTWe5fHQcW1KNjtpuomPFeeVzldcyHN0O5vdfL66" +
                "/naJmmWfzesH7pa5yFS+2j50cXuFcx4Z55BIBEN9MVq8fOfRLwnw+OPX49PDJfEL521gmXPvsBmJRW3V" +
                "wZuDdLd7m3SKDwodqsyu+ahj+3719HD9dZ+wUq3Jy//ilLZ+2sowPe8uUtrT9zszxZ1Oupth10ajv+1U" +
                "XvwzBgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

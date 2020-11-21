/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract (Name = "visualization_msgs/MenuEntry")]
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
        [DataMember (Name = "id")] public uint Id { get; set; }
        // ID of the parent of this menu entry, if it is a submenu.  If this
        // menu entry is a top-level entry, set parent_id to 0.
        [DataMember (Name = "parent_id")] public uint ParentId { get; set; }
        // menu / entry title
        [DataMember (Name = "title")] public string Title { get; set; }
        // Arguments to command indicated by command_type (below)
        [DataMember (Name = "command")] public string Command { get; set; }
        // Command_type stores the type of response desired when this menu
        // entry is clicked.
        // FEEDBACK: send an InteractiveMarkerFeedback message with menu_entry_id set to this entry's id.
        // ROSRUN: execute "rosrun" with arguments given in the command field (above).
        // ROSLAUNCH: execute "roslaunch" with arguments given in the command field (above).
        public const byte FEEDBACK = 0;
        public const byte ROSRUN = 1;
        public const byte ROSLAUNCH = 2;
        [DataMember (Name = "command_type")] public byte CommandType { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MenuEntry()
        {
            Title = "";
            Command = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public MenuEntry(uint Id, uint ParentId, string Title, string Command, byte CommandType)
        {
            this.Id = Id;
            this.ParentId = ParentId;
            this.Title = Title;
            this.Command = Command;
            this.CommandType = CommandType;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MenuEntry(ref Buffer b)
        {
            Id = b.Deserialize<uint>();
            ParentId = b.Deserialize<uint>();
            Title = b.DeserializeString();
            Command = b.DeserializeString();
            CommandType = b.Deserialize<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MenuEntry(ref b);
        }
        
        MenuEntry IDeserializable<MenuEntry>.RosDeserialize(ref Buffer b)
        {
            return new MenuEntry(ref b);
        }
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 17;
                size += BuiltIns.UTF8.GetByteCount(Title);
                size += BuiltIns.UTF8.GetByteCount(Command);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/MenuEntry";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b90ec63024573de83b57aa93eb39be2d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACp1UTU/bQBC9R8p/GIVDQeKjDa1UIeVAIbSohVa0nNHansSr2Lvu7jqQf983u7Fx4IDU" +
                "k3fXM2/mvfnYoxs27dwEt6GavVdLPh6PxqM9mqu8pGsT2Kk86DXfKLdi1xlRqTwpQ8o5tSG7eA3jgbNH" +
                "55TbqmIgWDM00+wp2CWHEpgF+9zpjEmJSw2TE99m3bc7csgpOOZDCqVtlyU+vEECTD5YxwVppEOLSgVB" +
                "iYkdE/0pOXrByLV5aGGuPTluHHsGu4KyDS31WpslsVCWWIQ/bhNhDF1fkmnrDHkqUyDCpEFMEx50MaGF" +
                "5qqQKLY5qnjNVfQUcpIXEhQMa3B/1KGk3pNm9B5uosaOh3UFCxWxBh0AaCcQCRtX8qpmeoTmPfvOJ9lD" +
                "bxOUNsKn0+BXDCs4Xay69YFU07BylPEC8qVYlJe6KmDdNcGTqpuKz+R8RDHvUznTLpX0FHSoGNfJojWT" +
                "gcf0bQ9nMxuGPh9f+0xf+DRuOvT49LYHqo+TOMmPr2hqqJ4KHluk0ivRQfvIF8Agsj3FBBMcbgjdnxNo" +
                "kgutguZSXb9A15c9JUUX7dHsrdF/Wx6UWhCkes5Wh7HXPPq8Ksig+FImtMx41GoTTqeg3EfEVEndE/F0" +
                "QxLPIQ9JL0iHlNl2mJDGdTLsJi7ZJqOw080A8BwGwgY7zKR/TwlFrJMtWlR+PMLoSTtub3EtuGULyyBL" +
                "AJzrWuhqU+hcbUdy+/gQNg3TfsaVfTzokbY/E9bF0DKuAqDK1MsdcuDeWONZ1oyOw1WyeRapG4vIPa90" +
                "vuIirq6r+fzyy/nF9zPQl8E3r7fhFXORqXzVb8U45YL6ECFFLdEu2BQvPr7zqF6McPfz99397RnxE+dt" +
                "YBkD7zA7CUb1GmE9IeN+vpNacfXQvsrsmg86uB/n97cX33YRK9WavPw/UKnx516JGcY2vaTMZx8G9xR6" +
                "hsFIT8MCSqH+AV9RpehqBgAA";
                
    }
}

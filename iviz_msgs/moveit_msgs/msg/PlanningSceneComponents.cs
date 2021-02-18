/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlanningSceneComponents")]
    public sealed class PlanningSceneComponents : IDeserializable<PlanningSceneComponents>, IMessage
    {
        // This message defines the components that make up the PlanningScene message.
        // The values can be used as a bitfield to specify which parts of the PlanningScene message
        // are of interest
        // Scene name, model name, model root
        public const uint SCENE_SETTINGS = 1;
        // Joint values of the robot state
        public const uint ROBOT_STATE = 2;
        // Attached objects (including geometry) for the robot state
        public const uint ROBOT_STATE_ATTACHED_OBJECTS = 4;
        // The names of the world objects
        public const uint WORLD_OBJECT_NAMES = 8;
        // The geometry of the world objects
        public const uint WORLD_OBJECT_GEOMETRY = 16;
        // The maintained octomap 
        public const uint OCTOMAP = 32;
        // The maintained list of transforms
        public const uint TRANSFORMS = 64;
        // The allowed collision matrix
        public const uint ALLOWED_COLLISION_MATRIX = 128;
        // The default link padding and link scaling
        public const uint LINK_PADDING_AND_SCALING = 256;
        // The stored object colors
        public const uint OBJECT_COLORS = 512;
        // Bitfield combining options indicated above
        [DataMember (Name = "components")] public uint Components { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlanningSceneComponents()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlanningSceneComponents(uint Components)
        {
            this.Components = Components;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PlanningSceneComponents(ref Buffer b)
        {
            Components = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlanningSceneComponents(ref b);
        }
        
        PlanningSceneComponents IDeserializable<PlanningSceneComponents>.RosDeserialize(ref Buffer b)
        {
            return new PlanningSceneComponents(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Components);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlanningSceneComponents";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "bc993e784476960b918b6e7ad5bb58ce";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE5VTXWvbMBR9D+Q/XOjLBmOQdC178YPieFk6xwqWoN2TkWUl0WpLRlKa9d/3Oh92Bits" +
                "D8a2dM+551wd3QDfaQ+N8l5sFVRqo43yEHYKpG1aa5QJ3a8I0IhnBfv2uLeuhTHabJlURl3Qn8ejG6RT" +
                "8CLqPZJIYaBEiFcVCA8CSh02WtUVBAu+VVJvXuGw03IHrXDYxm7eJ++4hVNdjTZBOeXDeNQtnqqMaNQn" +
                "aGyl6j++nbVYt0fI7RRYnGRJwRLOl9mCRZMTwYPF3YvmswRnSxvABxFUj87pjPKCccKTaHqCkhCE3KE9" +
                "W/5SEh180EbW+wrFw1bZRgX3+hE21v0LaUE4J/H3ZF7Q2UMScxZ9OXXpRtp56tUdrKv7nj3VI83TC7bI" +
                "yCph0deB4CLnPzgWCV0lPP8ZTe4HnkZgJT6daRlsI1ro0TTmdEXW0e30r/W19uHY3gnjcSjN0JfnJGPf" +
                "aL5i0f2Va1HX9oBIaWsEa2uQLjj9u8eRNKWPOLGYpumSLWlWrAjPl0/RZHrlHVMt9nVAAeYZo1Ydz0eY" +
                "6rTgpcD3tudMl9mPYk3mcwxJQbJ5wWKCa4toenc1Bx+s6w++E2jdYOc8QFRFcxbdTc7zmF3yj1er1F3E" +
                "wbYBbXnMdKUlBgNvSmlfhnwMl3A8Gr0B9CtwoK0DAAA=";
                
    }
}

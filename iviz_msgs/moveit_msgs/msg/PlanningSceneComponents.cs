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
        [DataMember (Name = "components")] public uint Components;
    
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
        internal PlanningSceneComponents(ref Buffer b)
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
                "H4sIAAAAAAAAE41TwY6bMBS88xVP2ksrVZWS7a564eAQmmZLcIQtbXtCxpjEXbCR7Wy6f99HNqaq1Ep7" +
                "QID9Zt7M8/gG+FF7GJT34qCgVZ02ykM4KpB2GK1RJky/IsAgnhScxsvevhfGaHNgUhkV0R+TG2RT8Cz6" +
                "E3JIYaBBhFctCA8CGh06rfoWggU/Kqm7FzgftTzCKBx2sd3/uZFaODWVaBOUUz4kuPRaYsSgPsBgW9X/" +
                "9e2sDckJ62+XwLK8zGuWc74tNyxdTOgHi3tR7bW5s40N4IMIKkIruqK8ZpzwPF1OOBKCkEd0ZZufSqLw" +
                "d9rI/tSiZjgoO6jgXt5DZ90bGGvCOcm+5uuarh7yjLP0U3Id42Rl1nW2rp8bRp5HWhURWJdkl7P0c0RH" +
                "IW8n2OR0l/PqR7q4jySDwDp8Jq8y2EGMEKE043RH9unt8h/Fvfbh0tgJ43EQw9yRV6RkX2i1Y+n97FT0" +
                "vT0jTNoekdoa5ApO/4ogUhT0EUeU0aLYsi0t6x3h1fZ7uljOfjG54tQHbG2eME/t5TSEaV8XvBT4PkTC" +
                "Ylt+q/dkvcYw1KRc1ywjuLZJl3ezdx+sm894kmbd7OI6MdRDK5beLS4zWMV448Vp9JRgsGNANx4z22qJ" +
                "AcCL0NjnOQd/bliS/AYseISiiQMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

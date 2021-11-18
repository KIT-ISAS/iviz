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
                "H4sIAAAAAAAACo1TwYrbMBS8G/oPD/bSQinE21168UFx3DRbxwqWYNuTkWUlUdeWjKRsun/f5zRyKbSw" +
                "B2NbejNv5ml0A/yoPQzKe3FQ0Km9NspDOCqQdhitUSZMvyLAIJ4UnMbL3q4XxmhzYFIZFdEfkhtkU/As" +
                "+hNySGGgRYRXHQgPAlod9lr1HQQLflRS71/gfNTyCKNw2MXu/8+N1MKpqUSboJzyIcGl3yVGDOo9DLZT" +
                "/V/fztqQnLD+NgWWF1XRsILzTbVm2WJCP1jci2qvzZ1tbQAfRFARWtMl5Q3jhBdZOuFICEIe0ZVtfyiJ" +
                "wt9qI/tTh5rhoOyggnt5B3vrXsHYEM5J/qVYNXT5UOScZR+T6xgnK7Ous3X93DDyPNK6jMCmItuCZZ8i" +
                "Ogp5PcG6oNuC19+zxX0kGQTW4TN5lcEOYoQIpTmnW7LLbtN/FPfah0tjJ4zHQQxzR16Tin2m9ZZl97NT" +
                "0ff2jDBpe0Rqa5ArOP0zgkhZ0kccUU7LcsM2tGq2hNebb9kinf1icsWpD9jaPGGeustpCDNpwQUvBb4P" +
                "kbDcVF+bHVmtMAwNqVYNywmurbP0bvbug3XzGU/SrJtdXCeGemjNsrvFZQbLGG+8OK2eEgx2DOjGY2Y7" +
                "LTEAeBFa+zzn4M8NS94kvwDPeOxPigMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

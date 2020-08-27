using System.Runtime.Serialization;
using UnityEngine;

namespace Iviz.App.Gui
{
    [DataContract]
    public class IconImageData
    {
        [DataMember] public string IconResource { get; set; }
        [DataMember] public int Width { get; set; }
        [DataMember] public int Height { get; set; }
        [DataMember] public byte[] Data { get; set; }
    }
    
    [DataContract]
    public class ButtonData
    {
        [DataMember] public IconImageData IconData { get; set; }
        [DataMember] public string Text { get; set; }
    }

    [DataContract]
    public class PanelData
    {
        [DataMember] public float Width { get; set; }
        [DataMember] public float Height { get; set; }
        [DataMember] public string FrameId { get; set; }

        [DataMember] public string Title { get; set; }
        [DataMember] public string Text { get; set; }

        [DataMember] public ButtonData[] Buttons { get; set; }
    } 
    
    public class Panel : MonoBehaviour
    {
        
    }
}
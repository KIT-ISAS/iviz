using System;
using System.Runtime.Serialization;
using Iviz.Controllers;
using UnityEngine;

namespace Iviz.App.Gui
{
    [DataContract]
    public class IconImageData
    {
        [DataMember] public string IconResource { get; set; } = "";
        [DataMember] public int Width { get; set; }
        [DataMember] public int Height { get; set; }
        [DataMember] public byte[] Data { get; set; } = Array.Empty<byte>();
    }
    
    [DataContract]
    public class ButtonData
    {
        [DataMember] public IconImageData IconData { get; set; }
        [DataMember] public string Text { get; set; } = "";
    }

    [DataContract]
    public class PanelData
    {
        [DataMember] public float Width { get; set; }
        [DataMember] public float Height { get; set; }
        [DataMember] public string AttachToFrameId { get; set; } = "";
        [DataMember] public string Title { get; set; } = "";
        [DataMember] public string Text { get; set; } = "";
        [DataMember] public ButtonData[] Buttons { get; set; } = Array.Empty<ButtonData>();
    } 
    
    public class GuiPanel : MonoBehaviour
    {
        [SerializeField] TextMesh titleText = null;
        [SerializeField] TextMesh contentText = null;
        TfFrame attachedFrame;
        SimpleDisplayNode node;

        void Awake()
        {
            node = SimpleDisplayNode.Instantiate("GuiPanel Node");

            PanelData panelData = new PanelData
            {
                Title =  "Part Validation",
                Text = "Is this part correct?"
            };
            
            Parse(panelData);
        }

        public void Parse(PanelData panelData)
        {
            titleText.text = panelData.Title ?? "";
            contentText.text = panelData.Text ?? "";

            attachedFrame?.RemoveListener(node);
            if (!string.IsNullOrEmpty(panelData.AttachToFrameId))
            {
                attachedFrame = TfListener.GetOrCreateFrame(panelData.AttachToFrameId, node);
            }
            
            
        }

        void OnDestroy()
        {
            attachedFrame?.RemoveListener(node);
            Destroy(node.gameObject);
        }
    }
}
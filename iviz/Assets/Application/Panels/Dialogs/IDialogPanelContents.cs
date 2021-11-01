using Iviz.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public interface IDialogPanelContents
    {
        bool Active { set; }
        void ClearSubscribers();
    }

    public abstract class PanelContents : MonoBehaviour, IDialogPanelContents
    {
        public bool Active
        {
            set => gameObject.SetActive(value);
        }
        
        public abstract void ClearSubscribers();
    }

    public abstract class DetachablePanelContents : PanelContents
    {
        [SerializeField] protected DialogScalerWidget scalerWidget = null;
        Image panelImage;

        Image PanelImage => panelImage != null ? panelImage : (panelImage = GetComponent<Image>());

        public bool Detached
        {
            set
            {
                scalerWidget.gameObject.SetActive(value);
                PanelImage.color = value ? Resource.Colors.DetachedPanelColor : Resource.Colors.AttachedPanelColor;
            }
        }
    }
    
}
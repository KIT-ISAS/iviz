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
        [SerializeField] DialogScalerWidget scalerWidget = null;
        Image image;

        Image Image => image != null ? image : (image = GetComponent<Image>());

        public bool Detached
        {
            set
            {
                scalerWidget.gameObject.SetActive(value);
                Image.color = value ? Resource.Colors.DetachedPanelColor : Resource.Colors.AttachedPanelColor;
            }
        }
    }
    
}
using UnityEngine;

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
}
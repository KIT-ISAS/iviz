using UnityEngine;

namespace Iviz.App
{
    public interface IDialogPanel
    {
        bool Active { set; }
        void ClearSubscribers();
    }

    public abstract class DialogPanel : MonoBehaviour, IDialogPanel
    {
        public bool Active
        {
            set => gameObject.SetActive(value);
        }
        
        public abstract void ClearSubscribers();
    }
}
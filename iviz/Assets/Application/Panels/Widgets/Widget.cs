using UnityEngine;

namespace Iviz.App
{
    public abstract class Widget : MonoBehaviour
    {
        public string Name => gameObject.name;

        public abstract void ClearSubscribers();
    }
}
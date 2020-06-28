using UnityEngine;

namespace Iviz.App
{
    public sealed class AddTopicDialogContents : ItemListDialogContents
    {
        [SerializeField] ToggleWidget selectAll = null;

        public ToggleWidget ShowAll => selectAll;

        public override void ClearSubscribers()
        {
            base.ClearSubscribers();
            selectAll.ClearSubscribers();
        }
    }
}
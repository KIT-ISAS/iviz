using UnityEngine;

namespace Iviz.App
{
    public sealed class AddTopicDialogContents : ItemListDialogContents
    {
        [SerializeField] ToggleWidget selectAll = null;
        [SerializeField] ToggleWidget sortByType = null;

        public ToggleWidget ShowAll => selectAll;
        public ToggleWidget SortByType => sortByType;

        public override void ClearSubscribers()
        {
            base.ClearSubscribers();
            selectAll.ClearSubscribers();
        }
    }
}
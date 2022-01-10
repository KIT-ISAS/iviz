using UnityEngine;

namespace Iviz.App
{
    public sealed class AddTopicDialogPanel : ItemListDialogPanel
    {
        [SerializeField] ToggleWidget selectAll;
        [SerializeField] ToggleWidget sortByType;

        public ToggleWidget ShowAll => selectAll;
        public ToggleWidget SortByType => sortByType;

        public override void ClearSubscribers()
        {
            base.ClearSubscribers();
            selectAll.ClearSubscribers();
        }
    }
}
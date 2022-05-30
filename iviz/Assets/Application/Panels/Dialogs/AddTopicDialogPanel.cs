#nullable enable

using Iviz.Core;
using UnityEngine;

namespace Iviz.App
{
    public sealed class AddTopicDialogPanel : ItemListDialogPanel
    {
        [SerializeField] ToggleWidget? selectAll;
        [SerializeField] ToggleWidget? sortByType;

        public ToggleWidget ShowAll => selectAll.AssertNotNull(nameof(selectAll));
        public ToggleWidget SortByType => sortByType.AssertNotNull(nameof(sortByType));

        public override void ClearSubscribers()
        {
            base.ClearSubscribers();
            ShowAll.ClearSubscribers();
            SortByType.ClearSubscribers();
        }
    }
}
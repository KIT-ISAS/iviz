using UnityEngine.UI;

namespace Iviz.App
{
    public class HeadTitleWidget : Widget
    {
        public Text label;

        public string Label
        {
            get => label.text;
            set
            {
                label.text = value;
                name = "HeadTitle:" + value;
            }
        }

        public HeadTitleWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }

        public override void ClearSubscribers() { }

    }
}
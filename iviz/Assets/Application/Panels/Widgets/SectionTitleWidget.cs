using UnityEngine.UI;

namespace Iviz.App
{
    public class SectionTitleWidget : Widget
    {
        public Text label;

        public string Label
        {
            get => label.text;
            set
            {
                label.text = value;
                name = "SectionTitle:" + value;
            }
        }

        public override void ClearSubscribers() { }

        public SectionTitleWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }
    }
}
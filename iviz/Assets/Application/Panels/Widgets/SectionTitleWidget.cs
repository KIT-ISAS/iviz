using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class SectionTitleWidget : MonoBehaviour, IWidget
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

        public void ClearSubscribers() { }

        public SectionTitleWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }
    }
}
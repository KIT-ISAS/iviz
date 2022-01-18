using UnityEngine;

namespace Iviz.Displays.ARDialogs
{
    public interface IWidget : IDisplay
    {
        bool Interactable { set; }
    }
    
    public interface IWidgetWithColor : IWidget
    {
        Color Color { set; }
        Color SecondaryColor { set; }
    }

    public interface IWidgetWithCaption : IWidget
    {
        string Caption { set; }
    }    
}
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class AnchorToggleButton : MonoBehaviour
    {
        static readonly Color EnabledColor = new Color(0, 0, 0.4f, 0.75f); 
        static readonly Color DisabledColor = new Color(0.75f, 0.75f, 0.75f, 0.25f); 

        [SerializeField] bool state;
        Image image;

        public event Action Clicked;
        
        public bool State
        {
            get => state;
            set
            {
                state = value;
                if (image != null)
                {
                    image.color = state ? EnabledColor : DisabledColor;
                }
            }
        } 
        
        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        } 
        
        void Awake()
        {
            image = transform.GetChild(0).GetComponent<Image>();
            State = State;
            GetComponent<Button>().onClick.AddListener(() =>
            {
                State = !State;
                Clicked?.Invoke();
            });
        }
    }
}
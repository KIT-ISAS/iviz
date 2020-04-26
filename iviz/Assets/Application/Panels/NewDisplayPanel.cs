using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Iviz.App
{

    public class NewDisplayPanel : MonoBehaviour
    {
        static readonly List<Tuple<string, Resource.Module>> Displays = new List<Tuple<string, Resource.Module>>()
        {
            Tuple.Create("<b>Robot</b>\nA robot object", Resource.Module.Robot),
            Tuple.Create("<b>Grid</b>\nA reference plane", Resource.Module.Grid),
            Tuple.Create("<b>DepthProjector</b>\nPoint cloud generator for depth images", Resource.Module.DepthImageProjector),
        };

        public DisplayListPanel displaysPanel;
        public GameObject contentObject;
        public TrashButtonWidget closeButton;
        readonly List<GameObject> topicButtons = new List<GameObject>();
        const float yOffset = 5;
        Canvas parentCanvas;
        float buttonHeight;

        public bool Active
        {
            get => parentCanvas.gameObject.activeSelf;
            set
            {
                parentCanvas.gameObject.SetActive(value);
                parentCanvas.enabled = value;
            }
        }

        void Start()
        {
            Resource.Widgets.Initialize();

            parentCanvas = GetComponentInParent<Canvas>();
            buttonHeight = (Resource.Widgets.TopicsButton.GameObject.transform as RectTransform).rect.height;
            closeButton.Clicked += OnCloseClick;
            Active = false;

            UpdateDisplays();
        }

        void UpdateDisplays()
        {
            int i = 0;
            foreach (var entry in Displays)
            {
                float y = yOffset + i * (yOffset + buttonHeight);

                GameObject buttonObject = ResourcePool.GetOrCreate(Resource.Widgets.TopicsButton, contentObject.transform, false);
                (buttonObject.transform as RectTransform).anchoredPosition = new Vector2(0, -y);
                buttonObject.GetComponentInChildren<Text>().text = entry.Item1; 
                buttonObject.SetActive(true);
                
                topicButtons.Add(buttonObject);
                buttonObject.GetComponent<Button>().onClick.AddListener(() => OnDisplayClick(entry.Item2));
                i++;
            }
            (contentObject.transform as RectTransform).sizeDelta = new Vector2(0, 2*yOffset + (i+1) * buttonHeight);
        }

        void OnCloseClick()
        {
            Active = false;
        }

        void OnDisplayClick(Resource.Module module)
        {
            DisplayData displayData = displaysPanel.CreateDisplay(module);
            displayData.Start();
            displayData.Select();
            Active = false;
        }
    }
}
 
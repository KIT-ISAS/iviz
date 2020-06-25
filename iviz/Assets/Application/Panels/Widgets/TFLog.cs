using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using Iviz.App.Listeners;
using UnityEngine.EventSystems;
using TMPro;

namespace Iviz.App
{
    public class TFLog : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text tfText = null;
        [SerializeField] Text tfName = null;
        [SerializeField] TMP_Text cmdText = null;
        [SerializeField] GameObject content = null;

        [SerializeField] TFLink tfLink = null;
        [SerializeField] TFLink cmdLink = null;

        TFFrame selectedFrame;
        public TFFrame SelectedFrame
        {
            get => selectedFrame;
            set
            {
                selectedFrame = value;
                if (selectedFrame == null)
                {
                    tfName.text = "<color=grey>[ ]</color>";
                    cmdText.text = "";
                }
                else
                {
                    tfName.text = "[ >" + value.Id + "]";
                    cmdText.text =
                        "<link=\"1\">[GoTo]</link>    " +
                        "<link=\"2\">[Trace]</link>    " +
                        "<link=\"3\">[Lock Pivot]</link>    " +
                        "<link=\"4\">[Lock FPV]</link>";
                }
            }
        }

        void Awake()
        {
            tfLink.LinkClicked += OnTfLinkClicked;
            cmdLink.LinkClicked += OnCmdLinkClicked;
        }

        void OnCmdLinkClicked(string cmd)
        {
            Debug.Log(cmd);
            if (SelectedFrame == null)
            {
                return;
            }
            switch(cmd)
            {
                case "2":
                    SelectedFrame.TrailVisible = !SelectedFrame.TrailVisible;
                    Debug.Log(SelectedFrame.TrailVisible);
                    break;
            }
        }

        void OnTfLinkClicked(string frameId)
        {
            if (TFListener.TryGetFrame(frameId, out TFFrame frame))
            {
                SelectedFrame = frame;
            }
            else
            {
                SelectedFrame = null;
            }

        }

        class TFNode
        {
            public string Name { get; }
            public List<TFNode> Children { get; }
            public Pose Pose { get; }

            public TFNode(TFFrame frame)
            {
                Name = frame.Id;
                Children = new List<TFNode>();
                Pose = frame.WorldPose;

                var childrenList = frame.Children;
                foreach (TFFrame child in childrenList.Values)
                {
                    //Debug.Log(Name + " -> " + child.Id);
                    Children.Add(new TFNode(child));
                }
                Children.Sort((x, y) => string.CompareOrdinal(x.Name, y.Name));
            }

            void Write(StringBuilder str, int level)
            {
                string tabs = new string(' ', level * 4);

                Vector3 position = Pose.position.Unity2Ros();
                string x = position.x.ToString("#,0.#", UnityUtils.Culture);
                string y = position.y.ToString("#,0.#", UnityUtils.Culture);
                string z = position.z.ToString("#,0.#", UnityUtils.Culture);

                str.Append(tabs).AppendLine($"<link={Name}><b>>{Name}</b> [{x}, {y}, {z}]</link>");

                foreach (TFNode node in Children)
                {
                    node.Write(str, level + 1);
                }
            }

            public void Write(StringBuilder str)
            {
                foreach (TFNode node in Children)
                {
                    node.Write(str, 0);
                }
            }
        }

        public void Flush()
        {

            TFNode root = new TFNode(TFListener.RootFrame);

            StringBuilder str = new StringBuilder();
            root.Write(str);

            tfText.text = str.ToString();

            RectTransform ctransform = (RectTransform)content.transform;
            ctransform.sizeDelta = new Vector2(tfText.preferredWidth + 10, tfText.preferredHeight + 10);
        }

        public void ClearSubscribers()
        {
            //Active = false;
        }

        public void OnLinkClick()
        {

        }
    }
}
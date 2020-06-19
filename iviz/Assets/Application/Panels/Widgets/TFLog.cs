using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using Iviz.App.Listeners;

namespace Iviz.App
{
    public class TFLog : MonoBehaviour, IWidget
    {
        [SerializeField] Text text = null;

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
                    Children.Add(new TFNode(child));
                }
                Children.Sort((x, y) => string.CompareOrdinal(x.Name, y.Name));
            }

            public void Write(StringBuilder str, int level)
            {
                string tabs = new string(' ', level * 4);

                Vector3 position = Pose.position.Unity2Ros();
                string x = position.x.ToString("#,0.#", UnityUtils.Culture);
                string y = position.y.ToString("#,0.#", UnityUtils.Culture);
                string z = position.z.ToString("#,0.#", UnityUtils.Culture);

                str.Append(tabs).AppendLine($"<b>⮑{Name}</b> [{x}, {y}, {z}]");

                foreach(TFNode node in Children)
                {
                    node.Write(str, level + 1);
                }
            }
        }

        bool active = true;
        public bool Active
        {
            get => active;
            set
            {
                active = value;
                if (value)
                {
                    Flush();
                }
            }
        }

        public void Flush()
        {
            if (!Active)
            {
                return;
            }

            TFNode root = new TFNode(TFListener.BaseFrame);

            StringBuilder str = new StringBuilder();
            root.Write(str, 0);

            text.text = str.ToString();

            //RectTransform ctransform = (RectTransform)content.transform;
            //ctransform.sizeDelta = new Vector2(0, text.preferredHeight);
        }

        public void ClearSubscribers()
        {
            Active = false;
        }
    }
}
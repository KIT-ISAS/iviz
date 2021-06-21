using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    internal sealed class ModuleListButtons
    {
        const float YOffset = 2;

        [NotNull, ItemNotNull] readonly List<MovableButtonWidget> buttons = new List<MovableButtonWidget>();
        [NotNull] readonly RectTransform contentObjectTransform;
        readonly float buttonHeight;

        public ModuleListButtons([NotNull] GameObject contentObject)
        {
            buttonHeight = Resource.Widgets.DisplayButton.Object.GetComponent<RectTransform>().rect.height;
            contentObjectTransform = (RectTransform) contentObject.transform;
        }

        public void CreateButtonForModule([NotNull] ModuleData moduleData)
        {
            int size = buttons.Count;
            float y = 2 * YOffset + size * (buttonHeight + YOffset);

            MovableButtonWidget button = ResourcePool
                .Rent(Resource.Widgets.DisplayButton, contentObjectTransform, false)
                .GetComponentInChildren<MovableButtonWidget>();

            button.Transform.anchoredPosition = new Vector2(0, -y);
            button.Text.text = moduleData.ButtonText;
            button.Name = $"Button:{moduleData.ModuleType}";
            button.Visible = true;
            
            button.Clicked += moduleData.ToggleShowPanel;
            button.RevealedLeft += moduleData.ToggleVisible;
            button.RevealedRight += moduleData.Close;

            buttons.Add(button);

            contentObjectTransform.sizeDelta = new Vector2(0, y + buttonHeight + YOffset);
        }

        public void RemoveButton(int index)
        {
            MovableButtonWidget button = buttons[index];
            button.ClearSubscribers();
            ResourcePool.Return(Resource.Widgets.DisplayButton, button.GameObject);

            buttons.RemoveAt(index);

            int i;
            for (i = index; i < buttons.Count; i++)
            {
                float y = 2 * YOffset + i * (buttonHeight + YOffset);
                buttons[i].Transform.anchoredPosition = new Vector3(0, -y);
            }

            contentObjectTransform.sizeDelta = new Vector2(0, 2 * YOffset + i * (buttonHeight + YOffset));
        }

        public void UpdateButton(int index, [NotNull] string content)
        {
            if (index < 0 || index >= buttons.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var text = buttons[index].Text;
            text.text = content;
            int lineBreaks = content.Count(x => x == '\n');
            switch (lineBreaks)
            {
                case 2:
                    text.fontSize = 11;
                    break;
                case 3:
                    text.fontSize = 10;
                    break;
                default:
                    text.fontSize = 12;
                    break;
            }
        }
    }
}
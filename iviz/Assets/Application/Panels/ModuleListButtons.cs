#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    internal sealed class ModuleListButtons
    {
        const float YOffset = 2;

        readonly List<DraggableButtonWidget> buttons = new();
        readonly RectTransform contentObjectTransform;
        readonly float buttonHeight;

        public ModuleListButtons(GameObject contentObject)
        {
            buttonHeight = Resource.Widgets.DisplayButton.Object.GetComponent<RectTransform>().rect.height;
            contentObjectTransform = (RectTransform) contentObject.transform;
        }

        public void CreateButtonForModule(ModuleData moduleData)
        {
            int size = buttons.Count;
            float y = 2 * YOffset + size * (buttonHeight + YOffset);

            var button = ResourcePool
                .Rent(Resource.Widgets.DisplayButton, contentObjectTransform, false)
                .GetComponentInChildren<DraggableButtonWidget>();

            button.Transform.anchoredPosition = new Vector2(0, -y);
            button.ButtonText.text = moduleData.ButtonText;
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
            var button = buttons[index];
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

        public void UpdateButton(int index, string content)
        {
            if (index < 0 || index >= buttons.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var text = buttons[index].ButtonText;
            text.text = content;
            int lineBreaks = content.Count(x => x == '\n');
            text.fontSize = lineBreaks switch
            {
                2 => 11,
                3 => 10,
                _ => 12
            };
        }
    }
}
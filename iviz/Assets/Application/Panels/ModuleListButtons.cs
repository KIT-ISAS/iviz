#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using Iviz.Tools;
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
            buttonHeight = Resource.Widgets.DraggableDisplayButton.Object.GetComponent<RectTransform>().rect.height;
            contentObjectTransform = (RectTransform)contentObject.transform;
        }

        public void CreateButtonForModule(ModuleData moduleData)
        {
            int size = buttons.Count;
            float y = 2 * YOffset + size * (buttonHeight + YOffset);

            var button = ResourcePool
                .Rent(Resource.Widgets.DraggableDisplayButton, contentObjectTransform, false)
                .GetComponentInChildren<DraggableButtonWidget>();

            button.Transform.anchoredPosition = new Vector2(0, -y);
            button.ButtonText = moduleData.ModuleListButtonText;
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
            ResourcePool.Return(Resource.Widgets.DraggableDisplayButton, button.GameObject);

            buttons.RemoveAt(index);

            foreach (int i in index..buttons.Count)
            {
                float y = 2 * YOffset + i * (buttonHeight + YOffset);
                buttons[i].Transform.anchoredPosition = new Vector2(0, -y);
            }

            contentObjectTransform.sizeDelta = new Vector2(0, 2 * YOffset + buttons.Count * (buttonHeight + YOffset));
        }
        
        public void RemoveAllButtons()
        {
            foreach (var button in buttons)
            {
                button.ClearSubscribers();
                ResourcePool.Return(Resource.Widgets.DraggableDisplayButton, button.GameObject);
            }

            buttons.Clear();
            contentObjectTransform.sizeDelta = new Vector2(0, 2 * YOffset);
        }        

        public void UpdateButton(int index, string content)
        {
            if (index < 0 || index >= buttons.Count)
            {
                ThrowHelper.ThrowArgumentOutOfRange(nameof(index));
            }

            ThrowHelper.ThrowIfNull(content, nameof(content));

            buttons[index].ButtonText = content;
            int lineBreaks = content.Count(x => x == '\n');
            buttons[index].ButtonFontSize = lineBreaks switch
            {
                2 => 11,
                3 => 10,
                _ => 12
            };
        }
    }
}
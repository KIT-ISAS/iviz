﻿#nullable enable

using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;
using Iviz.Resources;
using Iviz.Msgs;
using Iviz.Ros;
using Iviz.Tools;
using TMPro;

namespace Iviz.App
{
    public sealed class SenderWidget : MonoBehaviour, IWidget
    {
        const int MaxTopicLength = 200;

        [SerializeField] TMP_Text? text = null;
        [SerializeField] Image? panel = null;

        ISender? sender;

        TMP_Text Text => text.AssertNotNull(nameof(text));
        Image Panel => panel.AssertNotNull(nameof(panel));
        string? Topic => Sender?.Topic;

        ISender? Sender
        {
            get => sender;
            set
            {
                if (sender == null && value != null)
                {
                    GameThread.EverySecond += UpdateStats;
                }
                else if (sender != null && value == null)
                {
                    GameThread.EverySecond -= UpdateStats;
                }

                sender = value;
                UpdateStats();
            }
        }

        int NumSubscribers =>
            (!ConnectionManager.IsConnected || Sender == null) ? -1 : Sender.NumSubscribers;

        int MessagesPerSecond => Sender?.Stats.MessagesPerSecond ?? 0;
        long BytesPerSecond => Sender?.Stats.BytesPerSecond ?? 0;

        public void Set(ISender? newSender)
        {
            Sender = newSender;
            if (newSender == null)
            {
                Text.text = "<i>Empty</i>\n" +
                            "<b>(?)</b>";
            }
        }

        public void Set<T>(Sender<T>? newSender) where T : IMessage, new()
        {
            Sender = newSender;
            if (newSender == null)
            {
                Text.text = "<i>Empty</i>\n" +
                            $"<b>{BuiltIns.GetMessageType(typeof(T))}</b>";
            }
        }

        void UpdateStats()
        {
            if (sender == null)
            {
                Text.text = "[No Topic Set]\n" +
                            "<b>Off</b>";
                Panel.color = Resource.Colors.DisabledPanel;
                return;
            }

            var description = BuilderPool.Rent();
            try
            {
                description
                    .Append(Resource.Font.Split(Topic ?? "", MaxTopicLength))
                    .Append("\n<b>");

                sender.WriteDescriptionTo(description);

                description.Append(" | ")
                    .Append(MessagesPerSecond)
                    .Append(" Hz | ");

                RosUtils.WriteFormattedBandwidth(description, BytesPerSecond);

                description.Append("</b>");

                Text.SetText(description);
            }
            finally
            {
                BuilderPool.Return(description);
            }

            Panel.color = Resource.Colors.EnabledSender;
        }

        public void ClearSubscribers()
        {
            Sender = null;
        }
    }
}
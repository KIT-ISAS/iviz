#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.Resources
{
    public enum AudioClipType
    {
        Click,
        ClickWrong,
        ClickDoubleWrong,
        ClickMelodic
    }

    public sealed class AudioType
    {
        public AudioClip GetAudioClip(AudioClipType clip)
        {
            var assetHolder = Resource.Extras.AudioAssetHolder;
            var assetClip = clip switch
            {
                AudioClipType.Click => assetHolder.Click.AssertNotNull(nameof(assetHolder.Click)),
                AudioClipType.ClickWrong => assetHolder.ClickWrong.AssertNotNull(nameof(assetHolder.ClickWrong)),
                AudioClipType.ClickDoubleWrong =>
                    assetHolder.ClickDoubleWrong.AssertNotNull(nameof(assetHolder.ClickDoubleWrong)),
                AudioClipType.ClickMelodic => assetHolder.ClickMelodic.AssertNotNull(nameof(assetHolder.ClickMelodic)),
                _ => throw new ArgumentOutOfRangeException(nameof(clip))
            };

            return assetClip;
        }
        
        public void PlayAt(in Vector3 position, AudioClipType clip)
        {
            AudioSource.PlayClipAtPoint(GetAudioClip(clip), position);
        }
    }
}
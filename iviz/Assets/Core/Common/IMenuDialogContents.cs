using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public interface IMenuDialogContents
    {
        void Set([NotNull] MenuEntryList menu, Vector3 positionHint, [NotNull] Action<uint> callback);
    }
}
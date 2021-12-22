using System;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public interface IMenuDialogContents
    {
        void Set([NotNull] MenuEntryDescription[] menuEntries, [NotNull] Action<uint> callback);
    }
}
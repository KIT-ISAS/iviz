using System;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers.Markers
{
    public interface IMenuDialogContents
    {
        void Set([NotNull] MenuEntryDescription[] menuEntries, [NotNull] Action<uint> callback);
    }
}
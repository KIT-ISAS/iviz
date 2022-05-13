using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// This module is attached to the "iviz" object on the scene and exists only
    /// to call Dispose() on <see cref="ModuleListPanel"/> to finalize gracefully.
    /// </summary>
    public class ModuleDisposer : MonoBehaviour
    {
        void OnApplicationQuit()
        {
            ModuleListPanel.Instance.Dispose();
        }
    }
}
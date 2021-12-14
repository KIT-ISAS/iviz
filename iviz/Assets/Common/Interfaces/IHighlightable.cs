
using UnityEngine;

namespace Iviz.Common
{
    public interface IHighlightable
    {
        void Highlight(in Vector3 hitPoint);
        bool IsAlive { get; }
    }
}
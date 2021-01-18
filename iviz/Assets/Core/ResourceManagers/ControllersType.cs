using Iviz.Displays;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class ControllersType
    {
        public Info<GameObject> AR { get; }

        public ControllersType()
        {
            var appAssetHolder = UnityEngine.Resources.Load<GameObject>("App Asset Holder").GetComponent<AppAssetHolder>();
            AR = new Info<GameObject>(appAssetHolder.ARPrefab);
        }
    }
}
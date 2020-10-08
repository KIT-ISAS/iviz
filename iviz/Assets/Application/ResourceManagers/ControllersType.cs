using UnityEngine;

namespace Iviz.Resources
{
    public sealed class ControllersType
    {
        public Info<GameObject> AR { get; }

        public ControllersType()
        {
            AR = new Info<GameObject>("Controllers/AR");
        }
    }
}
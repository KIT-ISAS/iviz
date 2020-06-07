using UnityEngine;
using System.Collections;
using Iviz.Resources;

namespace Iviz.Displays
{
    public class AxisResource : MarkerResource
    {
        public override string Name => "Axis";

        MeshRenderer Renderer;
        Material[] materials;

        Color colorX;
        public Color ColorX
        {
            get => colorX;
            set
            {
                colorX = value;
                Material material = value.a > 254f / 255f ?
                    Resource.Materials.Lit.Object :
                    Resource.Materials.TransparentLit.Object;
                SetMaterial(material, 0);
                Renderer.SetPropertyColor(value, 0);
            }
        }

        Color colorY;
        public Color ColorY
        {
            get => colorY;
            set
            {
                colorY = value;
                Material material = value.a > 254f / 255f ?
                    Resource.Materials.Lit.Object :
                    Resource.Materials.TransparentLit.Object;
                SetMaterial(material, 1);
                Renderer.SetPropertyColor(value, 1);
            }
        }

        Color colorZ;
        public Color ColorZ
        {
            get => colorZ;
            set
            {
                colorZ = value;
                Material material = value.a > 254f / 255f ?
                    Resource.Materials.Lit.Object :
                    Resource.Materials.TransparentLit.Object;
                SetMaterial(material, 2);
                Renderer.SetPropertyColor(value, 2);
            }
        }

        float scale = 0.25f;
        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                transform.localScale = new Vector3(-1, 1, 1) * value;
            }
        }

        void SetMaterial(Material material, int index)
        {
            if (materials[index] != material)
            {
                materials[index] = material;
                Renderer.materials = materials;
            }
        }


        protected override void Awake()
        {
            base.Awake();
            Renderer = GetComponent<MeshRenderer>();
            materials = Renderer.materials;
            ColorX = Color.red;
            ColorY = Color.green;
            ColorZ = Color.blue;
        }

        public override void Stop()
        {
            base.Stop();
            ColorX = Color.red;
            ColorY = Color.green;
            ColorZ = Color.blue;
        }
    }
}
using UnityEngine;
using System.Collections;
using Iviz.Resources;

namespace Iviz.Displays
{
    public class AxisResource : MarkerResource
    {
        MeshRenderer Renderer;
        Material[] materials;

        Color colorX;
        public Color ColorX
        {
            get => colorX;
            set
            {
                colorX = value;
                SetMaterial(MatForAlpha(value.a), 0);
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
                SetMaterial(MatForAlpha(value.a), 1);
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
                SetMaterial(MatForAlpha(value.a), 2);
                Renderer.SetPropertyColor(value, 2);
            }
        }

        static Material MatForAlpha(float a)
        {
            return a > 254f / 255f ?
                Resource.Materials.Lit.Object :
                Resource.Materials.TransparentLit.Object;
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
#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class TexturedMaterialsType
    {
        readonly Dictionary<(Texture? Diffuse, Texture? Bump, bool alpha), Material> materialsByTexture = new();

        static readonly int BumpMap = Shader.PropertyToID("_BumpMap");
        static readonly int MainTex = Shader.PropertyToID("_MainTex");

        public Material Get(Texture? diffuse = null, Texture? bump = null)
        {
            if (diffuse == null && bump == null)
            {
                throw new ArgumentNullException(nameof(diffuse));
            }

            var key = (diffuse, bump, false);
            if (materialsByTexture.TryGetValue(key, out Material existingMaterial))
            {
                return existingMaterial;
            }

            Material material;
            if (bump != null)
            {
                material = Resource.Materials.BumpLit.Instantiate();
                material.SetTexture(BumpMap, bump);
                material.name = $"{Resource.Materials.BumpLit.Name} - {materialsByTexture.Count.ToString()}";
            }
            else
            {
                material = Resource.Materials.TexturedLit.Instantiate();
                material.name = $"{Resource.Materials.TexturedLit.Name} - {materialsByTexture.Count.ToString()}";
            }

            material.SetTexture(MainTex, diffuse);
            materialsByTexture[key] = material;
            return material;
        }

        public Material GetAlpha(Texture? diffuse = null, Texture? bump = null)
        {
            if (diffuse == null && bump == null)
            {
                throw new ArgumentNullException(nameof(diffuse));
            }

            var key = (diffuse, bump, true);
            if (materialsByTexture.TryGetValue(key, out Material existingMaterial))
            {
                return existingMaterial;
            }

            Material material;
            if (bump != null)
            {
                material = Resource.Materials.TransparentBumpLit.Instantiate();
                material.SetTexture(BumpMap, bump);
                material.name = $"{Resource.Materials.TransparentBumpLit.Name} - {materialsByTexture.Count.ToString()}";
            }
            else
            {
                material = Resource.Materials.TransparentTexturedLit.Instantiate();
                material.name = $"{Resource.Materials.TransparentTexturedLit.Name} - {materialsByTexture.Count.ToString()}";
            }

            material.SetTexture(MainTex, diffuse);
            materialsByTexture[key] = material;
            return material;
        }
    }
}
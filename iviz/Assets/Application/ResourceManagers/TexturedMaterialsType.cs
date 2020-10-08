using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class TexturedMaterialsType
    {
        readonly Dictionary<Texture, Material> materialsByTexture = new Dictionary<Texture, Material>();
        readonly Dictionary<Texture, Material> materialsByTextureAlpha = new Dictionary<Texture, Material>();

        public Material Get(Texture texture)
        {
            if (texture is null)
            {
                throw new ArgumentNullException(nameof(texture));
            }

            if (materialsByTexture.TryGetValue(texture, out Material material))
            {
                return material;
            }

            material = Resource.Materials.TexturedLit.Instantiate();
            material.mainTexture = texture;
            material.name = Resource.Materials.TexturedLit.Name + " - " + materialsByTexture.Count;
            materialsByTexture[texture] = material;
            return material;
        }

        public Material GetAlpha(Texture texture)
        {
            if (texture is null)
            {
                throw new ArgumentNullException(nameof(texture));
            }

            if (materialsByTextureAlpha.TryGetValue(texture, out Material material))
            {
                return material;
            }

            material = Resource.Materials.TransparentTexturedLit.Instantiate();
            material.mainTexture = texture;
            material.name = Resource.Materials.TransparentTexturedLit.Name + " - " + materialsByTextureAlpha.Count;
            materialsByTextureAlpha[texture] = material;
            return material;
        }
    }
}
#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class TexturedMaterialsType
    {
        readonly Dictionary<FullKey, Material> materialsByTexture = new();
        readonly Dictionary<SimpleKey, Material> simpleMaterialsByTexture = new();

        static readonly int BumpMap = Shader.PropertyToID("_BumpMap");
        static readonly int MainTex = Shader.PropertyToID("_MainTex");

        public Material Get(Texture? diffuse = null, Texture? bump = null)
        {
            return Settings.UseSimpleMaterials
                ? GetSimple(diffuse)
                : GetFull(diffuse, bump);
        }

        public Material GetFull(Texture? diffuse = null, Texture? bump = null)
        {
            if (diffuse == null && bump == null)
            {
                throw new ArgumentNullException(nameof(diffuse));
            }

            var key = new FullKey(diffuse, bump, false);
            if (materialsByTexture.TryGetValue(key, out var existingMaterial))
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

        public Material GetSimple(Texture? diffuse)
        {
            ThrowHelper.ThrowIfNull(diffuse, nameof(diffuse));
            var key = new SimpleKey(diffuse, false);
            if (simpleMaterialsByTexture.TryGetValue(key, out var existingMaterial))
            {
                return existingMaterial;
            }

            var material = Resource.Materials.SimpleTexturedLit.Instantiate();
            material.name =
                $"{Resource.Materials.SimpleTexturedLit.Name} - {simpleMaterialsByTexture.Count.ToString()}";
            material.SetTexture(MainTex, diffuse);
            simpleMaterialsByTexture[key] = material;
            return material;
        }

        public Material GetAlpha(Texture? diffuse = null, Texture? bump = null)
        {
            return Settings.UseSimpleMaterials
                ? GetAlphaSimple(diffuse)
                : GetAlphaFull(diffuse, bump);
        }

        Material GetAlphaFull(Texture? diffuse = null, Texture? bump = null)
        {
            if (diffuse == null && bump == null)
            {
                throw new ArgumentNullException(nameof(diffuse));
            }

            var key = new FullKey(diffuse, bump, true);
            if (materialsByTexture.TryGetValue(key, out var existingMaterial))
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
                material.name =
                    $"{Resource.Materials.TransparentTexturedLit.Name} - {materialsByTexture.Count.ToString()}";
            }

            material.SetTexture(MainTex, diffuse);
            materialsByTexture[key] = material;
            return material;
        }

        Material GetAlphaSimple(Texture? diffuse = null)
        {
            ThrowHelper.ThrowIfNull(diffuse, nameof(diffuse));
            var key = new SimpleKey(diffuse, true);
            if (simpleMaterialsByTexture.TryGetValue(key, out var existingMaterial))
            {
                return existingMaterial;
            }

            var material = Resource.Materials.SimpleTransparentTexturedLit.Instantiate();
            material.name =
                $"{Resource.Materials.SimpleTransparentTexturedLit.Name} - {simpleMaterialsByTexture.Count.ToString()}";
            material.SetTexture(MainTex, diffuse);
            simpleMaterialsByTexture[key] = material;
            return material;
        }
        
        readonly struct FullKey : IEquatable<FullKey>
        {
            readonly Texture? diffuse;
            readonly Texture? bump;
            readonly bool alpha;

            public FullKey(Texture? diffuse, Texture? bump, bool alpha) =>
                (this.diffuse, this.bump, this.alpha) = (diffuse, bump, alpha);

            public bool Equals(FullKey other) => diffuse == other.diffuse && bump == other.bump && alpha == other.alpha;
            public override bool Equals(object? obj) => obj is FullKey other && Equals(other);
            public override int GetHashCode() => HashCode.Combine(diffuse, bump, alpha);
        }

        readonly struct SimpleKey : IEquatable<SimpleKey>
        {
            readonly Texture? diffuse;
            readonly bool alpha;

            public SimpleKey(Texture? diffuse, bool alpha) => (this.diffuse, this.alpha) = (diffuse, alpha);
            public bool Equals(SimpleKey other) => diffuse == other.diffuse && alpha == other.alpha;
            public override bool Equals(object? obj) => obj is SimpleKey other && Equals(other);
            public override int GetHashCode() => HashCode.Combine(diffuse, alpha);
        }
    }
}
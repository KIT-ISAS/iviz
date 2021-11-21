#nullable enable

using System;
using Iviz.Msgs;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Core
{
    public static class MeshRendererUtils
    {
        static MaterialPropertyBlock? propBlock;
        static MaterialPropertyBlock PropBlock => propBlock ??= new MaterialPropertyBlock();

        static readonly int ColorPropId = Shader.PropertyToID("_Color");
        static readonly int EmissiveColorPropId = Shader.PropertyToID("_EmissiveColor");
        static readonly int MainTexStPropId = Shader.PropertyToID("_MainTex_ST_");
        static readonly int BumpMapStPropId = Shader.PropertyToID("_BumpMap_ST_");
        static readonly int SmoothnessPropId = Shader.PropertyToID("_Smoothness");
        static readonly int MetallicPropId = Shader.PropertyToID("_Metallic");

        public static void SetPropertyColor(this MeshRenderer meshRenderer, in Color color, int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            meshRenderer.GetPropertyBlock(PropBlock, id);
            PropBlock.SetColor(ColorPropId, color);
            meshRenderer.SetPropertyBlock(PropBlock, id);
        }

        public static void SetPropertyEmissiveColor(this MeshRenderer meshRenderer, in Color color,
            int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            meshRenderer.GetPropertyBlock(PropBlock, id);
            PropBlock.SetColor(EmissiveColorPropId, color);
            meshRenderer.SetPropertyBlock(PropBlock, id);
        }

        public static void SetPropertySmoothness(this MeshRenderer meshRenderer, float smoothness, int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            meshRenderer.GetPropertyBlock(PropBlock, id);
            PropBlock.SetFloat(SmoothnessPropId, smoothness);
            meshRenderer.SetPropertyBlock(PropBlock, id);
        }

        public static void SetPropertyMetallic(this MeshRenderer meshRenderer, float metallic, int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            meshRenderer.GetPropertyBlock(PropBlock, id);
            PropBlock.SetFloat(MetallicPropId, metallic);
            meshRenderer.SetPropertyBlock(PropBlock, id);
        }

        public static void ResetPropertyTextureScale(this MeshRenderer meshRenderer)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            meshRenderer.GetPropertyBlock(PropBlock, 0);
            PropBlock.SetVector(MainTexStPropId, new Vector4(1, 1, 0, 0));
            PropBlock.SetVector(BumpMapStPropId, new Vector4(1, 1, 0, 0));
            meshRenderer.SetPropertyBlock(PropBlock, 0);
        }
    }
}
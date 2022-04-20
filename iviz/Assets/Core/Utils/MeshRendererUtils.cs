#nullable enable

using System;
using Iviz.Displays;
using Iviz.Msgs;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Core
{
    public static class MeshRendererUtils
    {
        static MaterialPropertyBlock? propBlock;
        static MaterialPropertyBlock PropBlock => propBlock ??= new MaterialPropertyBlock();

        public static void SetPropertyColor(this MeshRenderer meshRenderer, in Color color, int id = 0)
        {
            ThrowHelper.ThrowIfNull(meshRenderer, nameof(meshRenderer));
            meshRenderer.GetPropertyBlock(PropBlock, id);
            PropBlock.SetColor(ShaderIds.ColorId, color);
            meshRenderer.SetPropertyBlock(PropBlock, id);
        }

        public static void SetPropertyEmissiveColor(this MeshRenderer meshRenderer, in Color color,
            int id = 0)
        {
            ThrowHelper.ThrowIfNull(meshRenderer, nameof(meshRenderer));
            meshRenderer.GetPropertyBlock(PropBlock, id);
            PropBlock.SetColor(ShaderIds.EmissiveColorId, color);
            meshRenderer.SetPropertyBlock(PropBlock, id);
        }

        public static void SetPropertySmoothness(this MeshRenderer meshRenderer, float smoothness, int id = 0)
        {
            ThrowHelper.ThrowIfNull(meshRenderer, nameof(meshRenderer));
            meshRenderer.GetPropertyBlock(PropBlock, id);
            PropBlock.SetFloat(ShaderIds.SmoothnessId, smoothness);
            meshRenderer.SetPropertyBlock(PropBlock, id);
        }

        public static void SetPropertyMetallic(this MeshRenderer meshRenderer, float metallic, int id = 0)
        {
            ThrowHelper.ThrowIfNull(meshRenderer, nameof(meshRenderer));
            meshRenderer.GetPropertyBlock(PropBlock, id);
            PropBlock.SetFloat(ShaderIds.MetallicId, metallic);
            meshRenderer.SetPropertyBlock(PropBlock, id);
        }
        
        public static void SetPropertyTextureScale(this MeshRenderer meshRenderer, float scaleX, float scaleY)
        {
            ThrowHelper.ThrowIfNull(meshRenderer, nameof(meshRenderer));
            meshRenderer.GetPropertyBlock(PropBlock, 0);
            PropBlock.SetVector(ShaderIds.MainTexStId, new Vector4(scaleX, scaleY, 0, 0));
            meshRenderer.SetPropertyBlock(PropBlock, 0);
        }

        public static void ResetPropertyTextureScale(this MeshRenderer meshRenderer)
        {
            ThrowHelper.ThrowIfNull(meshRenderer, nameof(meshRenderer));
            meshRenderer.GetPropertyBlock(PropBlock, 0);
            PropBlock.SetVector(ShaderIds.MainTexStId, new Vector4(1, 1, 0, 0));
            PropBlock.SetVector(ShaderIds.BumpMapStId, new Vector4(1, 1, 0, 0));
            meshRenderer.SetPropertyBlock(PropBlock, 0);
        }
    }
}
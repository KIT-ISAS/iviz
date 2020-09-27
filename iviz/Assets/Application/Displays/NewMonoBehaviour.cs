using Iviz.Resources;
using UnityEngine;
using UnityEngine.Windows;
using Color = UnityEngine.Color;

namespace Iviz.App
{
    public class NewMonoBehaviour : MonoBehaviour
    {
        void OnEnable()
        {
            /*
            Texture2D t = null;

            Color[] atlasColors = new Color[16 * 16];
            
            for (int i = 0; i < Resource.Colormaps.Names.Count; i++)
            {
                Texture2D tex = Resource.Colormaps.Textures[(Resource.ColormapId) i];
                Color[] colors = tex.GetPixels();
                for (int j = 0; j < 16; j++)
                {
                    atlasColors[16 * (15 - i) + j] = colors[j];
                }

            }
            
            Texture2D newTexture = new Texture2D(16, 16);
            newTexture.SetPixels(atlasColors);
            newTexture.Apply();
            byte[] bytes = newTexture.EncodeToPNG();
            File.WriteAllBytes("/Users/akzeac/atlas.png", bytes);
            */
        }
    }


}
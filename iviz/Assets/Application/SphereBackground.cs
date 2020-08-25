using System;
using System.IO;
using UnityEngine;

namespace Application
{
    public sealed class SphereBackground : MonoBehaviour
    {
        void Start()
        {
            MeshFilter filter = GetComponent<MeshFilter>();
            Mesh mesh = filter.mesh; // makes copy

            Vector3[] ps = mesh.vertices;
            Vector2[] uvs = new Vector2[mesh.vertexCount];

            //float min = float.MaxValue;
            //float max = float.MinValue;
            for (int i = 0; i < uvs.Length; i++)
            {
                //min = Mathf.Min(min, Mathf.Asin(ps[i].z) / Mathf.PI);
                //max = Mathf.Max(max, Mathf.Asin(ps[i].z) / Mathf.PI);
                uvs[i] = new Vector2(Mathf.Asin(ps[i].z) / Mathf.PI + 0.5f, 0);
            }

            mesh.uv = uvs;
            //Debug.Log(min + " " + max);
            
            Texture2D texture2D = new Texture2D(5, 1, TextureFormat.RGB24, true);
            Color32[] colors =
            {
                Color.black,
                new Color32(0x43, 0x2B, 0x3F, 0xFF), 
                new Color32(0x1D,0x43, 0x7D, 0xFF), 
                new Color32(0x00, 0x29, 0x4F, 0xFF), 
                new Color32(0x00, 0x1D, 0x17, 0xFF), 
            };
            
            texture2D.SetPixels32(colors);
            texture2D.Apply();

            File.WriteAllBytes("/Users/akzeac/short-background.png", texture2D.EncodeToPNG());
            
            GetComponent<MeshRenderer>().sharedMaterial.mainTexture = texture2D;
        }

        void Update()
        {
            transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
    }
}
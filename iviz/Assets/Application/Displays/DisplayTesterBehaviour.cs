using System.Collections.Generic;
using System.IO;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Ros;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public class DisplayTesterBehaviour : MonoBehaviour
    {
        const int MaxSegmentsForMesh = 30;

        void OnEnable()
        {
            /*
            Texture2D texture = GenerateSquareTexture();
            byte[] png = texture.EncodeToPNG();
            File.WriteAllBytes("Assets/Core/Textures/grid.png", png);
            */
            
            CreatePointListResource(-3);
            CreateLineResource(0);
            CreateLineResourceShort(5);
            CreateMeshListResource(10);
        }
        
        [NotNull]
        static Texture2D GenerateSquareTexture()
        {
            const int size = 128;
            const int border = 2;
            Color32 white = Color.white;
            Color32 frameColor = new Color(0.25f, 0.25f, 0.25f, 1);

            Color32[] colors = new Color32[size * size];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = white;
            }

            for (int v = 0; v < size; v++)
            {
                for (int i = 0; i < border; i++)
                {
                    colors[v * size + i] = frameColor;
                    colors[(v + 1) * size - 1 - i] = frameColor;
                }
            }

            for (int i = 0; i < size * border; i++)
            {
                colors[i] = frameColor;
            }

            for (int i = size * (size - border); i < size * size; i++)
            {
                colors[i] = frameColor;
            }

            Texture2D texture = new Texture2D(size, size);
            texture.SetPixels32(colors);
            texture.Apply();
            return texture;
        }        

        static void CreateMeshListResource(float z)
        {
            {
                var resource = ResourcePool.GetOrCreateDisplay<MeshListResource>();
                List<PointWithColor> points = new List<PointWithColor>();
                for (int i = 0; i < 40; i++)
                {
                    points.Add(new PointWithColor(new Vector3(i, 0, z), Color.green));
                }

                resource.MeshResource = Resource.Displays.Cube;
                resource.ElementScale = 0.5f;
                resource.ElementScale3 = new Vector3(1, 2, 1);
                resource.UseColormap = false;
                resource.Colormap = Resource.ColormapId.hsv;
                resource.Set(points);
            }

            {
                var resource = ResourcePool.GetOrCreateDisplay<MeshListResource>();
                List<PointWithColor> points = new List<PointWithColor>();
                for (int i = 0; i < 40; i++)
                {
                    points.Add(new PointWithColor(new Vector3(i, 0, z + 1), 0.025f * i));
                }

                resource.MeshResource = Resource.Displays.Cube;
                resource.ElementScale = 0.5f;
                resource.ElementScale3 = new Vector3(1, 2, 1);
                resource.UseColormap = true;
                resource.Colormap = Resource.ColormapId.hsv;
                resource.Set(points);
            }

            {
                var resource = ResourcePool.GetOrCreateDisplay<MeshListResource>();
                List<PointWithColor> points = new List<PointWithColor>();
                for (int i = 0; i < 40; i++)
                {
                    points.Add(new PointWithColor(new Vector3(i, 0, z + 2), 0.025f * i));
                }

                resource.MeshResource = Resource.Displays.Sphere;
                resource.ElementScale = 0.5f;
                resource.ElementScale3 = new Vector3(1, 2, 1);
                resource.UseIntensityForScaleY = true;
                resource.Colormap = Resource.ColormapId.hsv;
                resource.Set(points);
            }

            {
                var resource = ResourcePool.GetOrCreateDisplay<MeshListResource>();
                List<PointWithColor> points = new List<PointWithColor>();
                for (int i = 0; i < 40; i++)
                {
                    points.Add(new PointWithColor(new Vector3(i, 0, z + 3), 0.025f * i));
                }

                resource.MeshResource = Resource.Displays.Cylinder;
                resource.ElementScale = 0.5f;
                resource.ElementScale3 = new Vector3(1, 2, 1);
                resource.Set(points);
                resource.OcclusionOnly = true;
            }

            {
                var resource = ResourcePool.GetOrCreateDisplay<MeshListResource>();
                List<PointWithColor> points = new List<PointWithColor>();
                for (int i = 0; i < 40; i++)
                {
                    points.Add(new PointWithColor(new Vector3(i, 0, z + 4), 0.025f * i));
                }

                resource.MeshResource = Resource.Displays.Cube;
                resource.ElementScale = 0.5f;
                resource.ElementScale3 = new Vector3(1, 2, 1);
                resource.UseIntensityForScaleY = true;
                resource.Colormap = Resource.ColormapId.hsv;
                resource.Set(points);
                resource.OcclusionOnly = true;
            }
        }

        static void CreateLineResourceShort(float z)
        {
            {
                LineResource resource = ResourcePool.GetOrCreateDisplay<LineResource>();
                List<LineWithColor> lines = new List<LineWithColor>();
                for (int i = 0; i < MaxSegmentsForMesh / 2; i++)
                {
                    lines.Add(new LineWithColor(
                        new Vector3(i, 0, z), Color.green,
                        new Vector3(i + 1, 1, z), Color.green
                    ));
                }

                resource.Set(lines);
            }

            {
                LineResource resource = ResourcePool.GetOrCreateDisplay<LineResource>();
                List<LineWithColor> lines = new List<LineWithColor>();
                for (int i = 0; i < MaxSegmentsForMesh / 2; i++)
                {
                    lines.Add(new LineWithColor(
                        new Vector3(i, 0, z + 1), 0.1f * i,
                        new Vector3(i + 1, 1, z + 1), 0.1f * i
                    ));
                }

                resource.UseColormap = true;
                resource.Colormap = Resource.ColormapId.hsv;
                resource.Set(lines);
            }

            {
                LineResource resource = ResourcePool.GetOrCreateDisplay<LineResource>();
                List<LineWithColor> lines = new List<LineWithColor>();
                for (int i = 0; i < MaxSegmentsForMesh / 2; i++)
                {
                    lines.Add(new LineWithColor(
                        new Vector3(i, 0, z + 2), Color.red.WithAlpha(0.25f),
                        new Vector3(i + 1, 1, z + 2), Color.green
                    ));
                }

                resource.Set(lines);
            }

            {
                LineResource resource = ResourcePool.GetOrCreateDisplay<LineResource>();
                List<LineWithColor> lines = new List<LineWithColor>();
                for (int i = 0; i < MaxSegmentsForMesh / 2; i++)
                {
                    lines.Add(new LineWithColor(
                        new Vector3(i, 0, z + 3), 0.1f * i,
                        new Vector3(i + 1, 1, z + 3), 0.1f * i
                    ));
                }

                resource.UseColormap = true;
                resource.Tint = Color.white.WithAlpha(0.25f);
                resource.Colormap = Resource.ColormapId.hsv;
                resource.Set(lines);
            }
        }


        static void CreateLineResource(float z)
        {
            {
                LineResource resource = ResourcePool.GetOrCreateDisplay<LineResource>();
                List<LineWithColor> lines = new List<LineWithColor>();
                for (int i = 0; i < MaxSegmentsForMesh + 10; i++)
                {
                    lines.Add(new LineWithColor(
                        new Vector3(i, 0, z), Color.green,
                        new Vector3(i + 1, 1, z), Color.green
                    ));
                }

                resource.Set(lines);
            }

            {
                LineResource resource = ResourcePool.GetOrCreateDisplay<LineResource>();
                List<LineWithColor> lines = new List<LineWithColor>();
                for (int i = 0; i < MaxSegmentsForMesh + 10; i++)
                {
                    lines.Add(new LineWithColor(
                        new Vector3(i, 0, z + 1), 0.025f * i,
                        new Vector3(i + 1, 1, z + 1), 0.025f * i
                    ));
                }

                resource.UseColormap = true;
                resource.Colormap = Resource.ColormapId.hsv;
                resource.Set(lines);
            }

            {
                LineResource resource = ResourcePool.GetOrCreateDisplay<LineResource>();
                List<LineWithColor> lines = new List<LineWithColor>();
                for (int i = 0; i < MaxSegmentsForMesh + 10; i++)
                {
                    lines.Add(new LineWithColor(
                        new Vector3(i, 0, z + 2), new Color(1, 0, 0, 0.5f),
                        new Vector3(i + 1, 1, z + 2), Color.green
                    ));
                }

                resource.Set(lines);
            }

            {
                LineResource resource = ResourcePool.GetOrCreateDisplay<LineResource>();
                List<LineWithColor> lines = new List<LineWithColor>();
                for (int i = 0; i < MaxSegmentsForMesh + 10; i++)
                {
                    lines.Add(new LineWithColor(
                        new Vector3(i, 0, z + 3), 0.025f * i,
                        new Vector3(i + 1, 1, z + 3), 0.025f * i
                    ));
                }

                resource.UseColormap = true;
                resource.Tint = new Color(1, 1, 1, 0.5f);
                resource.Colormap = Resource.ColormapId.hsv;
                resource.Set(lines);
            }
        }

        static void CreatePointListResource(float z)
        {
            {
                var resource = ResourcePool.GetOrCreateDisplay<PointListResource>();
                List<PointWithColor> points = new List<PointWithColor>();
                for (int i = 0; i < 40; i++)
                {
                    points.Add(new PointWithColor(new Vector3(i, 0, z + 1), Color.magenta));
                }

                resource.ElementScale = 0.5f;
                resource.Set(points);
            }
            {
                var resource = ResourcePool.GetOrCreateDisplay<PointListResource>();
                List<PointWithColor> points = new List<PointWithColor>();
                for (int i = 0; i < 40; i++)
                {
                    points.Add(new PointWithColor(new Vector3(i, 0, z), 0.025f * i));
                }

                resource.ElementScale = 0.5f;
                resource.UseColormap = true;
                resource.Colormap = Resource.ColormapId.hsv;
                resource.Set(points);
            }
        }
    }
}
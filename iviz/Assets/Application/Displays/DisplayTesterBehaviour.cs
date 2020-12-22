using System.Collections.Generic;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Ros;
using UnityEngine;

namespace Iviz.App
{
    public class DisplayTesterBehaviour : MonoBehaviour
    {
        const int MaxSegmentsForMesh = 30;

        async void OnEnable()
        {
            var uri = "package://e2_urdf_model/meshes/chassis.dae";
            Info<GameObject> info = await
                Resource.External.TryGetGameObjectAsync(uri, ConnectionManager.Connection, default);
            info.Instantiate();



            /*
            Matrix4x4 f = new Matrix4x4();
            f.SetColumn(0, new Vector3(1, 0, 0).Ros2Unity());
            f.SetColumn(1, new Vector3(0, 1, 0).Ros2Unity());
            f.SetColumn(2, new Vector3(0, 0, 1).Ros2Unity());
            f.SetColumn(3, new Vector4(0, 0, 0, 1));
            Matrix4x4 q = Matrix4x4.Rotate(new Quaternion(0.5f, -0.5f, 0.5f, 0.5f));
            
            Matrix4x4 f = new Matrix4x4();
            f.SetColumn(0, new Vector3(1, 0, 0).Unity2Ros());
            f.SetColumn(1, new Vector3(0, 1, 0).Unity2Ros());
            f.SetColumn(2, new Vector3(0, 0, 1).Unity2Ros());
            f.SetColumn(3, new Vector4(0, 0, 0, 1));
            Matrix4x4 q = Matrix4x4.Rotate(new Quaternion(0.5f, -0.5f, -0.5f, 0.5f));

            
            Debug.Log(f);
            Debug.Log(q);
            */

            /*
            CreatePointListResource(-3);
            CreateLineResource(0);
            CreateLineResourceShort(5);
            CreateMeshListResource(10);
            */
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
                        new Vector3(i, 0, z + 2), new Color(1, 0, 0, 0.25f),
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
                resource.Tint = new Color(1, 1, 1, 0.25f);
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
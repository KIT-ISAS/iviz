using System;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.XmlRpc;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public class ARLineConnector : MonoBehaviour, IDisplay, IRecyclable
    {
        MeshMarkerResource[] spheres;
        LineResource lines;
        int layer;
        Color32 color;

        readonly NativeList<LineWithColor> lineSegments = new NativeList<LineWithColor>();

        public Func<Vector3> Start { get; set; }
        public Func<Vector3> End { get; set; }

        public Bounds? Bounds => null;

        void Awake()
        {
            lines = ResourcePool.RentDisplay<LineResource>(TfListener.ListenersFrame.Transform);
            lines.ElementScale = 0.005f;

            spheres = new MeshMarkerResource[3];
            foreach (ref var sphere in spheres.Ref())
            {
                sphere = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere, transform);
                sphere.Transform.localScale = 0.05f * Vector3.one;
                sphere.CastsShadows = false;
            }

            Color = Color.cyan;
            Layer = LayerType.IgnoreRaycast;
        }

        public int Layer
        {
            get => layer;
            set
            {
                layer = value;
                lines.Layer = value;
                foreach (var sphere in spheres)
                {
                    sphere.Layer = value;
                }
            }
        }

        public void Suspend()
        {
            Color = Color.cyan;
            lineSegments.Clear();
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public Color Color
        {
            get => color;
            set
            {
                color = value;

                Color32 colorMid = color.WithAlpha(130);
                Color32 colorB = color.WithAlpha(10);

                spheres[0].Color = color;
                spheres[1].Color = colorMid;
                spheres[2].Color = colorB;
                
                spheres[0].EmissiveColor = color;
                spheres[1].EmissiveColor = colorMid;
                spheres[2].EmissiveColor = colorB;
            }
        }

        public string Name { get; set; }

        public void SplitForRecycle()
        {
            ResourcePool.ReturnDisplay(lines);

            foreach (var sphere in spheres)
            {
                sphere.EmissiveColor = Color.black;
                ResourcePool.Return(Resource.Displays.Sphere, sphere.gameObject);
            }
        }

        public void Update()
        {
            if (Start == null || End == null)
            {
                return;
            }

            Vector3 a = Start();
            Vector3 b = End();
            Vector3 mid = new Vector3((a.x + b.x) / 2, b.y, (a.z + b.z) / 2);

            spheres[0].Transform.position = a;
            spheres[1].Transform.position = mid;
            spheres[2].Transform.position = b;

            Color32 colorMid = color.WithAlpha(130);
            Color32 colorB = color.WithAlpha(10);

            lineSegments.Clear();
            lineSegments.Add(new LineWithColor(a, color, mid, colorMid));
            lineSegments.Add(new LineWithColor(mid, colorMid, b, colorB));
            lines.Set(lineSegments);
        }

        void OnDestroy()
        {
            lineSegments.Dispose();
        }
    }
}
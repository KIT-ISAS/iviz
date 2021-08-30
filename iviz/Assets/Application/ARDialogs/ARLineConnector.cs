using System;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class ARLineConnector : MonoBehaviour
    {
        GameObject node;
        readonly MeshMarkerResource[] spheres = new MeshMarkerResource[3];
        LineResource lines;
        int layer;
        Color32 color;

        readonly NativeList<LineWithColor> lineSegments = new NativeList<LineWithColor>();

        public Func<Vector3> Start { get; set; }
        public Func<Vector3> End { get; set; }

        void Awake()
        {
            node = new GameObject("AR LineConnector Node");
            node.transform.SetParentLocal(TfListener.OriginFrame.transform);
            lines = ResourcePool.RentDisplay<LineResource>(node.transform);
            lines.ElementScale = 0.005f;

            foreach (ref var sphere in spheres.Ref())
            {
                sphere = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere, node.transform);
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

        public bool Visible
        {
            get => gameObject.activeSelf;
            set
            {
                gameObject.SetActive(value);
                node.SetActive(value);
            }
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

        public void SplitForRecycle()
        {
            lines.ReturnToPool();

            foreach (var sphere in spheres)
            {
                sphere.ReturnToPool(Resource.Displays.Sphere);
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

            spheres[0].Transform.localPosition = a;
            spheres[1].Transform.localPosition = mid;
            spheres[2].Transform.localPosition = b;

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
            Destroy(node);
        }
    }
}
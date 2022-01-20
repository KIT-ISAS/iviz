#nullable enable

using System;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class DialogConnector : MonoBehaviour
    {
        [SerializeField] XRDialog? dialog;
        
        MeshMarkerResource[]? spheres;
        FrameNode? node;
        LineResource? lines;
        Color32 color = Color.cyan;

        FrameNode Node => node ??= new FrameNode("AR LineConnector Node", TfListener.OriginFrame);
        LineResource Lines => lines != null ? lines : (lines = ResourcePool.RentDisplay<LineResource>(Node.Transform));
        MeshMarkerResource[] Spheres => spheres ??= new[] { RentSphere(), RentSphere(), RentSphere() };
        XRDialog Dialog => dialog.AssertNotNull(nameof(dialog));

        void Awake()
        {
            Lines.ElementScale = 0.005f;
            Color = color;
        }

        MeshMarkerResource RentSphere()
        {
            var sphere = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere, Node.Transform);
            sphere.Transform.localScale = 0.05f * Vector3.one;
            sphere.EnableShadows = false;
            return sphere;
        }

        public bool Visible
        {
            set
            {
                gameObject.SetActive(value);
                Node.Visible = value;
                Lines.Visible = value;
            }
        }

        public Color Color
        {
            set
            {
                color = value;

                var colorMid = color.WithAlpha(130);
                var colorB = color.WithAlpha(10);

                var mSpheres = Spheres;
                mSpheres[0].Color = color;
                mSpheres[1].Color = colorMid;
                mSpheres[2].Color = colorB;

                mSpheres[0].EmissiveColor = color;
                mSpheres[1].EmissiveColor = colorMid;
                mSpheres[2].EmissiveColor = colorB;
            }
        }

        public void SplitForRecycle()
        {
            lines.ReturnToPool();

            if (spheres == null)
            {
                return;
            }
            
            foreach (var sphere in spheres)
            {
                sphere.ReturnToPool(Resource.Displays.Sphere);
            }
        }

        void Update()
        {
            var mDialog = Dialog;
            if (!mDialog.NeedsConnector)
            {
                return;
            }
            
            var a = mDialog.ConnectorStart;
            var b = mDialog.ConnectorEnd;
            var mid = new Vector3((a.x + b.x) / 2, b.y, (a.z + b.z) / 2);

            var mSpheres = Spheres;
            mSpheres[0].Transform.localPosition = a;
            mSpheres[1].Transform.localPosition = mid;
            mSpheres[2].Transform.localPosition = b;

            var colorMid = color.WithAlpha(130);
            var colorB = color.WithAlpha(10);

            ReadOnlySpan<LineWithColor> lineSegments = stackalloc[]
            {
                new LineWithColor(a, color, mid, colorMid),
                new LineWithColor(mid, colorMid, b, colorB)
            };

            Lines.Set(lineSegments, true);
        }

        void OnDestroy()
        {
            node?.Dispose();
        }
    }
}
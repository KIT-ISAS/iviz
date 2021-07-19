using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App.Tests
{
    public class PulseTest : MonoBehaviour
    {
        float start;
        static readonly int PulseCenter = Shader.PropertyToID("_PulseCenter");
        static readonly int PulseTime = Shader.PropertyToID("_PulseTime");
        static readonly int PulseDelta = Shader.PropertyToID("_PulseDelta");

        void Awake()
        {
            var resource = ResourcePool.RentDisplay<LineResource>();
            resource.ElementScale = 0.005f;
            resource.MaterialOverride = Resource.Materials.LinePulse.Object;

            using (var lines = new NativeList<LineWithColor>())
            {
                const int n = 10;
                for (int v = 0; v < n; v++)
                {
                    for (int u = 0; u < n; u++)
                    {
                        lines.Add(new LineWithColor(new Vector3(u, 0, v), new Vector3(u + 1, 0, v)));
                        lines.Add(new LineWithColor(new Vector3(u + 1, 0, v), new Vector3(u + 1, 0, v + 1)));
                        lines.Add(new LineWithColor(new Vector3(u + 1, 0,v + 1), new Vector3(u, 0, v + 1)));
                        lines.Add(new LineWithColor(new Vector3(u, 0, v + 1), new Vector3(u + 1, 0, v)));
                    }
                }
                
                resource.Set(lines);
            }

            start = Time.time;
        }

        void Update()
        {
            float timeDiff = Time.time - start;
            var material = Resource.Materials.LinePulse.Object;
            material.SetVector(PulseCenter,  new Vector4(0, 0, 0, 0));
            material.SetFloat(PulseTime,  (timeDiff - 1) * 2);
            material.SetFloat(PulseDelta, 1f);

            if (timeDiff > 10)
            {
                start = Time.time;
            }
        }
        
    }
}
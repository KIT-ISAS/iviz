using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App.Tests
{
    public class PulseTest : MonoBehaviour
    {
        float start;
        
        void Awake()
        {
            var resource = ResourcePool.RentDisplay<LineDisplay>();
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
                
                resource.Set(lines, false);
            }

            start = Time.time;
        }

        void Update()
        {
            float timeDiff = Time.time - start;
            var material = Resource.Materials.LinePulse.Object;
            material.SetVector(ShaderIds.PulseCenterId,  new Vector4(0, 0, 0, 0));
            material.SetFloat(ShaderIds.PulseTimeId,  (timeDiff - 0.5f) );
            material.SetFloat(ShaderIds.PulseDeltaId, 0.25f);

            if (timeDiff > 5)
            {
                start = Time.time;
            }
        }
        
    }
}
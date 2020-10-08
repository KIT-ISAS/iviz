using System;
using UnityEngine;
using System.Collections.Generic;
using Iviz.Resources;

namespace Iviz.Displays
{
    public sealed class AngleAxisResource : DisplayWrapperResource, ISupportsTint
    {
        LineResource resource;
        readonly List<LineWithColor> lines = new List<LineWithColor>();

        protected override IDisplay Display => resource;
        
        void Awake()
        {
            resource = ResourcePool.GetOrCreate<LineResource>(Resource.Displays.Line, transform);
            resource.ElementScale = 0.01f;
            Color = Color.yellow;
        }

        public void Set(in Quaternion q, float scale = 0.3f)
        {
            q.ToAngleAxis(out float angle, out Vector3 axis);
            Set(angle * Mathf.Deg2Rad, axis, scale);
        }

        public void Set(in Vector3 rod, float scale = 0.3f)
        {
            float angle = rod.magnitude;
            if (Mathf.Approximately(angle, 0))
            {
                Set(0, Vector3.zero, 0);
            }
            else
            {
                Vector3 axis = rod / angle;
                Set(angle, axis, scale);
            }
        }

        Color color;
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                for (int i = 0; i < lines.Count; i++)
                {
                    LineWithColor prevLine = lines[i];
                    lines[i] = new LineWithColor(prevLine.A, prevLine.B, color);
                }
                resource.LinesWithColor = lines;
            }
        }
        
        public Color Tint
        {
            get => resource.Tint;
            set => resource.Tint = value;
        }

        public void Reset()
        {
            resource.Visible = false;            
        }
        
        public void Set(float angle, Vector3 axis, float scale = 0.3f)
        {
            if (Mathf.Approximately(angle, 0) || 
                Mathf.Approximately(axis.sqrMagnitude, 0))
            {
                resource.Visible = false;
                return;
            }
            
            resource.Visible = true;

            angle *= -1;

            if (axis.y < 0)
            {
                angle *= -1;
                axis *= -1;
            }
            
            lines.Clear();
            //lines.Add(new LineWithColor(Vector3.zero, scale * axis, Color));

            Vector3 x = Vector3.forward;
            if (x == axis)
            {
                x = Vector3.right;
            }
            Vector3 diry = Vector3.Cross(x, axis).normalized;
            Vector3 dirx = Vector3.Cross(axis, diry).normalized;
            dirx *= scale;
            diry *= scale;

            int n = (int)(Mathf.Abs(angle) / (2 * Mathf.PI) * 32 + 1);
            //Debug.Log(angle + " -> " + n);
            
            Vector3 v0 = dirx;
            Vector3 v1 = Vector3.zero;

            lines.Add(new LineWithColor(Vector3.zero, 1.2f * v0, Color));

            for (int i = 1; i <= n; i++)
            {
                float a = i / (float)n * angle;
                float ax = Mathf.Cos(a);
                float ay = Mathf.Sin(a);
                v1 = ax * dirx + ay * diry;
                //float iScale = 1.01f;
                v1 *= (1 + a * 0.02f);
                lines.Add(new LineWithColor(v0, v1, Color));
                v0 = v1;
            }

            lines.Add(new LineWithColor(Vector3.zero, 1.2f * v1, Color));
            resource.LinesWithColor = lines;
        }
    }
}
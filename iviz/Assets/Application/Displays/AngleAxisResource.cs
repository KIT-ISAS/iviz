using UnityEngine;
using System.Collections.Generic;
using Iviz.App;
using Iviz.Resources;
using RosSharp.RosBridgeClient.Actionlib;

namespace Iviz.Displays
{
    public sealed class AngleAxisResource : MonoBehaviour, IRecyclable, IDisplay, ISupportsTint
    {
        LineResource resource;
        readonly List<LineWithColor> lines = new List<LineWithColor>();

        public string Name => "AxisAngleResource";
        public Bounds Bounds => resource.Bounds;
        public Bounds WorldBounds => resource.WorldBounds;
        public Pose WorldPose => resource.WorldPose;
        public Vector3 WorldScale => resource.WorldScale;

        public int Layer
        {
            get => resource.Layer;
            set => resource.Layer = value;
        }

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }

        public bool ColliderEnabled
        {
            get => resource.ColliderEnabled;
            set => resource.ColliderEnabled = value;
        }

        public void Stop()
        {
            resource.Stop();
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void Awake()
        {
            resource = ResourcePool.GetOrCreate<LineResource>(Resource.Displays.Line, transform);
            resource.UseAlpha = false;
            resource.LineScale = 0.01f;
            Color = Color.yellow;
        }

        public void Set(in Quaternion q, float scale = 0.3f)
        {
            q.ToAngleAxis(out float angle, out Vector3 axis);
            Debug.Log("in: " + q + "angle: " + angle + " " + axis);
            Set(angle * Mathf.Deg2Rad, axis, scale);
        }

        public void Set(in Vector3 rod, float scale = 0.3f)
        {
            float angle = rod.magnitude;
            if (angle == 0)
            {
                Debug.Log("out!");
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
        
        public void Set(float angle, in Vector3 axis, float scale = 0.3f)
        {
            if (angle == 0 || axis.magnitude == 0)
            {
                Debug.Log("out!");
                resource.Visible = false;
                return;
            }
            
            resource.Visible = true;
            
            lines.Clear();
            lines.Add(new LineWithColor(Vector3.zero, scale * axis, Color));

            Vector3 x = new Vector3(0, 0, 1);
            if (x == axis)
            {
                x = new Vector3(1, 0, 0);
            }
            Vector3 diry = Vector3.Cross(x, axis).normalized;
            Vector3 dirx = Vector3.Cross(axis, diry).normalized;
            dirx *= scale;
            diry *= scale;

            int n = (int)(angle / (2 * Mathf.PI) * 32 + 1);
            Debug.Log(angle + " -> " + n);
            
            Vector3 v0 = dirx * scale;
            Vector3 v1 = Vector3.zero;

            lines.Add(new LineWithColor(Vector3.zero, v0, Color));

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

            lines.Add(new LineWithColor(Vector3.zero, v1, Color));
            resource.LinesWithColor = lines;
        }
        
        public void Recycle()
        {
            ResourcePool.Dispose(Resource.Displays.Line, resource.gameObject);
        }
    }
}
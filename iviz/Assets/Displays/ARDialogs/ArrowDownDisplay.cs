using System;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class ArrowDownDisplay : MeshMarkerDisplay
    {
        static Mesh baseMesh;
        float? currentAngle;

        [SerializeField] Vector3 startPosition = Vector3.zero;

        protected override void Awake()
        {
            base.Awake();
            
            if (baseMesh == null)
            {
                baseMesh = CreateMesh(Mesh);
            }

            Mesh = baseMesh;
            //Color = Color.cyan.WithAlpha(0.95f);
            EmissiveColor = Color.WithAlpha(1);
            
            Position = startPosition;
            Transform.localScale = new Vector3(0.25f, 0.25f, 0.5f);
        }

        Vector3 Position
        {
            set => Transform.localPosition = value + 0.5f * Vector3.up;
        }

        void Update()
        {
            UpdateRotation();
        }
        
        void UpdateRotation()
        {
            (float x, _, float z) = Transform.position - Settings.MainCameraTransform.position;
            float targetAngle = -Mathf.Atan2(z, x) * Mathf.Rad2Deg + 90;

            if (currentAngle == null)
            {
                currentAngle = targetAngle;
            }
            else
            {
                float alternativeAngle = targetAngle - 360;
                float closestAngle = Mathf.Abs(alternativeAngle - currentAngle.Value) <
                                     Mathf.Abs(targetAngle - currentAngle.Value)
                    ? alternativeAngle
                    : targetAngle;

                float deltaAngle = closestAngle - currentAngle.Value;
                if (Mathf.Abs(deltaAngle) < 1)
                {
                    return;
                }

                currentAngle = currentAngle.Value + deltaAngle * 0.05f;
                if (currentAngle > 180)
                {
                    currentAngle -= 360;
                }
                else if (currentAngle < -180)
                {
                    currentAngle += 360;
                }
            }

            Transform.rotation = Quaternion.AngleAxis(currentAngle.Value, Vector3.up) 
                                 * Quaternions.Rotate270AroundX;
        }

        [NotNull]
        static Mesh CreateMesh([NotNull] Mesh sourceMesh)
        {
            var vertices = sourceMesh.vertices;
            var normals = sourceMesh.normals;
            int[] triangles = sourceMesh.triangles;
            var colors = vertices.Select(
                    vertex => Color.Lerp(Color.white.WithAlpha(0), Color.white, vertex.z))
                .ToArray();

            return new Mesh
            {
                name = "Arrow Mesh",
                vertices = vertices,
                normals = normals,
                colors = colors,
                triangles = triangles
            };
        }
    }
}
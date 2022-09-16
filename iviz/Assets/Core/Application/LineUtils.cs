#nullable enable

using System.Collections.Generic;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public static class LineUtils
    {
        public static void AddLineStipple(List<LineWithColor> lines, in Vector3 a, in Vector3 b, Color32? color = null,
            float stippleLength = 0.1f)
        {
            ThrowHelper.ThrowIfNull(lines, nameof(lines));

            var ab = b - a;
            float remainingLength = ab.Magnitude();
            var direction = ab / remainingLength;
            var advance = direction * stippleLength;
            var position = a;

            var advanceTwice = 2 * advance;
            float stippleLengthTwice = 2 * stippleLength;

            var validatedColor = color ?? Color.white;
            
            while (remainingLength >= 0)
            {
                if (remainingLength < stippleLength)
                {
                    lines.Add(new LineWithColor(position, b, validatedColor));
                    break;
                }

                lines.Add(new LineWithColor(position, position + advance, validatedColor));
                position += advanceTwice;
                remainingLength -= stippleLengthTwice;
            }
        }

        public static void AddCircleStipple(List<LineWithColor> lines, in Vector3 c, float radius, in Vector3 axis,
            Color color,
            int numStipples = 10)
        {
            Vector3 notAxis = Vector3.forward;
            if (notAxis.Cross(axis).ApproximatelyZero())
            {
                notAxis = Vector3.right;
            }

            Vector3 dirY = notAxis.Cross(axis).Normalized();
            Vector3 dirX = axis.Cross(dirY).Normalized();
            dirX *= radius;
            dirY *= radius;

            float coeff = Mathf.PI / numStipples;
            for (int i = 1; i <= 2 * numStipples + 1; i += 2)
            {
                float a, ax, ay;

                a = i * coeff;
                ax = Mathf.Cos(a);
                ay = Mathf.Sin(a);
                Vector3 v0 = ax * dirX + ay * dirY;

                a += coeff;
                ax = Mathf.Cos(a);
                ay = Mathf.Sin(a);
                Vector3 v1 = ax * dirX + ay * dirY;

                lines.Add(new LineWithColor(c + v0, c + v1, color));
            }
        }
    }
}
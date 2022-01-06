#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public static class LineUtils
    {
        public static void AddLineStipple(List<LineWithColor> lines, in Vector3 a, in Vector3 b, Color color,
            float stippleLength = 0.1f)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            float remainingLength = (b - a).Magnitude();
            Vector3 direction = (b - a) / remainingLength;
            Vector3 advance = direction * stippleLength;
            Vector3 position = a;

            while (true)
            {
                if (remainingLength < 0)
                {
                    break;
                }

                if (remainingLength < stippleLength)
                {
                    lines.Add(new LineWithColor(position, b, color));
                    break;
                }

                lines.Add(new LineWithColor(position, position + advance, color));
                position += 2 * advance;
                remainingLength -= 2 * stippleLength;
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
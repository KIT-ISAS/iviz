using System.IO;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.MarkerDetection;
using Iviz.Msgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Tools;
using Iviz.XmlRpc;
using UnityEngine;
using Transform = Iviz.Msgs.GeometryMsgs.Transform;

namespace Iviz.App.Tests
{
    /*
{"IvizE":0,"Size":176,"Tf":"table1"}
{"IvizE":1,"Size":176}

     */
    public class OpencvTestScript : MonoBehaviour
    {
        ARMarkerExecutor executor = new ARMarkerExecutor();

        void Start()
        {
            //byte[] bytes = File.ReadAllBytes("/Users/akzeac/Downloads/IMG_7764.JPG");
            //byte[] bytes = File.ReadAllBytes("/Users/akzeac/Downloads/IMG_0655.JPG");
            byte[] bytes = File.ReadAllBytes("/Users/akzeac/Downloads/IMG_0669.JPG");
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);


            CvContext cvContext = new CvContext(texture.width, texture.height);


            cvContext.SetImageDataFlipY(texture.GetRawTextureData(), 3);
            var qrs = cvContext.DetectQrMarkers();
            if (qrs.Length == 0)
            {
                return;
            }

            var qr = qrs[0];

            DrawLine(texture,
                0, 0,
                texture.width - 1, 0);
            DrawLine(texture,
                (int) qr.Corners[0].X, texture.height - 1 - (int) qr.Corners[0].Y,
                (int) qr.Corners[1].X, texture.height - 1 - (int) qr.Corners[1].Y, Color.blue);
            DrawLine(texture,
                (int) qr.Corners[1].X, texture.height - 1 - (int) qr.Corners[1].Y,
                (int) qr.Corners[2].X, texture.height - 1 - (int) qr.Corners[2].Y);
            DrawLine(texture,
                (int) qr.Corners[2].X, texture.height - 1 - (int) qr.Corners[2].Y,
                (int) qr.Corners[3].X, texture.height - 1 - (int) qr.Corners[3].Y);
            DrawLine(texture,
                (int) qr.Corners[3].X, texture.height - 1 - (int) qr.Corners[3].Y,
                (int) qr.Corners[0].X, texture.height - 1 - (int) qr.Corners[0].Y);


            var detectedMarker = new ARMarker
            {
                Type = (byte) ARMarkerType.QrCode,
                Header = new Header(0, default, TfListener.FixedFrameId),
                Code = qr.Code,
                CameraPose = TfListener.RelativePoseToFixedFrame(Pose.identity).Unity2RosPose()
                    .ToCameraFrame(),
                Corners = qr.Corners.Select(v => new Msgs.GeometryMsgs.Vector3(v.X, v.Y, 0)).ToArray(),
                CameraIntrinsic = new Intrinsic(3000, texture.width / 2f, 3000, texture.height / 2f).ToArray(),
            };

            executor.Process(detectedMarker);

            Msgs.GeometryMsgs.Pose localPoseInRos = Transform.Identity;
            {
                var position = localPoseInRos.Position;
                float x = (float) (position.X / position.Z) * 3000 + texture.width / 2f;
                float y = (float) (position.Y / position.Z) * 3000 + texture.height / 2f;
                DrawLine(texture,
                    (int) x - 15, texture.height - 1 - (int) y,
                    (int) x + 15, texture.height - 1 - (int) y);
                DrawLine(texture,
                    (int) x, texture.height - 1 - (int) y - 15,
                    (int) x, texture.height - 1 - (int) y + 15);
            }

            const float size = 0.175f;
            var objectCorners = new Iviz.Msgs.GeometryMsgs.Vector3[]
            {
                (-size / 2, size / 2, 0),
                (size / 2, size / 2, 0),
                (size / 2, -size / 2, 0),
                (-size / 2, -size / 2, 0),
            };

            for (int i = 0; i < 4; i++)
            {
                var position = (Msgs.GeometryMsgs.Transform) localPoseInRos * objectCorners[i];

                float x = (float) (position.X / position.Z) * 3000 + texture.width / 2f;
                float y = (float) (position.Y / position.Z) * 3000 + texture.height / 2f;
                DrawLine(texture,
                    (int) x - 15, texture.height - 1 - (int) y,
                    (int) x + 15, texture.height - 1 - (int) y);
                DrawLine(texture,
                    (int) x, texture.height - 1 - (int) y - 15,
                    (int) x, texture.height - 1 - (int) y + 15);
            }

            byte[] dstBytes = texture.EncodeToPNG();
            File.WriteAllBytes("/Users/akzeac/Downloads/IMG_0669__2.JPG", dstBytes);
        }

        static void DrawLine(Texture2D t, int x0, int y0, int x1, int y1, Color? color = null)
        {
            Color ccolor = color ?? Color.red;
            int dx = Mathf.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = -Mathf.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = dx + dy, e2; /* error value e_xy */

            while (true)
            {
                t.SetPixel(x0, y0, ccolor);
                if (x0 == x1 && y0 == y1) break;
                e2 = 2 * err;
                if (e2 > dy)
                {
                    err += dy;
                    x0 += sx;
                } /* e_xy+e_x > 0 */

                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                } /* e_xy+e_y < 0 */
            }
        }
    }
}
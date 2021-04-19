using System.IO;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Msgs.StdMsgs;
using UnityEngine;

namespace Iviz.MarkerDetection
{
    public class OpencvTestScript : MonoBehaviour
    {
        ARMarkerExecutor executor = new ARMarkerExecutor();

        void Start()
        {
            //byte[] bytes = File.ReadAllBytes("/Users/akzeac/Downloads/IMG_7764.JPG");
            //byte[] bytes = File.ReadAllBytes("/Users/akzeac/Downloads/IMG_0655.JPG");
            byte[] bytes = File.ReadAllBytes("/Users/akzeac/Downloads/IMG_8109.JPG");
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);

            
            CvContext cvContext = new CvContext(texture.width, texture.height);


            cvContext.SetImageDataFlipY(texture.GetRawTextureData(), 3);
            int numQrs = cvContext.DetectQrMarkers();
            if (numQrs == 0)
            {
                return;
            }

            var qr = cvContext.GetDetectedQrCorners()[0];



            var detectedMarker = new DetectedARMarker
            {
                MarkerType = ARMarkerType.QrCode,
                Header = new Header(0, default, TfListener.FixedFrameId ?? ""),
                ArucoId = 0,
                QrCode = qr.Code,
                CameraPose = TfListener.RelativePoseToFixedFrame(Pose.identity).Unity2RosPose()
                    .ToCameraFrame(),
                Corners = qr.Corners.ToArray(),
                Intrinsic = new Intrinsic(3000, texture.width / 2f, 3000, texture.height / 2f)
            };
            
            executor.Execute(detectedMarker);

        }
    }
}
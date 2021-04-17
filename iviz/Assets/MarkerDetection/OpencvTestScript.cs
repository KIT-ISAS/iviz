using System.IO;
using UnityEngine;

namespace Iviz.MarkerDetection
{
    public class OpencvTestScript : MonoBehaviour
    {
        void Start()
        {
            //byte[] bytes = File.ReadAllBytes("/Users/akzeac/Downloads/IMG_7764.JPG");
            byte[] bytes = File.ReadAllBytes("/Users/akzeac/Downloads/IMG_0655.JPG");
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);

            
            CvContext cvContext = new CvContext(texture.width, texture.height);


            cvContext.SetImageDataFlipY(texture.GetRawTextureData(), 3);
            
            /*
            int numArucos = context.DetectArucoMarkers();
            Debug.Log(numArucos);
            Debug.Log(string.Join(", ", context.GetDetectedArucoIds()));
            Debug.Log(string.Join(", ", context.GetDetectedArucoCorners()));
            */
            int numQrs = cvContext.DetectQrMarkers();
            Debug.Log(numQrs);
            Debug.Log(string.Join(", ", cvContext.GetDetectedQrCodes()));
            //Debug.Log(string.Join(", ", cvContext.GetDetectedQrCorners()));
        }
    }
}
using System.IO;
using UnityEngine;

namespace Iviz.Opencv
{
    public class OpencvTestScript : MonoBehaviour
    {
        void Start()
        {
            //byte[] bytes = File.ReadAllBytes("/Users/akzeac/Downloads/IMG_7764.JPG");
            byte[] bytes = File.ReadAllBytes("/Users/akzeac/Downloads/IMG_0655.JPG");
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);

            
            Context context = new Context(texture.width, texture.height);


            context.SetImageDataFlipY(texture.GetRawTextureData<byte>());
            
            /*
            int numArucos = context.DetectArucoMarkers();
            Debug.Log(numArucos);
            Debug.Log(string.Join(", ", context.GetDetectedArucoIds()));
            Debug.Log(string.Join(", ", context.GetDetectedArucoCorners()));
            */
            int numQrs = context.DetectQrMarkers();
            Debug.Log(numQrs);
            Debug.Log(string.Join(", ", context.GetDetectedQrCodes()));
            Debug.Log(string.Join(", ", context.GetDetectedQrCorners()));
        }
    }
}
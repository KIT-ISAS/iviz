using System.IO;
using UnityEngine;

namespace Iviz.Opencv
{
    public class OpencvTestScript : MonoBehaviour
    {
        void Start()
        {
            byte[] bytes = File.ReadAllBytes("/Users/akzeac/Downloads/IMG_7764.JPG");
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);

            
            Context context = new Context(texture.width, texture.height);


            context.SetImageData(texture.GetRawTextureData<byte>());
            
            int numArucos = context.DetectArucoMarkers();
            Debug.Log(numArucos);
            Debug.Log(string.Join(", ", context.GetDetectedArucoIds()));
        }
    }
}
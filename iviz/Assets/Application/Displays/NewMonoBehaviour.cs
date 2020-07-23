using Iviz.Displays;
using UnityEngine;

namespace Iviz.App
{
    public class NewMonoBehaviour : MonoBehaviour
    {
        public Mesh mesh;
        void Start()
        {
            PointWithColor[] points = new PointWithColor[10];
            for (int i = 0; i < 10; i++)
            {
                points[i] = new PointWithColor(new Vector3(i, 1, 0), 1.0f);
            }

            MeshListResource resource = GetComponent<MeshListResource>();
            resource.Mesh = mesh;
            resource.PointsWithColor = points;
            resource.OcclusionOnly = false;
        }
        
    }


}
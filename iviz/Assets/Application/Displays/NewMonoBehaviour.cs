using UnityEngine;
using System.Collections;
using Application.Displays;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;

namespace Iviz.App
{


    public class NewMonoBehaviour : MonoBehaviour
    {
        //OccupancyGridResource resource;
        AnchorLine resource;

        private void Start()
        {
            transform.position = new Vector3(0, 2, 0);
            resource = GetComponent<AnchorLine>();

            resource.FindAnchor = (in Vector3 position, out Vector3 anchor, out Vector3 normal) =>
            {
                anchor = new Vector3(position.x, 0, position.z);
                normal = Vector3.up;
                return true;
            };
            resource.SetPosition(new Vector3(0, 2, 0), true);

            //resource.Set(Quaternion.Euler(30, 270, 40));
            //resource.Set(new Vector3(0, 1, 1) * 20);
            //transform.position = new Vector3(1, 0, 0).Ros2Unity();
            /*
            resource = gameObject.AddComponent<OccupancyGridResource>();
            resource.NumCellsX = 1000;
            resource.NumCellsY = 1000;
            resource.CellSize = 0.01f;

            sbyte[] bytes = new sbyte[resource.NumCellsX * resource.NumCellsY];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (sbyte)(Mathf.Sin(i) * 100);
            }

            resource.SetOccupancy(bytes);
            */
            /*
            ArrowResource resource = GetComponent<ArrowResource>();
            resource.Set(new Vector3(0, 3, 0), new Vector3(-2, 2, 2));
            */
        }

        void Update()
        {
            resource.Position = transform.position;
        }
    }
}
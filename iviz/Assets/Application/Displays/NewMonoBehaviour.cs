using UnityEngine;
using System.Collections;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;

namespace Iviz.App
{


    public class NewMonoBehaviour : MonoBehaviour
    {
        OccupancyGridResource resource;

        private void Start()
        {
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
    }
}
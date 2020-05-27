using UnityEngine;
using System.Collections;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;

namespace Iviz.App
{


    public class NewMonoBehaviour : MonoBehaviour
    {
        Mesh mesh;
        RosSender<MarkerArray> sender;
        // Use this for initialization
        void Start()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            Debug.Log(mesh);
            sender = new RosSender<MarkerArray>("/iviz/markers");
        }

        /*
        private void OnEnable()
        {
            if (mesh == null) return;
            MarkerArray array = MeshToMarker(mesh, transform.AsPose());
            sender?.Publish(array);
        }
        */


        // Update is called once per frame
        void Update()
        {

        }
    }
}
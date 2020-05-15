using UnityEngine;
using System.Collections;
using Iviz.App.Displays;
using System.Collections.Generic;

namespace Iviz.App {

    public class NewMonoBehaviour : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            List<LineWithColor> ls = new List<LineWithColor>();
            ls.Add(new LineWithColor(
                new Vector3(0, 0, 0),
                new Vector3(1, 1, 1),
                Color.red
                ));
            ls.Add(new LineWithColor(
                new Vector3(1, 1, 1),
                new Vector3(2, 0, 2),
                Color.blue
                ));
            LineResource resource = GetComponent<LineResource>();
            resource.Scale = 0.01f;
            resource.Set(ls);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
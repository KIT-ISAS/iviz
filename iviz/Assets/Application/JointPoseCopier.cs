using UnityEngine;
using RosSharp.Urdf;
using System.Linq;

public class JointPoseCopier : MonoBehaviour
{
    public GameObject source;

    void Start()
    {
        source.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(x => x.enabled = false);

        UrdfJoint[] sourceJoints = source.GetComponentsInChildren<UrdfJoint>();
        foreach (UrdfJoint urdfJoint in sourceJoints)
        {
            string jointName = urdfJoint.name;
            Transform displayTransform = transform.Find(jointName);
            if (displayTransform == null)
            {
                //Debug.Log("Cannot find child " + jointName);
            }
            else
            {
                //displayTransform.GetComponentInChildren<MeshRenderer>().receiveShadows = false;
                //displayTransform.GetComponentInChildren<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                displayTransform.transform.parent = urdfJoint.transform;
                displayTransform.transform.localPosition = Vector3.zero;
                displayTransform.transform.localRotation = Quaternion.identity;

                Transform defaultChild = displayTransform.Find("default");
                if (defaultChild != null)
                {
                    defaultChild.localRotation = Quaternion.Euler(-90, 90, 0);
                }
            }
        }
    }
}

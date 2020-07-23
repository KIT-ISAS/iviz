using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Urdf;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.App
{
    public class RobotModel
    {
        public string Root { get; }

        readonly Dictionary<string, GameObject> linkObjects = new Dictionary<string, GameObject>();
        readonly Dictionary<string, GameObject> jointObjects = new Dictionary<string, GameObject>();
        readonly Dictionary<string, string> parents = new Dictionary<string, string>();
        readonly Dictionary<string, Urdf.Joint> joints = new Dictionary<string, Urdf.Joint>();
        readonly Urdf.Robot robot;

        public RobotModel(string robotDescription)
        {
            //string robotDescription = ConnectionManager.Connection.GetParameter("e1_description");
            //string robotDescription =
            //    File.ReadAllText("/Users/akzeac/Documents/iviz/iviz/Assets/Resources/Packages/iosb/urdf/iosb.urdf.xml");
            robot = Urdf.Robot.Create(robotDescription);

            foreach (var link in robot.Links)
            {
                Debug.Log(link.Name);
                GameObject obj = new GameObject("Link:" + link.Name);
                linkObjects[link.Name] = obj;

                foreach (Visual visual in link.Visuals)
                {
                    Geometry geometry = visual.Geometry;

                    GameObject visualObj = new GameObject
                    (
                        name: visual.Name != null ? $"[Visual:{visual.Name}]" : "[Visual]"
                    );
                    visualObj.transform.SetParent(obj.transform, false);
                    visualObj.transform.SetLocalPose(visual.Origin.ToPose());


                    if (geometry.Mesh != null)
                    {
                        Uri uri = new Uri(geometry.Mesh.Filename);
                        if (!Resource.TryGetResource(uri, out Resource.Info<GameObject> info))
                        {
                            continue;
                        }

                        GameObject resourceObj = ResourcePool.GetOrCreate(info);
                        resourceObj.transform.SetParent(visualObj.transform, false);
                        visualObj.transform.localScale = geometry.Mesh.Scale.ToVector3().Abs();
                    }
                }
            }

            foreach (var joint in robot.Joints)
            {
                GameObject jointObj = new GameObject("{Joint:" + joint.Name + "}");
                jointObjects[joint.Name] = jointObj;

                GameObject originObj = new GameObject($"[Origin:{joint.Name}]");

                GameObject parent = linkObjects[joint.Parent.Link];
                GameObject child = linkObjects[joint.Child.Link];

                if (parent.transform.IsChildOf(child.transform))
                {
                    Stop();
                    throw new MalformedUrdfException();
                }

                parents[joint.Child.Link] = joint.Parent.Link;
                joints[joint.Name] = joint;

                originObj.transform.SetParent(parent.transform, false);
                jointObj.transform.SetParent(originObj.transform, false);
                child.transform.SetParent(jointObj.transform, false);

                originObj.transform.SetLocalPose(joint.Origin.ToPose());
            }

            if (linkObjects.Count == 0)
            {
                return;
            }

            Root = parents.FirstOrDefault(x => x.Value is null).Key;
        }

        public void Stop()
        {
            foreach (var link in linkObjects.Values)
            {
                UnityEngine.Object.Destroy(link.gameObject);
            }

            foreach (var joint in jointObjects.Values)
            {
                UnityEngine.Object.Destroy(joint.gameObject);
            }
        }

        public bool TryApplyJoint(string jointName, float value, out Pose unityPose)
        {
            if (!joints.TryGetValue(jointName, out Urdf.Joint joint))
            {
                unityPose = new Pose();
                return false;
            }

            switch (joint.Type)
            {
                case Urdf.Joint.JointType.Revolute:
                case Urdf.Joint.JointType.Continuous:
                    unityPose = new Pose(Vector3.zero, Quaternion.AngleAxis(value, joint.Axis.Xyz.ToVector3()));
                    break;
                case Urdf.Joint.JointType.Prismatic:
                    unityPose = new Pose(joint.Axis.Xyz.ToVector3() * value, Quaternion.identity);
                    break;
                case Urdf.Joint.JointType.Fixed:
                    unityPose = Pose.identity;
                    break;
                default:
                    unityPose = Pose.identity;
                    return false;
            }

            return true;
        }
    }
    
    static class UrdfUtils
    {
        public static Vector3 ToVector3(this Urdf.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z).Ros2Unity();
        }

        static Quaternion ToQuaternion(this Urdf.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z).RosRpy2Unity();
        }

        public static Pose ToPose(this Urdf.Origin v)
        {
            return new Pose(v.Xyz.ToVector3(), v.Rpy.ToQuaternion());
        }
    }    
}
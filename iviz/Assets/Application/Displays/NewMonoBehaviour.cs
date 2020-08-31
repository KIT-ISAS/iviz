using System.IO;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Roslib;
using Iviz.Sdf;
using UnityEngine;
using Pose = Iviz.Sdf.Pose;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.App
{
    public class NewMonoBehaviour : MonoBehaviour
    {
        void OnEnable()
        {
            //string robotDescription = File.ReadAllText("/Users/akzeac/Documents/iviz/iviz/Assets/Robots/neo_mp_500/robot_model/mp_500.urdf.xml");            
            //string robotDescription = File.ReadAllText("/Users/akzeac/Documents/iviz/iviz/Assets/Robots/pr2_description/robots/robot.urdf.xml");            
            //string robotDescription = File.ReadAllText("/Users/akzeac/Documents/iviz/iviz/Assets/Robots/crayler/urdf/crayler_high_res.urdf.xml");            
            //string robotDescription = File.ReadAllText("/Users/akzeac/Documents/iviz/iviz/Assets/Robots/iosb/urdf/iosb.urdf.xml");            
            //RobotModel robot = new RobotModel(robotDescription);
            //robot.BaseLinkObject.transform.position = new Vector3(-2, 0, 0);

            //robot.TryApplyJoint("boom_revolute", 0.5f, out Pose _, true);
            //robot.ApplyCosmeticConfiguration();

            string packagePath = "/Users/akzeac/Shared/aws-robomaker-hospital-world";
            string localPath = "/worlds/hospital.world";
            
            string xmlData = File.ReadAllText(packagePath + localPath);
            SdfFile sdf = SdfFile.Create(xmlData);
            
            var modelPaths = SdfFile.CreateModelPaths(packagePath);
            SdfFile newSdf = sdf.ResolveIncludes(modelPaths);
            
            CreateWorld(newSdf.Worlds[0]);
        }

        void CreateWorld(World world)
        {
            GameObject worldObject = new GameObject("World:" + world.Name);
            
            foreach (Model model in world.Models)
            {
                CreateModel(model)?.transform.SetParent(worldObject.transform, false);
            }
            
        }
        GameObject CreateModel(Model model)
        {
            GameObject modelObject = new GameObject("Model:" + model.Name);
            UnityEngine.Pose pose = model.Pose?.ToPose() ?? UnityEngine.Pose.identity;
            UnityEngine.Pose includePose = model.IncludePose?.ToPose() ?? UnityEngine.Pose.identity;
            modelObject.transform.SetLocalPose(includePose.Multiply(pose));

            if (model.Models is null || model.Links is null)
            {
                // invalid
                return modelObject;
            }
            
            foreach (Model innerModel in model.Models)
            {
                CreateModel(innerModel)?.transform.SetParent(modelObject.transform, false);
            }

            foreach (Link link in model.Links)
            {
                CreateLink(link)?.transform.SetParent(modelObject.transform, false);
            }
            
            return modelObject;
        }
        
        GameObject CreateLink(Link link)
        {
            GameObject linkObject = new GameObject("Link:" + link.Name);
            linkObject.transform.SetLocalPose(link.Pose?.ToPose() ?? UnityEngine.Pose.identity);

            foreach (Visual visual in link.Visuals)
            {
                Geometry geometry = visual.Geometry;

                GameObject visualObject = new GameObject
                (
                    name: visual.Name != null ? $"[Visual:{visual.Name}]" : "[Visual]"
                );
                visualObject.transform.SetParent(linkObject.transform, false);

                GameObject resourceObject = null;
                bool isSynthetic = false;
                if (geometry.Mesh != null)
                {
                    System.Uri uri = geometry.Mesh.Uri.ToUri();
                    if (!Resource.TryGetResource(uri, out Resource.Info<GameObject> info))
                    {
                        Debug.Log("Robot: Failed to retrieve " + uri);
                        continue;
                    }

                    resourceObject = ResourcePool.GetOrCreate(info);
                    resourceObject.transform.SetParent(visualObject.transform, false);
                    visualObject.transform.localScale = geometry.Mesh.Scale?.ToVector3().Abs() ?? Vector3.one;
                    isSynthetic = true;
                }
                else if (geometry.Cylinder != null)
                {
                    resourceObject = ResourcePool.GetOrCreate(Resource.Displays.Cylinder);
                    resourceObject.transform.SetParent(visualObject.transform, false);
                    visualObject.transform.localScale = new Vector3(
                        (float)geometry.Cylinder.Radius * 2,
                        (float)geometry.Cylinder.Length,
                        (float)geometry.Cylinder.Radius * 2);
                }
                else if (geometry.Box != null)
                {
                    resourceObject = ResourcePool.GetOrCreate(Resource.Displays.Cube);
                    resourceObject.transform.SetParent(visualObject.transform, false);
                    visualObject.transform.localScale = geometry.Box.Scale?.ToVector3().Abs() ?? Vector3.one;
                }
                else if (geometry.Sphere != null)
                {
                    resourceObject = ResourcePool.GetOrCreate(Resource.Displays.Sphere);
                    resourceObject.transform.SetParent(visualObject.transform, false);
                    visualObject.transform.localScale = (float)geometry.Sphere.Radius * Vector3.one;
                }

                if (resourceObject is null)
                {
                    continue; //?
                }
            }

            return linkObject;
        }

        
        
        static UnityEngine.Pose ToPose(Sdf.Pose pose)
        {
            if (pose is null)
            {
                return UnityEngine.Pose.identity;
            }
            
            Msgs.GeometryMsgs.Point xyz = new Msgs.GeometryMsgs.Point(pose.Position.X, pose.Position.Y, pose.Position.Z);
            Vector3 rpy = new Vector3((float)pose.Orientation.X, (float)pose.Orientation.Y, (float)pose.Orientation.Z);
            
            return new UnityEngine.Pose(xyz.Ros2Unity(), rpy.RosRpy2Unity());
        } 

    }


}
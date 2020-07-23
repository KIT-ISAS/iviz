using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
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
            string robotDescription = File.ReadAllText("/Users/akzeac/Documents/iviz/iviz/Assets/Robots/iosb/urdf/iosb.urdf.xml");            
            RobotModel robot = new RobotModel(robotDescription);
            robot.BaseLinkObject.transform.position = new Vector3(-2, 0, 0);

            //robot.TryApplyJoint("boom_revolute", 0.5f, out Pose _, true);
            //robot.ApplyCosmeticConfiguration();
        }
    }


}
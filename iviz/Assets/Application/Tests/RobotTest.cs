using System;
using System.Collections;
using System.Linq;
using Iviz.Common;
using UnityEngine;

namespace Iviz.App.Tests
{
    public class RobotTest : MonoBehaviour
    {
        void Start()
        {
            var robotData = ModuleListPanel.Instance.ModuleDatas.FirstOrDefault(data => data.ModuleType == ModuleType.Robot);
            if (robotData == null)
            {
                Debug.LogWarning("No robot selected!");
                return;
            }

            var robotModel = ((SimpleRobotModuleData)robotData).RobotController.Robot;
            if (robotModel == null)
            {
                Debug.LogWarning("Robot is empty!");
                return;
            }

            var otherRobot = robotModel.Clone();
            _ = otherRobot.StartAsync(loadColliders: false);
            Debug.LogWarning("finished!");
        }
        
    }
}
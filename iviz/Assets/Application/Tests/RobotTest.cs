using System;
using System.Collections;
using System.Collections.Generic;
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

    public readonly struct NonRetardedEnumerable<T>
    {
        readonly List<T> list;

        public struct Enumerator
        {
            readonly List<T> list;
            int index;

            public Enumerator(List<T> list)
            {
                this.list = list;
                index = 0;
            }

            public bool MoveNext() => ++index < list.Count;
            public void Reset() => index = -1;
            public T Current => list[index];
        }
    }
}
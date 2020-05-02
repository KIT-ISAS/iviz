using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Iviz.App
{
    public class GameThread : MonoBehaviour
    {
        readonly List<Action> actionsOnlyOnce = new List<Action>();
        readonly List<Action> actionsOnlyOnceTmp = new List<Action>();
        DateTime lastRunTime;

        public static event Action EveryFrame;
        public static event Action EverySecond;

        static GameThread Instance;

        void Awake()
        {
            Instance = this;
            lastRunTime = DateTime.Now;
        }

        void OnDestroy()
        {
            Instance = null;
        }

        public static void RunOnce(Action action)
        {
            if (Instance != null)
            {
                lock (Instance.actionsOnlyOnce)
                {
                    Instance.actionsOnlyOnce.Add(action);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            EveryFrame?.Invoke();

            DateTime newRunTime = DateTime.Now;
            if ((newRunTime - lastRunTime).TotalMilliseconds > 1000)
            {
                EverySecond?.Invoke();
                lastRunTime = newRunTime;
            }

            lock (actionsOnlyOnce)
            {
                if (!actionsOnlyOnce.Any()) return;
                actionsOnlyOnceTmp.Clear();
                actionsOnlyOnceTmp.AddRange(actionsOnlyOnce);
                actionsOnlyOnce.Clear();
            }
            actionsOnlyOnceTmp.ForEach(x => x());
        }

    }
}
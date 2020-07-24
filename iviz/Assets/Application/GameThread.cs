using UnityEngine;
using System.Collections.Generic;
using System;

namespace Iviz.Displays
{
    public class GameThread : MonoBehaviour
    {
        readonly List<Action> actionsOnlyOnce = new List<Action>();
        readonly List<Action> actionsOnlyOnceTmp = new List<Action>();
        float lastRunTime;

        public static event Action EveryFrame;
        public static event Action LateEveryFrame;
        public static event Action EverySecond;
        public static event Action LateEverySecond;
        
        static GameThread mInstance;
        static GameThread Instance
        {
            get
            {
                if (!(mInstance is null))
                {
                    return mInstance;
                }

                mInstance = new GameObject("GameThread").AddComponent<GameThread>();
                mInstance.lastRunTime = Time.time;
                return mInstance;
            }
        }

        void OnDestroy()
        {
            mInstance = null;
        }

        public static void RunOnce(Action action)
        {
            if (Instance is null)
            {
                return;
            }

            lock (Instance.actionsOnlyOnce)
            {
                Instance.actionsOnlyOnce.Add(action);
            }
        }

        // Update is called once per frame
        void Update()
        {
            EveryFrame?.Invoke();

            float newRunTime = Time.time;
            if (newRunTime - lastRunTime > 1)
            {
                EverySecond?.Invoke();
                LateEverySecond?.Invoke();
                lastRunTime = newRunTime;
            }

            lock (actionsOnlyOnce)
            {
                if (actionsOnlyOnce.Count == 0) return;
                actionsOnlyOnceTmp.Clear();
                actionsOnlyOnceTmp.AddRange(actionsOnlyOnce);
                actionsOnlyOnce.Clear();
            }
            actionsOnlyOnceTmp.ForEach(x => x());
        }
        
        void LateUpdate()
        {
            LateEveryFrame?.Invoke();
        }

    }
}
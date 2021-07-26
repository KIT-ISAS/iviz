using System;
using System.Collections.Concurrent;
using System.Threading;
using Iviz.Msgs;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Core
{
    /// <summary>
    /// Singleton that other threads can use to post actions to the main thread.
    /// </summary>
    public class GameThread : MonoBehaviour
    {
        static GameThread Instance;
        readonly ConcurrentQueue<Action> actionsQueue = new ConcurrentQueue<Action>();
        readonly ConcurrentQueue<Action> listenerQueue = new ConcurrentQueue<Action>();
        float lastSecondRunTime;
        float lastTickRunTime;
        Thread gameThread; // only used for id
        int counter;

        static int networkFrameSkip = Settings.IsHololens ? 2 : 1;

        public static int NetworkFrameSkip
        {
            get => networkFrameSkip;
            set => networkFrameSkip =
                (value >= 1) ? value : throw new ArgumentException($"Invalid argument {value}");
        }

        public static float GameTime { get; private set; }

        public static DateTime Now { get; private set; } = DateTime.Now;

        [CanBeNull] static string nowFormatted;
        [NotNull] public static string NowFormatted => nowFormatted ?? (nowFormatted = Now.ToString("HH:mm:ss.fff"));

        void Awake()
        {
            Instance = this;
            gameThread = Thread.CurrentThread;
        }

        void Update()
        {
            try
            {
                EveryFrame?.Invoke();
            }
            catch (Exception e)
            {
                Logger.Error($"{this}: Error during EveryFrame" + e);
            }

            Now = DateTime.Now;
            nowFormatted = null;
            GameTime = Time.time;
            if (GameTime - lastSecondRunTime > 1)
            {
                try
                {
                    EverySecond?.Invoke();
                }
                catch (Exception e)
                {
                    Logger.Error($"{this}: Error during EverySecond", e);
                }

                try
                {
                    LateEverySecond?.Invoke();
                }
                catch (Exception e)
                {
                    Logger.Error($"{this}: Error during LateEverySecond" + e);
                }

                lastSecondRunTime = GameTime;
            }
            
            if (GameTime - lastTickRunTime > 0.1f)
            {
                try
                {
                    EveryFastTick?.Invoke();
                }
                catch (Exception e)
                {
                    Logger.Error($"{this}: Error during EveryFastTick", e);
                }

                lastTickRunTime = GameTime;
            }            

            while (actionsQueue.TryDequeue(out Action action))
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Logger.Error($"{this}: Error during action call", e);
                }
            }

            counter++;
            if (counter < NetworkFrameSkip)
            {
                return;
            }

            try
            {
                ListenersEveryFrame?.Invoke();
            }
            catch (Exception e)
            {
                Logger.Error($"{this}: Error during ListenersEveryFrame", e);
            }

            int queueSize = listenerQueue.Count;
            for (int i = 0; i < queueSize; i++)
            {
                if (!listenerQueue.TryDequeue(out Action action))
                {
                    break;
                }

                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Logger.Error($"{this}: Error during listener action call", e);
                }
            }

            counter = 0;
        }

        [NotNull]
        public override string ToString()
        {
            return "[GameThread]";
        }

        void LateUpdate()
        {
            try
            {
                LateEveryFrame?.Invoke();
            }
            catch (Exception e)
            {
                Logger.Error($"{this}: Error during LateEveryFrame", e);
            }
        }

        void OnDestroy()
        {
            Instance = null;
            EveryFrame = null;
            ListenersEveryFrame = null;
            LateEveryFrame = null;
            EverySecond = null;
            LateEverySecond = null;

            while (actionsQueue.Count != 0)
            {
                actionsQueue.TryDequeue(out _);
            }
        }

        /// <summary>
        /// Runs every frame on the Unity thread.
        /// </summary>
        public static event Action EveryFrame;

        /// <summary>
        /// Runs every frame on the Unity thread, but may be slowed down if the CPU load is too high.
        /// Used by the ROS listeners.
        /// </summary>
        public static event Action ListenersEveryFrame;

        /// <summary>
        /// Runs every frame on the Unity thread, but after EveryFrame has finished.
        /// </summary>
        public static event Action LateEveryFrame;

        /// <summary>
        /// Runs once per second.
        /// </summary>
        public static event Action EverySecond;

        public static event Action EveryFastTick;

        /// <summary>
        /// Runs once per second, but after EverySecond has finished.
        /// </summary>
        public static event Action LateEverySecond;

        /// <summary>
        /// Puts this action in a queue to be run on the main thread.
        /// If it is already on the main thread, it will run on the next frame.
        /// </summary>
        /// <param name="action">Action to be run.</param>
        /// <exception cref="ArgumentNullException">If action is null.</exception>
        public static void Post([NotNull] Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (Instance == null)
            {
                return;
            }

            Instance.actionsQueue.Enqueue(action);
        }

        public static void PostInListenerQueue([NotNull] Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (Instance == null)
            {
                return;
            }

            Instance.listenerQueue.Enqueue(action);
        }

        /// <summary>
        /// Puts this action in a queue to be run on the main thread.
        /// If it is already on the main thread, runs it immediately.
        /// </summary>
        /// <param name="action">Action to be run.</param>
        /// <exception cref="ArgumentNullException">If action is null.</exception>        
        public static void PostImmediate([NotNull] Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (IsGameThread)
            {
                action();
                return;
            }

            if (Instance == null)
            {
                return;
            }

            Instance.actionsQueue.Enqueue(action);
        }

        static bool IsGameThread => Thread.CurrentThread == Instance.gameThread;
    }
}
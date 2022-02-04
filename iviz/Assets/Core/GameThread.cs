#nullable enable

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Core
{
    /// <summary>
    /// Singleton that other threads can use to post actions to the main thread.
    /// </summary>
    public class GameThread : MonoBehaviour
    {
        static GameThread? instance;
        static int networkFrameSkip = 1;
        static string? nowFormatted;
        
        static bool IsGameThread => instance != null && Thread.CurrentThread == instance.gameThread;
        
        /// <summary>
        /// How many frames <see cref="ListenersEveryFrame"/> should skip.
        /// A value of 1 means no skips, a value of 2 means every second frame, and so on.
        /// </summary>
        public static int NetworkFrameSkip
        {
            get => networkFrameSkip;
            set => networkFrameSkip =
                (value >= 1) ? value : throw new ArgumentException($"Invalid argument {value}");
        }

        /// <summary>
        /// Same as <see cref="Time.time"/> for the start of the frame, but can be accessed from any thread.
        /// </summary>
        public static float GameTime { get; private set; }

        /// <summary>
        /// DateTime timestamp of the start of the frame.
        /// </summary>
        public static DateTime Now { get; private set; } = DateTime.Now;

        /// <summary>
        /// ROS timestamp of the start of the frame. Takes into account NTP adjustment.
        /// </summary>
        public static time TimeNow { get; private set; } = time.Now();

        /// <summary>
        /// Cached current time as string.
        /// </summary>
        public static string NowFormatted => nowFormatted ??= Now.ToString("HH:mm:ss.fff");

        /// <summary>
        /// Runs every frame on the Unity thread.
        /// </summary>
        public static event Action? EveryFrame;

        /// <summary>
        /// Runs every frame on the Unity thread, but can be set to skip frames with <see cref="NetworkFrameSkip"/>
        /// if the CPU load is too high. Used by ROS listeners.
        /// </summary>
        public static event Action? ListenersEveryFrame;

        /// <summary>
        /// Runs every frame on the Unity thread, but after <see cref="EveryFrame"/> has finished.
        /// </summary>
        public static event Action? LateEveryFrame;

        /// <summary>
        /// Runs once per second.
        /// </summary>
        public static event Action? EverySecond;

        /// <summary>
        /// Runs multiple times per second.
        /// </summary>
        public static event Action? EveryTenthOfASecond;

        /// <summary>
        /// Runs once per second, but after <see cref="EverySecond"/> has finished.
        /// </summary>
        public static event Action? LateEverySecond;

        readonly ConcurrentQueue<Action> actionsQueue = new();
        readonly ConcurrentQueue<Action> listenerQueue = new();
        float lastSecondRunTime;
        float lastTickRunTime;
        Thread? gameThread; // only used for id
        int frameCounter;

        void Awake()
        {
            instance = this;
            GameTime = 0;
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
                RosLogger.Error($"{this}: Error during EveryFrame", e);
            }

            Now = DateTime.Now;
            TimeNow = time.Now();
            nowFormatted = null;
            GameTime = Time.time;
            if (GameTime - lastSecondRunTime > 1)
            {
                try
                {
                    if (Settings.IsIPhone)
                    {
                        // BUG!! IL2CPP crashes in IOS if EverySecond.Invoke() is called!
                        // I have no idea why only EverySecond, but in C+++ rgctxVar is null.
                        // I need to check in a few versions to see if it is fixed
                        if (EverySecond != null)
                        {
                            var delegates = EverySecond.GetInvocationList();
                            foreach (var @delegate in delegates)
                            {
                                var action = (Action)@delegate;
                                action();
                            }
                        }
                    }
                    else
                    {
                        EverySecond?.Invoke();
                    }
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error during EverySecond", e);
                }

                try
                {
                    LateEverySecond?.Invoke();
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error during LateEverySecond", e);
                }

                RosLogger.ResetCounter();
                lastSecondRunTime = GameTime;
            }

            if (GameTime - lastTickRunTime > 0.1f)
            {
                try
                {
                    EveryTenthOfASecond?.Invoke();
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error during EveryFastTick", e);
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
                    RosLogger.Error($"{this}: Error during action call", e);
                }
            }

            frameCounter++;
            if (frameCounter < NetworkFrameSkip)
            {
                return;
            }

            try
            {
                ListenersEveryFrame?.Invoke();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error during ListenersEveryFrame", e);
            }

            foreach (int _ in ..listenerQueue.Count)
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
                    RosLogger.Error($"{this}: Error during listener action call", e);
                }
            }

            frameCounter = 0;
        }

        public override string ToString() => "[GameThread]";

        void LateUpdate()
        {
            try
            {
                LateEveryFrame?.Invoke();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error during LateEveryFrame", e);
            }
        }

        void OnDestroy()
        {
            instance = null;
            EveryFrame = null;
            ListenersEveryFrame = null;
            LateEveryFrame = null;
            EverySecond = null;
            LateEverySecond = null;
            EveryTenthOfASecond = null;
            GameTime = 0;

            actionsQueue.Clear();
        }

        /// <summary>
        /// Puts this action in a queue to be run on the main thread.
        /// If it is already on the main thread, it will run on the next frame.
        /// </summary>
        /// <param name="action">Action to be run.</param>
        public static void Post(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (instance == null)
            {
                return;
            }

            instance.actionsQueue.Enqueue(action);
        }

        /// <summary>
        /// Puts this async action in a queue to be run on the main thread.
        /// If it is already on the main thread, it will run on the next frame -
        /// though the run order of async tasks is not very well defined.
        /// The return type is treated as async void. 
        /// </summary>
        /// <param name="action">Action to be run.</param>
        public static void Post(Func<Task> action) => Post(() => { action(); });

        /// <summary>
        /// Puts this async action in a queue to be run on the main thread.
        /// The listener queue may have less priority than <see cref="Post(System.Action)"/>
        /// If it is already on the main thread, it will run on the next frame.
        /// </summary>
        /// <param name="action">Action to be run.</param>
        public static void PostInListenerQueue(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (instance == null)
            {
                return;
            }

            instance.listenerQueue.Enqueue(action);
        }

        /// <summary>
        /// Puts this action in a queue to be run on the main thread.
        /// If it is already on the main thread, runs it immediately.
        /// </summary>
        /// <param name="action">Action to be run.</param>
        /// <exception cref="ArgumentNullException">If action is null.</exception>        
        public static void PostImmediate(Action action)
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

            if (instance != null)
            {
                instance.actionsQueue.Enqueue(action);
            }
        }
    }
}
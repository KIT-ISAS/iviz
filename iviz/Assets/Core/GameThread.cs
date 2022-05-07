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
    public sealed class GameThread : MonoBehaviour
    {
        static GameThread? instance;

        static GameThread Instance =>
            instance != null
                ? instance
                : instance = GameObject.Find("Game Thread").AssertHasComponent<GameThread>(nameof(Instance));

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

        public static event Action? ApplicationPause;

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

            int actionsCount = actionsQueue.Count; // avoid processing new entries
            foreach (int _ in ..actionsCount)
            {
                if (!actionsQueue.TryDequeue(out Action action))
                {
                    break;
                }

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

        public override string ToString() => $"[{nameof(GameThread)}]";

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

        void OnApplicationPause(bool pauseStatus)
        {
            ApplicationPause?.Invoke();
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
            ThrowHelper.ThrowIfNull(action, nameof(action));

            if (Settings.IsShuttingDown)
            {
                return;
            }

            Instance.actionsQueue.Enqueue(action);
        }

        /// <summary>
        /// Puts this async action in a queue to be run on the main thread.
        /// The function returns immediately.
        /// </summary>
        /// <param name="action">Action to be run.</param>
        public static void Post(Func<ValueTask> action) => Post(() => { action(); });

        /// <summary>
        /// Puts this async action in a queue to be run on the main thread,
        /// and returns a task that will be completed when the given action finishes running.
        /// </summary>
        /// <param name="action">Action to be run.</param>
        public static Task PostAsync(Func<ValueTask> action)
        {
            ThrowHelper.ThrowIfNull(action, nameof(action));
            var ts = TaskUtils.CreateCompletionSource();
            Post(() => TrySetAsync(ts, action()));
            return ts.Task;
            
            static async ValueTask TrySetAsync(TaskCompletionSource ts, ValueTask task)
            {
                try
                {
                    await task;
                    ts.TrySetResult();
                }
                catch (OperationCanceledException)
                {
                    ts.TrySetCanceled();
                }
                catch (Exception e)
                {
                    ts.TrySetException(e);
                }
            }  
        }
        
        /// <summary>
        /// Puts this async action in a queue to be run on the main thread,
        /// and returns a task that will be completed when the given action finishes running.
        /// </summary>
        /// <param name="action">Action to be run.</param>
        public static Task<T> PostAsync<T>(Func<ValueTask<T>> action)
        {
            ThrowHelper.ThrowIfNull(action, nameof(action));
            var ts = TaskUtils.CreateCompletionSource<T>();
            Post(() => TrySetAsync(ts, action()));
            return ts.Task;
            
            static async ValueTask TrySetAsync(TaskCompletionSource<T> ts, ValueTask<T> task)
            {
                try
                {
                    ts.TrySetResult(await task);
                }
                catch (OperationCanceledException)
                {
                    ts.TrySetCanceled();
                }
                catch (Exception e)
                {
                    ts.TrySetException(e);
                }
            }       
        }

        /// <summary>
        /// Puts this async action in a queue to be run on the main thread,
        /// and returns a task that will be completed when the given action finishes running.
        /// </summary>
        /// <param name="action">Action to be run.</param>
        public static Task PostAsync(Action action)
        {
            ThrowHelper.ThrowIfNull(action, nameof(action));
            var ts = TaskUtils.CreateCompletionSource();
            Post(() => TrySet(ts, action));
            return ts.Task;
            
            static void TrySet(TaskCompletionSource ts, Action action)
            {
                try
                {
                    action();
                    ts.TrySetResult();
                }
                catch (Exception e)
                {
                    ts.TrySetException(e);
                }
            }          
        }

        /// <summary>
        /// Puts this action in a queue to be run on the main thread.
        /// The listener queue can have less priority than <see cref="Post(System.Action)"/> and may skip several frames.
        /// If it is already on the main thread, it will run on the next frame.
        /// </summary>
        /// <param name="action">Action to be run.</param>
        public static void PostInListenerQueue(Action action)
        {
            ThrowHelper.ThrowIfNull(action, nameof(action));

            if (Settings.IsShuttingDown)
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
        public static void PostImmediate(Action action)
        {
            ThrowHelper.ThrowIfNull(action, nameof(action));

            if (IsGameThread)
            {
                action();
                return;
            }

            if (Settings.IsShuttingDown)
            {
                return;
            }

            Instance.actionsQueue.Enqueue(action);
        }
    }
}
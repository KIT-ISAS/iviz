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

        public static bool IsGameThread => instance != null && Thread.CurrentThread == instance.gameThread;

        /// <summary>
        /// How many frames <see cref="ListenersEveryFrame"/> should skip.
        /// A value of 1 means no skips, a value of 2 means every second frame, and so on.
        /// </summary>
        public static int NetworkFrameSkip
        {
            get => networkFrameSkip;
            set => networkFrameSkip =
                (value >= 1) ? value : throw new ArgumentException($"Invalid argument {value.ToString()}");
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

        public static event Action<bool>? ApplicationPaused;

        /// Called after all the TFs of the frame have been processed, but before <see cref="AfterFramesUpdatedLate"/>
        public static event Action? AfterFramesUpdated; // camera

        /// Called after all the TFs of the frame have been processed, but after <see cref="AfterFramesUpdated"/>
        public static event Action? AfterFramesUpdatedLate; // sprites


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
            Now = DateTime.Now;
            TimeNow = time.Now();
            nowFormatted = null;
            GameTime = Time.time;

            EveryFrame.TryRaise(this, nameof(EveryFrame));

            if (GameTime - lastSecondRunTime > 1)
            {
                EverySecond.TryRaise(this, nameof(EverySecond));
                LateEverySecond.TryRaise(this, nameof(LateEverySecond));

                RosLogger.ResetCounter();
                lastSecondRunTime = GameTime;
            }

            if (GameTime - lastTickRunTime > 0.1f)
            {
                EveryTenthOfASecond.TryRaise(this, nameof(EveryTenthOfASecond));
                lastTickRunTime = GameTime;
            }

            int actionsCount = actionsQueue.Count; // avoid processing new entries
            for (int i = 0; i < actionsCount; i++)
            {
                if (!actionsQueue.TryDequeue(out var action))
                {
                    break;
                }

                action.TryRaise(this, nameof(Post));
            }

            frameCounter++;
            if (frameCounter < NetworkFrameSkip)
            {
                return;
            }

            ListenersEveryFrame.TryRaise(this, nameof(ListenersEveryFrame));

            int listenerCount = listenerQueue.Count;
            for (int i = 0; i < listenerCount; i++)
            {
                if (!listenerQueue.TryDequeue(out Action action))
                {
                    break;
                }

                action.TryRaise(this, nameof(PostInListenerQueue));
            }

            frameCounter = 0;
        }

        public override string ToString() => $"[{nameof(GameThread)}]";

        void LateUpdate()
        {
            LateEveryFrame.TryRaise(this, nameof(LateEveryFrame));
        }

        void OnApplicationPause(bool pauseStatus)
        {
            ApplicationPaused.TryRaise(pauseStatus, this);
        }

        void OnDestroy()
        {
            instance = null;
            ClearResources();
            GameTime = 0;

            actionsQueue.Clear();
        }

        public static void ClearResources()
        {
            EveryFrame = null;
            ListenersEveryFrame = null;
            LateEveryFrame = null;
            EverySecond = null;
            LateEverySecond = null;
            EveryTenthOfASecond = null;
            AfterFramesUpdated = null;
            AfterFramesUpdatedLate = null;
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
        public static void Post(Func<ValueTask> action)
        {
            void Action() => _ = action();
            Post(Action);  
        } 

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
        }

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
        }

        static async ValueTask TrySetAsync<T>(TaskCompletionSource<T> ts, ValueTask<T> task)
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
        }

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
                action.TryRaise(nameof(GameThread));
                return;
            }

            if (Settings.IsShuttingDown)
            {
                return;
            }

            Instance.actionsQueue.Enqueue(action);
        }

        public static Task WaitUntilAsync(Func<bool> predicate)
        {
            bool b0;
            try
            {
                b0 = predicate();
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }

            if (b0)
            {
                return Task.CompletedTask;
            }

            var ts = new TaskCompletionSource(TaskCreationOptions.None);
            EveryFrame += KeepChecking;
            return ts.Task;

            void KeepChecking()
            {
                bool b1;

                try
                {
                    b1 = predicate();
                }
                catch (Exception e)
                {
                    EveryFrame -= KeepChecking;
                    ts.TrySetException(e);
                    return;
                }
                
                if (!b1)
                {
                    return;
                }

                EveryFrame -= KeepChecking;
                ts.TrySetResult();
            }
        }

        public static void InvokeAfterFramesUpdated()
        {
            AfterFramesUpdated.TryRaise(nameof(GameThread));
            AfterFramesUpdatedLate.TryRaise(nameof(GameThread));
        }
    }
}
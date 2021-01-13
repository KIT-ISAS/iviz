using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.ActionlibMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.XmlRpc;

namespace Iviz.Roslib.Actionlib
{
    public class InvalidRosActionState : RoslibException
    {
        public InvalidRosActionState(string message) : base(message)
        {
        }
    }

    public sealed class RosActionClient<TAGoal, TAFeedback, TAResult> : IDisposable
#if !NETSTANDARD2_0
        , IAsyncDisposable
#endif
        where TAGoal : IActionGoal, new()
        where TAFeedback : IActionFeedback, IDeserializable<TAFeedback>, new()
        where TAResult : IActionResult, IDeserializable<TAResult>, new()
    {
        readonly CancellationTokenSource tokenSource = new CancellationTokenSource();

        string? actionName;
        string? callerId;
        bool disposed;

        RosChannelReader<TAFeedback>? feedbackSubscriber;
        RosChannelReader<TAResult>? resultSubscriber;
        RosChannelWriter<GoalID>? cancelPublisher;
        RosChannelWriter<TAGoal>? goalPublisher;

        RosActionClientState state = RosActionClientState.Done;
        MergedChannelReader? channelReader;
        GoalID? goalId;

        public RosActionClient()
        {
        }

        public RosActionClient(RosClient client, string newActionName)
        {
            Start(client, newActionName);
        }

        public RosActionClientState State
        {
            get => state;
            private set
            {
                RosActionClientState oldState = state;
                state = value;
                StateChanged?.Invoke(oldState, state);
                Logger.LogDebug($"{this}: {oldState} -> {state}");
            }
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            if (goalPublisher == null)
            {
                return; // not started
            }

            tokenSource.Cancel();
            goalPublisher!.Dispose();
            cancelPublisher!.Dispose();
            feedbackSubscriber!.Dispose();
            resultSubscriber!.Dispose();
        }

        public async Task DisposeAsync()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            if (goalPublisher == null)
            {
                return; // not started
            }

            tokenSource.Cancel();
            await goalPublisher.DisposeAsync();
            await cancelPublisher!.DisposeAsync();
            await feedbackSubscriber!.DisposeAsync();
            await resultSubscriber!.DisposeAsync();
        }

#if !NETSTANDARD2_0
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await DisposeAsync();
        }
#endif

        public event Action<RosActionClientState /* old */, RosActionClientState /* new */>? StateChanged;

        public override string ToString()
        {
            return $"[RosActionClient {actionName} state={State}]]";
        }

        void ValidateStart(IRosClient client, string newActionName)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (!RosClient.IsValidResourceName(newActionName))
            {
                throw new ArgumentException($"Action name '{newActionName}' is not a valid resource name");
            }

            if (actionName != null)
            {
                throw new InvalidOperationException("Action client has already been started!");
            }

            actionName = newActionName;
            callerId = client.CallerId;
        }

        public void Start(RosClient client, string newActionName)
        {
            ValidateStart(client, newActionName);
            if (newActionName[0] == '/')
            {
                newActionName = newActionName.Substring(1);
            }

            goalPublisher = new RosChannelWriter<TAGoal>(client, $"/{newActionName}/goal") {LatchingEnabled = true};
            cancelPublisher = new RosChannelWriter<GoalID>(client, $"/{newActionName}/cancel") {LatchingEnabled = true};
            feedbackSubscriber = new RosChannelReader<TAFeedback>(client, $"/{newActionName}/feedback");
            resultSubscriber = new RosChannelReader<TAResult>(client, $"/{newActionName}/result");
            channelReader = new MergedChannelReader(feedbackSubscriber, resultSubscriber);
        }

        public async Task StartAsync(IRosClient client, string newActionName)
        {
            ValidateStart(client, newActionName);
            if (newActionName[0] == '/')
            {
                newActionName = newActionName.Substring(1);
            }

            goalPublisher = new RosChannelWriter<TAGoal> {LatchingEnabled = true};
            await goalPublisher.StartAsync(client, $"/{newActionName}/goal");

            cancelPublisher = new RosChannelWriter<GoalID> {LatchingEnabled = true};
            await cancelPublisher.StartAsync(client, $"/{newActionName}/cancel");

            feedbackSubscriber = new RosChannelReader<TAFeedback>();
            resultSubscriber = new RosChannelReader<TAResult>();

            await feedbackSubscriber.StartAsync(client, $"/{newActionName}/feedback");
            await resultSubscriber.StartAsync(client, $"/{newActionName}/result");

            channelReader = new MergedChannelReader(feedbackSubscriber, resultSubscriber);
        }

        // ---------------------------------------------------------------------------

        void ProcessGoalStatus(RosGoalStatus status)
        {
            switch (State)
            {
                case RosActionClientState.WaitingForGoalAck when status == RosGoalStatus.Pending:
                    State = RosActionClientState.Pending;
                    break;
                case RosActionClientState.WaitingForGoalAck when status == RosGoalStatus.Active:
                    State = RosActionClientState.Active;
                    break;
                case RosActionClientState.WaitingForGoalAck when status == RosGoalStatus.Preempted:
                    State = RosActionClientState.WaitingForResult;
                    break;
                case RosActionClientState.Pending when status == RosGoalStatus.Active:
                    State = RosActionClientState.Active;
                    break;
                case RosActionClientState.Pending when status == RosGoalStatus.Recalling:
                    State = RosActionClientState.Recalling;
                    break;
                case RosActionClientState.Active when status == RosGoalStatus.Active:
                    // do nothing
                    break;
                case RosActionClientState.Active when status == RosGoalStatus.Preempting:
                    State = RosActionClientState.Preempting;
                    break;
                case RosActionClientState.WaitingForCancelAck when status == RosGoalStatus.Recalling:
                    State = RosActionClientState.Recalling;
                    break;
                case RosActionClientState.WaitingForCancelAck when status == RosGoalStatus.Preempting:
                    State = RosActionClientState.Preempting;
                    break;
                case RosActionClientState.WaitingForCancelAck when status == RosGoalStatus.Recalled:
                    State = RosActionClientState.WaitingForResult;
                    break;
                case RosActionClientState.WaitingForCancelAck when status == RosGoalStatus.Preempted:
                    State = RosActionClientState.WaitingForResult;
                    break;
                case RosActionClientState.WaitingForCancelAck when status == RosGoalStatus.Active:
                    // do nothing
                    break;
                case RosActionClientState.Recalling when status == RosGoalStatus.Preempting:
                    State = RosActionClientState.Preempting;
                    break;
                case RosActionClientState.Pending when status == RosGoalStatus.Rejected:
                    State = RosActionClientState.WaitingForResult;
                    break;
                case RosActionClientState.Active when status == RosGoalStatus.Aborted:
                case RosActionClientState.Active when status == RosGoalStatus.Succeeded:
                    State = RosActionClientState.WaitingForResult;
                    break;
                case RosActionClientState.Recalling when status == RosGoalStatus.Recalled:
                case RosActionClientState.Recalling when status == RosGoalStatus.Rejected:
                    State = RosActionClientState.WaitingForResult;
                    break;
                case RosActionClientState.Preempting when status == RosGoalStatus.Preempted:
                case RosActionClientState.Preempting when status == RosGoalStatus.Aborted:
                case RosActionClientState.Preempting when status == RosGoalStatus.Succeeded:
                    State = RosActionClientState.WaitingForResult;
                    break;
                case RosActionClientState.WaitingForResult:
                    break;
                case RosActionClientState.Done:
                    break;
                default:
                    Logger.Log($"RosActionClient: No transition for state {State} with input {status}");
                    break;
            }
        }

        public void SetGoal<TGoal>(TGoal goal) where TGoal : IGoal<TAGoal>
        {
            if (goalPublisher == null)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            if (goal == null)
            {
                throw new ArgumentNullException(nameof(goal));
            }

            time now = time.Now();
            goalId = new GoalID
            {
                Id = $"{callerId}#{now.ToDateTime()}",
                Stamp = now
            };

            TAGoal actionGoal = new TAGoal
            {
                GoalId = goalId,
                Header = new Header
                {
                    Stamp = time.Now()
                }
            };

            IActionGoal<TGoal> actionTGoal = (IActionGoal<TGoal>) actionGoal;
            actionTGoal.Goal = goal;

            goalPublisher.Write(actionGoal);

            State = RosActionClientState.WaitingForGoalAck;
        }

        public void Cancel()
        {
            if (cancelPublisher == null)
            {
                throw new InvalidOperationException("Start has not been called!!");
            }

            if (goalId == null)
            {
                throw new InvalidOperationException("Goal has not been set!");
            }

            if (State != RosActionClientState.WaitingForGoalAck
                && State != RosActionClientState.Pending
                && State != RosActionClientState.Active)
            {
                throw new InvalidRosActionState($"Cannot cancel from state {State}");
            }

            cancelPublisher.Write(goalId);
            State = RosActionClientState.WaitingForCancelAck;
        }
        
        public async Task CancelAsync()
        {
            if (cancelPublisher == null)
            {
                throw new InvalidOperationException("Start has not been called!!");
            }

            if (goalId == null)
            {
                throw new InvalidOperationException("Goal has not been set!");
            }

            if (State != RosActionClientState.WaitingForGoalAck
                && State != RosActionClientState.Pending
                && State != RosActionClientState.Active)
            {
                throw new InvalidRosActionState($"Cannot cancel from state {State}");
            }

            await cancelPublisher.WriteAsync(goalId, RosPublishPolicy.WaitUntilSent);
            State = RosActionClientState.WaitingForCancelAck;
        }

        // ---------------------------------------------------------------------------

        static bool Equals(GoalID a, GoalID b)
        {
            return (a.Id, a.Stamp) == (b.Id, b.Stamp);
        }

        #region WaitForServer

        public bool WaitForServer(int timeInMs)
        {
            using CancellationTokenSource source = new CancellationTokenSource(timeInMs);
            return WaitForServer(source.Token);
        }

        public bool WaitForServer(CancellationToken token = default)
        {
            const int sleepTimeInMs = 100;
            string actionServerId = $"/{actionName}";

            if (goalPublisher == null)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            using CancellationTokenSource linkedSource =
                CancellationTokenSource.CreateLinkedTokenSource(token, tokenSource.Token);
            while (!linkedSource.IsCancellationRequested)
            {
                PublisherTopicState pState = goalPublisher.Publisher.GetState();
                bool serverFound = pState.Senders.Any(sender => sender.RemoteId == actionServerId);
                if (serverFound)
                {
                    return true;
                }

                Thread.Sleep(sleepTimeInMs);
            }

            return false;
        }

        public async Task<bool> WaitForServerAsync(int timeInMs)
        {
            using CancellationTokenSource source = new CancellationTokenSource(timeInMs);
            return await WaitForServerAsync(source.Token).Caf();
        }

        public async Task<bool> WaitForServerAsync(CancellationToken token = default)
        {
            const int sleepTimeInMs = 100;
            string actionServerId = $"/{actionName}";

            if (goalPublisher == null)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            using CancellationTokenSource linkedSource =
                CancellationTokenSource.CreateLinkedTokenSource(token, tokenSource.Token);
            while (!linkedSource.IsCancellationRequested)
            {
                PublisherTopicState pState = goalPublisher.Publisher.GetState();
                bool serverFound = pState.Senders.Any(sender => sender.RemoteId == actionServerId);
                if (serverFound)
                {
                    return true;
                }

                await Task.Delay(sleepTimeInMs, token).Caf();
            }

            return false;
        }

        #endregion


        #region WaitForResult

        public bool WaitForResult(int timeInMs)
        {
            using CancellationTokenSource source = new CancellationTokenSource(timeInMs);
            return WaitForResult(source.Token);
        }

        public bool WaitForResult(CancellationToken token = default)
        {
            return WaitForResult<IFeedback<TAFeedback>, IResult<TAResult>>(token, null, null);
        }

        public bool WaitForResult<TT, TU>(IProgress<TT>? feedbackCallback, IProgress<TU>? resultCallback)
            where TT : IFeedback<TAFeedback> where TU : IResult<TAResult>
        {
            return WaitForResult(CancellationToken.None, feedbackCallback, resultCallback);
        }

        public bool WaitForResult<TT, TU>(CancellationToken token,
            IProgress<TT>? feedbackCallback,
            IProgress<TU>? resultCallback)
            where TT : IFeedback<TAFeedback> where TU : IResult<TAResult>
        {
            if (goalId == null)
            {
                throw new InvalidOperationException("Goal has not been set!");
            }

            if (channelReader == null)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            using CancellationTokenSource linkedSource =
                CancellationTokenSource.CreateLinkedTokenSource(token, tokenSource.Token);

            foreach (IMessage msg in channelReader.ReadAll(linkedSource.Token))
                switch (msg)
                {
                    case IActionFeedback<TT> actionFeedback:
                        if (!Equals(actionFeedback.Status.GoalId, goalId))
                        {
                            continue;
                        }

                        ProcessGoalStatus((RosGoalStatus) actionFeedback.Status.Status);
                        feedbackCallback?.Report(actionFeedback.Feedback);
                        break;
                    case IActionResult<TU> actionResult:
                        if (!Equals(actionResult.Status.GoalId, goalId))
                        {
                            continue;
                        }

                        ProcessGoalStatus((RosGoalStatus) actionResult.Status.Status);
                        resultCallback?.Report(actionResult.Result);
                        if (State != RosActionClientState.WaitingForResult)
                        {
                            Logger.LogDebug($"{this}: Terminated in state {State}");
                        }

                        State = RosActionClientState.Done;
                        return true;
                }

            return false;
        }

        #endregion

        #region WaitForResultAsync

#if !NETSTANDARD2_0
        public async Task<bool> WaitForResultAsync(CancellationToken token = default)
        {
            return await WaitForResultAsync<IFeedback<TAFeedback>, IResult<TAResult>>(token, null, null);
        }

        public async Task<bool> WaitForResultAsync<TT, TU>(IProgress<TT>? feedbackCallback,
            IProgress<TU>? resultCallback)
            where TT : IFeedback<TAFeedback> where TU : IResult<TAResult>
        {
            return await WaitForResultAsync(CancellationToken.None, feedbackCallback, resultCallback);
        }

        public async Task<bool> WaitForResultAsync<TT, TU>(CancellationToken token,
            IProgress<TT>? feedbackCallback,
            IProgress<TU>? resultCallback)
            where TT : IFeedback<TAFeedback> where TU : IResult<TAResult>
        {
            if (goalId == null)
            {
                throw new InvalidOperationException("Goal has not been set!");
            }

            if (channelReader == null)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            using CancellationTokenSource linkedSource =
                CancellationTokenSource.CreateLinkedTokenSource(token, tokenSource.Token);

            await foreach (IMessage msg in channelReader.ReadAllAsync(linkedSource.Token))
            {
                switch (msg)
                {
                    case IActionFeedback<TT> actionFeedback:
                        if (!Equals(actionFeedback.Status.GoalId, goalId))
                        {
                            continue;
                        }

                        ProcessGoalStatus((RosGoalStatus) actionFeedback.Status.Status);
                        feedbackCallback?.Report(actionFeedback.Feedback);
                        break;
                    case IActionResult<TU> actionResult:
                        if (!Equals(actionResult.Status.GoalId, goalId))
                        {
                            continue;
                        }

                        ProcessGoalStatus((RosGoalStatus) actionResult.Status.Status);
                        resultCallback?.Report(actionResult.Result);
                        if (State != RosActionClientState.WaitingForResult)
                        {
                            //Console.WriteLine($"{this}: Terminated in state {State}");
                        }

                        State = RosActionClientState.Done;
                        var endStatus = (RosGoalStatus) actionResult.Status.Status;
                        if (endStatus != RosGoalStatus.Succeeded)
                        {
                            throw new RosActionFailedException(endStatus, actionResult.Status.Text);
                        }

                        return true;
                }
            }

            return false;
        }
#endif

        #endregion
    }

    public class RosActionFailedException : Exception
    {
        public RosGoalStatus Status { get; }
        public string Text { get; }

        public RosActionFailedException(RosGoalStatus status, string text) :
            base($"Action ended with status {status}; Message: {text}")
        {
            Status = status;
            Text = text;
        }
    }

    public static class RosActionClient
    {
        public static RosActionClient<TActionGoal, TActionFeedback, TActionResult>
            Create<TActionGoal, TActionFeedback, TActionResult>
            (
                RosClient client,
                string actionName,
                IAction<TActionGoal, TActionFeedback, TActionResult>? _
            )
            where TActionGoal : IActionGoal, new()
            where TActionFeedback : IActionFeedback, IDeserializable<TActionFeedback>, new()
            where TActionResult : IActionResult, IDeserializable<TActionResult>, new()
        {
            return new RosActionClient<TActionGoal, TActionFeedback, TActionResult>(client, actionName);
        }

        public static RosActionClient<TActionGoal, TActionFeedback, TActionResult>
            Create<TActionGoal, TActionFeedback, TActionResult>
            (
                IAction<TActionGoal, TActionFeedback, TActionResult>? _
            )
            where TActionGoal : IActionGoal, new()
            where TActionFeedback : IActionFeedback, IDeserializable<TActionFeedback>, new()
            where TActionResult : IActionResult, IDeserializable<TActionResult>, new()
        {
            return new RosActionClient<TActionGoal, TActionFeedback, TActionResult>();
        }
    }
}
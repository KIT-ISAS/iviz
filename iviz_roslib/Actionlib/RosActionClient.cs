using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.ActionlibMsgs;

namespace Iviz.Roslib.Actionlib
{
    public class InvalidRosActionState : RoslibException
    {
        public InvalidRosActionState(string message) : base(message)
        {
        }
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

    public sealed class RosActionClient<TAGoal, TAFeedback, TAResult> : IDisposable
#if !NETSTANDARD2_0
        , IAsyncDisposable
#endif
        where TAGoal : class, IActionGoal, new()
        where TAFeedback : class, IActionFeedback, IDeserializable<TAFeedback>, new()
        where TAResult : class, IActionResult, IDeserializable<TAResult>, new()
    {
        readonly CancellationTokenSource runningTs = new();

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

            runningTs.Cancel();
            goalPublisher!.Dispose();
            cancelPublisher!.Dispose();
            feedbackSubscriber!.Dispose();
            resultSubscriber!.Dispose();
            runningTs.Dispose();
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

            runningTs.Cancel();
            await goalPublisher.DisposeAsync();
            await cancelPublisher!.DisposeAsync();
            await feedbackSubscriber!.DisposeAsync();
            await resultSubscriber!.DisposeAsync();
            runningTs.Dispose();
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

        public async Task StartAsync(IRosClient client, string newActionName, CancellationToken token = default)
        {
            ValidateStart(client, newActionName);
            if (newActionName[0] == '/')
            {
                newActionName = newActionName.Substring(1);
            }

            goalPublisher = new RosChannelWriter<TAGoal> {LatchingEnabled = true};
            await goalPublisher.StartAsync(client, $"/{newActionName}/goal", token);

            cancelPublisher = new RosChannelWriter<GoalID> {LatchingEnabled = true};
            await cancelPublisher.StartAsync(client, $"/{newActionName}/cancel", token);

            feedbackSubscriber = new RosChannelReader<TAFeedback>();
            resultSubscriber = new RosChannelReader<TAResult>();

            await feedbackSubscriber.StartAsync(client, $"/{newActionName}/feedback", token);
            await resultSubscriber.StartAsync(client, $"/{newActionName}/result", token);

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

            TAGoal actionGoal = new()
            {
                GoalId = goalId
            };

            IActionGoal<TGoal> actionTGoal = (IActionGoal<TGoal>) actionGoal;
            actionTGoal.Goal = goal;

            goalPublisher.Write(actionGoal);

            State = RosActionClientState.WaitingForGoalAck;
        }

        public Task SetGoalAsync<TGoal>(TGoal goal) where TGoal : IGoal<TAGoal>
        {
            SetGoal(goal);
            return Task.CompletedTask; // TODO!
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

        public Task CancelAsync()
        {
            Cancel();
            return Task.CompletedTask; // TODO!
        }

        // ---------------------------------------------------------------------------

        public void WaitForServer(int timeoutInMs)
        {
            if (goalPublisher == null)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            try
            {
                using CancellationTokenSource linkedSource =
                    CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token);
                linkedSource.CancelAfter(timeoutInMs);
                goalPublisher.Publisher.WaitForAnySubscriber(linkedSource.Token);
            }
            catch (OperationCanceledException e)
            {
                if (e.CancellationToken == runningTs.Token)
                {
                    throw new ObjectDisposedException("this", "Client was disposed.");
                }

                throw new TimeoutException("Wait for server timed out");
            }
        }

        public void WaitForServer(CancellationToken token = default)
        {
            if (goalPublisher == null)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            using CancellationTokenSource linkedSource =
                CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);
            goalPublisher.Publisher.WaitForAnySubscriber(linkedSource.Token);
        }

        public async Task WaitForServerAsync(CancellationToken token = default)
        {
            if (goalPublisher == null)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            using CancellationTokenSource linkedSource =
                CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);
            await goalPublisher.Publisher.WaitForAnySubscriberAsync(linkedSource.Token);
        }

        public IEnumerable<(TAFeedback? Feedback, TAResult? Result)> ReadAll(CancellationToken token = default)
        {
            if (channelReader == null)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            return token == default
                ? ReadAllImpl(channelReader.ReadAll(runningTs.Token))
                : ReadAllWithToken(token);
        }

        IEnumerable<(TAFeedback? Feedback, TAResult? Result)> ReadAllWithToken(CancellationToken token)
        {
            using CancellationTokenSource linkedSource =
                CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);
            var source = channelReader!.ReadAll(linkedSource.Token);
            foreach (var msg in ReadAllImpl(source))
            {
                yield return msg;
            }
        }

        public IEnumerable<(TAFeedback? Feedback, TAResult? Result)> TryReadAll()
        {
            if (channelReader == null)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            return ReadAllImpl(channelReader.TryReadAll());
        }

        IEnumerable<(TAFeedback? Feedback, TAResult? Result)> ReadAllImpl(IEnumerable<IMessage> source)
        {
            if (goalId == null)
            {
                throw new InvalidOperationException("Goal has not been set!");
            }

            foreach (IMessage msg in source)
            {
                switch (msg)
                {
                    case TAFeedback actionFeedback:
                        if (actionFeedback.Status.GoalId != goalId)
                        {
                            continue;
                        }

                        ProcessGoalStatus((RosGoalStatus) actionFeedback.Status.Status);
                        yield return (actionFeedback, null);
                        break;
                    case TAResult actionResult:
                        if (actionResult.Status.GoalId != goalId)
                        {
                            continue;
                        }

                        ProcessGoalStatus((RosGoalStatus) actionResult.Status.Status);
                        yield return (null, actionResult);

                        State = RosActionClientState.Done;
                        yield break;
                }
            }
        }

        #region WaitForResultAsync

#if !NETSTANDARD2_0
        public async IAsyncEnumerable<(TAFeedback? Feedback, TAResult? Result)> ReadAllAsync(
            [EnumeratorCancellation] CancellationToken token)
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
                CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);

            await foreach (IMessage msg in channelReader.ReadAllAsync(linkedSource.Token))
            {
                switch (msg)
                {
                    case TAFeedback actionFeedback:
                        if (actionFeedback.Status.GoalId != goalId)
                        {
                            continue;
                        }

                        ProcessGoalStatus((RosGoalStatus) actionFeedback.Status.Status);
                        yield return (actionFeedback, null);
                        break;
                    case TAResult actionResult:
                        if (actionResult.Status.GoalId != goalId)
                        {
                            continue;
                        }

                        ProcessGoalStatus((RosGoalStatus) actionResult.Status.Status);
                        yield return (null, actionResult);

                        State = RosActionClientState.Done;
                        yield break;
                }
            }
        }
#endif

        #endregion
    }

    public static class RosActionClient
    {
        public static RosActionClient<TActionGoal, TActionFeedback, TActionResult>
            Create<TActionGoal, TActionFeedback, TActionResult>
            (
                this IAction<TActionGoal, TActionFeedback, TActionResult>? _,
                RosClient client,
                string actionName
            )
            where TActionGoal : class, IActionGoal, new()
            where TActionFeedback : class, IActionFeedback, IDeserializable<TActionFeedback>, new()
            where TActionResult : class, IActionResult, IDeserializable<TActionResult>, new()
        {
            return new(client, actionName);
        }

        public static RosActionClient<TActionGoal, TActionFeedback, TActionResult>
            Create<TActionGoal, TActionFeedback, TActionResult>
            (
                this IAction<TActionGoal, TActionFeedback, TActionResult>? _
            )
            where TActionGoal : class, IActionGoal, new()
            where TActionFeedback : class, IActionFeedback, IDeserializable<TActionFeedback>, new()
            where TActionResult : class, IActionResult, IDeserializable<TActionResult>, new()
        {
            return new();
        }
    }
}
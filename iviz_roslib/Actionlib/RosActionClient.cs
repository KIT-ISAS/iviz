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
        where TAGoal : IActionGoal, new()
        where TAFeedback : IActionFeedback, IDeserializable<TAFeedback>, new()
        where TAResult : IActionResult, IDeserializable<TAResult>, new()
    {
        readonly string actionName;
        readonly RosClient client;
        readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        RosPublisher<GoalID> cancelPublisher;

        string cancelPublisherId;
        bool disposed;
        RosSubscriberChannel<TAFeedback> feedbackSubscriber;
        GoalID goalId;

        RosPublisher<TAGoal> goalPublisher;
        string goalPublisherId;
        RosSubscriberChannel<TAResult> resultSubscriber;

        RosActionClientState state = RosActionClientState.Done;
        CombinedSubscriberQueue subscriberQueue;

        public RosActionClient(RosClient client, string actionName)
        {
            if (!RosClient.IsValidResourceName(actionName))
            {
                throw new ArgumentException($"Action name '{actionName}' is not a valid resource name");
            }

            this.actionName = actionName;
            this.client = client ?? throw new ArgumentNullException(nameof(client));
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
            tokenSource.Cancel();
            goalPublisher.Unadvertise(goalPublisherId);
            cancelPublisher.Unadvertise(cancelPublisherId);
            feedbackSubscriber.Dispose();
            resultSubscriber.Dispose();
        }

        public event Action<RosActionClientState /* old */, RosActionClientState /* new */> StateChanged;

        public override string ToString()
        {
            return $"[RosActionClient {actionName} state={State}]]";
        }

        public void Start()
        {
            goalPublisherId = client.Advertise($"/{actionName}/goal", out goalPublisher);
            cancelPublisherId = client.Advertise($"/{actionName}/cancel", out cancelPublisher);
            feedbackSubscriber = new RosSubscriberChannel<TAFeedback>(client, $"/{actionName}/feedback");
            resultSubscriber = new RosSubscriberChannel<TAResult>(client, $"/{actionName}/result");
            subscriberQueue =
                new CombinedSubscriberQueue(new ISubscriberQueue[] {feedbackSubscriber, resultSubscriber});
        }

        public async Task StartAsync()
        {
            (goalPublisherId, goalPublisher) = await client.AdvertiseAsync<TAGoal>($"/{actionName}/goal");
            (cancelPublisherId, cancelPublisher) = await client.AdvertiseAsync<GoalID>($"/{actionName}/cancel");

            feedbackSubscriber = new RosSubscriberChannel<TAFeedback>();
            resultSubscriber = new RosSubscriberChannel<TAResult>();

            await feedbackSubscriber.StartAsync(client, $"/{actionName}/feedback");
            await resultSubscriber.StartAsync(client, $"/{actionName}/result");

            subscriberQueue =
                new CombinedSubscriberQueue(new ISubscriberQueue[] {feedbackSubscriber, resultSubscriber});
        }

        // ---------------------------------------------------------------------------

        void CreateGoalId()
        {
            time now = time.Now();
            goalId = new GoalID
            {
                Id = $"{client.CallerId}#{now.ToDateTime()}",
                Stamp = now
            };
        }

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
            if (goalPublisherId == null)
            {
                throw new InvalidOperationException("Start has not been called!");
            }

            if (goal == null)
            {
                throw new ArgumentNullException(nameof(goal));
            }

            CreateGoalId();

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

            goalPublisher.Publish(actionGoal);

            State = RosActionClientState.WaitingForGoalAck;
        }

        public void Cancel()
        {
            if (State != RosActionClientState.WaitingForGoalAck
                && State != RosActionClientState.Pending
                && State != RosActionClientState.Active)
            {
                throw new InvalidRosActionState($"Cannot cancel from state {State}");
            }

            cancelPublisher.Publish(goalId);
            State = RosActionClientState.WaitingForCancelAck;
        }

        // ---------------------------------------------------------------------------

        static bool Equals(GoalID a, GoalID b)
        {
            return (a.Id, a.Stamp) == (b.Id, b.Stamp);
        }

        #region WaitForServer

        public void WaitForServer()
        {
            WaitForServer(CancellationToken.None);
        }

        public bool WaitForServer(int timeInMs)
        {
            using CancellationTokenSource source = new CancellationTokenSource(timeInMs);
            return WaitForServer(source.Token);
        }

        public bool WaitForServer(CancellationToken token)
        {
            const int sleepTimeInMs = 100;
            string actionServerId = $"/{actionName}";

            using CancellationTokenSource linkedSource =
                CancellationTokenSource.CreateLinkedTokenSource(token, tokenSource.Token);
            while (!linkedSource.IsCancellationRequested)
            {
                PublisherTopicState pState = goalPublisher.GetState();
                bool serverFound = pState.Senders.Any(sender => sender.RemoteId == actionServerId);
                if (serverFound)
                {
                    return true;
                }

                Thread.Sleep(sleepTimeInMs);
            }

            return false;
        }

        public async Task WaitForServerAsync()
        {
            await WaitForServerAsync(CancellationToken.None).Caf();
        }

        public async Task<bool> WaitForServerAsync(int timeInMs)
        {
            using CancellationTokenSource source = new CancellationTokenSource(timeInMs);
            return await WaitForServerAsync(source.Token).Caf();
        }

        public async Task<bool> WaitForServerAsync(CancellationToken token)
        {
            const int sleepTimeInMs = 100;
            string actionServerId = $"/{actionName}";

            using CancellationTokenSource linkedSource =
                CancellationTokenSource.CreateLinkedTokenSource(token, tokenSource.Token);
            while (!linkedSource.IsCancellationRequested)
            {
                PublisherTopicState pState = goalPublisher.GetState();
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

        public bool WaitForResult()
        {
            return WaitForResult(CancellationToken.None);
        }

        public bool WaitForResult(int timeInMs)
        {
            using CancellationTokenSource source = new CancellationTokenSource(timeInMs);
            return WaitForResult(source.Token);
        }

        public bool WaitForResult(CancellationToken token)
        {
            return WaitForResult<IFeedback<TAFeedback>, IResult<TAResult>>(token, null, null);
        }

        public bool WaitForResult<TT, TU>(IProgress<TT> feedbackCallback, IProgress<TU> resultCallback)
            where TT : IFeedback<TAFeedback> where TU : IResult<TAResult>
        {
            return WaitForResult(CancellationToken.None, feedbackCallback, resultCallback);
        }

        public bool WaitForResult<TT, TU>(CancellationToken token, 
            IProgress<TT> feedbackCallback,
            IProgress<TU> resultCallback)
            where TT : IFeedback<TAFeedback> where TU : IResult<TAResult>
        {
            if (goalId == null)
            {
                throw new InvalidOperationException("Goal has not been set!");
            }

            using CancellationTokenSource linkedSource =
                CancellationTokenSource.CreateLinkedTokenSource(token, tokenSource.Token);

            foreach (IMessage msg in subscriberQueue.AsEnum(linkedSource.Token))
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
        public async Task<bool> WaitForResultAsync()
        {
            return await WaitForResultAsync(CancellationToken.None);
        }

        public async Task<bool> WaitForResultAsync(CancellationToken token)
        {
            return await WaitForResultAsync<IFeedback<TAFeedback>, IResult<TAResult>>(token, null, null);
        }

        public async Task<bool> WaitForResultAsync<TT, TU>(Action<TT> feedbackCallback, Action<TU> resultCallback)
            where TT : IFeedback<TAFeedback> where TU : IResult<TAResult>
        {
            return await WaitForResultAsync(CancellationToken.None, feedbackCallback, resultCallback);
        }

        public async Task<bool> WaitForResultAsync<TT, TU>(CancellationToken token, Action<TT> feedbackCallback,
            Action<TU> resultCallback)
            where TT : IFeedback<TAFeedback> where TU : IResult<TAResult>
        {
            if (goalId == null)
            {
                throw new InvalidOperationException("Goal has not been set!");
            }

            using CancellationTokenSource linkedSource =
                CancellationTokenSource.CreateLinkedTokenSource(token, tokenSource.Token);

            try
            {
                await foreach (IMessage msg in subscriberQueue.AsAsyncEnum(linkedSource.Token))
                    switch (msg)
                    {
                        case IActionFeedback<TT> actionFeedback:
                            if (!Equals(actionFeedback.Status.GoalId, goalId))
                            {
                                continue;
                            }

                            ProcessGoalStatus((RosGoalStatus) actionFeedback.Status.Status);
                            feedbackCallback?.Invoke(actionFeedback.Feedback);
                            break;
                        case IActionResult<TU> actionResult:
                            if (!Equals(actionResult.Status.GoalId, goalId))
                            {
                                continue;
                            }

                            ProcessGoalStatus((RosGoalStatus) actionResult.Status.Status);
                            resultCallback?.Invoke(actionResult.Result);
                            if (State != RosActionClientState.WaitingForResult)
                            {
                                Logger.LogDebug($"{this}: Terminated in state {State}");
                            }

                            State = RosActionClientState.Done;
                            return true;
                    }
            }
            catch (OperationCanceledException)
            {
            }

            return false;
        }
#endif

        #endregion
    }

    public static class ActionClient
    {
        public static RosActionClient<TActionGoal, TActionFeedback, TActionResult>
            Create<TActionGoal, TActionFeedback, TActionResult>
            (
                RosClient client,
                string actionName,
                IAction<TActionGoal, TActionFeedback, TActionResult> _
            )
            where TActionGoal : IActionGoal, new()
            where TActionFeedback : IActionFeedback, IDeserializable<TActionFeedback>, new()
            where TActionResult : IActionResult, IDeserializable<TActionResult>, new()
        {
            return new RosActionClient<TActionGoal, TActionFeedback, TActionResult>(client, actionName);
        }
    }
}
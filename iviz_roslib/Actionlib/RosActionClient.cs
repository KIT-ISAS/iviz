using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.ActionlibMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Tools;

namespace Iviz.Roslib.Actionlib;

public sealed class RosActionClient<TAGoal, TAFeedback, TAResult> : IDisposable, IAsyncDisposable
    where TAGoal : class, IMessage, IActionGoal, new()
    where TAFeedback : class, IMessage, IActionFeedback, new()
    where TAResult : class, IMessage, IActionResult, new()
{
    readonly CancellationTokenSource runningTs = new();

    string actionName = "";
    string? callerId;
    bool disposed;

    RosChannelReader<TAFeedback>? feedbackSubscriber;
    RosChannelReader<TAResult>? resultSubscriber;
    RosChannelWriter<GoalID>? cancelPublisher;
    RosChannelWriter<TAGoal>? goalPublisher;
    MergedChannelReader? mergedReader;
    RosActionClientState state = RosActionClientState.Done;
    GoalID? goalId;

    RosChannelWriter<TAGoal> GoalPublisher =>
        goalPublisher ?? throw new InvalidOperationException("Start has not been called!");

    RosChannelWriter<GoalID> CancelPublisher =>
        cancelPublisher ?? throw new InvalidOperationException("Start has not been called!");

    MergedChannelReader ChannelReader =>
        mergedReader ?? throw new InvalidOperationException("Start has not been called!");


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
        }
    }

    public void Dispose()
    {
        if (disposed) return;
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
    }

    public async ValueTask DisposeAsync()
    {
        if (disposed) return;
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
    }

    public event Action<RosActionClientState /* old */, RosActionClientState /* new */>? StateChanged;

    public override string ToString()
    {
        return $"[{nameof(RosActionClient)} {actionName} state={State}]]";
    }

    void ValidateStart(IRosClient client, string newActionName)
    {
        if (client == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(client));
        }

        if (!string.IsNullOrEmpty(actionName))
        {
            throw new InvalidOperationException("Action client has already been started!");
        }

        string validatedActionName = (newActionName[0] == '/')
            ? newActionName[1..]
            : newActionName;

        RosNameUtils.ValidateResourceName(validatedActionName);

        actionName = validatedActionName;
        callerId = client.CallerId;
    }

    public void Start(RosClient client, string newActionName)
    {
        ValidateStart(client, newActionName);
        goalPublisher = new RosChannelWriter<TAGoal>(client, $"/{actionName}/goal", true);
        cancelPublisher = new RosChannelWriter<GoalID>(client, $"/{actionName}/cancel", true);
        feedbackSubscriber = new RosChannelReader<TAFeedback>(client, $"/{actionName}/feedback");
        resultSubscriber = new RosChannelReader<TAResult>(client, $"/{actionName}/result");
        mergedReader = new MergedChannelReader(feedbackSubscriber, resultSubscriber);
    }

    public async ValueTask StartAsync(IRosClient client, string newActionName, CancellationToken token = default)
    {
        ValidateStart(client, newActionName);
        goalPublisher = await client.CreateWriterAsync<TAGoal>($"/{actionName}/goal", true, token);
        cancelPublisher = await client.CreateWriterAsync<GoalID>($"/{actionName}/cancel", true, token);
        feedbackSubscriber = await client.CreateReaderAsync<TAFeedback>($"/{actionName}/feedback", token);
        resultSubscriber = await client.CreateReaderAsync<TAResult>($"/{actionName}/result", token);
        mergedReader = new MergedChannelReader(feedbackSubscriber, resultSubscriber);
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
                Logger.LogFormat("{0}: No transition for state {1} with input {2}", this, State, status);
                break;
        }
    }

    public void SetGoal<TGoal>(TGoal goal) where TGoal : IGoal<TAGoal>
    {
        if (goal == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(goal));
        }

        time now = time.Now();
        goalId = new GoalID
        {
            Id = $"{callerId}#{now.ToDateTime().ToString(BuiltIns.Culture)}",
            Stamp = now
        };

        TAGoal actionGoal = new()
        {
            Header = new Header(0, time.Now(), ""),
            GoalId = goalId
        };

        IActionGoal<TGoal> actionTGoal = (IActionGoal<TGoal>)actionGoal;
        actionTGoal.Goal = goal;

        GoalPublisher.Write(actionGoal);

        State = RosActionClientState.WaitingForGoalAck;
    }

    public Task SetGoalAsync<TGoal>(TGoal goal) where TGoal : IGoal<TAGoal>
    {
        SetGoal(goal);
        return Task.CompletedTask; // TODO!
    }

    public void Cancel()
    {
        if (goalId == null)
        {
            throw new InvalidOperationException("Goal has not been set!");
        }

        if (State != RosActionClientState.WaitingForGoalAck
            && State != RosActionClientState.Pending
            && State != RosActionClientState.Active)
        {
            throw new InvalidRosActionStateException($"Cannot cancel from state {State}");
        }

        CancelPublisher.Write(goalId);
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
        try
        {
            using var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token);
            linkedSource.CancelAfter(timeoutInMs);
            GoalPublisher.Publisher.WaitForAnySubscriber(linkedSource.Token);
        }
        catch (OperationCanceledException e)
        {
            if (e.CancellationToken == runningTs.Token)
            {
                BuiltIns.ThrowObjectDisposed(nameof(RosActionClient), "Client was disposed.");
            }

            throw new TimeoutException("Wait for server timed out");
        }
    }

    public void WaitForServer(CancellationToken token = default)
    {
        if (GoalPublisher.Publisher.NumSubscribers != 0)
        {
            return;
        }

        using var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);
        GoalPublisher.Publisher.WaitForAnySubscriber(linkedSource.Token);
    }

    public async ValueTask WaitForServerAsync(CancellationToken token = default)
    {
        if (GoalPublisher.Publisher.NumSubscribers != 0)
        {
            return;
        }

        using var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);
        await GoalPublisher.Publisher.WaitForAnySubscriberAsync(linkedSource.Token);
    }

    public IEnumerable<(TAFeedback? Feedback, TAResult? Result)> ReadAll(CancellationToken token = default)
    {
        return token == default
            ? ReadAllImpl(ChannelReader.ReadAll(runningTs.Token))
            : ReadAllWithToken(token);
    }

    IEnumerable<(TAFeedback? Feedback, TAResult? Result)> ReadAllWithToken(CancellationToken token)
    {
        using var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);
        var source = ChannelReader.ReadAll(linkedSource.Token);
        foreach (var msg in ReadAllImpl(source))
        {
            yield return msg;
        }
    }

    public IEnumerable<(TAFeedback? Feedback, TAResult? Result)> TryReadAll()
    {
        return ReadAllImpl(ChannelReader.TryReadAll());
    }

    IEnumerable<(TAFeedback? Feedback, TAResult? Result)> ReadAllImpl(IEnumerable<IMessage> source)
    {
        if (goalId == null)
        {
            throw new InvalidOperationException("Goal has not been set!");
        }

        foreach (var msg in source)
        {
            switch (msg)
            {
                case TAFeedback actionFeedback:
                    if (actionFeedback.Status.GoalId != goalId)
                    {
                        continue;
                    }

                    ProcessGoalStatus((RosGoalStatus)actionFeedback.Status.Status);
                    yield return (actionFeedback, null);
                    break;
                case TAResult actionResult:
                    if (actionResult.Status.GoalId != goalId)
                    {
                        continue;
                    }

                    ProcessGoalStatus((RosGoalStatus)actionResult.Status.Status);
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

        using var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);

        await foreach (IMessage msg in ChannelReader.ReadAllAsync(linkedSource.Token))
        {
            switch (msg)
            {
                case TAFeedback actionFeedback:
                    if (actionFeedback.Status.GoalId != goalId)
                    {
                        continue;
                    }

                    ProcessGoalStatus((RosGoalStatus)actionFeedback.Status.Status);
                    yield return (actionFeedback, null);
                    break;
                case TAResult actionResult:
                    if (actionResult.Status.GoalId != goalId)
                    {
                        continue;
                    }

                    ProcessGoalStatus((RosGoalStatus)actionResult.Status.Status);
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
        where TActionGoal : class, IMessage, IActionGoal, new()
        where TActionFeedback : class, IMessage, IActionFeedback, new()
        where TActionResult : class, IMessage, IActionResult, new()
    {
        return new(client, actionName);
    }

    public static RosActionClient<TActionGoal, TActionFeedback, TActionResult>
        Create<TActionGoal, TActionFeedback, TActionResult>
        (
            this IAction<TActionGoal, TActionFeedback, TActionResult>? _
        )
        where TActionGoal : class, IMessage, IActionGoal, new()
        where TActionFeedback : class, IMessage, IActionFeedback, new()
        where TActionResult : class, IMessage, IActionResult, new()
    {
        return new();
    }

    public static async ValueTask<RosActionClient<TActionGoal, TActionFeedback, TActionResult>>
        CreateClientAsync<TActionGoal, TActionFeedback, TActionResult>(
            this IRosClient client, string actionName, CancellationToken token = default)
        where TActionGoal : class,IMessage, IActionGoal, new()
        where TActionFeedback : class, IMessage, IActionFeedback, new()
        where TActionResult : class, IMessage, IActionResult, new()
    {
        var actionClient = new RosActionClient<TActionGoal, TActionFeedback, TActionResult>();
        await actionClient.StartAsync(client, actionName, token);
        return actionClient;
    }

    public static RosGoalStatus GetStatus<TActionResult>(this TActionResult result)
        where TActionResult : class, IActionResult
    {
        return (RosGoalStatus)result.Status.Status;
    }
}
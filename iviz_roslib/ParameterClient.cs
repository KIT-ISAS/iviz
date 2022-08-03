using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;
using Iviz.XmlRpc;
using TopicTuple = System.Tuple<string, string>;
using TopicTuples = System.Tuple<string, string[]>;

namespace Iviz.Roslib.XmlRpc;

/// <summary>
/// Contains utilities to access data from a ROS parameter server.
/// </summary>
public sealed class ParameterClient
{
    readonly RosMasterClient backend;

    public Uri MasterUri => backend.MasterUri;
    public Uri CallerUri => backend.CallerUri;
    public string CallerId => backend.CallerId;

    public ParameterClient(RosMasterClient backend)
    {
        this.backend = backend;
    }

    public override string ToString()
    {
        return $"[ParameterClient masterUri={MasterUri} callerUri={CallerUri} callerId={CallerId}]";
    }

    public bool SetParameter(string key, XmlRpcArg value)
    {
        if (key == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(key));
        }

        value.ThrowIfEmpty();
        return SetParam(key, value).IsValid;
    }

    public async ValueTask<bool> SetParameterAsync(string key, XmlRpcArg value, CancellationToken token = default)
    {
        if (key == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(key));
        }

        value.ThrowIfEmpty();
        return (await SetParamAsync(key, value, token)).IsValid;
    }

    public bool GetParameter(string key, out RosParameterValue value)
    {
        if (key == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(key));
        }

        var response = GetParam(key);
        bool success = response.IsValid;
        value = success ? response.ParameterValue : default;
        return success;
    }

    public async ValueTask<(bool success, RosParameterValue value)> GetParameterAsync(string key,
        CancellationToken token = default)
    {
        if (key == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(key));
        }

        var response = await GetParamAsync(key, token);
        bool success = response.IsValid;
        return (success, success ? response.ParameterValue : default);
    }

    public string[] GetParameterNames()
    {
        var response = GetParamNames();
        if (response.IsValid)
        {
            return response.ParameterNameList;
        }

        throw new RosRpcException("Failed to retrieve parameter names: " + response.StatusMessage);
    }

    public async ValueTask<string[]> GetParameterNamesAsync(CancellationToken token = default)
    {
        var response = await GetParamNamesAsync(token);
        if (response.IsValid)
        {
            return response.ParameterNameList;
        }

        throw new RosRpcException("Failed to retrieve parameter names: " + response.StatusMessage);
    }


    public bool DeleteParameter(string key)
    {
        if (key == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(key));
        }

        return DeleteParam(key).IsValid;
    }

    public async ValueTask<bool> DeleteParameterAsync(string key, CancellationToken token = default)
    {
        if (key == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(key));
        }

        return (await DeleteParamAsync(key, token)).IsValid;
    }

    public bool HasParameter(string key)
    {
        if (key == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(key));
        }

        return HasParam(key).HasParam;
    }

    public async ValueTask<bool> HasParameterAsync(string key, CancellationToken token = default)
    {
        if (key == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(key));
        }

        return (await HasParamAsync(key, token)).HasParam;
    }

    public bool SubscribeParameter(string key)
    {
        if (key == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(key));
        }

        return SubscribeParam(key).IsValid;
    }

    public async ValueTask<bool> SubscribeParameterAsync(string key, CancellationToken token = default)
    {
        if (key == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(key));
        }

        return (await SubscribeParamAsync(key, token)).IsValid;
    }

    public bool UnsubscribeParameter(string key)
    {
        if (key == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(key));
        }

        return UnsubscribeParam(key).IsValid;
    }

    public async ValueTask<bool> UnsubscribeParameterAsync(string key, CancellationToken token = default)
    {
        if (key == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(key));
        }

        return (await UnsubscribeParamAsync(key, token)).IsValid;
    }

    DefaultResponse DeleteParam(string key)
    {
        XmlRpcArg[] args = {CallerId, key};
        var response = MethodCall("deleteParam", args);
        return DefaultResponse.Create(response);
    }

    async ValueTask<DefaultResponse> DeleteParamAsync(string key, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key};
        var response = await MethodCallAsync("deleteParam", args, token);
        return DefaultResponse.Create(response);
    }

    DefaultResponse SetParam(string key, XmlRpcArg value)
    {
        XmlRpcArg[] args = {CallerId, key, value};
        var response = MethodCall("setParam", args);
        return DefaultResponse.Create(response);
    }

    async ValueTask<DefaultResponse> SetParamAsync(string key, XmlRpcArg value, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key, value};
        var response = await MethodCallAsync("setParam", args, token);
        return DefaultResponse.Create(response);
    }

    GetParamResponse GetParam(string key)
    {
        XmlRpcArg[] args = {CallerId, key};
        var response = MethodCall("getParam", args);
        return new GetParamResponse(response);
    }

    async ValueTask<GetParamResponse> GetParamAsync(string key, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key};
        var response = await MethodCallAsync("getParam", args, token);
        return new GetParamResponse(response);
    }

    SearchParamResponse SearchParam(string key)
    {
        XmlRpcArg[] args = {CallerId, key};
        var response = MethodCall("searchParam", args);
        return new SearchParamResponse(response);
    }

    async ValueTask<SearchParamResponse> SearchParamAsync(string key, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key};
        var response = await MethodCallAsync("searchParam", args, token);
        return new SearchParamResponse(response);
    }


    SubscribeParamResponse SubscribeParam(string key)
    {
        XmlRpcArg[] args = {CallerId, key, CallerUri};
        var response = MethodCall("subscribeParam", args);
        return new SubscribeParamResponse(response);
    }

    async ValueTask<SubscribeParamResponse> SubscribeParamAsync(string key, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key, CallerUri};
        var response = await MethodCallAsync("subscribeParam", args, token);
        return new SubscribeParamResponse(response);
    }

    UnsubscribeParamResponse UnsubscribeParam(string key)
    {
        XmlRpcArg[] args = {CallerId, key, CallerUri};
        var response = MethodCall("unsubscribeParam", args);
        return new UnsubscribeParamResponse(response);
    }

    async ValueTask<UnsubscribeParamResponse> UnsubscribeParamAsync(string key, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key, CallerUri};
        var response = await MethodCallAsync("unsubscribeParam", args, token);
        return new UnsubscribeParamResponse(response);
    }

    HasParamResponse HasParam(string key)
    {
        XmlRpcArg[] args = {CallerId, key};
        var response = MethodCall("hasParam", args);
        return new HasParamResponse(response);
    }

    async ValueTask<HasParamResponse> HasParamAsync(string key, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key};
        var response = await MethodCallAsync("hasParam", args, token);
        return new HasParamResponse(response);
    }

    GetParamNamesResponse GetParamNames()
    {
        XmlRpcArg[] args = {CallerId};
        var response = MethodCall("getParamNames", args);
        return new GetParamNamesResponse(response);
    }

    async ValueTask<GetParamNamesResponse> GetParamNamesAsync(CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId};
        var response = await MethodCallAsync("getParamNames", args, token);
        return new GetParamNamesResponse(response);
    }

    RosParameterValue[] MethodCall(string function, XmlRpcArg[] args)
    {
        return backend.MethodCall(function, args);
    }

    ValueTask<RosParameterValue[]> MethodCallAsync(string function, XmlRpcArg[] args, CancellationToken token = default)
    {
        return backend.MethodCallAsync(function, args, token);
    }

    internal sealed class GetParamResponse : BaseResponse
    {
        public RosParameterValue ParameterValue { get; }

        internal GetParamResponse(RosParameterValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            responseCode = code;
            StatusMessage = statusMessage;

            if (responseCode == StatusCode.Error)
            {
                return;
            }

            ParameterValue = a[2];
        }
    }

    internal sealed class SearchParamResponse : BaseResponse
    {
        public string? FoundKey { get; }

        internal SearchParamResponse(RosParameterValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            responseCode = code;
            StatusMessage = statusMessage;

            if (responseCode == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetString(out string foundKeyStr))
            {
                MarkError();
                return;
            }

            FoundKey = foundKeyStr;
        }
    }

    internal sealed class SubscribeParamResponse : BaseResponse
    {
        public RosParameterValue ParameterValue { get; }

        internal SubscribeParamResponse(RosParameterValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            responseCode = code;
            StatusMessage = statusMessage;

            if (responseCode == StatusCode.Error)
            {
                return;
            }

            ParameterValue = a[2];
        }
    }

    internal sealed class UnsubscribeParamResponse : BaseResponse
    {
        public int NumUnsubscribed { get; }

        internal UnsubscribeParamResponse(RosParameterValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            responseCode = code;
            StatusMessage = statusMessage;

            if (responseCode == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetInteger(out int numUnsubscribed))
            {
                MarkError();
                return;
            }

            NumUnsubscribed = numUnsubscribed;
        }
    }

    internal sealed class HasParamResponse : BaseResponse
    {
        public bool HasParam { get; }

        internal HasParamResponse(RosParameterValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            responseCode = code;
            StatusMessage = statusMessage;

            if (responseCode == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetBoolean(out bool hasParam))
            {
                MarkError();
                return;
            }

            HasParam = hasParam;
        }
    }

    internal sealed class GetParamNamesResponse : BaseResponse
    {
        public string[] ParameterNameList { get; } = Array.Empty<string>();

        internal GetParamNamesResponse(RosParameterValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            responseCode = code;
            StatusMessage = statusMessage;

            if (responseCode == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetArray(out RosParameterValue[] objNameList))
            {
                MarkError();
                return;
            }

            string[] nameList = new string[objNameList.Length];
            int r = 0;
            foreach (var objName in objNameList)
            {
                if (!objName.TryGetString(out string name))
                {
                    MarkError();
                    return;
                }

                nameList[r++] = name;
            }

            ParameterNameList = nameList;
        }
    }
}
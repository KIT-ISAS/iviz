using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.XmlRpc;
using Iviz.Tools;
using Iviz.XmlRpc;

namespace Iviz.Roslib;

/// <summary>
/// Contains utilities to access data from a ROS parameter server.
/// </summary>
public sealed class RosParameterClient
{
    readonly RosMasterClient backend;

    public Uri MasterUri => backend.MasterUri;
    public Uri CallerUri => backend.CallerUri;
    public string CallerId => backend.CallerId;

    public RosParameterClient(RosMasterClient backend)
    {
        this.backend = backend;
    }

    public async ValueTask<bool> SetParameterAsync(string key, XmlRpcArg value, CancellationToken token = default)
    {
        if (key == null) BuiltIns.ThrowArgumentNull(nameof(key));
        value.ThrowIfEmpty();

        return (await SetParamAsync(key, value, token)).IsValid;
    }

    public async ValueTask<RosValue> GetParameterAsync(string key, CancellationToken token = default)
    {
        if (key == null) BuiltIns.ThrowArgumentNull(nameof(key));

        var response = await GetParamAsync(key, token);
        if (response.HasParseError) throw new ParseException("Failed to parse response");
        if (!response.IsSuccess) throw new RosParameterNotFoundException();
        return response.Value;
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
    
    public async ValueTask<bool> DeleteParameterAsync(string key, CancellationToken token = default)
    {
        if (key == null) BuiltIns.ThrowArgumentNull(nameof(key));
        return (await DeleteParamAsync(key, token)).IsValid;
    }
    
    public async ValueTask<bool> HasParameterAsync(string key, CancellationToken token = default)
    {
        if (key == null) BuiltIns.ThrowArgumentNull(nameof(key));
        return (await HasParamAsync(key, token)).HasParam;
    }

    public async ValueTask<bool> SubscribeParameterAsync(string key, CancellationToken token = default)
    {
        if (key == null) BuiltIns.ThrowArgumentNull(nameof(key));
        return (await SubscribeParamAsync(key, token)).IsValid;
    }

    public async ValueTask<bool> UnsubscribeParameterAsync(string key, CancellationToken token = default)
    {
        if (key == null) BuiltIns.ThrowArgumentNull(nameof(key));
        return (await UnsubscribeParamAsync(key, token)).IsValid;
    }
    
    async ValueTask<DefaultResponse> DeleteParamAsync(string key, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key};
        var response = await MethodCallAsync("deleteParam", args, token);
        return DefaultResponse.Create(response);
    }

    async ValueTask<DefaultResponse> SetParamAsync(string key, XmlRpcArg value, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key, value};
        var response = await MethodCallAsync("setParam", args, token);
        return DefaultResponse.Create(response);
    }
    
    async ValueTask<GetParamResponse> GetParamAsync(string key, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key};
        var response = await MethodCallAsync("getParam", args, token);
        return new GetParamResponse(response);
    }
    
    async ValueTask<SearchParamResponse> SearchParamAsync(string key, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key};
        var response = await MethodCallAsync("searchParam", args, token);
        return new SearchParamResponse(response);
    }
    
    async ValueTask<SubscribeParamResponse> SubscribeParamAsync(string key, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key, CallerUri};
        var response = await MethodCallAsync("subscribeParam", args, token);
        return new SubscribeParamResponse(response);
    }

    async ValueTask<UnsubscribeParamResponse> UnsubscribeParamAsync(string key, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key, CallerUri};
        var response = await MethodCallAsync("unsubscribeParam", args, token);
        return new UnsubscribeParamResponse(response);
    }

    async ValueTask<HasParamResponse> HasParamAsync(string key, CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId, key};
        var response = await MethodCallAsync("hasParam", args, token);
        return new HasParamResponse(response);
    }

    async ValueTask<GetParamNamesResponse> GetParamNamesAsync(CancellationToken token = default)
    {
        XmlRpcArg[] args = {CallerId};
        var response = await MethodCallAsync("getParamNames", args, token);
        return new GetParamNamesResponse(response);
    }

    ValueTask<RosValue[]> MethodCallAsync(string function, XmlRpcArg[] args, CancellationToken token = default)
    {
        return backend.MethodCallAsync(function, args, token);
    }

    public override string ToString()
    {
        return $"[{nameof(RosParameterClient)} masterUri={MasterUri} callerUri={CallerUri} callerId={CallerId}]";
    }

    sealed class GetParamResponse : BaseResponse
    {
        public RosValue Value { get; }

        internal GetParamResponse(RosValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGet(out int code) ||
                !a[1].TryGet(out string statusMessage))
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

            Value = a[2];
        }
    }

    sealed class SearchParamResponse : BaseResponse
    {
        public string? FoundKey { get; }

        internal SearchParamResponse(RosValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGet(out int code) ||
                !a[1].TryGet(out string statusMessage))
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

            if (!a[2].TryGet(out string foundKeyStr))
            {
                MarkError();
                return;
            }

            FoundKey = foundKeyStr;
        }
    }

    sealed class SubscribeParamResponse : BaseResponse
    {
        public RosValue Value { get; }

        internal SubscribeParamResponse(RosValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGet(out int code) ||
                !a[1].TryGet(out string statusMessage))
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

            Value = a[2];
        }
    }

    sealed class UnsubscribeParamResponse : BaseResponse
    {
        public int NumUnsubscribed { get; }

        internal UnsubscribeParamResponse(RosValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGet(out int code) ||
                !a[1].TryGet(out string statusMessage))
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

            if (!a[2].TryGet(out int numUnsubscribed))
            {
                MarkError();
                return;
            }

            NumUnsubscribed = numUnsubscribed;
        }
    }

    sealed class HasParamResponse : BaseResponse
    {
        public bool HasParam { get; }

        internal HasParamResponse(RosValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGet(out int code) ||
                !a[1].TryGet(out string statusMessage))
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

            if (!a[2].TryGet(out bool hasParam))
            {
                MarkError();
                return;
            }

            HasParam = hasParam;
        }
    }

    sealed class GetParamNamesResponse : BaseResponse
    {
        public string[] ParameterNameList { get; } = Array.Empty<string>();

        internal GetParamNamesResponse(RosValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGet(out int code) ||
                !a[1].TryGet(out string statusMessage))
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

            if (!a[2].TryGetArray(out RosValue[] objNameList))
            {
                MarkError();
                return;
            }

            string[] nameList = new string[objNameList.Length];
            int r = 0;
            foreach (var objName in objNameList)
            {
                if (!objName.TryGet(out string name))
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
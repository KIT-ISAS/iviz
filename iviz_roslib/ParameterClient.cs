using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Iviz.XmlRpc;
using TopicTuple = System.Tuple<string, string>;
using TopicTuples = System.Tuple<string, string[]>;

namespace Iviz.Roslib.XmlRpc
{
    /// <summary>
    /// Contains utilities to access data from a ROS parameter server.
    /// </summary>
    public sealed class ParameterClient
    {
        public Uri MasterUri { get; }
        public Uri CallerUri { get; }
        public string CallerId { get; }
        public int TimeoutInMs { get; set; } = 2000;

        public ParameterClient(Uri masterUri, string callerId, Uri callerUri)
        {
            MasterUri = masterUri ?? throw new ArgumentNullException(nameof(masterUri));
            CallerUri = callerUri ?? throw new ArgumentNullException(nameof(callerUri));
            CallerId = callerId ?? throw new ArgumentNullException(nameof(callerId));
        }

        public override string ToString()
        {
            return $"[ParameterClient masterUri={MasterUri} callerUri={CallerUri} callerId={CallerId}]";
        }

        public bool SetParameter(string key, Arg value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            value.ThrowIfEmpty();
            return SetParam(key, value).IsValid;
        }

        public async Task<bool> SetParameterAsync(string key, Arg value, CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            value.ThrowIfEmpty();
            return (await SetParamAsync(key, value, token).Caf()).IsValid;
        }

        public bool GetParameter(string key, out object? value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var response = GetParam(key);
            bool success = response.IsValid;
            value = success ? response.ParameterValue : null;
            return success;
        }

        public async Task<(bool success, object? value)> GetParameterAsync(string key,
            CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var response = await GetParamAsync(key, token).Caf();
            bool success = response.IsValid;
            return (success, success ? response.ParameterValue : null);
        }

        public ReadOnlyCollection<string> GetParameterNames()
        {
            var response = GetParamNames();
            if (response.IsValid)
            {
                return response.ParameterNameList!;
            }

            throw new RosRpcException("Failed to retrieve parameter names: " + response.StatusMessage);
        }

        public async Task<ReadOnlyCollection<string>> GetParameterNamesAsync(CancellationToken token = default)
        {
            var response = await GetParamNamesAsync(token).Caf();
            if (response.IsValid)
            {
                return response.ParameterNameList!;
            }

            throw new RosRpcException("Failed to retrieve parameter names: " + response.StatusMessage);
        }


        public bool DeleteParameter(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return DeleteParam(key).IsValid;
        }

        public async Task<bool> DeleteParameterAsync(string key, CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return (await DeleteParamAsync(key, token).Caf()).IsValid;
        }

        public bool HasParameter(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return HasParam(key).HasParam;
        }

        public async Task<bool> HasParameterAsync(string key, CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return (await HasParamAsync(key, token).Caf()).HasParam;
        }

        public bool SubscribeParameter(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return SubscribeParam(key).IsValid;
        }

        public async Task<bool> SubscribeParameterAsync(string key, CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return (await SubscribeParamAsync(key, token).Caf()).IsValid;
        }

        public bool UnsubscribeParameter(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return UnsubscribeParam(key).IsValid;
        }

        public async Task<bool> UnsubscribeParameterAsync(string key, CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return (await UnsubscribeParamAsync(key, token).Caf()).IsValid;
        }

        DefaultResponse DeleteParam(string key)
        {
            Arg[] args = {CallerId, key};
            object response = MethodCall("deleteParam", args);
            return new DefaultResponse((object[]) response);
        }

        async Task<DefaultResponse> DeleteParamAsync(string key, CancellationToken token = default)
        {
            Arg[] args = {CallerId, key};
            object response = await MethodCallAsync("deleteParam", args, token).Caf();
            return new DefaultResponse((object[]) response);
        }

        DefaultResponse SetParam(string key, Arg value)
        {
            Arg[] args = {CallerId, key, value};
            object response = MethodCall("setParam", args);
            return new DefaultResponse((object[]) response);
        }

        async Task<DefaultResponse> SetParamAsync(string key, Arg value, CancellationToken token = default)
        {
            Arg[] args = {CallerId, key, value};
            object response = await MethodCallAsync("setParam", args, token).Caf();
            return new DefaultResponse((object[]) response);
        }

        GetParamResponse GetParam(string key)
        {
            Arg[] args = {CallerId, key};
            object response = MethodCall("getParam", args);
            return new GetParamResponse((object[]) response);
        }

        async Task<GetParamResponse> GetParamAsync(string key, CancellationToken token = default)
        {
            Arg[] args = {CallerId, key};
            object response = await MethodCallAsync("getParam", args, token).Caf();
            return new GetParamResponse((object[]) response);
        }

        SearchParamResponse SearchParam(string key)
        {
            Arg[] args = {CallerId, key};
            object response = MethodCall("searchParam", args);
            return new SearchParamResponse((object[]) response);
        }

        async Task<SearchParamResponse> SearchParamAsync(string key, CancellationToken token = default)
        {
            Arg[] args = {CallerId, key};
            object response = await MethodCallAsync("searchParam", args, token).Caf();
            return new SearchParamResponse((object[]) response);
        }


        SubscribeParamResponse SubscribeParam(string key, CancellationToken token = default)
        {
            Arg[] args = {CallerId, key, CallerUri};
            object response = MethodCall("subscribeParam", args);
            return new SubscribeParamResponse((object[]) response);
        }

        async Task<SubscribeParamResponse> SubscribeParamAsync(string key, CancellationToken token = default)
        {
            Arg[] args = {CallerId, key, CallerUri};
            object response = await MethodCallAsync("subscribeParam", args, token).Caf();
            return new SubscribeParamResponse((object[]) response);
        }

        UnsubscribeParamResponse UnsubscribeParam(string key)
        {
            Arg[] args = {CallerId, key, CallerUri};
            object response = MethodCall("unsubscribeParam", args);
            return new UnsubscribeParamResponse((object[]) response);
        }

        async Task<UnsubscribeParamResponse> UnsubscribeParamAsync(string key, CancellationToken token = default)
        {
            Arg[] args = {CallerId, key, CallerUri};
            object response = await MethodCallAsync("unsubscribeParam", args, token).Caf();
            return new UnsubscribeParamResponse((object[]) response);
        }

        HasParamResponse HasParam(string key)
        {
            Arg[] args = {CallerId, key};
            object response = MethodCall("hasParam", args);
            return new HasParamResponse((object[]) response);
        }

        async Task<HasParamResponse> HasParamAsync(string key, CancellationToken token = default)
        {
            Arg[] args = {CallerId, key};
            object response = await MethodCallAsync("hasParam", args, token).Caf();
            return new HasParamResponse((object[]) response);
        }

        GetParamNamesResponse GetParamNames()
        {
            Arg[] args = {CallerId};
            object response = MethodCall("getParamNames", args);
            return new GetParamNamesResponse((object[]) response);
        }

        async Task<GetParamNamesResponse> GetParamNamesAsync(CancellationToken token = default)
        {
            Arg[] args = {CallerId};
            object response = await MethodCallAsync("getParamNames", args, token).Caf();
            return new GetParamNamesResponse((object[]) response);
        }

        object[] MethodCall(string function, Arg[] args)
        {
            object tmp = XmlRpcService.MethodCall(MasterUri, CallerUri, function, args, TimeoutInMs);
            if (tmp is object[] result)
            {
                return result;
            }

            throw new ParseException($"Rpc Response: Expected type object[], got {tmp.GetType().Name}");
        }

        async Task<object[]> MethodCallAsync(string function, Arg[] args, CancellationToken token = default)
        {
            object tmp = await XmlRpcService
                .MethodCallAsync(MasterUri, CallerUri, function, args, TimeoutInMs, token)
                .Caf();
            if (tmp is object[] result)
            {
                return result;
            }

            throw new ParseException($"Rpc Response: Expected type object[], got {tmp.GetType().Name}");
        }
    }

    internal sealed class GetParamResponse : BaseResponse
    {
        public object? ParameterValue { get; }

        internal GetParamResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            ParameterValue = a[2];
        }
    }

    internal sealed class SearchParamResponse : BaseResponse
    {
        public string? FoundKey { get; }

        internal SearchParamResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is string foundKeyStr))
            {
                MarkError();
                return;
            }

            FoundKey = foundKeyStr;
        }
    }

    internal sealed class SubscribeParamResponse : BaseResponse
    {
        public object? ParameterValue { get; }

        internal SubscribeParamResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is string parameterValue))
            {
                MarkError();
                return;
            }

            ParameterValue = parameterValue;
        }
    }

    internal sealed class UnsubscribeParamResponse : BaseResponse
    {
        public int NumUnsubscribed { get; }

        internal UnsubscribeParamResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is int numUnsubscribed))
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

        internal HasParamResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is bool hasParam))
            {
                MarkError();
                return;
            }

            HasParam = hasParam;
        }
    }

    internal sealed class GetParamNamesResponse : BaseResponse
    {
        public ReadOnlyCollection<string>? ParameterNameList { get; }

        internal GetParamNamesResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is object[] objNameList))
            {
                MarkError();
                return;
            }

            List<string> nameList = new List<string>();
            foreach (var objName in objNameList)
            {
                if (!(objName is string name))
                {
                    MarkError();
                    return;
                }

                nameList.Add(name);
            }

            ParameterNameList = nameList.AsReadOnly();
        }
    }
}
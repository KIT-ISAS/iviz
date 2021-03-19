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
        readonly RosMasterApi backend;

        public Uri MasterUri => backend.MasterUri;
        public Uri CallerUri => backend.CallerUri;
        public string CallerId => backend.CallerId;

        public ParameterClient(RosMasterApi backend)
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
                throw new ArgumentNullException(nameof(key));
            }

            value.ThrowIfEmpty();
            return SetParam(key, value).IsValid;
        }

        public async ValueTask<bool> SetParameterAsync(string key, XmlRpcArg value, CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            value.ThrowIfEmpty();
            return (await SetParamAsync(key, value, token).Caf()).IsValid;
        }

        public bool GetParameter(string key, out XmlRpcValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var response = GetParam(key);
            bool success = response.IsValid;
            value = success ? response.ParameterValue : default;
            return success;
        }

        public async ValueTask<(bool success, XmlRpcValue value)> GetParameterAsync(string key,
            CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var response = await GetParamAsync(key, token).Caf();
            bool success = response.IsValid;
            return (success, success ? response.ParameterValue : default);
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

        public async ValueTask<ReadOnlyCollection<string>> GetParameterNamesAsync(CancellationToken token = default)
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

        public async ValueTask<bool> DeleteParameterAsync(string key, CancellationToken token = default)
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

        public async ValueTask<bool> HasParameterAsync(string key, CancellationToken token = default)
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

        public async ValueTask<bool> SubscribeParameterAsync(string key, CancellationToken token = default)
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

        public async ValueTask<bool> UnsubscribeParameterAsync(string key, CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return (await UnsubscribeParamAsync(key, token).Caf()).IsValid;
        }

        DefaultResponse DeleteParam(string key)
        {
            XmlRpcArg[] args = {CallerId, key};
            var response = MethodCall("deleteParam", args);
            return new DefaultResponse(response);
        }

        async ValueTask<DefaultResponse> DeleteParamAsync(string key, CancellationToken token = default)
        {
            XmlRpcArg[] args = {CallerId, key};
            var response = await MethodCallAsync("deleteParam", args, token).Caf();
            return new DefaultResponse(response);
        }

        DefaultResponse SetParam(string key, XmlRpcArg value)
        {
            XmlRpcArg[] args = {CallerId, key, value};
            var response = MethodCall("setParam", args);
            return new DefaultResponse(response);
        }

        async ValueTask<DefaultResponse> SetParamAsync(string key, XmlRpcArg value, CancellationToken token = default)
        {
            XmlRpcArg[] args = {CallerId, key, value};
            var response = await MethodCallAsync("setParam", args, token).Caf();
            return new DefaultResponse(response);
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
            var response = await MethodCallAsync("getParam", args, token).Caf();
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
            var response = await MethodCallAsync("searchParam", args, token).Caf();
            return new SearchParamResponse(response);
        }


        SubscribeParamResponse SubscribeParam(string key, CancellationToken token = default)
        {
            XmlRpcArg[] args = {CallerId, key, CallerUri};
            var response = MethodCall("subscribeParam", args);
            return new SubscribeParamResponse(response);
        }

        async ValueTask<SubscribeParamResponse> SubscribeParamAsync(string key, CancellationToken token = default)
        {
            XmlRpcArg[] args = {CallerId, key, CallerUri};
            var response = await MethodCallAsync("subscribeParam", args, token).Caf();
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
            var response = await MethodCallAsync("unsubscribeParam", args, token).Caf();
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
            var response = await MethodCallAsync("hasParam", args, token).Caf();
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
            var response = await MethodCallAsync("getParamNames", args, token).Caf();
            return new GetParamNamesResponse(response);
        }

        XmlRpcValue[] MethodCall(string function, XmlRpcArg[] args)
        {
            return backend.MethodCall(function, args);
        }

        ValueTask<XmlRpcValue[]> MethodCallAsync(string function, XmlRpcArg[] args, CancellationToken token = default)
        {
            return backend.MethodCallAsync(function, args, token);
        }

        internal sealed class GetParamResponse : BaseResponse
        {
            public XmlRpcValue ParameterValue { get; }

            internal GetParamResponse(XmlRpcValue[]? a)
            {
                if (a is null ||
                    a.Length != 3 ||
                    !a[0].TryGetInteger(out int code) ||
                    !a[1].TryGetString(out string statusMessage))
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

            internal SearchParamResponse(XmlRpcValue[]? a)
            {
                if (a is null ||
                    a.Length != 3 ||
                    !a[0].TryGetInteger(out int code) ||
                    !a[1].TryGetString(out string statusMessage))
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
            public XmlRpcValue ParameterValue { get; }

            internal SubscribeParamResponse(XmlRpcValue[]? a)
            {
                if (a is null ||
                    a.Length != 3 ||
                    !a[0].TryGetInteger(out int code) ||
                    !a[1].TryGetString(out string statusMessage))
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

        internal sealed class UnsubscribeParamResponse : BaseResponse
        {
            public int NumUnsubscribed { get; }

            internal UnsubscribeParamResponse(XmlRpcValue[]? a)
            {
                if (a is null ||
                    a.Length != 3 ||
                    !a[0].TryGetInteger(out int code) ||
                    !a[1].TryGetString(out string statusMessage))
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

            internal HasParamResponse(XmlRpcValue[]? a)
            {
                if (a is null ||
                    a.Length != 3 ||
                    !a[0].TryGetInteger(out int code) ||
                    !a[1].TryGetString(out string statusMessage))
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
            public ReadOnlyCollection<string>? ParameterNameList { get; }

            internal GetParamNamesResponse(XmlRpcValue[]? a)
            {
                if (a is null ||
                    a.Length != 3 ||
                    !a[0].TryGetInteger(out int code) ||
                    !a[1].TryGetString(out string statusMessage))
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

                if (!a[2].TryGetArray(out XmlRpcValue[] objNameList))
                {
                    MarkError();
                    return;
                }

                List<string> nameList = new();
                foreach (var objName in objNameList)
                {
                    if (!objName.TryGetString(out string name))
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
}
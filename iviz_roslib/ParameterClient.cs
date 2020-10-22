using System;
using System.Collections.ObjectModel;
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

        public ParameterClient(Uri masterUri, string callerId, Uri callerUri)
        {
            MasterUri = masterUri;
            CallerUri = callerUri;
            CallerId = callerId;
        }
        
        public bool SetParameter(string key, Arg value)
        {
            return SetParam(key, value).Code == StatusCode.Success;
        }

        public async Task<bool> SetParameterAsync(string key, Arg value)
        {
            return (await SetParamAsync(key, value).Caf()).Code == StatusCode.Success;
        }

        public bool GetParameter(string key, out object value)
        {
            var response = GetParam(key);
            value = response.ParameterValue;
            return response.Code == StatusCode.Success;
        }

        public async Task<(bool success, object value)> GetParameterAsync(string key)
        {
            var response = await GetParamAsync(key).Caf();
            bool success = response.Code == StatusCode.Success;
            return (success, success ? response.ParameterValue : null);
        }

        public ReadOnlyCollection<string> GetParameterNames()
        {
            var response = GetParamNames();
            if (response.IsValid)
            {
                return response.ParameterNameList;
            }

            throw new RosRpcException("Failed to retrieve parameter names: " + response.StatusMessage);
        }

        public async Task<ReadOnlyCollection<string>> GetParameterNamesAsync()
        {
            var response = await GetParamNamesAsync().Caf();
            if (response.IsValid)
            {
                return response.ParameterNameList;
            }

            throw new RosRpcException("Failed to retrieve parameter names: " + response.StatusMessage);
        }


        public bool DeleteParameter(string key)
        {
            return DeleteParam(key).Code == StatusCode.Success;
        }

        public async Task<bool> DeleteParameterAsync(string key)
        {
            return (await DeleteParamAsync(key).Caf()).Code == StatusCode.Success;
        }

        public bool HasParameter(string key)
        {
            return HasParam(key).HasParam;
        }

        public async Task<bool> HasParameterAsync(string key)
        {
            return (await HasParamAsync(key).Caf()).HasParam;
        }

        public bool SubscribeParameter(string key)
        {
            return SubscribeParam(key).Code == StatusCode.Success;
        }

        public async Task<bool> SubscribeParameterAsync(string key)
        {
            return (await SubscribeParamAsync(key).Caf()).Code == StatusCode.Success;
        }        
        
        public bool UnsubscribeParameter(string key)
        {
            return UnsubscribeParam(key).Code == StatusCode.Success;
        }
        
        public async Task<bool> UnsubscribeParameterAsync(string key)
        {
            return (await UnsubscribeParamAsync(key).Caf()).Code == StatusCode.Success;
        }

        DefaultResponse DeleteParam(string key)
        {
            Arg[] args = {CallerId, key};
            object response = XmlRpcService.MethodCall(MasterUri, CallerUri, "deleteParam", args);
            return new DefaultResponse((object[]) response);
        }

        async Task<DefaultResponse> DeleteParamAsync(string key)
        {
            Arg[] args = {CallerId, key};
            object response = await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, "deleteParam", args).Caf();
            return new DefaultResponse((object[]) response);
        }

        DefaultResponse SetParam(string key, Arg value)
        {
            Arg[] args = {CallerId, key, value};
            object response = XmlRpcService.MethodCall(MasterUri, CallerUri, "setParam", args);
            return new DefaultResponse((object[]) response);
        }

        async Task<DefaultResponse> SetParamAsync(string key, Arg value)
        {
            Arg[] args = {CallerId, key, value};
            object response = await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, "setParam", args).Caf();
            return new DefaultResponse((object[]) response);
        }

        GetParamResponse GetParam(string key)
        {
            Arg[] args = {CallerId, key};
            object response = XmlRpcService.MethodCall(MasterUri, CallerUri, "getParam", args);
            return new GetParamResponse((object[]) response);
        }

        async Task<GetParamResponse> GetParamAsync(string key)
        {
            Arg[] args = {CallerId, key};
            object response = await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, "getParam", args).Caf();
            return new GetParamResponse((object[]) response);
        }

        SearchParamResponse SearchParam(string key)
        {
            Arg[] args = {CallerId, key};
            object response = XmlRpcService.MethodCall(MasterUri, CallerUri, "searchParam", args);
            return new SearchParamResponse((object[]) response);
        }

        async Task<SearchParamResponse> SearchParamAsync(string key)
        {
            Arg[] args = {CallerId, key};
            object response = await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, "searchParam", args).Caf();
            return new SearchParamResponse((object[]) response);
        }


        SubscribeParamResponse SubscribeParam(string key)
        {
            Arg[] args = {CallerId, key, CallerUri};
            object response = XmlRpcService.MethodCall(MasterUri, CallerUri, "subscribeParam", args);
            return new SubscribeParamResponse((object[]) response);
        }

        async Task<SubscribeParamResponse> SubscribeParamAsync(string key)
        {
            Arg[] args = {CallerId, key, CallerUri};
            object response = await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, "subscribeParam", args).Caf();
            return new SubscribeParamResponse((object[]) response);
        }

        UnsubscribeParamResponse UnsubscribeParam(string key)
        {
            Arg[] args = {CallerId, key, CallerUri};
            object response = XmlRpcService.MethodCall(MasterUri, CallerUri, "unsubscribeParam", args);
            return new UnsubscribeParamResponse((object[]) response);
        }

        async Task<UnsubscribeParamResponse> UnsubscribeParamAsync(string key)
        {
            Arg[] args = {CallerId, key, CallerUri};
            object response = await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, "unsubscribeParam", args).Caf();
            return new UnsubscribeParamResponse((object[]) response);
        }

        HasParamResponse HasParam(string key)
        {
            Arg[] args = {CallerId, key};
            object response = XmlRpcService.MethodCall(MasterUri, CallerUri, "hasParam", args);
            return new HasParamResponse((object[]) response);
        }

        async Task<HasParamResponse> HasParamAsync(string key)
        {
            Arg[] args = {CallerId, key};
            object response = await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, "hasParam", args).Caf();
            return new HasParamResponse((object[]) response);
        }

        GetParamNamesResponse GetParamNames()
        {
            Arg[] args = {CallerId};
            object response = XmlRpcService.MethodCall(MasterUri, CallerUri, "getParamNames", args);
            return new GetParamNamesResponse((object[]) response);
        }

        async Task<GetParamNamesResponse> GetParamNamesAsync()
        {
            Arg[] args = {CallerId};
            object response = await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, "getParamNames", args).Caf();
            return new GetParamNamesResponse((object[]) response);
        }
    }

    sealed class GetParamResponse : BaseResponse
    {
        public object ParameterValue { get; }

        internal GetParamResponse(object[] a) : base(a)
        {
            ParameterValue = a[2];
        }
    }

    sealed class SearchParamResponse : BaseResponse
    {
        public string FoundKey { get; }

        internal SearchParamResponse(object[] a) : base(a)
        {
            FoundKey = (Code == StatusCode.Success) ? (string) a[2] : null;
        }
    }

    sealed class SubscribeParamResponse : BaseResponse
    {
        public object ParameterValue { get; }

        internal SubscribeParamResponse(object[] a) : base(a)
        {
            ParameterValue = (Code == StatusCode.Success) ? a[2] : null;
        }
    }

    sealed class UnsubscribeParamResponse : BaseResponse
    {
        public int NumUnsubscribed { get; }

        internal UnsubscribeParamResponse(object[] a) : base(a)
        {
            NumUnsubscribed = (int) a[2];
        }
    }

    sealed class HasParamResponse : BaseResponse
    {
        public bool HasParam { get; }

        internal HasParamResponse(object[] a) : base(a)
        {
            HasParam = (bool) a[2];
        }
    }

    sealed class GetParamNamesResponse : BaseResponse
    {
        public ReadOnlyCollection<string> ParameterNameList { get; }

        internal GetParamNamesResponse(object[] a) : base(a)
        {
            object[] tmp = (object[]) a[2];
            string[] nameList = new string[tmp.Length];
            for (int i = 0; i < nameList.Length; i++)
            {
                nameList[i] = (string) tmp[i];
            }

            ParameterNameList = nameList.AsReadOnly();
        }
    }
}
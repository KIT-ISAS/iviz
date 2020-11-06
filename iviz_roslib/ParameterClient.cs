using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using Iviz.Msgs;
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
            return SetParam(key, value).Code == StatusCode.Success;
        }

        public async Task<bool> SetParameterAsync(string key, Arg value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            value.ThrowIfEmpty();
            return (await SetParamAsync(key, value).Caf()).Code == StatusCode.Success;
        }

        public bool GetParameter(string key, out object? value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var response = GetParam(key);
            bool success = response.Code == StatusCode.Success;
            value = success ? response.ParameterValue : null;
            return success;
        }

        public async Task<(bool success, object? value)> GetParameterAsync(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var response = await GetParamAsync(key).Caf();
            bool success = response.Code == StatusCode.Success;
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

        public async Task<ReadOnlyCollection<string>> GetParameterNamesAsync()
        {
            var response = await GetParamNamesAsync().Caf();
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

            return DeleteParam(key).Code == StatusCode.Success;
        }

        public async Task<bool> DeleteParameterAsync(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return (await DeleteParamAsync(key).Caf()).Code == StatusCode.Success;
        }

        public bool HasParameter(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return HasParam(key).HasParam;
        }

        public async Task<bool> HasParameterAsync(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return (await HasParamAsync(key).Caf()).HasParam;
        }

        public bool SubscribeParameter(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return SubscribeParam(key).Code == StatusCode.Success;
        }

        public async Task<bool> SubscribeParameterAsync(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return (await SubscribeParamAsync(key).Caf()).Code == StatusCode.Success;
        }        
        
        public bool UnsubscribeParameter(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return UnsubscribeParam(key).Code == StatusCode.Success;
        }
        
        public async Task<bool> UnsubscribeParameterAsync(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

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

    internal sealed class GetParamResponse : BaseResponse
    {
        public object? ParameterValue { get; }

        internal GetParamResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage) ||
                !(a[2] is string parameterValue))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");                    
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }            
            
            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }            
            
            ParameterValue = parameterValue;
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
                !(a[1] is string statusMessage) ||
                !(a[2] is string foundKeyStr))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");                    
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
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
                !(a[1] is string statusMessage) ||
                !(a[2] is string parameterValue))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");                    
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
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
                !(a[1] is string statusMessage) ||
                !(a[2] is int numUnsubscribed))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");                    
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
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
                !(a[1] is string statusMessage) ||
                !(a[2] is bool hasParam))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");                    
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
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
                !(a[1] is string statusMessage) ||
                !(a[2] is object[] objNameList))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");                    
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }                 
            
            List<string> nameList = new List<string>();
            foreach (var objName in objNameList)
            {
                if (!(objName is string name))
                {
                    Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");                    
                    Code = StatusCode.Error;
                    hasParseError = true;
                    return;                    
                }
                
                nameList.Add(name);
            }

            ParameterNameList = nameList.AsReadOnly();
        }
    }
}
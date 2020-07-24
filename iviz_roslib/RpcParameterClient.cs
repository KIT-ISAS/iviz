using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using TopicTuple = System.Tuple<string, string>;
using TopicTuples = System.Tuple<string, string[]>;

namespace Iviz.Roslib.XmlRpc
{
    public sealed class ParameterClient
    {
        public Uri MasterUri { get; }
        public Uri CallerUri { get; }
        public string CallerId { get; }

        internal ParameterClient(Uri masterUri, string callerId, Uri callerUri)
        {
            MasterUri = masterUri;
            CallerUri = callerUri;
            CallerId = callerId;
        }

        public DefaultResponse DeleteParam(string key)
        {
            Arg[] args = { 
                new Arg(CallerId),
                new Arg(key)
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "deleteParam", args);
            return new DefaultResponse((object[])response);
        }

        public DefaultResponse SetParam(string key, Arg value)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(key),
                value
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "setParam", args);
            return new DefaultResponse((object[])response);
        }

        public GetParamResponse GetParam(string key)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(key),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "getParam", args);
            return new GetParamResponse((object[])response);
        }

        public SearchParamResponse SearchParam(string key)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(key),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "searchParam", args);
            return new SearchParamResponse((object[])response);
        }

        public SubscribeParamResponse SubscribeParam(string key)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(key),
                new Arg(CallerUri),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "subscribeParam", args);
            return new SubscribeParamResponse((object[])response);
        }

        public UnsubscribeParamResponse UnsubscribeParam(string key)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(key),
                new Arg(CallerUri),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "unsubscribeParam", args);
            return new UnsubscribeParamResponse((object[])response);
        }

        public HasParamResponse HasParam(string key)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(key)
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "hasParam", args);
            return new HasParamResponse((object[])response);
        }

        public GetParamNamesResponse GetParamNames()
        {
            Arg[] args = {
                new Arg(CallerId),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "getParamNames", args);
            return new GetParamNamesResponse((object[])response);
        }
    }

    public sealed class GetParamResponse : BaseResponse
    {
        public object ParameterValue { get; }

        internal GetParamResponse(object[] a) : base(a)
        {
            ParameterValue = a[2];
        }
    }

    public sealed class SearchParamResponse : BaseResponse
    {
        public string FoundKey { get; }

        internal SearchParamResponse(object[] a) : base(a)
        {
            FoundKey = (Code == StatusCode.Success) ? (string)a[2] : null;
        }
    }

    public sealed class SubscribeParamResponse : BaseResponse
    {
        public object ParameterValue { get; }

        internal SubscribeParamResponse(object[] a) : base(a)
        {
            ParameterValue = (Code == StatusCode.Success) ? a[2] : null;
        }
    }

    public sealed class UnsubscribeParamResponse : BaseResponse
    {
        public int NumUnsubscribed { get; }

        internal UnsubscribeParamResponse(object[] a) : base(a)
        {
            NumUnsubscribed = (int)a[2];
        }
    }

    public sealed class HasParamResponse : BaseResponse
    {
        public bool HasParam { get; }

        internal HasParamResponse(object[] a) : base(a)
        {
            HasParam = (bool)a[2];
        }
    }

    public sealed class GetParamNamesResponse : BaseResponse
    {
        public ReadOnlyCollection<string> ParameterNameList { get; }

        internal GetParamNamesResponse(object[] a) : base(a)
        {
            object[] tmp = (object[])a[2];
            string[] nameList = new string[tmp.Length];
            for (int i = 0; i < nameList.Length; i++)
            {
                nameList[i] = (string)tmp[i];
            }
            ParameterNameList = new ReadOnlyCollection<string>(nameList);
        }
    }
}

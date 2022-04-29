/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GetMapActionResult : IDeserializable<GetMapActionResult>, IActionResult<GetMapResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public GetMapResult Result { get; set; }
    
        /// Constructor for empty message.
        public GetMapActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new GetMapResult();
        }
        
        /// Explicit constructor.
        public GetMapActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, GetMapResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        public GetMapActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new GetMapResult(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetMapActionResult(ref b);
        
        public GetMapActionResult RosDeserialize(ref ReadBuffer b) => new GetMapActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Result is null) BuiltIns.ThrowNullReference();
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ac66e5b9a79bb4bbd33dab245236c892";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71XTVPcOBC9+1eoikNga2ZCPjabTRUHFmYJqZCwQPZCUZRs94yV2NJEkhlmf/2+lmyP" +
                "zUfCITA14LEtdT+9ft1qvSeZkxVFuCQy88roUqWXlZu75wdGlqde+toJFy7JAfkjuTghV5de2HBJdn7x" +
                "Jzk6PXgHh3kE8T5C2xBAonNpc1GRl7n0UswMkKt5QXZc0hWVjLJaUC7CW79akJtg4lmhnMB3TpqsLMuV" +
                "qB0GeSMyU1W1Vpn0JLyqaDAfM5UWUiyk9SqrS2kx3thcaR4+s7Iito6vo+816YzE4f47jNGOstorAFrB" +
                "QmZJOqXneCmSWmn/6iVPSDbOlmaMW5qD/8658IX0DJauF+CXcUr3Dj5+i4ubwDbIIXjJndgMzy5x67YE" +
                "nAACLUxWiE0gP175wmgYJHElrZJpSWw4AwOw+ownPdvqWdbBtJbatOajxbWPh5jVnV1e07hAzEpevavn" +
                "IBADF9ZcqRxD01UwkpWKtBcQnZV2lfCs6DLZ+Js5xiDMChHBVTpnMoUA5GKpfJE4b9l6iMalypNHUuO9" +
                "iZHwT0R2jgv75wC/bbMl3hxPP+0ffjoQ7WdHbOM/y5LCNFFIJ1bkWZApMT9ZDHxDUPSNmNsr5EG0ubt3" +
                "dvjvVPRsvhja5IjU1oJZiDAl5uhBho9PptOj47Ppfmf45dCwpYwgbcgSIYc8+AnU77yQMw8lK8+rtxwg" +
                "ug55oOeJ+MFnA38QSWAhCg5ZuSiJLSjvWisAunlGtkL2lVwKPG01kE+/7O1Np/s9yK+GkJewLLNCEcN2" +
                "dcYszGquA3cRcZ+b3b8+n6x5YTev73CTmrD0vA6yXGO/01Ne00+pYVU4gzSYSVXWlu6DdzL9MN3r4dsR" +
                "v9+GZ+krZf4eBYSEMrW/KZfRzzGmlEnU1GCzc1ajTnoJpFwhUKmVvpKlyu9bQKO8LlN2xJsnUF4nPW18" +
                "SMK1+LrgdQzv7X78uM7kHfHHQwGmhK2K7kT4EHYRk9vRGoLWM2Ur3tR4+/D9KhCQUD5YRF8mb3/BIh5G" +
                "M4tikH7RAW8b92ji4+fTs76pHfFnMLirWzKa3QOWRI6osRGKJMiOArYyiV2Ag8DLPPCWPiD3HNs2zDZT" +
                "ulRYPjJH6hulM9nYLUuzDP0ID0QqWM7bbrMCmGaj4hwTvb6Kp+SU1vM509gM8nTtkyfcyg73k6iA2II0" +
                "JDnP4eb1hD0ZlC4Lhd4i7Me9khLUQTn3QoehdambPeYmT5hPmvWDVZJjgtDiULVArMoSs9mmi8FbElx3" +
                "plvpQZJkuaQERP1WocGP6tK0FyjFgLcaRmFGlKcy+8ZqxIzYv6KddE7OKYbGLShTM5W1yRAQuEljnXu9" +
                "OACgqjokBeqcwqhJGzxuQh4pdFpeNUHrteFJ9/RzltULpNTqwIKISi4eG8bAYdtoW+LGFYnDIXg5RmvU" +
                "oBlxOx0lRNiOBad+fzQrDdFLZapK5VfCzGDStC4mSfK+f1YRiPkRDgL77UGAp/OaQUz3XOmZabWBd6GL" +
                "DDCsWY4r+RXT0NCTHcWS1iXo5vZoe2siRLdA2FhDY/FKroSxC7ZSQz3n26MX29sXmPRFf9NmqTkTxi8m" +
                "CRey84vg+tF10Vt7G47CIJVTnECywIatZMhINCrNrpIV0iJNySoIHaSHh8PINgzeLAORUy4CpUFI8piQ" +
                "eHbJ95chR9fcI8ymjM3QefWcg3+RzDCQz0Prd5hwxEZVjjCc8yh30Z6awsNmQEE49/mbI+LTxqmxCkW1" +
                "XRFDOK9GAl8rc45TeywMMSRZjpfGgqsFjj/NJBgKIg1yaMMNQ5NkTmjJvF1F2o/DlODukSJ82x+w7a6z" +
                "JwYVqAN6IJ2hsqJWyYxGoaUxXJPjexUFgA0ZkNu5E5EcG5DYDUj+qVEyrQ521+MeS8I3FwgorYLR3HDN" +
                "bwpEix9rwdE8QB4sN2rqzWtx3f1adb/+exr4a+ruKooDPofg+e77mndOV9S9H6+o/bVMkv8B3mBxBc0R" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

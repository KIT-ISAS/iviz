/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/GetMapActionResult")]
    public sealed class GetMapActionResult : IDeserializable<GetMapActionResult>, IActionResult<GetMapResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public GetMapResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapActionResult()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Result = new GetMapResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMapActionResult(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, GetMapResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMapActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new GetMapResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMapActionResult(ref b);
        }
        
        GetMapActionResult IDeserializable<GetMapActionResult>.RosDeserialize(ref Buffer b)
        {
            return new GetMapActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
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
                "H4sIAAAAAAAACr1X30/bSBB+t5T/YaU+FE5JSn9cr4fEAwc5SgUtB/ReEEJrexJva++mu2tC7q+/b9a/" +
                "YghqHgpRwI49O/PNfDOzsx9JpmRFFi6RTLwyOlfxTeFm7tWRkfmFl750woVLdET+VM7PyZW5FzZcBtHe" +
                "L/4MotOLo12YTCsYHwO4QfRCAIxOpU1FQV6m0ksxNQCvZhnZUU63lDPQYk6pCG/9ck5uzCsvM+UEvjPS" +
                "ZGWeL0XpIOWNSExRlFol0pPwqqCeAl6qtJBiLq1XSZlLiwXGpkqz/NTKgoJ+/nP0oySdkDg+3IWUdpSU" +
                "XgHUEjoSS9IpPcNLCJdK+7dveAUWXi7MCL9pBiJaBMJn0jNiupsj0AxWul0281vl4xjqESSCodSJrfDs" +
                "Bj/dtoAdoKC5STKxBfhnS58ZDY0kbqVVMs6JNSeIA9S+5EUvt1dVM/RdoaU2jf5KZWdkE726U8xujTKQ" +
                "l3MIXDlDHCE5t+ZWpZCNl0FLkivSXiABrbTLQcTLKqNQ8jcHG2JYF7jBVTpnEgUmUrFQPhtEzls2EHi5" +
                "UekgerLsfLRUBhHfg+UZLgEDk/2hrqDm19nk8+Hx5yPRfPbEDv5znlJYKDLpxJI8Z2hMHKikSoI6UpV5" +
                "0G9vuTQqpfsHl8f/TsSK0td9pUxOaS1ijJyMiUO1meaz88nk9OxycthqftPXbCkhpDqSFPQjVfgJqsF5" +
                "Iaceea08B8AyU3QX6kLPBlEH9eHnBf6QMCEQVfahUuc5sQrlXaMGULcuyRYoyJz7g6ftBvTF14ODyeRw" +
                "BfTbPugFVMskU2gcKZIy4UBMS24O62LxqJ39v76cd6FhO+/W2IlN8D4tQ4Z26NeaSkv6eXQ4N5xBTUyl" +
                "yktLjwI8n3yaHKwg3BO/PwRo6RsljHAtIC4vU/r7STPcAGVMiUSzDUpbayX6p5fAyj0DPVzpW5mr9FEX" +
                "6gRsS2ZPvH+OBGwzUBsfyrHLwZbBLsoH+ycnXVHviT82hRgT9jFai3GjCIOYh5T1YeupsgXveLyttFSE" +
                "bs1QKO27sZosH36BGxuGmlOjV4iVBd5OHsuMky8Xl6u69sSfQeO+buJR7ypQJVJQx1qoioNso8BaxtWU" +
                "4JDoeRpCF29ShY6VG444h3WhEAGUEIzd66TYwvbz3CzCzMKiKArcmG4XA556A+NyEyvjFy9JKS5nsxDL" +
                "WsrTHcav593kjg+rcarel5toOc/Ms1dhz0ZsF5nC+BG265UeExKF0jAzHYf5JsxhawIGBaQ5l+ArOY4T" +
                "5iAq5mAtz3k5a3UVjwuC8VZ5k4fIT7LcZAKm/jTROIGWUw8haNHAuOwTMiVKY5l85+TkJdXIi/HTOTmj" +
                "iiY3p0RNVdJUR0DhxrX6MBhWEkBWlKFM0P4UxMYtk9Wo8mQ8anlbM7gyvw+i9vGXJCnnqLTlkUVACjl/" +
                "eiQ9k+2EbomHXRQUs/FmhCGqBjTkMbxKKcKWDWbzfFWaMw9ExjJWufJLYaas0zRGEOlB1DvrYAiHxCnO" +
                "EYfNOYJ1BN8RovaF0lPTpQteh+EzwLFmMSrkN6zEgYDssOp6bflu7Qx3tsdCtJ6ykg4jJ7XkblkN0FZq" +
                "JNTVzvD1zs41Vn3V37VZoACcGL0GfO51V9fB+HMkykoIWnIyg0KPcYpJQlRsIUO5YrKp958kkxY1TFah" +
                "AEBBeHiP6CaS99tEFVtuErkBP5AMEnh4ww9uqhLuWADxJq8mqKviFafD9SCaQpTPVd1LXgJfwEgKSq5Y" +
                "zl2356/wtBHJCOdI/0CmetyYNlahBTe+MZCrYijwtTJl2ppzZqCUZD5aGIuozXGQqhexppC9IT8a+qEJ" +
                "JM8I45y3y4qDs7AoGHwywh9aZHz7XWlVHAN6cAFop+jC6GgyoWGYhvA4rd+rKh+wjwN1s3aMSjsziGUr" +
                "MYj+KdFcrQ6aO8knzOv7bgJOm9aYjXiTqHtI4wU8wqk/4O45XSfZ+3firrtFaTe3/z2bF10Q17bPXmj7" +
                "PvCvHx0FXMqhQf7Us+Z2wdL/A1BeBko7EgAA";
                
    }
}

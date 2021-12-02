/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class XRGazeState : IDeserializable<XRGazeState>, IMessage
    {
        [DataMember (Name = "is_valid")] public bool IsValid;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "transform")] public GeometryMsgs.Transform Transform;
    
        /// Constructor for empty message.
        public XRGazeState()
        {
        }
        
        /// Explicit constructor.
        public XRGazeState(bool IsValid, in StdMsgs.Header Header, in GeometryMsgs.Transform Transform)
        {
            this.IsValid = IsValid;
            this.Header = Header;
            this.Transform = Transform;
        }
        
        /// Constructor with buffer.
        internal XRGazeState(ref Buffer b)
        {
            IsValid = b.Deserialize<bool>();
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Transform);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new XRGazeState(ref b);
        
        XRGazeState IDeserializable<XRGazeState>.RosDeserialize(ref Buffer b) => new XRGazeState(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(IsValid);
            Header.RosSerialize(ref b);
            b.Serialize(ref Transform);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 57 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/XRGazeState";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "b062fdd884f10dff560e6f7d4400606b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwW7bMAy96ysI9NB2SDOgHXYIsNuwrYcBHVrsGjA2YwuTJVei43pfvyc7cdqtw3bY" +
                "GgQwLfE9ko+kNyE4smm9Y2dL80m4lEj1+DCVhEY0DusmVen1XWSftiE2pAfLGPPuH//M59uPK0paTkGn" +
                "hMwJ3Sr7kmNJyIhLViYkQLWtaokXTnbiAOKmlZLGWx1aSUsA72qbUCBV4iWycwN1CU4aqAhN03lbsAqp" +
                "beQJHkjrianlqLboHEf4h1han923kRvJ7Pgnue/EF0LX71fw8UmKTi0SGsBQROFkfYVLMp31enWZAebk" +
                "rg8XeJUKcs/BSWvWnKw8tFFSzpPTCjFeTcUtwQ1xBFHKRGfj2Rqv6ZwQBClIG4qazpD5zaB18CAU2nG0" +
                "vHGSiQsoANbTDDo9f8Sc016RZx8O9BPjMcbf0PqZN9d0UaNnLlefugoCwrGNYWdLuG6GkaRwVrySs5vI" +
                "cTAZNYU0Jx+yxnACauwInpxSKCwaUFJvtTZJY2Yfu7HG+P6nafzNHhyGK0puFspIY0nzdtBGtBeBWn34" +
                "ZXgwkx6WoNyWC8yS+SqFhng14R2rDd586QCIHibFoNPZixS5T+aZEpl2491P+edNuB5nN3hMfiOMtmLJ" +
                "ZiSApY2AooYlWCUKRJIFWaUyQA8fFBwNfwOlYJAymtsWZPxYk3wMyJksq+WC+hr6jl55EMa1HRfdFhRt" +
                "ZctjN2Yw0764Ben2EoPk3JTzFAwtBMlB7fMlXW9pCB31uSAYcf99CWjvnNe4BxrCIn9c9hRPBb0J2HbI" +
                "khJXWBmfFF82dH3rAuvbN/QwW8NsfX+RVh9n7Lluewoxr+gk35Oe57f744Bmkf9Y0MHqjfkB38dFlnoG" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

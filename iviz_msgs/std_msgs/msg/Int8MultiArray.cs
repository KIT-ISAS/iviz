/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Int8MultiArray : IDeserializable<Int8MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public sbyte[] Data; // array of data
    
        /// Constructor for empty message.
        public Int8MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<sbyte>();
        }
        
        /// Explicit constructor.
        public Int8MultiArray(MultiArrayLayout Layout, sbyte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Int8MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<sbyte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Int8MultiArray(ref b);
        
        Int8MultiArray IDeserializable<Int8MultiArray>.RosDeserialize(ref Buffer b) => new Int8MultiArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + Data.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Int8MultiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d7c1af35a1b4781bbe79e03dd94b7c13";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR996+4JC9NlmT5KKUt5CEw2EsLgw7GCKGo1nWsxLaCJDfrfv2O5M+0exwT" +
                "Btv385wjXQ3pW8bCMmVaH0k4cinTY5k5tTFGvD2IN106ytlasWeSnKhCOaULSrSJhiR1XOZcOBFseESW" +
                "Ue7ThU+3syj6UIyy+l2tIdkTxypRcV0kISmcqKMiVbjb7a4J9it42zWk0KlJi6Jo/Y9X9Pj09Z6sk8+5" +
                "3dvP7/lAhe/QrCMNleJMGLYkaM8FGxVX3qlU0MqCpMg61AIFTsI4FZfIqti5txPPiL408ShlmLSRbFhS" +
                "YnRO6MyGcm09AKdJFUX9f6F5WwIaoj3k2rRyNS46GX1iIGAbldB7tQwonnWSWO7t00lIqYo9ccZ+zwHK" +
                "eSyF68RH+TjGYdHGkk11mUnaPPzY/HyiF6azUc5xAagE7Lm9BGGdUZJRQRSyORIgG3hOPa9ebKKM5zkk" +
                "PJ3wV2pymBxHtA5gtn0On3zyc9Viu9iN1aVluRsfYDnuoqGnACwAIYyc0GoapwLSZnRzPf91fTsnlftJ" +
                "OCuXggiwYXxegTPWmTZUB1tUOQf2oN1xEfY+NEDn7Xw3y8QL6gLuIGW1T92gc1n1m6H5mtCxZw1oYV2N" +
                "gWbs0azpbrm4mc+JrgrtuI6sxSRl6VBCuVAOagfso7rgoo/grKRLB52nBYBGPesFALwXd8vGveyXq3UY" +
                "dL624Kpna8sFWT7upOGEcZJwvP215CU3+jyhAz6gd5kXk3Bajv6/6jj7j/PfzlbkiWAwav4YleoLiu/V" +
                "K058e3Kb+arV8JdfvTXvAunKTwmuASpx4dpRm1hJ5hOrr7/0+AOTsSw71AUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

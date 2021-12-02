/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AccelStamped : IDeserializable<AccelStamped>, IMessage
    {
        // An accel with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "accel")] public Accel Accel;
    
        /// Constructor for empty message.
        public AccelStamped()
        {
            Accel = new Accel();
        }
        
        /// Explicit constructor.
        public AccelStamped(in StdMsgs.Header Header, Accel Accel)
        {
            this.Header = Header;
            this.Accel = Accel;
        }
        
        /// Constructor with buffer.
        internal AccelStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Accel = new Accel(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AccelStamped(ref b);
        
        AccelStamped IDeserializable<AccelStamped>.RosDeserialize(ref Buffer b) => new AccelStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Accel.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Accel is null) throw new System.NullReferenceException(nameof(Accel));
            Accel.RosValidate();
        }
    
        public int RosMessageLength => 48 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/AccelStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d8a98a5d81351b6eb0578c78557e7659";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvcQAy9+1cI9pCkbFxISg8LPQRK2xwKgYReg3Ys20PsGXdG3o376/tm7HUaeumh" +
                "7WJYf0hPek9Ps6EbR2yMdHS02lKQWoI4I2S8D5V1rEJ14F6IXUVqe4nK/VB8Ea4kUJv/ipuMkHGK4sNf" +
                "/hVf7z/vKGr12Mcmvp0rFxu6V7TEoaJelCtWptqjI9u0Ei47OaCj3KtUlL/qNEgskfjQ2ki4GnESuOsm" +
                "GiOC1IN034/OmsR65XrKR6aFWDRwUGvGjsNvIiV0XFG+j1nE2487xLgoZlSLhiYgmCAcrWvwkYrROr2+" +
                "SgnF5uHoL/EoDXRdi5O2rKlZeR6CxNQnxx1qvJnJlcCGOIIqVaTz/O4Rj/GCUAQtyOBNS+fo/G7S1jsA" +
                "Ch04WN53koANFADqWUo6u/gFObW9I8fOn+BnxJcafwLrVtzE6bLFzLrEPo4NBETgEPzBVgjdTxnEdFac" +
                "Umf3gcNUpKy5ZLH5lI2oaXx5IvjnGL2xGECVDVxEDQk9T+PRVv/KjY14uC5MsyWz/U/GOg0KvaXXcJha" +
                "6AOl6iCgMjA03Af/JOklTGc1gq0TyJF2jF2TvZVsBrt+E6M+XNMS8vK8xP0fhkvVE8cgiSPGBJJ0yN9e" +
                "EyzTGtxm43oH2/fCmCnIrplIrGxAKsQpgYpjB+srW8hBlYd6ziswen4CpMBFhGweBoBhlQO72M3CZgXp" +
                "XMqm3NKxhao5Krkg72zecmso2MZiyVMmCvVrMtNCbktaX8FFXTf3PBeDJQESvOaEi5Jua5r8SMdECDdh" +
                "OVw87dHi0ldeAvV+m06WBeK1oHces4csMXKDfXFRcayVRVF3nvX9O3pe76b17kfxE+DbH2GvBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

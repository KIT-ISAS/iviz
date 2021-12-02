/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class InertiaStamped : IDeserializable<InertiaStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "inertia")] public Inertia Inertia;
    
        /// Constructor for empty message.
        public InertiaStamped()
        {
            Inertia = new Inertia();
        }
        
        /// Explicit constructor.
        public InertiaStamped(in StdMsgs.Header Header, Inertia Inertia)
        {
            this.Header = Header;
            this.Inertia = Inertia;
        }
        
        /// Constructor with buffer.
        internal InertiaStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Inertia = new Inertia(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new InertiaStamped(ref b);
        
        InertiaStamped IDeserializable<InertiaStamped>.RosDeserialize(ref Buffer b) => new InertiaStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Inertia.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Inertia is null) throw new System.NullReferenceException(nameof(Inertia));
            Inertia.RosValidate();
        }
    
        public int RosMessageLength => 80 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/InertiaStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ddee48caeab5a966c5e8d166654a9ac7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC961cM5JCk2CokpQdDTi1tfAgEEnopaRhLY2mJtKvurmzL5Mf3zcpfueXQ" +
                "VkhImp335ntuhUvxVKdXNrfio2Ey4zvLbv7yld09fJ9RiOVzG6rw8XY0e0YPkW3JvqRWIpccmZYOXpmq" +
                "Fj9tZCUNQNx2UlI6jUMnIQfwsTaBcFcCl7lpBuoDlKKjwrVtb03BUSiaVt7ggTSWmDpGnEXfsIe+86Wx" +
                "qr703Iqy4w7yuxdbCM2/zqBjgxR9NHBoAEPhhYOxFQ4p642N11cKyM4e126KX6mQ24NxijVHdVY2nZeg" +
                "fnKYwcaHMbgc3EiOwEoZ6CLJnvEbLglG4IJ0rqjpAp7fD7F2FoRCK/aGF40ocYEMgPVcQeeXJ8zq9ows" +
                "W7enHxmPNt5Daw+8GtO0Rs0ajT70FRIIxc67lSmhuhgSSdEYsZEas/Dsh0xRo8ns7JvmGEpApYrgzSG4" +
                "wqAAJa1NrLMQvbKnajyb8l91YyUOXeeHsSV3I4AY7+AP/XypnrJl4zh+/kRtBvEXRISquiW1SaF9yt4y" +
                "/JAiOn+tDaj6+5l6FBvQ0yCctr+unnCi1yuZzQYPummzpVfVp5skhWTQZ5TudbdJYrYq3bsFhpPv4eR7" +
                "e/weTuTDiXy7/T953WVlP7JedASQSpSdVulMJ3LpBR3ScSG5Dt88jYuzGLZWGJ2EuT4gASyNB9Q4m4NV" +
                "vGBpyIRMpNJJIOsiOFp+ASWSL4rmrgMZFohnGxpWrIoBuZC8yie0rgWLQbW099KmSLvFFORNZbBaFAlD" +
                "7QHMtAtuQnF5hd5tmtHn0RgGASTexQS4zGm+pMH1tNaA8OF3K83RAi7u/EqjF52b6D7bUbxN6L3DgkFa" +
                "QuAKU2pDxDLNs0Nljz1xrPw2+wMdC6hy8AUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

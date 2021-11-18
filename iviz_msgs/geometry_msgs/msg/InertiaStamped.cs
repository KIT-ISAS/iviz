/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/InertiaStamped")]
    public sealed class InertiaStamped : IDeserializable<InertiaStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "inertia")] public Inertia Inertia;
    
        /// <summary> Constructor for empty message. </summary>
        public InertiaStamped()
        {
            Inertia = new Inertia();
        }
        
        /// <summary> Explicit constructor. </summary>
        public InertiaStamped(in StdMsgs.Header Header, Inertia Inertia)
        {
            this.Header = Header;
            this.Inertia = Inertia;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal InertiaStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Inertia = new Inertia(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new InertiaStamped(ref b);
        }
        
        InertiaStamped IDeserializable<InertiaStamped>.RosDeserialize(ref Buffer b)
        {
            return new InertiaStamped(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/InertiaStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ddee48caeab5a966c5e8d166654a9ac7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvcMBC9G/Y/DOSQpOy6kJYeFnJqabOHQCChl5KGWXvWFrElV5J34yU/vm/k/cqt" +
                "h7bGRvJo3pvRfN0Il+KpTku2sOKjYTLjmk2y67/8TLLb+29zCrF8akMV3t8kw5PsjO4j25J9Sa1ELjky" +
                "rRwcM1UtftbIWhqguO2kpHQah05CDuBDbQLhrQRec9MM1AcoRUeFa9vemoKjUDStvMEDaSwxdYyrFn3D" +
                "HvrOl8aq+spzK8qON8ivXmwhtPgyh44NUvTRwKEBDIUXDsZWOKSsNzZ+uFJAdvawcTP8SoXwHoxTrDmq" +
                "s/LSeQnqJ4c5bLwbL5eDG9ERWCkDXSTZE37DJcEIXJDOFTVdwPO7IdbOglBozd7wshElLhABsJ4r6Pzy" +
                "hFndnpNl6/b0I+PRxp/Q2gOv3mlWI2eN3j70FQIIxc67tSmhuhwSSdEYsZEas/Tsh0xRo8ns7KvGGEpA" +
                "pYxg5RBcYZCAkjYm1lmIXtlTNp5M+e8KshKHuvPDWJW7PtCyvIVL9OO5esxWjeP46SO1GcSfcSkk1q2o" +
                "TQrtY/aW4rsU0fkPWoOqv++sB7EBZQ3CWfvz6hEn+rySeXnBh4J62dKr6tN1kkIy6DdK97rbJDFble7d" +
                "AsPJfjjZb4/74UQ+nMi32/8V2l1cJvvG9aKNgGgi+bROh9qXKy+ok44LybUFF6lpnEXLtcKoJ3T3AQlg" +
                "aTygxtkcrOIFo0OmZCKVTgJZF8HR8jMoEX/UnCPuOpBhjHi2oWHFqhiQC8mrfEqbWjAeVEsrMM2LNGFM" +
                "Qd5UBgNGkTDUHsBMu9tNKa6uUMFNM/o8GkM7gMS7mACXOS1WNLieNnohbPxusDlawsWdX6kBo3NTnWo7" +
                "ircRvXMYMwhLCFyhV22ImKl5dkjusSyOyd9Ost9VyWNA+gUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

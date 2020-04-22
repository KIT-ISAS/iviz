
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class InertiaStamped : IMessage
    {
        public std_msgs.Header header;
        public Inertia inertia;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/InertiaStamped";
    
        public IMessage Create() => new InertiaStamped();
    
        public int GetLength()
        {
            int size = 80;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public InertiaStamped()
        {
            header = new std_msgs.Header();
            inertia = new Inertia();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            inertia.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            inertia.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ddee48caeab5a966c5e8d166654a9ac7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}

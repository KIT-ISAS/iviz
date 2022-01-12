/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ARMarkerArray : IDeserializable<ARMarkerArray>, IMessage
    {
        [DataMember (Name = "markers")] public ARMarker[] Markers;
    
        /// Constructor for empty message.
        public ARMarkerArray()
        {
            Markers = System.Array.Empty<ARMarker>();
        }
        
        /// Explicit constructor.
        public ARMarkerArray(ARMarker[] Markers)
        {
            this.Markers = Markers;
        }
        
        /// Constructor with buffer.
        public ARMarkerArray(ref ReadBuffer b)
        {
            Markers = b.DeserializeArray<ARMarker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new ARMarker(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ARMarkerArray(ref b);
        
        public ARMarkerArray RosDeserialize(ref ReadBuffer b) => new ARMarkerArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Markers);
        }
        
        public void RosValidate()
        {
            if (Markers is null) throw new System.NullReferenceException(nameof(Markers));
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) throw new System.NullReferenceException($"{nameof(Markers)}[{i}]");
                Markers[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Markers);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/ARMarkerArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "38745a121d365c2cc5cc1b47928542b2";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UTW/bRhA9e3/FAD7ELmS1TYICNZCDEaetD0Ecxy1QGAExIkfkNuQus7uSTP/6vllK" +
                "VBQYSA+NBQJaLufNm3nzcXHzlsMnCXcfqcuHaMyr//ln3n74/Zzs2j4UXazjjxdbTrMYktDt39dvioub" +
                "P1+/o1f00xd3729ev7t8g8ufjflDuJJATf4bbdLQi4kpWFdT6SsxtfhOUhhGkr+kTD68uHv5EV+DQ2JH" +
                "R8fU23tpqffRJusdbWxq6CHzLlvP6ZeXd7/CnjsJXFinzqMtjxQZ/AYK/eMDTfdfMV77KDssGMSYhfct" +
                "NRwLud9Cxg9brq3iRbQPArai68D0mFMFFUFaTnYtRfLFyHL0vUoVUzWyj7KbY/qQ2FUcKkJoXHFiWkKJ" +
                "xtaNhLNW1hA1Ju56qSh/1eLEOYC3jY2EpxaUgNt2oFWEUfKoStetnC1Za2k7OcADaR0x9RySLVctB9j7" +
                "UFmn5suA/NU7niifV+JKoavLc9i4KOVKZQKTdWUQjtogV5dkVijci+cKMMe3G3+GV6nRVBM5pYaTBiv3" +
                "fZCocXI8B8cPY3Jz+IY4ApYq0km+K/AaTwkkCEF6XzZ0gsivh9Sgv1IjtOZgedGKOi6hALw+U9Cz0y88" +
                "u+zasfM796PHPcd/cesmv5rTWYOatZp9XNUQEIZ98GtbwXQxZCdla8Ulau0icBiMokZKc/ybagwjoHJF" +
                "8M8x+tKiAFUenN3w5WoUtvpe3fjoXO9aK4iWCkkgPFrnb9o5yyDIpOdS5tokV7ms3qEpOmFkjP6bkABW" +
                "NgCKjTCHVwmC5pYZ2USVl0jOJ/jo+BNcCjRWNPc9nKHRA7uog6ll8Qo5kXk9n9GmETdaqUa5o/MM2JKC" +
                "rW01IkHUTWCmbXIzSsvn0Lhtx5hHMhTM6B5KGXA6p6slDX5FG00Ih7AdPU8LmeLKLZK8n+ncbV18vWEw" +
                "CJAlRq7RTS4mDP3cTEvqfjoN0+nhSUqtuw/hXuxLNUrll3kjHtZ5pgtFr6vt93HDYwTIB7vDohvGfHcG" +
                "5v0KDR1c9ru3e5pezqHsOhljnxjy58Gc4kcu2II55IN0v1GeJwl/L91j03ig52Hw+vZ5r7sOwTcbbnfa" +
                "GPMvocLe0bQIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

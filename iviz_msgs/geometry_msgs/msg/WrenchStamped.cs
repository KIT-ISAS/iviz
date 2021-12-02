/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class WrenchStamped : IDeserializable<WrenchStamped>, IMessage
    {
        // A wrench with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "wrench")] public Wrench Wrench;
    
        /// Constructor for empty message.
        public WrenchStamped()
        {
            Wrench = new Wrench();
        }
        
        /// Explicit constructor.
        public WrenchStamped(in StdMsgs.Header Header, Wrench Wrench)
        {
            this.Header = Header;
            this.Wrench = Wrench;
        }
        
        /// Constructor with buffer.
        internal WrenchStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Wrench = new Wrench(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new WrenchStamped(ref b);
        
        WrenchStamped IDeserializable<WrenchStamped>.RosDeserialize(ref Buffer b) => new WrenchStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Wrench.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Wrench is null) throw new System.NullReferenceException(nameof(Wrench));
            Wrench.RosValidate();
        }
    
        public int RosMessageLength => 48 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/WrenchStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d78d3cb249ce23087ade7e7d0c40cfa7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwW7TQBC971eMlENTlBipRRwicUBCQA5IlVrBsZrYY3uFvWt210nN1/NmnbpUcOAA" +
                "RFbstfe9mTfzZlf0lk5BXNnSyaaWgtSiS6HS+1BZx0moDtwLsaso2V5i4n4wH4UrCdTmm/lypsg3Y978" +
                "5Z/5dPthRzFV931s4ss5tlnRbUJSHCrqJXHFian2yMk2rYRtJ0fpKGcrFeWvaRokFgDetTYSrkacBO66" +
                "icaITclDdt+Pzpaqe1H7iAfSOmIaOCRbjh2HX8qk7LiifBtzGffvdtjjopRjskhoAkMZhKN1DT6SGa1L" +
                "11cKMKu7k99iKQ0quwSn1HLSZOVhCBI1T447xHgxiyvAjeIIolSR1vndPZbxkhAEKcjg0Zs1Mr+ZUusd" +
                "CIWOHCwfOlHiEhUA64WCLi5/Yta0d+TY+Uf6mfEpxp/QuoVXNW1b9KxT9XFsUEBsHII/2gpbD1MmKTsr" +
                "LlFnD4HDZBQ1hzSr99mKSduXO4I7x+hLiwZU2cImpqDsuRv3tvpXbmzEw3Vhmi05D8Cjs4Jop6AhqiFR" +
                "MBSpDgIVA5eyQbPgoJwxuu3VVdiJmgjqoWPGrsnmUp/Br5+lTD5c00z2tMQ/XPZ/BJ6D/kYh0zF/ey6y" +
                "0CnYZ996B9f3wmgpBmxBAljZAKj1rgArzh3oQ3VsospLJOcTOHr+CkqBiQhoHgaQYZIDu9ixYvU1IGsp" +
                "mmJDp1YwobpLTZBHNg+5LSnYxmLGFYlA/QJmOovbUKqvYKKum3Oeg8GRIAk+ZcBlQfuaJj/SSQXhIZzP" +
                "Fk8HpHjOK89A8n6jB8uZ4nlBbzx6j7LEyI0aJCacaoUxdec5vX5FD8vTtDx9Nz8AHy6CgrAFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

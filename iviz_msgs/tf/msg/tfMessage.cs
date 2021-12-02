/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class tfMessage : IDeserializable<tfMessage>, IMessage
    {
        [DataMember (Name = "transforms")] public GeometryMsgs.TransformStamped[] Transforms;
    
        /// Constructor for empty message.
        public tfMessage()
        {
            Transforms = System.Array.Empty<GeometryMsgs.TransformStamped>();
        }
        
        /// Explicit constructor.
        public tfMessage(GeometryMsgs.TransformStamped[] Transforms)
        {
            this.Transforms = Transforms;
        }
        
        /// Constructor with buffer.
        internal tfMessage(ref Buffer b)
        {
            Transforms = b.DeserializeArray<GeometryMsgs.TransformStamped>();
            for (int i = 0; i < Transforms.Length; i++)
            {
                GeometryMsgs.TransformStamped.Deserialize(ref b, out Transforms[i]);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new tfMessage(ref b);
        
        tfMessage IDeserializable<tfMessage>.RosDeserialize(ref Buffer b) => new tfMessage(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Transforms);
        }
        
        public void RosValidate()
        {
            if (Transforms is null) throw new System.NullReferenceException(nameof(Transforms));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Transforms);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "tf/tfMessage";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "94810edda583a504dfda3829e70d7eec";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/UMBC951eM6IEWtVkJEIcV7QkBPSCBWnFBqJomk8RqYgd70iX8ep6dTVIoAg7Q" +
                "VaS1Hc+bj/dmUovrRP141YU6bC4921A5310od72Unz6Tzkchy07/8S97d/FmS/VvQ8gO6LIxgeRr7yUE" +
                "CcRrTFR511HhnC+NZRXsuRNqhEvxedpcmQihjrSR+zeLxrTl1Xpx9tbBFddCcemCtiMNQUq6HhMMbr1k" +
                "arxUp48a1X672ezMjcm9C7nz9UarR2davdzwGfVc3AAojzYXAkANVLpi6MQqq3GWkAd8eLyyMaV0mGfZ" +
                "25TDPpUsqDe2/ilcOkjRTJlg66opyXhpOs2Waq41+180Bi0nBqfIY77KtmRfoprKJSunXBtTN+JPWrmV" +
                "FkaJZEpvdewl5DMFeGqx4rmdqw8SC9d1gzVFZFANWLprD0tjIY+evZpiaNnfIzyi4wnyZRBbCJ2/2uKO" +
                "DVIMahDQCITCC4dY7fNXlA3G6rOn0SA7uNy5E2ylBi+Lc5Scle4ItCQOW/h4MiWXAxvFEXgpAx2msyts" +
                "wxHBCUKQ3hUNHSLy96M2EETk8Ja94es2CbBABYD6OBo9PrqDHMPekmXrZvgJcfXxN7B2wY05nTTgrI3Z" +
                "h6FGAXGx9+7WlKv6i9ZAvNSaa89+zKLV5DI7eJ2kqJG+xAj+OQRXGBBQ0s5oMyt5abmHHSqzuLxEspBG" +
                "SCmtA+VadCeCau3cPfFAk2hXjy4OaGtoKfsohTr/bLJvU+tmHwYYeBtb27upxx8myX0wv0iR6Ta9+yn+" +
                "2AnnSbvOQvmdMGhFky2WMCyNh2kcSUAVTDxMqmNMMQwx1MM6BUbHN4AUCClac98DjO/WJB7D5FDyOj+m" +
                "XYP6pltRCKltU6ObgrypMccWNhZjpn1yx6TVUwipbaeYJ2egECBztY9yOq9odAPtYkJY+P18caB3iSv1" +
                "gTp3HIfLHuLHgr536Pb1U2CDYrKB9ap1rC+e09dlNS6rbw9C9aqxX7FtyfnYolP5fuA87r6sAo1F/mNC" +
                "82qXZd8BqQ5/iy4IAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

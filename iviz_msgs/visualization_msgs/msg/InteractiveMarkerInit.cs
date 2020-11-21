/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract (Name = "visualization_msgs/InteractiveMarkerInit")]
    public sealed class InteractiveMarkerInit : IDeserializable<InteractiveMarkerInit>, IMessage
    {
        // Identifying string. Must be unique in the topic namespace
        // that this server works on.
        [DataMember (Name = "server_id")] public string ServerId { get; set; }
        // Sequence number.
        // The client will use this to detect if it has missed a subsequent
        // update.  Every update message will have the same sequence number as
        // an init message.  Clients will likely want to unsubscribe from the
        // init topic after a successful initialization to avoid receiving
        // duplicate data.
        [DataMember (Name = "seq_num")] public ulong SeqNum { get; set; }
        // All markers.
        [DataMember (Name = "markers")] public InteractiveMarker[] Markers { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerInit()
        {
            ServerId = "";
            Markers = System.Array.Empty<InteractiveMarker>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public InteractiveMarkerInit(string ServerId, ulong SeqNum, InteractiveMarker[] Markers)
        {
            this.ServerId = ServerId;
            this.SeqNum = SeqNum;
            this.Markers = Markers;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public InteractiveMarkerInit(ref Buffer b)
        {
            ServerId = b.DeserializeString();
            SeqNum = b.Deserialize<ulong>();
            Markers = b.DeserializeArray<InteractiveMarker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new InteractiveMarker(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerInit(ref b);
        }
        
        InteractiveMarkerInit IDeserializable<InteractiveMarkerInit>.RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerInit(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ServerId);
            b.Serialize(SeqNum);
            b.SerializeArray(Markers, 0);
        }
        
        public void RosValidate()
        {
            if (ServerId is null) throw new System.NullReferenceException(nameof(ServerId));
            if (Markers is null) throw new System.NullReferenceException(nameof(Markers));
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) throw new System.NullReferenceException($"{nameof(Markers)}[{i}]");
                Markers[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.UTF8.GetByteCount(ServerId);
                foreach (var i in Markers)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerInit";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d5f2c5045a72456d228676ab91048734";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVa6VMbRxb/vFTxP3TZlTJsQGBwvIl2+YCRsFXBwIKco1IpVWumJXU8mp7MgZD/+v29" +
                "97rnQJC4tjbeHPZMT7/X775az9UoNmlpZ2ubzlVR5virp95XRammRlWp/b0yyqaqXBhVusxGKtVLU2Q6" +
                "Mttbz7GsS/xhC1WY/M7kauXyj4VyaW97S5D5DxMbb28RxK0BxjQyKq2WU5Nj33M1BvIosaBDrWySqKow" +
                "grR0KjaliUplZ8qWaqELtbRFYWKlVVFNC0ZWEo4qi3VpekoNcdravyqQWui5EbQLfUd4jSrAAujqEKJ0" +
                "QWh0Cm5xkgcEvjMmrBAUif1okrVaaZAK4qqUiIhyC1nNcrck7ISFUYi49Kwk5KA2ioBzViX81erEftKl" +
                "dRCtU/rO2VjlJjL2DjIjFHGVJTYiHsCIhpgqm5avXxHZE1AswjwFSUudfzR5gR2jFEfpqLR35j0v/vJr" +
                "+ErbT/7H/2xvvb9921d3tqhqZibLYl4cbBDCSrZLczDLSfQ2nTlW/GimFkbHMIMSXxWbEQv2cI8VJdSL" +
                "6CHi3JS5TouZy5ewAMjDsbChHMELYRodLRQhK0qT9dTPrhJoka2oP3Owr5kx8VRHH0VdjVkwJibuCmv5" +
                "yhZmT62BZmnni7LGE8AJVqvYzmYmJ/ttwM9drvI7+4k4AWOBBzooqnLe/GJm78EIw7xg1qdr3gAHIM94" +
                "x7LxIhKNj8R2mIce9F+4PbjIzKamEN7snSvxFcJRkJPKXcmKIfuYG7eEBNeipGuSAqHxiP8gDMwTN9UJ" +
                "7L6OB+L7ISQ0USD4G2uSfGSRu2q+aKIBRQ8fCBYuL0E7uU/GjrDzL/XqUEULTbYDo91twFrbPHSkEzqe" +
                "oxTFA2IWgtBVUqrIpWXukkLt+JWTl4RrljhdHh+pgmBbDmTSCn4fU0DhZxCeW8hTF4WLLFwwhvbKheeQ" +
                "TRLY3mPrEDvX5GV4nngwQXxhITo3a0iJbZEleu0p7aLa8JczgQLmAP8XOnBRxmIRYm4s3RLy0HkMxkpN" +
                "8YeJXsADTL6fmDuTAEovM3DDX8t1ZgofysEY/pubFCyxzZB2oKjILZewHw5p4qAtBN4Ltcp0XtqoSnQO" +
                "AJfHNqX9jVfR/3XkHg36JKDCRBXJDofZNMqNLshkRgMlQZM0bn4H4Hjl9vFu5vCpmgIxXlBs7rPcSGop" +
                "+nTM34XHHtBDSAYHxbAoXpvgtdhVOAdUmMwh5uyA/Ot1uaCADse407nVU5goMMPcEqB9QUAvdtuoifQ+" +
                "fCJ1Ab+gbA75HLxpg5jY2l9AeQn7cTWHHLEzy92djbHXxxefbBM7zXW+3t7i6MuHAsk5x1K2UNaN3fCE" +
                "2i1ZLz61/0XWuRm02G8RiEld4EKSKFyNwzqkNcsNmKEKZY+MjpZj/93yXvJ1B1f1sD3YyTUHzLBje+vf" +
                "FZjNU8bc7PyCbIKc2p0oBmib+gAfuABHcBemu8O0j3MoFe6bR+g4PH76Ylw0QqxZqbUGm+qItssDvf3e" +
                "qIASPrz/MzgLj6svW/DUuYAYrV/qIlJSwpBKk41QX6dMqm0hE53nek3K3UAjAfYU5gC3j4INhG2UsEo3" +
                "N1S0+HSJ1KgJhLLTgU9u9Hd4NCVSN2ROFQplabKvNQigUOByrrBgYrNEsykyYaiHqVgnKKoSqqissL2t" +
                "WAkyc65jpRqrcypLBywiMoeaG56o1TMEfXxHHHmmZtYkMZ3iMp9o6myMg3x57aja4ZRcQ6oTdQgwkkYH" +
                "AikEhZmELSnzLGc4wV1XfSvIvOY+wPiy0Lsf8RNkcM3HEp5w1pJKJZ1lBgF3amCwTKxFFlvYJMbuYAT3" +
                "CLKJ4QSzr5juY3pWXVZkqbQlIv2Jejar0mctiKM/h8jd1JVtmFebMEcPYLL8qA3xzZ9DQPt4IiD68BZG" +
                "DamLwtlEqF3iTML8AjEY8U9MoKDDG46unwWpr0oHnH6CvVAV8sCmSOndbrVRNWHwBdSeVHiw8yRWKZRP" +
                "aoLJ1CVC6E9xIryKIy0zLm9c2oYj93w3ypR5ZwIZI9kYPE72yqayY81AQLV+I1hqeBpK6nUhiHEdeGws" +
                "+Tr9+jcOC/m8WnJc9aUWsWvTmOstdkm/OKFaTe1MTeJWuzUm/1FwnbV3ciiQ1MPvEAfeM6q6KMxYdq6F" +
                "SRshBbdg3lFpRB9NLB3RcDh4c3r2PVVT5PjpZjQ8D21V07jDy+vyek3S8i0in8eLL1Bxygk3V7c3Hy77" +
                "qOWoJDTkBkUO3xE0upYRwhMorv1bpMWhR+3oqbszuwHdxemHy7N3XYyJrtJo8d8hJR1/W0vi5DCsCOUn" +
                "L1vvcvTJUVhqK/D/3M/7/oRl1Mrowdnqmlr6s6b1qROUiI6kVWP27dBT3WjTNvlDWEPU36ewGR6iFIWd" +
                "U8byceBOJ/gzWAr+3ujdw8hGvf0wkrwCUwblRBAMvG7FVWZyqkFosNWhQe2YHvpkraZVWeIbW/vuZrdL" +
                "yAetLj1xqN03Ghy1kxskWx5TuGZS0QlH0ig9FNoumd1qYREZQdzU0Nnc+VM3AyvksQlQe0ccSFfc92Iu" +
                "kZifqtwelr88GGmVbUsXm37T5S7cqlPVoZlPQ+Uyunw3vBmN+5iMJMmDfeDxMVNAzBj9NESX970xWQdA" +
                "JidkZX4ggu6l5ID/w2j44+T89Gx0CWM+Tcgk1vufFCww9eEEtZGBp+7c98moVuhzMeHpJ2ZW7qlP/Spr" +
                "/NSTTPlV1b5LFGGldtXWgZIiw4cWvROSU5jgeD699DYMO7S5l1eXw37TAZC54dGlaHOXhgaQBNjx1n+q" +
                "lII/DrinCSayEtejw8sPfQwkkIoJ5R4Zq8ppmrXP9ioZhvIUC59h3nwYj68QSIeJoeCGVjMlTyYZCZC3" +
                "pPdXPwwnpz+NbkFosDGlEwf7Exu/39f3tmj2Xl+cMlf1Zhiu7KyV5EPv+HQcUN+wKSPUugrG/DhiAehT" +
                "8pqynzXHsQu0ENbqJXF0dEuiaqtWxCBa9TsCw1S4NXDNWVxphRheHynlVGuzfMPya+Lg2fHgWeMA3jAK" +
                "HqaH0ROcwiEWfX37bnQ+/vpsfHMB+5KPxwOaKBYub4njeNAWMnVWPB2hvdxhtYVMe72INzcqpC7YCxEA" +
                "e57jC7XSUho1Y5kWT4zuvMJg7fX+4OqcUcaIsxTGPEGhCQ/DydYZwog/qlaUZwni+scD4fLit4+IVr58" +
                "13hjS7y1N/oZNBoZboLqWh+hxQ/OZW6rMWcl+4e3WQxfCMzXPBiOVpacJ8XYFcLpRDGcAhamDp6rE7QY" +
                "xaRGIGdLNt3Mk2gFIxS1kJr494NpB4v8wsh1BlVBy6xc830EX5yYh4NQxRMhCcMcWVyJoC2lPrVzgVfv" +
                "5TJPlvaPGtKpheJQzj3MV3tSslM4qafbD+bzrWznR/SSzDaDPfO0L0U0mmGZfPOMw18CBD5RfaPW4MwY" +
                "TmVsEBCnu4IZU+rPc+njVDx2dSJxuxPpSblSx4fygqyIOgO6LIhd+qL010SNgHlCx6pGyJ6TiGvnjiCc" +
                "XEPdZiVDnV5HM8wpupcVLra4i0bJm9bpqW1oqPlNhvKaWgiBnjySwj9j+E6y4mqoU/DEzhSsdy58nr1H" +
                "Wcv0S79Jo7RWeXEqY2DcELRPmmoeyIn3PAx6T038v1yx29xY3aKDXZRl1j84WK1WPVT+PZfPD1b2oz2g" +
                "m52DgTjsmKbfHo7j2h8CjSt0VKhYAkTx1fHpV0eHbzC1jvD37UIDG+f2JU0R6K4sX/oqKYWHc+PXvmth" +
                "GyLUTaw7vbm5+rFpLs4+vBk2rcXtNaxm2PQVZz9fjC4Hw5uT47CC9+Hkdnwzuj551Vm7GN2OT75po5Wl" +
                "113csliH6uur0eX49qSO0uPhT+NJy5dOvmvy7+27yc3w9urDzRkorhkAKaeXby884pcvW5wOBg2f768G" +
                "o/Ofm/fB8GI4bnEq76cXF8Tqg+s1mj089s/z8J3LtO4dptdLU+4XT2ERTJfh6pyiACyHHkiTbvob33Gn" +
                "vV5P7kvg3PC439BnsuJZxTIooPsNqoVCl0Mthi8hjUe0vRVGGupvf/sDcvyxmHjgTLmY3jyV0NZX/mKW" +
                "OrUZbmhKnojB3GOTGH5pSFBUduSBEB4cPEkKRsN+sNAl30eFp8EOlY7jA0QNNIo8TmbwPfWS7v2QDnj0" +
                "sbunjoRCmk01m46bRYRWWS2evCN95PTrVj4JhHehf8Cay4/lvvEBtNxfdsDVyz38y7V9UWfwnYoCFEoy" +
                "+kC/JCgwF88NT278td2ZS1x+8/YNT4ahn+5B/FX9ctg73H/ZO/x1eyuucokmiZ1BbbCeR4X7DrmG6/gW" +
                "gX58lmiet/GQU3pNjZhGvkA3UmsvWJriHnpuaCtGbj5Fya0RqgJ0EY8f7kdpIfX7c/lHFgDdF1AUAj3c" +
                "gW/+KKD1iwA6dV3/HoBz3xU1UOJkIn0Zc2UmshjWIHgjtxZQIxdT3rGWaPSR7iSQ7bXi456Cy0IZj1zf" +
                "oILgRF78VWc+90NRvmWGkv0Q2sBr/XSTOgTclaL48jVQA1GTdnlFXZNOsoUOZewaRQ0R+5iN8ZU0HSZ1" +
                "hEBzT1rfwkv3GSqeMKrE4tMgnbi/AQvlLSYof12V0/UW2xCvAX5iwFGMq00UO3APyqtf7H7L+/ejl1vq" +
                "jj92r7WkPBqVD/p4Hk15UP4xCUKXXEyiBMzZz6jo5dqLFMRTYY12HgBU8GP0lWXApjsNFvuBDKj2pFvh" +
                "XeyuTEeozajV8iOiptQgpMoziNZodiQlKFMtp/n5eujhdnvktVT7ctWLh9z/NsDVjQEo47vr0uFXK75X" +
                "EUo2vKf5JUkKz9U0bfiM67+/SO2bbtD8mARBLTzid1vhcdo84gpue+s/qoiw62cnAAA=";
                
    }
}

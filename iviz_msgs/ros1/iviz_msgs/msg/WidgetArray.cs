/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class WidgetArray : IDeserializable<WidgetArray>, IMessage
    {
        [DataMember (Name = "widgets")] public IvizMsgs.Widget[] Widgets;
    
        public WidgetArray()
        {
            Widgets = System.Array.Empty<IvizMsgs.Widget>();
        }
        
        public WidgetArray(IvizMsgs.Widget[] Widgets)
        {
            this.Widgets = Widgets;
        }
        
        public WidgetArray(ref ReadBuffer b)
        {
            b.DeserializeArray(out Widgets);
            for (int i = 0; i < Widgets.Length; i++)
            {
                Widgets[i] = new IvizMsgs.Widget(ref b);
            }
        }
        
        public WidgetArray(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeArray(out Widgets);
            for (int i = 0; i < Widgets.Length; i++)
            {
                Widgets[i] = new IvizMsgs.Widget(ref b);
            }
        }
        
        public WidgetArray RosDeserialize(ref ReadBuffer b) => new WidgetArray(ref b);
        
        public WidgetArray RosDeserialize(ref ReadBuffer2 b) => new WidgetArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Widgets.Length);
            foreach (var t in Widgets)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Widgets.Length);
            foreach (var t in Widgets)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Widgets is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Widgets.Length; i++)
            {
                if (Widgets[i] is null) BuiltIns.ThrowNullReference(nameof(Widgets), i);
                Widgets[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetArraySize(Widgets);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Widgets.Length
            foreach (var t in Widgets)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "iviz_msgs/WidgetArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "52c3708030aead19c44b9c8801a7076c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WW28aRxR+319xJD/Ermxa22maWsoDBurQOoYCTWVFERp2D8uoy8xmZhaMf32/mV0W" +
                "NqVKH4oRgtlz+ebcz8qVfJ4ubWq//1MmKbtPn2kdDjaKonf/8yf6ML67IfnVlVEhlXtL7c6kP3iYtrtd" +
                "ekc/NImj3ofBxx7ol4fo7ft7sK6iijd5HPamo8Gk7UW6/XFnDy/wxsNR/+Gu4lwe5lx3A+QebzJq/9rr" +
                "TAajx0rzusEdDO4n/SHIrxvk9uiuh59eG5wf9znDwbi/Z+Cbf+MFQ37a594O/njotkePoL89RO+873V+" +
                "A/PnKLIuKSP9nkXChhbhL5ptHJOIndQKIkaqlGRSUt0m5yhlvWRnNqXuUFumHD87uI7OtBnd3bYp9qdD" +
                "DMuxVokASCkyz7Rwb16TjUXGu6daqqRX1sQiD8bd6kIlINzqJ5r5MyT3iWMnljknKNodUCUn2R6rgL+K" +
                "anRCsMPfmRDCJhLhBM01oi3TBZuLjFecQSmYSoHro2xbUJwspCV8U1ZsRJZtqLAQchqBXS4LJWPhkyKX" +
                "3NCHplQkKBfGybjIhIG8NoiKF58bsWSPjq/lLwWrmKnfvYGMQpwKJ2HQBgixYWF9vPtdCpV0feUV6IQ+" +
                "jbS9/BydTNb6AnROUTy1FeQWwnmr+Sk3bL3Bwt7gsu9KL1u45KbKiKXTQJvi0Z4RboMtnOt4QadwYbhx" +
                "C60AyLRCzsQsYw+MWsiA+sorvTrbQ1YBWgmlt/Al4u6O/wKralzv08UCyct8GGyRIpIQzI1eyQSis00A" +
                "iTPJylEmZ8ZXoNcqr4xOfvHBhhC0QmrwL6zVsUQmEsxSt9gWdUjLFI12pLL8Z9fCwTYZ9kmC+cK3FOl5" +
                "6GVfP3PDcCMXMZ/7cvPkpOLLIIu4kDZyq9uiaKhRDbVA9HsBL40KuDu5l3IQpmxbCLXghFQ2ZKu2H76g" +
                "R4LJDXfr8fNUnzb16fllzN+FbutDnShUUCOeTeP905dd3DFolq3oGx5tT+ujj8R6AZR3YqCY+pTWp1l9" +
                "Esd/y9jbF4c2W4wwY4g3OR85dtpcY7o888vUQ3XjoWKgVeA1y6Dlp3s/jGGtMM2XLFDmWBy1JhQTaTgu" +
                "W3eCVcQoFvS6dJRotqS075+l+AuQjJnotUWeAwwbyghls7L8QIbKKbfS1jmtF6xKKT/TwioKy0vGZGQq" +
                "k1LTV2WtLKhy7pzc/AozMctKm8vL0LIAMbos9rMW9ee00QWtvUM4mGpnappxbVcY6U7rc78wK4gD8wFh" +
                "sVakvmmsw7b+ZqccJ9UHi7F6eYma72YHX3mivwEQH56ApwsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

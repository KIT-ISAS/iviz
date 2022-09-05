/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class WidgetArray : IDeserializable<WidgetArray>, IHasSerializer<WidgetArray>, IMessage
    {
        [DataMember (Name = "widgets")] public IvizMsgs.Widget[] Widgets;
    
        public WidgetArray()
        {
            Widgets = EmptyArray<IvizMsgs.Widget>.Value;
        }
        
        public WidgetArray(IvizMsgs.Widget[] Widgets)
        {
            this.Widgets = Widgets;
        }
        
        public WidgetArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<IvizMsgs.Widget>.Value
                    : new IvizMsgs.Widget[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new IvizMsgs.Widget(ref b);
                }
                Widgets = array;
            }
        }
        
        public WidgetArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<IvizMsgs.Widget>.Value
                    : new IvizMsgs.Widget[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new IvizMsgs.Widget(ref b);
                }
                Widgets = array;
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
            for (int i = 0; i < Widgets.Length; i++)
            {
                c = Widgets[i].AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "iviz_msgs/WidgetArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "7ede0cfb563479b8d3428c913c47d749";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UbU/bMBD+7l9xEh+AaXTjZQxV4kPWdl0noF0abUIIVW5yTSyldrCdQvj1Oztt2jLQ" +
                "9mFQRc3lXp67e+5ssRCPk7lJzYdfIknR3tzCvRcMY+z8P//Y5bjfBvEkJSuFtGcQdKLB8GoSdLtwDh+3" +
                "lWHvcvizR/rD5/TBxQWZjtjSFl2PepNoOLyIBqMNKK8Oh1HgIruDcWcDztvGo3Bw1V9ajp63HLvijrcS" +
                "hcH3XicahtfLyJOXrT7605Y9CPs9+usFZDndtIyG48FGpZ9fsnnMM8aMTWpSvyFPUEPmX2xaWQQeW6Ek" +
                "uWghUxBJrbVVgSxFNUerqzp2pAxCQX9ruI7KlQ77XwKInfScwWCsZDKp7bNccXt6AibmOa6/apdauawj" +
                "5sVmWSuUpfaVlu8JTWwHxpbLhOsEiAeecMthpog+kWaoD3JcYE5BfF5gAt7qaDMtCowyYYCeFCVqnucV" +
                "lIacrCKm5vNSipg7lsUct+IpUkjgUHBtRVzmXJO/0omQzn2m+RwdOj0G70qUMcKg2yYfSQyVVlBBFSHE" +
                "GrlxvA264Hfj+MgFwA7chMoc3rKd6F4dkB5T2oamCrAZt65qfCg0GlcwN21K9q7uskVJ2stZGNjzugl9" +
                "mn2gbFQLFirOYI9aGFU2U5IAERZcCz7N0QHTiHNC3XVBu/sbyNJDSy7VCr5GXOf4F1jZ4LqeDjIaXu7X" +
                "p0yJSXIstFqIhFynlQeJc4HSQi6mmuuKuag6Jdv56sgmJ4ryo6E3N0bFgiaR0D1os9Vy+rFM6OS80lr+" +
                "eQypwQA0uiFR+dydCVAzfzjd/sw0UhsFj/G9WzenTpZ24X2JF1BarGJbwEaKtqFxYD9K6lJLj7v2e6sG" +
                "qZTVEaJdsFxI46fV1E+90BnxJW+121wpD41UNdLj25S/pm7VQzMo2qAtPreLd193a97popm32F86Wkn3" +
                "r34lNjd6nZMuFN1IaSNNG4kz9huH8zcLPwgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<WidgetArray> CreateSerializer() => new Serializer();
        public Deserializer<WidgetArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<WidgetArray>
        {
            public override void RosSerialize(WidgetArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(WidgetArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(WidgetArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(WidgetArray msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<WidgetArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out WidgetArray msg) => msg = new WidgetArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out WidgetArray msg) => msg = new WidgetArray(ref b);
        }
    }
}

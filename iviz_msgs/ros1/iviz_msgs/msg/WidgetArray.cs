/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class WidgetArray : IHasSerializer<WidgetArray>, IMessage
    {
        [DataMember (Name = "widgets")] public Widget[] Widgets;
    
        public WidgetArray()
        {
            Widgets = EmptyArray<Widget>.Value;
        }
        
        public WidgetArray(Widget[] Widgets)
        {
            this.Widgets = Widgets;
        }
        
        public WidgetArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Widget[] array;
                if (n == 0) array = EmptyArray<Widget>.Value;
                else
                {
                    array = new Widget[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Widget(ref b);
                    }
                }
                Widgets = array;
            }
        }
        
        public WidgetArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Widget[] array;
                if (n == 0) array = EmptyArray<Widget>.Value;
                else
                {
                    array = new Widget[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Widget(ref b);
                    }
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
            b.Align4();
            b.Serialize(Widgets.Length);
            foreach (var t in Widgets)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Widgets, nameof(Widgets));
            foreach (var msg in Widgets) msg.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Widgets) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Widgets.Length
            foreach (var msg in Widgets) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/WidgetArray";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "72dda0549d1652367b0da36fa2ba4946";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UW0/bMBR+9684Eg/ANLpxGUOVeMjarusEtEujIYRQ5SaniaXEDrZbKL9+x06athto" +
                "exhUUXNyLt/5zsW+FkmK9vYOHrxgGGPn//nHLsf9NoiFeJoUJjUfrn0mNhfSnkHQiQbDq0nQ7cI5fNxW" +
                "hr3L4c8e6Q+f0wcXF2Q6YrUtuhn1JtFweBENRhtQXh0Oo8BFdgfjzgact41H4eCqX1uOnrccO3LHW4nC" +
                "4HuvEw3Dmzry5GWrj/60ZQ/Cfo/+egFZTjcto+F4sMH080s2j3nGmLFJ1dRvyBPUkPlXHcVjK5QkHy1k" +
                "CiKp1XZZIktRFWj1sooeKYNQ0t8asKNypcP+lwBiJz1nMBgrmUwq+yxX3J6egIl5juuvyqVS1kRiXm7y" +
                "WqHU2ldav98axXZgbLlMuE6A+sATbjnMFDVQpBnqgxwXmFMQL0pMwFtd20yLAqNMGKAnRYma5/kS5oac" +
                "rKJOFcVciphbBCsK3IqnSCGBQ8m1FfE855r8lU6EdO4zzQt06PQYvJ+jjBEG3Tb5SOrQ3AoitCSEWCM3" +
                "rm+DLviBHh+5ANiB21CZwzu2Ez2oA9JjSvvQsACbcetY42Op0TjC3LQp2buqyhYladezMLDndRP6NPtA" +
                "2YgLlirOYI9KGC1tpiQBIiy4FnyaowOmEeeEuuuCdvc3kKWHllyqFXyFuM7xL7CywXU1HWQ0vNyvzzyl" +
                "TpJjqdVCJOQ6XXqQOBcoLeRiqrleMhdVpWQ7X12zyYmi/GjozY1RsaBJJHQT2my1nH4sEzo6r7SWfx5D" +
                "KjAAjW5IRJ+7MwFq5g+n25+ZRiqj5DG+d+vm1EltF96X+gJKi1VsC9hI0TY0DuzHnKrU0uOu/d6qQKKy" +
                "OkK0C5YLafy0Gv5UC50RT3mr3OZKeWykZSM9vQ39detWNTSDog3a6uc2efd1v+47XTRFi/2lopX08OpX" +
                "YnOjVznpQtGNlDbStJE4Y78AgD985jcIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<WidgetArray> CreateSerializer() => new Serializer();
        public Deserializer<WidgetArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<WidgetArray>
        {
            public override void RosSerialize(WidgetArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(WidgetArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(WidgetArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(WidgetArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(WidgetArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<WidgetArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out WidgetArray msg) => msg = new WidgetArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out WidgetArray msg) => msg = new WidgetArray(ref b);
        }
    }
}

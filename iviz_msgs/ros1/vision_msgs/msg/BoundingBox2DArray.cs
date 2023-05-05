/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class BoundingBox2DArray : IHasSerializer<BoundingBox2DArray>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "boxes")] public VisionMsgs.BoundingBox2D[] Boxes;
    
        public BoundingBox2DArray()
        {
            Boxes = EmptyArray<VisionMsgs.BoundingBox2D>.Value;
        }
        
        public BoundingBox2DArray(in StdMsgs.Header Header, VisionMsgs.BoundingBox2D[] Boxes)
        {
            this.Header = Header;
            this.Boxes = Boxes;
        }
        
        public BoundingBox2DArray(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                VisionMsgs.BoundingBox2D[] array;
                if (n == 0) array = EmptyArray<VisionMsgs.BoundingBox2D>.Value;
                else
                {
                    array = new VisionMsgs.BoundingBox2D[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new VisionMsgs.BoundingBox2D(ref b);
                    }
                }
                Boxes = array;
            }
        }
        
        public BoundingBox2DArray(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                VisionMsgs.BoundingBox2D[] array;
                if (n == 0) array = EmptyArray<VisionMsgs.BoundingBox2D>.Value;
                else
                {
                    array = new VisionMsgs.BoundingBox2D[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new VisionMsgs.BoundingBox2D(ref b);
                    }
                }
                Boxes = array;
            }
        }
        
        public BoundingBox2DArray RosDeserialize(ref ReadBuffer b) => new BoundingBox2DArray(ref b);
        
        public BoundingBox2DArray RosDeserialize(ref ReadBuffer2 b) => new BoundingBox2DArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Boxes.Length);
            foreach (var t in Boxes)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Boxes.Length);
            foreach (var t in Boxes)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Boxes, nameof(Boxes));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += 40 * Boxes.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Boxes.Length
            size = WriteBuffer2.Align8(size);
            size += 40 * Boxes.Length;
            return size;
        }
    
        public const string MessageType = "vision_msgs/BoundingBox2DArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "583fadc917b98c913c8ed3ee2bf4514a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VVTW/cNhC961cM4MPGhXeLOkUOBnKwYbT1oYDRpKcgMLjiaMWWIlWS2l311/fN0Fp7" +
                "0RTIoTEWsCTOvPl685iLfRryLn//CxvLiXr91+xddjHUk7s4BevC7i4er+8/faZtPHJumvf/81/z64ef" +
                "byif59Nc0IdigjXJ0sDFWFMMdRF5ul3Pae15zx5OZhjZkp6WeeS8gePH3mXCb8eBk/F+pinDqERq4zBM" +
                "wbWmMBU38Jk/PF0gQ6NJxbWTNwn2MaEDYt4lM7Cg45f5r4lDy/RwfwObkLmdikNCMxDaxCajazikZnKh" +
                "vL0WB7qgT7/F/MPn5uLjIa7xnXdo+ykLKr0pkjUfx8RZEjb5BsG+q1VuEARdYoSzmd7otye85ktCNOTC" +
                "Y2x7eoMSHufSxwBApr1Jzmw9C3CLVgB1JU6ry1fIQaGDCXGBr4gvMb4GNpxwpaZ1j+F5aUOedugkDMcU" +
                "987CdDsrSOsdh0LebZNJcyNeNWRz8ZM0G0bw0tHgv8k5tg6TsHRwpW9ySYKuY3ly9lvR8j/3AXXe0vU9" +
                "lqJ+lO2oM2xNoC1TikWzNbDAYAs6hXI5CYduvSeLeoOAo7bEQr3RHdnnK9rCPrGwQBws2KuV+mgKHtZj" +
                "BHkAQhiDnzgLsTGDeECrt2sFQa+51cw39NAREuKjaUuNQG2Ko7Q0gcYusVUs2S1zSvp1VVdIfikKw5bN" +
                "eMnqTrPszbDKmGRgxTJ+FxOGNGwaXUeWRo0xu4KMlEu11EtkZgmmqNPoWeyUGmdNXdq24wglSHMdxiPI" +
                "CtR6uITJ7m8+w/8SXp5SWt7lMG7/4FY6jrqwxFoBWipHyJkF4/X0dA7vftRYT8fz1/lb0fBLtSPRe2GJ" +
                "qJnM8NFDeFi0TnPvJpDsrfaddQ4PYVFEilNCvaKGDInVzjvlkTibk+uJhKfhQHUTZEBaJ6MT0mAIeQTb" +
                "OteSGUcv6qqsHsyfNRN8BeOSLK+IgHRbDUDj5zZ7E1jB8ObSiWVqJdsQR6iDKvnBeWV35rSv6MjTBfgO" +
                "NUk7qS4gTgsVxWOtvaMDU2/gA3kXxcIaxNFhe1AVShCZ0Quklki/P1RuIkICm0YWctfoaBlNIwXmSqFY" +
                "ce2Exoqsp860dStHH8urlm3oFq5znM5wRM0owHDL2laR1edJoLQSo89Ln+VbixlrkhiZknteQT0wytFz" +
                "kRsIouDsFfRSIq1sTQ6AiZ3YDKK5WGgNa13XcdIvYZxAcq15KvIM5ShYasns5UYEfi9X8lJcOe029iRp" +
                "GlOpc5nNgRiblpbBktAWl3nOZscaSZrLqn7G49a083O5clavBRnQSVRtrFeC4KE2DHa57Jc7M8v1vejM" +
                "v7RFLnckO5jguugt/JflfVnj+fSE5Ippmn8AJA8xoigJAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<BoundingBox2DArray> CreateSerializer() => new Serializer();
        public Deserializer<BoundingBox2DArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<BoundingBox2DArray>
        {
            public override void RosSerialize(BoundingBox2DArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(BoundingBox2DArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(BoundingBox2DArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(BoundingBox2DArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(BoundingBox2DArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<BoundingBox2DArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out BoundingBox2DArray msg) => msg = new BoundingBox2DArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out BoundingBox2DArray msg) => msg = new BoundingBox2DArray(ref b);
        }
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class DetectionBoxArray : IHasSerializer<DetectionBoxArray>, System.IDisposable, IMessage
    {
        [DataMember (Name = "boxes")] public DetectionBox[] Boxes;
    
        public DetectionBoxArray()
        {
            Boxes = EmptyArray<DetectionBox>.Value;
        }
        
        public DetectionBoxArray(DetectionBox[] Boxes)
        {
            this.Boxes = Boxes;
        }
        
        public DetectionBoxArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                DetectionBox[] array;
                if (n == 0) array = EmptyArray<DetectionBox>.Value;
                else
                {
                    array = new DetectionBox[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new DetectionBox(ref b);
                    }
                }
                Boxes = array;
            }
        }
        
        public DetectionBoxArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                DetectionBox[] array;
                if (n == 0) array = EmptyArray<DetectionBox>.Value;
                else
                {
                    array = new DetectionBox[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new DetectionBox(ref b);
                    }
                }
                Boxes = array;
            }
        }
        
        public DetectionBoxArray RosDeserialize(ref ReadBuffer b) => new DetectionBoxArray(ref b);
        
        public DetectionBoxArray RosDeserialize(ref ReadBuffer2 b) => new DetectionBoxArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Boxes.Length);
            foreach (var t in Boxes)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
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
            foreach (var msg in Boxes) msg.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Boxes) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Boxes.Length
            foreach (var msg in Boxes) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/DetectionBoxArray";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "1236cfe810136e972389a81a1e55b319";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71X308bORB+vv0rRuWhUIXcAb0eqoROtLm2SBQ4yvWlqpCzO0ms7tqp7QW2f/19Y683" +
                "CUXqPRyNEsU7nhnPj88zsxMOXAZtzSt79+kzTe0d+6I4+p8/xfsPb1+SvtHfrhs/979O1k4tWm3CIR2/" +
                "vjo5P7s+nkzoiH7bJF7+9f7841+g7z1EPz49xdZ+UfhQJfXvWFXsaBH/ehEVzwOP02ZOuiqKV7Y1FR5g" +
                "A/zG2hdztg0H1yU1H2GjdQd0w7UtdehWB7y2tXWXb18dUymroleLAJa18h4hnNVWhRfPQfGldRJTz8Zb" +
                "l+QvLGx6Xdu22qelrK9LeSiKxwr9vdAUW/QhKFMpVxEcVpUKimYWIdPzBbvdmuEzhFSz5IribuiW7McQ" +
                "vFpoT/jO2bBTdd1R68EULGLRNK3RpQpMQTe8IQ9JbUjRUrmgy7ZWDvzWIQHCPnOqYdGOr+evLZuS6WTy" +
                "EjzGc9kGDYM6aCgdKy8ZPJlQzOzBvgjQFn26tH7vc7F1dWt3Qec5EDBYQWGhgljNd0tkQwxW/iUOe5a8" +
                "HOMQRIlxXOVpO9Ku8eh3CKfBFl7ackHbcOGiCwtroJDpRjmtpjWL4hKhgNanIvR0Z02ziaqNMjarTxpX" +
                "Z/wXtWbQKz7tLpC8WsLg2zkiCcalsze6Auu0i0rKWrMJVOupU64rRCodWWy9kWCDCVIxNfgHaAFxZKKi" +
                "Wx0W+ZrEtFzLZXkcWK4qwtplvHcLLxA3KuELYPvw/fT6Gz+Whd/bghQck2OBEYxSUlXIznCNYSbyOHOM" +
                "QC9VySO5EEKu+n0deZE5sk5n2TEVsRoMDMXfLfLgTNS74vtZDsKUfMmB1qC08RFPg/3wRaWiteluLnl0" +
                "N6y6YfXt55i/Cl32YUgUML4Rz03j5enrKu4ohc24+IFHeXX7c3zr0f6QY+hQsrfp0lhq6Uksetagdjas" +
                "kDKU6UESgpV2qRGPoZUdw3HgVgeqLHsyVrDQqC9Qie7FIq2WSyhDP3DK+DqFEmSIbPN4Ph7R7YJN4pIK" +
                "Egt/bBW6JKfnukqSEuFBWFHv3IjCbB8VqK6TzekwwA9KnE2J2xnTyYw629KtOISF6zuUpSkPdsUCGqwd" +
                "SXvqVTyAdYTFezUXAPiA3vjDrD9Oqr+fLNKR6G5uWM2H1XRYqUebGB4eVzL8ctgWtq4EgZiD6oQkKRBn" +
                "uxX6jfF4VHUqFl6QodFDG9VBSV9bSFVVLCtg02YFCt+CUwkCQakhKx0d+kI3Ig5lRCu0pDKUm5gHhGJj" +
                "hz1TzBWuo2ltp4JnT7UCUsDLvnR6OvTJ3pR4j2C4VLonM81w6gkp51QHQIjL3J8V57R0IhwRwFk3VwYd" +
                "qKL9Cp28QVR2a/2Fd7BDexWkt1u4gamLK2D3YqXGr8nCaIhHaZ81o6VXbRlNFTPRih1uLC/Dos+OH+IE" +
                "7Dq2cmIcD+xsd1ZjlAvJeun9cC4JJeNV+bXVqaKPYkuKM8O9gUyGrG0ZCw+qHGy/My42x2vs7E9ggGvL" +
                "0DrOUVwLV7ywaSaR4CFTQ0BG0CJ2ggg9e9GSW13BQ53aTs1mjqcHlObpLynIT1FYbJr0iU5qSgxMhmuf" +
                "XdUuA0KnmavHS4yNgGacuvIbgQKG+ASJophaWxM+2l9PNcpapdFVUGb92jg1bPyZjepnfCRpCdktOh2c" +
                "Wuuk0y7gNSFLOHub+e9LYGuT/xAGysm/9MyfLgHnz3SMfOTbF/dHcVKSyG5n9c9S9HZ6z+BWFQt9/wF2" +
                "XAuJGH3kVuFnUOzNjapRyRMifloFisl4uABJCtPVXuYaZE3GC6Lhuj7REF9/+cpaUuUZ9++KJ2dXh+L+" +
                "6oXzn54k75kDz96LSDlY4xHSET1f8UguQfl9jUdIR/Sip7w5PT8W0hH9sU5Brzmiw/xiideHhnNKzlS6" +
                "zhGTGTB2NvMcEsN5Ws+cbWTkdyHNnxKKdE37gyIo5L1OhCZ5zaaVQpMqg8c0oab2hvM5Jeb00BvyDkBs" +
                "lOmIa25iBe0vU7Ks+BdfiqdoXBAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            foreach (var e in Boxes) e.Dispose();
        }
    
        public Serializer<DetectionBoxArray> CreateSerializer() => new Serializer();
        public Deserializer<DetectionBoxArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<DetectionBoxArray>
        {
            public override void RosSerialize(DetectionBoxArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(DetectionBoxArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(DetectionBoxArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(DetectionBoxArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(DetectionBoxArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<DetectionBoxArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out DetectionBoxArray msg) => msg = new DetectionBoxArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out DetectionBoxArray msg) => msg = new DetectionBoxArray(ref b);
        }
    }
}

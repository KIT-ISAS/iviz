/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class Classification2D : IHasSerializer<Classification2D>, System.IDisposable, IMessage
    {
        // Defines a 2D classification result.
        //
        // This result does not contain any position information. It is designed for
        //   classifiers, which simply provide class probabilities given a source image.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of class probabilities. This list need not provide a probability for
        //   every possible class, just ones that are nonzero, or above some
        //   user-defined threshold.
        [DataMember (Name = "results")] public ObjectHypothesis[] Results;
        // The 2D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all use cases, so it may be empty.
        [DataMember (Name = "source_img")] public SensorMsgs.Image SourceImg;
    
        public Classification2D()
        {
            Results = EmptyArray<ObjectHypothesis>.Value;
            SourceImg = new SensorMsgs.Image();
        }
        
        public Classification2D(in StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.Image SourceImg)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceImg = SourceImg;
        }
        
        public Classification2D(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                ObjectHypothesis[] array;
                if (n == 0) array = EmptyArray<ObjectHypothesis>.Value;
                else
                {
                    array = new ObjectHypothesis[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new ObjectHypothesis(ref b);
                    }
                }
                Results = array;
            }
            SourceImg = new SensorMsgs.Image(ref b);
        }
        
        public Classification2D(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                ObjectHypothesis[] array;
                if (n == 0) array = EmptyArray<ObjectHypothesis>.Value;
                else
                {
                    array = new ObjectHypothesis[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new ObjectHypothesis(ref b);
                    }
                }
                Results = array;
            }
            SourceImg = new SensorMsgs.Image(ref b);
        }
        
        public Classification2D RosDeserialize(ref ReadBuffer b) => new Classification2D(ref b);
        
        public Classification2D RosDeserialize(ref ReadBuffer2 b) => new Classification2D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Results.Length);
            foreach (var t in Results)
            {
                t.RosSerialize(ref b);
            }
            SourceImg.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Results.Length);
            foreach (var t in Results)
            {
                t.RosSerialize(ref b);
            }
            SourceImg.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Results, nameof(Results));
            BuiltIns.ThrowIfNull(SourceImg, nameof(SourceImg));
            SourceImg.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += 16 * Results.Length;
                size += SourceImg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Results.Length
            size = WriteBuffer2.Align8(size);
            size += 16 * Results.Length;
            size = SourceImg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "vision_msgs/Classification2D";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b23d0855d0f41568e09106615351255f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VWXW/bNhR916+4gB+atLb6sWEoCgz7yrrmodvQFnsJgoAWryW2EqmRlB311+9cUlKc" +
                "NC36sBpObFO8h/fj3MO7ojPeGcuBFD07o6pVIZidqVQ0zpLnMLSxLFbFit41JkwLpB0MrItUORuVsaTs" +
                "SL0LJlkZu3O+SwglnUeCneZgasua8ARYtBzEPqzp0JiqoWC6vgWMd3ujOe+QX1u1NS2QcWRt9ozDKLjB" +
                "V0ymUzWXRfGKlWZPTfoogP8LtSZEcrv7UMocStphGT5JIPOp6mjvuHjLe/YpwGC27eTamt4PcobkLjYq" +
                "kvIMKPuRvVuT86S2bs9wteOEMQT2G52SrWGATDau1WXx1/Y9V/HV2LvYIEvh4nLKcihS1lnqolVU+ZSa" +
                "LXsVEwYHnvfSiSm5xK9aKoAY4KxqqcKXHnvdINlIfsAsJ+60pD8Rued/B+NzaUi1rThKlQqMCIMjE6lT" +
                "I22ZuOvjWBaBbXD+qgt1eHwuOFM1rkxXFz/+z6/i9ds/XlCIOp+XC40w3kZltfKaOo4qJUe8b0zdIMkt" +
                "ytXCSHUSek7d2KPwM4vxzmlswTeEi1w6ULnrBivMZ4qm41v2sBSWU698NNXQKo/9zmtjZfvOq44FHe+A" +
                "dLIFOc/PXkh7BK6GCNriJGMrzyoYW+MhFYOx8btnYkArunjjwtPLYvXu4DZY5xp8XrzIlYfXfN2j3uKw" +
                "Ci9w2MMcZYlDkCXGcRpMSGtX+BlO0VTiC/cODXaCEP4eYwOCCAn2yhsldAZwhVQA9YEYPTg9QrYJ2irr" +
                "ZviMeHPG18DaBVdi2jQoXitpCEONTJowt5+m7ZhAqtawjejRrVd+LMQqH1msXkqysUlURUqDT3Sjq0xq" +
                "ioOJTRGiF/RUliuji29Ey70JaLbMzLtdLCJkyaVVapblXMlJNUVC71fNufPBR7CJ7NCxN5WwBpI2gWqO" +
                "+GANOXOgM/RH64SEtj/CEhka4tT5cOD8DG09gA0KfQDdaIZO2Q14qVPRLFK2TtooDYJqN25oNfXsBREd" +
                "0Dr3YegTXuqIpQHl3xayUdJb5lu5+Sd9P4dPJX6nTu0cxBIRKNOWBfj+w/ckdcpRHyuwk0azO3BDWLdX" +
                "LdKBHAhH5gRMCSnp11H24oaQuNc3IWerKRIQSxwXAK8s1OviyebpZVnsWqfEjVDBtW+lY3elcxYk9HkQ" +
                "JV2IoSxqD0Wa291Mu0+erOnJaeJ8hGj10Lqd8MmjWJKXad+dK5Gm14qm5RtlmbICeVcV7oGJi6nfFji6" +
                "77VgzV12BOV6iCRomB6lWxifXn0OyHlToyTYlw0+BapQ0xzgl5EeXS+EdWCVyHoqNO6FOFf9izE9Gm8D" +
                "aHewX2f48bYh/pwc37fKLoz9IsJ53rOkEzcZimL17dUJ6beUBmmpz8HNlLqjjTd+pL5qTRU/hyA7t9yo" +
                "vXFJowc7TS/FfHU1nBJ7Y5KB8/J6vrXWol/bXD7vDmG2PhgNfz6xTsv3GleuHTq7jEUt16BGau6QRAUS" +
                "4bQIv0xiKNrOQNCCrx4n4Kv5cSirXgQMCR/dQAeViRKmkcJ8lCnQ8oHmayRJKcY9FBZm3oWNzHLhZ1HJ" +
                "UObpB5swilqRYZQMja6lh7mDwE3TWHIs4c6OQOanIxbP51T8Pi8g7N5ccxtos6EKF6fFaNOxsngoUyY6" +
                "MH0LcPv+Qkol1QeMzTvvulTUeTbOhwdJFe70dtD8+Fih7matyXV/jqpcbQ0GKG0QYq5cOLqQl2c/LSNO" +
                "5P6WQy8HzJngAmpoa5AAHmzHyJkazzEBJ6Ajg4s3UIBLaFQcUHOUw5vrtClHLg6cpFMeJoqdFsV/J9cV" +
                "f9kMAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            SourceImg.Dispose();
        }
    
        public Serializer<Classification2D> CreateSerializer() => new Serializer();
        public Deserializer<Classification2D> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Classification2D>
        {
            public override void RosSerialize(Classification2D msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Classification2D msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Classification2D msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Classification2D msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Classification2D msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Classification2D>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Classification2D msg) => msg = new Classification2D(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Classification2D msg) => msg = new Classification2D(ref b);
        }
    }
}

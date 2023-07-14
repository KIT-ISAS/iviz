/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class Detection2D : IHasSerializer<Detection2D>, System.IDisposable, IMessage
    {
        // Defines a 2D detection result.
        //
        // This is similar to a 2D classification, but includes position information,
        //   allowing a classification result for a specific crop or image point to
        //   to be located in the larger image.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Class probabilities
        [DataMember (Name = "results")] public ObjectHypothesisWithPose[] Results;
        // 2D bounding box surrounding the object.
        [DataMember (Name = "bbox")] public BoundingBox2D Bbox;
        // The 2D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all use cases, so it may be empty.
        [DataMember (Name = "source_img")] public SensorMsgs.Image SourceImg;
    
        public Detection2D()
        {
            Results = EmptyArray<ObjectHypothesisWithPose>.Value;
            Bbox = new BoundingBox2D();
            SourceImg = new SensorMsgs.Image();
        }
        
        public Detection2D(in StdMsgs.Header Header, ObjectHypothesisWithPose[] Results, BoundingBox2D Bbox, SensorMsgs.Image SourceImg)
        {
            this.Header = Header;
            this.Results = Results;
            this.Bbox = Bbox;
            this.SourceImg = SourceImg;
        }
        
        public Detection2D(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                ObjectHypothesisWithPose[] array;
                if (n == 0) array = EmptyArray<ObjectHypothesisWithPose>.Value;
                else
                {
                    array = new ObjectHypothesisWithPose[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new ObjectHypothesisWithPose(ref b);
                    }
                }
                Results = array;
            }
            Bbox = new BoundingBox2D(ref b);
            SourceImg = new SensorMsgs.Image(ref b);
        }
        
        public Detection2D(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                ObjectHypothesisWithPose[] array;
                if (n == 0) array = EmptyArray<ObjectHypothesisWithPose>.Value;
                else
                {
                    array = new ObjectHypothesisWithPose[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new ObjectHypothesisWithPose(ref b);
                    }
                }
                Results = array;
            }
            Bbox = new BoundingBox2D(ref b);
            SourceImg = new SensorMsgs.Image(ref b);
        }
        
        public Detection2D RosDeserialize(ref ReadBuffer b) => new Detection2D(ref b);
        
        public Detection2D RosDeserialize(ref ReadBuffer2 b) => new Detection2D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Results.Length);
            foreach (var t in Results)
            {
                t.RosSerialize(ref b);
            }
            Bbox.RosSerialize(ref b);
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
            Bbox.RosSerialize(ref b);
            SourceImg.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Results, nameof(Results));
            BuiltIns.ThrowIfNull(Bbox, nameof(Bbox));
            Bbox.RosValidate();
            BuiltIns.ThrowIfNull(SourceImg, nameof(SourceImg));
            SourceImg.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 44;
                size += Header.RosMessageLength;
                size += 360 * Results.Length;
                size += SourceImg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Results.Length
            size = WriteBuffer2.Align8(size);
            size += 360 * Results.Length;
            size += 40; // Bbox
            size = SourceImg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "vision_msgs/Detection2D";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "9e11092151fa150724a255fbac727f3b";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71YXZPbthV956/AzD6sN5FoJ+7sZNzptLG3afYhrWu7nzs7OxAJSnBAgAHIlehf33Pv" +
                "BSlpV3byUHvHY0kkcHA/z70XZ+rKNNabpLT69krVpjdVb4NX0aTB9WVxVpypdxubFP4l21qno+qDrK6c" +
                "Tsk2ttK0ZaFWQ6+sr9xQA68LyTKS9U2IrSwBmFLaubC1fg2QY4R8qMJ6vEudqeiVqmLoFB7ZVq8NcK3v" +
                "IQJDQZKVUS5gv6lxkuo3+Knj2uTlZVH8aHSNnxv+KLDtFR2quhhWemUdhDSp+NvqPRT/cewCEJJN/7L9" +
                "5nVI5uY2C5VoJ3RehcHXJPwq7FQaYpx+08mBUcriZX74MuxoC5YWbEbDNta9xmrdq7XxJrLkdKiZTlJP" +
                "bGlK/FqTTSAnTKkdm6HD2gArh0bUByKreVGqv4YeW34ZbMQatqBzagBqpZNJC5WCsr1q9UgWM23Xj2WR" +
                "jE8h3rVpnZ5es3VTGGJl7my7Loo//J//ip/e/uWFSn0tB4pfoMfbXvtax1q1ptdsHRJ/Y9cbE5fO3BuH" +
                "Tbol3cV2Y2dSeRCXYkfnRtK3pqCoQtsOnsLKqN625mg/diJStOp07G01UERXIUQ4jJY3UbeG0PEvwZ7G" +
                "V0ZdX73AGp9MNfQWAo0U59HoRJ6/vlLFgKB8/i1tUGfq5k1I39wWZ++2YYnnhqJxlkJcD6nNroPDSWCd" +
                "XuCwr0TLEofASgbH1QgFfnaHn+kC+UeymC5UG8SIV6/HfhMk5u91tHrlDAFXMAVQz2nT+cUBsmdor32Y" +
                "4AVxf8ZvgfUzLum03MB5jsyQhjUsaTmz7m2NpauRQSpnDVLW2VXUcSxolxxZnP1AxsYi7GLX4BO5GSrL" +
                "WbFFEhapj4TObrmz9ecKy3ubkG0SmR8jA6j8vc9Jrjbza/EoDNpr608TXznlP4ISIaX80JoIakPohGZC" +
                "FPI1daneBcR0r3RdMxKS/wBLaXBQn/Mfp19fIbkHhIRGMoA9NkOr/RLBWbPnPOy2gPFTT1kCl2/C4GrV" +
                "mUiISAMXws9Dx3icFnMW0n8rkEep3hpzZKB/8vdryFTiN6drG6IhDbR1ZYGgv/ydImeJ1nuuHYnIYaoG" +
                "AUKhd68dzAEbUKBMBph4VL0cae09omcuHqyy7MqaILom5o/ag8Nuni2/uS2LxgVNYqQKok2SXF6Rf+YT" +
                "H/myFFLhNRl/ZfjgmuskJSvvnFBSQAA3doc30TQmslZSo3IOyRmTj2beXhvs7CkIKugHhsgiHRUYGIsz" +
                "aF7RUumSlYw0VZwz4n8jkdjPGuDToyxk9vu9JNFRLFXaUz2YlFuNjApmpIwFYyUUBeba7x0KiJ/PqG3D" +
                "yvZSwqneo+5Y9l6TpU5yWId6xfFEtWej77M5Z4D9xuwcnJeNM0q4Ue5RDr4KzEdiYaTj52GCXzt7KjzR" +
                "EH9DA+qdxNweFkaqpE5DRGIv5HtlIvEClduCwER0gLwJ22Wr38NYM5L4JAfC5e4S0T9rDIdFu8thHKKd" +
                "l8NZMHRPuU3lXks0LvXuUMbMQmfqGvgRdXchUbzfC5dTFXqyW6hxoT4sVAz9Ad+ofytCfPT4P6cf/5cf" +
                "X0xJePP88vZAmc9F4o9dR5F7wr6P3bWgnoEe1/m9cDiK26GxSwUfUm5PC4q/DyhV0TPuft2XUhCiTOE4" +
                "15/MTnbSVWc6OlJ3Zsfd/G2cv334MuLvTXcqpY7s+SC18OuXvd2JzZBcn9Zo+rb9Eg3EUf/PIfhwcpCW" +
                "QciXU4cqC2cOlXDhe6FdB6ps0acDnJOUlO+Q4S7JwDXbDAgD96OsKr4suxwfSgpm4sGNhi/UotWSQYh9" +
                "K5a8VNcNWd3sNCqivOTJi/0icwVjyXQ2CX2o1YImjKwUukVqrfdSvWQpN7o9TyjZ3uRRcA0v95u2PBiQ" +
                "5uB9Mqt68TART1bLyWyPaYCGVX45HZPsB3OEfwrv9HgHa0Avm+sYTHrYDhx6b25AcNbd7vjn+OUokCPw" +
                "iqKEp2T8eO0wuRiuFiR7MyDInkvtZT+gRuSRCsMmlScapwxmNElFjqNcavLWxwyLsS1ijuAx30tPACfM" +
                "M73uOpen/oTS9rNIgqeIuEjdP/WM7+UuAm2tn8zstDdTg2HjHGW8irIhdBgveBTcWsfRnUy8F3TIedj6" +
                "1AMPFjinQp+Dr6J7o7aG2xQujhh5kAahs4bbrjy+8wQqKqp/XEts4oSIaOoMBbecDpOpoVPeGAmhILj1" +
                "wA09YqQBmbE1O4c2bW+yEnNGrcYwHOFwQ0H93MqwWam7z56Aan0ILk12nud+AoXLOLjHc7AHVTmHig8L" +
                "gRRsvaCGFied1yIcAKOxtKal5gwJzcfu2zVu6JLoPPT0HczRn0unuR+pHfV6sZ6V6+fcNjICtNgqfhn1" +
                "VhlkWpw7R+6TcvfJJ5FxDbOfdjTZjFldeidzJTloJtU6SBNMeNANjp0qzDR0557tZJGnxp3vtzBH2Sa4" +
                "+leKC4Tr9We7Lnl4RTNpMplnrvvQG90m7JpvFWxe/eTZQj274NGa7s26pTMNjavRy0iR1z24KFP570zl" +
                "x/sLjHkuUrpCXchG5LF+hlOn/masaZg/gKLERTbnEQU4FT6j/hgQ3LW2TDSy4THQfmb6NNLXu3kkzjeL" +
                "Mkra9aaf5spP6vT1eAxQh63/bRs/HG8UlguZ5HJB+iTCtayZzSlZIvmyf5qRXrEZaGj/GNycccdXMHs5" +
                "eHIHb/cfQ+ASakAkNvBVEIqnTJbFdEMG2ibD7rcIsDxeTJdjC7ohWYn7YtimaffW1pDn0W5+fHJzFdzQ" +
                "+jSVfWfWCI3cDRE7YFwPTM65s2osSDXF6ikD302vU1l1nVQHZmUtgZLyzSU1Exo8j8Yq31ZxlVmo93As" +
                "tsWQlqDkmP5E9zCplFtWLFqb0tNFD1yWiOWp/2q1dfnWVy7JCXcSBFSUj5gln0zx5+kBzS/c1ajlUlUb" +
                "7T1qWGtAZn69kOmPv1EjctqR5EnUZOq5Qyt36pCbrzv4cKrI003/00OGemi1jfj9O3jlbmXRVNSY/7Ln" +
                "0sG93/zuj/NNam+6I4F+oFYDsQAf+jWCABKsxt5IaHx3cytABxtu3oABbsFRPdVbGZ95kWhOAjzhU77i" +
                "ELsoiv8BFoWBuQ0ZAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            SourceImg.Dispose();
        }
    
        public Serializer<Detection2D> CreateSerializer() => new Serializer();
        public Deserializer<Detection2D> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Detection2D>
        {
            public override void RosSerialize(Detection2D msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Detection2D msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Detection2D msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Detection2D msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Detection2D msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Detection2D>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Detection2D msg) => msg = new Detection2D(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Detection2D msg) => msg = new Detection2D(ref b);
        }
    }
}

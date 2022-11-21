/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class FacialLandmarks : IHasSerializer<FacialLandmarks>, IMessage
    {
        // This message contains a list of facial features detected on a face
        // (0, 0) is at top-left corner of image
        // Features' coordinates are expressed in normalised pixel coordinates 
        // (in the range [0., 1.]), from the top-left corner.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Facial landmarks naming
        // Follows dlib and OpenPose convention
        // https://github.com/CMU-Perceptual-Computing-Lab/openpose/blob/master/doc/02_output.md#face-output-format
        public const byte RIGHT_EAR = 0;
        public const byte RIGHT_PROFILE_1 = 1;
        public const byte RIGHT_PROFILE_2 = 2;
        public const byte RIGHT_PROFILE_3 = 3;
        public const byte RIGHT_PROFILE_4 = 4;
        public const byte RIGHT_PROFILE_5 = 5;
        public const byte RIGHT_PROFILE_6 = 6;
        public const byte RIGHT_PROFILE_7 = 7;
        public const byte MENTON = 8;
        public const byte LEFT_EAR = 16;
        public const byte LEFT_PROFILE_1 = 15;
        public const byte LEFT_PROFILE_2 = 14;
        public const byte LEFT_PROFILE_3 = 13;
        public const byte LEFT_PROFILE_4 = 12;
        public const byte LEFT_PROFILE_5 = 11;
        public const byte LEFT_PROFILE_6 = 10;
        public const byte LEFT_PROFILE_7 = 9;
        public const byte RIGHT_EYEBROW_OUTSIDE = 17;
        public const byte RIGHT_EYEBROW_1 = 18;
        public const byte RIGHT_EYEBROW_2 = 19;
        public const byte RIGHT_EYEBROW_3 = 20;
        public const byte RIGHT_EYEBROW_INSIDE = 21;
        public const byte RIGHT_EYE_OUTSIDE = 36;
        public const byte RIGHT_EYE_TOP_1 = 37;
        public const byte RIGHT_EYE_TOP_2 = 38;
        public const byte RIGHT_EYE_INSIDE = 39;
        public const byte RIGHT_EYE_BOTTOM_1 = 41;
        public const byte RIGHT_EYE_BOTTOM_2 = 40;
        public const byte RIGHT_PUPIL = 68;
        public const byte LEFT_EYEBROW_OUTSIDE = 26;
        public const byte LEFT_EYEBROW_1 = 25;
        public const byte LEFT_EYEBROW_2 = 24;
        public const byte LEFT_EYEBROW_3 = 23;
        public const byte LEFT_EYEBROW_INSIDE = 22;
        public const byte LEFT_EYE_OUTSIDE = 45;
        public const byte LEFT_EYE_TOP_1 = 44;
        public const byte LEFT_EYE_TOP_2 = 43;
        public const byte LEFT_EYE_INSIDE = 42;
        public const byte LEFT_EYE_BOTTOM_1 = 46;
        public const byte LEFT_EYE_BOTTOM_2 = 47;
        public const byte LEFT_PUPIL = 69;
        public const byte SELLION = 27;
        public const byte NOSE_1 = 28;
        public const byte NOSE_2 = 29;
        public const byte NOSE = 30;
        public const byte NOSTRIL_1 = 31;
        public const byte NOSTRIL_2 = 32;
        public const byte NOSTRIL_3 = 33;
        public const byte NOSTRIL_4 = 34;
        public const byte NOSTRIL_5 = 35;
        public const byte MOUTH_OUTER_RIGHT = 48;
        public const byte MOUTH_OUTER_TOP_1 = 49;
        public const byte MOUTH_OUTER_TOP_2 = 50;
        public const byte MOUTH_OUTER_TOP_3 = 51;
        public const byte MOUTH_OUTER_TOP_4 = 52;
        public const byte MOUTH_OUTER_TOP_5 = 53;
        public const byte MOUTH_OUTER_LEFT = 54;
        public const byte MOUTH_OUTER_BOTTOM_1 = 59;
        public const byte MOUTH_OUTER_BOTTOM_2 = 58;
        public const byte MOUTH_OUTER_BOTTOM_3 = 57;
        public const byte MOUTH_OUTER_BOTTOM_4 = 56;
        public const byte MOUTH_OUTER_BOTTOM_5 = 55;
        public const byte MOUTH_INNER_RIGHT = 60;
        public const byte MOUTH_INNER_TOP_1 = 61;
        public const byte MOUTH_INNER_TOP_2 = 62;
        public const byte MOUTH_INNER_TOP_3 = 63;
        public const byte MOUTH_INNER_LEFT = 64;
        public const byte MOUTH_INNER_BOTTOM_1 = 67;
        public const byte MOUTH_INNER_BOTTOM_2 = 66;
        public const byte MOUTH_INNER_BOTTOM_3 = 65;
        /// <summary> The ordering of landmarks must follow the indices defined above </summary>
        [DataMember (Name = "landmarks")] public NormalizedPointOfInterest2D[] Landmarks;
        /// <summary> Image height in pixels, that is, number of rows </summary>
        [DataMember (Name = "height")] public uint Height;
        /// <summary> Image width in pixels, that is, number of columns </summary>
        [DataMember (Name = "width")] public uint Width;
    
        public FacialLandmarks()
        {
            Landmarks = EmptyArray<NormalizedPointOfInterest2D>.Value;
        }
        
        public FacialLandmarks(in StdMsgs.Header Header, NormalizedPointOfInterest2D[] Landmarks, uint Height, uint Width)
        {
            this.Header = Header;
            this.Landmarks = Landmarks;
            this.Height = Height;
            this.Width = Width;
        }
        
        public FacialLandmarks(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<NormalizedPointOfInterest2D>.Value
                    : new NormalizedPointOfInterest2D[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new NormalizedPointOfInterest2D(ref b);
                }
                Landmarks = array;
            }
            b.Deserialize(out Height);
            b.Deserialize(out Width);
        }
        
        public FacialLandmarks(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<NormalizedPointOfInterest2D>.Value
                    : new NormalizedPointOfInterest2D[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new NormalizedPointOfInterest2D(ref b);
                }
                Landmarks = array;
            }
            b.Align4();
            b.Deserialize(out Height);
            b.Deserialize(out Width);
        }
        
        public FacialLandmarks RosDeserialize(ref ReadBuffer b) => new FacialLandmarks(ref b);
        
        public FacialLandmarks RosDeserialize(ref ReadBuffer2 b) => new FacialLandmarks(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Landmarks.Length);
            foreach (var t in Landmarks)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Height);
            b.Serialize(Width);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Landmarks.Length);
            foreach (var t in Landmarks)
            {
                t.RosSerialize(ref b);
            }
            b.Align4();
            b.Serialize(Height);
            b.Serialize(Width);
        }
        
        public void RosValidate()
        {
            if (Landmarks is null) BuiltIns.ThrowNullReference(nameof(Landmarks));
            for (int i = 0; i < Landmarks.Length; i++)
            {
                if (Landmarks[i] is null) BuiltIns.ThrowNullReference(nameof(Landmarks), i);
                Landmarks[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 12;
                size += Header.RosMessageLength;
                size += 12 * Landmarks.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Landmarks.Length
            size += 12 * Landmarks.Length;
            size += 4; // Height
            size += 4; // Width
            return size;
        }
    
        public const string MessageType = "hri_msgs/FacialLandmarks";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c779d9fd02c1af8ab6a1712921ab9da5";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VXXU/jOBR9z6+w1AdgRT/yWUDyw85QhkqlrUrRaoVQ5SZua21iZ2MH6Pz6vXaaUII7" +
                "2oddVInmHvvc6+Pr+qSDljsmUUalJFuKYsEVYVwiglImFRIbtCExIynaUKLKgkqUUEVjRRMkOIwClDod" +
                "dD64RIMLBExEISXybko3CtgKTgtNwjJgh3F3B5YzwESRME4UUJKCIvqeQ1wCL+OIiyIjUAA85eydpp9G" +
                "63QwRu0oKgiHop8HvUvk9l4uLtGmEJlBWjX0nHtKEqhlZ/45upRqXSnhSUaKvyTiJGN8qxGRpuINVpqy" +
                "NQIYzXLK50IaeV4pV0xwGLZTKpc3/f6WqV257sUi639/eOrOaRHTXJUk7X4XWV4qIO1OyLovgCUHlv46" +
                "Fet+RqSiRT8RcX/grUSpYGQvSzpa0G712N1oGZRTMq6u0GL84365Gv2+wINPkflidjeejFYudq1xD3vW" +
                "uI99azzAgTUe4tAaj3BkjQ/x0DkAD6PpcjbFV/XzZHRXrcSNjiNHKwltcQ+7gS3uY9e3xQPserZ4iF3X" +
                "Fo+wO7DFh/ja+bwJf46+LWZ/rGZPy8fx7Qi7QysM67iyArCQayvgY29gBcZTk8hzvxTSFOFHX6DlbA5F" +
                "+F+qM4CH/S/V1Xn8L+Wtvs2Wy9kD0AXuKczDweBzffOn+XiCo9bGt9TzIhvqYi+0xaGdA1scpPNt8Vo5" +
                "r11Dkz9o5znoFrTzHGQL2nnqHIHXBj5Ea6/xSLPhp8oOkjUd9ziaTMZweLx6F6ezR31EvKvjZ1Dl+ugZ" +
                "+81GwONyMZ7oPnBbIegArxWCHwW/FQqwH7RCIfbD5myDjPday9FiZTYdB1cW6CDp9QnIw+HgBOTj0D0B" +
                "BTj0TkDwa+VbIC0xDgML0mxVaKux2a3QtrgDCpUOT6NQbHQahXpbko6n00bSaGCBKkkj9wTk4cg7Afk4" +
                "8i2QEScKLEgjTjQ8jULC6DQKOWGB0+pi/0mTuYCRs82Ywy1IpfJun1+O7uKOucThzqcFXJ/aQXxgWQnG" +
                "ZGPuaDOK8YTFxphsGAfHQNbilVZa+h5c+Gy7U6j+61RWpA6DjzAGQ14CFVgXBl94ma0r11KAC6h53lii" +
                "dugLTxX+NU0s0jLj0nHwf/znPDz+uEFSJatMbmW/sjjgTB4ViEWKBGydIglRYNMEWB9YMS3AFb2CoZKK" +
                "ZDmIZVC1z6nswUTjBeGzpeCZSJruUak9mBKwhiwrOYvBgSHFwC4ez4eZTJvBnBSKxWVKiiPDBp6MZFSz" +
                "w0fSv0vKY4rGtzfaTUkag0GCgvbAEBeUSL3d41tUyw4TQOvnhZDui9NZvokuxOkWhG2qqCU/spBE3kCy" +
                "36pV9iAJqEQhXSLRuYmt4FFeIMgGtdBcxDvjKed7tROVtXwlBSPrlGriGKQA1jM96eziiJkbak64qOkr" +
                "xo8c/4aWN7x6Td0dbF6qZZDlFpSEgXkhXlkCQ9d7QxKnDFwoOPR1QYq9o2dVKZ3OnRYbBsEsszXakUsp" +
                "wOpqy/4GVtWRypwpsy0rlvxfbbkrWNWWvzjzdcs1rx16deCRmbbY+uhAT+k55hXiMAudQ7uy2HSn7jpe" +
                "nUQjn9am9V5B0jeyl/UbxU/dHODozW/ImqYChIDu1m8Q8AKhmzQ2kumKNiC53szqwJyvqXqjlKNBzzC4" +
                "vYtjZYHEtOF8NnY2qSC6d9+bb/vmW+w4/wAFzyXncw0AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<FacialLandmarks> CreateSerializer() => new Serializer();
        public Deserializer<FacialLandmarks> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<FacialLandmarks>
        {
            public override void RosSerialize(FacialLandmarks msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(FacialLandmarks msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(FacialLandmarks msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(FacialLandmarks msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(FacialLandmarks msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<FacialLandmarks>
        {
            public override void RosDeserialize(ref ReadBuffer b, out FacialLandmarks msg) => msg = new FacialLandmarks(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out FacialLandmarks msg) => msg = new FacialLandmarks(ref b);
        }
    }
}

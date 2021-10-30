/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Detection2DArray")]
    public sealed class Detection2DArray : IDeserializable<Detection2DArray>, IMessage
    {
        // A list of 2D detections, for a multi-object 2D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection2D[] Detections;
    
        /// <summary> Constructor for empty message. </summary>
        public Detection2DArray()
        {
            Detections = System.Array.Empty<Detection2D>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Detection2DArray(in StdMsgs.Header Header, Detection2D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Detection2DArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Detections = b.DeserializeArray<Detection2D>();
            for (int i = 0; i < Detections.Length; i++)
            {
                Detections[i] = new Detection2D(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Detection2DArray(ref b);
        }
        
        Detection2DArray IDeserializable<Detection2DArray>.RosDeserialize(ref Buffer b)
        {
            return new Detection2DArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Detections, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Detections is null) throw new System.NullReferenceException(nameof(Detections));
            for (int i = 0; i < Detections.Length; i++)
            {
                if (Detections[i] is null) throw new System.NullReferenceException($"{nameof(Detections)}[{i}]");
                Detections[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + BuiltIns.GetArraySize(Detections);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Detection2DArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "5f62729a3134356dd48cf97c678c6753";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZW2/cuBV+168g4AfbuzNKmhTGIkXRNnG364e2aZNeg8DgSJwZJhSpkJQ9yq/vdw5J" +
                "SWM72X1obAQZDUUenut3LnMi/iCMDlG4rXh2KVoVVRO1s2Elts4LKbrBRL12mw9Yn3c4X1fVT0q2yos9" +
                "f1TVyYJU3Ku8U7Wi9653QZpQY0eiV5YmcqLTu30UO2WVl1GBmAARHRLFWx33opN2FI20rW6xY8HqdKoV" +
                "W+86MB203RkltO2HWFeXZeezy3fvF+eq6rf/57/qz2/+9EKE2F53YReeJAVBljcRbEvfik5FCe4lK3cP" +
                "kZVfG3WjDA7JrocE/DaOvQo1Dr4lFegioTGjGAI2RSca13WD1Q2pIupOHZ3HSW2hh176qJvBSI/9zrfa" +
                "0vatl50i6vgX1KdB2UaJq8sX2GODaoaowdAICo1XklSJl6IatI3Pn9GB6uTtrVvjq9rB/NPlsJeMxKw6" +
                "9F4F4lOGF7jjuyRcDdpQjsItbRBnvHaNr+Gc7AUWVO+avTgD56/HuHeWvehGei03ZMwA4xsDqqd06PR8" +
                "QdkyaSutK+QTxfmOX0LWTnRJpvUeNjMkfRh2UCA2wmtvdIutm5GJNEYrG+GiGy/9WNGpdGV18iPpOHkw" +
                "WwSfMgTXaHZT8ucqRE/U2RrXuv1W3nijA5w9OeQiEiDlpdpqq8DYUeQL2A4xCv9Y+F/QnSYvguPx7sZA" +
                "Gr0l98ORldgMkfzFDC3oIbI1U9IWbt6lLRzRULS7JaHlHQr50ow5oVcNvRINYEJgSXdyp0AXPgcWEjg4" +
                "sVHCuIYVqpNVwSK5JG9/CKBe0aVkxo3caAMmVaj+ytD209g7UAg6/Au2eQ0/AlQkpgKdhMwbNwB7wPzG" +
                "HeAT3pfvdHMCyLp6mRdfugMdwdaK1ahYxxzbFCUzYtGlqtwEL61VjW870skEkqQGim03ELhmbFRJzPNa" +
                "/MVFHPk0aE8ISBo0hnACjh0UgDw4oSPgcySNqa6PY10FZYPzySuuWLvBDb6BI3Y74vgKGO4HtUoujAgP" +
                "tAexFaUG4uZ8EL1sPpIKFpauq41zBk5zXd4yvcuEXMQewQxQHbAzCgnZYBLOCr1ZoHq5E0mDnfBGmkEh" +
                "cIxh+Y3+SBjV6u0WFmbYZ5XgBq1MC5ghhxRKAlE0LIK4HaDJu8auk6ijG/gEC8sEVqQrGFmxyxuokCnO" +
                "QtH6W2ioLnFcXjxSKH/JbSkN22Kf/fQ6ud1kv4dCtC6eirSCpEAmy5k8U+OQhTkc3DcK2bZMAlpdEBES" +
                "YRLn9H11Cf8bYAQJHIGD7wek8TXySsvoyyQB3B08zbBPKA+02bsBNuyVJ7pCJos793HoU16b0ij9t4GX" +
                "1+KNUkf6+Sc/X4GzGt/Z7zrn2cGkNpPVyFhJ6hkVRpGcdAuspyySXO9uVZMjXrwcae8NEsEEc3F22CwK" +
                "EkXBKC8tIund0/Wv3tfV1jgZL34tQgPeCicXl2QfdUf7sy1zRPCeTH+TKqaWEZ3yLp8sVIJDLtrqA954" +
                "hXhhqRKa5nSY7iimmhBmp3ASigISQz4EWmbpCAqhLE6G046ObJp2MqWCjSeEVCp5YpwkwKcFgOX65Tcp" +
                "Hx65FGo+isYi3GZkqqhtKPlOMEGOT3FqpzsSONAmdjPKTEBIzdbbZq5Dugx1YkJnQsm9vMnqnAjMB7Nx" +
                "cF9Wzpj8jWKPYvCV49IiaRjh+I2g4OcuL7nbK6rFIAKl+aRvCxUjWEIvG5Uq6wEHPAEDZYaKiGXeT8Tf" +
                "3e26kx+grYlSMkr2hIvDBdx/EhkW8/qQ/dh5PW2HtaDpSNFNmUkmd1zLw5LHDEOAZND3yNmr5MbzWdic" +
                "Ksqzw0qMK/F5JbyLC9wR/xZE8d7yfx5e/i8vn5cofPf84v1CmMczHbdO9/V731wrKvtpuc3vE4ijUF0q" +
                "uxawIQV32VD9bUCt4S3Tnfc9loBgpbjjlIAyPOkiq8x4dCTuBI+H6Wmcnj4/Dvuz6h4KqSN93gktfPs0" +
                "653gDMH1dYnK0+1jVBBHpSq74N0iN9UMCX05dCi1cORQKk+An3AXjTyaHxu4HUeQkvA9ItyE1BtMOgOF" +
                "gVtKFhUP6z77h0gZM6SCC30CktFmzUQIfhvmvKZqDQypg0RKTC+5SWC7pBKYaaVGojC9lGpFxXAWCp0f" +
                "dcczVy+Zy73sTmnyYFXuWnawctx39aKWn5z3bBL1/G4gPpgui9ruwwD1VfyyXBP0Z3VE/yF6D3ci0Abk" +
                "0jmRQaXLemBpvakCwV3Xh+Ov4+NBYG5IydBkDnx5bRQKO84WxPt2gJM9T8mX7YAckaci6IsoPdFERNk2" +
                "hyL7UU41+eh9hFU3yo8oRagjtakogBGm9lP2vckNKloU+TFxglV4nKdOnorGD2UKBTfOajbSqlJhaD95" +
                "Ge+iaHB9RO9G0xzqZwRz5W8SdfC5rH3agctU3NOg0MFjnbuzW8V1CidHYygMXI92tszxUMvwECmJKP5x" +
                "lXwTN3h4U6/IudPtUJlAXW2VanNbw3S5XaIZj98CzFibvUGdNqusRqPRcuu0pMMFBRV0G8VqpSo/WwKi" +
                "RbSGoeh5alGJKEzGzj2eesVZziDjQ0MABY1uDBUtbjptE3Mg6JWmPR1VZzpPCOd6jSu6kGQeIj0DOeJp" +
                "KjXnqZihYs+3k3Bxim2VeoAOR5NdRnkrFCLNT6Uj10mlM6abSLmK0U8a6nDGLC69SzMiMtAEqq3L/Sbo" +
                "QTYYtmSYMkDLNduDSZ4qdx7FoJ/SW2fan0kuYC7KbzbxvDtNKJLcGxxAblSb0GueEOq8++zpSjw95zEZ" +
                "jXj6tVFb6le9TT1F3ndnpiPy34nIy/MwcmqMhGyQF7ISeUQ3kRMP/U20ymBuQYoCF9GcexTQafDp5ZcI" +
                "wVw7zUCTDtwnNDdNX6f0/WFqivMQLPWSPDTPjeVXZfp+PCbQulv7yw5+Pj6YUM5lkMsJ6asUrtKeSZ0p" +
                "SlK8zKuZ0itWA7XtXyI3RdzxOHXmg1t34Hb8EgVOoQpAoh2PdZE8U2tZlSE3YJsUOx9JhNPyqgy6V8IO" +
                "3SaZz7vbUE7f6hb83DvNyw8ebpwZOhtK2jdqB9fI1RChA/p1x+CcK6utBqgG3zxhwtfldaibvp8HWrcy" +
                "OUrIPz5QMSGB8yis8uSZs8xKfIBhccy7sAYk+/B7msSEOg0EsWmnaksDH0tjNfxH9VcntckDyjTPJbqF" +
                "EUBRvmLivKjij2WB+heuasR6LZq9tBY5rFMAM7tbpe6Pn6gQediQZEnkZDtP/8qvT+lyyshlKP1kiVB3" +
                "tbZPdv+BxnsbjaKiRf+XLRcWM/zp3e+mH0Oi6o8Y+pFKDfgCbGh3kUaPYjNGlVzjB/r1iQgtDqCMpUSb" +
                "+mZ+m0Smm8+Y/HfsW+dV9T9h4qeQqxsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

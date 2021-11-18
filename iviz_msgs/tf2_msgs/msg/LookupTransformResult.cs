/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = "tf2_msgs/LookupTransformResult")]
    public sealed class LookupTransformResult : IDeserializable<LookupTransformResult>, IResult<LookupTransformActionResult>
    {
        [DataMember (Name = "transform")] public GeometryMsgs.TransformStamped Transform;
        [DataMember (Name = "error")] public Tf2Msgs.TF2Error Error;
    
        /// <summary> Constructor for empty message. </summary>
        public LookupTransformResult()
        {
            Error = new Tf2Msgs.TF2Error();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LookupTransformResult(in GeometryMsgs.TransformStamped Transform, Tf2Msgs.TF2Error Error)
        {
            this.Transform = Transform;
            this.Error = Error;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal LookupTransformResult(ref Buffer b)
        {
            Transform = new GeometryMsgs.TransformStamped(ref b);
            Error = new Tf2Msgs.TF2Error(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LookupTransformResult(ref b);
        }
        
        LookupTransformResult IDeserializable<LookupTransformResult>.RosDeserialize(ref Buffer b)
        {
            return new LookupTransformResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Transform.RosSerialize(ref b);
            Error.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Error is null) throw new System.NullReferenceException(nameof(Error));
            Error.RosValidate();
        }
    
        public int RosMessageLength => 0 + Transform.RosMessageLength + Error.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3fe5db6a19ca9cfb675418c5ad875c36";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W32/bNhB+F5D/4dA8NCkSeUvaYjCaAkbjdMISO3OcYHsyGOksEZFIlaTien/9PlKy" +
                "7CbFtoc1hmGLx7vvfnx3Z+esK3ZmvahsbgdzI5RdalPdOFHVnJHbCCK3POl0Lk7GxmhD7D/3orP/+bUX" +
                "Xd18HlL+j4HtRfs0L6Ql/lobtpYtiW2wtDS6olRrk0klHOMsKqaCRcYmDoeFzADhNLmCn2umhSyzxVZx" +
                "462CK5Ez+UdtXbmmxqJK9+sAA60PggrDy7NXhXP1cDBYyQcZG21jbfKBW7766JYfBuIj1SJ9AFDsbW4Y" +
                "gM5SptOmYuWEk1oR8oAPgyvlUwrCOIp+DTl0qUTWGanyJ+HSfoimzQRHvWyT9EqtNOrLuUPwD2PSuqwl" +
                "sY3dUwcWVSZMhoI6kQknQrqFzAs2xyU/cgmrtgPDrVvXbOMNC3jnrNiIckMAeEx1VTVKpp5EJ0HUrj0s" +
                "pUKH1MI4mTalMM849+h4W/7SsEqZkvMhdJTltHESAa2BkBoW1hc8OaeokcqdnniDaH++0sc4cg5qeueo" +
                "unC006MZCTuEjzdtcjGwUR2Gl8zSQZAtcLSHBCcIgWudFnSAyK/XrkBPeBofhZHivgw9mKICQH3tjV4f" +
                "7iD7sIekhNIb+BZx6+O/wKoe1+d0XICz0mdvmxwFhGJt9KPMtgOQlhL9S6W8N8KsI2/Vuoz2L0I3Ok9f" +
                "YATfwlqdShCQ0Uq6YtPM/dS99Grpd4phzxcysSGr7Vq5Z7diRsFW+ln/oC0xtAazbDHcaKfojlOnzWlr" +
                "X4YBjn5vYGCUH3Cj20l/qTy7cL6XpaDHcPkkBT8PSehgrdD/FQuQi1HrLWGYSQNTv5uAylh9WFlHWGfY" +
                "ZiiJ0g4YlXgAJKOdvLWoa4CJ3bJ4MUwOOM7jI1oVKHHQ8u0QhjeMu0zJyBwLrSekNxbUZXdE+J1CO5Vl" +
                "G3PrDCwCZFPww5iSJa11QyufEB5Mt2U0GO7jCtPgtD7yK6aD+Lai1xozv/1NUNZhwYH4ZamFe/+WvvZP" +
                "6/7prxdie9to3yVckTZ+VtsKfkO7P33Ztqmv87/l1D+tflwzu6f/P/bCBv6FJtPFeDabzuiMfupEl9Pp" +
                "b7fXvfjnTvxpOpmMP82Tu2T+Z3950l2O/5jPRtfTy9E8mU7629PuNpncjS6T88Vo9vn2ajyZ9wpvO4V5" +
                "cjWe3m7l7zby2WhyczGdXfU376PuKvx/2my9cFi0BxTxb5v/wP6XCQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TwoIntsActionResult")]
    public sealed class TwoIntsActionResult : IDeserializable<TwoIntsActionResult>, IActionResult<TwoIntsResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TwoIntsResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsActionResult()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TwoIntsResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsActionResult(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TwoIntsResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwoIntsActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TwoIntsResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionResult(ref b);
        }
        
        TwoIntsActionResult IDeserializable<TwoIntsActionResult>.RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3ba7dea8b8cddcae4528ade4ef74b6e7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bNxC9L6D/QCCH2EXttEmapAZ0UCXVUeAkhq32anDJ0S5bLlcluZL17/uGq9VH" +
                "LCE6JBEsr2STbx7fvBnOe5KavCjTI5MqmtpZkz9UoQgvrmtp76OMTRAhPbLpsp64GO4oNDYKnx69rP+N" +
                "X73s4/31FWLqlsf7xK6XPRNg47T0WlQUpZZRilkN9qYoyV9YWpBlptWctEj/jas5hUveOS1NEPgpyJGX" +
                "1q5EE7Aq1kLVVdU4o2QkEU1FewC81TghxVz6aFRjpceG2mvjeP3My4oSPr8D/deQUyQmoyuscoFUEw1I" +
                "rYChPMlgXIF/YnFjXHz1kndgI1S9wHcqkIkNAxFLGZkxPc4hNJOV4YrD/NSe8RLwEIkQSAdxlv72gK/h" +
                "XCAOWNC8VqU4A/3bVSxrB0QSC+mNzC0xsoIOgH3Om56f70Iz9SvhpKs7/BZyG+QUXLcF5mNdlEieZQlC" +
                "U0BHrJz7emE01uarhKKsIRcFHOilX/Uy3tYGBcifLDaWYV/KDZ4yhFoZZEKLpYllLwvRc4CUlweje9l3" +
                "c+fRWull/BlZLvBIHDjZ79Yl1H27HX8aTT5di+7VF7/gN/uU0kZRyiBWFNmhObFQqjXBWqk2PNLvF1wa" +
                "LehgOJ38PRY7oL/ug3JyGu+hMTyZE0t1GvLt3Xj88XY6Hm2QX+4je1IEq8OkSD+swn9BNYQo5CzC1yay" +
                "AJ4zRY+pLlzRy7ZUn76e4Q3DJCFa96FS55YYwsTQwYDq2ZR8hYK03B8inXek7/8aDsfj0Q7pV/ukl4CW" +
                "qjRoHBqmVCzErOHmcEiLo3EGf3y+20rDcV4fiJPX6fS6SQ7dsj8YSjf0dXXYG6FGTcyksY2nowTvxh/G" +
                "wx2GffHbU4Ke/iHFDA8S4vKqm/ilaX4+gWVOSqLZJtBNtAb9M0pw5Z6BHm7cQlqjjx5hbcBNyfTFmx9h" +
                "wI0DXR1TOW49uMngVuXh4OZmW9R98fZUijnhHqODHE9SGIl5mrJ92m5mfMU3Hl8rm1Skbs1USO8fY9cs" +
                "777BMU6Umq2xV4htBL5Ojjnj5vP9dBerL35PiAPX6bG+VQAlNFLHKNTqIDcqMMplOyUEGN3qJF1+ShUG" +
                "Bq9ZcZZ1aaAASgjBvuikuMIG1tbLNLPwUhQFPtTbWwx81hcYl5vYmb94i6a8KYqk5XpVpEeMXz/2kpuM" +
                "2nFqfS93aoXImedTpTsb2i5Lg/EjXdc7PSYZhXSamSZpvklz2AHBAECOvYSzUmCdMAdRNUfWrOXtjBra" +
                "PC4JwTfgnQ/hT/LcZBKn/WmiOwRaznoICVi/lOh9uwmZEelcqn/ZnLylHXkxfoYgC0420hTmpMzMqK46" +
                "EovAZmL4NBi2K8CsalKZoP0ZLIMK60y2o8r3z+OLvQm+l6GC3rxGxVW97H+eRda2CwwAAA==";
                
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract (Name = "vision_msgs/Classification3D")]
    public sealed class Classification3D : IDeserializable<Classification3D>, IMessage
    {
        // Defines a 3D classification result.
        //
        // This result does not contain any position information. It is designed for
        //   classifiers, which simply provide probabilities given a source image.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Class probabilities
        [DataMember (Name = "results")] public ObjectHypothesis[] Results { get; set; }
        // The 3D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all detectors, so it may be empty.
        [DataMember (Name = "source_cloud")] public SensorMsgs.PointCloud2 SourceCloud { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Classification3D()
        {
            Header = new StdMsgs.Header();
            Results = System.Array.Empty<ObjectHypothesis>();
            SourceCloud = new SensorMsgs.PointCloud2();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Classification3D(StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.PointCloud2 SourceCloud)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceCloud = SourceCloud;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Classification3D(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Results = b.DeserializeArray<ObjectHypothesis>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesis(ref b);
            }
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Classification3D(ref b);
        }
        
        Classification3D IDeserializable<Classification3D>.RosDeserialize(ref Buffer b)
        {
            return new Classification3D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results, 0);
            SourceCloud.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Results is null) throw new System.NullReferenceException(nameof(Results));
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) throw new System.NullReferenceException($"{nameof(Results)}[{i}]");
                Results[i].RosValidate();
            }
            if (SourceCloud is null) throw new System.NullReferenceException(nameof(SourceCloud));
            SourceCloud.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var i in Results)
                {
                    size += i.RosMessageLength;
                }
                size += SourceCloud.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Classification3D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "fcf55de4cff8870324fd6e7873a6f904";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VXTW/cNhC961cMuofYwa5a22lqBDCKNEYaA6kTIG4vQWBQInfFhiJVklpX/fV9Q4ry" +
                "OvGhh8ZwvCuK8zgfb94wK7pUW21VIEFnl9QaEYLe6lZE7Sx5FUYT62pVreim02FeIOlgYF2k1tkotCVh" +
                "Jxpc0MlK263zfUKo6SoS7KQKemeVJLwBFi0HKR/WdNfptqOg+8EAxru9loo/G9FoA0wcttN7hWMouNG3" +
                "inQvdqquqjdKSOWpSx8VkF8x7kPb6l3zp2rjm2lwsYMf4eOnOY5QpbgURy5FFBQ7EWmnrPIiwlnerspe" +
                "OtK1qvG04xhxAuIVhlp8GbDXjZHcNsUGs+zgcU3XSJJXf43a5+BJGINsRDjkOPTgSEfqxUSNItUPcaqr" +
                "oGxw/rYPu/D9e6dtfGXcKE/n2G9bfqou/uef6rcPv76gEGU+NycW4XyIwkrhJfUqipQkjqLTu075jVF7" +
                "ZWAkek5BTuE0qFAXvuA3p9OgsmPgnDqQpu9HyxxTFHWvHtjDkvlEg/BRt6MRHvudl9ry9q0XvWJ0/Aak" +
                "VVmQ4eryBRMxqHaMoAlO0rb1SgRtd3hJ1Ygcnp2yQbW6uXMbPKodaLMcngsPZ9XfA8rNforwAmc8zcHV" +
                "wEZyFE6RIEJau8VjOAZr2QU1ODD4CJ6/n2IHfjAH9sJr0RjFwC0yANQnbPTk+ADZJmgrrCvwGfH+jP8C" +
                "axdcjmnToWaGow/jDgnUoXSVpGZKIK3RykYyuvHCTxVb5SOr1WvOMTZx23JF8Imecq1OPXGnY1eF6Bk9" +
                "VeNWy+obsXGvA3otE/LLJkaoLy25tErdspwrOcsSa9TjslQaHzQEiZgkbpvyMgMmearpxoG+kYSUCQLt" +
                "fgBCokHTzx2Pk68u0c4jaCDAe+hFN/bCbsBDmaqVIFHoXq2R9hC5LVDszo1G0qA845JIcMa5z+OQ+2Bp" +
                "O/7TiAAJ+qDUg9T8kb5fwbMaz6k/e+cVy4zQpi7V4jrlqO/lcSLH/WW34Aazbi8M0jHnIssUa1tKSk2/" +
                "TLwXQszhr+8jz1ZzKCAWe84AXtidoo8/bE4+1dXWOBGfP6PQwrdvJV+PK2dRI3R7gCxT54zkgdc6dE+b" +
                "aomQrzcSXWBDLvTA5stsgkADZJl2j/OhFN/yioEt6wzw4rQmFduacw+UhLy0VsAcSHIDfxqInMckMK5Z" +
                "JwoZMfFcwfRsvW6W7p1dQR1CqdV3GKUI6jsS3otp4Xc+K02MfOI8aZzfCav/AeKphL7wsNoY/VkdMx9O" +
                "JKyPRoSBEaAkptj7e5hwYAunYZ6sQ0EGt+TYJlfZTdDdg7tqiN1cnbDkCT3gleMTk2i57WZrMFdi9p4V" +
                "CcFlo+y8aDFIczevceWQWcm+mA7czEfcA2eyJDsc11/fFE4v4YAf2zj6hfEH6cLNZTsrJScPlVoSsgYK" +
                "+0lJhU6SJ3daIsKkP4qMsjs8PQJaRlEGKE/JmH26nAudYVrIuFUmlFC1L4SY+2vmS8oNk6auUqFeMxVw" +
                "ycmUqKrGOYNWhXe3jcY4llpYWtFVOBD55cXPxans9y2KNMB2RW+XoMQcErxopogLVrHw7q7s/9ICrx7u" +
                "P4eD6eTyAz1HLUrnpXfQU/CMs3pUoJ/mzB3PUSEkCBcuaQvIjYcU6ZR51FXgH2aAtpAoLWc2fKtx9ZX6" +
                "pEI8Lj5ZX7naQ9EfZwtX0Nh+mosM88M7YEHJqpP5dE5X1zfnHP4Fncwrv89LF3R6v+fkeVo5O9jDSxf0" +
                "7H4P1xErPx7s4aULej6vvH777iUvXdBPhytQ9gs6r8qw4TlXSnItcisnPhayuO02YLSmDe/y9613PV9C" +
                "PN+k51TkFp0PSqTgCyYbXZbvyo4sMlkVAmYj5vJelXNaN9o4O/IGJOz5vyrKqD6p59xI2bPqX7yWssYN" +
                "DQAA";
                
    }
}

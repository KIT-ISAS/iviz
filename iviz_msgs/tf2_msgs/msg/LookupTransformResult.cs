/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = "tf2_msgs/LookupTransformResult")]
    public sealed class LookupTransformResult : IDeserializable<LookupTransformResult>, IResult<LookupTransformActionResult>
    {
        [DataMember (Name = "transform")] public GeometryMsgs.TransformStamped Transform { get; set; }
        [DataMember (Name = "error")] public Tf2Msgs.TF2Error Error { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public LookupTransformResult()
        {
            Transform = new GeometryMsgs.TransformStamped();
            Error = new Tf2Msgs.TF2Error();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LookupTransformResult(GeometryMsgs.TransformStamped Transform, Tf2Msgs.TF2Error Error)
        {
            this.Transform = Transform;
            this.Error = Error;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public LookupTransformResult(ref Buffer b)
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
            if (Transform is null) throw new System.NullReferenceException(nameof(Transform));
            Transform.RosValidate();
            if (Error is null) throw new System.NullReferenceException(nameof(Error));
            Error.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Transform.RosMessageLength;
                size += Error.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3fe5db6a19ca9cfb675418c5ad875c36";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WbW/bOAz+bsD/gdg+rB1a5657wSFYBwRrujOuTXppWtx9ClSbsYXakifRzXK//igp" +
                "trN2OOzDrUFeJJF8RPIh6RSoaySzXdW2sKOlEcqutamvSdQN5kDdQUTrk53O+cnUGG0A3Xccnf7Przi6" +
                "vP48huI/HYujl7AspQX82hi0Fi2IwVlYG11DprXJpRKEvBc1QokiR5P4zUp6DNJAJT5VzUpZ5as9zf7C" +
                "mm8TBYJbakvVFlrLibrbeiCn9kFAaXB9+qIkasaj0Ubey8Rom2hTjGj94iOtP4zER2hEds9IiTe6RoYk" +
                "C7nO2hoVCZJaAQfDtxgWKReXP0ziKI5+96HsIoojS0aq4pHX8NK7FALirV6HWJ1SOI2jPq9D8hz8T+LU" +
                "Uh7oDO77uEmoXJic80oiFyR8zKUsSjTHFT5gxVahFr2Utg3apGeD3wUqNKLqiGBGM13XrZKZo5MkE7YP" +
                "4Eyl4mpphCGZtZUwT+j3+O5j8UuLKkNIz8aspSxmLUl2assYmUFhXdrTM1ZupaI3J86CDZcbfcx7LJij" +
                "3gPOviDYq9kchB27a16HGBOG5yQhX5RbOPBnK97aQ+B72AtsdFbCAbt/taWS68Px+SCMFHeVr8iM88Cw" +
                "r5zRq8N9aOf6GJRQusMPkMMlP4KrBmAX1nHJ5FUuBbYtOI+s2Rj9IPOhIbJKcjVDJe+MMNs4cmbhUgY5" +
                "96VJjkjPDf8Ka3UmmYkcNpLKvrSHVnzuidPXmkFHG0djfWTDtLlD2iBy1jb6SSlxiXIbG+5uy/3uKiuO" +
                "bjEjbd4EhMo3dRz92bKNUa7rjQ7t/2yx7hz6XqQCHrzwURi+O1JfzVpxN9QomGXuvd6ULXNp2NaNLIZF" +
                "Hok8yY54yvGQ47woTQ6kFvcMilxZzlw0DaOJ/dS4Y7Y5wKRIjmBTcqK9lquK0M5+AsgMjCx4zPW89NYC" +
                "dgEeAT/FuK6qKngdbmMyHUqX9cME0jVsdQsbFxMvzG70aGa698y3Bml95MZOh/FtWq80z4DhgaEs8djz" +
                "FbCutKD3b+HrsOTe6Jb/PBPtQ8l9l3kF2rjmDXn8hn+3+zIUrMv2j8XVLTc/sbbp8T+VMJx/g9l8NV0s" +
                "5gs4hV+6s4v5/I+bq/781+7803w2m35aprfp8u9eetJJp38tF5Or+cVkmc5nvZgbKIjT2e3kIj1bTRaf" +
                "by6ns2Wv8bbTWKaX0/nNIHjXCxaT2fX5fHHZi967VAXh7h/XbiT63SpsnM6/Mg5FRcsJAAA=";
                
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = "tf2_msgs/LookupTransformActionResult")]
    public sealed class LookupTransformActionResult : IDeserializable<LookupTransformActionResult>, IActionResult<LookupTransformResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public LookupTransformResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public LookupTransformActionResult()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Result = new LookupTransformResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LookupTransformActionResult(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, LookupTransformResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public LookupTransformActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new LookupTransformResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LookupTransformActionResult(ref b);
        }
        
        LookupTransformActionResult IDeserializable<LookupTransformActionResult>.RosDeserialize(ref Buffer b)
        {
            return new LookupTransformActionResult(ref b);
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
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ac26ce75a41384fa8bb4dc10f491ab90";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XXVMbNxR93xn/B03ykNAhpiUfTZmQGRcc4hZsagzTPjHy7rWtsrtyJC3G/fU9V9oP" +
                "G0zrh4AH7N2VdHV07rkf+5VkQkbM/E8kY6d0nqrxdWandu9Ey/TCSVdYYf1PdKr1TTEfGZnbiTbZkGyR" +
                "OmH8Tys6/M6fVnR2cXKAvZOA56tH2YpeCqDKE2kSkZGTiXRSAI6YqemMzJuUbillxNmcEuFH3XJOts0r" +
                "RzNlBf6mlJORaboUhcUsp0Wss6zIVSwdCacyWjPAS1UupJhL41RcpNJggTaJynn+xMiMvH3+t/StoDwm" +
                "0Ts+wKzcUlw4BVBL2IgNSavyKQYxuVC5e7vPK7BwtNBvcE9TeKRGINxMOkZMd3MQzWClPeBtfghnbMM8" +
                "SCJslFjx2j+7xq3dEdgHKGiu45l4DfjnSzfTOSySuJVGyXFKbDkGDzD7ihe92lk1zdAPRC5zXdkPJptN" +
                "trGbN4b5WG9mcF7KFNhiCh4xc270rUowd7z0VuJUUe4ElGikWbYiXhY2hZEvTDamYZ33DX6ltTpW8EQi" +
                "FsrNWpF1hjfwfrlWSSt6MnU+GjOtiK/h5Sl+PAZ29scylKq7827/uNc/EdXnUPyIb9Yp+YViJq1YkmOF" +
                "jomJioMISqbC9nC/ueXQCEY7R6PeVVesGP1p3Sg7pzAGHEOTY2KqtrN8Pux2z85H3ePa8v66ZUMxQeoQ" +
                "KdwPqfATRIN1Qk4cdK0cE2DYU3Tn4yKftqIG6sPPS/xDMJ6IoD5E6jwlNqGcrcwA6usRmQwBmXJ+cLRT" +
                "gb64PDrqdo9XQL9dB72AaRnPFBJHAlHGTMSk4OSwiYtH9+n8Ohg21PA+7zbsM9b+9EnhFdqg37hVUtD/" +
                "s8PasBoxMZEqLQw9CnDY/a17tILwULx/CNDQ3xQzwo2AOLx04e6LZncLlGOKJZKtN1rvViB/OgmsnDOQ" +
                "w1V+K1OVPHqEUoB1yByKD88hwFqBuXY+HBsN1h5sWD7qnJ42QX0oft4W4phQx2gjxq0YhmMeumwddj5R" +
                "JuOKx2WldoXP1gyFkvVjrIrl43c4xpZUszTWAjHswOXkMWWcDi5Gq7YOxS/eYiev+CirCkyJBK5jKxR4" +
                "kDULbKUdugQLoaeJp268TRRaNq6ZcaZ1ocAAQgib3cukKGGdNNUL37PwVAQFLnRTxYCnLGAcbmKlD+Ml" +
                "CY2L6dRzWc5ydIf263mLXO84tFNlXa7Yso49z6fyNRvcLmYK7Ycv1ys5xguFEt8z9Xx/4/uwDYTBAOWs" +
                "JZyVLPOEPoiyObyWprycrdrgxwVh89p4pUPokwwnGY9pvZuoDoGUUzYhSNHAuFx3yIQoGcv4hsXJS0LL" +
                "i/bTWjml4CY7p1hNVFxFh0dh26V53xiGGUCWFT5MkP4UprVrT4ZW5cn86Cb7wYMbG/lWNCWUEWeWYVI9" +
                "fFG2wq56ENWGRl/2u8ZAlcTfTwb8v4HVjX3VI7MPa7DoAnX2oFsv33naTYuI7lSH7vP+VLQGaXK9MrPe" +
                "sBIAX2rL3VQlYzbE0z5JMTM0OXwxc25+sLe3UDeqbbRtazPdc5MXn93k0578jBeL+AaW2n7RBXH2RH+r" +
                "4yJDWpE+Hjj4M59Ycz6XfwjptKLwYlSeqNbSOmrkKIYUDoRbPQln5UnhaSuqeW3Ie0IxPuLTmltD7Eyc" +
                "3oZ8UoMbk1sQcoJb6Ae+wssdiEKCQLDJmCOrFV2hn9HmbbCQetpa0R8F1piceTU6EPxsZy0BbTqpFLd+" +
                "8N4xRJMnkYeX0J1EsYFe66VYmaDk+Mzpcw5EB63scuZMNHhBHWMjmbyBUcI7mc9w8zmsyVVq+DHWvKb2" +
                "tL0bkq+fxZIKL8L+3Rl5zqgphFT7pV4tRXnAXU44SI5pGlCH3eBMn0JL1nfaojcRS10g6+IYuDDlS7t/" +
                "46mQ+QbRab3LEVbZWKf1XKMRaEIyRw2QXGFa0STV0n14J+6aS7xVVpf/PJPbG8lt9HyO/pcblMDjmv/5" +
                "7lsjWGZ7u3NVl4vnKCqjshZUHVl/cN0dDgdDfqlturTB75fn9fOfqudHg34f7ya9q97or3p0vxrt/jka" +
                "ds4Hp51Rb9CvhxFAYbjXv+qc9o6vO8OTy7Nuf1TPeFfNGPXOuoPLZuB9PTDs9C++DIZn9dAHpioMljWt" +
                "zKf+7jrc8Jx/AYLB7K48EwAA";
                
    }
}

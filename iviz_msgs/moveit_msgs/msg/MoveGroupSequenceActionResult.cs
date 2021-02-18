/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupSequenceActionResult")]
    public sealed class MoveGroupSequenceActionResult : IDeserializable<MoveGroupSequenceActionResult>, IActionResult<MoveGroupSequenceResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public MoveGroupSequenceResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupSequenceActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new MoveGroupSequenceResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupSequenceActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, MoveGroupSequenceResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupSequenceActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new MoveGroupSequenceResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceActionResult(ref b);
        }
        
        MoveGroupSequenceActionResult IDeserializable<MoveGroupSequenceActionResult>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e995e5b2d6c322a0395df341caa51d8e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+07a2/bRrbfBfg/DNbA2t7KSmInaeuFPii2nKi1JVdSsk2DgBiRI4lriqNySMvqYv/7" +
                "nsfMkJTkJItde3GB66a2RZ45c96vGb9TMlKZmNOPhgzzWKdJPAkWZmaevdUyGeUyL4ww9KNxre/U20wX" +
                "y5H6vVBpqIbKFEkuMvrRaP+XvxrXo7dnsHfE9LxjKvcFEJVGMovEQuUykrkUUw1MxLO5yo4TdacSJHix" +
                "VJGgt/l6qUwLFo7nsRHwb6ZSlckkWYvCAFCuRagXiyKNQ5krkccLVVsPK+NUSLGUWR6HRSIzgNdZFKcI" +
                "Ps3kQiF2+GesXETv4gxgUqPCIo+BoDVgCDMlTZzO4KVoFHGan57ggsb+eKWP4aOagSr85iKfyxyJVfdL" +
                "kC/SKc0Z7PEXZq4FuEE4CnaJjDikZwF8NEcCNgES1FKHc3EIlN+s87lOAaESdzKL5SRRiDgECQDWA1x0" +
                "cFTBnBLqVKbaoWeM5R7fgjb1eJGn4znoLEHuTTEDAQLgMtN3cQSgkzUhCZNYpbkA+8tktm7gKt6ysX+J" +
                "MgYgWEUagZ/SGB3GoIBIrOJ83jB5hthJG0EcNR7JGh/0kQb+CpqdwQ/cHxX8g3Mc/nDT7V/0+m+F+2qL" +
                "5/AdzVLRMjGXRqxVjgY5USifkBVvBcR7g86zO/ADxtk5H/c+dEUF54s6TtRIkWUgWTDCiUIZfRPim2G3" +
                "e30z7l54xCd1xJkKFZg2mCWoHMwDn4D1m1zIaQ6WHOfIfYYKUvfkB+msIb7wtQ//g5GQFNjgwCuXiUIM" +
                "cW4cFiD0cKyyBXhfgqEgV0eW5NH78/Nu96JC8mmd5BVgluE8Vki2KUKUwrTAOLBLEA9t03kzGJZywW1e" +
                "7thmoon1qCCzLGnfuVNUqK+KBq3CaHCDqYyTIlMPkTfs/tQ9r9DXFq+2ycvU31WYP2AB5FC6yDfNpfl1" +
                "GicqlBBTCaffrIA4mUugFCMEROo4vZNJHD3EgLU87ylt8foJLM+bXqpzcsLS+LzyvITPO1dXpSe3xfff" +
                "SuBEQapSOyn8FumCTra1VSc6ncbZApMapo+8GgWIEhXVmKiayQ//BSa+TcxoFDX34w0wbTxgE1eD0biK" +
                "qi1+JISd1AnDZg/AJCLQGiJRLATpRYBYWlwFGDDwJCK5Tb7B9wzi1ihtFOkqBvbBc2S6ETob+50k0Suq" +
                "RxAQXCFDv/XJCoixiQp9TFRKLFwSqUkxm6EYLVCu7vPGE6ay3kWDLYBLECskk6O6kR/KySDS1TyG2oLy" +
                "cSWkkHWoCGuhHpUuhc0xm3KC9SpF+wEulUEBQYmjFkvQVZLAasRpWHkrBVt71M70wCRVhiGFKKqWCpZ+" +
                "iC62vIBQDOSt61qYKhVNZHiL1ggruH6FctIYOVOsGrNUYTyNQ+cMRIFpWexY6zEAELUoyCkgzsUA1XLK" +
                "wyLkkVS3AFOMc9bbA0U57H2tUeaVx0usSJFb+uVpiNtFQoMcV2WZxko6QpKmCeQj8gvWOrjbKtPpbI+a" +
                "jl7eReBzjfqkdQGu22vsWX1jGOFoRCUmhg6hp2SzmZ6Ak9tgSCDujavW9xpDhKGA4x8GBFrukGcSU6Zm" +
                "i3XBdZnIFHoJNLaoCMEW0I19vrCIx27p+tNnuyIKqvjKXeRCFykTiDaNLqL1re1QKB3RtgsSK+Haa0wT" +
                "LfPXLxkzsB/g0r2nMr2qahrc1EB2Z44wYIJHg2/M5V2ssz0LQIXaaNR+4R5cdnpX74fd9o/4hWv58c1V" +
                "p9+HPBvg++5F+9gv6PU/dK56F8H1YNwb9AMEbB+fuLeVp4GF7EBFFLz5GHT7H3rDQf+62x8H5+86/bfd" +
                "9vGpW3c+6I+Hgyu/3Uv34n2/8+aqG4wHQeeX971hNxh1+6PBMAC0nfbxKwc27l3DLoP34/bxa8+Dq6Pb" +
                "x9+zVJyexJ/FLSSphYSm0odPtm7jJTXuDMcBfB93gZPgfAB1xwh4A1E83wXzoTe4gp+j4KYzfgfg/dF4" +
                "2On1xyNY8KIU7NtB52oT30nt5ZcQndYgK+/cKtTUy3I3p6y3w8H7m6DfuQaZv3i19XYDGcC83oQZDt4M" +
                "LKvw+vvN11CT/ezw/7D5cvAG62L3+kfWhVlDWllsCP1yCDABkNEfXQ6G14GzzuOTF6WlWMGBEXXPf0Yb" +
                "BRv5AIBoKADppVkhGb/TSy9Aa0a9/uXAv3zJlFVMo05dfxD0fg5Gg6v3Y1Lc6Ysn8fUyRLpBikuXUHNi" +
                "KjZQ10P0Q5ohNEH/Y4tVjr8UkpsibqkWB05tYgQ0GOuwu/u7BvYMlYdJnN4Cu1BzGQj0tPtP+JYDNAEG" +
                "hI8l9ROvpJi8gEwPsQYiJKTAGAKmuBhcCgklQ5mu51BS1LFfIzAAVnah9UGkp8HWfh1oZsI5IAp1ksQG" +
                "udUTjOVGHEr3zjVryIuwUxKSxNFewyE4d+sHtBySg1seeNSBRc1bXyYS6oo0wkEVJ0sFiDOuxEyoUprA" +
                "8PgAJ0oZ1FU5TY6gCori6ZRrG6wAAKknUhMSWr9XGZMttMGhQbxYQi8rIS3RmM3NcagRcexOdIR58dAR" +
                "BIBYWmPeSpTMdgFTgpgCXWxlZ2chVHZnZ5VUbIu9Yhkxt5ANify8Yn0gzonWWN0HyN+jucJuY6wIS3p/" +
                "IEOc6ySywyoQAuTGMIsnytUgXJ1AL6A41+sMgj85UqapDWJvaInGflmQ8yIFMuTX4jBTdzopcmqql1ls" +
                "KF4cITWRmkL8wOIbR4biLzWfc/WPwyIjRLA4apagdyqBUjlfb4M+MwT8zByRr5ZL1BS0lfuxpVwuE/S2" +
                "OK0i6C9wdf+oRYx1S16wW6DyGQ0ugvKPfRWaBwwOqVwoKwieVXt3Njv7ErcfSc24JgIMLIL2RECPtgXD" +
                "M14NDuTUSGGEXckt9VUkTSNjGvvuiIYmhk7B2rYLQzLL5No0aQeqEZH1ZaUYrRCDaq8N8ICKhbxVvMjC" +
                "A+9oYXqJGpVJS/wNeyrVmrXEWheZi6fERaoBodXPxtwUcS2auIRaZ3DWO1VVJ9EtsDdbW2tE6TE3rNsK" +
                "77ZfnDscJCcT/wGBH1gGQTKeitcgFPSga9eieRsoe7VSOGgFnmgQdAZ6yTGJkwZb0PG8q51n2EYM4irZ" +
                "j62TsQa3jlB55Ay+8ogl8BQBZTv/AFdDhXN/8ATpfJZjAFqPVS7JwptYpGbQRZPhTeGXSINeAc9U40jC" +
                "RWwQSRHmOD4EsHI/NmTu2kH0hZ8gSeefeABiqyWSPHZyEZmmxN4oNZj7ec1M5aX/A1qZaLu7L2pEOIeS" +
                "oSUuqV2S2No08YQFSmOZuWAhycLeDy8uKaadYio/vAdjhX9yhScLGIHyuYamll6iBaOVVU5MqtSxIOFH" +
                "FgMWXoseXXsPWBnCYQOHhjaGhg84MQCGazT8fxh72jC2yqA7n39zGHPg/5fC2ENRjCtRXG4aMwWFW56t" +
                "OYCMnQkDlDfnLaAVKBQB8OfGu7+RmOAly+uxgt4DVDtJZi7kWXfwYWWi8pUCu8hXeuvolfSHAQ+cSYZg" +
                "y40PNFc55fUJe/UvBSzIUgwAmeaQ+jRMWmJ2sCgh6eC7DfqFD8RkUQuFlTfYlF9JpTzaDPBA88iM6uQm" +
                "1seRVnQYQFHsVlGWIffHcLx2wZBlgo9hySE6W5NHsgSFqYIOvemYHGJ1Fs/iaDOMUuC3zDVFPj0BkwaX" +
                "Ipp5M1AhzlattI9aojclB10hQ+TcrjyeKE8XnRHlWjeFPUYiOqoCvSEncr4KfWcOfgJad2Owe/+bT+bi" +
                "jydRdWlju7QNYTmLfTqv6Rw//V4aKAr5qwy531ZP5KsUNCxbLsGask+o8zPJ9C1O+FMyMYNdMHaBmHJl" +
                "OqMbFJg0INg5X7Ug5WcL9zTccfjboTVQBaunZK4JTgXEU+pBBjHlfhuLhKz8yF3fU8xuHhg42PS88ZT9" +
                "eFLp1ylXSUjV964zRaellInjjT134IEfKD3RsIJE6WdD9tiygErAzOVS8aQHCnFl/GnZBiGAg2NDdaiC" +
                "cLzpPoQTe6pEOkOGCWmq7TymBVEL5yIqb+LBkvCjlP1dGN1chcs1z4sXB+8Q7TU25aUttW6Gb5t6mmKV" +
                "JwV+SzcrwqGUxNLckqGLcI4oYHPo3iUeSB174pgULM6TDMLdmuszKA+YWrtgr6wVCF1gJ2mIFqTlzsyY" +
                "MqomoeAOuUmvXW8CvVCPwJrBVMSsUhpaIA7PDuK2KQRCnAL5Q9/EeS1MNN13kpkuyC0smqOmG0fQJqnC" +
                "g2CZrWm7TCV8TQoRU13FW6Mi8RzSze/8+cm6MpQpT1rwEBgIDOwmpXZWKp7Nfbm6oRXIoVNxm+pV5ViF" +
                "FzzJjHXbPzu2CmzyWeqUqgU713MtHTnRXr1qLPkFH7C8WkEekikROtAjH+Ec7ZWHluVSp/T1UrGFYL6e" +
                "SEO9I0mpdCn+JcAWY5ZSc80sMSdjRIF4SuTl8NSGYmyCdlT1zpftujizQQPdZ3OYScV6Zc5bSmKk8cKJ" +
                "2ynEadkixpt5OGfGeMTUEtiNe4eTggrcZhliagCBVQPudq3MfAMzPgLwhX2zExe+rKJ5g25DN3CgLcbp" +
                "qsIiwQY8z2fTXwshMNe2crEBYijYB0F/URRzl0UiPKrRd4Nr7fHkw8ziyyqFnSgyVduyOvAnXDRSpsKv" +
                "AgS2exfrwkCJqO5jPNan0pUTL8UjUPZkDfV95+Ki/Zx3GlL0rW02zfSCR1XpXZzpdIG1MbbcGXZehwq6" +
                "9jUEL/ISOhrIwdPNhpHE0ZHdbNi9Hnzotl9YzpZLjGVY5aaeOxqI2ABMpBs33vwyx64q50WOW9BHhdUb" +
                "vB3YPvHButx29460UROC58o6hNU7dQiHdPHHqtC1ue6aQqKmObewR7gXRDyjExQZSNjFlDLoRsqAQCNL" +
                "JonolIkcLFXmewG8r6gyrFw9rHbvHy12fj3oNPb/7S/BB4V4S/ffX2y/WEDnXz4Yo7hK45Up5Ucb6CDK" +
                "4UALW12jeACD5eYCLzXhDGXGJyx+xMCjfoE3Afa3apFb5Uf41U3O+HYSoSjnVJmzrRnEs1REE58VAE2J" +
                "M5pUCbJJmSZtP40G/Wd4UcGO3z52rq/sfacW3vhwVhWVHlHpUt2tQz8VoRGjrQRc6mmJLtUacbpD++RZ" +
                "NP/BSxNJfKvOxJ/+cYCCPjg7OMeS6OLNQVMcZFrn8GSe58uzZ8+ghZEJCD0/+OefLJMZFVup5ulfasMm" +
                "a9FWRaikihyw8ozzA1gUh9Rx3yplL3FPE3DdSZxAn9Sq59aa6YZ0swnl6BrvizdsJIQF+cJAYLfmwRmZ" +
                "GV/vdXNUc0YnhUCjZZg+C8J0JrwU+CEKAh5uCuLs1Y8/vLQgmKh52ACA22QfuN1Gv1wJ0J9RePTl9VXf" +
                "fPR78s6BWPS0nThYzczpa/sIjxvPxKuXpyf8GW/RIUiM1bKDgVJhpbNo8zkWN8iQ28Wdn9rXCx0VCQLQ" +
                "oCHXywNv42jujzXrf6jCAJou2H0n+h4ayyVaXlOEayjRqeoLcdBqp5Wub8qUP94DM3NTSiiMJq5eAGSY" +
                "EDD/k29y/f28Cf+1GvZG5ZvBr+0X7uL0zbvusNs+sR/PP171+hfdYfvUPRj0u+2X7m6di1uUhZAmC4XP" +
                "Gw4oiiEdG3fAX4KWxyslhFuDoy4kv7qgAnbGg2PsfOg0moXACR3Fde/C10G55oCTX8PaKL4FxolUbkJ+" +
                "bYqPfBbwW5VmaS88Jiqd5X5YXYtKOLGl+4X2JQi9Vco2+LX9vPLpo5c1fvoNRF0lieVvqaIJGqodAyn8" +
                "TP1VyqYNMhSfme9MRnGBJNhmiS2oVdNrMOxc9N6PgJ7qnk7JhBMVzH8mw1Jh06GZBg0iXS1JJzl2q9+E" +
                "hIKkJcoJZA1v8K7be/tuLA4Rt/1wVPLEh/8ViZc8zWsdmvMFcYi+cMT7YdRz+zB3dh/+UNnnoV1wMulk" +
                "x+qzfc3uPc91ysMF9wrvcfneYNMn8dQozqiV5gujebwsbYhkiuuxY0V7L5ZNe0b2nRVqY8MTrfy8SW0w" +
                "D8ZV8dQt4FIwCPg4IW67X6DuNds6xMRidXOgRtrCgoHf8yUFlHZlYtoSDZ78+sPbyki/AvdUDMapH4fW" +
                "hlzVSxaSdVxn9ytz3cdPQdiKusRTIRW7TwwQ0JWz82WxTGdQT/y1EmHvZFIobNSmfLG9vMYFPOIxKRQ/" +
                "5tPnBu4xtgjoTMriatjgYUeBboXr0G7xtg/fcEVqdsicjup50ROJyrGxQ2SOrQNTEsV/2PfplOlU9wGN" +
                "FZ+EWurhd94c4JN11bTDgHJY4CcK8l58h5PE70T4B3yL8I/TUFlSnLXBwNX00/PPOJz0H1/gx9B/PMGP" +
                "kf94+tmfX3x6+ZmePZYAvjII3DhN3XmYurHEGRo576Mp7it0uwhDlwtKWBtRynsDKqZ20Dvip6Y7lIG3" +
                "8EGG+Fc/3Iibz6glXYfmay6f/RS+shenMr7Ozn/XQXWoH57YaIDZz00lcLaIlwwy7GTqZ94UIza4bAHr" +
                "jR03c8z21Rxgp/Kwxtb2pZ2ocKMJyP0BTor4Pv+T3ZStGODXJtVsiSXU9my7dj+osnLzkmoVxxNZ7QOk" +
                "1W+5uD8FwvumZDz2D343TvLtlRzF6YX+jppuxOywTKdfs3lZYnfRYW+wHIOo3AFWieuQL7G4+zubF4aO" +
                "nBMyRHmZqYKCzojiNEyKSNGola6bVKoZ86y04md12923F0atv5AH2bbDPvLYKr7U9H+vuOF2DgyH6cJm" +
                "26oPErLWA7dKHtDm/yYgfokYp5NNteKQxJlY9V7aIZYVWrzG6+BH33Z5xg+C7GBVlofdZQR0pqnxjsTG" +
                "xdoHrt9UwtnWFmktrv1n+9St7AsR8V/P0dl0BUIAAA==";
                
    }
}

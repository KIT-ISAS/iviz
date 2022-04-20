/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Detection2DArray : IDeserializable<Detection2DArray>, IMessage
    {
        // A list of 2D detections, for a multi-object 2D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection2D[] Detections;
    
        /// Constructor for empty message.
        public Detection2DArray()
        {
            Detections = System.Array.Empty<Detection2D>();
        }
        
        /// Explicit constructor.
        public Detection2DArray(in StdMsgs.Header Header, Detection2D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        /// Constructor with buffer.
        public Detection2DArray(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Detections = b.DeserializeArray<Detection2D>();
            for (int i = 0; i < Detections.Length; i++)
            {
                Detections[i] = new Detection2D(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Detection2DArray(ref b);
        
        public Detection2DArray RosDeserialize(ref ReadBuffer b) => new Detection2DArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Detections);
        }
        
        public void RosValidate()
        {
            if (Detections is null) BuiltIns.ThrowNullReference(nameof(Detections));
            for (int i = 0; i < Detections.Length; i++)
            {
                if (Detections[i] is null) BuiltIns.ThrowNullReference($"{nameof(Detections)}[{i}]");
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
                "H4sIAAAAAAAAE71ZW2/cuBV+168g4AfbuzNKmhTBIkXRNnG364dts5tsb0FgcCTODBOKVEjKHuXX9zuH" +
                "pKSxnew+NDaCjIYiD8/1O5c5EX8RRoco3FY8uRCtiqqJ2tmwElvnhRTdYKJeu817rM87nK+r6gclW+XF" +
                "nj+q6mRBKu5V3qla0XvXuyBNqLEj0StLEznR6d0+ip2yysuoQEyAiA6J4o2Oe9FJO4pG2la32LFgdTrV" +
                "iq13HZgO2u6MEtr2Q6yri7LzycXbd4tzVfXH//Nf9ePrvz0XIbZXXdiFR0lBkOV1BNvSt6JTUYJ7ycrd" +
                "Q2Tl10ZdK4NDsushAb+NY69CjYNvSAW6SGjMKIaATdGJxnXdYHVDqoi6U0fncVJb6KGXPupmMNJjv/Ot" +
                "trR962WniDr+BfVxULZR4vLiOfbYoJohajA0gkLjlSRV4qWoBm3j0yd0oDp5c+PW+Kp2MP90OewlIzGr" +
                "Dr1XgfiU4Tnu+CYJV4M2lKNwSxvEGa9d4Ws4J3uBBdW7Zi/OwPmrMe6dZS+6ll7LDRkzwPjGgOopHTo9" +
                "X1C2TNpK6wr5RHG+47eQtRNdkmm9h80MSR+GHRSIjfDaa91i62ZkIo3Ryka46MZLP1Z0Kl1ZnXxPOk4e" +
                "zBbBpwzBNZrdlPy5CtETdbbGlW6/ljde6wBnTw65iARIeaG22iowdhT5ArZDjMI/Fv4XdKfJi+B4vLsx" +
                "kEZvyf1wZCU2QyR/MUMLeohszZS0hZt3aQtHNBTtbkhoeYtCvjRjTuhVQ69EA5gQWNKd3CnQhc+BhQQO" +
                "TmyUMK5hhepkVbBILsnb7wOol3QpmXEjN9qASRWqfzC0/TD2DhSCDv+CbV7BjwAVialAJyHzxg3AHjC/" +
                "cQf4hPflO92cALKuXuTFF+5AR7C1YjUq1jHHNkXJjFh0qSo3wUtrVePbjnQygSSpgWLbDQSuGRtVEvO8" +
                "Fn93EUc+DtoTApIGjSGcgGMHBSAPTugI+BxJY6rr41hXQdngfPKKS9ZucINv4Ijdjji+BIb7Qa2SCyPC" +
                "A+1BbEWpgbg5H0Qvmw+kgoWl62rjnIHTXJW3TO8iIRexRzADVAfsjEJCNpiEs0JvFqhe7kTSYCe8lmZQ" +
                "CBxjWH6jPxBGtXq7hYUZ9lkluEEr0wJmyCGFkkAUDYsgbgdo8rax6yTq6AY+wcIygRXpCkZW7PIGKmSK" +
                "s1C0/gYaqksclxcPFMqfc1tKw7bYZz+9Tm432e++EK2LpyKtICmQyXImz9Q4ZGEOB/eNQrYtk4BWF0SE" +
                "RJjEOX1fXsD/BhhBAkfg4PsBaXyNvNIy+jJJAHcHTzPsE8oDbfZugA175YmukMnizn0Y+pTXpjRK/23g" +
                "5bV4rdSRfv7Jz5fgrMZ39rvOeXYwqc1kNTJWknpGhVEkJ90C6ymLJNe7XdXkiBcvRtp7jUQwwVycHTaL" +
                "gkRRMMpLi0h6+3j9u3d1tTVOxme/F6EBb4WTZxdkH3VL+7Mtc0Twnkx/kyqmlhGd8i6fLFSCQy7a6gPe" +
                "eIV4YakSmuZ0mO4oppoQZqdwEooCEkM+BFpm6QgKoSxOhtOOjmyadjKlgo0nhFQqeWKcJMCnBYDl+uUP" +
                "KR8euRRqPorGItxmZKqobSj5TjBBjk9xaqc7EjjQJnYzykxASM3W22auQ7oMdWJCZ0LJvbzO6pwIzAez" +
                "cXBfVs6Y/I1ij2LwpePSImkY4fiVoODXLi+52yuqxSACpfmkbwsVI1hCLxuVKusBBzwBA2WGiohl3k/E" +
                "z+5m3cn30NZEKRkle8KzwzO4/yQyLOb1Ifux83raDmtB05GimzKTTO64locljxmGAMmg75GzV8mN57Ow" +
                "OVWUZ4eVGFfi00p4Fxe4I/4tiOKd5f/cv/xfXj4vUfj26bN3C2EeznTcOt3V711zrajsp+U2v08gjkJ1" +
                "qexawIYU3GVD9dOAWsNbpjvveygBwUpxxykBZXjSRVaZ8ehI3AkeD9PTOD19ehj2Z9XdF1JH+rwVWvj2" +
                "cdY7wRmC68sSlaebh6ggjkpVdsHbRW6qGRL6cuhQauHIoVSeAD/hLhp5ND82cDuOICXhe0S4Cak3mHQG" +
                "CgO3lCwqHtZ99g+RMmZIBRf6BCSjzZqJEPw2zHlN1RoYUgeJlJhecpPAdkklMNNKjURheinViorhLBQ6" +
                "P+qOZ65eMJd72Z3S5MGq3LXsYOW47+pFLT8579kk6vntQLw3XRa13YUB6qv4Zbkm6E/qiP599O7vRKAN" +
                "yKVzIoNKl/XA0npTBYK7rg7HX8eHg8DckJKhyRz48sooFHacLYj37QAne5qSL9sBOSJPRdAXUXqiiYiy" +
                "bQ5F9qOcavLRuwirrpUfUYpQR2pTUQAjTO2n7HuTG1S0KPJD4gSr8DhPnTwVje/LFApunNVspFWlwtB+" +
                "8jLeRdHg+ojejaY51M8I5spfJ+rgc1n7tAOXqbinQaGDxzp3ZzeK6xROjsZQGLge7WyZ46GW4SFSElH8" +
                "cpl8Ezd4eFOvyLnT7VCZQF1tlWpzW8N0uV2iGY/fAsxYm71BnTarrEaj0XLrtKTDBQUVdBvFaqUqP1sC" +
                "okW0hqHoeWpRiShMxs49nnrFWc4g40NDAAWNbgwVLW46bRNzIOiVpj0dVWc6Twjneo0rupBkHiI9Azni" +
                "aSo156mYoWLPt5NwcYptlXqADkeTXUZ5IxQizU+lI9dJpTOmm0i5itFPGupwxiwuvUszIjLQBKqty/0m" +
                "6EE2GLZkmDJAyzXbvUmeKncexaCf0ltn2l9JLmAuyq828bw9TSiS3BkcQG5Um9BrnhDqvPvs8Uo8Pucx" +
                "GY14+rVRW+pXvU09Rd53a6Yj8t+JyMvzMHJqjIRskBeyEnlEN5ET9/1NtMpgbkGKAhfRnHsU0Gnw6eXn" +
                "CMFcO81Akw7cJTQ3TV+m9O1haorzECz1kjw0z43lF2X6djwm0Lob+9sOfjo+mFDOZZDLCemLFC7Tnkmd" +
                "KUpSvMyrmdJLVgO17Z8jN0Xc8Th15oNbd+B2/BwFTqEKQKIdj3WRPFNrWZUhN2CbFDsfSYTT8qoMulfC" +
                "Dt0mmc+7m1BO3+gW/Nw5zcv3Hm6cGTobSto3agfXyNUQoQP6dcfgnCurrQaoBt88YsJX5XWom76fB1o3" +
                "MjlKyD8+UDEhgfMorPLkmbPMSryHYXHMu7AGJPvwZ5rEhDoNBLFpp2pLAx9LYzX8R/VXJ7XJA8o0zyW6" +
                "hRFAUb5i4ryo4q9lgfoXrmrEei2avbQWOaxTADO7W6Xuj5+oELnfkGRJ5GQ7T//Kr0/pcsrIZSj9aIlQ" +
                "t7W2T3b/jsZ7G42iokX/ly0XFjP86d2fph9DouqPGPqeSg34Amxod5FGj2IzRpVc4zv69YkILQ68/RkI" +
                "8A4YFSnfpvaZNyXJiYEzvuUbdrHzqvofSFReWrIbAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            foreach (var e in Detections) e.Dispose();
        }
    }
}

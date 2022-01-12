/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectRecognitionActionResult : IDeserializable<ObjectRecognitionActionResult>, IActionResult<ObjectRecognitionResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ObjectRecognitionResult Result { get; set; }
    
        /// Constructor for empty message.
        public ObjectRecognitionActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ObjectRecognitionResult();
        }
        
        /// Explicit constructor.
        public ObjectRecognitionActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ObjectRecognitionResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        public ObjectRecognitionActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ObjectRecognitionResult(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ObjectRecognitionActionResult(ref b);
        
        public ObjectRecognitionActionResult RosDeserialize(ref ReadBuffer b) => new ObjectRecognitionActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "1ef766aeca50bc1bb70773fc73d4471d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8Va/1MbNxb/+fav0IQfDB3jNCGlKTfMDQGn0CFAgd5dL5Nh5F3ZVlivHEmLcW/uf7/P" +
                "e5LWazAtTRvKJMHWSk/v+/u8tzlUslBWjPlXJnOvTVXqwdXEjdzz740sL7z0tROOf2Wng48q9+cqN6NK" +
                "095z5erSC8u/st0/+Sd7d/H9Du4uAj+Hgcs1AaaqQtpCTJSXhfRSDA2E0KOxspululElMTyZqkLwUz+f" +
                "KtfDwcuxdgJ/RqpSVpblXNQOm7wRuZlM6krn0ivh9UQtncdJXQkpptJ6ndeltNhvbKEr2j60cqKIOv44" +
                "9alWVa7E0cEO9lRO5bXXYGgOCrlV0ulqhIciq3Xlt17SgWztcmY28VWNYIrmcuHH0hOz6nYK/RKf0u3g" +
                "jq+CcD3QhnJgi6pwYp3XrvDVbQhcAhbU1ORjsQ7Oz+Z+bCoQVOJGWi0HpSLCOTQAqh061NloUa6YdCUr" +
                "k8gHios7HkO2auiSTJtj2Kwk6V09ggKxcWrNjS6wdTBnInmpVeUF/M9KO8/oVLgyW3tLOsYmnGKL4Ld0" +
                "zuQaBijETPtx5rwl6myNK11kX8gbH4yRjD7CsiP8ovvJwK9T4IQvZ/2Tg6OT70X62RVf419yS8XHxFg6" +
                "MVeeHHKgSD95MHxUULgbNrc3iINAc2//8uiffdGi+WKZJlmkthaahRMOFOnoUYTPzvv9d2eX/YOG8Mtl" +
                "wlblCq4Nt4TJ4R60Au93XsihhydrT9JbMpC65TioRpn4lZ81/IWTsBaCwyEqp6UiCtq7RAWMrl8qO0H0" +
                "lZQKvNqILF/8tL/f7x+0WN5aZnkGyjIfa0VsuzonLQxrygOrFPHQNXtvTs8XeqFrXq24ZmBY9KJmt1zw" +
                "vvKmola/qRryCmcQBkOpy9qqh9g77//Q32/xtyu+uc+eVZTJH/AADihT+7vu0v1tHgcql8ipTLO5rEae" +
                "9BKcUoZAptbVjSx18ZAA0fOaSNkV20/geY3rVcZzEC6crzFeo+H9vePjRSTvim8fy+BAoVSplRw+Rruw" +
                "yX1rLTNdDbWdUFGj8uHbWYA5UcWSEG03ef0nCPE4NZNTLIVfuIDKxgM+cXx6cdkmtSu+Y4J7VVJGrB6g" +
                "JApYjYiooATZqICo9AIKcHDwsmC9DR4Re45oG9I2qXSmIT4iR1Z3Ume2tleWZsZ4hDYiFCzFbVOswEws" +
                "VBRjogWx6EihBvVoRGqMm7y69dkTlrKjgyx4QIAgUUnOk7lJHq7JUOlsrIEtuB63Ugp7hyoICx0xdKlj" +
                "jbmrJ5xXFfkPpFSOFASIoyZT2KoscZpoumC8mcLVDenkenBJZSmlMEdtqBD5R3aJ8AKpGOzNl60wVKoY" +
                "yPyavBEnAn4FnHROjlQwjZuqXA91noKBOXC9SJ2wXtgApiY1BwXynMauXjIegZAvZDrDQPzKLpB4sOED" +
                "AD3LHjoQt/6iinB0z1royjarV+Gge2I5VrKVrX3Ojzjs7x30z8VnHQ4/2eFSm5QaieQu1F4gdF1u9YDd" +
                "bWo80oWG27gcnUZIyqPaShJwh1IAMrUZRumjo1PiAtQv2akpAunoo+32/kOiln2mmi76e+f7h39ITTH2" +
                "crNp8oA5geEmEtFwi/DwM6WCaAv3apgelkaiH4IY6Kyaw08dPne1+td5XFRl8LjQ1EAz0IqbckOGbEaa" +
                "nKJDC0+74uT0Mq6hYl7lpamL1Jve9d/fL9LpGwKV4ujk7enny0VS7ZuK8jba8AqZfMIhQWA5IhuOJYII" +
                "UTo2DsUK9fh1lRyGhgAEhfEASJV0oFUJtUz0aOxjUccaI4o2JA3tvsrHnKnRTBreTDvp+YTQ6Z39qMpG" +
                "McDiQhEQEO1NNmKWmRG1zOOvJ+lLOkZnQYcTREEevyPGgA+O4P3c1EIGmKVjjggUWT/ENhp0XGqFqdSi" +
                "5qZA+5q3vWiUCYeIYATbG6KK9gMtpQAUC1Z+v58kL9k//unisn9+8bmekkXz8iyE9RCSo7dAEcz0VhHc" +
                "POploEpDaCkERdBSV8REyjHgkoWM1SOGl0B0zliHq9aDMgikcPWT4c7gS2N5g1yMhtEuzmxk4UMw5xkx" +
                "sk/hRumrFX1uSY5uIwgw1zyhE/j1gHyG8MLA3D53YzlFLOO+ivjfKlpTqyjPIuYz3h24eKfcuCF1hfvG" +
                "K27nm1mil2ipqymCzpnEV4fmeDwUGwH2cxlzKpz7SABnpAhKwz+8qW2XIoV4v41RUEpsYeEBnxBNiL9S" +
                "V9dhusZm0BY7yPdk7Q0FPg2K5tlIEY923lIm1NhIEu/7zLImzk4v+n9C1komCNWLwSI7HvnFfGCKOZVu" +
                "ruU7CxvBXqFf5+xNSq0CtKQdrP2AJgEbcZBC3cpCy+r5hPoX8kzqnV0ty3tKcupfILRveO6GWL2I40q6" +
                "96/BnJTO/khhOfjyZeVeFSH1k6H8zBAk4xwxlWQt0j8cFy5P+TXgegRGoYbUVcoKN8YkH93/Ws1X1YDg" +
                "DeG8oLTqqWOwKZSDTxSD1DGASiJYDNqscAdGA2BI8MPF6clzGk7EqfDPe++OY1PZo344Fgl0QKlwAYNd" +
                "K05eqhntcEloAAW1nGs8Wh1Ip3qi3xv1OIHeN3qXagelsdKYa8T4NWrWs/92SMOdnc6+qfPxwZtOV3Ss" +
                "MR4rY++nO8+flwbhDm37zv+eBREtT4srmr1XN6QZQ2k5WC820GSclhaolGnfwSENYIm8cq1UnJMPS3Wr" +
                "B7rUft6LGlzhrzn3jqREfgOA5u7gTfCNBoIjBRZLmIKcK8xPEeOSRkI8hX8LBqOw9FUwmR3RKIDXSAVY" +
                "u6uCnW++e/0q7MgNMEoe+oLOfY478aaLH48BDIBGxqYsGjstXXzxqTxMOwJtvkp0ZiO3tR1WpsZi5ZtX" +
                "Wy/5K6Eb2qBpZhF3oEeeGVvcWa5gAhIkXZDawvB0Yoq6pOeeJnzeTDvJoeHaX+qt0OoKfLc3I2Ydl62k" +
                "ZwrTk80CRbFyweUCkujGocaEGk0uc9SJyaLQ0TPbmcXByILHblgpHYWKJ3p+jtrtc87roBLqYXpj0Q7i" +
                "ATCInYtBaQZdHm6Xck5hmdrINJiNrKjKNwjmWfDLZwGt9FLCCHcF2M83QhCqwsaOZOi1UPPX9QRa2aSo" +
                "3aAK/oLwxXoNMdAYqGKjJ84WZFzrLJgmyECnXaIMCFPUObNKbMJFrIQAUxqeBKDU6MkhfpWhG/ldkBlu" +
                "DkuCV4F7HZB8OBSYl/mnWruYcBoIe+elG1WNdcoUDJOCGTfudzvi5QHlxzr3BKujFlvq6omjYYLIUB7N" +
                "rZJCuqDCMFBTHghgeqYLSBjBQKmqEb6tIJre8AUC6RsfJp4OmnkB3zyWVaVKl0TVNjlErBTRX1g35DS9" +
                "jA31llwBcCmmqmxgTEnjSu2uBho5juCEQGfgWu/Omgf/SExF0AojTXneedwIJaNImt5beOWaE9bM0v67" +
                "J/BosX+Nx7fgkK9ejFT3YIwUevysKxwcjdS6nmh/FVQXR8CRxt8igffniIkPUWKIW3B1a+hfWiq4bBVK" +
                "+fiLQpPeOwRPebLMxEZanZjItCHkpyk3EUIOaoGAdh4dAMdbWa6hEjJSLw7Jj04uX4fh+Iu48lNc2hUv" +
                "F3tebPPKVmsPLe2KV4s9ZGN6c9TaQ0u7YjuuvD0+3aOlXfFte2X7Fb1IyFL+p7qRTHIiQ5izryZHMsMh" +
                "DcN4w2n4PLRmEl4sMB5jVYTwjRexH3DVxqGD9FlVNSWgkDGcUoT+blS6Jwco85GRQzjoRFbA7KWacGZN" +
                "nSJz9qXcYrlX4xQANNkacKSmrdSORfeE7UfAG3+PHUOhbgU8mAYXVg1DM59mJCwEuhG0Vcq9/5DRHZeR" +
                "AEKnoUUXxFdIFIDpRABAjBPrKW9gblY3Z+nQE6kqibFCZUksYMKGqWDx91uBT3V7BcV9SW5X6CgFe566" +
                "kvvTrCavDi181U1lrsIMBhF023yaN59+eSr2H2gtk0jpf6PwvEQ5lHP+Lxjc8HJzK4NvqvD+/k7B5vKW" +
                "/odLdqdS37/7i/azvyV4ktgqkpjzhGwa+4XVgtR1FV9IUf/BkkTe18S5mW1O5EfAlIaSTH5AfrF9uw1F" +
                "NSKHeXnqYaxutrcaIRpPopXVt6rYlLdtHnkrDwNBnzBMN/heq4my3Des33YFwOovXdRr326X/y2I4r3l" +
                "n1cv/4eXN5Kbvt/a/tAS5ulMB4n2Vuj3vrm6/F7fUOMcnoeYJMdsKbsnAr5qNmQ/1vBiWzHdxb6nEXBx" +
                "9yqfXGLojm/i26cF44QW4J2/nmbSp1mW/R9tVyprHigAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

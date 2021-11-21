/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectRecognitionAction")]
    public sealed class ObjectRecognitionAction : IDeserializable<ObjectRecognitionAction>,
		IAction<ObjectRecognitionActionGoal, ObjectRecognitionActionFeedback, ObjectRecognitionActionResult>
    {
        [DataMember (Name = "action_goal")] public ObjectRecognitionActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public ObjectRecognitionActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public ObjectRecognitionActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectRecognitionAction()
        {
            ActionGoal = new ObjectRecognitionActionGoal();
            ActionResult = new ObjectRecognitionActionResult();
            ActionFeedback = new ObjectRecognitionActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectRecognitionAction(ObjectRecognitionActionGoal ActionGoal, ObjectRecognitionActionResult ActionResult, ObjectRecognitionActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ObjectRecognitionAction(ref Buffer b)
        {
            ActionGoal = new ObjectRecognitionActionGoal(ref b);
            ActionResult = new ObjectRecognitionActionResult(ref b);
            ActionFeedback = new ObjectRecognitionActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionAction(ref b);
        }
        
        ObjectRecognitionAction IDeserializable<ObjectRecognitionAction>.RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionAction(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) throw new System.NullReferenceException(nameof(ActionGoal));
            ActionGoal.RosValidate();
            if (ActionResult is null) throw new System.NullReferenceException(nameof(ActionResult));
            ActionResult.RosValidate();
            if (ActionFeedback is null) throw new System.NullReferenceException(nameof(ActionFeedback));
            ActionFeedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += ActionGoal.RosMessageLength;
                size += ActionResult.RosMessageLength;
                size += ActionFeedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7d8979a0cf97e5078553ee3efee047d2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACs1abVMbRxL+vlX8hyn7A5ASODYO53DlusIgx6QwECB3l3OlqNHuSFqz2pFndi3JV/ff" +
                "7+numdVKiITYAUflF2k109Pv/XSPTnvvTVqdm9QOyrzKbbmf0r8/WF0ozW+vBnifnK5ed258XVRxpeNP" +
                "t619bUzW0+l1XN0Pn9eSl3/yay15e/HDnrLMB7hqGLka+YF/8hsyryVvjM6MU0P+LxFWi7wnO2nJ0aEi" +
                "jVzl2U1BWW307b3J5KtMWBE+15LH6qLSZaZdpkam0pmutOpbCJAPhsZtFeajKbBLj8YmU/xtNRsbv42N" +
                "l8PcK/wZmNI4XRQzVXssqqxK7WhUl3mqK6OqfGQW9mNnXiqtxtpVeVoX2mG9dVle0vK+0yND1PHHmw+1" +
                "KVOjjg73sKb0Jq2rHAzNQCF1Rvu8HOBLldR5We08ow3J48uJ3cJHM4AZmsNVNdQVMWumY7gZ8an9Hs74" +
                "RoTbBm1oB6YoM682+NkVPvpNhUPAghnbdKg2wPnZrBraEgSN+qhdrnuFIcIpNACq67RpfbNFmdjeU6Uu" +
                "bSQvFOdn3IVs2dAlmbaGsFlB0vt6AAVi4djZj3mGpb0ZE0mL3JSVgu857WYJ7ZIjk8evScdYhF1sEfyv" +
                "vbdpDgNkapJXw8RXjqizNchV780hV0YIueUlZBDT+aGtiwwfrCOuxaUUzDkZ5rAJy0FBoybaK0c+4yEH" +
                "+dARm5y9ElrRZTgNdnYf4R2ToSlVXinIajz5LVzDjMbIR0WB3UTTi+NMDI5uSKueQYiABZUaV2kYjzhq" +
                "qzjwn2fRLNAw2INl7FzVKmYwcJZhh6Q/hKH3emDYDsqPTZr381QEDBz47UCdYkQWgKlR7StwphB4WLUd" +
                "TYhVyddPkZIck561Bdnjytk86RdWI2rf/ar6eVEZd1Xko7zyX5/Zdm26Q0ZHAq1qRDX/dzOphxon1n3I" +
                "OBK+1pKlqkO58kXkVj6cdU8Oj05+UPH1Un2Lf8WH2fGGiKyZQaRY8jD4dCo5NOSahbAKNPcPLo/+2VUt" +
                "mk8XaVJyq51DkkI+7xny1TsRPjvvdt+eXXYPG8LPFgnDwAZVAhke2ROZtokbpftwM4p4SO8okM2US0o5" +
                "SOaM3nw9xl+EKGtBcjcK3LgwRAEOG6mA0Y1L40YoZAVV1cpsBpYvfj446HYPWyzvLLJMmUunwxzVFomu" +
                "TkkL/ZpK6ipF3HbM/qvT87le6JjnK47pWRY9qzk9zHlfeVJWm99VDXmFt8h9fZ0XNfLiLeydd3/sHrT4" +
                "e6m+u8meMxQ+t3gA50RbV8vu0vl9Hnsm1Ug8TLM5rAbkoPzNxRagJy8/6gJJ+xYBguc1kfJS7T6A5zWu" +
                "V9qKg3DufI3xGg0f7B8fzyP5pfrbXRkMJW0Vh3fRLmxy01qLTJf93I0IH1JBbczAEIc4MdmCEG03efEn" +
                "CHE3NZNTLISfHEAI7BafOD69uGyTeqm+Z4L7DdgIQAyUVAarEREjStCNCogKFXW8DWiH9Na7Q+x5om1J" +
                "26TSSQ7xV0AdAJL9orAThva0EKHgFsGIhs44IzDuaNU12pKZXj0YkBrDospMq78CrohFOrltR1j7yWSy" +
                "d985wDDXPL2SjQ+POlYyBuD7OS/1prt/2D1Xn7VZXkswJ7Z3EYxS0wcv8KnLewxmx7aC5+VwX5+i/5P4" +
                "HtROk4SAI3A8+GU/iB9gNMUAGrCCITPhe9p6Z9MBKAZqyWeq6aK7f37w5ovUFJB9ardsKvAFcGCkERVT" +
                "xGs1MUZEm3tYw/Qc76LfbTY/fBAt6/UrOl3QpjiddJtQDhTjx9wpI0ORMsdoneXbjjo5vQzPkH+v0sLW" +
                "WRwaLLvwHxfp9BVBFHV08vpUfZFUB7YkYIH5SIn8Cf+gXAzoFeokhxMVnCAdW4fChYYvNZ6L7Wg6Q8AK" +
                "XwD3kA5yU0AtIwxnqlAiqHmi+tQGODKHMemQW0F0+ZYX00r6fkRYZ2k9crzFSizjTlTqKa2NNmKWmRGz" +
                "yONv5+pL2kZ7QYdzREZOv6eGKEaewOLM1kpL0c5DmhCKrB9iG5MTHOqULc28qY+x9i0ve9ooEw4RShuW" +
                "N0RRFE2F2htjUM1Z+eN+Er3k4Pjni8vu+cXnekoSzMtDKtaD5MfKoXYz0zuZuHnQS88UlmqvBIVoqUPD" +
                "NLYJxYCPFrIuR7mm7GxKb53HURuiDIIGXAO1nCm+NNQfkY7Rfrj5ns1E3og5z4iRAwo3ymCt6PMLcnQa" +
                "QdCNzuL4A34N3y8x4xvgzfSJH+oxYhnnYQBTQs7WODHIM4/5hFcLF2+NHzakrnDecMXpfDJL9AwNWjlG" +
                "0Hkb+VqnVpynlQOASK5kHqzQt+9pgjJA3QKwRPza2nUoUoj3aYiCQmMJCw+chGhC/GEMdy1jTzZD7rCC" +
                "fE/XlaXApwneLBkY4tHNWsqEGhtJwnmfWdnU2elF90/IWtEEUsB4bMGOR34x69lsRtWby/ne3Eawl3R/" +
                "nL1JqaVgSFrB2pdxFeZS2Eih7nSW6/IJ9GHYM6kT8zVuCZaV5M2/QOjA8kAUsXoR5sh07tcCn5TQ1r6k" +
                "thzef2W5UUjIAmSraoJW3cAMWIH5uxYTwHfh9ZRiZXaI2MhMn9oUXeLEkOdDBFyb2aoyIA4h+zFzJGyI" +
                "YkUttfR67BZZL/YPoBIJZr02KzzlpeE8JPjx4vTkCXW7YWL/y/7b49ClbFODFeoEGvZYu4DErimPYVre" +
                "zAq4KjSYgnqYxzz27mlvtlV3e7DNOfSm1fFYIGth7TXC/Bpl69F/10nD63vrB7ZOh4ev1jtq3Vlb4cmw" +
                "qsZ7T54UFhEPbVfr/3skIlJ1I/Yoq0ANRBtZVqwXOjIyTksLVM3yah2bcsBLpJZrY8IdRr8w07yXF3k1" +
                "k0sYs8phIbARJfLtDAbIh6/ENxogjiyYLcAKci4ZyCHMEWiF4RuS12AwCEsfFZPZU40C+BmpAM+WVbD3" +
                "3fcvnsuK1AKmcE+KdTc5Xg8nXfx0DGwAQDK06IKjnRYOvvhQvIkrhDYfpdYnA7+zK0/GGHLtqe+e7zzj" +
                "jwRwaAFStp2EFWh9J7h2WnpcwgQkSDwg9ofy7chmdUHfgy2MYOx4PTo0XPv+Lu1Wl+G15SaN+CWTz1VN" +
                "kXqylaE0ll68TvBEJ9ydjPQMRKj4UEums4xtQTOJVnLBQGRIvlTSkwJ76XYN9KoZKniVcnYHFamK8UKp" +
                "Hcc9IBGHUWZhexRTHlUU5bZq+sk47AuswAEbHPNIXPORYJbtmDPkLAH/fCIEoVps3UBL04XKv5GPoJUt" +
                "CtxNquNPCWVs1BAD7YHJNrfV2ZyMb+0F0wQcaLePlAFksjplVolNeInTEGBMdzQClxo94YrIGeBPuX7c" +
                "sv2tfkEgS7inSyIIJ5uEeZ1+qHPpATpzILt0J0qFY4OSBYMlMePmzZ5HPTukFFmnFYHroMWWurbVkTwT" +
                "5dH1WFRIB1QYDOaUCgRST/IMEgZIUJhygE8riMYLWCEQP/Fm4umwGRzwybi8LA2u1oKouYsOEYpF8BfW" +
                "DTnNdsKGek2uwLdFnK3kMgmv3F/1cqQ5AhUYih351tVm88U/IlMBusJIY56hHTdC6SASuOjNKuObHc5O" +
                "4vrlHfhqcf0LMMgnz6d0uE4CuGnFRwe3CJ+4JmxE0t+I5jaDVBAJJRSZuSFy6aiusuYps+Mv6kmcV4s3" +
                "PGACYlPckn/IghLZ45iCCA6L+IhtWFbsjO2tbNZQkcQjLvUCoPbyRbg+Ck9+Do9w7zNf83RXrlVaa+gR" +
                "bkDma8iUdOnQWkOPMMUPT14fn+7TI4zN2092n9MMOomZnipEtMoJvYeA7JLRX2y/T8MvXnAq7/vOjmQm" +
                "zciLVSFRGg5iv+D6jE2H8b0pa8ozkhi8gd17GPXGc1LALz4GW97AD0e6BEAvzIgTaGwLmbP784zF1oxc" +
                "4pCgY2ugEZu0IqfGCB5MWH4AcPH30CFkZoqfPBQ0qHCmL817nImwHOg+0EYZ/+7XhA65DAQQaQ0tOiBc" +
                "QFCsxR2CdhgU1oSfjHCzuhmLmx5MW1GQVVqLkgEDNnyJ3d/tCKtmegXd3S/DKxTVRH1ADX7FDKvJo338" +
                "oAI/UdAYs/DkBaE0bd7NmnefHk6CW1rKRqr4+yAelGAGBhRA90jc6XJXq8VJee57o0ZzRYu/OUqWivPN" +
                "w++5kf092RuhnSGhOW3IPGTRdiJ4XYZfu1DjwcII+yBybidbI/0e4KShJNAxQIXd6S501Ugt4/LYvODH" +
                "N3F5qwOi0SR62Hxqsi09bfPIS3kQCPqEXDriga3uyXHDsDHFWKejPnVQpat2n/xvRRRvPP5l9eP/8OPN" +
                "6KzvdnZ/bQnzkNYje+2vUPFNi3X4khiPs/C9BCe5Z0vf20qAVbMg+amGL7uS6c7XPZSM89NXeuYCT0se" +
                "ik8f5rwThICP/nbKie8mf4FrzP2FX7h++W+Omt/K/nV+JDsXbi35P3AsmSw2LAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

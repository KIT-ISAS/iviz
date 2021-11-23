/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AveragingActionResult : IDeserializable<AveragingActionResult>, IActionResult<AveragingResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public AveragingResult Result { get; set; }
    
        /// Constructor for empty message.
        public AveragingActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new AveragingResult();
        }
        
        /// Explicit constructor.
        public AveragingActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, AveragingResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal AveragingActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new AveragingResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AveragingActionResult(ref b);
        
        AveragingActionResult IDeserializable<AveragingActionResult>.RosDeserialize(ref Buffer b) => new AveragingActionResult(ref b);
    
        public void RosSerialize(ref Buffer b)
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
    
        public int RosMessageLength => 8 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c8d13d5d140f1047a2e4d3bf5c045822";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WTXPbNhC981dgxofYnVppk36kntFBlVRHGSfx2GqvHpBckWhBQMWHZP37vgUpSnKs" +
                "RockGtu0JODtw9u3i31LsiQn6vTIZBGUNVrlD42v/MtrK/V9kCF64dMjG63IyUqZ6o581EG49MiGX/iV" +
                "vb+/vkLMsuXxtmV3JkDGlNKVoqEgSxmkWFiQV1VN7lLTijQTbZZUivRt2CzJD7BxXisv8FORwQG03ojo" +
                "sShYUdimiUYVMpAIqqGD/dipjJBiKV1QRdTSYb11pTK8fOFkQ4yOH0//RjIFidnkCmuMpyIGBUIbIBSO" +
                "pIdo+FJkUZnw+hVvyM7ma3uJt1QhBX1wEWoZmCw9LqEv85T+CjG+aw83ADbEIUQpvThPnz3grb8QCAIK" +
                "tLRFLc7B/HYTamsASGIlnZK5JgYuoABQX/CmFxd7yCZBG2nsFr5F3MU4Bdb0uHymyxo503x6HysIiIVL" +
                "Z1eqxNJ8k0AKrcgEAd856TYZ72pDZmd/sMZYhF0pI3hK722hkIBSrFWoMx8co6dsPKgy+0puPFobGf+L" +
                "zFZ4cHxO8JttwbRvbqcfJrMP12L7Goof8JdtSWmbqKUXGwpsyJxYn6JNfCdQGxs5d6i/DnM0ns/+moo9" +
                "zB8PMTkj0TkoCxPmxBqdBHx7N52+v51PJz3wq0NgRwXB2rAlUg578Cdwvw9CLgKcrAKf3nGC6DHVgaky" +
                "8T+vM/zCJEmF1nCoyqUmRlDBb1FA9HxOrkH1aW4FgS46yvd/jsfT6WSP8utDymsgy6JWxLR9LFiFReQ+" +
                "8JwQx8KMfv94t9OFw/z0TJjcpqOXMdlyx/3ZSGWkz0rDrvAWZbCQSkdHx+jdTd9Nx3v8huLnT+k5+puK" +
                "cMQBqaBsDE/t8v3nOeZUSPTUhNkHi+iTQYIpdwh0amVWUqvy2AE65/WVMhS/fAPn9dYzNqQi3JmvT16v" +
                "8Hh0c7Or5KH49VSCOeGqomcZnqIucvJptg5Jm4VyDV9qfH2E/S6QmFB5cIh9m7z5Aoc4TWY2xUH5tQH4" +
                "2jjiiZuP9/N9qKH4LQGOzFaM7vYAkiiRNQahVgTZS8Aog3YK8DC4LpNu+Qm15xnbstos6Vrh+KgcaZ60" +
                "zuxspLVdp3mEF6IUHNdtf1mBTHdRcY2JvdGKt5SUx4rnqu1tFugxZN/wKptNstYB7QjSieQDp5vPk+5k" +
                "SLquFWaLdB/vtZTkDip5Fpql0SV2d8xTnbCfDPsHpyTPAmHEoWaJXGmN3Yzp2+StCaF76K31YEly3FIS" +
                "o/1RoeOP7tKNF2jFoLc5zMKCqMxl8Q+7ETva+RXjpPeyojY1fkmFWqhiWwyJgR906DzrtQtAqompKNDn" +
                "FFYNtsnjIeSrpy5EJEdBrpdPhvIsW2grecbk0dIp6x6kqTT1H8ulRUqb7D98rCbg/AsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

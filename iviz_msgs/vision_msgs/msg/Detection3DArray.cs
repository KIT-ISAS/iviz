/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Detection3DArray : IDeserializable<Detection3DArray>, IMessage
    {
        // A list of 3D detections, for a multi-object 3D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection3D[] Detections;
    
        /// Constructor for empty message.
        public Detection3DArray()
        {
            Detections = System.Array.Empty<Detection3D>();
        }
        
        /// Explicit constructor.
        public Detection3DArray(in StdMsgs.Header Header, Detection3D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        /// Constructor with buffer.
        public Detection3DArray(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Detections = b.DeserializeArray<Detection3D>();
            for (int i = 0; i < Detections.Length; i++)
            {
                Detections[i] = new Detection3D(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Detection3DArray(ref b);
        
        public Detection3DArray RosDeserialize(ref ReadBuffer b) => new Detection3DArray(ref b);
    
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
        [Preserve] public const string RosMessageType = "vision_msgs/Detection3DArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ce4a6fa9e38b86b8d286a82799ca586d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZbW/cuBH+XP0K4vwh9mFXPcdXN0hhFEkWaQxck/SSXl8Cw+BK3F1eJFEhKdubX99n" +
                "Zkit1tn0+qGxkTgSRQ7n5ZlnhsyReqYaG6JyK3W2ULWJporWdWGmVs4rrdqhiXbulr9ifDfD+bIoXhld" +
                "G682/E9RHE1ExY1JM02teu96F3QTSswQeXloFKdau95EtTad8ToaCFMQYoNIvLVxo1rdbVWlu9rWmDFR" +
                "dVxVq5V3LZQOtls3RtmuH2JZLPLMs8WHq8m6orj4P/8Uf333l6cqxPq6Devwe3EQbHkXobb2tWpN1NBe" +
                "s3M3MNn4eWNuTINFuu1hAX+N296EEgvfkwtstrBptmoImBSdqlzbDp2tyBXRtmZvPVbaDn7otY+2Ghrt" +
                "Md/52nY0feV1a0g6/gTzaTBdZdTl4inmdMFUQ7RQaAsJlTeaXImPqhhsF88e04Li6P2tm+PVrBH+cXPE" +
                "S0dS1tz13gTSU4en2ON7Ma6EbDjHYJc6qGMeu8ZrOKF4QQXTu2qjjqH5223cuI5RdKO91UsKZkDwmwZS" +
                "H9GiRycTyR2L7nTnsniRuNvjfxHbjXLJpvkGMWvI+jCs4UBMBGpvbI2pyy0LqRprugiILr3224JWyZbF" +
                "0UvysSCYI4J/dQiusgxTwnMRoifpHI1rW38rNN7YALALICeZACsXZmU7A8X2Ml8hdshR4CPjz9xFQz7V" +
                "agk4VDS7amCNXRH8aMmS0dIMNRmExLY8ajugvOUZM05o+Nnd0hR9X4DsmSgn9KaiT1NJSuN3q9fwqRNy" +
                "cGppVOMqdqiVqALoBEmeeIigXtCuFMalXtoGspFlauHgg85FtdE3JD6ZYtRm2ztIDfjMijUNaRQsYMMq" +
                "JFK0NciSdg+V83kuqArjLJYYjHLBG4LA0Er+QvkfyuINi3iVNrLhHwDGW4AYPCUuCaQ2HL50Q8feXbo7" +
                "ANL7/E4biyJl8TwNPnd3tARTC46h4QAzsVCK7uiSrcs7IUVKU+JtTS4fGbrCAxGLG4jZEzEb8fFJmRhq" +
                "F2lCOlntQSzWEyUn12Wmh7MCfBzB51uWBk+Yto/bsgimC84LVN86EMyLxg01OMcNvjLXFb2RRZcrySwQ" +
                "TyBI3OqgotfVR3KIWDOTGStrmhqcE/F9ACSWzjXQ8DrPZmkLIVVSlBiQwtVVW6VhOdDCBatvJgUn7xuS" +
                "9Te6GaCEbRq2p7EfiT5ru1oBfFyR2GH1RBvg1WiQnUW8QCkD/HwfCqUYunWD6D+aMyOPAQKMVVRVJxJ3" +
                "RtH4ezY3UUz+8EAs8zVQU4fQ5azZjJ8FlPB81BbF/BB9lBnHqHioVxSy1GQkaUwnCIcDuKPSdc0i4NUp" +
                "MjWSKO46i8sFkDggCECPBfw3AzqMOUpezYWBRaKmtGYmKYykQV3ZuAEx7I0nuUpLxJ37OPRScscKT7/A" +
                "l8iod8bs+ecXfr6EZiXeGXctmIMApm0zRs3W2eodYW2VgHSFMkQFTqB3v+FKfKCeb2nuDWrUSMFxB9hk" +
                "CmpYpk+vO2TThx/mp1dlsWqcjuc/CqtlTc4XFB9zz/u7WKaM4DlJ/lLIsuZiQy0Br8xSgkOZXNk7fPEG" +
                "+cJW9ZT7KlVq2SOHauSftcFKOKpSFexDoiWV9ogSzuI6Pc5oKaYyc0LhlGqvHTVShMQ4WpCoLLVWf5JS" +
                "vQcptKOUjdm4pTAa2i7qC0aaIOBTnnbjHkIONIlhZqgomdpy9FZJ6yCboYUV7gZfcokSd44CdgtTcLBf" +
                "cs42U2kwlIMvHHc94mGk4zeigt/aPLcV3lCbCBOosxB/d3AxkiX0ujLS9A9Y4IkYqD4UJCzpfqR+drfz" +
                "Vv8Kb42SJCgJCed354D/aDIi5u1dwrHzdpyOaMHTkbIbdQC6MBzn+m6qY6IhUDLke7QTUvInaxFzanaP" +
                "72ZqO1OfZ8q7OOEd9U9FEr8Y/tfh4X/z8EnOwg9n51cTYx4udHyq+9K/X4ZrRicSGq7TdyFx9NBTZ5eq" +
                "4MI+Tij+NqAT8R3L3c17KAOhSobjWIASPdlsq058tGfuSI9349N2fPr8MOrvXHcopfb8eS+18PZp53ei" +
                "MyTXf7coP90+RAex18gyBO+3wNIzCPvmYFF1Adw4k+iZE4kqe+L/43O1ePOSzncLnNS6wHcHLvWSJJRa" +
                "dEvtHSX0jIWhXFHhmaWGlbZr7Tp1z3ISwakTkPHgguoj6H7XVOxFa5aL7GqI6N3KSWf+tWQ5WNLElgMM" +
                "n78kucF+NocEzKYWHj5IPMoek2IG2fcryi/cyZ/xJg+D9bTjwdpxIzdIexAv6XLjkq8jXIduvDUaKYzG" +
                "eFyJhTUOKJXQElyG/gOdDke6TkdCyGj1R4gEWqTd7nsI09RRd6GRKEU+zRybcl3O1O3GdDJLzrmQwMct" +
                "9CnernEG4JW7BoJkqmQcSsrqMZ8jRGfZTFqVXB5w4kqHglsyCA8+nez4SJn14g42OjfjkiYiDnDfeIIC" +
                "70U0vr/JAt8m1F859+VgZyU3ruFbiMo1TTqJAeCv53XOZt0IVQeKg6026YiZmP1rx4Kcrh2NNJyokeRF" +
                "lHETK8YGpEgRyJc5AQGTblarpe20BzU0bjljvmn0lqinNqHydjneFyVVGLUpM7/jE134DtTj9XYkBdmL" +
                "z7u73m9Jbctad8i5Wj2ucVynI/icjpsn1Ome1tR+DB23J6YGUt7uxITJWiiN5bw6ZMk4YtRDlVvYCk2N" +
                "R36YHi2YRCeMfgJSvHHcW9M1mVvNVw1d3or2dAdGfT0vEuV19WmwQnFCqtyT37uPpDPdMbW+Z3V2djgp" +
                "v7zCebyAAn6oiESzFyfuKuVywCTnIVKjQ2aQwpfMNkDOKWtya2tYaKXoN6Zb4+2A0Hz5KQLyGy8u+CJN" +
                "Ai1iqo3uOtOEbKr1GRCpAiS8sG8INKX0RC8JCh+u5JAfCrmrwA9O9ksLEqnR+imQWphcK44f/pyVEr2v" +
                "EaQea4/UT6NRkz5muY0mjCu8u83z76/Ap/35T+gGHTv/Lk3+8DPgfKWeIR45+/j7TAoQ9DzO4r8X750U" +
                "4y1MzbSafo74zkJZ9j7dleFvR5dxOK2CNwUR3+zK/j4DcTAOE5ActSnifeYg9B7JdnjDb1OgsXx6i5Wl" +
                "CPMIpp6oy9fvn5D5F+o0jfw9DV2ox7s5p+c8cjaZQ0MX6sfdHIolRv4wmUNDF+o8jbz86c0zGrpQf5yO" +
                "gNkv1JMi3zvQlUcOyWst6cyYzIBxqxXdOPGEN/LMt1w4J/so3T+5QtI0bcSgoP/WoEWL/Gy6gYhGmCGg" +
                "dqNnwwE37VOhNYlJkVcAIv8HkGlMywya2ynWrPgPHKsfWMgaAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            foreach (var e in Detections) e.Dispose();
        }
    }
}

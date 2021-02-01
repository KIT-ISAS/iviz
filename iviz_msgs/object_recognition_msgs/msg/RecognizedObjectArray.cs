/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/RecognizedObjectArray")]
    public sealed class RecognizedObjectArray : IDeserializable<RecognizedObjectArray>, IMessage
    {
        //#################################################### HEADER ###########################################################
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // This message type describes a potential scene configuration: a set of objects that can explain the scene
        [DataMember (Name = "objects")] public ObjectRecognitionMsgs.RecognizedObject[] Objects { get; set; }
        //#################################################### SEARCH ###########################################################
        // The co-occurrence matrix between the recognized objects
        [DataMember (Name = "cooccurrence")] public float[] Cooccurrence { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public RecognizedObjectArray()
        {
            Objects = System.Array.Empty<ObjectRecognitionMsgs.RecognizedObject>();
            Cooccurrence = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public RecognizedObjectArray(in StdMsgs.Header Header, ObjectRecognitionMsgs.RecognizedObject[] Objects, float[] Cooccurrence)
        {
            this.Header = Header;
            this.Objects = Objects;
            this.Cooccurrence = Cooccurrence;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public RecognizedObjectArray(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Objects = b.DeserializeArray<ObjectRecognitionMsgs.RecognizedObject>();
            for (int i = 0; i < Objects.Length; i++)
            {
                Objects[i] = new ObjectRecognitionMsgs.RecognizedObject(ref b);
            }
            Cooccurrence = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new RecognizedObjectArray(ref b);
        }
        
        RecognizedObjectArray IDeserializable<RecognizedObjectArray>.RosDeserialize(ref Buffer b)
        {
            return new RecognizedObjectArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Objects, 0);
            b.SerializeStructArray(Cooccurrence, 0);
        }
        
        public void RosValidate()
        {
            if (Objects is null) throw new System.NullReferenceException(nameof(Objects));
            for (int i = 0; i < Objects.Length; i++)
            {
                if (Objects[i] is null) throw new System.NullReferenceException($"{nameof(Objects)}[{i}]");
                Objects[i].RosValidate();
            }
            if (Cooccurrence is null) throw new System.NullReferenceException(nameof(Cooccurrence));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                foreach (var i in Objects)
                {
                    size += i.RosMessageLength;
                }
                size += 4 * Cooccurrence.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/RecognizedObjectArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "bad6b1546b9ebcabb49fb3b858d78964";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZbW8buRH+vr+CiD/IPshyEydu6sIoHL80OSRxLvahvQaBwd2lJMYrUiG5ljZF/3uf" +
                "GS5Xa1vX3vliCzas5Q6H8/LMG72xcY+PeH1yeHzyUdxnb/vJXitZKiem/CfLNsTFVHsxU97LiRKhmStR" +
                "Kl84nSsvpJjboEzQshK+UEaJwpqxntROBm3NPgi8CsKOhc2/qCJ4EaYyiEIaoZbzSmqDBRW3ZpHk0qnC" +
                "Toym/ZczP/E7H+PCN1WeMcWnz4kbxLuXmc5PDj8evf5DZmLDkLrbtihq55QplJjJ4PRS5CoslIqquU74" +
                "TuhxZWXYfQY1CrvanGUH3/mTvTv/+77woYxmjI6F3OdBmlK6Ek4NspRBirGFw/Vkqtx2pa4VfBnkbA6R" +
                "+S353I8SEvAzgbecrKpG1B5EwUKR2aw2upABENEAS38/dsLPgIp0QRd1JR0p7kptiHzs5EwRd/x49bVm" +
                "Q7453ickeVXUQUOgBhwKp6TXZoKXIqu1gQlpQ7ZxsbDbeFQTwLY7PAINwgJnDuCFMNLv44wfonIj8IZx" +
                "4B1TerHJa5d49FsCh0AENbfFVGxC8g9NmNrozWvptMwrRYwLWABcB7RpsNXjbJi1kcYm9pHj6ozfwtZ0" +
                "fEmn7Sl8VpH2vp7AgCCcO3utS5DmDTMpKo1IFJXOnXRNRrvikdnGKdkYRNjFHsFf6b0tNBxQioUO08wD" +
                "ueDO3rjU5UOh8bfG+D0D+zvkvzawY/6L9gAUEaN+zr4E2snaczg3vh2K92cX7RpgeFlUti4TrG9n09+v" +
                "0tmrH0+OLsSb96dn99eLtDqyJiDhIoIN4n3GCVrI3NaBhefMDpAl7dg5lLkpPdQmpS/KHxYmwQvAl2yg" +
                "VQWzzJA+gjA2IPthjTGcq0IiQzDHmClUMTUaMQ4cWiYmSno/E0hBt+gdiEAJMhzqKDV4FnWWfMQisyDq" +
                "poy/hrKIrQvaRnvBh8tVSTlnX0ztAtHllGhsLST+ak4gnEgiR7YPiY3YxqFOWMOp6w0TprT/JyZ72hkT" +
                "gCBlAByQd0wV0QeAoi0HYiXK78dJQsnR25/PL04+nt8XKVnrXk6jbIdYqoOTRYTJbhlh3tolV5VF2miD" +
                "IlppKNqyzjHgk4es0xOkfBQXZbx1HkdtRmPgCOmcbJCU+MyIpam8RgJDOXKrPVtZ/BLd+YEEOaJwo2La" +
                "iz5/Q49hp4g1qCRt3gSuc8IMZb3cLnf8VM4RyzjPkPy7Za/gtfqsYj5j6ijFO+WnHatLnDddczqfzBo9" +
                "K8FujqDzNsk1CIA319MJih03VV7FfV9qYH6CFkoSPoKt3ZAihWRftlFQSZCw8sjkiCbEHwrFVSzM7Abt" +
                "QEHYk3WwFPhUY5psokhG1/SMCTN2mrTn3bPJEh/Ozk++Q9ZKLoi9lK+rEIFHuGhyWzZU4Lmz3F/5CP7y" +
                "lKRi9iajGq5yTMHWH7Hp0LJgI4W6k6WWZgf2UIxMILH2tazuGMmrf4DRkeWSjVg9bzsdOvexi+Yqnf2R" +
                "wnL88GXlThUh85OjwsLSgMA5Au2hjPYHcAF5yq81VwsERqnGmgBscGKb5Fv4X6lmXQ2IaIj7BaXVoFGp" +
                "XArliIkyT30PuCSGZd4XxQfruHeEBj+en73fQaubGspfDt+9FZHBSByaVCR02RUuTARXipOXYgAmq6wa" +
                "CuzFudSV5dKrkTgZTUacQO86fUi1g9JYZe0VYvwKNevJvwdk4cH+4MjWxfT41WAoBs7agJVpCPP9nZ3K" +
                "Itxh7TD4z5OoouNG01Dbbq7JMpbScvQezwKxsvesQKVMhwE2aTSyyCtXSrUt9rhSS53rSodm1FpwDV6h" +
                "sIpG5OFBF+L4VcRGNxAiBZY3egoCFw9HiPYloqxS3MCfQsBWWXoUzGZfdAbgNTIB1m6bYP/FX14+jxSF" +
                "RY9SxCl1cFfiQXvS+U9v0RigG5naquz8dOPg86/V60QRefNRYrCY+N29uDK3Disvnu8+40fqbogA+dou" +
                "Wgp05AtMRbeWDVxAiqQDLtspMr6d2bKu6H2g8SHY+SABGtB+qIFyfQW+fVNAwnouW8nOFKbvt0sUReMj" +
                "5GInMRSLqcasNZMNmBQxnwhZlrpFZj+zeDiZgGRopfIUKoH4hQa1OxSc18El1sM07PSDOEcP4hqRVzan" +
                "gPKonw2FZbrUSPNUK4oyoetgnkRcPondyigljHhWbPv5RChCVdi6iYyTP2r+pp7BKtsUtVtUwZ9Sf7FZ" +
                "Qw0MBqrcGokPKza+txdCU8tAu33ijBamrAsWlcQERJyEAnNUuLZR6uzkEb/K0ok8Rtrx9rii9ipKr2Mn" +
                "HzdF4WXxtda+TThdC3trXqeqsUmZgtuk6Matu9OOeHZM+bEuArXVrRV75hqJN+PUIsN48FRnkCG4cBuo" +
                "KQ/EZnqhS2jYNgOVMhM8rWGaLgcig/TEm0mm4+72ik/GYG1U5ZOq2iVAtJWixQvbhkAzythRpwQFtEtt" +
                "qspyaysEJKS7zDVyHLUTApOB743d3Yu/JaHaphVOmmPvhnjbKSVblSBF3gTlux3OLhL97R14dZP+JQTk" +
                "k9NnQxzCFyny+N1QeOCMrLqZWP8QLbfVagWVSq5gHZMLR0WVLU9pHb8oJtpcy0qn8eDRsg87Yn3yIffF" +
                "sJ6n/ENdcNQdge2a1snY3stkHZeYdSKeXqKXvXhJ6h+Ip+3Kz+3SgXi2onm6xyu7PRpaOhDPVzTkR6y8" +
                "6NHQ0oHYa1dO354d0tKB+HN/Ze85Vl5mKcdTbUgueS9jKDMeE1jseEzXr0xwFr+PnZ3RtZDjW9loihii" +
                "7UEMCq7M2HScvitTU5KJWcErRR3etUrnFGi8QivIa4BwJg368krNOHumaZAleyhY3JzHOMzRMfYuMdJg" +
                "VmnPqgfq3yfoKf7aTgWlWgogmC4nnBrHgT3dg7ASmDgwOin/6XNGZ1y0DBBjHS86gLjJGGVpR2xyuBes" +
                "50zA0qwfwNKmRzJVUmONyZJa6Ps6oaLHP+1GOdXyEoZ7SGnX2CgFe5Emj7s3Vl3uHDtg1c9loeI9CyJo" +
                "2X1rum/fHkv8Xxkfk0rpsprvRJRHyeYbWh5qeYCVEZt8SX6nKHMJSxfg2a1qfPfsB51Z/5/iSWOnSGPO" +
                "E7Ib3ldei1rX2ODI1zRjsCat7Bvio11sz+QXtCIdJ5lwQLjYW+7BUJ3K8T80aU5xuiPvDTt0BYlxVS9V" +
                "uS2XfRmZlC/8wJ/6lGHEXm9QcjwbbC6HAg3ptyFqcuiPxP8UxPHO8i/rl//Fy1sJpp929z73lHk810Gj" +
                "wzX2veuuIf0niJbL9n2MSQJmz9gjEXuojiD7qQaKnWG+K7rHUXB19jpM3hDoFjbx9HUlOHULQOf/TjPp" +
                "2yLL/gugXJOm4h0AAA==";
                
    }
}

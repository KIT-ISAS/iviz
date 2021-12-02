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
        internal ObjectRecognitionActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ObjectRecognitionResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ObjectRecognitionActionResult(ref b);
        
        ObjectRecognitionActionResult IDeserializable<ObjectRecognitionActionResult>.RosDeserialize(ref Buffer b) => new ObjectRecognitionActionResult(ref b);
    
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
                "H4sIAAAAAAAACsVabVMbRxL+vr9iynwQpIQcG4dzuHJdYZBjUhgIkLvLuVLUaHckbVjtyDO7luSr++/3" +
                "dPfMagUiJnYglG2k3Zmefu+ne/zW6Mw4NeZfiU6r3JZFPria+JF/+oPVxUWlq9orz7+S08FvJq3OTWpH" +
                "ZU5rz42vi0o5/pW8+pN/kncXP+zh7Ez4eStcbigwVWbaZWpiKp3pSquhhRD5aGzcdmE+moIYnkxNpvht" +
                "tZga38PGy3HuFf6MTGmcLoqFqj0WVValdjKpyzzVlVFVPjEr+7EzL5VWU+2qPK0L7bDeuiwvafnQ6Ykh" +
                "6vjjzYfalKlRR4d7WFN6k9ZVDoYWoJA6o31ejvBSJXVeVjvPaUOycTmz2/hqRjBFc7iqxroiZs18Cv0S" +
                "n9rv4YxvRLgeaEM5sEWZebXJz67w1W8pHAIWzNSmY7UJzs8W1diWIGjUR+1yPSgMEU6hAVDt0KbOVosy" +
                "sb2nSl3aSF4oLs+4D9myoUsybY9hs4Kk9/UICsTCqbMf8wxLBwsmkha5KSsF/3PaLRLaJUcmG29Ix1iE" +
                "XWwR/Nbe2zSHATI1y6tx4itH1NkaV3mWPJA33hkjCX2EZUf4ReeTgV/GwJEvZ/2Tw6OTH1T8eaW+xb/k" +
                "loa3qbH2amEqcsiBIf2kYvigIDkbNncfEQdCc//g8uiffdWi+WyVJlmkdg6ahRMODOnoXoTPzvv9d2eX" +
                "/cOG8PNVws6kBq4Nt4TJ4R70BN7vK6WHFTw5r0h6RwYyc46DcpQsGb39s4G/cBLWgjgconJaGKKQVz5S" +
                "AaObl8ZNEH0FpYLKbAWWL34+OOj3D1ss76yyPANlnY5zpIgMfpiSFoY15YF1irjrmP3Xp+dLvdAxL9Yc" +
                "M7AselazWy55X3tSVpvPqoa8wluEwVDnRe3MXeyd93/sH7T4e6W+u82eM5TJ7/AADihbVzfdpft5Hgcm" +
                "1cipTLM5rEaerDQ4pQyBTJ2XH3WRZ3cJEDyviZRXavcRPK9xvdJWHIRL52uM12j4YP/4eBnJr9Tf7svg" +
                "wKBUmbUc3ke7sMlta60yXQ5zN6GiRuWjMQPnZeLEZCtCtN3k5Z8gxP3UTE6xEn5yAJWNO3zi+PTisk3q" +
                "lfqeCe6XURmheoCSymA1ImJECbpRAVHpCQrwcPAiY70N7hF7nmhb0japdJZDfEQOzlpNncnGflHYGeMR" +
                "WohQwAe7LFZgJhQqijHVgli0JTODejQiNYZFlZlXySOWsqNDRkmh7kYl+YrMTfJwTYZKZ+Mc2ILrcSul" +
                "sHeYjLDQEUMXRldr9IT9piT/gZTGk4IAccxkClsVBXYTTS/Gmxkc3ZCOrgeXNI5SCnPUhgqBf2SXAC+Q" +
                "isHeYtUKQ2OygU6vyRuxQ/Ar4KT3emTENH5q0nyYpzEYmAPfC9QJ68kCMDWpOSiQ53Ks6kXjEQh5INNZ" +
                "BuJXbonExYZ3APQkuWtDWPrJZLJ13znoyjVPr2Sjf2Q51rKVbHzJj3rb3z/sn6sv2iw/yWqbFBuJ6C7U" +
                "XiB0feryAbvb1FZIFzncxqfoNCQpj2qnSUDEHaIAyWQYpA+OTokLUL9gp6YIpK33ttv7XyO15AvVdNHf" +
                "Pz94+1VqCrGX2m2bCuYEhptoRMMc4VHNjBHRlu7VMD0srEY/BDHQWTWbHzt8bmr1r/O4oErxOGlqoBlo" +
                "xU+5IUM2I01O0aHJ2646Ob0Mz1Axr9LC1lnsTW/67x8X6fQ1gUp1dPLmVH2VVAe2pLyNNrxEJodzUFUA" +
                "WA7IhmOJIEKQjo1DsUI9fo3nYjoaAhAUxgsgVdJBbgqoZYIZQBWKOp4xomhDUmn3TTrmTI1m0vJiWknv" +
                "J4ROb6xHVbZYiWVcKAQB0dpoI2aZGTGrPP5+kr6kbbQXdDhBZOTxe2oM+OAJ3i9srbTArDzkCKHI+iG2" +
                "0aDjUKdsaZY1Nwbat7zsWaNMOEQAI1jeEAWMMRXQUgxAtWTlj/tJ9JKD458vLvvnF1/qKUkwL89CWA+S" +
                "HCsHFMFM72Ti5kEvA1NYQksSFKKlLs1s2CYUAz5ayLocAItSsym9dR5HbYoyCKRw9dNypvjSWH9ELkbD" +
                "6JZ7thL5IOY8I0YOKNwofbWiz6/I0W0EAeZaRHQCv4bvlxgljfBh/tSP9RSxjPOAj0rI2ZpaBXmWMZ/w" +
                "auHinfHjhtQVzhuvOZ1PZomeo6Uupwg6byNfHZrj8VBsBNjPZcyDFXr7GwGcEYoWcBfi19auS5FCvM9D" +
                "FBQaS1h4wCdEE+IP055rma6xGXKHFeR7uq4sBT4NihbJyBCPbtFSJtTYSBLO+8Kyps5OL/p/QtaKJpDq" +
                "xWCRHY/8YjGw2YJKN9fyvaWNYC/p1zl7k1JLgZa0grUvaBKwERsp1J3Ocl0+hT4Meyb1zr7WxS0lefMv" +
                "EDqwPHdDrF6EcSWd+9dgTkpnX1NYDh++rNyqIqR+MlQ1w2TFwAZYgRmvFv3DceHylF8F1yMwMjOkrlKX" +
                "ODEk+eD+12axrgaIN8h+9AOEClGpaAIirTn7RDaIHQOoRILZoM0Kd2A0AIYEP16cnjyl4USYCv+y/+44" +
                "NJU96odDkUAHFAsXMNg1JTFMZJvRDpeEBlBQy7nBo9WB9qan+r1RjxPobaPjsYDVwtprxPg1ataT/3ZI" +
                "w529zoGt0/Hh605XdZy1FZ6Mq2q69/RpYRHu0HbV+d8TEZFKG7FHKQVqINpIsWK90ECTcVpaoFKWVx1s" +
                "ygEskVeujQlz8mFh5vkgL/JqIYN+s85fIbARJfINAJq7w9fiGw0ERwrMVjAFOZfMTxHjiLLC8BT+DRgM" +
                "wtJXxWT2VKMAfkYqwLObKtj77vuXL2RFaoFRuDXGutscd8JJFz8dAxgAjYwt+vFop5WDLz4Ub+MKoc1H" +
                "qc5s5Hd25ckUM8k99d2Lnef8ldANLUC+trOwAj3yDFcbNx6XMAEJEg+IbaG8ndisLug92MLEzE470aHh" +
                "2g91K7S+At/szYhZsvdSzxSmJ9sZimLpxeUESXTDUGNCjSaXOerEdJaxIWh+1MosGF6NyZFKelJgL13f" +
                "gF61QO2uUs7roCL1MN5YtIN4AAziMHYu7IACyqN+otBWTRsZB7OBFXhfg2CeiF8+EbTSiwlDzhLYzydC" +
                "EKrC1o209Fqo+Zv5BFrZpqjdogr+jPDFZg0x0BiYbKunzpZkfGsvmCbIQLt9pAwIk9Ups0pswkWchgBT" +
                "Gp4IUGr0hNmNM0Cecr+1bYfbw4LglXBP0xsIJ5uEeZ1+qHNB/90lhL1x6UZVY5MyBcMkMePW7W5HPT+k" +
                "/FinFcHqoMWWunrqSJ6J8mhuFRXSBRWGgTnlAQHTszyDhAEMFKYc4dsaovGGTwjEb7yZeDps5gV8Mm7H" +
                "SoOZVxA1d9EhQqUI/sK6IafpJWyoN+QKgEshVSUDawsaV+b+apAjxxGcwADzyLfuzpoX/4hMBdAKI015" +
                "3nncCKWDSOBisKiMb3Y4O4vrb+7Aq9X1L8Egn7ycqO7DFjHy+F0XNz6fuCBsRtLfiOa2glQQCfUTabkh" +
                "cumoqLLmKa3jL4pJvFsQb3i07MOGWJ98yHwS1tOYfwgFi+wIbJhVjIztrUzWUJGsI/70Elj28mW45wtP" +
                "fg6PcEG3XPNsV+6/WmvoEa6qlmvIjnQ71FpDj3DdEp68OT7dp0e432g/2X1BlwVJzPFUG6JJTugzBGR/" +
                "jM5ih0MaePGCU/k8dHYilweMuVgVEqLhIHYKrszYdBg/m7KmJCNZwRsYfYCZfDwnBfDiY7DlLZxwokvg" +
                "8sJMOHvGbpA5eyi3WO3HOMyBGFtDjNiYFTk1Q/Bdwu8jYIq/h64gM3Pcphc0nHBmKA17nIOwEOg40DoZ" +
                "//7XhM64DAQQYw0tOiBcE1GUxR0CchgL1gSbjHCzvgGLmx5JVVGMNSqLYgH3NUyJxd/vCJ9mfgXFPSS3" +
                "a3QUgz0gBb9mYtXkziFuN3BfoDFU4TkLImjefFo0nz49Fvt3tI9RpPg/TngmgnEXyj7dvXBTyw2sFt/k" +
                "+e6toswlLP4vluRGNb599oP2rJ8TPErsDEnMeULmHqtWE6nrMlw6UY/BkgTeN9S5nW1P9G+AIg0lAYoB" +
                "GOzOd6GoRmSZicc+BXdgcXmr2aERJNrVfG6ybT1v88hLeeAH+oRTuuJ7rUbJcW+wOcf4pqs+dVGTq3ZL" +
                "/G9FFG89/mX94//w463opu93dmlK36jw0UwHifbX6Pe2ubp8d4/HWXgvMUmO2VJ2TwmGahYkP9XwYlcy" +
                "3eW6xxFwefY6n1xh6IZv4tuHJeOEFuCdv59m4qdZkvwfxvjo1QIoAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

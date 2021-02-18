/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectRecognitionResult")]
    public sealed class ObjectRecognitionResult : IDeserializable<ObjectRecognitionResult>, IResult<ObjectRecognitionActionResult>
    {
        // Send the found objects, see the msg files for docs
        [DataMember (Name = "recognized_objects")] public ObjectRecognitionMsgs.RecognizedObjectArray RecognizedObjects { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectRecognitionResult()
        {
            RecognizedObjects = new ObjectRecognitionMsgs.RecognizedObjectArray();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectRecognitionResult(ObjectRecognitionMsgs.RecognizedObjectArray RecognizedObjects)
        {
            this.RecognizedObjects = RecognizedObjects;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectRecognitionResult(ref Buffer b)
        {
            RecognizedObjects = new ObjectRecognitionMsgs.RecognizedObjectArray(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionResult(ref b);
        }
        
        ObjectRecognitionResult IDeserializable<ObjectRecognitionResult>.RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            RecognizedObjects.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (RecognizedObjects is null) throw new System.NullReferenceException(nameof(RecognizedObjects));
            RecognizedObjects.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += RecognizedObjects.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "868e41288f9f8636e2b6c51f1af6aa9c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VZbW8bNxL+voD/AxF/kF3I8iVOfDkfjINjO5cUSZxGLu56QWBwdymJ8YpUSK4l5dD/" +
                "3meGy9XaVu9St3aFBNZyh8N5eeaNymz+WRXhwqnCjo0O2pqLqR/73Q9x4asqz5jiyDm5FK5dvYgbfXb4" +
                "B3+yt8N/HojfJFa2eZePeHV6dHL6Qdxpc/xsZK+ULJUTE/6zkW1km+J8or2YKu/lWImwnClRKl84nSsv" +
                "pJjZoEzQshK+UEaJwpqRHtdOko4HIPAqCDtqDOBFmMggCmmEWswqqQ0WVNy6kX2rkT5+SuxYwjsZa3h6" +
                "9OH41e8zVrQO6bxji6J2TplCiakMTi9ErsJcqajfCmYrwUeVlWHvCXQp7Gr3RnZPAPShjMaMHobkwyBN" +
                "KV0J3wZZyiDFyMLzejxRbqdSVwouDXI6g9D8llzvBwkQ+DeG05ysqqWoPYiChSbTaW10IQOQooGZ7n7s" +
                "hLuBGOmCLupKOtLcldoQ+cjJqSLu+OfVl5pN+frkgADlVVEHDYGW4FA4Jb02Y7wUWa0NbEgbss3zud3B" +
                "oxoDv+3hEW8QFnBzwDCEkf4AZ3wXlRuAN4wD/5jSiy1eu8Cj3xY4BCKomS0mYguSv1+GiY3+vJJOy7xS" +
                "xLiABcC1R5t62x3OhlkbaWxiHzmuzvgWtqblSzrtTOCzirT39RgGBOHM2StdgjRfMpOi0ghIUencSbfM" +
                "aFc8Mtt8STYGEXaxR/BXem8LDQeUYq7DJPPALrizNy50eV9o/NZI/xMzYQrumAqjRQBGhKmfsTeBd7L3" +
                "DO6Nb/vi3dl5swYgXhSVrcsG2Osy62/X6+zF96fH5+L1u5dnd1cuqnZsTUD6RSAbhP2U07WQua0Da8B5" +
                "HlhLKrKPKI9TlqhNymPEaWhhGLwBjMkSWlUwzhRpJAhjA/Ig1hjLuSokMgWzjBlDFROjEevAo2VioqT3" +
                "U4FUdIPegQiUIKNTHeUIz8JOV85isVkWdUPOX0NcxNk57aPNzIprWEkZ6EBM7Byx5pRY2lpI/NWcTjit" +
                "RJ5sJhIekY5znbCG/L0pXjNlqgN/YbrHrVGBDtIJKAJ9y1URfSCANPVBrIS5C2gSZI7f/Dg8P/0wvCts" +
                "oonJ1Zxa2Rqxigcni4iZvTICv7FOriqLVNKESbRVXzQVn6PCJ1dZp8coAyg4ynjrPJ21FU2CMyS3ajIe" +
                "GoE1kVfIaqhRbrVpeyOL36Jj35MoxxSCVGM7Eelv6NJvlbEGFabJpwB6TgCibJjbxa6fyBkiHEca0mGv" +
                "3OhUwkapVSqAKEQfJXmr/KRldoETJ2sl4NNZsSclOM4Qid4m2XoBiOdaO0Yh5L7Lq7jvc40wGKPLkoSV" +
                "YGvXp+gh+RcpLioJGrYB0jxCDEGJKnIZqzb7QztQEBJlHSylAypAy41srEhKt+wYFeZstWlOvHMjJt6f" +
                "DU//kISWfBH7LV9XIcKQQLLMbbmkFoBb0IOVs+A4T+krZncyreE6yBTsgwHbD00NNlL4O1lqaXZhExVx" +
                "ClzWvpbVbVN59S+wOrZc1hG/w6YbopPvrc/7/3nu9xSek4coO7eqDPmAvBXmlsYJThvoImV0AiAM9FPi" +
                "rbmYIEZKNdIEZUNHpgqQYuFSLdeViIiKyEJQwg0atcyl2I7YKPON1CKBzYpnmXcF8sE6bjShx/fDs3e7" +
                "6ItT9/nT0ds3IrIYiCOTaogu2+qGAeJScVJTjMVkm1Xvgb10MPVwufRqIE4H4wGn1tve71NpoeRWWXuJ" +
                "oL9ETXv03x4ZunfQO7Z1MTl50euLnrM2YGUSwuxgd7eyiH8YPfR+ftQo6bgvNdTlmysyj6WMHb3Io0Ns" +
                "ADp2oFKnQw+bNPpeZJpLpZqOfFSphc51pcNykIy4BrrQWUU78rChC3HyIoKknSORFstrvQfDjMcpBP8C" +
                "IVcpf0CLLyFjozA/C+Z0IForxEUyBBZvGuLg2d+eP21ICouGpogTbu+22L102vCHN2gg0LtMbFW2/rp+" +
                "+PBL9SqRNOz5ONGbj/3efrM0sw5Lz57uPYnP1A8RCZK5nScaNPNzDFQ31w38QQqlU9JVR/N6asu6IoJA" +
                "s0ews16LcYL7fc2j60v1zfsGktdzZUsWp9h9t1OibhofIRibjr6YTzRGtSndn3AlpNsFWZa6QWo343g4" +
                "nHBlaKXyFDuB+IUlSnwoOOmDS6yYaVbqhnWOdsUtRV7ZnCLMo8IuKVDT1UgaxxpRlAlts/MowvRR7GsG" +
                "WYP+eFacGfhEKEJ12rqxjFcHaAu29BRW2aEw3qYi/7ikTqmGGhgqVLk9EO9XbHxnL4SmroJ2+8QZnU5Z" +
                "FywqiQmUOAkFZih/TUvV2skjnpWlE3kKtaOdUUWNWJRexwEgborCy+JLrX2TgdqW98a4T/VkizLHXpmM" +
                "7bcH1yclOuDJCWXMugjUhzdW7JhrIF6PUksN48FTrUH64MINo/bgE5vvuS6hYdMpVMqM8bSGabpbiAzS" +
                "E28mmU7aOzA+GXO5UZVPqmqXANGUjwYvbBsCzSBjR70kKKCfipDIstzaCiEJ6S5yjZRHvYbAJOE7U3v7" +
                "4h9JqKa7hZNm2Lsp3rRKyUYlSJEvg/LtDmfnif7mDry6Tv8cAvLJ6bMpjuCLFHn8ri88cEZW3Uqsv4uW" +
                "2260gkol17SWybmjSsuWpyyP/ygu2lzJSqdJ4sGyDztiffIh98WwnqX8Q31y1B2B7ZaNk7G9k8laLjHr" +
                "RDw9R6t7/pzUPxSPm5Ufm6VD8WRF83ifV/Y6NLR0KJ6uaMiPWHnWoaGlQ7HfrLx8c3ZES4fir92V/adY" +
                "eZ6lJE/lIbnknYyhzHhMYLGjEV3iMsFZ/D5ydkq3So7vdqMpYog2BzEouFBj00n6rkxNSSZmBa8UdX5X" +
                "Kp1ToBsLjSCvAMKpNGjaKzXl7JkGR5bsvmBxfWjjMEcn2bn8oOmNwr/SnlUP1NqP0WH8vRkZSrUQQDDd" +
                "aTg1igN+uj9hJTCOYLhS/uOnjM44bxggxlpedABxkzHK0o7Y83B3WM+YgKVZP6GlTQ9kqqTGGpMltdAH" +
                "tkJFj3/ci3KqxQUMd5/SrrFRCvYiTSS3b7ra3DlywKqfyULFaxlE0KL9tmy/fX0o8X9lskwqpbtuvj1R" +
                "HiWbL3h54uXpVkZs8h37raLMJSzdn2c3qvHts5ntn6V40tgp0pjzhGwn+5XXotY1NjjydaCu6z0PKyz7" +
                "pvhg5ztT+RmtSMtJJhwQLvYX+zBUq3L8iSeNLU635J3hh24uMcbqhSp35KIrI5PSTx2vwZ/6lH7EXmdw" +
                "cop+o9ha9AUa0q991OTQHZX/LYjjreWf1i//h5e3E0w/7u1/6ijzcK6DRkdr7HvbXX36IYmWy+Z9jEkC" +
                "ZsfYAxF7qJYg+6EGip1hviu6h1FwdfY6TF4T6AY28fRlJTh1C0Dn/04z6ds8y34BzaEBsvEeAAA=";
                
    }
}

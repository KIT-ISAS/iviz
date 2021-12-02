/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class RecognizedObjectArray : IDeserializable<RecognizedObjectArray>, IMessage
    {
        //#################################################### HEADER ###########################################################
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // This message type describes a potential scene configuration: a set of objects that can explain the scene
        [DataMember (Name = "objects")] public ObjectRecognitionMsgs.RecognizedObject[] Objects;
        //#################################################### SEARCH ###########################################################
        // The co-occurrence matrix between the recognized objects
        [DataMember (Name = "cooccurrence")] public float[] Cooccurrence;
    
        /// Constructor for empty message.
        public RecognizedObjectArray()
        {
            Objects = System.Array.Empty<ObjectRecognitionMsgs.RecognizedObject>();
            Cooccurrence = System.Array.Empty<float>();
        }
        
        /// Explicit constructor.
        public RecognizedObjectArray(in StdMsgs.Header Header, ObjectRecognitionMsgs.RecognizedObject[] Objects, float[] Cooccurrence)
        {
            this.Header = Header;
            this.Objects = Objects;
            this.Cooccurrence = Cooccurrence;
        }
        
        /// Constructor with buffer.
        internal RecognizedObjectArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Objects = b.DeserializeArray<ObjectRecognitionMsgs.RecognizedObject>();
            for (int i = 0; i < Objects.Length; i++)
            {
                Objects[i] = new ObjectRecognitionMsgs.RecognizedObject(ref b);
            }
            Cooccurrence = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new RecognizedObjectArray(ref b);
        
        RecognizedObjectArray IDeserializable<RecognizedObjectArray>.RosDeserialize(ref Buffer b) => new RecognizedObjectArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Objects);
            b.SerializeStructArray(Cooccurrence);
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
                size += BuiltIns.GetArraySize(Objects);
                size += 4 * Cooccurrence.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/RecognizedObjectArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "bad6b1546b9ebcabb49fb3b858d78964";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVZbVMbyRH+vr9iynwQXAkRG5s4SlEpDPjsK9v4DFfJxXVFjXZH0pjVjjwziySn8t/z" +
                "dPfOagFdcmcfRAXFaranp1+efhu2tr7io16dHp2cflBfs7f5ZK+MLoxXU/6TZVvqYmqDmpkQ9MSouJob" +
                "VZiQezsyQWk1d9FU0epShdxURuWuGttJ7XW0rhqCIJio3Fi50SeTx6DiVEeV60qZ5bzUtsKCka2ZkFx6" +
                "k7tJZWn/5SxMwt4HWfhiijOm+PhL4gbxvspM56dHH45ffZOZ2DCk7q7L89p7U+VGzXT0dqlGJi6MEdV8" +
                "K3wr9Lh0Ou4/gRq5W2/OssM/+JO9Pf9+qEIsxIziWMh9HnVVaF/AqVEXOmo1dnC4nUyN3y3NtYEvo57N" +
                "ITK/JZ+HQUICfibwltdluVJ1AFF0UGQ2qyub6wiIWIClux874WdARfto87rUnhT3ha2IfOz1zBB3/ATz" +
                "uWZDvj4ZEpKCyetoIdAKHHJvdLDVBC9VVtsKJqQN2dbFwu3iq5kAtu3hAjQIC5x5gBfC6DDEGd+JcgPw" +
                "hnHgnaoIapvXLvE17CgcAhHM3OVTtQ3J36/i1Ik3r7W3elQaYpzDAuDao029nQ5nEnuoKl25xF44rs/4" +
                "LWyrli/ptDuFz0rSPtQTGBCEc++ubQHS0YqZ5KVFJKrSjrz2q4x2yZHZ1kuyMYiwiz2CvzoEl1s4oFAL" +
                "G6dZAHLBnb1xaYv7QuNvjfHs/5b/msCW/Cf2ABQRo2HOvgTaydpzOFfe9tW7s4tmDTC8zEtXFwnWt7Pp" +
                "71fp7MUPp8cX6vW7l2fqm7Q6dlVEwkUEV4h3pCoYX+mRqyMLz5kdIEvasXMoc1N6qLEurqP84WASvAB8" +
                "yQbWlDDLDOkjqspFZD+sMYZHJtfIEMxRMoXJp5VFjAOHjomJkt7PFFLQLXoPIlCCDId6Sg2BRZ0lH7HI" +
                "LIi5KeOvoUywdUHbaC/4cLkqKOcM1dQtEF3eqJWrlcZfywmEE4lwZPuQ2IhtHOqVqzh1vWbClPb/xGSP" +
                "W2MCEKQMgAPylqkh+ghQNOVArUX5/ThJKDl+89P5xemH869FSta4l9Mo20FKdfQa2pPQ+4XAvLHLyJQO" +
                "aaMJCrFSn9I9+4RiICQPOW8nSPkoLqYKzgcctS3GwBHae71CUuIzBUtTfY0EhnLk13t2MnkQd74nQY4p" +
                "3KiYdqIv3NCj3yriKlSSJm8C18B+hSo0wcNyL0z1HLGM8yqSf7/oFLxGn3XMZ0wtUrw1YdqyusR50w2n" +
                "88ms0ZMC7OYIuuCSXL0IeHM9naDYcVMVIAq9/VQD8xO0UJrwEV3t+xQpJPuyiYJSg4SVRyZHNCH+UCiu" +
                "pDCzG6wHBWFP19FR4FONWWUTQzL6VceYMGOrSXPeVzZZ6v3Z+ekfkLWSC6SXCnUZBXiEi9XIFSsq8NxZ" +
                "Dtc+gr8CJSnJ3mTUiqscU7D1B2w6tCzYSKHudWF1tQd7GEYmkFiHWpd3jBTM38Ho2HHJRqyeN50OnfvQ" +
                "RXOdzr6lsJzcf1m5U0XI/OSouHA0IHCOQHuoxf4ALiBP+RUOQtpHYBRmbAnAFU5sknwD/yuz2lQDBA2y" +
                "X1FajRaVyqdQFkwUo9T3gEtiWIy6ooToPPeO0OCH87N3e2h1U0P589HbN0oYDNRRlYqELdrChYngipIY" +
                "mjkGYLLKuqHAXpxLXdlIBzNQp4PJgBPoXadjWUan0rkrxPgVatajf/XIwr1h79jV+fTkRa+vet65iJVp" +
                "jPPh3l7pEO6wduz9+5GoSKWNxKOUAjMQb6RY8R7PAlLZO1agUmZjD5ssGlnklStjmhZ7XJqlHdnSxpXM" +
                "CGYTXqGwESPy8GBzdfJCsNEOhEiBxY2egsDFwxGifYkoKw038C8hYKMsfVXMZqhaA/AamQBrt00wfPaX" +
                "50+FInfoUXKZUnt3Je41J53/+AaNAbqRqSuL1k83Dj7/XL5KFMKbj1K9xSTsH8jK3HmsPHu6/4S/UndD" +
                "BMjXbtFQoCNfYCq6tVzBBaRIOuCymSLl7cwVdUnvIVZpopv3EqAB7fsaKDdX4Ns3BSQs+XttZwrTd7sF" +
                "imIVBHLSSfTVYmoxa830Ckyo7NC9gC4KdgTIupklwMkEpIpWSuylyQ/84gq1O+ac18FF6mEadrpBPEIP" +
                "4ldqVLoRBVRA/UShje2lRpqnGlGAvraDeSS4fCTdyiAlDDlL2n4+EYpQFXZ+omXyR83ftjNYZZeidocq" +
                "+GPqL7ZrqIHBwBQ7A/V+zSZ09kJoahlod0ic0cIUdc6ikpiAiNdQYI4K1zRKrZ0C4teg85TReNeNd8cl" +
                "tVciPc2IUE42ifA6/1xb6f776xb21rxOVWObMgW3SeLGnbvTjnpyQvmxziO11Y0VO+YaqNeyJsaDp1qD" +
                "9MGF20BLeUCa6YUtoGHTDJSmmuDbBqbpckAYpG+8mWQ6aW+v+GQM1pUpJcVhwfoEiKZSNHhh2xBoBhk7" +
                "6iVBAe1Sk6qykXMlAhLSXY4schy1EwqTQeiM3e2LvyWhmqYVTppj75Z60yqlG5UgxWgVTWh3eLdI9Ld3" +
                "4NVN+ucQkE9Ony11BF+kyON3fdxOfOGCsJ1YfyeW22m0gkqon0jLLZMLT0WVLU9pHb8oJra61iVqn6Dh" +
                "wbIPO2Jz8iH3SVjPU/6hLlh0R2DDreJkbO9kspaLZB3B03P0shfPSf1D9bhZ+alZOlRP1jSPD3hlv0ND" +
                "S4fq6ZqG/IiVZx0aWjpUB83KyzdnR7R0qP7cXTl4ipXnWcrxVBuSS97RMxRkPCawuPGYrl+Z4Eyex97N" +
                "6FrI862smEJCtDmIQcGVGZtO0rOpakoykhWCgdNH7tqkc3I0XnwMtrwCCGe6Ql9emhlnzzQNsmT3BYub" +
                "8xiHOTrGziVGGsxKS8MQsEv9+wQ9xV+bqaAwS1zElXQ54c1YBvZ0D8JKYOLA6GTCx18yOuOiYYAYa3nR" +
                "AcRNS5SlHdLkcC9YU9tkRJrNA1ja9ECmSmpsMFlSC31fK5R4/OO+yGmWlzDcfUq7wUYp2JtOIWy4sWpz" +
                "59gDq2GucanC9yyIoGX7tGqfvjyU+L8yPiaV0mU134ngugtln25oeajlAVYLNvmS/E5R5hKWLsCzW9X4" +
                "7tn3OrP+L8WTxt6Qxpwn5N7jptdE6xobPPmaZgzWpJF9S31wi92Z/oRWpOUkjWLTGBwsD2CoVmX5D02a" +
                "U7xtyTvDDl1BYly1S1Ps6mVXRiblCz/wpz6lL9jrDEqeZ4PtJa5v+upLHzU5dkfifyjieGf5583L/+Tl" +
                "nQTTj/sH9D+j1oQP5jpodLTBvnfd1af/BNFy0byXmCRgdow9UNJDtQTZjzVQ7Cvmu6Z7GAXXZ2/C5A2B" +
                "bmET3z6vBaduAej872kmPS2y7D+gXJOm4h0AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

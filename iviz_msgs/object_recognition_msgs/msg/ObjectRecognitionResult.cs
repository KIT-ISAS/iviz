/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectRecognitionResult : IDeserializable<ObjectRecognitionResult>, IResult<ObjectRecognitionActionResult>
    {
        // Send the found objects, see the msg files for docs
        [DataMember (Name = "recognized_objects")] public ObjectRecognitionMsgs.RecognizedObjectArray RecognizedObjects;
    
        /// Constructor for empty message.
        public ObjectRecognitionResult()
        {
            RecognizedObjects = new ObjectRecognitionMsgs.RecognizedObjectArray();
        }
        
        /// Explicit constructor.
        public ObjectRecognitionResult(ObjectRecognitionMsgs.RecognizedObjectArray RecognizedObjects)
        {
            this.RecognizedObjects = RecognizedObjects;
        }
        
        /// Constructor with buffer.
        internal ObjectRecognitionResult(ref Buffer b)
        {
            RecognizedObjects = new ObjectRecognitionMsgs.RecognizedObjectArray(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ObjectRecognitionResult(ref b);
        
        ObjectRecognitionResult IDeserializable<ObjectRecognitionResult>.RosDeserialize(ref Buffer b) => new ObjectRecognitionResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            RecognizedObjects.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (RecognizedObjects is null) throw new System.NullReferenceException(nameof(RecognizedObjects));
            RecognizedObjects.RosValidate();
        }
    
        public int RosMessageLength => 0 + RecognizedObjects.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "868e41288f9f8636e2b6c51f1af6aa9c";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVZbW8buRH+vr+CiD/IPshyEydu6sIoHL9cckjiXOxDew0OBrVLSYxXS4XkWlKK/vc+" +
                "M7NcrW1dm5ezK9jwijsczsszb3Tmhh9NHi+9yd24stG66nIaxmHnvSx8NsUZUxx6r5fKt6uXsjFkB3/w" +
                "J3tz/uO++iqxso1v+aiXJ4fHJ+/VN22WT/bS6MJ4NeE/WbahLiY2qKkJQY+NisuZUYUJubdDE5RWMxdN" +
                "Fa0uVchNZVTuqpEd116TgvsgCCYqN2q0DypOdFS5rpRZzEptKywY2Zp9qYE+/Ja4Zd9opvOTw/dHL7/L" +
                "TGwYUnfb5Xntvalyo6Y6ertQQxPnxohqK3i1Qo9Kp+PuE6iRu9Xm7J5gF2IhZhTHQu7zqKtC+wJOjbrQ" +
                "UauRg8PteGL8dmmuDXwZ9XQGkfkt+TwMEhLwM4a3vC7LpaoDiKKDItNpXdlcR0DEAizd/dgJPwMq2keb" +
                "16X2pLgvbEXkI6+nhrjjJ5hPNRvy1fE+ISmYvI4WAi3BIfdGB1uN8VJlta1gQtqQbVzM3Ta+mjFg2x4u" +
                "QIOwwJkHeCGMDvs44wdRbgDeMA68UxVBbfLaJb6GLYVDIIKZuXyiNiH5u2WcOPHmtfZWD0tDjHNYAFx7" +
                "tKm31eFMYu+rSlcusReOqzO+hG3V8iWdtifwWUnah3oMA4Jw5t21LUA6XDKTvLSIRFXaodd+mdEuOTLb" +
                "OCUbgwi72CP4q0NwuYUDCjW3cZIFIBfc2RuXtrgvNH5pjP//8l8T2JL/xB6AImI0zNiXQDtZewbnytu+" +
                "ent20awBhpd56eoiwfp2Nv16lc5e/HRydKFevT09U9+l1ZGrIhIuIrhCvCNVwfhKD10dWXjO7ABZ0o6d" +
                "Q5mb0kONdXEd5Q8Hk+AF4Es2sKaEWaZIH1FVLiL7YY0xPDS5RoZgjpIpTD6pLGIcOHRMTJT0fqqQgm7R" +
                "exCBEmQ41FNqCCzqNPmIRWZBzE0Zfw9lgq0L2kZ7wYfLVUE5Z19N3BzR5Y1aulpp/LWcQDiRCEe2D4mN" +
                "2MahXrmKU9crJkxp/09M9rg1JgBBygA4IG+ZGqKPAEVTDtRKlK/HSULJ0etfzi9O3p9/K1Kyxr2cRtkO" +
                "Uqqj19CehN4tBOaNXYamdEgbTVCIlfqU7tknFAMhech5O0bKR3ExVXA+4KhNMQaO0NyLaTlTsDTR10hg" +
                "KEd+tWcrkwdx5zsS5IjCjYppJ/rCDT36rSKuQiVp8iZwDexXqEJjPCx2wkTPEMs4ryL5d4tOwWv0WcV8" +
                "xtQixRsTJi2rS5w3WXM6n8waPSnAboagCy7J1YuAN9fTMYodN1UBotDbjzUwP0YLpQkf0dW+T5FCsi+a" +
                "KCg1SFh5ZHJEE+IPheJKCjO7wXpQEPZ0HR0FPtWYZTY2JKNfdowJM7aaNOd9Y5Ol3p2dn/wBWSu5QHqp" +
                "UJdRgEe4WA5dsaQCz53l/spH8FegJCXZm4xacZVjCrb+gE2HlgUbKdS9LqyudmAPw8gEEutQ6/KOkYL5" +
                "OxgdOS7ZiNXzptOhcx+6aK7S2fcUluP7Lyt3qgiZnxwV544GBM4RaA+12B/ABeQpv8JBSPsIjMKMLAG4" +
                "wolNkm/gf2WW62qAoEH2K0qr0aJS+RTKgolimPoecEkMi2FXlBCd594RGvx0fvZ2B61uaih/PXzzWgmD" +
                "gTqsUpGwRVu4MBFcURJDM8cATFZZNRTYi3OpKxvqYAbqZDAecAK963Qsy+hUOneFGL9CzXr0rx5ZuLff" +
                "O3J1Pjl+0eurnncuYmUS42x/Z6d0CHdYO/b+/UhUpNJG4lFKgRmIN1KseI9nAansHStQKbOxh00WjSzy" +
                "ypUxTYs9Ks3CDm1p41JmBLMOr1DYiBF5eLC5On4h2GgHQqTA4kZPQeDi4QjRvkCUlYYb+FMI2ChLXxWz" +
                "2VetAXiNTIC12ybYf/aX50+FInfoUXKZUnt3Je41J53//BqNAbqRiSuL1k83Dj7/VL5MFMKbj1K9+Tjs" +
                "7snKzHmsPHu6+4S/UndDBMjXbt5QoCOfYyq6tVzBBaRIOiBdUsjbqSvqkt5DrNJEN+slQAPa9zVQrq/A" +
                "t28KSFjy98rOFKZvtwsUxSoI5KST6Kv5xGLWmtK1B5c5uhfQRcGOAFk3swQ4mYBU0UqJvTT5gV9conbH" +
                "nPM6uEg9TMNON4iH6EH8Ug1LN6SACqifKLSxvdRI81QjCtDXdjCPBJePpFsZpIQhZ0nbzydCEarCzo+1" +
                "TP6o+Zt2CqtsU9RuUQV/TP3FZg01MBiYYmug3q3YhM5eCE0tA+0OiTNamKLOWVQSExDxGgrMUOGaRqm1" +
                "U0D8GnSeMhpvu9H2qKT2SqSnGRHKySYRXuefaivdf3/Vwt6a16lqbFKm4DZJ3Lh1d9pRT44pP9Z5pLa6" +
                "sWLHXAP1StbEePBUa5A+uHAbaCkPSDM9twU0bJqB0lRjfFvDNF0OCIP0jTeTTMft7RWfjMG6MqWkOCxY" +
                "nwDRVIoGL2wbAs0gY0edEhTQLjWpKhs6VyIgId3l0CLHUTuhMBmEztjdvvhbEqppWuGkGfZuqNetUrpR" +
                "CVIMl9GEdod380R/ewde3aR/DgH55PTZUIfwRYo8ftfH7cRnLgibifUPYrmtRiuohPqJtNwyufBUVNny" +
                "lNbxi2Jiq2tdovYJGh4s+7Aj1icfcp+E9SzlH+qCRXcENtwqTsb2TiZruUjWETw9Ry978ZzUP1CPm5Vf" +
                "mqUD9WRF83iPV3Y7NLR0oJ6uaMiPWHnWoaGlA7XXrJy+PjukpQP15+7K3lOsPM9SjqfakFzylp6hIOMx" +
                "gcWNRnT9ygRn8jzybkrXQp5vZcUUEqLNQQwKrszYdJyeTVVTkpGsEAycPnTXJp2To/HiY7DlJUA41RX6" +
                "8tJMOXumaZAluy9Y3JzHOMzRMXYuMdJgVloahoBd6t/H6Cn+2kwFhVngIq6kywlvRjKwp3sQVgITB0Yn" +
                "Ez78ltEZFw0DxFjLiw4gblqiLO2QJod7wZraJiPSrB/A0qYHMlVSY43Jklro+1qhxOMfdkVOs7iE4e5T" +
                "2jU2SsHedAphzY1VmztHHlgNM41LFb5nQQQt2qdl+/T5ocT/nfExqZQuq/lOBNddKPt0Q8tDLQ+wWrDJ" +
                "l+R3ijKXsHQBnt2qxnfPvteZ9X8pnjT2hjTmPCH3Hje9JlrX2ODJ1zRjsCaN7BvqvZtvT/VHtCItJ2kU" +
                "m8Zgb7EHQ7Uqy39o0pzibUveGXboChLjql2YYlsvujIyKV/4gT/1KX3BXmdQ8jwbbC5wfdNXn/uoybE7" +
                "Ev9DEcc7y7+uX/4nL28lmH7Y3aP/GbUmfDDXQaPDNfa9664+/SeIlovmvcQkAbNj7IGSHqolyH6ugWJf" +
                "Md8V3cMouDp7HSZvCHQLm/j2aSU4dQtA539PM+lpnmX/AW0wbj2oHgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

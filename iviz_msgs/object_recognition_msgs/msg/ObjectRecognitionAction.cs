/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [DataContract]
    public sealed class ObjectRecognitionAction : IDeserializable<ObjectRecognitionAction>,
		IAction<ObjectRecognitionActionGoal, ObjectRecognitionActionFeedback, ObjectRecognitionActionResult>
    {
        [DataMember (Name = "action_goal")] public ObjectRecognitionActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public ObjectRecognitionActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public ObjectRecognitionActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public ObjectRecognitionAction()
        {
            ActionGoal = new ObjectRecognitionActionGoal();
            ActionResult = new ObjectRecognitionActionResult();
            ActionFeedback = new ObjectRecognitionActionFeedback();
        }
        
        /// Explicit constructor.
        public ObjectRecognitionAction(ObjectRecognitionActionGoal ActionGoal, ObjectRecognitionActionResult ActionResult, ObjectRecognitionActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        public ObjectRecognitionAction(ref ReadBuffer b)
        {
            ActionGoal = new ObjectRecognitionActionGoal(ref b);
            ActionResult = new ObjectRecognitionActionResult(ref b);
            ActionFeedback = new ObjectRecognitionActionFeedback(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ObjectRecognitionAction(ref b);
        
        public ObjectRecognitionAction RosDeserialize(ref ReadBuffer b) => new ObjectRecognitionAction(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) BuiltIns.ThrowNullReference();
            ActionGoal.RosValidate();
            if (ActionResult is null) BuiltIns.ThrowNullReference();
            ActionResult.RosValidate();
            if (ActionFeedback is null) BuiltIns.ThrowNullReference();
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "object_recognition_msgs/ObjectRecognitionAction";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "7d8979a0cf97e5078553ee3efee047d2";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE81abVMbRxL+fPoVU+aDICXk2DjE4Yq6wiDHpDA4QO4u53JRo92RNGG1I8/MIimp++/3" +
                "dM/MaiVETJyAj7JBuzvT0+/9dK/O+r+ozJ+rzAxL7bUpDzL6/b2RhZD88WqIz62z9evOlasKn1Zavrpr" +
                "7Wul8r7MrtPqQbxu7f/FP623F9/vCcNcgKeajauxG7qnd3BHErfeKJkrK0b8pxX4LHQ/bKQVx0eC1HGl" +
                "89tSss5YWQ8jkPN5YCRw2doQF16WubS5GCsvc+mlGBhwr4cjZbcLdaMKbJLjicoFP/XziXJdbLwcaSfw" +
                "b6hKZWVRzEXlsMgbkZnxuCp1Jr0SXo/V0n7s1KWQYiKt11lVSIv1xua6pOUDK8eKqOOfUx8rVWZKHB/t" +
                "YU3pVFZ5DYbmoJBZJZ0uh3goWpUu/c5z2tDauJyabVyqIWxQHy78SHpiVs0mcDDiU7o9nPFVEK4L2lAO" +
                "7FDmTmzyvStcui2BQ8CCmphsJDbB+bu5H5kSBJW4kVbLfqGIcAYNgGqbNrW3GpRLJl3K0iTygeLijPuQ" +
                "LWu6JNP2CDYrSHpXDaFALJxYc6NzLO3PmUhWaFV6Acez0s5btCsc2dp4TTrGIuxii+CvdM5kGgbIxVT7" +
                "Uct5S9TZGuSnD+SNa4ODXSsyK9zIVEWOC2MVy8WCwJbTkYZBWAgKFzGVTlhyGAchyIGO2d7sklCJLONh" +
                "MLK9gWtMR6oU2gsIqhw5LfxCjSdIQ0WB3UTTBa+ZKhxdkxZ9NSBepMiU9RKWI46a+o386zzZBOoFe3M6" +
                "pNazGNSJrMyxI2Q9xKBzcqjYCMJNVKYHOgsCRg5cN1KnAAkLwNS4ch6cCUQdVnWT/chyXzozck5s9Y0p" +
                "yBhX1ujWoDAS8fr+gxjowit7Veix9u5Lc9qsRp/O4sibvnJkc/y5nchjUYvV7GEku5Or1kqZofz4MrEa" +
                "Lt71To+OT78X6WdffI3fwXXZ30YIqLny5LVwLLhyFvJmzC9L0RRpHhxeHv+zJxo0ny3TpIRWWYvEhBze" +
                "V+Si9yL87rzXe/vusndUE36+TBjGVagMOYWlRHatw0XIARyMAh3SW4pfNeMyUg5b4nd+NvAfkclaCPka" +
                "RW1SKKIAV01UwOjmpbJjFK+CKqlXW5Hli58OD3u9owbLO8ssU8KS2UgrYttVGWlhUFEZXaeIu445eHV2" +
                "vtALHfNizTF9w6LnFWeFBe9rT8or9UnVkFc4g5Q3kLqokA7vYO+890PvsMHfvvjmNntWUezc4QGcCk3l" +
                "V92l82ke+yqTSDlMsz6sAsygtM0FFkBHlzeyQK6+Q4DoeXWk7IvdR/C82vVK4zkIF85XG6/W8OHBycki" +
                "kvfFt/dlMFaydRzeR7uwyW1rLTNdDrQdEyakOuqbWYA5UfmSEE03efkXCHE/NZNTLIVfOIBQ1x0+cXJ2" +
                "cdkktS++Y4IHNcaI4AuURA6rEREVlCBrFRCVbgDREeSQ3vr3iD1HtA1pm1Q61RB/DcIBDjkoCjNlOE8L" +
                "EQp2GYNIEXECw41GUaMtuepXwyGpMS7yaua/PJyI5bl114a49FeVh60H1gJ62fruVdj42GBjLVutjc/5" +
                "EW96B0e9c/FZm8PPCrpJzVxCn9Tiwf4us7rP6HViPHxOw3Fdhm4vRPawspIE3CM/QribQZQ+4mbyfrRb" +
                "BWNkAvS09d52AzhMlvpMNV30Ds4P3/wpNUUon5ltkwXgAiAwloiHGSLVT5UKoi3cq2Z6gXHR3dabHzt8" +
                "VrX65TwuqjJ4XGgsoRloxU24KUZiIk1O0CWHpx1xenYZ7yHtXmWFqfI0H1j13z8u0tkrQibi+PT12efL" +
                "RVIdmpLwhAOSQNocc0gQ4orlkWOJ6kyUjo1DsUJzlqpMDkODGMJTeAC4QzrQqoBaxno48rEyULdUcAe6" +
                "wDVh5KKyETd+aOgNL6aV9HxMEGdlPVK7UVylue8MZZTWJhsxy8yIWubx95P0JW2jvaDDCSInj98TI9Qg" +
                "RxhxbiohQ63WMUcEiqwfYlvi1wgMmlItWvgUaF/zsme1MuEQsaJheU1U0XqU3BSAYsHKH/eT5CWHJz9d" +
                "XPbOLz7XU1rRvDyPYj2E5OgtSjYzvZMHN4966avCUMkNQRG01BExkXIMuGQhY/WQMQpggTPW4ajNoAxC" +
                "BFz9ZDgz+NJI3iAXo+uwiz1brfAhmPMdMXJI4UbpqxF9bkmOTi0IetB5GnbAr/vkM4QY+mb21I3kBLGM" +
                "80rifydvTA6jPIuYb/HqwMVb5UY1qSucN1pzOp/MEj1HX1ZOEHTOJL7a1H7zYHII7MhlzKmw7xealwwV" +
                "4TH4hzeV7VCkEO+zGAWFxBIWHvAI0YT4K3R5HSacbAZtsYJ8T1beUODTsG7eGiri0c4byoQaa0nieZ9Z" +
                "1sS7s4veX5C1kglC9eJRBTse+cW8b/I5lW6u5XsLG8Feoenj7E1KLQN0pBWs/TCcqkpqkSnUrcy1LJ+O" +
                "CQSTZ1ID5ipZ3FKSU/8CoUPDs0/E6kUcGdO5XwZzUjr7M4Xl6OHLyq0qQuonQ/mpIUjGOWIiyVqkfzgu" +
                "XJ7yaxgTIjByNaDWRJY4MSb56P7Xar6uBgRvCPsFpVVPA0ibQjn4RN5PPQOoJIJ5v8kKD3RpCA8Jfrg4" +
                "O31KHW6czP988PYkdiZdaqpikUCTngoXMNi14uSl6vkAl4QaUFDfssHj7b50qit63WGXE+hto3eodlAa" +
                "K4y5Roxfo2Y9+a1NGm7vtQ9NlY2OXrU7om2N8bgz8n6y9/RpYRDu0LZv//dJENHyxL6k9x/lDWnGUFoO" +
                "1otdGBmnoQUqZdq3sUkDWCKvXCsV31UMCjXTfV1oP+9GDa7x14xH0aREfgujM3H0KvhGDcGRAvMlTEHO" +
                "FYZwiHFJcwV+E/IaDEZh6VIwmT1RK4DvkQpwb1UFe9989/JFWJEZYJQs9AXt2xy340kXP54AGACNjEyR" +
                "13ZaOvjiY/EmrQi0+SjRng7dzm64MzEWd755sfOcLwnd0AJNjW9cgXZ3amy+cruECUiQdEBqC8PTscmr" +
                "gp57GhN5M2knh4ZrP9SbufUVeLU3I2Ydl62kZwrT0+0cRbF0weUCkujEdyRjajS5zFEnJvNcR89sZhYH" +
                "Iwue3eBO4ShUPNHzc9Run3FeB5VQD9Nbo2YQ94FB7Fz0C9Pv8IS0kHMKy9RGpuleZEWVvkYwT4JfPglo" +
                "pZsSRjgrwH4+EYJQFTZ2KEOvhZq/qcfQyjZF7RZV8GeELzYriIHGQOVbXfFuQcY19oJpggy02yXKgDB5" +
                "lTGrxCZcxEoIMKF3MQEo1XpyiF9l6ER+H2cG24OC4FXgXgckHzYF5mX2sdIuJpwawq68+KSqsUmZgmFS" +
                "MOPW7W5HPD+i/FhlnmB11GJDXV1xPEgQGcqj12BJIR1QYRioKQ8EMD3VOSSMYKBQ5RBXa4imt6yBQLri" +
                "zcTTUT0v4JNHsixV4ZKo2iaHiJUi+gvrhpym22JDvSZX4BdDnKrCeyP8aHfV18hxBCcEOgPXeH9ZP/hH" +
                "YiqCVhhpwkOzk1ooGUXSNPz2ytU7rJmm9as78Gh5/UswSCf/LS5+fw53/iAOYI8Uffy8Ixx8jTS7mch/" +
                "FbS3FSWDWDlXsXq+d2mpsLL2KbXjPwpKGlIHj3i0DMTGWJ+AyIQhtCcpBxESDrJDG3YeDY3tjWxWUwmZ" +
                "pxsnqsenly/DJPVZvPNTvLUvni/WPNvlOzuNNXRrX7xYrCFb0muGxhq6tS92453XJ2cHdGtffNu8s/uC" +
                "ps6tlOepPiSTnMoQzuyTyWHMYEBDL15wFj4PrBmHKTTjLlZFCNN4EDsFV2dsOkqfVVlRogmZwSlFKO9G" +
                "pXMygC8fGXkDRxzLEti8UGPOoKkjZM4eyi2WezIOdaDGxiAjNWeFdiy6Jww/BK74e+wMcjUT8GAaUFg1" +
                "CE17moWwEOg60D4p9/5Di864jAQQZzUtOiC+b6AoSzsC0GE8WE14AXOzvglLmx5JVUmMNSpLYgH71UwF" +
                "i7/fCXyq2RUU95DcrtFRCvYsdR+3p1Z1/hxY+KqbyEyFWQsiaFZ/mteffn0s9u9oIZNI6Zs/PBdRDmWb" +
                "v+7CjS03sTL4pgove1cKM5ex9G2i1kpFvn32g/atnxI8SWwVScx5QtYN/MJqQeqqjN9joT6DJYm8b4hz" +
                "M90ey18AR2pKMvkB+cXubBeKqkUOc/HUq1hdL280PDSGRMuqZyrflrMmj7yUh36gT1ilE3yv0SxZ7g82" +
                "Zx0BUPprB3XZN9vifwuieOv2z+tv/4dvbyU3fb+z+6EhzOOZDhIdrNHvbXN1+CWwoQY5PA8xSY7ZUHZX" +
                "BBxVL2j9WMGLbcl0F+seR8DF2et8comhFd/E1ccF44QW4J2/n2bSp+kXf0e5/FXVP/1Vovo7r/8vX3at" +
                "JWv9D7qu3IH6KwAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}

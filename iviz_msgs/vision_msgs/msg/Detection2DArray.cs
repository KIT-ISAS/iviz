/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Detection2DArray")]
    public sealed class Detection2DArray : IDeserializable<Detection2DArray>, IMessage
    {
        // A list of 2D detections, for a multi-object 2D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection2D[] Detections { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Detection2DArray()
        {
            Header = new StdMsgs.Header();
            Detections = System.Array.Empty<Detection2D>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Detection2DArray(StdMsgs.Header Header, Detection2D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Detection2DArray(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Detections = b.DeserializeArray<Detection2D>();
            for (int i = 0; i < Detections.Length; i++)
            {
                Detections[i] = new Detection2D(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Detection2DArray(ref b);
        }
        
        Detection2DArray IDeserializable<Detection2DArray>.RosDeserialize(ref Buffer b)
        {
            return new Detection2DArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Detections, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Detections is null) throw new System.NullReferenceException(nameof(Detections));
            for (int i = 0; i < Detections.Length; i++)
            {
                if (Detections[i] is null) throw new System.NullReferenceException($"{nameof(Detections)}[{i}]");
                Detections[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var i in Detections)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Detection2DArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "5f62729a3134356dd48cf97c678c6753";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1ZW2/cuBV+N+D/QMAPiXdnJmmyMIoURdvE3V0/tJs26TUIDErizDCRRIWUPKP8+n7n" +
                "HJLSeOxNHhobsK2hyMNz/c5lztSfVG1Dr9xaPbtUlelN2VvXhoVaO6+0aoa6t0tXfMD6tMP51enJ6cnP" +
                "RlfGqy3/o4WzGbl+a+JuU6nOu84FXYcVdgjNtJRJqsZutr3amNZ43RuipkDFBiG5s/1WNbodVanbylbY" +
                "MuM3H6vU2rsGnAfbbmqjbNsNPbi9TFufXb57PztIbP/+//xzevKXNz+9UKGvrpuwCU9ETyTQmx68a1+p" +
                "xvQaImhW8xaCG7+szY2pcUo3HcTgt/3YmQDuz9RbUoRNctb1qIaAXb1TpWuaobUlKaS3jTkgQEdtC3V0" +
                "2ve2HGrtccD5yra0f+11Y5g+/QbzaTBtadTV5QvsaoMph96CqRE0Sm806RQvsXmwbf/8GZ3Awbc7t8Rn" +
                "s4EzZA5gOt0Tx2bfeROIWR1e0DXfiYwrkIeSDC6qgnrMa9f4GM7JduDCdK7cqsdg//XYb13LLnWjvdUF" +
                "GTbAEeoaZB/RoUfnc9LE+gvV6tYl+kJyuuRr6LYTYRJruYXxalJBGDbQI3bCiW9shb3FyFTK2pq2h8MW" +
                "Xvvx9ISOyaUg8iMpWzyabYP/OgRXWnZb8u/Tk9B7uoDtcm2rb+idNzbA/8VBZ8FBwl6atW0NuDvABAUr" +
                "InLJW+YOGWxjyavgiby/rCGUXZM/4tBCFQO8oC3roQJFRLxlWraF4zeyRSIdOnc7kl3fIhHvjYAUOlPS" +
                "K1UCPxSWbKM3BoThgOAhooZThVG1o5ioyP/JNmCSHJT33wdfr+hmsmqhC1uDVQOA+IXR7+excyATbPgX" +
                "LPUajgUcEdYYRM5I+MINwCYIUbg9vMT79JkYEBTF1S/j6ku3pzPYKwTeYhNpnCOfomcCNbrZpOvguyuz" +
                "wqcNqScDKWmEAt9B426d8NOIxOcr9VfX48ynwXpCSdJmXROKwN+DAeIHp2wPiB1Jeabp+hG8BtMG58VN" +
                "rljVwQ2+hHM2G+H6CljvB7MQz0b0B9qFqOu1BTDH3NF7XX4kTcwsD/KFczW86Dq9jiQvBd2IScIh4D9w" +
                "aVQaIsI+nEA6xOrkm/Fa5Bf2yxtdDwYhVdeihtp+JBSr7HoNi3OGYM3gCmvqCihETqqMBuBY2AZBPUCj" +
                "ty2/ivKObuAjLDFTgJ9DM4M3HAc1VMkkJ8Fo/S3UtMoxnt48XJjf58icuNtkqW1+L06YLXlX8K4mz0UO" +
                "Qvog28XsH+lxNMMuDu7cK11VTATanZFRGpHTz1L+1SX8cYA1NEAGHr8dkPqXSEEVgzTTBL43cDuqDihO" +
                "AEVbN8CYnfFEWOloe+c+Dp1kwZx26U8Bt1+pN8YcKOmf/HwF3lb4zC7YOM++pm09mU+sJqJPgDESJEFj" +
                "a2QFSjjiiLfLoYgE6iWKGdfeIGVMOMjiy7EoD3JKwjCvW8TWu6fL37wHJ+va6f7iBxVKMDhxc3FJpsq3" +
                "Hpk1xgjviVcUsdyqGPkpU/PRRCY4JK613eONN4ggFk0gN6ZPuSTZbMKejcFRKAx4DSkRe5GpA6CEyjh5" +
                "5h0NWVd2CqkMnWeEYkYckzXFLOJ/C3CLlc/vJH0e+BdqRorQJF8xCl1URZSvM3pwJFD0tvkWwQzaxU5H" +
                "SQz4admO68h5kOtQaQp4E4Zu9U1SaqYwnYw2ohujikZxP4pHistXjisSUTSF6DdDiC/dn5O9N1TIQQ6q" +
                "DETvLVSN+AmdBp9coQ844QkvOHucnhC9LMGZ+rvbLRv9AXrL1MRA0S8u9hcIiSw6rOftPjm28zbvh+Wg" +
                "dPhL4BSmxT2Xej9nNEEUQBs3eKR5ylJmfhgOwEXp4/1CjQv1eaG862eYpP6tiObR8n/uXv4vL5/n2Hz3" +
                "/OL9TKAHNaP0Y8d6PjbdgpoIWq7ie8F5lLtzna9Q97/moE87Tk/+NqA88S1TnnY+oJhgJztozlQRu0QK" +
                "SETuSnwfCD3hJxwsPQIX0uPnB5NiUuKdsXag2lsxh0+fJhMQ3nHUfVGy9Lh7oNLjoOQVx7xdLkuxITjN" +
                "UUV5iIOKCgBJDhGgMTRAY4W6kDp/RDCpoUP810H6jaw9kBi4aWV58bBkRxBc5iQbpGBD74HcVSyZCuF0" +
                "ycyvqNoDS2avkULlJTcebCIppYWYdCeJ77lgC6qqo1zoLKkHn/h6yXxudfOIxhxtzBi63sDk/VaMmRuD" +
                "7NKPs7znt4P0zgSblXcMEtSx8dvppmA/m4Mr7iJ5d3MDpUA6jAxyHzYvIw7smIsX3HY981P+PD4oUKau" +
                "l8xOtqFPr2uMO1DU4pdEWA9wuueStKNRkFLiMAYdF+UzGsQYjHckTNmxYmqKh4+hGBMfjxkEt72wIzkR" +
                "DJJ7XN11deyC0fXoj8ILVuGCSCgoUfBEeucNcOyo71q3JhUn1mev410UH67DYIKHSNQikbsH42+EOvic" +
                "F07VwNUu7ilRJOExSb9WO8NFDmdTDEwQGa5Dw5xmiSiEeHwlQqp/XImr4g4Pz+oMubvcD6UpVOitQXUk" +
                "nRLT5RaM5kp+DahjfXY1yrxJaZgm4ih1Y3M6XIdQPVgYViw1DNEWEK5HxxmSpnP3S0RhNHb08REQhRJi" +
                "jRIBOgJOWDR4qIlx06NKmANBbyztgc0JouTaqdbjcpDQmxtyegaY9Ahz4myaxoH+lgaCSbg+hzrVO8zG" +
                "wKWoUaPeKYOo87nu5OIqddx0EykXT/RbU7M0RnHpnQylyEAZaSsXW1jQg2xs2pyC0twuVnt3lgRU/vPU" +
                "B92ZXbu6+orsAx57/S3HrrcnFlmio9kEVIB6FSqOA0qelHA1+HShnp7zhI6mSh1ms2vqhD3inZEsbjwa" +
                "IwH0+OdMxeVpHppbLYwxkDmiQnlCOFFM5w9+MrE0FpzRolBGfMeWB4RK/Pf6Xkow3gbQjo1y4pjS1IZ9" +
                "gdT3+9xxx+mb9Kg8xo8N66+L9f14SKFyu/YrT34+PCnY5yL0xYT16yRobkXInnQqoSNBNK1GUq9YFTQV" +
                "uJdejsPDme7ECc8FgOf9vSQ4yxrgi6XBJjqbNjas5Gdx5A5EJ/1Oh4S2LC/S1H2h2qEpxIze7UI+vrMV" +
                "eDo6zst3ni5dPTTyPYmUB7XZwEti8UTIgWmAY+COldjaAnCDL58w6ev0OqzKrpsN0HZafCbE70So6tBI" +
                "AijE4hCck9BCfYCF6Rxmf0sAtg9/pJFPWMkgErswz21ptATbIfgrLtgaDGviaFSGykQ4scIoFW/J3CeF" +
                "/DktUCvEFZBaLlWJ4X+LHNcYIF27WUg7yU9UsdxjUgWbIm1TyR5HjunrMbmdknaajj+ZA9dt1eGrATHh" +
                "b2moWFhUHhU6ymhCgvT0rUJ+94fpW5redAc8/UgFCdwCtmw3cAcwUYw9zbr5CvqCjEjNTqD8pWws/Ti/" +
                "Fbnpbnx1A/rfsZ+h8z09+R9oB1+FVhwAAA==";
                
    }
}

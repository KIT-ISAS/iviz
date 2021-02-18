/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/TrajectoryConstraints")]
    public sealed class TrajectoryConstraints : IDeserializable<TrajectoryConstraints>, IMessage
    {
        // The array of constraints to consider along the trajectory
        [DataMember (Name = "constraints")] public Constraints[] Constraints { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TrajectoryConstraints()
        {
            Constraints = System.Array.Empty<Constraints>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TrajectoryConstraints(Constraints[] Constraints)
        {
            this.Constraints = Constraints;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TrajectoryConstraints(ref Buffer b)
        {
            Constraints = b.DeserializeArray<Constraints>();
            for (int i = 0; i < Constraints.Length; i++)
            {
                Constraints[i] = new Constraints(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TrajectoryConstraints(ref b);
        }
        
        TrajectoryConstraints IDeserializable<TrajectoryConstraints>.RosDeserialize(ref Buffer b)
        {
            return new TrajectoryConstraints(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Constraints, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Constraints is null) throw new System.NullReferenceException(nameof(Constraints));
            for (int i = 0; i < Constraints.Length; i++)
            {
                if (Constraints[i] is null) throw new System.NullReferenceException($"{nameof(Constraints)}[{i}]");
                Constraints[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Constraints)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/TrajectoryConstraints";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "461e1a732dfebb01e7d6c75d51a51eac";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+VZW2/bOBZ+F+D/QKAPTXZcT5oWi0UHeeg06TSDbZttukUvKAzaom1OJVFDSXbcX7/f" +
                "OSRFyna2s8A0WGBmCjiWyXP5zv3onni7UkJaK7fCLMTcVE1rpa7aRrSGv+pcWSELUy1Fi6P49Tc1b43d" +
                "jrJn8fSnz+ndUZad/cn/ZS+vf3kiSrNWup2WzbL5MeGe3YMauhGlahq5VCRKix8aIUWhm5Y0K02rTSXq" +
                "QlaVhi6JtJMR7j8tioH2ZYd7MyUa2epmoVUuFgY4iKWRBUEzUz06+G0tC52PslGG+0S9kqWir78aEIuC" +
                "AqXf6Ml0gNUouzKNJvEGJ2v/cPfwa6sV1Ns7b+Lz3SvvdKNnutDtdnBj3T+e3rXxdoCBAfov7GZBeTKd" +
                "dKB51De6XeGQFHNlychiZroq75F3+Ab87zExPuGvy/lKqzVMBnf51HN5gF8LZWU1V9OZKsxmHCX4IflN" +
                "zqDD51G2KIxs//64PxSf7Jw99AMzcNI9FRull6uWJF9ICit2s5acOZpEHOWqMq1qhFUFDLxWQpe1gfog" +
                "R3oZaGkHLi2eFabBM/z4VVmDyJAIhwLx0V9tj6NwToo7sPu+p98au2S5XC10lThCb5So64SQfKEkpakV" +
                "fzhoKa9ZMzMtUkD1ZQ9SqxbKUpLrPYeOJY5D981i0SjA752SCS0sjhx7MwF9aZc4UrOHmuTchtKqigkH" +
                "F0bZUplStXbrwHjHifSRpzFlGlPHM8qwNkVXqtvlF6PsZ/Jv8HjnjsZTU6uW7J3/f7723bJM0+YOXecT" +
                "UPwaAuTS5hCslblsJeu9ghTKPiiQDApckmWNpMC/tttaNZPgl/i3VBVCtyi2omtU7gpjWXaVnssWmGi4" +
                "bnofNzlB1dK2et4VkuAyFhai4+w/RB3/GvV7pwjXy/MnDKmadwQ5OOlqbpVsyFiX5yLrgPSjU7qQ3Xu7" +
                "MQ/wVS0J88AcdpQtCatuagvoIYxsnoDH35xyE9AGOApc8kYc8bMpvjbHAkwggqrNfMWufrVtV96T19Jq" +
                "OSsUEZ4DAVC9T5fuHyeUKyZdycoE8o5i5PFHyFY9XdLpwQo2K0j7plsCQBysrVmj3OZitmUi84LqHYJt" +
                "ZqXdZnTLsczuPSeMnWezRfApm8bMNQyQcwUJQc/WmOr8e3njwYAPrmUVmUpRuyHFmn8jz1lYBU1qOVcT" +
                "cpJLNqup4BQUWVwH+5u4mGuLqwjzCeUL5AVj1VjoVuQGUYxYBo1SfgFJBYzptqxrEJPUy1VNwR0DPcaV" +
                "IzVZTsZis1KVO0UYsUdzDOi5sHqpc3cTjMr+shReubFoF6fAGP0Uy+yYwWAgYo3rT44n4nIhtqYTG1II" +
                "f1gfelyjg1zsIq0xY4o7T2II6BWn3VA6UDVaBP0k63PNTf/Xtv/r6x2UuWFGhtjnVMgUmdnlaJj50XnI" +
                "y1RjUN4agyYyKAioa6tLTemgccHNRuxqF0G+KhhXIpU4kuREyElAFyVtJWvlRLkmqleBFPWVPdlYZFBX" +
                "wQU8NiuNLMAdWOROdawwlOzy3RKGej6gOWVKjvAlfCjPdfCQSHBMFlspiv2Ke+xazTX32ORIOX/0fRt5" +
                "oNf2SC5RccaI5UOKvgRNiOJI/1fVPPdvq0UHg0bfq1zdZqnoNDNzMwZGFNtjMd8iK6KujaleKOEnF6bi" +
                "NHL+wBiu5JrDRtsIJYgR8EgdPLhIy73xyRj/I26ozPxD/Pz6/dlD//f11YuLNxdnp/7rsw//vHx1fvHm" +
                "7FF48PrVxdnjzINNpZNcmVBmmfwpep6FQzkyNSYnlLvhUR+gwD2eCHco85H46YXk2BOh0Ni7roQyigPB" +
                "9ZAE143wLdz9eOe+G3uJw3PfzEFxFnXM396PxQc4G+D5mMpMIHOXp6pluwoSzY1FRq4No0ylHaWq1w+g" +
                "TyK20/dnJ8m3Dz3W9O0joE5Fcvh7qTihktnRf4AV5X+UBmpKnJzI4EufL9D8ylx3JIKfgpwHTQZ2nb55" +
                "en7572vIk/IMRmaaZGDXEjlUnOtQj8vl202d5Em0IvCsPgp5o9EYxoI0oDt9cXH5y4u34oho+y/HUScQ" +
                "AW4J4lGnFfeOPeY+FsQRxcKx40ejZODjtPN83JeEz21cqFAF7Jz5ZKNu5/mMDEJIhZ9wfyd5JjFJewNt" +
                "uSucuJDRdfQhxpTuU10jf+/qsV++/OBBzXYi0ePXu9SO8nCuJFL3Dkdg6OD3SXH7qZWrXt/DyDDfUaYd" +
                "NkBjZy3qZt3vrpoQ2sm6A22SawTCgexfHbK6rZhuPHdXCiZT7WCaHS413Mg4UPcbfcudiB+hO9SmDvAc" +
                "Ck/ffo+4U3f4zU6snwnvoLxSfxCKamIGqvGU/MKiEK2XrJaYXn9KqsdaFh2PxAs35wZLNqT0WtGcp5pP" +
                "nzPi8dYTQB3raWU+MWLs7mTR39jv61iaA/5E2zp/6Y6gCmocgCyodb+JQrkB9dMjJ6e6mQK47ylt2m0f" +
                "3Ij+L3uloVf/sd0SyrumDiq96fN4snaKrS335zE+djvOwxnrz1llmZpowe+ogDxg0wllLVX7sJRsoqRx" +
                "UyNnGEm6Vk1vpnRz2p8+cGT77SNf9478hdZSqb8eWsd7Mycau85u0fHrhpIe0bY77uzd1J3rZi6O4iby" +
                "eG/35BZOoxAPfIGSczPobWRPeev6kM2KqjF1J5rHJzpsaPrv3Zz2CcYy5ZdePB7WooTEjmjNtgJnO/bN" +
                "GCBhPyHMDDp3c8cjuU5yMPlXsPyTUVJTA0ZM4tXrtyAPleAJJWQouxKSArIb/hMKt87FZ6rdKFUlwhNN" +
                "aiYifmNIZB1ddNOBbLLpdT2cHycQLwW3JmutNvgMbxw8NHDu/YU3T71HxLUGDnKGlr5Zma7Ij4kwbxgp" +
                "AJoOA03dWR4/J0kiGLSmbE2uIo4GkQi+Qu+lCExEe/Iawu2aHZXhlJzS/InyVTQVea/v/3dPgh0stUJW" +
                "UXRno4oiWsrN9wEs3sq53ie6Z0xsY9Zb3ciyLtShifzab1n7fXnjc8hza8oE9+CnrdlImzcD6/ZiuxCQ" +
                "u/6WehkPOLKGmeBJvDrseCFbymrrBrwJz0xeZL83c2ceuwNj4VxCuk4P+Y3LfbPr6m5Rzy+oRL0FUDqP" +
                "IcvdRzCwLDZyS74sHrGXY7jCSbceJpmnzHfHsuDt1h/D4HS+HP1lEi45U+k8MI37xp36Bgpxr+LR53zR" +
                "Yh73BN0CUzZNV6o8PUjuURjzxa8waVnYDoxFqQxQxw1nDwlTq10TSnmy4/U8JRNkElhi1rX98gmFbz8K" +
                "GyJFAkAvU6z9tliDLMF5u+850RPfu1hzRjHdchWdi/qOvSgcH8pvIUbQNcGnSumiZ6bmsotxdqDHYDa0" +
                "ySYFULrbsDQLqcehDjmZNxFNoosOc8rypL7ArUqDBnhBkSTZd8fRa+bS7V/3hA8gKmQzdLArueZ364Vx" +
                "i+YTv7lzky4zdN0HFYbBTMzNMf/kk3TIpBUtmYugXMKfAQi5u/eQsEZfHMwIvX+7iOIXsel4wx24ryCh" +
                "v4tSu2BHfCq7IMDpl7GTsw9S3ntDnxNfkzfGS+QWAYAfF03N84NqjgNJmnIL1dKLn1AMPetb6F/pH08d" +
                "i5Q8JKsV9d2UWY8nafYIcDudiar2+voymVhnLy04QrHCSE/UJ3YDpoLeSFk8JtJHJ2OW0O35T4hVE5Lv" +
                "0AuS2k3JIlQpCDWlg1M+GJIZxX24yHWy0aWGpsXWtUjpHc4ABW1hhmp6gvcONAOhbOw5U/Ayal9dLofz" +
                "V/iNSY/4Rd6w7qQNgy97zgET7yNtaJ8dULRcQnvoQmuyA5LQC35fQNG1AxgTGCKWypzmYCN4POPXl4Pw" +
                "Ca1MeL0d26T34kycjsUHfDwci4/4OBmFXeLFq+vXb6Yfz3affDh7uPPk/dlpeOIzKZusF+AvNRPcWmUY" +
                "AvoeMjwGPH5PvPse2blmeAc8HFUzJkCFKsv+A0ro9qfnJQAA";
                
    }
}

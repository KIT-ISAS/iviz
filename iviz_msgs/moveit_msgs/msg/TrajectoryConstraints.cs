/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TrajectoryConstraints : IDeserializable<TrajectoryConstraints>, IMessage
    {
        // The array of constraints to consider along the trajectory
        [DataMember (Name = "constraints")] public Constraints[] Constraints;
    
        /// Constructor for empty message.
        public TrajectoryConstraints()
        {
            Constraints = System.Array.Empty<Constraints>();
        }
        
        /// Explicit constructor.
        public TrajectoryConstraints(Constraints[] Constraints)
        {
            this.Constraints = Constraints;
        }
        
        /// Constructor with buffer.
        internal TrajectoryConstraints(ref Buffer b)
        {
            Constraints = b.DeserializeArray<Constraints>();
            for (int i = 0; i < Constraints.Length; i++)
            {
                Constraints[i] = new Constraints(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TrajectoryConstraints(ref b);
        
        TrajectoryConstraints IDeserializable<TrajectoryConstraints>.RosDeserialize(ref Buffer b) => new TrajectoryConstraints(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Constraints);
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
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Constraints);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/TrajectoryConstraints";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "461e1a732dfebb01e7d6c75d51a51eac";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuVZW28bxxV+568YwA+WGppRLKMoFOjBsZRYRS2rlmv4goAYcofkxrs7m9ldUvSv73fO" +
                "mdmZJam6RWuhQBIDIrkz5/Kd+9lH6u3KKO2c3iq7UHNbNa3TedU2qrX8Nc+MU7qw1VK1OIqnv5l5a912" +
                "9CIe/vRrenU0Ov8f/zd6dfvLmSrt2uTttGyWzfcJ89EjKJE3qjRNo5eGJGnxoFFaFXnTkl6lbXNbqbrQ" +
                "VZVDk0TYCa4/L4qB6mWHazOjGt3mzSI3mVpYgKCWVheECx4FaPBsrYs8G41wm0hXujSj0V8tCEUZgc9v" +
                "9Mt0gNKNbXKSa3Cu9j8Oj752uYFWe6dt/H144V3e5LO8yNvt4Py6/3n6wAbbQQSo91/YsYLeZC4taHmo" +
                "N3m7wiGt5saRYdXMdlUW8BZcBfVHTIkf+7t6vsrNGkaCf3zqWTzB08I4Xc3NdGYKuxlH9t8lz/QMCvw6" +
                "WhRWt39+1p/pf9g5eeB3pk6CPVcbky9XLYm80BRA7FPQrElcTx1lprKtaZQzBay6Niovawu1QYxUslDQ" +
                "DdxXvShsg9/w8ItxFlGg4foFYqG/2h73kokQD2Dufd++N0zJZplZ5FVi/94cUdXJaPTSaMpGK/5DqFLy" +
                "cnZmW0R69XkPTWcWxlEmC85Cp3pfoct2sWgMYPdOyFQWDgeOvXmAunZLHKnZI21ybkOJ08SkggujpbGl" +
                "ad1WQHjHmfLUk5gyiamwDAKsbdGV5n7J1egncmfQfycn46GpM0vyxv877/pG7tW0mcAqbgC1b8E+0y6D" +
                "WK3OdKtZ6xWEMO5JgbgvcEmXNeKfn7bb2nDCZ0fEv6WpEKhFsVVdg0Nc8sqyq/K5boFIDl9N7+MmJ6Ja" +
                "uzafd4UmsKyDdeg4+w1Rx7/G/N4ZQvXq4owBNfOOAAenvJo7oxsy1dWFGnXA+fQpXRg9eruxT/DVLAnx" +
                "wBxW1C0Ja+5qB+AhjG7OwONPotwEtAGOAZesUUf82xRfm2MFJhDB1Ha+Yhe/2bYr78Fr7XI9K+AB8BAg" +
                "AKqP6dLj44QyiX2GilbZQF4oRh7/Dtmqp0s6PVnBZoifpWq6JQDEwdrZNWpppmZbJjIvqKghyGZOo8+g" +
                "W8Jy9Ohnwlj8mi2Cv7pp7DyHATKuFCHU2RpTKszfxhsPRnpwLWfIVFCCmpA1PyPPWTgDTWo9NxNykis2" +
                "q63gFBRXXO/6m7iY5Q5XEeITShVICdaZscpblVnEMCIZNEr9GSQNMKbbuq5BDI6O6tNQiJNZLF05MpPl" +
                "ZKw2K1PJKcKIPZpjIJ8rly/zTG6CUdlf1sorN1bt4ikwRrPEMgszGAxEnJUm5HiirhZqazu1IYXwwfnQ" +
                "43Ic5GIXaa0dU9x5EkNAbzjdhlqBMtEi6FECQqq56z9t+09fHqCuDbMxxL6gyoVKEDI5zHx6ITmZKguq" +
                "WWPRHwb1AHTt8hLFbQ0bcmizCTuK81gOrFREo440uRAyErAdNStdG5HjlojeBErUN/ZUQ2lBDQULMNis" +
                "ciQAbrIiaypdhaU8l+1ULZTuAcEpEyKqV/CdLJPCDBEjtTFZagWqc432jHzczHNunMmBMv7Tt2bkeV7P" +
                "I71EnRkjhg+o+AokIYdQ/hdKedZfVYjOBV2+jaPca5/oJzN7NwY8FM5jNd8iEaKU4ZPFMz+JMBXRR5yA" +
                "4VtplGuom7uIIogR5sgWPIhox53vyRj/I1SosvxF/fT6/fkP/vPtzcvLN5fnT/3XFx/+dnV9cfnm/DT8" +
                "8Pr68vxZgJqqJfkvYcwy+VP0+ygcypCcMQmhwg2P+pgE7PFEuEPJjsRPLyTHzpRB2y5tCCURAUH6RILr" +
                "jmKMvjyOdx7LDEscfvZ9GxRnUcf87f1YfYCfAZ6PqcwEMjd0plq2qyDR3Dok4RoVD2JSNUd16vUD6JOI" +
                "7fT9+Uny7UOPNX37CKhTkQR/LxXnUDI7Wg6wopSPakB9iMiJpL30SQJNrs7yjkTwM454UJBD6E7fPL+4" +
                "+sct5El5BiMzTTKwdEGCirgOtbNcsWWaJE+ied+z+qj0XY5OMNagAd3py8urX16+VUdE2385jjqBCHBL" +
                "EI86rbhb7DH3saCOKBaOhR9NiYGPaOf5yJeEz31cqDYF7MR8GhXnXp4YVwwjFR7h/k7STGKS9gC540aQ" +
                "SzQanDr6EGNK96mUkb939dhvUr7zoIYg3QGzd6kd5eFcSaTuHY7A0MFvk+L2MyuXur5tkbYBGFCiHfY8" +
                "Y7EWNbDyXAoJoZ2sMdAZSe3vp+2/d8jpjspDeu6hFEwm18HEOtxXyHQ4UPcrrcqDiB+hO9SZDvAcCk/f" +
                "fo+4U0P41earnwIfoLxSbxCKamIGKvGU/MLeD/2WrpYYV39Mqgf2dR3PwBixuY3xlsREWKFZp9HOYKM5" +
                "Ih5vPQHUsZ5W2DNhzu6wDww39ps5luaAP9Eezl96IKiCGgcgC2o9bqJQMpN+OhU5zd0UwH1LadMG++Cm" +
                "8z/ZHQ29+uv7I5R2zFmDHBRyeLJaih0tN+QxNnYMfF+y+i93VbYmMvA2KhtP2GDKOEc1PuwamyhjH4p6" +
                "huGja830bkoXp/3h/RPbr574snvij7J4St3z0F7dGzfRVxq5RcdvC0r6ifbWcfkuc3WWN3N1FHeMx3vb" +
                "pX6lxN7P5ykVN4NORveEt9J1bFZUe6kXofUI1gq06KTxvndsWhhYR4RfeeF4KIvyETcihY0MjnbskDEi" +
                "wv5B2Rk07ubCIrlOYhD1axj9LCmfAR8mcP36LYhDH/hACQnKroSYgOuOP0Jb9o8GbVa7MdhbRMlBktqG" +
                "CN0Y4jghi745UE3Wt9Kt+cEBMVJwE7LOzSZpqgUVOPX+9pon2yNiim3VDPuLLXKs7YrsmOjy9pAcv+kw" +
                "udSd4zFz0of9oANlM3KxEApEIPgIvU4iGBHe8W2CbI+FSDoHpwR/pLwULUQu63v83ZPgBQNhC4c8ijsb" +
                "UxS9hWR6Dyjxrk3am+iSMYWNWWNzh8UcKst+Y3jrV6f99hu9Ig0mzpYJ3MEzW7vBJrcZ2LQXWZxe77pY" +
                "4lk8v2Aj5Sy8h5eBHa9YS11tZX6b8EjkxfWbMDnzTA6MaQCXZWvFyYyLuUiUMJatO79cUvUWGOXYS3lB" +
                "uLUIZtXFRm/Je9Up+zUmp8lItr0k8JSZDuwJvrLWGEai+G50EWEXDITdkmcYd4c7xQsE4rrEw86pocWg" +
                "LfRkF4ldKtYyWXqOfKKw9rPfRtLerx0YiXIWMI7LygAGE+O6JfkQzRJuU9pAzoAFZl3br5NQ2fZjjoYn" +
                "Yg+lbMHbD97aNx1wvN/dRO7e3S7XnDlst1xFf6JWYi/ixN92XCzEBBoh+FEJFUBzZua6i1F1oHNgLrSO" +
                "JuFRljkBpTlG8IaQzBo0k1iis5yZPKXPcKXSoqNdUOho9lbGXYw6pzpwSHQPn0HSQkeKBRK/+qa6ShxO" +
                "/BJOJlfmJ30F5f7BjMvNLj/ymdjnS6RcABI0S7iz9iFB944RFuGLgwkgOLWEEL83TYcV7qd9kQgdW5RZ" +
                "YhvxaNyCwKYnY5ayD0reW0OZE19xN9bLI1M9oMc9W/MwYJrjQJFG1sK09OImFDvP+TD5m/z7p8IhpQ65" +
                "aphBUihoJ8kiQC0a0xsmr6wvg4lh9vKA0IlVRHuaPoFb8MRn9KP4GZSPTsYsnyzpT4gTVtTB7VP7J6UZ" +
                "2SEUIog0pXNTmSX6OA+3uAo2WJZASXozYHducMQXtEwZqhhGk/06H4rDng8F56KOVHI2PL7Cs7D02S0u" +
                "aS/gC5u4XeJ0pAvtowN+jmtkj5pvOnbgUTk2/NJgnAyh4uspVqm4abLFCxryDX7lOAiY0KGEV9F98/Ne" +
                "naun2Gjizw9jbOjOVVgC3V5e375+g83jzg9xMel/eN+vgX3CZDv1vP8wzf3yvkLCAND3kMcxn/Er3d1X" +
                "vuKM4XXtzpDJBKQW/RO4/2VMbCUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

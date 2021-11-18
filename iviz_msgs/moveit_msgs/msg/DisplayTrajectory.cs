/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/DisplayTrajectory")]
    public sealed class DisplayTrajectory : IDeserializable<DisplayTrajectory>, IMessage
    {
        // The model id for which this path has been generated
        [DataMember (Name = "model_id")] public string ModelId;
        // The representation of the path contains position values for all the joints that are moving along the path; a sequence of trajectories may be specified
        [DataMember (Name = "trajectory")] public RobotTrajectory[] Trajectory;
        // The robot state is used to obtain positions for all/some of the joints of the robot. 
        // It is used by the path display node to determine the positions of the joints that are not specified in the joint path message above. 
        // If the robot state message contains joint position information for joints that are also mentioned in the joint path message, the positions in the joint path message will overwrite the positions specified in the robot state message. 
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart;
    
        /// <summary> Constructor for empty message. </summary>
        public DisplayTrajectory()
        {
            ModelId = string.Empty;
            Trajectory = System.Array.Empty<RobotTrajectory>();
            TrajectoryStart = new RobotState();
        }
        
        /// <summary> Explicit constructor. </summary>
        public DisplayTrajectory(string ModelId, RobotTrajectory[] Trajectory, RobotState TrajectoryStart)
        {
            this.ModelId = ModelId;
            this.Trajectory = Trajectory;
            this.TrajectoryStart = TrajectoryStart;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal DisplayTrajectory(ref Buffer b)
        {
            ModelId = b.DeserializeString();
            Trajectory = b.DeserializeArray<RobotTrajectory>();
            for (int i = 0; i < Trajectory.Length; i++)
            {
                Trajectory[i] = new RobotTrajectory(ref b);
            }
            TrajectoryStart = new RobotState(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DisplayTrajectory(ref b);
        }
        
        DisplayTrajectory IDeserializable<DisplayTrajectory>.RosDeserialize(ref Buffer b)
        {
            return new DisplayTrajectory(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ModelId);
            b.SerializeArray(Trajectory, 0);
            TrajectoryStart.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ModelId is null) throw new System.NullReferenceException(nameof(ModelId));
            if (Trajectory is null) throw new System.NullReferenceException(nameof(Trajectory));
            for (int i = 0; i < Trajectory.Length; i++)
            {
                if (Trajectory[i] is null) throw new System.NullReferenceException($"{nameof(Trajectory)}[{i}]");
                Trajectory[i].RosValidate();
            }
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.GetStringSize(ModelId);
                size += BuiltIns.GetArraySize(Trajectory);
                size += TrajectoryStart.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/DisplayTrajectory";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c3c039261ab9e8a11457dac56b6316c8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bW2/bRhZ+F5D/MKgfbG8duU3SoqsiD0nsNCmaS5P0GgQCRY4k1hRHJSnL6mL/+37f" +
                "OTNDipKbFLvxYoFNg9gkZ879PtMD82ZuzcJltjB5ZqauMut5ns5NM89rs0yauZkntZlYW5qZLW2VNDYb" +
                "1E2VlzPdNs6zweBAwFR2Wdnalk3S5K40bgooVoGkDm/zEiBdncvXy6RY2VowJkUhK39zednU+DVpTFKR" +
                "rEuiSQqHfwOor01iavv7ypapFRRV8ptNG1flgLZINqDV1Eub5tMclL5yE9e8CUs2b9+16zeRbK4xNai2" +
                "BkyvapuZxhk3IcWR4Ejqae0Wirkl2T8JpKEB3KdNBDXZtGLI8npZgMYSkiOOzDa2WuSl1SUR1Tb0KJCS" +
                "dAbeDIiLixT8wtZ1MrMmmbhLq3R0CPMshkVRJR5AUExegtGFqpAs94lIitoBSMkFf0bFSY+n68ld5zAA" +
                "UFytqxwEbm/b4XcPL2BVFP1aXrYaHmNV1QxuDe7/h//cGjx7/c2IBmrzZryoZ/Vpz9JuDTpkyIJvyXi7" +
                "QAUx7phjf8OzVdHkZy8e9zcu+H6cuel4B8RH4/Q9zNwaPLFJZiszlx8+QMDblMIygaIGvT0v+YglS7Gv" +
                "j0d63WRKs5J4C14BOymzpMpgP02SJU0ihj7PZ3Nb3S7sJYIhLGexhNHJ12aztPVQwgW8Gn81FBbFJkaL" +
                "1C0WqzJPxQBzsNvdj50w3gRWXzV5uiqSCutdleUll08riIfQ8TdGtqdnI3pobdNVk4OgDSCklU1qRsSn" +
                "Z2awgtTu3uGGwcGbtbuNRzuDBiJy9VgQa68YlklnUo+A42/K3BCwIR0LLFltjuTdGI/1sQESkGCXDong" +
                "CJS/3DRzp953mVR5MikkUqaQAKAectPhcQcyyR6ZMildAK8QWxwfAraMcMnT7Tl0VpD7ejWDAJmeKiSI" +
                "rA2xaZEjLJkin1QJ/Qm7FOXg4DFlrElNNIKfSV27NGc+QwBq5iGpiTaY1P5bviR+QSs9T5iHW78XR4nx" +
                "EHoF1VB4DJVvTwwMBTw1+IqHJE1twYzNj+/eAaLbXm2nMPvmnRg/5dfBBYtGHrVXND+b0TgfIEa3sdhn" +
                "b+YDH5ZrChg2DYpQMLSRXnyfi3psDsH7YDAtXNJ8eU/CgCes865lp/Nyi63Oe+VmkK30kxjNeFq5xcfO" +
                "Ah8Yt2/5ckNDZLfSULF6G+7FBQOr17Rnp7aS0CB2vUdngfO6F4ypvD21WaKJ5DYSiVdVB9aRpfGpxdFX" +
                "+K2sY2FwHMxTV7QVWgeEZHW4fbFCrZMg/FUVKh8gi5o+bfV7uq3VAw1eLD2jbRW2nKFo8K8itI6VnYiw" +
                "tjapQYZldHpG2b51CrDhYH/aukadN5W+PtC6tqNGX7Mp5B+srKNLc7RCmnDmSwOAx4OZRWXbBERvwiot" +
                "mvX32hfN8HvCRIxIgpduOuEhWCeq8hlcv1vOmj6adV43276+gwLG0zGPfw/PtqH9F8LFNVLW8ABfi56q" +
                "Ztwqa2KbNfuwZu12woRE2Gll2fskKaqJwY9iF3d1fyE8Dr5fYUNVkt3KaSS4KT49Ofu4pAXxY4+Fto1y" +
                "JcqfhU0Yoly7ExuzvMJWsDFUi4Gc0HfkjckcRIJ2CTAWyQVAWlQT3J0sl0X0ARULX2PLkR3OhidofyFi" +
                "WSW9J6mQai9PDY0s60VCgWk8d2h5pnd8O0OaFRm0CCBB4MdDNmUbtzJrMoRfKl9kSsoNdEkx1Dh3wizh" +
                "QWxLVDw+9lBo5BqEeyjeZ0RzFX/bxN/+uCFtt4a2V+HwVLTrIRdtqZ1Pv7dmSjm/j6f42/rGnJaxJHIW" +
                "6uu6jYTbLE0qdwGjgrpoaDUK1NKigmWySsqZtAPsDNBhBKf1S9pnv+4GW1nppSOPO0OD7pQA44ZV0+/N" +
                "4YZDOPHOXIMCCMkYAgCjF/WA7umqTjmsnbymYQHHpPBtZxLBSc88ubSa6pZwFqQwqUfb7MCIsAV6K3cq" +
                "in4zHZE9aBpkUkBJXVHktRRNEyZb9C5J+MbijdMgcgGXb2VwPAj7H4XtL2Q3M5D/Mo6Qxx4y8T4ukhmk" +
                "m7GVZARCPJIyX3qXOkUoYkjUYMGer4LxsYdBZcN4OJ1KNwN3oxwkYOq8SoDI/k4fu3B1w9ZysUTpzHAk" +
                "fXBotKTOCqxOXMam4yjQg4UcR7H1LWjNexaz45A6U2xrNEoRnUejzqRmIgHbrJYIgESIKCzENx2TOx5M" +
                "nEMhWY/J3EccE+w1wegAUgQHL9Di1BVobEPozmydVjkDOEtP4U54r20jZYmr0NiL+1TQOkSkPsAs5/uC" +
                "uElKb61hjip76Qq0YNK6VXlNj0uPSU1mp4gR7H3Z06P17nradiV0VCUZASyOT9qlMVbtLD2tZfEpBgH0" +
                "0HaLdlhxrsDstjOHPHq+4O7nx+gZQzWqX9jwlDmkQIPLOD8UJ0XvzpDAWnu7QWobXSmD2PQAb5hOB3wi" +
                "tdqsWS6iFmC9nw2lWe2v0SEMZpZRjRI/1JPCVqKIzQNxKhv9GFgj/tedJorxR5oI9CDEQDfSDgpxfVvC" +
                "QowUN+R15hLaNqmQOkU2+fXgnRbmltRoUgzNTyxNWKVo1eCjqHBROgD0+ukNNghrcSIVB+tpOCu5btWp" +
                "nZRdLJuNt0ZKT7lR3XZ4r+duVbDH8jBETnX+B8I9WObIQOB0vEbKcxZDa2ABm9EGIpm9diwSDUFXLO84" +
                "FhMNohDot7axYxP72Z0m7A4TNrsjgxuJKbu5h7Hl1U5brmGgM3gXcUQry+wMhYXYHiuMzEG1gDNFUHHr" +
                "ELQhlVXarOASWNYiVFvWmhrSXy2YwWSsH1yUQ8p6g0pyocKvlzK9kxq3bURkz8w2bQgA2HYGcIG4JHHK" +
                "pHPUCkPzmN5whTlcATPBFLRISiQMHy+Qt4D2h1dnjyWs3WUaP7qCveJvsuagQScgGCXqR98UdqeaO/MJ" +
                "OBMMA1B0L51667u0BVwRoMGncf5Al5kk6QUZ3qLh/5HsZiPZmpOu+QdHsrD8fymSXRfIuqOnDxrI7B9z" +
                "NPzZ+/aTiAkfVV431S0p2n2NIBjQjq9tlk4QNGFqonM2TMzaH9YyCbD2USuum2mYrqn1w8S391oHBJNO" +
                "uSx2gkOo/CoUhrQvMVd2FuFogr+LZaBPEFHGbkyKb0R8uGA9T3BYJZJCEvQn7M0uFYMDrXy6vQyXCcID" +
                "jCgAT7pzaozMCsiS5+LcPsQkhA2JbRBkOe/wHczBPnihn9FyL7IRBaEIskFfUAo0nNP7Ulo6Rh/Uuz1H" +
                "6M70jBrZ0NPgVjD1AyJGyZwgj5rbkTAlg+mwwNlattHaDg6plPoNrXMKsLH2rHq6niJr8JqAUiVlKPJb" +
                "qmXx1okftCEpWfXBsZYyKSOtBWFEVgDaT6MQ5izEjkJFR2RpgcKGc6nKrcQXPJTjeNouOEqbMsxhCk9s" +
                "FQaecnDoR98eMdUHmKFRft95Ne4qgLqxRxGUsrY4NY2JoacMzACm5qJ061iH+fU345Z73PGBD7gyVtP5" +
                "cWyiQ/UkbrN7hkJuYfSeUy/DIzEggQUFPgPyp+j9vb+2N2OCqnGOrHbBtnGSoKRxXkDRgfTnmA3JrJQa" +
                "VplRHt4QAsEEyO10wgddNkx78mZwW92Wizn6A7z+vECyYWeIEgTw2hXgP6BJ2Y8uQOAlhCVBR+mUVS/D" +
                "J5bi7bL+ALPe+k7T0un/M1vPt6HyDdYu9MNeOPzWgnhI/6ASWG1yboGDdT9u7NwpOTETP7mSZaEa1Okk" +
                "mF+ps0FjWSa6QIAliuMubS+5lYwIpmuY5LeWugcZq+qOYajUuaqUI27OaWRQ3FkEG73M3apGxWGvkN1J" +
                "PiYmmlIl5gwHkw2qkAdnZ/c/I5pXEle3MPGEQ7u/8jKvXMmLPPi9wSAdcI9w96HCqby6gozZGvizP1Br" +
                "b+Jkx4rp1fmzFz+e3/9ceFouGarYKwRr9t2Fj61CdLwH9Oe8hvm9bgp8Qgstky9fnj8/u3/Hx+EW5350" +
                "guUEgXHtLd+rWg4Sjrgi6C0UjItV3XBFYaeNFoNsRhDQaldQVv522nZAxRgIksyURJHNXRL4YhnOrjTp" +
                "4pHFW1jowuePFxffH1YQHv/yH/Pi4bfnj97wQspf3+z/UD6P/nysLHFTpi1TSXs+liGSsSvkeRhqA+mJ" +
                "WTpCi7ZiIzLTSWWs03VkBlPhYHKrtLiwcQ7WxTCSN7q/nVnJQZVYDIJWabJJiPeAEgBmky4pPs1Ko/rt" +
                "6xfPT3FbKHSvvzx49p1RAJhbRStGpI0+0DnBYqwOUmk7dE3tIacMzbnUDpx27WhdXEm6J+cuULVc2JH5" +
                "5B+HlPDh6PAR65uzh4cn5rByrsGbedMsR6enGJ0kBaTdHP7zk3g8C3tHPSitc+mDo2rP1zhUTkcKrB/z" +
                "5hCbct5icJCX9beUpgW8dZIXGM/4DLXPYDk/ViGGE7mzh2obAoRc0fU9Zm06aVwryIlRTkcQcgeKIwnP" +
                "rIxPBczIRAHIO4oA7/oiGH3x96/u6QpmXz2AxLpdig89ptfff4dxMaoEzoyjnrYQv/69eBJWKGxBZQ7X" +
                "s/rul/qGQ/qR+eLe3TvyiNUVF6CIdmu/Apl/jUFm7zWLFDISEITzBv2Ka7yrgt/l0LFxy8Ng0DDtjzcb" +
                "u65kYJl2pp46cRjC1Esa24lJN6ixpXSDxVkTriT5dgeWESbisKzQ1aPOmYRCAMAY9pnYxRm1gv7sBP+h" +
                "Hed1uq/Mwxc/I5np769fPjl/dY4Eo4+Pfvnu6fOz81cI6P7Fi+fn9+8Fhw8hSnINafKrtFYLUQEDZfQX" +
                "nRu+urSdSLYrwh6ee5P87obOspEOWti3yAGOCkETNsV1FYLVYbvnUFOcnDb55hCMC6naRvx8Yn7R2dmv" +
                "XZopZOmc9CaOp6gfhtg/Rf4g9GEr2/HPqEvap1+irPn0K3N5hySVv6dK5i5UOyMnfvrzBARQpRNBjdFY" +
                "+cYZRr4iCb7fUQsKdCjc8asHZ09/eM06qYMzKFlgUsF69VOloqYjowi5lRCKRJl8elS/mgRlx9C01xG2" +
                "4I6fnD/95skbc0TY/uG45UkPzDoSb3mab/VZwRfMEX3hWPEx1AU8yp3How8dPNdh4TWFIDtVn29R9uNE" +
                "1tapQPiE/W213/dJTlnzSnphua2B45Vla0MiU+5n10l7Xy1P/Ez5Uy/U4KQ9YUaT6jHPqrT11J3FrWCw" +
                "8IYmYWwGbl1zHY9VaX8UJgpjkdC5NScC71yfwKRTb4LEI4/OLZ/Oupvj0V9E251QdU8nMRoJRw4tx++5" +
                "53ETuYiNZsxAHWrZXTJSoNlWL8R95XKGauLrTqj1t2LlqiZNfut/fuD5Agqf+u27AZG88QBkmOthEUFn" +
                "lBd2hFYMVSDv60lSADV75C7HXLrpxqQVGNkntcAZir5Il15cf3tXSbVXYxkO3hDBL9mH7z9404MpeJ02" +
                "/e1QIE4OkivzKceCn5r0D/yTmftG2uzEjO7D0u307We8ZT2Jj5/zMY2Pd/iYxce779r7z/feyTvI4F/2" +
                "bcG3kjUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

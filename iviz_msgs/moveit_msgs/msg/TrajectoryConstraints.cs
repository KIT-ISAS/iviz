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
        public TrajectoryConstraints(ref ReadBuffer b)
        {
            Constraints = b.DeserializeArray<Constraints>();
            for (int i = 0; i < Constraints.Length; i++)
            {
                Constraints[i] = new Constraints(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TrajectoryConstraints(ref b);
        
        public TrajectoryConstraints RosDeserialize(ref ReadBuffer b) => new TrajectoryConstraints(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
                "H4sIAAAAAAAAE+VZW2/byBV+168YIA+xu4w2GwdFkYUfsrGzcdEkbpwGuSAQRuJImg3J4Q5Jycqv73fO" +
                "mSGHkt20aGMU2N0AssiZc/nO/eieers2Snuvd8ot1cJVTeu1rdpGtY6/2tx4pQtXrVSLo3j7m1m0zu8m" +
                "z4bDnz6nVyeT0//xf5OXV78+UaXbGNvOymbV/Jgwn9yDErZRpWkavTIkSYsXjdKqsE1LepWuta5SdaGr" +
                "ykKTRNgprj8tipHqZYdrc6Ma3dpmaU2ulg4gqJXTBeEyNz00eLfRhc0nE9wm0pUuzWTyVwdCg4zA5zd6" +
                "MhuhdOkaS3KNztXh4fjoa28NtDo47Ybn4wvvbGPntrDtbnR+0z+e3bHB9hAB6v0XdqyoN5lLC1oB6q1t" +
                "1zik1cJ4Mqyau67KI96Cq6B+jynx63BXL9bWbGAk+MennsUDvC2M19XCzOamcNtsYP9D8k7PocDnybJw" +
                "uv3z4/5M/2Dv5A3PmToJ9lRtjV2tWxJ5qSmA2KdactzBEuooN5VrTaO8KWDVjVG2rB3UBjFSyUFBP3Jf" +
                "9axwDZ7h5VfjHaJAw/ULxEJ/tT3uJRMh7sDch759a5iSzXKztFVi/94cg6rTyeSF0ZSN1vwxYYJGeTd3" +
                "LSK9+nKApjdL4ymTRWehU72v0GW3XDYGsAcnZCpLjwPHwTxAXfsVjtTskS45t6XEaYakgguTlXGlaf1O" +
                "QHjHmfIkkJgxiZmwjAJsXNGV5nbJ1eQXcmfQfycnh0Mzb1bkjf933vWd3Ktpc4FV3ABqX4F9rn0OsVqd" +
                "61az1msIYfyDAnFf4JIua8Q/v213teGEz46IfytTIVCLYqe6xuRS8sqyq+xCt0DEwlfT+7jJiajWvrWL" +
                "rtAElvOwDh1nvyHq+NeY3ztDqF6cPWFAzaIjwMHJVgtvdEOmujhTkw44nzyiC5N7b7fuAb6aFSEemcOK" +
                "uiVhzXXtATyE0c0T8PiTKDcFbYBjwCVv1BE/m+Frc6zABCKY2i3W7OKXu3YdPHijvdXzwhDhBRAA1ft0" +
                "6f5xQrli0pWuXCQvFAce/w7ZqqdLOj1Yw2YFad90KwCIg7V3G9TSXM13TGRRUFFDkM29Rp9Bt4Tl5N5z" +
                "wlj8mi2CT900bmFhgJwrRQx1tsaMCvP38cYbIz26ljdkKkO9hFYbfkees/QGmtR6YabkJBdsVlfBKSiu" +
                "uN71N3Extx5XEeJTShVICc6bTNlW5Q4xjEgGjVJ/AUkDjOm2rmsQ09SlVU3BbQE9xpUjM11NM7Vdm0pO" +
                "EUbs0RwDdqG8XdlcboJR2V/WKiiXqXb5CBijWWKZhRkMBiLeSRNyPFUXS7VzndqSQvjDh9DjchzlYhdp" +
                "ncso7gKJMaCXnG5jrUCZaBH0KAEx1Vz3f+36v77eQV0bZ2OIfUaVy5CZJT/DzCdnkpOpsqCaNQ79YVQP" +
                "QNfelpaSQSOhzSbsaomfUA6cVESjjjS5EDISsJ00a10bkeOKiF5GStQ39lRjaUENBQsw2K4tEgA3WQNr" +
                "Kl2FozyXT/ahb0YEZ0yIqF7Ad/LcRs8YqGVkqbWhmK+4ca7NwnLjTA6U80ffmpHnBT2P9Ap1JkMM36Di" +
                "S5CEHEL5XygVWH9TIToXdfk+jnKrfQY/mbvrDPBQOGdqsUMiRCnLqEQYFSYRpiL6iBMwfGu94UixfkAR" +
                "xAhzZAseRLTnzvdhhv8RKlRZ/qJ+ef3+9Kfw99Xli/M356ePwtdnH/528ers/M3pSXzw+tX56eMINVVL" +
                "8l/CmGUKp+j5JB7KkZwxCaHCjY+GmATsw4l4h5IdiZ9eSI49UQZtu7QhlEQEBOkTCa5rFbq1+8Od+zLD" +
                "EofnoW+D4ixqxt/eZ+oD/AzwfExlJpC5oTPVql1HiRbOIwnXjlGmao7q1OsH0KcDtrP3pw+Tbx96rOnb" +
                "R0CdiiT4B6k4h5LZ0XKAFaV8VAPqQ0ROJO1VSBJocnVuOxIhzDjiQdORXWdvnp5d/OMK8qQ8o5GZJhlY" +
                "uiBBRVyH2lmu2DJNkifRvB9YfVT62qITHGrQiO7sxfnFry/eqiOiHb4cDzqBCHBLEB90WnO32GMeYkEd" +
                "USwcCz+aEiMf0S7wkS8Jn9u4UG2K2In5dGNu5/mMDEJIxVe4v5c0k5ikPYD13AhOJWRsPfgQY0r3qZSR" +
                "v3d1FjYpPwRQJ3uRGPDrXWpPeThXEqkHhwdg6OD3SXGHmZVLXd+26DjDUaId9zyZWIsaWHkvhYTQTtYY" +
                "6Iyk9vfT9t875HRfMd3h3F0pmEyuo4l1vK+Q6XCk7jdalTsRf4Dups50hOdYePr2+4A7NYTfbL76KfAO" +
                "yiv1BrGoJmagEk/JL+790G/paoVx9eekemx00fEMvJTBNlqyIaU3hkY703z6PCEebwMB1LGeVtwzYc7u" +
                "dNHfOGzmWJob/In2cOHSHUEV1bgBsqjW/WYQSmbSTycip7meAbjvKW3aYN+46fxPdkdjr/72/gil3VL3" +
                "lN4KOTxZLQ0dLTfkQ2xMbou4cbL6L3dVriYy8DYqGw/YYMp4TzU+7hqbQcY+FPUcw0fXmtn1jC7O+sOH" +
                "J3bfPPF1/8QfZfGUuudNe/Vg3ERfaeSWHf9aUNIj2lsPy3eZq3PbLNTRsGM8Ptgu9Ssl9n4+T6m4GXUy" +
                "uie8k65ju6baS72I5VGJDjsa73vHpoWB80T4ZRCOh7JBPuJGpOY7haMdO+QQEXH/oNwcGncLYZFcJzGI" +
                "+isY/UlSPiM+TODV67cgDn3gAyUkKLsSYgKua/4T2rbi13PTbo2pEslBktqGAboM4nghi745Uk3Wt9Kt" +
                "hcEBMVJwE7KxZps01YIKnPpwe82T7RExrYGBnqN3b9auK/JjosvbQ3L8psPkUneex8xpH/ajDpTNyMVC" +
                "KBCB6CP0cxLBiPAefk2Q7bEQSefglODPlJcGC5HLhh5//yR4wUBr5BBDd7amKHoLyfQeUeJdm7Q3g0sO" +
                "KSxjjc21LmtUlsPG8CqsTvvtd8Mp47l3ZQJ39MzWbbXPm5FNe5HF6fW+iyWexfOLrmEceA8vAztesZa6" +
                "2sn8NuWRKIgbNmFy5rEcyBT7gZY+DsmMi3mz79uydecfl1S9A0Y27yOUW4toVl1s9Y68V52wXzsa2GTb" +
                "SwLPmOnInuAra41xJIrvDi4yDXfEQDaPDIfd4V7xAoFhXRJg59TQYtAWerKL1E3TlSZPz5FPFM59CdtI" +
                "2vu1IyNRzgLGw7IygsHEauktKR92vGintIGcAQvMu7ZfJ6GyHcYcDU/EHkq5YhPWvhZUgePt7iZy9+52" +
                "vuHM4brVevAnaiUOIi67KYvFmEAjBD8qNQfL3Cx0N0TVDZ0Dc6F1NAmPstzGDVjMMYI3hGTWoJnEEp3l" +
                "zBQofYErlQ4d7ZJCR7O3ZoOzLLTsUA9ED/AZJC10pGu94Z++Cye74odhCSeTK/OTvoJy/2jG5WaXX4VM" +
                "HPJlRWviImqWcGftY4LuHSMuwpc3JoDo1BJC/LtpOqxwPx2KROzYBpklthGPxi8JbHqTsZR9UPLeGso8" +
                "DBV364I8MtUDetxzNQ8DpjmOFGlkLUxLP9zEYhc430z+0v74SDik1CFXbaiHphR6PE2TRYRaNKZfmIKy" +
                "oQwmhjnIA0JnqCI60AwJ3IGnot+TPB6D8tHDjOWTJf1D4tTENDu2f1KakR1iIYJIMzo3k1mij/N4i6tg" +
                "Y0sLJYudtD7pDY74gpYpYxXjaHJY52NxOPCh6FzUkUrOhsdXeBeXPvvFJe0FQmETt0ucjnShfXTEz3ON" +
                "7FELTccePMouedFPETWGiq+nWKXipsnWKZ6v+CfHUcDEDiX+FN03P+/VqXqUqQ/4+ClTH/ERl0BX56+u" +
                "Xr+ZfTzdezAsJsOD9/0aOCRMtlPP+w/T3N9aSBgA+h7zOOYz/kl3/ydfccb4c+3ekMkEpBb9E7j/ZUxs" +
                "JQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MotionPlanResponse")]
    public sealed class MotionPlanResponse : IDeserializable<MotionPlanResponse>, IMessage
    {
        // The representation of a solution to a planning problem
        // The corresponding robot state
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart { get; set; }
        // The group used for planning (usually the same as in the request)
        [DataMember (Name = "group_name")] public string GroupName { get; set; }
        // A solution trajectory, if found
        [DataMember (Name = "trajectory")] public RobotTrajectory Trajectory { get; set; }
        // Planning time (seconds)
        [DataMember (Name = "planning_time")] public double PlanningTime { get; set; }
        // Error code - encodes the overall reason for failure
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MotionPlanResponse()
        {
            TrajectoryStart = new RobotState();
            GroupName = string.Empty;
            Trajectory = new RobotTrajectory();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MotionPlanResponse(RobotState TrajectoryStart, string GroupName, RobotTrajectory Trajectory, double PlanningTime, MoveItErrorCodes ErrorCode)
        {
            this.TrajectoryStart = TrajectoryStart;
            this.GroupName = GroupName;
            this.Trajectory = Trajectory;
            this.PlanningTime = PlanningTime;
            this.ErrorCode = ErrorCode;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MotionPlanResponse(ref Buffer b)
        {
            TrajectoryStart = new RobotState(ref b);
            GroupName = b.DeserializeString();
            Trajectory = new RobotTrajectory(ref b);
            PlanningTime = b.Deserialize<double>();
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MotionPlanResponse(ref b);
        }
        
        MotionPlanResponse IDeserializable<MotionPlanResponse>.RosDeserialize(ref Buffer b)
        {
            return new MotionPlanResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            TrajectoryStart.RosSerialize(ref b);
            b.Serialize(GroupName);
            Trajectory.RosSerialize(ref b);
            b.Serialize(PlanningTime);
            ErrorCode.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
            if (GroupName is null) throw new System.NullReferenceException(nameof(GroupName));
            if (Trajectory is null) throw new System.NullReferenceException(nameof(Trajectory));
            Trajectory.RosValidate();
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += TrajectoryStart.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(GroupName);
                size += Trajectory.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionPlanResponse";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e493d20ab41424c48f671e152c70fc74";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bb2/bRtJ/LyDfYXEGzvZVVto4zfV88AvFlhO1tuRKcq5pEBAUuZJ4prgql7LsHp7v" +
                "/vxmZpekaKXp4Xni4oBzi5jkzs7O/52ZXe+pyUKrXK9ybXVWhEViMmVmKlTWpGt+KwzeVmmYZUk2V6vc" +
                "TFO9bLX2eGZkcsxcmSymQYyZQlmg0a0RPY/pURV5+E8dFSZ/CDCWF37yPDfrlVpbHauZyas1DtZ2Habp" +
                "gyoAZMOlVqFVCShhUn9Za1sctmyREyzjCDIAEdZujexy0bZKZlhgncVC1KQcqQHR7GtPQJFgzQOrI/Bl" +
                "D1uz1ITFq5clgQGN04RenoPuyMRaHSmd0YNlKs2dzsEBqA0taCHuZmGSrnPdusJYv+CZZwyv6TGgua1n" +
                "rdP/559nravxmxO1xKJJESzt3D6vFPOM9ZBYtdTWhnPSJkwgyUjYIHkp1hBOzboQ2VfqhUw7usNfV8Ym" +
                "BGjJbpLCqn+aJMOvMItVmmS3tgXLsmCRV/+eBsUsGC4QawElPELiCwu1DB/UIrzTarlOi2SVanU+vFBh" +
                "DnNY6SiZJTCZhYY066ivCBZwtSV4ehCbWdBYrFsUYbQAlsikaWLZ6qdkClYdhH4Mlm8NLIG4UGRSXgaH" +
                "LT//zE8f8uwPH5WfDY26ocBhpnUv0nAO6cZJBOHC0jYLDaw5UEMNNtKZVnjAwlM8ZIXO4ZgFSIEDhCpO" +
                "ZjO1SYoF7IrkUJQUGkbC871OSa3GFvCiZLkyeRFmBdvhAnpJ2cjBTcnq1MQJbPHA0wPAzDD+KNVhvgsY" +
                "C3GgENs6OUEo0CcnNbefaqyn1XoVC69JIcQXNZM7bE2NSUFsQMx9OQfYbYKlA+D/sPQCtsCFSWOrQHlI" +
                "QoCfRnkCnXBEYu4kSOqCHhBAEJXYfXJoHSISH+io1p6LdeUkDRnKsDrI9R2FK3xH9MsTSx4XHRI1sZ4l" +
                "GYn64QQI1F+2PI2XxLvHEsaEYHnYrkDvdGqipHh4DPrcMvBze8geWk3RM6irEO5JHqtVSm7m4q5DMFjS" +
                "7MFhhxnrVbxgxjpLIAUyuBhbiTjpFG/wKY7PIoiFDmOyVefHEi854GLdzSIBxmo9lppVG7g6bB4WFuu4" +
                "o7qIrE0YYAelBv7j1cjxQzzJT6Ulyj2F1hQ2mjHQJhY0i3H7+BPmefhg27wCuRGrcYXNbFvCTAypXTY4" +
                "E5JtExXL8FbLJAcP3snCzIo0GqYd9Y+FzpTuzDvqwaxzH0WZi8wAodNPaC00i1Vi70162aYpKgozBWcl" +
                "rit1Mt1KL1fFg7NGkp5wI7qt8W4XZp0isnocLCeb/IpwD5YhSMFT8xre7DLofINVwGZpAyWZNeGQFZRE" +
                "S+oQFUAmGuy0Wq23YhxiIy23xyOosv24bRiv3hFqn7zB1z6JBL5gTCliCShCNQUTRJUsDvMYEi1CDh4c" +
                "cpM5YupRqkEkMbtcQXkSWh5WxHglzzkiOGUOD5IWUfw1yyWkiv1CTHZrvlh9yIaYROsUkToyMPUkI/BZ" +
                "DrERdpIx5U1ZpFX//IRtXEfIk0AQHDSLKE+hEN0/V601VHX8gia09iYbc0Tb0Jy2KL94GSL0PaWNRGdo" +
                "KUz9RZjrADdFXM6ekEbRtwCviDhYBCTolYEfHIDy64di4TbWuzBPQqSWhDiCBIB1nybtH9YwE9knsIbM" +
                "ePSCsVrj96DNSrzE01G5I9r1HAIEIJLcOwQxDl9sqoiEsN80meYhUkWOVrxka++CZCxexBqhyLntoT5T" +
                "ZW0ESfw0m9zjZIjsc/Qo0Zd9iazUBRz2zzLsxXqea0RgQM7wEBvEGuCZYZczG59FgMF1VCC5JbBqQQmu" +
                "fdlNrF0vyaDJdEK/Z5Dp2gdb6GWtkOBwCdfIw8xSFipz5rqo9iSgDVPjVr/FRskbp4oWSF476oLC8z20" +
                "kyJuSekCtboNTCqJm9H5Be+zx5RXHtwjgOL/cEM2QbsizMdqGaSoSpGvZut16kSQ+JUnwCJzaZfZGgdW" +
                "gfDYYLuoEMhCpmF0Swxv0fDfrfVpt9ZNjtC4+N1bqwf/T9paP7WzSl1E021rrlFLFKjSOYJMvAkDqjTn" +
                "R0Ab5EoEQL8bY/9gMWFQ5PXl4t4n6C6z+7K94ZLNMrJMdbHRMI1iYx7tm6xCinlIVcMIwaz1jjsFxzI/" +
                "Fcf+cY0JeUYxIDcSVZ+KT0fOLi5DpEM02GBBleGY7WqpqSaEZZUzucYkywEbHXKznEs4FPuFig1EgpqQ" +
                "YxkcjjYbzq8pKMMo62Khz5hyQC7Xpho3EyjaMDhp4TQHETtP5kncDKYc/h13bVXMXsCw4VhMsywGLQKJ" +
                "F/hhR/Vn7KYbYohd3BduVLI5ujgLKIxpU2rlUGxL9JpdyXss+iAFvAWK9/2f+/KpTDPVr0+k7crQdioc" +
                "e3lOOYpIcEvt9PZLZaYk58/xVD5tnsxpKX6UnPnN1lZ17DZL09zcwqigLjI0Sx0a6lLQ9htmc86DaQNB" +
                "4PNO60Cqdwf3VAxKMNylOyhElFTx14Z3gX7eiYhHSvN/H5eMrHqVxsTT9BU/0RITlrVqfBafnta6Srx7" +
                "oWBK7n3/hByYN1FqwPk0mp59v5dFWTYtuUeFPBSJgV2EKKxYUqgV8UQJbvGYitaexIh6y4/AeME9RBXg" +
                "Y4cijRGzjDIzrlnYQfCivp0ukPpRiHKNvr1d+HzbT8qKko1SELJA3GoKSpD6nrnrOHFj1aWa9dacb2JS" +
                "qxQUmY2jwayxAe/Rwugshcju0bD2hAkZlKSnqAPjB2mBIE0QSt2EKmVgZIG0dmlHmakIuSxU4Kjibg2y" +
                "7ki6R1vVKbTBhYLog3YiYZJ3oSXhKFkBareBILppiB31vOxqUYr6n7YS9P7ZFxyWw7Zvk/EamY4olKPN" +
                "T6vlGgkcTaOyj1IrWZjUB5y+n1w7qqh6hbUDgxh1PZh3S3ilbDQq/DJdbSiDjx9uM7Mp2xUO/mnccoc7" +
                "dl0a2K7OXspes6/p2G2aWaNwC6N3nDoZHrABMS4oUA44ygMaFLxunlc1eh5iF7RJT0NsxsYJqHQg+R1Q" +
                "326ecatHmBEeJoSB0FSnUL6J74Iu9RV3ZPPebWVawuZIkDCWZludc/TaWYMXwNik4N8vE1HbdgkC7yAs" +
                "DjpCJ0Nd+yHqWFVgzZzDbo2TaQEIK11pu9jGSl8Au5SBnXhorELxmvyDT+1QA1N7H00glyHYiru2mroD" +
                "HgbzNaokFGB+Lc4GjcUx6wIBlpY4rNNGZ2fMCK/0CSZprKKuG1OtXzMMkXp5DMjHGZzb1YBgo3eJWVtk" +
                "gfoeOQORj1RTtlSOOZ3W9AFJfPf8/PRrWmbEcXVrpVluqK2Aoiu7S3KTLSn3pZ41gsQDpITaHB0kcQU+" +
                "jSrgz7ZhE0l8KCuNelfDd73Tb5in1YpCFaWw3ppdz8PFVia6PM38bV59yi2TPJ/QQsXk9XVvcH76wsXh" +
                "as3dy/EqbQTGjbN8p2rO/Q8IwuvNl7HLtS0IItWzQkpUapEgoOGclWQF0fqIUQVUnJZAkrGQyLI5JgKH" +
                "K7Q2fYYPnHilZNQDGj/85eLi58MKwuO//aOGr7/vnU2oefrvT3Y/JJ+z3z595bjJhxJ8lu1jGSIZ9aqo" +
                "hEVuwJ06Sh2hRZ1Te2QuB3pl90BOlmAqdH63lVrc6vK4qL7CCX+R+dXRDteWbDEIWpmKpz7eA4tHGE/r" +
                "pLhtlttn34+Hg+fobPue2vvu1aUSBDjeKa0Ykbb0gVrRSbHaS2XrAgLW9XtKR/U4d6BDoUdaZ1fino4x" +
                "t8habvWJ+tO/9knC+yf7Z5TfnL/eb6v93JgCXxZFsTp5/hylSJhC2sX+//xJWMTGAXtHPsgNvcwFR9Ge" +
                "y3FIOTUpUP6YFPuYlCDrhyPcau066rMU3jpNUpQ7bofaZbB0zCpC9EX0+WuxDUZCXJHru5WlFUbGtYac" +
                "KMpJY5T79dQodczyKSOjOVGlAPgbiQDfmiI4+fZv370UCNp9pWcAuMcU77uVxj9e4lQVWQIdrZZ62lp4" +
                "/Ev61kMIbl5K7W/m9viVfKGz7BP17cvjF/wK6JwAkESbjYPAzr9BM6fxmZIUYsQv4I/lZXRp4nVK49wn" +
                "KMxq3xs0TPvLdew/lTJQmnYunjo1aA3bFRlbW0UPyLE5dYPFaeV6jr7cgWX4g2NYlu81Is+Z+kQAyCjs" +
                "08bOzigZ9Ndt/IemAB39fKdeD3/CZibP4+u3vVEPG4y8nr2/7A/OeyMEdPdhOOidvvQO70MU7zVEk4OS" +
                "XM1HBZyeoL5wF0Yq0OrgroLwc6hVReTXJ9TATqT9S3UL33MQIciGTeK698Fqv5qzL1scX8pwxSEYZ1Kl" +
                "jPiprd5LR//nOs0kZK6cdDZHyugoaoYhqp9K/iD0TiXb4CfkJdXb+1LW9PYz7eU1kkT+jirugJHaKXLi" +
                "tzt2RwAVOhHUKBoL3zjqT9ZEgqt3xII8HYI3GHXP+zdjypNqa3olM05SsBxTilTEdLgVwY1EnyTyeYxb" +
                "6mcVIu3oqKqDuIU3eNvrv3k7UQeE270cVjzJvZKaxCueFlt1lvcFdUC+cCjrUajz6wh3bh15qa3zqVWo" +
                "s+hlJ+pzJcruNbFrS1fAD2F+le03fZLOfpKca2FusOIEclXZEMuU5lPVSfa+XrXdSddXTqjeSRvCLE2q" +
                "wTxlpZWnPgKuBAPAJ+qEUTEgVejja4eUlTZbYawwShJkXG7AkMBrHU+0tqV5W94MqDXma3BPxyOIKZt9" +
                "Wx2q+iUetEb8QWjF8Wdas0+xF1GhWe5ANWqpuqRIgWJbvBBn69kc2cTfa6H2LkxxQxPKndGlAVO7Hwg2" +
                "6dQTiY/98LFFi0wcAj5icrhogVorz8/wpRiyQFwaJQCmZofc+TaITHoyaXlGdknNc4akr6RLLll8OBZS" +
                "9X3AzcEnIphr9d3XAeS4HF4nRX/VFCg7B+G9+oragl+p6Ff8E6tTxWV2qE5OYel69uHrj9RpLF+/odeo" +
                "fH1Br3H5evyxPIv48PIjf/tyMvhMd+9Zo9+185C0McdbHDuy/cNILwMOX8mrgF2AqW7boS9I5WDplB/a" +
                "/owFo3gJowiNUinE7UfSldmGlltVH8uOem0t2d/0PV0tohaFS07LjomLDLQl+oYENQ7pah5ONWzjOJvj" +
                "RYPNDnhv7bgIZh/fBKMbqtXHLbYe3xGL174zgYQgoPaQu6n+dDey62b4uS602GPt8npzwtbtn9rE5n3o" +
                "Goons9xP0PZs+36oS2z5hjMbkLuJ1TildzdutGw3fMGNb2ftsE6v4x1d7V2piLv7eQRh+dOoCteBXP/0" +
                "N1+b94EOvSMKRHVXqYaCj31weS1dx8SFu01Sy3Hs88qSn2/b7567o+x8hr3I1SPuU4mt5k9SJGxNEtfz" +
                "YNQzp5Ky6YeMDJnnznj4CXX+UXHxt8gp42NTs9Q08VZWv3l2QJmGUa/orw8Of9/1mLIv5FqrYXWEXQVC" +
                "b53ITOfUoKpfc/rEBZtaVHu0BIynZh7/t3W2De0PDozNv5V51pK7qdhISAr+r2ymGq2OxORudHxzdtYb" +
                "U0Uj7xfd/uUNmhh/o5+W+3h92R0M+oM3AY32zk+PPHR/8K572T8ProaT/nAQENzp0Qs3WPsYOMDupHce" +
                "vH4f9Abv+qPh4Ko3mARnb7uDN73To2M3DXXWZDS8LNd66b7fDLqvL3vBZBh0f7zpox4f9wbj4SgA0u7p" +
                "0bcOatK/whLDm8np0StP/ajXu7rGyqdHfyVJlOcIf66uQ1r/t1LyF0bWS2fSHU0C/DvpgYXgbHh52R+D" +
                "KUjg6x0g7/rDS/weB9fdyVtAD8aTUbc/mIwBj9pSJrwZdi+byF7Ux34Ly3EdsDbkJ5Fu0GHa1s6b0fDm" +
                "Ohh0ryDlb75tDjYwAeRVA2Q0fD10LJ4effPXxihK6B888u8aY9Ls96OwJzR75ArrlpgvRgAIQMBgfDEc" +
                "XbHqyQiPXnhDK4UFc+md/UC2CHt4BzgyCgB6CdZopX95zAvNGUx/cDEsx7gdVzODLboGw6D/QzAeXt6Q" +
                "JcNEqe7/X9RtZgEGOAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MotionSequenceResponse")]
    public sealed class MotionSequenceResponse : IDeserializable<MotionSequenceResponse>, IMessage
    {
        // An error code reflecting what went wrong
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
        // The full starting state of the robot at the start of the sequence
        [DataMember (Name = "sequence_start")] public RobotState SequenceStart { get; set; }
        // The trajectories that the planner produced for execution
        [DataMember (Name = "planned_trajectories")] public RobotTrajectory[] PlannedTrajectories { get; set; }
        // The amount of time it took to complete the motion plan
        [DataMember (Name = "planning_time")] public double PlanningTime { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MotionSequenceResponse()
        {
            ErrorCode = new MoveItErrorCodes();
            SequenceStart = new RobotState();
            PlannedTrajectories = System.Array.Empty<RobotTrajectory>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MotionSequenceResponse(MoveItErrorCodes ErrorCode, RobotState SequenceStart, RobotTrajectory[] PlannedTrajectories, double PlanningTime)
        {
            this.ErrorCode = ErrorCode;
            this.SequenceStart = SequenceStart;
            this.PlannedTrajectories = PlannedTrajectories;
            this.PlanningTime = PlanningTime;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MotionSequenceResponse(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
            SequenceStart = new RobotState(ref b);
            PlannedTrajectories = b.DeserializeArray<RobotTrajectory>();
            for (int i = 0; i < PlannedTrajectories.Length; i++)
            {
                PlannedTrajectories[i] = new RobotTrajectory(ref b);
            }
            PlanningTime = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MotionSequenceResponse(ref b);
        }
        
        MotionSequenceResponse IDeserializable<MotionSequenceResponse>.RosDeserialize(ref Buffer b)
        {
            return new MotionSequenceResponse(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
            SequenceStart.RosSerialize(ref b);
            b.SerializeArray(PlannedTrajectories, 0);
            b.Serialize(PlanningTime);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
            if (SequenceStart is null) throw new System.NullReferenceException(nameof(SequenceStart));
            SequenceStart.RosValidate();
            if (PlannedTrajectories is null) throw new System.NullReferenceException(nameof(PlannedTrajectories));
            for (int i = 0; i < PlannedTrajectories.Length; i++)
            {
                if (PlannedTrajectories[i] is null) throw new System.NullReferenceException($"{nameof(PlannedTrajectories)}[{i}]");
                PlannedTrajectories[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += SequenceStart.RosMessageLength;
                foreach (var i in PlannedTrajectories)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionSequenceResponse";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3b9d4e8079db4576e4829d30617a3d1d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+07bW/bRprfBfg/DNbA2d7KSmKn2dYHfVBsOVFrS66kZJsGATEiRxLXFEedoSyrh/vv" +
                "+7zMDElZTrrYsw8HXFo4JvnMM8/720z2RScXyhhtRKwTJYyaZiou0nwm1nNZiLXK4YfR+Wyvca3vVK/o" +
                "IvA5wFpeF+G6vcZeY1+M50pMV1kmbCEN4YBfCiX0VBTwyeiJLgQgxQcC8V+s+n2l8hjQDBFmRKv8y4hA" +
                "yx0KI/8BJGqTAgnF3OFbZjLPlRFLo5NVrBIxBZbUvYpXRapzh3jsl24+f3ErkqiKr9xFLvQqZwLThRIp" +
                "7KL1LfwAOS2WmQICcduFRvSEa68xzbQs3rxmzMB+hEv3Go32//CfxvXo3RlsfafSIlrYmX2xrZpGmhen" +
                "J+JOZswRfDYSFDNRc3mXarPnAEYfzs+7o1H7lX9x2eldfRh22z/iH1zLr2+uOv1+r/8uwu/di/ZxWNDr" +
                "f+xc9S6i68G4N+hHCNg+PvFfK28jB9kZdy+it5+ibv9jbzjoX3f74+j8faf/rts+PvXrzgf98XBwFbZ7" +
                "7T986HfeXnWj8SDq/PKhN+xGo25/NBhGgLbTPv7eg41717DL4MO4ffwm8DDsdq9vxojubywVryfxH+I2" +
                "zdVCFmlswQXA7mzB1m2DpMad4TiCn+MucBKdD66ueiPgDUTxchfMx97gCv4eRTed8XsA74/Gw06vPx7B" +
                "glelYN8NOlfb+E5qH7+G6LQGWfnmV6GmXpe7eWW9Gw4+3ET9zjXI/NX3D75uIQOYN9sww8HbgWMVPv9t" +
                "+/NVr/+zx//D9sfB25+652P/+UfWhd3YQi22hH45BJgIyOiPLgfD68hb5/HJq9JSnODAiLrnP6ONgo18" +
                "BEA0FIAM0qyQjD/pYxCgM6Ne/3IQPr5myiqmUaeuP4h6P0ejwdWHMSnu9NWz+HoZIilUpVYslLVypiAy" +
                "5YVMcyvSHKIf0gyhSU70qqjEXwrJTZG2VIsDp7YpAlqMdWlhxT80sGeFzBORpfktsGtVbiHQ0+4/4VcO" +
                "0AQYET6W1E+8kmLyQm4ExBqIkKusSCFgiovBpZAG4vpSxek0hRA9V0bVsV8jMABWdqH1UaKn0YP9OkUh" +
                "4zkginWWpRa51ROM5VYcSv8NIrbVEMGRFwEQQRJHew2P4NyvH9BySA5+eRRQRw41b32ZyRmIOUlj6ZKl" +
                "AsQGsIM+bKxySBkW957AL3mhzNJAykiEBMGKJJ1OxTot5hBrUBpFIFITElq/57WLCta2yDYiXSy1KSSk" +
                "JUxuc9BQhnsjR4HdiU4wLx56ggAw17RBnClpdgFTgpgCXWxlZ2exNursrJKKJwo2VGK1TJhbyIZEflGx" +
                "PhDnROsMyI2Qvydzhd3GWBGWDP5AhjjXWWIFEC5RCJAbY5NOlK9BuDqRgJVzvTYQ/MmRDCgeRMTe0BKN" +
                "fVcYhEUKZMifxaFRdzpb4XusQFJL8eIIqUnUFOIHiHpzBgjEX2s+5+sfj0UmiGBx1CxB71Sm47TYPAR9" +
                "YQn4hT0iXy2XqCloq2DuUR7LZYbeluZVBP0Fru4ftYixbskLrFjlKUgBDS6B8o99dbKh4JDLhXKCmCuZ" +
                "oLE6d7aEnYolrBvnaTyv7EdSs1BOGqwxwcASlbREB0qSbRjADpRqcCCvRgoj7Ep+aagigRzas+X0U4+G" +
                "NrWFdbbtw5A0Rm5sk3agGhFZX1aK0QoxqHbidaZl5hx6IW8VL3LwwDtamF6iRmXWEn+fK6ipW7OW2OiV" +
                "8fGUuMg1IHT6kdaCZiVGBedMatHEJSKWuQBnvVNVdRLdQi2WxcZZI0qPuWHdVni3c73KEic5Lyeb/gGB" +
                "H1gGQTKeitcglM5B52vYBdgMNhDIrAgHrSAQDYI2oJcCkzhpsNVoNN6zcbCNNBq2MBA3IK6S/bg6GWtw" +
                "5wiVV97gK69YAk8WUIqEownTDGxARMkTaRIQZyEpclC8TWcQUI8zBRQip4slaI7jymaJXJfCnEH8xnp7" +
                "I1aWUxA0DQsQaUxyBHutrWeTl2SFabzKJDZjYOdpjuBTI8m+Ecz3RKJ3cUYGTv3NHflqHhslLYbn3oVo" +
                "rLhGgQWN/fFaH2MSmmGC8puH+KDuITdZS8kJY9RfmbkW4MZoC7uAdR/SuwgeIdzAJkCCWmpwgkOg/GZT" +
                "zF1mvZMmlZOMsl8sKYIe4KKDowrmnFDnMtcePWMs9/gzaPOAF3k6DunQrmYgQACEVvAuTTh2kZ1CGATj" +
                "zdKJkWbToFBFWzb2Lw0FElQfaQTDZt09nQmzNqI0eY709rAaAmaHCtUFjEifQTgjoYm6UEOeGQJeomZG" +
                "KQqDU/gl0RBlAM8U8pte+/oBuFvFxcpQZiv347DaK5xAVgu0ZrQb6bMF2q2r3SkO2CUpFExeYqeeW6xE" +
                "ec1MFWU2ArQy0273UGKLeA4FbEtcUvMusdFuomNAoyaNT12S4t2H4cUlZdhTLCwP7yF0wv9yjQaB+RBs" +
                "xyr+iPEUY17F0KvUsSDhL5MCFl6L+aX2HbAyhMcGhgtNNZrHRMY0GKjR8P9J9XmT6tpAXJz/6aTqwf8v" +
                "JdXHcir3RbjcNmYK2ojCbDiAjL0JA1Qw5wdAa1AoAuDfW9/+TmKCjyyvpwp6j1DtJWl8yHPuEMLKRBVr" +
                "BXZRrPWDjEn6w4AHziRjsOXGR5rynfL6jL36lxUsMDkGAKM5pD4Pk46YHSxKKIHw2xb9IgRisqiFwj4Q" +
                "bCqspMYSbQZ4aKGDGeramtitJRrkAX0gRbFbRVmG3B/D8cYHQ5YJvoYlh+hsTWxsc4bCVEG1ClU3EKtN" +
                "OkuT7TBKgd8x1xTF9ARMGlyKaObNQIWAxEv7qCV6U3LQNTJEzu2btYkKdFHyL7RuYkXlUNQFekNO5H01" +
                "zSElyQS07oey9+G3UFqKP55F1aWN7dI2hGWThnRe0zk+/V4aKAr5mwz539bP5KsUNBxbPsHasmut8zMx" +
                "+lYhk2RiFmcyOJPAlCvzGRW+mDQg2HlfdSDls4N7Hu44/O3QGqiC1VMy1wSnAuIp9SCDmHL/HIuErHzk" +
                "GcRzTBIfGX+59Lz1lv14UpkeUa6C3ii993MSdFpKmThs2/MlMz5QeqLRGYkyTCppHAWFJ1QCdi6hjSJJ" +
                "QVuoSMj0fYsQwMGxoTriQzjedB/CiYzZmVBnyDAhzbWbDrYgauGUThVQ7WFs8oO9/V0Y/ZSPy7XASxAH" +
                "75DsNbblpR21/kTJjZhoplqeW4Ut/eQSR6QSS3NHhl7Fc0QBmydqKqHiEseBOCYFi/MMmr9kw/UZlAdM" +
                "rVuwV9YKhC5yc11EC9KKoYwFZTjKqJqEgjvmkVGtKwW9UI/AmsFUxKxSGlogjsAO4nYpBEKcAvlDF895" +
                "Lc40tanS6BW5hUNz1PTDMdokVzEGc7Oh7YzKuLtFxFRX8daoSEAapsnhNG9TGRGW534gEiQwcpuU2lkr" +
                "aO9DubqlFcihU3Gb63XlkI8XPMvE/6F/dlwV2OQBw5SqBTdl9i0dOdFevWos+QUfcLw6QR6SKRE60CMf" +
                "KB4FH06TcqlX+map2EIwX0+kpd6RpFS6FP8SYYsxy2nUwywxJ2NEgXhK5OUo34VibIJ2VPXel9261Lig" +
                "ge6zPVqnYr1y6lBKYqQzEITfKcbZ7SLFgQqeemA8YmoJ7MZ/w7lVBW67DLE1gMipAXe7Vna+hRlfAfjC" +
                "fdmJCz9W0bxFt0GlYFuMs36FRYILeIHPppisyqNx5dtWLjZADCv2QdBfkqTcZZEIj2r03eBad1j+OLP4" +
                "sUphJ0ls1bacDsJ5Kx1wUOFXAQLbvUv1ykKJqO6hqkAW0oKDOMcjUPZkA/V95+Ki/ZJ3GlL0rW02NXrB" +
                "g9P8LjU6X2BtjC23wc7rUEHXvoHgRV5CB1UFeLrdMpI0OXKbDbvXg4/d9ivH2XKJsQyr3DxwRwMRF4CJ" +
                "dOuH7V/n2FflvMhzC/qosHpz0+1ftE9CsC633b0jbdSE4Ll2DuH0Th3CIUJ4Ffo2d7GyBUJkalpwC3uE" +
                "e0HEszpDkYGEfUwpg26iLAg0cWSSiE6ZyMFSmdALAF54xMo1wGr//cli57eDTmP/X/4j+Ngah6v/+mL3" +
                "hwV0/vVjWoqrNF6ZUn50gQ6iHA60sNW1igcwWG6CKpXBGcqMz/vCiIEPngTeS9l/UIvcqnCgVN3kjN4w" +
                "inJOZbxtzSCe5SKZhKwAaEqcyaRKkEvKNGn7aTTov8BrM2789qlzfSUYRQvvH3mrSkqPqHSpGM29bMoR" +
                "o6sEfOppiS7VGmm+Q/vkWTT/wSs8WXqrzsRf/usABX1wdnCOJdHF24OmODBaF/BmXhTLsxcvoIWRGQi9" +
                "OPjvvzgmDRVbuebpX+7CJmvRVUWopIocsPJMiwNYlMbUcd8q5Wbv0wxcd5Jm0Ce16rm1Zrp4Gsty9I33" +
                "xVs2EsKCfGEgcFvz4IzMbAWywtDHc1R7RufWQKNjmJ4FYToTQQr8EgUBL7cFcfb9jz+8diCYqHnYAIAP" +
                "yT7wu41+uRKgP6vwIDboq7756PfsvQdx6Gk7cbCe2dM37hUefp+J71+fnvAzLDAIkmK17GGgVFhrk2y/" +
                "x+IGGfK7+NN893mhk1WGADRoKPTyINg4mvtTzfofqzCApgt234m+h8ZyiZbXFPEGSnSq+mIctLpppe+b" +
                "jAqHzWBmfkoJhdHE1wuADBMC5n/yTa6/Xzbhv1aDTox+EG8Hv7Zfud9HN++7w277xD2ef7rq9S+6w/ap" +
                "fzHod9uvG/5enotblIWQJgeF7xseKEkhHVt/3aQELQ/7Sgi/BkddSH51QQXsjAfH2PnQ3QgWAid0FNe9" +
                "D18H5ZoDTn4NZ6P4FRgnUrkJ+bUpPvFZwG9VmlHI1HupfFaEYXUtKuHENk1U4A+E3iplG/3afll5+hRk" +
                "jU+/gairJLH8HVU0QUO1YyCFv92pgsUyiYMMxWfm28gkXSEJrlliC2rV9BoNOxe9DyOgp7qnVzLhRAXz" +
                "6SZLhU2HZho0iPS1JJ3kuK1+ExIKkpYoJ5A1vNH7bu/d+7E4RNzu4ajkia+iVCRe8jSvdWjeF8Qh+sIR" +
                "74dRz+/D3Ll9+KGyz2O74GTSy47V5/qa3Xue65yHC/4T3ioMvcG2T+KpUWqolW6xy6TL0oZIprgeO1a0" +
                "99Wy6c7IvnNCbWx5opNfMKkt5sG4Kp76ALgUDAI+TYh72C9Q92oeHGJisbo9UCNtYcHA3/nKDEq7MjFt" +
                "iQZPfsNVgspIvwL3XAymeRiH1oZc1Ss/knVcZ/cbc92nT0HYivrEUyEVu08MENCVs/OZVOYzqCf+sxJh" +
                "72S2UtioTfGKga5cKgQe8ZgUih/7+UsD9xg7BHQm5XA1XPBwo0C/wndot3j3jO9bIzU7ZE4XR3jRM4nK" +
                "s7FDZJ6tA1sSxfcxPp8yneo+orHis1BLPfzOmwN8sq6abhhQDgvCREHei+9wkvidiP+AH4loi5eoLCnO" +
                "2mDgavr55RccTobHV/gYh8cTfEzC4+mXcH7x+fUXevdUAvjGIHDrNHXnYerWEm9o5LxPprhv0O0jDF0u" +
                "KGFdRCnvDaiU2sHgiJ+b/lAGvsKDjGOVuUbcfkEt6To0X7r6Eqbwlb04lfE/rlBJy9ehYXjiogFmPz+V" +
                "wNkiXjIw2MnUz7wpRmxx2QLWGzvuidmHF8WAncrLGlsPr5AlKz+agNwf4aSI/3XJs93brhjgtybVbIkl" +
                "1MPZdu1+UGXl9pXpKo5nstpHSKvfcnH1K91+JuNx97S2TvLdlRzF6YWuv9GNmB2W6fVrty9L7C463A2W" +
                "YxCVP8AqcR3yJRZ/f2f7wtCRd0KGKC8zVVDQGVGax9kqUTRqpesmlWrGviit+EXddvfd9WXnL+RBru1w" +
                "rwK2ii9xL1BbxG7nwXCYLly2rfogIWs9cqvkEW3+7wTErxHjdbKtVhySeBOr3ks7xLJCizf4jxOO/tzl" +
                "mTAIcoNVWR52lxHQm6bGOxJb17wfuX5TCWcPtshrce3f26duZV+JiP8EMZA81xs4AAA=";
                
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupResult")]
    public sealed class MoveGroupResult : IDeserializable<MoveGroupResult>, IResult<MoveGroupActionResult>
    {
        // An error code reflecting what went wrong
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
        // The full starting state of the robot at the start of the trajectory
        [DataMember (Name = "trajectory_start")] public MoveitMsgs.RobotState TrajectoryStart { get; set; }
        // The trajectory that moved group produced for execution
        [DataMember (Name = "planned_trajectory")] public MoveitMsgs.RobotTrajectory PlannedTrajectory { get; set; }
        // The trace of the trajectory recorded during execution
        [DataMember (Name = "executed_trajectory")] public MoveitMsgs.RobotTrajectory ExecutedTrajectory { get; set; }
        // The amount of time it took to complete the motion plan
        [DataMember (Name = "planning_time")] public double PlanningTime { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupResult()
        {
            ErrorCode = new MoveItErrorCodes();
            TrajectoryStart = new MoveitMsgs.RobotState();
            PlannedTrajectory = new MoveitMsgs.RobotTrajectory();
            ExecutedTrajectory = new MoveitMsgs.RobotTrajectory();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupResult(MoveItErrorCodes ErrorCode, MoveitMsgs.RobotState TrajectoryStart, MoveitMsgs.RobotTrajectory PlannedTrajectory, MoveitMsgs.RobotTrajectory ExecutedTrajectory, double PlanningTime)
        {
            this.ErrorCode = ErrorCode;
            this.TrajectoryStart = TrajectoryStart;
            this.PlannedTrajectory = PlannedTrajectory;
            this.ExecutedTrajectory = ExecutedTrajectory;
            this.PlanningTime = PlanningTime;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupResult(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
            TrajectoryStart = new MoveitMsgs.RobotState(ref b);
            PlannedTrajectory = new MoveitMsgs.RobotTrajectory(ref b);
            ExecutedTrajectory = new MoveitMsgs.RobotTrajectory(ref b);
            PlanningTime = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupResult(ref b);
        }
        
        MoveGroupResult IDeserializable<MoveGroupResult>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
            TrajectoryStart.RosSerialize(ref b);
            PlannedTrajectory.RosSerialize(ref b);
            ExecutedTrajectory.RosSerialize(ref b);
            b.Serialize(PlanningTime);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
            if (PlannedTrajectory is null) throw new System.NullReferenceException(nameof(PlannedTrajectory));
            PlannedTrajectory.RosValidate();
            if (ExecutedTrajectory is null) throw new System.NullReferenceException(nameof(ExecutedTrajectory));
            ExecutedTrajectory.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += TrajectoryStart.RosMessageLength;
                size += PlannedTrajectory.RosMessageLength;
                size += ExecutedTrajectory.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "34098589d402fee7ae9c3fd413e5a6c6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+0b224bx/WdgP5hEAOV1FC0LTluwoIPtETZdCRSISk3jmEshrtDcqPlDrOzFMUU/fee" +
                "y8zsRZSdopWKAnUCW7t75sy532bUaFzqW9XPe1mms1MdKSMU/hiE8HOj0VjC1zgPlmZuno/0VOfjXOZK" +
                "5Jn8VYW5zraByWWW74KceBixSmSaqigoln1lgbpT4Tqvr5glWuavXzG6OJ0HebwEIjv/4T+Ny/HbtiiT" +
                "V5dRI07zk2NxK5O9xl7jmYDPmUwSMVULeRvrbM8CjK9PT3vjceele3He7V9cj3qdH/APruXXVxfdwaA/" +
                "eBvg995Z58gv6A8+dC/6Z8HlcNIfDgIE7Bwdu6+lt4GF7E56Z8Gbj0Fv8KE/Gg4ue4NJcPquO3jb6xyd" +
                "uHWnw8FkNLzw271yH64H3TcXvWAyDLo/XfdHvWDcG4yHowDQdjtH3zmwSf8SdhleTzpHrz0Po17v8mqC" +
                "6P7CUnFqEn8SN3GqljKPQyMy9dtamZzNzHhJTbqjSQB/T3rASXA6vLjoj4E3EMWLXTAf+sML+HccXHUn" +
                "7wB8MJ6Muv3BZAwLXhaCfTvsXtTxHVc+fgnRSQWy9M2tQk29KnZzyno7Gl5fBYPuJcj85Xf3vtaQAczr" +
                "Osxo+GZoWYXPf6l/vugPfnT4v69/HL553zuduM8/sC7M1uRqWRP6+QhgAiBjMD4fji4DZ51Hxy8LS7GC" +
                "AyPqnf6INgo28gEA0VAA0kuzRDL+TR+9AK0Z9QfnQ//xFVNWMo0qdYNh0P8xGA8vriekuBMg6gl8vYhy" +
                "QNxkERuxVMbIuRKhTnMZp0bE6UxnSLNOhZzqdS7yhRIZLhQGVzZF3FItervSJkZAI/RMxLkRv2pgzwiZ" +
                "RiKJ0xtg16jUQMSl3d/jV46xBBgQPpbUe16ZL2QulnIrINYosVwnebxKlDgbnguZKWFWKoxnsYrEQmWq" +
                "iv0SgQGwtAutDyI9C+7t181zGS4AUaiTJDbIrZ5iNDbiQLpvuRZGLxXxIgDCS+Jwr+EQnLr1Q1r+6bNw" +
                "ywOPOrCoeevzRM5BzFEcgpQhgmwWChBngB30YUKVKgE/wN5T+CHNVbbKFKQLIUGwIopnM7GJ8wXEGpRG" +
                "7onUhITW7zntooK1yZOtiJcrneUyzQWoF6SbRgnujRx5dqc6iiFHHjiCADDVtEGYKJntAqYEMQO62Mra" +
                "7VBnqt0uZdOpgg2VWK8i5jbOmfy8ZH0gzqnWCZAbIH+P5gq7jbEkLOn9gQxxoZPICCBcohAgN4ZZPFUk" +
                "BzIlZt2oHH+APA7BnxwpA8WDiNgbWqLxjLYoLVIgQ/4sDjJ1q5M1vs/EKosNxYtDpCZSM4gfIOptGxCI" +
                "P1d8jrZcKI9FRohgedgsQG9VosM4394HfW4I+Lk5JF8tlqgZaCtn7lEeq1WC3hanZQSDJa4eHLaIsV7B" +
                "C6xYpzFIAQ0uUmnOvjrdUnBI5VJZQSyUjNBYrTsbwo4FD9gX+EMcLkr7kdSM2IDHg9GDgUUqaokulCR1" +
                "GMAOlGpwIKdGCiPsSm4pbkEaBHJoz5bVTzUamtjkxtq2C0Myy+TWNGkHdCNS4wpKxKqEiRhUO/E61zKx" +
                "Dr2UN4oXWXjgHS1Mr1CjMmmJvy1UKlRr3hJbvc5cPCUuUg0IrX6kMaBZiVHBOpNaNnGJCGUqwFlvVVmd" +
                "RLdQy1W+tdaI0mNuWLcl3s1Cr5PISs7JycS/Q+AHlkGQjKfkNQilU9D5BnYBNr0NeDJLwkEr8ESDoDPQ" +
                "S45JnDTYgkr4HRsH20ijYfIM4gbEVbIfWybDo3OE0itn8KVXLIFHCyh5xNGEaQY2IKKkkcwiEGcuKXJQ" +
                "vI3nEFCPEgUUIqfLFWiO48p2hVwXwpxD/MZ6eyvWhlNQqJdLEGlIcgR7raxnk5dkhXG4TiBMhxrsPE4R" +
                "fJZJsm8EM1ibpqES/bM2GTh2IfEt+WoaZkoaDM/9M9FYc40CCxrPJht9hElojgnKbe7jg7qD3GQMJSeM" +
                "UX9m5lqAG6Mt7ALWfUDvAniEcAObAAlqpcEJDoDyq22+sJn1VmaxnCaU/UJJEXQfF+0fljCnhDqVqXbo" +
                "GWOxxx9Bm3q8yNORT4dmPQcBAuAq07dxxLGL7BTCIBhvEk8zCe0ahSrasvHsPKNAguojjWDYrLqnNWHW" +
                "RhBHT5He7ldDwOxIobqAEekyCGckNFEbasgzfcCL1DxTisLgDH6INEQZwDOD/KY3rn4A7tZhvs4osxX7" +
                "cVjt51Yg6yVaM9qNdNkC7dbW7hQHzIoUCiYvsQlPDVaivGau8iIbAVqZaLu7L7FFuIACtiXOMTDfgWoS" +
                "iFiSGjWZudQlKd5dj87OKcOeYGF5cAehE/6XGzQIzIdgO0bxR4ynGPNKhl6mjgUJ/2QxYOG1mF8q3wEr" +
                "QzhsYLjQVKN5TGV4gwxXaPh/Un3apLrJIC4u/nBSdeD/S0n1oZzKfREuN425gjYiz7YcQCbOhAHKm/M9" +
                "oA0oFAHw39q3v5GY4CPL67GC3gNUO0lmLuRZd/BhZaryjQK7yDf6XsYk/WHAA2eSIdhy4wPN6U54fcJe" +
                "/dMaFmQpBoBMc0h9GiYtMTtYlFAC4bca/cIHYrKopcI+EGzKr6TGEm0GeGihg2XUtTWxW4s0yAP6QIpi" +
                "4GqYZcj9MRxvXTBkmeBrWHKAztbExjZlKEwVVKtQdQOxOovncVQPoxT4LXNNkc+OwaTBpYhm3gxUCEic" +
                "tA9boj8jB90gQ+TcrlmbKk8XJf9c6yZWVBZFVaBX5ETOV+MUUpKMWsVM9s7/5EtL8fuTqLqwsV3ahrCc" +
                "xT6dV3SOT78VBopC/ipD7qfNE/kqBQ3Llkuwpuhaq/xMM32jkEkyMYMzGZxJYMqV6ZwKX0waEOycr1qQ" +
                "4tnCPQ13HP52aA1UweopmGuCUwHxlHqQQUy5f4xFQlY88gziKSaJD4y/bHquvWU/npamR5SroDeK79yc" +
                "BJ2WUiYO2/ZcyYwPlJ5odEai9JNKGkdB4QmVgFlIaKNIUtAWKhIyfa8RAjg4NpRHfAjHmz6DcCJDdibU" +
                "GTJMSFNtp4MtiFo4pVM5VHsYm9xg79kujG7Kx+Wa58WLg3eI9hp1eWlL7Z4bHPGIiWaqtsIsz+Lc5BJH" +
                "pBJLc0uGXocLRAGbR2omoeISR544JgWL8wSav2jL9RmUB0ytXbBX1AqELrBzXUQL0gqhjAVlWMqomoSC" +
                "O+SRUaUrBb1Qj8CawVTErFIaWiIOzw7itikEQpwC+UMXz3ktTDS1qTLTa3ILi+aw6YZjtEmqQgzm2Za2" +
                "y1TC3S0iprqKt0ZFAlI/TS4d/BUjwtLBXaSQwMBuUmhno6C99+VqTSuQQ2fiJtWbdK8IsLTgSSb+9/2z" +
                "a6vAJg8YZlQt2Cmza+nIifaqVWPBL/iA5dUK8oBMidCBHvlA8dD7cBwVS53StyvFFoL5eioN9Y4kpcKl" +
                "+IcAW4x5SqMeZok5mSAKxFMgL0b5NhRjE7Sjqne+bNfFmQ0a6D710ToV66VTh0ISY52AINxOIc5ulzEO" +
                "VPDUA+MRU0tgV+4bzq1KcPUyxFQAAqsG3O1SmUUNM74C8KX9shMXfiyjeYNug0rBthhn/QqLBBvwPJ9N" +
                "MbXnPgTm2lYuNkAMa/ZB0F8UxdxlkQgPK/Rd4Vrkh7Z6iFn8WKawG0WmbFtWB/68lQ44qPArAYHt3sZ6" +
                "bRI8XYeqAlmIcw7iHI9A2dMt1Pfds7POC95pRNG3stks00senKa3cabTJdbG2HJn2HkdKOjatxC8yEvo" +
                "oCoHTzc1I4mjQ7vZqHc5/NDrvLScrVYYy7DKTT13NBCxAZhIN27Y/mWOXVXOixy3oI8Sq1dXvcFZ59gH" +
                "62Lb3TvSRk0InhvrEFbv1CEcIIRToWtzl2uTI0SiZjm3sIe4F0Q8oxMUGUjYxZQi6EbKgEAjSyaJ6ISJ" +
                "HK5U5nsBwAuPWLl6WO2+P1rs/HrQaTz7l/8IPrbG4eq/vtj+YQGdfvmYluIqjVdmlB9toIMohwMtbHWN" +
                "4gEMlpugSpXhDGXO531+xMAHT2AvdLxXq0VulD9QKm/SpjeMophTZc625hDPUhFNfVYANAXOaFomyCZl" +
                "mrS9Hw8Hz0O9dOO3j93LC8EoWqLrDRoisfeIUpeK0dzJphgx2krApZ6W6FGtEac7tE+eRfMfrW+gzLlR" +
                "bfHN3/dR0Pvt/VMsic7e7DfFfqZ1Dm8Web5qP38OLYxMQOj5/j++sUxmVGylmqd/qQ2brEVbFaGSSnLA" +
                "yjPO92FRHFLHfaOUnb3PEnDdaZxAn9Sq5taK6eJpLMvRNd5nb9hICAvyhYHAbs2DMzKzNcgKQx/PUU2b" +
                "zq2BRsswPQvC1BZeCvwSBQEv64Jof/fD968sCCZqHjYA4H2y991u458uBOjPKDyI9fqqbj7+LXnnQCx6" +
                "2k7sb+bm5LV9hYffbfHdq5NjfoYFGYLEWC07GCgVNjqL6u+xuEGG3C7uNN9+XuponSAADRpyvdr3No7m" +
                "/liz/ocqDKDpjN13qu+gsVyh5TVFuIUSnaq+EAetdlrp+qZM+cNmMDM3pYTCaOrqBUCGCQHzP/km198v" +
                "mvBfq0EnRt+LN8OfOy/tz+Ord71Rr3NsH08/XvQHZ71R58S9GA56nVcNa7oublEWQposFL5vOKAohnRs" +
                "3HWTArQ47Csg3BocdSH55QUlsDYPjrHzobsRLARO6CiuOxe+9os1+5z8GtZG8SswTqRyE/JzU3zks4Bf" +
                "yjSjkKn3Uuk898PqSlTCiW0cKc8fCL1VyDb4ufOi9PTRyxqffgFRl0li+VuqaIKGasdACv/aUwWDZRIH" +
                "GYrPzHcmo3iNJNhmiS2oVdFrMOqe9a/HQE95T6dkwokK5tNNlgqbDs00aBDpakk6ybFb/SIkFCQtUUwg" +
                "K3iDd73+23cTcYC47cNhwRNfRSlJvOBpUenQnC+IA/SFQ94Po57bh7mz+/BDaZ+HdsHJpJMdq8/2Nbv3" +
                "PNUpDxfcJ7xV6HuDuk/iqVGcUSvdYpeJV4UNkUxxPXasaO/rVdOekX1rhdqoeaKVnzepGvNgXCVPvQdc" +
                "CAYBHyfE3e8XqHvN7h1iYrFaH6iRtrBg4O98ZQalXZqYtkSDJ7/+KkFppF+CeyoG49SPQytDrvKVH8k6" +
                "rrL7lbnu46cgbEVd4imRit0nBgjoytn5slimc6gn/lqKsLcyWSts1GZ4xUCXLhUCj3hMCsWP+fS5gXtM" +
                "LAI6k7K4GjZ42FGgW+E6tBu8e0YARM0OmdPFEV70RKJybOwQmWNr3xRE8X2MTydMp7oLaKz4JNRSD7/z" +
                "5gCfrKumHQYUwwI/UZB34lucJH4rwt/hr0h0xAtUlhTtDhi4mn168RmHk/7xJT6G/vEYHyP/ePLZn198" +
                "evWZ3j2WAL4yCKydpu48TK0tcYZGzvtoivsK3S7C0OWCvPQbDHzA7u8NqJjaQe+In5ruUAa+woMMQ5XY" +
                "Rtx8Ri3pKjRfuvrsp/ClvTiVuV+BaLk61A9PbDTA7OemEjhbxEsGGXYy1TNvihE1LlvAemPHPTFz/6IY" +
                "sFN6WWHr/hWyaO1GE5D7A5wUuV8PeRxlfuFXSL46qWZLLKDuz7Yr94NKK+tXpss4nshqHyCtesvF1q90" +
                "+5mMx97Tqp3k2ys5itMLXX+jGzE7LNPp19QvS+wuOuwNliMQlTvAKnAd8CUWd3+nfmHo0DkhQxSXmUoo" +
                "6IwoTsNkHSkatdJ1k1I1Y54XVvy8arvP7PVl6y/kQbbtsK88tpIvcS9QWcRu58BwmC5sti37ICFrPXCr" +
                "5AFt/ncC4peIcTqpqxWHJM7EyvfSDrCs0OI1/nLC4R+7POMHQXawKovD7iICOtPUeEeids37ges3pXB2" +
                "b4u0Etf+vX2qVvaFiPhPdZCQQHo3AAA=";
                
    }
}

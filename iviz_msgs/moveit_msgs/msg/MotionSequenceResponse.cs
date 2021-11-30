/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MotionSequenceResponse : IDeserializable<MotionSequenceResponse>, IMessage
    {
        // An error code reflecting what went wrong
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
        // The full starting state of the robot at the start of the sequence
        [DataMember (Name = "sequence_start")] public RobotState SequenceStart;
        // The trajectories that the planner produced for execution
        [DataMember (Name = "planned_trajectories")] public RobotTrajectory[] PlannedTrajectories;
        // The amount of time it took to complete the motion plan
        [DataMember (Name = "planning_time")] public double PlanningTime;
    
        /// Constructor for empty message.
        public MotionSequenceResponse()
        {
            ErrorCode = new MoveItErrorCodes();
            SequenceStart = new RobotState();
            PlannedTrajectories = System.Array.Empty<RobotTrajectory>();
        }
        
        /// Explicit constructor.
        public MotionSequenceResponse(MoveItErrorCodes ErrorCode, RobotState SequenceStart, RobotTrajectory[] PlannedTrajectories, double PlanningTime)
        {
            this.ErrorCode = ErrorCode;
            this.SequenceStart = SequenceStart;
            this.PlannedTrajectories = PlannedTrajectories;
            this.PlanningTime = PlanningTime;
        }
        
        /// Constructor with buffer.
        internal MotionSequenceResponse(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new MotionSequenceResponse(ref b);
        
        MotionSequenceResponse IDeserializable<MotionSequenceResponse>.RosDeserialize(ref Buffer b) => new MotionSequenceResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
            SequenceStart.RosSerialize(ref b);
            b.SerializeArray(PlannedTrajectories);
            b.Serialize(PlanningTime);
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
    
        public int RosMessageLength => 16 + SequenceStart.RosMessageLength + BuiltIns.GetArraySize(PlannedTrajectories);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionSequenceResponse";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3b9d4e8079db4576e4829d30617a3d1d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+1bbXPbNrb+zl+BWc+s7a2sNHGabX3HHxRbTtTakivJ2aaZDAciIQlrilAByrK6s/99" +
                "zwtAUrScdGfXvnNnbtpxTBI4wHl7zguQPdHJhbLWWJGYVAmrpplKCp3PxHouC7FWOfywJp9FV+ZO9You" +
                "jj2DoY6nxTgtivbEeK7EdJVlwhXSEgH4pVDCTEUBn6yZmEIARXygIeGLU7+tVJ6oaIhDRjQpvItpZCBf" +
                "WPl32JyxGlYv5p7YMpN5rqxYWpOuEpWKKTCj7lWyKrTJmeo4zNx8+uwnpHGdXFhCLswq563phRIaljDm" +
                "Fn6AeBbLTMHecM2FQdpEKZpmRhZvXjNZ4DvGmVF0+l/+E12N3p3AwndKF/HCzdyLpkIinRfHr8SdzJAb" +
                "+GglqGOi5vJOG+u/jm7Ozrqj0elL/3zR6V3eDLunP+CfyL+8vuz0+73+uxi/ds9Pj8LoXv9D57J3Hl8N" +
                "xr1BP8Zxp0ev/Mfay9gP7Iy75/Hbj3G3/6E3HPSvuv1xfPa+03/XPT069tPOBv3xcHBZrvXav7/pd95e" +
                "duPxIO78fNMbduNRtz8aDGMg2jk9+s6PGveuYInBzfj06E3Y/bDbvboeI62/oiSCYsSfxa3O1UIWOnFg" +
                "6WBirmArDrIbjTvDcQw/x11gIT4bXF72RsAUSODbHUM+9AaX8Pcovu6M38Po/mg87PT64xGMfxmE+W7Q" +
                "uWwSe1X/9iUqx/WBtU9hEurmddTQzrvh4OY67neuQMovv2t+bFCCIW8aQ4aDtwPPInz9a+PrZa//UyD+" +
                "fePb4O2P3bNx+PoDSt9tXKEW22K+GMKAGDbQH10MhldxMMKjVy9Lo/DCAnPpnv2Etgj28AHGoVHAwCDB" +
                "2l7xJ30LQvMG0+tfDMpvr3FPNTPY2ld/EPd+ikeDy5sx6en45XP4cYV7BELaiYVyTs4UYE5eSJ07oXMA" +
                "NdwxgI6cmFVRw1SC2ZbQbdVmPDRO40CHKKYLJ/5ugDsnZJ6KTOe3LnIqd4DctPiP+JFBl8bFRA6F9CNP" +
                "I5xdyI0AGAHgW2WFBhwU54MLIS0g9VIleqoBdufKqi3SVzgWxtWWoOlxaqZxY7FOUchkDlQSk2XaIZ9m" +
                "gujsxIEM3wCFnQFURi4EjChlcBiF+Wdh+oBmA9qH2XFJOfaUcd2LTM5AuqlOpA96CqhaIA1qcInKIQY4" +
                "XHgCv+SFsksLMSAVEuQpUj2dirUu5gAmKIei3KEhIjQ/6BTValyRbYReLI0tJIQZjFRz0EuGSyM3JasT" +
                "k2KQOwj7gYG5IfpJpqTdNRgxfwq7YtM6OUmMVScntaA6UbCeEqtlyrxCcKPNFzWTO4wmxmSw2RiZeyrr" +
                "322ANUnJ0gXI/OYmS52AbUuUAIS6xOqJCqkEJxkSqHLgNhaQnXzHgspBPuwAbRHt+ShfTlIgQP4sDqy6" +
                "M9kK32MuoR0BxCHuJlVTAAyQ8+YECIi/bLlZSGMCFZkigcVhqxp6pzKT6GLzcOgLR4NfuENyz2qKmoKu" +
                "CuYe5bFcZuhjOq8T6C9wdv+wTYx1K15gxirXIAW0thRSOPbQyYbwIJcL5QUxVzJFQ/VO7Ig6ZT6Y+811" +
                "Mq+tR1JzkBJazBPBvFKVtkUHcozmGKAOOzXgPEGNBB7sRmFqmQzCdmjNttfPNgA67QrnLTuAj7RWblyL" +
                "VqBsD1lf1nLK2mZQ7cTrzMjMO/NC3iqe5McD72hhZokalVlb/G2uIC9uz9piY1Y2QChxkRsg6PUjnQPN" +
                "SkQE70pq0cIpIpG5AE+9U3V10r6FWiyLjbdGlB5zw7qt8e7mZpWlXnJBTk7/DlgPLIMgmU7Na3CUyUHn" +
                "a1gF2CxtoNxmTThoBeWmQdAW9FJgvCYNtqMoes/GwTYSRa6wgBqAqGQ/PuvFdNo7Qu1VMPjaK5bAkwFK" +
                "kTKa8J6BDUCUPJU2BXEWkpCDwFbPAE2PMgU7RE4XS9Ac48pmiVxXwpwBdmMCvRErx7EHKoAFiDQhOYK9" +
                "bs1nk5dkhTpZZRILKrBznePwqZVk3zgs1Daid35CBk6Vyh35ap5YJR2Cc+9cRCvOSmBCtDdemyMMQDMM" +
                "TmHxEh/UPcQl5ygwIUb9hZlrA21EW1gFrPuA3sXwCHADi8AW1NKAExzAzq83xdyH1DtptZxkFPkSSQi6" +
                "j5P2D2uUcyKdy9wE8kyxWuOPkM1LusjTURkL3WoGAoSBUNTd6ZSxi+wUYBCMN9MTK+0mIqiiJaO9C0tA" +
                "guojjSBsbrunN2HWRqzT5whvD5MgYHaoUF3AiAwRhCMSmqiHGvLMEvBSNbNKEQxO4ZfUAMoAnSnEN7MO" +
                "yQNwt0qKlaXIVq3HsNorvEBWC7RmtBsZogXarU/TCQfckhQKJi+x5s4dJp88Z6aKKhoBWZkZv3qZU4tk" +
                "DjlrW1xQGS6xam6hY0AVJm0IXZLw7mZ4fkER9hjTyYN7gE74X67RIDAegu04xR8RTxHzaoZe3x0LEv6y" +
                "GqjwXIwvW9+BKo8I1MBwoUpG85jIhKr8rT38f1B93qC6toCL8z8cVMPw/0tB9bGYyuUQTnfRTEEJUdgN" +
                "A8g4mDCMKs35waA1KBQH4N+Nb38jMcFHltdTgd4juw6StAHyvDuUsDJRxVqBXRRr8yBikv4Q8MCZZAK2" +
                "HH2ght0xz8/Yq39ewQSbIwBYw5D6PEz6zexgUUIKhN8a+xclEJNFLRQWgWBT5UwqKtFmgIc2Opilmq2F" +
                "tVpqQB5QBBKK3SqKMuT+CMebAIYsE3wNUw7Q2VpY1OY8CkMF5SqU3QBWWz3TaRNGCfg9cy1RTF+BSYNL" +
                "0Z55MVAhEAnSPmyL3pQcdI0MkXOHYm2iyn1R8C+MaWFG5UlsC/SanCj4qs4hJMkUtB5arPflb2VqKX5/" +
                "FlVXNrZL2wDLVpfhfEvn+PRbZaAo5K8yFH5bP5OvEmh4tkKAdVXVus3PxJpbhUySiTlsxmBDAkOuzGeU" +
                "+GLQALALvuqHVM9+3PNwx/C3Q2ugClZPxVwLnAo2T6EHGcSQ+8dYJGLVI/cgnqN5+Ejjy4fnxlv240mt" +
                "dUSxCmojfR/6JOi0FDKxyxYyZvydolMElQh+LDuT1IiCrBPSADeXUEORmKAmVCRh+v5gb4wL9b4eDqMF" +
                "9wBJZMJ+hOpCXolkbnxHsA2Ahc05VUCih7Dku3l7u+iF3h4naiUbpSB4gTRqCoqJhlMh31mi7ml18FSu" +
                "FzqV2A+VmJH7PZgVGR4snKqphDxLHJUb421gSp5ByZduOCuDpIB36idUCQIRi7l/i1FkKhLIXEEFfleU" +
                "QEKOnXCXaKsQBW1QWcD6wOjDTFLkWSCNkhUg7YMGgJoCsUPdzpEsyQwVptKaFTmCp3LYCu0wWiNXCcK3" +
                "3dBqVmVcz2KFh4kUL4zqA5qhaVwexG1qLcHqyA6EgbuL/RJBKWsFxXyZnDaUARFzKm5zs67O53j8c/jk" +
                "Q1/s+Iyvxc2EKWUGvpscyjfymWaCyKyCxXs2vQAPyHqIFmiPDwIPg7NiYcvzgp43S8VGgVF5Ih1ViCSd" +
                "0nv47xjriFlO/RzmhVkYIwUkEyhXbXqPtljn7Ejcg8/yNG09MqCnNBvnlI7XThOCAEYmA/7DMgn2Zhca" +
                "GyYuIsThfdKo6/AJ21LVsGaS4ba+xyx4WOlKufk2VXwDYxf8YScd/FaReIvOgUrAchcb+AqDv0ezkruW" +
                "mKyqw2sVylFOIoD5FXsaaCxNNVdPJLjD+t6ucao/zn6USfxW7a6Tpq5uRl7q5QEpHVhQMlcbBDZ6p83K" +
                "Qdqn7iFTwO3rgtGZAacdTTaQsnfOz0+/jai9gd6wtdLUmgV3QvM7bU2+wGQXa2iLpdSBgjJ8A9BErkDn" +
                "TQU4s2vYhE4PeaVh92rwoXv6knhaLhGnMGfNS76oveGBlTbtQuv8y7yGHJsnBT5BCxWT19fd/vnpKw/C" +
                "1Zq7l6NVWoCKa2/5XtWU7B/giKC3ULEuVq7AEZmaFlyNYjcE0MyZDGUFog2IUaFpqhxIMuUtkmyOcYOD" +
                "pbJlSg804RET0DDQhM9PBYpfB5Vo79/+I/iQGTuk//5k/weFc/blw1UCTeqQTCngeSADGMOeFFarTnEP" +
                "BTNGUKGy2AaZ8Xld2SXgsyOwEzye20oqblV5IFRf4YTe8Pyqz2SDQc0AsXKRTgLYA5VAMJ3Ut+IDLLXJ" +
                "fhwN+i/wAovvnX3sXF0KJtDGC0DBkNLKAWolJgJ1kErVH+SgHgJKW3Qpa9D5DqWTH1HvBu/SZPpWnYg/" +
                "/WMfJbx/sn+Gmc352/2W2LfGFPBmXhTLkxcvoPyQGUi72P/nn5hFSxlTbrhxl3tkZO357AaVU5MCZo66" +
                "2IdJOqFi+VYp3zafZuCqE53p0O9Ru+wVT1FZiKFkPn/LtkFEkCv0e78yt7zQuFYgJ4Q4boBSUx4bop5Z" +
                "OkckMieiFAC9QxHAu6YITr774fvXPAJDL3cIYNzDHe/7lUY/XwpQm1N4eFrqaWvh0W/Z+zCCadNSYn89" +
                "c8dv+A0eVZ+I714fv6JHGG1xgMY014+AsL82Nm28xgwFGQkLhFN3/row6SrD79QVKMxyPxg0mPZTteUf" +
                "yxZgR+fsphNzDzXgEi2tJZINpNaUtCXYE/WNxVDlWFWeC4NZhYYiZDiTkAIAMQR8DOnkiZw4f9uC/9oR" +
                "He58L94OfoEwxr+Prt93h10ILfx49vGy1z/vDgHK/YtBv3v6urwM5/GJogzuyY/iLC1AgoZA68JlkGpo" +
                "dS5XjQhzsCuF269PqA074R4vlit0h4GFwKEaxXUfkGq/mrPPwS3ypolfgXHaKlcPv7TER27b/1rfMwqZ" +
                "CiaVz4qyr9zEICybSv5A6O1KtvEvkJFUTx9LWePTrxjFa1ti+ftdUbML1Y6wCX/7AwCH2Q+DCkEx821l" +
                "qle4BV/msAW1t/QaDzvnvZsRZki1NYOSiSYqmA8iWSpsOtR+oJ5hSA/p0MUv9auQkHC0RdUs3KIbv+/2" +
                "3r0fiwOk7R8OK574zkhN4hVP863yKviCOEBfOOT1EOfCOsydX4cfaus8tgo2EYPsWH2+ONm95pnJuRkQ" +
                "PuHtvjLPb/okHvBoSyVwm11GLysbIpnifCw20d5Xy5Y/zvrGCzVqeKKXX2lSDeYxH6089cHgSjCnT3at" +
                "7GERQMWnfXDeiMlos/dF2sL0gL/z7RaUdq252RYRN2nLU/9a97027rkY1HnZudxqSdVv50jW8Ta7X2nB" +
                "Pn0IwtIyBJ7aVrGcRICA6pqdz2qZzyCD+J8awt7JbKWw/pribQBTu/IHPOKJJiQ77tPnCNcYewJ0fORp" +
                "RR48fOMuzAi11y1eEuN7zribHTKnOx486ZlEFdjYIbLA1r6rNsVXJz4d8z7VfUx9wGfZLdXlOw/5+RBc" +
                "tXx9X9X/ZZNA3otvsP33jUh+hx+pOBVUUUtxcgoGrqafvv2MHcXy8SU+JuXjK3xMy8fjz+VRw6fXn+nd" +
                "UwngKz28Rl9r57lnY0owNHLeJ1PcV/YdEIbuAVRjPaJUR/xKU9lXOuKnVjg/ga/wIJNEZb7adp9RS2Z7" +
                "NN+P+lz2zGtrcSjjf9GAfQifh5ZtEY8GGP1C1wG7g3gfwGLpsn08TRjR4LINrEc7rnS5h3e68KJp9XKL" +
                "rYe3vdJVaD9A7I+xBxT+QcfTKPPBreqaAX6tycyWWDw+YesmT21i805zjcQz2ewjO9u+juKzV7qiTKbj" +
                "L1Q1jtz93RnFwYXuqdHVlR12GbS7o2m9K+XwV02OQFLhpKmidcC3TcJFm+bNnsPggjyiunVUI0FHOjpP" +
                "slWqqHdK90JquYx7Udnwi23L3fP3jL23kP/4osO/KqnVPIkrga1J7HRhGLbEhY+1dQ8kYu1Hrn88os3/" +
                "HTj80maCTppqxZ5IMLH6BbIDTCqMeIP/duDwj91yKds+vm0qq1PpCv+CaRq8zNC4j/3IPZkamD1YIt9C" +
                "tf9snW0r+wIe/gspc279iDcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

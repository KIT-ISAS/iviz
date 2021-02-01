/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PickupResult")]
    public sealed class PickupResult : IDeserializable<PickupResult>, IResult<PickupActionResult>
    {
        // The overall result of the pickup attempt
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
        // The full starting state of the robot at the start of the trajectory
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart { get; set; }
        // The trajectory that moved group produced for execution
        [DataMember (Name = "trajectory_stages")] public RobotTrajectory[] TrajectoryStages { get; set; }
        [DataMember (Name = "trajectory_descriptions")] public string[] TrajectoryDescriptions { get; set; }
        // The performed grasp, if attempt was successful
        [DataMember (Name = "grasp")] public Grasp Grasp { get; set; }
        // The amount of time in seconds it took to complete the plan
        [DataMember (Name = "planning_time")] public double PlanningTime { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PickupResult()
        {
            ErrorCode = new MoveItErrorCodes();
            TrajectoryStart = new RobotState();
            TrajectoryStages = System.Array.Empty<RobotTrajectory>();
            TrajectoryDescriptions = System.Array.Empty<string>();
            Grasp = new Grasp();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PickupResult(MoveItErrorCodes ErrorCode, RobotState TrajectoryStart, RobotTrajectory[] TrajectoryStages, string[] TrajectoryDescriptions, Grasp Grasp, double PlanningTime)
        {
            this.ErrorCode = ErrorCode;
            this.TrajectoryStart = TrajectoryStart;
            this.TrajectoryStages = TrajectoryStages;
            this.TrajectoryDescriptions = TrajectoryDescriptions;
            this.Grasp = Grasp;
            this.PlanningTime = PlanningTime;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PickupResult(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
            TrajectoryStart = new RobotState(ref b);
            TrajectoryStages = b.DeserializeArray<RobotTrajectory>();
            for (int i = 0; i < TrajectoryStages.Length; i++)
            {
                TrajectoryStages[i] = new RobotTrajectory(ref b);
            }
            TrajectoryDescriptions = b.DeserializeStringArray();
            Grasp = new Grasp(ref b);
            PlanningTime = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PickupResult(ref b);
        }
        
        PickupResult IDeserializable<PickupResult>.RosDeserialize(ref Buffer b)
        {
            return new PickupResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
            TrajectoryStart.RosSerialize(ref b);
            b.SerializeArray(TrajectoryStages, 0);
            b.SerializeArray(TrajectoryDescriptions, 0);
            Grasp.RosSerialize(ref b);
            b.Serialize(PlanningTime);
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
            if (TrajectoryStages is null) throw new System.NullReferenceException(nameof(TrajectoryStages));
            for (int i = 0; i < TrajectoryStages.Length; i++)
            {
                if (TrajectoryStages[i] is null) throw new System.NullReferenceException($"{nameof(TrajectoryStages)}[{i}]");
                TrajectoryStages[i].RosValidate();
            }
            if (TrajectoryDescriptions is null) throw new System.NullReferenceException(nameof(TrajectoryDescriptions));
            for (int i = 0; i < TrajectoryDescriptions.Length; i++)
            {
                if (TrajectoryDescriptions[i] is null) throw new System.NullReferenceException($"{nameof(TrajectoryDescriptions)}[{i}]");
            }
            if (Grasp is null) throw new System.NullReferenceException(nameof(Grasp));
            Grasp.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += TrajectoryStart.RosMessageLength;
                foreach (var i in TrajectoryStages)
                {
                    size += i.RosMessageLength;
                }
                size += 4 * TrajectoryDescriptions.Length;
                foreach (string s in TrajectoryDescriptions)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += Grasp.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c1e72234397d9de0761966243e264774";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+07a28bN7bf51cQCXBtt7LSxGm29V5/UGwlUWtLrqWkjyAYUDOUxPVoqA5HltWL+9/v" +
                "eZCch+Wkxa69WOCmRSLNkOfwvB88iqILc6MGZb8oTHFqUmWFwo9xAp+jKLoyU1OOS1kqURbyHyopTbGN" +
                "bSmL0r+dhOcfP7UWzZWNIlsWOp833wGepNCrUpscVkRvC2lXYo5/w7dZZmT56qVYZTLPYWtc6qWKTv7F" +
                "f6KL8dtjsQTqdRkv7dw+a3Mi0nl59ELcyCyKngp4WcgsE1O1kDfaFO7t+P3paX88Pnnuvr/pDc7fX/VP" +
                "vsc/kXt4ed4bDgfDtzG+7Z+dHPrVg+GH3vngLL4YTQajYYzrTg5fuJe1h7Fb2Jv0z+LXv8b94YfB1Wh4" +
                "0R9O4tN3veHb/snhkdt2OhpOrkbnAddL9/z9sPf6vB9PRnHvp/eDq3487g/Ho6sYgPZODr91qyaDC0Ax" +
                "ej85OXzlT3/V719cThDW35ATXi7iv8S1ztVSljqxolC/r5UtWX0878aT3tUkhr8nfSAhPh2dnw/GQBRw" +
                "4JsdSz4MRufw7zi+7E3ewerheHLVGwwnY1j/3DPz7ah33gb2ov7uc1CO6gtrr/wmlM3LqCWdt1ej95fx" +
                "sHcBXH7+bftlCxIsedVacjV6PXIkwtu/td6eD4Y/euDftd6NXv/QP534t98j9+3WlmrZZPObK1gQwwGG" +
                "4zejq4vYK+Hhi+dBKRyzQF36pz+iLoI+fIB1qBSw0HOwdlb8m955pjmFGQzfjMK7l3immho0zjUcxYMf" +
                "4/Ho/P2E5HQEQnx4O658FhxtstBWLJW14IxEYvJS6twKnc9MgSc2uZBTsy5FuVCiwI3C4s6O0F3Vpacr" +
                "YzU5KmFmQpdW/MMAdVbIPBWZzq9tZFVuwWUS8h/wJTtMWhcTOGTSD7ytXMhSLOVWgBtRYrnOSr3KlDgb" +
                "vRGyUMKuVKJnWqVioQrVAH2Ba2FdDQVtj1Mzi1vIemUpkwVASUyWaYt0mil6Xyv2pX9XGmHNUhEVAlYE" +
                "HhxEfv+p3z6i3eDF/e44QI4dZMT7JpNz4G6qE2AuuIjNQgHUAkCDGGyiciXgAyCewoe8VMWqUCUcRQI/" +
                "RapnM7HR5QKcCfKhDCc0BIT2e5miWI0ts63Qy5UpSpmXAoQKbM3TDFEjNYHUqUk1xLZ9fx5YmBuCn2RK" +
                "FrsWo8+fwalYtY6PE1Oo4+NaQJwqwKfEepUyrbrkw5c1lTuIpsZkcNgYiXso7d+tgDVOyWACpH4Lk6VW" +
                "wLElcoCD8VQRE0iDmHCrSvwAARs8O9lOASIH/rABdEX0lFDUNilgIL8W+4W6MdkanxdiVWhLDuIAT5Oq" +
                "GTgM4PP2GACIrxpmRigXKkCRKQJYHnSqpTcqM4kut3eXPrO0+Jk9IPOstqgZyKpk6pEfq1WGNqbzOoDh" +
                "EncPD7pEWL+iBXascw1cQG1LVV6yhU635A9yuVSOEQslU1RUZ8SWoGP6AsoFtqCTRQ0fcc2KDdg5KDyo" +
                "V6rSruhBjtFeA9DhpAaMx4uRnAebkd+KKEiCcBzC2XXyaTpAq21pnWZ75yOLQm5thzCgDZEYV5DjNTlM" +
                "h0GxE61zIzNnzEt5rXiTWw+0o4YZyvBk1hU/L1QuVHfeFVuzLrwLJSpyAwCdfKS1IFmJHsGZklp2cItI" +
                "ZC7AUm9UXZx0bqGWq3LrtBG5x9SwbGu024VZZ6njnOeT1X+ArweSgZEMp2Y1uMrkIPMNYAEygw6EY9aY" +
                "g1oQDg2MLkAuJcZrkmAX8tp3rBysI7W8mPTHJb3w1RtC7ZFX+Noj5sCDOZQyZW/CZwYywKPkqSxSYGcp" +
                "yXOQs9Vz8KaHmYITIqXLFUiO/cp2hVRXzJyD78YEeivWlmNPYpZLYGlCfAR9bexnlZekhTpZZ+CjEwN6" +
                "rnNcPisk6Tcus5h45okSg7NjUnCVrEt9Q7aaJ4WSFp3z4ExEa85KYEP0dLIxhxiA5hicPPLgH9QtxCVr" +
                "KTChj/qKiesCbPS2gAW0e5+exfAV3A0ggSOolQEj2IeTX27LhQupN7LQcppR5EskedA93LR3UIOcE+hc" +
                "5saDZ4gVjj8DNg9wkabDEAvteg4MhIWrwtzolH0X6Sm4QVDeTE8LWWwjclWEMnr6piBHguIjiaDbbJqn" +
                "U2GWRqzTxwhvd5MgIPZKobiAEOkjCEckVFHnasgyg8NL1bxQitzgDD6kBrwMwJlBfDMbnzwAdeukXBcU" +
                "2Sp87FYHpWPIeonajHojfbRAvXVpOvkBuyKBgspLLIJzi8kn75mrsopGAFZmxmEPObVIFpCzdsUbdMy3" +
                "IJoMPJakKkwWPnRJ8nfvr87eUIQ9wnRy/xZcJ/wvN6gQGA9Bd6zil+hP0efVFL1+OmYk/FNogMJ7Mb40" +
                "3gNUXuGhgeJClYzqMZXJNRLcOMP/B9XHDaqbAvzi4k8HVb/8Pymo3hdTuRzC7TaaKyghymLLDmTiVZg7" +
                "Uvz5zqINCBQX4L+tdz8Tm+Al8+uhnN49p/acLLzLc+YQ3MpUlRsFelFuzJ2ISfJDhwfGJBPQ5egDNeSO" +
                "eH/GVv3TGjYUOTqAwrBLfRwi3WF2kCghBcJ3rfOL4IhJo5YKi0DQqbCTikrUGaChiwZWUM3WwVotNcAP" +
                "KALJi4GpYZQh80d3vPXOkHmCj2HLPhpbB4vanFdhqKBchbIb8NWFnuu07UbJ8TviOqKcvQCVBpOiMzMy" +
                "ECEA8dw+6IrBjAx0gwSRcftibarCuSj4l8Z0MKNyIJoMvSQj8raqcwhJMu1WHdbb8CmkluKPRxF1pWO7" +
                "pA1uudAhnDdkjt9+rxQUmfxFgvynzSPZKjkNR5YPsLaqWpv0TAtzrZBIUjGLzRhsSGDIlfmcEl8MGuDs" +
                "vK26JdV3t+5xqGP3t0NqIAoWT0VcB4wKDk+hBwnEkPvnSCRg1VfuQTxG8/CexpcLz62nbMfTWuuIYhXU" +
                "RvrW90nQaClkYpfNZ8z4maJTBJUIvgydSWpEQdYJaYBdSKihiE1QEyriML2/czb2C/W+Hi4jhE/Bk8iE" +
                "7QjFhbQSyNy4jmAXHBY251QJiR66JdfNe7oLnu/tcaIWyAiMYARp1GYUA418v4g7S9Q9dYllvf/mO5XY" +
                "D5WYkbszmDUpHiBO1UxCniUOw8H4GJiSZ1DypVvOyiAp4JO6DbXLKAQWc/8Wo8hMJJC5ggjcqSiBhBw7" +
                "4S5RoxAFaVBZwPLA6MNEUuRZIoxACoB2QQOcmgK2Q93OkSzJDBWmsjBrMgQH5aDj22GEI1cJuu9iS9gK" +
                "lXE9ixUeJlKMGMUHMH3TuHbRVrUEq2s6YAaeLnYovFA2Cor5kJy2hAERcyauc7PJK29K6x/DJu/aYs9l" +
                "fB1uJswoM3DdZF++kc20E0QmFTTekekYuE/aQ7BAenwReOCNFQtb3uflvF0pVgqMylNpqUIk7gTr4X9j" +
                "rCPmOfVzmBYmYYIQEIyHXLXpnbfFOmdH4u5tlrfpwnkGtJR245zS8dptgmfA2GRAv0eTYG92qbFhYiPy" +
                "OHxOWnXpX2FbqlrWTjJs433MjAdMF8oumlDxCaxd8oudcPBdBeI1GgcKActdbOArDP7OmwXqOmLqrnBo" +
                "mS9HOYkA4tdsaSCxNNVcPRHjDupnu8StSAhhuodIfFedrpemtq5GjuvhgpQuLCiZqy0CHb3RZm0h7VO3" +
                "kCng8XXJ3pkdTjeabiFl752dnXwTUXsDraGBaVaYJXdC8xtdmHyJyS7W0AWWUvsKyvAtuCYyBbpvKsGY" +
                "bUsndHrAmK76F6MP/ZPnRNNqhX4Kc9Y80EXtDedY6dDWt84/T6vPsXmTpxOkUBF5edkfnp28cE64wrkb" +
                "HWHpgFfcOM13oqZkfx9XeLn5inW5tiWuyNSs5GoUuyHgzazJkFfAWu8xKm+aKgucTPmIxJsjPOBopYqQ" +
                "0gNM+IoJqF9o/OuHcopfdirR07/8R/AlM3ZI//pm9weZc/r5y1VymtQhmVHAc44M3Bj2pLBatYp7KJgx" +
                "gghVgW2QOd/XhS4B3x2BnuD1XCOpuFbhQqiO4Zie8P6qz1R4hZqDx8pFOvXOHqB4gOm0fhQXYKlN9sN4" +
                "NHyWmKXvnf3auzgXDKArekGFwc0GA6iVmOioPVeq/iAHdR9QuqJPWYPOdwid7Ih6N8ZcQ75yrY7Fk//Z" +
                "Qw7vHe+dYmZz9nqvI/YKY0p4sijL1fGzZ1B+yAy4Xe797xMmsaCMKTfcuMudZ2TpuewGhVPjAmaOutyD" +
                "TTqhYvlaKdc2n2VgqlOdad/vUbv0FW9RmYm+ZD57zbpBQJAqtHuHmVteqFxr4BO6OG6AUlMeG6KOWLpH" +
                "JDDHIjCAniEL4FmbBcfffv/dS16BoZc7BLDu7on3HKbxT+cCxGYVXp4GOTUQj3/P3vkVDJtQib3N3B69" +
                "4id4VX0svn159IK+wuoCF2hMc90KCPsbU6Stx5ihICEegb9157dLk64zfE9dgdKs9rxCg2o/VFv+vmwB" +
                "TnTGZjo1t1ADrlDTOiLZQmpNSVuCPVHXWPRVTqHCvTColW8oQoYz9SkAAEOHjyGdLJET52868F83osud" +
                "78Tr0S8Qxvjz+PJd/6oPoYW/nv56Phie9a/AlbsHo2H/5KW3du+fKMrgmdwqztK8S9AQaK0fBqmWVvdy" +
                "1Qq/B7tSePz6htqyY+7xYrlCMwzMBA7VyK5b76n2qj17HNwip5r4Fgino3L18EtH/Mpt+9/qZ0YmU8Gk" +
                "8nkZ+sptH4RlU6APmN6teBv/AhlJ9e3XwGv89htG8dqRmP/uVNTsQrGj24R/3QWAxeyHnQq5Yqa7kKle" +
                "4xFcmcMa1G3INb7qnQ3ejzFDquH0QiaYKGC+iGSusOpQ+4F6hj49pEsXh+o3ISHh6IqqWdiAG7/rD96+" +
                "m4h9hO2+HFQ08cxIjeMVTYtGeeVtQeyjLRwwPvRzHg9T5/Dwlxqe+7BgE9HzjsXnipPdOE9Nzs0A/wqn" +
                "+0Ke37ZJvODRBZXAXTYZvap0iHiK+7HYRH1frzruOutrx9SoZYmOf0GlWsRjPlpZ6p3FFWNOHmys7G4R" +
                "QMVncee+EZPRdu+LpIXpAb/n6Rbkdq252RURN2nDrX+t+15b91gE6jx0Lhstqfp0jmQZN8n9Qgv24UMQ" +
                "lpY+8NSOiuUkOgiortn4Ci3zOWQQf6952BuZrRXWXzOcBjC1kT+gEW80IdmxHz9FiGPiAND1kYMVOefh" +
                "Gnd+h6+9rnFIjBbQaXbwnGY8eNMjscqTsYNlnqw9Wx2KRyc+HvE51W1MfcBHOS3V5Tsv+fkSXHVcfV/V" +
                "/6FJIG/F19j++1okf8BfqTgRVFFLcXwCCq5mH7/5hB3F8PU5fk3C1xf4NQ1fjz6Fq4aPLz/Rs4diwBd6" +
                "eK2+1s57z9YWr2hkvA8muC+c23sYmgOo1jqPUl3xK01lXzDEjx1/fwJv4YtMEpW5att+QimZ5mqej/oU" +
                "euY1XBzK1C3OCWEfwuWhoS3ivAFGP991wO4gzgMUWLo0r6fJR7So7ALp0Y6RLnt3pgsHTauHDbLuTnul" +
                "a99+gNgfYw/I/xTjYYR5Z6q6poBfajKzJpb3b2hM8tQ2tmeaayAeSWfvOVlzHMVlrzSiTKrjBqpaV+5u" +
                "dkZxcKE5NRpd2aGXXro7mta7Ug43anIInPI3TRWsfZ428YM27cmeA2+CvKKaOqqBoCsdnSfZOlXUO6W5" +
                "kFouY59VOvysqblP3ZyxsxayH1d0uEcBWs2SuBJobGKj88uwJS5crK1bIAHr3jP+cY80/z3u8HOH8TJp" +
                "ixV7Il7F6gNk+5hUGPEKfztw8OemXELbx7VNZXUrXfk/r5oGhxla89j3zMnUnNkdFHnDq/1zeJpa9m/1" +
                "h/SbtR2jYJwzS1H7iRtbK/28je1iQ0NTU75xwkEtvjWuXTGqPD0MV4UgZN4cuuMdZ5iY9izMRtDVulyt" +
                "CoMKpOH9HHDTB1UmVF3WThmaku64AHdL03Dtvq0UTxgxGckTsS+rQmA0rPXMcWRe/Ayk4cWDG/UtKQG+" +
                "Z1asuqVdTrG+3Im+htsNcO5uLXtd4eqbb0Wa7MNSbO0mywhq7Au2+gm77GrpLjDc7fKvEu9cBdJ1o7tH" +
                "8jOiVOajC/OaDS77cO70hBogVWGB2QVJ/0txFGDE4cj1+9o/dQKPvYaYL6EhnfgLh9h5gPYvNhpMbxzA" +
                "K6AOxaSqmjtPQO0hrtFd/JNdsDqkq1xb8ayUh+AUlwYTvtL5V3e2IuLtSiduI+APplcHw50T/DmRSMHR" +
                "AE83BdaLBHhadYGItft1BAduXGCq5jLv7rg0HLuh9krrPPuUBa9Fl7tgtlPJnXOaXV4neOffUkEaqKx+" +
                "DIXnVdI6uaMPeDI3Jn3CV2zdUHozXqiHELpHHTxFGI6jq0ssVB21wLNrd2fqblnegkNZqWJSG4qrVNMD" +
                "9AgKYIKS5Q74coYtOO8NMdZPcVoRDTSjX4LtV7dwfgbhYCdykJbD7rC1kS+Nx+zu6XhwL8xQ1PzX35nR" +
                "NfeCDomdZHMCI+g0TjKpbMZXz/cC7jglrzsuPysTAHM3jaH6nNIfWZIo1is+D5C2LnK+hqqPEkNqCElD" +
                "B/TjDiHEyBupMxoXRBLQYrDHhLi697KWrqLrrKX+hbzVy/WSA0dSupkvnOLFe62FzhwZyIT9/z75hn5e" +
                "pi2iPmCVPHqBQGIHIOY5LxpPD4PNoVdjpmCLSeYbKRs3oW3VUuY4GH8nGoQxd3eThGaOl0EqfbZaW/yH" +
                "ppN8WZeYdcEOwB+6yiDd3FHMc0LhF44Pk1fc6zYoHuF3P93ha4jdZYb/HUurgCAA7Hoe5vzNvKitT9hC" +
                "cZcazaHayp5oSycM4LDKU0mPWgiew15jUTGrfgJBnzM9KxteCmRJHSzSdVjg/ZI3yjIYAQJGgTvFrhyV" +
                "s6j6QPTOKWXv2MPOAIpnBxqUUlTJfZcUbMAtisMLb2C6Wuzah25+oTbPNkM3g4RUoQlHf9krWaHwRht8" +
                "Jw47gAoFs9N5Dd3DKMLnOLVzrNtPeP4z6u1h8Ih4FP0fxrmF2tRCAAA=";
                
    }
}

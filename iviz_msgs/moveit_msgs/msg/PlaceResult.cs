/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceResult")]
    public sealed class PlaceResult : IDeserializable<PlaceResult>, IResult<PlaceActionResult>
    {
        // The result of the place attempt
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode { get; set; }
        // The full starting state of the robot at the start of the trajectory
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart { get; set; }
        // The trajectory that moved group produced for execution
        [DataMember (Name = "trajectory_stages")] public RobotTrajectory[] TrajectoryStages { get; set; }
        [DataMember (Name = "trajectory_descriptions")] public string[] TrajectoryDescriptions { get; set; }
        // The successful place location, if any
        [DataMember (Name = "place_location")] public PlaceLocation PlaceLocation { get; set; }
        // The amount of time in seconds it took to complete the plan
        [DataMember (Name = "planning_time")] public double PlanningTime { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlaceResult()
        {
            ErrorCode = new MoveItErrorCodes();
            TrajectoryStart = new RobotState();
            TrajectoryStages = System.Array.Empty<RobotTrajectory>();
            TrajectoryDescriptions = System.Array.Empty<string>();
            PlaceLocation = new PlaceLocation();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlaceResult(MoveItErrorCodes ErrorCode, RobotState TrajectoryStart, RobotTrajectory[] TrajectoryStages, string[] TrajectoryDescriptions, PlaceLocation PlaceLocation, double PlanningTime)
        {
            this.ErrorCode = ErrorCode;
            this.TrajectoryStart = TrajectoryStart;
            this.TrajectoryStages = TrajectoryStages;
            this.TrajectoryDescriptions = TrajectoryDescriptions;
            this.PlaceLocation = PlaceLocation;
            this.PlanningTime = PlanningTime;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PlaceResult(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
            TrajectoryStart = new RobotState(ref b);
            TrajectoryStages = b.DeserializeArray<RobotTrajectory>();
            for (int i = 0; i < TrajectoryStages.Length; i++)
            {
                TrajectoryStages[i] = new RobotTrajectory(ref b);
            }
            TrajectoryDescriptions = b.DeserializeStringArray();
            PlaceLocation = new PlaceLocation(ref b);
            PlanningTime = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceResult(ref b);
        }
        
        PlaceResult IDeserializable<PlaceResult>.RosDeserialize(ref Buffer b)
        {
            return new PlaceResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
            TrajectoryStart.RosSerialize(ref b);
            b.SerializeArray(TrajectoryStages, 0);
            b.SerializeArray(TrajectoryDescriptions, 0);
            PlaceLocation.RosSerialize(ref b);
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
            if (PlaceLocation is null) throw new System.NullReferenceException(nameof(PlaceLocation));
            PlaceLocation.RosValidate();
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
                size += PlaceLocation.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "94bc2148a619282cbe09156013d6c4c9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+07a28bR5LfCeg/NNbASdpQtC053kQHfaAl2mYikQpFe+MYxqA50yRnNZxmuoeimMP9" +
                "96tHd8+DlJ3FnrQ44LyLmDNTXd31fnS51brSd6pf9IzR5lwnygqFP6MYfrdarZGe6OKmkIUShZH/UHGh" +
                "zSayhTSF/zoO7z9/aQDNlG21bGHSfFb/BvvEJl0Wqc4BonWdyVhd6ljiC7HEpyhzj/B5mmlZvH6FH/Ic" +
                "cEVFulCts//lP62rm3enYgHsSItoYWf2eZM1rTQvTo7Fncz2WnutZwI+G5llYqLm8i7VZs8B3Hw4P+/d" +
                "3Jy99C/edvuXH0a9sx/xD67l19eX3cGgP3gX4ffexdlRWNAffOxe9i+iq+G4PxxECHh2dOy/Vt5GDrI7" +
                "7l1Ebz5FvcHH/mg4uOoNxtH5++7gXe/s6MSvOx8OxqPhZdjulf/wYdB9c9mLxsOo+8uH/qgX3fQGN8NR" +
                "BGi7Z0ffe7Bx/wp2GX4Ynx29DjSMer2r6zGi+xtzxUtJ/Ie4TXO1ACnGVhj1+0rZgrXLBk6Nu6NxBP8d" +
                "94CS6Hx4edm/AdqAFS92wXzsDy/h75voujt+D+CDm/Go2x+Mb2DBy5Kx74bdyya+49rHryE6qUFWvvlV" +
                "KKlX5W5eWO9Gww/X0aB7BTx/+f3W1wYygHndhBkN3wwdqfD5b83Pl/3Bzx7/D82Pwzc/9c7H/vOPLAu7" +
                "sYVaNJj+dgQwERxjcPN2OLqKvHYeHb8sNcUxDpSod/4z6ijoyEcAREUByMDNypHxv/QxMNCpUX/wdhg+" +
                "vuKTVVSjfrrBMOr/HN0MLz+MSXAncKgnsPXS0cHhxvPUioWyFjyYiHVeyDS3Is2n2izYR8mJXhWimCth" +
                "cKGwuLIt0o7q0Nultil5N6GnIi2s+IcG8qyQeSKyNL8Fcq3KLTha2v0n/MpulgAjwsec+olXFnNZiIXc" +
                "CPA1SixWWZEuMyUuhm+FNErYpYrTaaoSMVdG1bFfITAAVnah9VGip9HWft2ikPEcEMU6y1KL1OoJOm4r" +
                "DqT/Vmhh9UIRLQIgAicO91oewblfP6TlEAH88iigjhxq3vptJmfA5iRF1w8eZD1XgNgAdpCHjVWuBPyA" +
                "vSfwIy+UWRpVwGkkMFYk6XQq1mkxB1+D3CjCITUhofV7XrooYG2LbCPSxVKbQuaFAPECd/Mkw72RokDu" +
                "RCcphMYDfyAAzDVtEGdKml3AFCCmcC7WstPTWBt1eloJqBMFGyqxWiZMbVrw8YuK9gE7J1pncNwI6Xs0" +
                "U9itjBVmyWAPpIhznSVWwMElMoHD+UQRH0iVmHSrCvwBIR+cPxmSAcEDi9gaOqL1jLaoLFLAQ/4sDoy6" +
                "09kK3xuxNKklf3GIp0nUFPwHsHpzCgjEX2s2R1vOVcAiE0SwOGyXoHcK0ou02GyDPrcE/Nwekq2WS9QU" +
                "pFUw9ciP5TJDa0vzKoLBAlcPDjtEWK+kBVas8hS4gAqXqLxgW51syDnkcqEcI+ZKJqiszpwtYcd8B/QL" +
                "7CGN55X9iGtWrMHiQelBwRKVdEQXUpImDGCHk2owIC9GciNsSn4pbkEShOPQnh0nn7o3tKktrNNt74ak" +
                "MXJj27QDmhGJcQlZYp3DdBgUO9E60zJzBr2Qt4oXOXigHTVMU44os474+1zlQnVmHbHRK+P9KVGRa0Do" +
                "5COtBclK9ArOmNSijUtELHMBxnqnquKkcwu1WBYbp43IPaaGZVuh3c71Kksc5zyfbPoHOH4gGRjJeCpW" +
                "g1A6B5mvYRcgM+hAOGaFOagF4dDAaANyKTCIkwQ7kAi/Z+VgHalk1qQ/LkuGR28IlVde4SuvmAOP5lCK" +
                "hL0JnxnIAI+SJ9IkwM5Ckucgf5vOwKEeZQpOiJQuliA59iubJVJdMnMG/hvz7Y1YWQ5BsV4sgKUx8RH0" +
                "tbaeVV6SFqbxKgM3HWvQ8zRH8KmRpN8IZjE3zWMl+henpOAqXhXpHdlqHhslLbrn/oVorThHgQWtZ+O1" +
                "PsIgNMMA5TcP/kHdQ2yyloIT+qi/MnEdwI3eFnYB7T6gdxE8gruBTeAIaqnBCA7g5NebYu4i6500qZxk" +
                "FP1iSR50HxftH1Yw54Q6l7n26BljucefQZsHvEjTUQiHdjUDBgLg0ui7NGHfRXoKbhCUN0snRppNi1wV" +
                "bdl69taQI0HxkUTQbdbN06kwSyNKk6cIb9vZEBA7UiguIET6CMIRCVXUuRqyzODwEjUzSpEbnMKPRIOX" +
                "ATxTiG967fMHoG4VFytDka3cj91qv3AMWS1Qm1FvpI8WqLcudyc/YJckUFB5iWV0bjET5TUzVZTRCNDK" +
                "TLvdQ4ot4jkksB3xFh3zPYgmA48lqVCTxocuSf7uw+jiLUXYE0wsD+7BdcL/5RoVAuMh6I5V/BH9Kfq8" +
                "iqJXT8eMhL9MClh4LcaX2nfAyhAeGyguFNWoHhMZ3yLBtTP8f1B92qC6NuAX5386qHrw/0tB9aGYynUR" +
                "LretmYIyojAbdiBjr8Lc0+LfW0BrECgC4N+Nb38nNsFH5tdjOb0HTu05abzLc+YQ3MpEFWsFelGs9VbE" +
                "JPmhwwNjkjHocusjtfROeH3GVv3LChaYHB2A0YVr4z0Fke4wO0iUkALht8b5RXDEpFELhXUg6FRYSYUl" +
                "6gzQ0EEDM1S1tbFaSzTwA+pA8mJgahhlyPzRHW+8M2Se4GtYcoDG1sbCNmcoDBWUq1B2A77apLM0abpR" +
                "cvyOuLYopseg0mBSdGbeDEQISDy3DzuiPyUDXSNBZNy+WJuocC4K/oXWbcyoHIo6Q6/JiLytpjmEJJl0" +
                "ypbsffgVUkvxx5OIutSxXdIGt2zSEM5rMsen30sFRSZ/kyD/a/1EtkpOw5HlA6wtq9Y6PROjbxUSSSpm" +
                "sSeDPQkMuTKfUeKLQQOcnbdVB1I+O7inoY7d3w6pgShYPCVxbTAqODyFHiQQQ+6fI5GQlY/cg3iKTuID" +
                "7S8Xnhtv2Y4nle4RxSqojdJ73ydBo6WQic22PZ8y4wOFJ2qdEStDp5LaUZB4QiZg5xLKKOIUlIWKmEzf" +
                "GwcBHOwbqi0+hONNn4E7kTEbE8oMCSakuXbdwQ54LezSqQKyPfRNvrH3bBdG3+XjdC3QEtjBOyR7rSa/" +
                "tDvtnm8ccYuJeqouw6z24nznElukElNzdwy9iueIAjZP1FRCxiWOwuH4KJicZ1D8JRvOzyA94NO6BXuV" +
                "my1EF7m+LqIFbsWQxoIw3Mkom6SLrWSrKgW5UI3AksFQxKRSGFogjkAO4nYhBFycAv5DFc9xLc40lanS" +
                "6BWZhUNz2PbNMdokVzE6c7Oh7YzKuLpFxJRX8dYoSEAausmVq7uyRVhe/AFL8ICR26SUzlpBeR/S1YZU" +
                "IIZOxW2u1/le6WBpwZN0/Lfts+uywDY3GKaULbgusy/pyIj26lljSS/YgKPVMfKAVInQgRz5QvEw2HCa" +
                "lEu90DdLxRqC8XoiLdWOxKXSpPhHhCXGLKdWD5PElIwRBeIpkZetfOeKsQjakdV7W3brUuOcBppPs7VO" +
                "yXrl1qHkxI3OgBF+pxh7t4sUGyp464H+iE9LYNf+G/atKnDNNMTWACInBtztStl5AzO+AvCF+7ITF36s" +
                "onmDZoNCwbIYe/0KkwTn8AKdbTFx9z4E5stWTjaADSu2QZBfkqRcZRELD2vnu8a1SA9t9RCx+LF6wm6S" +
                "2KpuORmE+1a64KDErwIEunuX6pWFFFHdQ1aBJKQFO3H2RyDsyQby++7FxdkL3mlE3re22dToBTdO87vU" +
                "6HyBuTGW3AYrrwMFVfsGnBdZCV1UFWDptqEkaXLoNhv1roYfe2cvHWXLJfoyzHLzQB01RJwDpqNb32z/" +
                "OsU+K+dFnlqQR4XU6+ve4OLsODjrctvdO9JGbXCea2cQTu5UIRwghBehL3MXK1sgRKamBZewh7gXeDyr" +
                "M2QZcNj7lNLpJsoCQxN3TGLRCR9yuFQm1AKAFx4xcw2w2n9/NN/5bafTevZP/xF8bY3N1X9+sfvDDDr/" +
                "+jUt+VVqr0wpPjpHB14OG1pY6lrFDRhMN0GUymAPZcb3faHFwBdPoC90vdfIRW5VuFCqbnJKbxhF2acy" +
                "Xrdm4M9ykUxCVAA0Jc5kUj2QC8rUafvpZjh4HuuFb7996l5dCkbREd2g0OCJg0VUqlT05p43ZYvRZQI+" +
                "9HREj3KNNN8hfbIs6v9ofQtpzq06FX/5r31k9P7p/jmmRBdv9tti32hdwJt5USxPnz/HuZ4MmF7s//df" +
                "HJGGkq1cc/cvd26TpeiyIhRShQ+YeabFPixKY6q4b5VyvfdpBqY7STOokzr12FpTXbyNZT76wvviDSsJ" +
                "YUG60BG4rblxRmq2Al6h6+M+qj2le2s4oyOYngVhOhWBC/wSGQEvm4w4/f7HH145EAzU3GwAwO1j7/vd" +
                "bn65FCA/q/AiNsirvvnN79l7D+LQ03Zifz2zJ6/dK7z8PhXfvzo55mdYYBAkxWzZw0CqsNYmab7H5AYJ" +
                "8rv423z3eaGTVYYA1Ggo9HI/6Diq+2P1+h/KMOBMF2y+E30PheUSNa8t4g2k6JT1xdhodd1KXzcZFS6b" +
                "Qc18lxISo4nPFwAZBgSM/2SbnH+/aMP/Oi26MfpBvBn+evbS/b65ft8b9c6O3eP5p8v+4KI3OjvxL4aD" +
                "3tmrllNd77coCuGZHBS+b3mgJIVwbP24SQlaXvaVEH4Ntrrw+NUFFbBTbhxj5UOzEcwEDujIrnvvvvbL" +
                "Nfsc/FpOR/ErEE5H5SLk17b4xHcBv1XPjEym2kvlsyI0q2teCTu2aaICfcD0Tsnb6NezF5WnT4HX+PQb" +
                "sLp6JOa/OxV10FDs6Ejhb3erYDFNYidD/pnpNjJJV3gEVyyxBnVqco1G3Yv+hxs4T3VPL2TCiQLm203m" +
                "CqsO9TSoEelzSbrJcVv9JiQkJB1RdiBreKP3vf6792NxgLjdw2FJE4+iVDhe0jSvVWjeFsQB2sIh74de" +
                "z+/D1Ll9+KGyz0O7YGfS847F5+qa3Xue65ybC/4TThWG2qBpk3hrlBoqpTtsMumy1CHiKa7HihX1fbVs" +
                "uzuy7xxTWw1LdPwLKtUgHpSrYqlbwCVjEPBxXNx2vUDVq9m6xMRktdlQI2lhwsDfeWQGuV3pmHZEizu/" +
                "YZSg0tKvwD0VgWke2qG1Jld15EeyjOvkfqOv+/ghCEtRH3gqR8XqEx0EVOVsfCaV+Qzyif+seNg7ma0U" +
                "FmpTHDHQlaFCoBGvSSH5sZ+/tHCPsUNAd1IOV8s5D9cK9Ct8hXaLs2cEQKfZwXMaHOFFT8QqT8YOlnmy" +
                "9m15KJ7H+HzC51T3EbUVn+S0VMPvnBzgm3XVds2AslkQOgryXnyHncTvRPwH/CcRZ+IFCkuK0zNQcDX9" +
                "/OILNifD40t8jMPjMT4m4fHkS7i/+PzqC717LAZ8oxHYuE3deZnaWOIVjYz30QT3jXN7D0PDBSWs8yjl" +
                "3IBKqRwMhvi57S9l4Cs8yDhWmSvE7ReUkq5D89DVl9CFr+zFoUzd4/CRSjo+Dw3NE+cNMPr5rgT2FnHI" +
                "wGAlU7/zJh/RoLIDpLd2zInZ7UExIKfyskbW9ghZsvKtCYj9EXaK/L8QeRxhbs1tVxTwW51q1sQSaru3" +
                "XZsPqqxsjkxXcTyR1j5wtPqUi8tfafqZlMfNaTVu8t1IjuLwQuNvNBGzQzO9fG1zWGJ30uEmWI6AVf4C" +
                "q8R1wEMsfn6nOTB06I2QIcphpgoKuiNK8zhbJYparTRuUslm7PNSi5/XdfeZG1929kIW5MoO9ypgq9gS" +
                "1wK1RWx2Hgyb6cJF26oNErLOA1MlD0jz3+MQv3YYL5OmWLFJ4lWsOpd2gGmFFq/xHycc/rnhmdAIco1V" +
                "WV52lx7Qq6bGGYnGmPcD4zcVd7a1RV7za//aPnUt+7d6xNo/piMTpTuocLs4M9Iud15B0WWXu7HwU4tU" +
                "I6L2e6a45c8q2ajke0gIBRye0P18+84QN4n43/pt3Rs2R/lVnhyFm87aSfAKU2JdyDMypTNjJ0cXpmFm" +
                "P1tja74UtMqsWnMXCNujjdG0w10XMzduxDgcvHJquVwaTYaiuRP/zqTLpTLjyhgQOEtHtIcu1xvYSmFq" +
                "/pXlJdMcNC/HCww31RdqCj0BnYszn/Cv3XiiVQuZ41ToVqN8Lwx5ug4ojgphB1Mlz5cri3/RrbzPP2K9" +
                "MlzHkyioeRw8nbttj/huPFxdPllF7ARFBoDP/v7S68fueOjnuBuRjhCgsJ/CgLfFjtm+67/Vh8pKU6Al" +
                "7XDTvEzjW/DCmH2isohC2lvqX0/LGWD6naXTwl1ZugsDPaVqi2bDAMCrqS9l+MPUYSaZ++GBMCjnzbZy" +
                "1KYtucEcb05haQUb34XV6E1S7EbGyg0MnBx7qKj84tcv0hLelbzuTq4yzjHF8QckaBKaarieXQsOX+Hd" +
                "jCwKvMFDp+a3BeSVLZ9ytrHU6+0RRz/t9K+ousfB45Kt1v8AeUXZJyI+AAA=";
                
    }
}

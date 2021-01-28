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
                "H4sIAAAAAAAAA+07a28bN7bf51cQG+Da3spKE6fZ1hf+oNhKotaWXEnJtg2CATVDSbMeDVVyZFm9uP/9" +
                "ngfJeVhOuthrLy5w3SKeGZKHPO8Hj6PoSt+qQdk3RptznSorFD7GCTxHUTTWM11OSlkqURr5D5WU2uxi" +
                "W0pT+tFp+P7pc2vSQtkosqXJikVzDPZJTLYuM13AjOg6l4m61InED2KNb3HuXmF4nmtZvn6FA0UBsOIy" +
                "W6no7H/5J7qavDsVKyBHVsYru7DP26SJsqI8eSluZR5FzwQMGpnnYqaW8jbTxo1OPpyf9yeTsxfu/W1v" +
                "cPlh3D/7AX8i9/H6sjccDobvYhztX5wd+9mD4cfe5eAivhpNB6NhjPPOjl+6wdrH2E3sTfsX8Ztf4/7w" +
                "42A8Gl71h9P4/H1v+K5/dnzilp2PhtPx6DLs9cp9/zDsvbnsx9NR3Pv5w2Dcjyf94WQ0jgFo7+z4Ozdr" +
                "OriCLUYfpmfHr/3px/3+1fUUYf0NKeH5Iv5D3GSFWgHfEiuM+n2jbMny5Gk3mfbG0xj+nfYBhfh8dHk5" +
                "mABSQIFv90z5OBhdwu9JfN2bvofZw8l03BsMpxOY/8IT892od9kG9rI+9iUoJ/WJtSG/CHnzKmpx5914" +
                "9OE6HvaugMovvmsPtiDBlNetKePRm5FDEUb/1hq9HAx/8sC/b42N3vzYP5/60R+Q+nZnS7VqkvntGCbE" +
                "cIDh5O1ofBV7ITx++SIIhSMWiEv//CeURZCHjzAPhQImegrWzor/0pgnmhOYwfDtKIy9wjPVxKBxruEo" +
                "HvwUT0aXH6bEpxNg4uPrcWXE4GjTZWbFSlkL1kkkuihlVliRFXNtVmx/5ExvSlEulTC4UFhc2RFZV3Xp" +
                "61rbjCyX0HORlVb8QwN2VsgiFXlW3NjIqsKCDaXNf8RBtqA0LyZwSKQfeVm5lKVYyZ0AM6LEapOX2TpX" +
                "4mL0VkijhF2rJJtnKhVLZVQD9BXOhXm1LWh5nOp53NqsV5YyWQKUROd5ZhFPPUNzbMWh9GOlFlavFGEh" +
                "YEagwVHk15/75SNaDWbdr44D5NhBxn3f5nIB1E0ztOZgIrZLBVANgAY22EQVSsADbDyDh6JUZm1UCUeR" +
                "QE+RZvO52GblEowJ0qEMJ9QEhNZ7niJbtS3znchWa21KWZQCmApkLdIct0ZsAqoznWbg7A79eWBioQl+" +
                "kitp9k1Gmz+HU7FonZ4m2qjT05qHnCnYT4nNOmVcs5IPX9ZE7iiaaZ3DYWNE7rGkf78A1iglgwqQ+C11" +
                "nloBx5ZIAfbOM0VEIAlixK0q8QE8OFh20h0DLAf6sAJ0RfSMtqgtUkBAHhaHRt3qfIPfjVibzJKBOMLT" +
                "pGoOBgPovDsFAOKvDTWjLZcqQJEpAlgddaqptwqihazc3Z/63NLk5/aI1LNaoubAq5KxR3qs1znqWFbU" +
                "AQxXuHp41CXE+hUusGJTZEAFlLZUFSVr6GxH9qCQK+UIsVQyRUF1SmwJOoYvIFygC1myrO1HVLNiC3oO" +
                "Ag/ilaq0K3oQY7TnAHQ4qQbl8Wwk48Fq5JfiFsRBOA7t2XX8aRpAm9nSOsn2xkcaI3e2QzugDhEb1xD0" +
                "NSlMh0G2E64LLXOnzCt5o3iRmw+4o4RpCvlk3hV/X6pCqO6iK3Z6Y7wJJSwKDQAdf6S1wFmJFsGpklp1" +
                "cIlIZCFAU29VnZ10bqFW63LnpBGpx9gwb2u426Xe5KmjnKeTzf4AWw8oAyEZTk1rcJYugOdb2AXQDDIQ" +
                "jlkjDkpBODQQ2gBfSvTXxMEuxLXvWThYRmqBMsmPC3rh1StC7ZMX+NonpsCjGZQyZWvCZwY0wKIUqTQp" +
                "kLOUZDnI2GYLsKbHuYITIqarNXCO7cpujVhXxFyA7cYAeic2ln1PolcrIGlCdAR5baxnkZckhVmyycFG" +
                "JxrkPCtw+txIkm+cZjHwLBIlBhenJOAq2ZTZLelqkRglLRrnwYWINhyVwILo2XSrj9EBLdA5+c2DfVB3" +
                "4JesJceENuqvjFwXYKO1hV1Aug/pWwyvYG5gEziCWmtQgkM4+fWuXDqXeitNJmc5eb5EkgU9wEUHRzXI" +
                "BYEuZKE9eIZY7fFnwBYBLuJ0HHyh3SyAgDBxbfRtlrLtIjkFMwjCm2czI80uIlNFW0bP3hoyJMg+4gia" +
                "zaZ6OhFmbsRZ+hTu7X4QBMiOFbILEJHeg7BHQhF1poY0Mxi8VC2MUmQG5/CQarAyAGcO/k1vffAA2G2S" +
                "cmPIs1X7sVkdlI4gmxVKM8qN9N4C5daF6WQH7JoYCiIvMSsuLAafvGahysobAViZa7d7iKlFsoSYtSve" +
                "omG+A9bkYLEkZWHSeNclyd59GF+8JQ97guHk4R2YTvhfblEg0B+C7FjFg2hP0ebVBL1+OiYk/DIZQOG1" +
                "6F8a4wCVZ3hoILiQJaN4zGRygwg3zvD/TvVpnerWgF1c/mmn6qf/X3KqD/lUTodwuY0WClKI0uzYgEy9" +
                "CHOJip/vTdoCQ3EC/m6N/Z3IBINMr8cyeg+c2lPSeJPn1CGYlZkqtwrkotzqex6T+IcGD5RJJiDL0Ueq" +
                "0J3w+py1+ucNLDAFGgCjS1eVewok3WH2oCghBMKx1vlFMMQkUSuFSSDIVFhJSSXKDODQRQUzlLN1MFdL" +
                "NdADkkCyYqBq6GVI/dEc77wxZJrgZ1hyiMrWwaS24FnoKihWoegGbLXJFlnaNqNk+B1yHVHOX4JIg0rR" +
                "mXkzYCEA8dQ+6orBnBR0iwiRcvtkbabCucj5l1p3MKJyIJoEvSYl8rqaFeCSZNqtKqx34SmEluKPJ2F1" +
                "JWP7uA1m2WTBnTd4jm+/VwKKRP4qQv5p+0S6SkbDoeUdrK2y1iY+M6NvFCJJImaxGIMFCXS5slhQ4ItO" +
                "A4yd11U3pXp3854GOzZ/e7gGrGD2VMh1QKng8OR6EEF0uX8ORQJWvXIN4imKhw8Uvpx7bn1lPZ7VSkfk" +
                "qyA3yu58nQSVllwmVtl8xIzP5J0iyERwMFQmqRAFUSeEAXYpIYciMkFOqIjCNH7vbGwX6nU9nEYbPgNL" +
                "IhPWI2QX4kogC+0qgl0wWFicUyUEemiWXDXv2T54vrbHgVpAIxCCN0ijNqEYaOTrRVxZouqpCyzr9Tdf" +
                "qcR6qMSI3J1Bb0jwYONUzSXEWeI4HIyPgSF5DilfuuOoDIICPqlbULudQmAx12/Ri8xFApErsMCdigJI" +
                "uppK7yWiwA1KC5gf6H0YSfI8K4QRUAHQzmmAUVNAdsjb2ZMluabEVBq9IUVwUI46vhxGexQqQfNtdrSb" +
                "UTnns5jhYSDFGyP7AKYvGtdu3qqSYHVvB8TA08VuC8+UrYJkPgSnLWaAx5yLm0Jvi8qa0vyn0Mn7uthz" +
                "EV+HiwlzigxcNdmnb6Qz7QCRUQWJd2g6Ah6S9BAs4B5fBB55ZcXEltd5Pu/WioUCvfJMWsoQiTpBe/h3" +
                "jHnEoqB6DuPCKEwRAoLxkKsyvbO2mOfsCdy9zvKyzDjLgJrSLpxTOF67TfAEmOgc8PfbJFibXWVYMLER" +
                "WRw+J8269kNYlqqmtYMM2xiPmfCw05WyyyZU/AJzVzywFw6OVSDeoHIgEzDdxQK+QufvrFnAriNm7gqH" +
                "pvl0lIMIQH7DmgYcS9OMsyci3FH9bNe4FBGhnR5AEseq0/XS1NbFyFE9XJDShQUFc7VJIKO3md5YCPvU" +
                "HUQKePysZOvMBqcbzXYQsvcuLs6+jai8gdrQ2Glu9IorocVtZnSxwmAXc2iDqdShgjR8B6aJVIHum0pQ" +
                "ZtuSiSw94p3G/avRx/7ZC8JpvUY7hTFrEfCi8oYzrHRo60vnX8bVx9i8yOMJXKiQvL7uDy/OXjojXO25" +
                "fzvapQNWcesk37Gagv1DnOH55jPW1caWOCNX85KzUayGgDWzOkdaAWm9xaisaaosUDLlIxJtTvCAo7Uy" +
                "IaQHmPCKAaifqP3wYxnFrxuV6Nk//SP4khkrpP/8YveDxDn/8uUqGU2qkMzJ4TlDBmYMa1KYrVrFNRSM" +
                "GIGFymAZZMH3daFKwHdHICd4PdcIKm5UuBCq73BKX3h9VWcyXqAWYLEKkc68sQcoHmA6qx/FOVgqk/04" +
                "GQ2fJ3rla2e/9q4uBQPoil4QYTCzQQFqKSYaak+Vqj7ITt07lK7oU9SQFXuYTnpEtRutbyBeuVGn4i//" +
                "dYAUPjg9OMfI5uLNQUccGK1L+LIsy/Xp8+fYYpMDtcuD//4Lo2goYio0F+4KZxmZey66QebUqICRY1Ye" +
                "wKIsoWT5RilXNp/noKqzLM98vUftk1e8RWUi+pT54g3LBgFBrFDv3c5c8kLh2gCd0MRxAZSK8lgQdcjS" +
                "PSKBORWBAPQNSQDf2iQ4/e6H71/xDHS9XCGAefdPfOB2mvx8KYBtVuHlaeBTY+PJ7/l7P4Nh01biYLuw" +
                "J6/5C15Vn4rvXp28pFeYbXBChmGumwFuf6tN2vqMEQoi4jfwt+48utLpJsdxqgqUen3gBRpE+7HK8g9F" +
                "C3CiC1bTmb6DHHCNktYRyQ5CawraEqyJusKiz3KMCvfCIFa+oAgRzsyHAAAMDT66dNJEDpy/7cB/3Ygu" +
                "d74Xb0a/gBvj58n1+/64D66FX89/vRwML/pjMOXuw2jYP3vltd3bJ/IyeCY3i6M0bxIycLTWN4NUU6t7" +
                "uWqGX4NVKTx+fUFt2inXeDFdoR4GJgK7aiTXnbdUB9WaA3ZukRNNHAXE6aicPfzSEb9y2f63+pmRyJQw" +
                "qWJRhrpy2wZh2hTwA6J3K9rGv0BEUr39GmiNb7+hF68dienvTkXFLmQ7mk347S4ALEY/bFTIFDPeRqbZ" +
                "Bo/g0hyWoG6Dr/G4dzH4MMEIqbanZzLBRAbzRSRThUWHyg9UM/ThIV26uK1+ExICjq6oioUNuPH7/uDd" +
                "+6k4RNju5ajCiXtGahSvcFo20iuvC+IQdeGI90M75/dh7Nw+/FLb56FdsIjoacfsc8nJ/j3PdcHFAD+E" +
                "3X0hzm/rJF7wZIZS4C6rTLauZIhoiusx2UR536w77jrrG0fUqKWJjn5BpFrIYzxaaeq9yRVhzh6trex+" +
                "EkDJp7l334jBaLv2RdzC8IDHubsFqV0rbnZFxEXacOtfq77X5j0VglkRKpeNklS9O0cyj5vofqUE+/gu" +
                "CFNL73hqR8V0Eg0EZNesfCaTxQIiiP+sWdhbmW8U5l9z7AbQtZY/wBFvNCHYsZ8+R7jH1AGg6yMHK3LG" +
                "wxXu/Aqfe91gkxhNoNPsoTn1ePCiJyKVR2MPyTxaB7Y6FLdOfDrhc6q7mOqAT3Jaysv3XvLzJbjquPy+" +
                "yv9DkUDeiW+w/PeNSP6Af1JxJiijluL0DARczT99+xkriuH1Bb4m4fUlvqbh9eRzuGr49OozfXssAnyl" +
                "hteqa+2992wt8YJGyvtojPvKub2FoT6Aaq6zKNUVv8oo7QuK+Knj709gFF5kkqjcZdv2M3JJN2dzf9Tn" +
                "UDOv7cWuTN1hnxDWIVwcGsoizhqg9/NVB6wOYj+AwdSleT1NNqKFZRdQj/a0dNn7PV3YaFp9bKB1v9sr" +
                "3fjyA/j+GGtA/m8zHoeZ97qqawL4tSIzS2L58IJGJ09tYbunuQbiiWT2gZM121Fc9EotyiQ6rqGqdeXu" +
                "emcUOxfqU6PWlT1y6bm7p2i9L+RwrSbHQCl/01TBOuRuE99o0+7sOfIqyDOqrqMaCLrSyYok36SKaqfU" +
                "F1KLZezzSoafNyX3meszdtpC+uOSDvcpQKtpEmcCjUWsdH4alsSF87V1DSRg3QfaPx7g5r/HHH7pMJ4n" +
                "bbZiTcSLWL2B7BCDCi1e498OHP25LpdQ9nFlU1ndSlf2z4umxmaGVj/2A30yNWN2b4uiYdX+tX2aUvZv" +
                "tYeNP2IjFaU7pHAnuDDSru9fIdE1lbt/8L2FlB6i6HuK8NpntThU8tUhOAF2TGh6vmqCcYuY/7yuddfX" +
                "brZXRXocbiYbp8ArR4npIHexVFaMrRveb4am+nyLBfeKwSq3asu1HyyBtnrH2iKL2dXEtQCHQ4cTy/Xa" +
                "aNIOTQR/Z7L1WplprUcHDKTD1k/2iw3sojAUf3htRSo3mcLEInTbhQRCz0DEktxH91vXNmjVShbYrXmv" +
                "+h16L115Ezt4sEKp0ufrjcVfdGXuY41Ebwzn7ER/LMkEs+Yuw2O+vA5/dvM4wv4gd0jY8d1fOXqR2O/7" +
                "fHN1y6sRAObw45y/rqz3OY5xvau0NTu9KumnJZ1wK7zOkhuwuBhnopyIUtobS03DoS+XnvNsXrrrRncP" +
                "oOeUVlG/Fkzw0ulzFh6YO8DIcOfiQuuaV9N6l97e1jmvP2FlAMUXWg1M0wwrjj51P3npJ8VhwC1eZdVk" +
                "l9O6S7Vak8UcGxMQkVmommE/GtkQ7IPCaxZZlngDByLktwTQte0eRxC+RKm9vYa+7ehfEW8Pg/sWo+h/" +
                "AL0/ce56PQAA";
                
    }
}

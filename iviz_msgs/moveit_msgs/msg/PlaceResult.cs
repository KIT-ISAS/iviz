/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlaceResult : IDeserializable<PlaceResult>, IResult<PlaceActionResult>
    {
        // The result of the place attempt
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
        // The full starting state of the robot at the start of the trajectory
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart;
        // The trajectory that moved group produced for execution
        [DataMember (Name = "trajectory_stages")] public RobotTrajectory[] TrajectoryStages;
        [DataMember (Name = "trajectory_descriptions")] public string[] TrajectoryDescriptions;
        // The successful place location, if any
        [DataMember (Name = "place_location")] public PlaceLocation PlaceLocation;
        // The amount of time in seconds it took to complete the plan
        [DataMember (Name = "planning_time")] public double PlanningTime;
    
        /// Constructor for empty message.
        public PlaceResult()
        {
            ErrorCode = new MoveItErrorCodes();
            TrajectoryStart = new RobotState();
            TrajectoryStages = System.Array.Empty<RobotTrajectory>();
            TrajectoryDescriptions = System.Array.Empty<string>();
            PlaceLocation = new PlaceLocation();
        }
        
        /// Explicit constructor.
        public PlaceResult(MoveItErrorCodes ErrorCode, RobotState TrajectoryStart, RobotTrajectory[] TrajectoryStages, string[] TrajectoryDescriptions, PlaceLocation PlaceLocation, double PlanningTime)
        {
            this.ErrorCode = ErrorCode;
            this.TrajectoryStart = TrajectoryStart;
            this.TrajectoryStages = TrajectoryStages;
            this.TrajectoryDescriptions = TrajectoryDescriptions;
            this.PlaceLocation = PlaceLocation;
            this.PlanningTime = PlanningTime;
        }
        
        /// Constructor with buffer.
        internal PlaceResult(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new PlaceResult(ref b);
        
        PlaceResult IDeserializable<PlaceResult>.RosDeserialize(ref Buffer b) => new PlaceResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
            TrajectoryStart.RosSerialize(ref b);
            b.SerializeArray(TrajectoryStages);
            b.SerializeArray(TrajectoryDescriptions);
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
                size += BuiltIns.GetArraySize(TrajectoryStages);
                size += BuiltIns.GetArraySize(TrajectoryDescriptions);
                size += PlaceLocation.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "94bc2148a619282cbe09156013d6c4c9";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbXPbRpL+zl8xtao6SRuJSiTHm9WVPtASbTORSIWkvXFcLhQIDEmsQAyDAU0pV/ff" +
                "7+nuGQAEKTtbe1Lqqs67FRPATE+/d093u9W6MZ91r+jmuckvTayt0vQziPC71WoNzcQUoyIstCry8J86" +
                "Kkz+ENgizAv/dVy+//ipsWimbatlizzJZpvfcE6UJ8siMRlWtG7TMNLXJgrphVrSU5C6R3yepiYsXr6g" +
                "D1kGWEGRLHTr4n/5T+tm9OZcLcCOpAgWdmZPmqxpJVlxdqo+h2mrtafwMQ/TVE30PPycmNx9Hb27vOyO" +
                "RhffuefXnd71u2H34u/0p+Ve3l53+v1e/01AX7tXF8d+da//vnPduwpuBuPeoB/QuovjU/ex9jJwCzvj" +
                "7lXw6kPQ7b/vDQf9m25/HFy+7fTfdC+Oz9y2y0F/PBxcl2e9cO/f9TuvrrvBeBB0fn7XG3aDUbc/GgwD" +
                "AO1cHH/vVo17Nzhi8G58cfzSYz/sdm9ucfLF8d+IE14u6j/UXZLpBeQWWZXr31baFqJPnnejcWc4DvDf" +
                "cRckBJeD6+veCESBA9/uWPK+N7jG36PgtjN+i9X90XjY6fXHI6z/zjPzzaBz3QR2Wv/2JShn9YW1T34T" +
                "yeZFqyGdN8PBu9ug37kBl7/7vvmxAQlLXjaWDAevBo5EfP1b4+t1r/+TB/5D49vg1Y/dy7H/Cn3aU/bB" +
                "FnqxyebXQywIgEB/9HowvAm8Eh6fekUrmQV16V7+RLoIfXiPdaQUWOg5WMOV/svfPNOcwvT6rwflNzBr" +
                "r64GG3j1B0Hvp2A0uH5HmgwVhRCf3o4rJwbUxvPEqoW2Ft5JRSYrwiSzKsmmJl+I/wknZlWoYq5VThsV" +
                "HFmhj1TS1m1+uzQ2Yc+lzFQlhVX/NKDOqjCLVZpkd7ZldWbhQ/nwH+mjeFBeR34RiAAT/mIBMizUInxQ" +
                "cCNaLVZpkSxTra4Gr1WYa2WXOkqmiY7VXOd6A/QNrcW62hG8PYjNNGgc1imKMJoDSmTSNLFEp5mQO7bq" +
                "IPTfCqOsWWimQmFFyYPDlt9/6bcPeDfcut+NkOE+BQ4ynfs6DWfgbpyQN4eLWM81oOYADTHYSGda4QcO" +
                "nuBHVuh8mesCqITgp4qT6VStk2IOZ0J8KEoMDQPh/V6mJFZji/RBJYulyYswKxSECrZmMQiaMTUlqRMT" +
                "Jwh2Bx4fLMwMw49SHea7FpPPnwIrUa3z88jk+vy8FiEnGudptVrGQmtSCPJFTeUOWxNjUiAbEHFPpf27" +
                "FbDGqbA0AVa/uUljq4B2SByQ6AyBEBNYg4Rwqwv6gQgOz862k0Pk4I8YQFu19viI2iYNBspndZDrzyZd" +
                "0ftcLfPEsoM4JGxiPYXDAJ8fzgFA/XXDzPhIPHsoYUwAFodH1dLPGtlCUjxsLz2xvPjEHrJ5Vlv0FLIq" +
                "hHrix3KZko0lWR1Af0G7+4dtJqxb0YIdqywBF0jbYp0VYqETPMGgshDpiTBirsOYFNUZMVk72Ir0BcoF" +
                "W0gAsTqPuWbVGnYOhYd6xTpuqw5yjOYaQAemBsbjxcjOQ8zIb6UjWIJAh88UMpoO0CYWOItme+cT5nn4" +
                "YI/4BLIhFuMSSd8mhxkZEjvTOjMhKTZhsQjvtGxy60E7aZjhlC9M2+ofc50p3Z611YNZ5d6FMhWZAUAn" +
                "n9BaSBanxN6U9OKItqgozBQslaiuxMl4K71YFg9OG4l7Qo3Itka7nZtVCrfqYTCfbPI7fD1IBiMFTs1q" +
                "aJXJIPM1TgGZpQ6UaNaYQ1pQIg1G55BLQfGaJdhGXvtWlEN0pJYos/64pBeP3hBqr7zC114JB57MoRSx" +
                "eBPBGWTAo2RxmMdgZxGy52Bnm8zgTY9TDQyJ0sUSkhO/8rAkqitmzuC7KYF+UCsrsScyiwVYikgh+rqx" +
                "X1Q+ZC1MolUKHx0Z6HmS0fJpDp4RdGIwJZ5ZpFXv6pwVXEerIgFCsM4synVoyTn3rlRrJVkJNrT2xmtz" +
                "TAFoRsHJH176B32PuGQJz9CSj/qrENcGbPK2OAXafcDvAjzC3eAQoKCXBkZwAMxvH4q5C6mfwzwJJzAz" +
                "AI7AAUDdp037hzXIhPY5VCEzHrxArM74I2CzEi7RdFzGQruagYFYuMzNZ3gw9l2sp3CDUN40meRh/tBi" +
                "V8VHtvZeE4/FhFgi5DY3zdOpsEgjSOLnCG/bSRCIHWoSFwiRjA5OSCISqahzNWyZpcOL9SzX8L1YOcWP" +
                "2MDLAM4U8c2sffIA6lZRsYJ3xrLqPHGrPYkj1q4WpM2kN6GPFqS3Lk1nP2CXLFCoPOwiDzNLyafsmemi" +
                "ikYAG6bGnV7m1CqaI2dtq9fkmO8hmhQeK+RbGGTqQhfyJxz7bnj1miPsGaWTB/dwnfh/uCaFoHgI3bFa" +
                "PpI/JZ9XU/Q6dsJI/JUngCJ7Kb5sfAdUWeGhQXFxSyb1mITRHRG8gcP/B9XnDarrHH5x/oeDql/+fymo" +
                "PhZT5TpE221rpnGFKFCGYgcy9iosJSr5vbVojSyJFtDfjW//YDbho/DrqZzeI1h7Tube5bkcs3QrE12s" +
                "NfSiWJutiMnyI4eHDBVFN/DvPVfozmR/Klb98wob8owcQG7EpT4PkQ6ZHSSGSIHoWwN/VTpi1qiFpksg" +
                "dKrcyZdK0hnQ0CYDy/nOhtt9oWIDfuASyF4MpkZRhnNqcsdQxzpP6DW2HJCxHdGlNpNVFCo4V+HsBr46" +
                "T2ZJ3HSj7PgdcUeqmJ5CpWFSjLMcBhECiOf2YVv1pmygayKIjdtf1uia5vDi4F8Yc0QZlQOxydBbNiJv" +
                "qyh8FLCTdlVhvS9/laml+v1ZRF3p2C5pI4TnlJcI+zZkTk+/VQpKTP4qQf7X+plslZ2GI8sHWFvdWjfp" +
                "meTmDuoEQZGKWSrGUEGCQm6YzTjxpaABZ+dt1S2pnt2656FO3N8OqUEUIp6KuCMYFZDn0EMEUlL/x0hk" +
                "YNWj1CCeo3j4SOHLhefGW7HjSa10xLEKd6Pk3tdJyGg5ZFKVzWfM9JujE2pmzMeyMsmFKGSdSAPsPMQd" +
                "itmEOyF+UTpL37dwE79Qr+vRMj5wD54E8NiOSFxEK4PMjKsItuGwqDinCyR65JZcNW9vFzxf25MbRElG" +
                "yQg5IG41GSVAW75eJJUlrp66xLJef/OVSqqHAiOzdjiYFSseDkYFKUQur45LxAQNSslTXPniByl1ICkQ" +
                "TN2GWneKgAVSv6UoMlURMleIwGHFVRluTcVbF1FIg68FIg+KPkIkR54FwShJAWgXNODUNNiOe7tEsijF" +
                "PZ/CR25WbAgOyuGRL4fxGZmOyH3nD3xarpGu0Ta64VEiJQeT+ADTF41rnbeqJFj17cAMwi5wR3ihrDUu" +
                "82Vy2hAGIuZU3WVmXZYl3PrnsMltW+y4jI9DX8ysKavJ/vrGNtNMEIVUaLwj0zHwgLWHYUF60gg89MZK" +
                "F1vZ5+WM2oYoBUXlSYjoaxx3SuuRvwMqzs0yrucILULCmCAQGA+5KtM7b0vFwx2Ju7dZ2ZawLtJKaEqz" +
                "cM7peK2b4BkwMino98dEVJtdAMHPYBZ7HMGTV936T1SWqpY1kwy78Z30ihrBe+pG2/kmVHqDtQv5sBMO" +
                "fatAvCLjICHQdZcK+Cj2uKzAVtQdqYlr4fAyfx2VJALEr8TSILE4ZlnAu9IRh3Xc0JXOmBA+6REi6VuF" +
                "XSema31NMYTrZYOUGxaczNUWQUfRP15ZpH36HpkCoY/cUoIpO5x2a/KAlL1zdXXxLR0zZKe6cdI0N1RB" +
                "wP0q+5zkJltQskuFaXiIB3AJ13BUisQUuN9UwJhtQyeS+FBOGnZvBu+76GETTcsl+SnKWb02u/KGc6yM" +
                "tLsJfo1Wn2PLJk8npFAReXvb7V9dnDonXJ25+zg+5Qhece0034mak/0DWuHl5m+sixWa0liR6mkht1Gq" +
                "hsCbWZMSr8Ba7zEqb4qWCDgZC4rMmzNCcLBECdOn9ICJR0pA/ULjPz+VU/y6U2nt/ct/lDSZqUL6r292" +
                "f4g5l19urrLT5LbDlAOec2RwY1STotsqsgKuyFHGCBHqnMogM+nXlVUC6R1BT6g9t5FU3OmyIVQ/4Zzf" +
                "yP6qecM3SVYXeKxMxRPv7AHFA4wndVRcgOUy2Y+jQf8E5WtfO/vQublWAgANnFKF4WZLA6hdMclRe65U" +
                "9UEJ6j6gtFWXswZq+2wJne2IazfG3CFfudPn6i//tU8c3j/fv6TM5urV/pHaz40p8GZeFMvzkxMasUnB" +
                "7WL/v/8iJCJqQNmRCXLhLnOeUaTnshsSTo0LlDkmxT42JUj2YQV3Wruy+TSFqU6SFFccF5526St1UYWJ" +
                "/sp89Up0g4EQVWT37mQpeZFyrcAncnFSAOWiPBVEHbHcR2Qw56pkAL8jFuBdkwXn3//9hxeygkKvVAiw" +
                "bhvjfXfS6Odr9E2RIlDztJTTxsGj39K3foXA5qPU/npmz17KG2pVn6vvX5yd8iNW57QA6bNZuxUI+2vU" +
                "bRqvKUMhQvwBvusuXxcmXqX0nasChVnue4WGaj9VWf6xbAEYXYmZTgzqv3ZJmnakogek1py0Qd20coVF" +
                "f8uBWvi+MNTKFxSR4Ux8CgBg5PAppLMlSuL87RH+hxIANXd+UK8GvyCMye/R7dsuhmBO3ePlB0zaXHWH" +
                "cOXuxaDfveD5lXHNP3GUIZzcKsnSvEtAfwTXCjcMUi2t+nLVCr+HqlKEfn1Dbdm51HjpusIzDMIECdXE" +
                "rnvvqfarPfsS3Hjgwt0JQTijKreHX47UBynb/1rHmZjMFyadzZAsOoyaPoiuTSV9YHq74m3wCzKS6ulD" +
                "yWt6+pWieA0l4b/DiotdJHZym/jbddXhPQVPeDRyxUI3OvnJilBw1xzRII+HwA2GnaveuxFlSLUzvZAZ" +
                "JglYGpHCFVEdLj9wzdCnh9x0cUf9qkIkHG1VFQs34AZvu703b8fqgGC7h8OKJpkZqXG8omm+cb3ytqAO" +
                "yBYO5Tzyc/4coc6dIw+1cx47hYqInnciPnc52X0mQrYUA/wnmu4r8/ymTVKDJ8n5Csy1VPQYl5UOMU9p" +
                "P102Sd9XyyPXzvrGMdUbaYOZpUo1iKd8tLLUrcUVY2jh07i47UsAXz7zrX4jJaPN2hdLi9ID+S7TLcTt" +
                "WnETJWwp0pZd/1r1vbbuuQgEKr60t1GSqk/noBbi+5wVuV8pwT59CKKrpQ88NVTpOkkOArdrMT40zbMZ" +
                "Moj/rHlYjPhidhVindI0gKmN/IFG6mgi2bEfP7XojLEDwO0jB4sOqBXu/A5/90Lmt6Ikicd35ls3S/CS" +
                "Zzxk0zOxypOxg2WeLGR5JVIyOvHxTPDU9wHXAZ8FW76X72zySxMclib3++r+XxYJwnv1DZX/vlHR7/hP" +
                "rC4U36hDdX4BBdfTj99+oopi+fgdPUbl4yk9xuXj2aey1fDxxSd+91QM+EoNr1HX2tn3bGzxisbGa/8k" +
                "vL2H4eG6aq3zKNXcHGp/dO0rDfHjke+f4CsewihCKVRu2/YTSclsrpb5qE9lzbx2loQyfU9zQlSHcHlo" +
                "WRZx3oCin686UHWQhuzQtLCN9jT7iAaVbZDe2jHSZbdnumjQtHq5Qdb2tFe88uUHxP6AakD+32Y801R1" +
                "TQG/VmQWTSwe37AxyVPb2JxproF4Jp19BLPNGU+XvfKIMquOG6hqtNzd7IyW4MJzajxktUMvvXR3FK13" +
                "pRxufvMYnPKdpgrWgYxw+unV5mTPoTdBWVFNHdVAcEsHM2jpKiYq3FxILZexJ5UOn2xq7p6bM3bWwvbj" +
                "Lh3uVQmtZklyE9jYJEbnl1FJnO6NTQtkYEgvd7rBR6T557jDLyHjZdIUK9VEvIrVB8gOKKkw6iX924HD" +
                "PzblUpZ9XNk0rLrSlf/zqon0c0b1p/q00iNzMjVntnUENKemG//eOZta9qf6w41/xMYmyj2ksic4y0O7" +
                "3G4hcZvK9R/8bCFfD0n1PUdk714tD6WvEgQkMJHr+aoLpiMC+ed1jV5fc9geFf/jsjO5gQW1HEGiTJuH" +
                "NS8m3o36m+VQfYqRrlpXRunU6rXUfqgE2pgda6os3a5GbgS4RLrEGHMyuWHrMMzwN3mCRkU+rs3owEE6" +
                "av1ivxldQRQViy/trVjlFnOamJXTduUFwkygYhGSfnFxazeLb9H4xb032q5+l7OXrrxJEzxUodTxyXKF" +
                "Plh8wi1zn2tEmPaROzvzn0oypVtzzfBAmtflP7t5GmV/VDqs7PTsW45eJXbHPj9c3YhqDEAk/PTGui1x" +
                "yutdpW1z0qvSft7iOswQzjKJ7uBxKc8kPVFFaDE7QEPD5Vwu/06TKdc/qlaWmfK1iue1sMBrp7+zyIep" +
                "A0wCdyGuHF3zZlqf0ts5Ouftp9xZgpKG1galMTQ6hNwkzzw79YuC8oPbvAD1/p2707qmWm3IYkqDCUSI" +
                "+2dRbh6NfUiChJ7aLBgqoA4cVMgfCdC1455GEb7EqZ2zhn7s6N9Rbw9D5hZbrf8BvT9x7no9AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

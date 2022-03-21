/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupResult : IDeserializable<MoveGroupResult>, IResult<MoveGroupActionResult>
    {
        // An error code reflecting what went wrong
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
        // The full starting state of the robot at the start of the trajectory
        [DataMember (Name = "trajectory_start")] public MoveitMsgs.RobotState TrajectoryStart;
        // The trajectory that moved group produced for execution
        [DataMember (Name = "planned_trajectory")] public MoveitMsgs.RobotTrajectory PlannedTrajectory;
        // The trace of the trajectory recorded during execution
        [DataMember (Name = "executed_trajectory")] public MoveitMsgs.RobotTrajectory ExecutedTrajectory;
        // The amount of time it took to complete the motion plan
        [DataMember (Name = "planning_time")] public double PlanningTime;
    
        /// Constructor for empty message.
        public MoveGroupResult()
        {
            ErrorCode = new MoveItErrorCodes();
            TrajectoryStart = new MoveitMsgs.RobotState();
            PlannedTrajectory = new MoveitMsgs.RobotTrajectory();
            ExecutedTrajectory = new MoveitMsgs.RobotTrajectory();
        }
        
        /// Constructor with buffer.
        public MoveGroupResult(ref ReadBuffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
            TrajectoryStart = new MoveitMsgs.RobotState(ref b);
            PlannedTrajectory = new MoveitMsgs.RobotTrajectory(ref b);
            ExecutedTrajectory = new MoveitMsgs.RobotTrajectory(ref b);
            PlanningTime = b.Deserialize<double>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MoveGroupResult(ref b);
        
        public MoveGroupResult RosDeserialize(ref ReadBuffer b) => new MoveGroupResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            ErrorCode.RosSerialize(ref b);
            TrajectoryStart.RosSerialize(ref b);
            PlannedTrajectory.RosSerialize(ref b);
            ExecutedTrajectory.RosSerialize(ref b);
            b.Serialize(PlanningTime);
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) BuiltIns.ThrowNullReference(nameof(ErrorCode));
            ErrorCode.RosValidate();
            if (TrajectoryStart is null) BuiltIns.ThrowNullReference(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
            if (PlannedTrajectory is null) BuiltIns.ThrowNullReference(nameof(PlannedTrajectory));
            PlannedTrajectory.RosValidate();
            if (ExecutedTrajectory is null) BuiltIns.ThrowNullReference(nameof(ExecutedTrajectory));
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
                "H4sIAAAAAAAAE+0bbW/buPm7fgWxAkuyc9xr0+vuMuSDmzit7xI7Zzvd9YpCoCXa5iKLPlKO4xv23/e8" +
                "kJKsOG2HLRkGLC0SSyQfPu9vpKPo0tyqXtG11thTkyonFH6ME/gcRdECRnURL9zMPR+aiSlGhSyUKKz8" +
                "m0oKYzexK6Qtds0cl3PEMpN5rtK4WvaFBepOJauiuWKaGVm8fsXgdD6LC70AJE/+wz/R5ejtsaij1+RR" +
                "pPPi6KW4lVkUPRMwaGWWiYmay1ttrB8dXZ+edkejkxf++bzTu7gedk9+wJ/Iv7y66PT7vf7bGEe7ZyeH" +
                "YXav/75z0TuLLwfj3qAf47yTw5d+sPYy9hM74+5Z/OZD3O2/7w0H/ctufxyfvuv033ZPDo/8stNBfzwc" +
                "XJR7vfLvr/udNxfdeDyIOz9f94bdeNTtjwbDGIB2Tg6/87PGvUvYYnA9Pjl8HbAfdruXV2OE9WfkRBCM" +
                "+KO40blayEInTlj120q5ghUr8G407gzHMfwed4GE+HRwcdEbAVHAgW93THnfG1zA31F81Rm/g9n90XjY" +
                "6fXHI5j/IjDz7aBz0QT2sj72OShH9Ym1obAIZfMqakjn7XBwfRX3O5fA5RffNQcbkGDK68aU4eDNwJMI" +
                "o39ujF70+j8F4N83xgZvfuyejsPoD8h9t3GFWmyz+XwIE2JAoD86Hwwv46CEhy9flErhmQXq0j39CXUR" +
                "9OE9zEOlgImBgzVc8TeNBaZ5hen1zwfl2CvEqaYGW3j1B3Hvp3g0uLgek5yOXjyFHVceDFAbz7UTC+Wc" +
                "nCmRmLyQOndC51NjEWOTCzkxq0IUcyUsLhQOV7aEbqs2vV0ap3GiE2YqdOHE3wxQ54TMU5Hp/MZFTuUO" +
                "nClt/iMOsvukeTGBQyb9yMuKuSzEQm4EuBElFqus0MtMibPBuZBWCbdUiZ5qlYq5smoL9CXOhXm1LWh5" +
                "nJpp3NisUxQymQOUxGSZdkinmaCPdWJfhrHCCGcWiqgQMKPkwUEU1p+G5QNa/fGTCKvjEnLsIeO+55mc" +
                "AXdTnQBzwUWs5wqgWgANYnCJypWAD7DxBD7khbJLqyACCAn8FKmeTsVaF3NwJsiHosTQEBBaH2SKYjWu" +
                "yDZCL5bGFjIvBAgV2JqnGW6N1JSkTkyqIertB3xgYm4IfpIpaXdNRp8/BaxYtY6PE2PV8XEtPE4U7KfE" +
                "apkyrbpg5Iuayh1EE2MyQDZG4h5L+3crYI1TsjQBUr+5yVInAG2JHIBQl1g9UcQE0iAm3KkCP0BYBs9O" +
                "tmNB5MAfNoC2iJ7RFrVFChjIw2LfqluTrfC9FUurHTmIA8QmVVNwGMDnzTEAEH/aMjPacq5KKDJFAIuD" +
                "VjX1VmUm0cXm/tTnjiY/dwdkntUSNQVZFUw98mO5zNDGdF4H0F/g6v5BmwjrVrTAilWugQuobanKC7bQ" +
                "yYb8QS4XyjNirmSKiuqN2BF0zF9AucAWdDKv7Udcc2INdg4KD+qVqrQtOpBjNOcAdMDUgPEEMZLzYDMK" +
                "S3ELkiCgQ3u2vXy2HaDTrnBes4PzkdbKjWvRDmhDJMYlZHzbHCZkUOxE68zIzBvzQt4oXuTnA+2oYWaJ" +
                "EpVZW/x1rnKh2rO22JiVDS6UqMgNAPTykc6BZCV6BG9KatHCJSKRuQBLvVV1cRLeQi2WxcZrI3KPqWHZ" +
                "1mh3c7PKUs+5wCenfwdfDyQDIxlOzWpwlslB5mvYBcgsdaBEs8Yc1IISaWC0BbkUGK9Jgm1IbN+xcrCO" +
                "RJErLHgN8KikPz7rhcdgCLVXQeFrr5gDj+ZQipS9CeMMZIBHyVNpU2BnIclzkLPVM/Cmh5kCDJHSxRIk" +
                "x35ls0SqK2bOwHdjAr0RK8exJzGLBbA0IT6Cvm6tZ5WXpIU6WWXgoxMDeq5znD61kvQbpzlMPPNEid7Z" +
                "MSk4FhX6lmw1T6ySDp1z70xEK85KYEH0bLw2hxiAZhicwualf1B3EJeco8CEPupPTFwbYKO3hV1Au/fp" +
                "XQyP4G5gE0BBLQ0YwT5gfrUp5j6k3kqr5SSjyJdI8qB7uGjvoAY5J9C5zE0AzxCrPb4GbF7CRZoOy1jo" +
                "VjNgIExcWnOrU/ZdpKfgBkF5Mz2xEqovclW0ZfTs3JIjQfGRRNBtbpunV2GWRqzTpwhv95MgIHaoUFxA" +
                "iAwRhCMSqqh3NWSZpcNL1cwqRW5wCh9SA14G4Ewhvpl1SB6AulVSrCxFtmo/dqu9wjNktUBtRr2RIVqg" +
                "3vo0nfyAW5JAQeUl1tS5w+ST18xUUUUjACsz43cvc2qRzCFnbYtzdMx3IJoMPJakKkzaELok+bvr4dk5" +
                "RdgjTCf378B1wn+5RoXAeAi64xQPoj9Fn1dT9Dp2zEj4YzVA4bUYX7bGASrPCNBAcaFKRvWYyOQGCd7C" +
                "4f9B9WmD6tqCX5x/dVAN0/+XgupDMZXLIVzuopmCEqKwG3Yg46DCMKtU53uT1iBQnIB/G2N/JTbBIPPr" +
                "sZzeA1gHTtrg8rw5lG5looq1Ar0o1uZexCT5ocMDY5IJ6HL0ntpuR7w+Y6v+eQULbI4OwBp2qU9DpEdm" +
                "B4kSUiAca+AvSkdMGrVQWASCTpUrqahEnQEa2mhglmq2FtZqqQF+QBFIXgxMDaMMmT+6401whswTfA1L" +
                "9tHYWljU5jwLQwXlKpTdgK+2eqbTphslx++Ja4li+hJUGkyKcObNQIQAJHD7oC16UzLQNRJExh2KtYkq" +
                "8aLgXxjTwozKg9hm6BUZUbBVnUNIkmm7arHelZ/K1FL8/iSirnRsl7TBLVtdhvMtmePTb5WCIpO/SFD4" +
                "tH4iWyWn4ckKAdZVVes2PRNrbhQSSSrmsBmDDQkMuTKfUeKLQQOcXbBVP6V69vOehjp2fzukBqJg8VTE" +
                "tcCoAHkKPUgghtyvI5GAVY/cg3iK5uEDjS8fnhtv2Y4ntdYRxSqojfRd6JOg0VLIxC5byJjxM0WnCCoR" +
                "HCw7k9SIgqwT0gA3l1BDEZugJlTEYRq/hxv7hXpfD6fRhs/Ak8iE7QjFhbQSyNz4jmAbHBY251QBiR66" +
                "Jd/Ne7YLXujtcaJWklEygjdIoyajGGgU+kXcWaLuqU8s6/230KnEfqjEjNzjYFakeLBxqqYS8ixxWCLG" +
                "aGBKnkHJl244K4OkgDH1C6oEgYDF3L/FKDIVCWSuIAKPFSWQkGMn3CXaKkRBGlQWsDww+jCRFHkWCKMk" +
                "BUD7oAFOTQHboW7nSJZkhgpTac2KDMFDOWiFdhjtkasE3bfd0G5WZVzPYoWHiRRvjOIDmKFpXDu3q1qC" +
                "tXO3VCF2sd8iCGWtoJgvk9OGMCBiTsVNbtZ55U1p/lPY5H1b7PiMr8XNhCllBr6bHMo3splmgsikgsZ7" +
                "Mj0D90l7CBZIjw8CD4KxYmHL64KcN0vFSoFReSIdVYjEndJ6+G+MdcQsp34O08IkjBECggmQqza997ZY" +
                "5+xI3IPN8jJtvWdAS2k2zikdr50mBAaMTAb0h20S7M0uNDZMXEQeh/GkWVdhCNtS1bRmkuG2xmNmPOx0" +
                "qdx8Gyq+gbkLHtgJB8cqEG/QOFAIWO5iA19h8PferKSuJSb+CIemhXKUkwggfsWWBhJLU83VEzHuoI7b" +
                "FS5FQminB4jEsQq7Tpq6uhp5rpcHpHRgQclcbRLo6K02K5fhAThkCoi+Ltg7s8NpR5MNpOyds7OTbyNq" +
                "b6A1bO00tWbBndD8VluTLzDZxRraYim1r6AM34BrIlOg86YCjNk1dEKnB7zTsHs5eN89eUE0LZfopzBn" +
                "zUu6qL3hHSsh7ULr/PO0hhybFwU6QQoVkVdX3f7ZyUvvhKs9d29Hu7TAK6695ntRU7K/jzOC3ELFuli5" +
                "AmdkalpwNYrdEPBmzmTIK2Bt8BiVN02VA06mjCLx5ggRHCyVLVN6gAmPmICGiSYMP5ZT/LJTiZ79yz+C" +
                "D5mxQ/qvL/Y/yJzTzx+uktOkDsmUAp53ZODGsCeF1apT3EPBjBFEqCy2QWZ8Xld2CfjsCPQEj+e2koob" +
                "VR4I1Xc4pje8vuoz2aBQM/BYuUgnwdkDlAAwndRR8QGW2mQ/jgb954lZhN7Zh87lhWAAbdEpVRjcbGkA" +
                "tRITHXXgStUf5KAeAkpbdClr0PkOoZMdUe/GmBvIV27UsfjD3/eQw3vHe6eY2Zy92WuJPWtMAW/mRbE8" +
                "fv4cyg+ZAbeLvX/8gUm0lDHlhht3ufeMLD2f3aBwalzAzFEXe7BIJ1Qs3yjl2+bTDEx1ojMd+j1ql77i" +
                "KSozMZTMZ29YNwgIUoV273fmlhcq1wr4hC6OG6DUlMeGqCeWzhEJzLEoGUDvkAXwrsmC4+9++P4Vz8DQ" +
                "yx0CmHcf4z2/0+jnCwFicwoPT0s5bW08+i17F2YwbNpK7K1n7ug1v8Gj6mPx3aujl/QIsy1O0Jjm+hkQ" +
                "9tfGpo3XmKEgIWGDcOrOowuTrjIcp65AYZZ7QaFBtR+rLf9QtgAYnbGZTswd1IBL1LSWSDaQWlPSlmBP" +
                "1DcWQ5VjVXkuDGoVGoqQ4UxCCgDA0OFjSCdL5MT52xb8a0d0uPO9eDP4BcIYfx5dvesOuxBa+PH0w0Wv" +
                "f9Ydgiv3Lwb97smrYO3BP1GUQZz8LM7SgkvQEGhduAxSTa3O5aoZYQ12pRD9+oLatGPu8WK5QncYmAkc" +
                "qpFdd8FT7VVr9ji4RV41cRQIJ1S5evilJT5w2/7XOs7IZCqYVD4ryr5y0wdh2VTSB0xvV7yNf4GMpHr6" +
                "UPIan37FKF5DifnvsaJmF4od3Sb89QcADrMfdirkipluK1O9QhR8mcMa1N6SazzsnPWuR5gh1fYMQiaY" +
                "KGA+iGSusOpQ+4F6hiE9pEMXv9WvQkLC0RZVs3ALbvyu23v7biz2EbZ/OKho4jsjNY5XNM23yqtgC2If" +
                "beGA90M/F/Zh6vw+/FDb56FdsIkYeMfi88XJ7j1PTc7NgDCEt/vKPL9pk3jAoy2VwG02Gb2sdIh4iuux" +
                "2ER9Xy1b/jjrG8/UqGGJnn+lSjWIx3y0stR7kyvGnDzatbL7RQAVn/beeSMmo83eF0kL0wMe59styO1a" +
                "c7MtIm7Slqf+te57bd5TEajzsnO51ZKq386RLONtcr/Qgn38EISlZQg8NVSxnEQHAdU1G5/VMp9BBvGX" +
                "moe9ldlKYf01xdsApnblD2jEE01IdtzHTxHuMfYA6PjIw4q88/CNu7Ai1F43eEmMJhA2O3hOdzx40ROx" +
                "KpCxg2WBrD1XIcVXJz4eMZ7qLqY+4JNgS3X5zkN+PgRXLV/fV/V/2SSQd+IbbP99I5Lf4VcqTgRV1FIc" +
                "n4CCq+nHbz9hR7F8fIGPSfn4Eh/T8vHoU3nU8PHVJ3r3WAz4Qg+v0dfaee7ZWBIUjYz30QT3BbyDh6F7" +
                "AEXtuwN8Fl4e8StNZV9piB9b4fwERuFBJonKfLXtPqGUzPZsvh/1qeyZ1/biUBa+fNAOeWjZFvHeAKNf" +
                "6DpgdxDvA1gsXbaPp8lHNKhsA+nRjitd7v6dLrxoWr3cIuv+ba90FdoPEPtj7AGFL2Y8jjA/8+WNLzaZ" +
                "WROLhxds3eSpLWzeaa5/OeRxyPxKzLavo/jsla4ok+r4C1WNI3d/d0ZxcKF7anR1ZYdeBunuaFrvSjn8" +
                "VZND4FQ4aapg7fNtk3DRpnmz5yCYIM+obh3VQNCRjs6TbJUq6p3SvZBaLuOeVzr8fFtzn/l7xt5ayH58" +
                "0eFfldBqlsSVwNYiNrowDVviwsfaugUSsPYD1z8ekOZ/xx1+Dpkgk6ZYsScSVKx+gWwfkwojXuN3Bw6+" +
                "7pZL2fbxbVNZnUpX/i+opsHLDI372A/ck6k5s3tb5Fte7d/bZ1vLPuMP/wkYrUs18jYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

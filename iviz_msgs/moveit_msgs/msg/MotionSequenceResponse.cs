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
                "H4sIAAAAAAAACu1bbXPbxhH+zl9xU81UUiPRseS4KTv6QEuUzUQiFZJy43g8GJA4kohAHIMDRTGd/vc+" +
                "u3sHgBAVp9Nanc7UycgCsLe372933lPtVOksM5mamEirTE8TPcnjdKbW8zBXa53iR2bSWePa3Otu3iHY" +
                "c4BaWRbQskZjT43mWk1XSaJsHmaMAL/kWpmpyvEpM2OTK2CkBwbxX6z+ZaXTiW4MCGTIi/y7gCE9+jwL" +
                "fwZxJouxe07k0fplEqapztQyM9FqoiM1BTP6QU9WeWxSwTryKzcfP7kFUVBF57cIF2aVCmnxQqsYWxhz" +
                "hx8Qz2KZaNBGey4M4WZMjWliwvz1K0ELvgNa2Wic/Yf/NK6Hb1vY+F7HebCwM/uirpBGnOanJ+o+TIgb" +
                "fMxCqGOs5+F9bDL3dXh7ft4ZDs9euufLdvfqdtA5+wv9abiXN1ftXq/bexvQ187F2bGH7vbet6+6F8F1" +
                "f9Tt9wKCOzs+cR8rLwMH2B51LoI3H4JO73130O9dd3qj4Pxdu/e2c3Z86pad93ujQf+q2OuVe3/ba7+5" +
                "6gSjftD+4bY76ATDTm/YHwRA2j47/sZBjbrX2KJ/Ozo7fu2pH3Q61zfY+ez4zyQJrxj1R3UXp3oR5vHE" +
                "wtJhYjYXK/ayG47ag1GAn6MOWAjO+1dX3SGYggS+3gHyvtu/wt/D4KY9egfo3nA0aHd7oyHgX3phvu23" +
                "r+rITqrffgvLaRWw8skvIt28atS083bQv70Jeu1rSPnlN/WPNUwAeV0DGfTf9B2L+Prn2terbu97j/zb" +
                "2rf+m+865yP/Ffa0p+zG5nqxLebLAQACENAbXvYH14E3wuMTb2iFsGAunfPvyRZhD+8BR0YBQC/BCq30" +
                "k795oTmD6fYu+8U3CGuvagZbdPX6Qff7YNi/uiVLhom+fA4/LuMeB6HYqoW2NpxpxJw0D+PUqjhFUCOK" +
                "EXTCsVnllZjKYfZIxU3dlHhobEyAlqJYnFv1swF3VoVppJI4vbMNq1OLyM2bf0cfJegyHEVcEAJK+IuL" +
                "s4twoxBGEPhWSR4jDqqL/qUKM0TqpZ7E0xhhd64zvYX6mmABV9mClweRmQa1zdp5Hk7mwDIxSRJb4tOM" +
                "KTpbdRD6b4jC1iAqExcKEIUMDht+/blf3ufViPZ+NRKV+xQ4zLTvZRLOIN0onoQu6WlgzYAaarATnSIH" +
                "QAYGYRRguc6WGXJApELIU0XxdKrWcT5HMCE55AWFhpHweq9TUquxebJR8WJpsjxEmqFMNYdewNCMuSlY" +
                "HZuIktyBpweAKXRNeSjRYbYLmGL+FFSJabVaE5PpVquSVMca+2m1WkbCK5IbE59XTO6wMTYmAbEBMfel" +
                "rH+3AVYkFRYuwOY3N0lkFcgOSQJIdZMshkJcKSFFRgiskrhNhsjOvpNB5ZCPOEBTNfZcli8WaQhQPquD" +
                "TN+bZEXvqZaILQeIQ6Im0lMEDMh50wIC9actN/NljMcSRoRgcXhUgt7rxEzifPMY9IVl4Bf2kN2zXKKn" +
                "0FUu3JM8lsuEfCxOqwh6C1rdO2wyY52SF6xYpTGkQNYWoYQTDx3jCQ6VhqhPRBBzHUZkqM6Jydu1VD5U" +
                "+81jYCz3Y6lZlIQwokzDvCIdNVUbNUYdBthBqYHzeDVy8BA38kuLYhDk8J7CRj0A2tiCZrFsH3zCLAs3" +
                "9oh34GqPWF9WasoKMaR25nVmQjJsomIR3mlZ5ODBO1mYWZJGw6Sp/jbXqIubs6bamFXmQyhzkRogdPoJ" +
                "rYVmsUvkXUkvjmiJmoSpgqcS16U6mW6lF8t846yRpCfciG4rvNu5WSUIqx4Hy8nGvyLWg2UIUvBUvIag" +
                "TAqdr7EL2CxsoCCzIhyygoJoCDqDXnLK16zBZqPReCfGITbSaNg8Q9RARGX7cVUvldPOESqvvMFXXokE" +
                "vlhAySOJJkIz2EBESaMwiyDOPOTIwcE2niGaHicaFBKniyU0J3FlsySuS2HOELupgN6olZXcgw5gAZEi" +
                "U4i9bq0Xkw/ZCuPJKkGMnhjYeZwS+DSDzAg7Cdj1Nqp70WID504FBME700mmQ0vBuXuhGiupSrCgsTda" +
                "m2NKQDNKTn7zIj7oB+QlS3SGlmLUn4S5JnBTtMUusO4DfhfgEeEGm4AEvTRwggNQfrPJ5y6l3odZHI7h" +
                "ZkA8gQSAdZ8W7R9WMBPZLZhCajx6wVju8XvQpgVe4um4yIV2NYMAAYim7h4RjGMX2ynCIIw3icdZmG0a" +
                "HKp4y8beJclYXIg1QmFz2z2dCYs2gjh6jvT2uAgCswNN6gIjUtEhCElGIhN1oYY9swh4kZ5lGrEXkFP8" +
                "EhlEGeCZIr+ZtS8ewN1qkq8QnQFW7idhtSt5xNrVgqyZ7Cb02YLs1pXpHAfskhUKk4dfZGFqqfiUNTOd" +
                "l9kIaMPEuN2LmlpN5qhZm+qS23CoJkHECrkLg05d6kL9hG1vBxeXnGFPqZw8eEDoxP/hmgyC8iFsx2r5" +
                "SPGUYl7F0KvUiSDxVxYDi6yl/LL1HVgFwmOD4aJLJvMYhxPu8rdo+H9Sfd6kus4QF+e/O6l68P+lpPpU" +
                "TpV2iJbbxkyjhcizjQSQkTdhQBXm/AhojSqJAOjv2re/sZjwUeT1pYLeE1R7SWY+5LkaswgrY52vNewi" +
                "X5tHGZP1RwEPFWo4QSRrvOeB3amsT8Srf1hhQZZSAMiMhNTnYdIRs4PFECUQfavRr4pAzBa10NQEwqaK" +
                "ldxUks2AhyY5WMY9G7r7XEUG8kATyFEMrkZZhmtqCscwx6pM6DWWHJCzHVFTmwoUpQquVbi6QazO4lkc" +
                "1cMoB37H3JHKpycwabgU0yybQYVA4qV92FTdKTvomhhi5/bNGrVpji5O/hinHlFF5VBsC/SGncj7KgYf" +
                "OfwEWvcj1ofit6K0VL8+i6pLG9ulbaRwDJF9Ot/SOT39UhooCfmzDPnf1s/kqxw0HFs+wdqya93mZ5yZ" +
                "O5gTFEUmZmkYQwMJSrlhOuPCl5IGgp33VQdSPju45+FOwt8OrUEVop6SuSM4FYjn1EMMUlH/+1hkZOWj" +
                "zCCeY3j4xODLpefaW/HjcWV0xLkKvVH84Ock5LScMmnK5itm+p2zE2ZmLMdiMsmDKFSdKAPsPEQPxWJC" +
                "T4jfqJyl749ok7hQnesRGG+4h0gCfOxHpC7ilVGmxk0EmwhYNJzTOQo9Cktumre3C5+f7UkHUbBRCEI2" +
                "iBp1QQlSfyrkJks8PS0Pnor9/KSS5qGgyKwdDWbFhoeNMUEKUcur44IwIYNK8gQtX7SRUQeKAqHULSgL" +
                "BEYWyPyWsshUTVC5QgWOKp7KoMamA7B6IwptcFsg+qDsI0xy5lkQjoIVoHZJA0FNQ+zo2yWTTRL0+ZQ+" +
                "MpyQkSM4LIdHfhzGe6R6QuE72/BumUa5Rsuow6NCSjYm9QGnHxoXB3HOZbl3KY/sIAyiLnBbeKWsNZr5" +
                "ojitKQMZc6ruUrMuz+cE/jl88rEvtl3Fx6lPTiiLabJv39hn6gWisAqLd2w6AR6w9TAuaE8OAg+9s1Jj" +
                "K+u8njHbEKOgrDwOkX2Nk07hPfJ3QMO5WcrzHOFFWBgRBkLjMZdjehdtaXi4o3D3PivLYrZFgoSl1Afn" +
                "XI5XThO8AIYmAf9+mwnNZhcg8B7C4ogjdDLUjf9EY6kSrF5k2K3vZFdyAHyt7XwbK70B7EI+7MRD30oU" +
                "b8g5SAnU7tIAH8MeVxXYkrsjNXZHOAzm21EpIsD8SjwNGosi1gWiK21xWKXthpa64+wnmaRvJXXtiNr6" +
                "imGI1IsDUj6w4GKuAgQbxfnxyqLs0w+oFIh81JaSTDngNBvjDUr29sXF2de0zYCD6tZO08zQBAH9VXof" +
                "4zrBgopdGkwjQmwgJbThmBSJK/B5Uw5ntjWbiKND2WnQue6/7+AMm3haLilOUc3qrdmNN1xgZaJdJ/g5" +
                "Xn2NLYs8n9BCyeTNTad3cXbignC55+7teJcjRMW1s3ynai72DwjC6813rIsVDqUBkehpLt0oTUMQzaxJ" +
                "SFYQrY8YZTTFkQgkGQmJLJtTIrC/xAjTl/TAiUcqQD2g8Z+/VFD8fFBp7P3Lf5QcMtOE9F9f7P6QcM5/" +
                "+3CVgyYfO0w54blAhjBGMynqVlEV8ESOKkaoUGc0BpnJeV0xJZCzI9gJHc9tFRV3ujgQqu7Q4jeyvjy8" +
                "4U6SzQURK1XR2Ad7YPEIo3GVFJdgeUz23bDfe0EXWNzs7EP7+koJAhzgFCaMMFs4QKXFpEDtpVLOByWp" +
                "+4TSVB2uGujY55HS2Y94dkN3aZL4TrfUH/6+TxLeb+2fU2Vz8Wb/SO1nxuR4M8/zZevFC7QfYQJp5/v/" +
                "+IOwiKwBY0clyIO71EVG0Z6rbkg5FSlQ5Rjn+1gUo9iHF9xp7cbmuOj0EI/jBC2OS0+77JVOUUWIvmW+" +
                "eCO2wUiIK/J7t7OMvMi4VpAThTgZgPJQngaijlk+R2Q0LVUIgN+RCPCuLoLWN3/59pVAUOqVCQHgHlO8" +
                "73Ya/nCFc1OUCHR4Wuhpa+PhL8k7DyG4eSu1v57Z09fyho6qW+qbV6cn/AjojABQPpu1g0DaX2NuU3tN" +
                "FQox4jfwp+7ydYG7Wgl956lAbpb73qBh2l9qLP9UtQCKLsRNxwbzX7skSztSkw1Kay7aYG5aucGi73Jg" +
                "Fv5cGGblB4qocMa+BAAyCviU0tkTpXD++gj/YQRAhzvfqjf9H5HG5PfhzbsOLsGcuMfzD7hpc9EZIJS7" +
                "F/1e5+xVcRnOxSfOMkSTg5IqzYcEnI+grXCXQUrQ8lyuhPBraCpF5FcXVMBaMuOldoXvMIgQJFWTuB58" +
                "pNov1+xLcuMLF64nBONMqnQPPx6pDzK2/6lKMwmZGyadzlAsOorqMYjapoI/CL1Zyjb4ERVJ+fShkDU9" +
                "/URZvEKSyN9RxcMuUjuFTfztTtURPYVORDQKxcI3TvLjFZHg2hyxIE+H4A0G7Yvu7ZAqpMqeXsmMkxQs" +
                "B5EiFTEdHj/wzNCXh3zo4rb6SYUoOJqqHBZu4Q3edbpv343UAeF2D4clT3JnpCLxkqf5VnvlfUEdkC8c" +
                "yn4U5/w+wp3bRx4q+zy1Cw0RvexEfa452b0nUrYMA/wnut1X1Pl1n6QDnjjjFphnqThjXJY2xDKl9dRs" +
                "kr2vlkfuOOsrJ1TvpDVhFiZVY57q0dJTHwGXgiHALxPiHjcB3Hxmj84bqRitz75YW1QeyHe53ULSrgw3" +
                "McKWIW1x6l+ZvlfgnotBkOJHe1sjqertHMxC/Dlnye5nRrBfPgVRa+kTT4VUaicpQKC7FufDoXk6QwXx" +
                "10qExRVf3F2ly9p0G8BUrvyBRzrRRLFjP35q0B4jh4CPjxwu2qAyuPMrfO+Fym9FRRJf35k/6iwhS77j" +
                "IYueSVSejR0i82yhyiuIkqsTH0+FTv0Q8BzwWajlvnznIb8cgsPTpL8v+/9iSBA+qK9o/PeVmvyKH5E6" +
                "U9xRh6p1BgPX049ff6KJYvH4kh4nxeMJPUbF4+mn4qjh46tP/O5LCeAzM7zaXGvnuWdtiTc0dl77X6Lb" +
                "Rxi+XFfCuohS3pvD7I/avsIRPx758xN8xUM4mWAUKt22/URaMtvQcj/qUzEzr+wlqUz+RQPNIVwdWoxF" +
                "XDSg7OenDjQdpEt2OLSwteNpjhE1LptgvbHjSpd9fKeLLpqWL7fYenzbK1r58QNyf0AzIP8POp7pVnXF" +
                "AD83ZBZLzJ9esHWTp7Kwfqe5guKZbPYJyrbveLrqla8os+m4C1W1I3d3d4b/JRAKSrrSxZesdtil1+6O" +
                "ofWuksPd3zyGpPxJU4nrQK5w+tur9Zs9h94FBaK8dVRBwUc6uIOWrCLiwt0LqdQy9kVpwy+2LXfP3TN2" +
                "3sL+45oO96rAVvEk6QS2FonTeTAaiVPfWPdARobycmcYfEKb/51w+FvEeJ3U1UozEW9i1QtkB1RUGPWa" +
                "/u3A4e+75dLwYx83NqV7Fe5Uuox/3jRRfs5o/lS9rfTEPZlKMHu0BSynYhv/3j7bVvYb8fCfKXNu/Yg3" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

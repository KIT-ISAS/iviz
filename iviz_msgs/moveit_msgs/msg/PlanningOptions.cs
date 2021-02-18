/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlanningOptions")]
    public sealed class PlanningOptions : IDeserializable<PlanningOptions>, IMessage
    {
        // The diff to consider for the planning scene (optional)
        [DataMember (Name = "planning_scene_diff")] public PlanningScene PlanningSceneDiff { get; set; }
        // If this flag is set to true, the action
        // returns an executable plan in the response but does not attempt execution  
        [DataMember (Name = "plan_only")] public bool PlanOnly { get; set; }
        // If this flag is set to true, the action of planning &
        // executing is allowed to look around  (move sensors) if
        // it seems that not enough information is available about
        // the environment
        [DataMember (Name = "look_around")] public bool LookAround { get; set; }
        // If this value is positive, the action of planning & executing
        // is allowed to look around for a maximum number of attempts;
        // If the value is left as 0, the default value is used, as set
        // with dynamic_reconfigure
        [DataMember (Name = "look_around_attempts")] public int LookAroundAttempts { get; set; }
        // If set and if look_around is true, this value is used as
        // the maximum cost allowed for a path to be considered executable.
        // If the cost of a path is higher than this value, more sensing or 
        // a new plan needed. If left as 0.0 but look_around is true, then 
        // the default value set via dynamic_reconfigure is used
        [DataMember (Name = "max_safe_execution_cost")] public double MaxSafeExecutionCost { get; set; }
        // If the plan becomes invalidated during execution, it is possible to have
        // that plan recomputed and execution restarted. This flag enables this
        // functionality 
        [DataMember (Name = "replan")] public bool Replan { get; set; }
        // The maximum number of replanning attempts 
        [DataMember (Name = "replan_attempts")] public int ReplanAttempts { get; set; }
        // The amount of time to wait in between replanning attempts (in seconds)
        [DataMember (Name = "replan_delay")] public double ReplanDelay { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlanningOptions()
        {
            PlanningSceneDiff = new PlanningScene();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlanningOptions(PlanningScene PlanningSceneDiff, bool PlanOnly, bool LookAround, int LookAroundAttempts, double MaxSafeExecutionCost, bool Replan, int ReplanAttempts, double ReplanDelay)
        {
            this.PlanningSceneDiff = PlanningSceneDiff;
            this.PlanOnly = PlanOnly;
            this.LookAround = LookAround;
            this.LookAroundAttempts = LookAroundAttempts;
            this.MaxSafeExecutionCost = MaxSafeExecutionCost;
            this.Replan = Replan;
            this.ReplanAttempts = ReplanAttempts;
            this.ReplanDelay = ReplanDelay;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PlanningOptions(ref Buffer b)
        {
            PlanningSceneDiff = new PlanningScene(ref b);
            PlanOnly = b.Deserialize<bool>();
            LookAround = b.Deserialize<bool>();
            LookAroundAttempts = b.Deserialize<int>();
            MaxSafeExecutionCost = b.Deserialize<double>();
            Replan = b.Deserialize<bool>();
            ReplanAttempts = b.Deserialize<int>();
            ReplanDelay = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlanningOptions(ref b);
        }
        
        PlanningOptions IDeserializable<PlanningOptions>.RosDeserialize(ref Buffer b)
        {
            return new PlanningOptions(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            PlanningSceneDiff.RosSerialize(ref b);
            b.Serialize(PlanOnly);
            b.Serialize(LookAround);
            b.Serialize(LookAroundAttempts);
            b.Serialize(MaxSafeExecutionCost);
            b.Serialize(Replan);
            b.Serialize(ReplanAttempts);
            b.Serialize(ReplanDelay);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (PlanningSceneDiff is null) throw new System.NullReferenceException(nameof(PlanningSceneDiff));
            PlanningSceneDiff.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 27;
                size += PlanningSceneDiff.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlanningOptions";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3934e50ede2ecea03e532aade900ab50";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+1be48bN5L/X8B8ByIGbmY2iuzY2UVudmcB2zNJHMSP9cxuHoYhUN2UxEx3U2l2j0Y5" +
                "3He/+lWR7G5JEyeHsw4HnBPY6m6yivV+kDwaPVDXS6NyO5+rxqnMVd7mplZzV6uGPqwKXVW2Wiifmcqo" +
                "E7dqrKt0cXo0ehM+XfGXOHDKA6cAeDQ6IvAvCPDSejUv9ELRv940wNTUrRkzCp0BJIbWpmnryitdKXNn" +
                "srbRs0IgK1vx2Nr4FS3RqFnbqNwZryrXKN00plw1YRIBU+poNHOu4LlTVxWbP7QW5eYd4f+GiQFyxbN0" +
                "Ubi1yTGzcO5G6dq1Va7USeluDcGsvKv9qbJzzLQNvTGlJ/C64dWayrWLJVFEPC414wPQW20LplfPXNtg" +
                "KhZkqltbu6o0VRNIAsqpoBwSdauL1gDUynnb2NvfIKmjh5d4L0nQAq1KfWfLtlRVW85INQhSYLj/a8Jv" +
                "OvSFmZNIvHok+HMz123RdN9bb/IxBhD3MX9tm6XKN5UubTatDang3C7a2hyNbNU8edwneBoRJ8ohQU0r" +
                "tfP+OKCJQu1zBqgJc2RuJCxzvkksEJpXmhZF3JiZZBP0rdPKSY9yng6uyCzCs7SLpYEB6aq3grEqXS0K" +
                "AikQHgDRqjJrUfLKmNzkE8BNTJw8Yl2/hzhTqUjMkM/gy63V+/gaGXE0mhdON3/5AnyYej0302RAU9DU" +
                "U69ghTOCUpLR2Yqw2Fw3xJO8rUFNmjqGyosWegt9Ji4u9a2RdZINMCSsp1y1AADxdZZLFt7ougEbrpOp" +
                "mgo898xLwJm3VSZ+yDabaOu1AWRZ9HVPup3aygg2gahIKmqZfNtSMIDRJTGd5dvYkqlZa1AIbjRrY6q9" +
                "YE/ouwfPc3/aMTogyU2hySONzv+H/4xeXn19puCFbDMt/cI/HPhoIoiUwQxcAXvro5FvWIj4LJTP24I4" +
                "6mbkr0geDb19i4cr/Jb30/A+8imCZjfNE0tHhIr+S/SAOF2dsAkYHjVNmHWeWxGtmtcaygaDzNtVYTPN" +
                "HriZqxP2GQgGJhMP3g9VPI+4vjCkrE29EVZc17rycLlEQ7ky+bv3am7vTD7l4dMmfhbRM/3RJWSuKKyH" +
                "dpK/ru3d0eipfHkeP7zk93HCNE2Yxgls6QSysNUNuQkisloQpu/o8Y080XrwcRo+bk3xmSYDkAlX+B2H" +
                "8wcZ/LRpdLak9brZz8QXP+4tPL0yt0ZCqcsaV+oV3FCfvpXKyDxhsOxZ6BMFM/JE3mWW7V24XQbz1HWt" +
                "NwpysHPLFkqfjkavGd1zTKaFCvKpwJK1it/sod3KJ753dZGrNf6WCV9xyK7yqAXrpWnEx/bVSzw2WbSp" +
                "V5RNsLcnD8vpzbbOeAeFZSDBCtiNWB+SlwOYZ2dTbEREACm81wsOOo22lR+kCZwZ9AyMLZD87cRMxAI4" +
                "8FO4gh1a8kI/O2KFZw8LZSHeS3Yi2L/FV7FoHti36G9lJjvskiTMClFSfLErcukXr78iyZsk91wRF80Q" +
                "+ksMpoE9LDx/mrv5dAdf0t0dlVUnOn6LQmOLcFXHCTL2CCCZpKggaV+c3rPKAPpwmhWkCwFTYC02ypYr" +
                "VzeaAgvcG+UJecG+DQEnkjtzOUzqJC6IBiJ/RJpeGF3vGwxMnImIlp2dZZRxnJ31fPfMzJGEtKtcqKVQ" +
                "xstvetp3eiBT2K+MPWbpZA+siEtX5F7RwjWYkBuf1XZmmA+sSkI6Uh/EIVf/0ooh1SR4YpFYw0SNHoSQ" +
                "lSYZ4qF8Vie1uXVFi/e1WtXWw/ayU6yGMixbgdWbMwKg/jSwuRj6IhSdA0B5Ou6G3pqCnCglLDtDH3oe" +
                "/JCqBthqN8XMSVqNUA9+rCgM0gpCNRQAvCox+9XphAm77GhBoldZ4gIULqcSQmx1tmHngJAbGLE0GkXf" +
                "wI1LukN410ubLXv4mGtercniOY2rOWeliLgzBrUFMYYMKIqR3YiYUpwKFCxB5A/AOQnyGXpDb33jg25H" +
                "N8TBh2IaMMCMWIwryh6HHObFQOxM68LpIhh0qW+MTArjiXZoWCxyJ+p7ZNhmspiojWvr6E+ZisqhLhT5" +
                "9MJjMCZTjjGFgykZ663pi1OCJhLFTdBGcE+oEdn2aPdL11IoXEYYzCdvfyXHTyQTIwVOz2o4ulPNS4nq" +
                "BmQmHUjL7DGH07a4aGJ0TXJpCJhIcDIajb4R5RAdGYXkjfwq60/IbOkxGkLvVVT43ivhwEdzKE0u3kTW" +
                "TGSQR6lyXefEzkaz52B/y/XZZwUlQwUoRT4Y/MpmBao7Zi7If9eUhm2keOQeSVkSSzPmI+nrYL6ovGYt" +
                "tFlbaGRXpOe2wnBONQEdDDYklCoz6sXFGSs4V0C3bKtVVhvNReKLCzVqpUKhCaMH12v3GYLQAgEqIk/+" +
                "wdxRbPJS5MJH/UmImxDss1iOqBN+N6VHcjeEhJZgVo6MADXLm02zDJH1VteWGxIEmJJMeNBjTDo+7UGu" +
                "GHSlKxfBC8QOx+8BWyW4oOmzFA59uyAGopis3a3NxXexnpIbJOUt7KzW9WbEropRjh58VbMjgfhYItZv" +
                "m2esPyTxt/khwttuNkTEvjUQFxGiYwSRiAQVDa6GLTM5vNwsamPYDc7pR+7Iy6BYc6g7Yv5A1LVZg0Kf" +
                "hnX4xK2+aAJD2hLaDL3RMVpAb/3GUwUrfgCttpwdJdlFLI9kzsI0XTTiOsUF7DcUIjlkqmxJCexEfQXH" +
                "fEeiKchjaa7SdB1Dl2Z/98+3F19xhH2CxPLkjlwn/a/XUAjEQ9Idb+Rjxl2IgaL3VyeMlKJrHOYivgy+" +
                "E1QZEaGR4t4a9BzUTGc3IHiwhv8PqocNquua/OLydwfVOPz/UlC9L6ZKXYTp/r72BY3qehXbg9YkUAzA" +
                "v1vfvmc20Ufh18dyevesOnKyji4vmENyK7GZ1qzdTsRk+cHhkTHpjHR59C/ip6ufyPxCrPofLU2oK24i" +
                "OnGphyEyLGYPiZpSIHzbWr9Kjpg1qjSoA0mn0kwuLKEzRAMaLeQVULVxazXuerAXu5F2Mps/3PEmOkPh" +
                "CV7TlBMY2xiFbSWjuFWJVXB2Q766tgubb7tRdvyBuLFq5o9JpcmkeM2CjESIbZvA7VPuXMNA1yCIjTsW" +
                "azOT1qWlKezGyKgCiCFD37ARRVu1FYUknZPUYxv1Lv1KqaX69SCi7nRsn7TJLdc2hfOBzPH0S6egYPIH" +
                "CYq/1geyVXYagawYYH1XtQ7pmdXuxoBIVjGPngx6Egi5ulpw4ougQc4u2moY0j2HcYehTtzfHqmRKEQ8" +
                "HXFjMipaPIceEMjbeL+LRAbWPUoP4hCdxHvaXyE8b70VO571ukccq7Q0xEMM4z6vZapvUsueW86pV8+s" +
                "TJ3KsNnYUibgl5rKKOYUlYWhh9/sLoRgiG/ot/gwTpA+IHcSti9ZZry/CqCVC93Bic3DPu6Ye+Wpsfdg" +
                "H8TY5ZN0LdGS2CEY8qPRNr9cWG3c6wgtJu6phgyz34uLnUu0SHs7q41rsyVAEPK4W/dZWpwshfdiqfjL" +
                "N/19x27CUZcrMLhp6OuGnbqM0lgSRlgZZ5OUcGfSMhpUpSQXrhFEMghFQiqHoRIwEjmAHUIIduaxXTef" +
                "S1zLCsdlatiZ1LFrezqOzTFGUpkMzrzeMLraFFLdxo2AgBqCJKCpm0wB42dGs+m1CK/TS2IJFjgNSDrp" +
                "rA2V9yld3ZLKGFvFN5VbV922nEw4SMd/1z6fhixwLA2GOWcLocscSzo2oqNh1tjRSzYQaA2MPGFVYnAk" +
                "x5eE/gVa5MGGbd5NjULfrIxoCOL1THuuHZlLnUmFXRyUGIuKWz1CklByDRCA0wHvWvnBFaMI2pPVR1sO" +
                "82wdnAbMZ99GX3/XoePElSuIERFTht5tyccgsOsBfySr5WFv4jf0rXrjttMQPxgwDWIAtpfGL7cg4xUN" +
                "L8OXvbDwsQ/mGcwGQkFZjF6/QZIQHF6ic8xHAOIep4llqyQbxIZWbJDk122eAsnpYH3YX2N6GNV9xOJj" +
                "f4VP89z3dWt7s5U3OCbxlEAYRLp7a13rKUU0d5RVgAQ5FcA74OSPSNizDeX3Ty8uzh8JprfsfQfI5rUr" +
                "t4/BKJTcNSqvE0NV+4acF1sJb1Q1ZOl+S0lsfhqQvb18+fpfl+efB8pWK/gyZLlVoo4bIsEB89J9bLb/" +
                "NsXpLBJPitSSPHqkvnlz+eri/HFy1h3a/RgZ0ViOh7BBBLlzhXCCEVGEscwtW99gBJ8e4RL2lA/4zJV3" +
                "BVhGHI4+pXO6ufHE0Dwsk1n0RBb5emXqVAsQXHpE5prGuvj9o/nODzud0YM//Ee9fvbt5fNrNFf/+OTw" +
                "Rxj0/Le3admvcntlzvExODrycmhoodT1RhowSDdJlKZGD2Uh+32pxSAbT0rzIbntXOTGpA2lPpIzfiMg" +
                "uj5VHXVrYXEOIJ+lqEBgOpj5rL+gEJS50/bt1etXD3F0J7Tffnz68jslICbqaVJo8sTJInpVKrx55E3X" +
                "YgyZQAw9E3XJuYat9kifLYv7PzimVtgbc6Y++Y9jMPr47Pg5UqKLZ8djdVw719CbZdOszh4+pBJGF8T0" +
                "5vg/PwlEykGoykn3r4pnTliKISuCkHp84HNmzTFNshlX3DfGhN77vCDTnVkcR5oMY+tAdbEbK3yMhffF" +
                "M1EShpLxwSgdG57SOGM1a4lXcH3SR/VnvG9NawwE87NiSGcqcUFeghH0cpsRZ3/+9y+/CEMQqKXZQAN3" +
                "l30csV394ztF8vMGG7FJXkPkV78U38QhATyjU8frhX/yl/AKm99n6s9fPHkszzShxhCLbDmOoVRh7ep8" +
                "+z2SGxAUscTd/PC5dHlbYAA3Ghq3Ok46DnX/WL3++zIMWtOFmO/M3VFhuYLmjVW2oRSds74MjdbQrYx1" +
                "U23SZnM4PQeTocRoFvMFAoaAgPjPtin596Mx/TcZ8Y7Rl+rZ6x/OPw+/r958c/n28vxxeHz+43cvXl1c" +
                "vj1/El+8fnV5/sUoqG70WxyFsKYwCu9HcVBuS5xkDMdNuqHdZl83Is5Bq4vPPvYm9IadSeMYlQ+fjRAm" +
                "SEAHu+6i+zru5hxL8BsFHcVXIpyXKkXID2P1o+wF/NRfM5jMtZepFk1qVg+8Ejq25DoTfcT0Scfb6Q/n" +
                "j3pPPyZe4+knYnV/ScL/sCruoEHscKT0b9hVkHOx7GTYPwvdtc5tiyWEYkk0aDKQ6/Tt04sX/7yi9fRx" +
                "RiEzTAhYdjeFK6I63NPgRmTMJXknJ6D6SWlKSCaq60AO4E6/uXzx9TfX6gSww8NpR5McRelxvKNpOajQ" +
                "oi2oE9jCqeCD14t4hLqARx56eO7Dgs5k5J2IL9Q1+3E+d5U0F+Inmt/VBts2iV0jW3MpPRGTsatOh5in" +
                "mI+KFfrersZhj+zTwNTRliUG/iWV2iKelKtnqTuDO8Zg4Mdxcbv1Alev9c4mJpLV7YYaSwsJg3yXIzPg" +
                "dq9jOlEj6fymowS9ln5v3KEItFVqhw6aXP0jP1pkPCT3A33djx+CUIrGwNNbKqpPOAiqysX4aqurBeUT" +
                "f+15WD4/jkJtjiMGrneokGjENiklP/7d+xFwXAcAvCcVYI2C8witwDgjVmg3OHvGA3g1e3jOB0dk0oFY" +
                "FcnYw7JI1rHvFiXnMd49kXWauym3FQ+yWq7h954ckJ11Mw7NgK5ZkDoK+k59ik7ipyr7lf7K1bl6BGFp" +
                "dXZOCm7m7x69R3MyPX6Oxyw9PsZjnh6fvE/7F+++eM/vPhYDPtAI3NpN3buZujUlKhob70cT3AfWHT0M" +
                "Hy7oxgaP0p0bMJbLwWSI78ZxU4a+0oPOMlOEQty/h5TccLQcunqfuvA9XBLK5PqFyScxD03Nk+ANEP1i" +
                "V4LP+ddguPZbe97sI7aonBDpoz3nxPzuQTEip/dyQNbuEbK8ja0Jiv1TdIqmfHPkwBvb4TbB7r5Z/8wM" +
                "97G2d7WDvk7SMaQHsbm2MzJb2iJeVMDA7aMc3blibvqGQ1I06m9aLcmTn38Syr+1vbGT2vmJqxcPm/kn" +
                "f2/mf3uo/061VnZDgHhn+orCGLa7cpe1ZXIx0B2+vtTrdOwcZAh11nC5SuKBUNL1qnmQvB1dd6eL0oGB" +
                "Q2ya7b3FEUqVGCSJA2QnSbs5coVLHcnT8BjxNL22PEWFW5sjCOK77ebff6+ECg5y1ygQ/aaURvZYeuBD" +
                "sxsg3CbjEh/TqsSCQ9OxaxF3ujZcA0RvijlhxTq9666CYHTcthKmyFWzdEio49RkcHVsl9x4//KX1tS2" +
                "d+fO8jUt4fVJ9flYVY9P070xK+0kaXIFTt67ejlpiOW5+e59k4i/t8UWzigAUVBQIYeWwQfvZSG9rnIV" +
                "du6440oEckd45khUNIM3KR+z2xysl4eHQ/qBl6EeI68BxvKiwsU42ZSt1NNXF/1Oa0/vAojpQB1wdH/n" +
                "W9SC/wWrYnXEsa7hNYZOGtnSZDfhbq3c9MsTFen5AAvvXcYaPeAttBgwf2MPPF7m6k7gDLawY9afbnUd" +
                "iA6+I/YHqMA1sg9TES6bHYCG3gWy4ValFW8h5yfXcZ9nIxfV9u5xRjcQfYAMy9N+rKvffv3safzysU+m" +
                "J4TCVNw8Tb8W6dcs/dIHvybKV+9GD3ZvYu2ci3j3Xt1zp4oFFi4Ysk/tH5PqNik7FGgmH43ClKAC8vA9" +
                "eUE0GyK8j7fV9BvIudPx5ILvR+LmLyVncpiBwg7fZafxVP5vn+sURnA/JO7Q87i929rhvLDkSXt2Z9EC" +
                "WdgqQg31dQCIJHsfAZGqQzLtv80svonHN7soQBCFceoJN0zRX3nosqxdUfw9RRjhS7r8RlfZJrLiZDJr" +
                "Hk6QKdjCnMpNNgEEHM8L7X0/DZXODjpTMr1zISMutD1ug0FFcY+hPE2t6xJ3F5AiyrTK5V31AtGGaQAS" +
                "yPCU7JCX/TXV7DJVzkHKGXRuYU6WcgpHy934dW2bqDget9W/RHQnexmN/guc0LO0v0MAAA==";
                
    }
}

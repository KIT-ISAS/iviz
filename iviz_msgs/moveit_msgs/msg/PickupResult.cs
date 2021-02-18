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
                "H4sIAAAAAAAAE+07/W/bRrK/C/D/sEiAZ7uVlcROc63v+QfFVhK1tuTaSto0CIgVuZJ4prgql7KsPrz/" +
                "/eZjd7mk5CbFnX14wEuLRCJ353tmZ2ZHrdaFvlX9slcUujjViTJC4ccohs+tVutKj3V5XcpSibKQ/1Bx" +
                "qYt1ZEpZlO7tyD//9LmxaKpMq2XKIs2n9XeAJy7SRZnqHFa03hbSLMQU/4Zvk0zL8tVLschknsPWqEzn" +
                "qnXyb/7Turh+eyzmwH1aRnMzNc+akmileXl0KG5lttPaaT0V8LqQWSbGaiZvU13s2AXX709Pe9fXJy/c" +
                "gzfd/vn7q97JD/gH9/Ljy/PuYNAfvI3wfe/s5MBv6A8+dM/7Z9HFcNQfDiJceHJw6N4GTyO7sjvqnUWv" +
                "P0a9wYf+1XBw0RuMotN33cHb3snBkdt3OhyMrobnHt1L9+L9oPv6vBeNhlH35/f9q1503RtcD68iANs9" +
                "OfjOLRv1LwDL8P3o5OCV5+Gq17u4HCG4v7FUnJbEf4mbNFdzWaaxEYX6falMycZkvKRG3atRBH+PesBJ" +
                "dDo8P+9fA28giufb1nzoD8/h3+vosjt6B8sH16Orbn8wuoYNLyrBvh12z5vwDmsv/wzQUW1l8M7tQk29" +
                "rLA5Zb29Gr6/jAbdC5D5i+823jaAwZpXzTVXw9dDyyq8/lvz9Xl/8JOD/33z5fD1j73TkXv9A+vCrE2p" +
                "5g2hv7mCNRGQMbh+M7y6iJx1Hhy+qCzFCg6MqHf6E9oo2MgHWIiGAiu9NAOS8W966QVozag/eDP0L18y" +
                "ZYFp1KkbDKP+T9H18Pz9iBR3BEQ9gq9XcQ2IG81SI+bKGAhYItZ5KdPciDSf6AJp1rmQY70sRTlTosCN" +
                "wuDOtkg7qkNPF9qkFMyEnoi0NOIfGtgzQuaJyNL8Btg1KjcQVwn7j/iWoyotjAgeS+pH3lnOZCnmci0g" +
                "1igxX2ZlusiUOBu+EbJQwixUnE5SlYiZKlQd+gUuhoUBFtofJXoSbeDrlqWMZwAo1lmWGuRWjzFOG7En" +
                "3btSC6PningRsMJLYn+n5QCcuv1D2g4B322PPOjIgmbUbzI5BTEnaQxShgiymikAXAB00IeJVa4EfADc" +
                "Y/iQl6pYFKoEaiQIViTpZCJWaTmDWIPSKD2RmoDQ/h2nXVSwNmW2Ful8oYtS5qUA9YJ08yRD3MiRZ3es" +
                "kxROwj1HECzMNSGIMyWLbYvpgJgAXWxlx8exLtTxcXB+jhUgVGK5SJjbtGTyy8D6QJxjrTMgN0L+HswV" +
                "thtjICzp/YEMcaazxAggXKIQ+PQeK5IDmRKzblSJH+CEh+BPjlSA4kFE7A0d0XpKKIJNCmTIr8VeoW51" +
                "tsTnhVgUqaF4sY/UJGoC8QNEvT4GAOKbms8RypnyUGSCAOb77Wrprcp0nJbrzaXPDC1+ZvbJV6stagLa" +
                "Kpl7lMdikaG3pXkIYDDH3YP9DjHWq3iBHcs8BSmgwSUqL9lXx2sKDrmcKyuImZIJGqt1Z0PQMd8B+wJ/" +
                "SONZgI+kZsQKPB6MHgwsUUlHdCElaa4B6ECpBgdyaqQwwq7ktiIK0iCQQzg7Vj/1aGhSUxpr2y4MyaKQ" +
                "a9MmDOhGpMYFJIV1CRMxqHbidaplZh16Lm8Ub7LrgXe0ME0pocw64peZyoXqTDtirZeFi6fERa4BoNWP" +
                "NAY0KzEqWGdS8zZuEbHMBTjrrQrVSXQLNV+Ua2uNKD3mhnUb8G5mepklVnJOTib9AwI/sAyCZDiB1+Aq" +
                "nYPOV4AF2PQ24MkMhINW4IkGQReglxIPcdJgBxLhd2wcbCNBIk32Y7Nk+OocIXjkDD54xBJ4sIBSJhxN" +
                "mGZgAyJKnsgiAXGWkiIHxdt0CgH1IFNAIXI6X4DmOK6sF8h1JcwpxG/Mt9diafgIivV8DiKNSY5gr7X9" +
                "bPKSrDCNlxmE6ViDnac5Lp8UkuwblxnMTfNYif7ZMRm4ipdleku+mseFkgbDc/9MtJaco8CG1tPRSh/g" +
                "ITTFA8oh9/FB3cHZZAwdThijvmHmOgAboy1gAeveo2cRfIVwA0iABLXQ4AR7QPnlupzZk/VWFqkcZ3T6" +
                "xZIi6C5u2t0PIOcEOpe5duAZYoXja8DmHi7ydOCPQ7OcggBh4aLQt2nCsYvsFMIgGG+WjgtZrFsUqghl" +
                "6+mbggIJqo80gmGz7p7WhFkbUZo8xvG2mQ0Bs1cK1QWMSHeC8ImEJmpDDXmmD3iJmhZKURicwIdEQ5QB" +
                "OBM43/TK5Q/A3TIulwWdbBU+Dqv90gpkOUdrRruR7rRAu7W5O8UBsyCFgslLrJpzg5ko75mqsjqNAKzM" +
                "tMXuU2wRzyCB7Yg3GJjvQDUZRCxJhZos3NElKd69vzp7QyfsESaWe3cQOuF/uUKDwPMQbMcofonxFGNe" +
                "YOghdSxI+KdIAQrvxfOl9h6g8goHDQwXimo0j7GMb5DhGg3/f6g+7qG6KiAuzr76UHXL/y8dqvedqVwX" +
                "4XbTmiooI8pizQFk5EyYW1j8eWPRChSKC/DfxrtfSEzwkuX1UEHvHqqdJAsX8qw7+LAyVuVKgV2UK71x" +
                "YpL+MOCBM8kYbLn1gTp4R7w/Y6/+eQkbihwDQKE5pD4Ok5aYLSxKSIHwXYN+4QMxWdRcYR0INuV3UmGJ" +
                "NgM8dNDBCqra2litJRrkAXUgRTFwNTxlyP0xHK9dMGSZ4GPYsofO1sbCNudVeFRQrkLZDcTqIp2mSTOM" +
                "UuC3zLVFOTkEkwaXIpoZGagQgDhp73dEf0IOukKGyLldsTZWni46/Eut25hRWRB1gV6SEzlfTXM4kmTS" +
                "qVqyd/6TTy3FH4+i6srGtmkbwnKR+uO8pnP89ntloCjkLzLkPq0eyVcpaFi23AFrqqq1zs+40DcKmSQT" +
                "M9iTwZ4EHrkyn1Lii4cGBDvnq3ZJ9d2uexzuOPxt0RqogtVTMdcGpwLi6ehBBvHI/ToWCVj1lXsQj9FJ" +
                "vKf9ZY/nxlP243HQPaKzCmqj9M71SdBp6cjEZtuOS5nxCx1P1DojUfpOJbWjIPGETMDMJJRRJCkoCxUJ" +
                "md43CAEYHBvCFh+uY6RPIZzImJ0JdYYME9Bc2+5gB6IWdulUCdkexibX2Hu6DaLr8nG65nnx4mAMyU6r" +
                "KS9tqd1xjSNuMVFP1WaYYS/OdS6xRSoxNbdk6GU8QxCAPFETCRmXOPDEMSmYnGdQ/CVrzs8gPWBq7Yad" +
                "4CILwUW2r4tgQVoxpLGgDEsZZZOQcMfcMqpVpaAXqhFYM3gUMat0DM0RhmcHYdsjBEKcAvlDFc/nWpxp" +
                "KlNloZfkFhbMfts1xwhJrmIM5sWa0BUq4+oWAVNexahRkQDUd5ODm7qqRVjd84FIkMDIIqm0s1JQ3vt0" +
                "taEVOEMn4ibXq3ynCrC04VE6/pv+2bVZYJsbDBPKFmyX2ZV05EQ79ayx4hd8wPJqBblHpkTgQI98objv" +
                "fThNqq1O6euFYgvB83osDdWOJKXKpfhDhCXGNKdWD7PEnIwQBMKpgFetfBuKsQjaktU7X7b70sIGDXSf" +
                "ZmudkvXg1qGSxLXOQBAOU4y923mKDRW89cB4xNTSskv3DvtWwbpmGmJqCyKrBsR2ocysARkfwfK5fbMV" +
                "Fr4MwbxGt0GlYFmMvX6FSYINeJ7Pthjbex9a5spWTjZADEv2QdBfkqRcZZEI92v0XeJe5IdQ3ccsvgwp" +
                "7CaJCW3L6sDft9IFByV+wSKw3dtULw2kiOoOsgpkIS05iHM8AmWP15Dfd8/OTp4zpiuKvjVkk0LPuXGa" +
                "36aFzueYG2PJXWDltaegal9D8CIvoYuqEjzdNIwkTfYtsqvexfBD7+SF5WyxwFiGWW7uuaOGiA3ARLpx" +
                "zfY/59hl5bzJcQv6CFi9vOwNzk4OfbCu0G7HSIjaEDxX1iGs3qlC2MMVToWuzJ0vTYkrMjUpuYTdR1wQ" +
                "8YzOUGQgYRdTqqCbKAMCTSyZJKIjJnK4UIWvBQAufMXM1a/V7v2Dxc4vB53W07/8R/C1NTZX//pm+4cF" +
                "dPrn17QUV6m9MqHz0QY6iHLY0MJS1yhuwGC6CapUBfZQpnzf51sMfPEE9kLXe41c5Eb5C6UQyTE9YRBV" +
                "n6pwtjWFeJaLZOxPBQBTwUzGIUH2UKZO24/Xw8GzWM9d++1j9+JcMIiO6HqDhkjsPSKoUjGaO9lULUab" +
                "CbijpyN6lGuk+Rbtk2dR/0frG0hzbtSxePI/uyjo3ePdU0yJzl7vtsVuoXUJT2ZluTh+9gxKGJmB0Mvd" +
                "/31imSwo2co1d/9yGzZZizYrQiUFcsDMMy13YVMaU8V9o5TtvU8ycN1xmkGd1KmfrTXTxdtYlqMrvM9e" +
                "s5EQFOQLA4FFzY0zMrMlyApDH/dRzTHdWwONlmH6LgjSsfBS4IcoCHjYFMTxdz98/9IuwYOamw2wcJPs" +
                "XYft+udzAfozCi9ivb7qyK9/z965JRY8oRO7q6k5emUf4eX3sfju5dEhf4cNBS5JMVt2ayBVWOkiaT7H" +
                "5AYZcljcbb59PdfJMsMF1Ggo9WLX2zia+0P1+u/LMICmM3bfsb6DwnKBltcW8RpSdMr6Ymy02m6lq5sK" +
                "5S+bwcxclxISo7HLFwAYHgh4/pNvcv79vA3/dVp0Y/S9eD389eSF/Xx9+a531Ts5tF9PP573B2e9q5Mj" +
                "92A46J28bFnTdXGLTiGkya7C5y23KEnhODZu3KRaWl32VSvcHmx1IfnhhmDZMTeOsfKh2QgWAh/oKK47" +
                "F752qz27fPi1rI3iW2CcSOUi5Ne2+Mh3Ab+FNKOQqfZS+bT0zepaVMKObZoozx8IvVPJNvr15Hnw7aOX" +
                "NX77DUQdksTyt1RRBw3VjoEU/rW3CgbTJA4yFJ+Z70Im6RJJsMUSW1CnptfoqnvWf38N9IQ4nZIJJiqY" +
                "bzdZKmw61NOgRqTLJekmx6L6TUhISDqi6kDW4Ebvev2370ZiD2HbL/sVTzyKEki84mlWq9CcL4g99IV9" +
                "xodRz+Fh7iwe/hLguQ8Ldiad7Fh9tq7ZjvNU59xccK9wqtDXBk2fxFujtKBSusMuky4qGyKZ4n6sWNHe" +
                "l4u2vSP71gq11fBEKz9vUg3mwbgCT91YXAkGFz5MiNusF6h6LTYuMTFZbTbUSFuYMPB7HplBaQcd045o" +
                "cefXjxIELf1g3WMxmOa+HVprcoUjP5J1XGf3C33dhz+CsBR1B09AKlafGCCgKmfnK1KZTyGf+HsQYW9l" +
                "tlRYqE1wxEAHQ4XAI16TQvJjPn1uIY6RBUB3UhZWywYP2wp0O1yFdoOzZ7SAqNkicxoc4U2PJCrHxhaR" +
                "ObZ2TUUUz2N8OmI61V1EbcVHoZZq+K2TA3yzrtq2GVA1C3xHQd6Jb7GT+K2I/4C/EnEinqOypDg+AQNX" +
                "k0/PP2Nz0n99gV9j//UQvyb+69Fnf3/x6eVnevZQAvhCI7Bxm7r1MrWxxRkaOe+DKe4LdLsIQ8MF1Vob" +
                "Uaq5AZVSOegd8VPbXcrAW/gi41hlthA3n1FLur6ah64++y58gIuPMnWHw0cq6bg81DdPbDTA0891JbC3" +
                "iEMGBVYy9TtvihENLjvAemvLnJjZHBQDdoKHNbY2R8iSpWtNwNkfYafI/SDkYZS5MbcdGOCXOtVsidWq" +
                "zd52bT4o2NkcmQ5hPJLV3kNafcrF5q80/UzGY+e0Gjf5diRH8fFC4280EbPFMp1+TXNYYnvSYSdYDkBU" +
                "7gKrgrXHQyxufqc5MLTvnJBXVMNMAQi6I0rzOFsmilqtNG4SZDPmWWXFz+q2+9SOL1t/IQ+yZYd95KEF" +
                "vsS1QG0Tu51bhs10YU/b0AcJWOeeqZJ7tPmfCYh/RozTSVOt2CRxJhbOpe1hWqHFK/xxwv7XDc/4RpBt" +
                "rMrqsruKgM40Nc5INMa87xm/CcLZBoq8Ftf+NTx1K/uPRkT67dyWCTPOmqUIfmrH3ko/s2O/WNEs1phv" +
                "rLCJY2+jg9tKlScH/tIRtMy7fQO9bT0TM5+ZXsEKBCMXi0KjCaWwYArY6YMqY6owAzp9q9ISDIDX1Dff" +
                "aOpK8YRRk588EXuyqgaGg6Cvvk9twF+APbyksFPEJaXB94yhVde+8zFWmdsJCLDb4dDtnWdnMPYHIXyH" +
                "UpchlmRLO7ZGYCNXuIU0duxlCc+0+dti2rD1SpEuL+0NlJtCpZofo5kzcojeBxbEU26HVGUG5hpsCV86" +
                "VgFK5Cmv3wF/FRWeggA5325DgvFXCLmHiOZvQ2oaqBHhLDL1FSZS7O7Hn4AnwFlHd/1PtgFrk/VyxcVj" +
                "WQ6EM2Uaf/gmzb/Z2Iuo14s0tjuBAu+PIRz4jHDw50sigfADol0VWEcS5HHVHSIJ74UY9u04wlhNZd7Z" +
                "dvF4bUfoKzOshKgMhDO6LwZvHktustOs9DLGqYKGTdIAZ/UDLKJZSWNNAIPDk6nWyRO+oetUQwCMG4ol" +
                "hF+h90HED+TRFSjWsZZpkN2NvX91dzNvIdgsVDEKJvEqW3UQKxwFSEPJcgsKOcEmnYuWmAuMcUgSXTej" +
                "H6HtVdd4btBhfzt+UJwlwKLbxD/XDrm96OORQT+vEcS3v7PIg+BD8YrDaH3cwxs5DlGpbMK32fdCbjur" +
                "DwObm9DxkLnpxmBd4umIlqSS5cJSBNwti5wvsMI5ZkggIbVog7Fs8ELivJVpRrOKyAT5EDajEFvnfgnT" +
                "7XZdwtTrkHfpfDnnAyYu7dAZjhHjrdgszSwvKIq9/z55Tr9vSw2i37cmenSIUCILISIIjAANzw5X+9aO" +
                "HoOLxpnru6zslLhRc5njcP7GqbHjZ+3tRRT6P14kqeTZYmnwHxqOcmVgrJcFRwZHeDCaZIeeIh5R8hMk" +
                "j9aYtPGEzi387sZIXM2xvSxxP6dpFBwEAGPS4+RRTbvCpou9BqnP9la+RVvafuCHrZ+aAGiNEEnMDV0j" +
                "TqqfYtDnLJ2UtcgF6qSmF5k9LHChynlo6f0BIZPOnYVXscu6V0BqM+Tb+UgX9f3WABqPJNT4pVMnR6N3" +
                "/mBXRdUb729ptd52Hu1oRDBVN8HYgwxVpxfut7HKCIVX5BBVcZACswDvhmkeoHzMEfPKrjcnzd3Q6b9i" +
                "6g4GT623Wv8EDTJY85hDAAA=";
                
    }
}

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
                "H4sIAAAAAAAACu1b/2/bxpL/XUD+h0UMnO3WVhI7zevznX9QbCVRa0uupeS1DQKCIlcSnymuSlKW1Yf7" +
                "3+8zM7tLipabFO/i4oBLi0Qid+fbzszON7Val+ZW98punpv8zMS6UJo+BhE+t1qtazM25bAMS63KPPyn" +
                "jkqTr4OiDPPSvR355x8/NRZNddFqFWWeZNPNd8AT5cmiTEyGFa23eVgs1JT+xrdJasLy1Uu1SMMsw9ag" +
                "TOb6Sev0f/nPk9bl8O2JmoP/pAzmxbR41pTFk1aSlcdH6jZMW60dhbd5mKZqrGfhbWJy+3b4/uysOxye" +
                "vrDf33R6F++vu6d/pz8t+/DqotPv9/pvA3rbPT89dKt7/Q+di955cDkY9Qb9gNadHh7Zl7WHgV3YGXXP" +
                "g9e/BN3+h971oH/Z7Y+Cs3ed/tvu6eGx3XY26I+uBxce10v7/H2/8/qiG4wGQeen973rbjDs9oeD6wBA" +
                "O6eH39lVo94lUAzej04PXznqr7vdyytgPj38G0nCHY36D3WTZHoelklUqFz/ttRFKRpUOOmMOtejAH+P" +
                "umAhOBtcXPSGYAoSeL5lyYfe4AL/DoOrzugdVveHo+tOrz8aYv0LJ8y3g85FE9hR/d0fQTmuL6y9cpvo" +
                "bF62Gqfz9nrw/irody4h5RffNV82IGHJq8aS68HrgWURb//WeHvR6//ogH/feDd4/UP3bOTeQp92VLEu" +
                "Sj3fFPObaywIQEB/+GZwfRk4JTw8cormhQV16Z79SLoIffiAdaQUWOgkWKOV/uZ3TmhWYXr9NwP/DsLa" +
                "qavBBl39QdD7MRgOLt6TJkNFX7QexZQrx/UE1I1mSaHmuijgklRksjJMskIl2cTkRLTJVDg2y1KVM61y" +
                "2qngvUp9oJK2bvPThSkSdlfKTFRSFuqfBgwWKsxilSbZTdEqdFbAcTL2H+iluE1eR86whD/dUfymAMiw" +
                "VPNwreBJtJov0zJZpFqdD96oMNeqWOgomSQ6VjOd6w3Ql7QW62ooeHsQm0nQQNYpyzCaAUpk0jQpiE8z" +
                "Jh9cqL3QvSuNKsxcMxcKK7wM9ltu/5nbPuDd8OVuN+4J+yqwkAnvmzScQrpxEkG48BKrmQbUHKBxDEWk" +
                "M63wAYjH+JCVOl/kugQpIeSp4mQyUauknMGfkBxKT6FhILzfnSkdqynKdK2S+cLkZZiVCocKsWYxGJoy" +
                "N57VsYkT3HB7jh4szAzDj1Id5tsWk9ufgCrRrZOTyOT65KR2LY418Gm1XMTCa1IK8WVN5fZbY2NSEBsQ" +
                "c1/PALaroDcA/B96K2ANnJk0LhQoD0kIcivjTEgOrETCe6FL+oCbG/6dzSfHqUNEYgNt1dphFLVNGjKU" +
                "12ov17cmXdLzXC3ypGA3sU/UxHoCtwFRr08AQH2zYWmMEt8dlDAmAPP9g2rprU5NlJTr+0ufFbz4WbHP" +
                "Flpt0RMcVynckzwWi5TMLMnqAPpz2t3fbzNj3YoX7FhmCaRAChfrrBQjHeMbbCoL56KYGmYbxqSr1o7J" +
                "4CFWxDHQL5hDAogVPpZaoVYwdeg8NCzWcVt1EGk01wA6KDWwH3eM7D/EktxWQsEnCHIYp7DR9IFFUoBm" +
                "UW7nf8I8D9fFAWMgM+JjXCDY25QwE0PHzrxOTUi6TVTMwxstm+x68E4aZjjUC9O2+sdMZ0q3p221Nsvc" +
                "eVHmIjMAaM8nLAqcLLDEzpr0/IC2qCjMFIyVuK6Ok+lWer4o11YbSXrCjZxtjfdiZpYpPKuDwXIqkt/h" +
                "7sEyBClwalZDq0yGM18BC9j0OuDJrAmHtMATDUHnOJeSbm0+wTYC3HeiHKIjtQCZ9cdGv/jqDKH2yCl8" +
                "7ZFI4Cv6lDIWhyJUkzOBV8niMI8h0TJk58EuN5nCpx6mGkQSs/MFDk9cy3pBjFfynMKDUyS9VstCbqDI" +
                "zOeQKu4LUdmN/aL1IStiEi1TeOrIQNWTjJZPcoiNoJOMKQLNIq165yes4zpalgkIgoFmUa7Dglx071y1" +
                "lhKeYENrZ7Qyh3QNTemKcsi9i9B3uJ0KojMsyE19I8y1AZs8LrBAwff4WYCv8DhAAhL0wsAO9kD51bqc" +
                "2Yv1NsyTcAxLA+AIEgDUXdq0u1+DTGSfQBsy48ALxArHl4DNPFzi6dDfiMVyCgFi4SI3t3Bi7L5YVeEJ" +
                "ob9pMs7DfN1ib8UoWztvSMZiRXwi5Dk3LdRqsZxGkMSPc8ndD4ZIP681nRh4kdAOrkjuJdJS63DYPr3b" +
                "i/U01/DAWDnBh9jA1wDOBLecWbkoAgwuo3IJH41lFUJxrj25TYpiOSeFJtUJ3Z1BqmtDdvYGxYLPFFoP" +
                "08jDrKAoVPZMdVndSQAbpsZi9/G1imYIXtvqDbnnO5xOCr8VckaGY7UXGAIpoH1/ff6G79ljiiv37uBA" +
                "8X+4Ip2gWxHqU2h5SV6VPF9N1+vUiSDxT54AiuylW2bjPaDKCgcNuouMmTRkHEY3xPAGDf9/tT7u1brK" +
                "4RpnX3y1uuX/l67Wh25WyYtoe9GaauQSJYpQ7EFGToWlQCWf7y1aIVaiBfRv490/WEx4KfL6en7vAbp9" +
                "dJ87r2eDTe9ZxrpcaahGuTL37k0+QvJ5CFXDCM6s9YFLdMeyPxXD/mmJDXlGPiA34lUfi09LzjYuQ4RD" +
                "9LLBgvLumPVqriknhGb5nZxjkuaAjTaZWc4pHJL9UsUGIkFOyL4MBkeXDcfX5JShlHWx0GNs2SOTO6Ac" +
                "N5NVdGFw0MJhDjx2nkyTuOlM2f1b7g5UOTmCYsOwmGZBhlMEECfw/bbqTdhMV8QQm7hL3Chls3RxFFAa" +
                "c0ChlQWxKdErNiVnsaiDlLCWdlV2vfOffJipfn+k064UbeuB4y7PKUYRCW4cO337rVJTkvPnePKfVo9m" +
                "tOQ/PGfusi2qPHaTpXFubqBUOC5StIIqNFSloOs3zKYcB9MFAsfnjNYuqb7bdY/FoDjDbWeHA5FDqvg7" +
                "gHWBfr6JiEcK87+MSwZWfZXCxOPUFR8oiQnLWjUei02Pa1Ulvr2QMCV3rn5CBsyXKBXgXBhNn/m+QjmN" +
                "RemLllyjQhyKwKCYhUisWFLIFfGJAtzyPhWtHfER9ZIfLWOEO/AqgMcGRSdGzDLIzNhiYRvOi+p2ukTo" +
                "Ry7KFvp2tsFzZT9JKzwbXhCCIG41BSVAW66OJBUnLqzaULNemnNFTCqVgiKzsjSYJS7gHUKMylKI6F4d" +
                "esKEDArSU+SB8VpKIAgThFK7odatImCBlHbpRpmoCLEsjsBSxdUaRN2RVI82slOcBicKch50EwmTfAvN" +
                "CYZnBaDtBQLvpiF25PNyq0Up8n+6SnKzZFuwUPYPXJmMcWQ6IleerxlbrhHA0TZK+yi0EsR0fIDp6sm1" +
                "TlxVK6z6eBAGURdYFO5QVhoZvg9XG4eB23OibjKz8uUKu/5xzHKLOXZsGMg3YczS8bVml9Ox2TSjRuEW" +
                "Sm85tTLcYwViWDhAaRXuO3tFwmv3uaNGzUP0gi7pcYjL2FgBeQOSfwOq200zLvUIM8LDiCAQGAe5KuJb" +
                "p0t1xS3RvDNb2ZawOtJKKEuzrM4xeq3X4AQwNCn4d2giKtvOQeAthMVOR+jkVVfuFVWsqmXNmKPYeE+q" +
                "Rb3hHXWpi9kmVHqCtXN5sRUOvatAvCb7oEOgHJjK+ygC2QihqLg7UGPb4OFlLkeVgALML8XYcGJxzGcB" +
                "B0so9uu0XdFWYoQxPcAkvauo68SU69cUQ6TuO6jczuDYrrYIOooG87JAFKjvEDMQ+Qg15Upln9NujdcI" +
                "4jvn56fPCc01+9UNTJPcUFkBSVd2m+Qmm1PsSzVrOIk1pITcHBUkMQXuRpWw56KhE0m8L5iuu5eDD100" +
                "uYmnxYJcFYWwTpttzcP6Vibapoef49WF3LLJ8YlTqJi8uur2z0+PrB+ucG5Hx1gO4BhXVvPtUXPsv0cr" +
                "3Lm5NHa+RNcaK1I9KSVFpRIJHFphUpIVROs8RuVQ0S2BJGMhkWVzTAQOFihtuggfMPGVglG30LjXX88v" +
                "ft6twD3+6T9KGtFUPP3zm+0fks/ZH3df2W9yU2LC1571ZfBkVKuiFBaxAVfqKHTEKeqcyiNTaej56oF0" +
                "lqAq1L/bCC1utG8X1TGc8BPZX7V2OLdkjYHTylQ8dv4eUBzAeFwnxV6zXD77YTjoP0Nl29XUfulcXigB" +
                "gPaO12J4Wm8DtaSTfLWTSlU3lKvd3Slt1eXYgZpC906dTYlrOsbcIGq50Sfq6b92ScK7J7tnFN+cv949" +
                "ULu5MSWezMpycfLsGVKRMIW0y93/fios4uKAviMe5IJeZp2jnJ6NcehwalKg+DEpd7EpQdQPQ7jR2lbU" +
                "JymsdZykSHfsDbVNYanNKkJ0SfT5a9ENBkJckelbzFIKI+VaQk7k5aQwyvV6KpRaZrnLyGBOlBcAPyMR" +
                "4FlTBCff/f37l7KCbl+pGWDdfYp3LabhTxfoqiJKoNaqP6cNxMPf0nduhcBmVGp3NS2OX8kT6mWfqO9e" +
                "Hh/xV6zOaQGCaLOyK3Dzr1DMaTymIIUYcQhcW17ezk28TOk91wlKs9h1Cg3V/noV+4dCBgrTzsVSxwal" +
                "4WJBynagojVibA7doHFa2ZqjS3egGa5xDM1ytUbEOWMXCAAYuX262NkYJYJ+foD/UBSg1s/36vXgZ1xm" +
                "8nl49a6LWZkj+/XsFwzknHev4dDtg0G/e8pjLqOai+K7hmiyqyRWc14B3RPkF3ZgpFpaNe6qFW4PlaqI" +
                "/PqG2rITKf9S3sJzDiIEubBJXHfOWe1We3bliuOhDJscgnEmVdKInw/UL1LR/7VOMwmZMyedTREyWoqa" +
                "bojyJ88fhN6uZBv8jLik+vaLlzV9+5Xu8hpJIn9LFVfA6NjJc+Jf23aHAxU64dTIGwvfaPUnSyLB5jui" +
                "QY4OgRtcd85774cUJ9VwukNmmHTA0qYUqYjqcCmCC4kuSOR+jEX1qwoRdrRVVUHcgBu86/bevhupPYJt" +
                "v+xXPMlcSU3iFU+zjTzL2YLaI1vYF3zk6hwe4c7ikS81PA9hocqik50cn01RtuPErS1VAfeKhgB9tN+0" +
                "Ser9JDnnwlxgRQdyUekQy5T2U9ZJ+r5cHNhO17dWqM5IG8L0KtVgnqLSylLvLa4Eg4WPVAmjZECy0Pxe" +
                "N5Ki0mYpjA+MggR5LxMwJPBaxROlbSne+smAWmG+tu7xeAQxvti3UaGqD/GgNOIaoRXHnynNPsZdRImm" +
                "v4Fq1FJ2SZ4CybZYIXrr2RTRxH/WXC1GgjHrisOd0NCAqc0Hgk3qeiLwKT5+ahGSkQXALSYLixDUSnlu" +
                "h0vFEAUuKWDiQZ/ZvUQT4uRpENn0aNJyjGyTmuMMQZ+nS4YsPh4Lqfou4OLgIxHMufr2cQBpl8PqJOmv" +
                "igK+chDeqW+pLPitin7HX7E6VZxmh+rkFJquJx+ff6JKo//6gr5G/usRfY391+NPvhfx8eUnfvb1ZPCZ" +
                "6t6TRr1ra5O0scdpHBty8ZeR7h0Oj+RVi62DqabtUBekdNAb5ccD12PBW3wJowiFUknEi090VmZztUxV" +
                "ffIV9Rouud/0HY0WUYnCBqe+YmI9A12JriBBhUMazUNXo2i0s9lfNNhsg/fWlkGw4v4kGE2oVg832Lo/" +
                "IxYvXWUCAUFA5SH7S47Hm8iuq+HnqtCij9Wqexs2pn9qG5vz0DUQj6a5D9D2ZHM+1Aa2POHMCmQnsRpd" +
                "ejtxo+W64QE3ns7aop3ujLdUtbeFInb28xDCct2oCtaejH+6ydfmPNC+M0RZUc0q1UBw2wfDa+kyJi7s" +
                "NEktximeVZr8bFN/d+yMsrUZtiKbj9hHHlrNniRJ2NgkpueWUc2cUsqmHTIwRJ5b/eEDx/lX+cU/Isf7" +
                "x+bJUtHEaVl98myPIg2jXtGvD/a/bDzG14VsaTWsWtiVI3Taich0SgWq+pjTAwM2Na92DwWUp6Ye/x6e" +
                "TUX7ix0j//rt4V+poINU/VpObJZ/KSfWseKBK8iH/AgNeUl/udaMRKX+0DcVcc6y2RfRD6x5UiA0Myus" +
                "oDBngeFQ0qEE76fAzR90GXH6WaPSFy4tuYC75nHdZm03VE8FMZvKU4woVjnCoF8rrdPQPUbZ4PsQM9pJ" +
                "YZqOf3DOrOrnzseUgG5FX8Nthz+3l5+dukh6Ls2TTfFRokY/ZuDVBJVbPc1JuLY4XG4Z+i6w/MDxXseQ" +
                "u5K23eTmS7kOQI7MKTcc96Hs35EKSZVwUKTBp/+5CxUwAk9yvbP7RRQ47DXE0q5GaPEniNhKQPM3HxtC" +
                "3yDAKaAdHWTZ++rPU6g9bjfu2j/dBuuAdVVyLpmwchCs4vIIwzdJ9s29rYR4vcBog2wEfm96dTBSWqHf" +
                "JKHsAieSYi6Q8kgGbH8r5EW7V0ewbwcLxnoaZu0tvcWhnYmvtM6JDxPrCVQZ72C241Cq6zz3vISjK4qG" +
                "CvIwZvWLKqIXQwP23MkHPJ0aEz+VTlzbZ+WCFxkSQXeovafwI3Xc4aQE1nILmd3Y1qrtxLyFQ0EbbFQb" +
                "patU0wF0CNCth32XW+CHE6rROW9IN/6YxhzJQFHbhjD2qmadm1bY34ocp2WxW2xN5HPjMNt2noz7+WmL" +
                "mv+iIgHpZ+VeyCGJk9yc1fA6TWNPOp1Ih/pBwPS7J6k/VI7LTdV4wFJuE6gusnQkh3wUS7JhbiTDAKle" +
                "B7nVx5ARICJuQC3eTiXV0ZEgb8Mk5SFDYoEshipQhKv9oGi5Y10XLdc1UOibL+dycVC3mgfEaAKYel+z" +
                "JLVskBD2/uv0Of9ALSkI9b6o5PERAQksgECGwni03Q9F+xqOGcMWI9Rd7K1pfzhVYBoHNcjo/m3gR+Rt" +
                "t4nMnBpGOn62WGIyIX7Gc0wuxYswjikOwBFdxZF2QimQiSI3/PGIdUjrOKQcSQ/cHIhLJrbnG+6XMI1M" +
                "ggGw93mk6KipVMSH631sDuRWVsV77OQPjkgUn5N80kX4jwJjXfQLD/8jCv6cJhOuSFcjBmbCxS3WeCxw" +
                "3smZpryYWMB07Fa9K3dl7ao+T711xNm5d7/Tg5JBgw1O+W7B0XlLsIsC/8KZGbh3z2xx0Q471ObfJuRs" +
                "iJHqgqKxYfFNqKZQ7xselCYjcMN740uyCt3jjoPXNPr+VLibC/13lNzBkAlzcPc/mzHDGClDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

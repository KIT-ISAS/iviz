/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MotionPlanDetailedResponse : IDeserializable<MotionPlanDetailedResponse>, IMessage
    {
        // The representation of a solution to a planning problem, including intermediate data
        // The starting state considered for the robot solution path
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart;
        // The group used for planning (usually the same as in the request)
        [DataMember (Name = "group_name")] public string GroupName;
        // Multiple solution paths are reported, each reflecting intermediate steps in the trajectory processing
        // The list of reported trajectories
        [DataMember (Name = "trajectory")] public RobotTrajectory[] Trajectory;
        // Description of the reported trajectories (name of processing step)
        [DataMember (Name = "description")] public string[] Description;
        // The amount of time spent computing a particular step in motion plan computation 
        [DataMember (Name = "processing_time")] public double[] ProcessingTime;
        // Status at the end of this plan
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
    
        /// Constructor for empty message.
        public MotionPlanDetailedResponse()
        {
            TrajectoryStart = new RobotState();
            GroupName = string.Empty;
            Trajectory = System.Array.Empty<RobotTrajectory>();
            Description = System.Array.Empty<string>();
            ProcessingTime = System.Array.Empty<double>();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// Explicit constructor.
        public MotionPlanDetailedResponse(RobotState TrajectoryStart, string GroupName, RobotTrajectory[] Trajectory, string[] Description, double[] ProcessingTime, MoveItErrorCodes ErrorCode)
        {
            this.TrajectoryStart = TrajectoryStart;
            this.GroupName = GroupName;
            this.Trajectory = Trajectory;
            this.Description = Description;
            this.ProcessingTime = ProcessingTime;
            this.ErrorCode = ErrorCode;
        }
        
        /// Constructor with buffer.
        internal MotionPlanDetailedResponse(ref Buffer b)
        {
            TrajectoryStart = new RobotState(ref b);
            GroupName = b.DeserializeString();
            Trajectory = b.DeserializeArray<RobotTrajectory>();
            for (int i = 0; i < Trajectory.Length; i++)
            {
                Trajectory[i] = new RobotTrajectory(ref b);
            }
            Description = b.DeserializeStringArray();
            ProcessingTime = b.DeserializeStructArray<double>();
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MotionPlanDetailedResponse(ref b);
        
        MotionPlanDetailedResponse IDeserializable<MotionPlanDetailedResponse>.RosDeserialize(ref Buffer b) => new MotionPlanDetailedResponse(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            TrajectoryStart.RosSerialize(ref b);
            b.Serialize(GroupName);
            b.SerializeArray(Trajectory, 0);
            b.SerializeArray(Description, 0);
            b.SerializeStructArray(ProcessingTime, 0);
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
            if (GroupName is null) throw new System.NullReferenceException(nameof(GroupName));
            if (Trajectory is null) throw new System.NullReferenceException(nameof(Trajectory));
            for (int i = 0; i < Trajectory.Length; i++)
            {
                if (Trajectory[i] is null) throw new System.NullReferenceException($"{nameof(Trajectory)}[{i}]");
                Trajectory[i].RosValidate();
            }
            if (Description is null) throw new System.NullReferenceException(nameof(Description));
            for (int i = 0; i < Description.Length; i++)
            {
                if (Description[i] is null) throw new System.NullReferenceException($"{nameof(Description)}[{i}]");
            }
            if (ProcessingTime is null) throw new System.NullReferenceException(nameof(ProcessingTime));
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += TrajectoryStart.RosMessageLength;
                size += BuiltIns.GetStringSize(GroupName);
                size += BuiltIns.GetArraySize(Trajectory);
                size += BuiltIns.GetArraySize(Description);
                size += 8 * ProcessingTime.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionPlanDetailedResponse";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "7b84c374bb2e37bdc0eba664f7636a8f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+07a2/bRrbf+SsGa+Da3spKE6fZ1gt/UGw5UWtLriRnmwYBMSJHEtcUR50hLauL+9/v" +
                "eczwZbnJ4q69WGDTIjbJM2fO+zWTPTFdKmHU2iirslzmic6EngsprE4Leso1PK1TmWVJthBro2epWnVE" +
                "kkVpEeOrJMuVWak4kbkSscxlEOwRWptLkyME/AKfIp3ZJFZGxWKujchxYz3TebXVWubLYIzvJrQiN/Lv" +
                "Ksq12YaEyyNeGF2sRWEdppK4g8IWMk23hNvKlRLSAnm8lfqtUDY/DGxuEJZwhBkAIdarIs2TdaqatFgh" +
                "DUlHm1zFHaFktITHeQpEPeDc5mpd7lZRjhKLlLUA78lPE5ujkD3iCjpRlvmflus/fa4hQwznykYmWXtN" +
                "MW87EIkDZA5BKgqISC8CwBxXuDxxcqWLjMjLE1hu12AWoLrVuiCWwRRQqVGRSkPYkOOVZpGBHhwoG1Iw" +
                "T7XM37yGnSoaQsSLu6GOCxBxTjyoLGZ2EkuIgit9pwZ53xhtzjQQKhT+GkbwexCc/ov/BFeTdyfAx51K" +
                "8nBlF/ZFZYYkGKBqBfTLBdlxLpMMdQ3Wt2JW5UwXed2mcSV4SVd16e1a2wQBLTKZ5Fb8XYP1APfAdppk" +
                "tzYA/7PAH23+I35kHyC4kNCh0H7kZfkSxLaSW7GUd0qsvPWejy7IZEFrUTJPwCKW4G8N1GTpAFfbgpaH" +
                "sZ6Hrc16eQ4WD1ginYLRksXN0MDAuKT/BvHB6pUiLoTOKhkcBn79mV8+otVgDn51WGIOHWbc9yKV6Fxx" +
                "Ekkyus1SAVbDxmEjlSkBv8DGM8U+COELzR+8XYo4mc/FJsnRU1EOeUmhJiS03usU1aptDiEjWaEPSTB2" +
                "DClL0EuKWyM3JaszHZNjeXoAMNOEP0qVNLuAYSMKp2xaJyeRNurkpBbjZgr2U6JYx8xrkjPxec3kDoOZ" +
                "1ikQGyJzT2X9uw2wJilZugCZ31KnsaWAjxLgUDJTHHuJNc4jioOJNhB/yXcMqBzkww7QFcFelS54EYVZ" +
                "+iwOjLrDiKxQ1muTWHS36BCpidU8yVDO2xNAIP7ccDMfGT0WGSOC1WGnAr1TqY6SfPsQ9IUl4Bf2kNyz" +
                "WqLmoKucuUd5rNcp+piL+Q7BcIWrh4ddYqxf8QIriiwBKaC1xRBW2UNnW4oHlIlYEEslYzRU58SWMwpG" +
                "Y9h3s0yiZW0/kpoVG0WZCswrVnFX9NL0AQxgB0o1OI9XIwUPdiO/1Idjyp64Z9fppxkALaQw6yzbBx9p" +
                "jNzaDu2APkRqxGzRlDARg2rnVK5l6px5JW8VL3LwwDtamKb8JNOu+NtSZUJ1F12x1YXxIZS4yDQgdPqR" +
                "1oJmJUYE50pYsMASEUGCAk+9U3V1Et1Crdb51lkjSo+5Yd3WeLdLXaSxk5yXk01+h1gPLIMgGU/NaxBK" +
                "Z6DzDewCbJY2UJJZEw6lbE80CNqAXnJAxhrsBkHwno2DbSSoUjnZTy3fOkeovfIGX3vFEniygJLHHE2Y" +
                "Zk75WSxNDOLMJUUOCrbJAqLpUaqAQuR0tQbNcVzZrpHrSpgLiN2GSjyq/jDy6tUKRBqRHMFeG+vZ5Bs1" +
                "S6TBzpMMwedGkn0jmMXyMIuUGJyfkIGrCAqeO/LVLDJKUvk0OBdBAXo6foULgr3pRh9hAlpgcvKbl/FB" +
                "3WNZbSkxYYz6MzPXBdwYbWEXsO4DehfCI4Qb2ARIgGoOnOAAKL/e5kuXUu+kSSSU3og4khRB93HR/mEN" +
                "c0aoM5lpj54xVnt8DdqsxIs8HZW50BYLECDWZ0bfJTHHLrJTCINgvGkyMxKKVC4ccctg78JQIEH1kUYw" +
                "bDbd0xfkpI0wiZ8jvT0sgoDZ8YMuiDMSmqgLNeSZZcCL1cIoRWFwDr/EGqIM4JlDftMbXzwAd0WUF4Yy" +
                "W7Ufh9VB7gRSrNCa0W6kzxZot3YLJfaK44Bdk0KpHYM6P7NYfPKahcqrbARoZard7reQIillimgJNWtX" +
                "XGBgvgfVpBCxuK+Txqcu7pZuxucXlGGPsZw8uIfQCf/LDRoE5kOwHav4I8ZTjHk1Q69Tx4KEHyYBLLwW" +
                "80vjO2BlCI8NDPdOUTszk9EtMtyg4b9J9XmT6sZAXFx+dVL14P9JSfWxnMrtEC63wUJBC5GbLQeQqTdh" +
                "bs/59wdAG1AoAuDP1re/kZjgI8vrqYLeI1R7SZaDH+unFi6szFS+UWAX+UY/yJikPwx44EwyAlsOPtDM" +
                "4ZjXp+zVPxewwGQYAIzmkPo8TDpidrAooQTCby36RRmIyaJWCptAsKlyJTWVaDPAQxcdzFDP1sFeLdYg" +
                "D2gCKYqBq2GWIffHcLz1wZBlgq9hyQE6Wweb2oyheK4CGKi6gVhtkkUSt8MoBX7HXEfk81dg0uBSRDNv" +
                "BioEJF7ah10xmJODbpAhcm7frM1USRcl/1zrDlZUDkVToNfkRN5XkwxSkoxB666MFPflb2VpKX5/FlVX" +
                "NrZL2xCWTVKm84bO8em3ykBRyF9kyP+2eSZfpaDh2PIJ1lZda5OfmdG3CpkkE7M4jMGBBKZcmS2o8MWk" +
                "AcHO+6oDqZ4d3PNwx+Fvh9ZAFayeirkOOBUQT6kHGcSU+3UsErLqkWcQzzE8fGTw5dJz6y378aw2OqJc" +
                "Bb1Rcu/nJOi0lDJxyuYrZvzdT7BJjuVkkgZRUHVCGWCXEnooEhP0hMqWs/cHtHFcqM/18iWP9YI9iCQy" +
                "Yj9CdSGvhDLTbiLYhYCFwzmVQ6GHYclN8/Z24fOzPS7USjZKQfAGcdAWFCMtjxd4skTTU1dY1udvflKJ" +
                "81CJFbmjQRdkeLBxrOYS6ixxVBLGZGBJnkLLF2/rZxbVgqpAIGQhz28xi8xFBJUrqMBRRQUk1NgRT4ka" +
                "jShog9oC1gdmH2aSMs8KcZSsAGqXNCCo4aAc+nbOZFGqqTGVRhfkCA7LYcePw2iPTOHwHXoz2s2olPtZ" +
                "7PCwkOKNUX2A0w+Na4cv1UiwOpcAYSB1odvCK2WjoJkvi9OWMiBjzsVtpjdZFU0J/jl88qEv9lzF16mO" +
                "ksppsm/fyGfaBSKzmsSeTSdAPnAhXKA9Pr8oz5uwseV1Xs/btWKjwKw8k5Y6RJJO6T38M8Q+YpHRPId5" +
                "YRamiAHReMzVmN5FW+xzdhTu3md5WWJcZEBPaQ/OqRyvnSZ4AUx0Cvz7bSKcza4SHJjYgCIO00lQ1/4T" +
                "HQOVYO0iwza+hyx4PJpTdtnEim8AdsUfduLBbxWKt+gcdHoJ7S4O8BUmfxfNSu46YuaOcAjMt6NcRADz" +
                "BXsaaCyOE+6eSHCHddqucSkyQjs9wiR+q6jrxbGtm5GTenmqSQcWVMzVgMBG7xJdWCj71D1UCkh+knN0" +
                "5oDTDWZbKNl75+en3wY03kBvaOw0N3rlDt/uEqOzFRa72EMbbKUOFLThWwhN5Ap03pSDM9uWTSTxIe80" +
                "7l+NPvRPXxJP6zXGKaxZs5IvGm+4wEpEl8elf8yrr7F5kecTtFAxeX3dH56fvnJBuNpz93a0Swei4sZZ" +
                "vlM1FfsHCOH15jvWVWFzhEjVPOduFKchEM2sTlFWIFofMapoGisLkoyZRJLNMRI4WitTlvSAEx6xAPWA" +
                "2n9+qqD45aAS7P3Tf8To7Y/9sylOSP/5xe4PCufsjw9XKWjShGROCc8FMghjOJPCbtUqnqFgxQgqVAbH" +
                "IAs+ryunBHx2BHaCx3ONouJWlQdC9R1O6A2vr+ZMxhvUAiJWJuKZD/aAxSOMZ3VSXIKlMdmPk9HwBR6X" +
                "u9nZx97VpWAEXdErTRjCbOkAtRYTA7WXSjUf5KTuE0pX9KlqSLIdSic/otmN1rdQr9yqE/Gnf+yjhPdP" +
                "9s+wsjl/u98R+0brHN4s83x98uIFtB8yBWnn+//7J2bRUMWUaR7cZS4ysvZcdYPKqUkBK8ck34dFSUTN" +
                "8q1Sbmw+T8FVZ0ma+HmP2mWveIrKQvQt8/lbtg1Cglyh37udeeSFxlWAnDDE8QCUhvI4EHXM0jkioTkR" +
                "pQDoHYoA3rVFcPLdD9+/ZghMvTwhALiHFO+7nSY/XwpQm1V4eFrqqbHx5Lf0vYdg3LSV2N8s7PEbfoNH" +
                "1Sfiu9fHr+gRoA0CJFjmOghI+xtt4tZrrFCQEb+BP3XnrysdFyl+p6lArtf73qDBtJ9qLP9YtUBXXchN" +
                "Z/oeesA1WlpHRFsoraloi3Am6gaLvssxqjwXBrPyA0WocGa+BABkGPAxpZMncuH8bQf+6wZ0uPO9eDv6" +
                "BdIY/z65ft8f9yG18OPZx8vB8Lw/hlDuXoyG/dPX3tt9fKIsgzQ5KK7SfEhIINFafxmkAq3O5SoIvwan" +
                "Ukh+fUEN7IRnvNiu0B0GFgKnahTXvY9U+9WafU5ugTNN/AqME6ncPfzSER95bP9rnWYUMjVMKlvk5Vy5" +
                "HYOwbSr5A6F3K9mGv0BFUj19LGWNT79iFq+RxPJ3VNGwC9WOYRN+ugMAi9UPBxUKxcy3kXFSIAmuzWEL" +
                "6jb0Go5754ObCVZItT29kgknKpgPIlkqbDo0fqCZoS8P6dDFbfWrkFBwdEU1LGzgDd/3B+/eT8UB4nYP" +
                "hxVPfGekJvGKp2WjvfK+IA7QFw55P4xzfh/mzu3DD7V9HtsFh4hedqw+15zs3vNMZzwM8J9gfVXnt30S" +
                "D3gSQy1wl10mWVc2RDLF9dhsor0X6447zvrGCTVoeaKTX2lSLeaxHq089QFwJRgEfJoQ97AJoObz4a1L" +
                "LEbbsy/SFpYH/J1vt6C0a8PNrgh4SFue+tem7zW452IwycrJZWMkVb+dI1nHTXa/MIJ9+hSEraVPPDVS" +
                "sZ3EAOFvbkJSlNkCKoi/1iLsnUwLhf3XHG8D6NqVP+ARTzSh2LGfPge4x9QhoOMjhytwwcMN7vwK33vd" +
                "4iUxAiBqdsic7njwomcSlWdjh8g8W/u2IoqvTnw6ZjrVfUhzwGehlvrynYf8fAiuOq6/r/r/ckgg78U3" +
                "OP77RkS/w1+xOBXUUUtxcgoGruafvv2ME8Xy8SU+RuXjK3yMy8fjz+VRw6fXn+ndUwngCzO81lxr57ln" +
                "a4k3NHLeJ1PcF+j2EYbuAdSvWvNZeHnErxJq+0pH/NTx5yfwFR5kFKnUddv2M2pJN6H5ftTncmZe24tT" +
                "mbrHe0I4h3B1aDkWcdEAs5+fOtB1bIMCl7Z1PE0xosVlF1gPdlzpsg/vdOFF0+plg62Ht73iwo8fIPeH" +
                "OAPyt+ufRpkPblXXDPBLQ2a2xPzxBY2bPLWF7TvN9Tv0T8PmV1LWvI7iqle6okym4y5UtY7c3d0ZxcmF" +
                "7qnR1ZUddum1u2NovavkcFdNjkBS/qSpwnXAt038RZv2zZ5D74IMUd06qqGgIx3+tyKKZqd0L6RWy9gX" +
                "lQ2/aFrunrtn7LyF/Mc1He5Via3mSdwJNBax03kwHIkLl2vrHkjIuo9c/3hEm/+ecPhHxHidtNWKMxFv" +
                "YvULZAdYVGjxBv/twOHX3XIpxz5ubCqrU+kq/nnT1HiZoXUf+5F7MrVg9mCLrBHV/n/7NK3s3xoP2//I" +
                "JeDLpZA8UATwES+7ggCW8i7Rxn2d3Jyd9SfYsPDzRW9weTPun/6AfwL38vqyNxwOhu9C/No/Pz3y0IPh" +
                "h97l4Dy8Gk0Ho2GIcKdHr9zH2svQAfam/fPw7cewP/wwGI+GV/3hNDx73xu+658eHbtl0EZNx6PLcq/X" +
                "7v3NsPf2sh9OR2Hv55sBtNuT/nAyGoeAtHd69J2Dmg6uYIvRzfT06I2nftzvX11PEddfUBLlAcH/VFca" +
                "rf83Xfwvg7zsJtPeeBrC39M+sBCejS4vBxNgCiTw7Q6QD4PRJfychNe96XuAHk6m495gOJ0A/EsvzHej" +
                "3mUb2av6tz/CclwHrH3yi1A3r4OWdt6NRzfX4bB3BVJ++V37YwsTgLxpgYxHb0eORfj6l9ZX6JB/8si/" +
                "b33jQb7/+gNK311DbYj5YgwAIRAwnFyMxlehN8KjVy9Lo3DCAnPpn/2Etgj28AHg0CgA0EuwRiv+Td+8" +
                "0JzBDIYXo/IbTdtqZtCgazgKBz+Fk9HlzZT0dAxK/D8/lfVF5jgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class MotionPlanResponse : IDeserializable<MotionPlanResponse>, IMessage
    {
        // The representation of a solution to a planning problem
        // The corresponding robot state
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart;
        // The group used for planning (usually the same as in the request)
        [DataMember (Name = "group_name")] public string GroupName;
        // A solution trajectory, if found
        [DataMember (Name = "trajectory")] public RobotTrajectory Trajectory;
        // Planning time (seconds)
        [DataMember (Name = "planning_time")] public double PlanningTime;
        // Error code - encodes the overall reason for failure
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
    
        /// Constructor for empty message.
        public MotionPlanResponse()
        {
            TrajectoryStart = new RobotState();
            GroupName = "";
            Trajectory = new RobotTrajectory();
            ErrorCode = new MoveItErrorCodes();
        }
        
        /// Constructor with buffer.
        public MotionPlanResponse(ref ReadBuffer b)
        {
            TrajectoryStart = new RobotState(ref b);
            b.DeserializeString(out GroupName);
            Trajectory = new RobotTrajectory(ref b);
            b.Deserialize(out PlanningTime);
            ErrorCode = new MoveItErrorCodes(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MotionPlanResponse(ref b);
        
        public MotionPlanResponse RosDeserialize(ref ReadBuffer b) => new MotionPlanResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            TrajectoryStart.RosSerialize(ref b);
            b.Serialize(GroupName);
            Trajectory.RosSerialize(ref b);
            b.Serialize(PlanningTime);
            ErrorCode.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (TrajectoryStart is null) BuiltIns.ThrowNullReference();
            TrajectoryStart.RosValidate();
            if (GroupName is null) BuiltIns.ThrowNullReference();
            if (Trajectory is null) BuiltIns.ThrowNullReference();
            Trajectory.RosValidate();
            if (ErrorCode is null) BuiltIns.ThrowNullReference();
            ErrorCode.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += TrajectoryStart.RosMessageLength;
                size += BuiltIns.GetStringSize(GroupName);
                size += Trajectory.RosMessageLength;
                return size;
            }
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/MotionPlanResponse";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "e493d20ab41424c48f671e152c70fc74";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+1bb2/bRtJ/z0+xuACP7ausNHGaa33wC8WWE7W25FpKrmkQEBS5knimuCqXsqwenu/+" +
                "/GZml6RkucnhOftwwLlFTHJnZ+f/zsyun6nRTKtCLwptdV5GZWpyZSYqUtZkS34rDd4WWZTnaT5Vi8KM" +
                "Mz0Pgmc8MzYFZi5MntAgxkypLNDo4Jqeh/SoyiL6u45LU6xDjBWlnzwtzHKhllYnamKKeo39pV1GWbZW" +
                "JYBsNNcqsirN+bXQvy21LQ8CWxYEyzjCHECEtdMgu1q0pdIJFljmiRA1qkYaQDT7yhNQplhz3+oYfNmD" +
                "YJKZqHz9qiIwpHGa0C0K0B2bRKtDpXN6sEyludUFOAC1kQUtxN0kSrNloYNLjPVKnnnK8JoeQ5obBCf/" +
                "4p/gcvj2WM2xZFqGczu1z2u1sBJSq+ba2mhKqoT+05wkDXrnYgrR2CxLEXytWwi0rdv8dWFsSoCWjCYt" +
                "rfq7SXP8ivJEZWl+YwOYlQV/vPiPNCg2wXChmAoo+VGmlbOoVPNorWbRrVbzZVami0yrs8G5igrYwkLH" +
                "6SSFvcw0RNlEfUmwgGsswdPDxEzCrcU6ZRnFM2CJTZallk1+THZg1X7kx2D21sAMiAtl8loGB4Gff+qn" +
                "D3j2p8/Kzw4rzKHDTOueZ9EU0k3SGMKFma1mGlgLoIYabKxzrfCAhcd4yEtdwCtLkALrj1SSTiZqlZYz" +
                "RQ4HlBWFhpHwfK9TUquxJVwonS9MUUZ5yUY4g14ytnBwU7E6NkkKQ9z39AAwN4w/znRU7ALGQhwlxLSO" +
                "jxEH9PFxw+fHGutptVwkwmtaCvFlw+QOgrExGYgNibnHsv7dBtiQVFS5AJvfzGSJVSA7IgnAQ+MiHWuJ" +
                "RcyahEdd0gNCB+IR+04BlUM+4gBtFTxzUa6apCFAGVb7hb6lQKVJ1ositeRu8QFRk+hJmpOc18dAoP68" +
                "4Wa85ExXWKKEEMwPWjXorc5MnJbr+6DPLQM/twfsnvUUPYGuSuGe5LFYZORjLuI6BP05ze4ftJmxbs0L" +
                "ZizzFFIga0uwiYiHjtccDzgyiyBmOkrIUJ0TS6TkUIt1V7M0njXWY6lZtYKfw+BhXolO2qqDmLoNA+yg" +
                "1MB5vBo5eIgb+am0RLWb0Jptp5/NAGhTW1pn2T74REURrW2LVyAfYjUusI1tSpiJIbXL1maizDnzPLrR" +
                "MsnBg3eyMLMgjUZZW/1tpnOl29O2Wptl4UMoc5EbIHT6iayFZiOKCM6V9LxFU1Qc5Qqeequb6mS6lZ4v" +
                "yrWzRpKecCO6bfBuZ2aZJU5yXk42/R2xHixDkIKn4TW8zeXQ+QqrgM3KBioyG8IhK6iIlqQhLoFMNNgO" +
                "guCdGIfYSOB2d0RUth+3AePVO0Ljkzf4xieRwKMFlDKRaCI0gw1ElDyJigTiLCOOHBxs0ymi6WGmQSFx" +
                "Ol9AcxJX1gviuhbmFLG74JSHsyGKvGY+h0hjliPsdWO+mHzEVpjGyyyiJAR2nuYEPikitm8Cs5Qu5bFW" +
                "vbNjNnAdIz26ZV/NY0pPKDj3zlSwhJ6OXtKE4NloZQ5pA5rS5uQXr+KDvqNs0fLGRDHqz8JcG7gp2nLS" +
                "hOyJvoV4RbjBIiBBLwycYB+UX63LmdtSb6MijZBREuI44gi6R5P2DhqYc0adR7nx6AVjvcbXoM0rvMTT" +
                "YbUX2uUUAgQgctvbNJHYxXaKMAjjzdJxESFD5FDFSwbPzgsOJKQ+1giFzU339AkqayNMk6fY3u4nQWD2" +
                "+l5yLzsSmagLNeyZVcBL9LTQmsPgBA+JQZQBngn2N7PyyQO4W8YlEloCq9eTsNornUCWc7JmspvI7xZk" +
                "t3ZtSz1vFA9Wqgyk47ml5FPmTHVZ70ZAG2XGrX6DLZK3TBXPkLO21TkF5juoJkPEknIlKvzWJdXD++uz" +
                "c95hjyid3L9D6MT/0YoMgvZD2I7VMkjxlGJew9Cb1Ikg8atIgUXm0v6yMQ6sAuGxwXBRFZB5jKP4hhje" +
                "oOG/m+rTbqqrAnFx9tWbqgf/T9pUH9pTpRyi6TaYapQQJSpzDiAjb8KAqsz5HtAKCiUA+r019jcWEwZF" +
                "Xo8V9B6g2kuy6mc4d6jCyliXKw27KFfm3o7J+qOAB2eKYthy8IFbA0cyPxOv/nmJCUVOAaAwElKfhklH" +
                "zA4WI6RANLZFv6oCMVvUXFMRCJuqZnJRSTYDHtrkYAXXbC2q1RIDeaAI5CgGV6Ndht2fwvHaB0ORCX3G" +
                "lH1ythYVtblA0VbBuQpnN4jVRTpNk+0wyoHfMddS5eQlTBouxTTLYlAhkHhpH7RVb8IOuiKG2Ll9sTbW" +
                "FV28+ZfGtCijcig2BXrFTuR9Nc2xJUUJtO67PXfVU5Vaqt+fRNW1je3SNsJykVbb+YbO6e232kBJyF9k" +
                "yD+tnshXOWg4tvwGa+uqdZOfcWFuNDHJJmapGUMNCdpyo3zKiS9tGgh23lcdSP3u4J6GOwl/O7QGVYh6" +
                "auZacCoQz1sPMUhb7texyMjqV+lBPEXz8IHGl9uet76KH48brSPeq1AbpXe+T0JOy1smddl8xkzPvqPL" +
                "cqw6k9yIQtaJNMDOItRQLCbUhJolzOP3aJO40OzrERgv+AyRJIrFj0hdxCujzI3rCLYRsKg5p0skehSW" +
                "XDfv2S58vrcniVrFRiUIWSAJtgUlSH1X3HWWuHvqEstm/813KqkfGlFG7mgwSzY8LJzoSYQ8Sx1WhAkZ" +
                "lJJnKPmStWRlSAqEUjehThAYWSj9W9pFJipG5goVOKo4gUSOHUuXaKMQhTa4LBB90O4jTPLOMyccFStA" +
                "7TYNBDUNsaNul50szgwXplFBbXuOeYzloOXbYbxGrmMK38WaVyt0JvUsVXiUSMnCpD7g9E3jxmFE3RJs" +
                "HAkkmqgL3RJeKSuNYr5KTreUwQcMN7lZ5XU0Zfin8Mn7vthxGV+rPlqpusm+fGOf2U4QhVVYvGPTCXCf" +
                "rYdxQXtyflGdv1BhK/O8ntcLLUZBu/I4slwhsnQq75HfIdUR05z7OcKLsDAiDISmPmTybXoXbanO2ZG4" +
                "e5+VaWnhIgN5ynbjnNPxxmmCF8DQZODfLxNTb3aeUsPEBhxxhE6GuvJD1JaqwbaTDLsxHorgsdKltrNN" +
                "rPQFsHMZ2ImHxmoUb8g5+FAO5S418DVt/i6aVdy11Ngd4TCYL0cliQDzS/E0aCxJUqmeWHAHTdroaIwZ" +
                "4ZUeYJLGauo6SWKbZuSkXp3y8YEFJ3MNINjobWqWFmmfvkOmQOSnpURnCTjtYLxGyt45Ozv5NuD2BnnD" +
                "xkqTwsylE5rfpoXJ55TsUg1dUCm1r1GGrxGa2BX4vKmEM9stm0iTA1npuns5+NA9ecE8LRYUpyhnzSu+" +
                "uL3hAisTXR1W/jGvPseWSZ5PaKFm8uqq2z87eemCcL3m7uV4lRai4spZvlM1J/v7BOH15ivW+dKWBJHp" +
                "SSnVKHVDEM2syUhWEK2PGHU0TbSFJBMhkWVzRAQOFrqoUnrgxCsloB7Q+OHHCopfDirBs3/6Rw3e/Ng9" +
                "HVGH9J+f7H5IOKd/fLjKQZM7JHxO7QMZwhj1pKhatVp6KJQxQoW6oDbIVM7rqi6BnB3BTuh4biOpuNHV" +
                "gVBzhWP+IvPrPlPhDWqKiJWrZOyDPbB4hMm4SYrbYLlN9uNw0H8em7nvnX3sXF4oQdBWncqEEWYrB2iU" +
                "mBSovVQ2LhdgXb+htFWXs4Y036F09iPu3Rhzg3zlRh+rP/1jjyS8d7x3SpnN2Zu9ltorjCnxZVaWi+Pn" +
                "z1F+RBmkXe7975+ExYIzptxI4y53kVG057IbUk5DCpQ5puUeJqUxF8s3Wru2+SSDq47TLPX9Hr3LXukU" +
                "VYToS+azN2IbjIS4Ir93K0vLi4xrCTlRiJMGKDflqSHqmOVzREZzrCoB8DcSAb5ti+D4ux++fyUQtPVK" +
                "hwBw9ynecysNf75QUJvVdHha6Wlj4eFv2TsPIbh5KbW3mtqj1/KFjqqP1Xevjl7yK6ALAkgpzXUQ2PZX" +
                "pki2PlOGQoz4Bfypu4zOTbLMaJy7AqVZ7HmDhmk/Vlv+oWwBFJ2Jm47NHWrABVlaS8VrpNactMXUE3WN" +
                "RV/lFLo6F4ZZ+YYiMpyxTwGAjAI+bensiZI4f9vCf+2AD3e+V28Gv2Abk+fh1bvudRdbi7yefrzo9c+6" +
                "1wjl7sOg3z155b3dxyfeZYgmByVZmg8JKTZa6y+D1KD1uVwN4edQV4rIb05ogB1Lj5fKFb7DIEKQrZrE" +
                "decj1V49Z082t8CZJo2CcSZVqodfWuqjtO1/bdJMQuaCSefTsuorb8cgKpsq/iD0di3b8BdkJPXbx0rW" +
                "9PYr7eINkkT+jipudpHaKWzitzsAsJT9SFDhUCx8F1GSLokEV+aIBbU39Bped85674eUITXW9EpmnKRg" +
                "OYgUqYjpcPuBe4Y+PeRDF7fUrypCwtFWdbNwA2/4rtt7+26k9gm3ezmoeZI7Iw2J1zzNNsor7wtqn3zh" +
                "QNajOOfXEe7cOvLSWOehVaiJ6GUn6nPFye41T00uzQA/hPl1nr/tk3TAkxZcArfFZdJFbUMsU5pPxSbZ" +
                "+3LRcsdZ3zihBlue6ORXmdQW85SP1p56D7gWDAE+Toi7XwRw8Xn/MiElo9u9L9YWpQcyLrdbSNqN5mZb" +
                "BdKkrU79G933BtxTMZjmVedyoyXVvJ0TiY432f1CC/bxtyAqLf3G0yCVykkKEKiuxfmKNMqnyCD+2oiw" +
                "t1G21FR/Teg2gGlc+QOPdKKJZMd++hzQGiOHgI+PHK7ABQ/XuPMzfO11Q5fEGICp2SFzvuMhk55IVJ6N" +
                "HSLzbO3Zmii5OvHpSOjUdyH3AZ+EWq7Ldx7yyyG4brn6vq7/qyZBdKe+ofbfNyr+Hf8k6kRxRR2p4xMY" +
                "uJ58+vYzdRSr1xf0GlevL+k1qV6PPldHDZ9efeZvjyWAL/TwtvpaO889t6Z4Q2PnfTTFfYFuH2H4HkAN" +
                "6yJKfcSvUy77Kkf81PLnJxjFSxTHOnPVtv1MWjKb0HI/6nPVM2+sJVuZvqN7QtSHcHlo1RZx0YB2P991" +
                "oO4g3QcoqHTZPJ7mGLHFZRusBzuudNn7d7roomn9cYOt+7e9kqVvP2DvD6kH5G+bP44y792qbhjgl5rM" +
                "YonlwxM2bvI0Jm7faW5eYH8cNr+Sss3rKC575SvKbDruQtXWkbu7O6Nlc+F7anx1ZYddeu3uaFrvSjnc" +
                "VZNDSMqfNNW49uW2ib9os32z58C7oEDUt44aKPhIJ83jbJlo7p3yvZBGLmOf1zb8fNNyn7l7xs5b2H9c" +
                "0eE+VdganiSVwMYkcToPRi1x5fbapgcysvYD1z8e0Oa/Jxz+ETFeJ9tqpZ6IN7HmBbJ9SiqMek1/O3Dw" +
                "dbdcqraPa5tG9al0Hf+8aRq6zLB1H/uBezKNYHZviXwjqv3/1tm0sn9rPNz+I5dALpdi8yAR+L+OGetZ" +
                "dJuawo0O35+edodUsMj7ead38f66e/ID/QTu49VFp9/v9d+GNNo9Ozn00L3+h85F7yy8HIx6g35IcCeH" +
                "L91g42PoADuj7ln45mPY7X/oXQ/6l93+KDx91+m/7Z4cHrlpKKNG14OLaq1X7vv7fufNRTccDcLOz+97" +
                "KLeH3f5wcB0Caefk8DsHNepdYonB+9HJ4WtP/XW3e3k1Ilx/IUlUBwT/U19ptP5vnOQvg7zshqPO9SjE" +
                "v6MuWAhPBxcXvSGYggS+3QHyoTe4wO9heNUZvQN0fzi67vT6oyHgX3hhvh10LraRvWyO/RGWoyZgY8hP" +
                "It28Cra08/Z68P4q7HcuIeUX320PbmECyOstkOvBm4FjEaN/2RpFhfyTR/791pg08v3oDyR9dw11Q8zn" +
                "1wAIQUB/eD64vgy9ER6+fFEZhRMWzKV7+hPZIuzhA+DIKADoJdiglf7lMS80ZzC9/vmgGuNuW8MMNujq" +
                "D8LeT+FwcPF+xHo6ghL/D6V6cu29NwAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}

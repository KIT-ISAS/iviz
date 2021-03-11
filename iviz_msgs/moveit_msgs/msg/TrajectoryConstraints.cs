/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/TrajectoryConstraints")]
    public sealed class TrajectoryConstraints : IDeserializable<TrajectoryConstraints>, IMessage
    {
        // The array of constraints to consider along the trajectory
        [DataMember (Name = "constraints")] public Constraints[] Constraints { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TrajectoryConstraints()
        {
            Constraints = System.Array.Empty<Constraints>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TrajectoryConstraints(Constraints[] Constraints)
        {
            this.Constraints = Constraints;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TrajectoryConstraints(ref Buffer b)
        {
            Constraints = b.DeserializeArray<Constraints>();
            for (int i = 0; i < Constraints.Length; i++)
            {
                Constraints[i] = new Constraints(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TrajectoryConstraints(ref b);
        }
        
        TrajectoryConstraints IDeserializable<TrajectoryConstraints>.RosDeserialize(ref Buffer b)
        {
            return new TrajectoryConstraints(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Constraints, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Constraints is null) throw new System.NullReferenceException(nameof(Constraints));
            for (int i = 0; i < Constraints.Length; i++)
            {
                if (Constraints[i] is null) throw new System.NullReferenceException($"{nameof(Constraints)}[{i}]");
                Constraints[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Constraints)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/TrajectoryConstraints";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "461e1a732dfebb01e7d6c75d51a51eac";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuVZW2/bRhZ+J5D/MIAfYm8V142DxcKFH9LYaVxsEm+cDXJBIYzEkcSG4rBDUrLy6/c7" +
                "58xwhpLc7GI3xgJtA1gSZ87lO/fDA/V2YZR2Tm+UnamprZrW6aJqG9Va/lrkxild2mquWhzF09/MtLVu" +
                "kz2Lhz/9ml7NHmTn/+P/HmQvb34+U0u7MkU7Xjbz5vuE/YPsAHoUjVqaptFzQ8K0eNIorcqiaUm1pW0L" +
                "W6m61FVVQJlE3mNcf1qWA+2XHa5NjGp0WzSzwuRqZoGDmltdEjR4FNDBs5UuizzLcJtIV3ppsuwXC0JR" +
                "SED0G/0yToHKrm1TkFyDc7X/cXj0tSsMtNo5bePvwwvviqaYFGXRbgbnV/3P43u32RYmZLf+G7tXUJ0s" +
                "pgUwj/a6aBc4pNXUOLKtmtiuygPkAq0Af8CU+LG/q6eLwqxgJ7jIp57FIzwtjdPV1IwnprTrUWT/XfJM" +
                "T6DBr9mstLr965P+TP/D1sk9vzN1EuypWptivmhJ5JmmMGK3gmZN4n3qMDeVbU2jnClh2JVRxbK2UBvE" +
                "SCULBd3Ag9Wz0jb4DQ+/GGcRCBreXyIc+qvtUS+ZCHEvFt918LuDlcyWm1lRJS7QWyRqe5xlL4ymtLTg" +
                "PwQsZTFnJ7ZFvFefdwB1ZmYcpbTgL3Sqdxe6bGezxgB574dMZeZw4MhbCMBrN8eRmp3SJufWlEFNTC24" +
                "kM2NXZrWbQSFd5wyTz2JMZMYC8sgwMqW3dLcLbnKfiKPBv13cjIeGjszJ4f8f3Owb5dTmjYXYMURyKVu" +
                "IEGuXQ7JWp3rVrPiC8hh3KMS0V/ill7WyAL8tN3UhjM/+yL+zU2FcC3LjeoaHOLyt1x2VTHVLUAp4K7p" +
                "fdzkdFRr1xbTrtSEl3UwEB1n1yHq+NeY3ztDwF5dnDGmZtoR5uBUVFNndEPWurpQWQeoTx/Thezg7do+" +
                "wlczJ9ADcxhStySsua0dsIcwujkDj7+IcsegDXQMuOSNOuTfxvjaHCkwgQimttMFe/n1pl14J15pV+hJ" +
                "CSeAkwABUH1Ilx4eJZRJ7DOUtsoG8kIx8vh3yFY9XdLp0QI2QwjNVdPNASAO1s6uUFRzNdkwkWlJ1Q1x" +
                "NnEaPQfdEpbZwXPCWFybLYK/umnstIABcq4XIdrZGmNU6G/mkHvDvc90zpC1oAc1JCt+SM4zcwbK1Hpq" +
                "jslPrtiytoJfUHRx4etv4mJeOFxFoB9TwkBisM6MVNGq3CKSEc+gsdSfQdIAZrqt6xrE4OsoQw0FOlnG" +
                "0pVDczw/Hqn1wlRyimBip+YwKKbKFfMil5tgtOwva+W1G6l29hgwo3FimYUZbAYizkpDcnSsrmZqYzu1" +
                "JoXwwfno47oc5GIvaa0dUeh5EkNErznphoqBYtEi8FEIQsK57T9t+k9f7qXADbMymfyCShhKQkjpsPTp" +
                "hSRnKjEoa41Fuxg0BNa1K5aociuYkQOcrdhRtMe6YKU0GnWoyYuQlwBv1ix0bUSQGyJ6HShRG9lTDTUG" +
                "xRQswGC9KJAGuOGKrKmGlZayXb5VvlDEBwTHTIioXsF98lwqNESM1EZkrAWoTjVaNXJzMy24jyYfyvlP" +
                "36aR83k9D/UcBWeESN6j4kuQhBxC+Q+U8qy/qhCd87p8u1J1l4USV5nY2xEQoqAeqekGGRFFDZ8snvnZ" +
                "hMmISuIHjOBCo3RD48JFIEGMYEfO4NFEO26ET0b4HwFDJeZv6qfX789/8J9vrl9cvrk8f+y/Pvvw96tX" +
                "F5dvzk/DD69fXZ4/CWhT2SQXJphZJn+Kfs/CoRxZGrMRSt3wqI9MIB9PhDuU8kj89EJy7EwZdPHSklAq" +
                "ERCkZyS4binM6MvDeOehDLbE4bnv4aA4izrib+9H6gNcDfB8TGUmkLm5M9W8XQSJptYhFdcofRCTyjrK" +
                "VK8fQD+O2I7fn58k3z70WNO3j4A6FUnw91JxJiWzo/cAK0r8qAnUkIicSN1znyfQ8Oq86EgEP/KIBwU5" +
                "hO74zdOLq3/eQJ6UZzAy0yQDSzskqIjrUGvLpVvmS/IkWgJ4Vh+Vvi3QFcZKNKA7fnF59fOLt+qQaPsv" +
                "R1EnEAFuCeJRpwV3jj3mPhbUIcXCkfCjoTHwEe08H/mS8LmLC1WogJ2YT6Pu3MkTs4thpMIj3N/Km0lM" +
                "0magcNwRcqFGp1NHH2JM6T4VNPL3rh759cp3HtQQpFtg9i61pTycK4nUncMRGBy8p/6H0iult6exf5H+" +
                "ATBQuh02PyMxGDWz8lzKCQGe7DbQIkkT0M/f/+iQ2R0VifTc/emYzrKDGXa4xJB5caDxH7ct96VBxG9v" +
                "ozpAdSg/ffs9ok/94dd6sTga3kuppU6hL7CJMajiUyIMW0G0X7qaY4z9Makk2OZ1PBtj9OauxtsTY2KF" +
                "9p3mPYOVZ0ZM3noCqGk9rbCCwvzdYVsYbuz2dizNHreiLZ2/dG9oBUX2oRY0e9hEuWRW/XQqoprbMbD7" +
                "tgKnXffeZeh/tFkauvfXt0so9pi/BikpZPVk8RTbXO7SY5BsmfmO3PXfbrJsTWTgc1RIHrHNlHGOqn5Y" +
                "RjZRxj4m9QQTSdea8e2YLo77w7snNl898WX7xJ9nLZV66L7tuzhourrz3d2s45cKS/qJdttxRy8jd140" +
                "U3UYl5BHO7unfuHEAcDnKS03g/ZG94Q30oqsF1SNqUGh5Qk2DrQJpcm/923aJVhHhF964XhYi/IRNyKF" +
                "fQ2OduyTMSjCakLZCTTupsIiuU5iEPVXsPtZUk0DPkzg1eu3IA594AZLSLDslhATcN3yR2jLLtKg92rX" +
                "BiuNKDlIUiMRoRtBHCdk0UwHqsl+V1o4P00gTEpuS1aFWSedtqACv97db/PEe0hMscuaYLWxQaq1XZkf" +
                "EV3eLZLvNx3GmbpzPH4e95E/aEvZjFw1hAIRCD5Cb50IRkR4fOMg62Uhks7HKcEfKTVFC5HP+sZ/+yR4" +
                "wUDY0SGV4s7alGVvIZnqA0q8iZNuJ7pkzGIj1tjcYm1Xmj2j+I1frPbr8YazxnNnlwncwTNbu8aetxnY" +
                "tBdZnF5vu1jiWTzUYFnlLLyHV4UdL2CXutrIUHfMc5IX1y/J5MwTOTCiqVxWsRXnM67qIlHCWNby/AJK" +
                "1RtgVGBl5QXhHiOYVZdrvSHvVafs1xinjjPZBZPAY2Y6sCf4yrpjGIniu9FFhF0wEHZOnmFcK27VLxCI" +
                "axQPO6eGFtO30JM1JTatWNfk6TnyidLaz35RSSvBdmAkylnAOO4xAxhMjEuX5EN0TbhNaQM5AxaYdG2/" +
                "ZkJx2405mqiIPZSyJa9EeKffdMDxbncTuXt3u1xx5rDdfBH9ibqJnYgTf9tysRATaIfgR0uoAJoTM9Vd" +
                "jKo9zQNzoWU1CY/KzAkozTGCN4Rk1qCZxBKd5czkKX2GKy0tWtsZhY5mb2XcxahTqgP7RPfwGSQttKbY" +
                "KvEbciqtxOHEL+dknGV+0lpQ7h8Mvtz18iOfiX2+RMoFIEGzhDtrHxJ07xhhRz7bmwCCU0sI8bvVdHDh" +
                "xtoXidC0RZklthGPxs0IbHoyYin7oOSVNpQ58RV3bb08MuoDetyzNU8FpjkKFGmILU1Lr3VCsfOc95O/" +
                "Lr5/LBxS6pCrhhkkhYJ2kiwC1KIxvX/yyvoymBhmJw8InVhFtKfpE7gFT3xGS4qfQfnwZMTyyf7+hDhh" +
                "dR3cPrV/UpqRHUIhgkhjOjfmc1kf5+EWV8EGGxQoSS8N7NYNjviSNixDFYXcwZ46H4rDjg8F56KmVHI2" +
                "PL7Cs7AJ2i4uaS/gC5u4XeJ0pAvtqQN+jmtkj5pvOrbgUQU2/9JgnAyh4uspVqm4abLFuxvyDX4hOQiY" +
                "0KGEd9V98/NenavHWHPizw8jrO3OVdgM3Vy+unn9BuvIrR/ittL/8L7fDfuEyXbqef+J+vv5XaVEll30" +
                "Q0jlmNL4ne/2O2Hxx/A+d2vUZAJcjh5k/wJX01jQmiUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

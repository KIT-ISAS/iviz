/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupActionGoal")]
    public sealed class MoveGroupActionGoal : IDeserializable<MoveGroupActionGoal>, IActionGoal<MoveGroupGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public MoveGroupGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new MoveGroupGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, MoveGroupGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MoveGroupActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new MoveGroupGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupActionGoal(ref b);
        }
        
        MoveGroupActionGoal IDeserializable<MoveGroupActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupActionGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                size += Goal.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "df11ac1a643d87b6e6a6fe5af1823709";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1d+28bR5L+nYD/h0EMnKSLQjt2drGnXR/gWE7iRfxYy5vHBgYxJIfSrMgZZmYoWTnc" +
                "/37fV4/uniEVJ3dr5g572WDFmemu7q6ud1V3viryedFkF/JnlM+6sq6W5XSyas/be1/W+fLZaXaOP5Ny" +
                "PnpeXxVfNvVmzffy9s7o0T/4nzuj52dfnmRtN9cpfCUTuzO6m511eTXPm3m2Krp8nnd5tqgx8fL8omg+" +
                "WRZXxRK98tW6mGfytbtZF+0YHd9clG2Gf8+Lqmjy5fIm27Ro1NXZrF6tNlU5y7si68pV0euPnmWV5dk6" +
                "b7pytlnmDdrXzbys2HzR5KuC0PFvW/y4KapZkT07PUGbqi1mm67EhG4AYdYUeVtW5/iYjTZl1T18wA6j" +
                "u2+u60/wWJwD/WHwrLvIO062eLduipbzzNsTjPGvurgxYAM7BUaZt9mhvJvgsT3KMAimUKzr2UV2iJm/" +
                "uuku6goAi+wqb8p8uiwIeAYMAOoBOx0cJZA57ZOsyqvawSvEOMYvAVsFuFzTJxfYsyVX327OgUA0XDf1" +
                "VTlH0+mNAJkty6LqMtBckzc3I/bSIUd3vyCO0Qi9ZEfwN2/belZiA+bZddldjNquIXTZDZLoByPInZxB" +
                "snyDNejWtRf1ZjnHQ91w1kpSGbbz+qLEnsg6yDTZdd5mDWmmxTpIQ89ky4UqgZW8stGwz80VqOP6oqiy" +
                "ssuw1qIl3YI0itW6y4Bz9CbMVgnnusDQAXQ2LcAimEI2K5oux+ZxRimKbf7l3LcFGMb0sDN1RHW2KIr5" +
                "NJ9dYmZz9ABdbpYd2LBt8/NC9iFr18WsXJQzXaDNoB0bdPKINsCkVpu2w8wyMB5ajX0L0erD7d4Kkqvs" +
                "dOt6UuzOCFKNuH61zKvXOm2f/mg04tsKs3u5ZhsQrz1Pan2xrxkPZqh0B5IhgZTgUfAr95dUUGTzYlFW" +
                "pRAQJWTu6+Gm8vtKwAGErAb0hW2VD/WmW29Ail2gBhLIq5zM1RVNK+DY8LpuLtt1joGF6vjKYRkTsAWE" +
                "Xwsgo2+9dQIpQJisw8uRCnlIW8qLjpS3WYPxIcWzZ4tAw3+vITVbHwiERImLcZqCdIbprOtWVt9mNWbG" +
                "GYOjRNRsmoaypq6K9phv2iJprAABAiDbooNKbIrR63pad2cyF8yo6SYyL+cbdG5LCkChevkUkbSq51BJ" +
                "5Faghm/H2dMccqBYFiuZxYJCEg3zpgHHya4J/xkLnWNWykDygsJvdlFCz3GO5UJXhIl3TS4I0b1OtJWC" +
                "AHjMPe/Kltw3ehJ7/PBWNXsChAt7URvygcq8usEi8QWipsauyE7n4HfKEv5u6vlmRqYWmtKlXpfLZXZV" +
                "1ktRq4LldJ6HIgjz9Xpp0gxSTAfBplR1l/2dAgJCSN8dpVOWwYcTJorSATgxlVEkJLz9ezGDTL5RyaOo" +
                "uBm9Ce9T+LH1rlEq6qNaUZ+smHSPdUB+C6FWtTYkf7rUOyZpco9zEZ39voIwSkQoABeH9o06bcfg55Rf" +
                "fDBmALBEyThc/KzXMHmIB4crPSeEFeBuVlM0JmRaIREE+2NZwmErCAboquzsom5A5EBjvdyYGPHpN7A9" +
                "GtFoauUA8CQIzLzrqLICKlf5u3K1WWX5qt4IM6i23IFZEstyWV+ryebMJLaIGSpHo8Wyzrvff+YN47Ci" +
                "8ChXYJ+IoQDNWpskUxkuBiFkx2Ym1JJMTXCbwYyDGuxIq6r+8tkMLEysgkDGWfbYJneVLyFlhd0wtcP7" +
                "x5++xdc3twG82QEu0ZLGYA3lj4kSQl6Rqsk5Zj+VQBBoHd1ocAKcLRAjgxKhQSgA2RGynUKSQMuGc4XO" +
                "b/LqHDM+LFfcuLzqljcUi2Urpu9suaE9MC1EHEMgAvv3x/ePRHLaOELj+sk0j9C3oIJ7+un4vsCCeFdE" +
                "H5bjYnx8G0YAsP8lRc7ROGwzGk2806TVrZ3ojHpt0u7DdvtR3Tt0X1DebkAF5Z0LLoG2qBMpExYbSH8y" +
                "Gr6KSAPri/kNFgTLHObZtH53RIpxGeB002cdziv1hww0rNobNTGda4Q7VvW0hGajPoEYWdDPiYBhwF4X" +
                "y+WYrHUqikuJQgx2NG6KBbQnvQfXhpgiFtqAodtR3+d0gVBCXmDjtZELOnQ7VgvTqNxJTechOlnEJZyW" +
                "0XlRA2sQ3YL6b0SOPyTgiQIdyp5//FCgORvqg1HXzpEDTUEGY/YwL0hNV/KRhLGAw5AJHY6z4G/Ixq8K" +
                "8D1XGnpST5UNBYrZH414Ece0C+c1JAJ0NGCs8kvaSfD3RIdDnUOiUX9W7VKFGV6jy2ExPgfHC4FJK+JQ" +
                "vGvxx+EyNOU5hJH0xECr0DnPbHWQOIsHqmZkzjoYtobOSN2ZdKDAu6k3sBywBvxoLAwgGsznJe5qV9fH" +
                "pH4D0cfoKxE8zptgyw5kCkJ3ufIu/LoJv37aiyyJtujtIqSsIgrzKYR+n4I7biMksBpv0U5eYKdaNybo" +
                "DkBSXrYjbm/d6Oh/5ke1hKVdtITlizmgK1iyF/lVUFRFdvryCzVBg2oTwzoF/Zxt0S4ZQrpP5vViMhjs" +
                "cdfBDAaUWb1cli3XWU9pskGP5f4Ne95iT2UVtI0CDo5G3v+Jd38pvWELe29wsH2aGGSO+8Uyh49azRks" +
                "IgWDns25oAqcgZSjuST6DtxEFxySkvy0WGyJFpmheijSP5HMq7qFLs6CXtZIl4dSxGfwpU7rOU2TQ58P" +
                "GtKGZnBrWcAJ2NEYA2HDc6OtkxNIrOLkJPF0LGwgzpe45p1OvktI7mg0rWt6JRMu7sOJu90kGBiAGi5w" +
                "gVDgRb1E6MpZHxbMrCkpADRMg9XJ2k3RQrrANRb2abDrQJHyAKVkjO1op4LOm5olh01B5cv3DXygsiXH" +
                "zaCCMbBoQ1pnjNohuJZymqsah5LPxeQ5Oo5Noy00bHqvlcb3EOojh8YuxQLb1YXIoXpWwYI0AC9W7P0C" +
                "RhQXJo6ofqEhIJEYEtwc8l+ZFNYlRYI4CooIVdeBj9udIS4fz3xhi0eBwhDxG9NU3mqjYVa45GEbRX4o" +
                "J3lXDiE7KAFBjKnLGMrAtkTQyYjb5Y941/AsOQLZSLaRVk0fwzIZUY6px02riXpOOll7rJ0U5t7DOPuW" +
                "qo1aTrWOSVFZRVUDoO3PIHRJWKtj0VgzuDRgVq46bqdGBegz3Rg1Enu6Gt3bZO0WDFHMOZ7a8ieIeywZ" +
                "iFQ4CddIvEcsP432BRqIYb+IHHE9fdI0m2ge0CCVHYRyHJp06mxCqAr9mJrEozNC8soJPnmlGNiPTNnW" +
                "PZQtr90YUk2KnVcxQAKy/RV0BCqbF+cwsYT2aGvNa2wtvTEIlfrahTawspl1G3pjiywOqLSsNhmwD5ue" +
                "4ThGX5xFmYZob2CJiM1KHSLxebGRgt2kfc6LLooAupchbnMJuSRyKptdwFYYZ1+QG94hDLwEmeRi/UNh" +
                "mLyA3sKwf319+oWItYdU44cwkOEh3uTXjO9r1BDGsH4kEZPQkrxFOjtFJP40JaBoXzJ177uYlWzh0MDT" +
                "CIBLKJvxZyy4N4f/l2T7lWTX9OkufrEk8+b/lyTZbYJMzVB2bwfO3xsnYbQK5LzV6BqqiQ34d/DtW0ET" +
                "Piq+9uU6hnnvch5FtwfJMi266wKk0V3XWznQduBfjkbuDSe+4OgvG3RoKsoA99j2tc449E4vGdIdaTWX" +
                "9L218OnHOHGi4n2OYPh1vbdtJEWFlbn4baMh2V/StKkvmU2sxDtv6SLRTaBARjRQ8gYS8hmHbbQm8dna" +
                "7WuByh679g4bopsU13cM5c7oGWUT10jr8petUoDFR/UM9hMkvMUn9ezy4HWItwe3TuQZyiHKd+7AaOgK" +
                "2KIH7KF//vbIv6CylzVEVHTDJNZFjrIJwRQDXTGdNZjF6K5a6KnPzWYy4F2EYiyFLTvGxQpIpEbUpx4j" +
                "4qOxYxgDjOuYp313Fzz3u9UtCctIEg8cgLmtPqIUqEf9zOWTyEbMW4bxPIrAWEUSNO3qDUQykoI3Ibr9" +
                "SZiYTkOirKjymN8kqcikQ1QiAmyisRWafDGpqbMSdwl2GDNqw9oT7IaYjrofDN/pIiV0tyKMsBSAtqgb" +
                "pFsBtMOg1lDgbAkDnPE3ZIGEFwyKBfV9jKqYUR0jZcbRGsTQpYTFQsA2MLePpQ4W0EkyZ9FZj0k2IIOz" +
                "m9gQvinXBep3ggEz2AzEqhbZZVVfB3/B2u+HLXew42MzDCR8OBfshGCPW/nCNrtj3CB6W6nh8FAISGBh" +
                "A1mZ8AwxqqQiQvv5VqOiSemC4Y1pDtO7NgQFBtK/EzrO51oEoIvRNbwhBIKJeVOPopnQlTzXtn3nbKvd" +
                "kEFS4UBm2RUyT4N9joCzmjknH2bGuMkKE7wCskTo6Dyl1Sv/RJcxNhsGatved5KWViw8L9qLPlS+QduV" +
                "ftgJh98iiM/JH56HZXwNJRRmIaRZ3WxqEVZp5l6LGhRY/EaZDTs2n8teQMByiKN0bqzrkIXISLcskt/i" +
                "7B7P6f0lhKFYD/kbiSdKQDxpBBpFUn7TwjIu3sFm4PQR2VOVKjJnPJrewKx7fHr66D6HeS1ytTfSoqnp" +
                "aMIMr67Kpq6kkoFBIwgJZPdQndCgPkxZQcLBHfhZISS5zfmRjvT66fOX3zx99Kmsab2mqKJP69RsXrDJ" +
                "Vpm0OQzvW6vnKbSTrxO7EBf56tXTF6ePHpgcjmPuHk5GQXlCcW2Ub1stCRNUNMDRsn1zx8bLnJbFolOn" +
                "hU4zBBoy58QVUOsSIwpUhCuByblOUXDzkBN8qVl8TYsAJh5pjHpDS/J/SJP6/WIF4vFX/5O9/PzPT5+8" +
                "YWnkr+9s/xA/T34+/SFyU6KCC1F7JssgyRi9oFMD26AdJF67+lwj6sGf1NAuSIUB9J5pcVmEeG06wom8" +
                "0f4xtioJOaEYCK0qm09d3gOKA5xP06mYmpWAyp/PXr64x3oMi7J8//j515kCQHw1UDEkbeCBJFNHWe1Y" +
                "iZEkVe2uU1CmJLYDo7Jbuy6sJF5+XV/CarksTrKP/uOAGD44OXhC++b084Pj7KCp6w5vLrpufXLvHlyR" +
                "fAlsdwf/+ZEukVUSnJ6GeCoTjrp7ZuNwcxIs0H4suwN0YsUbGOGyKKxedrEEtyJdDXfHNNQugmWeQ5Ho" +
                "mcfTz5U2BAhXRda3kTU4QuKyqjELlUk1LkNntlgJ8wuYkywgQN4RBXg3RMHJ7/7tD59pC2pfTbSi3faM" +
                "D2yks798jbQGrATmNsI+9QY++3H5lbdQ2DJUdnB93j78vb5hMukk+91nDx/II1o3bAAjur62FtD8qMyb" +
                "D17TSOFCfADPi+lXFKdslvwuydWuXh84QYO0P1wM9zaT4U4sSZCMfrsmsR1nsxvY2GK6geKKzKJQ7u6A" +
                "MjxzA8ry6BPsnKkbAgBGsU/FLsyoFvT9Y/wPQQEWdv8h+/zld1Bm+vvs1VdPXz+FgtHHJ99//ezF6dPX" +
                "EOj24uWLp48+c4Z3ESW6hnOyVmqruVRA4gP+hWVsY9MYOY8tQq0D8vucftohaXaiAUGpdWOi0asQ2Zbo" +
                "eufC6iD2OVAVJ1lRcw6xcJmquhHfHWffa4z3b+mciWTxnIrqHCajzWgohug/hfUB6eOI28l3sEvi0/cB" +
                "13z6G3V5MiXFv81K4oPcdkpO/LW8FwSozhNCjdLYagfzebnhFMzfUQryeSjcyevHp8/+ekY7KRnTN1lg" +
                "coP1EIJiRUlHQhFSfeFGokTobai/ZShNYfFWKLvowZ189fTZl1+9yQ4J2x6O4po0sZtgPK7poudnOS9k" +
                "h+SFIx2Pos7H0dXZOPqQjHPbKCzHcNzp9pmLsntMaG2NCvgnliQHa3/Ik8wGlI34wloVi8RLpCHBKfvT" +
                "69QaqWPLfXxsSHUmHSAzkNRg8bRKI6duNY6IQcM9RcLoDKgX2mzlp2iVDkNhsmE0EvS7pqCJ8CTiiYi8" +
                "VryE1FwSqk3a7W+NmEwI9vUiVGkWHaERT43FFb8nNLsPXURHM2igZLb0Likp4GwrFzYlApGwJv6YiFqr" +
                "IJXiOal1DQU6WCbzYDB8ULU94iBvDIAkHQwWB0hCed7DXTFYgVY5KLPZgXdJx2qnvWHLF7ILa74yGH1h" +
                "XnqE6oeHOtXi3USCg3uasPjquxPEmkAF16nTH4MCIXKQv8s+Zljw42z2E/5vnj3KxM3Os5NHoPRi8cP9" +
                "t4w0hsdP+TgLjw/4OA+PD9+GXMQPn72Vdx8OB++J7t0ZxLt2ps0GfZzitMz/N5t6EDhSE5OcFlABE8td" +
                "rJg6MOUPx0ltOB56deFvuVd1v7WWNbwNEfVkLNVvxTseHNQjYWKchohJv7Y85EVZG4OsRjtIcIq8GCxz" +
                "jLWPdlRitNulGCwRiy97y9ou0phvPDIBg2DC8BBr9Zq9hWjDGY2fK6t2qZueLCCTJkc8HOfpQRIP4YTT" +
                "M3bGRnLqfjIiBPylpt4zxlr4oiQfJxkYone25JVtRa+d70+/6cuokHutE0Xd7/ANgrvqGPfaX4XXveZ7" +
                "2bMBTrhv4WmHng/5rKmGLyT+7WkT8c4c5VHWuCKUz9bXD0/R9v4hDIFcDsJxSFTPiskU5H99HIf/OPmG" +
                "YNJV8TYYE8M6pmHLHe8FugQ6LYkRj6PEnE7cjOwQ8aKaNXSIrGJj4ZB6OahGP7RyNKXg7AmyO2o5/FQ0" +
                "tbh+yACCHWIlaTwqo5PYy45vE/jtzGrHSHpmQNiRuNrtSg11d7XSWZKGQ4SKWcW43q58qMaMFgsmCXmy" +
                "KKQepcbhKMrsvEFY0HRDnbS7ZqVhEpZGh1sOCSgIBPNJrTqkT8BOdNw682zkeYlvtGVsNNHTfv/rCGw/" +
                "MqWPliQElDtOsacPTxU7lghub81HbRvMvjEsKBa4rI/LGdqtq6Nfmr6yTbbMWVozO/B6GSqEGfCL813P" +
                "YoJJT4c7tGPLeIVsRTAomGaZhyR7CHKFI0Xn2HGYNO2OJfZzabcvyoZ+74KSxNteaGWnDv1VAqlfMvR+" +
                "oWRZnV4vi1wk8ipujtBWLDwaoO0W//x/KgC9ClCCJZ+I25UVTUPB4TosyXzGE5dTOQlaTN5N2HESGm+3" +
                "uHlvi5+GLf45pdkuo82LgZIlxyN8UtyJVzSJommn9cHzsp1phlIVz9FWZUm4ykQYQNpLCWMvhJcHwFLl" +
                "AltMk2cIwjGzqikl0OM500YWa5YyawJ+bpMTERPnx9EICj4xmm6EJiNThPxVPdXCaZXEsTunQegvsO8n" +
                "ScTI8SMAXrx8A+BaKbbCDHjuLzlmitUKibSxxjHMPNQ4O+p4PL1RsAgYO9TELPDjP7G6lri4KpE1jtFk" +
                "xQroetssEjl9KLVXwAHSKTdWD3vkx8OF9lskmbL1phGhOQ6c3wu9yjaKHosXJTiN0FkhGsHh0VBVq0SB" +
                "pFI9BfhH9zX9WLLoGC966Q3Ng2CoM68gSu2waNgh1UWOJT8y2r+6IEixYz20obm3HQrkzK7sCVZVK1Lj" +
                "C69TUHQ7ZXb1dd5Y7YTvaZiyEn0+JLGEsrTWa43NAfVIfYwGe1a8J0ESF4Oqejn1qG0+0wZyLECP6lQi" +
                "z8TO0BklA9vdDfRbsvUNcFRKVEAmoqerbVvzJcqeSb3ZQ6FrpAz8/D0nPJFBe/sZzgn3OVFpN5KIJ1J1" +
                "g2Ap2YDxnOhAfwFAVP6GdhENWtM13nHGIbQLGWU9ecri7a63SZRZwHE8mOrIEGCiulQeIvCJ3nIErtkU" +
                "WqCTVN9s8xyzBpr6hRKStJ+Ulbe8muZ2crMjJE5uT5nJZ2r9/CLSE62JLY5TehuQmPMEq2eydqXX+kyL" +
                "Wc5Dql4xtm08yCg7y69Mxii+/ZYSwEx4iW1FMhmkS5ASzx1qiU8u1Cp4102dUQ/smrqhj7U7MJaROZXA" +
                "ClWrXBhgJqWmbGQ8NS2So3J2QpeBa/lkktjkJUQuEOIrS0aX1buADoThh54XOwWAE7WykLjkw6p3VxJu" +
                "tMU5K2/nUn1DZPMLT+4JxShEr0i9bxoXpSU6H7WDgXqWpa7FoS3aI4fIRA1S5ywycWVnI+8G/6q890BH" +
                "SKFjXiyhUhEK2ImwCKcSZcWsQ7LFmhpMNmZLDthNQ0GL5AbTBDiKj6ScvGjwmpWn949lfnog+36o8u22" +
                "9j9RzfPB3Q5oN5F2o8Dn3ku0YAsPB4vkKfB60EM4fkkPqL9EBXd3h5535bBFQ05cNEqtRqSuUBurkJmr" +
                "HiiX1BYwxaZklxAd10LvKtzwIToyYM2MjgF6pG7Mr+DooUq6p7hKp5sK2zpUUQ8Yxi0UD3EE4+c7JCge" +
                "IJWPP58eIzXNdIXlwJ++OHv5Gin3wYuYkbcX34X6BxOYsk9h7H8i+/5WVaIJXb5wUe5XZwxP2ig9+k2B" +
                "A1dTAIg62ouLsvP2JPdR9MAX6Kp3K1MdguXJocSYAxncTLX3sPTgqrc7sehmsehN3vmkX4GaHboDfRRu" +
                "jTuTD+EuImmnx+W16FRofCEXC8RrdMRqsSyuHolEbfmmYR6jsgyRXGKhNzSZDJECmlZlX/Cb7Mol60RW" +
                "z/TQPrtOWBLzK+YhNQS+4n9BP4PKOvXeLTNixll1VHYoJz+U91t4nnSC4Dq1RbEyESVFupUYTmnxJmFe" +
                "5ZDxXKpUcpo5kNQd62I43kTHS5cThK0Gj69+ZjFxKaoYb1mM5n5cncRbs/xmqz/66Imkl+Jf+D+Q7tv3" +
                "I8UKJME6uosAmN8gJFPOpBCwWpTnPC2hBn2y1N59WhiVuyZ6Y5G2EjVvG5niRHRoTss31ZAzWn++eL9N" +
                "juX/w0xXpEIx6r0GR3Ns1gmj6NWs3OYqGR8xSZY1kSakLq2RU8paWU2SropC7goA2IC98X3V6rtXBmVu" +
                "K+njlzi5KvNdCHUk9FRpmy+KSWAWJMZ4BWRYn0wONnmt+VXJ9okLiAwoFxI6erV5uBzQCgrd7xNAnIve" +
                "qSbbFlkUrMz0abC7hC2LisjWq9loxW8qIeNc7GBlA6nhD1HAbSrV70LwTjqZkZV+2rqhbXgzG9y7nCur" +
                "ggm1C+bOa9lshDlU981+pbmIYOolvz2vL7X72VpiFgZ2cltOev+jvO7f/5heyZfe1Na/FIapIBtHYUgj" +
                "D7wmzqmdpyXrzTfrpV810y2yw11eXsw1SY7qtmPJZmdAp8oBwYnelZucVB7dlWU758cjQXoWf2QX3IXj" +
                "UM/1iL7ftRevy7H25GfAk6DAmsurztvR13h6pQ+YiUSe7VuvPS9pK7Q1r+wrvK28793/YzXIx9sXAaHS" +
                "Qt1x7A1Mi1WOOsGmt6y1uKxS5Uv3tV6KCxVPPSmCV8aCas707j1ZjUd68gFIqRsm4LV4W0F5+rk3ZN8q" +
                "+LZu9JbU5XxPtwvt57qen6fBHceU0wssxGffsnrVwB2HK57vOv1vtUSGH8XpseEwpxNvVvKrlgkGrf6U" +
                "Zxewux99ZLX61+VlOW7qdlw35/e6xUf/3i3+dC//d1Dz7BKA5HKFM1Qd0sec1zM4WB6Z0ZvzpEwnGDNb" +
                "aSETBv3pZkozIdzmpa1spG95Zalf9eG/9nRAeacEcHPfq2uABISvQgGSmhYiEUI5mDSxcjDrTaq/Kucs" +
                "WOTXMna+VSDB8UZ2jEX97c1Ks7d2c1+/KiodbbiEp/wWZqT1VTtyqUM2FsmHbS+WCwzJObZ2XGBggigy" +
                "1BAJ1w5FDI0Ty2J7nW6L/7gpgJBojJWiyhXDhxWc8koCBu7r05bW80eGwVtnHo3UkJfYHj2x+DxEh3GM" +
                "LnUpmITcOCbT2Lq8VAhDQk5izkH1XWToIAfBH0jIqjdbaW417IpFSy7YheMyJTOa9Mh7lT1+cZocf4uE" +
                "ZgAmKQlQAm59sp3/TdhIaJBcNLi8LW4FNN3s0nwsNQLnvgx/3MvME+2N+YrZ427wbTcMuOqPt0OmNwSE" +
                "QiqzAfa2CrEqfvEa7M7Yn1+D2ib7qV2IRodL33jgj6vRYD0EkzIjY/Rou32iPNzQa6wvbew/uSHgX3/5" +
                "+WP78OH/Ix9hxDuKVDol4dd5+DUNv/LfwIsQk41Y3zI4hxdPSPhqxx2SbxKLVGRpeg9NdAMifB71G1kP" +
                "IwB9+BbiTyJ+9vEDHvz9mdE1dPnwVCxq+oUwxvTOCKgbCWugQ1MUu6ti0pQxRuEpjl1ejl07qHbRjoyc" +
                "hdiD16pnHwwgi553reC3wdt/H1/iH0j1GYPgsAWs66EcbOMJmHv1bLZZQ/keUYuIQydvEB9HKFaxcTie" +
                "dvfGNBJws7Nd4KmA5BD1Eh5QanlqBRRdLe3elyKvmS/VmAV9/hXGtbIP5i95Rbx1q+DthsJxTbPqzfFM" +
                "xugyEKYvIWh/CtlN7aqX9OodcHLUbCzBKDmkSyPiukEWzdq2DGb8geqdPHNn9F+vO8J92GgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

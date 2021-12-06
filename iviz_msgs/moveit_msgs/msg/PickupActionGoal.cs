/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PickupActionGoal : IDeserializable<PickupActionGoal>, IActionGoal<PickupGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public PickupGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public PickupActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new PickupGoal();
        }
        
        /// Explicit constructor.
        public PickupActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, PickupGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal PickupActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new PickupGoal(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PickupActionGoal(ref b);
        
        public PickupActionGoal RosDeserialize(ref ReadBuffer b) => new PickupActionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9e12196da542c9a26bbc43e9655a1906";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09/W8bN5a/668gEuBst4qSJt1Fz20WSGO3SdHE2TjbryAQKA0lsR7NqMMZy8rh/vd7" +
                "nyRHkpP29uzeYa9b1JoZ8vHx8fF9k/vM2cI1ZkF/Bnba+roq/WS8DPNw/9vals9PzBz+jH0xeOWnF90K" +
                "X9KrweP/4X8GL86/PTahLXj0Z4zTXXPe2qqwTWGWrrWFba2Z1YCyny9cc690l66ETna5coWhr+1m5cII" +
                "Or5Z+GDg37mrXGPLcmO6AI3a2kzr5bKr/NS2zrR+6Xr9oaevjDUr27R+2pW2gfZ1U/gKm88au3QIHf4N" +
                "7rfOVVNnnp8cQ5squGnXekBoAxCmjbPBV3P4aAadr9pHD7HD4O6bdX0PHt0cCB8HN+3Ctoisu1o1LiCe" +
                "NhzDGJ/w5EYAG4jjYJQimEN6N4bHcGRgEEDBrerpwhwC5q827aKuAKAzl7bxdlI6BDwFCgDUA+x0cJRB" +
                "rgh0ZatawTPENMbvAVtFuDinewtYsxJnH7o5EBAarpr60hfQdLIhINPSu6o1wG2NbTYD7MVDDu5+gzSG" +
                "RtCLVgT+2hDqqYcFKMzat4tBaBuETquBzHlD3Lh3RxBrCbImLOquLOChbhzNiyYCa7leeFgQmgRuF7O2" +
                "wTTIMAEmgQz0nNabWBJIYisZDBa5uQTWWC9cZXxrYKIuINMCX7jlqjVAcOiNMANzzdrB0BG0mbgZ4mLN" +
                "1DWthZVDjHL6Cv6+0DUB8gJ6Gxwk0tnMnCsmdnoBmBXQA5iyK1vYgyHYuaNFMGHlpn7mpzxBwSCMBDpu" +
                "EG4ASC270AJmBnYdtBrp+uHK3dDSLetL51tetyS6YDgdu7XN3LXjCjgovZw3dbfaeueqYuxmMzeFNYa3" +
                "3zY2rN6+M6s6BA+7YDzHFyGDHLrVqm7aceiamZ06BTcYTOq6xPWr19DJr1auGWvbaV2WPsD6Jzgwhm1b" +
                "O124YlxPfoXxx23dTRdj2FgXNJ5AXPrKL/17p60KDwsNWxi+PwWp1DbABS3sQNsuxtP0IsN4VdoKhCTt" +
                "pN74iCsMz+MyeOw3K2vb/vXz+J36Q5cx8dpg8Eqez1bI0sHE7zW/uI1Fp3VSNaB8C9PHPYEcD3w/hUWg" +
                "PVfP4AWto2wp2tTIr7A9AQaxe08lAFPcU6bAjcOdYRczkYaoAcquQOIu6jW0ACh2BRsO1hO29dAgA9AP" +
                "105Hpo9lUcOerOpW0QW4G1JLsLOXljC2k7oDWWDu8MCrGlb0jjm0yJaeWpy9JHnE+ByhxPkRpuZQtrBG" +
                "bFGu5MOKLFvYSxAfJeivYoOqcuIrIsLu8NnYgWm02wZxmLsalHeDcwBK45tt8sEwq65lCUpQxzAPh4o4" +
                "x3CEsuuJwf1ERgBLV1rnTKCIeAMV21QglwBQ24FAlJFRM0lnhzL13lz4BOT8JlIPGKSR1Yfd8ivhuWG+" +
                "+g6n+ya+RBjjiDKO9Icw0NGzgeErkAaEwh9AYi8CkRX2Eb2HgDKgD0wWpH09Q2UOT3eA7UFVk9i5sw/W" +
                "kHgV1FdHVlYGQRgXe5pPfPXJTlcceLPyU+kI48etl4OB3wBmCdMzJNxAnTbwlwGLwoukPcwHOKI2ARrN" +
                "bTUaKCeKXgDw52L7Ja5T8oEy80syOWDbTuzEl77dIDahm06BIbdYcGjgMQB4UwMqaLkuwQKUdUcZcGde" +
                "18Ud1Oke7FMVoTzub0A6gK5DR0lR+MaxWQCbpLUXTmcLNLtAho8SB/QSaRRgiiqUvAMTaypAHaABIjjb" +
                "7oFvZ8C1URouLJIOLBHcoKVDYhzyeGSViXo62js4rJaMLqNtD76sdWToinKDjZ7GlWI6J/n1JRM6Ey8o" +
                "kFhIVg5XA4zIPk8DoYMrZ0Mz6drrAQ+FyXPBtfZliRwYAbNJw1DVh1CULS1Ft2J8YGpdU5H1TtpCtwub" +
                "T0Pgj52JECEvrS/JpMYp4I6xuAow1uha0oJOBdsiIy2Os7RXftktWXHAIgE4MOUBXQCFJmkp00AiHH71" +
                "+AF+gh2FQx8xS4KbAkDGAmBMABA6MhopS9h8Je49mFk9gb04LV00RFl3BLe0FSjKXW1AcApuPQWIuM3R" +
                "tHDF/VUX8A9q8ILlPmrrrmEBoEh/1DS5GbviI+J38KznR0ccf8VmZP6FwVaXV/hIViSbYn8K3oQErMkp" +
                "ipo2U2n4Plrv4JR6lGhJR70dGvBywVFo4Ss8WJCHJVoU+PHdO1Sm/dasz97FDZqNBQwIXOCu0Hdmx+gJ" +
                "7L7kOVzasnOsCYUrAqp/cMhRTLG+IjqTWYA7y2zNcgRTjyYrG+6MWPYuTSd72ZtW9p5nMyg6/kQO1njW" +
                "1MsxbAf4cEOLea3uIqMIn9kGa9wMPEJ04bfjFrz5NOawxbUEgPXfbeFPiDcOIx5gYFi1VdT0m4HHC5wA" +
                "Ym5I+qdG0cvf2a7B6dSN174jMyCOjg0Gf+9QjlYEN7W7rQny5iLDKvod7ZZdZmWz9aYbjYOr+GsTf72/" +
                "HfQT6XQOcaECKYNEzz7y+PRbojtqgNHgIzPSX+vbcQy3FSrMsHAzcHPQL2wzRZsMCuoyZO0PE2SdT2IO" +
                "1TCYTgFc8rvUwV1ZtJXod+lnbc9MgzXHRW9J2UMDNczUKmmjFYCAUeOJZk+WmpgUGZ5bZu0PJPceqWUb" +
                "e0ZQLsCrojfTGDNQI0AaZcEEsTB8asyKXGM7GFrwIEkA8gztLJxIss0xZsVmGeiTS7DGwHjEeBaIr2h3" +
                "+Cob7mYY4UOU2sfoRpr8U6JVYVxKDOkWp7Z3ToxHf9uOTIxJkicMzguIJdDOsSfGTJSVyCJuaHGH6NFo" +
                "xAK9HvQioH0guxP4G4D1dxW8hi6HbjQfDdkyp1a0TxALCk+A+dj4uRcuTXYkwtRFGZp29pDNdcKZB2Nm" +
                "a2oWTkcj83xmNnVn1jgh+NFInoAMD8WLtl1b17TFlV935XmMlYAsb2FxPyrZbmapc3GWhfg+EO1Sk118" +
                "Lg3ImSweqLZX9iru7QC0DGSPoWiyHPFlCmbbHmw1DMBILIbDnmSMJRyjVdwLRL4Sjdhrp2qy3/Qs6Z1e" +
                "60wf9Tv84EEUke/ea38ZX/eb3/yCbVEEqB4f9pgHbNsyqVEGUW5KQ/vgU1WF0jt5Gyqr6bP0RR3jyLcK" +
                "5m0c4h58RRMXRNp4AjbwepiG/zT7Bt7bpXsX+ToaWPpiq+We9wSd43dr5+cLUoszm8Wi2EwSqpjDwoE8" +
                "cSi5UGyAW+mXGClnvVNzkKXHvuZpCRYihRPfu6YmCRZMiZEa7doeJVODkLiN5MMOb1+7TVk5gyHSMw9l" +
                "OdJUQej09YvGVepJLTGxbWqS4sI0kjILtoq8gp3BLgmupVQfokFQSLMdJa+NMiZisNZZO3D80T+LQgU6" +
                "7DdJNOlCIMY8pCJwWZedpv32YW4GXyM7A/wfuGVqNG7cXOyb/13cdQvs1ScKUOCELFnU8kwmWNBHJ0wa" +
                "XGCMX9YgplW7gZ5dNX7pkQgSxyEN3q041yurUouFbA4tqpMObfujQVjYlWM8zhHoK4WE4jtCzeLRaGGn" +
                "3KjLh0YOAiccA657wrQ5QIrSEtTnIAyLwscwYoQ2xM21cEGjTCmegPZDQX+ihERekXke2jks9xADC7tT" +
                "fAEgAQ+G/IFJydAfnRC207ncDKNcuz6JTyb11RDIg9YceNkb2M8FOjrA+s5oMAah8HyYCYh8FOqD6fom" +
                "URGAIc0dZj4wCNqQAnowhP+B0MIqiC/M12c/Pf5Mfp+/enb6+vTxQ3l8+vP3z1+enL5+/EhfnL08ffy5" +
                "khorO9T7IZykFb4faKMCbPAqUCal1zSFb1IL7YN7GdHPO2TNjo3DABlJA7Qh1VfkuHHhrjQydZD6HMDk" +
                "G0sB/W9EfMLECdUhPf00ND8PyWv4JcfZSiajdNUcHA7BaFo3YIOvaqIyJtYpay8fgeijRNvxT48fZE8/" +
                "R1rj0y9A6hwlpr9gRSY0LjvFWiq0+KVAgfEEm30uQgJ0jS18F1KSiDlo1FvX8esnJ8//cQ745GPqIhNM" +
                "XGCu2GGqMOugVqHqEjbqkJPKmiaObX4x9soHSlmJC9KDO352+vzbZ2/MIcKWh6M0J85BZhRPc1qQ0I40" +
                "l71gDnEvHPF4aKzpODw7GYcfsnGuGwVdE6UdL5+NOa49Yz7FBUFK6SfMFfaFZrYn0Rz3DWWoOWfR+lXi" +
                "IaIp5RphkZDfu9WQKWs+FaIOtnai0C+y1NbkgbmynbrTOBEGG964iEP5rIIts6BQzA4oC8kuEOg8W81B" +
                "c3+Z7WAJM5O1Qaok5mNhb186zPu78PbdAMd4IwBAlkRYanJzFjT22FWohM0ez5KC0Nzplkil09hDMp3W" +
                "QUhIcQ3b20eMp7saA+FuEtvcyNnr9P0RM7ofr/y4Ka0hsryXpvKSlZ2sCjKKUtRzcF0stR+G/ifN9piP" +
                "w617jxbMuKahigpxu0LCMRXtTMAA7Fo3vhpjx3FsvNti89EW77db/Cva4PtCDLK42XxZmc46Cpws8RW6" +
                "8CkOwaGtwoepOUzu1tFONWosQSXup/YYHgs9bWIj4A1L/vUCsyqoDyhxzylwjLBFxsaYXd0g4BeCHBnG" +
                "CT8cDUFNNgaado3GrJmBY9ESZoWbbspDZN0RDYT+Ehb9OEuMKH0IwMuzNwCc09ZU19YtMUyueW0ND2NV" +
                "QrvGwoSEeUwuK+mwLqVhsGC7KNTMk2WNKcYb7JGS0kuX3q0zw4apwhm2LUeevItDHJQrRMB+4hqqI60d" +
                "I8YPHViPq64hU38Ut33PCqBlJGUhVVgTF3kEI2ue4+kpsMKONAPJfZEc4JeaIOUVQpYVO2u7JYwFC7QA" +
                "GeKwz9pRkSmvEHtQSiUKdnNkIrFkEmHDPAXygXKbGAjgeptvmnqZkVs5s63XtilCb00jysz0dpvFMs4i" +
                "G5IyLFdSydNR+cfSVhu2oUdklgq6EozmNp9zg6EhPrCcocOSClTmYZu3OQBBcTaz2gCNfBF3KJkWuqy2" +
                "XNsNcq95RHxdo9HM1eGI8JgG7a0njMuuZX8nMu8mFhlJH14g8O9lwBS+31JeACC5rEL2rKYnlSjbEMA1" +
                "LvJ2yBNlXV9IQgBD721vkbhmMcsXKDEI2ErrXrrAJWMoNkBmOC7WUZceNNvunkMDFoeHSdXlpcSLPEB1" +
                "H6ruYrwju51ekuSou/ki8ROaEjs7brhPiumeAEMI+GjJFdkTN7Vd2lV7LAcaRfJIqJZbjUKojGF6a2Hx" +
                "VjoR2pJkEkgXwEpUEIeFB8B1yDjDxCxTy2mMHdSFfA6EVof1XZeUBShrTtc8kEAIew80HtsVKPt7fgYZ" +
                "u/RJJLHIywozNaXOLBudZq8COmUzJRc12ysAlKl5C1EIOc9kST6SNYNYbAln3tuWKrWQ2PiFy73iptRK" +
                "rweicde14MOeFZAeM7YrcgZcOFKIWgyH/rIoOxl5P/hX/v5DHiGHDnitHNrQKEKPRrmwUFLzjLm0LC/v" +
                "yhZmRw4wnKRFrMAUAV6v8FwBetnwGiAfPhgSfpwne4AjBRWz/fXPVHOR6hexWAzbjdmXiPtce5EWDOCw" +
                "wiTLDZs+eQ/a8SU6tP0pqmuyq+dVOezwkDIXWqRSY1ZXFdaFieO9rVxyW0AUG7NdxnQ4F4wJKv0a0pGR" +
                "amJ0bJHH+BkFW3FH9UlF3XNa5ejmwrY25F9ZKYrupf9pXTQqH42fn8xj83BofoY/nw3NL/BHHfHz05fn" +
                "Z6/HvzzeepGCQ/LipxiKE4FJ69QrIPiXM+63zjKkOONsxpXrnP5MNeWaBQlTh/Fy9dKO4qmIc/oQz0RQ" +
                "uzHCo5D2jGk5K+1cNiNxKmlHiTBwyQrXmlIxDpfPUQ4bwUZepZhh4D0W7XMpvJBOyFKGz5Fg1zFGAf8A" +
                "HlSPpTP+N+gnUCvqJEWa2JHMBQkIm0MksPBYAA8HjW0w0YNzS9kKiChwECroa2tltaQUsXHVpW/qaokR" +
                "PpoMjjfm8fLpxE3NAZ7LD0wmTYUF8DWT4YS4iq2qW06AGdBAZjKHL3X0TKKUbtainf1gqMEKi6er4ncJ" +
                "uhLVB3L8pNiA3++n4wZP6c38HAv92XDMpjrWUWXOuGokn2Z5K1InspA5TTo+d7hTSxzaOHmeLR4l2k3/" +
                "Jy4cpTlTb/EX8IBIkKObuMxVNv6QbGHiCQrFUxm0NRVIOmLpyrkCtRuAjdQbPWDtsX9moDRiwVNOX6TJ" +
                "pbf7CKpE6InsYGduHDfLGCc0SPMj5MD2q7n4lEogyNUo2EGOHYdc+R/NO82hqH9BgBAXOglT8DmQuEUb" +
                "R7WlUb/TtnQVEptP8aC12FVTljVob/E2AGsJ4MZExw6X8ndieGUdI2zFn3ocRepqCYTmQKrnzODaenKP" +
                "VFXvg4l5ZTlfmoS4jFCAitjcpignAQzTqcSX7Yvsfv0KkhWsOHacYA1aN3iNv8/xJ78e82uhjwLN/W6Y" +
                "IbM66wRPZ0d0HIZBjTS0l3lAZJfwWZOiW5VoEVDcZWYO97kSKQdPufstl+iN1lKJX/T2nZn5K1eM+SBt" +
                "LLXCxaZp67aPxxOBhQDrq8ET/vBU37+g17EqP7YfS3vczGXJnucKp1fNw+B7eHrFD4AJxTblW699mFqM" +
                "sGPrc/ypbek92STis0qNZRhm+MZX7lKsyBqMlqWl4s18WivyiyiriT5SXZKdns4dM4GXsv8ou5eVyNOn" +
                "wRkN9hT7YkkSH8dkUJog6A3ZNwl+rBsw2df4X4qJkOZlIxBXdL1wLQvOnJE0hgUGPfhFrcaiyTzZZpB0" +
                "Okk4nUSED2J83PwGTDvn2uD9/hOE2fYDOToC367NKqUoioYOxK98FBFFJx+SFTM21V3xtuVSqbhtv+Nu" +
                "kqvZMBMsQWN4dLxPzr4hfy1F+zHv2AP9AttCu2wI6j4u6tl4a7DIrDs8ag41+BIXi/hfSm2IBkcD7R+3" +
                "HjNdflo47b54LuZW2CnduYCRCbwGQQ13vrNBLwUgi0unOqkL3ECHig80pHAQmNals82+xpzDtcJax8dT" +
                "sByOjzOxLGXH3arguYJ+IuTzw6RHt8L9+xkwo5SNW4DYb1GXRYgFqnxGWaItxEE8cSnkAefrt473TlPT" +
                "PQy8AbCWdxBvKeBOVMLAxYSHjbuk7A3Vrzc+4HabHuUxnskGI/Tmk942U62mUGxBIcyjYWoq52g2u03v" +
                "B2p8H8x93J6pC5+mSQ7wCpRcOgImAF5SFuAlHmMeyHkl/uKD3CmA3AY+Z8s7dLIheUDqlAnB+b2+xN65" +
                "rEHHI6oFvVlhioeNwPDCWpjtNgO6LQQDa7qMXBdD20i7anSUTi7hmCNZn74ADD60QThbhQ/pmTCkESga" +
                "iFPPTxZmyFAJN86VqnN5M1PqmTpJe0pzhJg7HJkf0VDGWmyujRYRSrOo8NS6rM/WDRyk8IZUV03BYCfV" +
                "QLE56Ue0/TbCjUg9ns32se7ewfNIp+DfOzpTi8fDCE62a0iPY72K3FsReSBdYJGIQxaZIs21NFN0q3gF" +
                "R4OdHHA8wEf8s3tybPfg2Gb3eNgtCJRdtQOzer1znoplAHLPr9lR/chihZs3jsuJ8DhAUS8p6TGr0ZZT" +
                "cc1RTjnNnMZjRt7OE9g2Fi5T8jJsArgBWRVT4FxntDe5z9y1af+TAVjL6BcglEhIYY4K43O9rI0lq9c2" +
                "Kiwscdg/Xp98QzLtESrwwytgVvjXrjVk11JClD5KOD+/eyfHjgnJhuwwHWDtfx/clRYKDTY0FUqAMMJr" +
                "VGDCPRz+X4zdrhhb47mdxe8WY9r8/5IYu06K5ceQr/EHqZQpOn9bjdawoNgA/259+5HIBB+ZXrdztCli" +
                "rZTcSgklsRKTB+t658RW2Dr/NIjHtPKTdVnpjp4ouqVJErVlgiqZQjKw+mcuJ019wXmdmiQGFlBazojY" +
                "ak45etxtwCU6SWmSnqXd7cyO+WbP+nGNxtbh3+AAedqzOEGKFf+uKfLlBfGRzeXb8HOv8dFErm29jWnC" +
                "6OXIdUMUmkkHgEjWoEO4rxhs95SxVCJq8TYWqVCpuuYpdnCTc0KZC4rNaMC7z2caIqflovA9gqxqcV5H" +
                "voipQwzcqON5dx+8mNoiDRenEQnBAxSDbULJTSfq2rATxPe7sEbOXUV1qtF1z0L3dF8E5hU3MSx8LyLG" +
                "aFCsX68/itHt1CFJ1t59XBQS1pNZjBVpXjohXOyUcemtQDF7JgeE6cjlkm590qlgmpZPS0rmPt7ig4l7" +
                "irNy9DseMz6KZT40Rv+KlHgPioSk0oUudI2dxDc+duVQ4RC77TuH1r0a6q3FGGIe4qKq19WfkMfb3YtP" +
                "RFUOU6lWDHyo3cvHM/bWiPoi1nowAQ+Je/SI+AsY+3l7tHszla4znmYgpkBvX+v2iDpx90gEEQ2wOVez" +
                "Sj0svX+DEPjwgxYbakRJD/e4nasTM4SlusI3eoilcfvLU7LAlxLg/NozRL/3UNDvPePzgo/bXHsO56NH" +
                "a+4aPSNFfgLGmhyW6oo0i7Pj4iMNoscin3gGgevCbJXXJ+EQvTNCGNClidBI10wSvyXsnhRS4ZYEwG4e" +
                "eaT5JmkEPHrp6y6AreiuPF78qPklSqpQicZkA7bOk5MTPICBfiHV/+VAYtFNlj016Hw0aIMe4l1xm1au" +
                "JqDQaDtdCITEE7444pFen744++EUK/1hTissbSEvL958wH6hCFZCOmiU58NzjZlr6qTzhFVIk3z16vTl" +
                "CR5uISGcxtw/HI0y5Nwicb6eEsP5U52Orpua+noOmlKPZMajG+npLB3SCkjbv1eKg2lUksQoEm0eIYJn" +
                "K9fEs+wTpzdJacNaP9+UUPy4UBnc/cP/mLOvvzt9+gZvu/3jneUfJM7TD+cB9AgYXgKMCk8EGYgxKmAD" +
                "Mz84dj7RYoQl5PL5OYeWo3slZxkteu19o+LCxdhlPsIxveH+yUFvlKHwdE9liokKe4AS60QmOSqiYCm+" +
                "8N352cv7mO6VoMPPT158bxgAOOmRhUHMxg2Q3a2Aglqpsn08TBXKyJyS1eCrPYtO+ygWcpb+wh2bO/9x" +
                "gBQ+OD54ipbNydcHQ3PQ1HULbxZtuzq+fx9PNJZA7fbgP+/wFDltXtUc8ag0b0mrJ9YNXS2UqMCnyQ6g" +
                "k+dqoAvn5ObNWQlblSsGRz192ePXKd1wi0TUuyJOvmbeiFeL4b6XkTlWgMzVNQ2JOI4c0QXLGEmSyVLI" +
                "m8Acm0gAeockgHfbJDj+y79/8Tm3QNXLRVLQbhfjAxnp/O/fG1i24DDOH9epN/D5b+UzbcGwaShzsJ6H" +
                "R3/lN5hVOTZ/+fzRQ3qE1g028GjmSgtQ+2tweLdeo4WCE9EBNEHEX5d10ZX4nYo42np1oAwNrH3zh5JI" +
                "d+6NYHKEzw1FBycdHRW5vTKfoon+qZm+h/8UVP1GFSTHj2Fx3OztA7yXbBIfP8PHaXx8iI9FfHz0Lt0Y" +
                "9vk7enfLsY2tO2lSBCAPm5IC37mKhq20Ubwc+66aFDstpwtfavIfG25H81ImT++pRjDQ6itrFo2bPb4j" +
                "W2LtL/yoqcOobub329mdv7Wzr+7bvwEbTi/w4lbscw4OPTruRT3tlnF1Z1IFnwv8nViWcGEfXcPeSyx4" +
                "13OO2IjfDt6kAHOMGd2G+7+3OELEmR5DBAqAfREvreOKK2oX3UtqIoE7dTpAxl/6Aj17/OpT52tLNe6a" +
                "ALsEj3eHzZLN9aFc4N27SS8fbXsGp/gtYsSHJfccwN8ucKB4uN7DibdElKGOJRZ5ZRYTg+uzYnw4UWiU" +
                "FVztzlNLFEE3N+mKmpX1VOHEFD6sPgODj+p1NdOICpV1fLxu8hrMU+1ePBa0O3oWKtAKeRsdYZ4KIEHp" +
                "bUIjs6D1Kg2yMWF2ZABPalgh6EBhlofknPWwpeacCRcqytkeuaqdUJJaMg4oVebJy5PcvoyMJgDGOQtg" +
                "dnznk6787e8h4kCM4/fLBNI6gGvCV+KitKTCuELnoI+3gHZW0jS4m11U7a6N3Gk5VLotK4+8xRt3tC7q" +
                "dqZAdVa/dwJYh/XRCUix1s2jn9Vg9QMuniUDJ8nW6sJuuNRrN0yjG163O7UpYiypbl5/+/UT+XDT/2co" +
                "cbx4V14Tf83jr0n8ZW+9mpJq17hurl/UtB3EffvO7C1PepNV5pHkzG+sSwGXBB+N54H0kJXnhx9B2NFl" +
                "pvLxxvzoD4xN0cVHJ1RWiJWxYHRx8BU0CxV2Q/vGuf2BxfxwZk3t9oblJBXM9s+eSJMcZol1u5wPEIB4" +
                "J+6+CfwZRPtvE4tK2qhMCs+agM6XroeUU8UM0v16Ou1WoGSPUGFQSSu9sdV0o6Q4HE3a+yM0BnypVWEM" +
                "iAISpcUTKMm8rPWEoHTvS47Xjg7GI4NiyfPyKJ6uxmOCDk0/7lbVRbpsmE8zdnrGRqYBXp0Hyfo+ukHc" +
                "la8o5PICulxktOCkgeWi8XXjW2WcgLXcX6Aax90y+C8DRV2n8GkAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

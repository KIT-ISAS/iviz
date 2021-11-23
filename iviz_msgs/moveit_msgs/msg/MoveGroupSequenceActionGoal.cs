/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupSequenceActionGoal : IDeserializable<MoveGroupSequenceActionGoal>, IActionGoal<MoveGroupSequenceGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public MoveGroupSequenceGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public MoveGroupSequenceActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new MoveGroupSequenceGoal();
        }
        
        /// Explicit constructor.
        public MoveGroupSequenceActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, MoveGroupSequenceGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal MoveGroupSequenceActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new MoveGroupSequenceGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupSequenceActionGoal(ref b);
        
        MoveGroupSequenceActionGoal IDeserializable<MoveGroupSequenceActionGoal>.RosDeserialize(ref Buffer b) => new MoveGroupSequenceActionGoal(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "33db6638fb44f932dc55788fa9d72325";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09a5PbNpLf9StYcdXNaKPIEzu7lZtdb5XjmcROxY/1ePNyuVQQCUnMUIRCkCMrV/ff" +
                "r58ASGni5G5He1d72dSOSAINoNHvbiBPrSlsk63oz8jkbenqqpzP1n7p73/lTPXsIlvCn1lZjJ67G/tV" +
                "47rNlf25s3Vu8Tt9HT36B/8zen711Xnm24In8pSndy+7ak1dmKbI1rY1hWlNtnAw+3K5ss0nlb2xFXQy" +
                "640tMvra7jbWT6Hjm1XpM/h3aWvbmKraZZ2HRq3Lcrded3WZm9Zmbbm2vf7Qs6wzk21M05Z5V5kG2rum" +
                "KGtsvmjM2iJ0+NcLTrJnF+fQpvY279oSJrQDCHljjS/rJXzMRl1Ztw8fYIfRvTdb9wk82iXsQRg8a1em" +
                "xcna95vGepyn8ecwxh94cVOADcixMErhs1N6N4NHP85gEJiC3bh8lZ3CzF/t2pWrAaDNbkxTmnllEXAO" +
                "GACoJ9jpZJxArgl0bWqn4BliHOO3gK0DXFzTJyvYswpX77slIBAabhp3UxbQdL4jIHlV2rrNgPAa0+xG" +
                "2IuHHN37EnEMjaAX7Qj8Nd67vIQNKLJt2a5Gvm0QOu0G0ukdUeNB5iDSkslmfuW6qoAH11haFy0E9nK7" +
                "KmFDaBHILtnW+KxBgvGwCCSgZ7TfRJKAElPLYLDJzQ2QxnZl66xsM1io9Ui0QBd2vWkzQDj0RpieqWZr" +
                "YegAOpvbBc7FZLltWgM7hzNK8SvzLwvdE0AvTG+HgwQ8Zwtri7nJr2FmBfQAouyqFnjQe7O0tAmZ39i8" +
                "XJQ5L1Bm4KcCHRmEG8Ck1p1vYWYZcB20mur+4c7d0datQXKVLe/bQSk2AuGGCNd3r3n+ug6Y2KvK1DVM" +
                "8+UG2wEJy/PM8YujzPzAFAGz35QwU7eAtkQzOjOdPO+PyYBJ62LWmKLsPIlNa/IVER9zMuzqBuUWgsJn" +
                "ggPEBzQmrzzuNTzB3tn3KN+I8YGWXW2DAJwOUPmsteu374B67dofZ3+HowuN43oCTnD9xI84r8iBYd0p" +
                "/kJbb5drlFOCIF3xBFFQLpBBif1M5l1VtiDJZEsUJUhDCWUR8zneF8Bpu7W2jnqEEDahgWRj18CWgHq/" +
                "dg7eFiAucJbA22XDqklH4/UyXNlvmTI2hS3TwWhVwNP0rbbvW9KG8GKCIgeWcgZMboDaa5kmDDgdLSpn" +
                "2j991iOo4+1sgkTV6yglS0AZaCwUcryNhV2UdUmIww00YTtBskWkAohA54IH17WbrsXdVJGIG/XKoHpp" +
                "beOFHoAXXHPtNya3LHpTnhFNgC1A/XsAMvpOWyeQAoTZJrwcsZkD9gZqzBbFb7cB1Qd2TPZsEQT5Tw7s" +
                "Bq8DzWnxOE5jUdjCdDbOlyyqkFpwxob5PO+ahqi4tkxfwNaxMQMEEEhpts2QDEav3dy1VzQXj1Ob0bxU" +
                "eUBnX6IkINFPnyKS1q4AowxVFrIfvJ1mlyB3MltZ4SU0E6ChaRqgb9o1UkKiR5ZI0TQMvUB6zVclWHo4" +
                "x1KoGibeNoYQwnud2GsMAsDD3E1belRBoyexB8gmMnATILiwF06QD6g09Q4WCV9AFjrYFdppA0K1ZIwC" +
                "nRRdjpotSmCWlDelq8iwJCyn8zxl3ttsKlHpKGZpENiU2rXZT6glQRPzu3E6ZRp8OOE3A0SISAdFjYQE" +
                "b3+yORgmO1a/jIrd6E14n8KPrQ+NUqNFJgIl1TlIZA6NGCJUEBnUEPlTVf8ESRP32JD90O+rqgWtILUJ" +
                "5BtZdfuDL1GN44MwAwBLLC2FCz/dBox+xIPCpZ4zhBXgdus5NEbIaIdHEKQWHHPYGgQDGGzZ1co1LcoS" +
                "EPSdiBGdfgPWd0NmHdv5AHgWjAXTtmi3BVSuzfty3a0zs3adKJZybQ9hFomlqtyWnRZlJrLGxVQfB8Es" +
                "DeOwZPWhXAELnUxlg5vLzML2C7lEIDu6vBQC16kRbjNwZMAWbJFW2QY0eQ4sjFgFAplm2WOZ3I2pOmwE" +
                "7AZTOz2bfPoOvr65DeDuALjEVBQGa1D+iChByGukauQc8SBK25D5AjQLLheAkwXCyECJoEBQAGJHkO0o" +
                "JBFo2eBcS9SQ9dKiX7PGjTN1W+0mtP3k/OVVh0bx3JI4tqQ+zqZnY9bMPA7RuA2aRembUIF7+un0jGCB" +
                "eGdEn5ZTO53chhEA2P+SImcc9S80mmmnmeetnfGMem3S7sN2x9DbBzSfam51IYLmNoRIwFlUiCgQFh2I" +
                "/prc7JbkWbdh7xP4D/jlFKxb936M5KICQImmzzc4rTQcIKDBrduxkxWMP2SNtZuXlSVlQhaUaCoGDDbf" +
                "1lbVFPnqgrQWU0QjAqqxC1CdaMmpKoQpwkKbGtf/tBd3UWlQgrCAXedGKuWg24RteCFxpTOeh1dbAdSw" +
                "HS2tA6yB3CbMf0tC/CECnjHQoeD5xw8FBKdD3Q1pHRxX9xSkL0zdojYwwFHMnSAqwV/OiAanWTD2adfR" +
                "vqVlhp6oocrGkgNOlkdDTvQELcLCWTSG0fJcm2u0kNBhQu0NihxkGWrO2lcsxuA1dDm10+V0wtRFrdho" +
                "R7cdY1HgMTflEsQQ9YSB1qGzyWRxIGsWD1jB0Jx5MNgX9MVdK3IBRd3OdWAzwBrgRyMhMNJdOi/y2Frn" +
                "Jkj6AqKP0FckcpQxgSdboFGgcpUo78OvXfj1yxGkSLRBbxUeZR3xZ+Yg6/u02+IeguBlmy2ax+i5ebUh" +
                "0AsAAXntR7i3ruHBv8aPbABTu2gAf+3E2gI7ER20lbkJ+slmFy+/ZMszaDSyp1PQz7EttEuGoO6zwi1m" +
                "g8Eety1YvwAld1VVelynm6OlBurL6DfYcA8bSqvIXKJAxyPt/0S7v6TeYAJr71mAPBPIOO6XlVkCdguM" +
                "kiL5AjGLT4GaLwc6jlYSqTlgpZbClsRMi8WeUKEZsmNC/ROZvHa+xaipqmMO8WoMkVwFXercFWiRnOp8" +
                "oCGazhjVraxpDjWGgWDDjZDW+TnIKnt+njg4EjIjn4vCUhJAaROSG4/mzqEzMsPF3ZWgO0yACaZMYAEi" +
                "v5WrCh+YHqyWvCnnrJXYlaGFi34FuQLuMPFO4yhsywyA8jEGNbkTBorEFDltLOpcfN+A31N6ZLd8jLNh" +
                "1w0tMoxVZ3/osZlqGIViCjJzxpPYNNo/w6b3PTW+78fEnrGLXcBetSFezt5UsBoFwIs19n4BhhMu7DKu" +
                "BfU/hSCR2gqQ/MyhYFGiPCDngBHBWjowsT8Y29XxxP+VQCyQV4EeA5jHe204uYCxM91GEh7MRto1k8iC" +
                "pzB4SfmGAwLQl55tFBOFD3nU4E3iCBrvI2Omj2GaDKnF1MtGYwk1HHWS9rB2pDD1GKbZd6jUUL+xvhER" +
                "SquoHQCU/RkE7BHWekK6Kgc3Bjj1xqbbyZEA9JN2Qo2IPV4N722ydgmArBQG4cmXv4CshyVjpJLgJFxD" +
                "MR4y+DjMHWggxrsjcsjd1EmjtYSGAdqhtIOgFoeWHDuYIFGJfkRBwqMyQvJKCT55xRg4hkDZVzuwqtdq" +
                "AxnlWZYBSD2yuYSLQGKFXYJlRYSHJlbhYF/R/XJocqu4BpR0eds1JEvieEzIbIoB6sGOL5iTjfInZt78" +
                "DgyQNWMe49MFB0uiucR9lraN/I/+ZAjUXINQIiGV5SuwEqbZl8gK780a5j/BUBFY/KZRYWGIwv7++uJL" +
                "kmkPUYGfglEMLuHObDGlxWFCMID5I1IwxcNjqi6dnZHILVAFQOG+yNG972RNYguFBgx9YxtK4GDWBRbc" +
                "m8P/i7HjirEt+nGr3yzGtPn/JTF2mxRjAxS7+4HD90ZJGFoFct5rtIUNxQb4d/DtO0ITfGR8HcddDLM+" +
                "4DASOwSxEnIkW7eX8/cDn3I0Uvc38f9Gf+sMRnxQAKiXdpxFxoEPucXAq00ZZHxvIfj0c5w14uGDnp/+" +
                "2h5pA4mSZFkqdX00HvvrmTfuGlPnNfniHn0i9AtQDpt6SfkBiu5MwwZKk/gs7Y6zOuaJA7sGW8HbExc3" +
                "AX2OUbKWLN4WBdVvXCIBi4/sChwjEniL/ykye/A2RNSDByeJ7EX5Xt0Vjk+VtOprDe7jb43tEx57eUED" +
                "pgimqVZmw8FsimbFhNXe3FiRpe41NqMB7z1baKUGbReulUDWThzzaVlIdHjCiXRxqu8dgqcuNmvvsIwk" +
                "tYADYPaqjygGqqE9cfAoiBEzk2E8DRhgWCKJjLauI8Kb70L8+pMwsZlmckzVgIbYJcnGpEPUGgRsxmEU" +
                "tPFi2pJnRVYFGF45O2u9fB3sBtmKIevOE+YQ3RphhKUAaImugVDDZDeYzxzyyyvHyfHGdcQIAkXC9jpG" +
                "bXPUv82ORmtsxWVaEueVgXH7sKJHYjdJbix65jGNBsjA2c1kCN2UrS2Xq2CxDDZjgonM69pt6yhNqf0x" +
                "eHKfFx+LGTDhgqQFhVAlqKM2PfHM4Sg2ULwsUxB4StRDsGD3sP7mWTtOq364n+7zbmOZKDCSMTdckELY" +
                "CdzDf2doXC45x89r4SW8QQgIJqZFNVom0pbSWPvWnPIsdysbkQzIKYeC4mlQTxFw5TClpMPkGCJZl5iU" +
                "8iOSODxPavVKP6F3GJsNo7G+933GiIeRnlu/6kPFN9B2zR8OwsFvEcQXyByaZsU4mkXlL9IsJm2zeRer" +
                "G6z6KGxExDIU2LGiKNmkJsSN07lh1QYthEa6ZZH4Lc7ucVH4lIwE6yFDQ3FDinonjYBGb0rXebCD7fsS" +
                "a+Aobs/KlATOdDTfgR33+OLi0dmIfF7kht5Ii8atOSBR35SNq6lQAR2rBu3rUwu+2Q5EE7EChX1bYGY/" +
                "oImyGPNIry+fv/z28tGntKbNBuUUerB1WBf5vCJYadI+FBb86lo1GcGddJ2wC3GRr15dvrh49ECEcBzz" +
                "8HA0ygSk4lYoX7aasiKnlHGXfVM3Rkv5Krto2UUZc1mGdxXiClCrEiNK08L6EstUaIqEm4c4wZcbzf+y" +
                "xoVHNEC1odPPdyUUPyxURvd+9z/Zyy++vnzyBgt/f39n+QeR8+TXcxwkNMltXpDCE0EGYgwDFejCeMuO" +
                "dZJXbd2Sw+bBdeQQLtAJRsl7RsW1DXHZdIRzesP9Y/ChUYJagsSqs2Kuwh6gKMBink5FFCzFTr6+evni" +
                "PtZaSEDlh8fPv8kYwDR7HEgYxGxggCQXh4JasRKDRqzUVaFMs0uyGsr6wKYTH5FD79w12CvX9jz76D9O" +
                "EMMn5ydP0LK5+OJkkp00zrXwZtW2m/P798H9MBVguz35z494iQ1ZTLXjaE4tkpF3T6wb3JwEC2g5lu0J" +
                "dMJqNuCCa2ulGnxRAavOy6rUIIA9RK85FftS9ZTkFi++YNogIDkVQBqNX3EcBIlLKsIkKka15hglk8VS" +
                "OJ/AnGcBAfQOUQDvhig4/+O/f/4Zt0DVy6lUaLc/4xMZ6epv32Swbd5iDiPsU2/gq5+rp9qCYdNQ2cl2" +
                "6R/+id9gxug8++NnDx/QI7RusEGJZq60ALW/BWd+8BotFFyIDqDJL/66dkVX4XdKn7Zuc6IEDaR9V7Ha" +
                "26yFWG5A2Xq/QUqbZPkOTGsy2nIMlEm0Sb2cxob0DJCVRpnAwpmrCQDAUOCjSidOZMP5bAL/m47ozMLn" +
                "2Rcvvwc1xr+vXj29fH0JqoUfn/zwzbMXF5evQZTLi5cvLh99ptyu8om0DM5JWrGVpiKhBEXrNScbm8bw" +
                "eGwR6hisIQWZdkianXPgj4rYMJWo5YXYFtH1XiXVSexzwsptJKSJX2HhNFX2Hr6fZD9wLPfHdM5Giv4r" +
                "Wy/bEGwcyiBPNfax3mIacTv7HiyS+PRDwDU+/YhaPJkS419mRXFA3HYUm/C3DscJJiJUSBRLUaBWBIub" +
                "wxQ07e3r7PXji2d/v0ILKRlTN5lg4gbz+RrGCpMOhR+ouELNQ4rEy1A/ZgYMDq4H46qKHtzZ08tnXz19" +
                "k50ibHkYxzVx6jbBeFzTqudeKS9kp8gLYx4P5ZyOw6uTcfghGee2UbDaoldNrc7J4TGfuJqDAfoJ+kc7" +
                "f8iTcxvquLnctS03kYYIp9gfnU2uf5pIjuNjQepowImCv0BSg8WjPRo5da9xRAw2vBsRt+8EkPPZ7CWh" +
                "0Bgdxr5ot9A84O+cZEZsJ8HNaTbiapaQfEtCskm7Yy2wrEPksheSSpPkRiqNe8v9QAj27lUQupaqeJKp" +
                "ojuJAqKSEyigFE29BAviz4mElYpQqoej2tVQeQNrxDQXGDv+7bsRjvFGAFBOQWCNRHhI4E57qO91bbUY" +
                "kGZzAOeUauVOR0KVLuMAynRZJz5Oik8Evn3I87TvZxQHPMpsyS8/mPnlzKidiH8f/f8QJDDvs48x/Pdx" +
                "lv8C/1dkjzLyqE12/ggI3C7enr3DiGJ4/BQf8/D4AB+L8PjwXUg1vP3sHb27KwR8IIY3iGsdTIYNuiih" +
                "cbX+P2neKmEoOZxU/LNEiXlfKYgOjPh2ktR3w0Ovtvsd7pLrt+YyhXchZp6MxaqMj4fx2UayQ0NYpF8f" +
                "HlKdmCRu0HXp5yxJRgxWOYWljw5UVvj90gqs94ove8vaL7ooOg0/gO6fYQxoRgWvxwnCxlMWt5dGV7cc" +
                "9EvOaCjC05MgGqQJx1/kkAzlyPVoQ4jnU1G8ZoC5fICJPc4xsELvcMgr2YdeO92cftOXUff2Wic6ud/h" +
                "29KL99trfxNe95vf/YYNMMIRGn44oNJDrmrOAQoKb2tKhFwwxXcUMar26LP01aNPaGC/DUN8Al+Rruvc" +
                "zuZA+NtJHP7j5JuZwwLeBbthWJE0bHngPUGnOKYkKOJhkpiviTuRnRa2di0pf8yJg9epVZ0c3+AC0JR8" +
                "sycVWHVkJ/xiGyfHD8EK8LEgdDzMktz9du/T9q1sintWDJR+2I641P2aC3ZouVqZsoFDbJIFhWG7Q4lO" +
                "DgktFpj9OxUiJChUsDCOoto0S9uKSnBJu60loZwegLilxJ9BzAjEjIfUCch5jFtnno005/Att4yNZnxQ" +
                "738fdR2BvPpIiREeowiFDX14waiR9K6/NdG0bxjrrjgN8J5SjLBDP2j8W/NSo3DYE9V3WjA2cGoxDAiq" +
                "/zcnsp7FzBFfbaDQJpLKCmmIYERg/qQIqfMQwwqngZaw3XQqe3+J/STZ7YuSoT+4oDSjdveEclBx/h5R" +
                "1C/++bA4klxNr5dEJRJJFXeGCCuWEI1uK0zqu9//Q9GnlXwUCPmE3KvMNg2KDFVdST4zHpOc0/FNO3s/" +
                "w46z0Hi/xe6DLX4ZtvhXlGOHzLSQgw/rjefuqDqzJGpNbDku8C1Kn3PSkfXNeK9SJFy/Q9RP7akGsReb" +
                "MwHwjuNoW06JlRtKlnKiyOERrEDYXCaNgJ/L5Ei4xPnhaAgKfF9o2jXqHzMBh6yUm3PlM8vg2B2ngdBf" +
                "wKafJwEhxQ8BePHyDQDnsq81zAAP6yUHQ2G1LdN1vMtBZx6KlBV1eKC8YbBlG6Am1oCe3InlsYiLm9Ju" +
                "kzAxYwWIet8YIgl9SrVUgAMzr3ZS0DrWA91E+L7Dyt2uIXE5DWzfi6nSNpIGi1cbKI2gd1LyVQPROGVj" +
                "RG+hiPI8Bfhn9Sz1IPGNRq2HLekMV77CI8F6wjPsEGshxZKe8+xfNhBE2IRrlTmjdkB1XMk1U8GY8iQy" +
                "vtTSA0a3UmbrtqaRcgjd0zBlJnozJLGEsqycjmkcUI+JRRtrvNmAMhKDsng6rchtPuMGEz79ajgyCcKM" +
                "LAw/pG25bQF9lWyzAxyVReBQPg8t22qqLRYQQMOHRNcOUxB8Yh4nPPN68UbYz3C4t8+JTLuRRDQ9yhsE" +
                "NpIMGM93DpQXAIhqX9BOooFrtOLlMPGQQmgX8sR8YhSrr9veJqHMAhzHA6WKDAK20WKbznd0MJxOrzWd" +
                "5ZqbpKBmn+cwUMAJXdBAN2JzlwAV8Hg7uckRECW3yxuSHK5briI9oSmxx3GTQ1JMeQILYjK/5tuo5jY3" +
                "XeSqA5YDjXKwokpkDONb7xUZ3Ut5CduSZBJI10BKeGSQq3YMUeskEktu+Jzr3tQFfViOA2byylChCtYN" +
                "0ghnYkxyLobGY7siOegmJ2sxNE2fRBKLvKwx0V/pypLRafUqoANh6GHlxUEBoETNLERu+LByXZWEWmxx" +
                "zszbhgpqENn4ZUKzDEypFaZnonG3TubDFjCgHstMN+THWj9WiJiEqWyLpSOq7GTkw+Bflfcf8AgpdJgX" +
                "VkWxCB1PU2ERzhTSirG0SBYrajDZmD05IHcDBS1iBKYIcLfhwnDbwGusJD2b0Pz4IPVZqNpt9/Y/Uc3F" +
                "4DYGaDfjyH/gc+1FWtCDbwOLrHZs+qQ9iOMr9H36S9REwr6eV+WwR0NKXGiRSuWHq2uq+uU05lC5pLaA" +
                "KDYmu4TocC3oV4U7OUhHBqyJ0TFAD5WC6aUZPVRR9xRX6XRTYetCVfSAYdRC0chGMH6+zx5lDybZD/Dn" +
                "00n2I6UlJLl9+eLq5evZj48GL2KqXV58HwobRGDSPoWx/2WM+1sVCSEAn1WO62UXw6MyTIx6teXAySQA" +
                "rIvuZv6pc3LwqiMlPTqrBRTVu0HJhbh4cp4w5joGt0gdOQI9uJAwFtEsFr2ZK3v0a0mzU3Wax+Fqwyv6" +
                "EC4NonZywP0el54CaS/oKoB43w0ZK5Ke5QNHjW27pqaDRsktgXyV0uCmQRR5wV2Su5GkE3J4xsfssesM" +
                "S1x+xzyoLEBX/G/QT6DWy8GNMGS9SbVTdkoHOJjlPTic6PuAx+StXYtkonLbmuyltBITYd4YEO10ISKW" +
                "ZYoVkFQQ82JwvBmPly4nyFgOFd/8ymLiUlgf3rIYzvGoFonXW+kVVH/W0RMBT2W84PacTTR2lFxkFCuK" +
                "COuje8z6xa4GKz+nqr56US7x0APb8clSexdfPWOdQOpikbYi7S4bmeKk43O1gtJ4dY1vw+L12jcs5B9m" +
                "tCIVTuOaqbe4b3iTm5dbhDO5J0/Hn5BrQjRBdWYNnS7mGmkk6dpaOhkLYAP2pmeszA+vDHS4rKSPX8TJ" +
                "TWkOIVSR0NOg3izsLDDLDBc0iuujyYEp7jiJSlk98vwKjleEjlo3Hm7xkwJBdffk2ky9/Iy2LbJoYylH" +
                "GswtYktbI7L5DjU03rs6Z1mD5i+zAVXjh8jfPpXydyJ4JZ1MyIo/7V2lNrxCDbw6U5K3qpbTIZgH70+T" +
                "EQrQ2LtjivIruQlF77jri+x+ShbRCkZ1crlNeksjve7f0phenJfep9a/wwWzPjIOw6BGGmlNHFI5B4t8" +
                "V3SbSm+GaRfZ6SHPLqaVKB1121lisS5Am9Ihvxnf6ZwcLx7do2Ur28eTPXyAfiTX0IVDTc/5XL3eiBdv" +
                "t5H2yMwAjwIBG1xevfSjb+DpFT/ATCjULN967fEqNcut8WI9q23pfe+6Hqkmnuzf2zPJ7I0Y9Q6MirXZ" +
                "oIhJl7UhN5VKdtFldRW5TfHwEiN4LfzHhkzviP96OuIjDE+wL2bZuQybQWmauTdk3yT4zjV8l2lVHOky" +
                "oGPcrvPrFKjuWzxhnF45QU76nqXLRu003EN+T4l/r2W+Kislbmw4zODEW5D0SnAEA63+YrIV2NqPPpKS" +
                "+215XU4b56euWd5vFx/9tV385b75K5Byfg2A6DqEK2vpYHDh8m4dQjELCbqlZsxeEkgkQX+62b0kABoP" +
                "+1Ejfjt6Ey/nCOftj3G8+CDzi/zT4hnAQLOLxUVsUVC7UOdFTaTOS3ojvd+UBRYg4tcydr5VFIGb/XNn" +
                "sDbf79acpZXL9foVT+lowxVc4rcwI66dOpAzHTIwyTzYc1stJpTYr7wLIiS1PBgZbH+EuzUihqaJQbG/" +
                "TjXBf+5sUyY2WEkanDF8WoMLXlN4QD17NKH5DJFg8NaZR9s0ZCH2R08MPQ3ImVAJzkuBSdDVYDSNvctF" +
                "iTAowERWHCi9VQYd6Bj3AwpQ9WZLzaUUnbEoqQS5FZ+mJLYSH1ivs8cvLtLza4HQBMAsJQGUfXufdOeP" +
                "z0NEgWjr969Yi/sACi6/Fr+KDb9C16CPR5h2orJH98jUUb/3tpsBVN3H2xvTk/2hSEr1/nGWQHbEb12A" +
                "XOX66wsQY+Tup5/YGFqNFA7q4VI4HL/VMo4dmzL7x8DDrbnC7tSmCGfVXfP6qy8ey4e7/u/OhPEYneh8" +
                "hF/L8Gsefpmjewtkm7Fd2Dcsh5dEUIDqwNWObxLLkyRnelVMNPcjfDycN5IesvP88B0IOwroycc7O6f7" +
                "K2NTWPLhBZnN6PmB0cWXO4BmocAFtG+sPVzrkuaCHbU7mKSSa7TY/jmQapPYefBL+diCAMTa5UML+Gcg" +
                "7b+NLPIAqJoMQ9v4nxbhrqd0Dg2Prdx3ed5tQMmOUWGQy0ZvTJ3vFBWn03l7f4rGQFnpjZoMiA48VwYD" +
                "3tG8dJqQlO59yfHa6lXs5NKvx6GYA7OSeFW7dKtdEYvCOXnaaUhfluHBnAHJ+kvIWXJXvjKXr2ajk2HT" +
                "FV9KYjgosm3KVgnHY6zic1TjyC2j/wI8KEdrZmsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PickupActionGoal")]
    public sealed class PickupActionGoal : IDeserializable<PickupActionGoal>, IActionGoal<PickupGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public PickupGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PickupActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new PickupGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PickupActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, PickupGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PickupActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new PickupGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PickupActionGoal(ref b);
        }
        
        PickupActionGoal IDeserializable<PickupActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new PickupActionGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9e12196da542c9a26bbc43e9655a1906";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1dbY/bRpL+LsD/gbCBm5nNWHbs7CI3WS/geCaJF/HLerx5hSFQIiVxRyIVkhpZPtx/" +
                "v+ep6upuUho7uT0rd9hzDM+Q7K7urq73qu58k6dZXidz+TFIJ21RlYtiPFo2s+be11W6eHqezPBjVGSD" +
                "l8Xkar3iS3l1a/Dof/jPrcGzy6/PkqbNdPxvZFa3BneSyzYts7TOkmXeplnapsm0wqyL2Tyv7y7y63yB" +
                "XulylWeJfG23q7wZouPredEk+DvLy7xOF4ttsm7QqK2SSbVcrstikrZ50hbLvNMfPYsySZNVWrfFZL1I" +
                "a7Sv6qwo2Xxap8uc0PG3yX9Z5+UkT56en6FN2eSTdVtgQltAmNR52hTlDB+Twboo24cP2GFw5/WmuovH" +
                "fAbc+8GTdp62nGz+dlXnDeeZNmcY4w+6uCFgAzs5Rsma5FjejfDYnCQYBFPIV9Vknhxj5i+37bwqATBP" +
                "rtO6SMeLnIAnwACgHrHT0UkEmdM+S8q0rAy8Qgxj/BqwpYfLNd2dY88WXH2zngGBaLiqq+siQ9PxVoBM" +
                "FkVetgkIrk7r7YC9dMjBna+IYzRCL9kR/EybppoU2IAs2RTtfNC0NaHLbpA+PxpB7mULkuVrrEG3rplX" +
                "60WGh6rmrJWkEmznZl5gT2QdZJpkkzZJTZppsA7S0FPZcqFKYCUt3WjY5/oa1LGZ52VStAnWmjekW5BG" +
                "vly1CXCO3oTZKOFscgztQSfjHCyCKSSTvG5TbB5nFKPYzb/IbFuAYUwPO1MFVCfTPM/G6eQKM8vQA3S5" +
                "XrRgw6ZJZ7nsQ9Ks8kkxLSa6QDeDZuigk0e0ASa1XDctZpaA8dBqaFuIVh9v95bVdV60unVBhN0a4I8b" +
                "vk3rWd6OStBReDmrq/Wq9y4vs1E+neYTbDPefl2nzernN8mqapoCvDCa8UUTQW7Wq1VVt6NmXU/TSW7g" +
                "BoNxVS24hdUGnYrVKq9H1nZSLRZFAxIIcDBG2rbpZJ5no2r8D4w/aqv1ZD4Ce13JeA7isiiLZfEut1ZZ" +
                "gb0GI+P7E8imtgYhtODDtJ1jHP8imvFqkZYQleSn7vicK4bXcRU8+00XVdr+6TP/Xfqjy0jIbTB46Z5f" +
                "rEjVGNq+V/riMPsuO6UcC2Yz6gUGyBmke1D/BPsgnFdN8UK20jGWsDapFkwKGEL0Hd0AurhrdEH20c7g" +
                "ZcXTKVXBYg3tMUvm1QYtACVdge2wpWDuU3QoVvJL3k6GSXeWWQXOLKvWpgu4W9FP4O9lKjNOx9UaEiG5" +
                "rQOvKmzq7eQYc6yaQlq8eC5SSedzQrnzPZYGxVk71dhSusTDOok2T68hRBZQZNmWOnNclIKE3eGjsRvF" +
                "0W4bzmGWV9DiEPd3iGm+6aMPw6zWVMtsTagjrAOypOzMcEgJ9hhKCxKN1oDKWDaPxYoTctC1dQnpBEDt" +
                "GmLRjUwV5TrnlKx3tT9mVkKBG/ZAILXbfTAMMVjVWyWsv3K5r/1Lwhj5KXOk3zQDGz0aGF+BGsiF3zCJ" +
                "vRPwpLAP6Z0JGAHir6CFuK+m1Op4ug2yh84WyXN7H6xToVUosbWYWxEER7jsmfyhKP+w05UDb1cwybQj" +
                "xvesF4PB7wCzxPISkW9QqjV+KmCn9jxqj+MBTqRNg0aztBwOjBKdbgD4S2cEBqoz9EGlFSBlfAPbjtNx" +
                "sSjaLWfTrCcTEGSPBE8TPDYAn1SYSs35whR0+04ZcHtWVdltavYChqpJUR33F6AO0G1oLymyosYyuIdg" +
                "kja9ym21wNkVCd5LHKgmUSogirJZKAcG0jSANkANJOQQdLvw0ymo1kvDOWyXcQ57hAy6yImMYx1PzDOn" +
                "oU72Do7dcqO70fqDLysbGV0pN9T0qfOFs6GD/PpCER2JFwokFZJlzt2ANdmlaSC6yRfT02QMKXQj4FNH" +
                "5LHg2hSLBSnQA1bDRqGaM2FThsQVC0Png6Wta4hLmFOiLYxd1Ig6BX3sLEQQeZ0WC7GtuQRyDOiu5ljD" +
                "G1ELtQrzIkItx1mmb4vleqmKA5sEcLDpMV2AomG6cMsgEo7//Og+P4GjOPSJkiT8FQAZOQAjAUDoJDRR" +
                "lmA+GCuysmoMXpwscm+Oqu5o8mVaQlHuagOBk2nrCSCSzWld5Nm91brhD6rwTOU+tfW6VgFgk/6QdfLR" +
                "TIsPCOBbg65b7af5D7YTI7AZ9Pq85KPYkmKQ/V5Tl2nQULqgwAmNdV7ekoePCgWPFXpN9fNpAqcXTkOL" +
                "r3hIIRUXtCv48c0bqtRua9VqbzybRmOBDEEL+Vu60uokPQYPBi/iOl3AxRB96GijoREA/5zCSrWWoFqM" +
                "A/JX0lvmEGv3tqta8Dqx6F1YTvSys6zova5mkK31kzhbo2ldLUdgCnz4aPt5ow7jJj5O+EKNsTqfwkGk" +
                "U9+PZCgXWhSiR7sCQBThAZegc69zhkFgbChKwfhmBk7hA4MeIPJORRfhdea+q43DFVXwmF3fYYJ1kB6s" +
                "weBva8rUUuCGdodbo+MysbO8G9L2zDTa75x1Z8XeVnjrf9v6394dagUBf34ZfrtotcZY7c6fT78E7FMn" +
                "wJJ//6Lst82hvMW+kuUis3wK34fOYhtp32BlSJ9TNQmwRjUEROpRN8OeauCq35EO+VswmtPvi2Ladmw3" +
                "7Dy3vhULAA3MWjNTRT9MHWCqQafug/nm7Ixonj1b9zsRgw/N3PU9Pai8wauss1IfSzDLwDWKggzO7MDq" +
                "7Z1qdwv7MOSAQBMhT2l8cSHBYGc4S201qJdrmGiwKBnqgsfjjZGiDMMdiNS7uNpL7olr809JWoNxreGl" +
                "w65u/7J0Kl3+hSy1oKU4yfBrIKKgsn1PhlOMoMRYrmWLEQVpfTCDDhEdDLSHSGfEcbUCsC5v4TW6HOfD" +
                "2fBUjXZpJdzCWUjkApZlXcwQxZSewcQkTNuX06SdPlBLXuasgynJ1ZVKqZNh8nSabKs1YqBYA36pXS5B" +
                "rBGblzBfW1XC6Ea1u8Ldh1Eg11vs74dE3EHkWhQCfF8szAx655FZxE6413U3myx65Zm8ATobsdMoo1KN" +
                "CisSI/6HDcfwjIvUaFxUjLQwSW8wdyKVL52C7LQzrdlt+iLooE7rSDd1O3yHuKt69p321/51p/lB9qyH" +
                "E+6bf9pjMKjZq9imMJIslmUA4HQhiO9QHnwRk9vy2fWlvoEMhiZrkp/9EHfxldYvZNtoDPN4cxqG/yT6" +
                "BvfuOn/jqdtbXfai13LPe4GuAb5NjjSfqEiE0EOwSg0nh5bkOMshVSBb4NZjY+F3FjALa6eDKo3CdCg4" +
                "ebKA2Sjxxnd5XYkcaxK4r43v2p4Ey0MmcZgcxQ6B38ysqqthl3RsRrcjYbWQPl1dY7GXaly5uFkfoaLE" +
                "mHAyemErTy7sDDOlyYF5R4cCRbTcSfDpJLHirNgqaofgAL03L1rQYb+FYrkZATHSIW0C19VibTnCfTNP" +
                "Bl+SogH/O20ZGiFSMnPmzv8qAjuMTOmihdR1LrYtNb5iCnv68Fyxwz1mmLOCvDZNB527qpFoIh5cuEe0" +
                "OezdNtoYRJMU7jGc8pRJP6jZQTNPV7lO5JJAXxokynEPNQpb0+YOidQ8HppEBC+dcdk90dwYoARzBepT" +
                "iMQsUxaRfKpBOyV/zQHVBaNCwIG2RCY/vJwkubh1Hqcz7DhCHs2eJT4DSMxDIb9nUW7oDy6I7dxaPl4B" +
                "xk07FJHKuHqLAOaKxh188C24GnKF3ji+WcCGYHRJSgeCQQkKYsVFHRAJYEQ7lLLYBmktmuj+Kf6D6GLh" +
                "xOfJly9+ePSp+/3y5TcXry4ePXCPT3789unz84tXjx7aixfPLx59ZthmMYi5RDIn14rvB9Yog1UO44Q5" +
                "l07TEOIJLawPOZrTjztEzc6SnEE0kQk0Kc2B1Ahzlr+16NVR6HOExdephP6/ckIUC5epnsrTD6fJjyA1" +
                "oOeneM5EskjXvJzBBXEzmlQ1TPIVCjroJiIRL1l+9xFIHwbcjn54dD96+tHjmk8/AdXxlBT/blZiUXPb" +
                "JRJT0gFwBQ06T5jwMycnoHHSrFhzCs7mUAqyeSjc0avH50//fon5xGPaJgtMbrAW+ShWlHSoW6QgRQ08" +
                "UtKikoWzzU8JwuEQy8Ej6cAdfXPx9OtvXifHhO0eTsKaNFsZYTysaS6i2+Pc8UJyTF440fFotdk4ujo3" +
                "jj5E49w0Cj0Vw51uX+qzYXvGhPGg3qZ9YlaxKzcjnqRpXtSSy9bsRot0tKchwalkJbFJpPc10luC2eQT" +
                "h1Rj0h4yPUn1Fg/iijh1p3FADBoeQspRSHvZFplSFLakQfOIoPnScgYV/kXExC4aLWaHKBSfvAV7I8GO" +
                "3Aci4m8GHOS1AwBx4mGZ+a0pU99jV63KbPb4mhKr1k4Hw5YtZB/WbGVHTZiXFr/9/FCnmr8dAXcfd8Kx" +
                "wbPXEfxNVnU3oPlhy9oCaHEvy/4FoztYGGIghbBob5tviFb/s1a8T+GRh+/KniV5XUsRhnPEmjDHUOoz" +
                "hjGIzMzo7YgdR77xbovtB1u867f41zTJ90UerMIwWrIq1ulaAipLvqJfH+ITGvVChHSSHAcH7GSnmNVX" +
                "sAoDSHtGzpqOZkk94K1qgc2c+RfqBkn3a+KcwTdP2wznoY4DgJ+5yYmdHObH0QgKpY5outbyO0/DvtSJ" +
                "ueR6jVi4uBOhO6dB6M+x72dR/sTwIwCev3gN4JrsloI4pMBBPpYNt/gxaxnaDcsZwsx9StpQx2qWWsHC" +
                "jjGokW+r2tMZcmATlKsAF9dFvomMHMWKpuN6vr04G8ccVOtKYEtp5dWJVZwJ7aPKZJ6s1rVY/kPP+R2L" +
                "QLZRtIar3QIAoxFG3IhGcHiItqhrrUBi1yQG+IUlVHWHSLPO5uq3xFjYIBTdQJSizyaXAlXdIXWoDEsS" +
                "CtdYRSDJIMVO4xzJe4p0fGhAq3S+QrY1QrdRZlttUDjedPbUT1mJPu2TWERZYk9KCuatq/9ZS9EIihq2" +
                "ak8PxUR103Vxam3zmTY4pUOktd0IpLMQg1pdZxQNrCEJCb4lqy1wVEjqWyYiNoZta7pAlS6pN3kodA1L" +
                "djjQ4nJOeCSDdvYT46qn2eVEpd1AIjqcbRDcfTdgiOz39BcABA/WoT2qBArlzagnhqecxe1IE4uqunK5" +
                "Akbl284maaVjlEowZAgwLdUUeaiFZhQbkBm5lviYhw/ltstzNGY5PBZVLcQblWq3hhXJN5ObztuT2wUz" +
                "VC0QNJsHeqI1scNxSm89EjOegDkEOlpqNfc4n6TrwFV7jAcZxWWZqJlFAMUyRvFtFcm9fCPaimRykFDU" +
                "vZAyOhYqgOpIOIJ33dQJ9cC+qTv0obKOpikceskOULVyhPsuLqKehIynpgVlf8fnEKtXPjlJ7OQlRC4Q" +
                "YiuLRpfVm4AO6U6XppruFQBG1MpCEleOk1wuYamawRltYc7K26nUdxHZ/KJFYp4prT7svtO4m8rNR70s" +
                "oJ4pXZR2gwrz5sQgWgkdfWen7NzI+8G/LO490BFi6JjXCtugIhSwI2FhqNYVa0FaXBQWbcyOHFA4QYuk" +
                "DqYT4BXG1IJWvAbk4/unMj9Nod3nSIgaGtnH+x+pZkgHU0QsMWO7kbQbeD63XqIFGzivWCTzdlWvh3D8" +
                "gs5td4kK7s4ePW/KYYeGjLholLrKtKpE6Z9CZgilp1xiW8ApNiW7iOi4FoYIDX+16EiPNWd09NCTFAi6" +
                "qoFxv4sq6R7jKp5uLGyRPiVtyAmnfn2A7IvF6b3x80PyKHmACBN+fHqKiMmjxJzyy4vnly9eIRLUexEC" +
                "Re7FDz4s5wSm7FOnwuBf0L7vHYO4FcKOKPaQknfNjIZidEuNNIhtgVjMVzvxJyou5YM/TyHtgOfpVILc" +
                "U0XndJHOHD8KsYqCdNEGrZvVIlWp2dGKO8lwE6wnVwkhNspm3kR3xRmuE6kq0TMo7DpiUPA3zEOKt2zF" +
                "/4Z+DiqL+Bur7mRHsRhcfDg5JoYdmTVwcmhvw0pv8nzpuIETBRFRR99YZGu1qFqNfl3UVYnwW6uL4Xgj" +
                "HS9ejudrDfZcv2cxYSkqg29YjObKTXKV6+UYxEAbWdHcfGGjR0JlkU+xDw0C5RaySHk4y393MVjBOrqL" +
                "2ZBt4f0XEyTAQHLTYsYTAmo7Rksd2ahuzdw1EVHTuJVoFLeRMU5EXKfNThEyDA1bvK6Wx5B2KwMCFYr9" +
                "aFFIjcC5ThhFD39ym8tofORwGNglTUhkXuqnUxRvb5SkyzzPqOAA1mNveF8VyP6VQW/4oqgYv8TJdZHu" +
                "Q6ghoSO1m3SajzyzoJCgkUSmW59MDuYfjE/GDaU6QrwNVJTKyTPrKFU0kYVnKRVzMQQQ5yJHaDI9QOJZ" +
                "FKzMclSv4oUt85LI1uM/NBjXpZCxnEFwPA2DCXB93mOHSvW7ELyRTuLISj91KEo01hKI1qAqjyTSk0i5" +
                "stJr630wmWx2J1SDHHcjZNAS28NKcxHBDNPIIaSY7UUad6tbiFnYcuo+YRvafPCKv8O/gIKW1yN97VBk" +
                "QGPvG4tUale1wC2E4nbjKAxpZDG+yA8S60TPqWTr1YJ2gURfpsnxPoci5OYlp99zjKQ2kgLVeUeIQU+L" +
                "tygG1NO4vhaL+y3LNs73pxtBRZj128Fj/fDE3j+T176i37cfufbkZ8AT/3PF5ZWzZvAtnl7qA2YiQU73" +
                "rdO+maQMuLP1JX+1tvJeLBPnubpSTLjuYb7+ldQmih0M02WZSo1nvKyVeEeS56SnVC3EWg+HlxXBS8eC" +
                "ku+LCuvl0+CFDAakVDULlvQ0p4KyfEFnyK5V8H1Vw3Df8F+JjIjyVVOQO4oQhZhOXUKySBbMenhHIjvk" +
                "ZCQtlD6BhJNNjtJFSiCOK/bHQXgw8M7Ngfz9BxAjDoQ0HcLJi+uoJJxGT0IKpPQEnh6zdfZsKMtSztVC" +
                "Ks+58sVnb7ZKB0vojYIe+PmLr8RxC5F/JiM7oJ+xLdpFQ0j3UVZNR73BPL3ukCliivbN9ktYwFXhCA5O" +
                "Btbfc5/SXXzeODCgP/R7EIoKdzcwRMHrFMyC17sf7HIBsbtsqeMqIw8d23zQUOJCMLAXOYKMexprYjd1" +
                "tHV2hpx5fnYWSWZXoLxeQSGLQdrq5OOzqCcHYoD9JOgZgOLEc4FQ4LxaIPZpdax6ytlFXoSIdO2uxgeO" +
                "2C9rZZ8auw4UKQ+w5NfpJN9JShu02vC4zhnx4XvUpNcFolkI6p7E8Z7xltH6RI989k+iGpSU6bJkeXIa" +
                "mrozONvdpveY5U+W92D3k0NDFz2JE5xhlPqSzZwv4wA8l4zAcx6EZigvrIWWm9xNQIKD/9kqkyJdQZEg" +
                "SlURoem+rtzeufTBxhOsNXZDAyhMbFDWyPTb0EcAYsA/to1aLyOcZF0tUiqnnjimLqMvAxskOkSapUH+" +
                "iLaBDuMIEhnk0uOzidFkpNJbzjqyglf5WfLR0sm1l5QHRnHu6TD5nhYzS7a1hNpJUVlFyXPvbn96l3mI" +
                "2juV8msJDOeuSsg3Fy1JI3DrqJHY09X0D4Z3jq57PDXFOxaroOood3AirhFtzjoWd/+Fp4FwEUZAjthl" +
                "NmmtsZnQv9IdRHKmnxL25/+EfnZPne0eOtvuHi07iEzZ1T2ULa92DmKpGCABuf0VdHgqy/JZnWulEQ8O" +
                "ZBW2VkLRNOpMaGvQ0x2JDgMqLffTBqB4K26WXGazbeASRAVOQp3u+IAcxZU+iMoFESCWYOVGv4JcEjnF" +
                "lBXDdZ0kDtxM2FNQGE5eQG9h2L+/Ov9KxNpDqvFjFMBt8TfdWAQP2QBESeSji+7HN/nEs1NEqkWrVVIy" +
                "3e53QNUWBg08LdUTkEe8kQUL7szh/yXZYSXZhod85r9aklnz/0uS7CZBFh9kvsExlBIn7wX2Gm2gmtiA" +
                "P3vfvhc04aPi61DnoPy8952EEt3uJYtPJyDV0z/h1fQOSw38sa74MF5Uz2PHjw62TqLcr9HkUxMsre5h" +
                "zXFdXWmyh2exEHKE2IRYpMRCAkIS9+Q50Iqt0zUJz67doRao9LNvF7V2o3d8uMkxf2FerlECyL9qlXoV" +
                "gn9U0/kwnu8NTpuVC/Ve+wyi93vc/UUSrwkHhkTu0EXcVyq2e07ZVStajTfrV6So3fIXvVkM7rhzRZFT" +
                "ymYy4B0EQV3oXHZMwvoEWSLJI92HKEawrCKjOeaK3tkHz2e9xG73y/CI0AGyQR9R7uoU83TUJ9ILY1Q7" +
                "x86judl05qOQvlxAwZTj1oeL7/qJ6TQkB2D3Kfmod+gQpGznji8JFdtJLp2V+BNyulj8m045jV0z5BNr" +
                "7nCxHNRkqjcshRlcPWPpkvr+WiDm9CX+qlFxf0T5xFcAyRjdO1f8xSouThVuiJHb8VzE40N3GGU5Z9e/" +
                "xEhTdqaxe5uBYM40uSqrTfm7pPj2sONjpzndqXBix0dDzAzWsxx7i0hB9FYJojg8FgKyE+bPMPhTBHF2" +
                "bruyrea5B6EL+v9W1ScI8gzkIov0LGda7uoqZuX9a0LQYxJWimhhJjsJRObfNYCMbbVbIeToys73Fq9E" +
                "0TBDwOWNB45+7QmiX3sgiPXMXajdQzsfOocDEHaiStwGBqBy1vI6geZXp6VJFlz3JUD+tIJWjfEKyBC1" +
                "5xCdA0UM9MpCZKQbFslvYXaPM1f/FmTAbopZcmJRI9DodVGtG5iOOaoLMD9LPUm+RQo4xlvYPY/Pz3lU" +
                "g26iVAfGQHxJTpRYxe8tqgEA95j3z215SHVm8dIW/Nz0aKLITnSkVxfPXnx3wcMDWNOKhS/i9PmLE9RN" +
                "dLJVJu0s6g+t1Se1pZOtE7sQFvny5cXzcx6DETkcxtw/nIxyqmlHoXw7Usb1SxWP7ZtZ/nZ6WrKSYtXT" +
                "q2QNCCqjecy+kiqrrkB1BUs6RcHNQ07wBcqC/CF4wHS3U1nDyj5/PLn4YbEC8fib/yQvvvzrxZPXvE33" +
                "t3d2f4ifJ+/PD9h5MV4yTLXnZBkkmVS4weqHbSDBDZqO2EUtsZ9pyNk7XO7sI9Kmd3qmxVXuA5rxCGfy" +
                "RvuH4KNcvyAUA6GFiqWxyXtA8VUk43gqTs1KxOGvly+e32My2IUhfnz87NtEASAA6akYktbzQHQvA2W1" +
                "YaV/lsx0yjC5ENuBYcudXRdW8pWei+IqP0tu/8cRMXx0dvSE9s35l0enyVFdVS3ezNt2dXbvHk9ALoDt" +
                "9ug/b+sSNakOe1BiIKWlNGX3nI0jFxUFLOjRsyN0KrRc6CrP3YWe0wW4VUsKrTpwD8EyEaBItHsmzr9U" +
                "2vA3lpH13cgaPSBxrYEnSjmNJckFzowtucVKHFzAnCUeAfKOKMC7PgrO/vjvn3+mLah9tYoK7XZnfORG" +
                "uvzbt4j7w0pg8N/vU2fgy18W31gLhS1DJUebWfPwT/qG2Zaz5I+fPXwgj2hdswGM6GrjWkDzI8mY9V7T" +
                "SOFCbABLHOlXpKfXC36XEo+2Wh0ZQYO0D3F8SRTo/rCmhv0guFUTB03t1Xn6NvmEtvonyeQd/smkQk5K" +
                "TM4eYX/y6c/3edfZ2D9+yseJf3zAx8w/PnwTbiH77I28O3jAo3+xTYgJxOFU0eQ799mouTb0V3DfMdti" +
                "p+VkXoASQsN+viLk+ewqbIJBqz+nybzOp49uO8bYFFfFsK6aYVXP7rXT239pp3++l/4FxDi54q2w7HMJ" +
                "/55+fFZNED+2DaaYkOqgSOzvxLgcLXanm6gn4+vi7WgkG+nbgUdnwNmBogF76ycsGGAnF4EE2Br+Ojwt" +
                "zJJ6Cu9tShMX0zMHBML+usjo6PNrETrfWM6B4CF4hYfCm+1STfdTd014546+eLT+Ei74zc9Iz1fuObnf" +
                "L4KQULnd88kbJhaNO27eK+BSZGgZl0+CBQzJ8RpnIO6u0yoZoaSBkFDKBicHeFYMH5eoni2lstfykNSs" +
                "quz9dZY3zDyU+PkDRLujR5EDq6XHOP46Xi4Fk5D8t0wjsqbtGg6xN7E6MYaR7Z4n6CBRlwfiqHVmK801" +
                "Ve6w6E4BuQvhZUqu5EzjS2Xy+Pl5ZGsGQnMARjEJMH2+88nt/O/CRkKD5KJeKUHYCngqeusuZaaU0GW2" +
                "DHs8yMyj2ifMN7oO20XX9p38dMVR4eKtOBznr+1xFVQHW4XUZP3qNbBo64Nr0MquwxwyDiVbJn2Ddc3V" +
                "aFUABJMyI8N/aLsbvjHmN9aXNu5/iSLgX3395WP34eP/T1j8iLf8LXy1/23mfxv739LfoQZTCt6I9Z06" +
                "qH6UF9y5t6LpdVTPJ7I0vggvhGMCfNrVA9fDEYA+fA/xJ7emuo8f0ct+z+gafnx4LvWIrKqFMaYBWqgb" +
                "KQpHB+RB9kce47OdGIX5kn1xO1cEo3bRnlCUOwvja341Z+AA8grefSv4ffD238eX1MJJcRVPq8AWcF2P" +
                "JQ3LXNO9ajJZr6B8T6hFpBxW3uAgCwIBio3j4bi9N6SRgGu5XTmZApKIxQKuVmx5agSDoW/t3pUir3iw" +
                "USu+WTG9xLjufDYPGqIG3LqVqBX21xjreUjpRiBuGXD7Cgjad95J0q56/6FWJMhVJUMp5RePmEYEruRn" +
                "qEnaNiwF/5zqnTxza/BfJOCUWXVqAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

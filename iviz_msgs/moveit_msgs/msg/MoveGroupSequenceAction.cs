/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupSequenceAction")]
    public sealed class MoveGroupSequenceAction : IDeserializable<MoveGroupSequenceAction>,
		IAction<MoveGroupSequenceActionGoal, MoveGroupSequenceActionFeedback, MoveGroupSequenceActionResult>
    {
        [DataMember (Name = "action_goal")] public MoveGroupSequenceActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public MoveGroupSequenceActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public MoveGroupSequenceActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupSequenceAction()
        {
            ActionGoal = new MoveGroupSequenceActionGoal();
            ActionResult = new MoveGroupSequenceActionResult();
            ActionFeedback = new MoveGroupSequenceActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupSequenceAction(MoveGroupSequenceActionGoal ActionGoal, MoveGroupSequenceActionResult ActionResult, MoveGroupSequenceActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupSequenceAction(ref Buffer b)
        {
            ActionGoal = new MoveGroupSequenceActionGoal(ref b);
            ActionResult = new MoveGroupSequenceActionResult(ref b);
            ActionFeedback = new MoveGroupSequenceActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceAction(ref b);
        }
        
        MoveGroupSequenceAction IDeserializable<MoveGroupSequenceAction>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceAction(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) throw new System.NullReferenceException(nameof(ActionGoal));
            ActionGoal.RosValidate();
            if (ActionResult is null) throw new System.NullReferenceException(nameof(ActionResult));
            ActionResult.RosValidate();
            if (ActionFeedback is null) throw new System.NullReferenceException(nameof(ActionFeedback));
            ActionFeedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += ActionGoal.RosMessageLength;
                size += ActionResult.RosMessageLength;
                size += ActionFeedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "146b2ccf95324a792cf72761e640ab31";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09/W/bRpa/C/D/QDTAxd4qihOn3da7XsCxldRdx/baSvqFgKDIkcSaIlWSsqMe7n+/" +
                "9zkzpKgk3dsod9jLLpqInHkz8+bN+57HV8WdeVkWy8WN+W1p8tgcx3Va5C+LKAsi+mc4hX/3XnW3uzbV" +
                "Mqu1ZUm/NrV9YUwyjuJbbT2R372jf/Gf3qubl4fBHGaR1uG8mlaPN8wIV9n7zkSJKYMZ/dXjuWXpmDti" +
                "i7PTAFEQpsn6yghPhKBPs4iqTngiPMveg+CmjvIkKpNgbuooieoomBQw+3Q6M+WjzNyZDDpF84VJAnpb" +
                "rxamGkDH0SytAvj/1OSmjLJsFSwraFQXQVzM58s8jaPaBHU6N43+0DPNgyhYRGWdxsssKqF9USZpjs0n" +
                "ZTQ3CB3+XwlOgrPTQ2iTVyZe1ilMaAUQ4tJEVZpP4WXQW6Z5ffAUO/QejO6LR/DTTGEP7OBBPYtqnKx5" +
                "twCiwnlG1SGM8Sde3ABgA3IMjJJUwS49C+FntRfAIDAFsyjiWbALM79a1bMiB4AmuIvKNBpnBgHHgAGA" +
                "+hA7PdzzIOcEOo/yQsEzRDfGx4DNLVxc06MZ7FmGq6+WU0AgNFyUxV2aQNPxioDEWWryOgDCK6Ny1cNe" +
                "PGTvwQvEMTSCXrQj8HdUVUWcwgYkwX1az3pVXSJ02g2k009EjZ2Hg0hLJhtUs2KZJfCjKA2tixYCe3k/" +
                "S2FDaBF4XIL7qApKJJgKFoEEdEb7TSQJKIlyGQw2ubwD0rifmTxI6wAWaiokWqALM18A68ky6I0wK6aa" +
                "ewNDW9DB2ExwLlEQm7KOYOdwRj5+Zf5ponsC6IXprXAQi+dgYplXnkAP5nRwBqsqmhrahKBamDidpDEv" +
                "UGZQDQQ6HhBuAJOaL6saZhbAqYNWA90/3LnPwQ2JDwJzQ4Trs2uev64DJnaVRXkO07xcYDsgYfkdFvxg" +
                "KzPvmCJg9jyFmRYTaEs0ozPTyfP+RAEc0jwJyyhJlxWxTRPFs8EOUh8fZdjWBTIuhIW/CRBQHxCZPKpw" +
                "s+EXbJ55hwyOTj4Qc5EbywEBZHOmZ7WZ//IW6NfMq53tbHF7eCFzXJFFC6KAjiROjPFAp9Au3cehbVyZ" +
                "6Rx5leBIF91HLKQTPKR0BKOgKrK0Bm4m22KxgoTkkddOD0ceFbw9gNn63pjciRPCWp/Gkv2dw+mEDajm" +
                "RQFPE+AaOFE44mnJEsoOyItmwLLvMm1sCzuno9HK4GzTu9y8q0kqwoM+sh5Yzj4c9gioPpd5wogAf5IV" +
                "Uf31swZlbXGDPUyqhEd+mQLWQHYhu+PNTMwkzVPCHW5jZDcVeJzDK6LLUrygoljWi2WNm6rckbfrKkJZ" +
                "U5uyEsKAc1GUt9Uiig3zYf/8iFjAFqALVABmp/eDNvdAWRDhwj7k8UDtAf0DJWiN7Hi5AFEIek1wNrGM" +
                "/dcC9IhKxxoTCnCo0iQIAaa0KKqUeRfSDc464nMfL8uSSDo3TGlwzF1jhogwkOhMHSBB7PSui3FR39B0" +
                "KpxdSFMTap4RgCpF7kDygF46ZM2LBDQ1lGN4IOHpIBgCMwpMZuRwIRhsGZUlkDvtIIkmkS5TJHAahx4g" +
                "9cazFPQ/mmgqRA7Tr8uI8MIb76lxDAPgw/yjOq1QMu30TlwX4Fik+HpQeHUXhWwD4DTKV7BSeAdMsoD9" +
                "oW2PgN2mjFogm2QZo8xzvJlZ6F1aZKRyErr9qe7yaVwsMhH2yH9pENidvKiDX1F+gozmZ3uNWdPo63Me" +
                "tdAh7B6kOFIVPP3VxKC1rFg2M0JWO72RfeEP4Zp3D5SjxiaMxpdJSHMFKjlEuMBKqCGeWlUN+kSquN0R" +
                "KRjNzip6UE3aUa1BXoLe1z2BKYp6/CEHBOB52piChn8WCzAMEB0WNHUNEZgHejkfQ3sEjuq6g0KSo+CD" +
                "NwemAXpdcDMryhoZDciCpfIYXUUJWnqJzXZ6bBAA6NBqFVFdo4LnYXUevUvny3kQzYulyJ90brqQjNST" +
                "ZcU92zd6xEhxF61+z/FuaelGRqjCdUCfJ8U6ws3mM8TaDhlQwFmWcSpEr7MjLAdg9oDmWCP9AiulTY1j" +
                "ONyIYKCYQRAcywTvomyJreAYwvR29/tP3sLb0SaIqy54nmopx65E5iRcBkHPkdDxPInFkZqStB2gYjDR" +
                "EJ6sEcYG0gQ5gwwSe4IEQC6KUNMSZ5uiKM2nBg2hOe5glNfZqk+EQLwnj7MlqtFjQwzbkJjZH+zvsRDn" +
                "gYjqjZVASvCEDdzaJ4N9BgYygLG9mw7MoL8JKwCx+cbHz54nqqFVqL3Cijc45Dk1G/kA1hpuRch3iEgV" +
                "82p5WDEfETYBcU5yIpuYLEE20KmD18Tqlgu2WuFAwuHZBa24eLeHZKNMQYmneYhwXqJQsR9BgIM9uGLr" +
                "zGqMeErmxTjNDIkbUrlUmjFo0BTvTZYN+JSdkmxj2iiFcZVmAkIW1T+VmDBNWG2Zk0rQcNp4HCIFFgIU" +
                "wO2UAULPPpsAQvFKdDyZSlULENhwEqamAPQBZ6cteENs/gAhhwx1nR99gtGA/HS0T0RnnQPr9gJnhskb" +
                "lBYRHDE+r8BDweYOiCAHgTUWiABQN6aF2p4oxNLSkBFPekpJhngfVcmkMKhIo846j25Rp0KbC+U8iHxg" +
                "cChf8ypj1gaPocuuGUwHfSY0asUaP5r+6M8Cq7tMp8CZqCcMNLedo0AWB9xn8pSFD82ZB4OdQXu+qIVR" +
                "IPdbFUvQLmAN8I9S3Ggk2HReZPTVRdHHUyAgmgi9Ih6kpxQOaA2kOuhZBvPO/mtl//X7FliKU1o3cpI0" +
                "d/iLxsD+m9Rb4x4CJ2btzmnUaPlVqmOg+QD88haOKm5uUfLo3+NbVpmpoa8yf1+IXgZKJRp4s+jOii0T" +
                "nF6+YE3VCjpWwn3or7AxNPRGof5hUkzCtfGO6xo0ZgAUF1mWVrjaYoxaHci1SN/BtlewrbSWoPBEK+gP" +
                "CuBE+19Sd9CatXtoQYcCmod+kUVTQHOCLlekY6BqsUdQKMZA0E6XIgEIZ6omHyidqslkjb/QJNmoof4+" +
                "o54XVY0+WJXV7DBWjySZGLrccZGgxrKrE4KGqG6jjzgzUdnVGEeCvY+Eyg4PgW+Zw0PPOBIPHJls5OUS" +
                "f0ztUR+gc1wUaMaEuL5PxvW6idFDVmTPAxHirMiSynIAUGriMh2zqGILiJYukheYDFjVdJDKgvzAfBqQ" +
                "WTovKXdCz5MoKrulQWGMz0swl9IKz168h7Nhqw9VNnR+B39qnDkVOAolSkgJ2uu7pk47ajd9XFHjx9Ue" +
                "nVXXxUxgt2rrgGcjzKqVAuBijr0v9ga0sKFbC+oF5NNEgktADPBZBZUTmQMaEoIIltz2OFedzmIdT2xn" +
                "8ewCgSVoW4ACvdaGoxXoi9NtJDbCR0m7BuKdqMivnlIAo4MbVmnFukvk2BAZ42CE4gjqQCQlp4lhmgzJ" +
                "SN9ARy0KxR11kvawdqQwNSoGwQ8o4VDYsfARfkqryAsAKPvTigAgrHmfBFcMxg4c1jvjbyc7EdCeWgk1" +
                "IvZ4Nby33trFgzJTGISnKv0dGD8sGT2fBMc7NeQpIkWQ/eaWBpwD3SGHbFOdNCpPqCWggko7CDKyFZIT" +
                "YxT4KtGPSEv4qQfBe6QE7z1iDGyDoazLH1jVtSpEkZ5Z5gFIPbK5hAtLYomZgppFhIf6VlLAvqJ5VqAu" +
                "rhwbULKM62VJvMSNx4TMehmgHhT8hE9ypOcTQ3nVCrSROWMe/d0J+1ic7sR9pqZ25x8NTuvfuQWmREwq" +
                "iGegMgyCF3gU3kVzmH8fnUxgCUSlMouIKOz19ekL4mkHKMp3QUcGk3EV3WOMjJ2NoA/zS6Rg8q+72J8/" +
                "u0h8wEAVAIX74oluvCfVElsoNDjQd6akiBCGcWDBjTn8PxvbLhu7R+Nu9tFsTJv/X2Jjm7gYa6LYvWqZ" +
                "fyMlYWhlyXmt0T1sKDbAv1vvfiA0wUvG13ZsRzvrDuuRjoNlKzbYcl+sJRFULQOz11Nj2DMGe/9YRugP" +
                "QgagJtt2FukG7rKR4ayWqeXxjYXgr9/crBEPHzQD9V/3W9pAoiRZlnLdyimPzfWMy+IWY/E5GeYVmkZo" +
                "GiAfjvIpRRbI6zOwGyhN3G9pt53V8Zno2DXYCt4et7g+yHP0n9Wk8dbIqD5yiQTM/WRTYBs+wg1WqPDs" +
                "1lPrerdGnETGJ+k7NVfYXZXSqm9tJAB/uEAAobIRYYxAG8Eo1yxasMub/Fsu3tWaCMBgYeZb2tiOB31w" +
                "NtH8D9ozXDABzQsx0gdpIh7kPkfn1b5+0AVRjW2W4XYtXiQCR6DoVxNfhcxWXX5i6ZFrw4U57ZDqQEBP" +
                "hec7rYtlPKP44co6uh/ZyYUaBIqyEmTFyotbeh12nAAhcKG4Vyhq74KgPDNSMUALi9lya4T9YF9IcbTx" +
                "fJ40O+/mCMMuB2GL4w1YHMbQQZlmb2CcFRx0L4slHQsBIz5+HSQ3MUrjckXDlSbjLDD1BsvQuJGYMaRO" +
                "HS+85ix1F4kDlOAEQxnE7c69Saczq8O0dqWPQdHbvLjPnZufO2zFl79+Po9FNehz1tOEfKzi7FE9nw7R" +
                "Rnc3nAFZqyByl0iJwME+Yp7PGXqqbHqR66qbvloYphD0cYwjTn0hLLkjxf8IUe+cchYBL4lXMkIQCMeP" +
                "s6pHTVgxhcHWVT09y9IvLYVp4PHp8qD7zj+HiZsCQ1I6UowulHmKUS10PiI/4tlSsyt9h+aj167tvK0a" +
                "DULZBhztlalmLcj4CJrP5U0nLHzpg3mOx0ajt+hyM6gkCMNz0eBgvHSZFEZtGVY2XOIL7F+SpKx6Ewr3" +
                "GvPDLBFaDw21abH40p/hcZJUPm3JHtgoD/kZyV3uNQLavUuLZQU6s3mXYgIeOfxZ8BI/gs0er0DpOz49" +
                "Pdrnka6J+zYGm5TFnP0X+V1aFjnlRKAdVqI6vmvAlFsB86JTQv7iGk561SKSNNmTwa6Hry7fDI+eyMoW" +
                "C+RlaPPmdnVkJQsDpqlXNoPhvSvWWAZ30tXCfnhLvboaXpwePbXM2g3bPSIN1AfmeS8HQvad4iq7FNOX" +
                "LVTbRxMKMzOp2a7ZkzSQqsgQZYBh5SmO6SamSik7hqZJKDrgSV4uNLLMYhp+ouZq2xb6/pPxzg8znd6D" +
                "P/wnuHz+/fBkhEnIf7yz/GEEnbw/WkJ8lWzuCclHYXTA5dDLgfZPZdgq98K1dTFlt7u1O9n/C/RCXvaW" +
                "LnJrrF/XH+SQnjAI57wolbamwM/yIBlbqQBgHMxk7E9IhDK5X76/ubx4jIkd4pP56fjVecAgBsGxJWjg" +
                "xPZEeLE95OaKG+d3Ek1ARc8gGJKukeYdu08ni5wCRXELas6tOQy++M+HiOiHhw9PUCU6ff6wHzwsi6KG" +
                "J7O6Xhw+fgwmTJQB0uuH//WFLLIkZSsv2CWUC9vkXRStCDfJwwNqnmn9EDphZh2ciFtjJEd9ksHRHacZ" +
                "2EmDpmxtkG5MOciUviXhytPnTCQEJaakzEi9YOxNITKTxDRxrlWHFD6COcqC6XdAkA4DiwV+iIiAh21E" +
                "HH717TfPpAkKag7RQsP1aT/U0W7+cR7A/lUG4yF2v5qD3/yWfadNBDwNFzy8n1YHX8sjjEEdBl89O3jK" +
                "v6FDiU1S1Ja1DagK90WZtJ+jcoML0lE0qCav50WyzLABhWfrYvHQ0jiS+6dyAG/SMFxiA2UEVAukvH4Q" +
                "r0BFJ60vRu+buLDUbiqNjfkAmanrChSjseoLAAwFAsp/Opusf+/34X+DHt2s+CZ4fvnj0RP5983Vd8Pr" +
                "4dFT+Xny0/nZxenw+uhAH1xeDI+eaRa88i2SQjgnaYXPe9ooSUEcVxr1dU2dz9210D6YIIDT9zt4zQ7Z" +
                "m0jJdBii1HRHbIvoeqfs66Hr85CFX09oFN/CwmmqbIT82A9+Ygfxz/6cI7makJl8WlsPZoMroRuPbgK4" +
                "nI6Bw23449G+9+sni2v89TOg2p8S419mRc5F3HZkpPB3bi899IXJEH+W7ETNVxZjiSlo0NjX8Pr49Oz1" +
                "DczHH1M3mWDiBvMtIMYKkw75NCh9Q3VJcu/LUD8HESgknIbGeRsNuOF3w7OX342CXYQtP/bcmjgi7GHc" +
                "rWnWsND0LAS7eBb2eDzkejoOr07G4R/eOJtGwXyORq632jXdY54UOTsX9BX0d7ZB+0yOjU0z5/TbOl04" +
                "GiKcYn+0WDnbqi+Bky8Fqb3WSRT8WZJqLR6Iyzupa40dYrDhp2Fx6/YCWa/lWmQLldW2Q412CxUGfs+R" +
                "a8S25zEdBD3Ol7ERPc/P67Xb1gLT3LpDG04uP/IeSdZzY7kf8Ot+ehGEpqgKHm+qaH0ig8jkngxIxSif" +
                "gj7xF4/DSiIqZd5R2qzN7YE1YuwMlJ/ql7c9HGMkAChQIbB6wjzEFag91EK7NZp6SLPpwDnFb7nTllCl" +
                "y+hAmS7rYeUmxfcWfzngeZp3IbkVtzJbsuE7w8kcbjV9cQY4Z4H1KETvgi/Rk/hlEP8O/0mCo2C/R2nm" +
                "h0dA4Gbyy/5bdE7an0/wZ2x/PsWfif158NbGL3559paefSoEfMAR2AqxdUbYWl2U0PjmwGeat3IYijh7" +
                "Vw+Yo7hgsmRh24P4S9/LK4cfjZTyt7hLRbM15z68tV54bywWZXyHjW9gkh5qnSfNtHQbP8XIc4mWTDMQ" +
                "SjyitcoBLL3Xka5RredrwHK8h41lrWdyJEt1TYDsD9FTFFJS7Xa8uPa2x3sysbMN1xG9uyJkLCLG/Tsp" +
                "6sWx13Hk0g5F3vVqhY0NUCo+mu5i5Gh0hrbBTdSeh9ZNlSvZjkZL3aN240snhBvtPeHc7vImrcQwbvS4" +
                "s4+bHbaweS3EsBeHf3SIdxsMG7P7grzkGmYhc8xi3vEb9qSwybDMVWHUm1mob/9iR3kEb5HM89iEYzgH" +
                "9303gy+9d9EY1vDWBS20kXvSatv1ggYQ96eEPdzFFhcLclsS7CYmL2pSCDD4DpaoZpCyD4SzTX2SDk4y" +
                "0PRId/jdlIVcmATNoHLJp3vt4MsW9n2d0jeeXdy5pKUJ2E1xa6WLCxuCMpwoTVHHNkpJtUI/34awKnuP" +
                "JhOMMu4KURIgypDYc2w8KqemFnFReO3uDTFs/ybGpisGDCMkGCGP6eYgl0M2zj/Y6WkA4w03da1Cvlf4" +
                "v5HWtsJlmohxXqBIsQobe3Cq6JGAcrUxhrWuPuvmFOoe3iXH4hKtpb2PD3l511RRzvvpai3rF12HNbr+" +
                "Pz5KduZCUlysQQH2JUxmQxpW4cBwTGKj9tbfZe8pTWHj6Y55x0JbEbj3LE1G//Cy/IDdFqimU8D+ETbV" +
                "TEH6OFYl8Z9GT3FleFzMbRHRmUtmamOu22j/13BGzSwkH8ojsswCU5bIS1TGeXFT73LnmG6emvBdiD1D" +
                "27qjyerDTX5fa/JvyuW6tDsb/bcrdtcEKWk0JfL1VEDOO07SKuawJgulvbWcFS4zZC+1UAfKjWy49yIL" +
                "ecWuuHuOtqULCsly9KnAe2KWzDl9myC/kukR03EzxOEQFtjP0HZZqo3NxGxjXcWYU7KZQ7vuOI8B35mv" +
                "OWAyalEFgbi4HAF4zkibwxzwWqF3oxUWXDOJu3oVOvkdm0Ct+MM78iXDTWsL1lMc9GaRS91FfNyl5t6r" +
                "OyCoAeJe15+Ie+9SlhfgIRpnK8m23dPb6XQAqiWmFS9LYqMDjxE0vLO0myTlXOkGpRU0c1KuouBptay6" +
                "aLUNn9v7MP+iZqrehb5TF3i7Jd03i2d4qVmvprqdYjmlyNL7qc1KCpax9TmdmqN1XZLlRmprWfWrEh7y" +
                "QjMeGO9Kp3VxH5WSiKG7a6fNRyBq05tPZUYu8ZQFUFLkckbmWLeBYhyt7H26YcltnnGDvlzejdjZCfyN" +
                "1JGqTepSTwLtnWCxAkSliTuyfK9bNjjK7jFpAVoeEJUXpbE1AHDOYeUKjdidtTeUm4eTadnRi43C8laB" +
                "ViWDupupLfkGEJx+INgnfsHpY15dHHelwja0EWm+7Iq54nVjs5CVAardXViLEoK20JyfZbWkW+50365c" +
                "Gk798fJ61k8hRYg5dAzi6U709RTAIjo3057cWXG0N7wjjlIspzNHXKh3rJ3Cfhd/0zOCSTlBNY/49IxN" +
                "HC3dOevQMWiYzjwvYT2Mda2jQkUKHKliY2JZAuoWyAqvOnLuUES023dUE0d8U3dt8opETAkCDXsWUaIM" +
                "5jfSEPuigXKwhwZk7cO7nieXg9H3Ta+ESSsnzTG1INPFeeMTApR3WwrRC9eTTo5g6ZtPFNn17Yx7lSCq" +
                "37lZ82GPKJ8HEY5v+jxPe0g1KXZfZPJ9ITNitRnQj4mxCzKLTbWnIDHQk5ka01ZUGMrQG+BfpY+f8hA+" +
                "eJgZZmcxZ90b+NzD3oakNVN+k6xXxKS3O2tsQaojWQkTCVBh7MWCc9pNCY8p73W/TzPkG+H7Ntm4XqMC" +
                "T3Yn7ToT0DCkhsrMqMqUdCQ5WYFlBCvNVqwi+X2IA2RoOTWXKQAfdCgDKjbWiEmpDNVXyTkp8pySleEx" +
                "14doyh1fYRCxxwToUR+uBu0yW3WERKhFnaomLSRRWppWBWkijAA0MebP2efBhU3qbh0fVWXUW+LUpB+D" +
                "o+BpP/gJ/nrSD37GOMiOhtOHFzeX1+HPR+0nP2HWYOPJj5jJx0+Ek9KW2Qn8W9kEG6UMoQB/K4fXeh7t" +
                "iz9Mmlr5sxXSIQAoqLZh03TWelIapJtnQFqNGlKF9cd7tyNdkKVdTWvbDu9WxUZn92MJAX/yelSaqa7B" +
                "rlrdQE8K64be2FpJ1FDu7Et2K5H4hEocuBI/pNFIdFjKQJWmXpY53Z7ySilyJalWOUZkhNbUksJQ0glP" +
                "fCDVA7BviDk2f2gulJmgC/8P7CiQ82mrBg4pepJxFezSpRTmABWYrGQ4gb1VGTMXdkVpwTmpVX6aKAK9" +
                "i4DrU+lIzBlVTcHLdpYl4ZAhD9lclGXA7J6+e8+S3HpEZm5YEgebVMq4Ol9aiesvdnyP/1PGMRhN+331" +
                "Snl1nFxyE2Gfi38BN0hWOZgHMeUa5pN0Src4WP/3FtwqAXbGUoMEysRvR2qAbKqPmSXfHVbkunI9VW1R" +
                "oFXx8EJCO8DmqHLgrZy6iwWIZe4qKb4cSDVBnUGf7BoiEEp8K0ljiTirG4k8N4YuAANci8TBPgv97sWB" +
                "qNfFNPGMeLlLoy68KiKacraKJia0ByjENXnkJacQNPiCQ7sUaiTrMWEPiO2qCe+22KGkLVqTUQqOakk4" +
                "2j53cktDoVurn9FRNTninCvLkdK/zGPmQ6g0y8Gg+wSec3GdbLkFHQElpECpjN911Jhr15YD2zBKyfBV" +
                "ZasLbHdhORkkAcm+2iq/Jx4NC9IqgE22vhYwRgyDSu5V+GkUt6Tn7eKWfoFBv9Zcs4QNxZ9kNAZDrZxf" +
                "17Nv5RYwHshkuci0Nk49CXa7bEQX46LYWNvetZeSRR0B6Ut3HEOuke1uV9M0aP3KEtztJa4gsNOTUn32" +
                "CtcrriyghQNdlR/tQCcdQJJ/YYGLzKcw0jn8vOJfMB/ycMvLVhesNmekA9YgNNqcXrTqF0kWdH+9kFE/" +
                "MHdiHhSgkMyjBbIhf30LMn0pz5js4CIjO8xd1WJsz+V4shbUqHaAdYP5TsYJdsacAM4jZ1guIN4YtqVP" +
                "/FCUXB42S7ZXIWkrBYfeT5VqErpL134VDnIArKnLrBkPbK33B3oi1lrGszRTgseG7XCSqw2lZdcRDLT6" +
                "axTMQGE/+kLuDtynt+mgLKpBUU4f15Mv/lZP/vo4+htQdnwLgKhCxI0xdFc6KeLl3Hp6JuLe8/WftdII" +
                "wiCa0w0eeA5Xd9GRGvHT3sjVK7ElCLZx47qTGwhf1NQfwEC5cqlRrH8wc7BpatSG09S8O51A9HdpghmU" +
                "+D51/TfzJzDef1tGeLugWs05giwlCJs5W40B28sY4ks7K07/6gzmtg8z8ULYepNN+pSBkFWFYym+ssJI" +
                "YZXFlh1xmBo0VJD15aoe/9vSlKmnu6Uk7hnXuzlY9jm7HtRlgGo435ASTG6cvdNsXRxkfXxPRVTPX2Rz" +
                "2nk5MA0qnsYTWavOSlRCbixS/0AwzgLoQTfcn5IbrDFfar4jafWMS4lkyHcIaFKiYPGN/jw4vjj1r+l5" +
                "dCcgwgY5IDdce6dU8BlOFZEj2gvNUnRuN0D2xbdio7HGmNhV2N9bmLgn1HsPSC1Sa/o9BRRUKXBFLxv1" +
                "D2yql2oHW1oH6Rp/YBVSFfcDqxClZRtZFE4R0Wwqey0R18OhgHtNO1mxwtN5Qd4WIhYewM0Se5m/KK9f" +
                "Pj/WN5/68z92QEYqWjD2X1P7r7H9V7R1c4NUOFYhm0roWlENcoR11sUceYoq8VS/xo6zFNwQeBNxpydd" +
                "hAT4xw/ABcl5KC8/3T3l9wxOPtCDU9Kz0YIE5YwrYYDYIZ8ItC+NaVeKWg9nF9SuM1YmFchYT+oI+onX" +
                "XqHK5QwBiBnaXQvQVW0Taf80sshWoGw49KTjZ1646y7dtsPLOY+LOF4uQP7uoRghY4+eRHm8UlTsDsb1" +
                "4wFqCmlm9tg4YEA4xkkWoX/dqaGFBkalu/8JH7ylofXuyTUw37P5JhgdxXr40i0Hc7jyHAbaDYHIMipQ" +
                "doDL/m5Dp9yVSw9zVTu6/zaYcQmXiH0s92VaK+FU6PX4BqU7nJfP8oUh/2txH/7iGvodlhW5IpbV+kfX" +
                "5KNz8rW5T7OajbPqtT4JJ4Egfsc/sNrD2cXLQP8cBfvwX6+I3gyoeWXzcBZlEfOderHAGl++EpjHJ6Oz" +
                "N8PAg/mkCRNVQL4snq2k8v5HAb66Hg5fXY2Gpxbw0ybg0sQmxeT1CH0FsbGftgqiCV4kTaWmuXWYoiIU" +
                "vOcP3tVGCkUs8LfVNHSckAXpvIO7I1POU2T55Ina0zjc65OT4fDUm/JBc8r4cTGbc18tY8QCnvpVJyI2" +
                "DXP8/PLa4QWHedYxzJg+YbHmF+0eKVmaD6LGOismUZqhA3fD9K6HWMrCze8o+Gp9eqVB8bqBAmxVhha5" +
                "9D88R03uqP3BljnmzKSkewdUzYi/FrFhAUJ59qQcBV9vgfIs6aE1gYfQEZ/dPIvhk+Pzc3eSj4I/f+wE" +
                "peZ11ww/BrtSKaO5W81J55O0tOVda58L0ExM0liETybf/AsW8XFoRqJoHD8eACtjb6CJ88ubkQ/qKPiW" +
                "AB7b7wHKhxLRbZlgdY45fzqCatwqChBKM+ED8Tb+iLNH+WHFnZEvWqWl6foaIUhnMletjbSUgLyrExpJ" +
                "bRTW2TyhRv5tM15Op4hGaVSbd/Xn+fSfiOT1r/9J5FVDsNv/aJ1OoUe7z7neMahLmEpARUvILUwffIQ9" +
                "uy8LtJO55NkQG5+gasX9QuznTAxSAKvmB8UaoQwtrFt7FXH1c3TN737JQ76A6EawKQCpXuCwIQu8z6mf" +
                "xOJsNmE6AtglIUhRLlAyQx/exlgVfh0To7n8RVfiaex3dLcQPc9C82NHWyI9f2skEkfpVuTbuqPP0sIx" +
                "xRQ8NKq5AUn7mxtMw+EHL47Pzl9fD4++xT/Ylx9fnR9fXACzDvH98PToke1wdvHm+PzsNHx1OTq7vAix" +
                "4dGjp/rWexpKy2MQq+Hzn8LhxZuz68uLV8OLUXjy3fHFy+HRowPtd3J5Mbq+PLfDPdMXry+On58Pw9Fl" +
                "ePyP12fXw1DyhgDs8dGjr7TZ6OwVjHL5enT06Gu7BlXGjh79WfxELopvq27bL6cydVcWU6Pj61EI/x0N" +
                "YSXhySUIrxtYG6Biv6vNm7PLc/j7Jrw6Hn0HzS9uRtfHZxejG+jwxCH25eXxeRve08bL9wE6aLT03mkv" +
                "3KlnbjTdrJfXl6+vwovjV4DzJ1+tvW0BgzZft9tcXz6/lKXC6z+3X4Ng/7vC/6b9kuuE6etvxdvFddOb" +
                "SH9xDW1CmMbFzYvL61ehUuejp08cpQjigIiGJ39HGgUaeQMNkVCgpcWmN2X8L720CBQyOrt4cWlfPuOZ" +
                "eaTRnN3FZXj29/Dm8vz1iDbu4MlWznqLk32woCdfoPUTp9o9GrX1R/7X+pofePFhbOly/4ap9RoV4jUd" +
                "BZV78T12xuw0T9PlyxFv77jAr9fgq3WPSFdtFqn+/ijRDwH6sHa5ALzWvm8X28ca/UPXoivVjbN65dNv" +
                "pDBpwpy99v/YXfZ/3LziL6GamdzXoKCVVGeSRxaaV3Kgb1V524kjXe5Th/BOojh+qQICNthQkX3Dbn6e" +
                "uhHvm4zuSXtbUTNWEvOjybt4fbQAGwvg7X1c4fme1suTBP/I1QR3IS0lzebFKp7LhtL1XtWHtSHyTR82" +
                "/CfGaVLZZy0cscEj9kI+K/4/9okpIPuh8s+xJrsaPVusXPf+GwPDf25lggAA";
                
    }
}

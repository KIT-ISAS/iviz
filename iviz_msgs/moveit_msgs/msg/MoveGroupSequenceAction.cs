/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupSequenceAction : IDeserializable<MoveGroupSequenceAction>,
		IAction<MoveGroupSequenceActionGoal, MoveGroupSequenceActionFeedback, MoveGroupSequenceActionResult>
    {
        [DataMember (Name = "action_goal")] public MoveGroupSequenceActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public MoveGroupSequenceActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public MoveGroupSequenceActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public MoveGroupSequenceAction()
        {
            ActionGoal = new MoveGroupSequenceActionGoal();
            ActionResult = new MoveGroupSequenceActionResult();
            ActionFeedback = new MoveGroupSequenceActionFeedback();
        }
        
        /// Explicit constructor.
        public MoveGroupSequenceAction(MoveGroupSequenceActionGoal ActionGoal, MoveGroupSequenceActionResult ActionResult, MoveGroupSequenceActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal MoveGroupSequenceAction(ref Buffer b)
        {
            ActionGoal = new MoveGroupSequenceActionGoal(ref b);
            ActionResult = new MoveGroupSequenceActionResult(ref b);
            ActionFeedback = new MoveGroupSequenceActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupSequenceAction(ref b);
        
        MoveGroupSequenceAction IDeserializable<MoveGroupSequenceAction>.RosDeserialize(ref Buffer b) => new MoveGroupSequenceAction(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "146b2ccf95324a792cf72761e640ab31";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu19bXPcxpXu9/kVqKjqiryhKFmSHYdZbhUlUjITiVRISrbjck2BMxgS0cyAATCk6Fv7" +
                "3/d5zkt3AzNjKbshfW/tVVKWBug+ffr06fPejbfVdfG6rhZXp8U/FsV8VOyN2rKav67yaZbLP4cX+Pfg" +
                "7ep2J0WzmLbespZf69q+KorxeT766K0n9nuw+y/+M3h7+nonmwGLsh3Omovm8a/McvBdkY+LOruUvwaK" +
                "27Q8145scbifkQTDcrw8M6GTEOhuJtG0Y0VEsRw8yE7bfD7O63E2K9p8nLd5NqmAfXlxWdSPpsV1MUWn" +
                "fHZVjDN5295eFc02Op5dlk2G/18U86LOp9PbbNGgUVtlo2o2W8zLUd4WWVvOik5/9CznWZ5d5XVbjhbT" +
                "vEb7qh6Xczaf1PmsIHT8vzGaZIf7O2gzb4rRoi2B0C0gjOoib8r5BV5mg0U5b589ZYfBg7Ob6hF+FhdY" +
                "gzB41l7mLZEtPl2BqYhn3uxgjP+tk9sGbBCnwCjjJtuQZ0P8bDYzDAIUiqtqdJltAPN3t+1lNQfAIrvO" +
                "6zI/nxYEPAIFAPUhOz3cTCAT7Z1sns8rB68Q4xhfAnYe4HJOjy6xZlPOvllcgIBoeFVX1+UYTc9vBcho" +
                "WhbzNgPj1Xl9O2AvHXLw4BVpjEboJSuCv/OmqUYlFmCc3ZTt5aBpa0KX1SCf3hE3rtwcwlqGbNZcVovp" +
                "GD+qmigrP2VYy5vLEgsik+B2yW7yJqvJMA0mQQY6lPUWlgRJ8rkNhkWur8EaN5fFPCvbDBMtGjIt+KKY" +
                "XUH0TKfoTZiNcs1NgaED6Oy8wP4ACtmoqNscK0eMUvoa/uXY1wTkBXpYlirSOXNhBczG6KGSDnuwafKL" +
                "QhYha66KUTkpRzpBw6DZNujcINoASM0WTQvMMuw6tNr29ePK/RbSUOQghBsJ7s9OFH+fBxB7N83nc6B5" +
                "fMV2YGH7Paz0wb1gvgJFUPZNCUyrCdoKzzhmjryuT55hk87Hwzofl4tGxGaRjy6F+XQnY1WvKLcIir8F" +
                "DpgPPGaP8O6mxC+sXfGJ8k02Pni5moOfDK/tHikP22L208/g3mLW3M/69kc3Hud8Ak04f9mPxCvuwDDv" +
                "lH6hbVNczCinjEA+4y2SoJxwg8r2y7OmmpYtJJktiZOEPJRwlmy+StcFNG1vCuzyoEeEYFsykC3sDNsS" +
                "pG9mVYWnY4gLYom9Xdaqmnw0na/CtfU2lNkUS+aDyaywp+XdvPjUijbEgy2KHEzlCTZ5Dm6fG5oYcHsw" +
                "mVZ5+83zDkPd38omRHS9TilZgmTQWBRyuozjYlLOSyEcFzAPywnJFokKEIHPjQ7Vor1agBvaIBK5UO9y" +
                "qpe2qHXvsOFNVX9srnIMLKI33TOmCdgC6r8BkMH33jqBFCAMr8LDgZo5sDeoMVuK38UVVB/smOxwEgT5" +
                "3yvYDY0PBLagzYFx6oLCFuhcVY3MHmsPzIgx2F94YFHXwsXzQvkL2zo2VoAAQU4rWliGdTE4qc6r9lRw" +
                "AUZ1OxS8XHmgc1NSEojol1eRSLNqDKOMKovbD0+3swPInayYFraXaCagYV7X4G9ZNVFCpkcuyNEyjDwg" +
                "v44uS1h6xBGbTmYExNs6F4LoWif2moIAeOCet2VDFTR4GXtANomBmwDhxI4qIz5Imc9vMUm8gSyssCqy" +
                "0jmEKhUq/11X48WImi1KYJWU12U1FcNSqJziuaF77+pqaiqdYlYGwaLMqzb7O7UkNLE+20xRlsH7CJNE" +
                "6QAm0qGoyUh4+vdiBMMEMomAlRS3g7PwPIUfW68aZU6LzARKqnPIZBWNGGFUiAxpyP3pqn+LrMk1zsV+" +
                "6PZ11UIryG0CeydW3fLgF1Tj/GGbAcASS8vh4p/VFYx+0sHhSs8hYQW4i9k5GhMy7fAIQtQCxB+pO4Ng" +
                "gMGWnV5WNZgcZKymCxMjjn4N67sWs07tfAAeBmMhbyHVryIpZ/mncraYZfmsWphioW22grJklum0ulGn" +
                "xTeTWONmqm8GwWwN47Bi9VGuwEIXUxnmZWWSTO0XcYkgOxYj4ZYENaFtBkcGtmBLXlUbMB+NsIVJVTDI" +
                "dpbtGXLX+RRSVrYbUNt4svXVz3h7tg7g7QpwialoG6ym/DFRQsgzcjV3jnkQJQhE8wU8C5cL4GyCGBmc" +
                "CAVCAciOkO0UkgRa1sQVhm+dzy+A8UY548Ll83Z6S7FYNuL8jaYLGsXnhYhjCERQ/8n2k03VzDqO8Li+" +
                "Ms0j/C2k4Jp+tf1EYEG8K6E3yu1ie2sdRQCw+yYlzmbUv2g09E7DRpd2qBh12qTd++3uQ2+v0Hyuud2F" +
                "CJo7F0KCZlEhUiBMFhD93GV4K/IM+168T+w/7JcNWLfVp02yiwsAZ5ruviFaaTjAQMOtu1UnKxh/3Bqz" +
                "6ryEWqMyEQvKNJUChs13U0yn29xX+6K1lCPEX0XjuphAddKSc1UIFDHRGru56cVdXBqUEBZYdW3kUg7d" +
                "ttSGNxZ3PlM8RCGLrITPPrgoKlANclso/0GE+DMCHirQvuD51w8FhvOh7oa1Vo7rawrpC9RhWJCVruUd" +
                "uWICfzkTHtzOgrEvq077VqYZelJDlTVFiVketTjRW7QIxxVkAbQzYMzyj7SQ6DBRe0ORQ5ZRc86bqYox" +
                "PEaXjWL7AntduEtaqdFOt52xKHjMdXkBMSQ9MdAsdM4zmxxkzeSpKhjBWQfDutAXr1qTCxR1t9UCNgPm" +
                "gH/UFgIT3eV4icfWVtUWWd9AdAn6TkSOb0zsyRY8Ci53ifIp/Os2/OuXe5Ai0QZdKzzKeaRffg5Z3+Xd" +
                "lmsIwas2WzSP6bk1bkPQC4CA/NgMuLZVrYP/mS/VAJZ20QCWNxZ8oYN2mV8H/VRk+8ev1PIMGk3s6RT0" +
                "W7ZFu2QI6T4cV5Nhb7C9toX1CyijajotG86zOqelBvWV+zsseIMFlVnQJAo02Bx4/5fe/Vh6wwT23ti7" +
                "9mpokDnuq2mO+AzcP0RJyb5gZvMpqPlG4ONoJYmaw1Zi+AkykptpMlkSKoKhOibSP5HJs6qBCs6COtYQ" +
                "r8cQxVXwqZ5XY1okG44PGtJ0ZlR3WsD2X9EYA2HBc2OtnR3IqmJnJ3FwLGQmPpeEpSyA0iYstzk4ryo6" +
                "I0NO7q4E3WoGTCiVhy0g7HdZTRGw9U0Pq2VUl9z6Gp/E1GTipl8hV+AOy96pseSgj24AyscY1NRODBSZ" +
                "KbJRF9S5fF7D7ykbbrcRNC8GFiVIi4yxaoSU023mGsah5GMxcza3YtNo//SbPm6k8WMEuLk9Y5digrVq" +
                "Q7xcvalgNRqAoxl7H8Fw4sTE+dQ31P8SgiS3jSH5dYfCoqQ8EOdACaFaOmziZmVs18cz/9cCsWAvxLm3" +
                "aR4vtdHkAmNnvowiPHQbeVcOISsoYXCMqdPoC8AGMUARZXkUPuJRw5vkCB7vE2OmS2FBRtRi6mXTWKKG" +
                "k07WHnMnh7nHsJ19T6VG/ab6xkSozGJeAaCtTy9gT1izLdFVI7gx2KmcdVxOjQTQT7o1biT1dDa6tsnc" +
                "LQCilHM6NeUvkPWYMiOVAifZNRLjEYNPw9yBB2K8OxJH3E1HmtYSDQPaobKCUIt9S04dTEhU4R9TkPjp" +
                "GyF55AyfPFIK3IdAWVY7mNWJ20CqQ7HsKgPIPba4QovAYuPiApaVMB5NrHGFdaX7BYlS3bi4BkkWo3ZB" +
                "92uSxfGUkdUUA+lhxzP+xnCL709m3ppbGCBip2p8GvwnplEwl7TPRdHG/U9/MgRqPkIoiZDKRpewEraz" +
                "V9wKn5D8mDJyKxY/VIUJC0Zy59n7k/1XItOeUYFvwCiGS3ib3zClpWFCGMD6khws8fCYqkuxU0Lir7oE" +
                "FO3LHd15L9YkWzg0bGikfSSBw6wLJtzB4f+LsfsVYzf04y6/WIx58/+XxNg6KaYGKLs3PYfvzFkYrQI7" +
                "LzW6gV5iA/7de/e9kAkvlV734y4GrFc4jKLVg1gJOZKbainn3/R8ysHA3d/E/xv8dYEO9ZwCwL20+5lk" +
                "HHiVWwy5jjSyy/jORPjrHxFr0uGznp//6+aeFlA4yablUreJxmN3Pud19ZGp87n44g19IvoFlMOI+kl+" +
                "QKI722EBrUn8be3uZ3a6J1asGpZClydObgv6nFEyyiNOkObkl01RgMWf6grcRyRwjf9pMrv3NETUgwdn" +
                "iexJ+cndFY1PgVR0dj24z397bF/o2MkLIu65YJrqMkdpkJCJ0ayYsFrCTe3x1L1mMxnwAUIuVqkhy8W5" +
                "CkgkP9R93kZkR6PD0P6M35hT/WAVPHex1QkJ00hSCxyA2asuoRSoh/bMwZMgRsxMhvE8YMCwRBIZbauF" +
                "MB4G9vj1o4CYoiGhVFQyjW+TZGPSIWoNATbUMAptvJi2VKzEOYLhxZxZv74KqyG2Ysi6K8IaopsRRpgK" +
                "QFt0DUKNyW6YzxryG01hbjPOhjyPbASDYmF7H2NejKh/kRTjaDWi5FKmZXFeG5jLx4oei90kubHomcc0" +
                "GohB7IY2hC/KTYEatWCx9BYDYalJ9nFe3QTvwNrfx55c3ot7ZgZIjHAspAlBHbfpZc+sjmKD422aRsAN" +
                "4R6BhdVj/c0hYlFJ1Y/283VGyZ4yBSMZ57kWpAh1wu7Rv1FxOaouNMevc9EpnBECwcS0qEfLTNpKGmvZ" +
                "mvM9q92QIFLJwJ2yKiieBvWcAKeo/BiHYUYMkcyA4DWIJRJH8ZRW7/wVvcPYrB+NbTrvyVdakPC2aC67" +
                "UPkEbWf6YiUcvosgXnBzeJqVcTRUSJhVkCZts3OLpEoz91HUiIhlKFix8VjWAtKVQ2ymuLFqQyYiI62Z" +
                "JN9F7PbG9PUSxlCqhwyNxA0l6p00Ao8i575oYAcXn2ApEH1E8FSZisDZHpzfwo7b29/ffcJhTkSodkaa" +
                "1BXdShjd8+uyruZSqMD4ECQEkncoPqhRAKlbQcK+LTazQkhSl+NNHenk4O3xh4Pdr2ROV1eUU/RgnZvN" +
                "5zXBKkibe/C5uXoyQjv5PLEKcZLv3h0c7e8+NSEcx1w9nIyC6oPixjjfllqyIihYgFtl6+ZujJfyTYtJ" +
                "qy4KXWRIMyTGSSuQ1iVGlKaITIKSY0VRaPOMCB5rkl5zH4CJnzRAvaHl8O/Ohv68UBk8+Kf/ZMcv/nzw" +
                "8oyFv/98Z/tD4rz89RyHCE2J/k1E4ZkggxhjoIIuDKyCppdXbasLDZsH11FDuOATRsk7RsXHIsRl0xF2" +
                "5In2jzFUSbkJu0BizbPxuQt7QHGA4/MUFVOwEjv58+nx0WPWWlhA5ce9t28YQ2LtWbYXWBhiNmyAJBdH" +
                "Qe1UiUEjVequUFCCJFYDo69Liy77SBz6qvoIe+VjsZP97v88JIUf7jx8Sctm/8XDrexhjTI8PLls26ud" +
                "x4/hfuRTULt9+B+/0ymyAkIq5ySaMzfJqKtn1g0XJ6ECLceyfYhOrGbDLvhYFFYNPpliqyIbDRfH6/tW" +
                "8CuTGUpEzy3uv1DeECCcFfe9jaxxEDKXVYRZVExqzRkls8lKOF/A7GSBAPKMJMCzPgl2vv7jt8+1BVWv" +
                "plLRbhnjhzbS6V/fIH0BE4E5jLBOnYFP/zH9zlsobBkqe3hz0Tz7Rp8wY7STff382VP5idY1G8B8rm6s" +
                "BdQ+qu7Gvce0UDgRH8CTX/oWhSeLKd9L+rStrh46Q4O17ypWu85aiOUGkq1vrshpW9noFqa1GG1gtyKz" +
                "aJN7OWALT8+ArTzKBAvn3E0AAKPAp0qXnaiG85Mt/A8hAJ5Z+DZ7cfwD1Jj++/TddwcnB1At+vPlj28O" +
                "j/YPTiDK7cHx0cHuc9/tLp9EyxAna6VWmosEZDfgVlhONjaN4fHYItQxIH1P9NMOSbMdDfxJERtTiV5e" +
                "yLYk1yeXVA9jn4eq3CTvaT4hJi6oqvfww1b2o8Zy/5biTCKLw1TML2AsesVtTwbRbQrzA9G3I22HP8Ai" +
                "ib9+DLTmr79RiycoKf0NK4kDctkpNvG3JbcgPRVPSDSKYisK9Ipgc3OUgxwPhTs82ds/fH9KCykZ0xdZ" +
                "YHKB9XyNUkVZR8IPUlzh5qFE4m2ov2UoO2FVVqiq6MAdfndw+Pq7s2yDsO3HZpyTpm4Tisc5XXbcK98L" +
                "2Qb3wqaORznn4+jsbBz9kYyzbhRWW3Sqqd05WT0mVLYGA/wVa42Dnd/fk4z6Wx23lrsiwRJ5SGjK/nQ2" +
                "tf5py3Icvzei+ibtETOwVG/ytEfjTl1qHAnDhncj4padAHE+66UkFI3RfuxLVovmgb7XJDOpnQQ3EXbX" +
                "apaQfEtCskm7+5ogUPHQXicklSbJEQvx5Fec7mdCsHevguhauuJJUKU7SQEB71o3X10i5ggL4k+JhLWK" +
                "UKmHk9rVUHmDOTLNBWMHVdgDjnFmACSnYLA4QBK48x7ue8Hys2JAwWYFzSXVqp3uiVQ+jRUk82nBygtI" +
                "6YnAn54pnsWnocQB7wVb8ctXZn41M4qdpv599P9DkCD/lP2e4b/fZ6Nf8J9xtpuJR51nO7tg8GLy05Of" +
                "GVEMP7/iz1H4+ZQ/x+Hns59DquGn5z/Ls7siwGdieL241spkWK+LM5pW6/9GeLuEkRqXpOJfJUosX7GC" +
                "6LARf9pK6rvxo1Pb/TNXqeq21jKFn0PMPBlLVZkeD9OzjWKHhrBItz48pDpZ64KkRdPLWYqM6M1yG1Mf" +
                "rKisaJZLK1jvFR92prVcdDFeePgBun/IGBAL7+p7CsLGUxbrS6Onaw76JWc0nODpSRAP0oTjL3ZIRnLk" +
                "frQhxPOlKN4zwFrFoswecQxboXM45J2tQ6edL0636XHUvZ3WiU7udviA8K16v5321+Fxt/ndL1iPIhqh" +
                "0R8rVHrIVZ1rgELC254SERfM6R1FjKs9eW19/egTDeyfwhDI0yDahsTzqBieg/FvtuLwv0/eIVx0Xfwc" +
                "7IZ+RVK/5YrnAl3imJagiIdJYr4mrkS2gYhQxWo4BE6xqvA6vapT4xtaAJqyb/YSmRu1E34p6sqOH8IK" +
                "aGJB6GY/S3L3y73M22u3qZ0A6Sj9sBxxqss1F+rQarWyZAP71BQLimG7VYlODQlNJsz+8VBQyClKwcJm" +
                "FNV5jaifqYQqaXfDgsEk5IwOa0r8FQQC9WRVHdIRsPMYazHPBp5z+KAtY6OhHtT7v4+77oG9ukSJEZ7c" +
                "CYoFfbavpLH0brM20bRsGPuqsChY4LLMLWfYtppvfmleylbYUmJp3WvPqWUYEKr/ixNZhzFzpFcbOLQt" +
                "S2WFNEQwIpg/GYfUeYhhhdNAF1huOZW9PMVukmz9pGzoz04ozajdPaOsVJz/jCjqFv98XhxZrqbTy6IS" +
                "iaSKKyOMFUuIejRb537/N0WfV/JJIOSRuFdZUdcUGa66knxmPCZ5Lsc3i+GnITsOQ+PlFrefbfFLv8X/" +
                "RDm2ykwLOfgw33juTqoz8YhmULTltMB3XDYjTTqqvtlcqhQJ1+8I90t7qUHsxObyAFiqVmB/aUoM0TUm" +
                "SzVRBGa8YDLI73VgmTQBvzXkRLhE/DgaQcH3RdOFMGTcESErVZ1r5bPK4NidaBD6ERZ9JwkIOX0EwNHx" +
                "GYBr2dcMGPCwXnIwFLMV/miSuxwc81Ck7KTjgfJawSIS7FATa8BP7sTyWNLiukQiOIaJlSpg6mVjSCT0" +
                "htRSgQZIktxaQeumH+gWxm+QOsquFrWIy+2w7TsxVVlG0WDxagPnEXonJCO2dzRO1RjxWyiiPE8B/sk9" +
                "Sz9ILNrF61g6Q/MMFwrF55CjdsIzrJBqIaeSn/PsXjYQRNiWHrnQjNoK1XFq10wFYwrRT4bavfRAye2c" +
                "2VY3eW3lEL6mAWVl+rzPYglnae3WFRYH3CMlLxrUmfFmA8lI9Mri5bSitnmuDaSuXw/azEWYiYWhGCUD" +
                "220L9FWyq1vQqJQYgCCi56FtWfMp6pbJvdkz4WvkAvzEPBEeyqCd9QyHe7s7UXk3soinR3WBYCPZgPF8" +
                "Z095AUBU+0Z2EQ1ao7W94pBCaBfyxHpilNXXbWeRKLNA43ig1IkhwERvqTxEaBO95fRavSi05iYpqFne" +
                "cwwUaEIXGkjyeVIX3vBGpfXsZkdAnN0OmJ9nwvziMvITTYmlHaf81mMx3xMsiMmamd5GdV6Mch4u9SKw" +
                "ZctBRllZUWUyRunt94oAZrKX2FYkk0H6CFbikUGt2smFW4Xuuqgj6oFVqBv5WI4DMxkpUYmkUK/KEX8z" +
                "JjUXI+OpXZEcdLOTtQxNyyuTxCYvIXJBEJ9ZMrrM3gV0YAw/rDxZKQCcqXULiRver1x3JeEWW8RZ93Yu" +
                "BTUkNt/w3J1wjEL0CtMnpnFRMKL4qAUM0rPM9Er82KLZdIhMwiAhztIRV3Y28mrw78rHT3WEFDrwYlWU" +
                "ilDAToRFOFMoM2ZpkU3W1GCyMEtywO4GClokN5gmwFFPJIXhRY3HrCR9siX46UHqJ6Fqt11a/0Q1j3u3" +
                "MaDdUCP/YZ97L9GCDXwbTJKnt6teD9nxU/o+3Sl6ImFZz7tyWOIhZy5apFb5Uc1R66qQmYTuKZfUFjDF" +
                "pmyXMB3nQr8q3MkhOjJQzYyOHnmkFMwvzeiQSrqntErRTYVtFaqiexvGLRSPbATj5wckIp4iR4+/vtpC" +
                "zplpCUtuHxydHp8gl957EFPt9uCHUNhgAlPWKYz9P8a4X6tIhAD87XLcL7voH5VRZvSrLXtOpgBQXXT3" +
                "zsnKq46c9eSsFjiqc4NSFeLiyXnCmOvo3SJ1zxHo3oWEsYhmMulg7tujW0uabbjTvBmuNjyVF+HSIGln" +
                "B9ylfFRYeyJXAcT7bsRYsfSsHjhClfiiZr5int4SqFcp9W4apMgL7pLdjWSduMMzPWbPrkOWuPwTeEhZ" +
                "gM/4f6GfQWXFeedGGLHerNop25ADHLrlGzic9H3gMTVFMTPJJOW2c7GX0kpMwrzOIdrlQkSWZZoVkFQQ" +
                "62Q43lDHS6cTZKyGiq9/ZTJxKqoP10xGczyuReL1Vn4F1Z989ETASxkv3B4I9eWLjGJFkVAd3WXrj28R" +
                "hilHUtU3n5QXPPSgdnwy1c7FVxiVqybqYpK2Eu1uC5nSRFRnzr2aKsYRjT6fvF/7xkL+fkYrcqHY8l5T" +
                "o7k064RR9BZhLvM8GR9BSJYpkSekzqyW08VaI02WnheFHPAH2EC97SeqzFfPDDrcZtKlL2lyXearCOpE" +
                "6GjQJp8Uw7BZkADjPaVhfoIcTPFKk6iS1RPPD2lOTiR09LrxcIufFQi6u2fXZvrlZ7JscYtiKzNHGswt" +
                "2ZbFnMTWO9RovC/mwsa5mL+6DaQaP0T+lrlU3wvDO+tkxlb6aukqtf4VavDqcs5sHiynVTBX3p9mI4yh" +
                "sW/vU5SLAMZ0/I67rsjupmRJVhjVyeU26S2N8rh7S2N6cV56n1r3DhdmfWwchSGNPNKaOKR2Dpb7bry4" +
                "mvrNMO0k21jl2cW0kqSj1p0lNusC2lQO+Q31TufkePHggUzbt3082aMH6Ad2DV041PRWz9X7jXjxdhtr" +
                "z80MeBIIuOL05hfN4A1+vdMfwERCzfau055XqRXamhfrFd5Wnneu67Fq4q3le3tQS6EuONYGRsUsR9Ff" +
                "3ZnWlbipUrJLl7WaitsUDy8pgWe2/9SQ6dxUMtse6BEGEKWqmWXXMmwF5WnmzpBdk+D7qta7TKfje7oM" +
                "6D5u1/l1Dlw+YZxeOSFO+pKlq0btdriH/IEz/1JLpPFRYx4b9jM48RYkvxKcYNDq3/LsErb27u+s5P6m" +
                "/Fhu11WzXdUXj9vJ7/69nfzb4/zfwcqjjwAk1yGcooqQTuW4GsGj8lCM3m8nVTjBjFlKApkk6KKbKcOE" +
                "+JoXqbKRPuWton45Rzhvfx/Hi1dufpN/XjwDCiBYFYqL1KKQdqHOS5pYnZf1Jr9fl2MWIPJtGTuvFUVw" +
                "s5EIY21+czvTLK1drteteEpH68/ggO8CRlo7tSJn2t/AIvOw5sV0giGJY2NV/z3LQ4mh9ke4IihSaDsx" +
                "KJbn6Sb4PxYFCBJtsFI0uFJ4Yw4XfC7hAffsaULrGSKj4FrMo20ashDLoyeGngfkMI4xpU4FSMjVYILG" +
                "0uWiwhgSYBIrDkrvMkMHOcb9VAJUHWyluZWiKxUtlWC34gtKZivpgfV5tne0n55fC4xmAIYpC1D2Lb3y" +
                "lb//PSQcSFu/e8VaXAcouNFH86vU8Bv7HPznPaCdqOzBAzF13O9ddzOAq/t4e2N6sj8USbnev58piB3x" +
                "pROwq1x/fQJmjNw9+omN4dVI4aAep6LheAgj3YCMwrPp0jHwcGuubXdpY9+BEegnr1/s2Yu7/u5MGE/J" +
                "Secj/Osi/Os8/Cu/d29BbDO1C7uGZf+SCAlQrbja8SyxPEVyplfFRHM/wufhvIH1sJXXH99D2ElAz17e" +
                "2TndXxlbwpLP9sVspucHo0svd4BmkcAF2tdFsbrWJc0FYxCevljlythtgGr/rEi1Wew8+KV6bMEAsnZ5" +
                "1QR+C6L9l4klHoBUkzG0zU+LaNcNOYfGYyuPq9FocQUlu0mFIS6bPEHU+9ZJsbF93j7epjFQTv1GTQUk" +
                "B56n8HFS81KLmuhMafeu5DhhFlRDEnTpZ5uhmINZSV7Vbt3m8GdD8bcmT/UGd6ZYdBoIvpeQrL+EnKV2" +
                "1Stz9Wo2ORm2LbEmOVBLY+GmRm7M2jaMVXxLNc7d8tt940u/UPb5r3wxaoAaDAYMUEWxBMw+dGZfOLub" +
                "2azFatD7DJnlZfSd/uAtB4dHrzP/g4wP/pvcBHcJbr4NhS6og+C9L9Gz6nxtyWDuvTw7/HAQQDKZ1IVJ" +
                "Q08PToPp9X74LwL87uTg4O27s4P9APhpFzCCbQUiwOAquv4jxGj80yX5hEcsGZHWoFsSCo6ILv/hGWZy" +
                "KKmg3/PyxC2PhjZJOG/jrKiRSAYSEkfa9KzY+5cvDw72E5SfdVHmB61CcTrKfEgF7vrblYRYN8zei+OT" +
                "SBcO83zFMOfyeYWlOObqkcaL4rOkCSGICSL4jLauQe/kgFc5RPx2s6+X0asLqtY1HBBuJ+ixy9bncfSq" +
                "ijYdbIGYAqWipB3kmh79pMGaCRjnhZ2ym31zD5wXWI9+AzdhZL6weIHCL/fevIk7eTf7w5ciaNc2r8Lw" +
                "S6hrN0Z0V6uLNGLxvI1EraSwDFoYxoNF484kUjb59l8wiS8jM5mis/10AF7vvIYn3hyfnqWgdrM/CsC9" +
                "8A06+zgfo5Bj3lMBIOZk54EEhNItuCDdzr9g70lFFvSNfVIJGfZVX8CDdhbHNDhFvNqh+406VP6oQSA2" +
                "W6LUJERdnC8uLpIPwLTFJ37V7TdQzKaSl784ZzlRT47e/7fSHAXahXMroB7BXGJ2Xy7xkGCvfGQQa3aD" +
                "tOaFmAqH7QHbvqRlpd0QWx6HxINYf+HTEeFS8ZiH8Buu2aT/bbXOp6fsmR/Ls6slPB+v4e7eh7jCp5m0" +
                "gszFjUCNBQF2+RSsy2EKbk1qiR9iZKJVPx4qokzDiPFYXowfdD/Bcx9L2l0QS5qxxolBK+wl5i/OCxa8" +
                "wYPWt6LfT3nUX3+/2jt88/7kYPeP/DOwh+/e7B0dQTYP+fZgf/eRtz48+rD35nB/+Pb47PD4aMh2u4+e" +
                "2svk4dAa7kGHDl/8ODw4+nB4cnz09uDobPjyu72j1we7j55Zt5fHR2cnx2/CWM/t+fujvRdvDoZnx8O9" +
                "v74/PDkYWsEOgO7tPvraWp0dvsUQx+/Pdh9949i71bX76A8S/Yn59HBDdPgop3Kx0+70bO/kbIj/nh1g" +
                "CsOXx1BRp5gUKPBkRZMPh8dv8Pfp8N3e2XdofXR6drJ3eHR2ivZfOTFfH++96QN7mr77NSjP0obJK+/E" +
                "tXk+6K3O65Pj9++GR3tvQeWvvu6/7EFCk296TU6OXxzbFPH2D723UNp/ceDf9t7pFVj+9o8SutJbvTtk" +
                "fnWCBkMgcHT66vjk7dCZ8NFTZ7RALLDLwcu/kBfBDx/QjkyBhk7BBFf+V9450YxhDo9eHYd3ck9NwgYd" +
                "vI6Oh4d/GZ4ev3lPTgaL3tmFGEufO0mOpH/u2kk9ONqu79C58D3p2P/YSALink6xr8HMhK+FW7wMhLa6" +
                "BRBXpta89jFWpInMXnFS3c97r7jGctUlJPZhhUeglJ/njbA29NsK/lmJ/gXwm34ov/8VwQSE1Mja98bE" +
                "/vGStHC+/XE81f64e5bdkiu0V0V5khB2DZE9CtCSs/V6N1Cnkyal4hf18M4SL+mZfAGG3NDKixHWrOZv" +
                "c0HCryHja9JfVhq6zmJp0neDpykruEyAt/lll6ErC9ehTj6P91THNJSzZvcQkuKy5jr15HqDpSHm676l" +
                "918Yp8tlv+kNCW9XB7he2Zep/9shLgcUvnX9W8wpzCbcv6AlPP8JaEytTKiAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

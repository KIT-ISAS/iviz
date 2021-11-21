/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupAction")]
    public sealed class MoveGroupAction : IDeserializable<MoveGroupAction>,
		IAction<MoveGroupActionGoal, MoveGroupActionFeedback, MoveGroupActionResult>
    {
        [DataMember (Name = "action_goal")] public MoveGroupActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public MoveGroupActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public MoveGroupActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupAction()
        {
            ActionGoal = new MoveGroupActionGoal();
            ActionResult = new MoveGroupActionResult();
            ActionFeedback = new MoveGroupActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupAction(MoveGroupActionGoal ActionGoal, MoveGroupActionResult ActionResult, MoveGroupActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MoveGroupAction(ref Buffer b)
        {
            ActionGoal = new MoveGroupActionGoal(ref b);
            ActionResult = new MoveGroupActionResult(ref b);
            ActionFeedback = new MoveGroupActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupAction(ref b);
        }
        
        MoveGroupAction IDeserializable<MoveGroupAction>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupAction(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "be9c7984423000efe94194063f359cfc";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1dbXPbRpL+zir/B1RcdZYuMu3YTjbRrq5KlmRHu9bLSrKTbCrFAklQwpokGACUrFzd" +
                "f7/n6Z6eGYCQ7eyt6bva825FJDDT09PT0+8zPCqus5dlsVzsjuq8mL8s0mmSysfBJT73jprvz7JqOa2t" +
                "RSnf2m1eZNl4mI7eWquJ+36vt/NP/nevd3T+cjuZYfy8Hsyqy+pRx3zu9b7P0nFWJlfyp6doTfOh9mCT" +
                "w/2Esx3k4zAZIQWffjK8q3qsKCh+93r3k/M6nY/TcpzMsjodp3WaTAognl9eZeXDaXadTdErnS2ycSJv" +
                "69tFVvXR8eIqrxL8/zKbZ2U6nd4mywqN6iIZFbPZcp6P0jpL6nyWNfqjZz5P0mSRlnU+Wk7TEu2LcpzP" +
                "2XxSprOM0PH/Kvt1mc1HWXK4v4028yobLescCN0CwqjM0iqfX+Jl0lvm8/rpE3bo3b+4KR7ia3YJ8vvB" +
                "k/oqrYls9m4BFiKeabWNMf5dJ9cHbFAnwyjjKtmQZwN8rTYTDAIUskUxuko2gPnpbX1VzAEwS67TMk+H" +
                "04yAR6AAoD5gpwebEWSivZ3M03lh4BViGONjwM49XM7p4RXWbMrZV8tLEBANF2VxnY/RdHgrQEbTPJvX" +
                "CXiuTMvbHnvpkL37L0hjNEIvWRH8TauqGOVYgHFyk9dXvaouCV1Wgyz6yRiyc2eQLS8wB1266qpYTsf4" +
                "UpTEWlkqwXLeXOVYE5kHN01yk1ZJSZ6pMA/y0KEsuXAlqJLO3WhY5/Ia3HFzlc2TvE4w16wi34I1stkC" +
                "smY6RW/CrJRxbjIM7UEnwwxbBCgko6ysUyweMYpJ7PDPx7YsoDDQw8oUgdSJSSlgNkYPFW3YhlWVXmay" +
                "Dkm1yEb5JB/pBB0GVd9B5x7RBkBqtqxqYJZg46FV35YQrXrrFYMqACHVSOvTaQoJLmgb+r1ej0/nwO5k" +
                "wTZgXvd9UOiDdWHcwlD5DixDBsmxR7Ffub7kgiwZZ5N8ngsDUUKmNh8uKt/PBBxAyGzAX1hWeVEs68US" +
                "rFh7biCDnKbcXHVWVgKODW+K8m21SDGwcB0fGSy3CdgCwq8CkN4P1jqC5CEMFv5hT4U8pC3lRU3OWy6w" +
                "8SHFk8OJ5+G/F5CalQ0ERqLExThlRj4DOouiktlXSQHMiDF2lIiaZVlS1hTzrNrikyqLGitAgADIKquh" +
                "Esusd1YMi/pccAFGZT0QvGzfoHOVUwAK18urQKRZMYZK4m4Fafi0nxykkAPZNJsJFhMKSTRMyxI7TlZN" +
                "9p/bQpfASjeQPKDwG13l0HPEMZ/ojIB4XaZCEF3rSFspCIAH7mmdV9x9vb3Q4+dfVLNHQDix48IRH6RM" +
                "57eYJN5A1BRYFVnpFPudsoSfy2K8HHFTC0/pVG/y6TS5zoupqFWhcoznhgjCdLGYOmkGKaaDYFHmRZ38" +
                "nQICQkifbcYoy+BthEmieAAipjKKjISnf89GkMm3KnmUFLe9C/88hh9ad40ypz4qlPTRjMn3mAfktzDq" +
                "vNCG3J8m9bbImlzjVERns68QjBIRCsDEoXtHndYx+CXlF7+4zQBgkZIxuPhYLGDykA4GV3oOCMvDXc6G" +
                "aEzItEICCPbHtGSHzSAYoKuS86uiBJODjMV06cSIoV/C9ihFo6mVA8ADLzDTuqbK8qScpe/y2XKWpLNi" +
                "KZtBtWUHZcks02lxoyabbSaxRZyhstmbTIu0/uaZNQzDisKjXIF9IoYCNGvhJJnKcDEIITuWI+GWCDWh" +
                "bQIzDmqwJq+q+ktHI2xhUhUM0k+SXYfcdTqFlJXtBtQ2Hm999QveXtwF8LYDXKQl3QYrKX+cKCHkGbma" +
                "O8fZTzkIBF5HNxqcAOcmiJHBidAgFIDsCNlOIUmgeUlcofPLdH4JjDfyGRcundfTW4rFvBLTdzRd0h4Y" +
                "ZiKOIRBB/cf9x5siOd04wuP6ymke4W8hBdf0q/5jgQXxroTeyPtZf+suigBg801MnM2+X2Y0GlinQaVL" +
                "O1CMGm3i7u1261HdHbrPK28zoLzyToWWIFvQiZQJkyWkPzca3opIw9YX8xtbEFtmI02GxbtNcozJAOOb" +
                "5tYhXrE/5EDDqr1VE9N2jeyOWTHModmoTyBGJvRzAmAYsDfZdNrn1toXxaVMIQY7GpfZBNqT3oNpQ6CI" +
                "iZbY0FXL5zSBkENeYOG1kQk6dNtSC9NxubGa4iE6WcQlnJbeZVaAahDdQvo3IsefEvBAgbZlzz9/KPCc" +
                "G+qTcVfnyJ6nIIOBPcwLctO1vCRjTOAwJMKH/cT7G7Lwswz7njP1Pamn8pICxdkfpXgRW7QLxwUkAnQ0" +
                "YMzSt7ST4O+JDoc6h0Sj/pxXUxVmeIwuG1n/EjteGExakYbiXYs/DpehzC8hjKQnBpr5zmniZgeJM3mi" +
                "akZw1sGwNHRGitpJBwq822IJywFzwIfShQFEgxle4q7WRbFF7ncgmhQ9FcFjexPbsgabgtFNrrzzn279" +
                "p9/WIkuCLXq3CMnngYTpEEK/ycE1lxESWI23YCdPsFKVGRN0ByAp31Y9Lm9R6uh/5ku1hKVdsITljXNA" +
                "Z7Bkr9Jrr6iyZP/khZqgXrWJYR2DPmJbtIuGkO6DcTEZtAbbrWuYwYAyKqbTvOI8iyFNNuix1N5hzSus" +
                "qcyCtpGnwWbP+u9Z9xPpDVvYemMHu1cDB5njvpim8FHnYwaLyMHgZ+dcUAWOwMrBXBJ9h91EFxySkvtp" +
                "MlkRLYKheijSP5LMs6KCLk68XtZIl4VSxGewqQ6LMU2TDcMHDWlDM7g1zeAEdDTGQFjw1PHW9jYkVra9" +
                "HXk6Lmwgzpe45rUiX0cst9kbFgW9kgEn9+nEXTcL+g1ADed3gXDgVTFF6Mq2PiyYUZlTAGiYBrOTuTtF" +
                "C+kC11i2T4lVB4l0D1BKhtiOdsrovKlZslFmVL58XsIHyivuuBFUMAYWbUjrjFE7BNfinWaqxqCkYzF5" +
                "NrdC02ALtZs+qqTxI4T6uENDl2yC5ap95FA9K29BOgDHM/Y+hhHFiYkjqm9oCEgkhgw3hvzXTQrrkiJB" +
                "HAUlhKprv4+rzhCXjed8YRePAoch4tenqbzSRsOscMn9Mor80J1kXTmErKAEBDGmTqMtA6scQSfH3CZ/" +
                "xLuGZ8kRuI1kGWnVNCksyIhyjD1uWk3Uc9LJtcfcyWHmPfSTH6jaqOVU6zgpKrOYFwDo1qcVuiSs2ZZo" +
                "rBFcGmxWzjosp0YF6DPdOm4k9XQ2urbR3F0wRClndKry3yDuMWUQUuFEu0biPWL5abTP80AI+wXiiOtp" +
                "SNNsonlAg1RWEMqxbdKpswmhKvzj1CS+2kaIHhnDR4+UAuuRKau6h7LlzIwh1aRYeRUDZCC3vkIOz2Xj" +
                "7BImlvAeba1xgaWlNwahUtyY0AZVlqN6SW9skoQBlZfVJgP1YdMzHMfoi21RpiGqW1giYrNSh0h8Xmwk" +
                "bzdpn8usDiKA7qWP27yFXBI5lYyuYCv0kxfcDe8QBp6CTVKx/qEwnLyA3sKwr8/2X4hYe0o1vgEDGR7i" +
                "bXrD+L5GDWEM60syMRktylvE2Ckh8afMAUX7clM33otZyRYGDXsaAXAJZTP+jAk3cPh/SbZeSXZDn+7q" +
                "oyWZNf+/JMnuEmRqhrJ71XL+LoyF0cqz80qjG6gmNuDf1rsfhEx4qfRal+vo8e5yHkW3e8kyzOqbDKxR" +
                "3xQrOdCq5V/2euYNR75g769LdCjnlAHmsa1rnmHoTi8Z0h1pNZP0jbnw268BcZLiQ46g/3SztmUkR/mZ" +
                "mfitgiHZnNKwLN4ymzgX77yii0Q3gQIZ0UDJG0jIp++X0TUJ3127dU1Qt0fX2mFBdJHC/Lag3Bk9o2zi" +
                "HGldftwsBVj4qp7BeoKEd/ikll1uPfbxdu/WiTxDOUT+zhwYDV2BWvSALfTPzxb5F1I2soaIii6ZxLpK" +
                "UTYhlGKgK6SzWlj07quFHvvcbCYD3kcoxqWwZcU4WQGJ1Ij61H1EfDR2DGOAcR3nad/vgmd+t7olfhpR" +
                "4oEDMLfVJJQCtaifc/kkshHyln48iyIwVhEFTetiCZGMpOCtj24/9IgpGhJlRZXH+DZKRUYdghIRYAON" +
                "rdDkC0lNxUrcJdhhzKi1a0+wGmI66nowfKeTlNDdjDD8VADaRd0g3TKQHQa1hgJHUxjgjL8hCyR7wUFx" +
                "QX0bY56NqI6RMuNoJWLoUsLiQsBuYC4fSx1cQCfKnAVnPSTZQAxiN3BD2KLcZKjf8QZMazEQq5okb+fF" +
                "jfcXXPv1bMuO7bjrDAMJH46FOj7YY1a+bJvuGDeY3s3U0XBDGEhgYQFZmXCIGFVUEaH9bKlR0aR8wfDG" +
                "MIXpXTgC+Q2kf1F+NioutQhAJ6NzuCAEggl5U4uiOaErea5V+862rXZDBkmFAzdLV8g8DvYZAc4L5pxs" +
                "mBHjJjMgeA1iidBRPKXVqb2iyxiatQO1VeM9WUsrFo6y6qoJlU/QdqYvOuHwXQDxnPvD8rCMr6GEwlkI" +
                "cVY3GboIqzQzr0UNCkx+qZsNKzYey1pAwHKIzRg31nXIRGSkOybJdwG73TG9v4gxlOo+fyPxRAmIR43A" +
                "o0jKLytYxtk72AxEH5E9Vakic/q94S3Mut39/Z3HHOZM5GpjpElZ0NGEGT6/zstiLpUMDBpBSCC7h+qE" +
                "EvVhuhUkHFxjPyuEKLc53tSRzg6OTt4c7Hwlc1osKKro0xo3Oy/YyVZB2jkMH5qr5Sm0k80TqxAmeXp6" +
                "cLy/88TJ4TBm93AyCsoTshvH+W6pJWGCigY4Wm7dzLGxMqdpNqnVaaHTDIGGzDlpBdKaxAgCFeFKUHKs" +
                "KAptnhLBE83ia1oEMPGVxqg1dEn+T2lSf1isQDz+7n/JyfM/H+xdsDTy93d2/0ifvfenP0RuSlRwImrP" +
                "yTJIMkYv6NTANqhaide6uNSIuvcnNbQLVmEAvWFavM18vDYeYVueaP8QW5WEnHAMhNY8GQ9N3gOKARwP" +
                "Y1ScmpWAyp/PT44fsR7DRVl+2j16lSgAxFc9F0PS+j0QZeooq40qIZKkqt10CsqUxHZgVHZl1WUriZdf" +
                "FG9htbzNtpMv/vMBKfxg+8Ee7Zv95w+2kgdlUdR4clXXi+1Hj+CKpFNQu37wX1/oFFklQfQ0xDN3wlFX" +
                "z9k4XJyICrQf8/oBOrHiDRvhbZa5etnJFLsV6Wq4O05DdTEs8xxKRMs87j9X3hAgnBW3vhtZgyNkLlc1" +
                "5kJlUo3L0JmbrIT5Bcx24gkgz0gCPGuTYPvr7759pi2ofTXRinarGD9wI53/9RXSGrASmNvw69QY+PzX" +
                "6ffWQmHLUMmDm8vq6Tf6hMmk7eTrZ0+fyFe0LtkARnRx41pA86Myb9x6TCOFE7EBLC+mb1GcspzyvSRX" +
                "62LxwBgarP3pYrh3mQz3QkmCZPSrBZltKxndwsYW0w0clyUuCmXuDjjDMjfgLIs+wc4ZmiEAYBT7VOyy" +
                "GdWCfryF/yEowMLub5PnJz9Cmenn89PvD84OoGD0695Prw6P9w/OINDdg5Pjg51ntuFNRImuIU6uldpq" +
                "JhWQ+IB/4TK2oWmInIcWvtYB+X2iH3eImm1rQFBq3ZhotCpEtiW53pmwehD6PFAVJ1lR5xxi4oKquhE/" +
                "biU/aYz3bzHOJLJ4Ttn8Eiajw6gthug/+fmB6P1A28GPsEvCt588rfntb9TlEUpKf4eVxAe57JSc+Ovy" +
                "XhCgiieEGqWxqx1Mx/mSKDh/RznI8FC4g7Pd/cPX57STojFtkQUmF1gPIShVlHUkFCHVF2YkSoTeDfW3" +
                "BKUpLN7yZRcNuIPvDw5ffn+RbBC2+7IZ5qSJ3YjiYU5XDT/L9kKywb2wqeNR1Nk4Ojs3jn6JxrlrFJZj" +
                "GO10+ZyL0j0mtLZGBewVS5K9td/ek8wG5KX4wloVi8RL4CGhKfvT69QaqS2X+/jSEdU2aYuYnqVak6dV" +
                "GnbqSuNAGDRcUySMzoB6oeVKfopWaTsUJgtGI0HfawqaBI8inojIa8WLT81Fodqo3frmCGR8sK8RoYqz" +
                "6AiNWGoszPgDodl16CI6ml4DRdjSu6SkgLOtu7DMEYiENfHHSNS6ClIpnpNaV1+gg2kyDwbDB1XbPQ5y" +
                "4QBI0sHB4gBRKM96mCsGK9BVDgo2HXSXdKx2Whu1bCJdVLOZwejzeOkRqp+fKqrZu4EEB9eEsPjq3Qli" +
                "TaBi16nTH4ICPnKQvku+ZFjwy2T0G/4zTnYScbPTZHsHnJ5Nfn78CyON/utX/DryX5/w69h/ffqLz0X8" +
                "/OwXefbpaPCB6F77HGFn2qzVxzhOy/w/G+pe4EhNTHRaQAVMKHdxxdR+U/68FdWG40ujLvwXrlXRbK1l" +
                "Db/4iHo0luq37B0PDuqRMDFOfcSkWVvu86KsjUFWo2olOEVetKbZx9x7HZUY1WopBkvEwsPGtFaLNMZL" +
                "i0zAIBgwPMRavXJtIVp/RuN9ZdUmdeOTBdyk0REPo3l8kMRCOP70jDtjIzl1OxnhA/5SU28ZYy18UZYP" +
                "SPoN0ThbcuqWotHO1qfZ9CQo5EbrSFE3O7xBcFcd40b7a/+40Xwta9aiCdfNf+vQ8z6fNdTwhcS/LW0i" +
                "3pmRPMgaU4Ty2vW1w1O0vX/2QyCXg3AcEtWjbDAE+99sheG/jN4hmHSd/eKNiXYdU7tlx3OBLoFOl8QI" +
                "x1FCTicsRrKBeFHBGjpEVrGwcEitHFSjH1o5GnNwsofsjloOv2VlIa4fMoDYDqGSNByVUSTWsuKrDH73" +
                "ZnXHSBpmgF+RMNvVSg11d7XSWZKGbYKKWcW4Xlc+VGNGkwmThDxZ5FOPUuOwGWR2WiIs6HRDEbW7YaVh" +
                "FJZGhzsOCSgIBPPJrTqkIeBOdNyJedKzvMQbbRkaDfS03/86BluPTGmSJQoBpUZTrOnTfaWOSwRXd+aj" +
                "Vg1mWxgWFAtc1selDO0W882PTV+5RXaZs7hmtuX1MlQIM+Cj812HIcGkp8MN2pbLePlshTcomGYZ+yS7" +
                "D3L5I0WXWHGYNFXHFJu5tLsn5Yb+4ISixNtaeKVTh/4ugdQsGfqwUHJZnUYvF7mI5FVYHOGtUHjUItsd" +
                "/vn/VABaFaAESx6K25VkZUnBYTosynyGE5dDOQmaDd4N2HHgG6+2uP1gi9/aLf41pVmX0WbFQNGUwxE+" +
                "Ke7EI5pEwbTT+uBxXo00Q6mKZ3OlssRfZSIbQNpLCWMjhJd6wFLlAltMk2cIwjGzqikl8OMl00Yu1ixl" +
                "1gR85JATERPw42gEBZ8YTZfCk2FT+PxVMdTCaZXEoTvRIPRjrPt2FDEy+giA45MLANdKsRkw4Lm/6Jgp" +
                "ZissUoUaR4+5r3E20vF4eqlgETA2qJFZYMd/QnUtaXGdI2scoslKFfD1qlkkcnpDaq9AA6RTbl097KYd" +
                "Dxfer5BkShbLUoRm3+/8RuhVllH0WLgowXiEzgrJiB0eDFW1ShRILNVjgH80X9OOJYuOsaKXxtA8CIY6" +
                "8zlEqTss6ldIdZFRyY6MNq8u8FJsSw9taO6tQ4Gcuyt7vFVVidR4YXUKSm7jzLq4SUtXO2Fr6lFWpk/b" +
                "LBZxltZ6LbA44B6pj9Fgz4z3JEjiolVVL6cetc0zbSDHAvSozlzkmdgZilE0sLu7gX5LsrgFjXKJCggi" +
                "erraLWs6RdkzuTd5KnyNlIGdvyfCAxm0sZ7+nHBzJyrvBhaxRKouECwlN2A4J9rSXwAQlL8ju4gGrenq" +
                "d5xx8O18RllPnrJ4u24sEmUWaBwOphoxBJioLpWHCHyitxyBK5eZFuhE1Tere45ZA039QglJ2k/Kyite" +
                "TXM3u7kjJMZuB8zkM7V+eRX4idbEyo5TfmuxmO0JVs8k1Uyv9Rlmo5SHVK1ibNV4kFE6y6+cjFF62y0l" +
                "gBntJbYVyeQgvQUr8dyhlvikwq1Cd13UEfVAF+qOfKzdgbGMzKkEVqha5cIAZ1JqykbGU9MiOirnTugy" +
                "cC2vnCR28hIiFwSxmUWjy+xNQHvGsEPPk04BYEytW0hc8nbVuykJM9oCzrq3U6m+IbH5hif3hGMUolWk" +
                "PnYaF6Ulio/awSA9y1IX4tBm1aZBZKIGqXMWmZiycyN3gz/NHz3REWLowIslVCpCATsSFv5UosyYdUhu" +
                "sk4NRguzIgfcTUNei6QOphPgKD6ScvKsxGNWnj7eEvz0QPZjX+Vbr6x/pJrHrbsd0G4g7Xp+n1sv0YIV" +
                "PBxMkqfAi1YP2fFTekDNKSq4+x163pTDCg8Zc9EodTUixRy1sQqZueqWcoltAafYlO0ipuNc6F35Gz5E" +
                "R3qqOaOjRR6pG7MrOBqkku4xrWJ0Y2Fb+Crq1oYxC8VCHN74+REJiidI5ePPV1tITTNd4XLgB8fnJ2dI" +
                "ubcehIy8e/Cjr39wAlPWyY/9L2Tf36lKNKHLBybK7eqM9kkb5Ue7KbDlagoAUUdrcVE6b08yH0UPfIGv" +
                "GrcyFT5YHh1KDDmQ1s1Uaw9Lt656uxeKbiaTBvK2T5oVqMmGOdCb/ta4c3nh7yKSdnpcXotOhccncrFA" +
                "uEZHrBaXxdUjkagtX5bMY8xdhkgusdAbmpwMkQKaSmWf95vclUuuE7d6oof22XXAkpjfgYfUENiM/w39" +
                "HFTWqTdumREzzlVHJRty8kP3fgXPk04QXKcqy2ZOREmR7lwMp7h4kzCvU8h4TlUqOZ05ENUd62Q43kDH" +
                "i6fjha0Gj6/fM5kwFVWMd0xGcz+mTsKtWXaz1R9t9EjSS/Ev/B9I99X7kUIFklAd3UUAjG8RkslHUgg4" +
                "n+SXPC2hBn001cZ9WhiVqyZ6YxK3EjXvFjKmiejQlJZvrCFHtP5s8nabHMv/25muwIVi1FsNjubYXCeM" +
                "olezcpnn0fiISbKsiTwhdWmlnFLWymqy9DzL5K4AgPXU6z9Wrd49MyhzN5MmfUmT6zztIqgRoaFKq3SS" +
                "DfxmQWKMV0D6+QlysMkLza9Ktk9cQGRAORHf0arN/eWArqDQ/D4BRFz0TjVZtrBFsZWZPvV2l2zLbE5i" +
                "69VstOKXc2HjVOxg3QZSw++jgKtcqu+F4Y11EsdW+mrlhrb2zWxw71LObO5NqC6YndeyuRHGUN2365Xm" +
                "IoKpl+z2vKbUbmZrSVkY2NFtOfH9j/K4ef9jfCVffFNb81IYpoLcOApDGlngNXJO3Xlabr3xcjG1q2bq" +
                "SbLR5eWFXJPkqO46luzsDOhUOSA40Ltyo5PKvfsybdv54UiQnsXvuQvu/HGoIz2ib3fthetyXHvuZ8CT" +
                "oMCC05tfVr1X+HaqX4CJRJ7du0Z7XtKWaWte2ZdZW3neuP/H1SBvrV4EhEoLdcexNjAtZinqBMvGtBbi" +
                "skqVL93XYiouVDj1pASeuS2o5kzj3pNZv6cnH0CUomQCXou3FZSlnxtDNq2CH4pSb0mdjtd0u9B6rut5" +
                "Pw92HFOOL7AQn33F6lUDt++veL5v/L/SEhl+FKeHhu2cTrhZya5aJhi0+lOaXMHu3vnC1erf5G/zfllU" +
                "/aK8fFRPvviPevKnR+l/gJtHbwFILlc4R9UhfcxxMYKDZZEZvTlPynS8MbOSFnLCoIluojzjw21W2spG" +
                "+pRXltpVH/ZpTQeUOyWAmftWXQMiIHzlC5DUtBCJ4MvBpIkrB3O9yfXX+ZgFi3ybh853CiQ43siOsai/" +
                "up1p9tbd3NesiopHa0/hgO88Rlpf1ZFLbW9jkXxY9mw6wZDEsXLHBVomiBJDDRF/7VCgUD+yLFbnabb4" +
                "r8sMBAnGWC6qXCm8MYdTPpeAgfn6tKX1/JGj4J2YByPV5yVWR48sPgvRYRzHlzoVICE3jgkaK5eXCmNI" +
                "yEnMOai+qwQd5CD4EwlZNbCV5q6GXanokgvuwnFByRlNeuR9nuwe70fH3wKjOQCDmAUoAVdeuZX/LNtI" +
                "eJC7qHV5W1gKaLrRW+djqRE4tmnY17VgHmlv4Ctmj7nBd90wYKo/3A4Z3xDgC6mcDbC2WYhV8dFzcHfG" +
                "vn8Oapusp3YhGB0mfcOBP85Gg/UQTLoZGaNH29UT5f6GXrf1pY37yQ0Bf/by+a578el/5MOPeE+JSqfE" +
                "f7r0n4b+U/oZvAgx2Uj1FYOzffGEhK867pC8iCxSkaXxPTTBDQjwedSv53o4BtAvP0D8ScTPvfyEB3/f" +
                "M7qGLp/ui0VNvxDGmN4ZAXUjYQ10KLOsuyomThljFJ7i6PJy3LWDahd1ZORciN17rXr2wQFk0XPXDD4P" +
                "3f5xeol/INVnDILDFnBdN+RgG0/APCpGo+UCyneTWkQcOnmC+PitUWOjP6wf9Wkk5FO7wFMBySHqKTyg" +
                "2PLUCii6Wtq9KUXOmC/VmAV9/tmmL/tg/pJXxLtuc3i7vnBc06x6czyTMToNhOlzCNrffHZTu+olvXoH" +
                "nBw160swSg7p0oi4KZFFc20rBjO+pXrnnvkcv6ekv//0Eb+oxHACCjUYSUCphYfifj9KjZ11/o6N4nOv" +
                "1/q1J5e3USz1C69MODx+mdg/ZITw3+iiuSvw8K0vhEGdBO+RCa5W42dtHMzdvYvDNwceJJNNTZg0+/QI" +
                "Nlhdb6P/KMCnZwcHR6cXB/se8JMmYMTgMgSGwUsMB4wQt7EfSkknPKnJQLXG4qIIcUB09R9PQ5MvSQX9" +
                "7SRL7PKEaRVF+TYushKJZiAhsaVNy5q93ts7ONiPUH7aRJm/HOQL2VEGRCpwr992EuKuYXafn5wFunCY" +
                "Zx3DDOXHHFbCm90jjZfZB0njwxITBPYZhL0DvbMDXgoR8NtJvl5Fr8yoVu/gAH/PQYtdtj6Mo1Vd1PFg" +
                "SwQZKAslGyF3/ugPKNwxAcd5fqfsJN+sgfM869GF4CYMzOcXz1N4b/fVq7CTd5I/fCyC7m7oLgw/hrru" +
                "7onmajWRRoieV5uoheSXQQvHeBRp3JhEzCbf/hMm8XFkJlM0tp8OwAuk7+CJVyfnFzGoneQ7Abjrf+zL" +
                "/RAaI5Nj3ngBIM7lTj0JCKVZkEG6DT9i70nFFlSN+wEnZOC7fmoMOlmcVO8Z8ZKI5o+BoTJIzQAx1iJ9" +
                "JmHrbLi8vIx+bqbO3iGJsl51bIqYP+3FC7UOWMW8RytEC5oRpR3zUoXuu/Xj84J6kq2jZXSvmf5MzXgQ" +
                "ZbLf38FO+TV7eIe48cs1ayNcTKV7Li/Eeh6GY8AXjM8PMxZ3wR/Ut6Krznn6Xb+/2D189frsYOc7/uu5" +
                "h6evdo+PIWcGfHuwv/PQWh8ev9l9dbg/ODq5ODw5HrDdzsMn7mX0cOAa7kIfDJ7/NDg4fnN4dnJ8dHB8" +
                "Mdj7fvf45cHOw6eu297J8cXZySs/1jP3/PXx7vNXB4OLk8HuX18fnh0MXHEKgO7uPPzatbo4PMIQJ68v" +
                "dh5+Y9ibBbHz8A8S0QgpY3+Zsv8lP+WtyqhzsXt2McB/Lw4whcHeCcTtOSYFCjzuaPLm8OQV/p4PTncv" +
                "vkfr4/OLs93D44tztP/KiPnyZPdVG9iT+N37oDyNG0avrBPX5lmvtTovz05enw6Od49A5a++br9sQUKT" +
                "b1pNzk6en7gp4u0fWm+hgP5iwL9tvdOLoeztdxKL0QuwG2R+cYYGAyBwfP7i5OxoYEz48IkxmicW2OVg" +
                "7y/kRfDDG7QjU6ChUTDClf+Vd0Y0xzCHxy9O/Du5uiVigwZexyeDw78Mzk9evSYng0U/4R0R75E19z54" +
                "K6OemYwkUbtD4370qGP7BzoiEGs7yX0HbhYZc5EDq3eg9emCYp3ZI6v2CwVYkvfuOK1tZ547bnnsuprD" +
                "/RjBQxDLTrMGWBv6ewT2UwztG9M37WB6+1f4IhBSFep+r0s0upVf+TPej8LJ7kfN89wueUALTHx3EsLd" +
                "z+MeeWjR+XK9NKfRSZMu4Rfp8M4lFuJz6QIMuY/O+wHuWM7PdU/A+9Dx9wW0V5bWm3FZnNrc4EHCAn4A" +
                "AG5+3AXiysWlLw5Pw5XOIdNi3Nk8eaO43HEFeXTKf2WI+V0/R/cPjNNktM98UcBR9+9w/+PRGv9L3p/n" +
                "J7zDBPyNA1KZcq/334ogrIO2fAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceActionGoal")]
    public sealed class PlaceActionGoal : IDeserializable<PlaceActionGoal>, IActionGoal<PlaceGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public PlaceGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlaceActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new PlaceGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlaceActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, PlaceGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PlaceActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new PlaceGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceActionGoal(ref b);
        }
        
        PlaceActionGoal IDeserializable<PlaceActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new PlaceActionGoal(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "facadaee390f685ed5e693ac12f5aa3d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1ca48b15H9TkD/oWEBOzPxiFIkJ/COowCyZmwrsB7RKH7EEIgm+5LsDMmmu5szohf7" +
                "3/ecqrqPbnIse7Oid5H1Bqth931U1a131e2vXF64OpvLP4N80pbValGOR8tm1tz/ssoXz86zGf4ZlcXg" +
                "1SKfOD6TJ3cGj/+H/7szeH755VnWtIVu/5UAdWdwN7ts81WR10W2dG1e5G2eTSsAXc7mrr63cNdugVn5" +
                "cu2KTN6227Vrhpj4Zl42Gf43cytX54vFNts0GNRW2aRaLjercpK3LmvLpevMx8xyleXZOq/bcrJZ5DXG" +
                "V3VRrjh8WudLx9Xxv8b9uHGricuenZ9hzKpxk01bAqAtVpjULm/K1Qwvs8GmXLWPHnLC4O6bm+oefroZ" +
                "SB82z9p53hJY925du4Zw5s0Z9vidIjfE2qCOwy5Fkx3LsxF+NicZNgEIbl1N5tkxIH+1befVCgu67Dqv" +
                "y3y8cFx4Agpg1SNOOjpJVibYZ9kqX1V+eV0x7vFLll2FdYnTvTnObEHsm80MBMTAdV1dlwWGjreyyGRR" +
                "ulWbgd/qvN4OOEu3HNz9gjTGIMySE8G/edNUkxIHUGQ3ZTsfNG3N1eU0yJ4fjCH3SgXZ8g1w0KNr5tVm" +
                "UeBHVRNqZakMx3kzL3EmggeFJrvJm6wmzzTAgzz0TI5cuBJUyVe2G865vgZ33MzdKivbDLi6hnwL1nDL" +
                "dZuB5pjNNRtlnBuHrcPS2dhBRABCNnF1m+PwCFFKYoO/LPyxgMIADydTRVJnU+eKcT65AmQFZoAvN4sW" +
                "Ytg0+czJOWTN2k3KaTlRBA2CZmirU0Z0AIBabpoWkGUQPIwa+iPEqA93esvq2pWtHl3QYHcG+M92n9XV" +
                "Zj1agYvis7xt88ncFaNq/A83af1bmf91BaWBI/rhbbbm79HCHjRcdFxVC3vu3DTZptms11XdjppNPeVb" +
                "W9Jm4Dirm9GsLtdrV4/82Em1WJQNlsawp9igrXGSLQQpb+d4GR4k22DrFXQdBSI+BaiyARBqq81kbmhx" +
                "3nRR5e0fPwnvZT6mjIRfBGX5/XItKGbhfaUPDnhwnvAUvSfQVmBlmgEVrjpv1ik/GXdDydYrsOW6atoN" +
                "5KGainhSN9lkZ3PvckypSPKtm+I9aJ1jFsVzAFqTaFW9VZj+UmH1N+GhbEHy4WxtNw+FX9jv7lbFPSwv" +
                "87pQQHwWwPHaiRTi1xRSTU0sWo4G59hbiXwBYW0S4XKLxkFd1O40W1UYQkWNcSarJ4OZq2A+PfSvoOov" +
                "zeYFoAPE+RpKARKAQyDggy+VMYHtqiGAwAWKyLD1g/3kGrs4APkzcyOpbDDnQvkpU+G8wPYtyVWNoa0m" +
                "CxeUHI4O1GncMl/BNANL0G+pq+bjatPKOoWOnmBFaBtheVfcX28a/kO+KpQ8MEHVpm6ELYT+4J73icwH" +
                "4/f38NedQddXC2D+g+NEnTSD3pxX/Ek9xX9/O9AFDErtBVkqDla4AgvD84E9AYZBEH84zeBKwRS1eIsf" +
                "+WTiFnDk5OXbt1ix6o5WoX0bhCrZCwIFXnDv6KCp6X2yWCTic50vYLhE3I03GoocvD5ABKvNJ0Jq0Tzw" +
                "KldZD80hcA8KVahugCXPIjrJww5ayXPFZlBs9JWY8NG0rpYjCAVefLDzvFVVqOrlA7X9UUH1/WOVQu/b" +
                "9nhXFhB9c0AUFPba0bmG26kkheATDp7mFJ4V+AFK6ZTRAR8X9l61NzGq4IfZ3GEGPMgPfsDgrxugX69k" +
                "3TjucDialEnQA++Abp/ybWqAENUI1B2MgxvwLvy1DX/9dCgMIv0CGuG4aJRTqnbh568fI/VpE4aD9yDl" +
                "/7o5jAuzawaJZOGm5Upc7zaxj9EnkDmnGh8Ax3U5udqsRevRemZt3lw1WIYT3DsIGiIy/r0op634saCZ" +
                "2C2cPI++lZgNA7zN5iBRlPJiagvTDDKMw4uirDE9cV4SOHsuxTeiBh95ryLMDEu5Bo+KDqYF7HwO/aGn" +
                "gfjYBo3CC5u8BPb+mVp3H0zQD0b4wpWnjLeJiEU/mMggSTwrBtbXCKbg2jOAgkPnt8TScbsDsXqXVnvZ" +
                "PbMx/5Sm9Wtcy78Hxm4/WgpKV36hS30ojEh7C6cV7h1NdpiJiYGhJLas5YhPGRwXFSQIPi/WWOZX9A6R" +
                "ihAPer3GYl3ZwmNMOXbD2fBUw2sZJdJCKCRVBM+yLmeIjWVmdDHFKzfsTrN2+hCHAy9CYNbNlOXqSrXU" +
                "yTB7Ns221QaRNXDAH7VlqMQb8XCJ8LVVJYLuuXZXuYe4G3q9xfm+T8UdRK8lcWk4bw9nMEJ5cOg1KAhh" +
                "pEivTfc+WfIoCHkDcjbip1FH5ZprUCIm8g8fjrGfhYEaYYuTFoEMDnMnfH5lBrIzzlvN7tCX0QZ1Rie2" +
                "qTvhG0Tw43JRttvO+OvwuDP8IGfWownPLfza4zCo26vUpjKS3KjPKyHoQmrISB5jEa+35bXNpb2BDoYl" +
                "a7Ifwhb38JbeL3TbaAz3+OY0bv9x8g7h3bV7G7g7eF3+QW/knueyOgF7gkASyWMxkUjGxDBcHScjS3Zc" +
                "OGgV6JYQlZdwC2uzQVVWSbSScnD2dAG3EStV2U+urkSPNRnC1yZMbU+i5yFAHCaBssPgtwur2mr4JR2f" +
                "0U4kYgvt07U1Pvyvxkg/IPN7tUNQMWJMY3p+4ajALpwMN6VxoLzxoawiVu4kxnR5PcMQ9WKrZBySA4ze" +
                "gmrBhP0eii2BjAe5Vbf0AFxXi43PPO+DPBt8To7G+t/oyDgIuYyZuTv/qxjsMDqlSxZy17n4trT4Simc" +
                "6aNzpQ7PGHzVVNDX3tLB5q7rcgk2u/bpHrHm8Hfb5GAq85mzYwTlOVPJMLODZp6vnQJyyUVf+ZWox8Oq" +
                "SUKOPndMz7t0azKR5HThI+6GkemCkjOTVZ9BJRaFiohk6f1qp5SvOVa1ZFRMONCXKOSfoCfJLobncT7D" +
                "iSPl0exB8TmWBBy68s8gZVu/FyGOM1w+XFnvthNKWGVcvTsFhTSJOdlCqqFXGI3jnU/YcBlFSflAKChJ" +
                "QWBc1pGQWIxkh1EW3yCvxRI9OMX/QXWxHPdp9vnL7x7/3v6+fPXVxeuLxw/t59Pvv3724vzi9eNH/sHL" +
                "FxePP/HUZonRh0QCk43i84EfVMArh3PClHJnaEzxxBF+DiWa4KcTkmFnmWMSTXQCXUofQHIsyfXOZ6+O" +
                "4pwjIF/nW+7whSlRIC6gnsqv706z78FqIM/fU5hJZNGubjVDCGIQTaoaLvkaZUKGiSjvSO3IXoLow0jb" +
                "0XePHyS/vg+05q+/g9QpSEp/g0o8ah67ZGJWDACsTKZwwoWfmZ6AxcmLckMQzOdQDvJw6Lqj10/On/3t" +
                "EvCke/pDljV5wFo6Vqoo69C2SJlTHTxy0qISxDnm71n+roRajhFJZ93RVxfPvvzqTXbMte3HScSJCcxp" +
                "SvGI01xUd6C5yUJ2TFk40f3otfl9FDvbR38k+9y2CyMVTzs9vlyT4fv3hPOg0aZ/xaJJV28mMknXvKyl" +
                "eq7FwLZcRx4SmkrRBYdEft+sT5Wy2cdGVC+kPWIGluohD+ZKJHVncCQMBh5Cy1FJB92WuFJUtuRBHxHB" +
                "8uWrGUz4Z4kQWzZa3A4xKKE2BfFGYRi1D2TE3w64yRtbAOokrOXdb/geG0RKfsauWRVo9sSakqvWSQej" +
                "lkdkH9U8ZkdNhEtbKn54pKC6dyPQ7sMCnDo8ewPBX+VVdxOa7/esfQItnWUClTjd0cMQBymmRXvHfEu2" +
                "+p/14kMJjzJ8T84sc3VNhesDsaRqGevPYziDqMyM3o04cRQG747YvnfET/0R/5ou+b7Mg+9bSVBWwzrd" +
                "SEJlyUeM62N+QrNeyJBOWH32AdjJTotU6IsSAZDxzJw1HcuSh4W3agVu5qy/0DawqI00A6NAJt8CbzOd" +
                "V9Vc+LkBJ35yhI+7cSk00GDoRngyCoXPDkotud4gFy7hRJxOMLj6C5z7WVI/8fSRBV68fIPFgQ/YAAnj" +
                "crlZMo++BKPxT58/bmD22huHrGKEPJSkPelYoK91WfgxftUktlXraY4cxAStRaDFdeluEidHqaLluF5s" +
                "L8HGMTdFln+M7OLWOpROpPuGxQTyfoO6drbe1OL5D4PkdzwCOUaxGtbjhAU8jzDjRjJCwmO2RUNrXSQN" +
                "TdIFP/MFVT0h8qz5XP2R2AsHhHYNqFLMuXHS9qQnpAGVp5KkwjVXEVkyarHTtEbyM70QITWgzRBfoNqa" +
                "kNtzZlvdoB2x6ZxpAFmZPu+zWMJZ4k9KCQbcIx1tG+kTRFPDVv3pobioBq7lqXXMJzrglAGR9oKsRJ+J" +
                "VVeIko01JSHJt2y9BY1KKX0LIOJj+GO1dhIMfCR8DU92ONCWRQI8kk0754l9NdLsSqLybmQR3c4fEMJ9" +
                "2zBm9nv2CwvECNbILqpBW7Ji0xy61BApF+k48sSiqq6sVsCsfNs5JOos0DiWEjwxZDExXaoP4TVhNtUG" +
                "dAZOYLxpQ4QP47Yrc3RmuT2QqhYSjUrracM+t9vZTeEO7HbBChWaLDezeeQnehM7Eqf81mMxLxNwh8BH" +
                "S+0RHLtJvolStcd5kF2sykTLLAoo1TFKbwApW/fqjRgrmslWQqvgYon+Hhw4RCcXbhW666FOaAf2gW7k" +
                "Q28UXVME9FIdoGnlDg8sL6KRhOynrgV1fyfmEK9XXpkmNn0JlQuCeMyS3QV7r6BjudPKVNO9CsAztYqQ" +
                "5JXTIpcVLNUymNMWYVbZhjy6mj1g8uZUoAxCKVUlIPPALO5NZfBolAXSs6SLJkFwoWtO/IoM+BauZfex" +
                "N3a28/7lX5X3H+oO6eqAa41jUBWKtRNl4UmtGLNN2pA1M5gczI4e0HWiFcltTVPgFfbUfj08ZqPbg1OB" +
                "T0toD7gTsoae7dPzT0wztIM3RABpxHEjGTcIcu5niRVsELwCSdbtqt4MkfgFg9suirrc3T123huHHR7y" +
                "zEWn1DrTKjRpopvMgvC+cUl9ATNsynYJ0xEXpgg9/WqxkYFq5nT0yJOVSLqqg/GgSyqZntIqBTdVtiif" +
                "kjekb77fHyDn4vP0wfn5LnucPUSGCf/8/hQZk8eZD8ovL15cvnyNTFDvQUwU2YPvQlrOFKacU6fD4F/Q" +
                "v+/15t6JaUc0e8gdB62MhiMJpZEGuS0wi4/VTkKb76W8CE2+Mg50nqKTGWZ3quScLvKZyaMwqxhIyzbw" +
                "NNgnjv5XaaS1jjupcHPZwK6SQmxUzIKLbs0ZNolclYWO6tWIScFfAYc0b3mM/w3zbFV2CDe+u5MTxWOw" +
                "/HB2TAobmzUIcuhvw0tvnFuaNBBQMBFtdNp+yjWvc6gToup7UbXd97qsqxXSb60iw/1Gul+KTpBrTfZc" +
                "/wwyERXVwbcgo7Vyr7lWm+UYzEAfWcncfOZ3T5TKwk1xDg0S5T5lkbPlP7y3HKxQHdPFbSi2iP7LCQpg" +
                "YLlpOWPvs/qOCaojv6vhzFMTFTVNR4lFsYNMaSLqOqeTlSrjCR0Nj7xiy9743c6AyIXiP/ospGbgbBJ2" +
                "0StFPOZVsj9qOEzskickM1/T8uXZCspOWHrlXEEDh2UD9YYP1IDsxwx2IzRFpfQlTa7LfB9BPRE6WrvJ" +
                "p24UhAWNBI0UMg0/AQ7uH5xP5g2lO0KiDXSUEpEwUbpoEg/Pl1R8iCELEZblms2z2h8fRBSizHbUYOJF" +
                "LN2KxKbESJp3ulkJG+ficqkYwGHCuqHuscOl+l4Y3rNOZmylrzocJRZrCUJrUpUXXRhJ5MRsFaz1vjVZ" +
                "bLZ7T1GP2w4FrMT2sNpcVDDTNHLDIRV70cbd7hZSFr6chk84htYNXvNvxBcw0PJ4pI+NRH7RNPoGksrt" +
                "ahZ4hDDcto+uIYN8ji+Jg8Q7kfFgqvWCfoFkX6bZ8b6AItbmpabfC4ykN5IK1aIj5KCn5Ts0A+odr9CL" +
                "xfMWtL3kh3sy4CJA/W7wRF889c+fy+PQ0R/Gj2w85RnrSfy5JnqrWTP4Gr9e6Q9AIklOe9cZ30xyJtw5" +
                "+pJ/+rHyXDwTi1ytFROhe4Q3PJLeRPGD4bosc+nxTNFaS3QkdU5GStVCvPV4JU4JvDQRlHpf0lgvrwYv" +
                "ZTMQparZsKRXm3QpXy/obNn1Cr6tajjuN/z/khkR46uuIE8UKQpxnbqM5DNZcOsRHYnuYGpEPJQ+gzTg" +
                "BfO/lNNFSyCPK/7HQWQwys7tifydKyeJLImoQZsOEeSlfVSSTmMkIQ1SesGITNIMzJ+NbVkqudpIFSRX" +
                "3oTqzVb5YAm7UTICP3/5hQRuMfPPYmRn6ecci3HJFjJ9VFTTUW+zwK87bIqcon/nz0tEwLpwhAYnAz8/" +
                "SJ/yHS86+Gt1UQDDTbSDcFS8EcwUBS/peg9ebxT7K6vid3lUx1VBGTr28GCg5IXgYC8ckox7BmthNzfe" +
                "OjtDzdydnSWa2RqUN2sYZHFIWwW+e2XrMAKwnwWDAFCdBCkQDpxXC+Q+fR8rEhyTurTMizCR4m49PgjE" +
                "ftyo+NQ4dZBIZYAtv2aTwiRpbdBuw+PaMePD5+hJr0tks5DUPUnzPeMts/XZ7zqS5m2bXyVnuSxbnpzG" +
                "oXYHZ7s79D6r/NnyPvx+SmicojdxYjCMVl+KmcUytsALqQi8OIGHyVRexIWem9x4JcMh/mxVSFGuoEoQ" +
                "o6qE0HJfV2/vXCX2+wnVGn/vFxwmPih7ZPpjGCOAMJAff4zaLyOS5Kf6TKnceuKeikZfBzYodIg2y6P+" +
                "EWsDG8YdJDNI1JmF71JYgJFOb+IqHbwqz1KPlkk2Xkoe2MXC02H2LT1mtmxrC7VpUcFiVTGy0/PpXREX" +
                "s3cq7deSGHbWJRSGi5WkE7g1biT1FJv+/UmfuPKdRkqnpvyJzSroOnK2TiI1Ys3Zx2K3qgMPxOvVkTji" +
                "l3mgtcdmwvhKTxDFmX5JONz/E/7ZvXW2e+lsu3u17CA6Zdf2ULe83rmIpWqADGTnK+QIXFa4We2004gX" +
                "B4oKRyupaDp1Xmlr0tNu+sYNlZf7ZQNwvG9ullpms20QEiQNTsKddn2ARl/nICsXVYB4gpXtfgW9JHqK" +
                "JSum6zpFHISZ8KdgMExfwG5h27+9Pv9C1NojmvFjNMBt8b/8xmfwUA1AlkReWnY//T5ECp0SUj1a7ZIS" +
                "cLvvsaqO8KtBpqV7AvqI9/yBcAeG/9dkh9VkN7zkM//FmswP/7+kyW5TZOlF5lsCQ2lxClFgb9ANTBMH" +
                "8N/eu2+FTHip9DrUPagA976bUGLbg2YJ5QSUevo3vJreZalBuNaVXsZL+nn89aOD4UmSBxy9fmqip9W9" +
                "rDmuqyst9vAuFlKOUJtQi9RYKEBI4Z4yB17xeNqQ+NvGHQpB5Z99p6i9G73rw40D/CK8xFESyL8IS1ks" +
                "/lTX+TCR7y1Bm28X6j0OFcQQ94jA55qviReGRO8wRNzXKrZ7T9m6FX2PN/tXpKnd1y96UAzu2r2iJCjl" +
                "MNnwLpKgljqXE5O0Ppdcocgj04doRvBVRWZzfCh6d996oeolfntAIxBCNygGfULpoj77ZjGRhP5mndPg" +
                "0YfZDOaTlL58gIIlx21IF98LgCkYUgOooVG3adY7TohaVr9mockHTRX7m1wKlcQTcrtY4ptOO43/ikoo" +
                "rNnlYrmoyVJvRIUVXL1jaUX98MET1vQl/6pZ8XBF+SR0AMkeqI/SXqGRgbuhWqff0rI8lW3M4+M3lyzj" +
                "8b5PtBSO0PU/z6IlO2+xe4eBZM40u1pVN6vfpMS3RxyfmOW0W+GkTsiGeDdY73LsbSIF0/tOEKXhsTCQ" +
                "v2H+HJs/QxJn51M6/qh570H4gvG/7+oTAgUBsswiI8uZtrtax6w8f8MV9JqEb0X0aSZ/E4jCv+sAebHV" +
                "aaWwo7Wd721eSbJhngCXt144+qU3iH7phSD2M3dX7V7aed89HCzhb1RJ2MAElGMvrym0gJ22JvnkemgB" +
                "CrcVtGuMHxaLWXtu0blQxESvICI73YIk30XonhTW/xZ1wG6JWWpiySDw6HVZbRq4jg7dBYDPl56k3iIN" +
                "HOMt/J4n5+e8qsEwUboD00VCS05SWMXfLboBsO4xMun1lpdUZz5f2kKemx5PlMWJ7vT64vnLby54eQA4" +
                "rdn4IkFf+HCChommWwVo86jfh2soasskjydOISL56tXFi3NegxE9HPfcv53scqplR+F8f6WM+EsXjz83" +
                "7/n729NSlRSvnlEle0DQGc1r9pV0WXUVqjUsKYhCm0cE8CXagsIleKzJ7iU4rn5g5V9/OL34frUC9fir" +
                "/8tefv6Xi6dv+I3GXz/Z/iN9nv58fcDfF+OnK2n2TJdBk0mHG7x++AaS3KDriFPUFvuZppxDwGV3H1E2" +
                "vdtzLa5cSGimO5zJE50fk4/y+QXhGCgtdCyNvb7HKqGLZJyCYmZWMg5/uXz54j6LwZaG+P7J868zXQAJ" +
                "yMDF0LRBBpLvMlBXe6r075J5mzLMLsR3YNpy59RFlEKn56K8cmfZR/9xRAofnR09pX9z/vnRaXZUV1WL" +
                "J/O2XZ/dv88bkAtQuz36z48URS2qwx+UHMjKlzTl9MzHkQ8VRSro1bMjTCq1XejKOftw53QBadWWQt8d" +
                "uIdhWQhQIvrvTJx/rrwRvlhG0bedNXtA5tqATtRymkuSz4Iyt2TISh5cljnLAgHkGUmAZ30SnP3h3z/9" +
                "REfQ+moXFcbtQnxkO13+9Wvk/eElMPkfzqmz8eWPi6/8CF1btsqObmbNoz/qE1ZbzrI/fPLoofzE6JoD" +
                "4ERXNzYClh9FxqL3mE4KEfEb+MKRvkV5erPge2nxaKv1kWdosPYhri+JAd2f1tS0HxS3WuJoqYM5z99l" +
                "H9NX/zib/IT/V0iHnLSYnD3G+bjpDw/4rbNx+Pl7/pyEnw/5swg/H72NXyH75K08O3jCo/9hm5gTSNOp" +
                "Ysl3vmej7towfNj1rvctdkZO5iU4IQ7s1ytinc9/YJXLYNSf8mxeu+njj0wwbsqrclhXzbCqZ/fb6Ud/" +
                "bqd/up//Gcw4ucJCkuq7RHzPOL6oJsgf+wOmmpDuoETt7+S4jBe74GYayYS+eH81koP06SCQM9LsQNmA" +
                "vf0TPhngby6CCPA1wufwtDFL+ilCtClDLKfnAxAo++uyYKDPt2WcfGs7B5KHkBVeCm+2S3XdT+3js51v" +
                "9KW79VG44LsAkd6v3HNzv98EIalyHLtbTLElYWzsunmvgUuJoW1coQgWKSTXa8xB3MXTdzLCSIMgsZUN" +
                "QQ7orBQ+XqF7diWdvb4OScuqxj58zvIWyGOLX7hAtLt7kjnwvfTYJ3y0lKgACKl/CxiJN+0/wyH+JrAT" +
                "ZxjV7nmGCZJ1eSiBWgdaGa6lcqOi3QKyzwwLSNZypvmlVfbkxXnia0ZGswVGKQuwfL7zyk7+NxEj4UFK" +
                "Ua+VIB4FIpUJqkhyHNpCV3g0/M+DQJ70PgHe5Fu7ll3bd/PTmqPih7fSdFz4bI91UB0MC+nJ+sU4sGnr" +
                "vThoZ9dhLhnHli2vfaN3TWy0KwCKSYWR6T+M3U3feOH3oi9j7EP7svzrLz9/Yi8+/Kf9w453wlf46vDX" +
                "LPw1Dn/lv0EPpjS8keo7fVD9LC+kc29H05ukn090afohvJiOievTrx7YDGMA/fEt1J98NdVefsAo+2d2" +
                "1/Tjo3PpR2RXLZwxTdDC3EhTOCagDrI/85je7cQurJfsy9tZE4x98Xo3FWV3YULPr9YMbEF+gncfBr8N" +
                "3f779JJeOGmu4m0V+AI29VjKsKw13a8mk80axveEVkTaYeUJLrIgEaDUOB6O2/tDOgnlwreT6UKSsVgg" +
                "1Eo9T81gMPWt07ta5DUvNmrHNzuml9jX7mfzoiF6wP20FXqFw2eM9T6kTOMihgbCvhKK9qcQJOlU/f6h" +
                "diTIp0qG0sovETGdiJsa191sbMNW8E9p3ikzdwb/BVkfn4XKZAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

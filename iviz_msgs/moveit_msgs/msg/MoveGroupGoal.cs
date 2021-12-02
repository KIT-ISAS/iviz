/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupGoal : IDeserializable<MoveGroupGoal>, IGoal<MoveGroupActionGoal>
    {
        // Motion planning request to pass to planner
        [DataMember (Name = "request")] public MotionPlanRequest Request;
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions;
    
        /// Constructor for empty message.
        public MoveGroupGoal()
        {
            Request = new MotionPlanRequest();
            PlanningOptions = new PlanningOptions();
        }
        
        /// Explicit constructor.
        public MoveGroupGoal(MotionPlanRequest Request, PlanningOptions PlanningOptions)
        {
            this.Request = Request;
            this.PlanningOptions = PlanningOptions;
        }
        
        /// Constructor with buffer.
        internal MoveGroupGoal(ref Buffer b)
        {
            Request = new MotionPlanRequest(ref b);
            PlanningOptions = new PlanningOptions(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupGoal(ref b);
        
        MoveGroupGoal IDeserializable<MoveGroupGoal>.RosDeserialize(ref Buffer b) => new MoveGroupGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Request.RosSerialize(ref b);
            PlanningOptions.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Request is null) throw new System.NullReferenceException(nameof(Request));
            Request.RosValidate();
            if (PlanningOptions is null) throw new System.NullReferenceException(nameof(PlanningOptions));
            PlanningOptions.RosValidate();
        }
    
        public int RosMessageLength => 0 + Request.RosMessageLength + PlanningOptions.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "a6de2db49c561a49babce1a8172e8906";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1dbXMbx5H+jl+xZVUdyZiGZMlJ+egoVbJIx0pZLxEVv0SlQi2wA3BNYBfeXZCCr+6/" +
                "3/N0T8/MLkDLvguZu8opVSF2d6Znpqffu2c8el53ZV29WubVa/fTxrVd1ujf0WjEt1VZLV6u2abN1v55" +
                "UuuL0ejxP/jf6Pn5n0+yVX3lym6yahft/Z35je5lby7KNmtdc1XOXDarqy4vMbvuwmWFm5dVyR7ZvG6y" +
                "3BaTdbV8Xwk0gJClOLSoCvlQb7r1psvKLls39VVZOCzuXvYqb/KV61zTCjg2vK6by3adY+DuIu/klcFq" +
                "L+rNspAWGSYEIKPvrHUCKUCYrMNLDnbe5U0H7GZtl3cu26wL/GnH2bN5NnMN15j9WJdV19pAU1k8x2lc" +
                "AQCYzrpuZfVtVmNmnHFeyRxnm6ZxVZfVlWuP+aZ1SWMFCBAA2bouQ1c3el1P6+5c5oIZNd1E5sWZvuGi" +
                "67Ytp0uXLep8qVOOSFrVhVsS50QN346zs3x2kbmlW8ks5oDChnnT5FvdNXTPFVjjFpjVWIaRF9ht9C7d" +
                "lSyznOuKMPGuyQUhutdAZ1fONsu88SAAHnPPu7Kdl+j6NPZ4+04gTxIgXNiL2iMfqMyrLRaJL1m+rLEr" +
                "stN5d4Gt1d9NXWxmrvA0pUu9LpfL7KqslwSiWE7nediRcvP1elliucBPjuYyCDalqrvsxw1o9Trf6ruj" +
                "dMoy+HDCRFE6ACfWuHazFELC2x/drKubbbYiYEXFdvQmvE/hx9b7RqlAqdi3hIsC3WMdm9YJoVa1NiR/" +
                "rt2sJN6PSZrc4xzTGvYVhIHqAKAYYUxO23+blMW+wRdNvVnzwTMDgF1flCAuwa3Bxc967RosuFoYXOk5" +
                "IawAd7OaojEhlyvuiIFgfyxLOGwFweCKcXZ+UTcgcqCxXm68GLHpN27Nj8V4hDk9ekjAkyAt865zq3VE" +
                "5Sp/X642qyxf1RthBhl9H2ZJLMtlfQ0qS5gpOwQJtg6bVIBE5ss67/7wmTWMwxKoyJVZvuTy5zk3V5lF" +
                "BTi6bDF1kLFQSzI1wW125Zb1DCKCrFmJhJnNwMLEKghknGVP/OSu8iWkrLAbpnb44PjTd/j65iaA2z3g" +
                "Ir0YgzWUP16UEPKKVE3OgeTbcquAINA6unXllQM4v0CMDEqEAqEAZEfIdgpJAi0bzrUssiavFpjxYbni" +
                "xuVVt9xSLJYtBUw1W26gAbCxIo4hEIH9B+MHRyI5/ThC4/rJax6hb0EF9/TT8QOBBfGuiD4sx258fBNG" +
                "ALD/JUXO0ThsMxpNrNOk1a2d6Ix6bdLuw3Z3obf3aD7T3OC0Nl8kmjsXRAJnUSFSIMw3EP3kMnwVeQa+" +
                "J6qvwH/gl8M8m9bvj0guJgCMaPp8w2mNbfAygK4rbMr1hfPsJizCXainJdQalQlkyDzzmkoB51Dfbrkc" +
                "k69ORWspRXDabNy4OVRnBevAVCGmiIU24OZ29LXLoauhXfknSIMSwgK7ro1MyqHbMaQL1I0ncaMznYco" +
                "ZJGVdetGC1cDa5DbgvlvRYg/IuCJAh0Knn/8UCA4G+p2SKvtCh1ScajmUlXkDfSv63KYSrmg/KJcgO0/" +
                "WcJWEKtktQYNyNduu4Y1lZDBwmG+wplUPlw0ZP1qU5UzUd5UCGl/Yea+lTGr66YoKzYXCiB0IVfYnKSB" +
                "Z6cnop3dDPoCE9pStDQub4nOZ6fZaKPaAh1G995c159QWiyoUW1wNTIxWfd+jd3hPPP2BGP8Thc3BuwT" +
                "0wXZobyb4LE9yjAIpgC9BM6gwni17S4gZoWB8qbMabsBMCTDElAP2OngKIHMaZ9A81a1gVeIcYxfA7YK" +
                "cLmmT2COFqKM2s0CCKS2VXPby3QIBQh4SL9lOW3yZjsSzShDju59JVwmOlp2hFzetpCE2IBCKNg0veyG" +
                "2A+3Q417ucBIC7YAtgqLoGC7km+knHnjsBJKxDGJ5Jlsq8iglYP+If2FnrSXyoaKzdvBkCt1447pnxQ1" +
                "NBNsRcBY5Ze014FjsSVhVkKz0o6r2qUqVbxGl0M3XkDziKyTVsSRULTwQDnLmnIBpSg9MdAqdM4zvzho" +
                "vvlDNXdkzjoYNgxAmrrzWoqKd1tvYMFiDfjReNYTS8rmJSTS1fUx+c6D6CP0lShAUxPQEB2YHjLX9Nv7" +
                "8Gsbfv18BzotekQ3qrKyivjLp7A8+pK04x7CDFAPIjprc2xTaxYtfVIwyWU74t7WjQ7+F35Ud0zaRXdM" +
                "vrQqKlZwpy7yq2Atuez05VfqBwX7Sry7FPRztkW7ZAjpPinq+WQw2JOugy8GKLN6uSxbrrOe0m+AOMjt" +
                "Gza8xYbKKmigBxwcjaz/U+v+UnrDIbPe0CT+08RD5rhfLfMFsFtQOpN8Qczew6UdNgMdR5tdjC6wEsUC" +
                "NDaZaT7fUXEyQ3WTpX+iGlZ1C4MwC8ahqhaTXeK42lKndUH7+NDmg4Z05KhNlg4ibk9jDIQNzz1pnZxA" +
                "c7qTk8Tdngq3awSAA4KFZfJdQnJHo2ld0zWecHG3pnb3EmCCqTywgJDfRb2EojCmhw09a0qyPpCgjrUs" +
                "3Ft7kCtQlMI7DbYc+FEGoHz0Fkvo5Bg+UMP4sHG0APm+gfooW7LbDHYgBhaTjLqEOhKqLGUzs3cMSl6I" +
                "0X10HJtGa3zY9H4rje9DsZI9Yxc3x151QU+rbx98GA/gxYq9X8CM58IkFKJfaI1WJbBAaisg+ZVDoQsp" +
                "D8RVVUSozRiYWD19UY4YN5q/CtVHY67B5yB4kBf065jO2k4bNWoQFArbKMJD2ci6cgjZQVG/GFOXMRSA" +
                "bdmqxZxH4SPxHcQ2OAJ5SLaRFlQfwzIZUYtpzIemOzWcdPLtsXZSmPmv4+w7KjXqN9U3XoTKKqoaAP3+" +
                "DAwFwlodi66awakGp3LVcTs1LkWvfeupkdjT1ejeJmv34TjFnOGpLX+GrMeSgUiFk3CNRBzF/cAoDJ8Y" +
                "DYRpJsiR4IdNmrY7DQOambKDUItDv0KNIEhUoR+vIPFojJC8MoJPXikG7kKg7KodrOq12UCqQ7HtKgNI" +
                "PX5zBReBxAq3gGUlhEcTq6ixrwwGQKLU1yaugZLNrNswGDDP4nhKyGqKAfXwKhkNZvDP+JMWf7uFASJe" +
                "E7WHmMJiGgVzSfssXBf5n9GNEDa8hFASIZXNLmAljLOvyArvYdQuQSO5+J9QFV5YQGNh2L+9Pv1KZNoj" +
                "KvBDuGgIUGzza5rSGrSGXa4fScGkssRFSGeniMSfpgQU7UuO7n0Xa5ItDBoY+gpRZwqjfHbJBffm8P9i" +
                "7G7F2DWjChe/WoxZ8/9LYuwmKaYGKLu3g/DDGyNhtArkvNPoGnqJDfh38O07QRM+Kr7uxl0Ms97jMIpW" +
                "D2Jl6rprB7roruudWEM78ClHIwvGJP7f6K8bdGgqCgDz0u5mkXHgfW4x5HpDN19FU28hfPopzpp4+KDn" +
                "Z7+u72gDhZL8skzqttF47K9n2tSX2EPQMH3xlj4R/QLKYcSgJY4kscZx2EDfJD77dnezOuWJPbuGrdDt" +
                "iYs7hj5nzJbyiAukOfnrlijA4qO6AncRl77B//Qye/A25HeCBycCDKHG8r25KxotBaro7FoAir8t0yR4" +
                "7GWpEYXfMGl6kSMkKWhibDWmT3fmpvZ46l6zmQx4DyEXwBM+4nZxrQISqTh1n8eI7GiuAtqf8RvvVN/b" +
                "B89cbHVCwjKSRBcHYC61jygFaoFm7+BJECPmycN4FjBgWCKJ03f1RggPA1s25ZMwMZ2GBPYRQS22Seo7" +
                "6RC1hgCbaBiFNl5MouusxDmC4cUM7jCui90QW1H3g2E6XaSE6FaEEZYC0D66BqHmgHaYzxrymy1hbjPO" +
                "hqyjMIKH4pNINkblZtS/SNFytAY5GwkP+6yDH5jbB5gWu0kytdEzj0ldIIOzm/ghbFOuHWLjwWIZbAbC" +
                "UvPssqqvg3fg298FT+7y4hNvBkiMsBDUhKCO2fTCM/tzKqB4v0yPwEOhHoGF3XuOsZ8hFuWZNeaZbZ+R" +
                "KlCiYCRjmsPQrj12Avfo3wl95IVWnOhadAlvCIFgYpLeomVe2kpSddeaM57VbkhXqmQgp+xL0aRBPUPA" +
                "ec0Epw0zY4hkhQleAVkicXSe0uqVfaJ3GJsNo7Ft7zvpSstjnrv2og+Vb9B2pR/2wuG3COJLMocl/RlH" +
                "Q+7EWwVpCUE29ZFUaWY+ihoRWPxGOQ07VhSyF5CuHOIonRtriGQhMtINi+S3OLsnBX29hDAU6yFfKHFD" +
                "iXonjUCjqADZtLCD3XtYCpw+IniqTEXgjEfTLey4J6enjx9wmNciVHsjzZuabiWM7uqqbOpKymYYH4KE" +
                "QCoZ6a0GiRdlBQn7dmBmhZAk0osjHen12fOX3549/lTWtF5TTtGDNWr2Pq8XrDJp7x58aK2WjNBOtk7s" +
                "Qlzkq1dnL04fP/RCOI65fzgZBbUw7tpTvt9qyYqgfAZuld83c2OksgUtlm7eqYtCFxnSDGUaxBVQaxIj" +
                "SlNEJoHJQqcouHnECb7UkhHNfQAmHmmAWkNfUXJ7NvSHhcro3m/+l7388i9nT98w4fjbO/t/RM7TX85x" +
                "iNCU6N9cFJ4XZBBjDFTQhYFV0A6y/F290LB5cB01hAs6YZS8Z1RcuhCXTUc4kTfaP8ZQJeUm5AKJVWXF" +
                "1IQ9oBjAYppOxStYiZ385fzli/us/PEBlR+ePP8mUwCIowYShpgNDJDk4iioDSsxaKRK3RQKCuLEamD0" +
                "dWfThY/Eoa/rS9grl+4k++g/Dojhg5ODp7RsTr88OM4Omrru8Oai69Yn9+/D/ciXwHZ38J8f6RJZj8Pp" +
                "aTSn8pJRd89bN9ycBAu0HMvuAJ1YWwkuuHTOZ6HnS7AqaiPg4nj1tI9emcxQJFpu8fRLpQ0BwlWR7/3I" +
                "Ggchcfn6RB8Vkxw3o2R+sRLOFzAnWUCAvCMK8G6IgpPf//vnn2kLql5NpaLd7owP/Ejnf/0G6QuYCMxh" +
                "hH3qDXz+0/Jra6GwZajs4HrRPvqDvmHG6CT7/WePHsojWjdsAPO5vvYtoPZRA1oMXtNC4UJsAEt+6VeU" +
                "QW2W/C7p065eHxhBg7RvK1Z7k7UQi1+kdqRdk9KOs9kWprUYbSA3l/lok3k5IAtLz4CsLMoEC2dqJgCA" +
                "UeBTpQsnquH84Bj/QwiAtRKfZ1++/B5qTH+fv/r67PUZVIs+Pv3hm2cvTs9eQ5T7Fy9fnD3+zLjd5JNo" +
                "Gc7Jt1IrzUQCshtwK3xONjaN4fHYIlTVIH3P6acdkmYnGviTkkqmEq3YlW2JrvcmqQ5inwNVbpL39D4h" +
                "Fi5TVe/h++PsB43l/j2dM5EsDpOrFjAW/YyGMohuU1gfkD6OuJ18D4skPv0QcM2nv1OLJ1NS/PtZSRyQ" +
                "206xib8+uQXpqfOERKMo9iWqeVFuOAXv5igF2TwU7uT1k9NnfzunhZSMaZssMLnBWtejWFHSkfCDFFeY" +
                "eSiReD/U3zMUQbFGMFRV9OBOvj579uev32SHhO0fjuKaNHWbYDyu6aLnXhkvZIfkhSMdj3LOxtHV+XH0" +
                "IRnnplFYbWG40+3zzsn+MaGyNRhgn9A/2vlDnmTUv2zEBdbiayRYIg0JTtmfzqZW4x37HMfHHqnGpANk" +
                "BpIaLJ72aOTUncYRMWx4OyJu1wkQ57PZSULRGB3GvmS3aB7od00yE9tJcBNhd61mCcm3JCSbtLurBWIq" +
                "FtrrhaTSJDliIZb8isv9QAj29lUQXUtTPMlU6U5SQMC7VuZDDVq1gAXxRSJhfX2yVGdKJXWovMEameaC" +
                "sdO+fTfiGG88AMkpeFgcIAncWQ/zvWD5+dJUmc0enEuqVTvdEapsGXtQZsuClRcmpZWIbx/pPN37icQB" +
                "72S24pfvzfxqZhScpv599P9DkCB/n33M8N/H2exn/F+RPc7Eo86zk8cgcDd/++AdI4rh8VM+zsLjQz4W" +
                "4fHRu5BqePvZO3l3Wwj4QAxvENfamwwbdDFC07Mj/6R5m4SRGpfk/IlKlFi+4svzAyO+PU5OG+Chd9Lg" +
                "HXep7rfWMoV3IWaejKWqzL1n2S3jEN4ODWGR/mmFkOpkrQuSFu0gZykyYrDKMZY+2lNZ0e6WVrDeK77s" +
                "LWu36KLYWPgBun/CGBAL75o7CsLGMz83F+qbmE0PqpA3kxNDhvD0XJIFacJhLH9kS3LkdtAmxPPliIZl" +
                "gLWKRYk9zjGwQu+o0iu/D712tjn9pi+j7u21TnRyv8O3CN+q99trfxVe95vf/oYNMKIRGn3Yo9JDrmqq" +
                "AQoJb1tKRFwww3cUMab25LPvawfxaGC/DUMgT4NoGxLPMzeZgvCvj+PwHyffEC66cu+C3TCsSBq23PNe" +
                "oEsc0yco4tGmmK+JO5EdIiJUsxoOgVPsKrxOq+rU+IYWgKbkmz1F5kbthJ9dU4t/h+weeCEWhB4NsyS3" +
                "v927tH0jm/rzSD2lH7YjLnW35kIdWq1WlmzgEJtiQTFsty/RqSGh+ZzZPx4NCDlFKVg4iqI6bxD18yqh" +
                "Ttpds2AwCTmjww0HThQEAvUkVR3SJuBPB90482xkOYdvtWVsNNFjo//7qOsOyKuPlBjhyQ2h2NBHp4oa" +
                "n95tb0w07RrGtissCha4LHPLGbatq6Nfm5fyO+xTYmnd68CpZRgQqv9XJ7KexcyRuAcB2rFPZYU0RDAi" +
                "mD8pQuo8xLDC2bQFthtmTLtnif0k2c2L8kN/cEFpRu32CWWv4vwtoqhf/PNhceRzNb1ePiqRSKq4M0JY" +
                "sYRogLOb3O//oeizSj4JhHwi7lXmmoYiw1RXks+Mh3ancpjYTd5P2HESGu+22H6wxc/DFv+KcmyfmRZy" +
                "8GG98RSoVGfiFc2gaMtpgW9RtjNNOqq+OdqpFAnH/oT6pb3UIPZic3kALFUrsL80JYboGpOlmigCMS6Y" +
                "DPJBZCmTJuDnfnIiXOL8OBpBwfdF040QZOSIkJWqp1r5rDI4duc0CP0FNv0kCQgZfgTAi5dvAFzLvlaY" +
                "AY+OJseUsVqhjzbWKYaZhyJlQx2vN2gULCLBBjWxBuzkTiyPJS6uSiSCY5hYsQKi3jWGREIfSi0VcIAk" +
                "ydYXtB7Z9QJC+C1SR9l604i4HAe278VUZRtFg8WLNoxG6J0QjWDvaJyqMaJAUnmeAvzCPEs71i7axepY" +
                "ekPzDBcKxSvIUX/eOOyQaiHDkp067l99EUTYsR650IzaHtVx7o+3BmMK0U+G2q30QNFtlNnV13njyyFs" +
                "T8OUlejzIYkllKW1W2tsDqhHSl40qLPiPRuSkRiUxctpRW3zmTaQun49aFOJMBMLQ2eUDOzv/qCvkq23" +
                "wFEpMQCZiJ7O99uaL1G3TOrNHgldIxdg9zdwwhMZtLef4ah5nxOVdiOJWHpUNwg2kh8wnu8cKC8AiGrf" +
                "o11Eg9ZojfccUgjtQp5YT4yy+rrrbRJlFnAcD5QaMgSY6C2VhwhtorecXms2TmtukoKaXZ5joEATutBA" +
                "ks+TuvB2AzzeTG7+CIiR2xnz80yYLy4iPdGU2OE4pbcBiRlPsCAma1dYAuN9bpbzcKkVge1aDjLK3ooq" +
                "L2MU33bLDWAmvMS2Ipk8pEuQEo8MatVOLtQqeNdNnVEP7Ju6Rx/LcWAmIyUqkRTqVblwwhuTmouR8dSu" +
                "SA66+ZO1DE3LJy+JvbyEyAVCbGXJ6LJ6E9CBMOyw8nyvADCiVhYSN3xYuW5Kwiy2OGfl7VwKaohsfuG5" +
                "O6EYhWgVpg+8xkXBiM5HLWCgnmWma/FjXXtkEJmEQUKcpSOm7PzI+8G/Ku8/1BFS6JgXq6JUhAJ2IizC" +
                "mUJZMUuL/GK9Gkw2ZkcO+JuqghbJPUwvwFFPJIXhrsFrVpI+OJb56UHqB6Fqt9vZ/0Q1F4O7QdBuopH/" +
                "wOfWS7RgC98Gi+Tp7XrQQzh+Sd+nv0RLJOzqeVMOOzRkxEWL1Fd+1BVqXRUyk9AD5ZLaAl6xKdklRMe1" +
                "0K8KN8SIjgxY80bHAD1SCmZXuPRQJd1TXKXTTYVtHaqiBwxjFopFNoLx8z0SEQ+Ro8efT4+Rc2Zawie3" +
                "z16cv3yNXPrgRUy1+xffh8IGLzBln8LY/zLG/Y2KRBDAZ5PjdvXK8KiMEqNdqTFwMgWA6qLbd072Xrxl" +
                "pCdntUBRvfu86hAXT84TxlzH4E6zO45AD+4HjEU083lv5sYe/VrS7NCc5qNw0+C5fAhXWEk7f8BdykeF" +
                "tOdyFUC8fUmMFZ+e1QNHqBLfNMxXVD4NJHdO6MVeXnRIQUyrIi+4S/6mLt+JHJ7pMXt2nbDE5TfMQ8oC" +
                "bMX/hn4eKivOe/cTifXmq52yQznAoSzfwuGk7wOPqXVu5SWTlNtWYi+llZiEeZVDtHOpUpbprYCkglgX" +
                "w/EmOl66nCBjNVR89QuLiUtRfXjDYjTHY1okXrZmF6J9YaMnAl7KeOH2QKjvXqsVK4oE6+gurF9sEYYp" +
                "Z1LVV83LBQ89qB2fLLV3DRtG5a6JupinrUS7+41McSKqMyevpopxRqPPFm+XELKQf5jRilQotrzV1Ggu" +
                "zXfCKHp7Ebe5SsZHEJJlSqQJqTNr5HSx1kiTpCvn5IA/wAbsjR+oMt+/Muhwv5I+fomTqzLfh1BDQk+D" +
                "tvncTQKzIAHGa0PD+mRyMMVrTaJKVk88P6Q5uZDQ0erGw52SvkDQ3D0BxLnoVXyybZFFwcrMkQZzS9jS" +
                "VUS23uhH431TCRnnYv4qG0g1foj87VKpfheCN9LJPFnpp52L/YYX+sGry7myKlhO+2Duvc3Pj1BAY2/v" +
                "UpSLAMZy7MbFvsjup2SJVhjVyeU26Z2h8rp/Z2h6jWN6u1//Dhdmffw4CkMaWaQ1cUj9OVjyXbFZL+1m" +
                "mG6eHe7z7GJaSdJRN50l9tYFtKkc8pvoXVLJ8eLRPVm2sX082aMH6Ef+UsRwqOm5nqu3+xnj7Ta+PZkZ" +
                "8CQQsObyqkU7+gZPr/QBM5FQs//Wa8+L/Zy25jWPztrK+951Pb6a+Hj33h7UUqgLjr2BUbHKUfTX9Ja1" +
                "FjdVSnbpstZLcZvi4SVF8MrznxoyvZtKVuORHmEAUuqGWXYtw1ZQlmbuDdk3Cb6rG71Zd1nc0WVAd3G7" +
                "zi9T4O4J4/TKCXHSdyxdNWrH4f6ze0b8Oy2RxkeNeWw4zODEW5BE9fnb2dDqj3l2AVv78Ue+5P66vCzH" +
                "Td2O62Zxv5t/9Kdu/sf7+Z9AyrNLAJLrEM5RRUinsqhn8KgsFKO3LUoVTjBjdpJAXhL0p5spwYT4mhWp" +
                "spG+5R23djlHOG9/F8eL9zK/l39WPAMMIFgViovUopB2oc5Lmvg6L9+b9H5VFixA5Ncydr5RFMHNRiKM" +
                "tfntdqVZWn/VY7/iKR1tuIIzfgsz0tqpPTnTIQOLzMOeu+UcQ3KOra/6H1geigy1P8IVQRFD48Sg2F2n" +
                "meA/bRwQEm2wUjS4YviwggteSXjAPHua0HqGyGPwxplH2zRkIXZHTww9C8hhHE+UuhRMQq4Gk2nsXHUr" +
                "hCEBJrHioPQuMnSQY9wPJUDVm60096XoikWfStCrn3VK3lbSA+tV9uTFaXp+LRCaBzBJSYCyb+eT7fzd" +
                "85BQIG39/hVrcR+g4GaX3q9Sw6+wNdjjHUw7Udmje2LqmN97080Apu7j7Y3pyf5QJGV6/26WIHbEr12A" +
                "v1j4lxfgjZHbn35iY1g1Ujiox6VoOB7CSBmQUXg23TkGHu5w9uwubfz9swL99Z+/fOI/3PZ9t2E8RSed" +
                "j/BrEX5Nw6/8zr0Fsc3ULuwblsNLIiRAtedqxzeJ5SmSM70qJpr7ET4P5418D7/z+vAdhJ0E9PzHWzun" +
                "+wtjS1jy0amYzfT8YHTp5Q7QLBK4QPvGuf21LmkuGIPw9MU+V8bfBqj2z55Um4+dB79Ujy14gKxd3reA" +
                "fwbS/tvIEg9AqskY2obO910P5Rwaj63cr2ezzRpK9ogKQ1w2eYOoN8KsiorD8bS7P6YxgCu//Y2aCkgO" +
                "PC/h46TmpRY10ZnS7n3J8ZpZUA1J0KVfYVxfzMGsJP/DAb5bBX82FH9r8lT/ewJMsegyEHwvIVl/DjlL" +
                "7apX5urVbHIybCyxJjlQS2PhukFuzLdtGav4nGqc3DL6L6IXYwDpZQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

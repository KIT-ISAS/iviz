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
        public MoveGroupAction(ref Buffer b)
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
                "H4sIAAAAAAAAA+09a3PbyJHf+StQcdVJytK0/NjNrhKlSpZorzaWqEi091UuFEgMSUQgwMVDNPfq/vv1" +
                "c2YAgrY3F2nvKuek1iYw09PT09PvGVzkd+Z1kderk2mV5NnrPEqDiP4ZzuHfvYvm+2tT1mmlLQr61W7z" +
                "yph4Ek1vtdVMfveO/8V/ehc3r4+CJYyeVOGynJdPLrZn0/vWRLEpggX91WOc0mTCHbDF+VmAUw2T2M2E" +
                "6EAEuB+kyypmBBi73qPgpoqyOCriYGmqKI6qKJjlgHUyX5jicWruTAqdouXKxAG9rTYrUw6g43iRlAH8" +
                "f24yU0RpugnqEhpVeTDNl8s6S6ZRZYIqWZpGf+iZZEEUrKKiSqZ1GhXQPi/iJMPmsyJaGoQO/y/NL7XJ" +
                "piY4PzuCNllppnWVAEIbgDAtTFQm2RxeBr06yarnz7BD79F4nT+Gn2YOtLeDB9UiqhBZ82EFzIN4RuUR" +
                "jPFHntwAYANxDIwSl8E+PQvhZ3kQwCCAglnl00WwD5hfbapFngFAE9xFRRJNUoOAp0ABgLqHnfYOPMgZ" +
                "gc6iLFfwDNGN8TlgMwsX5/R4AWuW4uzLeg4EhIarIr9LYmg62RCQaZqYrAqA4Yqo2PSwFw/Ze/QKaQyN" +
                "oBetCPwdlWU+TWAB4mCdVIteWRUInVYD+fOeuLFzUxBrCbJBucjrNIYfeWFoXjQRWMv1IoEFoUngdgnW" +
                "URkUyDAlTAIZ6JzWm1gSSBJlMhgscnEHrLFemCxIqgAmakpkWuALs1yBiElT6I0wS+aatYGhLehgYmaI" +
                "SxRMTVFFsHKIkU9fwT+JdU2AvIDeBgexdA5mVlhlMfRgiQZ7sCyjuaFFCMqVmSazZMoTFAzKgUDHDcIN" +
                "AKllXVaAWQC7DloNdP1w5R5S+pHcA2GGhL5KI5DahLPiDsjg0wxQG62wDbCt/A5zfvAw6LbwU2GGrJHA" +
                "1oRtiitbEsPFZpZkCbEOCsZIJ4PLie+XBA1A0FSAs2BB6UVeV6u6Qh5TPkDWuIpwT1WmKAkcNlznxW25" +
                "iqaG+Q0fKSxhf2wBMq8EIL3vtbUHyUIIV/Zhj2U7CFkUExXyXL2C/Q7COzifWe79Rw7CstSBJjR5HKcw" +
                "yGGAziovE16rHDBDjCMWU9O6KFDE5Jkp+/ikNF5jBgggAGRpKlCDheld55O8uiFcSkQtJLx0x0DnMkG5" +
                "R/xOrxyRlnkMmgj3KZAGnw6CYQQSwKRmSVjMUDZCw6goYK/RqtHOk80zB6x469ADlHnTRQLqDXFMZjwj" +
                "QLwqIiIIr7WnpBgEgAfcoyopcd/1Tl2Pn9+zNveA4MQucyE+kDLKNjBJeANCJodVoZWOYKcnTFHgk7ie" +
                "4nYmnuKprpM0De6SPCVtSlT28dwnERitVqnIMZBfPAgsSpZXwT9QNID44WcHPso0eBvhcYsQiBhLJ2Qk" +
                "ePoPMwVpvGGZw6TY9Mb2uQ/fte4aJUM1lM+8XWT5HuYBkpsYNcu5Ie5PlXd9ZE1c44iEZrMvEQxlIYh+" +
                "FYTyjlTZ9uBzlF34QzYDAPPUi8KFf+YrsHSQDgqXeoYIy8KtlxNojJDR+HAgsD9Mi3bYEgQDaKngZpEX" +
                "FcqSMk9rESOKfgEmR0G6jI0bABxaaRlVFSorS8pl9CFZ1ssgWuY1bQbWkx2URWZJ03zNlppuJjJBxD45" +
                "6M3SPKq+eqEN3bCk6lCugFlC9kGEi8ubhQU42YEgO+ppIgyuqBFtA7DeQAFWyKus+KLpFLYwUhUYZBAE" +
                "J4LcXZTW2Ai2G6C2f9h/+h7ejncB3HSA8/SjbLAC5Y+IEoS8RK7GnSNmU2Jwn2M3tDMBnEwQRgZOBAWC" +
                "AhA7gmxHIYlAkwJxBW1fRNncoDG3xIWLsird9Gn5yeKdpjVaAhND4tiQ+jgcHB6Q5JRxiMeN1SzK30QK" +
                "XNOng0OCBeKdCb2fDMygv4siALD5xifOwcAuMzQKtVNY8tKGjFGjjd+93e4h9HaH5lPNrXaT1dwRERJo" +
                "5hQiCoRZDaI/I9+iInlWr9jkhv0H+2U/Cib5hwNkFxUAyjTNfYNo+T6QgAZbdsOWpW4Z2hrLfJKkhpRJ" +
                "iTiJpmLAYLeuTZoOcF+dkdZijihEQBVmBqoTPQZVhYAiTLTIcP5NJ1OlQQLCAladG6mUg259NiyFxZXP" +
                "GI9SbQVQw6Y3NzlQDeQ2Uf4dCfHnCDhkoG3B868fChhOh7of1uocV9cUpC+gblAbRLCjeHeCqAQnISAe" +
                "HATWx6BVXxrY8ThN2xM1VFIY8jrI8ijIc+ijRRjnIAtAOwOMZXSLFhI4eKS9QZGDLEPNmZUpizF4DF32" +
                "zWAOe524i1ohAcmdJgcc3IQimYMYop4w0NJ2jgKZHMia2TNWMIQzDwbrgg5IXolcQFG3yWuwGWAO8I9C" +
                "/H7SXYoX+adVnveR9QVEk6BXJHJ0Y8KerIBHgctVonyw/9rYf/36AFLE2aA7hUeSOfpFE5D1Td6tcA1B" +
                "8LLN5szjGSxTqTYEegEgIG/LHq5tXvDg3+FLNoCpnTOAv8vF2gI7cQkG7CK6s/rJBGejV2x5Wo1G9rQP" +
                "+gLbQjtvCOoexvksbA12UlVg/QKUaZ6mSYnzzCdoqYH6ivQdLHgJC0qzCHJPgR70tP+pdh9RbzCBtXdo" +
                "IYcCGcd9lUbglGYxhoaQfYGZxadAzTcFPnZWEqk52EoVxWpoM81mW0KFMGTHhPp7MnmZlxWGilQdc1xL" +
                "AyfkKuhUJ3mMFsm+4gMN0XTGUFZqoqKrMQwECx4Jax0dgawyR0eegyNxAvK5yBevGPnKY7mD3iTP0RkJ" +
                "cXL3Jei6GdCjVGS3ALHfIk/j0m56sFqmRTJhrcSuDE1c9CvIFXCHae8UOcWqeAOgfHSRHO5k0GFjU2S/" +
                "MKhz8XkBfk9S4nabHiA27LqhRYYBuuCPjW2mGkahRDGZOQd919TZP+2mT0pq/KQ8oO3pupgZrFVlg4Ts" +
                "TVmrUQBcLrH3JRhOOLGhmwvqf4q7ILfFIPl5h4JFifKAnAMmBGtpu4nLzoCWjif+r0SfgL1i9BjAPN5q" +
                "wxFVcMPtMpLw4G2kXQOJLJQU+0soyNohAMukZBslcsKHPGrwJnEE3EO0jGjMNClMyJBa9L1sNJZQw1En" +
                "aQ9zRw5Tj2EQfI9KDfUb6xsRoTSLLAeAsj6tKCXCWvZJV03BjYGdemf85eRIAPpJG+FGpB7PhtfWm7sE" +
                "QBYKg+hUJr+CrIcpAyEZjrdrKMZDBh/H9iwPuCCfIw65m4o0WktoGKAdSisIarFtybGDCRKV+EcUJPzU" +
                "jeA9Uob3HjEFHkKgbKsdmNW12kCR7lmWAcg9srhEC8tisZmDZUWMhyZWnMO6ovuVo8mt4hpIUk+ruiBZ" +
                "4sZjRmZTDEgPdnzMOznS/YnphnIDBsiSKV+uKA5PppE1l7jP3FRu/6M/aQM1tyCUSEgF0wVYCYPgFW6F" +
                "D9ES8O9jqAgs/qhQYRERh729PntFMu05KvB9MIrBJdxEa4zjc5gQDGB+iRyMXOblJ3zsmJDwV5EAFO6L" +
                "O7rxnqxJbKHQYEPfmYKi1hhqhgk3cPh/MfawYmyNftzis8WYNv+/JMZ2STE2QLF72XL4xsrC0Mqy81aj" +
                "NSwoNsC/W+++JzLBS6bXw7iLFusOh5G2gxUrE1OtDfBFtc63Ep1ly6fs9dT99fy/3t/rCCM+KADUS3uY" +
                "SbqBu9xi2KtFYmV8YyL46xeHNdLhk56f/mv9QAtInCTTUqlbOuOxOZ9Jkd9ivjAjX7xEnwj9ApTDUTan" +
                "/ABFdwZ2AaWJ+y3tHmZ2vCc6Vg2WgpfHTa4P+hyjZBVZvBUKqs+cIgFzP9kVeIhI4A7/U2R266mNqFsP" +
                "jgRYBPL7g7orHJ9KaNa3GtzHf2tsn+jYyAtGYIpgmmoRrTiYTdEsl7Dawo0Vme9eYzMa8NH5TNPTtFw4" +
                "VwKZ5eKYD5JYosN9yvCoU/2oC5662Ky97TS81AIOgNmrJqEYqIb2xMGjIIbLTNrxNGCAYQkvMlrlNTHe" +
                "ZGPj148tYqFmcqK0AA2x8ZKNXgenNQhYyGEUtPFc2pKxIqsCDK8pO2uNfB2sBtmKvB4YpuNJUohuiTDs" +
                "VAC0RNdAqBkgO5jPHPKbpjkVmURFXtNGECgSttcxMjNF/VtsaLTCpFybInFeGRiXD8sYJHbj5cacZ+7S" +
                "aEAMxC6UIXRR1iaZL6zF0lqMPiYyb7N8nTlpSu0fYk9u78UTMQP6XIUxoxCqBHXUpqc90x3FBo6XaQoB" +
                "94l7CBasHhYdnFcHfqkD99N13qwMMwVGMiZRSW4DUcfuHv47RONyzjl+ngtPYYwQEIxLi2q0TKQtpbG2" +
                "rTnds9wtKUQy4E7pCor7QT0lwE2OKSUdZoohkmWCSamyRxKH8aRWV/oKvUPXrB2NLRvvQyY8jHRhykUT" +
                "Kj6Btkt+0QkH3zkQL3FzaJoV42gGlb9IM5e0DSa1q24w6qOwEQGTr3mnwYrFccImNRHuwMcNqzZoIjTS" +
                "jkniO4fdSRyXPhsJ1W2GhuKGFPX2GgGP3iV5XYIdbD4kWPhDcXtWpiRwBr3JBuy4k7Oz48Me+by4Gxoj" +
                "zYp8yQGJ7C4p8owKFdCxKtC+3jfgm21ANNFWoLBvBZu5bPFEEh/wSNfDi9G74fFTmtNqhXIKPdjMzot8" +
                "XhGshHRpCws+OldNRnAnnSesgpvk1dXw8uz4mQhhN2b3cDRKH6TiWjhflpqyIvuUcZd1UzdG65dSM6vY" +
                "RTngsowyT5FWQFqVGE6axqZMsEyFUCTaPEcERyvN/7LGhZ9ogGrDXF/fl1D8tFDpPfrNf4LRy++Gp2Os" +
                "dvztneUPEuf04zkOEprkNs9I4YkgAzGGgQp0YUrDjrWXV63yOYfNrevIIVzgE4ySN4yKW2Pjsv4IR/SE" +
                "+7vgQ6EMNQeJlQXxRIU9QFGA8cRHRRQsxU6+uxldPsFaCwmo/Hhy8SZgAIPgxLIwiFm7AbxcHApqpYoL" +
                "GrFSV4UyCIZkNSRZx6LTPiKHPs9vwV65NUfBH/5zDym8d7R3ipbN2cu9frBX5HkFTxZVtTp68gTcjygF" +
                "ald7//UHnmJBFlOWczQnE8nIqyfWDS6ORwW0HJNqDzphNRvsgltjpAR2lsJWnSRpokEA08WvU6pwpOop" +
                "yS2evWTeICA4K9z3MjLHQZC5pCJMomJUYItRMpkshfMJzFFgCUDPkATwrE2Coy+/+foFt0DVy6lUaLeN" +
                "8Z6MdPP3NwEsW2kwh2HXqTHwzS/pt9qCYdNQwd56Xj7/ip9gxugo+PLF82f0E1oX2CBBM1dagNpfgzPf" +
                "eowWCk5EB9DkF79d5nGd4ntKn1b5ak8ZGlj7vmK1u6wFV25A2fpyhZzWD6YbMK3JaJtioEyiTerlFMam" +
                "Z4CtNMoEFs5ETQAAhgIfVTrtRDacD/vwv0GPCrW/Dl6OfgA1xv++ufp2eD0E1cI/T398c355NrwGUS4P" +
                "RpfD4xe621U+kZZBnKQVW2kqEhJQtKXmZF1TFx53LWwdg4lIQfodvGZHHPijIjZMJWp5IbZFcn1QSbXn" +
                "+uyxcusJa+JbmDihyt7DD/3gR47l/uTjHEmlc2qyeWWDjW0ZVFJhsau3GDjahj+AReJ+/Whpjb9+Qi3u" +
                "ocT0F6woDojLjmIT/s5sDXVfhAqJYikKjOKkLl3hJXPQoLGu4fXJ2fnbG7SQvDF1kQkmLjAfKmCqMOtQ" +
                "+IGKK9Q8pEi8DPVTEIHBwfVgXFXRgBt+Ozx//e042EfY8uPAzYlTtx7F3ZwWDfdK90Kwj3vhgMdDOafj" +
                "8OxkHP7hjbNrFKy2UNrx8olz0j3maZ5xMEBfQX9n57f3JEb9k4JcYC53rZKV4yGiKfZHZ5Prn/qS4/hC" +
                "iNpr7UShn2Wp1uTRHnU7dauxIww2vB8Rt+0EkPNZbCWh0Bhtx75otdA84PecZEZqe8HNQdDjahabfPNC" +
                "sl67h5pgktnIZSMk5SfJI6k0bkz3EyHY+1dB6Fqq4vFQRXcSBQR417z5iiTK5mBB/NmTsFIRSvVwVLtq" +
                "K29gjpjmAmOn/Pl9D8cYCwDKKQisnggPCdxpD/W9bo0WAxI2HTSnVCt3eiBS6TQ6SKbT2isdUnwM6ufn" +
                "jKf5EFIc8EGwJb+8M/PLmVHTF//e+f82SBB9CL7A8N8XwfRX+E8cHAfkUUfB0TEwuJn9fPgeI4r251P8" +
                "ObU/n+HP2P58/t6mGn5+8Z6e3RcBPhHDa8W1OpNhrS7KaFyt/zvhrRKGksNexT9LFJf3lYJouxF/7nv1" +
                "3fCjUdv9Hlcpb7bmMoX3NmbujcWqzHzAM398oIvsUBsWadaH21QnJokLdF2aOUuSEa1ZDmDqvY7KinK7" +
                "tALrvdzDxrS2iy7iWsMPoPtDjAGFVPD6MEFYd8pid2m0iln/aADuTe+MhhLcPwmiQRp7/EUOyVCOXI82" +
                "2Hg+FcVrBpjLB5jZHY52KzQOh1zJOjTa6eI0m46c7m209nRys8O7pBTvt9H+zj5uNr//BWtRhCM0/KND" +
                "pdtc1YQDFBTe1pQIuWBKbydiVO3Ra+mrR5/QwP7ZDvEY3iJfZ1MTToDx1303/Bfeu2gCE3hv7YZ2RVK7" +
                "Zcdzgk5xTElQuMMkLl/jViLYj02WV6T8MScOXqdWdXJ8gwtAffYNTlOw6shO+NUUOfl3JbhUZekKQg/a" +
                "WZL7X+5t3t65TXHN4pbSt8vhprpdc8EOLVcrUzawTU2yoDBs15Xo5JDQbIbZv31hQoJCBQsHTlRHxdxU" +
                "ohJyr93akFD2D0DsKPFnECGBCHlIRUDOY+zEPOhpzuEdt3SNQj6o97+Pux6AvZpEcRGeSAkKC/r8jEkj" +
                "6d1yZ6Jp2zDWVck1wLtPMcIa/aCDz81L9exhT1TffsFYy6nFMCCo/s9OZJ27zBGf51ZofUll2TSENSIw" +
                "fxLb1LmNYdnTQHNY7j5aE9tTbCbJdk9Khv7khPyM2v0zSqfi/C2iqFn882lxJLmaRi+JSniSyq0MMZYr" +
                "IertKkxqut//Q9GnlXwUCHlM7lVgigJFhqouL5/pjklO6PimCT+E2DG0jbdbbD7Z4td2i39HOdZlptkc" +
                "vJ2vO3dH1ZkJcatny3GBb5yUU046sr452KoUsXeOEPdTe6pBbMTmIgt4w3G0NafEkhUlSzlRlOMRLMvY" +
                "XCaNgC8EORIuDj8cDUGB7wtN60L9Y2Zgm5XKJ1z5zDLYdUc0EPolLPqRFxBS+hCAy9EYgHPZ1xIwwMN6" +
                "3sFQmG3FfG3rFC3mtkhZSYcHygsGm1QWqmcN6MkdVx6LtLhLzNoLEzNVgKm3jSGS0PtUSwU0iCbpRgpa" +
                "D/RANzF+WWPlbl2QuBzYbd+IqdIykgZzVxsoj6B3kvBVA844ZWOEgfjy3Af4Z/Us9SDxnUat2y3pDNd0" +
                "gUeC9YSnXSHWQkolPefZvGzAirA+1ypzRq1DddzI3TrWmCpJZLzS0gMmt3Jmla+jQsohdE0tysz0UZvF" +
                "PM4ycjqmyIF7Ile0scSbDSgj0SqLp9OK3OYFN+jz6deII5MgzMjCKNu8LbctoK8SrDZAoyS2O5TPQ8uy" +
                "RukaCwig4XPi6xxTEHxiHhEOS714w66nPdzb3InMu45FND3KCwQ2kgzozne2lBcAcGpfyE6igWu03J00" +
                "7pCCbWfzxHxiFKuvq8YiocwCGrsDpUoMArbSYpu6rOlgOJ1eK2rDNTdeQc32nsNAASd0QQPdic2dAFSg" +
                "4252kyMgym7DO5IceT1fOH5CU2Jrx/W7pJjuCSyICcolX8EzMdOodruqw3KgUTorqkTGML31XpHeI38v" +
                "YVuSTALpFlgJjwxy1U5E3Np3zDKN+JzrFupCPizHATN5EVGhCtYN0giHYkxyLobGY7vCO+gmJ2sxNE2v" +
                "RBKLvMww0Z/qzLzRafYqoC1j6GHlWacAUKbmLURueLtyXZWEWmwOZ97bERXUILHxTZ+wtJtSK0wPReOu" +
                "c8GHLWAgPZaZrsiPNeWBQsQkTGoqLB1RZScjd4O/Sp484xF86IAXVkWxCD0Y+MLCnimkGWNpkUxW1KC3" +
                "MFtyQO4GslokEpgiwPMVF4abAh5jJelhn/Djg9SHtmq32lp/TzXHrdsYoF3IkX+7z7UXacESfBuYZLph" +
                "08fvQTs+Rd+nOUVNJGzreVUOWzykzIUWqVR+5FlGVb+cxmwrF98WEMXGbOcxHc4F/Sp7JwfpSEs1MTpa" +
                "5KFSML00o0Eq6u7TykfXF7a5rYpubRi1UDSyYY2fH4Lj4Fk/+BH+etoPfqK0hCS3h5c3o+vwp+PWA5dq" +
                "lwc/2MIGEZi0TnbsfxvjfqciIQLgb5XjetlF+6gMM6Pe59dyMgkA66L7wd93TjqvOlLWo7NawFGNG5Ry" +
                "Gxf3zhO6XEfrFqkHjkC3bmRzRTSzWQNz3R7NWtJgX53mA3u32w29sJcGUTs54P6IS0+BtWd0FYC774aM" +
                "FUnP8oGjwlR1kdFBI04D0Z0TfJWSiA4qiClZ5Fl3Se5Gkk64wwM+Zo9dQyxx+Q14UFmAzvg/oJ9Azeat" +
                "G2HIepNqp2CfDnDwli/B4UTfBzym0pilSCYqt83IXvIrMRHmXQSiHadKZZliBXgVxDwZHC/k8fzpWBnL" +
                "oeK7j0zGTYX14Y7JcI5HtYi73kqvoPqzju4JeCrjBbfnsK+xI+8iI1dRRFTvPeKtH28ysPKnVNWXzZI5" +
                "HnpgO96bauPiq3PWCaQuZn4r0u6ykD5Naj5XKyR1V9eUlZ28XvuGhfztjJbjwoGbM/UW9w1vcivl6tRA" +
                "7snT8fvkmhBPUJ1ZQaeLuUYaWTozhk7GAlhLvcEhK/PumYEOl5k06Ys0uUuiLoIqERoatIxmJrSbJcQJ" +
                "9dz8CDkwxXNOolJWjzy/mOMVtqPWjdtb/KRAUN09AoS48OVntGxuixaGcqTW3KJtaTIkNt+hhsZ7nU1Z" +
                "1qD5y9uAqvFt5G+bS/k9MbyyTiBsxa+2rlJrX6EGXl2UkLeqllMXzM7702SEGDT25iFF+Y3chKJ33DVF" +
                "djMli2QFo9q73Ma/pZEeN29p9C/O8+9Ta97hglkfGYdhUCONtHoOqZyDxX0X16tUb4apZsF+l2fn0kqU" +
                "jtp1llisC9CmdMgv5ItsvePFvUc0bd327mQPH6DvyTV09lDTBZ+r1xvx3O020h43M8CjQMAKp5fNy94b" +
                "+HXFPwATCjXLu0Z7vErNcGu8WM9oW3reuK5Hqon72/f29ANzJ0Z9DkbFMlqhiPGntSI3lUp20WXNU3Kb" +
                "3OElJvBS9h8bMo0j/stBj48wnGJfzLJzGTaD0jRzY8imSfB9XvBdpmn8QJcBPcTtOh/nQHXf3Alj/8oJ" +
                "ctK3LF02agf28uVHyvxbLaeLJFXmxobtDI67BUnvQUYw0OovUbAAW/v4D1Jyv05uk0GRl4O8mD+pZn/4" +
                "azX7y5Por8DK01sARNch3BhDB4PjfFovbShmJkE334zZSgKJJGiiGzzyAqDusB814qe9sbucw563f4jj" +
                "xZ2bX+SfFs8ABYqNKy5ii4La2TovaiJ1XtIb+f0uibEAEd8mrvNOUQRu9i91hLX55WbJWVq5XK9Z8eSP" +
                "1p7BEN9ZjLh2qiNn2t7AJPNgzU0661NiPy1zK0J8y4OJwfaHvVvDUWjgGRTb81QT/JfaFIlngyWkwZnC" +
                "+xm44BmFB9SzRxOazxAJBXdi7mxTm4XYHt0z9DQgF9lKcJ4KIEFXgxEaW5eLEmNQgImsOFB6iwA60DHu" +
                "ZxSgamBLzaUUnakoqQS5CpxQEluJD6xnwcnlmX9+zTKaAAh9FkDZt/VKV/7h9xBxINr6zSvW3DqAgpve" +
                "il/Fhl+sc9CfD4C2p7J7j8jUUb93180Aqu7d7Y3+yX5bJKV6/2GmQHbE505ArnL9+ATEGLl/9D0bQ6uR" +
                "7EE9nAqH49daxrFhU2b7GLi9NVe2O7WJ7Vn1vLh+/fJEXtz3xzbseExOdD7sv+b2XxP7r+jBvQWyzdgu" +
                "bBqW7UsiKEDVcbXj2LM8SXL6V8U4c9/Bx8N5PekhK88/vgdhRwE9eXlv53Q/MjaFJZ+fkdmMnh8YXXy5" +
                "A2gWClxA+8KY7loXPxecU7vOJJVco8X2T0eqTWLn1i/lYwsCEGuXuybwexDtnyYWeQBUTYahbdD50nWf" +
                "zqHhsZUn+XRar0DJHqDCIJeNnkTZdKOk2B9MqicDNAaSVG/UZEB04DmNMODtzMtcE5LSvSk5ro1exU4u" +
                "/fLAFnNgVhKvapduWR67onBOntYa0pdplGDOgGT91eYsuStfmctXs9HJsMGCLyWJOCiyLpJKGafEWMXX" +
                "qMZxtzz8h4z4s0uf/pQRRgvqkuIHdem+ZiRfbZLPNd0P9jux6bW+sSR5GH7HP/BWg/PL14H+OQ4O4b/e" +
                "zW8L4N6NLWxZFfmUz5KLJ9X4pIzAPDkdn78bBh7Mp02YaNjxQWlgcr4P/rMAX10PhxdX4+GZBfysCbgw" +
                "U5NgVXeErv7U2G/GBNEMj1QmcvO2DWWiQRJ85A+eWUaORCrwR4s0URuTJ+jCd/tjUywTFPAUNzrQLNjb" +
                "09Ph8MxD+XkTZfxqjy1GL+spUgF3+aaTELuGOXk5unZ0wWFedAwzoc8pbMUtu0eKa/NJ0tiQwyxKUoyu" +
                "7kDveohXNzj8joMvt9ErDKrSHRxgbyNosUv/0zhqFUXlD1ZnWJWSkFkd0LU8/AmDHRMQzrM75Tj46gE4" +
                "z7Ie+gm4CR3z2cWzFD49efPG7eTj4E+fi6Bc09yF4edQV26IaK5WE+lslhT2TtLKlwKEiYkbk/DZ5Ot/" +
                "wSQ+j8zIFI3txwPgdc47eOLN6GbsgzoOviGAJ/ZDW/IFMow6xngvxZI/bUAXsyoJEEqzwALpNvmMvUcV" +
                "WPmdkU8oJYXp+swXaGNyRK0TVEtG3F1uGcmdIGyjecqMQtJmUs/n3gdfKvOhethvaokK7vX4wqsh1iOf" +
                "ovHBpcnhFP4Nb7uvuPdP+skhtO2W3qVj/JWYOPSS0x/voAf0mj2s19v8cMzDUM2nkaR6sDIHQy139KFC" +
                "4C8s0wK/j9+SlrrBA+r8+9XJ+Zu318Pjb/BPTx5evTm5vAQJE+Lb4dnxY219fvnu5M35WXgxGp+PLkNs" +
                "d/z4mbz0HobS8AQ0Qfjyx3B4+e78enR5Mbwch6ffnly+Hh4/fi7dTkeX4+vRGzvWC3n+9vLk5ZthOB6F" +
                "J39/e349DKXMBICeHD/+UlqNzy9giNHb8fHjrxR7tR2OH/+JYhYuC2zvNbbfz2PGUtrdjE+uxyH8dzyE" +
                "KYSnIxC0NzApoMBhR5N356M38PdNeHUy/hZaX96Mr0/OL8c30P6pEvP16ORNG9gz/93HoDz3G3qvtBOu" +
                "zYtea3VeX4/eXoWXJxdA5adftl+2IEGTr1pNrkcvRzJFePun1ltQPX9T4F+33vHFTfr2Gwq48F3UDTK/" +
                "uoYGISBwefNqdH0RKhM+fvbUMoUQC9hlePo35EXgh3fQDpkCGioFPVzxv/ROiSYMc375amTf0e0qHhs0" +
                "8Loched/C29Gb96OaZ2e39s1Dh8RM5+8LJGPO1a7OzSuKR/7HzFrfiLDF2P3M83PxKzXuGtbixfQ4pSw" +
                "V2dCSCv2XB0VJbE7zlfrKeWOyxe7rs6Qe7Qfx/qVNB/WPl+lrbeIt68tP9Cj5O1v33kgqLJTvpJFWlwL" +
                "qeyp7CfuLPaT5glsSQkspDafsiJyeY48stC8E+F9a1/aTpxKcd+Bg3eSLvBPkhOwwY67rXes5u9zrP9j" +
                "yOiatJcVzTVlMT9VuY9nAHMw/AHezrR78wrvnl5fJtXdkbtd2SVPlDWbR2cYlx2XgHuH8reGyHZ9Ae6f" +
                "GKfJZb/ruf4dX7r+pwMz9lPZv8c3si329nYALjD5b2htSRATfAAA";
                
    }
}

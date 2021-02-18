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
        public MoveGroupActionGoal(ref Buffer b)
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "df11ac1a643d87b6e6a6fe5af1823709";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09/Y8bt5W/C9j/YRADt7uNIm/stOht6wKOdxM7iD/q3ebLMARqhpKmOxoqw5mVleL+" +
                "93ufJGektZNDrd6hlwZdzQz5SD6+7/fIPLWmsE22pD8jk7elq6tyNl35hb//tTPVs4tsAX+mZTF67m7t" +
                "143r1vie3o4e/ZP/GT2/+vo8823BE3jK07qXXbWmLkxTZCvbmsK0Jps7mHW5WNrms8re2go6mdXaFhl9" +
                "bbdr6yfQ8XpZ+gz+XdjaNqaqtlnnoVHrstytVl1d5qa1WVuubK8/9CzrzGRr07Rl3lWmgfauKcoam88b" +
                "s7IIHf719ufO1rnNnl2cQ5va27xrS5jQFiDkjTW+rBfwMRt1Zd0+fIAdRveuN+4zeLQLwH0YPGuXpsXJ" +
                "2nfrxnqcp/HnMMbveHETgA3IsTBK4bMTejeFR3+awSAwBbt2+TI7gZm/2rZLVwNAm92apjSzyiLgHDAA" +
                "UI+x0/FpArkm0LWpnYJniHGMXwO2DnBxTZ8tYc8qXL3vFoBAaLhu3G1ZQNPZloDkVWnrNgOCa0yzHWEv" +
                "HnJ07yvEMTSCXrQj8Nd47/ISNqDINmW7HPm2Qei0G0ifH4ka9zIFkZZMNvNL11UFPLjG0rpoIbCXm2UJ" +
                "G0KLQHbJNsZnDRKMh0UgAT2j/SaSBJSYWgaDTW5ugTQ2S1tnZZvBQq1HogW6sKt1mwHCoTfC9Ew1GwtD" +
                "B9DZzM5xLibLbdMa2DmcUYpfmX9Z6J4AemF6Wxwk4DmbW1vMTH4DMyugBxBlV7XAg96bhaVNyPza5uW8" +
                "zHmBMgM/EejIINwAJrXqfAszy4DroNVE9w937iNt3QokVtnyvvWk1wiEGSL6VWXq1zxnnTtMBt/WMLWX" +
                "a2wDZCvPU8cvDjPdwfxUmCFplMCawKa4s54IrrDzsi6JdFAwGl0Mbid+XxG0I4BBawHSgh2lL65r112L" +
                "RKaEcDTCdq8MslVrG08QsenGNTd+bXLLJIevFJpwALYAsecBzNHoe22egAogpuvwkscDCQ+iFoVFi5TX" +
                "rYHrQYRnz+aBhv/uQGR6HWtGKMChGlsgBJjS2vmSt8zB7HDWhqVV3jUNShpXWz/GN94mjRkiwgCg3rag" +
                "DhtYwGs3c+0VTcfj7KY0NZ4ukjcA8CWKQCJ9+hiRtXIFKCVkWUARvp1klwaEga3simYyRzDY0jQN8B3t" +
                "IHGhMNICpsZsRC9Q/uXLElQdTbSc87pg+m1jCC+88YnGYhgAH+Zv2tIjEx6NnsQub96ybk+g8OpeONkG" +
                "wKmpt7BS+AZCx8H+0LYb4PySUQtkU3Q5sjfRGK93U1ZVdlu6irQroTud6gmJRLNeVyLXQJ7xILA7tWuz" +
                "v6OoAHHE7057s6bRd+d8PUAHzo0FFlIVvP27zUFAb1kMMUK2R6Pr8CEdIjbfP1CNysnNE94KrACrAXlO" +
                "hFs7bohcq1JwTKSK221IlvY7E95QRHrcKhGQ8hFU3P4JLFCq4YMwCMBLFI+Chp9uDTYQoiOApq5TBJaA" +
                "7lYzaI/A0TKJUBAErI4YbwVCA1RYdrV0TYuCxruqUxmjq2jAIGmw2dGIbR8APQ3C1LQt6rIEqyvzrlx1" +
                "q8ysXEcswop0D5KReqrKbdiUUxYjG0UMGKCZeeVM+4cvtGUcGaGK1AHThWwIg5vNPMRCnmxFkCxdXgrR" +
                "6+wIyxlYeKAkW6RfEKW0qXkOzI0IBoqZZNljmeCtqTpsBWwI0zs5G3/+Fr5e3wVxuw9eokWF7RoUTiJl" +
                "EPQKCR35SYyr0qIAwG5ojSI8WSOMDaQJegYFJPYEDYBSFKGWDc4WjILG1AuLNt8Kd9DUbbUdEyGQ7Knz" +
                "qkOLYWZJYFtSM2eTs1MSrTIQUb0NGkgJnrCBW/v55IyBgQ5gbJ+UEzsZ34UVgNj/kuLndBK3G1pNtdfU" +
                "8wZPeU79RimAnYZHh1Dye1Skqnk1soKaN4RNQFzUnCgm5h3oBuI6+EyirluzgQ4MCcxzYrKZe3eKZKNC" +
                "QYmnz0Q4r8lR4jIJcDB9t2yIKv8Ql6zcrKwsqRuPs1JtxqDBzt3Yqpowl12QbmPaaERwNXYOShZ9DNWY" +
                "ME1YbVOTSfA09UsTCVGCCAEK4HYqAKHnmK1RoXglOp6MV9MCFDZwwsI6QB9IdtqC70jMP0TIU4a6K48+" +
                "wmhAfjraR6KzvQPr9oJkhslb1BYGWIz5FWQouBcZEeQkC94JEcDKghDAhYaeqMTKxpK/QnZKQz7HGE3J" +
                "woF0AD0OMFbmBm0qcA1Jz4PKBwGH+rX2FYs2eA1dTuxkAcxPhEatEIXkiJPrDg5GUy5AMlFPGGgVOptM" +
                "FgfSZ/6AlQ/NmQeDnUHXxbUiKFD6bV0H1gWsAX40EjEgxabzIs+2dW6MXCAg+gh9RTJIuRQYtAVSnYyC" +
                "gHkXfm3Dr18OIFKi0XqnJCnriD8zA/Hfp94W9xAkMVt30aKewzZ5tTHQfQB5eQOsipvrGh79G/zKJjM1" +
                "TE3mb5zYZWBUrsDiXZrboLZsdvHyK7ZUg6JjIzyF/hwbQ8NkFOo/Ldx8ujPe47YFixkA5a6qSo+rdTO0" +
                "6kCvGf0G2+5hW2ktmUtUK9gPCuCJ9n9J3cFq1u7TAHoqoHnoryoDfm1dYHQJ6RioWvwRVIo5EHS0pUgB" +
                "Ak+1FO4hrprPd+QLTZKdGuqfCuqV8y2Gm1RXc2xMgy/kYuhyZ65Ai+VEJwQN0dzGcFhlTbOvMY4Ee2+E" +
                "ys7PQW7Z8/PEOZJgA7ls5NC3PP02oT5A58w5dGOmuL6PJvX2E2OCLBP4gQhx6arCBwkARk3elDNWVewB" +
                "0dJF84KQAa+aGKlxFPJibkBhGQNC3Mmis8eGykljURnj+wbcpdIj7+WnOBv2+tBkwzhf9rsez6nCUSim" +
                "ICPodBybRuto2PS+p8b3/Snxauxi57BbbYg1shMWzEoB8GKFvV+cTmhhl3EtaBdQ+AYJrgA1wLwKJicK" +
                "B3QkBBGsuQM7+71xMR1PfGcJYgGBFehbgAG904YDs+DGh20kMcKspF0ziU54CiGWFKvdIw196dl2MVEM" +
                "kTMOTiiOgGxE24hGTh/DNBnSkamDjlYUqjvqJO1h7Uhh6lRMsu9Rw6GyY+Uj8pRWUTsAKPszCHYirNWY" +
                "FFcOzg4w661Nt5ODCOhPbYUaEXu8Gt7bZO0SQVkqDMKTL38BwQ9LBkQynIRrKFJEhiCHCAMNxFhhRA75" +
                "pjppNJ7QSkADlXYQdGTfuhuJMwpylehHtCU8KiMkr5Tgk1eMgUMIlF39A6t6rQaRUZ5lGYDUI5tLuAgk" +
                "VtgFmFlEeGhvFQ72Fd0zh7a4SmxASZe3XUOyJI7HhMx2GaAeDPyCOdkof2LWwm/BGlkx5v2awvlkJwXb" +
                "ifssbBv5Hx3OEN+5AaFEQirLl2AyTLKvkBXemRXMf4xBJvAETKPCwhCF/e31xVck0x6iKj8BGxlcxq3Z" +
                "YDqAg41gD/NHpGCksiTNkc6OEQl/mhKgcF/k6N53Mi2xhUIDhr61DQW/MWINC+7N4f/F2GHF2Aadu+Wv" +
                "FmPa/P+SGLtLirElit39wP27VhKGVoGcdxptYEOxAf4dfPue0AQfGV+H8R3DrPd4j8QOQazMbLuxQBft" +
                "xu3kS/3AwRyN1BlOnMHRXzuD8SAUAOqyHWaRceB9PjLwalMGGd9bCD79HGeNePigG6i/NgfaQKIkWZZK" +
                "XR+Nx/56Zo27wbRjTY65R9cIXQOUw6ZeUGaBoj6TsIHSJD5Lu8Osjnliz67BVvD2xMWNQZ9j/Kwli7dF" +
                "QfUrl0jA4iO7AoeIEd7hhYrMHrwNoffgxJEAMyC/36m7wuGqklZ9EzIB+BATAYTKXobRgDWCWa6lWXPI" +
                "m+JbMd81mAjAYGWWetrYjge992yuqW7aM1wwAa2dOOmTspAI8piyQ8G/vrcPojrbrMPDWpJMBI5A2a8+" +
                "vpzMVkN+4ulRaCOmOcOQGkDASEUSO21dly8pf7gNge7PwuSmmgQyVQO6YpvkLZMOR1GBELiphFcQbJIE" +
                "5ZmRiQFWWM6eWy/tB/tChiPvDAbweKkUvFshjLAchC2BNxBxFvAPxjRHA/PKUeWKaVxHbCFgJMavg9Q2" +
                "R23cbGm4xlZc8KLRYBkaNxKLIzSok6TXoqceM3GAEpzgVAaJu7Ox5WIZbJjBrowxKXpTu00dw/zc4SCx" +
                "/F3+fCymwZgLPOYUY5Vgj9r5xER3hruBB2StgsgTIiUCB/uIJQ3PMFIVKiliV9307doyhWCMY2Y8ORSE" +
                "pchS/GOKdueCqwh4SbySawSBcNI8q0bURBRTGmzX1FNeln5lI0ID2WdfBD0N/kVMXDlMSelIOYZQViVm" +
                "tTD4iPKIZ0vNXuk3dB+TdsPgre81mMo24GjPrV8OIOMraL6SL3th4ccUzJfINpq9xZCbRSNBBF7MBmez" +
                "LlZSWPVl2NgANHTMg7B/RVGy6U0oPO3ND6tEaD001F2LxY/pDB8XhU9pS/YgZHkozkjh8qQR0O5t6ToP" +
                "NrN9V2KtEQX8WfGSPILNnm3B6Ht8cfHojEd6TdK3N9i8cSuOX9S3ZeNqqolAP6xBc/zEgiu3BeFFXELx" +
                "4hY43Q+IpCxOZbDXl89ffnf56HNZ2XqNsgx93jqsjrxkEcA0dR8qGN67Ys1lcCddLexHstRXry5fXDx6" +
                "EIR1HHb/iDTQGITnRhhC9p3yKieU05ctVN9Ha6cqO2/ZrzmVMhDvKkQZYFhlShS6hfUlVcfQNAlFD3mS" +
                "L9eaWWY1DY9ouYa2Tr9/NNn5YaEzuveb/8lefvnN5ZNrrLf87Z3lH0bQk/dnS0iuks89J/0ogg6kHEY5" +
                "0P/xlr3yJF3bugWH3YPfyfFfoBeKsg9skRsb4rrpIOf0hkHE4EWjtLUAeVZnxSxoBQATYRazdEKilCn8" +
                "8s3Vyxf3sbBDYjI/Pn7+bcYgJtnjQNAgiQNHJLk9lOaKmxh3EktAVc8kuyRbo6z37D5xFgUFnLsBM+fG" +
                "nmef/OMYEX18fvwETaKLL4/H2XHjXAtvlm27Pr9/H1wYUwHS2+P/+kQW2ZCxVTsOCdUiNnkXxSrCTUrw" +
                "gJZn2R5DJ6ysA464sVbKcecVsO6srMBPmvR1a490cyq3pPItSVdefMlEQlBwXSgIZGiOphCZSWGaBNf8" +
                "OaWPYI6yYHrOCNJ5FrDALxER8HKIiPPf/+cfv5AmqKg5RQsNd6d9rKNd/fXbDPbPW8yHhP3qD371c/VU" +
                "mwh4Gi473iz8wz/IK8xBnWe//+LhA36GDg02KdFa1jZgKmxcUwzfo3GDC9JRNKkmn1eu6CpsQOnZ1q2P" +
                "A40juX+sAPBdFkYsbKCKAL9Gyhtn+RZMdLL6coy+SQhL/abGhpwPkJmGrsAwmqm9AMBQIaD+J95k+/ts" +
                "DP+bjKiI/I/Zly9/ePS5/L569fTy9eWjB/L45Mdvn724uHz96KG+ePni8tEXWvCrcou0EM5JWuH7kTYq" +
                "SlDHXrO+sWmMuccW2gcLBHD6aYek2TlHE6mYDlOUWu6IbRFd71R8Hcc+x6z8RkKj+BUWTlNlJ+SHcfYj" +
                "B4h/SudspAq7svWiDRHMnlTCMB4VPceajknE7fSHR2fJ048B1/j0E6A6nRLjX2ZFwUXcdhSk8LcO9d1j" +
                "ETIkn6U60RRlh1MQZ4kpaNLb1+nrxxfP/nYF80nH1E0mmLjBfOCBscKkQzENKt9QW5LC+zLUT5kBg4TL" +
                "0Lhuowd3+vTy2ddPr7MThC0Pp3FNnBFOMB7XtOx5aMoL2QnywimPh1JPx+HVyTj8kIxz1yhYz6G44+0T" +
                "v2b/mE9czcEF/QT9o28w5ElMJZQNudJcftuW60hDhFPsjx4rV1uNJXHyqSB1NOBEwV8gqcHigbgSTt1p" +
                "HBGDDT+OiNv1F8h7bXYyW2isDgNqtFtoMPB3zlwjtpOI6SQbcb1MyOglcd6k3aEWWNYhHNoLcqWZdyNV" +
                "z73lfiCu+/FVELqiqniSqaL3iQICvHJmvqY09QLsiT8lElYKUanyjspmQ20PrBFzZ2D8+DdvRzjGtQCg" +
                "RIXAGonwkFCg9lAP7cZq6SHNZg/OKX/LnQ6EKl3GHpTpso59nBQf0XrzkOdp300prHiQ2ZIPvzedzOlW" +
                "O5ZgQAwWhIiCeZd9ipHET7P8F/i/InuUnY2ozPz8ERC4nb85e4vByfD4OT7m4fEBPhbh8eHbkL9488Vb" +
                "evexEPCBQOAgxbY3wzboooTGJwf+RfNWCUMZ5+ToAUuUmEyWKuzAiG/GSV05PPRKyt/iLrl+a659eBui" +
                "8MlYrMrsOzyPyIfNyA4NwZN+WXrIn2LmuUFPpp8IJRkxWOUElj7aU67hd+s1YDnJy96ydis5ik5DE6D7" +
                "pxgpmlJR7WGiuOG0x3sqsVXMpqcSkDeTsyLkLCLG0zMpGsUJx3Hk0A5l3vVoRcgNUCk+uu7i5Gh2hrYh" +
                "TjTww+CkyivZjl5L3aNh45dRCffaJ8p52OW70otj3OtxG173Oxxg8waI4SgOP+xR7yEZNuPwBUXJNc1C" +
                "7ljAfJQ3HElhl6Gr1WDUk1lob78Jo3wGX5HM69xOZ8AHm3GcwafJNzODNbyNSQttFN8M2u77QANI+FPS" +
                "HvFgS8wFxS3JTgpbu5YMAky+gyeqFaQcA+Fq05SksycVWHpkO/xiG0c+nwc3y/tYfHo6TL4cYN93Kf1O" +
                "3sWdKwaWQNiUuFY6uHBHUoYLpSnrOEQpmVYY57sjrcrRo/kcs4wnQpQEiCokTqMYN83CtqIuXNJuY0lg" +
                "pycx7jpiwDCmBGPKY8Y5yOGQO+efHY00gfEdN42tpnyu8H8jrR1EyvQRE6NARrEKG/vwQtEjCWV/Zw5r" +
                "13zWzXEaHj6hwGKH3tLpr095JcdUUc+n5WoD7xdDhy2G/n99luxZTEnxuXQFOJY0WUhpBIMD0zFFyNqH" +
                "eFc4p7SAjR+j5bFnoYMM3HuWJqN/eFlpwu4AVLNXwf4WMdUvQfp1okryP72eEspIpFjcIqKzWMw0xNx+" +
                "p/2fIxm1spBiKJ+RZ5bZpkFZojouyZsmhztndPLUTt9Nsec0tN7TZPvhJr/sNPk3lXL7rLuQ/Q8rjscE" +
                "qWi0JPJNTECuOy5Kn3Nak5XS6U7NCt+oEg61UAeqjeyF90yAvOVQ3IazbeWaUrKcfXJ4TiyQOZdvE+Tn" +
                "Mj0SOnGGOBzCAv8Z2naN+thMzCHX5WZcks0SOnbHeUz4zHzLCZPrAVUQiBcvrwE8V6StYA54rDA50QoL" +
                "bpnEQwllmPxRKKBW/OEZ+Ybhlm0AmxgOerIolu4iPm5Lu0nuHRDUAHHv2k8kvU+oygvwYGbVVqptT/V0" +
                "OjGA77CsuGtIjE4SQdCLztJukpaLVzcoraCbU/ItColVy6YLQ+lL+xTmn9RN1bPQtxoCH7ak82b5Eg81" +
                "69HUuFOspxRZej61f5NCEGxjLqfmbN0+zXIl1wgF88uLDPlKKx4Y70qnrduYRgoxdHfDtJkFzJDeUiqz" +
                "coincUBJJtaMrPDeBspxDKr36YQlt/mCG4zl8K7hYCfINzJH/JDU5T4J9Hey9RYQVRaRZflct2ywqTZY" +
                "tAAtHxKVu8aGOwBwzlMfLxoJOxtOKPeZk2k50kvIwvJWgVUlg8aTqQP9BhCifSDYJ3nB5WMEcHikIjQM" +
                "GWk+7Iq14m1vs1CUAarjWdiAEoK21pqfznd0yp3O2zWd5dKfpK5nlwspQ8ypY1BPt2KvlwAW0Xk37cmZ" +
                "lUh7l7ckUVy3WEbiQrtjhwvH++Sb8ggW5WR+ZZh7ZjY3XeSzPTYGDbO3zktED2Nd71GhSwoiqWJjElkC" +
                "6gbICo86cu2QIdodR6rJDZ/U3Zm8IhFLgsDCXhoqlMH6RhriTCxQTvbQgGx9JMfz5HAwxr7pkwhplaQ1" +
                "lhZUurhkfEKAyu5AIXrger5XIgT6Zo4iv35Yca8aRO27OGtmdkP1PIhw/DLmeQYm1aLYM9HJGyczYrMZ" +
                "0I+FsWtyi60/VZCY6Klsi2Urqgxl6DvgvyrvP+AhUvAwM6zOYsl6OkmlRzgNSWum+iZZr6jJZHd2xILc" +
                "jhQ0jBGgItjdmmvabQOvqe71bEwz5BPhZ6HYuN2hgkR3F8N7JqDhlBqqMEO+146kJz14RrDSassmUtqH" +
                "JECFnlN/mQLw3h5jQNXGDjEplaH5KjUnrq6pWBle8/0Qfb2TGgyi9pgAE+rD1aBfFm4dIRUaUKemyQBJ" +
                "VJamt4L0EUYA+hhL55zKYBeKugfso6aMRkuimfRD9ih7MM5+hD+fj7OfMA9ypOn0yxdXL19Pf3o0fPMj" +
                "Vg323vyAlXz8RiQpbVmYwL+VT3CnliEU4LNKeL3PY3jwh0lTLzkcpHQIACqqQ/g0e+96Uhqkk2dAWr07" +
                "pFyIxyenI2OSZXib1qED3oOL6qLfj1cIpJNXVumXumYn6nUDPSmsK/oS7kqihnJmX6pbicTndMVBvOKH" +
                "LBrJDss1UI1tu6am01OchqJbNfgmKREnVJDjWRAGV0suhpJOyPGZ3B6AfadYY/Ob5kKVCbrw/8COArle" +
                "DO7AIUNPKq6yEzqUwhLAg8tKjhP4W97alYgrKguuyaxKy0QR6K0BqY/rpZpRtRSSamdZEg455SH7iwoC" +
                "mMPTt+9ZUlyP6Mw7lsTJJtUy8Z4vvYnrT2H8RP5TxTE4TWdjjUol9zjF4ibCPl/+BdKg2NbgHuRUa1jP" +
                "ywWd4mD7P1nw4AqwZ6w1SKHM03ZkBsimppjp+OywIjde1+PbgAK9FQ8PJAwTbJEqJ8nKqbt4gHjNnZd7" +
                "ZjO5TVBnMCa/hgiECt8aslgMV3UjkdfW0gFggBuQODljpb9/caDqdTF9PCNebkuzD6+KiL6e9WZup4GB" +
                "primhLyEC8GCd5zapVQjeY8FR0BCVy14D5cdStlicBkJEs6Hr4Sj7Yuc21hK3Qb7jFjV1ohzvlmOjP6u" +
                "zlkOodEsjEHnCZLg4i7ZcgtiASWkTKmMv+25Y254txz4hqYkx1eNrX1g918sJ4MUoNm3B5X3JKNhQXoL" +
                "YF+s7ySMEcNgkic3/PQut6T3w8st0wsG07vm+lfYUP5JRmMw1CrGdRP/Vk4BI0MW3brSu3HaeXayz0eM" +
                "OS7KjQ393XAoWcwR0L50xnHK1wHH09U0DVq/ioR4eolvEDgayVV94QjXc75ZQC8OjLf8aAfidABJ8YU1" +
                "LrJewEjfwuMrfoL5UIRbPg664G1zVjrgHYRWm9OHwf1FUgU93r3IaJzZW3EPHBgkK7NGMZSub02uL9UZ" +
                "kx/sKvLD4lEtxvZK2JOtoN5tByuQjXwm4wl2xpoAriNnWDEh3ht2YE987xq+HrYqDndD0kEuHHo/VapL" +
                "GA9dp7dwUABgx1xmy3gSrrW+pxyx0zJflpUSPDYcppPi3VB6wzSCgVZ/NtkSDPZHn8jZgU15U04a5yeu" +
                "Wdxv55/8pZ3/+b75C1B2fgOA6IaIK2vprHTh8m4VIj1zCe+l9s/O1QgiIPrTze4lAdd40JEa8dvRdbyv" +
                "JFxBcIgT13ulgchFLf0BDDTbWBrF9gcLh1CmRm24TC050wlEf1sWWEGJ38vY/275BM77z53B0wV+u+IM" +
                "slxB2K/Z6g04XMYlfgyz4vKvvcncITOTLIStt9V8TBUIlXdRpKTGCiOFTZZw7UjE1KRnguwuV+34nzvb" +
                "lIntVpK6Z1yf1ODZ1xx60JABmuF8Qkoweefso2Ub8yC74ycmokb+TKhp5+XANOjyNJ7Izu2sRCUUxiLz" +
                "DxTjMoMedML9AYXBevOl5kdSVs+4lEyGXLlOkxIDi0/019njFxfpMb2E7gTEtEcOKA13vikV/Au4isgR" +
                "/YX+VXRxN0D35Tfio7HFWIRVhOcDTDxR6qN7ZBapN/2eCxTUKIiXXvbuPwilXmodHGgdZGv8hlXIrbgf" +
                "WIUYLYeoooiGiFZThWOJuB5OBWy07GTLBs/eA/LhImKRAdysCIf5XfP66y8f65eP/V86CQMyUtGDCb8W" +
                "4dcs/DIHdzfIhGMTsm+E7lyqQYGwvfdiXieGKsnU9I6d6CnEIfAk4tFIuggJ8MP3IAUpeCgfP9455fcM" +
                "TjHQhxdkZ6MHCcYZ34QBaodiItC+sXZ4U9RuOttRu725MrmBjO2kPUk/idorVDmcIQCxQnvfAnRVh0Ta" +
                "/xhZ5CtQNRxG0sEikK4ndNoOD+fcd3nerUH/nqIaIWeP3pg63yoqTiaz9v4ELYWysqfsHDAgHONJZTC+" +
                "Hs1Qp4lR6Z7+10rwlIbed0+hgdVpqDfB7Cjehy/danCHfRIw0G4IRJbhwdgBKftLSJ1yV756mG+1o/Nv" +
                "kyVf4WI4xrJpylYJx2PU44+o3YFfRqP/BumllXPVaQAA";
                
    }
}

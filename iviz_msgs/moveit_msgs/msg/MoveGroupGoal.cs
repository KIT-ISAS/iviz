/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupGoal")]
    public sealed class MoveGroupGoal : IDeserializable<MoveGroupGoal>, IGoal<MoveGroupActionGoal>
    {
        // Motion planning request to pass to planner
        [DataMember (Name = "request")] public MotionPlanRequest Request;
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions;
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupGoal()
        {
            Request = new MotionPlanRequest();
            PlanningOptions = new PlanningOptions();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupGoal(MotionPlanRequest Request, PlanningOptions PlanningOptions)
        {
            this.Request = Request;
            this.PlanningOptions = PlanningOptions;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MoveGroupGoal(ref Buffer b)
        {
            Request = new MotionPlanRequest(ref b);
            PlanningOptions = new PlanningOptions(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupGoal(ref b);
        }
        
        MoveGroupGoal IDeserializable<MoveGroupGoal>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupGoal(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a6de2db49c561a49babce1a8172e8906";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1dbY/bRpL+LsD/gYiBm5nLRHbs7GJvsl7A8Uw2XsQv6/HmzTAESqQ03JFIhaRGVhb3" +
                "3+95qrq6mxQnTu7Wyh32vMCOSHZXd1fXe1V3Rs+qtqjKl8u0fJX/uMmbNqn172g04tuyKBcv1mzTJGv3" +
                "PKn0xejO6NE/+d+d0bPLP58lq+omL9rJqlk09/ZmeGd0N3l9VTRJk9c3xSxPZlXZpgUm2F7lSZbPi7Jg" +
                "l2Re1Ulq60naSr6vBBxAyGpytCgz+VBt2vWmTYo2WdfVTZHlzQitXqZ1usrbvG4EHBtuq/q6WacYuL1K" +
                "W3llsJqrarPMpEWCCQHI6FtrHUHyECZr/5KDXbZp3QLBSdOmbZ5s1hn+NOPk6TyZ5TXXmPy9Ksq2sYGm" +
                "sniOU+cZAGA666qR1TdJhZlxxmkpc5xt6jov26Qq8+aUb5o8aqwAAQIgm7xN0DUfvaqmVXspc8GM6nYi" +
                "8+JMX3PRVdMU02WeLKp0qVMOSFpVWb4kzokavh0nF+nsKsmX+UpmMQcUNkzrOt3prqF7qsDqfIFZjWUY" +
                "eYHdRu8iv5FlFnNdESbe1qkgRPca6GyL2WaZ1g4EwGPuaVs08wJdn4Qeb94K5EkEhAt7XjnkA5VpucMi" +
                "8SVJlxV2RXY6ba+wtfq7rrLNLM8cTelSt8VymdwU1ZJAFMvxPI9bUm66Xi8LLBf4SdFcBsGmlFWb/H0D" +
                "Wt2mO313Ek9ZBu9PmCiKB+DE6rzZLIWQ8Pbv+ayt6l2yImBFxW702r+P4YfWQ6OUoFTsW8RFnu6xjk2T" +
                "C6GWlTYkf67zWUG8n5I0uccpptXvKwgD1QFANsKYnLb7NimyocEXdbVZ88ExA4BtrwoQl+DW4OJntc5r" +
                "LLhcGFzpOSEsD3ezmqIxIRcr7oiBYH8sSzhsBcGQZ+Pk8qqqQeRAY7XcODFi06/zNT9m4xHm9PABAU+8" +
                "wEzbNl+tAypX6btitVkl6araCDPI6EOYJbEsl9UWVBYxU3IMEmxybFIGEpkvq7T9/WfWMAxLoCJXZumS" +
                "y5+n3FxlFpXh6LLD1EHGQi3R1AS3yU2+rGYQEWTNUiTMbAYWJlZBIOMkeewmd5MuIWWF3TC14/unn77F" +
                "19e3AdwNgAv0YgxWU/44UULIK1I1OQeSb8etAoJA6+jWFjc5wLkFYmRQIjQIBSA7QrZTSBJoUXOuRZbU" +
                "abnAjI+LFTcuLdvljmKxaChgytlyAw2AjRVxDIEI7N8f3z8RyenGERrXT07zCH0LKrinn47vCyyId0X0" +
                "cTHOx6e3YQQAu19i5JyM/Taj0cQ6TRrd2onOqNMm7t5vdxjVPaD7vPIGszXpIlLeqeASaAs6kTJhvoH0" +
                "J6Phq4g0sD6xfQMWBMscp8m0endCijEZYHTTZR3Oa2yDFx50VWJftle54zjhEm5ENS2g2ahPIEbmiVNW" +
                "CjiFBs+XyzFZ61wUlxIFp83GdT6H9ixhIJg2xBSx0BoM3Yy+ylOoayhY/vECoYC8wMZrIxN06HYKAQON" +
                "46jcSE3nITpZxGXV5KNFXgFrEN2C+m9Ejj8k4IkC7cuef/5QoDk31AejrqbNdFDFIqkJ9kmZpTW0cN6m" +
                "MJhSwfpVsQDzf7KExSC2yWoNMpCv7W4NmyqihEWOKQt/UgVx3ZD4q01ZzESFUy3E/YWlu7bGrKrqrCjZ" +
                "XIiA0IViYXmSDJ6en4mOzmfQGpjQjgKmztOGGH16now2qjPQYXT39bb6hDJjQb1qg6upicnm79bYIM4z" +
                "bc4wxr/r4saAfWYaITmWdxM8NicJBsEUoJ3AHFQbL3ftFYSt8FBaFyktOACGfFgC6hE7HZ1EkDntM+jf" +
                "sjLwCjGM8UvAlh4u1/QJjNJMVFKzWQCB1LlqdDvJDrkAMQ8ZuCymdVrvRqIfZcjR3S+F0URTy46Q0ZsG" +
                "8hAbkAkRm76X3aAV8cEIcpAVvJCDUYDdwjoo3m7kI4lnXudYDAXjmHTyVHZWJNEqhyIiCfqeNJyKmhrO" +
                "GcSQLlWdn9JRySqoKBiNgLFKr2m4A81iVMK+hIqlQVc2S9WueI0ux/l4ARUkEk9aEU1C1MIGxSypiwW0" +
                "o/TEQCvfOU3c6qAC5w/U7pE562DYMwCpq9apK2rgXbWBKYs14EftuE9MKpuXUElbVadkPQeii9GXoglN" +
                "WUBPtGB8SF5TdO/8r53/9dNBlFtwjm7XaUUZUJhOYYV0RWrLbYRJoN5EcNzm2KnGrFv6p2CV62bE7a1q" +
                "Hf0v/KiumbQLrpl8aVRgrOBaXaU33nLKk/MXX6pP5G0t8fRi0M/YFu2iIaT7JKvmk95gj9sWfhmgzKrl" +
                "smi4zmpKHwJCIbVv2PMGeyqroLHucXAysv5PrPsL6Q3nzHpDpbhPEweZ4365TBfAbkYZTQoGPTtvlzbZ" +
                "DKQc7HcxwMBNFA5Q3eSn+XxP18kM1WWW/pGCWFUNjMPEG4qqYEyCiRNrS51WGW3lY5sPGtKpo05Z5hB0" +
                "A40xEDY8dbR1dgYVmp+dRa73VBheowEcEFwsk28jkjsZTauKbvKEi/uA+neQBD0DUBJ7LhAKvKqW0BjG" +
                "+jCpZ3VBAQA8qJ8ta3eWH6QLNKawT41dB4qUByglnfXiO+WMJqidfFzntAb5voYeKRpy3Aw2IQYW84xK" +
                "hcoSOi3mNLN9DEqaiQ1+chqaBuO83/ReI43vQcOSQ0OXfI7tar3CVlffuzQOwPMVez+HVc+FSWREv9Ay" +
                "LQtggQSXQf4rk0IpUiSI56qIUPvR87E6/qIlMW4whRWqC85sweqgeVAYFO2YvtteG7VuECPy2yjyQznJ" +
                "unII2UHRwxhTl9GXgU3RqPWcBvkj4R6EOjgC2Ui2kaZUF8MyGVGOcQiIZjz1nHRy7bF2Upi5s+PkW6o2" +
                "ajnVOk6KyirKCgDd/vQsBsJanYrGmsHHBrNy1WE7NUxFJ37nqJHY09Xo3kZrd9E5xZzhqSl+grjHkoFI" +
                "hRNxjQQgxRXBKIymGA34aUbIkViITZp2PM0D2puyg1COfR9DrSEIVaEfpybxaIwQvTKCj14pBg4jU/Z1" +
                "D2XLKzOGVJNi51UMkIDc/go6PJVl+QImltAeba2swtYyPAChUm1NaAMrm1m7YXhgnoQBlZbVJgP24WQy" +
                "PsxwoLEorf9mB0tEnCjqEDGLxUbydpP2WeRtEAGMd/hA4jXkksipZHYFW2GcfElueAcDdwkyScUdhcJw" +
                "8gJ6C8P+7dX5lyLWHlKNH8NjQ8hil25pVmsYGza6fiQRk9AidyGenSISf+oCULQvmbrzXcxKtjBo4Okb" +
                "xKEpj9LZNRfcmcP/S7LDSrItgwxXv1iSWfP/S5LsNkGmZii7N71oxGsjYbTy5LzXaAvVxAb82/v2raAJ" +
                "HxVfh3Id/byHnEfR7V6yTPN2m4M02m21F3poev7laGThmcgXHP11gw51SRlgHtuh1hmGHvSSId1rOv4q" +
                "oDpr4dOPYeJExfscQf9re7BtJEX5lZn4bYIh2V3StK6usZMgZnrnDV0kugkUyAhPS3BJYpBjv42uSXh2" +
                "7Q61QGWPob3DhugmhfWdQrkznEvZxDXSuvxlqxRg4VE9g8NErW/xSXXJedJ77RNA3q0TeYYoZPHOHBiN" +
                "pQJb9IAtNsXflooSVHbS2AjTb5hVvUoRrRRMMfIa8qu9WYzuqoUe+9xsJgPeRSgG8IShuGNcrIBErk59" +
                "6jEiPprMgDHAuI7ztO8OwTO/W90Sv4woE8YBmGztIkqBWhjauXwS2QiJdD+eRREYq4ii+G21gUhGlnrn" +
                "0y2f+InpNCTsj+Bqtoty41GHoEQE2ERjKzT5QpZdZyXuEuwwpnj7IV/shpiOuh8M3+kiJXS3Igy/FIB2" +
                "UTdItxxoh0GtocDZEgY4429ISwovOCguy2RjlPmM6hg5XI5WI6kjkWOXk3ADc/sA0wI6USo3OOsh6wtk" +
                "cHYTN4RtyjZH2NwbML3NQKxqnlyX1db7C679YdhygB0fO8NAwoeZYMcHe8zKF7YZTrqA6N1KHQ6PhYAE" +
                "FjbwGQZ/ihiV49eQi7atRiJB6YLhjWkK07tyCPIMpH8ndJwXWpWii9E1vCYEggmJfIuiOaEridd9+87Y" +
                "VrshpanCgcwylMOJg32GgMuKSVAbZsa4yQoTvAGyROjoPKXVS/tElzE06wdqm853kpaW0DzLm6suVL5B" +
                "25V+GITDbwHEF+QPKwxgfA2ZFWchxGUGydRFWKWZeS1qUGDxG2U27FiWyV5AwHKIk3huLDSShchItyyS" +
                "38LsHmf0/iLCUKz7hKLEEyUgHjUCjaJKZNPAMs7fwWbg9BHZU5UqMmc8mu5g1j0+P390n8O8ErnaGWle" +
                "V3Q0YYaXN0VdlVJaw6ARhATSzUh+1UjLKCtIOLgFPyuEKNmenehIry6evfjm4tGnsqb1mqKKPq1Rs/OC" +
                "nWyVSTuH4X1rtTyFdrJ1YhfCIl++vHh+/uiBk8NhzOHhZBTUy+RbR/luqyVhghIbOFpu38yxkeoXtFjm" +
                "81adFjrNEGgo5SCugFqTGEGgIlwJTGY6RcHNQ07whZaVaFoEMPFIY9QauqqTD2lSv1+sQDz+6n/Jiy/+" +
                "cvHkNTOSv76z+0f8PPn59IfITYkKzkXtOVkGScboBZ0a2AZNrxKgrRYaUff+pIZ2QSoMoHdMi+vcx2vj" +
                "Ec7kjfYPsVVJyAnFQGiVSTY1eQ8oBjCbxlNxalYCKn+5fPH8HguEXJTl+8fPvk4UAOKrnoohaT0PRJk6" +
                "ymrDSogkqWo3nYK6ObEdGJXd23VhJfHyq+oaVst1fpZ89I8jYvjo7OgJ7ZvzL45Ok6O6qlq8uWrb9dm9" +
                "e3BF0iWw3R7950e6RJbtcHoa4imdcNTdczYONyfCAu3Hoj1CJ5ZgghGu89ylqedLcCvqJ+DuOA01RLDM" +
                "cygSLfN4/oXShgDhqsj6bmQNjpC4XBmjC5VJEpyhM7dYCfMLmLPEI0DeEQV410fB2e/+4w+faQtqX020" +
                "ot3+jI/cSJd//RppDVgJzG34feoMfPnj8itrobBlqORou2ge/l7fMJl0lvzus4cP5BGtazaAEV1tXQto" +
                "fpSKZr3XNFK4EBvA8mL6FdVSmyW/S3K1rdZHRtAg7Q8Xw73NZLgTamSkxKRZk9hOk9kONraYbqC4PHFR" +
                "KHN3QBmWuQFlWfQJds7UDAEAo9inYhdmVAv6/in+h6AA6yn+kHzx4jsoM/19+fKri1cXUDD6+OT7r58+" +
                "P794BYHuXrx4fvHoM2N4E1Giazgn10ptNZMKSHzAv3AZ29A0RM5DC198g/w+px93iJqdaUBQii+ZaLSy" +
                "WLYlut6ZsDoKfY5UxUlW1DmHWLhMVd2I706T7zXG+0M8ZyJZPKe8XMBkdDPqiyH6T359QPo44HbyHeyS" +
                "8PS9xzWffqAuj6ak+Hezkvggt52SE39d3gsCVOcJoUZp7IpZ06zYcArO31EKsnko3Mmrx+dP/3ZJOyka" +
                "0zZZYHKDtfZHsaKkI6EIqb4wI1Ei9G6oHxLUSrGa0JdddOBOvrp4+uevXifHhO0eTsKaNLEbYTys6arj" +
                "ZxkvJMfkhRMdj6LOxtHVuXH0IRrntlFYjmG40+1zLsrwmNDaGhWwT+gfrP0+TzIbUNTiC2uZNhIvgYYE" +
                "p+xPr1OL9k5d7uNjh1Rj0h4yPUn1Fk+rNHDqXuOAGDQ8UCSMzoB6ofVefopWaT8UJhtGI0G/awqaCI8i" +
                "nojIa8WLT81Fodqo3eHWiMn4YF8nQhVn0REasdRYWPF7QrOH0EV0NL0GimZL75KSAs62ciEK1soFrInP" +
                "I1HrSpqlmlOKr32BDpbJPBgMn+bN2xEHee0ASNLBweIAUSjPepgrBivQlbLKbAbwLulY7XQwbNlChrBm" +
                "K4PR5+ellYtvHupU83cTCQ4eaMLiqw8niDWBCq5Tpz8EBXzkIH2XfMyw4MfJ7Cf8X5Y8SsTNTpOzR6D0" +
                "fP7m/ltGGv3jp3yc+ccHfMz848O3Phfx5rO38u7D4eA90b07vXjXYNqs18coTs+d/GZT9wJHamKi4ysq" +
                "YEK5i6vu90z55jQ6rICHzkGFt9yrqttayxre+oh6NJbqt/wd63UZonDGqY+YdA87+Lwoa2OQ1Wh6CU6R" +
                "F71ljrH20UAlRrNfisESsfCys6z9Io1sY5EJGAQThodYq1cfLETrDw39XJ2/Sd34qAuZNDpzZDiPTzZZ" +
                "CMcf53KHviSnbkd1fMBfDnlYxlgLX5TkwyQ9Q3QOO710W9FpZ/vTbfoiKORO60hRdzt8g+CuOsad9jf+" +
                "daf5QfashxPum38a0PM+nzXV8IXEvy1tIt6ZoTzIGlOE8tn1tdN8tL3f+CGQy0E4DonqWT6Zgvy3p2H4" +
                "j6NvCCbd5G+9MdGvY+q3HHgv0CXQ6ZIY4XxUyOmEzUiOES+qWEOHyCo2Fg6plYNq9EMrR2MKTp4gu6OW" +
                "w095XYnrhwwg2CFUkoazWzqJg+z4PoHfzqzuXFPHDPA7Ela7X6mh7q5WOkvSsI9QMasY1xvKh2rMaD5n" +
                "kpCHC3zqUWocToLMTmuEBZ1uqKJ2W1YaRmFpdLjl1IqCQDCf1KpD2gTcEaNbZ56MLC/xjbYMjSZ6/PR/" +
                "HYEdRqZ00RKFgFLDKfb04blixyWCm1vzUfsGs20MC4oFLuvjUoZ2q/Lkl6av3Ca7zFlcM9vzehkqhBnw" +
                "i/NdT0OCSdwGD+3UZbx8tsIbFEyzZD7J7oNc/ozbAjsOk6YZWGI3l3b7otzQ711QlHg7CK0M6tBfJZC6" +
                "JUPvF0ouq9Pp5SIXkbwKmyO0FQqPemi7xT//nwpAqwKUYMkn4nYleV1TcJgOizKf4QjwVI4m55N3E3ac" +
                "+Mb7LXbvbfFTv8W/pjQbMtqsGChacjhTKsWdeEWTKJh2Wh+cFc1MM5SqeE72Kkv8CUJhAGkvJYydEF7q" +
                "AUuVC2wxTZ4hCMfMqqaUQI8Lpo1crFnKrAn4mZuciJgwP45GUPCJ0XQjNBmYwuevqqkWTqskDt05DUJ/" +
                "jn0/iyJGhh8B8PzFawDXSrEVZsCDqNG5Z6xWSKQJNY5+5r7G2VDH+xJqBYuAsUGNzAI7/hOqa4mLmwJZ" +
                "4xBNVqyArvfNIpHTx1J7BRwgnbJz9bAndl+B0H6DJFOy3tQiNMee8zuhV9lG0WPh5g6jETorRCM4PBiq" +
                "apUokFiqxwA/N1/TzsmLjrGil87QPAiGOvMSotSdXvY7pLrIsGRnmLt3aXgpdqqHNjT3NqBALt1JWW9V" +
                "NSI1vrQ6BUW3UWZbbdPa1U7YnvopK9GnfRKLKEtrvdbYHFCP1MdosGfFizskcdGrqpdTj9rmM20gxwL0" +
                "qE4p8kzsDJ1RNLC7TIR+S7LeAUeFRAVkInrc321rukTZM6k3eSh0jZSBXQjBCU9k0M5++oPrXU5U2g0k" +
                "YolU3SBYSm7AcE60p78AICh/h3YRDVrTNR444+Db+Yyynjxl8Xbb2STKLOA4HEw1ZAgwUV0qDxH4RG85" +
                "Aldvci3Qiapv9nmOWQNN/UIJSdpPysqbDfB4O7m5IyRGbhfM5DO1vrgK9ERrYo/jlN56JGY8weqZpFlh" +
                "CYwD5rOUh1StYmzfeJBRBsuvnIxRfNu1OYAZ8RLbimRykK5BSjx3qCU+qVCr4F03dUY9MDR1hz7W7sBY" +
                "RuZUAitUrXKDhTMpNWUj46lpER2Vcyd0GbiWT04SO3kJkQuE2Mqi0WX1JqA9Ydih5/mgADCiVhYSl7xf" +
                "9W5Kwoy2MGfl7VSqb4hsfuHJPaEYhWgVqfedxkVpic5H7WCgnmWpa3Fo8+bEIDJRg9Q5i0xM2bmRh8G/" +
                "LO490BFi6JgXS6hUhAJ2JCz8qURZMeuQ3GKdGow2Zk8OuKuvvBZJHUwnwFF8JOXkeY3XrDy9fyrz0wPZ" +
                "932Vb7u3/5FqznqXjaDdRNqNPJ9bL9GCDTwcLJKnwKteD+H4JT2g7hIV3N0BPW/KYY+GjLholLoakapE" +
                "baxCZq66p1xiW8ApNiW7iOi4FnpX/soZ0ZEea87o6KFH6sbsTpgOqqR7jKt4urGwrXwVdY9hzEKxEIc3" +
                "fr5DguIBUvn48+kpUtNMV7gc+MXzyxevkHLvvQgZeffiO1//4ASm7JMf+1/Ivr9VlWhCly9MlNtdLv2T" +
                "NkqPdkFHz9UUAKKODuKiDF7nZT6KHvgCXXWuCat8sDw6lBhyIL2r0g4elu7dPXgnFN3M553JG590K1CT" +
                "Y3OgT/w1hpfywV+OJe30uLwWnQqNz+VigXCvk1gtLourRyJRW76pmccoXYZILrHQK8OcDJECmkZln/eb" +
                "3B1grhNZPdFD++w6YUnMr5iH1BDYiv8N/RxU1ql3rj0SM85VRyXHcvJDeb+B50knCK5Tk+crJ6KkSLcU" +
                "wyku3iTMmxQynkuVSk5nDkR1x7oYjjfR8eLleGGrweObn1lMWIoqxlsWo7kfUyfhGje7au1zGz2S9FL8" +
                "C/8H0n3/wq5QgSRYR3cRANkOIZliJoWA5bxY8LSEGvTRUjsXvGFU7projXncStS828gYJ6JDU1q+sYac" +
                "0fqzxdv1hiz/72e6AhWKUW81OJpjc50wit6IxG0uo/ERk2RZE2lC6tJqOaWsldUk6TLP5a4AgPXYG99X" +
                "rT68Mihzt5IufomTmyIdQqghoaNKm3SeTzyzIDHGO0n9+mRysMkrza9Ktk9cQGRAuRDf0arN/W2VrqDQ" +
                "/D4BxLnoJX+ybYFFwcpMn3q7S9gyL4lsvSuQVvymFDJOxQ5WNpAafh8F3KdS/S4Eb6STOLLST3tXBvav" +
                "CoR7l3JlpTehhmAO3hPoRsiguneHleYigqmX7DrHrtTuZmuJWRjY0W058YWk8rp7IWl8R2R8dWD3Uhim" +
                "gtw4CkMaWeA1ck7deVqyXrZZL+2qmXaeHA95eSHXJDmq244lOzsDOlUOCE70iqropPLorizbOD8cCdKz" +
                "+CN346I/DvVMj+jb5Y/huhzXnvwMeBIUWHN55aIZfY2nl/qAmUjk2X3rtOetgbm25h2SubWV9537f1wN" +
                "8un+RUCotFB3HHsD02KVok6w7ixrLS6rVPnSfa2W4kKFU0+K4JVjQTVnOveerMYjPfkApFQ1E/BavK2g" +
                "LP3cGbJrFXxb1Xpt7zI70O1Ch7mu5+dpcOCYcnyBhfjse1avGrhjf7PaXaP/vZbI8KM4PTTs53TCzUqi" +
                "AN29b2j1xzS5gt396CNXq78trotxXTXjql7ca+cf/amd//Fe+idQ8+wagORyhUtUHdLHzKoZHCyLzOhV" +
                "jlKm442ZvbSQEwbd6SZKMz7cZqWtbKRveYeuXfVhvw50QHlQApi5b9U1QALCV74ASU0LkQi+HEyauHIw" +
                "15tUf1NkLFjk1yJ0vlUgwfFGdoxF/c1updlbd5VktyoqHq2/hAt+8zPS+qqBXGqfjUXyYdvz5RxDco6N" +
                "Oy7QM0EUGWqI+GuHAobGkWWxv06zxX/c5EBIMMYKUeWK4eMSTnkpAQPz9WlL6/kjh8FbZx6MVJ+X2B89" +
                "svgsRIdxHF3qUjAJuXFMprF3m64QhoScxJyD6rtK0EEOgj+QkFVnttLc1bArFl1yQW+X1ik5o0mPvJfJ" +
                "4+fn0fG3QGgOwCQmAUrAvU9u538TNhIaJBf1Lm8LWwFNN7t2PpYagZktwx4PMvNIe2O+YvaYG3zbDQOm" +
                "+sPtkPENAb6QytkAB1uFWBW/eA3uEuOfX4PaJoepXQhGh0nfcOCPq9FgPQSTMiNj9Gi7f6LcXxntWF/a" +
                "uJtuBfyrP3/x2H348Hfr+hHvKFLplPhfC/9r6n+lv4EXISYbsb5ncPYvnpDw1cAdkq8ji1RkaXwPTXAD" +
                "Anwe9Ru5Ho4A9OFbiD+J+LmPH/Dg78+MrqHLh+diUdMvhDGmd0ZA3UhYAx3qPB+uiolTxhiFpziGvBx3" +
                "7aDaRQMZORdi916rnn1wAFn0PLSC3wZv/318iX8g1WcMgsMWcF2P5WAbT8Dcq2azzRrK94RaRBw6eYP4" +
                "OEKxio3j8bS9N6aRgKvG3QWeCkgOUS/hAcWWp1ZA0dXS7l0p8or5Uo1Z0OdfYVxX9sH8Jf+bBa5bCW/X" +
                "F45rmlX/UwZMxugyEKYvIGh/8tlN7aqX9OodcHLUbCzBKDmkSyNiWyOL5to2DGb8geqdPHNn9F/QK4j+" +
                "aGYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

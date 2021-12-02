/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PickupActionGoal : IDeserializable<PickupActionGoal>, IActionGoal<PickupGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public PickupGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public PickupActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new PickupGoal();
        }
        
        /// Explicit constructor.
        public PickupActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, PickupGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal PickupActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new PickupGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PickupActionGoal(ref b);
        
        PickupActionGoal IDeserializable<PickupActionGoal>.RosDeserialize(ref Buffer b) => new PickupActionGoal(ref b);
    
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9e12196da542c9a26bbc43e9655a1906";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1dbXMbx5H+jl+xJVUdyZiCFMlJ5egoVbJI20pZLxEVv8SlQi2wA2BDAAvvLkhCV/ff" +
                "73m6p2dmF6BkX47MXeUcl4ndnenp6enp95l84/LC1dlc/gzySVtWq0U5Hi2bWfPw6ypfvDjNZvgzKovB" +
                "m3JysVnzpbwaPP0f/mfw8vzrk6xpCx39G8Xpfnbe5qsir4ts6dq8yNs8m1ZAuZzNXf1g4S7dAp3y5doV" +
                "mXxtt2vXDNHx3bxsMvw7cytX54vFNts0aNRW2aRaLjercpK3LmvLpev0R89yleXZOq/bcrJZ5DXaV3VR" +
                "rth8WudLR+j4t3E/b9xq4rIXpydos2rcZNOWQGgLCJPa5U25muFjNtiUq/bJY3YY3H93VT3Ao5uB8GHw" +
                "rJ3nLZF11+vaNcQzb04wxm90ckPABnEcRima7FDejfDYHGUYBCi4dTWZZ4fA/M22nVcrAHTZZV6X+Xjh" +
                "CHgCCgDqATsdHCWQifZJtspXlYFXiHGMXwJ2FeByTg/mWLMFZ99sZiAgGq7r6rIs0HS8FSCTRelWbQZu" +
                "q/N6O2AvHXJw/yvSGI3QS1YEf/OmqSYlFqDIrsp2PmjamtBlNcict8SNe3eEsJZHNmvm1WZR4KGqibLy" +
                "U4a1vJqXWBCZBLdLdpU3WU2GaTAJMtALWW9hSZAkX/nBsMj1JVjjau5WWdlmmKhryLTgC7dctxkIjt6E" +
                "2SjXXDkMHUBnY4f9ARSyiavbHCtHjFL6evzLwtYE5AV6WJYq0jmbOleM88kFMCvQA0y5WbTYg02Tz5ws" +
                "Qtas3aSclhOdoMegGXro3CDaAEgtN00LzDLsOrQa2vpx5W5p6ZbVpStbXbcoujCcjd3m9cy1oxU4KL6c" +
                "1dVm3XvnVsXITadugjXG26/rvFn/9D5bV01TYheMZnzRJJCbzXpd1e2o2dTTfOIM3GAwrqoF16+6Qqdy" +
                "vXb1yNpOqsWibLD+EQ7GyNs2n8xdMarGf8f4o7baTOYjbKwLGc9DXJarcll+cNaqKLHQ2ML4/hxSqa3B" +
                "BS12YN7OMU54kWC8XuQrCEnZSZ3xiSuG13EVPPtNF1Xe/v7z8F36o8tIeG0weOOfX6/J0hjavlf64i4W" +
                "XdbJ1IDxLabPPUGOB99PsAiy56opXsg6+i0lm5r8iu0JGMLuHZUApnhgTMGNo52xi5VIx9QAiw2Uxiyb" +
                "V1doASj5GhsO64ltfYwO5Vp+uHYyzLpYFhX25KpqDV3A3Ypaws5e5oJxPq42kAXZPR14XWFF72WHwLFq" +
                "Smnx+pXII8XniBLne0wN+rL2GrGlXEmH9bJsnl9CfCygv4otVeW4XAkRdodPxm6URrttiMPMVVDekPL3" +
                "SWm+6ZMPw6w31MZsTagjzANSZNXBcEjZ9Qy6CrKMRoBKV1nnRKB48QYVW68glwCo3UAg+pGpmXxnR5n6" +
                "QPsDsxX0tlEPDFL71cduIQWreqt89WdO9114SRijgDJH+lUY2OjJwPgK0kAo/Aok9iIQWGEf0TsIGAPi" +
                "XyELaV9NqczxdA9sD1UtYufePljHwqtQXxuxshIInnHZM/tNufrNTlcOvF3DEtOOGD9svRQMfgPMEtPL" +
                "RLhBndb4q4C9wgukPUwHOJI2DRrN8tVwYJzo9QLAn3vbL3KdkQ/KrAQr4xu27Tgfl4uy3RKbZjOZgCF7" +
                "LHic4bEB+KwCKrRcl7AA/bpTBtybVVVxjzq9hH1qIlTH/RmkA3QbOkiKoqwxDa4hNkmbXzibLWh2QYYP" +
                "Egd6STQKmGLVLHQHRtY0gDZADSI4CLpd+PkUXBuk4RxWy9jBEuEGXTgS41DHE6vMq6ejvYNjtfzofrT+" +
                "4MvKRkZXyg01emq38KZzlF9fKKET8UKBpEJy5bgaMCK7PA1CN24xPc7GkEI3Aj72TJ4KrqtysSAHBsBq" +
                "0ihU8yEMZUhcsS4UH0xtU0NcwpASbWHbRc2nY/DHzkSEkJd5uRCTmlPgjgHf1RxreCNpoVNhWySk5TjL" +
                "/LpcbpaqOLBIAAdTHugCFE3ShZ8GiXD4x6eP+Ak7ikMfKUvCTQGQkQcwEgCETkYTZYnNB0tFZlaNsRcn" +
                "CxcMUdUdjVvmKyjKXW0gcAptPQFEbnOaFq54uN40/EMNXqjcp7be1CoADOlPmia3Y1d8QvwOun50wPHv" +
                "bCbmXzPodXnDR7Ei1RT7p+AtSGBNzihqYltFKljvcEqh2jG9oKN+Os7g5cJRaPEVDznk4YIWBT++f09l" +
                "2m2t+ux92KDJWGBAcIG7pu+sjtEz7L7oOVzmC7gVogk9VzRU/3DIKaZUXwmdxSzgzsp6sxxi6sFkVcNd" +
                "EUvexekkLzvTSt7rbAbFRj+JgzWa1tVyhO2AD7e0mDfqLjGK+Kw2WO2m8AjpwvfjFrr5LObQ41oBoPrv" +
                "rvAXxGvHiAcMDCUmNruZflN4vOAEiLlj0T94XfjvatdwOhX8Y993mGES5ARrMPjLhnJ0JXBju7uaoG4u" +
                "MayC39H27DIa7ES5M91gHFyHX9vw68PdoB9JZ3MIC0UbNaVnF3k+/RzpTg0Au/3jM7JfV3fjGPYVKmZY" +
                "uCncHPqFbaJoo0EhXY5V+2OCqvNFzFENw3Rq4JLflw7uGpvLq/JFOW07ZhrWnIveirJHAzPMzCrRD1MP" +
                "mBrPa/ZoqXmTIsGzZ9Z+J3LviVm2oWcA5Rq8KjozDTEDMwJ8oySY4C0MzN7eqSK32A5DC4gmEfKUdhYn" +
                "Em1zxqzULIM+uYQ1BuOR8SyIr2B3lKtkuNthhI9Rah+jZ77JPyRaDcaljyHd4dT2zknx6G5bCE+LSYon" +
                "DOcFYgnaOfRkzMRYSSziWhYXoY42RCzo9dCLQHvIcAYU12sA6+4qvEaXQzecDY/VMpdWsk+IhYQnYD7W" +
                "5QxBSukZ7UjCtEU5ztrpYzXXBWcdTJmtrlQ4HQ2zF9NsW20Q4sQc8KP2eQIxPAwv2XZtVckWN37dlech" +
                "VgJZ3mJxPynZbl+cJSG+j0S7zGT3PpcF5GTT+t5meyWvwt5uQMtG7DGKplwjvkrBZNvDVmMAxsdiNOwp" +
                "xljEMVjFnUDkG68RO+1MTXabvo56p9M60UfdDt8hrKq+e6f9ZXjdbX77C9ajCKgeHvaYB2rbKqkpgyQ3" +
                "ZaF9+FSIznt6R2/DZLV89n2pYyB3ob2a7KcwxAN8pYkLkTYawwa+Oo7Df5Z8g/d26d4Hvg4Glr3otdzz" +
                "XqBr/O7KIXknahHh8RiLUjPJUyU7LBzkCaQKvHasKtzKEhZg7fVOpUGWDvtmzxewECWc+MHVlUiwJoN3" +
                "2oSu7VE0NQSJu0g+7PD2jdtUlTMMkY556JcjThVCp6tfLK5SjSsfE+tTUxQX00jGLGwVeIWdYZc0DmT3" +
                "TChQRLMdRa9NMibeYK2SdnD86Z8FoYIO+00SS7oIiJEOaQhcVouNpf32YZ4NviQ7A/532jI2QhRk5u2b" +
                "/13cdQfs1SUKKHAqliy1vJIJC/rkVEnDBWb8soKYNu0GPbuukT4iEXwcRzQ4rNs2WRWEiRTuIXzunHk8" +
                "qNZBM8/XTvE4J9A3BoniO0BN4tG0sGNu1KVDk4PghDPguidMmwKUKK1AfQFhWBS6PyRFatCOubnmgOqj" +
                "TDGeQPuhkD9BQpJX/DwP8xmWGxGNZs8UXwIk8FDIH5mUH/qTE2I7m8vtMMqN6xP5ZFxdIyy5pjUHL3uL" +
                "/QyJQn8b3ywYQyg6H2UCIZ+E+jDdso5UBDDSHIpY7IG8FgX06Bj/g9BiFcQfsi9f//D0t/73+Ztvzt6e" +
                "PX3sH5//+O2LV6dnb58+sRevX509/dxIzcoO834EJ9+K7wfWqIANDoOEmZRO0xi+iS2sD/cy0U87JM1O" +
                "MscAmUgD2pDmK2rcuHDXFpk6iH0OMPk6l4D+V158YuKC6rE8/XCc/Qg+A3n+luJMIotcdasZHA6P0aSq" +
                "YYOvUZ1BjxCJdcna+48g+jDSdvTD00fJ04+B1nz6G0idoqT091iJCc1ll1jLiha/L1BQPGGzz7yQgK7J" +
                "i3JDFLypoRxkeCjc0dtnpy/+eg580jFtkQUmF1grdpQqyjrUKlJdokYdOWlRycTZ5m8ZgtwQyNEF6cAd" +
                "fXP24utv3mWHhO0fjuKcNAeZUDzOaS5CO9Dc74XskHvhSMejsWbj6Oz8OPqQjHPTKHRNjHa6fHnIce0Z" +
                "E1aD+pb2ibnCrtBM9iTN8bKWDLXmLFokmQMPCU0l14hFIr9vkLQSymafeaLaJu0RM7BUb/JgrmSn7jSO" +
                "hGHDWxdxlM8m2BILimKWDGguEHRevppBc3+R7GAfZhZrQ1RJyMdibyNnjnQGQt3vBxzjnQcAWRJgmcmt" +
                "WdDQY1ehCjZ7PEsJQmunOyKVTWMPyWxaB01ESmvYfnqieLrrEQh3m9imRs5ep+/XmNHdeOWnTWkLkaW9" +
                "LJUXrexoVYhRFKOevQW+KQz9D5rtIR/HrftAFixzdS0VFd7taiKOsWhnDAMQyZbR9YgdR6HxbovtJ1t8" +
                "6Lf4V7TB94UY/OIm81VlOt1I4GTJV3ThYxxCQ1sIgE6yw+huHe1Uo4YSVOF+ac/wWNPRJnkAvFXJfzVn" +
                "VoX6QBL3mgJnhC0wNmN2qMgA4JceOTGMI34cjaBQroimG62iCwwcipaYFa43CHWL/xC7Ew1Cf4VFP0kS" +
                "I0YfAfDq9TsA17S11LUhmQ3esby2hYdZldBesTAhYh6Sy0Y61qXUCha2i0FNPFnVmN54wx5B4QlocVm6" +
                "q8SwUapohq3nyIt3cchBtUIE9pPWUB1Z7ZgwPupF5tl6U4upPwzbvmMFyDKKsvBVWABgPMLIGsmI7R0D" +
                "K+pIK5DUF0kBfmEJUl0hsqy3s/otMRYWCOUzkKPoc+WkyFRXSD0oo5IEuzUyEVkyirDjNAXykXKbEAjQ" +
                "epuvkD1NyG2c2VZXqPxuOmsaUFamz/sslnCW2JCSYbn2lTwbKf9AecJWbeihmKUeXR+M1jafa4NjOkFa" +
                "nI1oOUsqqMwVo2RgDUBInC1bb0GjUlLZgoiYFras+QKVtuTe7InwNazX4UCrw4nwSAbtrCfGVdeyuxOV" +
                "dyOL6HC2QPDv/YAxfN9TXgAQXVZP9qSmJ5YooyYYrnGRtiNPLKrqwicEGHpvO4ukNYtJvsCIIcC06FLk" +
                "oZaMUWxAZjgt1jGXHpptd8/RgOXwmFS1EA9U6tYaVhXfzG6Kd2C3MyagUM++mc0jP9GU2Nlxym89FrM9" +
                "AUMIfLTUiuyxm+SbuKv2WA4yis8jUS2LAEpljNLbCot76US0FcnkIaEweyEFcSw8ANeRcYTuuqgT6oF9" +
                "qHvyoUaOFimceMkCUK9yhEc+EKLeg4yndgVlf8fPEGNXPnlJ7OUlRC4IYjNLRpfZm4CO2Uyfi5ruFQDG" +
                "1LqFJIScZrJ8PlI1g7fYIs66t3Op1CKx+UXLvcKmtEqvR17jXlUeH/WsQHpmbFGhDS50zZFBtGI4+ste" +
                "2fmR94N/Uz58rCOk0IHXGsugIhSwE2FhpNYZa2lZWt6VLMyOHFA4UYvkHqYX4BXG1NJUvAbkw0fHgp/m" +
                "yR5xJIQJje3T9U9UM6SDKSIWi7HdSH2JsM+tl2jBBg4rJsnkXNXrITt+QYe2O0VzTXb1vCmHHR4y5qJF" +
                "6mvMKpS1oy7MO9595ZLaAl6xKdslTMe5MCZo9KtFRwaqeaOjR56sRJRVDYxHXVJJ95RWKbqpsEWOlLwh" +
                "R5T66X9ZF4vKB+Pnh+xp9hhRJfz57TGiJE8zc8TPz16dv36L6E/vRQwO+Rc/hFCcF5iyTp0Cgn854753" +
                "liHGGVHIIZXrmv6MNeWWBWkQzAKnmJd2FE5FnMuHcCZC2oHI06mEtKdKy+kin/nNKJwq2tFHGLRkRWtN" +
                "pRhHy+ckh02wgVclZtjoHgv2uS+88J3IUpmeI2HXEaOAvwIPqceyGf8b+nmorMVvrEiTHcVc8AHh7JAE" +
                "9jzWwMOhsQ0TvXFu6bcCEQUHUUHfWCtrJaVaVH5Z1tUK8bZWJ8PxRjpeOp2wqTXAc/mRycSpqAC+YTKa" +
                "EDextdosx2AGGshK5uYLGz2RKAs3xTo0iIxbsCLn6arw3QddheroLjZDsYXfX06Q6wLLTcsZC/3VcEym" +
                "OrJR/Zy5aiKfpmkrUSd+IVOaiKzOm51aYlgZNnmdLY8S7ab/IxeK8WhhR426+U4YRY9ucplXyfjI2DCS" +
                "S56QULyUQeeowb5Sll45V1C7AWyg3vCRao/9M4PSCAVPKX1Jk8sy30dQI0JHZDf51I3CZkG1QCM5Sz8/" +
                "QQ62HyxPxgqlBEJcDZSHyukx6yh1Mol5ZzkU8y8EEHGRkzCFngMJWxRbmbWlQb/LtnQrEltP8dBa3KyE" +
                "jeUogd/TsJYANyQ6drhUvwvDG+tknq30U4ejRF0tQWgNpPJMId2InDNbBVW9Dybzyv58aRTifoQCKmJ7" +
                "l6JcBDCmIweJ0j0vorhbv0KywopTxwlr0LrBW/6GZwHVLK9H+trTx4CmfjdmqKyuOoHrB5Xtx1EY0shC" +
                "e4kHJHaJnjUpNusFLQKJu0yzw32uRMzBS+6+5xJJzSOlqfeLEHSelteo8tODtKHUiost07ZtH44ngoWA" +
                "9fXgmX54bu9fyutQlR/aj3x7bmbAE89zzemtZs3gWzy90QdgIrFN/63TvpnkjLCz9Tl/Wlt5LzaJ91l9" +
                "jSWc9ohveCVFh2IBw2hZ5lK8mU5rLX6RZDXpI1ULsdPjuWMl8NLvP8nuJSXy8mnwWgYDUaqaJUl6HFNB" +
                "WYKgM2TXJPi+qmGyX/G/EhMRzatGIFcUwQkxmrqMZDEsGPTwi0RwyOlGmid9Bomnkzyni4hA+FaNj9vf" +
                "gHHn3Bi833+CMNl+kKND+HZppZRE0ehASAmUHqHTQ7LejI11V7pttVQqbFv5EnI1W2WCJTRGScf79PVX" +
                "4q/FaD/zjh3QL9kW7ZIhpPuoqKaj3mCBWXd4FKFE+2aLJfzvS22EBkcD6x+2njJdelo47r5wLuZO2Cne" +
                "ucDIBK9BMMNd72ywSwHE4rKpjquCG+jQ8EFDCQfBtF44xBb3NNYcbu5Z6+QE6XF3cpKIZV92vFlDFYsp" +
                "2iry6WHSozvh/v0MmFAqD1tA2G9eLRDvtAJVPaPsoy3CQTpxX8gD5+vnje6dGksO+ugGYC2v10ahk5Qw" +
                "aDHhYe0Y5eF7lJnXJSJYCOQepTGe8ZYR+kwPbPbPkRqUnPmxbHl0HJv6czTb3aYPmc3Plg9h7nN7xi56" +
                "miY6wKjh5R7zLowH8EqyAK94jJnhuzgXGmxypwC5DT5nqzsUKQrKA1GnSgjN73Ul9s5lDTaeUK2xmxXA" +
                "XmJ6sham34auAQiDzWPLqHUxso2sq0VH5eQSx9Rp9AVgg+SGiLI8Ch/RM9BeHEGigZx6erIwQUZKuOWk" +
                "IqtzdTNL6lk6+faS5sAo3isdZt/TUGYtttZGexEqs1jx1Lpfn94NHKLwjqWuWoLBzlcDheaiH2n7bT03" +
                "kno6m/6x7s7B80CnpvzAohRUFzkPJ9k1osdZr+LvrQg8EC+wiMQRi8yQ1lqaCd0qXUEkZPo54HCAT/hn" +
                "9+TY7sGx7e7xsDsQKLtqB7N6u3OeSmUAuccvrtAisFjhZrXTciIeBygqrKvEnmnLmbjWKKc/zRzHU0bu" +
                "5wnA7la4LMnLZtvADUiqmIQ1/aEAOUUrfRCGi/tfDMDKj34BoSRCijkqxuc6WRu4ljCjoCq8sIDGwrB/" +
                "fXv6lci0J1Tgh6hy2+Lf/MpCdgj/IzIiH304P717J8VOCamGrJZCCbrd74CqLQwaNrQUSkAY8RoVTLiD" +
                "w/+LsbsVY1c8tzP/xWLMmv9fEmM3SbH0GPIN/qCUMgXnr9foCnqJDfi39+17IRM+Kr3u5mhTwNoo2UsJ" +
                "RbESkgdI7PRPbDW980+DcEwrPVmXlO7YiaI7mqRQ20/QJFMTDazumctxXV1oXodnqxBghMCEQKSsQq5B" +
                "cvTcbeASm6RvEp99u7uZnfLNnvXTGo3e4d/GAXnZs5ygxIp/0RT18oLwqObyXfi5N/hoXq713oY0YfBy" +
                "/HVDEpqJB4BE1tAh3FcMtnvK2FciWvE2i1SkVN3yFDu4+XNCiQvKZjLgfQQ7fYhclkvC9wS5QiZHug9R" +
                "cWCpQwZuzPG8vw9eSG2JoR6mEQihAxSDPqH8TSfm2qgTpPe7qEZOXUVzqum6J6F7uS+CecVtCAs/CIgp" +
                "GhLrt+uPQnQ7doiStXMfl4SE7WSWYiUOhJwQFoemUzNjtwKF7Jk/ICxHLpnPjVNhmlZPS/rMfbjFh4l7" +
                "ibNq9DscMz4KZT4yRveKlHAPig9JxQtd5Bo7H9/41JVDhSN2/TuHNC9nWrq3GAjdTLOLVXW1+ifk8Xb3" +
                "4jOvKv2xbpImBD7M7tXjGXtrRMHxVuuhBDwU7rEj4i8x9gvEa3ZuprJ15mkGYQp6+1a3J9QJu8dHEOlH" +
                "zrSa1dfDyvt3hKCHH6zY0CJKdriHO3/X4rE9q91K4UVfTL63PCUJfBkBzm88Q/RLDwX90jM+LFXuQu2e" +
                "w/nk0Zr7mZ2REj+BsSbHUl0vzcLstPjIguihyCecQdC6MF7UGKPzHKJzRogBXZmIjHTDJPktYves8BVu" +
                "UQDs5pEl8ZU0Ao9eltWmga3oUD8A/Cy/JEkVKdEYb2HrPDs95QEM+oVS/5cCCUU3SfYUv1vk+wH3kHfF" +
                "bXnidGah0RabuenxRFkc6Uhvz16+/u6Mlf6Y05qlLeLlhZsP1C/0glWQ9ib0p+YaMtfSyeaJVYiTfPPm" +
                "7NUpD7eIEI5j7h9ORjnW3KJwvp0S4/ylTsfWzUx9OwctqUcx4+lGssoDhc88LV9JHVVXmvqSJEVRaPOE" +
                "CL5G4U84yw6Y/iYpa1jZ59sSip8WKoP7v/qf7PWXfz57/o633f76zv4fEuf5x/MAdgSMlwBT4XlBBjEm" +
                "BWww82EVSCiDFiOWUMvnZxpaDu6VP8uIxOj9nlFx4ULsMh3hRN5o/xhnlCsUhF0gsVCQNDZhDyihTmSc" +
                "ouIVrMQX/nz++tVDpnt90OHHZy+/zRQAYo2BhSFmwwZI7lagoDaq9I+HmUIZZmdiNTBCubPoso9CIeei" +
                "vHAn2b3/OCCFD04OntOyOf3y4Dg7qKuqxZt5265PHj7kicYFqN0e/Oc9naKmzWEJSsRjZXlLWT1v3cjV" +
                "QpEKeprsAJ1KrQa6cM7fvDldYKtqxaAV/+3hVwb8lYh2V8Tpl8ob4Wox7ns/ssYKyFwb0IkiTiNHcsEy" +
                "I0l+shLyFjAnWSCAvCMJ8K5PgpPf/fsfPtcWVL1aJIV2uxgf+JHO//ItQvwwERjnD+vUGfj858U31kJh" +
                "y1DZwdWsefJ7fcOsykn2u8+fPJZHtK7ZAOZzdeVbQO0jk1j0XtNC4URsAEsQ6VfkoDcLfpcijrZaHxhD" +
                "g7Vv/1CS6M69EUyN8EFkqw6OOjoo8vw6+4wm+mfZ5AP+U0j1m1SQnDzF4rjpT494L9k4PP6Wj5Pw+JiP" +
                "RXh88j7eGPb5e3l3x7GN3p00MQKQhk1Fge9cRaNW2jBcjn3fTIqdlpN5CR6IDftJiZjJs3uqCQat/phn" +
                "89pNn97zW+KqvCiHddUMq3r2sJ3e+1M7/ePD/E9gw8kFL25ln3M49HTci2qCOLGtLgWEVP4kAn8nluW5" +
                "sItupt5LKHi3c45spG8HgZqRZnfi/u8tjvDizI4hggKwL8KldVpxJe2CeylNfODOnA7I+MuyoGfPr2Xs" +
                "fGOpBiKE2CU83t1sl2quH/sLvDs36aWj9Wdwxm8BIz0suecAfr/AQeLhdg8nb4lYNP7geK8yS4mh9Vkh" +
                "zRUpJIdmvFG4O08rUYRuBkFijRocG9BZKXy4Qk3sSup1LdNIhao6Plw3eQPmsXYvHAvaHT0JFViFPMYJ" +
                "1+VyKkBC0tuCRmJB21UaYmNidmIAI5k9z9BBwiyPxTnrYCvNNRPuqejP9vir2gUlX0umAaVV9uzVaWpf" +
                "BkbzAEYpCzA7vvPJVv7u95BwIOP43TKBuA5wTfRKXEpLKYwrbA72eAdoJyVNg/vJRdU+kLbvGKcveYq3" +
                "ZaWRt3DjjtVF3c0UpM7ql06AdVifnIAv1rp99JMarG7ABSqCU9FcP4SRbkDG+Nh0J0xjG962u7Tx/+ck" +
                "Av3t118+8x9u+/8MJYwX7sqrw69Z+DUOv/I7r6aU2jWtm+sWNfWDuNiOe8uT3iWVeSI50xvrYsAlwqfx" +
                "PPA9/Mrrw/cQdnKZqf94a370R8aW6OKTUykrZGUsjC4NvkKzSGE32iPBsT+wmB7OxCBMhOwLy/mKFrV/" +
                "9kSa/GGWULer+QAPkHfi7pvAP4No/21iSUmblEnxrAl0vu96KDlVZpAeVpPJZg0le0SFISWt8gbHUODn" +
                "KykOh+P24ZDGAK7H9lVhCkgCEgt4Uql5qQEKxrS1e1dyvOWxRC3ZZsnzEuP609U8Jogibuu2Qr1vuFRY" +
                "TzNKNwLx04BXV0KyfghukHbVKwq1vEAuFxlKLb44vDQWcDU+w0jStmEt9x+oxrlbBv8FA0Vdp/BpAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

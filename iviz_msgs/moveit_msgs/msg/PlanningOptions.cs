/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class PlanningOptions : IDeserializable<PlanningOptions>, IMessage
    {
        // The diff to consider for the planning scene (optional)
        [DataMember (Name = "planning_scene_diff")] public PlanningScene PlanningSceneDiff;
        // If this flag is set to true, the action
        // returns an executable plan in the response but does not attempt execution  
        [DataMember (Name = "plan_only")] public bool PlanOnly;
        // If this flag is set to true, the action of planning &
        // executing is allowed to look around  (move sensors) if
        // it seems that not enough information is available about
        // the environment
        [DataMember (Name = "look_around")] public bool LookAround;
        // If this value is positive, the action of planning & executing
        // is allowed to look around for a maximum number of attempts;
        // If the value is left as 0, the default value is used, as set
        // with dynamic_reconfigure
        [DataMember (Name = "look_around_attempts")] public int LookAroundAttempts;
        // If set and if look_around is true, this value is used as
        // the maximum cost allowed for a path to be considered executable.
        // If the cost of a path is higher than this value, more sensing or 
        // a new plan needed. If left as 0.0 but look_around is true, then 
        // the default value set via dynamic_reconfigure is used
        [DataMember (Name = "max_safe_execution_cost")] public double MaxSafeExecutionCost;
        // If the plan becomes invalidated during execution, it is possible to have
        // that plan recomputed and execution restarted. This flag enables this
        // functionality 
        [DataMember (Name = "replan")] public bool Replan;
        // The maximum number of replanning attempts 
        [DataMember (Name = "replan_attempts")] public int ReplanAttempts;
        // The amount of time to wait in between replanning attempts (in seconds)
        [DataMember (Name = "replan_delay")] public double ReplanDelay;
    
        /// Constructor for empty message.
        public PlanningOptions()
        {
            PlanningSceneDiff = new PlanningScene();
        }
        
        /// Constructor with buffer.
        public PlanningOptions(ref ReadBuffer b)
        {
            PlanningSceneDiff = new PlanningScene(ref b);
            b.Deserialize(out PlanOnly);
            b.Deserialize(out LookAround);
            b.Deserialize(out LookAroundAttempts);
            b.Deserialize(out MaxSafeExecutionCost);
            b.Deserialize(out Replan);
            b.Deserialize(out ReplanAttempts);
            b.Deserialize(out ReplanDelay);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PlanningOptions(ref b);
        
        public PlanningOptions RosDeserialize(ref ReadBuffer b) => new PlanningOptions(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            PlanningSceneDiff.RosSerialize(ref b);
            b.Serialize(PlanOnly);
            b.Serialize(LookAround);
            b.Serialize(LookAroundAttempts);
            b.Serialize(MaxSafeExecutionCost);
            b.Serialize(Replan);
            b.Serialize(ReplanAttempts);
            b.Serialize(ReplanDelay);
        }
        
        public void RosValidate()
        {
            if (PlanningSceneDiff is null) BuiltIns.ThrowNullReference();
            PlanningSceneDiff.RosValidate();
        }
    
        public int RosMessageLength => 27 + PlanningSceneDiff.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/PlanningOptions";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "3934e50ede2ecea03e532aade900ab50";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+1b/28bN5b/ff4KogbO9laV2yS76Hk3Czix06Rovmzs3X4JAoGaoSTWM0N1OLKsHu5/" +
                "v/d5j+TMSHLTHs4+HHBpkWhmyEe+79/I7EBdLYwq7GymWqdyV3tbmEbNXKNa+rAsdV3beq58bmqjjtyy" +
                "ta7W5XH2Lny55A9x3ITHTQAvyw7UK4K6sF7NSj1X9K83LZZpm5UZMXydAx6NbEy7amqvdK3MrclXrZ6W" +
                "AlbZmoc2xi9pe0ZNV60qnPGqdq3SbWuqZRsmESylsqlzJU+duLrc/IF9KDfrMP43mheg1jxJl6VbmwIT" +
                "S+eulW7cqi6UOqrcjSGQtXeNP1Z2RhNtSy9M5Qm4bnmjpnar+YKQIdJWmlcDzBttS0ZVT92qpZnYjalv" +
                "bOPqytStIIP1JrJeH50bXa4MwCydt629+Q1kOlSwuzuRAd+1qvStrVaVqlfVlISBAAUy+7/G1U23eGlm" +
                "xAevvpTVCzPTq7Ltvq+8KUYYQFSn6WvbLlSxqXVl80ljSORmdr5qTGbr9vGjPqqTuGrAGVzTtEk764/C" +
                "EpGRfZpgWVo1kDSilDvfJuQF26WmDREdpibJP33rpHDc4cyzQQ6ZRKss7HxhoCu67q0/UpVrRCZAfVqG" +
                "YGhVm7WIdG1MYYoxwCbqjb9kyb4DM1OrgMmQvqDJjdX7CBqJkM1Kp9u/PAENJl7PzCQpywQIZR1+vLkp" +
                "gahIvWxNS9hCt0SNYtUAkTRxBAkXwfMW4kv0W+gbw3skiWdA2Eu1XGE+2NapKKlyq5sWFLhKamlqENsz" +
                "FQnMbFXnYmtsuwk63RjAxX6veiztpFS+s8BH0VFBrOTTQKIAQ1dEaGZpayvGYq2BGajQro2p98I8ou8e" +
                "hC78caJuWKEwpSab8/R/+E/2+vKbUwVLY9tJ5ef+ZGCACR3ivxnoPJvizLfMOXzNmKwl0dFNySIRD1qT" +
                "vcfvS/yU1xN5HegTgbIB5lmVIwxF1MUngH+uiesIDB40CWvqorDCSTVrNCQLeleslqXNNRvXdqaO2CzA" +
                "xptcbHPf+/C842xuSDDbZiMEuGp07WFNaffV0hQfPqqZvTXFhEdP2vgZzGa0o9rnriythyCSIW7sbXYm" +
                "H57H96/5dRw/SeMnYTyUmeCVtr4mQ0Do1XOffUdP7+SBdoJvk/BtMN7nmqScR1/iZxzL7zHyrG11vqBt" +
                "uunPRAs/6u03vTI3Rryiy1tX6SVMTB+tpcpJAaGRMBv0hVwTWRnvcsv6LASugv7pptEbBdLbmWUVpE/Z" +
                "W17sOebSJmXpiYDKgi0aLDkMCb53TVmoNf7G6Bfseesisny9MK0Yzr4giRUmjTXNkiICNuBkNzk82RYQ" +
                "7yCaDEQknU2E9SH4uH8F7DSHlYW2T7Lt9ZzdSKtt7Qfenh18T5FYz8iOjs1YhJ19ODkg6JslK/OzI0J4" +
                "Np2QEJ9JiCGLf4uPorY8rlPbb2Uam+GK2MpCUJHHsEuy0+dvXxC7TWJ2oYiAZgD6NcbSuN4SPH1SuNlk" +
                "a7EkrDsyqo50/BaZxfLv6o4Gx1mcn1RPhI7kLc7uaV+A/EDiFHgKtpKTLDfKVkvXtJrcBewXOfyiZOMF" +
                "NxJRnboCCnQU90MDEfwhtC6NbvYNpoU4ohDROj3NKXI4Pe2Z5amZIZhYLQvBlfwTb77tidzxg0j/fgHs" +
                "UUonFWDxW7iy8Iq2rUGBwvi8sVPDRGAJEsQRwsDFuOaXlehOQywn+ogCjMmCBW+UJhkioHxWR425ceUK" +
                "7xu1bKyHuuXH2A1FSrYGnTenBED9aaBm0atFKLoAgOp41A29MSXZS4o+doaeeB58QuE+1LObYmbEq1aw" +
                "Bz2W5ORoByGFCQDeVJj95njMiF10uCBgqy1RAdJWUPQvGjrdsD1gdyqEWBiNLG1gsSWCoXXXC5sveusx" +
                "1bxak55zSNZw6Ek+b2cMUgMiDClPZCMbD1GjOBVLMAcRGmDNceDP0AB661sfJDsaH/Yz5L2wAnSI2bik" +
                "SHBIYd4M2M64zp0ugzJX+trIpDCecIeExax0rL5HoGzG87HauFUTTShjUTskdMKfnicMqmSqEaaw2yRN" +
                "vTF9dop/ROy3CdII6gk2wtse7n7hVuT5FhEG08nbX8nWE8pESIHT0xr245SpUuy5AZpJBtI2e8ThiCxu" +
                "mgjdEF+QVgkHx1mWvRThEBnJQmBGFpXlJ0Sr9BgVofcqCnzvlVDg3gxKW4g1kT0TGmRR6kI3BZGz1Ww5" +
                "2NhylvVFSWFPCUwR7gW7slkC646Yc7LdDUVbG0kAuahRVUTSnOlI8jqYLyKvWQptvio14iiSc1tjOEeS" +
                "gA4CG2JKnRv16vyUBZyzmRvW1TpvjOZU79W5ylaSctCE7OBq7b6AA5rDOcXFk30wt+SXvCSqsFF/EuTG" +
                "BPs0ZhjqiN9N6JHMDS1CWzBLR0qANOTdpl0El3qjG8ulBAJMwSQs6CEmHR73INcMuta1i+AFYrfG7wFb" +
                "J7jA6YvkC/1qTgREXti4G1uI7WI5JTNIwlvaaaObTcamipfMDl40bEjAPuaI9dvqGXMLiett8RDubTcI" +
                "ImTfG7CLENHRg4hHgogGU8OamQxeYeaNMWwGZ/SjcGRlkIQ5ZBYxeCDsVnmLhJ2GdeuJWX3VBoKsKkgz" +
                "5EZHbwG59RtPSanYAdTHCjaUpBcx+5E5c9N23ojTERdWvyYXyS5T5QuKWcfqBQzzLbGmJIulOQfTTXRd" +
                "mu3dP9+fv2AP+xjh5NEtmU76X68hEPCHJDveyMecCwoDQe/vTggpadUozIV/GXwnqDIiQiPBvTGoH6ip" +
                "zq+B8GAP/+9UH9aprhuyi4vf7VTj8P9LTvUunyrpEKb7u6oTNKpXitgatCaGYgD+3fr2PZOJPgq97svo" +
                "3bHrSMkmmrygDsmsxPpYu3Y7HpP5B4NHyqRzkuXsX0RP1zyW+aVo9T9WNKGpuSDoxKQ+DJJhM3tQ1BQC" +
                "4dvW/lUyxCxRlUESSDKVZnJSCZkhHFBTIauAnI2rpLFVwVbsWorCrP4wx5toDIUmeE1TjqBsIyS1tYzi" +
                "6iN2wdEN2erGzm2xbUbZ8AfkRqqdPSKRJpXiPctixEL0WgK1j7kADQVdAyFW7pisTU3al5b6rhshogog" +
                "hgR9x0oUddXW5JJ0QVyPpdHb9CuFlurXB2F1J2P7uE1mubHJnQ94jqdfOgEFkT+JUPy1fiBdZaMR0IoO" +
                "1ndZ6xCfaeOuDZBkEfMoxqAgAZer6zkHvnAaZOyiroYh3XMY9zDYifnbwzVihbCnQ25ESkWbZ9cDBLkB" +
                "97tQZGDdo9QgHqJ4eEfhK7jnrbeix9Ne6Yh9lZZ6d/BhXNK1jPV1jJi5shzK8EzHVJkMfcIVhQF+oSmH" +
                "YjJRThjK8+3uLrIDsQv9uh6G8YIHZElC35HZxT1RgKxdqAiObRFaryOuhsdq3sE+eLG2J4FaQiMRQhYo" +
                "sm1CCdDYvQiVJa6ehsCyX3+LlUrUQ3v90NatWPBo4dhr+yJtTLbBDVRK+YpNv2XYTegCBAY2kfqt9Nly" +
                "ilyJBWFXHEBSjJ1LlWiQiBI3OC0QfsD7CJLseSrASKgQ6OA00EBHs202E0+Wl44T09BS1LFAezyK5TBe" +
                "ozY5zHez4dUaU0o+G+r8YWGwj2DGojE5iJ95kU2vJHiVXhIxsLtJWCIyZW0omU/B6RYzRmjuXtduXXfW" +
                "lMc/hE7u6uJZiPhGUkyYcWQQqskxfWOd2Q4QBVWS+IBmIOARSw/DIu69prVftcdRWW0R50U+b5ZGhAJe" +
                "eao9Z4hMnaQ9oS2DPGJecz1HcBEUrgABYCLkrkwfrC3ynD2Be9RZmWabYBmgKfsadf1uQiTApSsJ/7hM" +
                "jtpsxYcUfMYWR/bJo97FTyhLdcO2gww/+D4RwtNKr41fDKHiDY2t5MNeOPjWgXgG5QATkO6igG/g/IM1" +
                "S9iNuEMfO5MmpqMSRBDyK9E04ljX8sQSx/29oUvGiPBKdyCJb93uzorC98Vouz3KDYtxbOKHQSSjN9at" +
                "PIV95pYiBWxfmvbcqSaDM86mGwrZz87Pn36ZcXkD2jBYada4avtIikIO3SCVOjKUhm/INLEqcL+pJWX2" +
                "WzJhi2NZ6f3F67f/unj6FeO0XMJOIWatE15c3giGlTftY+n8t3FNx4F4UsSTuNAh+e7dxZvzp4+CEe7W" +
                "3L8crzKSAxss+YHVHOwfYUTkW8xYq5VvMYLPc3A2imoIWTPvStCKSBstRmdNC+OJkoVskWnzGBt8uzRN" +
                "CukJJj0iAI0DXfx8X0bx00YlO/jDf9TbZ99ePL9ChfSPTw5/QJznv91cZaPJFZIZO7xgyMiMoSaFbNUb" +
                "qaEgYiQWmgZlkLn061KVQHpHJCdozw2CimuTGkL9FU75jczv6kxNFKi5Rce+mEZjT1AiwGLa30pwsFwm" +
                "+/by7ZsTnKEJtbMfz15/pwTAWJ0lESYzmxSgl2LCUEeqdPVBcerRoYzVBUcNtt7DdNYjrt3ghFhpr82p" +
                "+uw/DkHhw9PD54hszp8djtRh41xLbxZtuzw9OaH0Q5dE7fbwPz8TFOUsUu2kcFfHwyDMvRDdgDk9KvAx" +
                "r/aQJtmck+VrY0LZfFaSqk4tjgWNB/5yIK/oogoRY8p8/kxkg4HkfDxJx1KllLwgXCuiE0ycFEC5KI+C" +
                "aECW+4gM5lQlAvA7kIDebZPg9M///vUTGQHXKxUCGre748Ow0uU/vlPENm/QPE18Gix8+Uv5Mo4Q2LyU" +
                "OlzP/eO/yBu0qk/Vn588fsSPNLrBAIswN4wgt792TbH1GhEKEIkLxK67fK1csSrxnasCrVseRoEm0b6v" +
                "svxd0QLt6FzUdOpuKQdcQtJGKt9QaM1BW46aaCgsxiynMakvHM6sQUEowpnGEICAweDDpbMmSuD85Yj+" +
                "G2fc3PlaPXv7A7kx+X357uXF+wtyLfL4/MfvXr05v3hPpjy8ePvm4umTqO3RPrGXwZ7CKInSokmwFY4O" +
                "hsMg3dCuL9eNSMfhjGYH2Z/QG3YqNV6kK3yGQYggrhrkuo2W6rCbcyjOLQuiia+EOG9VsocfRupHKdv/" +
                "1N8ziMwJk6nnbaorb9sgpE0JPyL6uKPt5AeKSLqnHxOt8fQTvHhvS0L/sCsudoHtMJv0b2gAyBFUNips" +
                "igXvRhd2hS2ENEckaDzg6+T92fmrf14iQuqtGZnMMMFgaUQKVUR0uPzANcMYHnLTJSz1k9IUcIxVVywc" +
                "wJ28vHj1zcsrdQTY4eG4w0nOjPQo3uG0GKRXURfUEXThWNaDnYvrCHZhHXnorXPXKigiRtoJ+0Jysn/N" +
                "566WYkD8RPO7OH9bJ9HgsQ2nwGNRGbvsZIhpivlINiHvq+UotLM+D0TNtjQx0C+J1BbyiEc7Td0Z3BEG" +
                "A+/HxO0mAZx8Njv9RgSj27Uv5hbCA/kup1tA7V5xc6wyKdKmrn+v+t4b91AI2jpVLgclqf7pHC08HqL7" +
                "iRLs/bsgpJbR8fS2inQSBoKya1G+xup6ThHEX3sWlo9sI/+a4TSA6x35IxzR0aRgx3/4mGGNqwCA20cB" +
                "Vjx2GQp3cUbMva5xSIwH8G720JzPeMikByJVRGMPySJah77blByd+PBY9mluJ1wHfJDdcl6+t8kvTXAz" +
                "Cvl9l/+nIoG+VZ+j/Pe5yn+lvwr1VHFGrdXpUxJwM/vw5UdUFNPjV3jM0+MjPBbp8fHH1Gr48OQjv7sv" +
                "AnyihrdV19rb99yaEgWNlffeGPeJfUcLw+cAurHBonQtfmM57UuK+GEU+yf0lR50npsyZNv+I7jkhqPl" +
                "fNTHVDPvrSWuTG49oA4R4tBUFgnWAN4vVh34tH0Dgmu/1Z5mG7GF5ZhQz/Yc6fK7Z7pw0LR7OUBr97RX" +
                "sYrlB/L9E9SAJnxh44F70OFc/26Lq3+8hStU2w3oIK/jdGLoINbMdkbmC1vGKwMYuH3qojv/y2XbcJ6J" +
                "Rv1NqwVZ8qefhZxvba/tuHF+7Jr5STv77O/t7G8n+u+UZ+XXBIibyJfkxtCZKly+qpKJgezwfaFeRWPn" +
                "zEFIs4bbVeIPBJOu2syD5G121R0ESr39h+hv7b1SEVKV6CSJAqQnSbrZc8m4ZGh4SDA0sapOLuHGFvCA" +
                "+Gq7yXde8KBkg0w1kkO/qaQePZJC9lDl+qttY3CBb2lHorxcTezKvZ2QDdcHz005oyWxR+/SxQwMjj0m" +
                "IYbc6krneDoKjXvXtHbxjBcbf1mZxvZutlm+FyUUPqq/Gqn6EZ/aC+cPUTGSIlag4J07l5OA2FvKlnZX" +
                "7/XCwhECrBOEUlChTfCheN5Gr0Rchw4bF1EJO67wTh1xiCZwH/ERW8rBbnl4yIWEiiEDIzsBkvKWwg00" +
                "6ZjW6uzNeb+AmgQtAJj0RQBn6nc+Rc4/vA6xBOK81fByQceHfGHy63BbVa7TFRGH+PgA2+5dhMoOuOEV" +
                "neNdrel4iao7FdNvLcfoPt2mehgU+HbW70UAt7c+iUC44nX/2+/d3Bp2FK1YBjnMuI49mo1cENvtQ0aF" +
                "j+rOY4rULHXN+2+enYUP931CPK0n5MSVzvRrnn5N0y/94Hcw+cZbdrB7FWr7lMKHj2rvpSZmVLjPx5az" +
                "f1ap6yh28FEdzsKMwHl5+J6MHaoIEdy9NYp+Y22uYDw+58uIuE9LQZecLiDPwtfBaTyl9fs751zniL1z" +
                "Hre37xyO7Er8s6eVitLG3KYbwyFvDgARPO9D4H+DaP9tYvFFOL5cRa6AMIxTj7gQirrJicvz1ZKc7DEc" +
                "Bl+E5Te6zjeRFEfjaXsyRjBgy3iXTABxx63U3vfDS6nYoOIk04eW473xuJAFAcVVguo4laQrXB9A6CfT" +
                "ald0WQlYG6YBSEDDUzhDlvXXlIvLVDmKKMfAuTQ5XsipGC1XzdeNbaPgeNwA/xpuHNqS/RcYeyzy8kIA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

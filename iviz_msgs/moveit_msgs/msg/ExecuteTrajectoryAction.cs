/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/ExecuteTrajectoryAction")]
    public sealed class ExecuteTrajectoryAction : IDeserializable<ExecuteTrajectoryAction>,
		IAction<ExecuteTrajectoryActionGoal, ExecuteTrajectoryActionFeedback, ExecuteTrajectoryActionResult>
    {
        [DataMember (Name = "action_goal")] public ExecuteTrajectoryActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public ExecuteTrajectoryActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public ExecuteTrajectoryActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteTrajectoryAction()
        {
            ActionGoal = new ExecuteTrajectoryActionGoal();
            ActionResult = new ExecuteTrajectoryActionResult();
            ActionFeedback = new ExecuteTrajectoryActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ExecuteTrajectoryAction(ExecuteTrajectoryActionGoal ActionGoal, ExecuteTrajectoryActionResult ActionResult, ExecuteTrajectoryActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ExecuteTrajectoryAction(ref Buffer b)
        {
            ActionGoal = new ExecuteTrajectoryActionGoal(ref b);
            ActionResult = new ExecuteTrajectoryActionResult(ref b);
            ActionFeedback = new ExecuteTrajectoryActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryAction(ref b);
        }
        
        ExecuteTrajectoryAction IDeserializable<ExecuteTrajectoryAction>.RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryAction(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "24e882ecd7f84f3e3299d504b8e3deae";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VZbU/juBb+Hmn+g6WR7sIV7Sww71I/dCAw3SkN2xakq9UochO3zU4ad2OHTvfX3+fY" +
                "TpqUdgZpF0CINvHx8Xl5zosP/ncRFVqMc/6niLTM191IJzK7lDxl3HwNZ/ju+bvphkIVqS4pc/O0j/ZC" +
                "iHjCo28l9dQ9e51/+ce7Gl1+ZAt5JxIdLtRMvdojEWnpfRY8Fjmbmw/PypYmE7uRKHrnjEwQJvF9zYyd" +
                "jIEeRwmlYyuIldJ7yUaaZzHPY7YQmsdcczaVkD6ZzUXeSsWdSLGJL5YiZmZVr5dCtbFxPE8Uw+9MZCLn" +
                "abpmhQKRliySi0WRJRHXgulkIRr7sTPJGGdLnuskKlKeg17mcZIR+TTnC0Hc8avEX4XIIsF65x9Bkymy" +
                "VQKB1uAQ5YKrJJthkXlFkunTE9rgvRyvZAuPYgYfVIczPeeahBXflwAVycnVR5zxX6tcG7xhHIFTYsUO" +
                "zLsQj+qQ4RCIIJYymrMDSH691nOZgaFgdzxP+CQVxDiCBcD1F9r0y2GNc2ZYZzyTJXvLcXPGQ9hmFV/S" +
                "qTWHz1LSXhUzGBCEy1zeJTFIJ2vDJEoTkWkG4OU8X3u0yx7pvbwgG4MIu4xH8MmVklECB8Rslei5p3RO" +
                "3I03CKePhMadwWGg5YRlai6LNMaDzIXRyygCX67mCRxilKBwYSuuWE6AUVCCANQz/jaQhEl45g6Dk/M7" +
                "QGM1FxlLNIOiQhFogQuxWCL1pCl2E09lUbMSOLpizSZiSrJwFolcc3iOJKrb18mfxKVPYF6It6ZDKjuz" +
                "aZW8shg7bKZDDCrFZ8I4gamliJJpElkFnQSq7bhTgFgCCLUolIZkDFEHqnbpP/Lcc2RDkwe9oZxIvXkJ" +
                "U5dfn0KordO9zel2/TeJLFGT7k96DjdUL+7tuIKHkvPgYnvngt6HsZyG93k8kqI/UWarBlk0/PHV6Zgh" +
                "qJW3teWaHkGypE/1THIbIQBsn1NsbyxshKrgjiyO8IB6S6kSimn1xxFDWUBkaazigUeRSFGUzOLXr+Ao" +
                "m9RiiiDWX02pM1mlhlJJgSQspE0m6aZpLdTueFpQSCMHJDZfK0qnqGCQiCvzxtiZGTsT0ZaWbajuedNU" +
                "cv32tTG5E6z2bqNO7WVDrdp7q40XF3bJZKRwmstFiIyEhSdy5p74cPnQYrHKyTCztakrV1stAKVFs5CL" +
                "KfIvFUyTYnc4rFRbbaGePIftVO9RCa1l5BS52MRrC/Hq/FTjdSAIeRZuVBZpLVM4dGH2H5bYtBQ8lUiy" +
                "W+KsEsAFFT4tYmHKTp4j9+Owys2vNs591XTpS1tx5g5GBlipyGYoBu5Vxa0GsSNjrMYmi8aSjOo7M8yb" +
                "0DTM2t7u/LDHm8+TJ34kTOmTbbdGMH4JsZoj2UGxJPS9ZeB36M2ERONbnjMuqaBhtUO5ko6IJ54TKv7O" +
                "hetaYiihKfNklhiobey9fcwqUboZ5feOyBrh/s/OaaLsyRPFHhuXd4gqRlUZS85TE6FXAm2aXsl7CcIk" +
                "1il6NliGR2h5vFuDiVO7PzUKer8X2JBnpGsubQ54GiWdMDtUJOzQ2pb8rGpZ0RCjpxCcMpPc7MTGOMmF" +
                "aWLbFivUiB5RExtL2COTFAoL/g0sBe4Lpt9cLtMK/alzuqQtB6I9ax/ZNthQUb9obmfmPoeuk+AVbyVA" +
                "w5M55Y6Ynp7YfGdktofBhdTPOmsftllvytayQAcMHfAld9dIU2ZLucx1R0t5RMXBsWga1IR61R8nGXpx" +
                "jupcVkH2vfq2rr79/SSu3mBsl7czitOq/jR8Tk9/bQBKRv6pQuW31RPFKiWQUq3y7qw22a+pzySX3+hW" +
                "lRmIKVw+M4HbKVUnns3MVZ9u/apdxaoj2Tw7ume5tdQnUD+f4ozg0kLRnQ8f9wc5bpDlJliPo81eqbyt" +
                "MRPNR96XotqHa39w3htcsvKnw37FX9uqmfsm9RNroV2g4iob2bmJmy80btOOZ/ds3Lv1WY3ncZMnDTSK" +
                "HO2cRqKYCEo5D2J8PfT9q+uxf14xPmkyRl4UyR1Ndah+otiV12XGp5q6Ts1MhQOF7exxtMd+8POSsaqr" +
                "svOaSC6WqSAOBG3HBYIejEW+QGFKaZKmxaETeXRzdub75zWRT5si08ACbUsiSGxVRGSFaUFjtF2G2HdM" +
                "91Mw3NiFjnm945iJNKqj7pPJN7LvPCkuxE9NY9p3Sf04T9ICXcse8Yb+b/5ZTb4Oe3NfvFxQzOxBgBmF" +
                "yEJvw+Xo5zJORMRdOdkcVqCFoLGNqTimCuNChzq3RwGHvCpSOugbHx95FfRQ000QbsBXOa+y8Fm3399E" +
                "coe9e6iAbpK1S8KHWBc+ue+tptDZNMkXdNmjW42uZwEjiYgbStRh8v5fUOJhZiZQNMLPHkBT1z2Y6Aej" +
                "cZ1Vh30wDLvVjNENX6mVj+E1YiKsEXhlAuLStoXVDTnJbpMHxJ7pBiVZm0y6QlO4a8KJG0U3TeWqui8g" +
                "FPLmDJIzNyc048ZaUaMtsZgUsxmZ0RFp8V0/zzjRlWTPuwJVT/t5LvMzSYNVQV/DCN+fQrDt4z37bwck" +
                "kBfeCxoy3Zl/hMCJc36XyPyFIzC1YDTqHJcvLrq9/s3Q73ygH9prX1/3u4MBQjmkdf+806o29Aa33X7v" +
                "PLwKxr1gEBJhp3VSrtbeho6yi6Qbfvpf6A9ue8NgcOUPxuHZ5+7g0u+0Tst9Z8FgPAz61XGvy4WbQfdT" +
                "3w/HQdj9/aY39MORPxgFwxBsu53Wm5Js3LvCKcHNuNN6W+lQlupO6521yjLlWUYA+g/7hjigi0RUzeqt" +
                "B1VlqXF3OA7xd+xDk/AsQGobQTeY4tddNLe9oI/PUXjdHX8G+WA0HnZ7g/EIG443hr0Muv1tfieNxR8x" +
                "Om1Q1tbKXeSp15vTSmddDoOb63DQvYLNj9/cW91iBpq32zTD4FPgVMXyu+1lpP0vJf/324vBJyq95fIH" +
                "6wu1xr1psWX0iyFoQogxGF0Ew6uwRGfr5HiDFGc4gMg/+0IYBUZuQUhAAWVlzZrI9NcsVgZ0MOoNLoJq" +
                "8bWVrAaNpnSDIOx9CUdB/2ZsHHd6/GhT9QfcDsr/Of/j+0H1z+vn/K91pU05AzTiCu//OskIbsUfAAA=";
                
    }
}

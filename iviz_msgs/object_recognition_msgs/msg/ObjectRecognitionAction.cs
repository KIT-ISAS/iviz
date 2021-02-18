/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectRecognitionAction")]
    public sealed class ObjectRecognitionAction : IDeserializable<ObjectRecognitionAction>,
		IAction<ObjectRecognitionActionGoal, ObjectRecognitionActionFeedback, ObjectRecognitionActionResult>
    {
        [DataMember (Name = "action_goal")] public ObjectRecognitionActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public ObjectRecognitionActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public ObjectRecognitionActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectRecognitionAction()
        {
            ActionGoal = new ObjectRecognitionActionGoal();
            ActionResult = new ObjectRecognitionActionResult();
            ActionFeedback = new ObjectRecognitionActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectRecognitionAction(ObjectRecognitionActionGoal ActionGoal, ObjectRecognitionActionResult ActionResult, ObjectRecognitionActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectRecognitionAction(ref Buffer b)
        {
            ActionGoal = new ObjectRecognitionActionGoal(ref b);
            ActionResult = new ObjectRecognitionActionResult(ref b);
            ActionFeedback = new ObjectRecognitionActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionAction(ref b);
        }
        
        ObjectRecognitionAction IDeserializable<ObjectRecognitionAction>.RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionAction(ref b);
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
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7d8979a0cf97e5078553ee3efee047d2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE81abVMbRxL+rir+w5T9QZAS8tk4xOGKusIgx6QwECB3l3OlqNHuSJqw2pFnZpGUq/vv" +
                "93TPzGolRMKRGB9lg3Z3pqff++lenfV/UZm/UJkZltprUx5k9Ps7Iwsh+eP1EJ9bZ+vXXShXFT6ttHx1" +
                "39p3SuV9md2k1YN43dr/k39aHy6/2xOGuQBPNRvXYzd0L+7hjiRuvVcyV1aM+E8r8FnofthIK46PBKnj" +
                "Wud3pWSdsbI+j0DO54GRwGXrubj0ssylzcVYeZlLL8XAgHs9HCm7XahbVWCTHE9ULvipn0+U62Lj1Ug7" +
                "gX9DVSori2IuKodF3ojMjMdVqTPplfB6rJb2Y6cuhRQTab3OqkJarDc21yUtH1g5VkQd/5z6VKkyU+L4" +
                "aA9rSqeyymswNAeFzCrpdDnEQ9GqdOl3XtGG1vOrqdnGpRrCBvXhwo+kJ2bVbAIHIz6l28MZXwXhuqAN" +
                "5cAOZe7EJt+7xqXbEjgELKiJyUZiE5yfz/3IlCCoxK20WvYLRYQzaABU27SpvdWgXDLpUpYmkQ8UF2c8" +
                "hGxZ0yWZtkewWUHSu2oIBWLhxJpbnWNpf85EskKr0gs4npV23qJd4cjW83ekYyzCLrYI/krnTKZhgFxM" +
                "tR+1nLdEna1BfvqZvHFtcLBrRWaFG5mqyHFhrGK5WBDYcjrSMAgLQeEiptIJSw7jIAQ50DHbm10SKpFl" +
                "PAxGtrdwjelIlUJ7AUGVI6eFX6jxBGmoKLCbaLrgNVOFo2vSoq8GxIsUmbJewnLEUVO/kX+dJ5tAvWBv" +
                "TofUehaDOpGVOXaErIcYdE4OFRtBuInK9EBnQcDIgetG6hQgYQGYGlfOgzOBqMOqbrIfWe5LZ0bOia2+" +
                "MQUZ49oa3RoURiJeP/4sBrrwyl4Xeqy9+9KcNqvR72dx5E1fObI5/txN5LGoxWr2eSS7l6vWSpmh/Pgm" +
                "sRouznunR8en34n0sy/+gt/BddnfRgioufLktXAsuHIW8mbML0vRFGkeHF4d/70nGjRfLtOkhFZZi8SE" +
                "HN5X5KIPInx+0et9OL/qHdWEXy0ThnEVKkNOYSmRXetwEXIAB6NAh/SW4lfNuIyUw5b4jZ/n+I/IZC2E" +
                "fI2iNikUUYCrJipgdPNK2TGKV0GV1KutyPLlj4eHvd5Rg+WdZZYpYclspBWx7aqMtDCoqIyuU8R9xxy8" +
                "PbtY6IWOeb3mmL5h0fOKs8KC97Un5ZX6XdWQVziDlDeQuqiQDu9h76L3fe+wwd+++Poue1ZR7NzjAZwK" +
                "TeVX3aXz+zz2VSaRcphmfVgFmEFpmwssgI4ub2WBXH2PANHz6kjZF7tP4Hm165XGcxAunK82Xq3hw4OT" +
                "k0Uk74tvHspgrGTrOHyIdmGTu9ZaZrocaDsmTEh11DezAHOi8iUhmm7y5k8Q4mFqJqdYCr9wAKGue3zi" +
                "5OzyqklqX3zLBA9qjBHBFyiJHFYjIiooQdYqICrdAKIjyCG99R8Qe45oG9I2qXSqIf4ahAMcclAUZspw" +
                "nhYiFOwyBpEi4gSGG42iRlty1a+GQ1JjXOTVzH95OBHLc+u+DXHpryoPWw+sBfSy9d3rsPGpwcZatlrP" +
                "H/Mj3vcOjnoX4lGbw8/GMrzZaG2kfi4BUOry4AIus7rPAHZiPNxOw3ddhoYvBPewspJk3CNXQsSbQVRA" +
                "hM4UAOi4CobJhOlp68aDbQeAGMkxh49S1mXv4OLw/R9T1kaE9JnZNlkAMAAEY4m4mCFi/VSpIN/CzRaM" +
                "L8Au2tx698ZTB9Kqbr+g7yV1BucLTSaUA8W4CTfISFKkzQk65vC0I07PruI9pODrrDBVHmcF63z5f5fr" +
                "7C0BFXF8+u7s8cIF0Q5NSQDDAVogj445QAiCxXrJkUWFJ4rINqLIocFLVSbPIUqXhLDwBACINKFVAeWM" +
                "9XDkY62g/qngnnSBdMIQRmUjbgXR4hteTCvp+ZhAz8p6JHujuG5vcCsaKistXhiL2WZe1Aqfv526r2gf" +
                "bWZSnDVyCoA9MUJtcoQd56YSMtRwHRNHoMlqIuYlfo3ApinJ3qm3T5H3F173slYqvCOWOqyvqSpa78lB" +
                "YkSKBTOPcZrkMocnP15e9S4uH+s2QcVkap5WsTZC3vQWBZ0538mD40ft9FVhqCCHMAm66oiYYzkqXDKV" +
                "sXrICAagwRnr6KzNoBICDFwcZTg0ONZI3iJPoymxi01bG63wKRj2nFg5pBCkrNaISLciS6cWBl3qPI1D" +
                "4Oh9ciDCFH0ze+FGcoIIx5ElybCTbzSGi1GoRSoAK7Q+cPJBuVFN7BonjtZywKezYK/QvZUTRKIzibc2" +
                "Nek8vhwCYXKlcyrs+4WmKkNFqA2+4k1lOxQ9xP8sxUUhsYZ1ABSFEENQFrq8CYNQtoe2WEGeKCtvKB3Q" +
                "TG++0Roq4tLOG0qFOmtp4omPLn3i/Oyy96cktGSLUOF4rMFuSE4y75t8TjWei/7ewlgwXGgQObuTassA" +
                "M2kF2yAMsqqS2mkKfytzLcsXYwLM7KfUrblKFndV5dQ/QOrQ8KQU8XsZB8x08pNX1kWe+yOF5+gpys6d" +
                "KkM2IGv5qSEAx2ljIslkZAS4MLyfEm+YKyJGcjWgXkaWdGSqACkWbtR8XYkIXhFICEq4noaWNsV28I28" +
                "v5EaDZBZ0Mz7TYZ4Dkyze8jx/eXZ6QtqjONA/6eDDyexoelSLxZrCHr7VN0A2W4UJzVVjxW4YNTYA3vp" +
                "YBqL96VTXdHrDrucWu9av0OlhZJbYcwNgv4GNe3Zv9uk6PZe+9BU2ejobbsj2tYYjzsj7yd7L14UBvEP" +
                "pfv2f55FIS2P+kt6cVLeknoMZexgxdi+kZEaeqBSp30bmzSQKDLNjVLxJcegUDPd14X2825S4hrXzXiI" +
                "TXrk9zc6E0dvg5PUyB1pMV/CHuxmYX6HkJc0knB7dPMdeIwC87VgSnui1kK4SYrAzVVF7H397ZvXcUlm" +
                "AGiy0FO077LdTqdd/nACAAHsMjJFXttr+fDLT8X7tCSS5+NEezp0O7vx1sRY3Pr69c6rcE14iJZoap7T" +
                "GvTMU2Pz1fsl7EECpVNScxkfj01eFbTA07TJm0m79nFy98/1im99qV7t8Ihfx5UtaZxi93Q7R90sXXDB" +
                "ADo68WXLmDpWroTUz8k819FTmxnHweCCh0C4UziKHU/0/Bwl3mec9EElVMz0+qkZ1n3AFTsX/cL0Ozxq" +
                "LeScAjU1o2lMGFlRpa/BzrPgps8CrummNzHhrNAz8IkQhOq0sUMZmjXAgk09hla2KYy3qMi/pBcsmxXE" +
                "QFOh8q2uOF+QcY29YJpQBe12iTKQTl5lzCqxCS+xEgJM6KVOgFS1nhziWRk6kV/smcH2oCAgFrjXoQEI" +
                "mwLzMvtUaRczUA15V96gUj3ZpMyxkydlu63uyksNPHl1RBmzyjzh8KjFhrq64niQIDWUR+/TkkI6oMKA" +
                "UTvQCeB7qnNIGJFCocohrtYQTa9rA4F0xZuJp6N66sAnj2RZqsIlUbVNDhHLR/QX1g05TbfFhnpHrsBv" +
                "mMgl4gso/Gh33ddIeYQ1BDoJ13gRWj/4W2IqolsYacLTt5NaKBlF0jRF98rVO6yZpvWrO/Boef0bMMgn" +
                "L+Z7B7BFijx+1hEOfkZa3Uykvwqa24pSQaSca1pN5MpSpWXNU5bHfxSXNOkO3vBk2YcNsT75kPlCWE9S" +
                "/iGcHGRHYNt5NDK2NzJZTSVknW4cyx6fXr0J49iX8c6P8da+eLVY83KX7+w01tCtffF6sYbsSO8qGmvo" +
                "1r7YjXfenZwd0K198U3zzu5rGl23UpKn8pBMcipDKLM/JmcxgwGNzXjBWfg8sGYcRtmMxVgVIUTjQewU" +
                "XKix6Sh9VmVFSSZkBacUIb9blc7JgMZ8ZOQ9nHAsS4D2Qo05e6bGkTn7XG6x3LRxmANJNoYf1L1R+Bfa" +
                "seieoP0QCOOvsWXI1UzAg2mmYdUgNPhpfsJCoB1Bc6Xcx59bdMZVJIAYq2nRAfGlBUVZ2hEwD6PDasIL" +
                "mJv1HVra9ESqSmKsUVkSCziwZipY/ONO4FPNrqG4z8ntGh2lYM9SR3J30lXnzoGFr7qJzFQYyyCCZvWn" +
                "ef3p16di/57OMomUvj7E0xPlULL5OzPc8XJ3K4NvqvDGeKUocwlLX0lqrVTju2cz2S8leJLYKpKY84Ss" +
                "O/uF1YLUVRm/DOMJdZ1zs8K8PxcXZro9lr8AitSUZPID8ovd2S4UVYschuqpbbG6Xt5ofmhyiTZWz1S+" +
                "LWdNHnkpf/kH9AmndILvNRonq+hrX5uzjgAg/bWDmuybrfI/BVG8c/un9bf/xbe3kpt+3Nn9uSHM05kO" +
                "Eh2s0e9dc3X4TbKhdjk8DzFJjtlQdlcEDFUvaP1QwYttyXQX655GwMXZ63xyiaEV38TVpwXjhBbgnb+d" +
                "ZtKn6Rd/0bn8fdc//H2k+ouz/y/fmK0la/0XmWG/tz8sAAA=";
                
    }
}

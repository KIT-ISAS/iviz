/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupAction : IDeserializable<MoveGroupAction>,
		IAction<MoveGroupActionGoal, MoveGroupActionFeedback, MoveGroupActionResult>
    {
        [DataMember (Name = "action_goal")] public MoveGroupActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public MoveGroupActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public MoveGroupActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public MoveGroupAction()
        {
            ActionGoal = new MoveGroupActionGoal();
            ActionResult = new MoveGroupActionResult();
            ActionFeedback = new MoveGroupActionFeedback();
        }
        
        /// Explicit constructor.
        public MoveGroupAction(MoveGroupActionGoal ActionGoal, MoveGroupActionResult ActionResult, MoveGroupActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal MoveGroupAction(ref Buffer b)
        {
            ActionGoal = new MoveGroupActionGoal(ref b);
            ActionResult = new MoveGroupActionResult(ref b);
            ActionFeedback = new MoveGroupActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupAction(ref b);
        
        MoveGroupAction IDeserializable<MoveGroupAction>.RosDeserialize(ref Buffer b) => new MoveGroupAction(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "be9c7984423000efe94194063f359cfc";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1dbXPbRpL+zl+BiqvO0kWmHdvJJtrVVcmS7GjXellJdpJNuVggCUpYkwQDgJKVq/vv" +
                "9zzd0zMDELSdvZVyV3verdgEZnp6enr6fQZHxXX2qiyWi91RnRfzV0U6TVL55+AS/+4dNd+fZdVyWluL" +
                "Un6127zMsvEwHb23VhP3u7fzT/7TOzp/tZ3MMHpeD2bVZfW4Yza977N0nJXJlfzVU5ym+VA7sMXhfsKp" +
                "DvJxmInQQQhwN0hX9VgRUOx6D5LzOp2P03KczLI6Had1mkwKYJ1fXmXlo2l2nU3RKZ0tsnEib+vbRVb1" +
                "0fHiKq8S/P8ym2dlOp3eJssKjeoiGRWz2XKej9I6S+p8ljX6o2c+T9JkkZZ1PlpO0xLti3Kcz9l8Uqaz" +
                "jNDx/yr7ZZnNR1lyuL+NNvMqGy3rHAjdAsKozNIqn1/iZdJb5vP62VN26D24uCke4Wd2Cdr7wZP6Kq2J" +
                "bPZhAeYhnmm1jTH+XSfXB2wQJ8Mo4yrZkGcD/Kw2EwwCFLJFMbpKNoD56W19VcwBMEuu0zJPh9OMgEeg" +
                "AKA+ZKeHmxFkor2dzNN5YeAVYhjjc8DOPVzO6dEV1mzK2VfLSxAQDRdlcZ2P0XR4K0BG0zyb1wkYrkzL" +
                "2x576ZC9By9JYzRCL1kR/J1WVTHKsQDj5Cavr3pVXRK6rAb58464sXNTCGs5ZJPqqlhOx/hRlERZ+SnB" +
                "Wt5c5VgQmQS3S3KTVklJhqkwCTLQoay3sCRIks7dYFjk8hqscXOVzZO8TjDRrCLTgi+y2QIiZjpFb8Ks" +
                "lGtuMgztQSfDDPsDKCSjrKxTrBwxiunr8M/HtiYgL9DDshSBzokJJ2A2Rg+VaNiDVZVeZrIISbXIRvkk" +
                "H+kEHQZV30HnBtEGQGq2rGpglmDXoVXf1o8rd5/ST+QehBkJfTpNIbUFZ8MdyPDpHKidLNgGbOt+Dwp9" +
                "cD/otvAzYUbWyLE1sU25slz/LBlnk3yeC+tQMKY2GS4n388EGkDIVMBZWFB5USzrxRJMWHs+IGucptxT" +
                "dVZWAo4Nb4ryfbVIMbDwGx8ZLMf+bAGZVwFI7wdrHUHyEAYL/7Cnsh1ClmKiJs8tF9jvEN7J4cRz798L" +
                "CMvKBgILUdBinDIjhwGdRVHJ7KukAGbEGHtJJMyyLCliinlWbfFJlUWNFSBAAGSV1VCDZdY7K4ZFfS64" +
                "AKOyHghetmPQucop94Tf5VUg0qwYQxNxn4I0fNpPDlJIgGyazQSLCWUjGqZlib0mqyY7z22eS2ClW0ce" +
                "UOaNrnKoN+KYT3RGQLwuUyGIrnWkpBQEwAP3tM4r7rveXujx8zvV5hEQTuy4cMQHKdP5LSaJNxAyBVZF" +
                "VjrFTqcU4b/LYrwccTsLT+lUb/LpNLnOi6loU6FyjOeGiMB0sZg6OQb5pYNgUeZFnfydogHiR59txijL" +
                "4G2ESaJ4ACKm0omMhKd/z0aQxrcqc5QUt70L/zyGH1p3jTKnGiqU9NGMyfeYByS3MOq80Ibcnybvtsia" +
                "XONUhGazrxCMshCi3wSheyeqbHXwS8ou/nCbAcAi9WJw8c9iAUuHdDC40nNAWB7ucjZEY0Km8RFAsD+m" +
                "JTtsBsEALZWcXxUlmBxkLKZLJ0YM/RImRym6TI0bAB54aZnWNZWVJ+Us/ZDPlrMknRVL2QyqJzsoS2aZ" +
                "TosbtdRsM4kJ4uyTzd5kWqT1N8+tYRhWVB3lCswSsQ+gUwsnyVSAix0I2bEcCbdEqAltE1hvUIA1eVUV" +
                "XzoaYQuTqmCQfpLsOuSu0ymkrGw3oLbxZOurd3h7sQ7gbQe4SD+6DVZS/jhRQsgzcjV3jjObchAIvI5u" +
                "tDMBzk0QI4MToUAoANkRsp1CkkDzkrhC25fp/BIYb+QzLlw6r6e3FIt5JRbvaLqkJTDMRBxDIIL6T/pP" +
                "NkVyunGEx/WV0zzC30IKrulX/ScCC+JdCb2R97P+1jqKAGDzTUyczb5fZjQaWKdBpUs7UIwabeLu7Xb3" +
                "obc7NJ9pbrObvOZOhZCgWVCIFAiTJUQ/dxneijzDvheTG/sP+2UjTYbFh02yiwkAY5rmviFasQ/kQMOW" +
                "vVXL0raMbI1ZMcyh1qhMIEMm9G0CYNitN9l02ue+2hetpRwhRjoal9kEqpMeg6lCoIiJltjNVcvJNGmQ" +
                "Q1hg1bWRSTl021LD0rG48ZniIQpZZCUcld5lVoBqkNtC+bcixJ8R8ECBtgXPP38oMJwNdTes1TmurSmk" +
                "L1CHYUFWupZ35IoJnIREeLCfeB9DVn2WYcdzmr4nNVReUpQ4y6MUz2GLFuG4gCyAdgaMWfqeFhIcPNHe" +
                "UOSQZdSc82qqYgyP0WUj619irwt3SSsSUNxpccDhJpT5JcSQ9MRAM985TdzkIGsmT1XBCM46GNaFDkhR" +
                "O7lAUXdbLGEzYA74R+n8ftFdhpf4p3VRbJH1HYgmQU9F5NjGxJ6swaPgcpMoH/y/bv2/fr0HKRJs0LXC" +
                "I58H+qVDyPom79ZcQwhetdmCeTzBMlVmQ9ALgIB8X/W4tkWpg/+ZL9UAlnbBAJY3zuOcwYC9Sq+9fsqS" +
                "/ZOXanl6jSb2dAz6iG3RLhpCug/GxWTQGmy3rmH9AsqomE7zivMshrTUoL5Se4cFr7CgMguaRJ4Gmz3r" +
                "v2fdT6Q3TGDrjb3rXg0cZI77cprCKZ2PGRoi+4KZnU9BzTcCHwcrSdQcthJ9bshIbqbJZEWoCIbqmEj/" +
                "SCbPigoqOPHqWONaFjgRV8GmOizGtEg2DB80pOnMUNY0g+3f0RgDYcFTx1rb25BV2fZ25OC4OIH4XOKL" +
                "14p8HbHcZm9YFHRGBpzcXQm6bgaMKJX6LSDsd1VMEaWyTQ+rZVTm3PoalMHUZOJOv0KuwB2WvVNiyUEf" +
                "3QCUjyGSo50yOmxqimyUGXUun5fwe/KK220EzYuBRQnSImOADnG0eJuZhjEo6VjMnM2t0DTYP+2mjytp" +
                "/BhRPW7P0CWbYK1qHyRUb8pbjQ7A8Yy9j2E4cWLifOob6n+Ju5DbxpD8ukNhUVIeiHOghFAt7Tdx1RnQ" +
                "svGc/+uiT2AvBPf6NI9X2mhEFW64X0YRHrqNrCuHkBWU2B/G1Gm0BWCVI8TkONuEj3jU8CY5AveQLCON" +
                "mSaFBRlRi7GXTWOJGk46ufaYOznMPIZ+8gOVGvWb6hsnQmUW8wIA3fq0opSENdsSXTWCG4OdylmH5dRI" +
                "AP2kW8eNpJ7ORtc2mrsLgCjljE5V/itkPaYMQiqcaNdIjEcMPo3teR4IQb5AHHE3DWlaSzQMaIfKCkIt" +
                "ti05dTAhUYV/nILET9sI0SNj+OiRUuA+BMqq2sGszswGUh2KZVcZQO5xiyu08Cw2zi5hWQnj0cQaF1hX" +
                "ul+QKMWNiWuQZDmql3S/JkkYTxlZTTGQHnY8428Mt9j+ZLqhuoUBInYqtYfE4cU08uaS9rnM6rD/6U/6" +
                "QM17CCURUsnoClZCP3nJrfABEd8peCQVix+qwgkLaCwM++Zs/6XItGdU4BswiuES3qY3jONrmBAGsL4k" +
                "B5PLovxEjJ0SEn+VOaBoX+7oxnuxJtnCoGFDI9YtUWuGmjHhBg7/L8buV4zd0I+7+mwxZs3/L4mxdVJM" +
                "DVB2r1oO34WxMFp5dl5pdAO9xAb8u/XuByETXiq97sdd9Fh3OIyi1b1YGWb1TQa+qG+KlURn1fIpez1z" +
                "fyP/r/fXJTqUcwoA89LuZ5Jh4C63GHIduTOT8Y2J8NcvAWvS4ZOen/3r5p4WUDjJTcukbhWMx+Z8hmXx" +
                "nvnCufjiFX0i+gWUw4j6SX5Aojt9v4CuSfjt2t3P7HRPdKwalkKXJ0xuC/qcUTLKI06Q5uTnTVGAhZ/q" +
                "CtxHJHCN/+lkduupj6h7D04EGOoc8g/mrmh8CqSis2vBff7bYvtCx0ZeEHHPJdNUVynqIYRMjGaFhNUK" +
                "bmqPx+41m8mADxBycelpWS7OVUAi+aHucx+RHY0OQ/szfuOc6gdd8MzFVifETyNKLXAAZq+ahFKgFtpz" +
                "Dp4EMUJm0o9nAQOGJaLIaF0shfEwsMWvH3nEFA0JpaJ8Y3wbJRujDkFrCLCBhlFo44W0pWIlzhEML+bM" +
                "2kUlWA2xFXU9GKbTSUqIbkYYfioA7aJrEGoZyA7zWUN+oynMbcbZkOeRjeCguLC9jTHPRtS/SIpxtBJR" +
                "cqlNcXFeNzCXj2UMLnYT5caCZx7SaCAGsRu4IWxRbjIU5niLpbUYCEtNkvfz4sZ7B679fezJ1b2468wA" +
                "iRGOhTQ+qGM2veyZ7ig2ON5N0xFwQ7hHYGH1WHRwiFhUVOqg/WydUaekTMFIxjCFoV046vjdo3+jnGxU" +
                "XGqOX+eiU7ggBIIJaVGLljlpK2msVWvO9qx2Q4JIJQN3SldQPA7qGQHOC6aUbJgRQyQzIHgNYonEUTyl" +
                "1am9oncYmrWjsVXjPflKCxKOsuqqCZVP0HamLzrh8F0A8YKbw9KsjKOhQsJZBXHSNhm6SKo0Mx9FjQhM" +
                "fqk7DSs2HstaQLpyiM0YN1ZtyERkpDWT5LuA3e6Yvl7EGEp1n6GRuKFEvaNG4FHk3JcV7ODsAywFoo8I" +
                "nipTETj93vAWdtzu/v7OEw5zJkK1MdKkLOhWwuieX+dlMZdCBcaHICGQvEPxQYmqL90KEvatsZkVQpS6" +
                "HG/qSGcHRydvD3a+kjktFpRT9GCNm53P6wSrIO3cg0/N1ZIR2snmiVUIkzw9PTje33nqhHAYs3s4GQXV" +
                "B9mN43y31JIVQcEC3Cq3bubGWP3SNJvU6qLQRYY0Q2KctAJpTWIEaYrIJCg5VhSFNs+I4Ikm6TX3AZj4" +
                "SQPUGroc/t3Z0J8WKr0Hv/lPcvLizwd7F6x2/O2d3R8SZ+/jOQ4RmhL9m4jCc4IMYoyBCrowsAqqVl61" +
                "Li41bO5dRw3hgk8YJW8YFe8zH5eNR9iWJ9o/xFAl5SbsAok1T8ZDE/aAYgDHwxgVp2AldvLn85Pjx6y1" +
                "cAGVn3aPXicKAHFUz8IQs34DRLk4CmqjSggaqVI3hYISJLEaGH1dWXTZR+LQF8V72Cvvs+3ki/98SAo/" +
                "3H64R8tm/8XDreRhWRQ1nlzV9WL78WO4H+kU1K4f/tcXOkVWQBA9jebMnWTU1XPWDRcnogItx7x+iE6s" +
                "ZsMueJ9lrgR2MsVWRTYaLo5TT138ymSGEtFyi/svlDcECGfFfe9G1jgImctVhLmomBTYMkrmJivhfAGz" +
                "nXgCyDOSAM/aJNj++rtvn2sLql5NpaLdKsYP3Ujnf32N9AVMBOYw/Do1Bj7/Zfq9tVDYMlTy8OayevaN" +
                "PmHGaDv5+vmzp/ITrUs2gPlc3LgWUPuouhu3HtNC4URsAEt+6VsUniynfC/p07pYPDSGBmvfVax2nbUQ" +
                "yg0kW18tyGlbyegWprUYbWC3LHHRJvNywBaWngFbWZQJFs7QTAAAo8CnSpedqIbzky38DyEAFmp/m7w4" +
                "+RFqTP99fvr9wdkBVIv+3Pvp9eHx/sEZRLl7cHJ8sPPcdrvJJ9EyxMm1UivNRAKyG3ArXE42NA3h8dDC" +
                "1zEgfU/04w5Rs20N/EkRG1OJVl7ItiTXB5NUD0Ofh6rcJO/pfEJMXFBV7+HHreQnjeX+LcaZRBaHKZtf" +
                "wlh0GLVlEN0mPz8QvR9oO/gRFkn49ZOnNX/9jVo8Qknp77CSOCCXnWITf7vkFqSn4gmJRlHsigLTcb4k" +
                "Cs7NUQ4yPBTu4Gx3//DNOS2kaExbZIHJBdZDBUoVZR0JP0hxhZmHEol3Q/0tQdkJq7J8VUUD7uD7g8NX" +
                "318kG4TtfmyGOWnqNqJ4mNNVw72yvZBscC9s6niUczaOzs6Noz+icdaNwmoLo50un3NOuseEytZggL1i" +
                "rbG389t7klH/vBQXWMtdkWAJPCQ0ZX86m1r/tOVyHF86otombRHTs1Rr8rRHw05daRwIw4Z3I+JWnQBx" +
                "PsuVJBSN0XbsS1aL5oG+1yQzqR0FNxF212oWn3yLQrJRu/uaIFCx0F4jJBUnyRELseRXmO4nQrB3r4Lo" +
                "WpriiVClO0kBAe9aN1+ZI+YIC+KPkYR1FaFSDye1q77yBnNkmgvGDqqwexzjwgGQnIKDxQGiwJ31MN8L" +
                "lp8rBhRsOmguqVbtdE+ksml0kMymBSvPI6XHoH5+pnhmHwYSB7wXbMUv78z8amYUO039++D/+yBB+iH5" +
                "kuG/L5PRr/jPONlJxKNOk+0dMHg2+fnJO0YU/c+v+HPkfz7lz7H/+eydTzX8/PydPLsrAnwihteKa3Um" +
                "w1pdjNG0Wv93wtskjNS4RBX/KlFC+YoriPYb8eetqL4bPxq13e+4SkWztZYpvPMx82gsVWXZB5750wNd" +
                "Yof6sEizPtynOlnrgqRF1cpZioxozbKPqfc6Kiuq1dIK1nuFh41prRZdjJcWfoDuHzAGxMK78p6CsOGU" +
                "xfrSaBOz8dEA7s3ojIYRPD4JYkEaf/zFHZKRHLkdbfDxfCmKtwywVrEoswcc/VZoHA45devQaGeL02x6" +
                "EnRvo3Wkk5sd3iJ8q95vo/21f9xsfvcL1qKIRmj0R4dK97mqoQYoJLxtKRFxwYzeQcSY2pPXrq8dfaKB" +
                "/bMfAnkaRNuQeB5lgyEY/2YrDP9l9A7houvsnbcb2hVJ7ZYdzwW6xDFdgiIcJgn5mrASyQYiQgWr4RA4" +
                "xarC67SqTo1vaAFozL7JHjI3aif8mpWF+HfI7mEvhILQzXaW5O6Xe5W3125TdwKkofT9coSprtZcqEOr" +
                "1cqSDWxTUywohu26Ep0aEppMmP3joSCfU5SChc0gqtMSUT+nEoqo3Q0LBqOQMzqsKfFXEAjUk1V1SEPA" +
                "ncdYi3nSs5zDW20ZGg30oN7/Pu66B/ZqEiVEeFIjKBb02b6SxqV3q7WJplXD2FaFRcECl2VuKcO2xXzz" +
                "c/NSboVdSiyue205tQwDQvV/diLrMGSO9Dy3QdtyqSyfhvBGBPMnY5869zEsfxroEssNM6bqmGIzSbZ+" +
                "Um7oT04ozqjdPaN0Ks7fIoqaxT+fFkcuV9Po5aISkaQKKyOMFUqIWjRb537/D0WfVfJJIOSRuFdJVpYU" +
                "Gaa6onxmOCY5lOOb2eDDgB0HvvFqi9tPtvi13eJfUY51mWk+B+/nG87dSXUmHtEMCracFviO82qkSUfV" +
                "N5srlSL+zhHhfmkvNYiN2FzqAUvVCuwvTYkhusZkqSaKwIyXTAa5ILKUSRPwkUNOhEvAj6MRFHxfNF0K" +
                "Q4Yd4bNSxVArn1UGh+5Eg9CPsejbUUDI6CMAjk8uAFzLvmbAgIf1ooOhmK3wRxXqFD3mvkjZSMcD5aWC" +
                "RSTYoEbWgJ3cCeWxpMV1jkRwCBMrVcDUq8aQSOgNqaUCDZAkuXUFrZt2oFsYv0LqKFksSxGXfb/tGzFV" +
                "WUbRYOFqA+MReickI7Z3ME7VGFEgsTyPAf7RPEs7SCzaxepYGkPzDBcKxeeQo+6Ep18h1UJGJTvn2bxs" +
                "wIuwLT1yoRm1DtVx7u7W8cYUop8MtVvpgZLbOLMubtLSlUPYmnqUlenTNotFnKW1WwssDrhHSl40qDPj" +
                "zQaSkWiVxctpRW3zXBtIXb8etJmLMBMLQzGKBna3LdBXSRa3oFEuMQBBRM9Du2VNp6hbJvcmz4SvkQuw" +
                "E/NEeCCDNtbTH+5t7kTl3cAilh7VBYKN5AYM5ztbygsAgtp3ZBfRoDVa/Y5DCr6dzxPriVFWX9eNRaLM" +
                "Ao3DgVIjhgATvaXyEKFN9JbTa+Uy05qbqKBmdc8xUKAJXWggyedJXXjFa2TWs5s7AmLsdsD8PBPml1eB" +
                "n2hKrOw45bcWi9meYEFMUs30Cp5hNkp5uNSKwFYtBxmls6LKyRilt90rApjRXmJbkUwO0nuwEo8MatVO" +
                "KtwqdNdFHVEPdKHuyMdyHJjJSIlKJIV6VY74O2NSczEyntoV0UE3d7KWoWl55SSxk5cQuSCIzSwaXWZv" +
                "Atozhh1WnnQKAGNq3ULihrcr101JmMUWcNa9nUpBDYnNNzx3JxyjEK3C9InTuCgYUXzUAgbpWWa6ED82" +
                "qzYNIpMwSIizdMSUnRu5G/xp/vipjhBDB16silIRCtiRsPBnCmXGLC1yk3VqMFqYFTng7gbyWiR1MJ0A" +
                "Rz2RFIZnJR6zkvTJluCnB6mf+KrdemX9I9U8bt3GgHYDjfz7fW69RAtW8G0wSZ7eLlo9ZMdP6fs0p2iJ" +
                "hFU9b8phhYeMuWiRusqPYo5aV4XMJHRLucS2gFNsynYR03Eu9Kv8nRyiIz3VnNHRIo+UgtmlGQ1SSfeY" +
                "VjG6sbAtfFV0a8OYhWKRDW/8/IhExFPk6PHXV1vIOTMt4ZLbB8fnJ2fIpbcehFS7e/CjL2xwAlPWyY/9" +
                "L2Pcr1UkQgD+Njlul120j8ooM9p9fi0nUwCoLrp756TzqiNjPTmrBY5q3KBU+Lh4dJ4w5Dpat0jdcwS6" +
                "dSNbKKKZTBqY2/Zo1pImG+Y0b/q73c7lhb80SNq5A+5SPiqsPZGrAMJ9N2KsuPSsHjhClfiyZL5i7tJA" +
                "cueEXqXkRIcUxFQq8ry75O5Gcp24wxM9Zs+uA5a4/AY8pCzAZvxv6OegsuK8cSOMWG+u2inZkAMcuuUr" +
                "OJz0feAxVVk2c5JJym3nYi/FlZiEeZ1CtHOqUpbprICoglgnw/EGOl48HS9jNVR8/ZHJhKmoPlwzGc3x" +
                "mBYJ11vZFVR/tNEjAS9lvHB7INRXLzIKFUVCdXSXrT++RRgmH0lV33ySX/LQg9rx0VQbF19hVK6aqItJ" +
                "3Eq0u1vImCaiOlPu1Vgxjmj02eTt2jcW8rczWoELxZa3mhrNpblOGEWvTuUyz6PxEYRkmRJ5QurMSjld" +
                "rDXSZOl5lskBf4D11Os/UWXePTPocDeTJn1Jk+s87SKoEaGhQat0kg38ZkECjBc1+vkJcjDFC02iSlZP" +
                "PD+kOTkR39Hqxv0tfq5A0Nw9AURc9PIzWbawRbGVmSP15pZsy2xOYusdajTel3Nh41TMX90GUo3vI3+r" +
                "XKrvheGNdRLHVvpq5Sq19hVq8OpSzmzuLacumJ33p7kRxtDYt/cpykUAYzp2x11TZDdTsiQrjOrocpv4" +
                "lkZ53LylMb44L75PrXmHC7M+bhyFIY0s0ho5pO4cLPfdeLmY2s0w9STZ6PLsQlpJ0lHrzhI76wLaVA75" +
                "DfQi2+h4ce+BTNu2fTjZowfoe+4aOn+o6UjP1duNeOF2G9eemxnwJBCw4PTml1XvNX6d6g9gIqFm967R" +
                "nlepZdqaF+tl1laeN67rcdXEW6v39qCWQl1wrA2MilmKor+yMa2FuKlSskuXtZiK2xQOLymBZ27/qSHT" +
                "uKlk1u/pEQYQpSiZZdcybAVlaebGkE2T4Iei1LtMp+N7ugzoPm7X+TgHrp4wjq+cECd9xdJVo7bvL19+" +
                "YMy/0hJpfNSYh4btDE64BcnuQSYYtPpTmlzB1t75wpXc3+Tv835ZVP2ivHxcT774j3ryp8fpf4CVR+8B" +
                "SK5DOEcVIZ3KcTGCR2WhGL3fTqpwvBmzkgRykqCJbqIM4+NrVqTKRvqUt4ra5Rz+vP19HC/u3PxO/lnx" +
                "DCiAYJUvLlKLQtr5Oi9p4uq8XG/y+3U+ZgEi3+ah81pRBDcbiTDW5le3M83Susv1mhVP8WjtGRzwncdI" +
                "a6c6cqbtDSwyD2ueTScYkjhWruq/ZXkoMdT+8FcEBQr1I4NidZ5mgv+yzECQYIPlosGVwhtzuOBzCQ+Y" +
                "Z08TWs8QOQquxTzYpj4LsTp6ZOhZQA7jOKbUqQAJuRpM0Fi5XFQYQwJMYsVB6V0l6CDHuJ9KgKqBrTR3" +
                "pehKRZdKcFeBC0rOVtID6/Nk93g/Pr/mGc0BGMQsQNm38spW/v73kHAgbf3mFWthHaDgRu+dX6WG39jm" +
                "YD/vAe1IZfceiKljfu+6mwFM3YfbG+OT/b5IyvT+/UxB7IjPnYC7yvXjE3DGyN2jH9kYVo3kD+pxKhqO" +
                "hzDSDcgoPJuuHAP3t+a67S5t3McvBPrZqxe77sVdf2zDj6fkpPPh/3Xp/zX0/0rv3VsQ20ztwqZh2b4k" +
                "QgJUHVc7XkSWp0jO+KqYYO4H+Dyc13M93Mrrjx8g7CSg517e2Tndj4wtYcln+2I20/OD0aWXO0CzSOAC" +
                "7css6651iXPBGISnL7pcGXcboNo/Hak2Fzv3fqkeW3AAWbvcNYHfg2j/MLHEA5BqMoa2ofNd1w05h8Zj" +
                "K4+L0Wi5gJLdpMIQl02eIOp9a6TY6A/rx30aA/nUbtRUQHLgeQofJzYvtaiJzpR2b0qOM2ZBNSRBl362" +
                "6Ys5mJXkVe2u2xz+rC/+1uSp3uDOFItOA8H3HJL1V5+z1K56Za5ezSYnw/oSa5IDtTQWbkrkxlzbirGK" +
                "b6nGuVvu/0NG+tmlT3/KiNEC1F4wUIDqCQ/EfbXJfa7pbrBfi02v9Y0ll4fRd/qDtxocHr9K7A8yPPhv" +
                "dPPbFbj31he2oO6B97wET6rxSRkHc3fv4vDtgQfJ5FETJg07PSgNJtf74D8L8OnZwcHR6cXBvgf8tAkY" +
                "wbUMEV9wEV39EWIy9qmSdMIjlYxAa5AtCv0GRFf/8MwyOZJU0I8WWaKWR0GrKHy3cZGVSBwDCYkbbVoW" +
                "7M3e3sHBfoTysybK/GqPL0ZHWQ+pwF1+20mIdcPsvjg5C3ThMM87hhnK5xRW4pbdI42X2SdJ40MOE0Ts" +
                "GV1dg97ZAa9uCPjtJF+voldmVKVrOMDfRtBil61P42hVFHU82BIxBEpBSTPItTz6CYM1E3Cc53fKTvLN" +
                "PXCeZz36CdyEgfn84nkK7+2+fh128k7yh89F0F3T3IXh51DX3RDRXK0m0oi98/YRtYr8MmghGA8SjRuT" +
                "iNnk23/CJD6PzGSKxvbTAXid8xqeeH1yfhGD2km+E4C7/kNb7gtkjDqOeS8FgDinOvUkIJRmgQXpNvyM" +
                "vScVWNAz7hNKyKh3feYL2lgcUe8E8SqH5oe4UOmjBoDYaJEyk5B0NlxeXkYffKmzD/yM1T0qYqeC+Vkt" +
                "Xnh1wHrkPRofWpqM8OuYVx90X3Efn/Rzh9BWW0aXjulXYsaDKDn98Q52QK/Zw3u9zQ/H3A/VYhq5VA8r" +
                "cxhqAUcw6j7MWKYFv0/fipY65wF1/f1y9/D1m7ODne/4p+cenr7ePT6GhBnw7cH+ziNrfXj8dvf14f7g" +
                "6OTi8OR4wHY7j566l9HDgWu4C00wePHT4OD47eHZyfHRwfHFYO/73eNXBzuPnrlueyfHF2cnr/1Yz93z" +
                "N8e7L14fDC5OBrt/fXN4djBwZSYAurvz6GvX6uLwCEOcvLnYefSNYW+2w86jP0jMImSB/b3G/vt5ylhG" +
                "u/OL3bOLAf57cYApDPZOIGjPMSlQ4ElHk7eHJ6/x9/ngdPfie7Q+Pr842z08vjhH+6+MmK9Odl+3gT2N" +
                "330MyrO4YfTKOnFtnvdaq/Pq7OTN6eB49whU/urr9ssWJDT5ptXk7OTFiZsi3v6h9Raq5y8G/NvWO724" +
                "yd5+JwEXvYu6QeaXZ2gwAALH5y9Pzo4GxoSPnhqjeWKBXQ72/kJeBD+8RTsyBRoaBSNc+V95Z0RzDHN4" +
                "/PLEv5PbVSI2aOB1fDI4/Mvg/OT1G3IyWPTOrnH4iJj55GWJetyxXt+hcU151LH9iYxYjN3NND8TM+f1" +
                "uyCBFS/Q4nRhr86EkFXshToqSWJ3nK+2U8odly92XZ3hPgfwCJSyU6gB1oZ+EcA+htC+tnzTjpK3v30X" +
                "gZDKTveVLNHiVkjlT2U/DmexHzdPYLuUAK0u8dRJCHd5jnvkoUUnwvVGm0YnTaWE78DhnUsXxCfJBRgy" +
                "Gp3H+des5u9zrP9jyNiatJeV5pqxWJyq3OAZwAKGP+Btft4V3srCpa/uTsPtyiF5YqzZPDqjuKy5BDw6" +
                "lL8yxHzdF+D+gXGaXPa7nus/6v7S9T8cmPGfyv49vpHtsfe3A2iByX8DaG1JEBN8AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

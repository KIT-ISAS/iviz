/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlaceAction : IDeserializable<PlaceAction>,
		IAction<PlaceActionGoal, PlaceActionFeedback, PlaceActionResult>
    {
        [DataMember (Name = "action_goal")] public PlaceActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public PlaceActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public PlaceActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public PlaceAction()
        {
            ActionGoal = new PlaceActionGoal();
            ActionResult = new PlaceActionResult();
            ActionFeedback = new PlaceActionFeedback();
        }
        
        /// Explicit constructor.
        public PlaceAction(PlaceActionGoal ActionGoal, PlaceActionResult ActionResult, PlaceActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal PlaceAction(ref Buffer b)
        {
            ActionGoal = new PlaceActionGoal(ref b);
            ActionResult = new PlaceActionResult(ref b);
            ActionFeedback = new PlaceActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PlaceAction(ref b);
        
        PlaceAction IDeserializable<PlaceAction>.RosDeserialize(ref Buffer b) => new PlaceAction(ref b);
    
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "75889272b1933a2dc1af2de3bb645a64";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1de3PbxrX/n58CE89cSY0kO5aTJmrVGVmibbWyqEqykzTj4YDkkkJFEgwASmY697vf" +
                "33ntLkDKctqKuXd6lYwlAPs45+zZ817gfJz23WG/yvLp6zwdJyn/2R3h79Z5eHbhyvm4sqcFX8XPXzk3" +
                "6KX9G2sx1OvWwb/5p/X28vV+MslvXVZ1J+WofNrAoPXGpQNXJNf8qyXwjLOeNKYWJ8cJodfNBoIB480I" +
                "Pw6wZTWQyQWy1pPkskqng7QYJBNXpYO0SpNhDoiz0bUrdsbu1o3RKZ3M3CDhp9Vi5spddLy6zsoE/4/c" +
                "1BXpeLxI5iUaVXnSzyeT+TTrp5VLqmziav3RM5smaTJLiyrrz8dpgfZ5Mcim1HxYpBNHo+P/0v08d9O+" +
                "S06O99FmWrr+vMoA0AIj9AuXltl0hIdJa55Nq73n1KH15Oou38GlG4HufvKkuk4rAtZ9nIFhCM603Mcc" +
                "vxPkdjE2iOMwy6BMNvleF5flVoJJAIKb5f3rZBOQny+q63yKAV1ymxZZ2hs7GrgPCmDUDeq0sRWNTGDv" +
                "J9N0mtvwMmKY43OGnfpxCaeda6zZmLAv5yMQEA1nRX6bDdC0t+BB+uPMTasEzFakxaJFvWTK1pNXRGM0" +
                "Qi9eEfxOyzLvZ1iAQXKXVdetsipodF4N4s1H4saVG4JZS4FNyut8Ph7gIi8IZOGnBGt5d51hQRgJ2i7J" +
                "XVomBTFMCSSIgU54vZklQZJ0qpNhkYtbsMbdtZsmWZUAUVcS04Iv3GQGsTIeozeNWQrX3DlM7YdOeg77" +
                "AyAkfVdUKVaOIIrpq/BnA1sTkBfgYVnyQOfEhBIgG6CHSDHswbJMR44XISlnrp8Ns74gqBCUuzo6bRBp" +
                "AKAm87ICZAl2HVrt2vrRyq1L6rG8w49OPSry+aw7Bf+Ee2lVpf1rN+jmvb+7fmVPuftpDnGB9fnpQzKj" +
                "6+5Yb5Q0aC/Px3rfuWE0TTmfzfKi6pbzYkhPdUjtgbXM77qjIpvNXNG1tv18PM5KDI1mR5igKrCMFbZQ" +
                "Wl3job8RTYOpp5ByvBX8XYDKEwChKp/3rxUt6jcc52n1zQv/nPujS5eZhVHm686MUUz881xurG3VjOzg" +
                "pkMIKTAxSX/ZVkVazmJOUr6GbC2mYMhZXlZz7IR8yBuTRJJ2dtr3CbXJBEN66oZ4DkKn6EUbswVCE8Xy" +
                "YiEg/TnH6Ff+Jk9BtMPC6mwGhQ1ss7vpYAfDc786FNg4Y6B463j/4WqI/UwCmIUb6ZlNUw7pGNu0jLaV" +
                "G5cOgqJw28k0RxOSz2inu3SrNXI5tKZBfw4Jf6mqzgPtIU5nEAdgf6wBE/y1cCWwnZYEIHCBCFJsrbF1" +
                "LjCLA5Cf6BtIpY2pL8SecBTWCzxfEbnyHuRUf+y8eMPSgTqlm6RTaGRgCfpNZNS0l88rHmcgrfsYEXKG" +
                "+d0Nns7mJf0ithoIeaB58nlRMlsw/cE9D+6Xx2H2B7irYZx5GP9OzViQlK1Gl3O6JAlFv38ruBkIrEmb" +
                "mCm0FaA888LUgQ4Ben4L/rSdwHaC+qnwFBdpv+/GsNz44YcPGDGvt5bt+sFvp2gubCVwgftIFpmo28Px" +
                "ONo4t+kYyoo3unJFSZsNZh4ggqamO0xnljkwI6dJA8tdoO7lKJNcAYvuBXSimzW0ovuCTWswl0estrvD" +
                "Ip90sR3w4JEW814RwRKXrkXZB7nUtIZl85kl2+BaHkDEzLrgZ8ALR3Y0LEwhJjY7AUHrOIQdBU6AINom" +
                "R4BuD/S5SGxCJ4fVpX13EyBBnGANWn+dA/diyuOGdutCUDYXOzewBcjCE3aNNQ68Fwa5hq5X+h/9Xwv/" +
                "1y/rAT+QznDwC0UqOKZnHXi6+jnQnTTAbusBjOyvu3VYK8sqDxgO3DCbsn1dRaowqH/usi1OABCcZf2b" +
                "+YzFHCnKpErLmxLDUAf3EZsLPhf9Pc6GFdurIBirKKw5LXrFXhkamHqmRiwZ+cFQByaNR44aHgyyAt0j" +
                "OyWCs2E9vGe5t2cGhO/ph3Ilbg1qmA6g0lPIDFkKeMDaqOsfaOcJsLd7osjNYyB7Fz4KjTwkj5oQURcH" +
                "HckTYiOKXOdbeEww4clLgviyKTF0NN3jMMKnKLWK0RNt8i+JVhvjln+vFbWVOAkc9W0L4WmeLvzoBSxT" +
                "2HCknX1PdPSsxK5jwYu7Tb7vIMfegWGLMSbpDZmACDOwmTybYbD6rsJtdNl0u6PdbfGeuRXvE4KCw0Aw" +
                "H4tsBNeXewY7kk1vRW47qYbPsTIwGBhmmUyYrchFOG3tJifDZJHP4TgDB/xRaPSJDQ+Di7ddlee8xY1f" +
                "l+W5d6shyyss7oOS7fHFWeR32mIbkF7rpN5kF7Pfe4m8abW32V7RLb+3S9CyZHuMRFMqcQShYLTtYauR" +
                "d6eOnjjQbIwFGL1VXPOOz1Uj1tqZmqw37QS9U2sd6aN6h/dw0HvZOKsWtfa3/na9+eMvWIMioLq/WGEe" +
                "iG0rpCYZxBFPCxjBp0LMR+kdvA2T1fxY+5KOgdyF9iqTn/wUO3hKJi5EWrcHG/huO0z/ZfQM3tut++D5" +
                "2htYdqPRcsV9Hp0AO4SfiJAwq0UEWoKXLWaSUiXZHDjIE0gV73RnsAAL1Tt5krNLErNvcjSGhYiR8uQX" +
                "V+QswcoE3mnpu1ZbwdRgINYRHFni7Xu3qShnGCI181CXI6AKoVPXL+ba5z2EFhDMvVmiJisuCk4as1Ar" +
                "zyvUGXZJ6UB2ZUIehTXbVvDa0mKEJmKw5lE7OP7kn3mhgg6rTRIdAtEMYlWZ0gC4zcdzCyavgjxpvSR2" +
                "xvjvpWVohDjFSO2b/13ctQb2qhMFFDhmS5a0vJAJC7p3LKShBQZTlTnEtGk36NlZkU3AY7cWx2ENDuu2" +
                "ilYlVws52YTPnVJ0GKq1VV6nMydwXNKg5zYSiW8/ahRpIws7RNxdPDVxEEdqYYgtu4rxgBwM41FPIAwH" +
                "A9kfHHi30bZpc11jVI0yhXgC2Q8D/uUlJPGK4rmZjrDciGiUK1B8iyEBh4z8CaR06gcRonaGy+Mwyr3r" +
                "E/ikl3/cBnkkNNlfYD9DopC/jWcWjKFRBB9hAiYfh/qAblYEKmIwojkUMdsDacEK6Nk2/oPQotzat8nL" +
                "zg8HX+nfl+dv2hftg+d6efTj6cnZcfviYM9udM7aBy+M1JQvNO+HYdJWdL9ljQawwWGQUKC41jSEb0IL" +
                "60N7mcCPO0TN9hNHATKWBmRDmq9IbYlcHy0ytRH6bAD5Il3QDK9UfAJxBnWbr37YTn4En4E8f4thJiKz" +
                "XHXTERwOhaifF7DBZ8j5kUeIdA3ngvQhiL4baNv94eBZdPWjpzVd/Q2kjkES+itUbELTsnOsZUoWv6a9" +
                "BE7Y7CMVEtA16SCbEwhqaggHGRwybvfi8Pjk3SXgiee0ReYxaYElDyxUEdYhrcI5SzHqiJPGOSNObf6W" +
                "pB8zCOTggtTG7b5pn7x+c5Vs0th6sRVwouDkMKZ4wOmahbanue6FZJP2wpbMR8aazSPY6TxyEc1z3yzk" +
                "mhjtZPlSCXGvnhNWg/iW9ohSIXWhGe1JMsezglPhktyrslngIaYpp1KwSMTv89m2UDb5Uolqm7RBTM9S" +
                "DeTBXNFOXWocCEMNH13EkXw2wRZZUCRmiQHNBYLOS6cjaO4/RDtYw8xsbbAq8ekm7G1keZHOQKj7Q4vm" +
                "uNIBIEv8WGZyw+SYwzWyHssKlaFZ4VlyEFo6rYlUhsYKkhlaG2UASiojftoTON3HLgj3mNDGRs5Kp+/X" +
                "mNH1eOXDprSFyOJeuo8iKztYFWwUhahnY4HvC0P/i2a7z8fR1t3hBUtcUZCcNbcrSkGGTHIPBiCSLd2P" +
                "XerY9Y2XWywebPFLs8V/og2+KsSgixvhK8p0OOfAyYRukQsf4hAS2kIAtE95ZHO3tpZqnHxhE3M/t6fw" +
                "WFnTJqkfeCGS/+6asiqkDyg9jYgC+XwUYfOMTTG7vKCB3ypwbBgH+Gg2GgpFMGg6Z4YMO8JCgJwVLuYI" +
                "dbP/ELoTGDT6GRZ9P0qMGH14gLPOFQYHPuABxIOzyXxCYfIJuIz+tPBwCVVX3TmEDgPkPrlspKNUeyHD" +
                "wnaxUSNPVjSmGm/YIygPAi1uM3cXGTZCFcmwNRx59i42aVIE8XsIIS60ymiLK2goV0CMXyJDnczmBZv6" +
                "u37b16wAXkZWFlqnhAGMRyiyRmTE9g6BFXGkZZDYF4kH/IMlSGWFiGXVzmq2xFxYIBReQI6iz53j0iVZ" +
                "IfGgjEoc7JbIRGDJIMK24xTIJ6oafCBAyhpeIXsakds4s8rvUE9Y1tbUgyxMnzZZLOIstiE5wwLu4ZK0" +
                "ORf6oTxhITb0LpulCq4Go6XNC2mwTU6QVHVMWZixMheIooklAMFxtmS2AI0yTmUzIGxa2LJqYQga7jFf" +
                "w3rdbUnNIQHc5Ulr64l5xbWs70Th3cAiMp0tEPx7nTCE7xvKCwMEl1XJzqJBKqtC4RsqzeAaD+J2xBPj" +
                "PL/RhACF3qvaIpHMAo1DvsCIwYOx3hJ5CGMJvUlsQGZgBXrzyrv00GzLe44MWJoeSOVj9kC5drSkWrX7" +
                "2U3g9uzWpgQUqiTno+vAT2RKLO044bcGi9megCEEPppInV/P9dN52FUrLAeeRfNIpJZZAMUyRugNIHnq" +
                "RjoRbVky6Ugo9xtPUKmDBcfWSZlbme6yqH3SA6tAV/KhyoksUjjxnAUgvUozPNNAiHgPPJ/YFST7a34G" +
                "G7v8SCWxykuIXBDEMItmZ+xNQIdspuaihisFgDG1bCEOIceZLM1HimZQiy3ALHsb+9EVVM3FT7YZSr8p" +
                "OXUEZJ6pxr3LFR7xrEB6ytii1g9c6MotG5GcvLGrqHzYlJ3OvHr48+zpc5khHh1wzbAMIkIxdiQsjNSC" +
                "MdU5K7KqBqOFWZIDMk7QIqmOqQI8x5xSeYfbVLL2bJvhkzzZM5oJYUJj+3j9I9UM6WCKCCB1qV1XfAm/" +
                "z60Xa8ESDiuQpORc3ujBO35MDm0dRXNNlvW8KYclHjLmIotUa8xy1FqiLkwd76ZyiW0BVWzCdhHTES4U" +
                "EzT6FawjPdXU6GiQJ8kQZRUD41mdVNw9plUMbixskSMl3uDC92b6n9fFovLe+PkhOUieI6qEX19tI0py" +
                "kJgjftk+u+xcIPrTuBGCQ3rjBx+KU4HJ61QrIPiPM+4bBbYhzohCDj6hIOlPvx4+C1IimAVOMS9ty5fq" +
                "XvIDX6jL7UDkIaqRoXOHQsvhOB3pZmROZe2oEQYpWUFx5rzgelgtn+McNg3reZVjhqXsMW+fa+GFdiKW" +
                "SnxV9LRLUcBfAQfXYxnG/4V+OioV+pZWpEkd2VzQgHCySQRWHivh4ZCxDRO9dG6iW4EABQeRgo6rSGnM" +
                "2xSyhFC1klKp2r3NinyKeFslyNB8XZkvRsdvagnw3H4CmYCKCOB7kJGEuImt6XzSAzOQgSxkLv9gs0cS" +
                "ZeyGWIcSkXELVqRUs++fa9CVqY7ubDMMFvD7sz5yXWC5YTaiEmYxHCNUuzar4kyrxvJpGLdidaILGdOE" +
                "ZXVKFlYsiftkZRjygi3Vty+n/wMXsvFoYUeJumknzCIHgmiZp9H8yNhQJJd4gkPxBam9NJlC0jFLT50b" +
                "kHbDsJ56u89Ee6zGDErDFzzF9CWa3GbpKoIaEWoiu0yHrus3C6oFSs5ZKn4MHGw/WJ4UK+QSCHY1UB5K" +
                "iPiOXCcTmXeWQzH/ggciWCYzqoSVMne/RbGVqbbU63felm5KxKYdw3Hd4XzKbJyyvSXbANYSxvWJjiUu" +
                "lefM8MY6ibKVPKpxFKurCQgtgVQ6qUJuREqYTb2qXjUm5ZX11FIQ4jrDACpisU5RzgIY6PAphXjPsyiu" +
                "168QWWHFieOENahc64L+hmcB1cy3u3Jb6WODxn43MBRWF51A6weVrfPIGNzIQnuRB8R2CbcHR83GZBFw" +
                "3GWYbK5yJUIOnnP3DZeIax5JmqpfhKDzMPuIKj85nuVLrWixGW3b9v6gC1gIUH9sHcqDI7v/lm/7qnzf" +
                "vqvtaTNjPPY8Z4TedFS2TnF1LheAhGOb+qzWvuynFGGn1pf0p7Xl+2yTqM+qNZZw2gO8/hYXHbIFDKNl" +
                "knLxZozWjP0izmqSj5SP2U4Pp9mEwBPdf5zdi0rk+VGrw5OBKHlBJUlyNkmGsgRBbcq6SfB9XsBkv6N/" +
                "OSbCmleMQFpRBCfYaKozksWwYNDDL2LBQUERNk+aDFKCF9TyEk5nEYHwrRgfj78Bw865N3i/dGYk2ki8" +
                "zyBHd+HbxZVSHEUjB4JLoOSEEHFI2VIzNtRdybaVUim/bfmJz9UshAkm0BgZOd7HnVfsr4VoP+Uda0O/" +
                "pbZoF03B3buDfNhtTOaZdYlHEUq0Z7ZYzP9aasM02GpZf7/1hOnovIIdigu7z5+LWQs7hZO8FJmgw7Vm" +
                "uMtJYDtqyhaXodrLB7SBNg0eNORwEEzrsUNscUVjyeGmylr7+0iPu/39SCxr2fF8BlXMpmglwNfPXK2D" +
                "+1czYESp1G8BZr/rfIx4pxWoIqjRLzKNtjAHCeJayAPn6+e57J0CSw76yAagWl7VRr4TlzBIMeFm4SjK" +
                "Q/dRZl5kiGAhkLsVx3h6C4rQJ7+rbTPTajZKSvmxZLK1HZrqOZrFctOnlM1PJk9h7tP2DF3kNE1wgFHD" +
                "S3tMXRgd4IyzAGdbMCwpfBdwIYONT6oSt8HnrGSHIkVB8oDVqRBC8nt1ib10BNjmY6qVdl4X7MWmJ9XC" +
                "NNuQawDCYPPYMkpdDG8j62rRUT65RHMKGk0BWCK5waIsDcKH9Qy0F83A0UBCnSLvdQozMFzCTbhyda5s" +
                "Zk49cydtz2kOzKJe6W7yPRnKVIsttdEqQhmLaU4OnaxP41w3K7xtrqvmYLDTaiDfnPUj2X4L5UainmDT" +
                "PP1owSqrKBI6ldkvVJSC6iKn40S7hvU41avoaWjPA+FYdCAOW2QGtNTS9MmtkhVEQqaZA/YH+Jh/lk+O" +
                "LR8cWywfD1uDQFlWO8DqYuk8lcgA4h5dXKaFZ7GBGxVOyonoOMAgx7py7JlsORPXEuXUQ7phPmHkZp4A" +
                "7G6Fy5y8LBcl3ICoiolZUw8FkLqXPgjDhf3PBmCus99AKLGQohwVxedqWRu4ljCjoCpUWEBjYdp3F8ev" +
                "WKbtkQLfRJXbAv+ndxayQ/gfkRF+qOH8+I0OMXRCSDFkpRSKwa0/x6jSwkbDhuZCCQgjOpwPhGsw/L8Y" +
                "W68Yu6NzO9efLcas+f8lMXafFIuPId/jD3Ipk3f+Go3uoJeoAf1uPPueyYSHQq/1HG3yUBslGymhIFZ8" +
                "8gCJneaJrbJx/qnlj2nFJ+ui0h07UbQmJJnaiqBJpjIYWPUzl70iv5G8Dp2tQoARAhMCkWQVcg2co6fd" +
                "Bi4xJLVJuNZ268FO+GbF+kmNRuPwb+kAPO9ZQpBjxZ+FIg8WLsVcXoefe4+PpnKtcdenCb2Xw5s8ldBM" +
                "OADEsoYcwlXFYMunjLUS0Yq3qUiFS9UtT7EEm54TilxQasYTPkGwU0PkvFwcvqchp8jkcPddVBxY6pAC" +
                "N+Z4Plk1nk9tsaHu0fCEkAkGrSahZFALtKkTxI6+auTYVTSnmlz3KHTP74ugvOLCh4V3PGACBsf6C0jR" +
                "RRzdDh2CZJWXT0ioQULCdjJLoGIHgk8Is0NTq5mxl5747JkeEOYjl5TPDahQmlZOS2rm3r+fhBL3HGeV" +
                "6Lc/Zrzly3x4DiRBSUehWoFmQ0pO3nilISmdmJaPXo6k8Y2H3qgycARd820qkpczLd1YDIRuhsnNNL+b" +
                "/gZ5vOW9eKiqUo91E2l84MPsXjmesbJGFBxvtR5CwE3mHjsi/hZznyBes/TaG1tnOs3ATEHevtXtMXX8" +
                "7tEIIvmRI6lm1XpYvn9FI8jhBys2tIiSHe6hnb9s8dielW4Z86IWk68sT4kCX0aAy3vPEH3uoaDPPeND" +
                "pcr1UevncB48WvMksTNS7CdQrMlRqa5KM4+dFB9ZEN0X+fgzCFIXRq//CtF5mqJ2RogCuowIz3QPkvQs" +
                "QHc40Aq3IACW88ic+IoagUdvs3xewlZ0qB8AfJZf4qQKl2j0FrB1Do+P6QAG+YVc/xcP4otuouwp/q6Q" +
                "78e4m4iYFws6cTqy0GiFzVw2eCIbbMlMF+23nfdtqvQHTjMqbWEvz7/5QPxCFawMtJrQD+HqM9fcyfDE" +
                "KgQkz8/bZ8d0uIWFcJhz9XQ8y7bkFpnz7ZQY4c91OrZuZurbOWhOPbIZT24kVXmg8JlOy+dcR1WXplqS" +
                "JCAybfYIwA4Kf/xZdoxJ9UkwVq1hbo8fSyg+LFRaT371T9J5+ef20RW9Q/HXd9YfIs7Rp/MAdgSMXi1J" +
                "Ck8FGcQYF7DBzIdVwKEMshixhFI+P5LQsnev9CwjEqNPGkbFjfOxy3iGfb4j/UOckV+hwOwCiYWCpJ4J" +
                "e4zi60R6MSiqYDm+8OfLztlTSvdq0OHHw7eniQyAWKNnYYhZvwGidyuQoDaqNI+HmULZTdpsNVCEcmnR" +
                "eR/5Qs5xduP2ky/+sUEU3tjfOCLL5vjlxnayUeR5hTvXVTXbf/qUTjSOQe1q47+/EBQlbQ5LkCMeU8tb" +
                "8uqpdcOvFgpUkNNkG+iUSTXQjXP6Ys3hGFtVKgat+G8Fv1LAX4ho74o4fim84V8tRvteZ5ZYATHXHHQi" +
                "ESeRI35tJ0WSFFkOefMw+4knAN8jEuBekwT7X3/37QtpQapXiqTQbhniDZ3p8q+nCPHDRKA4v1+n2sSX" +
                "P4/fWAsZm6dKNu5G5d43coeyKvvJ1y/2nvMlWhfUAOZzfqctoPaRSRw0bpOFQojYBJYgkqfIQc/H9JyL" +
                "OKp8tmEMDdZ+/ENJrDtXRjAlwgeRLTo46GivyNOPyZdkon+Z9H/BPwOufuMKkv0DLI4b/vSM3kvW85df" +
                "0WXfXz6ny4G/3PsQ3hj24gPfW3Nso/FOmhABiMOmrMCXXkUjVtquf+XqEzMpllr2rzPwQGjYTEqETJ69" +
                "/ZSGQas/psl14YYHX+iWuMtust0iL3fzYvS0Gn7xp2r4x6fpn8CG/RsMxCG9Szj05LgP8j7ixLa6JCC4" +
                "8icS+EuxLOXCOriJeC++4N3OOVIjudvy1Aw0W4v7v7I4QsWZHUMEBWBf+JfWScUVt/PuJTfRwJ05HZDx" +
                "t9mAPHt6moXO95ZqIEKIXULHu8vFRMz1bX0tbO1NevFsTQza9MxDJIclVxzAbxY4cDwca+7GQ0xJMJZ6" +
                "cLxRmSXEkPosn+YKFOJDM2oULuNpJYrQzSBIqFGDYwM6C4U3p6iJnXK9rmUaSaGKjvevm7wH8lC7548F" +
                "Lc8ehQqsQh7z+JeKEioAgtPbDEZkQdurNNjGBHZsACOZfZ2gA4dZnrNzVoOWm0smXKmoZ3v0BcAMktaS" +
                "SUBpmhyeHcf2pWc0HaAbswBlx5ce2cqvfw8xB1Icv14mENYBrkkfeSJeCymMGxgOdrkGsKOSptaT6C24" +
                "GkhbdYxTS57C27LiyJt/447VRa0HBa6z+lwEqA7rQQS0WOvxwY9qsOoBF6gIQkVy/RBGsgEpxkdNl8I0" +
                "tuFtu3MbfeU9j37x+uWhPnjsV+z7+fy78gr/18j/1fN/pWuvpuTaNambqxc1NYO42I4ry5Ouoso8lpzx" +
                "G+tCwCWMT8ZzS3voysvF9xB2/DJTffhofvQn5ubo4t4xlxVSZSyMLgm+QrNwYTfaI8GxOrAYH87EJJQI" +
                "WRWW04oWffn0cqRJD7P4ul3JB+iA9E7cVQj8FkT7p4nFJW1cJkVnTaDztesm51Qpg/Q07/fnMyjZLVIY" +
                "XNLKd9Jpf2Gk2NztVU93yRjIxlYVJgNxQGIMTyo2LyVAQTFt6V6XHBd0LFFKtqnkebLlT1fTMUEUcVu3" +
                "Kep9/UuF5TQjd6NBFA14dRkk6y/eDZKu8opCKS/gl4vsci0+O7xkLNwVGYWRuG1Jtdzfkhqn3bLez5bI" +
                "x1Ue/nAJFZLMKTRCv+QV/vpdFv0gy+NAfS8krcbXVPRAlDyTC4o2npy9hsssP3A28W9UtXANrl34E+az" +
                "Iqf8S/Cgah+Q0DEPj65O3rf9kHSKqz4mGXQSwABz9xzHsz9n4POLdvvt+VX72A/8vD4w4hQOASxwD0VO" +
                "+s5/ISJJh/Q2HjqZI4cPoiMxAdDlH4olECcSFeQTJXZikuI+ZXSsYfPKFRNw+5g+HlNh9+lxtHdHR+32" +
                "cQTyXh1k+kaHf6NhOe8TFWh3L1YS4r5pDl92LgJdaJoXK6bpIdiy4jzH6pkGSDA/RBpfOztMszGl5+4B" +
                "76JNIdUA30Hy9TJ4hSMVeg8H+Chhg122H4bRjjNX8WRzxA5I+tnbi/W8y30IKOf5nXKQfLMGzvOsR/4B" +
                "bcLAfH7xPIWPDk9Pw04+SH7/uQBqifEqCD+Huhq5ra9WHejpMKOsQEhteynAkMCfiZGI2eTbfwMSn0dm" +
                "Yora9pMJqBT5Hp447VxexUMdJN/xgAh4KzH0e0MUxEWOmQdRZzr1JKBR6iediW69z9h7nOOBwhKf5g4J" +
                "mlUf9YEWZgfUOz8UYq1/dgdljaL42TaLFBkf1XG9+WgUPliRVO4jcgzrUsCqelstSUK36aVAR2RwyPuB" +
                "ED8f0Bvzoor5KNuvHzGQpyHhLyViUaMRBQJCKUR4JlXrdly2/lGexid5og/c1D9s8/iEapJGT73RqXiK" +
                "qoAJqI6l5+gVCXDx5Ckrpkt6nZlcvzo8OX130T74jn5aevP89PDsDEKlS0/bxwc71vrk7P3h6clx923n" +
                "6qRz1qV2BzvP9WF0s6sNDyH8uy9/7LbP3p9cdM7ets+uukdvDs9etw929rTbUefs6qJz6ud6offfnR2+" +
                "PG13rzrdw7++O7lod/WINwY9PNj5WltdnbzFFJ13Vwc73xj0Zi4c7PyewxPhQKwvw/UfyBJ+MtpdXh1e" +
                "XHXx71UbKHSPOpCtl0AKFHi2osn7k84pfl92zw+v3qD12eXVxeHJ2dUl2iOLLB1edw5Pm4M9j599apS9" +
                "uGH0yDrR2rxoNVbn9UXn3Xn37PAtqPzV182HjZHQ5JtGk4vOy46iiKe/bzyFtvmLDf5t45nkUO3pdxxb" +
                "kdLpGplfXaBBFwCcXb7qXLztGhPuPDdG88QCu7SP/kK8CH54j3bEFGhoFIxgpX/5mRFNGebk7FXHP+N3" +
                "cUZsUIPrrNM9+Uv3snP6jjgZLPrV2s6URd/ieahuSaphq/s71Krqo47NE13REGv6fM89kNVPuNg5bjIy" +
                "NcK1Mvdjb8sI3zTg0zArPtFjH7pZUQe16vMtenplB5Sy4sUw1qYcYLGzO80q+y073SMtwgmAaAiuEoRX" +
                "MZ4PCAut0Y6+C1M+DZ/zeVr/iI9G/8nQ8hXa+qpVveVHiz4qJEX/tU6SNbFmVGVlKY34Y0Q82O49pdj3" +
                "rOZv82WoTwFja9JcVrLQjMXirOQmveoyh62P8bY+r+LcVxJoJU4aCp1DnsRYs/7aOoHlnpr16LtOS1OQ" +
                "yRd441+bp85la/801GR1BMY+X/tPxWD8t2/X/dFbD3X4NCMfbfof/nmTT9R3AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

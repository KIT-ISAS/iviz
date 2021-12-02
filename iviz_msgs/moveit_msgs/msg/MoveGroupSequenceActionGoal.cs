/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupSequenceActionGoal : IDeserializable<MoveGroupSequenceActionGoal>, IActionGoal<MoveGroupSequenceGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public MoveGroupSequenceGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public MoveGroupSequenceActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new MoveGroupSequenceGoal();
        }
        
        /// Explicit constructor.
        public MoveGroupSequenceActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, MoveGroupSequenceGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal MoveGroupSequenceActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new MoveGroupSequenceGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupSequenceActionGoal(ref b);
        
        MoveGroupSequenceActionGoal IDeserializable<MoveGroupSequenceActionGoal>.RosDeserialize(ref Buffer b) => new MoveGroupSequenceActionGoal(ref b);
    
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "33db6638fb44f932dc55788fa9d72325";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1d+3Mbx5H+HX/FllV1JGMakiUnlWOiVMmibCtlPSIqfsSlQi2BBbghsAvvLgjRV/e/" +
                "3/f1Y2Z2AVr2XcjcVc5xhdjdmZ6enp5+z/irIp8VTXYhf0b5tCvralmeT1btor3/ZZ0vn59mC/yZlLPR" +
                "i/qq+LKpN+uz4sdNUU0Lfpevo8f/4H9GL86+PMnabqaIfKXo3cvOurya5c0sWxVdPsu7PJvXwL5cXBTN" +
                "J8viqliiU75aF7NMvnbX66Ido+Pbi7LN8O+iqIomXy6vs02LRl2dTevValOV07wrsq5cFb3+6FlWWZ6t" +
                "86Yrp5tl3qB93czKis3nTb4qCB3/tkaT7PnpCdpUbTHddCUQugaEaVPkbVkt8DEbbcqqe/SQHUb33m7r" +
                "T/BYLLAGYfCsu8g7Ilu8XzdFSzzz9gRj/EYnNwZsEKfAKLM2O5R3Ezy2RxkGAQrFup5eZIfA/PV1d1FX" +
                "AFhkV3lT5ufLgoCnoACgHrDTwVECmWifZFVe1Q5eIcYxfgnYKsDlnD65wJotOft2swAB0XDd1FflDE3P" +
                "rwXIdFkWVZeB8Zq8uR6xlw45uvcFaYxG6CUrgr9529bTEgswy7ZldzFqu4bQZTXIp7fEjXs3h7CWIZu1" +
                "F/VmOcND3RBl5acMa7m9KLEgMglul2ybt1lDhmkxCTLQc1lvYUmQJK9sMCxycwXW2F4UVVZ2GSZatGRa" +
                "8EWxWncZCI7ehNkq12wLDB1AZ+cF9gdQyKZF0+VYOWKU0tfwL2e+JiAv0MOy1JHO2bwoZuf59BKYzdAD" +
                "TLlZdtiDbZsvClmErF0X03JeTnWChkE7NujcINoASK02bQfMMuw6tBr7+nHlbmnpVpBcZafrtleKjSDc" +
                "SHB/90bx93kAsdfLvKqA5qs124GF7XlS64s7wXwPiqDs1yUwredoKzzjmDnyuj55hk1azSZNPis3rYjN" +
                "Ip9eCPPpTsaqrim3CIrPAgfMBx6zV/i2LfGEtSveU77Jxgcv1xX4yfAaD0j5vCtWP7wD9xar9m7Wdzi6" +
                "8TjnE2jC+ct+JF5xB4Z5p/QLbdtisaKcMgL5jI9JgnLODSrbL8/aell2kGS2JE4S8lDCWbL5al0X0LTb" +
                "FtjlQY8IwY5lIFvYFbYlSN+u6hpvZxAXxBJ7u2xUNfloOl+Fa+ttKLMplswHk1lhT8u3qnjfiTbEi2OK" +
                "HEzlATZ5Dm6vDE0MOB7Nl3Xe/e6zHkPd3comRHS9TilZgmTQWBRyuoyzYl5WpRCOC5iH5YRki0QFiMDn" +
                "Rod606034IYuiEQu1Ouc6qUrGt07bLitm8t2nWNgEb3pnjFNwBZQ/y2AjL711gmkAGGyDi9HaubA3qDG" +
                "7Ch+N2uoPtgx2fN5EOR/r2E3tD4Q2II2B8ZpCgpboLOuW5k91h6YEWOwv/DApmmEi6tC+QvbOjZWgABB" +
                "Tis6WIZNMXpTn9fdmeACjJpuIni58kDntqQkENEvnyKRVvUMRhlVFrcf3o6zZ5A7WbEsbC/RTEDDvGnA" +
                "37JqooRMjyzI0TKMvCC/Ti9KWHrEEZtOZgTEuyYXguhaJ/aaggB44J53ZUsVNHoae0A2iYGbAOHEXtZG" +
                "fJAyr64xSXyBLKyxKrLSOYQqFSp/N/VsM6VmixJYJeVVWS/FsBQqp3ge6t5br5em0ilmZRAsSlV32d+p" +
                "JaGJ9d1RirIMPkSYJEoHMJEORU1Gwtu/F1MYJpBJBKykuB69De9T+LH1vlEqWmQmUFKdQyaracQIo0Jk" +
                "SEPuT1f9x2RNrnEu9kO/r6sWWkFuE9g3sep2B19QjfPBNgOAJZaWw8XPeg2jn3RwuNJzQlgB7mZ1jsaE" +
                "TDs8ghC1APFH6q4gGGCwZWcXdQMmBxnr5cbEiKPfwPpuxKxTOx+AJ8FYyDtI9XUk5Sp/X642qyxf1RtT" +
                "LLTN9lCWzLJc1lt1WnwziTVupvpREMzWMA4rVh/lCix0MZVhXtYmydR+EZcIsmMzFW5JUBPaZnBkYAt2" +
                "5FW1AfPpFFuYVAWDjLPsiSF3lS8hZWW7AbXDB8efvsPXtzcBvN4DLjEVbYM1lD8mSgh5Ra7mzjEPogSB" +
                "aL6AZ+FyAZxNECODE6FAKADZEbKdQpJAy4a4wvBt8moBjA/LFRcur7rlNcVi2YrzN11uaBSfFyKOIRBB" +
                "/QfjB0eqmXUc4XH9ZJpH+FtIwTX9dPxAYEG8K6EPy3ExPr6JIgDY/5IS5yjqXzSaeKdJq0s7UYx6bdLu" +
                "w3Z3obf3aD7X3O5CBM2dCyFBs6gQKRDmG4h+7jJ8FXmGfS/eJ/Yf9sshrNv6/RHZxQWAM01/3xCtNBxg" +
                "oOHWXauTFYw/bo1VfV5CrVGZiAVlmkoBw+bbFsvlmPvqVLSWcoT4q2jcFHOoTlpyrgqBIibaYDe3o37c" +
                "xaVBCWGBVddGLuXQ7VhteGNx5zPFQxSyyEr47KNFUYNqkNtC+W9EiD8i4IkCHQqef/xQYDgf6nZYa++4" +
                "vqaQvkAdhgVZ6Uq+kSvm8Jcz4cFxFox9WXXatzLN0JMaqmwoSszyaMSJPqZFOKshC6CdAWOVX9JCosNE" +
                "7Q1FDllGzVm1SxVjeI0uh8V4gb0u3CWt1Gin285YFDzmplxADElPDLQKnfPMJgdZM3+oCkZw1sGwLvTF" +
                "687kAkXddb2BzYA54EdjITDRXY6XeGxdXR+T9Q1En6CvReT4xsSe7MCj4HKXKO/Dr+vw66c7kCLRBr1R" +
                "eJRVpF9+Dlnf592OawjBqzZbNI/pubVuQ9ALgIC8bEdc27rRwf/Mj2oAS7toAMsXC77QQbvIr4J+KrLT" +
                "V1+o5Rk0mtjTKegXbIt2yRDSfTKr55PBYE+6DtYvoEzr5bJsOc/6nJYa1Ffu37DgLRZUZkGTKNDgaOT9" +
                "n3r3V9IbJrD3xt61TxODzHG/WOaIz8D9Q5SU7AtmNp+Cmm8KPo5Wkqg5bCWGnyAjuZnm8x2hIhiqYyL9" +
                "E5m8qluo4CyoYw3xegxRXAWf6nk9o0Vy6PigIU1nRnWXBWz/PY0xEBY8N9Y6OYGsKk5OEgfHQmbic0lY" +
                "ygIoXcJyR6PzuqYzMuHkbkvQ7WfAhFJ52ALCfhf1EgFb3/SwWqZNya2v8UlMTSZu+hVyBe6w7J0GSw76" +
                "6AagfIxBTe3EQJGZIodNQZ3L9w38nrLldptC82JgUYK0yBirRkg53WauYRxKPhMz5+g4No32z7Dp/VYa" +
                "30eAm9szdinmWKsuxMvVmwpWowF4uWLvlzCcODFxPvUL9b+EIMltM0h+3aGwKCkPxDlQQqiWDpu43Rvb" +
                "9fHM/7VALNgLce4xzeOdNppcYOzMl1GEh24j78ohZAUlDI4xdRpDAdgiBiiiLI/CRzxqeJMcweN9Ysz0" +
                "KSzIiFpMvWwaS9Rw0snaY+7kMPcYxtm3VGrUb6pvTITKLKoaAG19BgF7wlodi66awo3BTuWs43JqJIB+" +
                "0rVxI6mns9G1TeZuARClnNOpLX+CrMeUGakUOMmukRiPGHwa5g48EOPdkTjibjrStJZoGNAOlRWEWhxa" +
                "cupgQqIK/5iCxKNvhOSVM3zySilwFwJlV+1gVm/cBlIdimVXGUDuscUVWgQWmxULWFbCeDSxZjXWle4X" +
                "JEq9dXENkmym3Ybu1zyL4ykjqykG0sOOZ/yN4Rbfn8y8tdcwQMRO1fg0+E9Mo2AuaZ9F0cX9T38yBGou" +
                "IZRESGXTC1gJ4+wLboX3SH4sGbkVix+qwoQFI7lV9tc3p1+ITHtEBX4Ioxgu4XW+ZUpLw4QwgPUjOVji" +
                "4TFVl2KnhMSfpgQU7csd3fsu1iRbODRsaKR9JIHDrAsm3MPh/8XY3YqxLf24i18sxrz5/yUxdpMUUwOU" +
                "3duBw/fWWRitAjvvNNpCL7EB/w6+fStkwkel1924iwHrPQ6jaPUgVkKOZFvv5PzbgU85Grn7m/h/o79s" +
                "0KGpKADcS7ubScaB97nFkOtII7uM702ETz9GrEmHD3p+/mt7RwsonGTTcqnbRuOxP5/zpr5k6rwSX7yl" +
                "T0S/gHIYUT/JD0h0ZxwW0JrEZ2t3N7PTPbFn1bAUujxxcsfQ54ySUR5xgjQnf9kUBVh8VFfgLiKBN/if" +
                "JrMHb0NEPXhwlsiel+/dXdH4FEhFZ9eD+/ztsX2hYy8viLjnhmmqixylQUImRrNiwmoHN7XHU/eazWTA" +
                "ewi5WKWGLBfnKiCR/FD3eYzIjkaHof0ZvzGn+t4+eO5iqxMSppGkFjgAs1d9QilQD+2ZgydBjJiZDON5" +
                "wIBhiSQy2tUbYTwM7PHrTwJiioaEUlHJNLtOko1Jh6g1BNhEwyi08WLaUrES5wiGF3Nmw/oqrIbYiiHr" +
                "rghriG5FGGEqAG3RNQg1JrthPmvIb7qEuc04G/I8shEMioXtfYyqmFL/IinG0RpEyaVMy+K8NjCXjxU9" +
                "FrtJcmPRM49pNBCD2E1sCF+UbYEatWCxDBYDYal5dlnV2+AdWPu72JO7e/GJmQESI5wJaUJQx2162TP7" +
                "o9jgeJumEfBQuEdgYfVYf/Mcsaik6kf7+TqjZE+ZgpGM81wLUoQ6Yffo3wl95IXm+HUuOoW3hEAwMS3q" +
                "0TKTtpLG2rXmfM9qNySIVDJwp+wLiqdBPSfAGSo/ZmGYKUMkKyB4BWKJxFE8pdVr/0TvMDYbRmPb3nfy" +
                "lRYkvCjaiz5UvkHblX7YC4ffIojPuTk8zco4GiokzCpIk7bZuUVSpZn7KGpExDIUrNhsJmsB6cohjlLc" +
                "WLUhE5GRbpgkv0Xsnszo6yWMoVQPGRqJG0rUO2kEHkXOfdPCDi7ew1Ig+ojgqTIVgTMenV/Djntyevr4" +
                "AYd5I0K1N9K8qelWwuiursqmrqRQgfEhSAgk71B80KAAUreChH07bGaFkKQuZ0c60ptnL1598+zxpzKn" +
                "9Zpyih6sc7P5vCZYBWlzDz40V09GaCefJ1YhTvL162cvTx8/NCEcx9w/nIyC6oNia5xvSy1ZERQswK2y" +
                "dXM3xkv5lsW8UxeFLjKkGRLjpBVI6xIjSlNEJkHJmaIotHlEBF9pkl5zH4CJRxqg3tBy+LdnQ39YqIzu" +
                "/ep/slef//nZ07cs/P31ne0fEufpz+c4RGhK9G8uCs8EGcQYAxV0YWAVtIO8alcvNGweXEcN4YJPGCXv" +
                "GRWXRYjLpiOcyBvtH2OoknITdoHEqrLZuQt7QHGAs/MUFVOwEjv589mrl/dZa2EBle+fvPiaMSTWnmVP" +
                "AgtDzIYNkOTiKKidKjFopErdFQpKkMRqYPR1Z9FlH4lDX9eXsFcui5Pso/84IIUPTg6e0rI5/fzgODto" +
                "UIaHNxddtz65fx/uR74EtbuD//xIp8gKCKmck2hOZZJRV8+sGy5OQgVajmV3gE6sZsMuuCwKqwafL7FV" +
                "kY2Gi+P1fXv4lckMJaLnFk8/V94QIJwV972NrHEQMpdVhFlUTGrNGSWzyUo4X8CcZIEA8o4kwLshCU5+" +
                "+++//0xbUPVqKhXtdjE+sJHO/vI10hcwEZjDCOvUG/jsx+VX3kJhy1DZwXbRPvqdvmHG6CT77WePHsoj" +
                "WjdsAPO53loLqH1U3c0Gr2mhcCI+gCe/9CsKTzZLfpf0aVevD5yhwdq3Fau9yVqI5QaSrW/X5LTjbHoN" +
                "01qMNrBbkVm0yb0csIWnZ8BWHmWChXPuJgCAUeBTpctOVMP5wTH+hxAAzyz8Pvv81XdQY/r77PVXz948" +
                "g2rRx6fff/385emzNxDl9uLVy2ePP/Pd7vJJtAxxslZqpblIQHYDboXlZGPTGB6PLUIdA9L3RD/tkDQ7" +
                "0cCfFLExlejlhWxLcr13SXUQ+xyocpO8p/mEmLigqt7Dd8fZ9xrL/VuKM4ksDlNRLWAsesXtQAbRbQrz" +
                "A9HHkbaT72CRxKfvA6359Ddq8QQlpb9hJXFALjvFJv5acgvSU/GERKMotqJArwg2N0c5yPFQuJM3T06f" +
                "//WMFlIypi+ywOQC6/kapYqyjoQfpLjCzUOJxNtQf8tQdsKqrFBV0YM7+erZ8y+/epsdErY9HMU5aeo2" +
                "oXic00XPvfK9kB1yLxzpeJRzPo7OzsbRh2Scm0ZhtUWvmtqdk/1jQmVrMMA/sdY42PnDPcmov9Vxa7kr" +
                "EiyRh4Sm7E9nU+ufji3H8bER1TfpgJiBpQaTpz0ad+pO40gYNrwdEbfrBIjz2ewkoWiMDmNfslo0D/S7" +
                "JplJ7SS4ibC7VrOE5FsSkk3a3dUEgYqH9nohqTRJjliIJ7/idD8Qgr19FUTX0hVPgirdSQoIeNe6+ZoS" +
                "MUdYEH9IJKxVhEo9nNSuhsobzJFpLhg7qMIecYy3BkByCgaLAySBO+/hvhcsPysGFGz20FxSrdrpjkjl" +
                "09hDMp8WrLyAlJ4I/OGR4lm8n0gc8E6wFb98b+ZXM6PYaerfR/8/BAny99nHDP99nE1/wv/NsseZeNR5" +
                "dvIYDF7Mf3jwjhHF8PgpH6fh8SEfZ+Hx0buQavjhs3fy7rYI8IEY3iCutTcZNujijKbV+v8kvF3CSI1L" +
                "UvGvEiWWr1hBdNiIPxwn9d146NV2v+Mq1f3WWqbwLsTMk7FUlenxMD3bKHZoCIv068NDqpO1LkhatIOc" +
                "pciIwSzHmPpoT2VFu1tawXqv+LI3rd2ii9nGww/Q/RPGgFh419xREDaesri5NHp5w0G/5IyGEzw9CeJB" +
                "mnD8xQ7JSI7cjzaEeL4UxXsGWKtYlNkjjmEr9A6HvLZ16LXzxek3fRV1b691opP7Hb5B+Fa93177q/C6" +
                "3/z2F2xAEY3Q6MMelR5yVecaoJDwtqdExAVzekcR42pPPltfP/pEA/uHMATyNIi2IfE8LSbnYPztcRz+" +
                "4+QbwkVXxbtgNwwrkoYt97wX6BLHtARFPEwS8zVxJbJDRIRqVsMhcIpVhdfpVZ0a39AC0JR9s6fI3Kid" +
                "8FPR1Hb8EFZAGwtCj4ZZkttf7l3evnGb2gmQntIPyxGnultzoQ6tVitLNnBITbGgGLbbl+jUkNB8zuwf" +
                "DwWFnKIULBxFUZ03iPqZSqiTdlsWDCYhZ3S4ocRfQSBQT1bVIR0BO49xI+bZyHMO32jL2GiiB/X+93HX" +
                "HbBXnygxwpM7QbGgj06VNJbebW9MNO0axr4qLAoWuCxzyxm2raujX5qXshW2lFha9zpwahkGhOr/xYms" +
                "5zFzpFcbOLRjS2WFNEQwIpg/mYXUeYhhhdNACyy3nMrenWI/SXbzpGzoD04ozajdPqPsVZy/RhT1i38+" +
                "LI4sV9PrZVGJRFLFlRHGiiVEA5rd5H7/D0WfV/JJIOQTca+yomkoMlx1JfnMeEzyXI5vFpP3E3achMa7" +
                "La4/2OKnYYt/RTm2z0wLOfgw33juTqoz8YpmULTltMB3VrZTTTqqvjnaqRQJ1+8I90t7qUHsxebyAFiq" +
                "VmB/aUoM0TUmSzVRBGZcMBnk9zqwTJqAXxhyIlwifhyNoOD7oulGGDLuiJCVqs+18lllcOxONAj9JRb9" +
                "JAkIOX0EwMtXbwFcy75WwICH9ZKDoZit8Eeb3OXgmIciZScdD5Q3ChaRYIeaWAN+cieWx5IWVyUSwTFM" +
                "rFQBU+8aQyKhD6WWCjRAkuTaClqP/EC3MH6L1FG23jQiLsdh2/diqrKMosHi1QbOI/ROSEZs72icqjHi" +
                "t1BEeZ4C/IN7ln6QWLSL17H0huYZLhSKV5CjdsIzrJBqIaeSn/PsXzYQRNixHrnQjNoe1XFm10wFYwrR" +
                "T4bavfRAye2c2dXbvLFyCF/TgLIyfT5ksYSztHZrjcUB90jJiwZ1VrzZQDISg7J4Oa2obT7TBlLXrwdt" +
                "KhFmYmEoRsnAdtsCfZVsfQ0alRIDEET0PLQta75E3TK5N3skfI1cgJ+YJ8ITGbS3nuFwb38nKu9GFvH0" +
                "qC4QbCQbMJ7vHCgvAIhq38guokFrtMZ7DimEdiFPrCdGWX3d9RaJMgs0jgdKnRgCTPSWykOENtFbTq81" +
                "m0JrbpKCmt09x0CBJnShgSSfJ3XhLW9Uupnd7AiIs9sz5ueZMF9cRH6iKbGz45TfBizme4IFMVm70tuo" +
                "zotpzsOlXgS2aznIKHsrqkzGKL39XhHATPYS24pkMkiXYCUeGdSqnVy4VeiuizqlHtiHupGP5Tgwk5ES" +
                "lUgK9aoc8TdjUnMxMp7aFclBNztZy9C0fDJJbPISIhcE8Zklo8vsXUAHxvDDyvO9AsCZWreQuOHDynVX" +
                "Em6xRZx1b+dSUENi8wvP3QnHKESvMH1gGhcFI4qPWsAgPctM1+LHFu2RQ2QSBglxlo64srOR94N/Xd5/" +
                "qCOk0IEXq6JUhAJ2IizCmUKZMUuLbLKmBpOF2ZEDdjdQ0CK5wTQBjnoiKQwvGrxmJemDY8FPD1I/CFW7" +
                "3c76J6p5NriNAe0mGvkP+9x7iRZs4dtgkjy9XQ96yI5f0vfpT9ETCbt63pXDDg85c9EitcqPukKtq0Jm" +
                "EnqgXFJbwBSbsl3CdJwL/apwJ4foyEA1MzoG5JFSML80o0cq6Z7SKkU3FbZ1qIoebBi3UDyyEYyf75CI" +
                "eIgcPf58eoycM9MSltx+9vLs1Rvk0gcvYqrdXnwXChtMYMo6hbH/ZYz7GxWJEIDPLsf9sovhURllRr/a" +
                "cuBkCgDVRbfvnOy96shZT85qgaN6NyjVIS6enCeMuY7BLVJ3HIEeXEgYi2jm8x7mvj36taTZoTvNR+Fq" +
                "wzP5EC4NknZ2wF3KR4W153IVQLzvRowVS8/qgSNUiW8a5iuq9JZAvUppcNMgRV5wl+xuJOvEHZ7pMXt2" +
                "nbDE5VfgIWUBPuN/Qz+Dyorz3o0wYr1ZtVN2KAc4dMu3cDjp+8BjaotiZZJJym0rsZfSSkzCvMoh2uVC" +
                "RJZlmhWQVBDrZDjeRMdLpxNkrIaKr35mMnEqqg9vmIzmeFyLxOut/AqqP/joiYCXMl64PRDquxcZxYoi" +
                "oTq6y9afXSMMU06lqq+alwseelA7Pplq7+IrjMpVE3UxT1uJdreFTGkiqjPnXk0V45RGn0/er31jIf8w" +
                "oxW5UGx5r6nRXJp1wih6izCXuUrGRxCSZUrkCakza+R0sdZIk6WropAD/gAbqDd+oMp8/8ygw20mffqS" +
                "Jldlvo+gToSeBm3zeTEJmwUJMN5TGuYnyMEUrzWJKlk98fyQ5uREQkevGw+3+FmBoLt7dm2mX34myxa3" +
                "KLYyc6TB3JJtWVQktt6hRuN9Uwkb52L+6jaQavwQ+dvlUv0uDO+skxlb6aedq9SGV6jBq8s5sypYTvtg" +
                "7r0/zUaYQWNf36UoFwGM6fgdd32R3U/JkqwwqpPLbdJbGuV1/5bG9OK89D61/h0uzPrYOApDGnmkNXFI" +
                "7Rws991ss176zTDdPDvc59nFtJKko246S2zWBbSpHPKb6J3OyfHi0T2Ztm/7eLJHD9CP7Bq6cKjphZ6r" +
                "9xvx4u021p6bGfAkELDm9KpFO/oaT6/1AZhIqNm+9drzKrVCW/NivcLbyvvedT1WTXy8e28PainUBcfa" +
                "wKhY5Sj6a3rTWoubKiW7dFnrpbhN8fCSEnhl+08Nmd5NJavxSI8wgCh1wyy7lmErKE8z94bsmwTf1o3e" +
                "Zbqc3dFlQHdxu87Pc+DuCeP0yglx0ncsXTVqx+Ee8nvO/DstkcZHjXlsOMzgxFuQ/EpwgkGrP+bZBWzt" +
                "xx9Zyf22vCzHTd2O62Zxv5t/9Kdu/sf7+Z/AytNLAJLrEM5QRUinclZP4VF5KEbvt5MqnGDG7CSBTBL0" +
                "0c2UYUJ8zYtU2Ujf8lZRv5wjnLe/i+PFeze/yT8vngEFEKwKxUVqUUi7UOclTazOy3qT36/KGQsQ+bWM" +
                "nW8URXCzkQhjbX57vdIsrV2u1694SkcbzuAZvwWMtHZqT850uIFF5mHNi+UcQxLH1qr+B5aHEkPtj3BF" +
                "UKTQODEodufpJviPmwIEiTZYKRpcKXxYwQWvJDzgnj1NaD1DZBS8EfNom4YsxO7oiaHnATmMY0ypUwES" +
                "cjWYoLFzuagwhgSYxIqD0rvI0EGOcT+UAFUPW2lupehKRUsl2K34gpLZSnpgvcqevDxNz68FRjMAk5QF" +
                "KPt2PvnK3/0eEg6krd+/Yi2uAxTc9NL8KjX8Zj4Hf7wDtBOVPbonpo77vTfdDODqPt7emJ7sD0VSrvfv" +
                "ZgpiR/zSCdhVrj8/ATNGbh/9xMbwaqRwUI9T0XA8hJFuQEbh2XTnGHi4Nde2u7Sx/w6MQH/z5edP7MNt" +
                "/3dnwnhKTjof4dci/DoPv/I79xbENlO7sG9YDi+JkADVnqsd3yaWp0jO9KqYaO5H+DycN7IetvL68C2E" +
                "nQT07OOtndP9mbElLPnoVMxmen4wuvRyB2gWCVygfVMU+2td0lwwBuHpi32ujN0GqPbPnlSbxc6DX6rH" +
                "Fgwga5f3TeCfQbT/NrHEA5BqMoa2+Z8W0a6Hcg6Nx1bu19PpZg0le0SFIS6bvEHUG2FWJcXh+Ly7P6Yx" +
                "gEuW7UZNBSQHnpfwcVLzUoua6Exp977keMMsqIYk6NKvMK4VczAryavarVsFfzYUf2vyVG9wZ4pFp4Hg" +
                "ewnJ+lPIWWpXvTJXr2aTk2FjiTXJgVoaC9sGuTFr2zJW8Xuqce6W0X8BPChHa2ZrAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

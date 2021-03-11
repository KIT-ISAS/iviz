/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PickupAction")]
    public sealed class PickupAction : IDeserializable<PickupAction>,
		IAction<PickupActionGoal, PickupActionFeedback, PickupActionResult>
    {
        [DataMember (Name = "action_goal")] public PickupActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public PickupActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public PickupActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PickupAction()
        {
            ActionGoal = new PickupActionGoal();
            ActionResult = new PickupActionResult();
            ActionFeedback = new PickupActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PickupAction(PickupActionGoal ActionGoal, PickupActionResult ActionResult, PickupActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PickupAction(ref Buffer b)
        {
            ActionGoal = new PickupActionGoal(ref b);
            ActionResult = new PickupActionResult(ref b);
            ActionFeedback = new PickupActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PickupAction(ref b);
        }
        
        PickupAction IDeserializable<PickupAction>.RosDeserialize(ref Buffer b)
        {
            return new PickupAction(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "966c9238fcaad4ba8d20e116b676ccc1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1dbXMbx5H+jir9hy2p6kTGFCVLsmPT4VVRJCUzkQiGpGQ7LhdqASzAjQAsvLsgBV/d" +
                "f7/n6e552QUoyUkE3VVOSVnC7kxPT09Pv8/sWT54u5gfDOq8mL0o0kmSyj97Y/y7cxa9PM+qxaR2r0v5" +
                "1WjwPMuG/XTw1jUZ2e87nf1/8Z87nVcXL/aSaXGd5XVvWo2rh+1p3Ol8n6XDrEyu5K+O4jTJ+9qcTU6O" +
                "Ek6ylw9tGjJ9PvpkGFf1UMdX5O507iUXdTobpuUwmWZ1OkzrNBkVwDofX2Xlg0l2nU3QK53Os2Eib+vl" +
                "PKt20fHyKq8S/H+czbIynUyWyaJCo7pIBsV0upjlg7TOkjqfZo3+6JnPkjSZp2WdDxaTtET7ohzmMzYf" +
                "lek0I3T8v8p+XWSzQZacHO2hzazKBos6B0JLQBiUWVrlszFeJp1FPqufPGaHzr3Lm+IBfmZj0N4PntRX" +
                "aU1ks3dzcA7xTKs9jPEHndwuYIM6GUYZVsmWPOvhZ7WdYBCgkM2LwVWyBczPlvVVMQPALLlOyzztTzIC" +
                "HoACgHqfne5vR5CJ9l4yS2eFA68QwxgfA3bm4XJOD66wZhPOvlqMQUA0nJfFdT5E0/5SgAwmeTarEzBc" +
                "mZbLDnvpkJ17z0ljNEIvWRH8nVZVMcixAMPkJq+vOlVdErqsBvnzkzHk2m1BtrzEHHTpqqtiMRniR1ES" +
                "a2WpBMt5c5VjTWQe3DTJTVolJXmmwjzIQyey5MKVoEo6s9GwzuU1uOPmKpsleZ1grllFvgVrZNM5RMxk" +
                "gt6EWSnj3GQY2oNO+hm2CFBIBllZp1g8YhST2PDPh25ZQGGgh5UpAqkTJ5+A2RA9VKJhG1ZVOs5kHZJq" +
                "ng3yUT7QCRoG1a5B5x7RBkBquqhqYJZg46HVrltCtOpsUACq6MMfG75Oy3FW92bgo/BwXBaLeetZNhv2" +
                "stEoG2CZ8fRFmVbzn39J5kVV5dgLvTEfVBHkajGfF2XdqxblKB1kDlyn0y8KKJHJpLhBp3w+z8qeazso" +
                "JpO8AgsEOBgjret0cJUNe0X/7xi/VxeLwVUP2+utjGcQp/ksn+a/Za7VMMdaYyPj/SFkU12CEWrsw7S+" +
                "wjj+QYTxfJLOICq5n5rjE1cMr+MqePYbTYq0/vqpfy/90aUn7NbpnNnv7pxcjaHd+0IfbGbdZaV0x2Kz" +
                "Oe4FBbgzyPfg/gHWQXZeMcIDWUrbWLK1ybXYpIAhTN/QDeCLB44vuH20M/ay0mmHqmCygPYYJ1fFDVoA" +
                "SjrHtsOSYnPvoEM+l39k9WA3aWI5LLAzZ0Xt0AXcpegn7O9pKhin/WIBiZDc1YHnBRb1brIFHIsqlxbd" +
                "U5FKis825c4PmBoUZ2mqsaZ0iYc1iXaVXkOITKDIhkvqzH4+EyKsDh+NXSmNVtsQh3FWQItD3N8jpfmk" +
                "TT4MM19QLbM1ofYwD8iSWQPDXUqwAygtSDRaAypj2TwWKybkoGvLGaQTANULiEUbmSrKOmeUrA+0PzCb" +
                "QYE76oFBSlt9bBhSsCiXylh/5nQv/UPC6HmUOdLvwsCNHg2MtyAN5MLvQGItAp4V1hG9gYBjQPxfyELa" +
                "FyNqdfy6C7aHzhbJc3cdrB3hVSixhZhbEQRjXPZM/pDP/rDSlQMv5zDJtCPG91svBoN/A8wU00tEvkGp" +
                "lvhbAZva86TdigfYljYVGo3T2W7HcaLpBoC/MCMwcJ0jH1RaDlbGO2zbftrPJ3m9JDbVYjAAQ7ZYcCfB" +
                "zwrgkwKolMQXpqCtO2XA3XFRDO9Ss+cwVJ0U1XF/BekA3Q3tJcUwLzENriE2SZ2+zdxsQbO3ZHgvcaCa" +
                "RKmAKWbVRHdgYE0H0A1QgggZBN0q/HQErvXS8Aq2Sz+DPcINOslIjC0dT8wz01DbawfHatnoNlp78Gnh" +
                "RkZXyg01fcpsYjZ0kF/fKaEj8UKBpEJylnE1YE02eRqErrLJaCfpQwrdCnjHmDwWXDf5ZEIO9IDVsFGo" +
                "zplwKEPiioWh+GBqixLiEuaUaAu3XdSI2gF/rExECHmd5hOxrTkF7hjwXcmxdm8lLdQqzIuItBxnmr7L" +
                "p4upKg4sEsDBpge6AEXDdGLTIBG2/rT/iK+wozj0trIk/BUA6RmAngAgdDKaKEtsPhgrMrOij704mGTe" +
                "HFXdUWXTdAZFuaoNBM5QWw8Akduc1kU2fDhfVPyLKnyocp/aelGqAHBIf8g6+WSmxQcEcNut9mj+ne3E" +
                "CKw6rT5n/Cm2pBhknwt1QYOG0jEFTmiseHlLHj4qFDxm6DXVzzsJnF44DTXe4kcKqTihXcGXv/xCldps" +
                "rVrtF79No7HAhuCF7B1daXWSDrAHgxdxnU7gYog+NN6oaATAP6ewUq0lpBbjgPsraU1zF3P3tqta8IpY" +
                "9CxMJ3rYmFb0XGfTGS70lThbvVFZTHvYFHjxydbzVh3GRTxI+ECNsTIbwUGkU9+OZOgudFGIFu8KAFGE" +
                "G5yC4l5mDIPA2FCSYuM7M3AEHxj8AJG3I7oIj4f2Xm0czqiAx2x9dxPMg/zgGnT+uqBMnQnc0G5zc7Rd" +
                "JnaWd0PqlplG+51YN2bsbYV3/l9L/6/fNjWDQD8/Db9ctFpjqjbx569fA/WpE2DJv39S7l83m/IW20qW" +
                "kxxmI/g+dBbrSPsGK0P67KhJgDmqISBSj7oZ9lQFV/2edMjeYaOZfp/ko7phu2HlufS1WABo4Kw1Z6ro" +
                "i5EBpho0dR/MN7MzIjxbtu4bEYNPnLnre3pQWYVHw8ZMfSzBWQbWKAoymNmB2btnqt1d2IchBwSaCHlE" +
                "44sTCQY7w1lqq0G9XMNEg0XJUBc8Hm+M5LMw3IZYvUmrteyeWJt/StI6GNcaXtrs7NZPS1Fp7l/IUhe0" +
                "FCcZfg1EFFS278lwimMoMZZLWWJEQWofzKBDRAcD7SHSGXGczwGsubfwGF22st3x7o4a7dJKdguxkMgF" +
                "LMsyHyOKKT2DiUmYbl12knr0WC15wVkHU5YrC5VS27vJyShZFgvEQDEH/KO0XIJYIw4v2Xx1UchGd1y7" +
                "Ktx9GAVyvcb6fkjEbUSuRSHA98XCnEFvHpmL2Mnute7OJose+U1egZyV2GmUUalGhZWI0f6HDcfwjEVq" +
                "NC4qRlpA0hvMjUjlmSnIRjunNZtNu0EHNVpHuqnZ4Q3irurZN9pf+8eN5htZsxZNuG7+1xqDQc1epTaF" +
                "kWSxXAYATheC+Eby4Is4uS2vrS/1DWQwNFmV/OyHeIC3tH4h23p9mMc3O2H4L6J3cO+us188d3uryz1o" +
                "tVzzXKBrgO8mQ5pPVCRC6CFYpYaTkSXZGmaQKpAtcOuxsPA7c5iFpemgQqMwDQ5ODicwGyXe+FtWFiLH" +
                "qgTua+W71tvB8hAkNpOjWGHw2zer6mrYJQ2b0VYkzBbSp6lrXOyl6BcWN2sTVJQYE06OX9jKsws7w0yp" +
                "MlDe+FCgiJbbDj6dJFbMii2idggO0HvzogUd1lsoLjcjIHo6pEPgupgsXI5wHeZJ5xk5GvDfaMvQCJGS" +
                "sZk7/6sYbDMypUkWcteR2LbU+EoprOmTI6UO15hhzgLy2mk66Nx5iUQT6WDhHtHmsHfraGEQTVK4W3DK" +
                "Uyb9oGY71VU6zxSRCwI9c5Aoxz3UKGxNmzskUrN4aDIRvHTGZddEc2OAEswVqCcQicOhbhHJpzpoO9xf" +
                "V4BqwagQcKAtMZS/vJwku9g8t9IxVhwhj2rNFF8BJPBQyO+ZlA39wQmxnc3l0xVg3LZCEav0i3cIYM5p" +
                "3MEHX2JXQ67QG8c7F7AhGJ2S8oFQUIKCmHFeBkICGMkOpSy2QVqKJnq0g/9BdLFw4pvkWffH/S/t3xdn" +
                "3x+fH+8/tp+HP708OT06Pt9/4h50T4/3nzpqsxjEuUSCk7Xi845rNIRVDuOEOZdG0xDiCS1cH+5ooh93" +
                "iJrtJRmDaCITaFI6B1IjzMPsnYte3Q997mPyZSqh/+cmRDFxQXVHfv24k/wEVgN5/hbjTCKLdM1mY7gg" +
                "htGgKGGSz1HQQTcRiXjJ8ttLEH030Lb34/6j6NdPntb89TeQOkZJ6W9YiUXNZZdIzIwOgBU0KJ4w4ccm" +
                "J6Bx0mG+IApmcygHOTwUbu/84Ojk9QXwicd0iywwucBa5KNUUdahbpGCFDXwyEmTQibONn9LEA6HWA4e" +
                "SQNu7/vjkxffXyZbhG0/tsOcNFsZUTzM6UpEt6e57YVki3thW8ej1ebG0dnZOPojGue2UeipONrp8qU+" +
                "G7ZmTBgP6m26V8wqNuVmtCdpmuel5LI1u1EjHe15SGgqWUksEvl9gfSWUDb5wojqNmmLmJ6lWpMHc0U7" +
                "daVxIAwabkLKUUh72RaZUhS25EHnEUHzpbMxVPh30Sa2aLSYHaJQfPIW2xsJduQ+EBH/pcNBLg0AxImH" +
                "5cxvTZn6HqtqVbBZ42tKrFo7bYxabiLrqOZmdr8KeGnx289PFNXsXQ+0+7QIxwbPWkfwd1nVzYDmhy1r" +
                "F0CLe7nsXzC6g4UhBlIIi7aW+ZZo9T9rxfsUHvfwA1mzJCtLKcIwR6wKOIZSnz6MQWRmeu967NjzjVdb" +
                "LD/Y4rd2i39Pk3xd5MFVGEZTVsU6WkhAZcpH9OtDfEKjXoiQDpKt4IBtrxSz+gpW2QDSnpGzqqFZUg94" +
                "qVrg5or5F+oGSfdr4pzBN8/bDOehjgOAXxlyYicH/DgaQaHUEU0XWn7nediXOjGXXC4QCxd3InQnGoR+" +
                "inXfi/Injj4C4LR7CeCa7JaCOKTAwT4uG+7ix6xlqG9YzhAw9ylpRzpWs5QKFnaMgxr5tqo9zZDDNkG5" +
                "CmhxnWc3kZGjVNF0XMu3F2dji4NqXQlsKa282nYVZ8L7qDK5SuaLUiz/Xb/zGxaBLKNoDavdAgDHI4y4" +
                "kYzY4SHaoq61Aoldkxjgdy6hqitEnjWbq90SY2GBUHQDUYo+N5kUqOoKqUPlqCShcI1VBJYMUmwnzpG8" +
                "p0jHhwa0Suc5sq0RuR1n1sUNCserxpp6lJXp0zaLRZwl9qSkYN5Z/c9CikZQ1LBUe3pXTFRD1+LU2uap" +
                "NtihQ6S13QiksxCDWl0xigbWkIQE35L5EjTKJfUtiIiN4ZY1naBKl9ybPBG+hiW729HiciLck0Eb64lx" +
                "1dNs7kTl3cAiOpxbILj7NmCI7Lf0FwAED9bIHlUChfJm1BPDUx7G7cgTk6J4a7kCRuXrxiJppWOUSnDE" +
                "EGBaqinyUAvNKDYgMzIt8XEePpTb6p6jMcvhMaliIt6oVLtVrEi+nd0Ub89ux8xQ1SDQ+CrwE62JlR2n" +
                "/NZiMbcnYA6Bj6Zazd3PBuki7Ko1xoOMYlkmamYRQLGMUXq7iuRWvhFtRTIZJBR1T6SMjoUK4DoyjtBd" +
                "F3VAPbAOdSMfKutomsKhl+wAVStHeGRxEfUkZDw1LSj7Gz6HWL3yyiSxyUuIXBDEzSwaXWbvBHRId1qa" +
                "arRWADim1i0kceU4yWUJS9UMZrQFnHVvp1LfRWLzjRaJ+U3p6sMemca9KQwf9bJAeqZ0UdoNLsyqbQfR" +
                "ldDRdzZlZyOvB3+WP3ysI8TQgdccy6AiFLAjYeFIrTPWgrS4KCxamBU5oHCCFkkNpgnwAmNqQSseA/LW" +
                "ox3BT1NojzgSooaO7eP1j1QzpINTRCwxY7uetOv4fe56iRas4LxikszbFa0esuMndG6bU1Rw99boeacc" +
                "VnjIMReNUqtMK2Yo/VPIDKG0lEtsC5hiU7aLmI5zYYjQ0a8UHempZkZHizxJjqCrGhiPmqSS7jGtYnRj" +
                "YYv0KXlDTji16wNkXVyc3hs/Pyb7yWNEmPDXlzuImOwnzim/OD696J4jEtR6EAJF9uBHH5YzgSnr1Kgw" +
                "+De071vHIO6EsCOKPaTkXTOjoRjdpUYqxLbALM5X2/YnKi7khT9PIe1A59FIgtwjJedoko5tPwqzioK0" +
                "aIPWzWqRqtTsaMWdZLgJ1rOrhBAr3WbeRLfiDOtErkr0DAq79hgU/B14SPGWm/F/oJ9BZRF/5ao72VEs" +
                "BosPJ1uksLFZBSeH9jas9CrLprYbiCiYiDr61iJbV4uq1ejXeVnMEH6rdTIcr6fjxdPx+1qDPdfvmUyY" +
                "isrgWyajuXInuWaLaR/MQBtZyVx950aPhMokG2EdKgTKXcgi5eEs/95isEJ1dBezYbiE958PkAADy43y" +
                "MU8IqO0YTbXnRrU5c9VERI3iVqJRbCFjmoi4TquVImQYGm7yOlseQ1qtDAhcKPaji0JqBM46YRQ9/Mll" +
                "nkXjI4fDwC55QiLzUj+donj7Rll6lmVDKjiA9dTbfaQKZP3MoDd8UVRMX9LkOk/XEdQRoSG1q3SU9fxm" +
                "QSFBJYlMm58gB/MPxifjhlIdId4GKkrl5JnrKFU0kYXnUirOxRBAxEWO0Az1AInfotjKLEf1Kl62ZTYj" +
                "sfX4Dw3GxUzYWM4g2J6GwQS4Pu+xwqX6XhjesU5ibKWvGhwlGmsKQmtQlUcS6UmknNnMa+t1MJlsthOq" +
                "QY7bCENoieVmpbmIYIZp5BBSvO1FGjerW0hZ2HLqPmEZ6qxzzn/Dv4CClsc9fWwkckBj7xuTVG5XtcAl" +
                "hOK2cRSGNHIxvsgPEutEz6kMF/MJ7QKJvoySrXUORcjNS06/5RhJbSQFqnlHiEGP8ncoBtTTuL4Wi+st" +
                "03Y7359uBBcB63edA31x6J6/kse+ot+371l77mfAE/9zzunNxlXnJX6d6Q9gIkFOe9doXw1SBtzZ+oL/" +
                "dG3luVgm5rlaKSZc94CvfyS1iWIHw3SZplLjGU9rLt6R5DnpKRUTsdbD4WUl8NS2oOT7osJ6edXpymAg" +
                "SlGyYElPcyooly9oDNm0Cn4oShjuN/yvREZE+aopyBVFiEJMpyYjuUgWzHp4RyI75GQkLZQ2g4STTcbp" +
                "IiUQxxX7YyN7MOyd2wP56w8gRjsQ0nQXTl5cRyXhNHoSUiClJ/D0mK3Zs6EsS3euFlL5nStvfPZmqXww" +
                "hd7I6YEfdZ+L4xYi/0xGNkC/Ylu0i4aQ7r1hMeq1BvP8usKmiCm6d269ZAtYFY7QYLvj+vvdp3wXnzcO" +
                "G9Af+t0IR4W7Gxii4HUKzoLXux/c5QJid7mp9osh99CWwwcNJS4EA3uSIci4prEmdlPjrb095Myzvb1I" +
                "MluB8mIOhSwGaa3Ix2dRtze0AdazoN8AFCd+FwgHXhUTxD5dHauecrbIizCRzt1qfOCI/brQ7VNi1UEi" +
                "3QMs+TWd5DtJaYNWG26VGSM+fI6a9DJHNAtB3e043tNfMlqf6JHP9klUByVluiyZbu+EpnYGZ7na9CGz" +
                "/Mn0Iex+7tDQRU/iBGcYpb7cZubLGIBTyQic8iA0Q3lhLrTc5G4CMhz8z1o3KdIVFAmiVJUQmu5ryu2V" +
                "Sx/ceEK1yt3QAA4TG5Q1Mu029BFAGOwft4xaLyM7yXV1kVI59cQxdRptGVgh0SHSLA3yR7QNdBhHkMgg" +
                "px6fTYyQkUpvOevICl7dz5KPlk7WXlIeGMXc093kB1rMLNnWEmqTojKLGc+92/q0LvMQtbcj5dcSGM6s" +
                "Ssg3Fy1JI3Bp3Ejq6WzaB8MbR9c9nar8NxaroOooMzjRrhFtzjoWu//C80C4CCMQR+wyh7TW2AzoX+kK" +
                "IjnTTgn783/CP6unzlYPnS1Xj5ZtRKas6h7KlvOVg1gqBshAtr5CDs9lw2xcZlppxIMDwwJLK6FoGnVO" +
                "aGvQ045EhwGVl9tpA3C8K26WXGa1rOASRAVOwp12fECO4kofROWCCBBLsLDR30IuiZxiyorhukYSB24m" +
                "7CkoDJMX0FsY9vX50XMRa0+oxrdQALfE/9MbF8FDNgBREnlp0f34Jp8YOyWkWrRaJSXoNt8DqrZw0LCn" +
                "pXoC8og3smDCDRz+X5JtVpLd8JDP1UdLMtf8/5Iku02QxQeZb3EMpcTJe4GtRjdQTWzAv1vvfhAy4aXS" +
                "a1PnoDze605CiW73ksWnE5DqaZ/wqlqHpTr+WFd8GC+q53HHjzY2T5Lcz9HJpypYWs3Dmv2yeKvJHp7F" +
                "QsgRYhNikRILCQhJ3HPPgVfcPK1J+G3tNjVB5Z91q6i1G63jw1UG/GXzco4SQP6oWepVCP6nms6b8Xxv" +
                "cdpcuVDrsc8ger/H7i+SeE04MCRyhy7iulKx1XPKVq3oarxZvyJF7S5/0cKic8/OFUVOKZvJgPcQBLXQ" +
                "uayYhPUJcoYkj3TfRTGCyyoymuNc0Xvr4Pmsl9jtfhqeEDrAsNMmlF2d4jwd9Yn0whjVzrHz6NxsOvNR" +
                "SF8uoGDKcenDxQ88YoqG5ADcfUo+6h06BCnbuONLQsXuJJdiJf6EnC4W/6ZRTuOuGfKJNTtcLAc1meoN" +
                "U2EGV89YWlLfXwvEnL7EXzUq7o8ob/sKIBmjeeeKv1jF4lThhhi5Hc8iHh+6w2iYEbv2JUaasnMau7UY" +
                "COaMkrez4mb2WVJ8a7bjgWlOOxVO6vhoiDOD9SzH2iJSML2rBFEabgkDuRPmrzD4CYI4K7dduaXmuQfh" +
                "C/r/rqpPCOQ3kEUW6VmOtdzVKmbl+SUh6DEJV4rowkzuJBA3/6oB5LatdsuFHa3sfG3xShQNcwS4uPXA" +
                "0ceeIPrYA0GsZ25CbR7a+dA5HIBwJ6rEbWAAKmMtrwk0PzstTXLBdV8C5E8raNUYr4AMUXsO0ThQxECv" +
                "TERGumWSfBewOxha/VuQAaspZsmJRY3Ao9d5sahgOmaoLgB+LvUk+RYp4OgvYfccHB3xqAbdRKkOjIH4" +
                "kpwosYp/16gGANwt3j+35CHVsYuX1tjPVYsn8uG2jnR+/Kr75piHBzCnOQtfxOnzFyeom2iyVZA2i/pD" +
                "c/VJbenk5olVCJM8Ozs+PeIxGJHDYcz1w8koO5p2FM53R8o4f6nicevmLH93elqykmLV06tkDQgqo3nM" +
                "vpAqq6ZAtYIlRVFo84QIdlEW5A/BA6bdTuUaFu71p5OLHxYrEI+/+0/Sffbn48NL3qb7+zvbH9Ln8P35" +
                "AXdejJcMU+2ZLIMkkwo3WP2wDSS4QdMRq6gl9mMNOXuHy84+Im16r2VavM18QDMeYU+eaP8QfJTrF4Rj" +
                "ILRQsdR38h5QfBVJP0bF1KxEHP580T19yGSwhSF+Onj1MlEACEB6Loak9XsgupeBstpRpX2WzOmU3eRY" +
                "bAeGLVdWXbaSr/Sc5G+zveTuf90nhe/v3T+kfXP07P5Ocr8sihpPrup6vvfwIU9ATkDt+v5/39UpalId" +
                "9qDEQGYupSmrZzaOXFQUqKBHz+6jU67lQm+zzC70HE2wW7Wk0FUHrmFYJgKUiO6eiaNnyhv+xjJufRtZ" +
                "owdkrgXoRCmnsSS5wJmxJZusxMEFzF7iCSDPSAI8a5Ng76tvv3mqLah9tYoK7VYxvm8jXfz1JeL+sBIY" +
                "/Pfr1Bj44tfJ966Fwpahkvs34+rJ1/qE2Za95KunTx7LT7Qu2QBGdHFjLaD5kWQcth7TSOFE3AAucaRv" +
                "kZ5eTPheSjzqYn7fMTRYexPHl0SBrg9ratgPgls1cdDUXp2n75IvaKt/kQx+w3+GUiEnJSZ7+1ifbPTz" +
                "I9511vc/v+TPgf/5mD+H/ueTX8ItZE9/kWcbD3i0L7YJMYE4nCqafOU+GzXXdv0V3PecbbHScnCVgxNC" +
                "w3a+IuT53FXYBINWf0qTqzIb7d+1jXGTv813y6LaLcrxw3p09z/r0Z8epv8JZhy85a2w7HMB/55+/LAY" +
                "IH7sFphiQqqDIrG/EuMyXmyim6gn4+vi3dFINtKnHU/OQLMNRQPW1k+4YIA7uQgiwNbw1+FpYZbUU3hv" +
                "U5pYTM85IBD21/mQjj7f5qHzreUcCB5ir/BQeLWcqum+Y9eEN+7oi0drT+GY7zxGer5yzcn9dhGEhMrd" +
                "PZ+8YWJS2XHzVgGXEkPLuHwSLFBIjteYgbg6T1fJCCUNgoRSNjg5oLNSeGuG6tmZVPa6PCQ1qyp7f53l" +
                "LZiHEj9/gGh19Chy4GrpMY6/jpdTARKS/xY0ImvaXcMh9iZmJ8Ywst1XCTpI1OWxOGoNbKW5psqNinYK" +
                "yC6EF5Ss5EzjS7Pk4PQosjUDoxmAXswCTJ+vvLKV/yzbSHiQu6hVShCWAp6K3rpLmSkldEM3DfdzI5hH" +
                "tU/AN7oO26Jr605+WnFUuHgrDsf5a3usgmpjs5CarI+eA4u2PjgHrezazCHjULLlpG+wrjkbrQqAYNLN" +
                "yPAf2q6Gb9zmd1tf2tgnUQT8+YtnB/bi03+ExY94x9/CV/p/jf2/+v5f6WeowZSCN1J9pQ6qHeXF7lxb" +
                "0XQZ1fOJLI0vwgvhmACfdnXHehgD6I8fIP7k1lR7+Qm97PeMruHHJ0dSj8iqWhhjGqCFupGicHRAHmR9" +
                "5DE+24lRmC9ZF7ezIhi1i9aEouwsjK/51ZyBAeQVvOtm8Hno9o/TS2rhpLiKp1VgC1jXLUnDMtf0sBgM" +
                "FnMo321qESmHlSfpbLB01Nja7dcPd2kk5BNXTqaAJGIxgasVW54awWDoW7s3pcg5DzZqxTcrpqfb/nw2" +
                "DxqiBtx1m6FW2F9jrOchpRuB2DTg9uUQtL95J0m76v2HWpEgV5XsSim/eMQ0Im7KnKEmaVuxFPwbqnfu" +
                "mY1/6Uq/yfUR37pi/cmC8RP+ZZ+7sg96qZmzyS8MKTJ3Oq2PcNnRKkVRfzAyeXL6Ar61/oFLiv9GBQ+8" +
                "t3/pz6rPy4LpmuBkNT44ZDAPDi9P3hx7kDwP1oRJg08jHWDyfiax748BfHZ+fPzq7PL4yAN+3ASMgEaG" +
                "SBe4iCGWQea/KGQfI+ABHz3DEJ2sCYiu/mHQgRxJKuhXrcLnC+gkhtMRW5dZOQXXT/i9sRq70A62vT48" +
                "PD4+ilB+0kSZ33TyFyba9yC4y5drCXHbMAfPuueBLhzm6Zph+ojKrDkWsn6kIVLSHyKNL74dpfmE2bxb" +
                "0Ds/Zuw14LeffLWKXplRod7CAT6c2GKXnQ/j6A5G1/FgC4QXKAXdRcl2bOa2CRjn+Z2yn3y9Ac7zrEfn" +
                "gZswMJ9fPE/hw4OXL8NO3k/++LEIWo3yOgw/hroW4m2uVhPp2ShnBiFkwr0UEEzg7MSTiNnkm3/BJD6O" +
                "zGSKxvbTAVjIfAtPvOxeXMag9pNvBSAi40YM+0Qdo71ISQsQc7ZTTwJCaZ6ZJt36H7H3JB8EtaWuzg2S" +
                "Oes+AgdtLO6p94kYi21+pg0VkWoAiJkWKTM57pP1F+Nx+GBGUmfvkIzYoCJ2KrjT0aT1MW8ZOqTxoRcO" +
                "Idg+5F18Udl9VCCgn1Cwt6FGQCvMokZjholC9UR4F33di98sk6+B2ceWwr3MjS+XbYQ2bVrcsSNzPFXP" +
                "WAuWnsUu/YxXLMDZ07eiji54NZr+fn5w8vL1+fH+t/zTsYdnLw9OTyFKenx7fLT/wLU+OX1z8PLkqPeq" +
                "e3nSPe2x3f6Dx/YyetizhgcQ+b1nP/WOT9+cnHdPXx2fXvYOvz84fXG8/+CJdTvsnl6ed1/6sZ7a89en" +
                "B89eHvcuu72Dv74+OT/u2RFxAD3Yf/CVtbo8eYUhuq8v9x987bB3RsL+gz9KuCKcpvV1u/4zispBlaPO" +
                "5cH5ZQ//vTzGFHqHXUjUC0wKFHi0psmbk+5L/H3ROzu4/B6tTy8uzw9OTi8v0B55Zu3wonvwsg3scfzu" +
                "fVCexA2jV64T1+Zpp7U6L867r896pwevQOUvv2q/bEFCk69bTc67z7o2Rbz9Y+stdMxfHPBvWu80xere" +
                "fiuBFq21bpD5+Tka9IDA6cXz7vmrnmPCB48do3ligV2OD/9CXgQ/vEE7MgUaOgpGuPK/8s4RzRjm5PR5" +
                "17+Tez0jNmjgddrtnfyld9F9+ZqcDBb9coOn0eIvAH2ovkkraEOrlQ6NUvyoY/ssWARiYx8NugU3F/ay" +
                "sIA7Ck4D0yJea1ND7s6N8N0EOUez5sNA7vM6a+ql1n0uxs69PACxXJ1jgLWlR1/cqZ92cf62OxekLcLB" +
                "gQiEFBTqpx05Cyvtjr5DUz0MHxF62Px0kGUGaGT5wm67vNUeeWjRp4z0rECjk2ZUXDNWY7l0R/wJJAG2" +
                "e0sF9y3L+bk+SfU+dPynqdorSwPNcVmct9zi5ZkFTH0A3P64WnVfcWBFO2kojg5pFMedzfvvFJdbqt2j" +
                "D0qtDEGLL7DHPzdOk9E+wzepprcEYty3z//BUIz/dPpn+GZ6QD1821ePR/0PYooEmBt+AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

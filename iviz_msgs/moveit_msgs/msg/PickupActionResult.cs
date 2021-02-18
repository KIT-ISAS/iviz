/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PickupActionResult")]
    public sealed class PickupActionResult : IDeserializable<PickupActionResult>, IActionResult<PickupResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public PickupResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PickupActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new PickupResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PickupActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, PickupResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PickupActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new PickupResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PickupActionResult(ref b);
        }
        
        PickupActionResult IDeserializable<PickupActionResult>.RosDeserialize(ref Buffer b)
        {
            return new PickupActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4c148688ab234ff8a8c02f1b8360c1bb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+08a2/bRrbfBfg/DGLg2mplJbGTtPVefVBsJXFrS66spI8gIEbkSOKaIlUOaVtd7H/f" +
                "85gZDik5SbFrLy5ws93YEmfOnPecF/NOyUjlYkE/WjIs4ixN4mmw1HP99G0mk6tCFqUWmn60LuPwulyN" +
                "lS6TQuT0o9X7D/9pXVy9PYYDI0biHaO2KwCTNJJ5JJaqkJEspJhlgHk8X6j8IFE3KkEslysVCXparFdK" +
                "d2HjZBFrAf/NVapymSRrUWpYVGQizJbLMo1DWShRxEtV2w8741RIsZJ5EYdlInNYn+VRnOLyWS6XCqHD" +
                "f1r9Uao0VOLs9BjWpFqFZREDQmuAEOZK6jidw0PRKuO0ODrEDa3dyW12AB/VHPjvDhfFQhaIrLpbAX8R" +
                "T6mP4YxvmLguwAbmKDgl0mKfvgvgo24LOARQUKssXIh9wPxyXSyyFAAqcSPzWE4ThYBD4ABA3cNNe20P" +
                "ckqgU5lmFjxDrM74GrCpg4s0HSxAZglSr8s5MBAWrvLsJo5g6XRNQMIkVmkhQOlyma9buIuPbO2+QR7D" +
                "IthFEoGfUussjEEAkbiNi0VLFzlCJ2kEcdR6IG281zBa+CtIdg4/8HwU8PfWWvjD5WB4ejZ8K+yfnngG" +
                "f6NaKtomFlKLtSpQIacK+ROy4A2D+GyQeX4DdsAw+yeTsw8D4cF8XoeJEinzHDgLSjhVyKOvAnw5Hgwu" +
                "LieDUwf4sA44V6EC1Qa1BJGDeuA3oP26EHJWgCbHBVKfo4DUHdlBOm+Jz/zZhf+DkhAXWOHAKleJQghx" +
                "oS0UQHR/ovIlWF+CrqBQbYPy1fuTk8Hg1EP5qI7yLUCW4SJWiLYuQ+TCrEQ/sI0R9x3Tfz0aV3zBY15s" +
                "OWaaEelRSWpZ4b71pKhUX2QNaoXOwAxmMk7KXN2H3njw4+DEw68nXm6il6u/q7C4RwPIoLKyaKpL58s4" +
                "TlUowacSTHdYCX6ykIApegjw1HF6I5M4uo8Ao3nOUnri1SNonlO9NCvICCvlc8JzHD7pn59XltwT330t" +
                "glMFV5XaiuHXcBdksimtOtLpLM6XeKnh9VH4XoAwUVGNCF9Nvv8PEPF1bEalqJkfH4DXxj06cT66mvig" +
                "euIHAthPLTPM7QGQRARSQyCKmSAdCxBKl6MADQqeRMS36VfYnkbYGXIbWXobA/lgOTJtuM7Wbj9JsluK" +
                "R3AhmEKOdusuK0DGXFRoY8KLq3BLpKblfI5sNIsKdVe0HvEqOzttsQZwCGKYpAsUN9JDdzKw9HYRQ2xB" +
                "97HnUkg7VISx0BmFLqW5Y5p8gv0qRf0BKpVGBkGIo5YrkFWSwG6EqVl4twqOdqCt6oFKqhxdCmHkhwoG" +
                "f/AuJrwAVwzoretSmCkVTWV4jdoIOzh+hXBSazlXLBq9UmE8i0NrDISB7hroGOvxAkBqWZJRgJ+LYVXX" +
                "Cg+DkAcS3RJUMS5Ybn4kDgdewKOzYpDnWX6SIfkKfw1C+B2ejrNpVpBlAe8l3gFZvg7IjO3Tifv+46fG" +
                "ornSLUNd/RmcE+bxCkUMK1pvc6lXYo5/w6dZksni1QuxSmSawtaAZPbwjGlyosUhN9w9O60dECCaM+gb" +
                "SG4hb+Is3zELKIy4uuo9t1+86Z+dvx8Pej/gH9zLX1+e94dDuAUCfD447R24DWfDD/3zs9PgYjQ5Gw0D" +
                "XNg7OLRPvW8Ds7IP93Xw+rdgMPxwNh4NLwbDSXDyrj98O+gdHNl9J6PhZDw6d8e9sA/eD/uvzwfBZBT0" +
                "f35/Nh4EV4Ph1WgcANh+7+ClXTY5u4BTRu8nvYNXjgYb5fUOvmOuWCmJ/xHX4EKXElIeZ9ysTNpxatIf" +
                "TwL4ezIASoKTEdyKV0AbsOLZtjUfzkbn8PMquOxP3sHy4dVk3D8bTq5gw/OKsW9H/fMmvMPaw88BOqqt" +
                "9J7ZXSipF9VpVlhvx6P3l8GwfwE8f/5y42kDGKx51VwzHr0eGVLh8XfNxxAx/GThf998OHqNUZt9/APL" +
                "Qq/B6S0bTH8zhjUBoDG8ejMaXwRWOw8On1eaYhgHSjQ4+Ql1FHTkAyxERYGVjpseyvg3PXQMNGp0Nnwz" +
                "cg9fMGaeatSxG46Cs5+Cq9H5+wkJ7giQegRbr/yaTfOtM4eICC8KyPtTuDoQZ7iGIDo3oVSOGylNUx0R" +
                "d1WXvl1lOiZnJrIZ5R5/z4A8TcELJLHXQC5EBBr8Kp3+Iz5lr0oLA4LHnPqRd9JltoR7CHyNghsjKWKI" +
                "LsXp6I2QcKFVl8kCLrw69AtcDAu9U2h/EGWzYOO8PoTa4QIAhVmSxBqpzabopyFpl/aZTSWQFmFyeOJE" +
                "e6dlAZzY/SPaDg7fbg8c6MCA5qPfJBJuvTTCMgqFOAsFgHOOE3SoUqoPcHKL9Y4cbv2C6hpwR0fxbMY3" +
                "L9zGwI3CIZkRENq/4xVxlpnGlDZeriDTkhCeURHIVhkoTLbkTrMI44x9ixAsxMAP6z6Jkvm2xXRBzAAv" +
                "1rLj4xDijuNj7/40oUi5iphaCGcI/cLTPmDnNMsw9gyQvgczhe3K6DFLOnsgRVxkSWRKKcAEvr2nHOKR" +
                "KjHpGhIK+AVueHD+ZEh5RkE6W0NXtHarcJE3KeAhPxb7ubrJkrKglG+Vx5r8RRuxidQM/AeGhljQEt/U" +
                "bI6OXCgHRUYIYNnuVEtvVAKBXLHeXPpU0+Knuk22Wm1RM5BW4YpqcrVK0Nri1AcwXOLuYbtLhA0qWjCW" +
                "peAOFS6CXIBtFUJbdA6pXCrDCC6fOnPWW6Nmex5xTdsQFxQsguBZQAaxsYYrkBkYkBUjuRE2JbtVmIRP" +
                "U60spqLkFm+oY4hjjW5bNyTzXK51h05AMyIxYrmzzmFCBsVeKy8BFkt5rXiTWQ+0o4ZlFBLKpCt+wYhf" +
                "deddsc7K3PpToiLNAKCRT6Oqh7CWHdxCiR0Y643yxUl4C8wc1kYbkXtMDcvWo91kMwsLg/ik4z/B8QPJ" +
                "wEiG41kNroIMaW0TCKcDVSZRMQe1wCENjM5BLgVe4iTBLgTC72ol9iqQJv0xUTJ8tIbgfWUV3vuKOfAY" +
                "DmXz/gGqxgqr0mAJ0tos+wDUHiNc4oVTsUjNIccjxZvBL1EGcgU4swwTZuuxgSVlWGBxC5ZV57Eic04J" +
                "rC9dfUNa+8TyvImWiPN6RXVrTPowT0k13v28Z66Kyv4BrEwyc7oLakS4gJChK96gKdxJrAN1sP4PobHM" +
                "rbOQpGHvx6dvyKcd4VW+fwfKCv/JW6x7owcqFplW/BA1GLXMq+f72DEj4UceAxTeixZdew5QeYWFBgYN" +
                "aQylxpjPAsE1HP7fjT2uG7vNVQrbv9aN2eX/l9zYfV6MI1HcrltzBYFbka/ZgUysCnPRgH/fWHQLAsUF" +
                "+LPx7BdiEzxkfj2U07sHa8vJ3Lo8Yw7OrUxVcatAL4rbbKMxSPJDhwfGJEPQ5dYHqpkc8f6ErfrnEjbk" +
                "KTqAPGOX+jhEGmS2kCjh0sFnDfyFc8SkUUuFkTfolNtJoTzqDNBA1bKc4uQOxsdRpqhUTV4MTA1vGTJ/" +
                "dMdr6wyZJ/g1bNlHY+twwZBW4VVBLVlq4oKvzuN5HDXdKDl+Q1xHFLNDUGkwKcKZDwMRYuXPcLvdFWcz" +
                "MtBbJIiM24bHU+Xwog5GkWUdYZochIfP0EsyImurkHcWYCfdqgh2535zl7n481FEXenYNmmDW85jd53X" +
                "ZI6f/qgUFJn8RYLsb7ePZKvkNAxZ9oLVVZ5Qp2eaZ9dYf05JxTRmwZgF4pUr0zn19/HSAGdnbdUsqT6b" +
                "dY9DHbu/LVIDUbB4KuI6YFSAPF09SCBeuV9HIgGrPnLW9xi1m3sKDuZ6bnzLdjz18nW6qyRc1Xc2M0Wj" +
                "pSsTyxs7thyPH+h6omIFsdLVhkxTrYRIQC/kSnGlBwJxpV0vp4EIwGDf4BdVcB0fugvuxPQ8SGZIMAFN" +
                "M1OP6YLXwrqIKjrY9hCulLK7DaKtq3C45mhx7OATop1Wk1+ZwXbHpuqc1FMVq2oSuiNtrQiLUhJDc4NG" +
                "VoYLBAGHQ/YusV1y4JBjVDA4T3Jwd2uOzyA8YGzNhh2vdYDgAlNJQ7DALdvRYcwomoSAO+QkvTZ8A3Kh" +
                "HIElg1cRk0rX0BJhOHIQtrlCwMUp4D/kTXyvhUlG0zgyz0oyCwOm3bHlCDokVdimlPmajstVwkM8CJji" +
                "Kj4aBYldMlu/83ojVVGm6qxgixIQDMwhlXRuVTxfuHC1IRW4Q2fiOs1u053KwdKGR6mxbtpn30SBHe70" +
                "zShaMHU9m9KREe3Uo8aKXrABQ6th5D6pEoEDOXILp71TtdSqrVbo65ViDcH7eio15Y7Epcqk+JcAU4x5" +
                "Ssk1k8SUTBAEwqmAV8VT44oxCdoS1VtbNvvi3DgNNJ9mMZOCda/OW3HiKsNxCHtSiNWyZYxzY1hnRn/E" +
                "2NKyS/sMKwXeumYYomsLAiMGPO1C6UUDMn4Fy5fmyVZY+NAH8xrNhuZDIC3G6qrCIME4PEdnxw0t0DKb" +
                "tnKwAWwo2QZBflEUc5ZFLGzX8LvEvUgPHXUfsfjQx7AfRdrXLSMD1+GikjIFft4i0N2bOCs1hIjqLsam" +
                "M4WufPGSPwJhT9cQ3/dPT3vP+KQxed/aYbM8W3KpKr2J8yxdYmyMKXeOmde+gqx9Dc6LrIRaAwVYum4o" +
                "SRy1zWHjwcXow6D33FC2WqEvwyg3ddRRQcQ4YEJd2/Lm5ym2UTlvstSCPDxSL3F2rXfonHV17PYT6aAO" +
                "OM9bYxBG7pQh7NNYihGhTXNtEz1Rs4JT2DaeBR5PZwmyDDhsfUrldCOlgaGRQZNYdMRIjlYqd7kATtOp" +
                "HCNXtzazzx/Md37Z6bR2//IfwY1CnCH965vNH2bQyecbY+RXqbwyo/vRODrwcljQwlRXKy7AYLi5xJEb" +
                "rKHMucPiSgxc6gd9oYZKIxa5Vq6E7x9yzLMzBKKqU+VWt+bgz1IRTd2tAGAqmNHUR8hcylRp+/FqNHyK" +
                "U12m/PZb/+LcTON0cZDIalVUWYSXpdqZOFcVoRKjiQTs1dMVA4o14nSL9MmyqP6TZdcQ5lyrY/HkH3vI" +
                "6L3jvRMMiU5f73XEXp5lBXyzKIrV8dOnkMLIBJhe7P3ziSEyp2Arzbj6lxq3yVI0UREKyeMDRp5xsQeb" +
                "4pAy7mulzIjxLAHTncYJ5End+t1aU92Q5m6QjzbxPn3NSkJQkC50BOZoLpyRmvHwqa2j6mPqFAKOhmD6" +
                "LAjSsXBc4C+REfBlkxHHL3/4/oVZghc1Fxtg4Sbae/a0q5/PBchPK2x9OXnVD7/6I3lnlxjwdJzYu53r" +
                "o1fmK2w3HouXL44O+TPOeOGSGKNluwZChdssj5rfY3CDBNlTbP/UPF5mUZngAio0FNlqz+k4qvtD1frv" +
                "izAAp1M232l2B4nlCjWvI8I1hOgU9YVYaDXVSps35cq190DNbJUSAqOpjRcAGF4IeP+TbXL8/awD/+u2" +
                "zLzf69Gvved2rPfy3WA86B2ajye/nZ8NTwfj3pH9YjQc9F7YyS/rt+gWQpzMKvy+ZRdFMVzH2jb4q6VV" +
                "e6VaYfdgqQvR9zd4y465cIyZD3WjmQl8oSO77qz72qv27PHl1zI6ik+BcEKVk5BfO+I37gX87uMszThe" +
                "otJ54YrVNa+EFVuafjMPgendirfBr71n3qffHK/x0+/Aah8l5r/BiipoKHZ0pPAzdYN+HeNkyD8z3bmM" +
                "4hJRMMkSa1C3Jtdg3D89e38F+PhnWiETTBQwv8TBXGHVoZoGFSJtLEmdHHPU70JCQNIVVQWyBjd4Nzh7" +
                "+24i9hG2+dCuaOLmv8fxiqZFLUOztiD20RbafB56PXsOU2fO4Q/eOfedgpVJyzsWn8lrtp95kqVcXLCP" +
                "cI7L5QZNm8SuUZxTKs3jjEW8qnSIeIr7MWNFfS9XHdMj+9YwtdWwRMM/p1IN4kG5PEvdWFwxBhc+jIvb" +
                "zBcoe803mpgYrDYLaiQtDBj4OQ8pILe9imlXtLjy65q3XknfW/dYBMapK4fWilz+kIVkGdfJ/UJd9+Gv" +
                "IExF7cXjoYrZJzoIyMrZ+PJYpnOIJ/7medgbmZQKE7UZj11XY1xAI7ZJIfjRHz+18IyJAUA9KQOrZZyH" +
                "KQXaHTZDu8ZpH1pA2GzhObXqedMjscqSsYVllqw9XSHFr519PGI81V1AZcVHwZZy+K2TA9xZVx1TDKiK" +
                "Ba6iIO/Et1hJ/FaEf8JfEb46hcKS4rgHCq5mH599wuKk+/gcP4bu4yF+jNzHo0+uf/HxxSf67qEY8IVC" +
                "YKOburWZ2thiFY2M98EE9wW8rYeh4YJqrfEo1dyAiikddIb4sWObMvAUPsgQ30nhRFx/Qill9dU85vLJ" +
                "VeG9s/gq41dP+K0DikNd8cR4A7z9bFUCa4s4ZJBjJlPveZOPaFDZBdJbWyZz9OZoDpDjfVkja3NoJypt" +
                "aQLu/gArRXYE/2GEuTEp6ynglyrVrInVqs3adm0+yNvZHFL1YTyS1t6DWn3Kxb6ogvOmpDzmddRGJ9+M" +
                "5Ci+XugtX5qI2aKZVr66OSyxPegwEywHwCrbwKpg7fMQi53faQ4Mta0R8opqmMkDQT2iOA2TMlJUaqVx" +
                "Ey+a0U8rLX5a191dMzBq7IUsyKQd5isHzbOljnubrmF2dhkW04W5bX0bJGDde6ZK7pHmf8chfg4ZK5Om" +
                "WLFIYlXMn0vbx7AiE69wHLz9dcMzrhBkCquyanZXHtCqZoYzEo3B2nvGbzx3tnFEWvNr/945dS37r3pE" +
                "elvp3tcGpPBebmJrpReb2C5uaRZryh0rLOKYbrTXrVRpdOCajiBl3u0K6B1jmRj5LLJbWIFg5GqVZ6hC" +
                "MSyYw+n0iypCyjA9PF2p0iAMgNdUN98o6krxhI8mO3ki9mWVDYyGXl29TWXAX4A8bFKYfyyhoDD4njG0" +
                "qu27nGKWuR0B73QzHLq98mwVxozgcw+lzkNMyUoztkZgA5u4+Th2TbOEZ9pct5g2bG0pUvPSdKDsFCrl" +
                "/OjNrJKD9z4wIHa5HFKlGRhrsCZ86VoFKIHDvN4D/iosHAbe4dzdhgDjryByDxLNafyaBGpIWI2MXYaJ" +
                "GNv++BOwBLjrqNf/ZBuwDmkvZ1w8lmVBWFWm8Ydv4vSbjb149HoVh2YnYODs0YcDvyMcfGFEROB+8IXV" +
                "HPNIgjytqkPE4X3/hLYZR5iquUy72xqPV+ZfCqnUsGKi0uDOqF8M1jyVXGSnWWl++bmhkzTAWb3yQjgr" +
                "qY0KoHN4Ms+y6Al36LrVEACfDckSwq+Od07EDeRRCxTzWEM08O7a9F9tb+YtOJuVyifeJF6lqxZidUYO" +
                "3FCy2HIEv4tvvSXGAlMckqxeq9+v2nh20KG9/XwQnEHAHLd5/jKzh5tGH48MunkNz7/9jVnuOR/yV+xG" +
                "6+MeTslxiEolM+5m3wu5Y7Xed2x2QsdB5qIbg7WBp0VakkjKlcEIqCvzlBtY/hwzv7DcAWXZoIXYeSPj" +
                "hGYVkQiyISxG4Wnd+zlM3e06h6nWIe/iZbnkCyYszNAZjhFjV2wRJ4YWZMX+//ae0RtFscbj20ZFjw4R" +
                "SmAgBASBD0DFM8PVrrSTTcFEw0S5N8D5ktFqKVMczt+4NXbcrL1pRKH9YyNJRU9XpcYfNBxl08AwK3P2" +
                "DBZxbzTJDD0FPKLkJkgerTBp/AndW/jZjpHYnGN7WmL/1aBGwkEA0Cc9ThzV1Cssupg2SH22t7It2tJx" +
                "Az+s/VQEQG0ET6KvqY04q17FoN+TeFbUPBeIk4pe9h8Zsa7KWmjh7AEhk8ythle+y5iXh2rT5Zv5SOv1" +
                "3VYPGo8k1OilWydFpbf2YFYF1RNnb3G13lQezWiEN1U3Q9+DBFW3F+43vkoLhS1y8Ko4SIFRgDPDOPWO" +
                "fMwR80qvNyfN7dDpv6PqFgZPrbda/wItWoKg0EwAAA==";
                
    }
}

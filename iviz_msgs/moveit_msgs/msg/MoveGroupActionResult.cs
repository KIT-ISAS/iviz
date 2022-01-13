/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupActionResult : IDeserializable<MoveGroupActionResult>, IActionResult<MoveGroupResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public MoveGroupResult Result { get; set; }
    
        /// Constructor for empty message.
        public MoveGroupActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new MoveGroupResult();
        }
        
        /// Explicit constructor.
        public MoveGroupActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, MoveGroupResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        public MoveGroupActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new MoveGroupResult(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MoveGroupActionResult(ref b);
        
        public MoveGroupActionResult RosDeserialize(ref ReadBuffer b) => new MoveGroupActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6ee8682a508d60603228accdc4a2b30b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+07a3PbNrbf+Ssw65lreysrTZykre/og2IriVpbciUl2zST4UAkJHFNESpB2lZ39r/v" +
                "eQDgw3Linbv2nTtz3dQyCeDgvF+A3isZq1ys6COQUZHoLE3m4doszbN3WqbTQhalEYY+ggt9rd7lutxM" +
                "lCnTQuT0EfT+wz/BxfTdCewZMx7vGbs9AchkscxjsVaFjGUhxUID8slypfKjVF2rFBFdb1QsaLTYbpTp" +
                "wsLZKjEC/i1VpnKZpltRGphUaBHp9brMkkgWShTJWjXWw8okE1JsZF4kUZnKHObrPE4ynL7I5VohdPhn" +
                "1B+lyiIlhmcnMCczKiqLBBDaAoQoV9Ik2RIGRVAmWXH8AhcEe7MbfQSPagki8JuLYiULRFbdboC/iKc0" +
                "J7DHX5m4LsAG5ijYJTbigN6F8GgOBWwCKKiNjlbiADC/3BYrnQFAJa5lnsh5qhBwBBwAqPu4aP+wBjkj" +
                "0JnMtAPPEKs9HgI283CRpqMVyCxF6k25BAbCxE2ur5MYps63BCRKE5UVAvQul/k2wFW8ZbD3FnkMk2AV" +
                "SQQ+pTE6SkAAsbhJilVgihyhkzTCJA4eSRvvtY0A/wTJLuED90cB/+gMhh8uB6Oz4eidcD898T38RrVU" +
                "tEyspBFbVaBCzhXyJ2LBWwbx3iDz/BrsgGH2T2fDjwNRg/m8CRMlUuY5cBaUcK6QRw8CfDkZDC4uZ4Mz" +
                "D/hFE3CuIgWqDWoJIgf1wDeg/aYQclGAJicFUp+jgNQt2UG2DMRXfvbgf1AS4gIrHFjlJlUIISmMgwKI" +
                "HsxUvgbrS9EVFOrQojz9cHo6GJzVUD5uonwDkGW0ShSibcoIubAo0Q/sYsR92/TfjCcVX3Cblzu2mWsi" +
                "PS5JLSvcd+4Ul+qbrEGtMBrMYCGTtMzVfehNBj8PTmv49cSru+jl6u8qKu7RADIoXRZtdel8G8e5iiT4" +
                "VILpNyvBTxYSMEUPAZ46ya5lmsT3EWA1z1tKT7x+As3zqpfpgoywUj4vPM/h0/75eWXJPfHDQxGcKwhV" +
                "aieGD+EuyOSutJpIZ4skX2NQw/BR1L0AYaLiBhF1NfnxP0DEw9iMStEwP94Aw8Y9OnE+ns7qoHriJwLY" +
                "zxwzbPQASCIGqSEQxUyQngUIpctZgAEFT2Pi2/wBtmcQtkZuI0tvEiAfLEdmLdcZ7PXTVN9QPoITwRRy" +
                "tFsfrAAZG6jQxkQttcIlsZqXyyWy0U4q1G0RPGEoG54FrAGcglgmmQLFjfRQTAaW3qwSyC0oHtdcCmmH" +
                "ijEXGlLqUtoY0+YTrFcZ6g9QqQwyCFIctd6ArNIUViNMw8K7UbC1B+1UD1RS5ehSCKN6qmDxB+9i0wtw" +
                "xYDetimFhVLxXEZXqI2wgvNXSCeNkUvFojEbFSWLJHLGQBiYroWOuR5PAKTWJRkF+LkEZnWd8DAJeSTR" +
                "rUEVk4Ll1krGYU98MywGea7zU40cUPhnGMHfMFpfO9FzXZChgSgkhgSdb0Oy6l0zZ36O2KQyy1QcVsu+" +
                "sYBdRHvFItWyeP2SwQHTQpLnE3GtzqOA83EITKhCaOmgiiDUlbxOdG5HKcGYTnvP7fPb/vD8w2TQ+wl/" +
                "Avvy8rw/GkFwCHF0cNY7crOHo4/98+FZeDGeDcejEOf1jl7YwdrL0E7sQxAP33wKB6OPw8l4dDEYzcLT" +
                "9/3Ru0Hv6NguOx2PZpPxud/rpX3/YdR/cz4IZ+Ow/+uH4WQQTgej6XgSAtB+7+iVnTUbXsAW4w+z3tFr" +
                "h71L+3pHPyAnnGDEf4kr8KhrCRWQt3VWLMe76aw/mYXwezYAEsLTMcTIKRAFHPh+x5SPw/E5fE7Dy/7s" +
                "PcweTWeT/nA0m8L8546Z78b98zawF/Wxr0E5rk+sDblFKJuXQUs67ybjD5fhqH8BXH7+qj3YggRTXrem" +
                "TMZvxpZEGP2hNQpZwy8O+I+tsfEbTNzc6E/IfbMFr7dusvntBCaEgMBo+nY8uQidEh69eO6VwjIL1GVw" +
                "+gvqIujDR5iHSgETHQdruOJvGnNMswozHL0d+7GXiFNNDRp4jcbh8JdwOj7/MCM5HT9/CjuuPJir750X" +
                "h1QIIwQU/BnEDMQY4g+k5TaHynEh1WeqI5Ku6tLbjTYJTjRCL6jo+LsG6gxlLVC9XpkAMgEDzpQ2/xkH" +
                "2X3SvJDAIZN+5mUUwtYQfcCNKIgTaZFATinOxm+FhDBWhZAVhLkG6AucC/NqW9DyMNaLsLVZH7LraAVQ" +
                "Ip2miUE69Rx9LNTp0o256gGpELZsJx4cBm79qVs+ptWfvwi3OvSQQwsZ932bSohyWYxtE0ppVgqg5pwX" +
                "mEhl1A/gYhb7GzlE+YL6GBCT42Sx4EgL0Rf4UHgMNQGh9bWezVobrGCT9QYKKwnZGPV8XFOBsmJH6lzH" +
                "mFYcOHxgIuZ52OZJlcx3TUafvwCsWLVOTiLIMk5OauHRJh7lJmZaIXkh5Iuayh0Gc60x0QyRuMfS/t0K" +
                "WOOU9CZA6rfSaWzbJsABCHVRnsw5nSMNYsINFA/wB4Rl8OxkO7mmhJwNoCuCvSo15EUKGMjD4iBX1zot" +
                "CyrvNnliyEEcIjaxWoDDwDQQm1firw0zoy1XykORMQJYH3aqqdcqhaSt2N6d+szQ5GfmkMyzWqIWIKvC" +
                "N9DkZpOijSVZHcBojatHh10ibFDRgnkrJXKobTHk/WyhkMaiP8jkWllGcLfUG7HZmSG7/YhrxqWzoF4x" +
                "JMoCqoU7c7jbqMF4nBjJebAZuaXCFneG+mIJNSB3OECTQM5qNds5H5nncms6tAPaEIkRW5tNDhMyKPZG" +
                "KwmwWMsrxYvsfKAdNUxvUKIy7Yq/YXavusuu2Ooydy6UqMg0ALTyaXXwENa6g0uoiANLvVZ1cRLeAquE" +
                "rdVG5B5Tw7Kt0W4rl5WDQXwyyZ/g64FkYCTDqVkNzoJqaOuKBa8DVdVQMQe1wCMNjM5BLgXGa5JgFxLb" +
                "942Oui0JwKOS/tisFx6dIdReOYWvvWIOPIVDuRt2gKqJwg40WIJ0Nss+ALXHCpd44VUsVkuo50jxFvBH" +
                "rEGuAGehsTh27hpYUkYFNrJgWrUfKzLXj8D60vcypLNPbMXbxIg4bzbUo8YCD6uYzGC45zVLVVT2D2Bl" +
                "qu3uPosR0QqyhK54i6ZwK7Hn08FeP+S9MnfOQpKGfZicvSWfdowB/OAWlBX+yRvscaMHKlbaKB5EDUYt" +
                "q/Xu69gxI+EjTwAKr0WLbowDVJ7hoIFBQ11CZTDWrkBwA4f/d2NP68ZucpXB8oe6MTf9/5Ibu8+LcQKK" +
                "y02wVJC0FfmWHcjMqTDM8up8Z9INCBQn4Gdr7G/EJhhkfj2W07sHa8fJ3Lk8aw7ercxVcaNAL4obfecQ" +
                "kOSHDg+MSUagy8FHanQc8/qUrfrXEhbkGTqAXLNLfRoiLTI7SJQQdHCshb/wjpg0aq0w7Qad8ispjUed" +
                "ARqoM5ZTltzB7DjWitrS5MXA1DDKkPmjO946Z8g8wdew5ACNrcPNQZqFoYKOX+nAFnx1niyTuO1GyfFb" +
                "4jqiWLwAlQaTIpx5MxAhdvkstw+7YrggA71Bgsi4XXo8Vx4vOq0otO4Ie6BBeNQZeklG5GwVSs0C7KRb" +
                "NbVu/V8+mIs/n0TUlY7tkja45Tzx4bwhc3z6o1JQZPI3CXJ/3TyRrZLTsGS5AGuqOqFJzzzXV9hrzkjF" +
                "DJa/WAJiyJXZks7yMWiAs3O2aqdUz3be01DH7m+H1EAULJ6KuA4YFSBPoQcJxJD7MBIJWPXIVd9TtGvu" +
                "aTXY8Nx6y3Y8rxXrFKskhOpbV5mi0VLIxL6G67zj3xSdgmCP+Oh7Qfb0rIQ0wKzkRnFnB7JwZfyhzR3c" +
                "2C/UOyk4jTbcA09ijzZIXEgrgcy07cF0wWFhO0QVHTzdcJ2ZYG8XPNdN4UTNk+EZwRvEQZtRDNQdftha" +
                "nvpV1Tmg38/1hrADJTEjtzjokhQPNoaaXeKByJFHjNHAlDzNwcltOSuDpIAxtQuqBIGAhdwxwyiy8Ac2" +
                "jBUlkJBjR1yXN+7WgDSoLGB5YPRhIinyrBGGJwVA26ABTk0B26FS4kgWpZru2shcl2QIFsphxzUgaI9M" +
                "4SGkzLe0W65SvqKDl1YwkeKNUXx4BGbbdLWTkqoJUzvpiBViF9otnFBuVLJc+eS0JQyImAtxlembrPKm" +
                "NP8pbPKuLfZtxtfhE7wFZQa2f+fKN7KZdoLoD98smZaBB6Q9BAukx0cvh/VjMl7n5LzdKFYKjMpzaahC" +
                "JO546+HPEOuIZUYVNNPCJMwQAoJxkKvGqPW2WOfsSNydzfKyJLeeAS2l3aqkdLzWv3UMmGq82uC2ibAb" +
                "tk7wDpgJyOMwnjTr0g1hI6Ca1k4yTGM8ZMbDThfKrJpQ8Q3MXfPATjg4VoF4g8ZBdzyg3MWWqcLgb72Z" +
                "p67jLx7QNFeOchIBxJdsaSCxOE64eiLGHdZxu8SlSAjtdA+ROFZh149jU1cjy3V/JEUtYkrmapNAR68T" +
                "XZoUjxwTPDSmdJSDKTmcbjDfQsrePzvrfR9QewOtobHTItdr7j1l10muszUmu1hD51hKHSgow7fgmsgU" +
                "qMNfgDGblk4k8SHvNBlcjD8Oes+Jps0G/RTmrJmni9ob1rES0sY1K79Oq8uxeZGjE6RQEXmJl856L6wT" +
                "rvbcvR3t0gGveGM134qakv0Duk1i5eYqVnf2napFwdUodkPAmxmdIq+Atc5jVN40VgY4GTOKxJtjRHC8" +
                "UblP6fECnMoxAXUTtRt+LKf4bacS7P3bP4KP9fDS57+/2P4gc06/fpxFTpM6JAsKeNaRgRvDnhRWq0Zx" +
                "DwUzxjXekME2yJJPSHyXgLv1oCd4INJIKq6Ub8HXdzjhey60vuoz5U6hluCxMhHPnbMHKA5gPK+jYgMs" +
                "tcl+no5Hz/D6le2dfepfnNtrM1288eMUKa4MoFZiustrvqVB/UEO6i6gdMWAsoYk2yF0siPq3Wh9BfnK" +
                "lToRf/nHPnJ4/2T/FDObszf7HbGfa13Am1VRbE6ePYPyQ6bA7WL/n39hEnPKmDLNjbvMekaWns1uUDg1" +
                "LmDmmBT7sCiJqFi+UsreBF6kYKrzJE1cv0ft0teIbscgE13JfPaGdYOAIFVo93ZnbnmhcvENUdcApXvG" +
                "2BC1xNLJDYE5EZ4B9A5ZAO/aLDh59dOPL3kGhl7uEMC8uxjv252mv54LEJtReFzl5dTYePpH+t7NYNi0" +
                "ldi/WZrj1/wGDwdPxKuXxy/oEe9f4YQE01w7A8L+jc7j1mvMUJAQt4E75+TRtY7LFMepK1Dozb5TaFDt" +
                "x2rL35ctAEZnbKZzfQs14AY1rSOiLaTWlLRF2BO1jUVX5eTKn8SBWrmGImQ4c5cCADB0+BjSyRI5cf6+" +
                "A/91A3sN7834Nwhj9rbt5fvBZAChhR9PP50PR2eDCbhy+2I8GvReOmt3/omiDOJkZ3GW5lxCAoHWuOP3" +
                "amp1ElLNcGuwK4Xo1xfUpp1wjxfLFTo1ZiZwqEZ23TpPtV+t2efgFljVxFEgnFDl6uG3jvjEbfvf6zhL" +
                "e0suVdmy8H3ltg8ydCnNDgLTuxVvw98gI6mePnle49PvGMVrKDH/LVbU7EKxo9uEz8zfv+tYp0KumOnO" +
                "ZZyUiIItc1iDug25hpP+2fDDFDOk2p5OyAQTBczfrWCusOpQ+4F6hi49pEMXu9XvQkLC0RVVs7ABN3w/" +
                "GL57PxMHCNs+HFY08Sl9jeMVTatGeeVsQRygLRzyfujn3D5Mnd2HH2r73LcLNhEd71h8tjjZveepzrgZ" +
                "4IbwPpXP89s2iQc8SU4lMN8yLJJNpUPEU1yPxSbqe7np2OOs7yxTg5YlWv55lWoRj/loZal3JleM6T3a" +
                "RZ67RQAVn/md80ZMRtu9L5IWpgc8zvcJkNu15mZXBNyk9eeste57bd5TEZhkvnPZaEnV70NIlnGT3G+0" +
                "YB8/BGFp6QJPDVUsJ9FBQHXNxpcnMltCBvHfNQ97LdNSYf214NvQ1SUroBFPNCHZMZ+/BLjHzAKg4yML" +
                "K7DOwzbu3ApXe13htRyaQNjs4DmdqvOiJ2KVI2MHyxxZ+6ZCir8N9vmY8VS3IfUBnwRbqst3HvLzIbjq" +
                "2Pq+qv99k0Deiu+w/fediP6EXzF+owmFJcVJDxRcLT5//wU7iv7xOT5G/vEFPsb+8fiLP2r4/PILvXss" +
                "Bnyjh9fqa+0892wtcYpGxvtogvsG3s7D0D2AonZbm8/C/RG/Sqjs84b4uePOT2AUHmSEXxXhatt8QSnp" +
                "5my+kfLF98xre3Eoc9e9uy4P9W0R6w0w+rmuA3YH8T5AjqVL83iafESLyi6QHuy4RGPu3qLBq33VywZZ" +
                "d+/XxKVrP0DsD7EH5K7CP44wv3Jd/ptNZtbE4v4FjZs8tYXtW6T16/iPQ+YDMWteR3HfHsFLoaQ69jui" +
                "rSN3e3dGcXChr97S1ZUdeumku6NpvSvlsFdNjoBT7qSpgnXAt03cRZv2zZ5DZ4I8o7p1VANBRzpJFqVl" +
                "rKh3SvdCarmMeVbp8LOm5u7Zm53WWsh+bNFhX3loNUvq+K+4tYzOTcOWuLCxtm6BBKx7z/WPe6T5v+MO" +
                "v4aMk0lbrNgTcSpWv0B2gEmFFq/xtvbhw265+LaPbZvK6lS68n9ONTVeZmjdgL3nnkzNmd3ZImt4tf/Z" +
                "Pk0t+4o//BeW14fbMEAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

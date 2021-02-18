/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupActionResult")]
    public sealed class MoveGroupActionResult : IDeserializable<MoveGroupActionResult>, IActionResult<MoveGroupResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public MoveGroupResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new MoveGroupResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, MoveGroupResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new MoveGroupResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupActionResult(ref b);
        }
        
        MoveGroupActionResult IDeserializable<MoveGroupActionResult>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupActionResult(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6ee8682a508d60603228accdc4a2b30b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+07a2/bRrbfBfg/DNbAtb2VlcRO0tYX+qDYSqLWllxJybYNAmJEjiSuKY7KIS2ri/vf" +
                "73nMDB+Wkyx27cUFrpvaFnnmzHm/ZvxeyUhlYkk/WjLMY50m8SxYmYV59k7LZJLLvDDC0I/Wlb5V7zJd" +
                "rMfKFEkuMvrR6v6bv1pXk3dnsGfEdLxn6vYFEJNGMovESuUykrkUcw3Ex4ulyo4TdasSJHS1VpGgt/l2" +
                "rUwHFk6XsRHwb6FSlckk2YrCAFCuRahXqyKNQ5krkccrVVsPK+NUSLGWWR6HRSIzgNdZFKcIPs/kSiF2" +
                "+GfUH4VKQyUGF2cAkxoVFnkMBG0BQ5gpaeJ0AS9Fq4jT/PQEF7T2pxt9DB/VAlTgNxf5UuZIrLpbg3yR" +
                "TmnOYI+/MnMdwA3CUbBLZMQhPQvgozkSsAmQoNY6XIpDoPx6my91CgiVuJVZLGeJQsQhSACwHuCig6MK" +
                "5pRQpzLVDj1jLPf4FrSpx4s8HS9BZwlyb4oFCBAA15m+jSMAnW0JSZjEKs0F2F0ms20LV/GWrf23KGMA" +
                "glWkEfgpjdFhDAqIxCbOly2TZ4idtBHEUeuRrPFB32jhr6DZBfzA/VHBPziH4Q/X/eHFYPhOuK+ueA7f" +
                "0SwVLRNLacRW5WiQM4XyCVnxVkC8N+g8uwU/YJy98+ngY19UcL6o40SNFFkGkgUjnCmU0Tchvh73+1fX" +
                "0/6FR3xSR5ypUIFpg1mCysE88AlYv8mFnOdgyXGO3GeoIHVHfpAuWuILX/vwPxgJSYENDrxynSjEEOfG" +
                "YQFCD6cqW4H3JRgKcnVkSZ58OD/v9y8qJJ/WSd4AZhkuY4VkmyJEKcwLjAO7BPHQNr03o3EpF9zm5Y5t" +
                "ZppYjwoyy5L2nTtFhfqqaNAqjAY3mMs4KTL1EHnj/k/98wp9XfHqPnmZ+rsK8wcsgBxKF3nTXNpfp3Gm" +
                "QgkxlXD6zQqIk7kESjFCQKSO01uZxNFDDFjL857SFa+fwPK86aU6Jycsjc8rz0v4vHd5WXpyV3z/rQTO" +
                "FKQqtZPCb5Eu6OS+tupEp/M4W2FSw/SRV6MAUaKiGhNVM/nh38DEt4kZjaLmfrwBpo0HbOJyNJlWUXXF" +
                "j4Swlzph2OwBmEQEWkMkioUgvQgQS4erAAMGnkQkt9k3+J5B3BqljSLdxMA+eI5MG6Gztd9LEr2hegQB" +
                "wRUy9FufrIAYm6jQx0SltMIlkZoViwWK0QLl6i5vPWEqG1y02AK4BLFCMjmqG/mhnAwi3SxjqC0oH1dC" +
                "ClmHirAWGlDpUtgc05QTrFcp2g9wqQwKCEoctVqDrpIEViNOw8rbKNjao3amByapMgwpRFG1VLD0Q3Sx" +
                "5QWEYiBvW9fCXKloJsMbtEZYwfUrlJPGyIVi1Zi1CuN5HDpnIApMx2LHWo8BgKhVQU4BcS4GqI5THhYh" +
                "j6S6FZhinLPeGsU47IlPBnk/y3R2rlECCn8NQvgd3lbXjvVM5+RooAqJKUFn24C8ehfk1MOIdSLTVEVB" +
                "uewrCzhENFfMEy3z1y8ZHQgtIH0+kdSqMmpxPQ6Jaa+1B9pFXwdjBLUu5W2ssz0LQDXGZNJ94R687Q0u" +
                "P4z73R/xC9fy4+vL3nAIKSLA9/2L7rFfMBh+7F0OLoKr0XQwGgYI2D0+cW8rTwML2YNkHrz5LegPPw7G" +
                "o+FVfzgNzt/3hu/63eNTt+58NJyOR5d+u5fuxYdh781lP5iOgt4vHwbjfjDpDyejcQBoe93jVw5sOriC" +
                "XUYfpt3j154HVwJ2j79nqTg1if8SNxBfVxL6Ie/5bGbGS2raG08D+D7tAyfB+QhS5gR4A1E83wXzcTC6" +
                "hJ+T4Lo3fQ/gw8l03BsMpxNY8KIU7LtR77KJ76T28kuITmuQlXduFWrqZbmbU9a78ejDdTDsXYHMX7y6" +
                "97aBDGBeN2HGozcjyyq8/r75GsqJnx3+H5ovR2+wpHOvf2RdmC1ExFVD6G/HABMAGcPJ29H4KnDWeXzy" +
                "orQUKzgwov75z2ijYCMfARANBSC9NCsk43d66QVozWgwfDvyL18yZRXTqFM3HAWDn4PJ6PLDlBR3CkQ9" +
                "ga+XUc7NAFykh3IJs4iBkhTyCtIMOQpKd1tnZbiQejjVFnFHdejpWpsYAY3Qc2pM/q6BPUOVDXS4N8Au" +
                "lAsGIi7t/hO+5RhLgAHhY0n9xCsp060gSUGsUZBOkjyG0lNcjN4KCdmuzDRLyIZ17FcIDICVXWh9EOl5" +
                "cG+/HtTh4RIQhTpJYoPc6hlGY+jopXvn+gzkRdgGnyRxtNdyCM7d+hEt//RZuOWBRx1Y1Lz120RCSkwj" +
                "nLFQ/bNUgDjjIsKEKqXhAXe+OAzJoCTIaegBCTyK53NOy5CqQRq5J1ITElq/V5nwrLTBfjderaENk1C7" +
                "0YTIjSCohnbsznSERcihIwgAsSrEoVCiZLYLmBLEHOhiKzs7C6EoOTurZFNbpxTriLmFWofIzyvWB+Kc" +
                "aY2FaYD8PZor7DbGirCk9wcyxKVOIjtnASFAbgyzeMb1H5kSs26g24BfII9D8CdHyjRV8OwNHdHaL2tJ" +
                "XqRAhvxaHGbqVidFTv3gOosNxYsjpCZSc4gfWDfitEv8teZztOVSeSwyQgSro3YJeqsSqPLy7X3QZ4aA" +
                "n5kj8tVyiZqDtnI/cZPrdYLeFqdVBMMVrh4edYixfskLFrpU+aHBRdAosK9C3YvBIZUrZQXB41XvzmZn" +
                "Se32I6kZV/+CgUVQWQtoL+7B8HhSgwM5NVIYYVdyS4XtBg0N0mKaWO6IhiaGItfatgtDMsvk1rRpB3Qj" +
                "UiPOQusSJmJQ7bXZE1CxkjeKF1l44B0tTK9RozLpiL9hO6A6i47Y6iJz8ZS4SDUgtPppjPwQ16qNS6jr" +
                "A2e9VVV1Et0C24qttUaUHnPDuq3wbludpcNBcjLxnxD4gWUQJOOpeA1CQfu0dd2Ft4GyzSiFg1bgiQZB" +
                "Z6CXHJM4abADlfD72gje9hAQV8l+bJkMH50jVB45g688Ygk8RUC5n3+Aq7HCkTV4gnQ+yzEArccql2Th" +
                "TSxSC2gAyfDm8EukQa+AZ66xm3YRG0RShDlOvgCs3I8NmRtOEH3hhx/S+SfO7m21RJI3axpqY0eIbU9q" +
                "MPfzmoXKS/8HtDLRdndf1IhwCSVDR7xFV7iTOCRq4+EAlMYyc8FCkoV9GF+8pZh2iqn88A6MFf7JDQ7F" +
                "MQLlS20Uv0QLRiurDPur1LEg4UcWAxZeix5dew9YGcJhA4eGNob6Zmx2geEaDf8fxp42jG0ylcLybw1j" +
                "Dvz/Uhh7KIpxJYrLTWuhoHDLsy0HkKkzYYDy5nwPaAMKRQD82Xj3NxITvGR5PVbQe4BqJ8nMhTzrDj6s" +
                "zFS+UWAX+UbfOzUk/WHAA2eSIdhy6yNNRk55fcJe/UsBC7IUA0CmOaQ+DZOWmB0sSkg6+K5Bv/CBmCxq" +
                "pbDyBpvyK6mUR5sBHmiUllGd3Mb6ONKK5tgUxcDVMMuQ+2M43rpgyDLBx7DkEJ2tzdNEgsJUQee1dMIL" +
                "sTqLF3HUDKMU+C1zbZHPT8CkwaWIZt4MVIhjQSvto44YzMlBN8gQObcrj2fK00XHG7nWbWFPQIiOqkCv" +
                "yYmcr0LfmYOfdMop2J3/zSdz8eeTqLq0sV3ahrCcxT6d13SOn/4oDRSF/FWG3G+bJ/JVChqWLZdgTdkn" +
                "1PmZZfoGh9MpmZjBLhi7QEy5Ml3Q4T8mDQh2zlctSPnZwj0Ndxz+dmgNVMHqKZlrg1MB8ZR6kEFMud/G" +
                "IiErP3LX9xSzmwcGDjY9N56yH88q/TrlKgmp+s51pui0lDJxvLHnZvX4gdITDStIlH42ZE/cCqgEzFKu" +
                "FU96oBBXxh/0NAgBHBwbqkMVhONN9yGc2AMR0hkyTEhTbecxHYhaOBdReRvPRIQfpezvwujmKlyueV68" +
                "OHiHaK/VlJe21O65Vp2beppilSeIfks3K8KhlMTS3JKhi3CJKGBz6N4lnqUce+KYFCzOkwzC3ZbrMygP" +
                "mFq7YK+sFQhdYCdpiBak5Y57mDKqJqHgDrlJr93MAb1Qj8CawVTErFIaWiEOzw7itikEQpwC+UPfxHkt" +
                "TDRd1ZGZLsgtLJqjthtH0CapwjNMmW1pu0wlfMMHEVNdxVujIvEIzc3vKkct5VCmclQSKSQwsJuU2tmo" +
                "eLH05WpDK5BD5+Im1Zt0rwywtOBJZqz3/bNnq8A2HwPOqVqwcz3X0pET7dWrxpJf8AHLqxXkIZkSoQM9" +
                "8hHO0V553lYudUrfrhVbCObrmTTUO5KUSpfiXwJsMRYpNdfMEnMyRRSIp0ReDk9tKMYmaEdV73zZrosz" +
                "GzTQfZrDTCrWK3PeUhITjXcl3E4hTstWMV4qwzkzxiOmlsCu3TucFFTgmmWIqQEEVg2425UyywZmfATg" +
                "K/tmJy58WUXzBt2GLo9AW4zTVYVFgg14ns+2v9FAYK5t5WIDxFCwD4L+oijmLotEeFSj7xrXIj+01UPM" +
                "4ssqhb0oMlXbsjrwJ1w0UqbCrwIEtnsb68IkeJ4Z44k0la6ceCkegbJnW6jvexcX3ee805iib22zeaZX" +
                "PKpKb+NMpyusjbHlzrDzOlTQtW8heJGX0NFADp5uGkYSR0d2s3H/avSx331hOVuvMZZhlZt67mggYgMw" +
                "kW7cePPLHLuqnBc5bkEfFVav8WJb98QH63Lb3TvSRm0InhvrEFbv1CEc0p0Vq0LX5roT9kTNc25hj3Av" +
                "iHhGJygykLCLKWXQjZQBgUaWTBLRKRM5WqvM9wJ41U5lWLl6WO3eP1rs/HrQae3/01+CDwrxguk/v9h+" +
                "sYDOv3wwRnGVxitzyo820EGUw4EWtrpG8QAGy80V3sfBGcqCT1j8iIFH/WAvdKDSqEVulB/hVzc544s1" +
                "hKKcU2XOthYQz1IRzXxWADQlzmhWJcgmZZq0/TQZDZ/hlS87fvutd3Vpr+p08JaRs6qo9IhKl+ouzPmp" +
                "CI0YbSXgUk9H9KnWiNMd2ifPovmP1jdQ5tyoM/GXfxygoA/ODs6xJLp4c9AWB5nWOTxZ5vn67NkzaGFk" +
                "AkLPD/7nL5bJjIqtVPP0L7Vhk7VoqyJUUkUOWHnG+QEsikPquG+UsveP5wm47ixOoE/q1HNrzXRDupSD" +
                "cnSN98UbNhLCgnxhILBb8+CMzIxvpro5qjmjk0Kg0TJMnwVhOhNeCvwQBQEPm4I4e/XjDy8tCCZqHjYA" +
                "4H2yD9xuk18uBejPKDz68vqqbz75I3nvQCx62k4cbBbm9LV9hMeNZ+LVy9MT/owXwBAkxmrZwUCpsNFZ" +
                "1HyOxQ0y5HZx56f29UpHRYIANGjI9frA2zia+2PN+h+qMICmC3bfmb6DxnKNltcW4RZKdKr6Qhy02mml" +
                "65sy5Y/3wMzclBIKo5mrFwAZJgTM/+SbXH8/b8N/nZa9DPhm9Gv3hbvze/2+P+53T+zH898uB8OL/rh7" +
                "6h6Mhv3uS3ctzMUtykJIk4XC5y0HFMWQjo074C9By+OVEsKtwVEXkl9dUAE748Exdj50Gs1C4ISO4rpz" +
                "4eugXHPAya9lbRTfAuNEKjchv7bFb3wW8HuVZmnv6iUqXeR+WF2LSjixpatx9iUIvVPKNvi1+7zy6Tcv" +
                "a/z0O4i6ShLL31JFEzRUOwZS+Jn6W4BtG2QoPjPfmYziAkmwzRJbUKem12Dcuxh8mAA91T2dkgknKpj/" +
                "woOlwqZDMw0aRLpakk5y7Fa/CwkFSUeUE8ga3uB9f/Du/VQcIm774ajkiQ//KxIveVrWOjTnC+IQfeGI" +
                "98Oo5/Zh7uw+/KGyz0O74GTSyY7VZ/ua3Xue65SHC+4V3uPyvUHTJ/HUKM6olea7jnm8Lm2IZIrrsWNF" +
                "ey/WbXtG9p0VaqvhiVZ+3qQazINxVTz1HnApGAR8nBB3v1+g7jW7d4iJxWpzoEbawoKB3/MlBZR2ZWLa" +
                "ES2e/PrD28pIvwL3VAzGqR+H1oZc1UsWknVcZ/crc93HT0HYirrEUyEVu08MENCVs/NlsUwXUE/8dyXC" +
                "3sqkUNiozflOdnmNC3jEY1Iofsynzy3cY2oR0JmUxdWywcOOAt0K16Hd4G0fAiBqdsicjup50ROJyrGx" +
                "Q2SOrQNTEsV/k/bplOlUdwGNFZ+EWurhd94c4JN11bbDgHJY4CcK8k58h5PE70T4J3yL8O+qUFlSnHXB" +
                "wNX80/PPOJz0H1/gx9B/PMGPkf94+tmfX3x6+ZmePZYAvjIIbJym7jxMbSxxhkbO+2iK+wrdLsLQ5YK8" +
                "cmecD9j9vQEVUzvoHfFT2x3KwFv4IEP8gxVuxM1n1JKuQ/M1l89+Cl/Zi1OZu3TecXWoH57YaIDZz00l" +
                "cLaIlwwy7GTqZ94UIxpcdoD11o6bOeb+1Rxgp/Kwxtb9SztR4UYTkPsDnBS5C/mPo8wvXNr/6qSaLbGE" +
                "uj/brt0PqqxsXlKt4ngiq32AtPotF/dXLHjflIzH/q1q4yTfXslRnF7oT4DpRswOy3T6Nc3LEruLDnuD" +
                "5RhE5Q6wSlyHfInF3d9pXhg6ck7IEOVlpgoKOiOK0zApIkWjVrpuUqlmzLPSip/VbXffXhi1/kIeZNsO" +
                "+8hjq/hS2/+pXcPtHBgO04XNtlUfJGSdB26VPKDN/0xA/BIxTidNteKQxJlY9V7aIZYVWrzG6+BH33Z5" +
                "xg+C7GBVlofdZQR0pqnxjkTjYu0D128q4ezeFmktrv1r+9St7AsR8X8BCoaXhbhAAAA=";
                
    }
}

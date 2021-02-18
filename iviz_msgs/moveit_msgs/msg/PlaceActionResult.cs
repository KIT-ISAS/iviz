/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceActionResult")]
    public sealed class PlaceActionResult : IDeserializable<PlaceActionResult>, IActionResult<PlaceResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public PlaceResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlaceActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new PlaceResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlaceActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, PlaceResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PlaceActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new PlaceResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceActionResult(ref b);
        }
        
        PlaceActionResult IDeserializable<PlaceActionResult>.RosDeserialize(ref Buffer b)
        {
            return new PlaceActionResult(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a93c9107fd23796469241f1d40422dfa";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+08a28aSbbfkfwfSmvp2uxgkthJZsZXfCA2SZixwQMkOzNR1Cq6C+h108V0NcbM6v73" +
                "ex5V1Q9wktWuvbrSzWZjN1116rzrvJj3SkYqEwv60ZBhHus0iafB0szNs3daJuNc5msjDP1o3CQyVCNl" +
                "1kkuMvrR6Pyb/zSux+/O4byIcXjPmB0KQCSNZBaJpcplJHMpZhoQj+cLlZ0k6k4liORypSJBb/PtSpk2" +
                "bJwsYiPg71ylKpNJshVrA4tyLUK9XK7TOJS5Enm8VJX9sDNOhRQrmeVxuE5kBut1FsUpLp9lcqkQOvw1" +
                "6o+1SkMl+pfnsCY1KlznMSC0BQhhpqSJ0zm8FI11nOZnp7ihcTjZ6BN4VHNgvz9c5AuZI7LqfgX8RTyl" +
                "OYcz/srEtQE2MEfBKZERx/RZAI+mKeAQQEGtdLgQx4D5zTZf6BQAKnEns1hOE4WAQ+AAQD3CTUfNEuSU" +
                "QKcy1Q48QyzO+BawqYeLNJ0sQGYJUm/Wc2AgLFxl+i6OYOl0S0DCJFZpLkDnMpltG7iLj2wcvkUewyLY" +
                "RRKBn9IYHcYggEhs4nzRMHmG0EkaQRw1HkkbH7SLBv4Kkp3DDzwfBfyDMxZ+uOkNLvuDd8L96Yjn8C+q" +
                "paJtYiGN2KocFXKqkD8hC94yiM8GmWd3YAcMs3sx6X/siRLMF1WYKJF1lgFnQQmnCnn0TYBvRr3e9c2k" +
                "d+kBn1YBZypUoNqgliByUA/8BLTf5ELOctDkOEfqMxSQuic7SOcN8YU/h/B/UBLiAiscWOUqUQghzo2D" +
                "AogeT1S2BOtL0BXkqmlRHn+4uOj1Lkson1VR3gBkGS5ihWibdYhcmK3RD+xjxEPHdN8MRwVf8JiXe46Z" +
                "aiI9WpNaFrjvPSlaq6+yBrXCaDCDmYyTdaYeQm/U+6l3UcKvI17topepv6swf0ADyKD0Oq+rS+vrOE5V" +
                "KMGnEkx/2Br8ZC4BU/QQ4Knj9E4mcfQQAVbzvKV0xOsn0DyveqnOyQgL5fPC8xy+6F5dFZbcEd9/K4JT" +
                "BVeV2ovht3AXZLIrrSrS6SzOlnip4fWRl70AYaKiChFlNfnh30DEt7EZlaJifnwAXhsP6MTVcDwpg+qI" +
                "HwlgN3XMsLcHQBIRSA2BKGaC9CxAKG2OAgwoeBIR36bfYHsGYWvkNrJ0EwP5YDkyrbnOxmE3SfSG4hFc" +
                "CKaQod36ywqQsRcV2pgohVW4JVLT9XyObLSLcnWfN57wKutfNlgDOASxTDI5ihvpoTsZWLpZxBBb0H1c" +
                "cimkHSrCWKhPocva3jF1PsF+laL+AJXKIIMgxFHLFcgqSWA3wjQsvI2Coz1op3qgkipDl0IYlUMFiz94" +
                "FxtegCsG9LZVKcyUiqYyvEVthB0cv0I4aYycKxaNWakwnsWhMwbCwLQtdIz1eAEgtVyTUYCfi2FV2wkP" +
                "g5BHEt0SVDHOWW6lQBzOu4Y3/byXZTq70Ei9wl+DEH6HtyM91TkZFrBe4hWgs21AVuzeTvznnz7XFs2V" +
                "aVjiqu/gnDCLVyhhWMGpwZWGYBpFvsKnILGP8HqWaJm/fokv0hRgBSTDx2dUnTUNDsHhLjpoHIBA0bxB" +
                "/0CSC3kX6+zALqCwYjzuvHAfvO32rz6Mep0f8Q/u5Y9vrrqDAdwKAb7vXXZO/Ib+4GP3qn8ZXA8n/eEg" +
                "wIWdk1P3tvRpYFd24f4O3vwW9AYf+6Ph4Lo3mAQX77uDd73OyZnbdzEcTEbDK3/cS/fiw6D75qoXTIZB" +
                "95cP/VEvGPcG4+EoALDdzskrt2zSv4ZThh8mnZPXngYX9XVOvmeuOCmJ/xK34FKXIMXQGztrl/GcmnRH" +
                "kwD+nfSAkuBiCLfkGGgDVjzft+Zjf3gFP8fBTXfyHpYPxpNRtz+YjGHDi4Kx74bdqzq808rLLwE6q6ws" +
                "vXO7UFIvi9OcsN6Nhh9ugkH3Gnj+4tXO2xowWPO6vmY0fDO0pMLr7+uvIYL42cH/of5y+AajOPf6R5aF" +
                "2YITXNaY/nYEawJAYzB+OxxdB047T05fFJpiGQdK1Lv4GXUUdOQjLERFgZWemyWU8V966Rlo1ag/eDv0" +
                "L18yZiXVqGI3GAb9n4Px8OrDhAR3Bkg9ga0Xjs6l/c65Q4SEF4eBKBSukiX7KIjWbWiV4UZK21RLxG3V" +
                "pk9X2sTk3YSeUS7ydw3kGQpmIKm9BXIhQjDgaOn0n/Atu1laGBA85tRPvJMutyXcS+BrFNwgSR5DtCku" +
                "h2+FhAuuuFwWcAFWoV/jYlhYOoX2B5GeBTvndSH0DhcAKNRJEhukVk/RcUMSL907l1ogLcLm9MSJ5kHD" +
                "Abhw+4e0HW4Atz3woAMLmo9+m0i4BdMIyyoU8iwUAM44bjChSqlewMku1j8yiAJyqnPAnR3FsxnfxHA7" +
                "Azdyj6QmILT/oFTUWWqDKW68XEHmJSFco6KQqzpQ2OzIneoI445jhxAsxEAQ60CJktm+xXRBzAAv1rLz" +
                "8xDikPPz0oVqQ5P1KmJqIbwh9POS9gE7p1pjLBogfY9mCvuVscQs6e2BFHGhk8iWVoAJfJ1POeQjVWLS" +
                "DSQY8Atc+eD8yZAyTUE7W0NbNA6L8JE3KeAhvxbHmbrTyTqnFHCVxYb8RROxidQM/AeGiljgEn+t2Bwd" +
                "uVAeiowQwLLZKpbeKQgv4ny7u/SZocXPTJNstdiiZiCt3BfZ5GqVoLXFaRnAYIm7B802EdYraMHYloI9" +
                "VLgIcgO2VQh10TmkcqksI7ia6s3Z7I2i3XnENeNCXlCwCIJpARnFzhquSGowICdGciNsSm6rsAmgodpZ" +
                "TEXKPd7QxBDXWt12bkhmmdyaFp2AZkRixPJnlcOEDIq9Um4CLJbyVvEmux5oRw3TFCPKpC3+hhmAas/b" +
                "YqvXmfOnREWqAaCVT63Kh7CWLdxCiR4Y650qi5PwFphJbK02IveYGpZtiXab3SwcDOKTif8Exw8kAyMZ" +
                "TslqcBVkTFuXUHgdKDKLgjmoBR5pYHQGcsnxEicJtiEQfl+puBeRNemPjZLh0RlC6SOn8KWPmANP4VB2" +
                "7x+gaqSwSg2WIJ3Nsg9A7bHCJV54FYvUHHI+UrwZ/BJpkCvAmWlMoJ3HBpaswxyLXbCsOI8VmXNMYP3a" +
                "1zuks08s19toiThvVlTHxiQQE5fU4N3Pe+YqL+wfwMpE29N9UCPCBYQMbfEWTeFeYl2ohf0ACI1l5pyF" +
                "JA37MLp8Sz7tDK/y43tQVvgrN1gHRw+UL7RR/BI1GLWsVN8vY8eMhB9ZDFB4L1p05T1A5RUOGhg0pDGU" +
                "KmN+CwRXcPh/N/a0bmyTqRS2f6sbc8v/L7mxh7wYR6K43TTmCgK3PNuyA5k4FeYqAv++s2gDAsUF+LP2" +
                "7m/EJnjJ/Hosp/cA1o6TmXN51hy8W5mqfKNAL/KN3mkUkvzQ4YExyRB0ufGRiihnvD9hq/5lDRuyFB1A" +
                "pnNbOHkKIi0ye0iUcOnguxr+wjti0qilwsgbdMrvpFAedQZooOpZRnFyC+PjSCsqXZMXA1PDW4bMH93x" +
                "1jlD5gl+DFuO0dhaXECkVXhVUIuWmrrgq7N4Hkd1N0qO3xLXEvnsFFQaTIpw5sNAhFgJtNxutkV/Rga6" +
                "QYLIuF14PFUeL+po5Fq3hG16EB5lht6QETlbhbwzBztpF0Wwe/+bv8zFn08i6kLH9kkb3HIW++u8InN8" +
                "+qNQUGTyVwlyv22eyFbJaViy3AVrijyhSs8007dYj05JxQxmwZgF4pUr0zn1+/HSAGfnbNUuKZ7tuqeh" +
                "jt3fHqmBKFg8BXEtMCpAnq4eJBCv3G8jkYAVj5z1PUXt5oGCg72ea5+yHU9L+TrdVRKu6nuXmaLR0pWJ" +
                "5Y0DV57HB7qeqFhBrPS1IdtkW0MkYBZypbjSA4G4Mr63U0MEYLBvKBdVcB0fegjuxPZASGZIMAFNta3H" +
                "tMFrYV1E5S1sgwhfSjncB9HVVThc87R4dvAJ0UGjzi9tsT1wqTon9VTFKpqG/khXK8KilMTQ3KKh1+EC" +
                "QcDhkL1LbJ+ceOQYFQzOkwzc3ZbjMwgPGFu74aDUS0Bwga2kIVjgluvwMGYUTVIrIdoZxgG5UI7AksGr" +
                "iEmla2iJMDw5CNteIeDiFPAf8ia+18JE03SOzPSazMKCabZcOYIOSRW2LWW2peMylfBQDwKmuIqPRkFi" +
                "18zV70rNkqIoU7RasGUJCAb2kEI6GxXPFz5crUkF7tCZuE31Jj0oHCxteJIa6659dm0U2OLO34yiBVvX" +
                "cykdGdFBNWos6AUbsLRaRh6TKhE4kCO3cJoHRYut2OqEvl0p1hC8r6fSUO5IXCpMin8JMMWYp5RcM0lM" +
                "yQRBIJwCeFE8ta4Yk6A9Ub2zZbsvzqzTQPOpFzMpWC/VeQtOjDWOR7iTQqyWLWOcI8M6M/ojxpaW3bh3" +
                "WCkorauHIaayILBiwNOulVnUIONHsHxp3+yFhS/LYN6g2dC8CKTFWF1VGCRYh+fpbPkhBlrm0lYONoAN" +
                "a7ZBkF8UxZxlEQubFfxucC/SQ0c9RCy+LGPYjSJT1i0rA9/hopIyBX6lRaC7d7FeGwgR1X2MTWgKXfni" +
                "JX8Ewp5uIb7vXl52nvNJI/K+lcNmmV5yqSq9izOdLjE2xpQ7w8zrWEHWvgXnRVZCrYEcLN3UlCSOmvaw" +
                "Ue96+LHXeWEpW63Ql2GUm3rqqCBiHTChblx588sUu6icNzlqQR4lUm9wlq1z6p11cez+E+mgFjjPjTUI" +
                "K3fKEI5pTMWK0KW5rqmeqFnOKWwTzwKPZ3SCLAMOO59SON1IGWBoZNEkFp0xksOVynwugNN1KsPI1a/V" +
                "7v2j+c6vO53G4T/9R3CjEGdK//nN9g8z6OLLjTHyq1RemdH9aB0deDksaGGqaxQXYDDcXOIIDtZQ5txh" +
                "8SUGLvWDvlBDpRaL3Cpfwi8fcs6zNASiqFNlTrfm4M9SEU39rQBgCpjRtIyQvZSp0vbTeDh4hlNetvz2" +
                "W/f6yk7ntHGwyGlVVFhEKUt1M3K+KkIlRhsJuKunLXoUa8TpHumTZVH9R+tbCHNu1bn4yz+OkNFH50cX" +
                "GBJdvjlqiaNM6xw+WeT56vzZM5ykSIDp+dH//MUSmVGwlWqu/qXWbbIUbVSEQirxASPPOD+CTXFIGfet" +
                "UnbkeJaA6U7jBPKkdvVurahuSHM4yEeXeF++YSUhKEgXOgJ7NBfOSM14GNXVUc05dQoBR0swPQuCdC48" +
                "F/hDZAR8WGfE+asff3hpl+BFzcUGWLiL9pE7bfzLlQD5GYWtLy+v6uHjP5L3bokFT8eJo83cnL22H2G7" +
                "8Vy8enl2ys8484VLYoyW3RoIFTY6i+qfY3CDBLlTXP/Uvl7qaJ3gAio05Hp15HUc1f2xav0PRRiA0yWb" +
                "71TfQ2K5Qs1riXALITpFfSEWWm210uVNmfLtPVAzV6WEwGjq4gUAhhcC3v9kmxx/P2/B/9oNO//3Zvhr" +
                "54Ub87153xv1Oqf28eK3q/7gsjfqnLkPhoNe56WbBHN+i24hxMmuws8bblEUw3VsXIO/WFq0V4oVbg+W" +
                "uhD98obSsnMuHGPmQ91oZgJf6Miue+e+joo9R3z5NayO4lsgnFDlJOTXlviNewG/l3GWdjwvUek898Xq" +
                "ilfCii1Nw9mXwPR2wdvg187z0tNvntf49DuwuowS899iRRU0FDs6UviZ+sG/lnUy5J+Z7kxG8RpRsMkS" +
                "a1C7Itdg1L3sfxgDPuUznZAJJgqYv9TBXGHVoZoGFSJdLEmdHHvU70JCQNIWRQWyAjd43+u/ez8Rxwjb" +
                "PjQLmrj5X+J4QdOikqE5WxDHaAtNPg+9njuHqbPn8EPpnIdOwcqk4x2Lz+Y1+8+80CkXF9wrnOPyuUHd" +
                "JrFrFGeUSvN4Yx6vCh0inuJ+zFhR39erlu2RfWeZ2qhZouWfV6ka8aBcJUvdWVwwBhc+jovbzRcoe812" +
                "mpgYrNYLaiQtDBj4PQ8pILdLFdO2aHDl1zdvSyX90rqnIjBOfTm0UuQqD1lIlnGV3K/UdR//CsJU1F08" +
                "JVQx+0QHAVk5G18Wy3QO8cR/lzzsnUzWChO1GY9hF2NcQCO2SSH4MZ8+N/CMiQVAPSkLq2Gdhy0Fuh0u" +
                "Q7vFaR9aQNjs4Tm16nnTE7HKkbGHZY6sI1MgxV9D+3TGeKr7gMqKT4It5fB7Jwe4s65athhQFAt8RUHe" +
                "i++wkvidCP+EfyL8KhUKS4rzDii4mn16/hmLk/7xBT6G/vEUHyP/ePbZ9y8+vfxMnz0WA75SCKx1U/c2" +
                "U2tbnKKR8T6a4L6Ct/MwNFxQrLUepZgbUDGlg94QP7VcUwbewoMM8TsqnIibzyglXV3NYy6ffRW+dBZf" +
                "ZfxVFP4WAsWhvnhivQHefq4qgbVFHDLIMJOp9rzJR9SobAPpjT2TOWZ3NAfIKX1YIWt3aCdau9IE3P0B" +
                "VorcTP7jCHNnUrakgF+rVLMmFqt2a9uV+aDSzvqQahnGE2ntA6hVp1zcF1dw3pSUx349tdbJtyM5iq8X" +
                "+tYvTcTs0UwnX1MfltgfdNgJlhNglWtgFbCOeYjFze/UB4aazgh5RTHMVAJBPaI4DZN1pKjUSuMmpWjG" +
                "PCu0+FlVdw/twKi1F7Igm3bYjzy0ki21/LframbnlmExXdjbtmyDBKz9wFTJA9L8zzjELyHjZFIXKxZJ" +
                "nIqV59KOMazQ4jWOgze/bXjGF4JsYVUWze7CAzrV1DgjURusfWD8puTOdo5IK37tXzunqmX/UY9Y+foS" +
                "mSj1oHx3cZ5Js9rbgqJml+1YuKlFyhFR+x1T7PbDUjQquQ8JVwFfT+h+vt4zxEMC/nbVTt+wPjyt0ujE" +
                "dzormGALU2JeyDMyhTNjJ0cNUz8lnWywNF8IWiVGbbgKhOXR2mhac19jZmz/ywoe8RLWcrXKNBmK5kr8" +
                "uyxerVQ2KY0BgbO0RLvVxf4MjlIYmn9he8E0u5q3YwPDTvX5nEJPQefCRPmvInL5yqilTHEqdKdQfuCH" +
                "PG0FFEeFsIKpomertcEf1JV38Ueo1xnn8SQKKh57T2e77QH3xn3r8skyYisoMgB8dv1Lpx/770P3n6+o" +
                "3XQEAIX9FAa8K3aM9m39rTpUVpgCbWn5TvMqDm/BC2P0icoicmluqX49K2aA6fcknuW2ZWkbBnpG2Zb7" +
                "trtTU5fK8IuZhUwyd8MDflDOmW0J1bot2cEcZ05+awka98Iq9EYxViNDZQcGzk7dqqB44/Yv42K9TXlt" +
                "T640zjHD8QckaOqLarifXQsOX2FvRuY5dvDQqbljAXjpyKecbSz0enfE0U07/Suq7mDwuGSj8b9CA+Jc" +
                "WEcAAA==";
                
    }
}

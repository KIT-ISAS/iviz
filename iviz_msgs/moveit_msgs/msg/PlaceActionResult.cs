/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlaceActionResult : IDeserializable<PlaceActionResult>, IActionResult<PlaceResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public PlaceResult Result { get; set; }
    
        /// Constructor for empty message.
        public PlaceActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new PlaceResult();
        }
        
        /// Explicit constructor.
        public PlaceActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, PlaceResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal PlaceActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new PlaceResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PlaceActionResult(ref b);
        
        PlaceActionResult IDeserializable<PlaceActionResult>.RosDeserialize(ref Buffer b) => new PlaceActionResult(ref b);
    
        public void RosSerialize(ref Buffer b)
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "a93c9107fd23796469241f1d40422dfa";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1cbXPbRpL+zl+BWlWdpItExZLtJLrSB1qibSUSqZC0N47LxQKJIYkVCDAAaEq5uv9+" +
                "z9M9A4AgZXtrV0pd1Xm9lkDM9PT0+xvz1viBSb2Z/Gj44zxM4igcDefZNDt6k/hRP/fzZeZl8qNxE/lj" +
                "0zPZMsq9VH40zv7NfxrX/TenOC9QHN4qZjseEIkDPw28ucn9wM99b5IA8XA6M+lhZD6biEjOFybw5G1+" +
                "vzBZExsHszDz8HdqYpP6UXTvLTMsyhNvnMznyzgc+7nx8nBu1vZjZxh7vrfw0zwcLyM/xfokDcKYyyep" +
                "PzeEjr+Z+WNp4rHxLi9OsSbOzHiZh0DoHhDGqfGzMJ7ipddYhnF+cswNjZ3BKjnEo5mC/MXhXj7zcyJr" +
                "7hagL/H0s1Oc8Z96uSZggzgGpwSZtyefDfGY7Xs4BCiYRTKeeXvA/OY+nyUxABrvs5+G/igyBDwGBQB1" +
                "l5t29yuQifapF/tx4sArxPKMbwEbF3B5p8MZeBbx9tlyCgJi4SJNPocBlo7uBcg4Ck2ce5C51E/vG9yl" +
                "RzZ2XpPGWIRdwhH89LMsGYdgQOCtwnzWyPKU0IUbwzBoPJI0PqgXDf4Kzk7xg+eTwT86ZdGHm3bn4rLz" +
                "xnN/zrzv8S/F0sg2b+Zn3r3JKZAjQ/qMlfGWQHo2eJ5+hh4ozNb54PJ926vAfLYOkxxZpikoCyEcGdLo" +
                "mwDf9Nrt65tB+6IAfLwOODVjA9GGWILlEA9+AunPcs+f5JDkMOftUzLI3IkexNNGiejmnx38H0IiVFCB" +
                "g1YuIkMIYZ45KEB0b2DSObQvoinIzb5Fuf/u/LzdvqigfLKO8gqQ/fEshIkIIIdjUmGypB3YRoiHjmm9" +
                "6vZKuvCY51uOGSVy9WApYlnivvWkYGm+ShpKRZZADSZ+GC1T8xB6vfbP7fMKfmfei030UvMPMyZ+W9Gh" +
                "QiXLvC4uB1/HcWTGPmyqwCwOW8JO5j4wpYWApQ7jz34UBg9dwEpeoSln3ssnkLxC9OIkFyUsha9gXkHh" +
                "89bVVanJZ94P34rgyMBVma0Yfgt1wZNNbq0jHU/CdE6nRvdRsEHsMjExwdolqmLy47/hEt9GZgrFmvrp" +
                "AXQbD8jEVbc/qII6834SgK3YEcN6D0DyAnCNQIwSwS9IQChNjQIyCHgUCN1G36B7GWEnpDZJugpxfWgO" +
                "zlo3nY2dVhQlK4lHuBCqgF+S0lkBGeuoqGNeJazilsCMltMpyWgX5eYubzyhK7u8kCjJ+l1HpCwnu3kf" +
                "8ckg6WoWIrYQf1wxKSIdJmAsdCmhi0RXW+iE/Sam/OCWJiOBEOKY+QK8iiLsJsxMmbcyOLoA7UQPImlS" +
                "mhTBqBoqWPxhXWx4AVMM9O7XuTAxJhj541tKI3Zo/IpwMsv8qVHWZAszDifh2CmDYJA1LXTGeroASM2X" +
                "ohSwcyFWNR3zGIQ8EuvmEMUwV75VAnGcd403l3k7TZP0POHtDX8djvE73vaSUZKLYoH0Pl1Akt4PRYvd" +
                "20Hx+cdPtUVTkzXs5dbf4ZxxGi7IYazQ1OAqQTBNli/4NIzsI15PosTPXz7nizgGrKHw8PEJVSdNQ0Nw" +
                "+CJKDZUb0gc+zvzPYZLatxJT9Ptnz+zz69bl1bte++wn/mnYD2+uWp0O/MGQb9sXZ4du9WXnfevq8mJ4" +
                "3R1cdjtDrjs7PLYvKx8O7cIW/Pbw1Ydhu/P+stftXLc7g+H521bnTfvs8MRuO+92Br3uVXHWc/v5u07r" +
                "1VV7OOgOW7++u+y1h/12p9/tDQG0dXb4wq4aXF7jiO67wdnhS4e9i/TODn8gJRxfvP/wbmFE5+AbIn5n" +
                "/EWeHO36g1ZvMMS/gzauMDzvwi32cSlQ4PstS95fdq/wsz+8aQ3eYnWnP+i1LjuDPtY/c8R8021d1YEd" +
                "V999CcpJdWHlldtE3jxv1Ljzptd9dzPstK5B5Wcv6i9rkLDkZW1Jr/uqa6+Itz/U3iJQ+MUB/7H2rvuK" +
                "sZp7C3mCWbmHoZuvk/l1DwuGQKDTf93tXQ+dEB4eO0EriAVxaZ//QlmEPLzHOgoFFjoKVnDlv/LOEc0K" +
                "zGXndbd4B2LtVMVgDa9Od3j5y7DfvXpHSYaIgomPr8elEXMpvTPciH7oFJDjx3ATc7U/iMRt2JRyo6Rk" +
                "5sALm6Ypny6SLBTL5SUTyTP+keB2mQQqSFhvswacfwYbKof/zJdqQWUd7SIQASbyxnqtORwOzIiBa4jy" +
                "EGGkd9F97fnwXKXXQLHCrIG+5lqsqxwh24dBMhnWDmshoB7PAGWcRFGY8Z7JiOYYqbnv3rmEgbfwbKYu" +
                "NNhvuP3nbntXdsOsu91wGfbV0ELmua8jH44tDlgpkShmZgCVwQ5jqjHqKvT7mr+ypJHCsdNvMyPygnAy" +
                "UecKhws65AWGiQCR/ZUyzTzJmLSG8wVyKR8BmJR5XB1BAmF31VESMJLYc/hgIUM7VnYig3LDlsW0+RNg" +
                "paJ1ejpGYHF6WvGQNtZYLlByEH+eK/J5ReT2G6MkYWw55OUeS/q3C2CFUn6hAiJ+syRC0UZrX4mn3hkM" +
                "IRFEgvTiGfIF/AIPDssuupOC5aCPKkATBa0yGtRNBgTU195eaj4n0ZKfp4i1wkwMxD6xCcwEBoORH+tV" +
                "KCtV1UyOxLOD4gcEMN8/KJeiXoY4Lb/fXHqUyeIjFLmonuUWMwGv8qJm5i8WSAdQNoirADpz7u7sI0DF" +
                "znZ5F4aqErtR2gKE+qqhiFxpD2JUlCwhtDhaKHG2NSh25wnVMhfBQrxQ62p6SBA21miBMYHyODaK8VA1" +
                "clt5hHBQSmE4U69RN4BZiDDVSrYzPn6a+vfZgZxAHRI2spq5TmFBhmxfqx4Bi7l/a3STXY+7U8ISCfn8" +
                "qOn9nQG9aU6b3n2yTJ0JlVvECQBa/tSKdoQ1P+AWydugqbx1yU7B22NicG+lkdTT2yhvK3e3yYpSztEp" +
                "C/+ErceVQUiFU9EarkICdO/yg0IGykShJA6loEAahEZJbZzTXwsHm4hr1wvoZaAs8mODXjw6Rah85AS+" +
                "8pFS4CkMyqbbwa16hkVnaIL6ULBdbQClxzJXaFGIWGCmSOFE8Cb4JUjAV8CZwKIkK2euQZLlOGftCsvK" +
                "81SQNWUE6ZdF+cJ3+snquw2MhPLZQsrSzOmYh8QZ3b3umZq81H+A9aPEnl5EMd54hiih6b2mKtwha4wg" +
                "I77EvXAV1ljAY+HYd72L12LTTujA9+4grPjrr1jWpgVCnQzVLnlJCaaUVcr1VeyUkPiRhoCie6nRa+8B" +
                "VVc4aFBo5CWS+TJdxYXXcPh/M/a0ZmyFOjq2f6sZc8v/L5mxh6yYBqDcnjWmBkFbjsRfDMjAibAWBfT3" +
                "jUUr+CUu4M/au78LmfBS6fVYRu8BrB0lU2fyrFcvzMrI5CsDuchXyUbfT/hHg4eYAGUO0O+91EROdH+k" +
                "Wv3rEhvSmAYgTdSkPs0lLTJbrujD6fBdDX+vMMQiUXPDsBsyVeyUMJ4ygztIMSyVKBn5VO4FCeiBsFus" +
                "GFSNXkaiGJpjiGOVJvwYW/aobAdaD5RVdBXScZUeLWx1Gk5RzKuZUTH89nIHXj45hkhDpQRnPQwsZGHP" +
                "Unu/6V1OREFXvJAotwuPGRhbvKRBkSfJASuXFsQ6QW9EiZyuItXMoSfNsqZ1V/xWOHPvzydhdSlj27gN" +
                "F45Sq3Pnazzn0x+lgJLIX72Q+231RLoqRsNeyznYrMwT1u8zSpNblpdjEbGM6S9TQLpcP55K+55OA8bO" +
                "6apdUj7bdU9zOzV/W7gGVih7yssdQKmAvLgeXpCZw7ddUYCVj5r1PUW55oFSg3XPtU9Vj0eVZF18FSY8" +
                "wjuXmVJpxWWyruGK7fxdvBOqFELHohZkG2ZLhAHZzMckiJAJUTh+c32aDdzULlQrKVwmB+7AkthuhrCL" +
                "dxWQcWJrME0YLJZDTI5Aj2bJ1k92tsFz1RTNN4trFITQA4JGnVAK1PU7bC4v9aqy9Vec52pDrEABo2Rl" +
                "cUiWIng4GDm7zx7IYYGYosGQPMLgSnCvySWCAsXUbqj0AwhsqBUzepFJ0aNRrCQPlmZAsDFOA25IWqD8" +
                "oPfRS4rnmRNGcRWAtk4DRs2A7MiU1JONI2RWdB9pshRFsFD2D1wBQs6I0deE+U7v5bQULVGZyuGcCgMp" +
                "PZjsY9fLlukqvY6yCFN2SthxBHZDe4RjyspgJKkITmvMgMeceLdxsioSQbv+KXRyUxdbNuIT1xcIaYr6" +
                "nUvfRGfqAWLRb7PXtATcE+kRWOCetl72q50x3ef4jAktFQp65ZEP75tY6hTaoz+HLIdMY8mg9S56hQEh" +
                "EIyDXBZGrbVluWZL4O50VreFIotcCUmplyolHK/Ubx0B+gmnGdwxY1bD5kDwM4glFkfxlFU37hULAeWy" +
                "epCRrb2nXLH1tuNdm2y2DpWfYO1cX2yFw3cliFdUDhnrQLrLkin6OjYqyMrbHRSzBrLMpaMaRODyS9U0" +
                "cCwIhBewrjxiv4ob+oCxXEROeuCSfFdi1wqY1lcEQ6letKSkRCzBXGURZBQdu2WGsM/cIVIg+ogt1ZmK" +
                "wWk2RvcI2VsXF2ff85ieGNW1kyZpwgoC8qv4c5gm8ZzBLkuBsBD3oBLScMy7qSpIhT+HMmc1mQiDfT2p" +
                "177uvm+ja8g7LRa0U4xZnTTb8oY1rIK0zQS/dlcXY+smd09wobzkDefMzo6tES7P3H6cnHIAq7iykm9Z" +
                "LcH+ngyQWL65jNW1uyMzyTUbZTUE1ixLItIKpHUWo7SmKEKDkoGiKLQ5IYLdBbquLqTnzJtJGYC6hYl7" +
                "/VhG8etGpbHzT//xtK3HOc9/frP9Q+Kcf7mdJUZTCr0TcXjWkMGMsSbFbBVRgVTkGDGChSZlGWSqHZKi" +
                "SqDVesgJGyJrQcWtKUrw1RNOdbRF9pflcskkRVxgsTCDM3LGHlAcwGBURcU6WCmT/dzvdo44cWVrZx9a" +
                "11d2UgYl80KEYWYLBaikmG5erShpSH1QnbpzKE2vLVEDC+0bTBc9ktpNktwiXrk1p97f/nuXFN493T1n" +
                "ZHPxavfA202TJMcnszxfnB4dcaghArXz3f/5m16RY1FETwt3sbWMyj0b3ZA5FSowcgzzXWwKEexDC26N" +
                "scO/kwiqOgojpDjWPW2TV/atlIguZb54pbIhQHgr6r09WUteFC4dCnUFUBktZkHUXlY6NwLm1CsIIJ+R" +
                "BPisToLTFz/9+FxX0PVqhQDrNjHetSf1f71CpwohAttVBZ/WDu7/Eb11KxS2HOXtrqbZyUv9hM3BU+/F" +
                "85NjeeTIFRcgfE5WdgXc/gp1m9rHjFB4EXeA63Pq23kSLCO+l6pAnix2nUBDtB+rLP9QtACMLlRNRwnq" +
                "v9mCknbgje8RWkvQBnEzni0suiwHYuE6cRArV1BEhDNyIQCA0eDTpYsmauD8/QH+hxKATt696v4GN2YH" +
                "bG/etjF2cGwfzz9gtuGi3YMptx90O+0zmRgYVOyTeBniZFdplOZMAhpZSCts+71cWnZCyhVuD6tSRL+6" +
                "obLsVGu8TFeka6xEUFdNct05S7Vb7tlV5yYtbpsT4uKCqmYPvx14H7Rs/3sVZxJZEiYTTxEsWozqNohp" +
                "U3E/EL1Z0nb4GyKS8ulDQWs+/U4vXkFJ6W+xkmIX2U6ziZ+2j8k5XWtUxBTrvdE7DTFkmExsmqMS5PBQ" +
                "uMNe6+LyXZ8RUuVMx2SBSQbr1ymUKio6Un6QmqELD6XpYo/63fMRcDS9sli4Bnf4tn355u3A2yNs+7Bf" +
                "3km79BWKl3earaVXThe8PerCvp5HO+fO0dvZc/Shcs5Dp7CI6Gin7LPJyfYz4bK1GOBecZ6qiPPrOskG" +
                "T5hKCqyDheillTIkNOV+JpuU9+XiwLazvrNEdUpaI2YhUrXLMx4tNXVjcUkYLnwcE7eZBEjymW70GxmM" +
                "1mtfwi2GB/pe5wlI7UpxEyVsLdIWfdZK9b2y7qkuCFRcaW+tJFWdh0AtxPU5y+t+pQT7+C6IqaVzPBVU" +
                "mU7SQCC7VuVLQ9QcEUH8V8XCYqgS04Jg60QHoMshK9yRHU0EO9nHTw2eMbAApH1kYfGASuHO7XC5FyK/" +
                "JYMkGZiYbWSWoKV01XXTE5HKXWMLydy1EOUVSOkXwD6eKJ7mbih1wCfBVvLyrU1+bYJD0zS/L/P/okjg" +
                "33nfsfz3nTf+E/8E/BITmeV7p2cQcDP5+P0nVhSLx2d8HBePx3wMiseTT0Wr4ePzT/LZYxHgKzW8Wl1r" +
                "a9+ztsUJmihv9hfh7SyMjDOVa61FKSeVUPtj2lco4scD1z/BWzz4Y347RLPt7BO5lKyv1omUT0XNvHKW" +
                "ujL9EojO/0scWpRFrDWg93NVB1YHOdaEpkVWa0+LjajdsomrN7YM0WSbUzQc7Ss/XLvW5nxNsHTlB/j+" +
                "IWtAbhr+ieZYKwL4tSKzSmL+8Ia1SZ7KxvoUaQXEE8nsA5itT9W5L4xwKFREx34ttNZyt7MzRp2LfNtW" +
                "JvC2yKXj7pai9baQw07MHYJSrtNUwtrToTk3L1if7Nl3KqgryqmjCghp6YTxOFoGvIWdC6nEMtlRKcNH" +
                "65K7Yyc7rbaI/tikw35UQKtokmYCa5tU6dwylsSZN9Y1UIAhvNxqBh/g5l9jDr+EjONJna2siTgRqw6Q" +
                "7TGoSLyXnNbe/7Ypl6LsY8umftmVLu2fE02En1PWn6rTSg/MyVSM2cYR/DZVKRv/2jnrUvaX2sO1rw2J" +
                "ikoPqegJTlM/W2y2kKRNZfsPbrZQ0kOKvqOI7t2pxKF8q05AHRNNz1dNMI8Y6heaar2++ngzKv6HRWdy" +
                "DQu2HHFFne/1K1ZMrRv7m8UYc4SRrkpXxjNRZlZa+2EJtDY7VhdZZld9+x8yKJAuMMacTJqIdiRC8Ddp" +
                "iEZFOqjM6MBA2tu6xW4zuoIoKuZf2luSyi6WMDEupu2KBCIZQcTGCPrVxK3s9HOGxi/y3vFm9buYvbTl" +
                "TU7wsEJpgqPFEn2w4Eha5i7WGGPaR3N2oX/59UqKvzbDh9q8Lr7o8DjC/iB3RNj57FqOTiS2+z73n4io" +
                "eTUBoBx+fGXd5DjjeltpW5/0KqVfttgOM5izCMe3sLiMMyknXu5nmB3g0HAxlyu/R+FE6h9lKyuZSFrl" +
                "vlDupNPlLPpiYgGT4dbFFaNrTk2rU3pbR+ec/hQ7C1Da0Fq7aQCJ5jeZNc48OXaLhsULu3mO27vPbE5r" +
                "m2qVIYsJBxN4EftFFDuPJjYkREDPNguGCtiBgwi5IwG6ctzjCMKXKLV11tCNHf0r4u1g6Nxio/G/J1bz" +
                "77BGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

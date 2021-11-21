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
        internal PlaceActionResult(ref Buffer b)
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
                "H4sIAAAAAAAACu1c/2/bxpL/XUD+B+IZONtXW0nsNG198A+KrSRubcmVlby2QSBQ4krmMyWqJBXZPdz/" +
                "fp/PzO6SouQmD+9FxQHnprHI3Z2dnZ3vM8pbE0YmC27lVyMcFXE6S+LhYJpP8qdv0jC5KcJikQe5/Gpc" +
                "J+HI9Ey+SIogk19PGqf/5p8njaubNyfYMVIs3gpuTxo7AXCZRWEWBVNThFFYhME4Be7x5NZkh4n5ZBLi" +
                "OZ2bKJDR4mFu8iYW9m/jPMCfiZmZLEySh2CRY1KRBqN0Ol3M4lFYmKCIp2ZlPVbGsyAM5mFWxKNFEmaY" +
                "n2ZRPOP0cRZODaHjT25+X5jZyAQX5yeYM8vNaFHEQOgBEEaZCfN4NsFg0FjEs+L4iAsaO/1leohHM8EN" +
                "+M2D4jYsiKy5n4PExDPMT7DHf+rhmoAN6hjsEuXBnrwb4DHfD7AJUDDzdHQb7AHz64fiNp0BoAk+hVkc" +
                "DhNDwCNQAFB3uWh3vwKZaJ8Es3CWOvAKsdzjS8DOPFye6fAWd5bw9PliAgJi4jxLP8URpg4fBMgoic2s" +
                "CMB2WZg9NLhKt2zsvCaNMQmr5EbwO8zzdBTjAqJgGRe3jbzICF1uYxBHja/GkI8Kx5MGP+NyJ/hFFHjH" +
                "3zuR0Yfrduf8ovMmcD+nwTP8Tc40siy4DfPgwRTkyaEhiUZ695ZGujmuPfsEOVWYrbP+xft2UIH5fBUm" +
                "L2WRZSAu+HBoSKYvAnzda7evrvvtcw/4aBVwZkYG3A3OxK2DQ/gGApAXQTguwMxxwdNnvCNzL6IwmzRK" +
                "RNd/dvA/+ESooDwHwZwnhhDiIndQgOhe32RTCGBCbVCYfYvyzbuzs3b7vILy8SrKS0AOR7cxtEQEVhyR" +
                "CuMFVcEmQjy2TetVt1fShdu82LDNMJWjRwvhzBL3jTtFC/NZ0pAr8hSSMA7jZJGZx9DrtX9sn1XwOw2+" +
                "XUcvM/8wI+K3ER3KVLoo6uxy8Hkch2YUQq0KTL/ZAqqyCIEplQSUdTz7FCZx9NgBLOd5STkNXm6B8zzr" +
                "zdJChLBkPn95nsJnrcvLUpJPg+++FMGhgbUyGzH8EuriTtZvaxXp2TjOprRrtCD+GkQ1ExMTrRyiyibf" +
                "/xsO8WVkJlOsiJ9uQMvxCE9cdm/6VVCnwQ8CsDVzxLAGBJCCCLdGIEaJEHoSEEpTHYEcDJ5EQrfhF8he" +
                "TtgpqU2SLmMcH5KDvVZVZ2OnlSTpUlwSToQo4ENa2isgY20VZSyoOFdcEpnhYjIhGe2kwtwXja1as4tz" +
                "OllkAnVELJ3ygjfOI4llBlWXtzE8DLHKFa0iDGIiekQX4sCIj7WBVFhvZmQhHNTkpBEcHTOd47qSBKsJ" +
                "M9f7Wxps7UE77gNXmoxaRTCqOgwWfygY62RAGwO9h9WLGBsTDcPRHRkSK9SRhVOZ5+HE6O3kczOKx/HI" +
                "yYNgkDctdHp8OgFITRciF1B1MWY13f1h1te7vSn4MS706io++ZNGo3GFoYuinWVpdpaSAIYfByN8xmgv" +
                "HaaFiBeoH9IQpNnDQGTZjfb9+w8fa5MmJm/Y862OYZ9RFs95yZihYcJlCq+atz7n0yCxjxgeJ2lYvHzB" +
                "gdkMsAa8wa3Qqk6cJw11x2GUyDuUcvAgbvM2/BSnmR0V5+Lm5vS5fX7durh812uf/sCfhn15fdnqdGAY" +
                "Bhxtn58eutkXnfety4vzwVW3f9HtDDjv9PDIDlZeDuzEFgz44NWvg3bn/UWv27lqd/qDs7etzpv26eGx" +
                "XXbW7fR73Uu/1wv7/l2n9eqyPeh3B62f31302oObduem2xsAaOv08Fs7q39xhS267/qnhy8d9s7lOz38" +
                "jpRwVxP8R3AHbTrF1cH7d1ZAWCp31Om3ev0B/u63cYTBWRf28QaHAgWebZjy/qJ7id83g+tW/y1md276" +
                "vdZFp3+D+c8dMd90W5d1YEfVsT+DclydWBlyi3g3Lxq123nT6767HnRaV6Dy82/rgzVImPKyNqXXfdW1" +
                "R8Tod7VReAw/OeDf18a6r+i0uVHwE5TLA9TddJXMr3uYMAACnZvX3d7VwDHh4ZFjNE8ssEv77CfyIvjh" +
                "PeaRKTDRUbCCK/+WMUc0yzAXndddPwZi7VTZYAWvTndw8dPgpnv5jpwMFn2+HbVXajK1WjA4ToPDE6J1" +
                "QMg/g72YqhaCV25dqIwrJTwzB0HcNE15O0/zWPRXkI4l5vhHigPm4rQgfr3LG3AEcmhS2f1HDqoelXnU" +
                "jgUU7E4gI9Z8TWF5oEkMbERSxHApg/Pu6yCECSvNB3IXZgX0FediXmULWT6I0vGgtlkLzvXoFlBGaZLE" +
                "Oc+ZDqmUEamHbswFDzxFYAN3ocF+w60/c8u7shrK3a2G4bBDAwuZ+75OQli4WcTEiXg0twZQ6fjQvxoh" +
                "zUIHQGNZZjgyWHgacEZHQRSPx2plYXlBh8JjmAoQWV/J2kzTnAFsPJ0jrgrhjEnWx6UVxCl2Rx2mEV2K" +
                "PYcPJtLNY6InMcg+bJhMtT8GVspbJycjeBgnJxU7aZ2OxRwZCDHshSJfVFhuvzFMU/qZAx7u6wnAZhb0" +
                "AsD0iJcC4cDbNEEaR7NhaaBmGndCOggT6dlzhA/4AFMO/S7ik+HWQSKVgSZSXKVnqIsMaKjDwV5mPqXJ" +
                "gu8z+F1xLmpin9hEZgy1QS+QGSwkmqqSJlvi2UEJIwKY7h+UU5FBg89WPKxPfZrL5KdIe1FCyyVmjOsq" +
                "fBYtnM8RHSCLMKsC6Ey5urMPZxUr2+VZ6LaKH0eGi+D5q5DCi6VKmCHHZAmhGVMvx/lGB9ntJ1TLnTcL" +
                "DkP2qxkgXliboynHFPLjrlH0h0qSW8ot5AYlOYY99Rh1HZjHcFktczv9E2ZZ+JAfyA4UI7lG5jdXKSzI" +
                "8NpXkknAYhreGV1k5+Ps5LBUfL8waQZ/p3NvmpNm8JAuMqdF5RSzFADt/dTSeIQ1PeASCeMgrDx1eZ2C" +
                "d8Ag4cFyI6mnp9G7rZzdBi5KOUenPP4D6h5HBiEVTkVqOAvR0IOLFTwPlEFDSRxygUcahEaGbVTQassN" +
                "NuHgrmbVS49Z+Md6v3h0glB55Ri+8kopsB2dsm57qFt6hploCINaUty8qgEykL1fIYfnsshMENEJ743x" +
                "IUpxtYAzhlJJl05pgyqLUcFsFqaVGyovawQJ6i98QiN0IsqUvPWQhPj5XHLVDPEYk8xyGn1dMzFFqQIA" +
                "NkxSu7t3Z4LRLXyFZvCa0nCPIDIBm4TiAMNgWH0Bu4Vt3/XOX4taO6YZ37sHv+JPuGSum0oImTPkv2SQ" +
                "TExGq+Twq9gpIfEriwFF11KoV8YBVWc4aJBpBCgSCDN6xYFXcPh/TbZdTbZEZh3Lv1STuen/lzTZY4pM" +
                "3VAuzxsTA9etQBJANEjfsbAmCPTz2qQlTBMn8Hdt7O9CJgwqvb6e3nsEb+9MZU7rWdvuNcvQFEsD1iiW" +
                "6Vo9UK6QOg+eAbIeIOF7SZEc6/pEBfvnBRZkM+qALFWtuq1zWnQ2nTKE9eFg7QiBV8fCV1NDFxyc5VeK" +
                "S0/OwTEkQ5aJx4zYqgiiFCSBCy66DAJHYyPuDJUymLJKFr7Gkj2K3IEmCWUWDYYUY6V8C42dxRNk+GrK" +
                "VNS/Pd1BUIyPwNgQLMFZN8MtMttnCb7fDC7GIqZLHkhE3PnJ9JAtXlK4KNL0gOlMC2KVotciSk5iEXYW" +
                "kJZmmeW695+8VQ/+2NJtl4y28cJhy5GCdXZ95dr59HvJpqTz587kPy23JrTUH/5kztjmZdiweqRhlt4x" +
                "8zwTRssZEDMopPkNZxOp79OAQPE5obVTymc7b1sHVGW46e5wIXpJ5fkOIF3AXywRz8hY4stOKcDKR40D" +
                "t5PGeSQD4SoRtdcq08NKEC/WC40g8b0LVynAYkSZ73DZeH4We4XshZDS54hsUW0BxyC/DdEwIpSCa45P" +
                "rpZTw6KxozqimmHhNNlwB1rFljvkxnhYATlLbW6mCeXFNIkp4PpRRdm8ys4meC7LokGoP4YnhG4QNeqE" +
                "UqCuIGIDfMljleVBv5/LGTEzBYzSpcUhXcAA73BjBPIhiySHHjFFg056gv6W6EEjTrgJiqldUKkWENhA" +
                "M2m0KGNfxFGsJDiWUkG01nWD25BAQe+DlkgPKVZoShj+KABtDQi0mwHZET6pVRslCLdoSrJ0IbJgoewf" +
                "uKyE7DFD7ROqPHuQ3TKUTaV5h+0sdK10Y14fy2I2fVephJSpmbKOwqoksBvYLdylLA06l7y7WrsMWM9x" +
                "cDdLlz46tPO3I5YbxLFl3UCxhJFQx6f2XEwnYlP3Gn1Nzp7U0nBPGEhg4QK1MrNfrZ7pOnfV6OVSvqCR" +
                "HoYwxqklkBcg/T1gmmQyk8haD6Nn6BMCwTjIZc7UKl2mcTZ4805sdVks7MiZYJZ6FlN89Epq1xHgJmXT" +
                "g9tmxCzZFAh+ArFE6SieMuvaDTFBUE6r+xz5yjhZi7W5neDK5LerUPkGc6c6sBEOx0oQrygf0v2BGJjZ" +
                "VFR9rIeQl6c78C0JMs3FqOpQ4PALFTbcWBTJXUDBcov9Km4oFM7kILLTI4fkWIldK2KsX2EMpbovWEn2" +
                "WHy7yiTwKOp5ixxeoLmHz0D04WqqSRWd02wMH+DEt87PT59xm57o1ZWdxlnKtAKCrtmnOEtnU/q+TBFC" +
                "STyASojN0RmnoiDJ/wLynNd4Io72dade+6r7vo2aIs80n1NV0YV13GxzHla3CtI2PPzcWZ3LrYvcOXEL" +
                "5SGv2Y52emT1cLnn5u1klwMoxqXlfHvV4vvvSZ+JvTcXxrqSeGLGhYaoTJFAoeVpQlqBtE5jlAoVyWlQ" +
                "MlIUhTbHRLA7R03WefhsjTMZnVE3MXXDX08vfl6tQD3+0z+B1v3YFPrPL7Y/pM/Znxe7RG9KDngsZs/q" +
                "Mmgy5qoYwsI3kEwdXUfcosmYHplo/cRnDzSRD1ZhuWTFtbgzPjtf3eFEm2BkfZlJl9hSOAZKC906Q6fv" +
                "AcUBjIZVVKyZlfTZjzfdzlP2Ztmc2q+tq0vbU4NsuudiaFovA5Wg03W2+VSH5A3VtDub0gza4jswB792" +
                "6yJKktNJ0zt4LXfmJPjbf++Swrsnu2f0b85f7R4Eu1maFnhzWxTzk6dP2fiQgNrF7v/8TY/IBiqipwm9" +
                "mVWOenvWx+HlVKhA/zEudrEohtcPQbgzxnYKjxNI6zBOEO5YC7WJYVnVUiK6IPr8lfKGAOGpKPp2Z02F" +
                "kbm0fdQlRqUPmYlSe1gp6giYk8ATQN6RBHhXJ8HJtz98/0Jn0PpqzgDz1jHetTvd/HyJIha8BFay/D2t" +
                "bHzze/LWzVDYslWwu5zkxy/1DUuHJ8G3L46P5JHNWZwAJzpd2hmw/Eskc2qv6aTwIG4DVwXV0WkaLRKO" +
                "S56gSOe7jqHB2l8vY/+Yy0A37VwldZgiNZzPyWwHwegBPra4buA4E9icowt3wBmuTgfOcrlG+DlD5wgA" +
                "GNU+DbsIo3rQzw7wH5IC2qb3qvsLjJntxr1+20ZrwpF9PPsV/Q/n7R4Uun3R7bRPpaugX1FRYmuIk52l" +
                "vprTCihzIb6w9flyalknKWe4NUxVEf3qgsq0E03/Mm6RsrISQQ02yXXvlNVuuWZXTZzUwG1wiIMLqhpG" +
                "/HIQ/KoZ/d+qOJPIEjmZ2QQuo8WoroYYP/nzgejNkraDX+CXlE+/elrz6Tfa8gpKSn+LlWTAeO3UnPht" +
                "q5xs6rV6RbSxnhuV1RgdienYxjvKQQ4PhTvotc4v3t3QT6rs6S5ZYPKC9esXShVlHUlFSCLROYlSj7Fb" +
                "/RaEcDuaQZlBXIE7eNu+ePO2H+wRtn3YL8+kZfwKxcsz3a7EWU4Wgj3Kwr7uR1Xn9tHT2X30obLPY7sw" +
                "s+hop9dnQ5TNe8Jqa1bADbHnynv7dZlk7SfOJBbWFkSU2UoeEppyPaNO8vtifmArXd9YojohrRHTs1Tt" +
                "8PRKS0ldm1wSBhO3lAljMKBRaLZWjaRXWk+FyYXRSdBxbTggwSsZT6S2NXnrC7GVxHxl3vbOCGR8sm8l" +
                "Q1XtmUBqxBVCyxN/JjW7DVvEQNNboAq2jC6pKRBsqxRmMRKR8Cb+q6Jq0YGJ1kJc7ljbpst2LByTVU84" +
                "PvmHjw1u0rcApMRkYXGDSirPrXChGLzABR0m6au4XQs0QU4pvuuirVHLHWQT1dzJ4PR5vPTLYx+OFVVz" +
                "P5Dk4JYQllh9czuAlsshdRr0l0kBnzkI74NvmBb8Jhj9gb8ifgGKVxYGJ6fgdDP+8OwjM43+8TkfR/7x" +
                "iI+Rfzz+6GsRH158lHdfjwafye49qeW7NhZJa2scx4kg538Z6l7hSAdUOdkqmLK5CXlBhoNeKD8cuBoL" +
                "RvEQjvj9Eg3E84+8q3R1tjaxfPQZ9cpeat/0ayT69QFxTn3GxGoGmkSXkGDikJ1QqGrktXK26IvaMZs4" +
                "e2ND302+3njDhsDy5cqx1ltyooXLTMAhGDA9ZDvpt9cAW2XDz2WhlR/LWWsLVrp/Kgvr7acVEFvj3Edw" +
                "e7Lajue+dcKGUmEg+w3TWpXedtwYNTfyxV1p3dvAne6ON2S1N7kittXuEMRy1agS1p5227lGw3o/0L4T" +
                "RJ1R9ipVQEjZJ56NkkXEU9hukoqPkz8tOfnpKv/u2JZQKzMiRTYesa88tIo8aZCwskhFz01jzpwhZV0O" +
                "BRg8z4368JHr/Kv04p+h4/Vj/WaZNHFcVu0826OnkQYv2ey9/2XtMT4vZFOrYVnCLhWh4054phMmqKpt" +
                "To802FS02toW/FZWyR7/2j6rjPYXK8aVbx9pzCClJl89nGRhPl+vNEk1y5YpXF+ixI8UAEcUXbtT8U85" +
                "qgZBjRQV0Gd1MbcY6BejalXBenc0CgOHvoa5ggWLkzijtgeHFV2mOo6VUN8FnaAdrFK8CUySm6Umh5gm" +
                "rfWd1bmWwdeN/ZcRPNIeY3TXZKkISCrR0pssRj0j61c6e6Am7WndZLcYxUMkHos/W1uSyk4Wx3HmO/V8" +
                "YJEOwWUjBAOq6Ja2eTpHiRiB8Wg9Q+77Nm0KlH0/zGKa6Ol8gXJZ9FSK687vGKFHSIN6oX/5ZU1KgJbN" +
                "B1rmdhXJLQbH9n6U3/nCFScdV2w2gu6fnaiZNwEgl7wVkV2/dp7DJeRWu8RKGZA1thyNK5rHozuoXnqe" +
                "5JagCHP0GrDt2Hf2yuckHkuapKx7pWOJuNyX1B2PulhGB8YWMK/dmjvf9uaEtdrkt7HvzkmRX+lBafVr" +
                "5aQR+JrfjlbP8/jITRr4Abt4itO7dzbitRW4SlPGmI0MPIj9QovtZRNNwu6pT+y3K9BVPYcb7rcE6HK7" +
                "7fYoVjh6vVXRNSv9K0zuYGjbI073v/wZ4ewORwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

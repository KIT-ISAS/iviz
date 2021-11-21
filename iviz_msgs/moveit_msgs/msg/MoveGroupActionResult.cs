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
        internal MoveGroupActionResult(ref Buffer b)
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
                "H4sIAAAAAAAACu1bbW/bRrb+LiD/gVgD1/atrSR2kra+8AfFVhK1tuRKSrZpEAiUOJK4pkiVpCy7i/3v" +
                "+zznzJAULTdZ3I0XF7huaomcmTPn/W3G74wfmNSby0fDn+RhEkfheLTIZtnTt4kfDXI/X2VeJh+Ny+TG" +
                "vE2T1bJvslWUe6l8PGmc/pt/njQuB29PsGugmLwT/J40djzgEwd+GngLk/uBn/veNAH+4Wxu0sPI3JiI" +
                "uC6WJvBkNL9bmqyJhcN5mHn4NzOxSf0ouvNWGSbliTdJFotVHE783Hh5uDAb67EyjD3fW/ppHk5WkZ9i" +
                "fpIGYczp09RfGELHv8z8vjLxxHid8xPMiTMzWeUhELoDhElq/CyMZxj0Gqswzo+PuKCxM1wnh3g0M0ih" +
                "2NzL535OZM3tEiwmnn52gj3+W4lrAja4Y7BLkHl78m6Ex2zfwyZAwSyTydzbA+ZXd/k8iQHQeDd+Gvrj" +
                "yBDwBBwA1F0u2t2vQCbaJ17sx4kDrxDLPb4GbFzAJU2Hc8gsIvXZagYGYuIyTW7CAFPHdwJkEoUmzj2o" +
                "Xuqndw2u0i0bO2/IY0zCKpEIPv0sSyYhBBB46zCfN7I8JXSRxigMGt9MIR80kCcNfodwZ/ggCpTxD85s" +
                "9OGq3T3vdN967ufUe4bf1Ewjy7y5n3l3JqdOjg1ZNFHZWx7p5hB7egNbVZits2HnQ9urwHy+CZNCWaUp" +
                "mAs9HBuy6asAX/Xb7curYfu8AHy0CTg1EwPthmZC6tAQvoEBZLnnT3Moc5iT+pQyMrdiCvGsUSJ6/2cH" +
                "/0NPhAuqczDMZWQIIcwzBwWI7g1NuoABRvQGudm3KA/en5212+cVlI83UV4Dsj+Zh/ASAVRxQi5MV3QF" +
                "2xjx0Dat171+yRdu82LLNuNESA9Wopkl7lt3Clbmi6yhVmQJLGHqh9EqNQ+h12//1D6r4HfqvbyPXmr+" +
                "ZibEbys6tKlkldfV5eDLOI7NxIdbFZjFZiu4ytwHpnQScNZhfONHYfAQAVbzCks59V49guYVqhcnuRhh" +
                "qXyF8AoOn7UuLkpLPvW+/1oExwbRymzF8Gu4C5ncl9Ym0vE0TBeMa4wghRjENRMTE2wQUVWTH/4NRHwd" +
                "m6kUG+anGzByPKATF73BsArq1PtRALZixwwbQADJCyA1AjHKBL9gAaE0NRHIoOBRIHwbf4XtZYSN3Acx" +
                "GvxZhyAfloO9Nl1nY6cVRclaUhJOhCngS1LGKyBjYxVtzKskWFwSmPFqNiMb7aTc3OaNR41mnXMmWVQC" +
                "TUQsn7KcEidJEpnB1fU8RIYhUbniVURBTMCMqCMJjORYW1iF9SamCoFQk5FHSHTMYglxRRFWE2am8lsb" +
                "bF2AdtoHrTQpvYpgVE0YLP5wMDbJgDcGenebgpgaE4z9yTUVEis0kUVSmWX+zKh0sqWZhNNw4uxBMMia" +
                "FjozPp0ApBYrsQu4uhCzmk5+mPXtpLeAPoa5iq6Wlz9pNCRV7+TtNE3Ss4RMMPw6muA7RquL+8k4ycXc" +
                "IA2fgSFJ70Zi29tmDos53jLy49gEo3LZFxaoo6ivmEaJn796oeDAt5GI9NEYV+XSk4bm5ohQVCSaPBQS" +
                "op37N2GS2lHJNAaD0+f2+U2rc/G+3z79kT8N+/LqotXtIkqMONo+Pz10szvdD62Lzvnosjfs9Lojzjs9" +
                "PLKDlZcjO7GFaD56/XHU7n7o9Hvdy3Z3ODp71+q+bZ8eHttlZ73usN+7KPZ6Yd+/77ZeX7RHw96o9cv7" +
                "Tr89GrS7g15/BKCt08OXdtawc4kteu+Hp4evHPYu/zs9/J6ccLLx/su7hmtd+KiGCotX3cocd4at/nCE" +
                "38M2SBid9RAsByAKHHi2ZcqHTu8Cn4PRVWv4DrO7g2G/1ekOB5j/3DHzba91UQd2VB37MyjH1YmVIbeI" +
                "snnRqEnnbb/3/mrUbV2Cy89f1gdrkDDlVW1Kv/e6Z0nE6Pe1UaQPPzvgP9TGeq+ZwblR6BM8zR1832KT" +
                "zW/6mDACAt3Bm17/cuSU8PDIKVrBLKhL++xn6iL04QPmUSkw0XGwgit/y5hjmlWYTvdNrxgDs3aqarCB" +
                "V7c36vw8GvQu3lOToaLPH8eUSzemIQzRx7lzpEUMFaj/YwQPIo1AhBTd5lMpV0qtZg68sGma8naZZCEn" +
                "Zl4ylQLkbwkIzCSDQTF7nTWQFWRwqbL7TxxUJyrz6D9zeNodT0ZsLFsgDMGTGASMKA+RX3rnvTeej3hW" +
                "xhI0MswG6EvOxbzKFrJ8FCTTUW2zFjLtyRxQJkkUhRnpTMb0tCjbfTfmKglS4dkqXniw33Drz9zynqz+" +
                "9BkxX0cQQezQyELmvm8iH+EuDthFkfRmbgCVWRCTrQl6LswGtLBluyNFuGc0Z6nkBeF0qiEXYRh8yAsM" +
                "EwEi6ystnEWSsZoNF0sUWT4yM2kBuR6DZMiO1HESML/Yc/hgInM+dn0ig1bElsl0+1Ngpbp1cjJBunFy" +
                "UgmSNgNZLdGOkCifK/J5ReX2G+MkYdI5InHfzgC2q2BhAOyVFFYgGjhPIvR0tDWWIOPMJmkImZAPokRK" +
                "e4ZaAl8Qn+HfxXxSSB0sUhtoot9Vpom6yICHOuztpeYmiRDnye5lGmbiJvaJTWCmcBtMCdnOQtepammy" +
                "JZ4dFD8ggMX+QTkV7TQkcPnd/alPM5n8FD0wWmi5xEwhrrxoqfnLJUoFtBTiKoDugqu7+8hcsbJd0sIc" +
                "VpI6KlyAMkCNFCktXUKMhpNlhLZQCzvOtmbLbj/hWuZSW2gYWmFND8XDvTnaf0xgP06M4j/UktxSbiES" +
                "lE4Z9lQy6j4wC5G/WuV2/sdPU/8uO5AdaEYiRjY7NzksyFDsG50lYLHwr40usvNBOzUsWVKiftT0/spM" +
                "3zRnTe8uWaXOiwoVcQKAVj61nh5hLQ64RGo6GCupLsUpeHusGO6sNpJ7So3KtkK7rWKUc45PWfgH3D1I" +
                "BiMVTsVqOAul0Z0rHAodKCuIkjnUggJpMBrttknOqC0SbCLD3Wyz2/IATlX0x6a/eHSGUHnlFL7ySjnw" +
                "OD7lfuyhb+kbtqVhDBpJIXl1A1QgK19hR6FlgZmhvBPdm+JLkEC0gDOFU0nWzmmDK6tJztYWppUbqi5r" +
                "OQnur4ruhu9MlP15myEJ87OlNK5Z77GiiTMGfV0zM3npAgDWjxK7e5HOeJM5coWm94bWcIuKMoKa+JIA" +
                "I2BYf4G4hW3f98/fiFs7Zhjfu4W+4p+/ZuObTghtNDTDZJBKTEWrNPSr2Ckj8ZGGgKJradQb44CqMxw0" +
                "2DQKFKmKWcqC4A0c/t+TPa4nW6PNjuVf68nc9P9LnuwhR6ZpKJdnjZlB6pajhSAeZOhUGLMKdb43aY3Q" +
                "xAn8rI39VdiEQeXXt/N7D+BdJFOp83o2theeZWzytYFq5Ovk3uGgiJA+D5mBP4Eza3yQvsexro/UsH9Z" +
                "YUEa0wekiXrVx6LTorONSh/Rh4M1ErzCHYteLQxTcGhWsVJSemoOyJB2WSoZM2qr3AsSsAQpuPgyGByD" +
                "jaQzdMpQyipb+BpL9mhyB9oxlFkMGHIyK2e58NhpOEO7r+ZMxf1b6g68fHoExYZhCc66GaTI1p9l+H7T" +
                "60zFTNckSEzc5cnMkC1ecoqRJ8kBe5sWxCZHr8SUnMWi7MxhLc2yzXVbfCuiuvfHI0m7VLStAkcsRz/W" +
                "xfUNsfPp91JNyecv0VR8Wz+a0dJ/FJS5YJuVZcMmSeM0uWYbOhZFy1gQsyhk+PXjmRz2M4DA8TmjtVPK" +
                "ZzvvsQhUZ7hNdhCICqmk7wDWBfwlEpFG1hJfR6UAKx+1DnycNs4DHQh3LFF7rTY9rhTxEr1wKyS8deUq" +
                "DViCKPsdrjXP7xKv0L0QVhY9InvCtkJikM193B4RTiE1xzd3sFPDorGjPqLaYeE02XAHXsWefYjESKyA" +
                "jBPbm2nCebFNYnKkfnRRtq+ysw2e67JoEVqQUTBCNwgadUYpUHc6Ygt86WOVZ4XFfq5nxM4UMErWFodk" +
                "hQC8w41RyPs8MTksEFM0mKRHuOwS3GnFiTRBMbULypRBgI20k8aIMi1OdBQrKY6RdU+0WN+4ggNpSKGg" +
                "8mAkUiIlCi0IoyAFoG0AgXczYDvKJ41qkwjlFkMJDk7EFiyU/QPXlZA9YhyEwpXj9IK7pThDlZs8vNvC" +
                "1Eo3pvh4Rmbbd5VzlLI1UzkHwRElsBvZLZxQ1gbXmIp0tSYMRM+pdx0n66I6tPMfxyy3mGPLpoESCQPh" +
                "TtHaczWdmE09aywO6Cyllod7okACCwLUk5n96lGarnOixsUu1QsG6bGPYJxYBhUGpJ8jtklmsVTWSozS" +
                "MCQEgnGQy56pdbps42zJ5p3Z6rJQ1JEzoSz1Lqbk6JXWrmPAIOENCLfNhF2yBRC8AbPE6SieMuvKDbFB" +
                "UE6r5xzZxjhVC5Ow06XJ5ptQ+QZzFzqwFQ7HShCvaR9yFQQ1MLupOPWxGUJWUndQ3E+Qaa5G1YQCxK/U" +
                "2CCxIBBZwMFyi/0qbldcSkJkpweI5FiJXStgrV9RDOV6cWAl3WPJ7SqToKM4z1tlyALNLXIGoo9UU0Oq" +
                "+JxmY3yHJL51fn76jNv0xa9u7DRNE7YVUHTFN2GaxAvmvmwRwkncgUuozXFNTk1Bmv857Dmr6UQY7OtO" +
                "/fZl70MbZ4qkabmkq2IK67TZ9jysbxWkbXn4JVpdyq2LHJ2QQknkFe+mnR5ZP1zuuX072eUAjnFtNd+K" +
                "WnL/Pbl0YuXmylh3Ph6Zaa4lKlskcGhZEpFXYK3zGKVDRXManAwUReHNMRHsLXEm6zJ83pMzKZNRNzFx" +
                "w9/OL37ZrcA9/ss/np778Ybov77Y/pA/Z39+2CV+U3rAUwl71pfBk7FXxRIWuYF06pg6QoomZXtkpucn" +
                "RfdAG/lQFR6XbKQW16bozld3ONEbMbK+7KRLbSkaA6eFqztj5+8BxQEMxlVUbJiV9tlPg173KS9q2Z7a" +
                "x9blhb1gg256ocXwtIUNVIpOd82taHVI31BDu4spTa8tuQN78PekLqYkPZ0kuUbWcm1OvL/8fZcc3j3Z" +
                "PWN+c/5698DbTZMkx5t5ni9Pnj5FKeJH4Ha++4+/KIm8TUX0tKEXW+eo0rM5DoVT4QLzxzDfxaIQWT8M" +
                "4doYe214GsFax2GEcsdGqG0Ky1MtZaIros9fq24IEFJF07c7ayuMyqV3SV1jVC4ls1FqiZVDHQFz4hUM" +
                "kHdkAd7VWXDy8scfXugMRl/tGWDefYx37U6DXy5wiIUsgSdZhZw2Nh78Hr1zMxS2bOXtrmfZ8St9w6PD" +
                "E+/li+MjeeRNLU5AEp2s7QxE/jWaObXXTFJIiNvAnYLq6CIJVhHHpU+QJ8tdp9BQ7W/XsX8oZWCadq6W" +
                "Ok7QGs6WVLYDb3KHHFtSN2ic8WzP0ZU70Ax3TgfNcr1G5DljlwgAGN0+A7sYo2bQzw7wH5oCemfvde9X" +
                "BDN7NffqXRtXE47s49lH3H84b/fh0O2LXrd9KrcKhhUXJbGGONlZmqs5r4BjLtQX9ny+nFqek5Qz3Bq2" +
                "qoh+dUFl2om2f1m3yLGyMkEDNtl165zVbrlmV0OcnIHb4hCEC6paRvx64H3Ujv5vVZzJZKmcTDxDymgx" +
                "qrsh1k8FfWB6s+Tt6FfkJeXTx4LXfPqNsbyCkvLfYiUdMIqdnhOf9pSTN3ytXxFvrHTjZDXE9cRkausd" +
                "1SCHh8Id9VvnnfcD5kmVPZ2QBSYFrH+LoVxR1ZFWhDQSXZIo5zF2q988H2lH0ys7iBtwR+/anbfvht4e" +
                "YduH/ZImPcavcLykab5RZzlb8PZoC/u6H12d20eps/voQ2Wfh3ZhZ9HxTsVnS5TteyJqa1fADfHOVZHt" +
                "122SZz9hKrWw3kfEMVupQ8JTrmfVSX1fLQ/sSdd3lqnOSGvMLFSqRjyz0tJS700uGYOJj9QJYzGgVWh6" +
                "7zSSWWm9FSYCY5Kg43rhgAyvdDzR2tbmbXEQW2nMV+Y9Ho1Apmj2bXSoqncm0BpxB6ElxV9ozT5GLGKh" +
                "WUSgCrasLukpUGyrFaYhGpHIJv6n4mpxAxNXCyHcqd6hLq9jgUyeeiLxyT59bnCToQUgR0wWFjeotPLc" +
                "CleKIQtcMWGSexXze4Um2CmH77ro0bjlCNnGNUcZkr4CL/1Lsk/Hiqq5HUlz8JEQllp9+3UAPS6H1WnR" +
                "XzYFis6Bf+t9x7bgd97kD/wK+NdQFJnvnZxC083007PP7DQWj8/5OCkej/gYFI/Hn4uziE8vPsu7b8eD" +
                "L3T3ntT6XVsPSWtrnMaJIWf/MdQLhyM3oMrJ1sGUl5vQF2Q5WBjlpwN3xoJRPPgT/rGJFuLZZ8oq2Zyt" +
                "l1g+Fx31yl4a39xV8aZLTouOifUMDImuIcHGIW9C4VQjqx1ni7+okdkE7Y0t926y+xdveCGwfLlB1v0r" +
                "OcHKdSaQEIzYHrLX6B/vAmxVDb/UhVZ9rFzFry/YuP1TWVi/floB8Wia+wBuTzav47k/QeGFUlEg++em" +
                "tVN6e+PGaLiRv+KVq3tbtNPJeEtXe1sqYq/aHYJZ7jSqhLWnt+3cRcP6faB9Z4g6o7yrVAEhxz5hPIlW" +
                "Aamwt0kqOU72tNTkp5v6u2OvhFqbESuy9Yh9VUCr2JMWCRuL1PTcNPbMWVLW7VCAIfPc6g8fEOd/yi/+" +
                "GTqFf6xLlk0Tp2XVm2d7zDQS7xUve+9/3fWYhusL2dYqr2LYI+zSETrtRGaKvxrbvOb0wAWbile7twX/" +
                "RKtUj//dPpuK9meO8Z9UdQ4sgkAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

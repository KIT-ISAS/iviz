/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MotionSequenceItem : IDeserializable<MotionSequenceItem>, IMessage
    {
        // The plan request for this item.
        // It is the planning request for this segment of the sequence, as if it were a solitary motion.
        [DataMember (Name = "req")] public MotionPlanRequest Req;
        // To blend between sequence items, the motion may be smoothed using a circular motion.
        // The blend radius of the circle between this and the next command, where 0 means no blending.
        [DataMember (Name = "blend_radius")] public double BlendRadius;
    
        /// Constructor for empty message.
        public MotionSequenceItem()
        {
            Req = new MotionPlanRequest();
        }
        
        /// Explicit constructor.
        public MotionSequenceItem(MotionPlanRequest Req, double BlendRadius)
        {
            this.Req = Req;
            this.BlendRadius = BlendRadius;
        }
        
        /// Constructor with buffer.
        public MotionSequenceItem(ref ReadBuffer b)
        {
            Req = new MotionPlanRequest(ref b);
            BlendRadius = b.Deserialize<double>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MotionSequenceItem(ref b);
        
        public MotionSequenceItem RosDeserialize(ref ReadBuffer b) => new MotionSequenceItem(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Req.RosSerialize(ref b);
            b.Serialize(BlendRadius);
        }
        
        public void RosValidate()
        {
            if (Req is null) BuiltIns.ThrowNullReference(nameof(Req));
            Req.RosValidate();
        }
    
        public int RosMessageLength => 8 + Req.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionSequenceItem";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "932aef4280f479e42c693b8b285624bf";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+1cW3MbN5Z+56/oGj9I3NC0YnumZjXlB8eSJ07FlsfS5OZysUA2SHbUbDDdTVHM1v73" +
                "Pd85OAC6SY2T2pF2t2azW2OxGzgAzv2GfpRdLW22Lk2V1faXjW3abO7qrF0WTVa0djUePMretBn9av24" +
                "qqgW+2Mbu1jZqs3cnAc2eF/N7CgzBGdOoLKtrW1mssaVRWvqXbZybeGq8eAt//ueIH/wQAn4gJa9ctm0" +
                "tFWeTW27tbYKQHljzYgXEijZyuxoWNasnKOnebZpsEuTzYp6tilNHVZ7xOcVuLXJi02jW8bQ0obF+FSG" +
                "RuFdZW/bbOZWK3owyrZLHOUkW1lTNVnlt0kLjgfz0pn2T8/lyUQWGAxe/JP/G7y9/OspHenGFu1k1Sya" +
                "J3tI5IMyXeqbglA2c1VrikrImNt5URWMOBDQBHK2LkEqgWB62zrgwW3a9aYFNde1uyly24BQ701tVra1" +
                "deP5wWZbV183a0MLt0vTRt4hWM3SbcqcR2S0IQIy+F5HJ5AChMk6PMRil8Q8LYjbtKa12Wad0z/NOHsz" +
                "z2a2xhmzn11RtY0uNOXDY53a5gSAtrN2DZ+eaE87w45NJTywqWvm4soKfzU2GSwACQQ4zbYZ2GDwwU1d" +
                "e8l7abC1Ce9r4BmNJjcFMUO2cKaULUckrVxuS+CcxY+ejrNzM1tmtrRelggKBpq6Jv5mqtF0I8BquwBH" +
                "8zL8APw6Wxb2ho9ZeK6mjbe1YYQIrdfAnwiFgCDwtHfTFs28oKmv4oyPnxjyJAGCg71zHvmESlPt6JD0" +
                "JjOlI6owpU27JNLK37XLNzOSSC+ofNRtUZbZTeFKABEsp/s8Ftlbr8uCjkv4MTScFyGiVK7Nft4Qr27N" +
                "Tp4N0y3z4v0NX/UQgY3VttmUzEj09Gc7ax10EgALKnaDq/A8hR9HH1qlIk5VhZKcGHxP59g0lhmVVAYP" +
                "hHyu7awA3kdgTdDY0Lb6cxlhxHUEIB/Qmti2fzcp8kOLL2q3WeOHFwYCtl0WxFyMW4VLf7q1rQ3woHB5" +
                "5gSwAtzNakqDAblYgSIKgs2CEwlbkWKw+Ti7XLq6hS4hRb/xakS3X9s1XubjAe3p2VMAnqhJmZiWtPo6" +
                "onJlbovVZpWZldt4w0KrH8IsmKUs3Za4LBGm7LiAySAi5cQiqpj9wLgsgLJemZkSx58bEFeExa2xDk3Z" +
                "0daJjQvP4Lo1xm12Y0s3IxVh2V6AjLMZiTCwSgwyzrKXfnM3ptxgEIkbbe34ZPTlJ3p7dRfA3QFwkV9U" +
                "wGroH69KAHkFrobkkObbgVQWco5pbXFjCZw/IK1MnEgGBAoQE0m3Q0kCaFFjrwUsZLWgHR8XKxDOVG25" +
                "GzH5oWCqWbkhC0CEZXVs2XycjE+GYpllHeZxGyyL8jejAjT9cnzCsEi9C6KPi7Edj+7CCAHsvkmRM4z2" +
                "lwZNdNKkEdJOZEedMen0/riHsNsHLJ9abpK0xiwSy20YkYSzaBChEOYbUv2QMnrL+ozkHqi+IfkjeTk2" +
                "2dTdDsEuqgCUabpyg22NdfEigHYVEYU8nio6fxCNlZsWpWVjwh6Ut1QCmHy+rS3LMeTqjK2WcETtFVRt" +
                "52Q64cmpKaQt0kHrCuf/2hqy1WRd8U/QBgUpC6K6DFItR9PIHSMeVhZXPpN9NOorkBm2g4V1hDXS24z5" +
                "71iJPwPgiQDtK55//lLEcLrU/bBW0+aypOBQ3KUqNzXZX9sacpUMo3xZLEjsH5fkK7BXsloTD/Dbdrcm" +
                "byphg4Wl/bJkwvjg0PCDN1UxY+MNg5DOZ2Huehkz52ryjjGcOQDQmV29N//m7JSts51toKJoJVIttTXs" +
                "wr85ywYbsRY0YfDoauseQ1ssYFF1cXEyabP2dk3UwT5Nc0pr/JscbkywT9UWZMf8bEI/m2FGi9AWyC6R" +
                "ZMBgvN+1Syea9cbUhYHvRoBJM5QE9QiTjoYJ5IpBV6ZyCl4gxjV+C9gqwMWZHpM7mrMxajYLwyrUu9te" +
                "p5NSIAVP2q8spjXFUgO2jLzk4NFrljK20UwRSHnTkCYkAuTMwWrpmRrsP9wPNx6UAmUt8gWIVBa+iSH9" +
                "LraCDHdt6STQiOMshJ6sgxBtsdCFmfCXihqGzfvBpFdcTTEnxSe5swjNEAetzDX8dcIx+5LkVpJlhR9X" +
                "NaUYVXpMU47teDEeia7jURJCEgSWgWKW1cWCjCLPpIVWYbLJ/OHI8s2firvDe5bFiGAEpHatt1IwvDu3" +
                "IQ+WzkB/1F702JPSfTGLtM6NIHceRBeh79kAqpkgC9GS0JPOVft2G/7ahb9+fQCbFiOiO01ZUUX8mSl5" +
                "Hl1N2oKG5AZIBBGDNeQRGvVoEZOSkFw3A9DW1bL4N3gp4RiPi+HYN877/qQqkC5YmpvgLdns7OK1xEHB" +
                "v+LoLgX9FmNpXLIET5/kbj7pLfaybSkWIygzV5ZFg3O6KeIGUgdG3xHBGyIonyJziTs3HOj8Vzr9gmdT" +
                "QKazJwHyxEPGuq9LsyDs5tDOYF9iZh/hwg+bER9Hn52dLhKlltUlC9N8vmfieIcSJvP8xDSsXNNCW6tz" +
                "KKZFdRcHrnrUqcvhHx/rfmggAjlYk9Ka+tBgWogIbjxrnZ6S5bSnp0m4PWVplwwAFkSOCZtvE5YbDqbO" +
                "ITSe4HD3ZnYPMmCCKRNEgNlv6cq8CUJPPvSsLqbiI0lgzQf33h7pFTKULDu1Y3MhAgD96D2WMMkifSCO" +
                "8XFt4QHieU3mo2ggbrMhdiOJBNgS2EgyZamYqb+jUEzOTvdwFIdGb7w/9EnDg5+QYYV4xil2TrRqg52W" +
                "2D7EMB7AuxVmvyM3Hgc7j2eBN1oVhAVwW06aXySUbCH0AYeqggjxGYMQS6TPxpHWje6vQPXZGM5LkhUh" +
                "JwXxKwVre2PEqXEkPEpGVh4iRjo183muhs1vwX7OAQXYFI14zCYqH87vNCNeATLEZIQH1cUwb4bNYprz" +
                "gesOC8eT/Hg6OzhM49dx9j2MGuyb2BuvQvkUlSOAnj49RwGwViO2VTMKqklSb2xKTslLIWrfeW4E9uQ0" +
                "Qtvk7D4dt1QYjKem+JV0PR2ZEClwEqnhjCOHH7QK0ifKA2GbCXI4+aGbhu8OxwBuJlOQzGI/rhAniDQq" +
                "8483kPRTBSF5pAyfPBIMPIRC2Tc7dKoP6gMZlVnRAeAeT1zGRWCx3C7Is2LGg4uVO6IrkgEOAaCqa0LJ" +
                "ZtZuatYlcT1hZHHFCPUUVeYiyUblEx5/syMHZCWYb9bsCrNrFNwlmbOwbZR/ZDdC2vCalBIrqWy2JC9h" +
                "nL2GKNySU1uijsDxp6lVWRjmsL9/OHvNOu0ZDPgxhWg7+n+zhSstSWvyy+UlOBhcloQI6e6MryMQVxAU" +
                "mQuJ7rxnbxIjFBoJ9I1FSiubmtk1DtzZw/+rsYdVY1tkFZa/WY3p8P9LauwuLSYOKKY3vfTDlbIwjQrs" +
                "vDdoSwTFAPzbe/c9o4leCr4eJlwMuz4QMLI4BLUSKnZbt5draHox5WCgyZgk/hv8bWOQf4QC0CjtYQ4Z" +
                "Fz4UFpOs1kXQ8Z2D4NcvcdfAw2cjP/1r+0AEZE7yx1Kt20TnsXueae2uLQ7JsXiDmAhxAfSwqRacR+Jc" +
                "4zgQ0A+Jv/24hzmdyMQBqhEphDzxcCOy58jZtuzxtlBUv/GIDCz+lFDgIfLSd8SfXmf3nob6TojgWIEZ" +
                "0t+3Gq5ItrTgU19rAgp/a6WJ8dipUhtyRVA0XZq1lFY4txrLp3t7E0OWhtcYxgs+ejMHPJYjkAtnZZCV" +
                "84H5uMh9rWLE9UYNqh8dgqchtljvcIyk0IUFUEvtIkqAaqLZB3icxIh18rCeJgyQlkjy9K3bMONNd6Ga" +
                "8jhsbKJ1RVPWZCF2Sek7mRCtBgObSBoFPl4sosuu2Ksgx2smwVonr0vUYF8x9IDIhiVFtwKMcBQC7bNr" +
                "pNTQekHus6T8ZqWTVo3abVgQPBRfRNI1KjuD/a13vFptS0kP+6qDXxjkI5iau0kqtTEyj0VdQgZ2N/FL" +
                "KFG2tlgsg8fSI8YIZfXrym2rqE15/EPI5L4svvRuwEhy83NOofqkjvr0LDOHayrE8f6YHoHHzD0Mi6j3" +
                "ltZ+0w5VWGOdWem8W1thCmQypqbhsIGxE6RH/p3AuVxIx4mcRY5wBQgAE4v0mi3z2paLqvvenMqsTCtq" +
                "rxkgKYdKNGlSTxFw6VDg1GVmSJGsCtQfmgFrHNknj3qvrxAdxmH9bGzTeT8RxNNKb22z7ELFExq7khcH" +
                "4eBdBPEVhEOL/sijWRh/r81iC0E23cReG6sxijgRsSmKKJbnhbjUjLhhujf0EPFBeKU7Dol3cXcv87xJ" +
                "2chjPdQLOW/IWe9kEPHoTeE2DfnB9pY8BWy/aEU7i8IZD6Y78uNenp29OBlwzAtp6Kw0r91KEhLVTVG7" +
                "ittmEFjV8K+PLcVmO1JNLAqc9m1JmJseTxT5UFb6cP724rvzF1/ymdZr6ClEsFU4F8e8XrHyppvQ5vIP" +
                "z6rFCJmk5yQqxEO+f3/+7uzFU6+E45qHl+NVRqQVt57zPam5KnLM/R+ebhrGcGcLjSjtvJUQZShNQo0r" +
                "gStCrWqMqE1z2xRomuItMm6eYYMXa+1GEItLP+GA6kCnr+9LKX5eqQwe/e7/souvvjl/dYWC4++f7P8D" +
                "cl794xoHK00Om+ds8LwiIzWGRAVCmMZKYJ1U+Vu3kLR5CB0lhUt8gix5x6m4tiEvm65wyk9kfkw+1MpQ" +
                "C9JYVZZPVdkTFAWYT9OteAPLuZNvLi/ePUHnj0+o/Pjy7beZABhnLwMLk5oNApDU4qCoFSsxaSRGXQ3K" +
                "ODtnr6GoDhCd5YgDeueuyV+5tqfZH/7jCBg+Oj16Bc/m7KujUXZUO9fSk2Xbrk+fPKHww5SE7fboP/8g" +
                "R6zZY6qcZHMqrxmFet67AXESLMBzLNojmoTeSpKCa2t9FXpekqhOi7LQJIA9xK8oZggStbZ49pXwBgOZ" +
                "cTuu0fyV5EHAXL4/0WfFuMaNLJk/LKfzGcxpFhDAz4ACetZHwekf//3Pz2UETK+UUmnc/o6P/EqXf/s2" +
                "I7I1FjWMQKfOwpe/lF/rCIHNS2VH20Xz7E/yBBWj0+yPz5895Z80usaAAm6uH0Fmf0vBfO8xPBQcRBfQ" +
                "4pe8Xbl8U+I9l09btz5ShibWvq9c7V3eQmx+4d6RZg1OG2WzHbnW7LTNkCjz2SaNcmobyjPEVpplIg9n" +
                "qi4AAYPCh0lnSRTH+WRE/zcecK/En7OvLn4gMyZ/X77/+vzDOZkW+fnqx2/fvDs7/0Cq3D+4eHf+4rlK" +
                "u+ontjLYkx8lXpqqhIIMbaM12Tg0psfjiNBVYw0byHRCMuxUEn/cUolSoja7YizQdaua6ijOORLjNvCs" +
                "ibd0cN6qRA8/jLIfJZf7U7pnIJkDJlst2pBs7OsghE1N0v0zjrid/EAeSfz1Y8A1fv0EK55sSfDvd8V5" +
                "QJAdapP+9VnhBt6PKBVWxb5FVfvTfZgjHDTu0HXy4eXZm79fwkNK1lQiM0wQWPp6BCvCOpx+4OYKdQ85" +
                "E++X+ikz5HBId6J0VXTgTr4+f/PXr6+yY8D2P4bxTFK6TTAez7TshFcqC9kxZGEo60HP6TpyOr+O/EjW" +
                "uWsVdFt0evs1ODm85itXSTJAX9H86Of3ZXJqw60Cab5ui3XkIcYp5iPYlG68ka9xfOGROuhJosdfYKne" +
                "4eGPRkndGxwRg4H3o+L2gwAOPuu9IhSc0X7ui6kF90DeS5EZ2E6Sm+NsIN0sofiWpGSTcQ91wKIKmctO" +
                "Siotkhvf99457mdSsPdvghBaquFJtopwEgqComsRvrow1YI8iL8kGtb3J3N3JndSh84bOiPKXOTsNB8/" +
                "DbDGlQfANQUPa+CVh0/c6QyNva6ttqbybg7gnEutMumBUKXHOIAyPdZREzclnYgfn8k+7e2E84APsluO" +
                "yw9WfqUyakc+vo/xf0gSmNvsC6T/vshmv9L/5NmLjCNqk52+IAa3848nn5BRDD+/xM9Z+PkUP/Pw89mn" +
                "UGr4+PwTP7svBHwmh9fLax0shvWmKKPJ3ZH/oX2rhuHicHL/RDRKrPv69vwgiB9HyW0D+tG5afAJVHLd" +
                "0dKm8CnkzJO1xJTZW7TdIg/h/dCQFuneVgilThSJa4Qu3Zol64jeKcd09MGBzopmv7UC/V7xYedY+00X" +
                "+UbTD2T7J8gBTbj9+mGSsPHOz92N+qpm04sqkM3kxpAiPL2XpEmacBnLX9niGrletAn5fL6ioRVgaR8Q" +
                "Zo97DKLQuar03tOhM06J0x16EW1vZ3Rik7sTvisaH/12xt+Ex93h90+wHkYkQyM/Dpj0UKuaSoKC09ta" +
                "EuEQTPEdVYyaPX7t5+pFPDjYH8MSj+kt+Lqa2cmUGH87ist/kbwzUzrAp+A39DuS+iMPPGfonMf0BYp4" +
                "tSnWayIlsuPcVq5l44+aOEWd2tUp+Q1pAE3ZN3tVklfHfsKvtnb+Mix5AU1sCB32qyT3T+593r5TTEGz" +
                "vGf0AzniUfd7LiSglW5lrgb2sckeFNJ2hwqdkhKaz1H9O/ZMyFC4YWEYVbWpF7b1JsEl47aWlXJ6HeeO" +
                "CycCYsIgJrKkbsDfDrpz59lAaw7fycg4aCLXRv/3cdcDsFcXKTHDYxShRNBnZ4IaX95t7iw07TvGShWn" +
                "Cd5jzhFuEAcNf2tdahCuHsN8pw1jvaAWaUAy/b+5kPUmVo44PAjQRr6UFcoQwYlA/SQPpfOQwwp30xZE" +
                "bv5GwP4Ru0Wyuw/ll/7sgdKK2v0zykHD+XtUUbf55/PqyNdqOrN8ViLRVJEyzFixhWhwV2NSN/z+b6o+" +
                "7eTjRMhjDq8yW9dQGWq6knpmvLQ75cvEdnI7wcRJGLw/YvfZEb/2R/wr6rFDblqowYfzxlug3J1ZMLcm" +
                "vpw0+OZFM5Oio9ib4V6nSLj2x9zP47kHsZObMwHwTvJoWymJFWsulkqhyOEKVmBsaZMG4Ld+c6xc4v6w" +
                "GkBR7EtDN7XGx8LAoSrlptL5LDo4Tsc2AP0dEf00SQgpfhjAu4srAi5tXyvaAa6OJteU6bSt8HX8soju" +
                "PDQpK+rweYNawBZtgJp4A3pzJ7bHAhc3hd0maWLBCjH1vjPEGvqYe6kIB2Za7nxD61A/L8CM32zQubup" +
                "WV2Og9h3cqpMRrZg8UMbyiOITgr58EV0TsUZ0W+iRH2eAvyLRpZ6rf1Gs9b9kXyHa7bEBXW9bxwoJFZI" +
                "saS3jrufvggqbCS9ylJRO2A6Lv311uBMNawyXmvrgaBbObN1W1P7dgiladiyML3ps1jCWdbfjqkdcY+J" +
                "TRsrfGeDKxK9tni+rShjnsuAkdzFNpKZJGXGHkbT523/7Q/EKtl6Rzgq8iChcjvfk9WUWzQQ0MBnzNcO" +
                "JQi5kYsNTxr9DEygZ7hq3pVE4d3IIloeFQKRj+QXjPc7e8aLAESz79HOqkF6tOKniuIlhTAu1Inlxii6" +
                "r9sOkaCzCMfxQqkig4Gttdlm02z4MjTfXqs3VnpukoaafZlDokAKumSBbrzPXRBUwuPd7OavgCi7nd+w" +
                "5nCbxTLyE1yJPYkbHdJiKhNoiMmalWFhmdqZ2USpOuA58CoHO6q8jhF861duBo9SWcJY1kwe0jWxEq4M" +
                "SteOYW4dRWaZGbnnurd1jz6045CbvDTcqIK+QV7hxDuTUovh9cSvSC66+Zu1SE3zK6+Jvb6sUOgv9WTJ" +
                "6nx6VdCBMfSy8vygAlCmFhHiMLzfua5GQj22uGeRbcMNNUA23ox4l0EotcP0xFvcrfP7EQ+YUI820zXH" +
                "sbYZKkQUYUrbonVEjZ1f+TD498WTp7JCCp32ha4oUaHDcaoswp1CPjFai/xhvRlMCLOnB/yXqoIVMR6m" +
                "V+BuLY3htqbH6CQ9GfH+5CL1Sejabffon5jmvPdtEBo3kcx/kHOdxVawodiGDlnuxPVJZ7DEl4h9ukfU" +
                "QsK+nVfjsMdDylzwSH3nh6sq7vqVMmbfuKS+gDdswnYJ0+EsiKvCF2LYRgaseaejhx5uBdNPuHRQxdNT" +
                "XKXbTZWtC13RPYFRD0UzG8H5+SF7kT0dZT/SP1+Osp+4LOGL2+fvLi8+TH560XsQS+3+wQ+hscErTKZT" +
                "WPtfxrm/05AwAvBb9bh+eqV/VUaYUT+p0QsyGYDYovvZfxqcHPzwlrIe39Uijup8z8uFvHhynzDWOnrf" +
                "NOtkoP8LhYUJhgFRAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

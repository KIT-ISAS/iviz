/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MotionSequenceRequest")]
    public sealed class MotionSequenceRequest : IDeserializable<MotionSequenceRequest>, IMessage
    {
        // List of motion planning request with a blend_radius for each.
        // In the response of the planner all of these will be executable as one sequence.
        [DataMember (Name = "items")] public MotionSequenceItem[] Items { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MotionSequenceRequest()
        {
            Items = System.Array.Empty<MotionSequenceItem>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MotionSequenceRequest(MotionSequenceItem[] Items)
        {
            this.Items = Items;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MotionSequenceRequest(ref Buffer b)
        {
            Items = b.DeserializeArray<MotionSequenceItem>();
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i] = new MotionSequenceItem(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MotionSequenceRequest(ref b);
        }
        
        MotionSequenceRequest IDeserializable<MotionSequenceRequest>.RosDeserialize(ref Buffer b)
        {
            return new MotionSequenceRequest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Items, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Items is null) throw new System.NullReferenceException(nameof(Items));
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] is null) throw new System.NullReferenceException($"{nameof(Items)}[{i}]");
                Items[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Items)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionSequenceRequest";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c89266756409bea218f39a7f05ef21a1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+08aXMbx5XfUaX/0BV9IBlDEHUklWWKH2SRiuWyjoiMbVmlQjVmGsCYg2l4DoLw1v73" +
                "fWd3zwC05KqIyVbWuxURM92vu9999dw33xVNa/zcrHxb+MqsS1tVRbUwtfulc/BqU7RLY82sdFU+rW1e" +
                "dI2Z+9o4my0n90b3zcvKtEsH45u1rxqHsPA3AXK1sWUpj+DdpoBfM2fcjcu61gJQYxvjK2caXK7KHIB8" +
                "RTu5kAcvW7f68NEU8E9zbzQ6/Sf/N3p18bcTOPy1K9rpqlk0D3eXh0NeyokCWhAF7bJoaGOMh9bAz3D0" +
                "FIdhcOMWK1e1iiM99BixUMwBltm4GnBiGl8Wra23QpaAlbcA+p1ABej3RrjypWfyAGbbjXNVgMtYG9Na" +
                "Qt+V3SIBmpX38DQ3XYMbtSYr6qwrbR0X5EMzYKG7bBvHAuV0NTqZhVH4rnI3rcn8agUPxmazxOMcm5Wz" +
                "VWMq2SesCPDnpbftn5/2OOsOCZxgko5K1KmvC8Ba5qvWFhUTM3fzoioId0hGG4ja+gSviK7A8YIK37Xr" +
                "rkWirmt/XeSuYXK9tbVdudbVjTAGyIWvr5q1hbXbpW178tMsfVfmNMLAngDMvdEPOjwBFUBM1+Ehr3cB" +
                "jNQilZvWts506xz+aSbm5dxkrsaTmp99UbWNrjUjFOBStcsRAmxp7RtCAjAB7A53bVnus66uiaUrx5wG" +
                "Yh4HM0SEgUznWoMMcW/0zs98e0HbaXB3U9qacPOSADQFaoeFtyXvOyJr5XNXIvpJIOHpxJyDMjKudCJc" +
                "CAZH2roGdicKwnzL0Gq3QAandegBcm+2LNw1H7YQJoftt7UlvDDh14hGFhKGAfBh/7YtmnmBc5/HKaCx" +
                "EPY0gcKne+2FDIBTW23hpPAOlKQH+hDZLajbglELbJN3GQhpoptZhV4XvkQgjO50q4csjet1WcCRAUmo" +
                "f2kRoE7lW/Nzh0rdbvnZUW/XtPruni8H6BB135XEVfD0Z5e1HpUVwmaEbO+NLsOLdIk4fP9CFfCuKprU" +
                "JiHPeVBXjhgXVAkNRKldu6xAAoyJVZHcFrY2nKymByAAqWBd3Lu8nBb5/g0sat+t8YcICMDbLAvgNcKy" +
                "goY//drVFtERQNPUKQJLQHerGYxH4MUKyaNQyHJ4FrwVKA2XT8zF0tctKhqwBZ3qGD1F7db4Ngc9Cht7" +
                "8hhBT9XuTG0Len+dYnVlb4pVtzJ25TuxP7CDfUhG7ilLvwG2S0TMHBZoVoBiOfKM6m4ZGVdGqKJ1Mlsi" +
                "HuYWic0y5Ne4FEzawgGAtQthet0dYdlcu9JnoD8cWRUiapaBcCOCgWMmxjyTDV7bssNRIIawvcPj8aOP" +
                "8PbyNojbffAi/6jY1aicRMsg6BUyOsoTaMYt0szV5O0AFxfXDuHJGWFtYE2wM6ggcSZYANSiCLWocbcF" +
                "mtJqAXs+LFZIQVu15XZMjEC6p8rKDgwFkJgUtiMzczw5PmIjzgsR17tggZThCRtI2keTYwYGNoCxfVhM" +
                "3GR8G1YAYv9Nip+jxFTDqKnOmjZM4CnvqT8oBbAz8E6M/B4TqWYeJK+xi8TMW8ImIC5aTlQT8w5sA0kd" +
                "vCZVB7oAEX4NAgnCcwhesb85QrZRpaDM0xci3Jc4VOgwBuC+AtKAi5R43SglKz8rSkfmhlwutWYMGjzF" +
                "jSvLCUvZGdk25o1aFFft5mBk0f1TiwnbhNPWFbkE3zgLhh0MMf6TaIgCVAhwAI9TBQgzxxwCCMcr0/Fm" +
                "GnUtwGCDJCycB/SBZicSfE9q/glCnjLUXX30BVYD9tPVvhCfNW3OazIq2cWqcluDpXatBffKEuqXxQI0" +
                "wYMSPAtyYlZr4Ad6227X4IElHLFwsGESVbRPeGz0obuqyMjMo7VI55N0912SzPsaPGscTpyA0Il1JRR4" +
                "eXZCRhwDL1BbsBIom9pZ8v9fnplRx4YEJozuX278A1QfC7S6ujj7prBZd7MG+uA+bXMCa/yRDzcB2Cdq" +
                "JMwhPZvCz+bIwCKwBbBZICVoSd5u26VnbXtt64ICQQAMiqIEqAc46eAogVwR6MpWXsEzxLjG54CtAlw8" +
                "0wPwYHMyUU23sKRUxU8XPQ8aApQ+qMOymNUQi43IZNKSo/svSNrIgBNFUOKbBjQjECAnHlZHgKgBHsaX" +
                "4sa9YqCsBX4CkMqh72JB4bP1AIteOzgJqseJCaErqSOM1Ejswkx0qYoajZ14zaBffA0xKwQ2uXcY1mEE" +
                "tbJX6OFjBgC9TnBAwdyit1c1JRtaeAxTDt1kMRmz2qNRHH8CBJKBIjN1sQA7STNhoVWYbI0cDmzh/DG7" +
                "QrRnXgwIBkBq34rZQlu89R34unAG+KMW0SM3S/dFLNJ6P0a5ExB9hL4li6g2A8xFC0I/GQVzdxP+2oa/" +
                "fr0DAxdDqFvtWlFF/NkZOCN9XdoiDcEv4FgjxneYh2jU48VgFoTkCgwHEtfXvPq3+JYDOBqYBnDfeokS" +
                "QFtgumFpr4MT5czZmxccNwW3i0PCFPorHAwDk1Vo/jT38+nOes/aFuI3AJT5siwaPK2fYYwBSsHqOyB7" +
                "A2SlsxifOHrgzSqA5zr/DU2HGE6nTwPoqYDmpV+UdgFozlFNIx8DV0t0jC5aBgwdPXtyx0CmWtKbJFXz" +
                "+Y61o01yiE3zU7dh5ZsW9bZ6jmxkVItRwKvHnfkc/edD3RAMxOAP7UrpbL1vMK4EtLfCZScnYEXdyUkS" +
                "qs9I8DmBgCsWkh1sE+4DdM68x6B6iuf7cjZ4LzMmyLJBHogRl77Mm6ABwMXO6mLGjhPH43R08QNByYDV" +
                "JEGqPdkOlgZUluLAhEmYBxW3+bB26Bri8xpsSdGg7GVHuBvOQaBhQYMJdi2VOXV/FIrNySU/Gseh0Vcf" +
                "Dn3Y0OCHYGVRVuMUNwdqtcFoc0ogBDkC4PUKZ78+mtDBzuNZ0EutCsACMlwOZoBlFQwjKgcMawUR7EcG" +
                "cebsAFlKWDf6xQxVMjmU5ASTAh4LRroQzu2MYQ8HM8NKRlIjLEo61UiurCFbXJDTs0cbNkXDnrSNaohS" +
                "Q82YVtB0NrlTfQzTZshGpuki9OnR3NEkGQ9nRw7TEHdifkALh8aOjY/oUzpF5QGg0GfgNSCs1ZgMVwah" +
                "NwjrtUvJySktjO63wo2IPT4N0zY5u+TzlgqD8NQUv4LihyNjHp7gJFJDeUsKS2AVzLcoD4RtJsihTIlu" +
                "Gl159BLQ5yQKgo3sxxoj8YhArxL/iLWEnyoIySNl+OQRY+AuFMqu/YFTvVOHyKrMsg5A7hHiEi4Ci+Vu" +
                "AW4WMR76W7kHumKywGNkqBobUNJlbVeTLonrMSOzXwaoh3AzZ0m2Kp/o/jdb8EZWjHmsvuSc8Yu+E89Z" +
                "uDbKP6Y/QrbxCpQSKSmTLcFlmJgXKAo34OGWWJSguNTWqiwscdg/3p29IJ32BE35IURsW/h/u0G/mlPf" +
                "4KTzS+RgqvbEeCHdnZWKBHAFQOG5KNG99+Ra4giFBgJ97TD3ZWY2u8ID9/bw/2rsbtXYBlMNy89WYzr8" +
                "/5Iau02LsSeK05tBMuJSWRhGBXbeGbQBguIA/Hfw7gdCE7xkfN1N7Bh2vSd6JHEIaiWU/jZ+J/HQDALM" +
                "0UhTM0kwOPp7ZzE7iQpAQ7a7OWRceF+MDLJaF0HH9w6Cv36Ju0Y8fDIM1L82d0RA4iQ5lmrdJjqP/fPM" +
                "an/l8JAUmDcYGmFogHrYVgtKKlEOchIIKEPibxl3N6djmdhDNSAFkycebgz2HLO5LXm8LSqqzzwiAYs/" +
                "ORS4i4z1LVGo6OzB01AICkGc9GnMixsNVzh5WtCpr0JdCn/EshShslfvtuCNYM11addcgKFsa6y+DjYC" +
                "MNiYpZE2juNF77+cI0QSJqQZHpiAVl6C9EmRSz1jzL0iGl/f3wdRg2224eEsSV0MV6BabB9fXnarCWiJ" +
                "9Ci1EYvuYUlNIGCmIsnkt77LllTN3oayy4OwuamWJG1Zg63YJlX0ZMK9aEAI3FTSK9RDEkvyvDNyMcAL" +
                "yzhy62V8gS7kOIbuEt40J+9WCCMcB2FL4g1UHHZ0gDPN2cCs9NwCUvuOxELASMVJF6lchta43tJytSs5" +
                "c6y1CVkaCQlAQ1InKfbGSD3WhQEluMGpLBKps3HFYhl8mAFVxliiv6r8popFJ55wJ5WlXfl8Jq7BmJP3" +
                "c8qxSrJH/XwSoluLLyADclZB5CGxEoEDOr6C5V9ipkpkOC1XK9G3a8ccgjmOmeVGLMJSFCn+Y4p+54J7" +
                "WvhIfJJLBIFw0qq/ZtREFVNRdtfVU1mWeUUtSgPFZ189J03+RUxceCyQ6koZplBWBRYrMPmI+oh3S8Pe" +
                "6jsMH5Nxw+Rt0xswFTLgaq9csxxAxkcwfCVv9sLClymYr1FstJcAU24OnQRReLE3wcy62NfjNJZhZyO2" +
                "YQH98rxg15tQeNTbH/Ys0XloqdsOiy/THT7L8yblLaFBqDlSnpHS5ckg4N3rwncN+MzuBrwKPELRshJn" +
                "fQTEnm3B6Xt2dnZ6zCu9I+3bW2xe+xXnL6rrovYVdehgHFajO37oIJTbgvIiKaF8cQuS3gyYpMiPZLF3" +
                "56/efH9++khOtl6jLsOYtwqnoyhZFDBtvQn9NL95Yq1l8CQ9LdAjOerbt+evz04fB2Udl92/Ii00BuW5" +
                "EYEQulNd5ZA6TISEGvtQCw2MKN285bjmSJqSGl8iygDDqlOi0s1dU1CvFm2TUPSEN/lmrX0ObKbhJ3qu" +
                "YazX919Md35a6Yzu/+7/zJuvvz1/fomFy98/Wf5jBD3/7WoJ6VWKuedkH0XRgZbDLAfGP43jqDxpHmj9" +
                "gtPuIe7k/C/wC2XZB77IlQt53XSRE3rCIGLyolbeWoA+q0w+C1YBwESY+SzdkBhlSr98e/Hm9UNsM5Kc" +
                "zPtnr74zDGJingWGBk0cJCKp7aE2V9zEvJN4Amp6JuacfI2i2kN9kixKCnh/BW7OlTsxf/jvA0T0wcnB" +
                "c3SJzr4+GJuD2vsWnizbdn3y8CGEMLYEpLcH//MHOWRNzlblOSVUidpkKopXhERK8ICeZ9EewCTs8wSJ" +
                "uHJO6trzEkR3VpQQJ036trXHulgUYTxqufLsa2YSgpJRi7DVLBhnU4jNpE1SkmvNCZWPYI9yYPptCNKJ" +
                "CVjgh4gIeDhExMmf/usvT2UIGmou0cLA3W0f6GoXf//OAP0ah/WQQK/+4he/lN/oEAFPy5mDzaJ58md5" +
                "hDWoE/Onp08e82+YUOOQAr1lHQOuwsbX+fA5Ojd4IF1Fi2ryeuXzrsQBVJ5t/fog8Diy+5dKAN/mYcQ2" +
                "G+pPadbIeWOTbcFFJ68vw+ybpLA0bqpdqPkAm2nqChyjmfoLAAwNAtp/kk32v4/H8H+TEXVj/MV8/ebH" +
                "00fy98Xbb87fnZ8+lp/P33/38vXZ+bvTJ/rgzevz06cjYV3VW2SFcE8yCp+PdFBegDlutOobh8acexyh" +
                "c7BBALefTkiGnXA2kVo7sUSpzbc4FtF1o+rrIM45YOM3Eh7Ft3Bw2ioHIT+OzXtOEP+U7hmRTLGXqxZt" +
                "yGD2tBKm8bDzOukwmkTcTn88PU5+vQ+4xl8/AarTLTH+ZVeUXESyoyKFfyXV3KCbxEqG9LP0ymr3vARL" +
                "zEGTHl2n756dvfzHBewnXVOJTDCRwNw5xFhh1qGcBrVvqC9J6X1Z6idjwSHhpkju2+jBnX5z/vJv31ya" +
                "Q4QtP47imbginGA8nmnZi9BUFswhysIRr4daT9fh08k6/CNZ57ZVsJ+jd/NA45r9az73FScX9BXMj7HB" +
                "UCZnLlx64GbwtlhHHiKc4nyMWLn3byyFk68EqaOBJAr+AksNDg/MlUjqzuCIGBz4ZVTcbrxA0Wu9U9lC" +
                "Z3WYUCNqocPA77lyjdhOMqYTM+J+mVDRS/K8ybi7OmBRhXRoL8mVVt6t9OD3jvuJvO6XN0EYiqrhSbaK" +
                "0ScqiFJubYFVtNUC/Im/JhpW2qKpD5SauENvD5wRa2fg/DQfPo5wjUsBQIUKgTUS5SGpQJ2hEdqV00ZY" +
                "2s0enFP9lifdEar0GHtQpsc6aOKmuNfxwxPep7uZUlrxTnZLMfzecjKXW91YkgExWRAyCvbGfIWZxK9M" +
                "9iv8T25OzTESy5qTU2BwN/9w/BGTk+HnI/yZhZ+P8Wcefj75GOoXH55+pGdfCgGfSAQOSmx7K2yDKcpo" +
                "fI/lX7Rv1TBUcU4uwrBGicVkuRMQBPHDOLnlAD96Fxw+IpV8fzT3PnwMWfhkLTZlfKPS5RP1Q0PypH9J" +
                "ItRPsfJcYyTTL4SSjhiccgJHH+1p12h2+zXgOMnD3rF2OznyTlMTYPunmCmaUov33WRxw92jW/snbVCz" +
                "w8uxyc0lChYR4+kNKc3ihMthcoWMKu960SfUBuhiCIbuEuRodYbIEDca5GFwb+qtkKM3Umk0HPwmGuHe" +
                "+MQ4D6d8XzQSGPdmXIfH/Ql3QLwBYjiLwz/2mPdQDJtx+oKy5FpmoXAsYD7qG86kcMjQVeow6j1B9Lc/" +
                "hFUewFtk8ypz0xnIwWYcd/BV8s7O4AwfY9FCB8Ung7H7XtACkv6Uske8ZhVrQZEk5jB3lW/JIcDiO0Si" +
                "2kHKORDuNk1Z2jwvwdMj3+FXV3u5vgueQRObT4+GxZc7oPsup98qu0i5fOAJBKLEs9I1mluKMtwoTVXH" +
                "IUrJtcI83y1lVc4ezedYZTwUpiRA1CFxFNW4rReuFXPhk3EbRwo7vRd024UXhjElGFNeM+5Brirdun9z" +
                "b6QFjO95aBw15Vuu/468didapo+YmAWyilUg7JMzRY8UlJtba1i77rMSx2t6+JASix1GS0efX/JKLk2j" +
                "nU/b1QbRL6YOW0z9f36V7GUsSVEoEQCOpUwWShrB4cByTB6q9iHfFW7NLYDw9MWDPQcdVOB+42iy+qeP" +
                "lRbs7oBr9hrY36Om+i1In6eqpP7TmympjESLRRIRn8VmpiHm9gft/xzNqJ2FlEN5QJGZcXWNukRtXFI3" +
                "Ta4az+getJveTHHmNIzeM2T76SG/7gz5D9Vy+7y7UP0PJ46XVqlptCD2TVxA7jvOiybjsiYbpaOdnhW+" +
                "mhgutdAE6o3spfdsgLzlVNyGq23FmkqyXH3yeE8ssDm3bxPkV7I9Ujpxh7gcwoL4GcZ2tcbYzMyh1uVn" +
                "3JLNGjpOx31M+AsOLRdMLgdcQSBev7kE8NyRtoI94CXX5H41HLhlFo9fT9HN3wsN1Io//GJDzXCLNoBN" +
                "HAe9WRRbdxEf14XbJF/BENQAc+/6T6S9D6nLC/BgZ+VWum2P9FsJJABNh23FXU1qdJIogl52lqhJVi5+" +
                "SER5BcOcgr/pkXi17Lrot19SbZ/C/KuGqXoz/1pT4MORdN8sW+IVe70oHSnFdkqRpbel+9/1CIptzO3U" +
                "XK3bZ1ku5D5ucL8a0SEvtOOB8a582vqNraURQ6kbts0iYIf8lnKZk0s8tQdOsrFnZIVfEaEax6B7n25Y" +
                "8pinPGAsV8ktJztBv5E70gxZXb5ugvGOWW8BUUUeRZa/MiAEtuUGmxZg5BPicl+78EUK3PO0iZ+9CZQN" +
                "9+X7wsm8HPklVGGZVOBVyaLxZurAvgGE6B8I9klfcPtY8pWmeKUiDAwVab7sir3ibY9YqMoA1fEubEAJ" +
                "QVtrz0/XdHSRm+7b1Z3j1p+kr2dXCqlCzKVjME/X4q8XABbReTvvyZ2VyHvn16RRfLdYRuZCv2NHCsf7" +
                "9JvKCDblmGZlWXpmLrNdlLM9PgYts7fPS1QPY12/6kOfzIisioNJZQmoK2ArvOrIvUOWeHccuSazfFN3" +
                "Z/OKRGwJAg97aalRBvsbaYlj8UC52EMLsveRXM+Ty8GY+6ZXoqRVk1bYWlDq4ZL1CQGquwOH6IXr+V6N" +
                "EPibJYri+mHHvVoQ9e/irlnYLfXzIMLxzZj3GYRUm2KPxSZvvOyI3WZAPzbGriksds2RgsRCT+labFtR" +
                "YyhL3wL/bfHwMS+RgoedYXcWa9ajSao9wm1IOjP1N8l5xUwm1NlRC/KtrmBhrAAVxe7X3NPuanhMfa/H" +
                "Y9oh3wg/Ds3G7Q4XJLY7H371BAZOaaAqM/rmmUwkO9lAZAQnLbfsIqVzSAOUGDn1jykA7+9xBtRs7DCT" +
                "chm6r9Jz4quKmpXhMX+tpG93UodBzB4zYMJ9eBqMy8I3cMiEBtSpazJAErWl6Tdq+ggjAH2MpXtOdbAP" +
                "Td0D8VFXRrMl0U360Zyax2PzHv55NDY/YR3knpbTz19fvHk3/el0+OQ9dg32nvyInXz8RDQpkSxs4D8q" +
                "JrjVyhAK8LdqeP26zPDiD7Omfi1kUNIhAGio7iKm2fvlMeVBunkGrNX7opkP+fjkdmQssgy/7dZPeP8v" +
                "3ZXdlEVTAAA=";
                
    }
}

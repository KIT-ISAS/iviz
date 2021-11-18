/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Detection2DArray")]
    public sealed class Detection2DArray : IDeserializable<Detection2DArray>, IMessage
    {
        // A list of 2D detections, for a multi-object 2D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection2D[] Detections;
    
        /// <summary> Constructor for empty message. </summary>
        public Detection2DArray()
        {
            Detections = System.Array.Empty<Detection2D>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Detection2DArray(in StdMsgs.Header Header, Detection2D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Detection2DArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Detections = b.DeserializeArray<Detection2D>();
            for (int i = 0; i < Detections.Length; i++)
            {
                Detections[i] = new Detection2D(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Detection2DArray(ref b);
        }
        
        Detection2DArray IDeserializable<Detection2DArray>.RosDeserialize(ref Buffer b)
        {
            return new Detection2DArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Detections, 0);
        }
        
        public void RosValidate()
        {
            if (Detections is null) throw new System.NullReferenceException(nameof(Detections));
            for (int i = 0; i < Detections.Length; i++)
            {
                if (Detections[i] is null) throw new System.NullReferenceException($"{nameof(Detections)}[{i}]");
                Detections[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + BuiltIns.GetArraySize(Detections);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Detection2DArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "5f62729a3134356dd48cf97c678c6753";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1ZW2/cuBV+F5D/QMAPjndnlDQpjCJF0W7ibtcPbdMmvQaBQUmcGSaUqJCSZ5Rf3+8c" +
                "XqSxJ9l9aGzEsYYiD8/1O5c5Ez8Io/0g7EY8uxKNGlQ9aNv5ldhYJ6RoRzPota0+YH3eYV1ZFD8p2Sgn" +
                "dvynKM4WpIadijtVI3pne+ul8SV2BHppKZMTrd7uBrFVnXJyUCAmQET7QHGvh51oZTeJWnaNbrBjwWo+" +
                "1YiNsy2Y9rrbGiV0149DWVylnc+u3r1fnCseFb/7P/88Kv785k8vhB+am9Zv/ZOgokcQ580AzqVrRKsG" +
                "CQEk63cHqZVbG3WrDE7JtocQ/HaYeuVLHHxLWtBJSGMmMXpsGqyobduOna5JG4Nu1dF5nNQdVNFLN+h6" +
                "NNJhv3WN7mj7xslWEXX88+rTqLpaieurF9jTeVWPgwZDEyjUTknSJl6KYtTd8PwZHSjO3u7tGh/VFh6Q" +
                "L4fJ5EDMqkPvlCc+pX+BO74LwpWgDe0o3NJ48ZjXbvDRX5DJwILqbb0Tj8H562nY2Y4d6VY6LSuyp4f9" +
                "jQHVczp0frGgTGy/EJ3sbCIfKM53/BKyXaZLMq13sJkh6f24hQKxEY57qxtsrSYmUhutugFeWjnppoJO" +
                "hSuLsx9Jx8GJ2SL4K723tWZPJZcu/OCIOlvjRjffziFvtYfHB59chAM55pXa6E6Bt6P4FzAfIhUusnBB" +
                "r1tNjgTf4921gUB6Qx6IIytRjTB+V5uxAT3Et2ZKuoOnt2ELxzV0bfckt7xDIV4akcf3qqZXogZYCCzp" +
                "Vm4V6MLtwEKACCsqJYylGGjI4ckmYJG8krefgqlXdClZspKVNmBS+eKvDHA/Tb0FBa/9v2Ce13AlAEZg" +
                "ytNJyFzZEQgE5it7gFs4lz7TzQEmy+JlXHxpD3QEW+n0W+wgHXN4U6DMuEWXqnQTHLVUJT5tSScZKkkN" +
                "FN4WWrabiJAqiHlRir/YAUc+jdoRDpIGjSGogG97BTj3VugBIDqRxlTbD1NZeNV564JbXLN2vR1dDV9s" +
                "t8TxNZDcjWoVvBhB7mkPwmuQGrgbs8LgZP2RVLCwdFlU1ho4zU16y/SuAngRe4Q0wHYgzyQkZINJODf0" +
                "iMjZDeOdSB3shLfSjAqxYwzLb/RHgqlGbzawMIM/qwQ3aGUaIA05pFASoKJhEYTuCE3eNXYZRJ3syCdY" +
                "WCYAl4ZKRqfY5Q1UyBRnoWj9LTRUplBOLx4smr/kuBTaP3TJRLv8PnheNuGpKC2TsyK5IDWQ1WJKj9Q4" +
                "amERCw8ehGwaJgHFLogIiUgZ5jx+fQUXHGEHCSiBj+9G5PM1skvDGMwkAd8tnI1SPkUGAGdnR5ixV47o" +
                "ChmMbu3HsQ/ZLSdT+q+Co5fijVJHCvonP1+DsxKf2fVa69jHpDbZcLBXlHoGholQB6raAPEplwTvu1ve" +
                "xKAXL1Gd2O4W6SAjHUseTkVRkC4STDnZIZjePV3/6n1ZbIyVw+Wvha/BW+Lk8orsk2+8Z8sYFLwn0q9C" +
                "6dQwqFP25ZOJirfISBt9wBunEDIsVQDUmBTDHclUGWS2CiehKIAx5EOsRZaO0BDK4pSYd7Rk07CTKSV4" +
                "PCOwUsETWUXMH/52wLBYxfw2ZMUjl0LxRwGZhKsmpooKh1JwRgpyfArVLt8R8IE2sZtRcgJIarbeJnLt" +
                "w2UoGANAE1Du5G1UZyYwH4zGwX1ROVPwNwo+CsJXlguMoGGvvh0a/Nz1hATsJ05RUQYpKNkHlXfQMuLF" +
                "9xJscpU94oQjbKD8UBC1wD6I/N3u1638AIVlSsEu0RkuD5eIgCw1jOb0IbqydTpvh8GgbPiI5/wkg0eu" +
                "5WHJY0QiADPoO2RuSkJqeRZmp9Ly8WElppX4vBLODgvoEf8WRPHe8n9OL/+Xly9SIL57fvl+IcxDWo+R" +
                "+4SK71tsRS0ALTfxfYByFK1LfZcCZqQQTxuKv40oOlzHdOd9DycjmMlOmTNRxKkgAsQhFyWujyTOOHnI" +
                "T1N++vxQEsz6OxlbR1q9E2P49GnWPkEbouzrQqWn/cMUFEe1a/DFu2VvKCECGHMYUabhKKLMHvA/wDAa" +
                "fHREKPSoTUfAkvw9ot340C1ktYHCyH0mS4uHNVuf0ZcTqA8lGDoH5KZqzUQIjWtmvaT6DQypg0SGDC+5" +
                "bWDThKKYaYXWIjG9lGpF5XEUCu0gtcwzVy+Zy51sz2ki0YWsIM0Whh52ZMJc3WcXfpxFvbgbkSezZ1Lb" +
                "fUCgTotfpmu8/qyO6J+id7o3gTYgF7r71D4ty4Ol9XJBgrtuZsfkj9NDwmHqUsnWZBF8eG0wlEB5il9i" +
                "fzPCz56HdMymQMqI0xI0S5StaFKiMH4JAcmuFDNPPHofbTGRcZgVcJsK65HfwA65J5V9b2LXir5Ffgyc" +
                "YBVOh5SBygNPpHDeAE+OmjayU6nm0C47Gu+igLA9Rgg85aEmhxzcK3cbqIPPZTXUjFy44p4apQ8eg+wb" +
                "sVdcuXCuxFgDkWB79LhpxIfqhodLQUTxj+vgnrjBwaF6Rf4dbofKBCrtTqHmCb0O0+UeimY/bgNIY232" +
                "BpXbrDIM+nCU+qklHa4vqMSrFKuV6v5oCYg2oF/0Sc+5byWiMBn793QOAKGMZ1AAQEPABY0WDTUubjpv" +
                "AnMg6JSmPbA4AVK4dq7guMYjlOZOmp4BHgPimjibp2Wgv6NxXRJuyOFNxQyzMXJ9qcQk90Ih2FwuJrls" +
                "Su0y3UTKxRP9Gup5piguvQuzIzJQxtXGxiYU9CAbDJvyTBqsxRLuZMKnWp7nM+iw9Maa5mdSDJgb5Dcc" +
                "ht4dMuSkeW+gANFRf0K1cXjIsw2q8Z6uxNMLnqDR6KfHwHRDTaxDkDNwhX13Zj0AOP45E3F5nlPmbgmj" +
                "B2SHqEee3mVy6fjRT6aVZnYLUhS7COjYuIBOjb9OfokQLLYFhmNfOHCf0NxJfZ3S94fcKcfhWGgweaQe" +
                "u82vyvT9dEygsfvulx38fHwwAJ2NOBfT0lcp0IiJMDypMwRKCJl5NVJ6xWqgXv5L5HLQHU9aZz64nwd0" +
                "D1+iwIlUAUs0jRzRonSx3yzS/BvITYqdjwTCYXmVZuAr0Y1tFczn7N6n03vdgJ97p3n55OHamrHFtxUx" +
                "+Ru1hWvEmogAAk28ZXyO9dVGA1e9q58w4Zv02pd138+Drr0MjuLj9xJUUkhAPcqrOJTmRLMSH2BYHMOA" +
                "bg1Udv4PNJ7xZRgUYhNmrB1NgWAyRHrDVViLyUocXIY5L9FNjACN4hWZ86SKP6YFame4thHrtagxhu+Q" +
                "xloFPOu2q9AP8hOVI6cNKWBJpGUqvuNUMH03FS6npJyG1U+WEHVXa7tg99/Q2K/SqCsadITRcgTZabyf" +
                "3/0+f08yqP6IoR+p2oAvwIbdFk4ADqppwPSZL6DvpojQ4gCKWcq1oZPmt0FkuhnfnoD8d+xbF0Dv/wGd" +
                "G1AsyhsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

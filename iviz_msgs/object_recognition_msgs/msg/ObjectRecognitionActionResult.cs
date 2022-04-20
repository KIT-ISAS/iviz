/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectRecognitionActionResult : IDeserializable<ObjectRecognitionActionResult>, IActionResult<ObjectRecognitionResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ObjectRecognitionResult Result { get; set; }
    
        /// Constructor for empty message.
        public ObjectRecognitionActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ObjectRecognitionResult();
        }
        
        /// Explicit constructor.
        public ObjectRecognitionActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ObjectRecognitionResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        public ObjectRecognitionActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ObjectRecognitionResult(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ObjectRecognitionActionResult(ref b);
        
        public ObjectRecognitionActionResult RosDeserialize(ref ReadBuffer b) => new ObjectRecognitionActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference(nameof(Status));
            Status.RosValidate();
            if (Result is null) BuiltIns.ThrowNullReference(nameof(Result));
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
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1ef766aeca50bc1bb70773fc73d4471d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8Va/1MbNxb/+fav0IQfgI5xmpDSlBvmhoBT6BCgQO+ul8kw8q5sK6xXjqTFdm/uf7/P" +
                "e5LWazAtTRvKJMHWSk/v+/u8tzlSslBWjPhXJnOvTVXq/vXYDd3z740sL730tROOf2Vn/Y8q9xcqN8NK" +
                "094L5erSC8u/sr0/+Sd7d/n9Lu4uAj9Hgcs1AaaqQtpCjJWXhfRSDAyE0MORslululUlMTyeqELwUz+f" +
                "KNfFwauRdgJ/hqpSVpblXNQOm7wRuRmP60rn0ivh9VgtncdJXQkpJtJ6ndeltNhvbKEr2j6wcqyIOv44" +
                "9alWVa7E8eEu9lRO5bXXYGgOCrlV0ulqiIciq3Xlt1/SgWztamq28FUNYYrmcuFH0hOzajaBfolP6XZx" +
                "x1dBuC5oQzmwRVU4scFr1/jqNgUuAQtqYvKR2ADn53M/MhUIKnErrZb9UhHhHBoA1XU6tL7Zolwx6UpW" +
                "JpEPFBd3PIZs1dAlmbZGsFlJ0rt6CAVi48SaW11ga3/ORPJSq8oL+J+Vdp7RqXBltvaWdIxNOMUWwW/p" +
                "nMk1DFCIqfajzHlL1Nka17rIvpA3PhgjGX2EZYf4RfeTgV+nwAlfznunh8en34v0sye+xr/kloqPiZF0" +
                "Yq48OWRfkX7yYPiooHA3bG5vEQeB5v7B1fE/e6JF88UyTbJIbS00CyfsK9LRowifX/R6786veocN4ZfL" +
                "hK3KFVwbbgmTwz1oBd7vvJADD0/WnqS3ZCA14ziohpn4lZ81/IWTsBaCwyEqJ6UiCtq7RAWMblwpO0b0" +
                "lZQKvNqMLF/+dHDQ6x22WN5eZnkKyjIfaUVsuzonLQxqygOrFPHQNftvzi4WeqFrXq24pm9Y9KJmt1zw" +
                "vvKmola/qRryCmcQBgOpy9qqh9i76P3QO2jxtye+uc+eVZTJH/AADihT+7vu0vltHvsql8ipTLO5rEae" +
                "9BKcUoZAptbVrSx18ZAA0fOaSNkTO0/geY3rVcZzEC6crzFeo+GD/ZOTRSTviW8fy2BfoVSplRw+Rruw" +
                "yX1rLTNdDbQdU1Gj8uHbWYA5UcWSEG03ef0nCPE4NZNTLIVfuIDKxgM+cXJ2edUmtSe+Y4L7VVJGrB6g" +
                "JApYjYiooATZqICodAMKcHDwsmC99R8Re45oG9I2qXSqIT4iR1Z3Ume2tl+WZsp4hDYiFCzFbVOswEws" +
                "VBRjogWx6Eih+vVwSGqMm7ya+ewJS9nxYRY8IECQqCTnydwkD9dkqHQ60sAWXI9bKYW9QxWEhY4ZutSx" +
                "xtzVE86rivwHUipHCgLEUeMJbFWWOE00XTDeVOHqhnRyPbikspRSmKM2VIj8I7tEeIFUDPbmy1YYKFX0" +
                "ZX5D3ogTAb8CTjonhyqYxk1Urgc6T8HAHLhupE5YL2wAU+OagwJ5TmNXNxmPQMgXMp1hIH5tF0g82PAB" +
                "gJ5lDx2IW39RRTi6by10ZZvV63DQPbEcK9nK1j7nRxz19g97F+KzDoef7GipTUqNRHIXai8Qui63us/u" +
                "NjEe6ULDbVyOTiMk5WFtJQm4SykAmdoMovTR0SlxAeqX7NQUgXT00XZ7/yFRyz5TTZe9/YuDoz+kphh7" +
                "udkyecCcwHBjiWiYITz8VKkg2sK9GqYHpZHohyAGOqvm8FOHz12t/nUeF1UZPC40NdAMtOIm3JAhm5Em" +
                "J+jQwtOOOD27imuomNd5aeoi9aZ3/ff3i3T2hkClOD59e/b5cpFUB6aivI02vEImH3NIEFiOyIZjiSBC" +
                "lI6NQ7FCPX5dJYehIQBBYTwAUiUdaFVCLWM9HPlY1LHGiKINSUO7r/IRZ2o0k4Y30056PiZ0emc/qrJR" +
                "DLC4UAQERHuTjZhlZkQt8/jrSfqKjtFZ0OEEUZDH74oR4IMjeD83tZABZumYIwJF1g+xjQYdl1phKrWo" +
                "uSnQvuZtLxplwiEiGMH2hqii/UBLKQDFgpXf7yfJSw5Ofrq86l1cfq6nZNG8PAthPYTk6C1QBDO9XQQ3" +
                "j3rpq9IQWgpBEbTUETGRcgy4ZCFj9ZDhJRCdM9bhqo2gDAIpXP1kuDP40kjeIhejYbSLM5tZ+BDMeU6M" +
                "HFC4UfpqRZ9bkqPTCALMNU/oBH7dJ58hvNA3s+duJCeIZdxXEf/bRWtqFeVZxHzGuwMX75QbNaSucd9o" +
                "xe18M0v0Ei11NUHQOZP4Wqc5Hg/FhoD9XMacCuc+EsAZKoLS8A9vatuhSCHeZzEKSoktLDzgE6IJ8Vfq" +
                "6iZM19gM2mIH+Z6svaHAp0HRPBsq4tHOW8qEGhtJ4n2fWdbE+dll70/IWskEoXoxWGTHI7+Y900xp9LN" +
                "tXx3YSPYK/TrnL1JqVWAlrSDtR/QJGAjDlKoW1loWT0fU/9Cnkm9s6tleU9JTv0LhA4Mz90Qq5dxXEn3" +
                "/jWYk9LZHyksh1++rNyrIqR+MpSfGoJknCMmkqxF+ofjwuUpvwZcj8Ao1IC6Slnhxpjko/vfqPmqGhC8" +
                "IZwXlFY9dQw2hXLwiaKfOgZQSQSLfpsV7sBoAAwJfrg8O31Ow4k4Ff55/91JbCq71A/HIoEOKBUuYLAb" +
                "xclLNaMdLgkNoKCWc41Hq33pVFf0usMuJ9D7Ru9Q7aA0Vhpzgxi/Qc169t910vD67vqBqfPR4Zv1jli3" +
                "xnisjLyf7D5/XhqEO7Tt1//3LIhoeVpc0ey9uiXNGErLwXqxgSbjtLRApUz7dRzSAJbIKzdKxTn5oFQz" +
                "3del9vNu1OAKf825dyQl8hsANHeHb4JvNBAcKbBYwhTkXGF+ihiXNBLiKfxbMBiFpa+CyeyKRgG8RirA" +
                "2l0V7H7z3etXYUdugFHy0Bes3+d4Pd50+eMJgAHQyMiURWOnpYsvP5VHaUegzVeJ9enQbe+ElYmxWPnm" +
                "1fZL/krohjZomlnEHeiRp8YWd5YrmIAESRektjA8HZuiLum5pwmfN5P15NBw7S/1Vmh1Bb7bmxGzjstW" +
                "0jOF6elWgaJYueByAUl04lBjTI0mlznqxGRR6OiZ7cziYGTBYzeslI5CxRM9P0ft9jnndVAJ9TC9sWgH" +
                "cR8YxM5FvzT9Dg+3SzmnsExtZBrMRlZU5RsE8yz45bOAVropYYS7AuznGyEIVWFjhzL0Wqj5G3oMrWxR" +
                "1G5SBX9B+GKjhhhoDFSx2RXnCzKudRZME2Sg0y5RBoQp6pxZJTbhIlZCgAkNTwJQavTkEL/K0I38LsgM" +
                "tgYlwavAvQ5IPhwKzMv8U61dTDgNhL3z0o2qxgZlCoZJwYyb97sd8fKQ8mOde4LVUYstdXXF8SBBZCiP" +
                "5lZJIR1QYRioKQ8EMD3VBSSMYKBU1RDfVhBNb/gCgfSNDxNPh828gG8eyapSpUuiapscIlaK6C+sG3Ka" +
                "bsaGekuuALgUU1XWN6akcaV2132NHEdwQqAzcK13Z82DfySmImiFkSY87zxphJJRJE3vLbxyzQlrpmn/" +
                "3RN4tLz/NRikm/8WN7+/gDt/EPuwR4o+ft4RDr5Gmt1I5L8K2tuMkkGsgqtYM5q9slRYWfuU2vEXBSW9" +
                "Xwge8WQZiI2xOgGRCUNoT1IOIiQcZIc27DwaGsdb2ayhEjJPNw7Dj0+vXoch+Iu48lNc2hMvF3te7PDK" +
                "dmsPLe2JV4s9ZEt6Q9TaQ0t7YieuvD0526elPfFte2XnFb0wyFKep/qQTHIqQzizTyaHMYMBDb14w1n4" +
                "PLBmHF4gMO5iVYQwjRexU3B1xqHD9FlVNSWakBmcUoTyblW6Jwf48pGRIzjiWFbA5qUacwZNHSFz9qXc" +
                "Yrkn41AHamwNMlJzVmrHonvC8EPgir/HzqBQMwEPpgGFVYPQtKdZCAuBrgPtk3LvP2R0x1UkgDhraNEF" +
                "8VURRVk6EYAO48F6whuYm9VNWDr0RKpKYqxQWRIL2K9hKlj8/XbgU82uobgvye0KHaVgz1P3cX9q1eTP" +
                "gYWvuonMVZi1IIJmzad58+mXp2L/gRYyiZT+1wnPRZRD2eb/asGNLTexMvimCu/p7xRmLmPpf7Jkdyry" +
                "/bu/aN/6W4Inia0iiTlPyKaBX1gtSF1X8cUT9RksSeR9TVyY6dZYfgQcaSjJ5AfkFzuzHSiqETnMxVOv" +
                "YnWzvdXw0BgSLaueqWJLzto88lYe+oE+YZVO8L1Ws2S5P9iYdQRA6S8d1GXfbov/LYjiveWfVy//h5c3" +
                "k5u+39750BLm6UwHifZX6Pe+uTr8/t5Qgxyeh5gkx2wpuysCjmo2ZD/W8GJbMd3FvqcRcHH3Kp9cYuiO" +
                "b+LbpwXjhBbgnb+eZtKnaZb9Hwvh/D8GKAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}

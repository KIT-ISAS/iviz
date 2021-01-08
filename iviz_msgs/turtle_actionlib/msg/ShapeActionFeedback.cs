/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = "turtle_actionlib/ShapeActionFeedback")]
    public sealed class ShapeActionFeedback : IDeserializable<ShapeActionFeedback>, IActionFeedback<ShapeFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public ShapeFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ShapeActionFeedback()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = ShapeFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ShapeActionFeedback(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ShapeFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ShapeActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = ShapeFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ShapeActionFeedback(ref b);
        }
        
        ShapeActionFeedback IDeserializable<ShapeActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new ShapeActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC9c0b/ATM5xO7UTpv0I/WMDqqkOOo4icdSe/WAxIpEC4IsAErWv+9bUKSk" +
                "WJrqkERjWV/A24e3bxf7nqQiJ4r4ksgs6MoanT6WPvevbitp5kGGxgsfX5J5IWt6R6RSmf0jlts3g2T4" +
                "hR+D5MP89gZRVcvkfeQ3SF4I8LFKOiVKClLJIMWyAn+dF+SuDK3IMNeyJiXir2FTk7/mnYtCe4G/nCw5" +
                "acxGNB6rQiWyqiwbqzMZSARd0gEAb9VWSFFLF3TWGOmwoXJKW16/dLKkiM9PT/82ZDMSs8kNVllPWRM0" +
                "SG2AkTmSXtscP2Jxo21485p3YONiXV3hM+XIRc9AhEIGZkxPtSPPZKW/4TDftWe8BjxEIgRSXlzE7x7x" +
                "0V8KxAELqqusEBegf78JRWWBSGIlnZapIUbOoANgX/Kml5f70Ez9Rlhpqw6/hdwFOQfX7oD5WFcFkmdY" +
                "At/k0BEra1ettMLadBNRMqPJBgEPOuk2g4S3tUEB8o7FxjLsi7nBq/S+yjQyocRah2KQ+OA4QMzLo1aD" +
                "5Ku582S1DBJ+jyzneIkcONlvt0XUfbqffpzMPt6K7jEUP+A/+5TiRlFILzYU2KEpsVBZa4KtUm14pN+t" +
                "uDRa0NF4MftrKvZAfzwE5eQ0zkFjeDIlluo85PuH6fTD/WI66ZFfHyI7yghWh0mRfliFv0E1+CDkMsDX" +
                "OrAAjjNFT7EubD5IdlSfP17gCcNEIVr3oVJrQwyhg+9gQPViQa5EQRruD4EuO9LzP8fj6XSyR/rNIek1" +
                "oGVWaDQOBVNmLMSy4eZwTIuTcUa/f3rYScNxfjoSJ63i6VUTHbpjfzSUauj/1WFv+Ao1sZTaNI5OEnyY" +
                "/jEd7zEcip+fE3T0N2XM8CghLq+qCZ+b5vszWKaUSTTbCNpHa9A/gwRX7hno4dqupNHq5BG2BuxLZih+" +
                "+RYG7B1oqxDLcefBPoM7lceju7tdUQ/Fr+dSTAn3GB3leJbCSMzzlB3StkvtSr7x+FrpUxG7NVMhdXiM" +
                "fbO8/QLHOFNqtsZBIbYR+Do55Yy7T/PFPtZQ/BYRR7bTY3urAEoopI5RqNVB9iowynU7JXgY3agoXXpO" +
                "FXoGr1hxlnWtoQBKCME+66S4wkbGVOs4s/BSFAXeVLtbDHy2FxiXm9ibwHiLorTJ86jldlWgp8C43/KS" +
                "m03acWp7L3dq+cCZ51PFOxvarguN8SNe13s9JhqFVJyZZnG+iXPYEcEAQJa9hLOSZ50wB1FZI2vG8HZG" +
                "9W0e14TgPXjnQ/iTHDeZyOlwmugOgZazHULQosERvW8/Id2Qy+bkLZjEGhMwfnovc0420uRryvRSZ111" +
                "RBaezcTwcTBsV4BZ2cQyQfvTWAYVtplsR5WvlsfQuGDosU/nq4NRfpAg8n++zm5RDAwAAA==";
                
    }
}

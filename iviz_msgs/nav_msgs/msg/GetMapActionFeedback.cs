/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/GetMapActionFeedback")]
    public sealed class GetMapActionFeedback : IDeserializable<GetMapActionFeedback>, IActionFeedback<GetMapFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public GetMapFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = GetMapFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMapActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, GetMapFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GetMapActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = GetMapFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMapActionFeedback(ref b);
        }
        
        GetMapActionFeedback IDeserializable<GetMapActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new GetMapActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC9c8b/ATM6xO7Ubpv0I/WMDqqkOMrYicdWe/WAxIpEC4IsAErWv+9bkKKk" +
                "WprokEQjW1/A24e3bxf7nqQiJ4r4ksgs6MoanT6VPvc/3FTSPAYZGi98fEluKNzJ+h2RSmX2j1h0b86S" +
                "4Rd+nCV3jzfXCKtaKu8jwbNkIEDIKumUKClIJYMUiwoH0HlB7tLQkgyTLWtSIv4a1jX5K2ycF9oLPHOy" +
                "5KQxa9F4LAqVyKqybKzOZCARdEl7+7FTWyFFLV3QWWOkw/rKKW15+cLJkhgdT0//NmQzErPJNdZYT1kT" +
                "NAitgZA5kl7bHD+KpNE2vHnNG5LBfFVd4iPlSEMfXIRCBiZLz7Ujzzylv0aM79rDXQEb6hCiKC/O43dP" +
                "+OgvBIKAAtVVVohzML9fh6KyACSxlE7L1BADZ1AAqK9406uLHWSmfS2stNUGvkXcxjgF1va4fKbLAjkz" +
                "fHrf5BAQC2tXLbXC0nQdQTKjyQYB7znp1gnvakMmg3esMRZhV8wIXqX3VaaRACVWOhSJD47RYzaetEq+" +
                "miGPVshZwu+R3BwvTIFz/HZTN+2H++nHyezjjdg8huJH/GdnUtwmCunFmgJ7MiWWKGtz32nUBkfa3RLF" +
                "2mKOxvPZX1Oxg/nTPiYnpXEO4sKHKbFMJwHfP0ynd/fz6aQHfr0P7CgjuBvORNbhEP4GBeCDkIsAM+vA" +
                "p3ecI3qOpWDzZEv05WOAP/gkqtB6DoVZG2IEHfwGBUTP5+RKFKDhbhDooqP8+Od4PJ1Odii/2ae8ArLM" +
                "Co0uoWDFjFVYNNwKDglxLMzoj08PW104zM8HwqRVPLpqojO33A9GUg19Vhp2ha9QCQupTePoGL2H6Yfp" +
                "eIffUPzykp6jvyljfgfpcE1VTfi/Xb7/PMeUMom2GjH7YA1aZZBgyk0CzVrbpTRaHTtA57y+Uobi12/g" +
                "vN56tgqxCLfm65PXKzwe3d5uK3kofjuVYEq4reggw1PURU5eZmuftF1oV/K9xjdIn4bYmpkJqb1D7Nrk" +
                "7Rc4xGkysyn2yq8NwDfHEU/cfnqc70INxe8RcGQ3YnQXCJCEQtYYhFoRZC8Bo1y1g4CHwY2KuqUn1J5n" +
                "7IrVZklXGsdH5SDWfutMBiNjqlUcSXghSgFvqu19BTLdXcU1JnYmLN6iKG3ynGXsFgV6Dsk3vc1mEx6y" +
                "2ATtINLp5ANnnI8Ub2aouio0Jox4K+90lWgQUjwRzeIAE2esA1JhP1m2EA5KnjXCoENljXQZg92M6dv8" +
                "rQihe+iN++BKctxVIqPdgSFp+aPBdEMGujHoodHtJmIzu7IhsQNTVmMChkrvZc4ZRnZ8TZle6GxTD5GB" +
                "ZwMxOjZ1C0CqbGJdoNVprLra5A+rvl72rFx2edubys8Q8T9cZFyP1wsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

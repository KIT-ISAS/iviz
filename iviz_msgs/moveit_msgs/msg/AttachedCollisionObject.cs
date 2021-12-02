/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AttachedCollisionObject : IDeserializable<AttachedCollisionObject>, IMessage
    {
        // The CollisionObject will be attached with a fixed joint to this link
        [DataMember (Name = "link_name")] public string LinkName;
        //This contains the actual shapes and poses for the CollisionObject
        //to be attached to the link
        //If action is remove and no object.id is set, all objects
        //attached to the link indicated by link_name will be removed
        [DataMember (Name = "object")] public CollisionObject Object;
        // The set of links that the attached objects are allowed to touch
        // by default - the link_name is already considered by default
        [DataMember (Name = "touch_links")] public string[] TouchLinks;
        // If certain links were placed in a particular posture for this object to remain attached 
        // (e.g., an end effector closing around an object), the posture necessary for releasing
        // the object is stored here
        [DataMember (Name = "detach_posture")] public TrajectoryMsgs.JointTrajectory DetachPosture;
        // The weight of the attached object, if known
        [DataMember (Name = "weight")] public double Weight;
    
        /// Constructor for empty message.
        public AttachedCollisionObject()
        {
            LinkName = string.Empty;
            Object = new CollisionObject();
            TouchLinks = System.Array.Empty<string>();
            DetachPosture = new TrajectoryMsgs.JointTrajectory();
        }
        
        /// Explicit constructor.
        public AttachedCollisionObject(string LinkName, CollisionObject Object, string[] TouchLinks, TrajectoryMsgs.JointTrajectory DetachPosture, double Weight)
        {
            this.LinkName = LinkName;
            this.Object = Object;
            this.TouchLinks = TouchLinks;
            this.DetachPosture = DetachPosture;
            this.Weight = Weight;
        }
        
        /// Constructor with buffer.
        internal AttachedCollisionObject(ref Buffer b)
        {
            LinkName = b.DeserializeString();
            Object = new CollisionObject(ref b);
            TouchLinks = b.DeserializeStringArray();
            DetachPosture = new TrajectoryMsgs.JointTrajectory(ref b);
            Weight = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AttachedCollisionObject(ref b);
        
        AttachedCollisionObject IDeserializable<AttachedCollisionObject>.RosDeserialize(ref Buffer b) => new AttachedCollisionObject(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(LinkName);
            Object.RosSerialize(ref b);
            b.SerializeArray(TouchLinks);
            DetachPosture.RosSerialize(ref b);
            b.Serialize(Weight);
        }
        
        public void RosValidate()
        {
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
            if (Object is null) throw new System.NullReferenceException(nameof(Object));
            Object.RosValidate();
            if (TouchLinks is null) throw new System.NullReferenceException(nameof(TouchLinks));
            for (int i = 0; i < TouchLinks.Length; i++)
            {
                if (TouchLinks[i] is null) throw new System.NullReferenceException($"{nameof(TouchLinks)}[{i}]");
            }
            if (DetachPosture is null) throw new System.NullReferenceException(nameof(DetachPosture));
            DetachPosture.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.GetStringSize(LinkName);
                size += Object.RosMessageLength;
                size += BuiltIns.GetArraySize(TouchLinks);
                size += DetachPosture.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/AttachedCollisionObject";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3ceac60b21e85bbd6c5b0ab9d476b752";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVZW28bNxZ+n19B1A+yN4qSxmnR1cIPjq02DuLYtb2LJIYhUDOUNPVoqAw5ltVF/3u/" +
                "cw45M5KdzRbYZB0jFsnDc79SO+pqbtSRLYrc5bY8m/xmUq9WeVGoiVHae53OTYYNP1daTfN7LH6zeemV" +
                "t8rPc6eKvLxNnK/ycsafx6VemCTZuaLD1JZe56UDKLClvtaFcnO9NE7pMlNL6/Bpais+3+Ii2QGJLhNM" +
                "0QjBnZMp4QOwApnKLOydYZSlVZavD/KMjpzxfaUhjuy6ZOcxfCovszzVHruTdStGowghkCXbihKkkJbV" +
                "CFrKTvk6Say9iB3pBQ6UrrBZFHYVeLB1OgcGEM7MVNeFV08bxoQNyKGLyuhsTRp1eWYq4TRcCPq/vhFk" +
                "Y+aAuIKWUlORCQJXK9xUy0KnuI9NrZa68nlaF7oia/gax2IP0BSGiUfITzgaUYB61wxmA6i2VAZqN9Mp" +
                "QHExLawjV9CVrbGPY8Gy12eZIo3SpMY5Xa2ZWmUKo+ka8BJUIEzmA07Qm4PtxFeatm21Hi/czD17Q354" +
                "1WxCGcTdOJCIRlmZfDZnuzxijL7Kp+q2tKsymRZW+x9fBvgkOfgf/ySnl78MFblR7oX/B/6uDiGohnH7" +
                "qnbgkVQDEU21rIwnpQYNGpe8ZrgAHkWFxwcxgwJ32XsYF6x3Ctonfi8Ga57Fe9HO66URp8i01xPtcBK0" +
                "00SP/B1XJrWzMqf4E1lEhCvCQGgi5jSKqGbGLgwIU+Q7Z9Ocg43zSsvwQK7l7IsECU9hkMq4ZXBFgqZF" +
                "Ps3ZMaICLm0B+SOZVC2rfAEG76AszjjCJ0OdxyMETAcs3A3edQ4OuufkVwACpVPj5ptYaQewCzl4FA+d" +
                "tSheUXCQERCKJQTdNZ9qHbNZI11fTWrJIQymUkQTkpGFFuEVGQzLkQaLZRnbAtmVSOx1eTunqyQIU/qM" +
                "kHTWcneYZZKx2wQQmSiJoktNaQaUXDpA8NG73NauWCtznzvwh+DiGK6MJJxBMll7ow6Pjw+eE5kLTqob" +
                "lKaVXfDalHd5ZcsFBMVnnyNDrKGlO1OtkZo4FJBeF9ojmN2WT+TZnlC6GJ2e/Wt08D3LtFxSnoIoTUoS" +
                "HDGxMtOO/P/LsmYWREvr5VKUE1ZohTw/H707PngRknBL83FyTKWPrLgKnh9M7Uj+XYKIdkNMVHrt1KJ2" +
                "niAKMwUbi6Vf74EUspmzBekKqo0Zo82mmXHQZCYssm72icGzpanE+6TiYonUs4iANh5/raTofCbuKFmN" +
                "gtmjlusqQ9h4TdmIc+EcmdlUTwu4AdoIrxdLWJtPKec4yR4QE78z6LNCkV1L9oNcqV0s6pKLvPI5wrF7" +
                "n1S3VQ1TayuEKIFPK6RRwo5fh0g1ZWrUyfGQi7FJa0oPoJSXKWzLMXlyrJIaqXv/BV1AM7SyTymTz5Cz" +
                "G+Lif2DW3CN4HPGp3RA0/ibCDYAbykGqLRGPu7w3xtLtKRABC2Zp07naBefniAoyH6x9p6tcTwpuGlJo" +
                "AFh7dKm318FMbA9VqUsb0QvGlsZ/g7Zs8JJMT+ewWcExU8+gQAAuK3hhJq0KIUkLylxoRSYVSn9Ct4Rk" +
                "svMz6VjaDrZI/qBOxLrF1hhT9fo63vjlEpfs/OUfdfbqzejoihzjr18OPxSqR7GhzksKUYlaPbGhTnAJ" +
                "R9GmeKH2K5RVhAUMrfzKUo+Kxm5Kng41ordA9rQzg8uVQnjAuQtuK3PUG12C4maLe2vWscfoUhjyjtxH" +
                "/qWMjVRM3QsfzBAgpcom0YTAEhFmky4rod3TML56c3n27hmiNsbGh8PTt0oQDNRhk1BR9Jt0vNC3nDMh" +
                "K3lM1EpqK2og4OfSYsb2ZqBG3MOCy4dG56xOabiw9hYue2uG6rt/90jDvWHviPrs41e9vupV1nrszL1f" +
                "Dp89KyyiA9r2vT++ExHRw8CZMZcg0KCGUKfFeqHXJuN0tEBzTO57uJQjJpG7bo0J2WJaoHBM8iL369As" +
                "PeavENiIEjkPohs6fiW+wUhIKqpCgTIsVWQYjNRRDT1Rwb1HSBaGc9HPYDAIS0vFaIaqUQDvkQqwt62C" +
                "4Q9//+mlQFAjCC7BHOAectwLlC5/fatgNmfmtsgaO20QvvxUvI4QgptJqd5q5vZ/lJ2lrbDzw8v9F7wE" +
                "dEUAGObsKkAguayQ4Le2qV8mQSKBceh75XRhs7qgc7BVGG+XvejQcO2vVRs/17uCo2MJ04m976P9IU/r" +
                "q3SNFMwjBNzNUMU6LNqZG24hsc0lca4xNHvutyexIQUyaj+oweRIlDHueR//BgnXtJ/Uq7P3aKrk8+X5" +
                "69HFCI2OLI8+vD15dzy6QGMRNs7ejQ5exmiP+Yl7HuIpQMnMEFMCygKGXNTWTdAwoaFfbSHinYXR3K51" +
                "L3TAhspg7ON6jc7CByVI40jquo+Zqtfe6UmrlQTXpFMIzqzKLPu+rz7QAJypj12eSck8vptyhtElcLSd" +
                "g2iIb+SD0getbsfv0R+3qw+Nrmn1kXrKDkui/8CVLZG8yeyUNvEXkiL1UAckfCKjUSoWuSud5TWxEIZu" +
                "8aDIh+AdXxwen/zzkvr1Ds1oZMZJBpb+S7QirkPPDZz3mmFFF7aZ7z8qjfZ3oCQ9IgeaDbzj16OTX15f" +
                "qV3CHRZ7rUxUlKZdjbcyzTeG/RgLapdiYU/oUZ6LdES6QEcWHTqfowIMje7EfGFUfpwmSrY8TcUj3G+n" +
                "zu2YRPud5hW3oFS2qVddtj7EOqX79PRB/l4v+6JZ9SQoNQbpljIbl9oSnqajNlIfALeKIcCvk+IejqT8" +
                "FIKxEREDO0iTQz0LVSsE6rQy5K6YKftiLWoP5JyrCWtbHI/vDlRyzm+WESD5FdO2qUrG28J9KwHBShxV" +
                "Nh5IG/65g2Mbb4rbPFLdN5/Wzaffvw37reqiDI2hqHPp6nOTeVp9avVODRCc9T9L1DzKfYPySo84sah2" +
                "zEAPN5T88I4liQVzUDlDd/SPTvW400WNWEYuowHPNpbkAR+PFhgqjbu+SYjGVUBAr7URFxHoPJHHG/GV" +
                "A11tTQ2gEW4e8Scgi5e+kaqiGI+oLIqFDrZhSqbh633h09yP+cX9m3DLL2D83rSdUBBkdEbPrc3zKr+0" +
                "Nc9x+l49oen1iUp/x3+ZOlD8dqXV8ADBa6bXz2/o7b5Zfk/LtFm+oGXWLPdvGo+/fnnDe19LAV94Ld96" +
                "QW6+QOAvd/hbB5dsXYmOxonJ/Z/4jtlzRP1cCxuyZXwExJsK5j+I1wTidR+Oh8kEK0Qiimaa4ksHeddy" +
                "N2QlNDddaHyjgSnipvl2qkNLyrS5p6cfevELPXbzABmyAVX2+L5HjxsYN8ARxjLakS/RWM8EtCXlAKK3" +
                "nkIqD4x19lpxOpsbYnX2RZokq+NDH/qaMb22jtEQ4yD5E8h1x8UJHAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

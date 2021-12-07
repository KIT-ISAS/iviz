/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Classification2D : IDeserializable<Classification2D>, IMessage
    {
        // Defines a 2D classification result.
        //
        // This result does not contain any position information. It is designed for
        //   classifiers, which simply provide class probabilities given a source image.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of class probabilities. This list need not provide a probability for
        //   every possible class, just ones that are nonzero, or above some
        //   user-defined threshold.
        [DataMember (Name = "results")] public ObjectHypothesis[] Results;
        // The 2D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all use cases, so it may be empty.
        [DataMember (Name = "source_img")] public SensorMsgs.Image SourceImg;
    
        /// Constructor for empty message.
        public Classification2D()
        {
            Results = System.Array.Empty<ObjectHypothesis>();
            SourceImg = new SensorMsgs.Image();
        }
        
        /// Explicit constructor.
        public Classification2D(in StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.Image SourceImg)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceImg = SourceImg;
        }
        
        /// Constructor with buffer.
        internal Classification2D(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Results = b.DeserializeArray<ObjectHypothesis>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesis(ref b);
            }
            SourceImg = new SensorMsgs.Image(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Classification2D(ref b);
        
        public Classification2D RosDeserialize(ref ReadBuffer b) => new Classification2D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results);
            SourceImg.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Results is null) throw new System.NullReferenceException(nameof(Results));
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) throw new System.NullReferenceException($"{nameof(Results)}[{i}]");
                Results[i].RosValidate();
            }
            if (SourceImg is null) throw new System.NullReferenceException(nameof(SourceImg));
            SourceImg.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(Results);
                size += SourceImg.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "vision_msgs/Classification2D";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "90b6e63f97920f8c0f94c08bd35cb96d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VWTW/cNhA9V79iAB9iJ7tKmhZFEaDol5vGh7RFE/QSBAZXnJWYUKRKUrte//q+ISV5" +
                "7ThBDs3Cya4ozpuPN/PIEzrnrXEcSdHTc2qsitFsTaOS8Y4Cx9GmujqpTuh1Z+K0QNrDwPlEjXdJGUfK" +
                "HWjw0WQr47Y+9BmhpotEsNMcTetYE94AixZHHOKK9p1pOoqmHyxggt8ZzWWHPG3Uxlggw2VrdgxnFP0Y" +
                "GibTq5brqnrBSnOgLn9VwP+ZrImJ/PY+lLqkknc4RkySyOxVHe09LNHyjkNOMJqNnUJb0btRfEjtUqcS" +
                "qcCActcc/Ip8ILXxO0aoPWeMMXJY61xsDQNUsvNW19Wfm3fcpBeHwacOVYpv3k5VjlWuOgsvWiVVvLTs" +
                "OKiUMTjyvJdOTc01nlphADkgWGWpwY8Be/0o1chxwKwU7qymP5B54H9HEwo1pKyVQKlRkZFh9GQS9epA" +
                "Gybuh3Soq8gu+nDZxzY+vhCciY1L07fVD//zp3r56vdnFJMu/grRSONVUk6roKnnpHJxJPrOtB2KbEGX" +
                "hZHqJfVSusMA4ucuxl8po0W/IV3U0qOV+3500vlMyfR8yx6W0uU0qJBMM1oVsN8HbZxs3wbVs6DjL6Kc" +
                "7NCcF+fPZDwiN2NC28KTcU1gFY1r8ZKq0bj0zVMxqE5e7/0aj9yijRfnhXAEy1cDaJY4VXwGHw9LcjWw" +
                "URyGF40GyGuXeIxnmCUJgQePuTpF5H8dUoe+EO53KhglXQzgBhUA6gMxenB2hOwytFPOz/AF8cbH58C6" +
                "BVdyWnfgzEr2cWxRQBPnqdO0OWSQxhp2CaO5CSocKrEqLquT51JjbBIxEUbwjSH0jcmzsDepq2IKgp7Z" +
                "uDS6+kLduDMRM1Ya8u7wivY48nmVumW5MDmJpSjn/WI5DzzaEE0kTQIFk7pMgFl3IF8e7Qu90TpDYMyP" +
                "QER2xjRNOjxfnGOMR7SBQt9DJ7qxV26NPtRqkTIQ3fMqK6KMBcju/Gg1DRwEl1SGs96/H4cyB8vYyX8b" +
                "iEVNr5hvleaf/PsCkdV4zvPZe0ikhqmx9cyW8FSyPhZeL/PltugN6bqdsijHVAvYoxaiabkoNf1ykL04" +
                "GCT91U3mxWpKBY0lkQtAUA6i9ebJ+uu3dbW1XqXvvqXYILYvJV93FXPWIcx5FAFdGkM5cA8hmsfdTLtP" +
                "n6zoyVnu+QStGiBxW+mnALakLtO+OychTZ8TmpZvlGWqClRdNZD/qRfzvC1wdN9nwZqn7AjKD9BGdGN+" +
                "lQ9ffAf1MSAfTAtKsK8YfAjUgNOS4KeRHl0tHeuhoqLmmWgcB2lm/ZM5PTrcBtB+7z7P8Pq2If55cT9Y" +
                "5ZaO/STCRdmzlBMHGEhx+vbqhPRrLoPM1Mfg5pa6o403ceS5sqZJH0OQnRvu1M74rNGjmy4t1XxidZwL" +
                "e2NSgMvyaj61VuTGflPoC34fZ+u90YjnA+u8fK9x4+3Yu+U2ZLlFa+ThjllVIBFei5TIBQykbQ10LYbm" +
                "cQa+nF/HuhlwjkjBD36kvSqNEqebhLmWy5/jPc3HSFZU3PJALMyCj2u5wsWfRCZjXS492IQbqBM1BmUY" +
                "dC0zzD0UbrqE5cAy7hwIZH5ysUQ+l+K3eQFpD+aKbaT1mhocnA43mp6Vw0u5XGIC86+IsO8nUphU73Fb" +
                "3gbfZ1LnK3FxHqVUONPtqPnxsULdrVpXeP8erFxuDO5N2iDFwlw8OpCXdz8uN5vEw62Ano+4XqIXwKFr" +
                "0QSIYHNIjFMze8DNNyMdWagmjWAbRARzld+WnMX1acZ/mJvrrDpG+Go2f/M3FORtVf0HbudrwOYMAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

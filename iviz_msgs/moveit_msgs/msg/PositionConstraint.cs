/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PositionConstraint")]
    public sealed class PositionConstraint : IDeserializable<PositionConstraint>, IMessage
    {
        // This message contains the definition of a position constraint.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // The robot link this constraint refers to
        [DataMember (Name = "link_name")] public string LinkName { get; set; }
        // The offset (in the link frame) for the target point on the link we are planning for
        [DataMember (Name = "target_point_offset")] public GeometryMsgs.Vector3 TargetPointOffset { get; set; }
        // The volume this constraint refers to 
        [DataMember (Name = "constraint_region")] public BoundingVolume ConstraintRegion { get; set; }
        // A weighting factor for this constraint (denotes relative importance to other constraints. Closer to zero means less important)
        [DataMember (Name = "weight")] public double Weight { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PositionConstraint()
        {
            LinkName = string.Empty;
            ConstraintRegion = new BoundingVolume();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PositionConstraint(in StdMsgs.Header Header, string LinkName, in GeometryMsgs.Vector3 TargetPointOffset, BoundingVolume ConstraintRegion, double Weight)
        {
            this.Header = Header;
            this.LinkName = LinkName;
            this.TargetPointOffset = TargetPointOffset;
            this.ConstraintRegion = ConstraintRegion;
            this.Weight = Weight;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PositionConstraint(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            LinkName = b.DeserializeString();
            TargetPointOffset = new GeometryMsgs.Vector3(ref b);
            ConstraintRegion = new BoundingVolume(ref b);
            Weight = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PositionConstraint(ref b);
        }
        
        PositionConstraint IDeserializable<PositionConstraint>.RosDeserialize(ref Buffer b)
        {
            return new PositionConstraint(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(LinkName);
            TargetPointOffset.RosSerialize(ref b);
            ConstraintRegion.RosSerialize(ref b);
            b.Serialize(Weight);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
            if (ConstraintRegion is null) throw new System.NullReferenceException(nameof(ConstraintRegion));
            ConstraintRegion.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(LinkName);
                size += ConstraintRegion.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PositionConstraint";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c83edf208d87d3aa3169f47775a58e6a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71X224bNxB9X0D/QMAPllpFTe2gKFz4IYmdWECTuLEb5IJAoHZHu0RWyw3Jla18fc+Q" +
                "3Iscu+lD7cSAtKuZMzNnbuSeuCyUFWuyVuYkUl05qSorXEEio5WqlFO6EnolpKi1DU+Qss5Azs1GySg5" +
                "I5mREYX/4Bd7ACVh9FI7UarqC9Bgo9cShlZkYESPErxSVe7FFpVcU6+vVytLToxV5b3xQCsDkYlYaePf" +
                "OWlyiNSaQfVA7oqENCTqUlYVw0NhlOSk1+TMdrG2uf3lHaVOm8OIsfAYi2Cz92Gjy2ZNd/svRskz3VQZ" +
                "bLwLor3UwlAOtgLYU7ik8sJ5ZyRbjkHsAo8zqrQjCwuldGpDQq1rbZysUmJzGhGagYKdieeltniHH7+R" +
                "0cikRPpK5LNTdZNRsiq1dL89iV6MkuT4f/6XvLp4eSSsywK7oSYQ+AUcyKTJ4JiTmXTSx13ACzKPStpQ" +
                "CSW5rikT/le3rcnOPP2gBn85VWRkWW5FYyGEOFO9XjeVSqUDJwqlO9SHJgoGxSqNU2lTSqZLG2SIxX39" +
                "MDr+LH1tiHmdnxx5SiltmHJYUlVqSFpO1vxEJA2YPjxghWTv8ko/wiPlzHlrHHmUjp2l69qAejgj7RFs" +
                "/BSCmwEb5BCsZFaM/bsFHu1EwAhcoFqnhS/1860rYiVvpFFyWRIDp2AAqPustD8ZIFceupKVbuEDYm/j" +
                "v8BWHS7H9KhAzkqO3jY5CIRgbfRGZRBdbj1IWiqquLuXRpptwlrBZLL3gjkOle0zgk9prU4VEpCJK+WK" +
                "tul9NhYqu69qvLXh29IyxKlCEHBPbPxvXDkrQ4iklinNuEjmPq26QlFwZzmuv04TipkyUEWbz3heYC5o" +
                "Q1OhnMg0uhi9DIy1/AJIAsesLesaYCh0g0blJue0aFYZ0yyfTcVVQVWQYo58RfseUKkwKldZ0IShdacs" +
                "RQxuKtzqAByXZfA5GEPCAGK08wqTmZivxFY34ooDwhcTW0+LJXV++RJxWk+57yLELqHnfuy2qwNbw6Hp" +
                "Z0k3a667b9vu27d7SvVab0i54NfuRIbbJ7zIiNMcZjTSfHjSzmXeMVhvVpegNgYIqmuj1orHgQ3N7ZPY" +
                "1KGD4lbQYUWSGEsuIswksIuVVsiagisXjHreQn36PIDtlwz2KqzAxlWhMAXYwsA677FS87DLbq6wcygO" +
                "MRceKQDPUUNZptoK6QGnnLGCuPcrzretKVUrFUYrVPiDXVhGFttoxzLHxpmil28L9BUw4UqA/tfQovUf" +
                "h8WCbUT3ta7uylRfNEt9PQVH3NtTkW4xFbHXprwviDvzKTrNo4SIQj14Dgu58W2jTE8lwJh4jA7AYUcY" +
                "/kE8nuI/+obXzO/i2Zv3x7/G7xfnZ6dvT48P4uPzD3/OX5+cvj0+bF+8eX16/CSJZPPq5FJmlr1PUYrf" +
                "J61QhkldWeTP7orGBgXvvUSrw5OP3R8qDMSOBEkk159KeKIEEsIZkum6FvEIt9/r7CN4I7ds4UU8zCFw" +
                "7+rUP72fig8oNtDzcegzk+xPeVTlrmg9SrXBRK61Z5lXO1ZVFx9In/XcLt4fPx48fei45qePoHroUuA/" +
                "euUHKqcd5w+Y4vmP1cCHkuAnJnge5wUOvzJTDbvAK5U58xU028nr4u3Tk/nfF/BnaLNNssfkBIcjUWAl" +
                "lA6fcf361oa3MFdSqX3gLPNRyGuFg2G/kHZwF2en85dnl2LM2PFh0scEEPA2YLyPqfBnx47z2AtizL0w" +
                "Cfag3dkJ0UU74WFg5y4rvKha7kL6pKW7bT7nhDBT7U/QvzE8Bz2JcZcq40+Fs9Ayqu5ryHPK+rzXuN6b" +
                "ehqYFT9HUpMbnRj560rqRvAorkGnfifcE8OC9zPivh+tfut1ZxjZ3u940u4egKYhW3yaDb+HbcJsh8KT" +
                "4eCThINAK5D81WCqm8rj9nIPFaDyNXwZL1b9bbbzP95l2eWdcH9wbnkQ93vqbjum7vC56zw/fe1559Ph" +
                "D09i3Z3wAdYrnw/apTpIA+94Hn6lsmGw4GpS5bi9/jHYHhtZNv5KvAr33DaTloPeEN/zyH76nLCNywiA" +
                "PdZhJXEw4trdyLLT+P5c5725pZ4A1io9EFVtGLdQ1oa1b3unwgX102Hwk64XIC54+w96fGwA4BEAAA==";
                
    }
}

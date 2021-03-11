/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/Constraints")]
    public sealed class Constraints : IDeserializable<Constraints>, IMessage
    {
        // This message contains a list of motion planning constraints.
        // All constraints must be satisfied for a goal to be considered valid
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "joint_constraints")] public JointConstraint[] JointConstraints { get; set; }
        [DataMember (Name = "position_constraints")] public PositionConstraint[] PositionConstraints { get; set; }
        [DataMember (Name = "orientation_constraints")] public OrientationConstraint[] OrientationConstraints { get; set; }
        [DataMember (Name = "visibility_constraints")] public VisibilityConstraint[] VisibilityConstraints { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Constraints()
        {
            Name = string.Empty;
            JointConstraints = System.Array.Empty<JointConstraint>();
            PositionConstraints = System.Array.Empty<PositionConstraint>();
            OrientationConstraints = System.Array.Empty<OrientationConstraint>();
            VisibilityConstraints = System.Array.Empty<VisibilityConstraint>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Constraints(string Name, JointConstraint[] JointConstraints, PositionConstraint[] PositionConstraints, OrientationConstraint[] OrientationConstraints, VisibilityConstraint[] VisibilityConstraints)
        {
            this.Name = Name;
            this.JointConstraints = JointConstraints;
            this.PositionConstraints = PositionConstraints;
            this.OrientationConstraints = OrientationConstraints;
            this.VisibilityConstraints = VisibilityConstraints;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Constraints(ref Buffer b)
        {
            Name = b.DeserializeString();
            JointConstraints = b.DeserializeArray<JointConstraint>();
            for (int i = 0; i < JointConstraints.Length; i++)
            {
                JointConstraints[i] = new JointConstraint(ref b);
            }
            PositionConstraints = b.DeserializeArray<PositionConstraint>();
            for (int i = 0; i < PositionConstraints.Length; i++)
            {
                PositionConstraints[i] = new PositionConstraint(ref b);
            }
            OrientationConstraints = b.DeserializeArray<OrientationConstraint>();
            for (int i = 0; i < OrientationConstraints.Length; i++)
            {
                OrientationConstraints[i] = new OrientationConstraint(ref b);
            }
            VisibilityConstraints = b.DeserializeArray<VisibilityConstraint>();
            for (int i = 0; i < VisibilityConstraints.Length; i++)
            {
                VisibilityConstraints[i] = new VisibilityConstraint(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Constraints(ref b);
        }
        
        Constraints IDeserializable<Constraints>.RosDeserialize(ref Buffer b)
        {
            return new Constraints(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.SerializeArray(JointConstraints, 0);
            b.SerializeArray(PositionConstraints, 0);
            b.SerializeArray(OrientationConstraints, 0);
            b.SerializeArray(VisibilityConstraints, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (JointConstraints is null) throw new System.NullReferenceException(nameof(JointConstraints));
            for (int i = 0; i < JointConstraints.Length; i++)
            {
                if (JointConstraints[i] is null) throw new System.NullReferenceException($"{nameof(JointConstraints)}[{i}]");
                JointConstraints[i].RosValidate();
            }
            if (PositionConstraints is null) throw new System.NullReferenceException(nameof(PositionConstraints));
            for (int i = 0; i < PositionConstraints.Length; i++)
            {
                if (PositionConstraints[i] is null) throw new System.NullReferenceException($"{nameof(PositionConstraints)}[{i}]");
                PositionConstraints[i].RosValidate();
            }
            if (OrientationConstraints is null) throw new System.NullReferenceException(nameof(OrientationConstraints));
            for (int i = 0; i < OrientationConstraints.Length; i++)
            {
                if (OrientationConstraints[i] is null) throw new System.NullReferenceException($"{nameof(OrientationConstraints)}[{i}]");
                OrientationConstraints[i].RosValidate();
            }
            if (VisibilityConstraints is null) throw new System.NullReferenceException(nameof(VisibilityConstraints));
            for (int i = 0; i < VisibilityConstraints.Length; i++)
            {
                if (VisibilityConstraints[i] is null) throw new System.NullReferenceException($"{nameof(VisibilityConstraints)}[{i}]");
                VisibilityConstraints[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += BuiltIns.UTF8.GetByteCount(Name);
                foreach (var i in JointConstraints)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in PositionConstraints)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in OrientationConstraints)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in VisibilityConstraints)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/Constraints";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8d5ce8d34ef26c65fb5d43c9e99bf6e0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuVZW2/byBV+J5D/MIAfYncVrdcOisILP2RjZ+OiSdzYDXJBIIzIkcSG4nCHpGTl1/c7" +
                "58yQQ0netGhjFNjdAJaomXP5zv3wQN0u8lotTV3ruVGpLRudl7XSqsjrRtmZWtomt6WqCl2WeTmnI3Xj" +
                "cKipx8mBelYU8SO1bHFtalStm7ye5SZTM+tAbm51oRpLP9HxPDMOv610kWdJgttEutRLkyR/tSD0vCP5" +
                "6bP6Jz2ZRFyS5NrWOck1OFf5h8Ojb1xuoNXOads/H154l9f5NC/yZjM4v+oeD44/Ss7/x/89Sl7d/HoG" +
                "4FcmbybLel7/uIXJIwDffVPNwnSqk8W0AObRXufNAoe0So0j26qpbcssQC7QCvAHTIl/9nd1usjNCnaC" +
                "i3zqWDzBr4VxukzNZGoKux717H+IftNTaPA5mRVWN39+2p3pHmyd3POcqZNgz9Ta5PNFQyLPdNrApcit" +
                "oFkdeZ86zExpG1MrZwoYdmVUvqws1AYxUslCQTfwYPW8sDWe4cevxlkEgob3FwiH7mpz1EkmQjyIxXcd" +
                "nIy+P1jJbJmZ5WXkAp1Fem3HSfLSaASeWvAfAvYWV52d2gbxXn7ZAdSZmXFgYIO/0KnOXeiync1qA+S9" +
                "HzKVmcOBI28hAK/dHEcqdkobnVvDw5zpUwsuJHNjl6ZxG0HhnSFbn3oSEyYxEZZBgJUt2qW5X3KV/EIe" +
                "Dfrv5GR/aOLMnBzy/83Bvl9OqZtMgBVHIJe6gQSZdhkka3SmG82KLyCHcU8KRH+BW3pZIQvwr82mMpz5" +
                "2Rfxb25KhGtRbFRb4xAUTe1y2ZZ5qhuAksNd4/u4yemo0q7J07bQhJd1MBAdZ9ch6vhXm99aQ8BeXZwx" +
                "piZtCXNwysvUGV2Tta4uVNIC6tMTupAc3K7tE3w1cwI9MIchdUPCmrvKAXsIo+sz8PiTKDcGbaBjwCWr" +
                "1SE/m+BrfaTABCKYyqYL9vLrTbPwTrzSLtfTAk4AJwECoPqYLj0+iiiT2GcobaUN5IViz+PfIVt2dEmn" +
                "JwvYDCE0V3U7B4A4WDm7QlHN1HTDRNKCqhvibOq02yR0S1gmBy8IY3Fttgj+6rq2aQ4DZFwvQrSzNSao" +
                "0N/NIfeGe5fpnCFrQQ9qSFb8IznPzBkoU+nUjMlPrtiytoRfUHRx4etu4mKWO1xFoI8pYSAxWGdGKm9U" +
                "ZhHJiGfQWOovIGkAM93WVQVi8HWUoZoCnSxj6cqhGc/HI7VemFJOEUzs1BwGeapcPs8zuQlGy+6yVl67" +
                "kWpmJ4AZjRPLLMxgMxBxVhqSo7G6mqmNbdWaFMIH56OP63KQi72ksXZEoedJDBG95qQbKgaKRYPARyEI" +
                "Ceeu+7TpPn19kAI3zMpk8gsqYSgJIaXD0qcXkpypxKCs1RbtYtAQWFcuX6LKrWBGDnC2YkvR3tcFK6XR" +
                "qENNXoS8BHiTeqErI4LcENHrQInayI5qqDEopmABButFjjTADVfPmmpYYSnbZVvlC0V8QHDChIjqFdwn" +
                "y6RCQ8Se2oiMtQDVVKNVIzc3ac59NPlQxn+6No2cz+t5qOcoOCNE8h4VX4Ek5BDKv6OUZ/1Nheic1+X7" +
                "lar7LBS5ytTejYAQBfVIpRtkRBQ1fLL4zc8mTEZUEj9gBBcapRsa564HEsQIduQMHk2040b4eIT/ETBU" +
                "Yv6ifnnz/vwn//nm+uXl28vzE//1+Ye/Xb2+uHx7fhoevHl9ef40oE1lk1yYYGaZ/Cl6noRDGbI0ZiOU" +
                "uuFRH5lAvj8R7lDKI/HjC9GxM2XQxUtLQqlEQJCekeC6ozCjL4/7O4+hvNMb4vDC93BQnEUd8bf3I/UB" +
                "rgZ4PsYyE8jc3Jly3iyCRKl1SMUVSh/EpLKOMtXpB9DHPbaT9+fH0bcPHdb07SOgjkUS/L1UnEnJ7Og9" +
                "wIoSP2oCNSQiJ1L33OcJNLw6y1sSwY884kFBDqE7efvs4uofN5An5hmMzDTJwNIOCSriOtTacumW+ZI8" +
                "qbCsOJ35qPRdjq6wr0QDupOXl1e/vrxVh0TbfznqdQIR4BYh3uu04M6xw9zHgjqkWDgSfjQ0Bj6inecj" +
                "XyI+93GhChWwE/Np1J17eWJ2MYxU+An3t/JmFJO0Gcgdd4RcqNHpVL0PMaZ0nwoa+XtbjQRZ9YMHNQTp" +
                "FpidS20pD+eKInXncA8MDj5Q/0PpldLbs75/kf4BMFC6HTY/IzEYNbPyu5QTAjzabaBFkiagm7//3iKz" +
                "OyoS8bmH0zGeZQcz7HCJIfPiQOPfb1seSoMev72N6gDVofz07bcefeoPv9WL9aPhg5Ra6hS6AhsZgyo+" +
                "JcKwFUT7pcs5xtifo0qCbV7LszFGb+5qvD0xJpZo32neM/WnzwkxufUEUNM6WmEFhfm7xbYw3Njt7Via" +
                "PW5FWzp/6cHQCorsQy1o9rju5ZJZ9dOpiGruJsDu+wocd917l6H/0WZp6N7f3i6h2GP+GqSkkNWjxVPf" +
                "5nKX3gfJlpnvyV3/7SbLVkQGPkeF5AnbTBnnqOqHZWTdy9jFpJ5iImkbM7mb0MVJd3j3xOabJ75un/jj" +
                "rKViD923fRcHjVd3vrubtfxSYUmPaLfd7+hl5M7yOlWH/RLyaGf31C2cOAD4PKXletDe6I7wRlqR9YKq" +
                "MTUotDzBxoE2oTT5d75NuwTriPArLxwPa718xI1IYV+Doy37ZB8UYTWh7BQat6mwiK6TGET9Nex+FlXT" +
                "gA8TeP3mFsShD9xgCQmW7RJiAq47/ght2UVq9F7N2mCl0UsOktRI9NCNII4TsmimA9VovystnJ8mECYF" +
                "tyWr3KyjTltQgV/v7rd54j0kpthlTbHa2CDV2rbIjogu7xbJ9+sW40zVOh4/x13kD9pSNiNXDaFABIKP" +
                "0FsnghER3r9xkPWyEInn45jgz5SaeguRz/rGf/skeMFA2NEhleLO2hRFZyGZ6gNKvImTbqd3yT6LjVhj" +
                "c4e1XWH2jOI3frHarcdrzhovnF1GcAfPbOwae956YNNOZHF6ve1ikWfxUINllbPwHl4VtryAXepyI0Pd" +
                "mOckL65fksmZp3JgRFO5rGJLzmdc1UWiiLGs5fkFlKo2wCjHysoLwj1GMKsu1npD3qtO2a8xTo0T2QWT" +
                "wBNmOrAn+Mq6YxiJ4ru9iwi7YCDsnDzDfq24Vb9AoF+jeNg5NTSYvoWerCmxacW6JovPkU8U1n7xi0pa" +
                "CTYDI1HOAsb9HjOAwcS4dEk+RNeE25Q2kDNggWnbdGsmFLfdmKOJithDKVvwSoR3+nULHO93N5G7c7fL" +
                "FWcO284XvT9RN7ETceJvWy4WYgLtEPxoCRVAc2pS3fZRtad5YC60rCbhUZk5AcU5RvCGkMwaNKNYorOc" +
                "mTylL3ClpUVrO6PQ0eytjLsYNaU6sE90D59B0kJriq0SvyGn0kocjv1yTsZZ5ietBeX+weDLXS//5DOx" +
                "z5dIuQAkaBZxZ+1Dgu4cI+zIZ3sTQHBqCSF+txoPLtxY+yIRmrZeZoltxKNxMwKbfhmxlF1Q8kobyhz7" +
                "iru2Xh4Z9QE97tmKpwJTHwWKNMQWpqHXOqHYec77yV/nP54Ih5g65KpgBkmhoB0liwC1aEzvn7yyvgxG" +
                "htnJA0KnryLa0/QJ3IInPqMlxWNQPjwesXyyvz8mTlhdB7eP7R+VZmSHUIgg0oTOTfhc0sV5uMVVsMYG" +
                "BUrSSwO7dYMjvqANy1BFIXewp86H4rDjQ8G5qCmVnA2PL/Fb2ARtF5e4F/CFTdwucjrShfbUAT/HNbJD" +
                "zTcdW/CoHJt/aTCOh1Dx9RirWNw42eLdDfkGv5AcBEzoUMK76q75ea/O1QnWnPjz0whru3MVNkM3l69v" +
                "3rzFOnLrQb+t9A/ed7thnzDZTh3vP1B/P7+vlMiyix6EVI4pjd/5br8TFn8M73O3Rk0mwOXoUfIvkrJB" +
                "p9IkAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

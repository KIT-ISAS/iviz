/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Constraints : IDeserializable<Constraints>, IMessage
    {
        // This message contains a list of motion planning constraints.
        // All constraints must be satisfied for a goal to be considered valid
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "joint_constraints")] public JointConstraint[] JointConstraints;
        [DataMember (Name = "position_constraints")] public PositionConstraint[] PositionConstraints;
        [DataMember (Name = "orientation_constraints")] public OrientationConstraint[] OrientationConstraints;
        [DataMember (Name = "visibility_constraints")] public VisibilityConstraint[] VisibilityConstraints;
    
        /// Constructor for empty message.
        public Constraints()
        {
            Name = string.Empty;
            JointConstraints = System.Array.Empty<JointConstraint>();
            PositionConstraints = System.Array.Empty<PositionConstraint>();
            OrientationConstraints = System.Array.Empty<OrientationConstraint>();
            VisibilityConstraints = System.Array.Empty<VisibilityConstraint>();
        }
        
        /// Explicit constructor.
        public Constraints(string Name, JointConstraint[] JointConstraints, PositionConstraint[] PositionConstraints, OrientationConstraint[] OrientationConstraints, VisibilityConstraint[] VisibilityConstraints)
        {
            this.Name = Name;
            this.JointConstraints = JointConstraints;
            this.PositionConstraints = PositionConstraints;
            this.OrientationConstraints = OrientationConstraints;
            this.VisibilityConstraints = VisibilityConstraints;
        }
        
        /// Constructor with buffer.
        internal Constraints(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new Constraints(ref b);
        
        Constraints IDeserializable<Constraints>.RosDeserialize(ref Buffer b) => new Constraints(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.SerializeArray(JointConstraints);
            b.SerializeArray(PositionConstraints);
            b.SerializeArray(OrientationConstraints);
            b.SerializeArray(VisibilityConstraints);
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
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetArraySize(JointConstraints);
                size += BuiltIns.GetArraySize(PositionConstraints);
                size += BuiltIns.GetArraySize(OrientationConstraints);
                size += BuiltIns.GetArraySize(VisibilityConstraints);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/Constraints";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "8d5ce8d34ef26c65fb5d43c9e99bf6e0";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuVZW2/byBV+568YIA+xu4rWGwdF4YUfsmvvxosmcWM3yAWBMCJHEhuSwx2SkpVf3++c" +
                "M0MOJblp0cYosLsBLFEz5/Kd++EjdbvKG1WaptFLo1JbtTqvGqVVkTetsgtV2ja3laoLXVV5taQjTetw" +
                "qG2mySP1vCjiR6rscG1uVKPbvFnkJlML60BuaXWhWks/0fE8Mw6/rXWRZ0mC20S60qVJkt8sCP3ck/z4" +
                "Sf2DnswiLklybZuc5Bqdq/3D8dHXLjfQau+0HZ6PL7zNm3yeF3m7HZ1f94/Hx8//x/8lL29+PQPsa5O3" +
                "s7JZNt/vIALU+y+qXZlebzKXFrQ81Ju8XeGQVqlxZFg1t12VBbwFV0H9EVPin/1dna5ys4aR4B8fexZP" +
                "8GthnK5SM5ubwm4mA/vvot/0HAp8ShaF1e2fn/Vn+gc7Jw88Z+ok2HO1Mfly1ZLIC5228CfyKWjWRK6n" +
                "jjJT2dY0ypkCVl0blZe1hdogRipZKOhG7qt+LmyDZ/jxi3EWUaDh+gViob/aHveSiRAPYO59307uC1Oy" +
                "WWYWeRXZvzfHoOo0SV4YjZBTK/5DqN7iqrNz2yLSq897aDqzMA4MbHAWOtX7Cl22i0VjALt3QqaycDhw" +
                "7M0D1LVb4kjNHmmjcxu4lzNDUsGFZGlsaVq3FRDeGjL0qScxYxIzYRkEWNuiK839kqvkJ3Jn0H8rJ4dD" +
                "M2eW5I3/d971jdyraTOBVdwAat+AfaZdBrFanelWs9YrCGHckwJxX+CSLmvEP//abmvDCZ8dEf+WpkKg" +
                "FsVWdQ0OQcvUlmVX5alugUgOX43v4yYnolq7Nk+7QhNY1sE6dJz9hqjjX2N+7wyhenVxxoCatCPAwSmv" +
                "Umd0Q6a6ulBJB5xPn9KF5NHtxj7BV7MkxANzWFG3JKy5qx2AhzC6OQOPP4lyU9AGOAZcskYd8bMZvjbH" +
                "CkwggqltumIXv962K+/Ba+1yPS/gAfAQIACqj+nS4+OIMol9hopW2UBeKA48/h2yVU+XdHqygs0QP0vV" +
                "dEsAiIO1s2vU0kzNt0wkLaioIcjmTrttQreEZfLoF8JY/Jotgr+6aWyawwAZV4oQ6myNGRXmb+ONByM9" +
                "uJYzZCooQU3Imn8jz1k4A01qnZopOckVm9VWcAqKK653/U1czHKHqwjxKaUKpATrzETlrcosYhiRDBql" +
                "/gySBhjTbV3XIAZHR/VpKMTJLJauHJnpcjpRm5Wp5BRhxB7NMZCnyuXLPJObYFT2l7Xyyk1Uu3gKjNEs" +
                "sczCDAYDEWelCTmeqquF2tpObUghfHA+9LgcB7nYRVprJxR3nsQY0GtOt6FWoEy0CHqUgJBq7vpP2/7T" +
                "lweoa+NsDLEvqHKhEoRMDjOfXkhOpsqCatZY9IdBPQBdu7xEcVvDhhzabMKO4nwoB1YqolFHmlwIGQnY" +
                "Js1K10bkuCGi14ES9Y091VBaUEPBAgw2qxwJgJusgTWVrsJSnst2qhZK94jgjAkR1Sv4TpZJYYaIA7UJ" +
                "WWoFqqlGe0Y+btKcG2dyoIz/9K0ZeZ7X80gvUWcmiOEDKr4EScghlP+FUp71VxWic0GXb+Mo99pn8JO5" +
                "vZsAHgrniUq3SIQoZfhk8ZufRJiK6CNOwPCtNMo11M3dgCKIEebIFjyIaMed78kE/yNUqLL8Rf30+t35" +
                "D/7zzfWLyzeX50/915/f//Xq1cXlm/PT8OD1q8vzZwFqqpbkv4Qxy+RP0fMkHMqQnDEJocKNj/qYBOzD" +
                "iXCHkh2JH1+Ijp0pg7Zd2hBKIgKC9IkE1x3FGH15PNx5DOWd3hKHX3zfBsVZ1Al/ezdR7+FngOdDLDOB" +
                "zA2dqZbtKkiUWockXKPiQUyq5qhOvX4AfTpgO3t3fhJ9e99jTd8+AOpYJMHfS8U5lMyOlgOsKOWjGlAf" +
                "InIiaS99kkCTq7O8IxH8jCMeFOQQurM3zy+u/n4DeWKewchMkwwsXZCgIq5D7SxXbJkmyZMKy4rTmQ9K" +
                "3+XoBIcaNKI7e3F59euLW3VEtP2X40EnEAFuEeKDTivuFnvMfSyoI4qFY+FHU2LgI9p5PvIl4nMfF6pN" +
                "ATsxn0bFuZcnxhXDSIWfcH8naUYxSXuA3HEjyCUaDU49+BBjSveplJG/d/VEkFXfeVBDkO6A2bvUjvJw" +
                "rihS9w4PwNDBb5Pi9jMrl7q+bZG2ARhQoh33PBOxFjWw8rsUEkI7WmOgM5La30/bf+uQ0x2Vh/jcQykY" +
                "Ta6jiXW8r5DpcKTuV1qVBxF/gO5QZzrCcyw8fft9wJ0awq82X/0U+ADllXqDUFQjM1CJp+QX9n7ot3S1" +
                "xLj6Y1Q9sK/reAbGiM1tjLckJsIKzTqNdqb5+CkhHreeAOpYTyvsmTBnd9gHhhv7zRxLc8CfaA/nLz0Q" +
                "VEGNA5AFtR43g1Ayk348FTnN3QzAfUtp4wb74KbzP9kdjb366/sjlHbMWaMcFHJ4tFoaOlpuyIfY2DHw" +
                "fcnqv9xV2ZrIwNuobDxhgynjHNX4sGtsBhn7UNRzDB9da2Z3M7o46w/vn9h+9cSX3RN/lMVT7J6H9ure" +
                "uJG+0sgtOn5bUNIj2lsPy3eZq7O8SdXRsGM83tsu9Ssl9n4+T6m4GXUyuie8la5js6LaS70IrUewVqBF" +
                "J433vWPTwsA6IvzSC8dD2SAfcSNS2MjgaMcOOURE2D8oO4fGXSosouskBlF/BaOfReUz4MMEXr2+BXHo" +
                "Ax8oIUHZlRATcN3xR2jL/tGgzWo3BnuLQXKQpLZhgG4CcZyQRd8cqEbrW+nW/OCAGCm4CVnnZhM11YIK" +
                "nHp/e82T7RExxbZqjv3FFjnWdkV2THR5e0iO33SYXOrO8Zg57cN+1IGyGblYCAUiEHyEXicRjAjv4W2C" +
                "bI+FSDwHxwR/pLw0WIhc1vf4uyfBCwbCFg55FHc2pih6C8n0HlDiXZu0N4NLDilswhqbOyzmUFn2G8Mb" +
                "vzrtt9/oFWkwcbaM4A6e2doNNrnNyKa9yOL0etfFIs/i+QUbKWfhPbwM7HjFWupqK/PblEciL67fhMmZ" +
                "Z3JgQgO4LFsrTmZczEWiiLFs3fnlkqq3wCjHXsoLwq1FMKsuNnpL3qtO2a8xOU0T2faSwDNmOrIn+Mpa" +
                "YxyJ4ruDiwi7YCDsljzDYXe4U7xAYFiXeNg5NbQYtIWe7CKxS8VaJovPkU8U1n7220ja+7UjI1HOAsbD" +
                "sjKAwcS4bkk+RLOE25Q2kDNggXnX9uskVLb9mKPhidhDKVvw9oO39k0HHO93N5G7d7fLNWcO2y1Xgz9R" +
                "K7EXceJvOy4WYgKNEPyohAqgOTep7oaoOtA5MBdaR5PwKMucgOIcI3hDSGYNmlEs0VnOTJ7SZ7hSadHR" +
                "Lih0NHsr4y5GTakOHBLdw2eQtNCRYoHEr76prhKHE7+Ek8mV+UlfQbl/NONys8s/+Uzs8yVSLgAJmkXc" +
                "WfuQoHvHCIvwxcEEEJxaQojfm8bDCvfTvkiEjm2QWWIb8WjcgsCmXyYsZR+UvLeGMie+4m6sl0emekCP" +
                "e7bmYcA0x4EijayFaenFTSh2nvNh8tf590+FQ0wdctUwg6RQ0I6SRYBaNKY3TF5ZXwYjw+zlAaEzVBHt" +
                "afoEbsETn9GP4jEoH51MWD5Z0p8QJ6yog9vH9o9KM7JDKEQQaUbnZjJL9HEebnEVbLAsgZL0ZsDu3OCI" +
                "L2iZMlYxjCb7dT4Uhz0fCs5FHankbHh8hd/C0me3uMS9gC9s4naR05EutI8O+DmukT1qvunYgUfl2PBL" +
                "g3Eyhoqvx1jF4sbJFi9oyDf4leMoYEKHEl5F983PO3WunmKjiT8/TLChO1dhCXRz+erm9RtsHnceDItJ" +
                "/+Bdvwb2CZPt1PP+wzT3y/sKCQNA30Mex3zGr3R3X/mKM4bXtTtDJhOQWvRPvO/lcqckAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

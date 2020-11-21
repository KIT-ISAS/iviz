/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [DataContract (Name = "trajectory_msgs/MultiDOFJointTrajectory")]
    public sealed class MultiDOFJointTrajectory : IDeserializable<MultiDOFJointTrajectory>, IMessage
    {
        // The header is used to specify the coordinate frame and the reference time for the trajectory durations
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // A representation of a multi-dof joint trajectory (each point is a transformation)
        // Each point along the trajectory will include an array of positions/velocities/accelerations
        // that has the same length as the array of joint names, and has the same order of joints as 
        // the joint names array.
        [DataMember (Name = "joint_names")] public string[] JointNames { get; set; }
        [DataMember (Name = "points")] public MultiDOFJointTrajectoryPoint[] Points { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MultiDOFJointTrajectory()
        {
            Header = new StdMsgs.Header();
            JointNames = System.Array.Empty<string>();
            Points = System.Array.Empty<MultiDOFJointTrajectoryPoint>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiDOFJointTrajectory(StdMsgs.Header Header, string[] JointNames, MultiDOFJointTrajectoryPoint[] Points)
        {
            this.Header = Header;
            this.JointNames = JointNames;
            this.Points = Points;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MultiDOFJointTrajectory(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            JointNames = b.DeserializeStringArray();
            Points = b.DeserializeArray<MultiDOFJointTrajectoryPoint>();
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new MultiDOFJointTrajectoryPoint(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MultiDOFJointTrajectory(ref b);
        }
        
        MultiDOFJointTrajectory IDeserializable<MultiDOFJointTrajectory>.RosDeserialize(ref Buffer b)
        {
            return new MultiDOFJointTrajectory(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames, 0);
            b.SerializeArray(Points, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (JointNames is null) throw new System.NullReferenceException(nameof(JointNames));
            for (int i = 0; i < JointNames.Length; i++)
            {
                if (JointNames[i] is null) throw new System.NullReferenceException($"{nameof(JointNames)}[{i}]");
            }
            if (Points is null) throw new System.NullReferenceException(nameof(Points));
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i] is null) throw new System.NullReferenceException($"{nameof(Points)}[{i}]");
                Points[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += 4 * JointNames.Length;
                foreach (string s in JointNames)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                foreach (var i in Points)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "trajectory_msgs/MultiDOFJointTrajectory";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ef145a45a5f47b77b7f5cdde4b16c942";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WXWscNxR9X9j/cMEPsct6DUnJg6EPhTStC6UpMX0pZdHO3JlRopEmksab6a/vudJ8" +
                "7K7dEmhjs4YZSffr3HOP5oLuG6aGVcmedKA+cEnRUei40NVAEbuFc77UVkWmyquWSdkybXiu2LMtmKLG" +
                "cuV8Wo5efeAiOj9Q2XsVtbNhvfopx8ih1qv16oK+h4fOc2Ab0ylyFSlqexP1dYnnD07beOzuklXRUJeW" +
                "kaySPRsQt032V+L0h+WIMs7W5ykdtDGkbWH6Uioh5b0aJHLngk653jywcQWeOdyoomDDcxEX8KYiNSok" +
                "t0HQMGzr2NC4NLvLyVucCJuE2IkREAUY0zGUEih752PD7G0raIXota3/+DNv79L2evWLYPXm17c/y+L9" +
                "XOQ7ecXZBAOOrVff/c9/CP3+x1sKsdy1oQ43ubtSwvuIYpUvqeWoShVV4kWj64b9tWFgCyvVduBZ2o1D" +
                "xwElXoCJ6Cl+NVsgbswws7FwbdtbXQgFhWonDsRUo4/UKR910RvlH1E2+Zf/wJ/6xNi7N7c4ZQMXfdRI" +
                "ahBOeFYBKGMTh3tg9+qlWMDw/uCu8c41ujZnkMmAjPmz0FiSVeFWwnyTa9zCPUBiBCoDXaa1HV7DFSEO" +
                "suDOga2XSP/dEBtMgBDgQXmt9obFcwEc4PaFGL24OnYtqd+CJtZN/rPLJciX+LWLYynrukHzjEAQ+ho4" +
                "4mTn3YMucXY/qoHRmFcyeu+VH9arNPopKJy8TfoQpZGpNzKkIWCY0IkSoxebicm5LztdfkV2LlOfSfpv" +
                "0zJLx7n8FNCISQ2PBIcue/Tf0WuCQ+BXswPhp0j30zEM4WySBlFoDtFMbveQCxrFZhiDaOA0CanzukYL" +
                "kcqiC48iHXSQUV8066koGI8jIfuPoc40cb2aVD5RaFd51+5ACB+/Ymv/Ae5ZRuaLJWvu0rY9xwMzUj24" +
                "RyoB9bF4YvC5U4WIxnr1e+LIq+zBpDLXq9962HgrJXuX765nq3VM6KlKhUyyeVZGEr67JFTOQuhaVnKt" +
                "usUUlqX2sEUh20wdgMUb0pFKB1ysSwPSqo9wyhANMVddZ+aZyNDIMmwueVtvN3RoAHQ6JQOflTqJuy5I" +
                "+Fae3d/JKY0FbihWL/NtnbLO0dBM8TKhfrWlu4oG19NBasKDH28VJ7yfMkuqF53byI0y+TiFNYkAoAlB" +
                "1VBIGyJutMSAyjgVX39Ln5dHyN70+NcztX2h3JOdx+h60eWM40n/5e3TQlhB+8vqqsbHw/PNsSjMXN90" +
                "q4ZFI08L23v3EQxD44R1AXeSZVxa8rGlbJ0+BOSbQL4upjEezxwtjCelxr8BOgunEw0LAAA=";
                
    }
}

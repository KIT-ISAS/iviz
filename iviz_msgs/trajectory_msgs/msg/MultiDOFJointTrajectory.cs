/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [Preserve, DataContract (Name = "trajectory_msgs/MultiDOFJointTrajectory")]
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
            JointNames = System.Array.Empty<string>();
            Points = System.Array.Empty<MultiDOFJointTrajectoryPoint>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiDOFJointTrajectory(in StdMsgs.Header Header, string[] JointNames, MultiDOFJointTrajectoryPoint[] Points)
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
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
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
                "H4sIAAAAAAAACr1WS4/bNhC+C/B/GMCH2IXXCyRFDgv0UCBNuwWCpoiRS1EYtDSWmFCkQlLrqL++31Av" +
                "25s+gDa72ANFznu++cZL2lVMFauCPelAbeCCoqPQcK6PHUW85s75QlsVmY5e1UzKFunB85E925wpalwf" +
                "nU/X0asPnEfnOypar6J2NmQ/9S56T1m2pO+h3ngObGMSIXckRXVror4pcP7gtI3ntlas8oqadI1IlbzZ" +
                "AKd10l/D5g+zhDLOltfhnLQxpG1u2kKyIOW96sRx44JOcd4+sHE5zhxuVZ6z4TGBJYypSJUKyWqQQhi2" +
                "ZaxouJqs9aFbSIRNKtaFEoqJQoxiSCRQMs7ner2xbZaF6LUtf/u9f9ynx+yNVOnVL69/lrvdlN9b+YRo" +
                "qkDIFtl3//PfInvz7sc7CrHY16EMt31TFwj/XUSeyhdUc1SFiiqhodJlxf7GMKoKLVU3QFd6jV3DYQvF" +
                "XYVe4r9ki1Ib000QzF1dt1bngjvB14U+NDX6R43yUeetUf4RTMU6/gN/ahNG71/dQcYGztuoEVAnSPCs" +
                "AuqLR8paVO3Fc1HIlruTu8Enl2jV5LxHAILlz4JciVOFO/j4pk9uC9uoDsNLEWiV7vb4DGuCE4TAjQNA" +
                "V4j8bRcrYF66/qC8VgfDYjhHBWD1mSg9W59ZlrDvAA3rRvO9xdnHvzFrJ7uS002FnhnJPrQlCgjBxrsH" +
                "XUD0MIy+0ZhPMvrgle+yNObJZbZ8naggSvtSR2QkQ8DsoAEFJi1WA3b7bux18fUAOY94j8u/m4/FyBPX" +
                "VJODEEbaOyMXWrVovKOXBIPrrGQHiI+OdqMUpm7SCEJvoFXPyeYBzEADrXSDB40SjXTpvC7ROsQxc8C1" +
                "m5MOMtgzOT12gWk446v/5ueS+bKRxRNq9kfv6j1A4OPXa+hfVHkxEsa0OnpenZt14HhiRqQn94gQwDMW" +
                "JwaEG5WDH7L3CRcven2Tcsx+baHgraTrXb+anirPIZwvZSkIkserFITg7hMlOQtCq1nJznSzJhQL7aGK" +
                "NLY9YlAn3pCOVDiUxLoIG7X6CJMMfhBt1TRmmoG+LHINlRVvy+2GThVKnKRkvhMbJ/7WOQnIiqvVnGzS" +
                "kN2G4vF5v4lTzL0zdBFGxoKvt3R/pM61dJKEcPDD2nCC9TGuRG/RuY3sjMHEZUXTxKMsIagSTGhDxMZC" +
                "44/GqfjyW/o8nbrp9McTdXsG2hcbjkn1Qr59BS/aLl+fZphKnf8pp2w8nZ5saIVLpszGjRlmJrxM6eDd" +
                "R4AK7RKgBawcy9hJ8utJ2TIteNn1+M0wDu0gMn8PckjwT1zKRpfWCgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}


namespace Iviz.Msgs.trajectory_msgs
{
    public sealed class MultiDOFJointTrajectory : IMessage
    {
        // The header is used to specify the coordinate frame and the reference time for the trajectory durations
        public std_msgs.Header header;
        
        // A representation of a multi-dof joint trajectory (each point is a transformation)
        // Each point along the trajectory will include an array of positions/velocities/accelerations
        // that has the same length as the array of joint names, and has the same order of joints as 
        // the joint names array.
        
        public string[] joint_names;
        public MultiDOFJointTrajectoryPoint[] points;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "trajectory_msgs/MultiDOFJointTrajectory";
    
        public IMessage Create() => new MultiDOFJointTrajectory();
    
        public int GetLength()
        {
            int size = 12;
            size += header.GetLength();
            for (int i = 0; i < joint_names.Length; i++)
            {
                size += joint_names[i].Length;
            }
            for (int i = 0; i < points.Length; i++)
            {
                size += points[i].GetLength();
            }
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public MultiDOFJointTrajectory()
        {
            header = new std_msgs.Header();
            joint_names = new string[0];
            points = new MultiDOFJointTrajectoryPoint[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out joint_names, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out points, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(joint_names, ref ptr, end, 0);
            BuiltIns.SerializeArray(points, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ef145a45a5f47b77b7f5cdde4b16c942";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}

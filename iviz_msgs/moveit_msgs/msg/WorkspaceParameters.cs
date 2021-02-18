/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/WorkspaceParameters")]
    public sealed class WorkspaceParameters : IDeserializable<WorkspaceParameters>, IMessage
    {
        // This message contains a set of parameters useful in
        // setting up the volume (a box) in which the robot is allowed to move.
        // This is useful only when planning for mobile parts of 
        // the robot as well.
        // Define the frame of reference for the box corners
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // The minumum corner of the box, with respect to the robot starting pose
        [DataMember (Name = "min_corner")] public GeometryMsgs.Vector3 MinCorner { get; set; }
        // The maximum corner of the box, with respect to the robot starting pose
        [DataMember (Name = "max_corner")] public GeometryMsgs.Vector3 MaxCorner { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public WorkspaceParameters()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public WorkspaceParameters(in StdMsgs.Header Header, in GeometryMsgs.Vector3 MinCorner, in GeometryMsgs.Vector3 MaxCorner)
        {
            this.Header = Header;
            this.MinCorner = MinCorner;
            this.MaxCorner = MaxCorner;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public WorkspaceParameters(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            MinCorner = new GeometryMsgs.Vector3(ref b);
            MaxCorner = new GeometryMsgs.Vector3(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new WorkspaceParameters(ref b);
        }
        
        WorkspaceParameters IDeserializable<WorkspaceParameters>.RosDeserialize(ref Buffer b)
        {
            return new WorkspaceParameters(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            MinCorner.RosSerialize(ref b);
            MaxCorner.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/WorkspaceParameters";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d639a834e7b1f927e9f1d6c30e920016";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VVwW7jRgy9C8g/EMhhk8LRAknRQ4DegnZzWGCBBL0GtERJg46G6szItvr1fRw5ShbY" +
                "wx4aXyxr+B4fycfxJT0PLtEoKXEv1GjI7EIipiSZtKOJI4+SJSaak3SzJxcuqks7zi70NE+UB6GD+nkU" +
                "umLa6+kaMXQcXDOUs6h7zYQs7L0epaWsNOpBauMp6d1GrsEvgEqgyXMIlqHTiPC982JicjJVhnyj5kRH" +
                "8R589v5BOhekHHem3eKjdBIlNFLY7AgyUW0MKOyi+iLcSqShfK0sz4gZXZjHeTzHGc8ZuaOjywNY0yRN" +
                "tnrexKQMkaZ70iQXVS+K9sXlZUx9+vwXwjXeGfPLyvouG5/cB2Xj05at+v1//lRfn/68h452zbm2EiU9" +
                "ZQ4txxbeytxy5tL6wfWDxBsvB/Emfpzgh3Kal0lS/c4RvUAwLLOYN4ppGh3HObiGM8br4Nn3eCDhOi4e" +
                "cc3sOSJeY+uChRcnGHux7j9z8cLjw705PkkzZwdBCxiaKJysoY8PVM0u5LtbA1SXz0e9wU/pMZstOSbB" +
                "xdpymjAf08npHjl+WYurwY3mCLK0ia7Kuxf8TNeEJJAgk2JLrqD825IHDesycXS8h99B3KADYP1koE/X" +
                "75hDoQ4c9JV+ZXzL8TO0YeO1mm4GzMxb9Wnu0UAETlEPrkXofikkjXcSMnm3jxyXylBryuryj7Jt2cZX" +
                "JmIbn5I2DgNoi4erlGNZaYt8ce1HufGHa/BqrSg2KhRht9yhnJlzuiioZOJGajPJYxlruY5G4VDWbkMC" +
                "2LoIqNNQ2/LiftEoO3KZWpVEQTM4Rv4blIIeG5qnCWQweuSQPBvWXgNyJXVf79Zrr0RZj4qjyw64hqLr" +
                "XbsikWjcwEzn4naUu1v02J+v0DUZBgaSqLkArmt67GjRmY5WEB7iefWU9rLpKhbJqjvbuzPF9w39pliE" +
                "7T8DfxcZS19XVeeV82+/0ml7Wranf6v/AHW4BbtrBgAA";
                
    }
}

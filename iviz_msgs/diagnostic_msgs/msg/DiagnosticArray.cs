/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class DiagnosticArray : IDeserializable<DiagnosticArray>, IMessage
    {
        // This message is used to send diagnostic information about the state of the robot
        [DataMember (Name = "header")] public StdMsgs.Header Header; //for timestamp
        [DataMember (Name = "status")] public DiagnosticStatus[] Status; // an array of components being reported on
    
        /// Constructor for empty message.
        public DiagnosticArray()
        {
            Status = System.Array.Empty<DiagnosticStatus>();
        }
        
        /// Explicit constructor.
        public DiagnosticArray(in StdMsgs.Header Header, DiagnosticStatus[] Status)
        {
            this.Header = Header;
            this.Status = Status;
        }
        
        /// Constructor with buffer.
        internal DiagnosticArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Status = b.DeserializeArray<DiagnosticStatus>();
            for (int i = 0; i < Status.Length; i++)
            {
                Status[i] = new DiagnosticStatus(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new DiagnosticArray(ref b);
        
        DiagnosticArray IDeserializable<DiagnosticArray>.RosDeserialize(ref Buffer b) => new DiagnosticArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Status);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            for (int i = 0; i < Status.Length; i++)
            {
                if (Status[i] is null) throw new System.NullReferenceException($"{nameof(Status)}[{i}]");
                Status[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + BuiltIns.GetArraySize(Status);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "diagnostic_msgs/DiagnosticArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "60810da900de1dd6ddd437c3503511da";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwW7bMAy96ysI5NB2QNutvRXIoUC7rei2FmmxHYahoC3WFmpLniQn89/vyYqTLMCA" +
                "HbYFDiTZ5CP5+KgZPdYmUCshcCWEbR9EU3QUxGrShivrQjQlGfvsfMvROEtcuD5SrIVC5CjknseDd4WL" +
                "6r2wFk91XmbwomgQIHLbqasN4AM8+/D12wjRB5oRA9h7HhJc6drOWbExUCHGVuSlcz4iNWfV/C//1MeH" +
                "dxfIQz+1oQqnuQA1I6RoNXsNeiJrjkypmNpUtfjjRpbSpOTbDlmNX+PQSThRa07xVGLFc9MMG1ZRV9tb" +
                "UybWNqxM/vA04IA69iCob9jD3nltbDJ/9txKQscT5HsvthS6ubqAjQ1S9tEgoQEIpRcOibObK1K9sfH8" +
                "LDmo2ePKHeMoFfqyCY7OcUzJyo/OQwZIhsMFYrzKxZ0AG+QIouhAh+O7JxzDESEIUkBjypoOkfn9EGuo" +
                "I0lhyd5w0YyKKsEAUA+S08HRDnJK+4IsQxBr+Iy4jfEnsHaDm2o6rtGzJlUf+goEwrDzbmk0TIthBCkb" +
                "A2FRYwrPflDJK4dUs7eJYxjBa+wIVg7BlQYN0LQysVYh+oQ+duPJaPWP1LidvCzK/cFRe5Nbuwb8TSOJ" +
                "ecIQYaCM1QbF99xsR+qXcR0Fhf+9C8EkZkddj+6ug3jTvAdVDBDg3e38dd59uVx8mr/J++vF4m4xP8uH" +
                "h8fLD9fzc5VPeURm63UXkcT2bdontRVuKTTxalMDcBeQllB6043W64QjFHu6LSNfCXCafCcufuOeiZmM" +
                "awz2in3q4egwnQnjieGibKZuZfjMTS+4qJZp3b+o1i/3VLIb738pZMp0KvBFBuS6StONi6fhAj0YlT2m" +
                "jA9iaWlktUNg/pLYyDv4Rc/lC6FD+cpQ6idpFVA/MgYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

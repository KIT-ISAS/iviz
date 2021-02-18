/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/TableArray")]
    public sealed class TableArray : IDeserializable<TableArray>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Just an array of tables
        [DataMember (Name = "tables")] public ObjectRecognitionMsgs.Table[] Tables { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TableArray()
        {
            Tables = System.Array.Empty<ObjectRecognitionMsgs.Table>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TableArray(in StdMsgs.Header Header, ObjectRecognitionMsgs.Table[] Tables)
        {
            this.Header = Header;
            this.Tables = Tables;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TableArray(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Tables = b.DeserializeArray<ObjectRecognitionMsgs.Table>();
            for (int i = 0; i < Tables.Length; i++)
            {
                Tables[i] = new ObjectRecognitionMsgs.Table(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TableArray(ref b);
        }
        
        TableArray IDeserializable<TableArray>.RosDeserialize(ref Buffer b)
        {
            return new TableArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Tables, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Tables is null) throw new System.NullReferenceException(nameof(Tables));
            for (int i = 0; i < Tables.Length; i++)
            {
                if (Tables[i] is null) throw new System.NullReferenceException($"{nameof(Tables)}[{i}]");
                Tables[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var i in Tables)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/TableArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d1c853e5acd0ed273eb6682dc01ab428";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VTW/bRhC9L+D/MIAPToJYBdIiBwc5FChap0A+CutWFMKIHJGbkrvMzlIy8+v7ZmlR" +
                "dgw0PTQRCPBjZ97Me/N2dS1cS6K23M7cmTun30fNxIE4JZ4o7ijzthM9c3H7Uaq8SVLFJvjsY9j02ugP" +
                "a1v/868lzr3+n3/u7c1vV6S5nutdl2bR6U3mUHOqqZfMNWemXQQX37SSLjvZS4ck7gepqazmaRBdIXHd" +
                "eiVcjQRJ3HUTjYqgHKmKfT8GX3EWyr6XB/nI9NCFBk7ZV2PHCfEx1T5Y+C5xL4aOS+XTKKESevPLFWKC" +
                "SjVmj4YmIFRJWH1osEhu9CH/+MIS3Pn6EC/xKg0mshSn3HK2ZuV2SKLWJ+sVajybya2ADXEwlFArPSnf" +
                "NnjVp4QiaEGGWLX0BJ1/mHIbAwCF9py8jcuAKygA1AtLunh6DzkU6MAhHuFnxFON/wIbFlzjdNliZp2x" +
                "17GBgAgcUtz7GqHbqYBUnZeQqfPbxGlyljWXdOe/msYIQlaZCO6sGiuPAdR08Ll1mpOhl2lsfP2t3Piv" +
                "mwFc3wR4sdd5erBMxwFsyxahlpW2IoFqyQCxiVpMA4ME6iLMBzjbjNePd+caAg2YR4lWmuJYNMuJg1rF" +
                "uWDmv2Vei7OkJ5vqpFl6g7KtbanW0/MiXnmPyTeYqsZeDthIYp5f4goRwfFQl28G85n4FnMIKM7dsWAJ" +
                "O3ONACWnadbmg/VtzS9UUnFKiNSMDAZZ5K7/pV4dwfLd+zXsnwVVd2PCYrKw0pYBwZt7uaV27LpXszlw" +
                "fSzHmNIOsuN2kIs9uG1V0h6C+7w6qnnH974aj/UyRB8UNr3Tc6k4c7mRbAhDxP5VO4Z6M+EXoQ9qPBYH" +
                "qThF5/jNDP2NzPt4LKDwMyWxEwZ7rxhw5qNl/ruEyejAFYyCI9I+13frxfvFEBDymLsiV/gsAe6PEWKm" +
                "UHBPcd+LIFo5HvvQNzNGOdv02D+48Dy9h3TdroucX/5Et8vTtDx9/j7tn6Q7clgGpfZHfU/Ph83b26eT" +
                "7ubKlfsKo+PTwbl/APey9p8cCAAA";
                
    }
}

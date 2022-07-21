/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Scene : IDeserializableCommon<Scene>, IMessageCommon
    {
        // A complete scene 
        /// <summary> Name of the scene </summary>
        [DataMember (Name = "name")] public string Name;
        /// <summary> Original filename </summary>
        [DataMember (Name = "filename")] public string Filename;
        /// <summary> List of models to be included </summary>
        [DataMember (Name = "includes")] public Include[] Includes;
        /// <summary> List of lights </summary>
        [DataMember (Name = "lights")] public Light[] Lights;
    
        public Scene()
        {
            Name = "";
            Filename = "";
            Includes = System.Array.Empty<Include>();
            Lights = System.Array.Empty<Light>();
        }
        
        public Scene(string Name, string Filename, Include[] Includes, Light[] Lights)
        {
            this.Name = Name;
            this.Filename = Filename;
            this.Includes = Includes;
            this.Lights = Lights;
        }
        
        public Scene(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Filename);
            b.DeserializeArray(out Includes);
            for (int i = 0; i < Includes.Length; i++)
            {
                Includes[i] = new Include(ref b);
            }
            b.DeserializeArray(out Lights);
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i] = new Light(ref b);
            }
        }
        
        public Scene(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Filename);
            b.DeserializeArray(out Includes);
            for (int i = 0; i < Includes.Length; i++)
            {
                Includes[i] = new Include(ref b);
            }
            b.DeserializeArray(out Lights);
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i] = new Light(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Scene(ref b);
        
        public Scene RosDeserialize(ref ReadBuffer b) => new Scene(ref b);
        
        public Scene RosDeserialize(ref ReadBuffer2 b) => new Scene(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.SerializeArray(Includes);
            b.SerializeArray(Lights);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.SerializeArray(Includes);
            b.SerializeArray(Lights);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Filename is null) BuiltIns.ThrowNullReference();
            if (Includes is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Includes.Length; i++)
            {
                if (Includes[i] is null) BuiltIns.ThrowNullReference(nameof(Includes), i);
                Includes[i].RosValidate();
            }
            if (Lights is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Lights.Length; i++)
            {
                if (Lights[i] is null) BuiltIns.ThrowNullReference(nameof(Lights), i);
                Lights[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Filename);
                size += WriteBuffer.GetArraySize(Includes);
                size += WriteBuffer.GetArraySize(Lights);
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Name);
            WriteBuffer2.AddLength(ref c, Filename);
            WriteBuffer2.AddLength(ref c, Includes);
            WriteBuffer2.AddLength(ref c, Lights);
        }
    
        public const string MessageType = "iviz_msgs/Scene";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "5e11da295d41bbfbd413d6274556c4a9";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VyW7bMBA9R19BINcCbZN0BXpQJDomqg2SnDYoCoGWKZutNpBUln59h5Io08i1qQ+m" +
                "9GY4fPM4MzpHLiq7pq+ZYkiWrGXIcaQSvN2jljbs7OwcRbCirkLqMLsYh4rXzDjFgu95S+sFdEhb1sOO" +
                "/fiJ+PQkwS3gUulYTbdjtUSqQ1tm7Dsn4PuDgg21XuWZ5T8hjvPlH/+cMLv5jPg9/1M0ci9fz6Sdc5Sy" +
                "ignWlkyTpC1ij4oJnSCVkqlFpEFwpIluYJ01mhxCCg6PV6jv5ChQAuszDyY4RGzmBxOzp+Vvuh93kWo8" +
                "4UAlopNocAUH1rBXEIhLkG7HS9gux7jzRs1YMirKg4NeXrI5UaeqO6ouL368ff8TNegcie4BMvvVif/D" +
                "YVJw4K36iK4DHPmFj1fuJsjRF/TmBHd9n+TkFoPh7UmxO15Xd+LyAtFmy1mrlvcdr6pBHu2s4VLye2Zy" +
                "Rh0oz9XT8r4dmr6QJa0h9ALKA295y6R8jhTAgrV7dVhMglU1KxWkCGEn9ltorF2hi8DJoRoHoXtLTU//" +
                "oTXm5GcyYl73hty80pcnMic/H5jfJbiI4ghb9zxiPlmtNtl0yxacJdjbBG4K+IWNu+E1wZGulksbxiHJ" +
                "sqlYrmx8jcnNWnu/O+WRhm6QAfz+5Mw1iUiEM234YBvixPVIfgfwx1PqWRK4Hg4nQp9sW6DPDd1E53WS" +
                "b4pXAfZyEkfadJLzJvoaxd9G/MKZDRAiIdFNsUrjsNjcWuoZS5ascWrrZwzeXUAiH9sSGtN1/N1S0KCQ" +
                "TGQraPAjr3eGVpwUITQtSYI7ixKg0LUWFQCyzXWeul5usQDUJ7fExxYH7RnGcb6eI1xZOLmJsD/jC4Nv" +
                "qZsU+s86f8S8wA0Ti8MIhiRNY1uJEfWx5wYjieNMh+YGB2htmNnscfZWT70p5Ib2vR4Xk9NwX0x+y0gZ" +
                "u7+ipepM73U9E1Txrp3fHwTtx/lQDM+Q+5fvy/HrbVRMYhLZo9cn6VScoy5GwiyJp/s7GcOWNNuuq1FJ" +
                "pSrkge66B/lsJi8Dk7Z75twyrc9lpT+7fJRmQXZc6IEKkNnC25aJAvbV1iQf1II5L6+ZYbecf7zw49fk" +
                "j+P8BaWYgtuoCQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Scene : IDeserializable<Scene>, IMessage
    {
        // A complete scene 
        /// Name of the scene
        [DataMember (Name = "name")] public string Name;
        /// Original filename
        [DataMember (Name = "filename")] public string Filename;
        /// List of models to be included
        [DataMember (Name = "includes")] public Include[] Includes;
        /// List of lights
        [DataMember (Name = "lights")] public Light[] Lights;
    
        /// Constructor for empty message.
        public Scene()
        {
            Name = string.Empty;
            Filename = string.Empty;
            Includes = System.Array.Empty<Include>();
            Lights = System.Array.Empty<Light>();
        }
        
        /// Explicit constructor.
        public Scene(string Name, string Filename, Include[] Includes, Light[] Lights)
        {
            this.Name = Name;
            this.Filename = Filename;
            this.Includes = Includes;
            this.Lights = Lights;
        }
        
        /// Constructor with buffer.
        public Scene(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            Filename = b.DeserializeString();
            Includes = b.DeserializeArray<Include>();
            for (int i = 0; i < Includes.Length; i++)
            {
                Includes[i] = new Include(ref b);
            }
            Lights = b.DeserializeArray<Light>();
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i] = new Light(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Scene(ref b);
        
        public Scene RosDeserialize(ref ReadBuffer b) => new Scene(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.SerializeArray(Includes);
            b.SerializeArray(Lights);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Filename is null) throw new System.NullReferenceException(nameof(Filename));
            if (Includes is null) throw new System.NullReferenceException(nameof(Includes));
            for (int i = 0; i < Includes.Length; i++)
            {
                if (Includes[i] is null) throw new System.NullReferenceException($"{nameof(Includes)}[{i}]");
                Includes[i].RosValidate();
            }
            if (Lights is null) throw new System.NullReferenceException(nameof(Lights));
            for (int i = 0; i < Lights.Length; i++)
            {
                if (Lights[i] is null) throw new System.NullReferenceException($"{nameof(Lights)}[{i}]");
                Lights[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetStringSize(Filename);
                size += BuiltIns.GetArraySize(Includes);
                size += BuiltIns.GetArraySize(Lights);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Scene";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "5e11da295d41bbfbd413d6274556c4a9";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
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

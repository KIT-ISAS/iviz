/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Scene")]
    public sealed class Scene : IDeserializable<Scene>, IMessage
    {
        // A complete scene 
        [DataMember (Name = "name")] public string Name; // Name of the scene
        [DataMember (Name = "filename")] public string Filename; // Original filename
        [DataMember (Name = "includes")] public Include[] Includes; // List of models to be included
        [DataMember (Name = "lights")] public Light[] Lights; // List of lights
    
        /// <summary> Constructor for empty message. </summary>
        public Scene()
        {
            Name = string.Empty;
            Filename = string.Empty;
            Includes = System.Array.Empty<Include>();
            Lights = System.Array.Empty<Light>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Scene(string Name, string Filename, Include[] Includes, Light[] Lights)
        {
            this.Name = Name;
            this.Filename = Filename;
            this.Includes = Includes;
            this.Lights = Lights;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Scene(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Scene(ref b);
        }
        
        Scene IDeserializable<Scene>.RosDeserialize(ref Buffer b)
        {
            return new Scene(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.SerializeArray(Includes, 0);
            b.SerializeArray(Lights, 0);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Filename);
                foreach (var i in Includes)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Lights)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Scene";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "5e11da295d41bbfbd413d6274556c4a9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
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

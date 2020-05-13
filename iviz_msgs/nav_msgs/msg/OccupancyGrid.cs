using System.Runtime.Serialization;

namespace Iviz.Msgs.nav_msgs
{
    [DataContract]
    public sealed class OccupancyGrid : IMessage
    {
        // This represents a 2-D grid map, in which each cell represents the probability of
        // occupancy.
        
        [DataMember] public std_msgs.Header header { get; set; }
        
        //MetaData for the map
        [DataMember] public MapMetaData info { get; set; }
        
        // The map data, in row-major order, starting with (0,0).  Occupancy
        // probabilities are in the range [0,100].  Unknown is -1.
        [DataMember] public sbyte[] data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public OccupancyGrid()
        {
            header = new std_msgs.Header();
            info = new MapMetaData();
            data = System.Array.Empty<sbyte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public OccupancyGrid(std_msgs.Header header, MapMetaData info, sbyte[] data)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.info = info ?? throw new System.ArgumentNullException(nameof(info));
            this.data = data ?? throw new System.ArgumentNullException(nameof(data));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal OccupancyGrid(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.info = new MapMetaData(b);
            this.data = b.DeserializeStructArray<sbyte>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new OccupancyGrid(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.header);
            b.Serialize(this.info);
            b.SerializeStructArray(this.data, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            header.Validate();
            if (info is null) throw new System.NullReferenceException();
            info.Validate();
            if (data is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 80;
                size += header.RosMessageLength;
                size += 1 * data.Length;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/OccupancyGrid";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3381f2d731d4076ec5c71b0759edbe4e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VTW/TQBC9768YKQdalIS0IIQqcUCqgB4qioBTVEWT9cResHfN7rqp+fW8XcdJChLi" +
                "QKmsxl7PvHnz5sMT+lyZQF5aL0FsDMR0Pruk0puCGm6nZCxtK6MrEsY/LXV9bB0roda7Na9NbWJPbqMm" +
                "5LTuWra6nyv1XrgQT9Xwo9TkWiJfcmTaOJ/dEUVdc7s/N3bjYAdi+R0VOMw0vNvOGv4KN+cBNqUQ2Udj" +
                "S9qaWNHJYro4nRN9GKMD40DNCFLzknBSUM+2FFoupmeLxS2cvthv1m0tQYvZ2VwZG18tb3NopV7/4z91" +
                "/endBcgXqyaU4dmgEMh+imwL9tAdUhSjRJUpK/GzWu6kThk3rRSZGMW+lTDPSoE2rlKseK7rnroAo+hI" +
                "u6bprNEchaJp5IE/PKEGU5tU1F3NHvaQ1thkvvHcSELHFeR7J1YLXV1ewMYG0V00INQDQXvhkKpwdUmq" +
                "g3LPz5ODmnzeuhkepUTh98GhPsdEVu5TEyWeHC4Q4+mQ3BzYEEcQpQh0ks9WeAynhCCgIK1DH56A+U0f" +
                "KzeU84694XUtCVhDAaA+SU5PTo+QbYa2bN0IPyAeYvwNrN3jppxmFWpWp+xDV0JAGKLn7kwB03WfQXRt" +
                "MCpUm7Vn36vkNYRUk7dJYxjBK1cEvxyC0wYFKHJXqxB9Qs/VWJnisbrR8t3QjUeTOHZW5WokgxrrPJu+" +
                "4WggEK9dF4cMK/aso3gTsBLcJh/ux/CdT7SHec7Jo/7DRtlNP205UO0wBcWgDs5W6XmVno42AfrF1V0O" +
                "vmyepVV0qzYwTB13eAeH6wRqCiyFZbIKt2Nf5sOdQSWYrPirxXC6C+q8KdERu4wShWUzJVyei7Q1xsHL" +
                "G0W4nm2dh1YtGmznBKC8MvNyGpcPgOaqFIc59/0g+012yeEeqcK/xwO3N4ddPhQVrDN7MN14QaO2rGWa" +
                "9kg6LnbvzdAAtkiUR985qRsHEfcG6mOHPvY24x7sHquFf00QVMYOxrRHNnb3uRr5Ixcsv0z5QbpDT718" +
                "Qff7u35/9+P/0D9IN+Zw/Il+oOdD8unp+0H3NK74Cv85o/Fuq9RPKZHLdhAIAAA=";
                
    }
}

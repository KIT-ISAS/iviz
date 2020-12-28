/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract (Name = "pcl_msgs/PolygonMesh")]
    public sealed class PolygonMesh : IDeserializable<PolygonMesh>, IMessage
    {
        // Separate header for the polygonal surface
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Vertices of the mesh as a point cloud
        [DataMember (Name = "cloud")] public SensorMsgs.PointCloud2 Cloud { get; set; }
        // List of polygons
        [DataMember (Name = "polygons")] public Vertices[] Polygons { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PolygonMesh()
        {
            Header = new StdMsgs.Header();
            Cloud = new SensorMsgs.PointCloud2();
            Polygons = System.Array.Empty<Vertices>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PolygonMesh(StdMsgs.Header Header, SensorMsgs.PointCloud2 Cloud, Vertices[] Polygons)
        {
            this.Header = Header;
            this.Cloud = Cloud;
            this.Polygons = Polygons;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PolygonMesh(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Cloud = new SensorMsgs.PointCloud2(ref b);
            Polygons = b.DeserializeArray<Vertices>();
            for (int i = 0; i < Polygons.Length; i++)
            {
                Polygons[i] = new Vertices(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PolygonMesh(ref b);
        }
        
        PolygonMesh IDeserializable<PolygonMesh>.RosDeserialize(ref Buffer b)
        {
            return new PolygonMesh(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Cloud.RosSerialize(ref b);
            b.SerializeArray(Polygons, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Cloud is null) throw new System.NullReferenceException(nameof(Cloud));
            Cloud.RosValidate();
            if (Polygons is null) throw new System.NullReferenceException(nameof(Polygons));
            for (int i = 0; i < Polygons.Length; i++)
            {
                if (Polygons[i] is null) throw new System.NullReferenceException($"{nameof(Polygons)}[{i}]");
                Polygons[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += Cloud.RosMessageLength;
                foreach (var i in Polygons)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "pcl_msgs/PolygonMesh";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "45a5fc6ad2cde8489600a790acc9a38a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71W32/bNhB+119B1A+Ni9hDki4LAhhDsSBLgC4NkGwvwxBQ5MkiJpEuSTlV//p9R4q2" +
                "G+RhD+sEG7aOd9/9+u6kmXigjfQykmhJavKicV7ElsTGdePaWdmJMPhGKqpuskLWq2biD/LRKArCNcmi" +
                "p9AKGYSErbFRqM4Nugpkg/NPfViHH+5Z/guLT6fTmfhoQmSEyV+oCuyff+1l1eo/vqrfHn69FCHqHNhN" +
                "yekhSqul10gmSi2jTPVozbolv+hoSyhHlP2GtEincdxQWMLwsTVB4LMmS1523SiGAKXohHJ9P1ijuMbR" +
                "oEiH9rA0lismOemhkx76zmtjWb3xsidGxyfQ54GsInF7dQkdG0gN0SCgEQjKkwzGrnEoqgFFPjtlg2r2" +
                "+OwWuKU1Grdzjm7JyMHSl42nwHHKcAkf73JyS2CjOAQvOoijJHvCbZgLOEEItHGqFUeI/H6MrbOp/Vvp" +
                "jaw7YmCFCgD1LRu9nR8g2wRtpXUFPiPuffwbWLvD5ZwWLXrWcfZhWKOAUNx4tzUaqvWYQFRnCIzsTO2l" +
                "Hyu2yi6r2TXXGEqwSh3BrwzBKYMGaPFsYluF6Bk9dePJ6O/GxtcnpZALzQtyjTl1neYhUw7FUNGgThif" +
                "u4VGUjaYNLJpAMOxeG4NGtXLESAod5RMNq1NzGrGgt29TBhhUGl6LUs62DJtgBfHY0FRLREEASWPdqlU" +
                "iM4n9iCeGpz1o6g7V8M4BtHJ0Q3QpaC8qXfNmEJBP3ab401jCEm9EdJ7OS6rlDIdrpHsEYmImoTza2nN" +
                "VyCeatClR1UWnfmb5jgRJ7xUjgakgYkmPV+K+z1MOLBF0DBP1qEggzd6UClUDhP99hIJbGI7dSfs6hQi" +
                "4B17TBx0zaLpsCZijp4JhuSyUQ5eqs+DCan0xwKMzcR8Mew8wEe8cs50KXaYL1+sXpycXiEAP6g4eCpV" +
                "PCjXUtw2E/G5eOjUriDHQOE4IQTOSYrk2WhkCC026ciucfcKaNksGaDcJWOO6WpqdIZRmEpLXSipGl8I" +
                "YfJgT3xJtWHSLKvUqGumAnZ/pkRV1c51ApcJT7XBdtVGWjETt+FgZncHP5egctxPaNIGtnjK7JIqTydE" +
                "UY+Rws7Cu+ei/9ICR9/qXyDA5LlcM/EBvSiTl86Osdq+prV1VKDf5crNp6yQkgZBaA/y6AdYpMqjrxJf" +
                "6+B5KzujJzb8b9snNeL15cPty2O9KfvH2cIVDLYfpybD/PCZX1Dy1sl8uhC3d48XnP5KnEyS3yfRSpzu" +
                "dU7Ok+TsQIdFK/F+r8N9hOTHAx0WrcT5JLn++OkDi1bip0PJ+XtILqqy6S1P4tSSO5lHOfGxkMU1TaCY" +
                "FT7l/413PT9T/PQ2w6XIIzo5SqTg9wU2uir/yQ68ZPJWCISm125LxY9yg41TIDcgYS/tKKijPm3PaZBy" +
                "ZN+JFhvVZU6U17JvXtnyIOkkzyFjMLZFs/oHIFCfsV4KAAA=";
                
    }
}

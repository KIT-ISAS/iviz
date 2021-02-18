/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectInformation")]
    public sealed class ObjectInformation : IDeserializable<ObjectInformation>, IMessage
    {
        //############################################# VISUALIZATION INFO ######################################################
        //################## THIS INFO SHOULD BE OBTAINED INDEPENDENTLY FROM THE CORE, LIKE IN AN RVIZ PLUGIN ###################
        // The human readable name of the object
        [DataMember (Name = "name")] public string Name { get; set; }
        // The full mesh of the object: this can be useful for display purposes, augmented reality ... but it can be big
        // Make sure the type is MESH
        [DataMember (Name = "ground_truth_mesh")] public ShapeMsgs.Mesh GroundTruthMesh { get; set; }
        // Sometimes, you only have a cloud in the DB
        // Make sure the type is POINTS
        [DataMember (Name = "ground_truth_point_cloud")] public SensorMsgs.PointCloud2 GroundTruthPointCloud { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectInformation()
        {
            Name = string.Empty;
            GroundTruthMesh = new ShapeMsgs.Mesh();
            GroundTruthPointCloud = new SensorMsgs.PointCloud2();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectInformation(string Name, ShapeMsgs.Mesh GroundTruthMesh, SensorMsgs.PointCloud2 GroundTruthPointCloud)
        {
            this.Name = Name;
            this.GroundTruthMesh = GroundTruthMesh;
            this.GroundTruthPointCloud = GroundTruthPointCloud;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectInformation(ref Buffer b)
        {
            Name = b.DeserializeString();
            GroundTruthMesh = new ShapeMsgs.Mesh(ref b);
            GroundTruthPointCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectInformation(ref b);
        }
        
        ObjectInformation IDeserializable<ObjectInformation>.RosDeserialize(ref Buffer b)
        {
            return new ObjectInformation(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            GroundTruthMesh.RosSerialize(ref b);
            GroundTruthPointCloud.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (GroundTruthMesh is null) throw new System.NullReferenceException(nameof(GroundTruthMesh));
            GroundTruthMesh.RosValidate();
            if (GroundTruthPointCloud is null) throw new System.NullReferenceException(nameof(GroundTruthPointCloud));
            GroundTruthPointCloud.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += GroundTruthMesh.RosMessageLength;
                size += GroundTruthPointCloud.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectInformation";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "921ec39f51c7b927902059cf3300ecde";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71XXW/bNhR9F9D/cNE8NCkSD0m6LshgDGntNMYcO2icAmtRGLREW9wkUiUpJ+qv37mk" +
                "ZLtdBqzAWsGJZYr33O9zqb29b7no3ej27mI8en8xG00nNJpcTumbADbXk+Qx+NnV6Dai3l5N78YDejWk" +
                "6avZxWgyHGB9MLwZ4t9kNv6DLt9Or7F/SK+nb4eHNB79PsQOupjQ23ej93QzvnuDn49qhm6a5ZLyuhSa" +
                "rBSZWBSStCglmSV5PDKLP2XqnyTOW6VX4dFWblkXBZXS5V/uPse9cpQCcyGpdhL7aGksZcpVhWioqm1l" +
                "nHSHJOpVKbWXGWsvlG+o1+vRovakfAewUCtWeC3+kuRqK4Mm31SSoOR6eHsF63JRyXnpVu6nazZnZU2t" +
                "s7m3tc/nbGC0+daU0quSFTemJqOLhnKxliQoLUydkdIBfPDq3xXeTEeT2S1USu2MjTpvjNL+NSOcfKm6" +
                "4gfzgP0kSfr/85Vc3745p698h+EDuVRaeWU050WEDCVYL5TzIVNWCb0qpPs1eKZ0Jh9oLYpaOqRhKS15" +
                "Q0hQgHAclLW0XqXSffiYsI5ZC/Dh4xaLFTCaSH0tio0E1oSnkkNZV2FDsGYlORW22YkfwDqhHxSqzo1H" +
                "Qta59cxtjaph5OnJh9Nop3yYI3Df09pHYhT6jlvLaC+UdiGgXaai5aHkOGdLK1G+lUhlsiyM8C9f0MPm" +
                "rtncff5uwX68QToXUAZOrMA9psgcN6ApCnBH68fkKEOfaoefKKbgE5r2PldpjmJqANKGgESWBe+xTWmQ" +
                "TCkChquxUzjSvFJAFgiM55tDkj7tMX8BJUYrE15wbztvLKhIsD0LpYVtaFGYBYS9IxCXAS9l0qVWLbBt" +
                "0XD4W1NAYq5jwadLJeHUUxLWiqaXtGwZdUWiCRrhCPObsSuh1WcgnmS0r0pE5ahQf8kDPKHjDNL7NdzI" +
                "JGw76NHNFsbtyMJoiAdp1yFX1mR1GkxlM8HdVsCByudtdtwmTs4D3rBGJsgjszxaFmqV+2i9igMhCkXj" +
                "RfqpVrHwQOM6C56nBnYicB6zwfIUGQ1on5n/NOuC7Q56yRUmDVgmD1+s4GQAA2wN6rCbybMTrh6N4lrL" +
                "0o42ATkECtuJReAcB0vuVQYPVeyOQuqVzx8DbRu6Beh+BeEkUEJMdIRJc6G1LFznqrJdQbQzo62XEBsu" +
                "ml4SEnXJpQBqiyWRJAtjCsKl3BxzTYJBMOT2aOTizIzi3YPfOqOi3XMkqYLsHo03Tu00/KLxG5Yisua+" +
                "2/+1BB59uf8MBgbN3bVHF5HGt/1xSA51xlHd76Cfx8gdtF7BpQwFIrcgM1tDIkQeeRX40waaMWpU1lbD" +
                "d2Mfn0XqibXGw98jdcJmYB4vgrdcmDk8kPaokGtZQEiUlWy7kwe+63V0hQ+Sgv4pcGbAkSbjGZmasqy1" +
                "Srnew7liVx6STE9UCZ4fdSHsP9qD0fFx8lMtdcrdcs5k4mRaewWDGiCkOBg5PnqhlbrkQiDZm92bIya1" +
                "lbRb5XHcwlj5UFkwbCCGc+h4Hp3rAZupGVrAHvthbY6f7gDpZRNkZUAI+7D8pvG5ibW9FhiHfDIMpzrw" +
                "dEbPWOjZwQ6yDtBaaNPBR8Stjv8Cqze4gYfQdVnB3rt6hQBiIzhtrbIN/aKXFbgXZ5uFRf8lLBVVJnuX" +
                "gYK2fYVv4ZxJleAD571Cn7fH2pCNucp+2CwMtPD4KGSn4pCpumlodMdccNU2LeVAfPfg2aHEGRjZ7Qxv" +
                "ArMzbsY+Hbcrd+1Sn062e45fhpXTnT281KcX2z3MKlj5eWcPL/XpZbtyOZ5e8FKfftldwSGjT2fJ7itE" +
                "RxCT9k0jsGNX3Wa5dNLHDdN4v7Sm5KzacH6NoYgDo1UUEhyO6BAadPdS1zzy4oxyOA6JhVnLTk+Kg7pv" +
                "DbkCJeIdqCFZyDLM8pbWo2XJ32I2ZC8eDgAA";
                
    }
}

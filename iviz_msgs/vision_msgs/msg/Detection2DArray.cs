/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Detection2DArray : IDeserializable<Detection2DArray>, IMessage
    {
        // A list of 2D detections, for a multi-object 2D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection2D[] Detections;
    
        /// Constructor for empty message.
        public Detection2DArray()
        {
            Detections = System.Array.Empty<Detection2D>();
        }
        
        /// Explicit constructor.
        public Detection2DArray(in StdMsgs.Header Header, Detection2D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        /// Constructor with buffer.
        internal Detection2DArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Detections = b.DeserializeArray<Detection2D>();
            for (int i = 0; i < Detections.Length; i++)
            {
                Detections[i] = new Detection2D(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Detection2DArray(ref b);
        
        Detection2DArray IDeserializable<Detection2DArray>.RosDeserialize(ref Buffer b) => new Detection2DArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Detections);
        }
        
        public void RosValidate()
        {
            if (Detections is null) throw new System.NullReferenceException(nameof(Detections));
            for (int i = 0; i < Detections.Length; i++)
            {
                if (Detections[i] is null) throw new System.NullReferenceException($"{nameof(Detections)}[{i}]");
                Detections[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + BuiltIns.GetArraySize(Detections);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "vision_msgs/Detection2DArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "5f62729a3134356dd48cf97c678c6753";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1ZW3PbuBV+56/AjB8S70pMmnQ8nXQ6bRN3u37YNm3SaybjgUhIQkISDEBaYn59v3MO" +
                "AFK2k92Hxp44pkDg4Fy/c9GZ+qNqbBiU26pnl6o2g6kG67qwUlvnlVbt2Ax27TYfsD7vcL4sih+Nro1X" +
                "e/5TFGcLUsPexJ2mVr13vQu6CSV2CL20lMmp1u72g9qZzng9GBBTIGKDUDzYYa9a3U2q0l1ta+xYsJpP" +
                "1WrrXQumg+12jVG268ehLC7TzmeX794vzhXF7/7PP8VPb/78QoWhvm7DLjwRBUGWNwPY1r5WrRk0uNes" +
                "3D1ENn7dmBvT4JBue0jAb4epN6HEwbekApskbJpJjQGbBqcq17ZjZytSxWBbc3IeJ20HPfTaD7YaG+2x" +
                "3/nadrR963VriDr+BfNpNF1l1NXlC+zpgqnGwYKhCRQqbzSpEi9VMdpueP6MDhRnbw9ujY9mB/Pny2Ev" +
                "PRCz5th7E4hPHV7gju9EuBK0oRyDW+qgHvPaNT6Gc7IXWDC9q/bqMTh/PQ1717EX3Whv9YaMGWD8pgHV" +
                "R3To0fmCMrH9QnW6c4m8UJzv+CVku0yXZFrvYbOGpA/jDgrERnjtja2xdTMxkaqxphvgohuv/VTQKbmy" +
                "OPuBdCwezBbBXx2Cqyy7KflzEQZP1Nka17b+Vt54YwOcXRxyEQmQ8tJsbWfA2EnkK9gOMQr/WPhfsK0l" +
                "L4Lj8e6qgTR2S+6HIyu1GWH5rmrGGvQQ2ZYp2Q5u3soWjmgo2h1IaH2LQrw0Yk7oTUWvVAWYUFiyrd4Z" +
                "0IXPgQUBB6c2RjWOAqAmbyeDgEVySd5+H0C9okvJjBu9sQ2YNKH4K0Pbj1PvQCHY8C/Y5jX8CFAhTAEn" +
                "zkjmjRuBPWB+447wCe/TZ7pZALIsXsbFl+5IR7CVTr/FDtIxxzZFyYxYdKlJN8FLS1Pi0450kkGS1ECx" +
                "7aBlt43YaETM81L9xQ048mm0nhCQNNg0hBNw7GAA5MEpOwA+J9KYafthKotguuC8eMUVaze40VdwxHZH" +
                "HF8Bw/1oVuLCiPBAexBbg7ZA3JgPBq+rj6SChaXLYuNcA6e5Tm+Z3qUgF7FHMANUB+xMSkM2mISzQo9w" +
                "nN0w3omkwU54o5vRIHCahuVv7EfCqNput7Awwz6rBDdY09SAGXJIZTQQxcIiiNsRmrxt7FJEndzIJ1hY" +
                "JgCXhkpGb9jlG6iQKc5C0fpbaKhMcZxePFAof8ltKQ13yT77/FrcLtvvvhAtk6cirSApkMliJo/UOGRh" +
                "Dgf3HZSuayYBrS6IKI0wGeb0fXUJ/xthBA0cgYPvR6TxNfJKzejLJAHcLTyNMj2FBdBm70bYsDee6Cot" +
                "Fnfu49hLXstplP7bwMtL9caYE/38k5+vwFmJz+x3rfPsYNo22WpkLJF6RoWJIAeq2gLrKYuI692uamLE" +
                "q5coSlx3g0SQYY4ll1NRFCSKhFFed4ikd0/Xv3pfFtvG6eHi1ypU4C1xcnFJ9sk33rFljAjeE+lvpGKq" +
                "GdEp7/LJRCU45KKtPeKNN4gXlkrQNKZDuSOZKiPMzuAkFAUkhnwItMjSCRRCWZwM846WbCo7mVLCxjNC" +
                "KiOeyCpi/vC3A4DF+uW3kg9PXAo1H0VjEm4zMVXUNpR8M0yQ41OcdvkOAQfaxG5GmQkIadl628h1kMtQ" +
                "Jwo6E0ru9U1UZyYwH4zGwX1ROZP4G8UexeArx6WFaBjh+I2g4OcuT7nbG6rFIAKledF3BxUjWEKvwSNX" +
                "1iMOeAIGygwFEYu8n6m/u8O61R+grUxJjBI94eJ4AffPIsNi3h6jHztv83ZYC5qGgwTOTFrcca2PSx4j" +
                "DAGSQd8jZ1P6McuzsDlVlI+PKzWt1OeV8m5Y4I76tyKKd5b/c//yf3n5PEXhu+cX7xfCPJzpuHW6q9+7" +
                "5lpR2U/LdXwvII5CdansUsGGFNxpQ/G3EbWG75juvO+hBAQryR1zAorwJPxDFnJOYvlE3AyPx/w05afP" +
                "D8P+rLr7QupEn7dCC58+zXonOENwfV2i9HR4iAripFRlF7xd5ErNIOjLoUOphSOHUrkAvuAuGnk0Pyjr" +
                "qB1HkJLwPSK8CdIbZJ2BwsgtJYuKhzXbneGWM2aQggt9ApLRZs1ECH4r5rykag0MmaNGSpSX3CSwXaQE" +
                "ZlrSSCSml1KtqBiOQqHzo+545uolc7nX7SOaPHSSBnSzg5WHPdkv1/LZeR9nUc9vB+K96TKp7S4MUF/F" +
                "L9M1wX42J/Tvo3d/JwJtQC408qlZWtYDS+vlCgR3Xc9eyR+nh4PA2JCSockc+PC6wfABxSh+ifftCCd7" +
                "LsmX7YAcEaci6IsoPdFExGDMIqHIfhRTTTx6F2ExefGYCXBHCtOR08AIuf3Ufd/EBhUtiv4onGAVHocc" +
                "gToDT6Rt3gA3jmpudGdShWF99jLeRdHgeowKeJpD/Qx5dzD+RqiDz2XtU49cpuKeCoUOHkX2rToYrlM4" +
                "OWJ8gTBwPdrZNMdDLcNDJBFR/eNKfBM3eHhTb8i55XaoTKGu7gwqHGlrmC63SzTj8VuAGWuzb1CnzSrD" +
                "NA9HqXVa0uGCggq6jWG1UpUfLQHRBrSGIek5t6hEFCZj554eAT0oyzXI+NAQQMGiG0NFi5se1cIcCHpj" +
                "aQ8sTmgk1871Gld0hM/cNNMzkGNAUBNn81QM9Pc0lkvCDTm2qXphNkauJo2a9EEZRJrPpSPXSakzpptI" +
                "uXii34Y6nCmKS+9kRkQGyqBau9hvgh5kg2FThkkDtFiz3ZvkqXLnUQz6Kbt1Tf0zyQXMDfqbTTxvTxOS" +
                "JHcGB5Ab1Sb0GieEPMOgiu7pSj095zEZjXh6TEW31K96RDhDluy7NdMBtPHPmYrL8zAyN0YYMSAvRCXy" +
                "iC6TS8dPfjKtNJhbkKLARTTHHgV0Kvz1+kuEYK4d0Bv75MBdQnPT9HVK3x9zUxyHYNJL8tA8NpZflen7" +
                "6ZRA7Q7dLzv4+fSgoJyLIBcT0lcp0CiJADypU6JE4mVejZResRqobf8SuRxxp+PUmQ9u3YHbw5cocAo1" +
                "ABJLo0U0JF1sLYs05AZsk2LnI0JYlldp0L1S3dhuxHzeHUI6fbA1+LlzmpfvPVy5Zmzp+whJ+43ZwTVi" +
                "NUTogH7dMTjHymprAarBV0+Y8HV6Hcqq7+eB1kGLo4T45QMVExo4j8IqTp45y6zUBxgWxzCIWwOSffgD" +
                "TWJCKQNBbMIstaOBD0yGQK+5/moxRIkDSpnnEt3ECKAoXpE5T6r4U1qg/oWrGrVeqwqz9g45rDUAs263" +
                "ku6Pn6gQud+QZEnkZKq54/Qvffskl1NGTkPpJ0uEuq21vdj9NzTe21gUFTX6v2g5wus0w8/vfp+/DBlM" +
                "f8LQD1RqwBdgw24HJwAHm2nAlJkvoG+fiNDiAMpYSrTSN/NbEZluxlckIP8d+9Z5UfwPYeKnkKsbAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

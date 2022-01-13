/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ARMarkerArray : IDeserializable<ARMarkerArray>, IMessage
    {
        [DataMember (Name = "markers")] public ARMarker[] Markers;
    
        /// Constructor for empty message.
        public ARMarkerArray()
        {
            Markers = System.Array.Empty<ARMarker>();
        }
        
        /// Explicit constructor.
        public ARMarkerArray(ARMarker[] Markers)
        {
            this.Markers = Markers;
        }
        
        /// Constructor with buffer.
        public ARMarkerArray(ref ReadBuffer b)
        {
            Markers = b.DeserializeArray<ARMarker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new ARMarker(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ARMarkerArray(ref b);
        
        public ARMarkerArray RosDeserialize(ref ReadBuffer b) => new ARMarkerArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Markers);
        }
        
        public void RosValidate()
        {
            if (Markers is null) throw new System.NullReferenceException(nameof(Markers));
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) throw new System.NullReferenceException($"{nameof(Markers)}[{i}]");
                Markers[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Markers);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/ARMarkerArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "bf6cdc9e7f0d5ddddaedaa788a1fcb50";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VUW/bNhB+nn7FAXloMjja1hQDFqAPRtJteSiapNmALigEWjpLXCVSJSnbyq/fd6Rl" +
                "x0Ww7mGNYVgUyfvu7rvvzvPbt8p9Ynf/kbq48Fn2+n/+ZG/f/3ZOeqUfis7X/of51me2GAPT3YfrN8X8" +
                "9o+Ld/Safny0d3N78e7yDTZ/yrLfWVXsqImPdCeMPWc+OG1qKm3F38nniO54E+I76SXd3M5Im8A1bJWn" +
                "7W0czN1Q2jyr2XYc3Jji+pPLYN3Z/auPAHAGXAjgRVxSrzfcUm+9DtoaP6O1Dg09SMx5tmytCj+/uv8F" +
                "lqpjpwp4hS+vyxjURdyk3Sa4xmozI2fXWP9t3ZexXFvPExaccoSJm3ZJoZnOAEmOWyUxUbDxZOlwJAfy" +
                "khjL6QNCh3tPQz+jv2hp3Vq5Ks+yhbUtNcoXgNFq0fLkDv6u4MoNPItIRogNa0tLzW0FIMSi5Ned1nrF" +
                "hrx+4Fxslqr1yWikNTsm9kEjY65wXQpwVlGlgtrxtpVeIQhgrui66D6pJOJKOl33JEkSbpE4WHERbJGY" +
                "meiaTiZ2HnGaMvkXbr5NI/hQpfCTqLMjeh+UqeCSkJsSZiQGanTdgNyWVxCeD6rrOfEWpe9zGN412hO+" +
                "NUOiqm1HqUclqZa26wajSyWdojs+sIcl+FTUKxd0ObTK4b51lTZyPepH0PH1/HlgUzJdXZ7jjvFcDsIm" +
                "PGlTOlaxnleXlA1g8OylGGRHd2t7OrXdzjnYV0GC5U3v2Eucyp/Dx/cpuRzYIIfhBeo6jnsFXv0JFCAh" +
                "cG/Lho4R+fUYGpv0vVIuqlaASzAA1Bdi9OLkEbKJ0EYZO8EnxL2P/wJrdriS02mDmrWSvR9qEIiLvbMr" +
                "XeHqYkxSazWbQK1eOOXGTKySy+zo19ijQcoXK4Kn8t6WOnaJjJZptMVqFLr6Vmp8cgRO0nIspWLpDUWr" +
                "eCbKWTpGJr0q0e+4ehXLag1E0bFCxtDfzhKGlXYwxYTKgYp5AHFjPOhAlWVPxgZgdOoTIBkci7Xqe4BB" +
                "6E4Zv59uMDnmvM4xfBsMnHhLOIqKjj2A0ep0ratkCUfdzljRNjlMpuVLcNy2KebkDAUDiLMhGpzEQTba" +
                "gdaSEBZu23qWFryLK0okWDuL0zBBfDmi0AigxXtVyxTzAU2P0TJNvs1uNe5WD89SapmOCHe+L1WiCv8v" +
                "cTge1HkmA0W2q+15/BcktABZpydbqCHlO13IbgYI2pmIu7/3PFqOoUxKRtsHBfpjY+7iRy4qDf/DdL9S" +
                "nmcJf0/dU914wOdh8PL2ec+7NMFXBTet1ln2D/U6K84SCgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

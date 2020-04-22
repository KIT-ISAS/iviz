
namespace Iviz.Msgs.visualization_msgs
{
    public sealed class MarkerArray : IMessage
    {
        public Marker[] markers;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/MarkerArray";
    
        public IMessage Create() => new MarkerArray();
    
        public int GetLength()
        {
            int size = 4;
            for (int i = 0; i < markers.Length; i++)
            {
                size += markers[i].GetLength();
            }
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public MarkerArray()
        {
            markers = new Marker[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeArray(out markers, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeArray(markers, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d155b9ce5188fbaf89745847fd5882d7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1X224TSRB9ZqT8Q0kR2mTljHNhWTaSH0JsEku5rW1gEUJWe6ZtN5mZHrp7YszX76nu" +
                "8TiGBPZhIbHivtW96lTlUphbad5/oNwvbLQVdf7nn63ocnh2THfKViJTX4RTuhjndmbbl17mVrRNQylp" +
                "7lx53G4vFovYaBtrM2sv1K1qmzv1pd1VtszEcrQs5YqORJF+n2hUOW2UyFYU9unRydPD/ZfCqgTfw7kA" +
                "N5pqQ7k2klSBZe71I3wqq4oZubmylEtrxUzSQrk5MesoqlThXtDJYHD9trNf705fv+x1DurN8Oa8N+h1" +
                "Dld37y76V93eoHNUH2DbGw9Hg/5N59n9o4v+cNT54x7HcPJ8g204+7M+u7nuX42GnRf1dtT7ZzR+0++9" +
                "Hb86Oe1fnXX+qi8ue8Pz8aA3vH49OIWiK7Whw8nV2UXN9OCgMa7bbUy7vO72X71rtt3eRW+0Ni5sTy4u" +
                "YF10LkWK6MzD1yM/26t79r5TuWxPjcg3YhBZZzgChX2MSWB0BTpbikSS04Qk4QUHTU8+ysSBYxzHCKZM" +
                "saREFx+rIvEx9tFUKZMlRgonSVBVqE+VpIJ18arNZc0ogqVHh/z+yZPvaFNL7XdZ5LTKHhTKXItGbZ+A" +
                "olBllcFwWMyJncpM+s1aA8K1NLUeDpXwuCbbxJVCerqpvAg6PE61TyJN27lO1XQJNWrqFh3QTipLIxNo" +
                "kO626DDoh+q59+hofZhl9amNZlLn0pllKPkbbSWV/Odb4f4OKt/z+SbxGxxpc0Q2EdkmAyCIP9ugpoMW" +
                "flG8AimUyqmoMkc7FcNQtoRJ4IwEtJ8qYeQusi0NUk51ps3g7OUJ4obVV3L8Lb3fj/f3DuL9D1FamYAY" +
                "mZoiYEibBx17rheU6c1w2rmushRRtY4mElkg8eXjD9ziGki8osGpaUyITrCFn94hEyZaZ+TrZpzp5BYZ" +
                "/qDs/rRGsQCbtdiJDKR7gbRFKpYxGXgbIrgIfcGgNpSDRF+cLHTpq9U6WUbR9nUB/UJpBcf7rLSlTNRU" +
                "4XQuLFlEkN+syimnHTmLa8xq3YPBFqFQd7/JF+iA9lTyt/05EreLKp/AMUgeH3F4qkJIJCoVp/DTPoGR" +
                "RJ5kjBW+eBuKlWJX16PeMRK/nAuCrwvtaCmdV/WB1IJFQRScSIFUN5Z5teVn1/TkGgj57LH3G9D+NSHi" +
                "NR8baXVlEhmyxh+BeixhSJpKKMjgws3y540AjR9Ci/CN3wHshEm5GEUqnPDWzNUMrt/LkHAZqERewkx/" +
                "y+G2MQhHnNH4zGQhja8T7wvGcp3ngHGGqjpX79GDEoAsqBQG9QXANXivTaoKfu7TnLnjYxFwWQCe+91j" +
                "RnArk8opKLQEB+4XfjwAzvseCHAFQbQ9Wug9bOUM2dEIR8oINCJL8jMw1LKewh5Dxu/BuBi84R0JKaml" +
                "HX82xtbuEoRABVnqZE470Pxm6eYAG87BO4FwTQB6YMxIAa6/MdFvu/c4s9rH6DWFXrEPHNcy/gvbouHL" +
                "Nu3NEbOMrbfVDA7Ew9LoO4UsosnSM0kyJQs0LDUxwiwjj4teZLT9ykOJByQfEXwLa3WiuLX49rhK24Bs" +
                "Kv15Cfltb+KcPAEKcpxgQT0McpkDT+CoqcGk6tt2ixONj9P6Xvm33LsxdK5oY4o8hDUPor8rLrTC812/" +
                "+3U2QpmtVf0gI5xQ6Cgcs8YEmIMC8VpvWBxNMy3c82f0uVktm9WXX2XB2n+NGU24wjiy9uqm/rz7tPY+" +
                "97g4+oFRq9XiV5lXzzgP2kZ3/nLTKmQYWryHF98SeERwjIMNJQhThdkt5OMIyOqHDXR8R6mWvlmBRy5u" +
                "wVKi1plalCWYAXB5HOC5lOGBhwE003gWt2gxl0V45UcW1sJjsUrIqBmP1atBoiEWVFvXIjc9RK3zmMg6" +
                "B2FIQjAxOsRuN+bBZakrWrBBWJi6BWjuySu9PFQ5rVu+4wcWD6R88y8cst2h+/wo8D+/BzazwFaQig5i" +
                "mtWsWU2alUAK/gui68qqrw8AAA==";
                
    }
}

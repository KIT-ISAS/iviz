namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Accel : IMessage
    {
        // This expresses acceleration in free space broken into its linear and angular parts.
        public Vector3 linear;
        public Vector3 angular;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            linear.Deserialize(ref ptr, end);
            angular.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            linear.Serialize(ref ptr, end);
            angular.Serialize(ref ptr, end);
        }
    
        public int GetLength() => 48;
    
        public IMessage Create() => new Accel();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Accel";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAE61RQUrEQBC8zysKvCiECCoeBM+yB0FQvEpv0pkddjITenrdja+3kyyRvRsY6GSqqqsq" +
                "V/jYhQI+DcKlcAE1DUcW0pATQkInzCgDNYyt5D1PHzUjaEEMiUlAqbXjD9HmgURL7T650Sz3OEP+3s84" +
                "557/+XGv7y9P8Jx7Vhm/+uLL7Xmru1oyCk8ZOZlzwvd8dxmwhkE3CsPmFEf0TElhYVemEdsgRrVyalNl" +
                "4S4LV1YH2mztpaym0dPeJDkVntg0DCZGUKFU4lLs3CCuufZ1hePOWp1RIXkDmoLnxBIaSPChXZi2qF/J" +
                "hHO4Ctrd4RhiXDwvy3THJiJZZ8JNjU2HMR9wnALZIGhJaRLa8uqLtnHymyscJuOzxGWhb9n+vdVSCnm2" +
                "7ooytbVzXcykjw84rdO4Tj/uF/F1eHFjAgAA";
                
    }
}

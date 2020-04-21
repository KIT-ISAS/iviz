
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Twist : IMessage 
    {
        // This expresses velocity in free space broken into its linear and angular parts.
        public Vector3 linear;
        public Vector3 angular;

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Twist";

        public IMessage Create() => new Twist();

        public int GetLength() => 48;

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

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "9f195f881246fdfa2798d1d3eebca84a";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAAE61RQUrEQBC8zysKvCiECCoeBM+yB0FQvEpv0skOO5kJPb3uxtfbkyyRvRsY6GSqqqsq" +
            "V/jY+Qw+jcI5c8Y3h9R4neAjOmFGHqlhbCXtOdpHTfCaEXxkElBs7fSHYPNIorl2n9xoknucIX/vZ5xz" +
            "z//8uNf3lyf0nAZWmb6G3Ofb81Z3teQTLvk4mnOyiOXuMmANg24Uhk0xTBiYosLCrkwjtl6M6lOsTZWF" +
            "uyRcWR1okzUXk5rGQHuT5Ji5sGkcTYygQjEHKlzMDeKa676ucNxZqzPKx96AptBzZPENxPe+XZi2aFjJ" +
            "hHO4Ctrd4ehDWDwvy3THJiJJZ8JNjU2HKR1wLIFsELSkVIS2vPqibSh+U4VDMT5LXBb6luzfWy05U8/W" +
            "XVamtnauC4n08QGndZrW6cf9ArHUTVJfAgAA";

    }
}

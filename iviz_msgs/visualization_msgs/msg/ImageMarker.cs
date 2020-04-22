
namespace Iviz.Msgs.visualization_msgs
{
    public sealed class ImageMarker : IMessage
    {
        public const byte CIRCLE = 0;
        public const byte LINE_STRIP = 1;
        public const byte LINE_LIST = 2;
        public const byte POLYGON = 3;
        public const byte POINTS = 4;
        
        public const byte ADD = 0;
        public const byte REMOVE = 1;
        
        public std_msgs.Header header;
        public string ns; // namespace, used with id to form a unique id
        public int id; // unique id within the namespace
        public int type; // CIRCLE/LINE_STRIP/etc.
        public int action; // ADD/REMOVE
        public geometry_msgs.Point position; // 2D, in pixel-coords
        public float scale; // the diameter for a circle, etc.
        public std_msgs.ColorRGBA outline_color;
        public byte filled; // whether to fill in the shape with color
        public std_msgs.ColorRGBA fill_color; // color [0.0-1.0]
        public duration lifetime; // How long the object should last before being automatically deleted.  0 means forever
        
        
        public geometry_msgs.Point[] points; // used for LINE_STRIP/LINE_LIST/POINTS/etc., 2D in pixel coords
        public std_msgs.ColorRGBA[] outline_colors; // a color for each line, point, etc.
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/ImageMarker";
    
        public IMessage Create() => new ImageMarker();
    
        public int GetLength()
        {
            int size = 93;
            size += header.GetLength();
            size += ns.Length;
            size += 24 * points.Length;
            size += 16 * outline_colors.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public ImageMarker()
        {
            header = new std_msgs.Header();
            ns = "";
            points = System.Array.Empty<geometry_msgs.Point>();
            outline_colors = System.Array.Empty<std_msgs.ColorRGBA>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out ns, ref ptr, end);
            BuiltIns.Deserialize(out id, ref ptr, end);
            BuiltIns.Deserialize(out type, ref ptr, end);
            BuiltIns.Deserialize(out action, ref ptr, end);
            position.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out scale, ref ptr, end);
            outline_color.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out filled, ref ptr, end);
            fill_color.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out lifetime, ref ptr, end);
            BuiltIns.DeserializeStructArray(out points, ref ptr, end, 0);
            BuiltIns.DeserializeStructArray(out outline_colors, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(ns, ref ptr, end);
            BuiltIns.Serialize(id, ref ptr, end);
            BuiltIns.Serialize(type, ref ptr, end);
            BuiltIns.Serialize(action, ref ptr, end);
            position.Serialize(ref ptr, end);
            BuiltIns.Serialize(scale, ref ptr, end);
            outline_color.Serialize(ref ptr, end);
            BuiltIns.Serialize(filled, ref ptr, end);
            fill_color.Serialize(ref ptr, end);
            BuiltIns.Serialize(lifetime, ref ptr, end);
            BuiltIns.SerializeStructArray(points, ref ptr, end, 0);
            BuiltIns.SerializeStructArray(outline_colors, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "1de93c67ec8858b831025a08fbf1b35c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACrVU22obMRB9zkL+YcAPaUp8SVJKCfghjdPEkBuxKZQSjKwd76popa2kTeJ+fY+0ziYh" +
                "fehDawy67MyZmTNH0ygTPtHJ9Pbk4nQ8ypp0vJhenS5m89vpzXj/5dXFdDYfH2xubq4vvp1dX40Pu/P0" +
                "aj4bf8g25+PJpAO8Pb28/noKsOycRc6OyrRkPjhlCjJ+a6tHRlTsayF5jxrPOT2oUJLKKVhaWVeRoMao" +
                "nw3jLgPq4UH82P0A0H1OrspQKPkZdeMT1jW/8GkrHz5XPOQgBxtbIYOyprNFRcO2kqxgW3Fw60XlCz+8" +
                "sbCn2nqV7Ht0MNkjxK/VI+u+tNblPltpKyKol0LzVsSL6eUK+QUwghJRoVROahCQkvAhb/FPrLbu9uzz" +
                "MdkmaGV4IePNhtyV0przyOBDycB0iTFcxhRiDF8K1JzobP3+ABztW1Sk367fR4NRf38wusvyxolUmVYr" +
                "Dqp6IrBH5/aBtEULYxy7/MEyIJxtdE5a+EBLRl2MJbZZNMFWAAIBek05axSeD4hGVLEwPlLA91BF9id6" +
                "v9+BYKweUZM8ImEv2tYJdNgKMfVxD53oGkGbRrytHtiviI0xxIaFGIaFLFG7QWNSDm1/trPxP/5tZ5ez" +
                "syPqEmwfy3bWo1kQJhcuB1NB5CKIlFepCrS7r8GahpeoavCSvkaV+wEc56XyhH/Bhl3iPZEHhUhbVXgx" +
                "UgSm2NNX/vAEbYJq4dCvRgvXsqdMNF85iDai4+8Zb85IpunkCDbGs2yCQkJrIEjHwsfWTyeU1Br1zz+z" +
                "3vzB9nHkIqr1KThEJEJMlh9rxz7mKfwRYrxvixsAG+wwouSe3qW7BY5+lxAEKXBt0ah3yPxmHUroNary" +
                "XjgllhpzwVNUHlB3otPO7gvkmPYRZoWxT/At4nOMv4E1HW6sqV+iZ1BNQb4pQCAMa2fvVQ7T5TqBSK0Y" +
                "k0OrpRNunaWnlUJmvS+RYxjBK3UEq/DeSoUGtAPuaXqmbiwwFP+bIIu3zzGKMmkLbAWh8HpjPd0ItKso" +
                "njQWQdvKMepKQzhNwY8f6LHbrbvdr/9Xwds3v90NZNftim637HYCSf0Geg+ejiUHAAA=";
                
    }
}

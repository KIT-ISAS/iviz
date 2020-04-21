
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class InertiaStamped : IMessage 
    {
        public std_msgs.Header header;
        public Inertia inertia;

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/InertiaStamped";

        public IMessage Create() => new InertiaStamped();

        public int GetLength()
        {
            int size = 80;
            size += header.GetLength();
            return size;
        }

        /// <summary> Constructor for empty message. </summary>
        public InertiaStamped()
        {
            header = new std_msgs.Header();
            inertia = new Inertia();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            inertia.Deserialize(ref ptr, end);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            inertia.Serialize(ref ptr, end);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ddee48caeab5a966c5e8d166654a9ac7";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAAE71UTWvcMBC961cM5JCk7LqQlB4CObW02UMgkNBLScOsPbZFLMmV5N310h/fJ3m/cuuh" +
            "rbGxPZr35nvuhCvx1OaXWljxUTPp6a3U7V++1P3j1xsKsXoxoQnv7yazZ/QY2VbsKzISueLIVDt4pZtW" +
            "/LyTlXQAsemlonwax15CAeBTqwPhbgQuc9eNNAQoRUelM2awuuQoFLWRN3ggtSWmnhFnOXTsoe98pW1S" +
            "rz0bSey4g/wcxJZCi8830LFByiFqODSCofTCQdsGh6QGbeP1VQKos6e1m+NXGuT2YJxiyzE5K5veS0h+" +
            "criBjXdTcAW4kRyBlSrQRZa94DdcEozABeld2dIFPH8YY+ssCIVW7DUvO0nEJTIA1vMEOr88YbaZ2rJ1" +
            "e/qJ8WjjT2jtgTfFNG9Rsy5FH4YGCYRi791KV1Bdjpmk7LTYSJ1eevajSqjJpDr7knIMJaByRfDmEFyp" +
            "UYCK1jq2KkSf2HM1XnT1r7qxEYeu8+PUkrsRQIz38Ie+vzbPqu4cx48fyCiIPyEiVNXVZLKCeVZvGb5J" +
            "GZ2/Tg2Y9Pcz9SQ2oKdBODc/rp5xkq5fpDcbPOimzZZ+JX26zVJIxvRM0r3uNkv0Nkn3boHh5Hs8+d4e" +
            "v8cT+Xgi327/T153WdmPrJc0Akglyk6rfJYmsvaCDum5lCIN3yKPi7MYNiOMTsJcH5AAVtoDqp0twCpe" +
            "sDRkRjpS5SSQdREchl9BieRLQnPfgwwLxLMNHSdsEgNyIUVTzGjdip20Uu/lTZF3iy7J60ZXExKGzAHM" +
            "tAtuRrG+Qu923eTzZAyDABLvYgZcFrSoaXQDrVNA+PC7leZoKQe/8uhF52Zpn+0o3ib0wWHBIC0hcIMp" +
            "tSFimRbqUNljTxwrv1W/AR0LqHLwBQAA";

    }
}

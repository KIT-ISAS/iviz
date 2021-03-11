/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectRecognitionActionResult")]
    public sealed class ObjectRecognitionActionResult : IDeserializable<ObjectRecognitionActionResult>, IActionResult<ObjectRecognitionResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ObjectRecognitionResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectRecognitionActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ObjectRecognitionResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectRecognitionActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ObjectRecognitionResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectRecognitionActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ObjectRecognitionResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionActionResult(ref b);
        }
        
        ObjectRecognitionActionResult IDeserializable<ObjectRecognitionActionResult>.RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1ef766aeca50bc1bb70773fc73d4471d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVabVMbRxL+vlX8hyn7gyAl5Ng4nMOV6wqDHJPCQIDcXc6Voka7I2nDakee2bUkX91/" +
                "v6e7Z1YrEAlxAqFsI+3O9PR7P93jd0Znxqkx/0p0WuW2LPLB1cSP/LPvrC4uKl3VXnn+lZwOfjFpdW5S" +
                "OypzWntufF1UyvGvjeT1n/yzkby/+G4Pp2fC0TvmcyN5qsBXmWmXqYmpdKYrrYYWcuSjsXHbhflkCuJ5" +
                "MjWZ4rfVYmp8Dxsvx7lX+DMypXG6KBaq9lhUWZXayaQu81RXRlX5xKzsx868VFpNtavytC60w3rrsryk" +
                "5UOnJ4ao4483H2tTpkYdHe5hTelNWlc5GFqAQuqM9nk5wkuV1HlZ7bygDcnTy5ndxlczgjWaw1U11hUx" +
                "a+ZTqJj41H4PZ3wlwvVAG9qBOcrMq01+doWvfkvhELBgpjYdq01wfraoxrYEQaM+aZfrQWGIcAoNgGqH" +
                "NnW2WpSJ7T1V6tJG8kJxecZ9yJYNXZJpewybFSS9r0dQIBZOnf2UZ1g6WDCRtMhNWSm4oNNukdAuOTJ5" +
                "+pZ0jEXYxRbBb+29TXMYIFOzvBonvnJEna1xlWfJgznknYGykdBnGHeEX8QC2fhVDB/5ctY/OTw6+U7F" +
                "n9fqa/xLnml4mxprrxamIp8cGFJRKrYPOpLDYXb3CTErNPcPLo/+2Vctms9XaZJRauegXPjhwJCa7kX4" +
                "7Lzff3922T9sCL9YJexMauDd8ExYHR5CTxAAvlJ6WMGZ84qkd2QjM+dQKEfJktHbP0/xF37CWhCfQ2BO" +
                "C0MU8spHKmB089K4CQKwoGxQma3A8sWPBwf9/mGL5Z1VlmegrNNxjiyRwRVT0sKwplSwThF3HbP/5vR8" +
                "qRc65uWaYwaWRc9q9swl72tPymrzm6ohr/AWkTDUeVE7cxd75/3v+wct/l6rb26z5wzl8zs8gGPK1tVN" +
                "d+n+No8Dk2qkVabZHFYjVVYanFKSQLLOy0+6yLO7BAie10TKa7X7CJ7XuF5pKw7CpfM1xms0fLB/fLyM" +
                "5Nfqb/dlcGBQrcxaDu+jXdjktrVWmS6HuZtQXaMK0piBUzNxYrIVIdpu8upPEOJ+aianWAk/OYAqxx0+" +
                "cXx6cdkm9Vp9ywT3y6iMUEBASWWwGhExogTdqICo9AQIeDh4kbHeBveIPU+0LWmbVDrLIT4iB2etps7k" +
                "6X5R2BlDElqIUMAHu6xXYCbUKoox1QJatCUzg3o0IjWGRZWZV8mjVrOjQwJZ5AQCRIKefEUWJ5G4MkOr" +
                "s3EOhMFVuZVV2EFMRojoiAEMY6w1qsJ+U5ILQVDjSUcAOmYyhbmKAruJphf7zQyObkhH74NXGkdZhTlq" +
                "A4bAPxJMABnIxmBvsWqIoTHZQKfX5JDYIUAWoNJ7PTJiHT81aT7M0xgPzIHvBeqE+GQBmJrUHBdIdTlW" +
                "9aL9sOrhrGcZk1+5JSgXM96B1TeS5K4dYe1nk8nefeegLtc8vZKN/tFFWcsYHPRLftS7/v5h/1x90Wb5" +
                "SVa7pthURKehVgMx7FOXD9jpprZC3sjhPD5F1yHZeVQ7TRIi/BALyCrDIH5wd8pggP0FuzbFIW29t+k+" +
                "/BypJV+opov+/vnBuz+kphCBqd22qYBPgLmJRkzMESTVzBgRbelhDdPDwmr0RhADXVaz+fGD6KZe/0Kn" +
                "C9oUp5MeB8qBYvyU+zOkNVLmFA2bvO2qk9PL8AzV8yotbJ3FVvWmC/9+kU7fEMBURydvT9UfkurAlpTA" +
                "0ZWXSOnwDyoPAM4B5XA4EVwI0rF1KFyo5a/xXGxHMwGCxXgB1Eo6yE0BtUwwEqhCgcczRhdteCrdv0nH" +
                "nLLRW1peTCvp/YSQ6o31qNAWK7GMK4agIVobbcQsMyNmlcdfz9WXtI32gg7niIycfk+NASU8Qf2FrZUW" +
                "yJWHNCEUWT/ENvp1HOqULc2y+MZY+5qXPW+UCYcIwATLG6KANKYCcooxqJas/H4/iV5ycPzjxWX//OJL" +
                "PSUJ5uXRCOtB8mPlACeY6Z1M3DzoZWAKS8hJgkK01KURDtuEYsBHC1mXA2xRdjalt87jqE1RBqEVroFa" +
                "zhRfGutPSMdoHt1yz1YiH8ScZ8TIAYUbZbBW9PkVObqNIEBfiwhT4Nfw/RKTpRE+zJ/5sZ4ilnEegFIJ" +
                "OVtDrCDPMuYTXi1cvDd+3JC6wnnjNafzySzRC7TX5RRB523kq0OTPZ6RjdACcCXzYIXe/kJIZ4S6BQCG" +
                "+LW161KkEO/zEAWFxhIWHjgK0YT4w/DnWoZtbIbcYQX5nq4rS4FPc6NFMjLEo1u0lAk1NpKE876wsqmz" +
                "04v+n5C1ogmkgDFqZMcjv1gMbLag6s3lfG9pI9hLenfO3qTUUjAmrWDtC6wEfsRGCnWns1yXz6APw55J" +
                "fbSvdXFLSd78C4QOLI/hEKsXYXpJ5/5V4JMS2sYfqS2HD19ZbhUSsgDZqpph0GJgBqzA1FeLCeC78HpK" +
                "sYLxERuZGVKTqUucGPJ8iIBrs1hXBsQhZD96A8KGKFY0EJFOnd0iG8TuAVQiwWzQZoW7MRoJQ4LvL05P" +
                "ntGsIsyJf9p/fxx6zB61x6FOoBuKtQtI7JryGGa0zaSHq0KDKagDfcrD1oH2pqf6vVGPc+htq+OxQNbC" +
                "2muE+TXK1pP/dkjDnb3Oga3T8eGbTld1nLUVnoyrarr37FlhEfHQdtX53xMRkaobsUdZBWog2siyYr3Q" +
                "T5NxWlqgapZXHWzKAS+RWq6NCZPzYWHm+SAv8moho3+zzmEhsBEl8p0AGr3DN+IbDRBHFsxWYAU5l4xT" +
                "EeYItMLwXP4tGAzC0lfFZPZUowB+RirAs5sq2Pvm21cvZUVqAVO4Tca62xx3wkkXPxwDGwCQjC1682in" +
                "lYMvPhbv4gqhzUepzmzkd3blyRQjyj31zcudF/yVAA4tQMq2s7AC/fIMlx03HpcwAQkSD4j9obyd2Kwu" +
                "6D3YwgDNTjvRoeHaD3dVtL4Mb9xs0ohfMvlS1RSpJ9sZSmPpxesET3TDjGOiFyBCxYdaMp1lbAuaKLWS" +
                "C8ZZY/Klkp4U2Et3OqBXLVDBq5SzO6hIVYzXGO04HgCJOAyiCzugmPKooii3VdNPxlFtYAUO2OCYJ+Ka" +
                "TwSz9GLOkLME/POJEIRqsXUjLU0XKv9mPoFWtilwt6iOPyeUsVlDDLQHJtvqqbMlGd/aC6YJONBuHykD" +
                "yGR1yqwSm/ASpyHAlGYpApcaPWGU4wzwp1x6bdvh9rAgkCXc0zAHwskmYV6nH+tceoDuEsjeuImjwrFJ" +
                "yYLBkphx63bPo14cUoqs04rAddBiS109dSTPRHk0xooK6YIKg8GcUoFA6lmeQcIACQpTjvBtDdF47ScE" +
                "4jfeTDwdNoMDPhlXZqXBCCyImrvoEKFYBH9h3ZDT9BI21FtyBYCmkK2SgbUFDTBzfzXIkeYIVGCkeeRb" +
                "F2rNi39EpgJ0hZGmPAE9boTSQSRwMVhUxjc7nJ3F9Td34NXq+ldgkE9ezlj3YYsYefyuizugz1wTNiPp" +
                "r0RzW0EqiIQSiszcELl0VFdZ85TZ8Rf1JN42iDc8YgJiU9yRf8iCEtnTmIIIDov4iG1YVuyM7a1s1lCR" +
                "xCMu9Qqg9vJVuPwLT34Mj3Brt1zzfFcuxVpr6BHur5ZryJR0ZdRaQ49wBxOevD0+3adHuPRoP9l9STcI" +
                "Scz0VCGiVU7oMwRkl4z+YodDGn7xglP5PHR2IjcKjLxYFRKl4SD2C67P2HQYP5uypjwjicEb2H2AQX08" +
                "JwX84mOw5R38cKJLAPTCTDiBxraQOXs4z1htzcglDgk6tgYasUkrcmqM4MGE5UcAF38PHUJm5rhoL2hQ" +
                "4cxQmvc4E2E50H2gjTL+w88JHXIZCCDSGlp0QLg+oliLOwTtMCisCT8Z4WZ9MxY3PZq2oiDrtBYlAwZs" +
                "+BK7f9gRVs38Crp7WIbXKKqJ+oAa/JoZVpNHh7j4wFWCxpiFJy8IpXnzadF8+vx4EtzRUjZSxf+VwoMS" +
                "zMCAAuhmhjtd7mq1OCnPfW/VaK5o8X+6JDeK8+3DH7iR/S3ZG6GdIaE5bcg8ZNV2InhdhlspajxYGGEf" +
                "RM7tbHuifwE4aSgJdAxQYXe+C101Usu4PDYvuCSLy1sdEI0m0cPmc5Nt63mbR17Kg0DQJ+TSFQ9sdU+O" +
                "G4bNOcY6XfW5iypdtfvkfyuieOvxT+sf/4cfb0Vn/bCz+3NLmMe0Htlrf42Kb1usy1f8eJyF9xKc5J4t" +
                "ffeUACsVFyQ/1PBlVzLd5brHknF5+lrPXOHphofi28cl7wQh4KO/nnLipxnE+z/db1y+MygAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

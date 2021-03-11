/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupSequenceResult")]
    public sealed class MoveGroupSequenceResult : IDeserializable<MoveGroupSequenceResult>, IResult<MoveGroupSequenceActionResult>
    {
        // Response comprising information on all sections of the sequence
        [DataMember (Name = "response")] public MotionSequenceResponse Response { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupSequenceResult()
        {
            Response = new MotionSequenceResponse();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupSequenceResult(MotionSequenceResponse Response)
        {
            this.Response = Response;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupSequenceResult(ref Buffer b)
        {
            Response = new MotionSequenceResponse(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceResult(ref b);
        }
        
        MoveGroupSequenceResult IDeserializable<MoveGroupSequenceResult>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Response.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Response is null) throw new System.NullReferenceException(nameof(Response));
            Response.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Response.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "039cee462ada3f0feb5f4e2e12baefae";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbW/bRrb+TiD/YbAGru2trDR2mu3qwh8UW07U2pIrydmmQUBQ4kjimuKoJGVZXdz/" +
                "fp9zzgxJ0XLTxW60uMBNC8ckz5w5728z8bwbk0cmGepfVzqZ6IHOlibJtErtL94L7/zf/OeFdzN811IL" +
                "86Cj3F9ks+zlbiJeeAeqnSidpiZVExMSVdNYT/Ioman1PMjVWif4kZpkBj4edDfvEOwFQDNZ5tMyD3hG" +
                "c62mqzhWWR6kjAC/5FqZqcrxKTVjkytgpAcGcV8yS5Q3IJAhL3LvfIZ06PM0+DuIM2mE3XMij9Yv4yBJ" +
                "dKqWqQlXEx2qKZjRj3qyIpYF68it3Hz6bBeEfhWd2yJYmFUipEULrSJsYcw9fkA8i2WsQRvtuWBxMiZv" +
                "Gpsgf/Na0IJvn1buS6vbKnnhRUl+dqoegpgYwtc0gEbGeh48RCa1X4d3Fxed4fD8lX2+anev7wad87/S" +
                "H8++vL1u93rd3jufvnYuz08cdLf3oX3dvfRv+qNuv+cT3PnJqf1YeelbwPaoc+m//eh3eh+6g37vptMb" +
                "+Rfv2713nfOTM7vsot8bDfrXxV6v7fu7Xvvtdccf9f32T3fdQccfdnrD/sAH0vb5yXcWatS9wRb9u9H5" +
                "yRtH/aDTubnFzucnfyFJON2o/1L3UaIXQR5NMhg7rCzLxZAzJ51RezDy8XPUAQv+Rf/6ujsEU5DAtztA" +
                "PnT71/h76N+2R+8B3RuOBu1ubzQE/CsnzHf99nUd2Wn12+9hOasCVj65RaSb115NO+8G/btbv9e+gZRf" +
                "fVf/WMMEkDc1kEH/bd+yiK9/qX297vZ+dMi/r33rv/2hczFyX2FPByrbZLlebIv5agAAHwT0hlf9wY3v" +
                "jPDk1BlaISyYS+fiR7JF2MMHwJFRANBJsEIr/eRvTmjWYLq9q37xDcI6qJrBFl29vt/90R/2r+/IkmGi" +
                "r/bjymXwo6A8mkeZWugsC2YakSfJgyjJVJQgtBHRCD3B2KzySmTlYNtQUVM3JSqaLCLAjGJZlGfq7wYM" +
                "ZipIQhVHyX3mZTrJEL959x/oo4RehqO4m3NY5y822i6CjUIkQfhbxXmEaKgu+1cqSBGvl3oSTSME37lO" +
                "9RbqG4IFXGULXu6HZurXNmvneTCZA8vExHGUEZ9mTDE6U0eB+4ZYnBnEZuJCAaKQwbHn1l+45X1ejZjv" +
                "ViNd2U++xUz7XsXBDNINo0lgU58G1hSooYZsohNkAsjAIJICLNfpMkUmCFUAeaowmk7VOsrnnNKBsqDQ" +
                "MBJe73RKajVZHm9UtFiaNA+QbChfzaEXMDRjbgpWxyakVHfk6AFgAl1TNop1kO4CprA/BVViW63WxKS6" +
                "1aqk1rHGflqtlqHwihTHxOcVkzv2xsbEINYn5r6eA+w2wcIB8H9QeAFb4NzEYaZAeUBCQMabpBF0YmsK" +
                "qTYCoJUMblLEd3afFFqHiMQHmso7sOm+WKQhQ/msjlL9YOIVvaeiIso4TBwTNaGeImxA1JsWEKg/b3ma" +
                "q2ccliAkBIvjRgn6oGMzifLNU9CXGQO/zI7ZQ8slegp15cI9yWO5jMnNoqSKoLeg1b3jJjPWKXnBilUS" +
                "QQpkcCFqOXHSMZ7gU0mAQkUEMddBSLZq/ZgcXksJREXgPALGcj+WWobaEHaUalhYqMOmaqPSqMMAOyg1" +
                "8B+nRo4f4kluaVEVghzeU9iox8AsykCzGLeLP0GaBpuswTtw2UesLyvFZYUYUjvzOjMB2TZRsQjutSyy" +
                "8OCdLMwsSaNB3FR/m2sUyM1ZU23MKnVRlLlIDBBa/QRZBs1il9B5k140aImaBImCsxLXpTqZbqUXy3xj" +
                "rZGkJ9yIbiu8Z3OzihFZHQ6WUxb9hnAPliFIwVPxGoIyCXS+xi5gs7CBgsyKcMgKCqIh6BR6ySlrswab" +
                "nue9F+MQG/G8LE8ROBBU2X5s+Ut1tXWEyitn8JVXIoGvGFPyUAKKUE3BBFElCYM0hETzgIMHh9xohph6" +
                "EmsQScwullCehJbNkhgv5TlDBKdKeqNWmWQgdAMLSBX5Qkx2a71YfcCGGE1WMSL1xMDUo4TApynERthJ" +
                "xrbPUd3LFts4dy0gCA6aTFIdZBSiu5fKW0l5ggXewWhtTigNzShFuc2LEKEfkZ0yojPIKEz9WZhrAjdF" +
                "XOwCAz/idz4eEXGwCUjQSwM/OALlt5t8bhPrQ5BGwRieBsQTSABYD2nR4XEFM5HdgjUkxqEXjOUefwRt" +
                "UuAlnk6KjJitZhAgANHgPSCIcfhiU0UkhP3G0TgN0o3H0Yq39A6uSMbiRawRipzbHmqtWLThR+F+ktzT" +
                "Yojsc6BJY+BFSjuEIslLZKU24LB/FmEv1LNUIwIDcopfQoNYAzxTZDmzdlUEGFxN8hViNMDKDSW4diWb" +
                "ZNlqQQZNphO4nEGma0t2jgY0K4BOYfVwjTRIMqpCZc1M52VOAtogNnb3or5WkzmK16a64q4c2okRtwLu" +
                "yKBWm8BQSGHbu8HlFefZM6orjx4RQPF/sCaboKwI88HghD9SVKXIV7H1KnUiSPyVRsAiaynLbH0HVoFw" +
                "2GC76JjJQsbBhJv+LRr+P7XuN7WuU4TG+R9OrQ78/1JqfS6zSl9EyzNvptFL5OlGIsjImTCgCnN+ArRG" +
                "rUQA9Hft299YTPgo8vp6ce8ZuovqPnVRzxabRWQZ63ytYRr52jzJm6xCinkoVYMJgpn3gUd4Z7I+Fsf+" +
                "aYUFaUIxIDUSVffFpyVnF5cByiH6WGNBFeGY7WqhqSeEZRUrucckywEbTXKzlFs4NPu5Cg1Egp6QYxkc" +
                "jpIN19cUlGGUVbHQayw5IpdrUI+bCBQlDC5auMxBxE6jWRTWgymHf8tdQ+XTUxg2HItpls2gRSBxAj9u" +
                "qu6U3XRNDLGLu8aNWjZLF1cBmLE2qLSyKLYlesuu5DwWc5Ac3gLFu7nrY/FbUWaq3/ak7dLQdiocuRzD" +
                "ZZfXt9ROT7+WZkpy/hJPxW/rvTktxY+CM5dss7KP3WZpnJp7GBXURYaW0YSGphSUfoNkxnUwJRAEPue0" +
                "FqR8tnD7YlCC4S7dQSGipJK/BrwL9HMmIh6pzP9jXDKy8lEGE/uZKz4zEhOWtaq9Fp8eV6ZKnL3QMEWP" +
                "bn5CDsxJlAZwroym3zlfYZzGoiyGljyjQh2KwiCbB2isWFLoFfEbFbj5Uyq8A4kR1ZEfgfGGB4gqwMcO" +
                "RRojZhllYuywsIngRXM7naP0oxBlB30Hu/C5sZ+0FQUbhSBkg9CrC0qQumMjO3HiwWp5MlXs54aYNCoF" +
                "RWZtaTArJOAD2hiTpQDVvTopCBMyqEiP0QeGGxmBoEwQSu2CsmRgZL6MdimjTNUEtSxUYKniaQ2qbjoh" +
                "q3en0AY3CqIPykTCJGehBeEoWAFqm0AQ3TTEjn5estokRv9PqSTFERr5gsVy3HBjMt4j0RMK5emGd0s1" +
                "CjhaRm0flVayMakPON08uTips27L3Ux5pgdhEHW+3cIpZa3R4Rflak0ZyJ5TdZ+YdXmAJ/D7ccsd7ti2" +
                "ZSBnQjnFLGbNrqdjt6lXjcItjN5yamV4xAbEuKBAOSo8dv6Khteuc6rGzEPsgpL0OEAyNlZAhQPJ3z7N" +
                "7WYJj3qEGeFhRBgIjcNcDvFt0KW54o5q3rmtLIvYHAkSxlIfq3ONXjlrcAIYmhj8u20mNLZdgMAHCIuD" +
                "jtDJULfuE02sSrB6zZFtfSfTkkPiG53Nt7HSG8Au5MNOPPStRPGW/IOUQD0wjfcxBLIVQlZy11Bje8DD" +
                "YK5HlYICzK/E2aCxMGRdIMDSFsdV2m5pqT3yfpZJ+lZS1w6p168Yhki9OEHl4wyu7SpAsFEcMK8yVIH6" +
                "ETUDkY9SU1Iqx5ymN96giG9fXp5/S9sMOK5u7TRNDY0V0HQlDxGuHCyo9qWZNYLEBlJCb44JkrgCn0bl" +
                "8OesZhNReCw7DTo3/Q8dHHITT8slhSoqYZ0125mHja1MtG0Pv8SrK7llkeMTWiiZvL3t9C7PT20cLvfc" +
                "vR3v0kBgXFvLt6rm2v+IIJzeXBu7WOHUGhCxnubSotKIBAEtMzHJCqJ1EaMMqDgtgSRDIZFlc0YE9pcY" +
                "bboKHzjxSMWoAzTu89eLi18OKwiP//QfJQfRNDz95xfbPySfi98/feW4yYcSU057NpYhktGsilpY1AY8" +
                "qaPSEVrUKY1HZnKgV0wP5GQJpkLnd1ulxb0ujouqO7T4jawvj3a4t2SLQdBKVDh28R5YHMJwXCXFplke" +
                "n/0w7Pde0j0XO1P72L65VoIAxzuFFSPSFj5QaTopVjuplHNDSe0upzRVh2sHOhR6onV2JZ7p0JWbOLrX" +
                "LfWnfxyShA9bhxdU31y+PWyow9SYHG/meb5svXyJViSIIe388H/+JCwiccDeUQ/yQC+xwVG0Z2scUk5F" +
                "ClQ/RvkhFkWo+uEI91rbiTruQz1G4yhGu2Mz1C6DpWNWEaJroi/fim0wEuKKXN/uLKMwMq4V5ERRTgaj" +
                "PK+nQalllk8ZGU1LFQLgdyQCvKuLoPXdX79/LRCUfWVmALinFB/anYY/XeNUFVUCHa0WetraePhr/N5B" +
                "CG7eSh2uZ9nZG3lDZ9kt9d3rs1N+BHRKACiizdpCIPOvMcypvaYihRhxG7hjefm6wJWumL7znCA3y0Nn" +
                "0DDtrzexf65koDLtUjx1bDAazpZkbA012aDG5tINFqeVnTm6dgeW4Q6OYVlu1og6Z+wKASCjsE+JnZ1R" +
                "KuhvG/gPQwE6+vleve3/jGQmvw9v33dwV+bUPl58xIWcy84AAd2+6Pc656+La3M2RHGuIZoslNRqLirg" +
                "9AT9hb0wUoKWB3clhFtDoyoiv7qgAtaS8S/1LXzPQYQgCZvE9eiC1WG55lBSHF/KsM0hGGdSpY34uaE+" +
                "ykT/lyrNJGTunHQyQ8loKaqHIeqfCv4g9GYpW/9n1CXl08dC1vT0C+XyCkkif0sVT8BI7RQ58bc9dkcA" +
                "FToR1CgaC9846o9WRILtd8SCHB2C1x+0L7t3Q6qTKns6JTNOUrAcU4pUxHR4FMGDRFck8nmM3eoXFaDs" +
                "aKpygriF13/f6b57P1JHhNs+HJc8yb2SisRLnuZbfZbzBXVEvnAs+1Goc/sId3Yfeajs89wuNFl0shP1" +
                "2RZl957I2jIVcJ/oEmBR7dd9ks5+opR7YR6w4gRyWdoQy5TWU9dJ9r5aNuxJ1zdWqM5Ja8IsTKrGPFWl" +
                "pac+AS4FA8A9TcKoGZAuNH1yGklVaX0UxgqjIkG+yw0YEnhl4onRtgxvi5sBlcF8BW5/PIKYYti3NaGq" +
                "XuLBaMQdhJYcf2E0u49cRI1mkYEq1FJ3SZECzbZ4Ic7Wkxmqif+uhFpcCcZdV7rfTZcGTOV+INikU08U" +
                "Ptmnzx5tMrII+IjJ4qINKqM8t8K1YqgCV1Qw8UWf+ZNGE+Lk2yCyaG/ScozskprjDEVfQZdcsvh0JqTq" +
                "R5+Hg3simHv13dcB5LgcXidNfzkUKCYHwaP6hsaC36jJb/gRqnPFbXagWuewdD399O1nmjQWj6/ocVI8" +
                "ntJjWDyefS7OIj69/szvvp4MvjDde1Gbd+08JK2tcRbHjpz9x0gvAg5fySuBbYApb9thLkjtYOGUnxru" +
                "jAVf8RBMJhiUSiOefSZdmW1ouVX1uZioV/aS/Cb/IIJGFLY4LSYmNjJQSnQDCRoc0tU8nGpkteNsjhc1" +
                "Npvg3dtxESx7ehOMbqiWL7fYenpHLFy5yQQKAp/GQ/bfg+zvRnbVDL80hRZ7LKGeLNi6/VNZWL8PXUGx" +
                "N8t9hrYX2/dDbWHLN5zZgOxNrNopvb1xw/+cCLUm3QXj21k7rNPpeMdUe1cpYu9+nkBY7jSqxHUk1z/d" +
                "zdf6faBj54gCUd5VqqDgYx9cXotXIXFhb5NUapzsZWnJL7ft98DeUbY+w15k+xH7qsBW8SdpErYWies5" +
                "MJqZU0tZ90NGhspzZzx8Rp3/qbj4e+QU8bGuWRqaOCur3jw7okrDqDf0rw+O/9j1GM/Nhexola5i2CPs" +
                "MhA660RlOqMBVfWa0zMXbCpR7ckWMJ6Kefxr+2wb2u8Fxv8Fa/zW+nA4AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

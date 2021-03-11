/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = "visualization_msgs/InteractiveMarkerInit")]
    public sealed class InteractiveMarkerInit : IDeserializable<InteractiveMarkerInit>, IMessage
    {
        // Identifying string. Must be unique in the topic namespace
        // that this server works on.
        [DataMember (Name = "server_id")] public string ServerId { get; set; }
        // Sequence number.
        // The client will use this to detect if it has missed a subsequent
        // update.  Every update message will have the same sequence number as
        // an init message.  Clients will likely want to unsubscribe from the
        // init topic after a successful initialization to avoid receiving
        // duplicate data.
        [DataMember (Name = "seq_num")] public ulong SeqNum { get; set; }
        // All markers.
        [DataMember (Name = "markers")] public InteractiveMarker[] Markers { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerInit()
        {
            ServerId = string.Empty;
            Markers = System.Array.Empty<InteractiveMarker>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public InteractiveMarkerInit(string ServerId, ulong SeqNum, InteractiveMarker[] Markers)
        {
            this.ServerId = ServerId;
            this.SeqNum = SeqNum;
            this.Markers = Markers;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public InteractiveMarkerInit(ref Buffer b)
        {
            ServerId = b.DeserializeString();
            SeqNum = b.Deserialize<ulong>();
            Markers = b.DeserializeArray<InteractiveMarker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new InteractiveMarker(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerInit(ref b);
        }
        
        InteractiveMarkerInit IDeserializable<InteractiveMarkerInit>.RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerInit(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ServerId);
            b.Serialize(SeqNum);
            b.SerializeArray(Markers, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (ServerId is null) throw new System.NullReferenceException(nameof(ServerId));
            if (Markers is null) throw new System.NullReferenceException(nameof(Markers));
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) throw new System.NullReferenceException($"{nameof(Markers)}[{i}]");
                Markers[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.UTF8.GetByteCount(ServerId);
                foreach (var i in Markers)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerInit";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d5f2c5045a72456d228676ab91048734";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVabW/bOBL+fAb6H4gWiya3jvPW7e36Lh/S2GmNTZNc4u4LFguDlmibW1nUUlIc99ff" +
                "M8MXS3HaLg7XXls0EkUOh/P6zDDPxChVeaVna53PRVlZ/OiJt3VZiakSda7/rJXQuagWSlSm0InI5VKV" +
                "hUxU5xlGZYX/dClKZe+UFStj35fC5L2OI+XHJzrtYPqtArU8USKvl1Nlexgag26SabAgVjrLRF0qR7Ay" +
                "IlWVSiqhZ0JXYiFLsdRlqVIhRVlPS6ZVgURdpLJSPSGG2GntXwWYLOVcOaoLeUdklSjBPHhqsSFkCSoy" +
                "xzGxj18HcmfMVukoZPq9ytZiJcEoWKtzYiGxGkKaWbMk4iDCFJyY5Kwi0mA1SUByVmf8VctMf5CVNhCp" +
                "EfLO6FRYlSh9B2mBQloXmU7oADiF7HVqnVcvXxDLE3BLQjwFN0tp3ytb9jqjHLvIpNJ36i2P/fZ7+Nh5" +
                "0jn5H/950nl7+7ov7nRZx2NMluW83N/i4wnpVi/V/sySyHU+M6Tu0UwslEyh+wofBRsOC/Sgy/pxvDuR" +
                "Q7RWVVbm5czYJfQOURgSMnTiqEKISiYLQbTKShU98aup3WInU6f0wsCoZkqlU5m8Zy1tbIEJEWdXGLIr" +
                "XaquWIPIUs8XVaQSFtNSKVI9mylLJhtXnxsr7J3+QKfAoQL/tE1SW577fKbvcQhe8pyPPV3zBJg8XOEN" +
                "i8VLh/Q8ctbC3Peg9dJ04REznavSnUrfmQpfIRUBAQlrKlYIrGKuzBKSWzvdXNPxiQpT/YS3zzMzlRms" +
                "PLo9u3hw/I2zB99i9ZFDLKyp54vo9BQi2N8XxlZgmhylYJvf+Zd4cSCShSRjgZHuxjWNWbw0kRltzFGI" +
                "nJ6OiOPLOqtEYvLKmqwUO37k5BCEZpmR1fGRKGlp9BSV1/DtlEIGP4NfqyFCWZYm0XC0FNqqFv5gbH69" +
                "zlvMHGLimtwJzxO/iqheaEjLzDZMpLosMrn2PLbobLnFmVsEsmH5l3PTskqdATjTIo+8rSAKaVMcqpIU" +
                "YJjlBWxd2b1M3akMq+SywFn4a7UuVOnCNE6Ff3OV40BsI6QU6CcxyyXshUOW88PGeudsUhTSVjqpM2kx" +
                "39hU5zQ9eg/+xZg8GvRJNqVKahIbdtJ5YpUsyUhGA8ERkbSs/uw8G6/MHl7VHJ4TN3eGCmbVfWGVyxhl" +
                "H3v83R2uB9qQjsIuKUyIxyZ4LXcFNgELqjAIKjvg/HpdLShSwwfupNVyCpsEYRhYBqrPadHz3QZlYrsP" +
                "889NIO8obvb4K2TzSJfOtLeAzjL213oOAWJiYc2dTjHVRxCfQDM9tdKuOxxaecvOs3MOlGyUrBG9ZfnB" +
                "AVkblKi/mEFuhyWyyVNEWdITTuDyInyLIzYENbMKJyGw0SVDo+HUf9c8lzzbwDP92p7oXHNADBM6/65x" +
                "UJsz3c28r3dGMMOpkBRALi917sN3OAKOAwdhrlsndvEMuf8+Pq3j04evdYKN/OIxorpgSi2ptvmntz83" +
                "0qcc3ut85lDhafVVwUuM9nTE+BKRIAX9IcGMrWgeEyGBU8hCWivXpNAtIhxET2EB8PAkqD3MonRUmbki" +
                "BOLzILKexArKPfs+c9HP8KgqpGNImuAGZV6yqDW2J7c3lqESjGqWScLHzBXwLEFtWkR5v06qGrOb2nTh" +
                "ZM5A1MGqmC/XDh8j+gbEDLeT4inCOj4jZjwVM62ylDYxhU8kMdFiH4eODUEXTrZxoTgRB1hFkmgtQI4A" +
                "wnIBysE1bUHBUY7gbQVpx5OHJR7deWejwwQBXPOuIBN2WhLwkUWhEFanChbKnGokqYXOUkx2ur9HJM0U" +
                "ZZA9wSwf41G0D8Ejla4QyU/E01mdP91MP/rcdGumpmoseLG14Ki9oLBHjenffW46dI0nrMDwa1gvZOyU" +
                "y+ZApQ1nCDohaIJ598BsMSW8YMvw6MgxmhxwQglmQWDigeWQctvF5EalIOBRUNdBNNhyloocSiZ9wDJC" +
                "snflI3aD13Dw5LO6N8ajYbuurxaZK+8tYGHkJnqPclPdnKplr1hPwHwjSqpMIhdxuBMI7XtSLOmQSN0L" +
                "+bud10uOkx4n0SF1njJYYm/zgxPCWWJnqjKz2g1k/DcidNacxh7ucgi/Qwh4LwgyUfDQ7DYLlW9E4y2e" +
                "TwyokLxXKdcsw+Hg1enZj4SFyJ/z7QB3HuqeTTUN742IeE0i8gUc78aDz4EUeYObq9ubd5d94DACc4rs" +
                "vLTwDEdFRukg5oDd6LdOThxQxI6cmju166ldnL67PHvTJpjJOk8W/xVNUuv3UQwnB37AsX1yuHl1+54c" +
                "+ZGm1v6/VbYvJyhv3TSycnCriIRdHbUpVGK2cVIjQUXKvnh5vFjclDh+C9IMFd05LIU7GmWp55R8vLff" +
                "yQz/B/vAz62SOrRPxOt3I04SsF5wTczApmOFLAplCUNQd6nFgdhRPRSxUkzrqsI3tvDdB7UoCA8atXNm" +
                "gLW3ShGxYxVSJrcNzKZz0Io5XNE8lNUuGdpqoRH5wNdU0b5cjlPpAbvjJgYoO78buJq178VbrR/U6x+B" +
                "rHSGqwbYWppU9TdV6MKsWlgMNXbuccfo8s3wZjTuo0uRZQ+m4XCP6B7BYfTLEJXYj0oVrfmuiUFW5dsT" +
                "qDMqiuQ/jYY/T85Pz0aXsN3TjIxgvfdBwOByHzaAaxR8cue+T1a0QhmKVks/U7OqKz706yJ6pOeX8qQI" +
                "TkrsYCA4ZWM3Tnd+uMHphOTj+ij+eF5mWybsCtDLq8thf4PTybDwaHIUoEtFXT9a1vLJf4qcwjrI31Pb" +
                "EImG4OPw8l0fPQKkVKLYJasUlppJe2yYLm9Q6mGB05JX78bjK4TJYaYodqEMzMlbSTBujTObt1c/DSen" +
                "v4xuwWWwJyEzA1tz5ny/J+91GadeX5zyieJc2KibGPXiwur4dBwI37DRIoyaGmb7KFk3v085acr+tNmM" +
                "bb1BL+iTBNFUJsmooUt3flaj/x5OSlArLtpsQwApxOa4GeOgxlT3CaMvwfnT48HTjZ17Qyi5Wx26P7B9" +
                "gzjz7e2b0fn427PxzQWsyX08HlATrzR2I4XjQVOyVPRwo4KmcvHTkCxN9XLdnieQi2AgtD1sd44vVN06" +
                "dBPbI43zMLXzGo2tl3uDq3OmmCJ+Uozy7ISyOPQDG1u4U/idgnb8eSCpf7SlymPfb8vUffgh+FxDrN7n" +
                "XJ8XRQbXJxGKI2741rTrj0q0NMnQ4VQaHRCs8rAFnchak4/kaHBCJK0AhR16namBe8oM+L+chOW0rcuK" +
                "2/kO5VkCFApBOQ9+0HIgIV8od0dAKGZZVGvu8vNlhHrYdxTckeHYSoHDVIjDjMapwgpH9F7sOrauIKMC" +
                "caqhJ0Cxh7mny+CagkXsHT/ofDcSl+9+u7y0Hb7pOHsO86I0dX1l7jL47no4IsAy0ALnuLApE4NoOHOV" +
                "dCohPp8UH+Vh6z7CBeNW6CZtOsgd4AFZDWF46sGnJn9e+SuXjWSpLcbqRSCek2ijAyeQipVQsVq5hkqv" +
                "pRE+I2qMFa6IqKAFTM1jrmnYFRC6KoCICeu7tZOHefgvNLZJQgxkWlglNaokVTNkefoWOJQ5dwUgulcN" +
                "dHDqmq1ovDf3mUpugTk3eRjRHuulf9VGSrz6uUVduaiqor+/v1qtesDpPWPn+yv9Xu/TPcn+wPnmmBrM" +
                "fh1HrU8uGteofQA6worym+PTb44OXqE5nODn7UKCGifqJdXydOlklx7n5PBmrs6aFxhsOkQ6BLPTm5ur" +
                "n2MlcPbu1TDWAbfXsJRhLALOfr0YXQ6GNyfHfgCvw8nt+GZ0ffKiOXQxuh2ffNeg6EZetsi6sRCAr69G" +
                "l+PbkxB7x8NfxpOG25z8EBPp7ZvJzfD26t3NGRgNbIOH08vXF57o4WE83GAQj/b2ajA6/zW+DoYXw/Hm" +
                "cO719OICp2vfTFEH4LE/z8J3Rlftiz+vg4jIy48RcYQuwwUz+TmMhB5IaWb6B18H571ez10/wH/hWH+g" +
                "AGQdszZd0U43BgRkQhlCVYAHfsoT6oTWgvjb3z7Bjd8VvQds6W5xtzclqvFe3BmgzHWBC4+KG1Aw7FRl" +
                "il82HAgCD9bzwbX8RzlB69XX+i3mve9/fNWBkGm6j9iAKo67tby6Kw7p6gxxnjsRu11x5PijztBm0vFm" +
                "EJHTjZYfu1x8ZPPrRqLwbLcX/4QhY4/djd2Dxe4CsLVaHHbxl+F4GXPyTk1hCKiKPtCVe4mms1XURvHX" +
                "X2cmM/bm9Stuv0I17X34q/jtoHewd9g7+L2T1tZFjEzPoDCYzaOCfYM0wti7wZ7vX2WS+13cTnS1oETc" +
                "Ih+gC561Fyo1Sw/8WWgqel4u/bh7GCR64P7H9/bdrJDO/bb8mwhYuueWIrn3cG+8fYPeuD+nTdfx9hyJ" +
                "7YrKHedaTvCu41SoRKN3guiMpFlCg4yMvDstUYAjmbmY1W2Ewa6Ao+5u2Qt4ACjg/Fx+mR2f+XYkX9JC" +
                "u77Rq+CpvrFI0B4XjoBSHtNsVgTGLq+oypFZsZABiq6BU4jVR0yLL3RpK0IHbimXjvHq2hWJHsGERiHG" +
                "Pja/FdofLoS+FhPgV1Nb3BOx1fAQVk8UDpLibhDQBd5AyfJrXRV5b370nkjc8cf2DRHjnVH1oM7mHpFf" +
                "yb9sgSjl7vYA5iy7FQFXhlKkFurFSpTbmE9wHS2oogAx2SqI2Oxdp6jrygyexc5JXASoRZWR79dssAPR" +
                "FP50qGhmRw5KMs9uM9fMDhXXbo9clBAsY1c84FqHr9MZtwa++Nq3Mvi1Dl9nMB9bzrL5bYscXirRC/j0" +
                "JdqX/32CaPVP4u9c2Pg0j0/T+CRhgv8BUujdD2kmAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

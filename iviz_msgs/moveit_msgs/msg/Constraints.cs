/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/Constraints")]
    public sealed class Constraints : IDeserializable<Constraints>, IMessage
    {
        // This message contains a list of motion planning constraints.
        // All constraints must be satisfied for a goal to be considered valid
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "joint_constraints")] public JointConstraint[] JointConstraints { get; set; }
        [DataMember (Name = "position_constraints")] public PositionConstraint[] PositionConstraints { get; set; }
        [DataMember (Name = "orientation_constraints")] public OrientationConstraint[] OrientationConstraints { get; set; }
        [DataMember (Name = "visibility_constraints")] public VisibilityConstraint[] VisibilityConstraints { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Constraints()
        {
            Name = string.Empty;
            JointConstraints = System.Array.Empty<JointConstraint>();
            PositionConstraints = System.Array.Empty<PositionConstraint>();
            OrientationConstraints = System.Array.Empty<OrientationConstraint>();
            VisibilityConstraints = System.Array.Empty<VisibilityConstraint>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Constraints(string Name, JointConstraint[] JointConstraints, PositionConstraint[] PositionConstraints, OrientationConstraint[] OrientationConstraints, VisibilityConstraint[] VisibilityConstraints)
        {
            this.Name = Name;
            this.JointConstraints = JointConstraints;
            this.PositionConstraints = PositionConstraints;
            this.OrientationConstraints = OrientationConstraints;
            this.VisibilityConstraints = VisibilityConstraints;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Constraints(ref Buffer b)
        {
            Name = b.DeserializeString();
            JointConstraints = b.DeserializeArray<JointConstraint>();
            for (int i = 0; i < JointConstraints.Length; i++)
            {
                JointConstraints[i] = new JointConstraint(ref b);
            }
            PositionConstraints = b.DeserializeArray<PositionConstraint>();
            for (int i = 0; i < PositionConstraints.Length; i++)
            {
                PositionConstraints[i] = new PositionConstraint(ref b);
            }
            OrientationConstraints = b.DeserializeArray<OrientationConstraint>();
            for (int i = 0; i < OrientationConstraints.Length; i++)
            {
                OrientationConstraints[i] = new OrientationConstraint(ref b);
            }
            VisibilityConstraints = b.DeserializeArray<VisibilityConstraint>();
            for (int i = 0; i < VisibilityConstraints.Length; i++)
            {
                VisibilityConstraints[i] = new VisibilityConstraint(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Constraints(ref b);
        }
        
        Constraints IDeserializable<Constraints>.RosDeserialize(ref Buffer b)
        {
            return new Constraints(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.SerializeArray(JointConstraints, 0);
            b.SerializeArray(PositionConstraints, 0);
            b.SerializeArray(OrientationConstraints, 0);
            b.SerializeArray(VisibilityConstraints, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (JointConstraints is null) throw new System.NullReferenceException(nameof(JointConstraints));
            for (int i = 0; i < JointConstraints.Length; i++)
            {
                if (JointConstraints[i] is null) throw new System.NullReferenceException($"{nameof(JointConstraints)}[{i}]");
                JointConstraints[i].RosValidate();
            }
            if (PositionConstraints is null) throw new System.NullReferenceException(nameof(PositionConstraints));
            for (int i = 0; i < PositionConstraints.Length; i++)
            {
                if (PositionConstraints[i] is null) throw new System.NullReferenceException($"{nameof(PositionConstraints)}[{i}]");
                PositionConstraints[i].RosValidate();
            }
            if (OrientationConstraints is null) throw new System.NullReferenceException(nameof(OrientationConstraints));
            for (int i = 0; i < OrientationConstraints.Length; i++)
            {
                if (OrientationConstraints[i] is null) throw new System.NullReferenceException($"{nameof(OrientationConstraints)}[{i}]");
                OrientationConstraints[i].RosValidate();
            }
            if (VisibilityConstraints is null) throw new System.NullReferenceException(nameof(VisibilityConstraints));
            for (int i = 0; i < VisibilityConstraints.Length; i++)
            {
                if (VisibilityConstraints[i] is null) throw new System.NullReferenceException($"{nameof(VisibilityConstraints)}[{i}]");
                VisibilityConstraints[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += BuiltIns.UTF8.GetByteCount(Name);
                foreach (var i in JointConstraints)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in PositionConstraints)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in OrientationConstraints)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in VisibilityConstraints)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/Constraints";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8d5ce8d34ef26c65fb5d43c9e99bf6e0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+VZW2/bOBZ+N5D/QCAPTXZUTyYpFosO8tBp0mkW2zbbZIteUBi0RdvcSqKGkuy4v36/" +
                "c0iKlO1sZ4FpsMDMFHAsH57Ld648OhS3S92IUjWNXCgxM1UrddUIKQrdtMLMRWlabSpRF7KqdLUgkqa1" +
                "IGqb8cHoUDwrivSZKDucmyrRyFY3c61yMTcW/BZGFqI19BOR61xZ/LaShc4PRgcjnCfulSwVff27AbPn" +
                "PdtPn8W/6ckkkURk16bRpN6AsvYPt4nfWK1g3g69ic+3j7zTjZ7qQrebwYlV/3h4YHT+B/83enXz61O4" +
                "YKV0OymbRfPjFjBwQP9FtEvVG0+ukw40j/pat0sQSTFTlpwspqar8h55h2/A/5CZMYU/LmdLrVZwGcLl" +
                "Uy/lMX4tlJXVTE2mqjDrLGrwQ/KbnMKGzwejeWFk+9cnPVF8skW77wcW4LR7JtZKL5YtaT6XsxYhRmHW" +
                "UjBHl4ijXFWmVY2wqoCDV0rosjYwH+zILgMr7SCkxfPCNHiGH78qa5AZEulQID/6o+1xVM5p8QB+3410" +
                "oLA/d8lzuZrrKgmE3inR1jEh+VJJJKJY8oeD9hbHrZmaFiWg+rIDqVVzZSHE9JFDZEng0HkznzcK8Pug" +
                "ZEZzC5Jj7yagL+0CJDVHqEno1og1q2LBwYGD0UKZUrV248B4p8jjZ57HhHlMnMyow8oUXanu118cjH6h" +
                "+IaMd440Uk2sWnB0/v/F2nerMk2bO3RdTMDwGyiQS5tDsVbmspVs9xJaKPu4QDEocEiWNYoC/9puatWM" +
                "Q1zi30JVSN2i2IiuUVxIZqYsu0rPZAtMNEI3PY+TXKBqaVs96wpJcBkLDxE5xw9xx79G/dYpwvXq4ilD" +
                "qmYdQQ5JuppZJRty1tWFGHVA+uyUDowOb9fmMb6qBWEehMOPsiVl1V1tAT2Ukc1TyPiLM24M3gBHQUre" +
                "iCN+NsHX5lhACFRQtZktOdSvN+3SR/JKWi2nhSLGMyAAro/o0KPjhHPFrCtZmcDecYwyfg/bqudLNj1e" +
                "wmcFWd90CwAIwtqaFdptLqYbZjIrqN8h2aZW2s2ITjmRo8MXhLGLbPYIPmXTmJmGA3LuICHp2RsTnX+v" +
                "aNyb8CG0rCJXKRo3pFjxbxQ5c6tgSS1nakxBcsVuNRWCgjKL+2B/EgdzbXEUaT6meoG6YKzKhG5FbpDF" +
                "yGXwKOUXsFTAmE7LugYzBDr6UVPwxECPceRIjRfjTKyXqnJUhBFHNOeAngmrFzp3JyGo7A9L4Y3LRDs/" +
                "BcaYp1hnJwwOAxNr3HxyPBZXc7ExnViTQfjD+tTjHh304hBpjcko7zyLIaDXXHZD60DXaJH041Ffa+76" +
                "vzb9X18foM0NKzLUvqBGpsjNrkbDzWcXoS5Tj0F7awyGyGAgoK6tLjWVg8YlNzuxq10G+a5gXItU4khS" +
                "EKEmAV20tKWslVPlhrheB1Y0V/ZsY5NBX4UUyFgvNaoAT2BROvWxwlCxy7dbGPr5gOeEOTnGV4ihPNch" +
                "QiLDjDy2VJT7Fc/YtZppnrEpkHL+6Oc2ikBv7ZFcoONkyOV9hr4CT6jiWP9X07z0b5tFhMGi79Wu7vNU" +
                "DJqpucuAEeV2JmYbVEX0tYz6hRL+5sJcnEUuHhjDpVxx2mgboQQzAh6lgy8u0vJsfJLhf+QNtZm/iV/e" +
                "vD//yf99c/3y8u3l+an/+vzDP65eX1y+PT8LD968vjx/MvJgU+ukUCaUWSdPRc9HgShHpcbNCe1uSOoT" +
                "FLhHinCGKh+pnx5IyJ4KhcHeTSVUURwIboYkuO6EH+EexTOPYLyVG5Lwwg9zMJxVzfjb+0x8QLABno+p" +
                "zgQyT3mqWrTLoNHMWFTk2jDK1NrRqnr7APo4Yjt5f36SfPvQY03fPgLqVCWHv9eKCyq5HfMHRFH9R2ug" +
                "ocTpiQq+8PUCw6/MdUcq+FuQi6DxwK+Tt88urv51A31SmcHJzJMc7EYih4oLHZpxuX27WydFUmHYcKL5" +
                "KOSdxmAYG9KA7+Tl5dWvL2/FEfH2X46jTWAC3BLEo01Lnh17zH0uiCPKhWMnj66SQY6zzstxXxI590mh" +
                "RhWwc+6Tjbpf5nNyCCEVfsL5reKZ5CTtDbTlqXDsUkbXMYYYUzpPfY3ivaszh6z4wYM62spEj18fUlvG" +
                "I7iSTN0hjsAQ4fcpcbullbteP8PIcL+jSjscgDLnLZpm3e+umxDayboDY5IbBALB6J8dqrqtmG+keygD" +
                "k1vt4DY7XGq4K+PA3G/MLQ+ifoRu35g6wHOoPH37LeJO0+E3J7H+TvgA7ZXmg9BUEzdQj6fiFxaFGL1k" +
                "tcDt9eeke6xk0fGVeO7uucGTDRm9UnTPU82nzyOScesZoI/1vEa+MOLa3cmiP7E717E2e+KJtnX+0ANB" +
                "FczYA1kw61ETlXIX1E9nTk91NwFw31PbdNreuxH9X/ZKw6j+fbsltHdNE1R60tfxZO0UR1uez2N+bE+c" +
                "+yvWH7PKMjXxQtxRA3nMrhPKWur2YSnZRE3jpkZOcSXpWjW5m9DJSU+9h2TzbZKvOyR/orVUGq/71vHe" +
                "zYnFbrKbd/y6oaRHtO2OO3t36851MxNHcRN5vLN7cgung5APfICKczOYbWTPeePmkPWSujFNJ5qvT0Rs" +
                "6PbfhzntE4xlzq+8enxZixqSOOI13QjQdhybMUHCfkKYKWzuZk5Gcpz0YPav4fmnB0lPDRgxi9dvbsEe" +
                "JiESSuhQdiU0BWR3/CcMbl2IT1W7VqpKlCeeNExE/DJoZB1fTNOBbbLpdTOcv04gXwoeTVZarfEZ3jh4" +
                "aBDcuwtvvvUekdQaOMgpRvpmaboiPybGvGGkBGg6XGjqzvL1c5wUgsFoyt7kLuJ4EIsQK/ReisBEtiev" +
                "Idyu2XEZ3pJTnj9TvYquouj18/82JcTBU0tUFUVn1qoooqfc/T6AxVs5N/vE8IyFLWO71Z0s60Ltu5Hf" +
                "+C1rvy9vfA15YU2Z4B7itDVrafNm4N1ebZcCcjve0ijjC46s4SZEEq8OO17IlrLauAvemO9MXmW/N3M0" +
                "TxxBJlxISDfpob5xu2+2Q90t6vkFlag3AErnMWV5+ggOlsVabiiWxRlHOS5XoHTrYdJ5wnK3PAvZbv0x" +
                "TE4XyzFexuGQc5XOg9C4b9zqb+AQ9yoefa4XLe7jnqFbYMqm6UqVp4QUHoUxX/wKk5aF7cBZVMoAddxw" +
                "9pAwt9oNoVQnO17PUzFBJYEnpl3bL5/Q+HazsCFWpADsMsXKb4s12BKc98eeUz2JvcsVVxTTLZYxuGju" +
                "2MnCbF99CzmCqQkxVUqXPVM1k13Msz0zBouhTTYZgNbdhqVZKD0OdejJsolpkl1EzCXLs/qCsCoNBuA5" +
                "ZZLk2M1i1Myk27/uKB9AVKhmmGCXcsXv1gvjFs0nfnPnbros0E0f1BgGd2IejvknX6RDJa1oyVwE4xL5" +
                "DECo3X2EhDX6fG9F6OPbZRS/iE2vNzyB+w4S5ruotUt25KeycwKcfsmcnn2S8t4b9pz4nrw2XiO3CAD8" +
                "OGhqvj+o5jiwpFtuoVp68ROaoRd9D/9r/eOpE5Gyh2a1ormbKuvxOK0eAW5nM3HV3l7fJhPv7JQFxyh2" +
                "GOmZ+sJuIFTQGymLx8T66CRjDd2e/4RENaH4DqMg6d1ULEKXglITIpwwYShmlPfhIPfJRpcalhYbNyKl" +
                "Z7gCFLSFGZrpGR7uGQZC29gJphBlNL66Wo7gr/Absz7gF3nDvpMODL7tuQBMoo+soX12QNFyC+2hC6PJ" +
                "FkhCz/l9AWXXFmDMYIhYqnNag43g6xm/vhykTxhlwuvtOCa9F+fiNBMf8PFTJj7i4+Qg7BIvX9+8eTv5" +
                "eL795MP5T1tP3p+fhie+krLLegX+VHeCe7sMQ0DfQ4XHBY/fE2+/R3ahGd4BD6+qI2ZAjWo0+g8GCgVt" +
                "ICUAAA==";
                
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Constraints : IDeserializable<Constraints>, IMessage
    {
        // This message contains a list of motion planning constraints.
        // All constraints must be satisfied for a goal to be considered valid
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "joint_constraints")] public JointConstraint[] JointConstraints;
        [DataMember (Name = "position_constraints")] public PositionConstraint[] PositionConstraints;
        [DataMember (Name = "orientation_constraints")] public OrientationConstraint[] OrientationConstraints;
        [DataMember (Name = "visibility_constraints")] public VisibilityConstraint[] VisibilityConstraints;
    
        /// Constructor for empty message.
        public Constraints()
        {
            Name = "";
            JointConstraints = System.Array.Empty<JointConstraint>();
            PositionConstraints = System.Array.Empty<PositionConstraint>();
            OrientationConstraints = System.Array.Empty<OrientationConstraint>();
            VisibilityConstraints = System.Array.Empty<VisibilityConstraint>();
        }
        
        /// Explicit constructor.
        public Constraints(string Name, JointConstraint[] JointConstraints, PositionConstraint[] PositionConstraints, OrientationConstraint[] OrientationConstraints, VisibilityConstraint[] VisibilityConstraints)
        {
            this.Name = Name;
            this.JointConstraints = JointConstraints;
            this.PositionConstraints = PositionConstraints;
            this.OrientationConstraints = OrientationConstraints;
            this.VisibilityConstraints = VisibilityConstraints;
        }
        
        /// Constructor with buffer.
        public Constraints(ref ReadBuffer b)
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Constraints(ref b);
        
        public Constraints RosDeserialize(ref ReadBuffer b) => new Constraints(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.SerializeArray(JointConstraints);
            b.SerializeArray(PositionConstraints);
            b.SerializeArray(OrientationConstraints);
            b.SerializeArray(VisibilityConstraints);
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
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetArraySize(JointConstraints);
                size += BuiltIns.GetArraySize(PositionConstraints);
                size += BuiltIns.GetArraySize(OrientationConstraints);
                size += BuiltIns.GetArraySize(VisibilityConstraints);
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
                "H4sIAAAAAAAAE+VZW2/byBV+568YIA+xu4zWGwdFkYUfsrGz8aJx3DgNckEgjMSRNA3J4c6QkpVf3++c" +
                "mSGHkty0aGMU2N0AsqiZc/nO/fCBeLvSTlTKOblUYm7qVuraCSlK7VphFqIyrTa1aEpZ17pe0hHXWhxq" +
                "3SR7IJ6VZfpIVB2uzZRwstVuoVUhFsaC3NLIUrSGfqLjulAWv61lqYssw20iXctKZdlvBoSe9yQ/fRb/" +
                "oCfThEuWXRunSa7RuSY8HB99bbWCVnunzfB8fOGddnqmS91uR+fX/ePx8bP/8X/Zq5tfnwL2tdLttHJL" +
                "9+MOIkC9/yLaler1JnNJj1aAeqPbFQ5JMVeWDCtmpquLiLfH1aP+gCnxz+GunK+0WsNI8I9PPYtH+LVU" +
                "VtZzNZ2p0mzygf0PyW9yBgU+Z4vSyPbPT/oz/YOdkweeM3US7JnYKL1ctSTyQs5b+BP5VEuOO1hCHBWq" +
                "Nq1ywqoSVl0roavGQG0QI5UMFLQj9xXPS+PwDD9+VdYgCiRcv0Qs9Ffb414yL8Q9mHvftwHC4TAlmxVq" +
                "oevE/r05BlUnWfZSSYScWPFHxgSVsGZmWkR6/WUPTasWyoKBic5Cp3pfoctmsXAKsAcnZCoLiwPHwTxA" +
                "XdoljjTskSY5t4F7WTUkFVzIlspUqrVbD8I7RYY+DSSmTGLqWUYB1qbsKnW35CL7hdwZ9N/5k8OhqVVL" +
                "8sb/O+/6Tu7l2sLD6t0Aat+AfSFtAbFaWchWstYrCKHsoxJxX+KSrBrEP//abhvFCZ8dEf+WqkagluVW" +
                "dE5xzpibqupqPZctENHw1fQ+bnIiaqRt9bwrJYFlLKxDx9lviDr+OfV7pwjVy/OnDKiadwQ4OOl6bpV0" +
                "ZKrLc5F1wPn0MV3IHrzdmEf4qpaEeGQOK8qWhFW3jQXwEEa6p+DxJ6/cBLQBjgKXwokjfjbFV3cswAQi" +
                "qMbMV+zi19t2FTx4La2Ws1IR4TkQANWHdOnhcUK5ZtK1rE0k7ykOPP4dsnVPl3R6tILNStLedUsAiION" +
                "NWvU0kLMtkxkXlJRQ5DNrLTbjG55ltmDF4Sx92u2CD6lc2auYYCCK0UMdbbGlArz9/HGg5EeXcsqMpWi" +
                "XkKKNf9GnrOwCpo0cq4m5CSXbFZTwykorrje9TdxsdAWVxHiE0oVSAnGqlzoVhQGMYxIBo1KfgFJBYzp" +
                "tmwaEIOjo/q4ktsCeowrR2qynORis1K1P0UYsUdzDOi5sHqpC38TjKr+shRBuVy0i8fAGM0Sy+yZwWAg" +
                "Yo1vQo4n4nIhtqYTG1IIf9gQelyOo1zsIq0xOcVdIDEG9JrTbawVKBMtgh4lIKaa2/6vbf/X13uoa+Ns" +
                "DLHPqXIpMrPPzzDz6bnPyVRZUM2cQX8Y1QPQjdWVpmTgfGizCbvGx08oB8ZXRCWOJLkQMhKwzdxKNsrL" +
                "cUNEryMl6ht7qrG0oIaCBRhsVhoJgJusgTWVrtJQniuyXejdiOCUCRHVS/hOUejoGQO1nCy1UhTzNTfO" +
                "jZprbpzJgQr+6Fsz8ryg55Fcos7kiOEDKr4CScjhKf8LpQLrbypE56Iu38dR7rTP4Cczc5sDHgrnXMy3" +
                "SIQoZTmVCCXCJMJUvD7eCRi+lVxzpGg7oAhihDmyBQ8i0nLne5Ljf4QKVZa/iF9evz/7Kfx9c/3y4s3F" +
                "2ePw9fmHv15enV+8OTuND15fXZw9iVBTtST/JYxZpnCKnmfxUIHkjEkIFW58NMQkYB9OxDuU7Ej89EJy" +
                "7KlQaNt9G0JJxIPg+0SC61aEbu3hcOchlLdySxxehL4NirOoOX97n4sP8DPA8zGVmUDmhk7Vy3YVJZob" +
                "iyTcGEaZqjmqU68fQJ8M2E7fn50k3z70WNO3j4A6FcnjH6TiHEpmR8sBVpTyUQ2oD/FyImkvQ5JAkysL" +
                "3ZEIYcbxHjQZ2XX65tn55d9vIE/KMxqZaZKBfRfkUfGuQ+0sV2w/TZInlYYVpzMfhbzV6ASHGjSiO315" +
                "cfnry7fiiGiHL8eDTiAC3BLEB51W3C32mIdYEEcUC8eeH02JkY/XLvDxXxI+d3Gh2hSx8+aTTt3N8zkZ" +
                "hJCKP+H+TtJMYpL2ANpyIzjxIaObwYcYU7pPpYz8vWtyj6z4IYCa7URiwK93qR3l4VxJpO4dHoChg98n" +
                "xe1nVi51fdsi4wxHiXbc8+TeWtTA+t99ISG0kzUGOiNf+/tp+28dcrqtme5w7r4UTCbX0cQ63lf46XCk" +
                "7jdalXsRf4DuUGc6wnMsPH37fcCdGsJvNl/9FHgP5ZV6g1hUEzNQiafkF/d+6LdkvcS4+nNSPday7HgG" +
                "XvjBNlrSkdJrRaOdcp8+Z8TjbSCAOtbTinsmzNmdLPsb+80cS3PAn2gPFy7dE1RRjQOQRbUeukEoP5N+" +
                "OvVyqtspgPue0qYN9sFN53+yOxp79bf3Ryjtmrqn9FbI4clqaehouSEfYiO7K+LGyeq/3FWZhsjA26hs" +
                "PGKDCWUt1fi4a3SDjH0oyhmGj65V09spXZz2h/dPbL954uvuiT/K4il1z0N79WDcRF/fyC06fltQ0SPa" +
                "Ww/Ldz9XF9rNxdGwYzze2y71KyX2fj5PqdiNOhnZE976rmOzotpLvYjmUYkOGxrve8emhYGxRPhVEI6H" +
                "skE+4kakZluBox075BARcf8gzAwad3PPIrlOYhD1Kxj9aVI+Iz5M4Or1WxCHPvCBChJUXQUxAdct/wlt" +
                "W+/XM9VulKoTyUGS2oYBuhziWE8WfXOkmqxvfbcWBgfESMlNyFqrTdJUe1Tg1Pvba55sj4hpAwzkDL27" +
                "W5muLI6JLm8PyfFdh8ml6SyPmZM+7EcdKJuRi4WnQASij9DrJIIR4T28TfDbY08knYNTgj9TXhosRC4b" +
                "evzdk+AFA62QQxTd2aiy7C3kp/eIEu/afHszuOSQwnLWWN3KqkFl2W8Mb8LqtN9+O04ZL6ypErijZ7Zm" +
                "I23hRjbtRfZOL3ddLPEsnl9kA+PAe3gZ2PGKtZL11s9vEx6JgrhhE+bPPPEHcsF+IH0fh2TGxdzt+rbf" +
                "uvPLJdFsgZEu+gjl1iKaVZYbuSXvFafs14YGNr/tJYGnzHRkT/D1a41xJHrfHVxkEu54A+kiMhx2hzvF" +
                "CwSGdUmAnVNDi0Hb0/O7SOlcV6kiPUc+URrzJWwjae/XjoxEOQsYD8vKCAYTa3xvSfmw40U7pQ3kDFhg" +
                "1rX9OgmVbT/maHgi9lDKlOuw9tWgChzvdjcvd+9uF2vOHKZbrgZ/olZiL+LyQ1ksxgQaIfhRJTlYZmou" +
                "uyGqDnQOzIXW0SQ8ynIbN2Axx3i8ISSzBs0klugsZ6ZA6QtcqTLoaBcUOpK9NR+cZS79DnVP9ACfQtJC" +
                "R7qSa371XRq/Kz4JSzg/uTI/31dQ7h/NuNzs8k8hE4d8WdOauIyaJdxZ+5ige8eIi/DFwQQQndqHEL83" +
                "TYcV7qdDkYgd2yCzj23Eo7ILApt+yVnKPih5bw1lTkLF3Zggj5/qAT3umYaHAeWOI0UaWUvV0oubWOwC" +
                "58Pkr/WPjz2HlDrkahT10JRCjydpsohQe43pDVNQNpTBxDB7ecDTGaqIDDRDAjfgKeh9ksVjUD46yVk+" +
                "v6Q/IU4uptmx/ZPSjOwQCxFEmtK5qZ8l+jiPt7gKOl1pKFlufeuT3uCIL2mZMlYxjib7dT4Whz0fis5F" +
                "HanP2fD4Gr/Fpc9ucUl7gVDYvNslTke60D464me5RvaohaZjBx6hF7zop4gaQ8XXU6xScdNkawTPV/zK" +
                "cRQwsUOJr6L75ue9OBOPc/EBHz/l4iM+4hLo5uLq5vWb6ceznQfDYjI8eN+vgUPCZDv1vP8wzf2dhYQB" +
                "oO8xj2M+41e6u698vTPG17U7QyYT8LXon7zv5XKnJAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

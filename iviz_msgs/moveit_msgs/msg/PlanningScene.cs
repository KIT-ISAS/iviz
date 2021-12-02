/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlanningScene : IDeserializable<PlanningScene>, IMessage
    {
        // name of planning scene
        [DataMember (Name = "name")] public string Name;
        // full robot state
        [DataMember (Name = "robot_state")] public RobotState RobotState;
        // The name of the robot model this scene is for
        [DataMember (Name = "robot_model_name")] public string RobotModelName;
        //additional frames for duplicating tf (with respect to the planning frame)
        [DataMember (Name = "fixed_frame_transforms")] public GeometryMsgs.TransformStamped[] FixedFrameTransforms;
        //full allowed collision matrix
        [DataMember (Name = "allowed_collision_matrix")] public AllowedCollisionMatrix AllowedCollisionMatrix;
        // all link paddings
        [DataMember (Name = "link_padding")] public LinkPadding[] LinkPadding;
        // all link scales
        [DataMember (Name = "link_scale")] public LinkScale[] LinkScale;
        // Attached objects, collision objects, even the octomap or collision map can have 
        // colors associated to them. This array specifies them.
        [DataMember (Name = "object_colors")] public ObjectColor[] ObjectColors;
        // the collision map
        [DataMember (Name = "world")] public PlanningSceneWorld World;
        // Flag indicating whether this scene is to be interpreted as a diff with respect to some other scene
        [DataMember (Name = "is_diff")] public bool IsDiff;
    
        /// Constructor for empty message.
        public PlanningScene()
        {
            Name = string.Empty;
            RobotState = new RobotState();
            RobotModelName = string.Empty;
            FixedFrameTransforms = System.Array.Empty<GeometryMsgs.TransformStamped>();
            AllowedCollisionMatrix = new AllowedCollisionMatrix();
            LinkPadding = System.Array.Empty<LinkPadding>();
            LinkScale = System.Array.Empty<LinkScale>();
            ObjectColors = System.Array.Empty<ObjectColor>();
            World = new PlanningSceneWorld();
        }
        
        /// Explicit constructor.
        public PlanningScene(string Name, RobotState RobotState, string RobotModelName, GeometryMsgs.TransformStamped[] FixedFrameTransforms, AllowedCollisionMatrix AllowedCollisionMatrix, LinkPadding[] LinkPadding, LinkScale[] LinkScale, ObjectColor[] ObjectColors, PlanningSceneWorld World, bool IsDiff)
        {
            this.Name = Name;
            this.RobotState = RobotState;
            this.RobotModelName = RobotModelName;
            this.FixedFrameTransforms = FixedFrameTransforms;
            this.AllowedCollisionMatrix = AllowedCollisionMatrix;
            this.LinkPadding = LinkPadding;
            this.LinkScale = LinkScale;
            this.ObjectColors = ObjectColors;
            this.World = World;
            this.IsDiff = IsDiff;
        }
        
        /// Constructor with buffer.
        internal PlanningScene(ref Buffer b)
        {
            Name = b.DeserializeString();
            RobotState = new RobotState(ref b);
            RobotModelName = b.DeserializeString();
            FixedFrameTransforms = b.DeserializeArray<GeometryMsgs.TransformStamped>();
            for (int i = 0; i < FixedFrameTransforms.Length; i++)
            {
                GeometryMsgs.TransformStamped.Deserialize(ref b, out FixedFrameTransforms[i]);
            }
            AllowedCollisionMatrix = new AllowedCollisionMatrix(ref b);
            LinkPadding = b.DeserializeArray<LinkPadding>();
            for (int i = 0; i < LinkPadding.Length; i++)
            {
                LinkPadding[i] = new LinkPadding(ref b);
            }
            LinkScale = b.DeserializeArray<LinkScale>();
            for (int i = 0; i < LinkScale.Length; i++)
            {
                LinkScale[i] = new LinkScale(ref b);
            }
            ObjectColors = b.DeserializeArray<ObjectColor>();
            for (int i = 0; i < ObjectColors.Length; i++)
            {
                ObjectColors[i] = new ObjectColor(ref b);
            }
            World = new PlanningSceneWorld(ref b);
            IsDiff = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PlanningScene(ref b);
        
        PlanningScene IDeserializable<PlanningScene>.RosDeserialize(ref Buffer b) => new PlanningScene(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            RobotState.RosSerialize(ref b);
            b.Serialize(RobotModelName);
            b.SerializeArray(FixedFrameTransforms);
            AllowedCollisionMatrix.RosSerialize(ref b);
            b.SerializeArray(LinkPadding);
            b.SerializeArray(LinkScale);
            b.SerializeArray(ObjectColors);
            World.RosSerialize(ref b);
            b.Serialize(IsDiff);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (RobotState is null) throw new System.NullReferenceException(nameof(RobotState));
            RobotState.RosValidate();
            if (RobotModelName is null) throw new System.NullReferenceException(nameof(RobotModelName));
            if (FixedFrameTransforms is null) throw new System.NullReferenceException(nameof(FixedFrameTransforms));
            if (AllowedCollisionMatrix is null) throw new System.NullReferenceException(nameof(AllowedCollisionMatrix));
            AllowedCollisionMatrix.RosValidate();
            if (LinkPadding is null) throw new System.NullReferenceException(nameof(LinkPadding));
            for (int i = 0; i < LinkPadding.Length; i++)
            {
                if (LinkPadding[i] is null) throw new System.NullReferenceException($"{nameof(LinkPadding)}[{i}]");
                LinkPadding[i].RosValidate();
            }
            if (LinkScale is null) throw new System.NullReferenceException(nameof(LinkScale));
            for (int i = 0; i < LinkScale.Length; i++)
            {
                if (LinkScale[i] is null) throw new System.NullReferenceException($"{nameof(LinkScale)}[{i}]");
                LinkScale[i].RosValidate();
            }
            if (ObjectColors is null) throw new System.NullReferenceException(nameof(ObjectColors));
            for (int i = 0; i < ObjectColors.Length; i++)
            {
                if (ObjectColors[i] is null) throw new System.NullReferenceException($"{nameof(ObjectColors)}[{i}]");
                ObjectColors[i].RosValidate();
            }
            if (World is null) throw new System.NullReferenceException(nameof(World));
            World.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 25;
                size += BuiltIns.GetStringSize(Name);
                size += RobotState.RosMessageLength;
                size += BuiltIns.GetStringSize(RobotModelName);
                size += BuiltIns.GetArraySize(FixedFrameTransforms);
                size += AllowedCollisionMatrix.RosMessageLength;
                size += BuiltIns.GetArraySize(LinkPadding);
                size += BuiltIns.GetArraySize(LinkScale);
                size += BuiltIns.GetArraySize(ObjectColors);
                size += World.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PlanningScene";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "89aac6d20db967ba716cba5a86b1b9d5";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1ba3PbxtX+zl+xE32Q1DBUYrmdVq07I1ty7Ex8qaQ2F4+HAwJLEhWIZbCgKKbT/97n" +
                "OWcXAC+K03desdOZOhmbwN7O/bo4MGUys8aNzbxIyjIvJ8antrQ9X1d84Givd2DGi6IwlRu52vg6qW3v" +
                "ir+v+VNfD/U1pt5MbbNpjd+6auYyW+A593qAwY+xq+I5uodMGoYzkyzL69yVSWHGFV7JfJMt5kWeJjUX" +
                "1WNztMzrqamsn9u0NrWTExtUZN1xb2LdzNbVajjzE39yUyWlx1YzQD+b2+zDRzPO7202lNnDOg57wCBo" +
                "J0XhljYzqSuK3AMiM0sA9X3vXAdexPdv5HWcP2zmD8N8EAdjpsjLWzMneuXE977F03t9ACQcG4axtfk+" +
                "TQqrs6/5M86V95x5XtdJOgWYbvR30ML3O/A2r+ydLYVELq3dLJkbULSL1tykSWmmyZ012BIjrvIm8d6l" +
                "ObibBQLPBmAy+JdUVbIyJH0+zsEfGeq9k8NAFFcBSD16qFsRTp6+dmTvfWDXNeXiO1cVmVnyb85+WSQT" +
                "k5dZZPlyarFBtSFIAGuEH2Vtq3llCWgC6EyWj8dmU0A8ZME42UQlfeRcgU2GnN3rPft//tN7c/31GaT/" +
                "zua1yl+rOaIsAB+y7ZMJyVLWSV56YEIBBMagUDJyi7qjSKJnfZMP7ECF3XlRE099y2tv/u5ACKBfZiIh" +
                "vudt6V2lh3/DQVVbmdeqrYyQhwmUFWwVIZgtijqfF9ZcvHsJdtuG2ZkBAe3a1m84F/M6R8jyYebGw43D" +
                "GmHdklFzlMSxyCyRf8xoaHDci+sb1VOhg7zF1R3tCzvvSZwCT8lW5+tiZfLZ3FV1UtZiv6bgSyHGC9g0" +
                "qI5cRgU6ivBgYulk/7SwSbVrMg4Cw5MgWmdnqavs2VnHLI8szrNmMc8U17xW4OuOyB3vRfp3C2CHUkmj" +
                "AiJ+U1dk3gDshBTIrE+rHAwhEUSCFHFva3ExrvppobpTgeWgjyrAABYseKNmkQUBddgcVfbOFQu+r8y8" +
                "yj3VLT0mNJkd5yXpvDrDBuY3a2oWvVrcJYHBrczsuN9OvbMF7GW92p564mXyiT8W9WyX2DF4VSv2pMcc" +
                "Tg4Q5GV3g7czrn57PBDELltcsGJR5qACpS2zZa0aOsITFErcqRJiapOMgtq12KbOIcE4dznNsWN7nlDN" +
                "myX0HAIP8cpsNjDweVtzsDsgdVCeyEYxHqpGcSmPEA4yNOCZisamAfS5B8wq2dH4iJ+B9+IJ1CFh4zwB" +
                "wdYoLMCQ7YLrxCFwUGWeJbdWF4X5wJ0S5uYaXwzMd1P4RTuYDMzKLapoQgWL0mHDwJ+OJwyqZGd9LhG3" +
                "CU0l1i071T/a2bxeBWkk9RQb5W0Hdz91C3g+pVykk89/hq0HyiCk7tPRGvHjJXi+xClAs5GBBswOcSQi" +
                "i0CD0BX4UmMz5eCg1+u9UuFQGemFwAwWVeRnXLik/t1TPEZF6LyKAt95pRR4NINSZ2pNFGagAYtSZkmV" +
                "gZx1IpZDjG0+gTX9okDYUxBThnvBrqzmxLol5gS2u0K0tTILr74ndbMZSApPofK6tl5FPhEpzNNFARud" +
                "Osh5XnK6RJLcnQS2YEqZWvP64kwE3KaLOgdA0M4yrWziaZxfX5jeAnw6fcIFvYObpfuCDmhC5xQPb+yD" +
                "vYdf8oQz8bRRv1HkBtib1hanQLqP5N0QjzA3OAQg2LmDEhwB8verehpc6l1S5ckIaoaNEUzSgh5y0eFx" +
                "Z2eCfQZRKF3cXndsz/g125bNvsTpi8YX+sUEBMTEeeXuYMHEdomcwgxCeIt8VCXVqiemSo7sHbwkjVWF" +
                "hCM0m+vqGXMLjevz7NGk8ReDICB7ZckuIKIRHYyQeiSKaDA1opmNwcvspLKwvZg5xo/MwcowCYN/c8sY" +
                "PAC7RVovYJ0xrT1Pzepr9SPeL2aUZspNEr0F5davfG1nagf8XBgKkYdexOxH10xs3XojSUdcOP0WLlJc" +
                "pkmniFkH5iUN8z1YU8BiQTEQ1IOnwXUhfsKxf726eCke9pTh5NE9TCf+T5YUCPpDyI63Okh7SpvXEfQu" +
                "dEpITav6YS39y9o4dtUZcTcI7p2tKB6jJL0lwmsw/M+p7tepLivYxemvdqpx+n+TU33Ip2o6xOX+oeoE" +
                "ZnVKERuTloiSOIH/box9J2TCoNLrsYzeA1BHSlbR5IUYszErI1svLYsQS7flMYV/NHiIUJMUlqz3N9DT" +
                "Vae6vlCt/ssCC6qSBqByalL3g2QAZgeKCUIgjm3AbxpDLBI1s0wCIVPNSkkqKTPAgTUVhNrM2ZDd1yZz" +
                "oAeSQLFiUDV6GYmpaY4hjl2a8DWWHFHZ+kxqS51FVyGxikQ3sNVVPsmzTTMqhj8g10dR7QlEGiolMOth" +
                "YCE2idQ+HpjXY1HQJRES5Y7JGtO0AJc4/9q5PiOqsMU6Qd+LEkVdReGjhp6A6yGMNPfNrya0ND/vhdWt" +
                "jO3iNlx4xbhEybfGcz791AooifxJhOKv5Z50VYxGQCs6WN9mrev4jCp3C3ECoyhinsUYFiTocpNyIoEv" +
                "nQaMXdTVMKV9DvP2g52avx1cAyuUPS1yfSgVgBfXQwQZ1P86FGWz9lFrEPsoHj5Q+ArueeOt6vGoUzoS" +
                "X5VovTv4MCnpglSsssWIWSrLoQwvdGwqk1KIQtSJMMBPE+RQQibkhKE8z/Et2NQudOt6nCYHHsCSYD/R" +
                "I7KLuMqWpQsVwQEMFotztkagR7MUqnkHu/aLtT3NIBo0GkLoAVlvk1C6aexehMqSVE9DYNmtv8VKJeuh" +
                "sTdAGNxCBA8Ho4KUIJY3XzSAKRgMyQukfNlKSx0IChTSsKANEGSzodZv6UXGJkXkChYEqKQqgxg71SrR" +
                "WiIKbkhaoPyg91EkxfPMuEeDCrYOTgNGzYLsyNvVk6UF8ny6j8otRBHCLsf9WA6TM0qb0nxXKzmtsgjX" +
                "uCzU+cPBZB/2jEVjOAi+dlFlJXe5aV6CGIRuGI6ITFlaJPNNcLrBDHjMsbkt3bIpS4T5+9DJbV08DxGf" +
                "uL5MSNNUk2P6JjqzGSAqqpD4gGYg4JFIj+wF7r3B2a9RBA/KysRW10U+o7ahQkGvPErgfV2gTqM9oS3D" +
                "4tyklHqO4qIo3HAHbhN3bsv0wdqyeLgjcI86q8tykUXOhKTsatR1uwmRANeuAP7xmJS12RkAvAOxxOIo" +
                "nDLrfRxiWaqdthlk+LVxyhUm4aQ31k/Xd+UbzJ3pwM59ONZu8ZzKQSYw3WUBH8WeEBX4Fru+GYUWjkyL" +
                "6agGEUB+oZoGjrUtTx5x3IWNXTJBRE56AEmOtdCdZ0zrO4Kx0R6VhoUEc51JkNG73C08wj57j0iB4CO2" +
                "VGcqBmfQG60Qsp9fXDz7ksdciVFdO2lcOVYQkF+Vd3nlyhmDXRamYSFWoBLScFSKVBWk31RDmf2GTOTZ" +
                "sZ50dfnm3d8un30lOM3ntFOMWaM0h/JGMKwCdMgEP4VrjLF1UcQTXGiRfP/+8u3FsyfBCLdn7j5OTunD" +
                "Ki6D5AdWS7B/xBmRbzFjnS18zRmFHdeajbIaAmvmXUFagbTRYrTWFC0RUDJTEIU2pwTw3RwlzBjSY088" +
                "MgCNE10cfiyj+Gmj0jv4t/+Yd8+/uXxxwwrpv784/CFxXvxyc1WMprQdxuLwgiGDGWNNitkqogKpyDFi" +
                "BAstOuO1m2i/rqkSaO8IcsL23FpQcWubhlD3hDN5o+vb5o1kkiIusFilyUbR2GOXuGE26oISHKyUyb65" +
                "fvf2BOXrWDv74fzNt0Y3QAOnEWGY2UYBOikmDXWkSlsfVKceHcrAXErUwLbPFtNFj6R249wt4pVbe2Y+" +
                "+8chKXx4dviCkc3F88O+Oaycq/FmWtfzs5MTpB9JAWrXh//8TFGE14CwIxKUwl0ZL4MI90J0Q+Z0qMDI" +
                "Ma8PsShHsA8tuLU2lM3HBVR1lBdIcYJ72iWv7KIqEWPKfPFcZUM2IVbU+3CylrwoXAvQiSZOC6BSlGdB" +
                "NCArfUTZ5sw0BJB3JAHebZLg7Ld/+P1TnUHXqxUCzNuG+DCcdP2Xb9E3RYjA5mnDp7WDr38qXsUZurcc" +
                "ZQ6XE3/6O33DVvWZ+e3T0yfyiNkVJyB8dsswA24f1zOyjdeMUIhIPCB23XUUF3sWBcelKlC7+WEUaIj2" +
                "Y5XlH4oWANGFqunIof7r55Q0XJdZIbSWoA3iJvdfWFiMWQ7EIvaFIVaxoIgIZxRDAGxGg0+XLpqogfOX" +
                "ffyHEgCbO783z999Dzemv6/fv7q8uoRr0ccXP3z7+u3F5RVMeXjx7u3ls6dR26N9Ei9DmMIsjdKiSUB/" +
                "BGlFuAzSTm37cu2MuIZVKYLfXdCZdqY1XqYrcodBiaCumuS6j5bqsF1zqM5NLlyEnBCIC6iaPXzfNz9o" +
                "2f7HLswksiRMtpwgWAwQbdogpk0NfiD6oKXt8HtEJO3TDw2t+fQjvXgHJKV/gEqKXWQ7zSb+DV11WE+F" +
                "ExaNpljxRic/XxCEkOaoBEU4dN/h1fnF679eM0LqnBmZLHuSwdqIVKqo6Ej5QWqGMTyUpks46keTIOAY" +
                "mLZYuLbv8NXl669f3Zgj7h0ejluc9M5Ih+ItTtO19CrqgjmiLhzrebRz8RzFLpyjD51zHjqFRcRIO2Vf" +
                "SE52nwmXrcWAOIT1bZy/qZNs8OSVpMBSS0WPcd7KkNCU65lsUt4X835oZ30eiBqVdIOYjUhtIM94tNXU" +
                "rcktYTjxcUzcdhIgyWe11W9kMLpZ+xJuMTzQcb3dQmp3ipsoYWuRtun6d6rvnXn7QhCgxNLeWkmqezsH" +
                "tZDY52zR/UQJ9vFdEFPL6Hg6oDKdpIFAdq3Kh6Z5OUEE8ceOhb1LigXkHfrO2wCuc+UPOLKjiWDHf/jY" +
                "4xk3YQNpH4W94rXLULiLK2LuhchvwSBJru9MtzJL0FLueOiiPZEqorGDZBEtRHkNUHp14sOpwmnvh1IH" +
                "3Au0kpfvbPJrExyapvl9m/83RYLk3nzO8t/nJv0Zf2XmmZGMOjFnzyDgdvzhy4+sKDaPX/ExbR6f8DFr" +
                "Hk8/Nq2GD08/yrvHIsAnangbda2dfc+NJVHQRHn9fwjuaGHkcl07N1iU9t4can9M+xpF/NCP/ROM4iFJ" +
                "U5RCNdv2H8kltz5b70fh5nkIDDpnqSuz97wnxDpEiEObskiwBvR+seogt+1xyQ5NC7/RnhYbsYHlAKj3" +
                "dlzp8tt3unjRtH25htb2ba9sEcsP8P1D1oB44/fxroH98r3+7RZX93qLVKg2G9BBXgfNjaGDWDPbmplO" +
                "cyQ57cTNq4zt/V8p24b7TJj1p8RMYcmffRZyvmV+mw8q5weumpzU48/+XI//dJL8GXlWeouNpIl8DTfG" +
                "zlTmUtzniSaGsjNjBNipaGzdOQhp1jq4Rv2BYtJWm2WSvu011Gxptpf+1s5PKkKqEp0kKAA9aaRbPJfO" +
                "awyNTAmGJlbV4RJwuYwekKN5u/jBDzyQbMBUMzn0q5nWo/tayF5Xue5pmxhccqyBSJVXqoltubcVsvXz" +
                "yXNbjHEkYfQh7QzOPPaYlBiyb3s5tqXQIFQud+M5lvvwHi1r1DpCp4K9JOS1oLNS+Kj8ChXNJ3JrL9w/" +
                "ZMVIi1iBgg9CrjcBCVuTLW2f3umFhSsEPCcIpaICIORSvIDRKRHr8SIYI1ispVR4Rw4cwgLpIz4RS7kG" +
                "rUwPuZBSMWRgsBMkqYA0m9P8ho5pac7fXnQLqI2ghQ2GXRHgnfqtocj5/euQSCDvW61/XNDyAbX3FPf5" +
                "hBe2ZIUmizjExz2A3fkQqncgDa/oHB9qTcePqNpbMd3Wcozum6+p9oOCfJ31axHg11ufRCB84vX44He+" +
                "3FrvKMJFEBX9QgDGSBWQTWxO3epDRoWP6i5zwo1t2f3q6+fnYeCxb4g35yk5cb+6an5Nml+j5leyD0Jv" +
                "f/HGsH7zU6jNWwpQx50fNd10vucTy9m9q9R2FNv9WR3uhRWB8/rwHYwdqwhxu0drFP3C2VLBOL2QjxFh" +
                "r0cIuvR2ATwLAmtvMB9p/e7OudQ5Yu9c5u3sO4fvYDT+2dFKZWljkpdx15A3hw0ZPO9C4D9BtP8zseRD" +
                "OPm4Cq4AGMalR1IIZd3kxKXpYg4ne0yHIR/CypukTFEpVVIcDUb1yYDBQF7Eb8l0I+m4FWgVdMNLrdiw" +
                "4qTL1y3HlUXLE59GQED5KcEM54aS9IyfDzD002UlvhJusg+yNizjJgENtC1yWNafm1xcl+pVRL0GLqXJ" +
                "AbI83opBR4fBwrLK2SeVub7HOh7dOLWl9y9u3SXQKT0AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

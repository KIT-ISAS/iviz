/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlanningScene")]
    public sealed class PlanningScene : IDeserializable<PlanningScene>, IMessage
    {
        // name of planning scene
        [DataMember (Name = "name")] public string Name { get; set; }
        // full robot state
        [DataMember (Name = "robot_state")] public RobotState RobotState { get; set; }
        // The name of the robot model this scene is for
        [DataMember (Name = "robot_model_name")] public string RobotModelName { get; set; }
        //additional frames for duplicating tf (with respect to the planning frame)
        [DataMember (Name = "fixed_frame_transforms")] public GeometryMsgs.TransformStamped[] FixedFrameTransforms { get; set; }
        //full allowed collision matrix
        [DataMember (Name = "allowed_collision_matrix")] public AllowedCollisionMatrix AllowedCollisionMatrix { get; set; }
        // all link paddings
        [DataMember (Name = "link_padding")] public LinkPadding[] LinkPadding { get; set; }
        // all link scales
        [DataMember (Name = "link_scale")] public LinkScale[] LinkScale { get; set; }
        // Attached objects, collision objects, even the octomap or collision map can have 
        // colors associated to them. This array specifies them.
        [DataMember (Name = "object_colors")] public ObjectColor[] ObjectColors { get; set; }
        // the collision map
        [DataMember (Name = "world")] public PlanningSceneWorld World { get; set; }
        // Flag indicating whether this scene is to be interpreted as a diff with respect to some other scene
        [DataMember (Name = "is_diff")] public bool IsDiff { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
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
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
        public PlanningScene(ref Buffer b)
        {
            Name = b.DeserializeString();
            RobotState = new RobotState(ref b);
            RobotModelName = b.DeserializeString();
            FixedFrameTransforms = b.DeserializeArray<GeometryMsgs.TransformStamped>();
            for (int i = 0; i < FixedFrameTransforms.Length; i++)
            {
                FixedFrameTransforms[i] = new GeometryMsgs.TransformStamped(ref b);
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlanningScene(ref b);
        }
        
        PlanningScene IDeserializable<PlanningScene>.RosDeserialize(ref Buffer b)
        {
            return new PlanningScene(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            RobotState.RosSerialize(ref b);
            b.Serialize(RobotModelName);
            b.SerializeArray(FixedFrameTransforms, 0);
            AllowedCollisionMatrix.RosSerialize(ref b);
            b.SerializeArray(LinkPadding, 0);
            b.SerializeArray(LinkScale, 0);
            b.SerializeArray(ObjectColors, 0);
            World.RosSerialize(ref b);
            b.Serialize(IsDiff);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += RobotState.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(RobotModelName);
                foreach (var i in FixedFrameTransforms)
                {
                    size += i.RosMessageLength;
                }
                size += AllowedCollisionMatrix.RosMessageLength;
                foreach (var i in LinkPadding)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in LinkScale)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in ObjectColors)
                {
                    size += i.RosMessageLength;
                }
                size += World.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlanningScene";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "89aac6d20db967ba716cba5a86b1b9d5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+0b62/bNv67gPwPxPIh8eYpXdINO98yIG3StcP6WJI9i8KgJdrmIokeKcXxDve/3+9B" +
                "UpLttNvh4sMB1w2tJT5+7yepfVHJUgkzFYtCVpWuZsJlqlJ7iastPuHwXrKX7ItpUxTCmomphatlDW8v" +
                "8eEKf/P7sX+Ps6/nKm5dz/0EUZpcFfCsHYMR8GNqbITG29CscYQs81zX2lSyEFMLL2mJyJtFoTNZ47J6" +
                "Kg6Xup4Lq9xCZbWoDQGNNNG6wV4yU6ZUtV2NSzdzR9dWVg72KoGGcqHyt+/EVN+pfEzTx3UYdoQG0S+L" +
                "wixVLjJTFNoBUqKUgPrdXnLGI0/DwEt6HxaM44JxWIBcglFR6OpGLJDIagaQvoPHN/wE+ODg2A+uLXGZ" +
                "LJRfcIW/w3Qa4MlndS2zOeBrJr8BX9ywg3h8pW5VRewyWW1KuRDA3S59C5HJSszlrRK4JwwZ64R0zmQa" +
                "5J17bpcpCB3kKa2VK4Fy0FMNwqKhveQ1gXuKiwFRBj7mvRhXxKAHdi954+V3hbryk7FFLpb4Ny94VsiZ" +
                "0FUetGA5V7CHXVMvQG4CP6pa2YVViK4EHEWup1OxrjPOoMLSJt4KJsYUsMsYp+8lyel/+E/y8uqbEVjF" +
                "rdI162RrU2REQAAovJMz5E1VS105oAWVUqJJCDkxTd0xMLLAodCpStkCjCPbcWiHunbiNwOsAAZUOSkL" +
                "8N6pyhnL0L/FUbZomti16G95ZT2XYMcgYVKIsilqvSiUOH/9DCSvotxzAVxU/d1f4mSY2IFC68e5mY43" +
                "4EXd3VBZcSjDWBAaWYSpWk6AsYcNokmyCoL2heUdq/Rb706zvHRRwMbVxUrocmFsLaua3NscJFSQbwOK" +
                "IrkTk6NJHQaEYGJlCEBWKGm3TUZIIHvptWw0yoxVo1HHd08UAFSiWeRMra4Z/bqjfYMdmcJ2ZewwS0Z7" +
                "IEWcmyJ3AhCXyIRcuczqiSI+kCox6U7VFIeM/b1hQ7IgeGARW0Mqkn0fsuIiBTzkYXFo1a0pGnxvxcJq" +
                "h7aXDRCbXE11haxejWAD8XHP5kLoC7vIHDcoB8N26q0qwInWq82pR44mH7kB2Wq7RE1BWjVTj/xYQBgE" +
                "DHTV3eBViatfDVIi7KKlBVY0lQYuoMLlqqrZVicrcg4Ycj0j5krmqKxdNy5qXaJ+gT3obN6BR1xzYgkW" +
                "D0oPCparPBUQETfmwO6AqQEDCmIkN8KmFJYiCJIg5g8IM/Xy6XtDp13tvG4HN0TBB2IaQkAzIjEupK37" +
                "HCZkUOxE68zIwht0KW8UL/LzgXbUMLPgDCQVP80hWqp0loqVaWzwp0RFZWBDL59OePTGpMohLqFgCsZ6" +
                "q7ri5KCpykW98tqI3GNqWLYd2t3cNBAK52EP4pPTf4DjB5KBkbxPx2ooulcg8yVAATKjDkQ0O8yhtC0g" +
                "DYy2IJcaNmMJpkmSPGflYB1JfPIGfpX0Z1oYWX/xGB6DIXReBYXvvGIOPJhDqXP2JowzkAEepcqlzYGd" +
                "tSTPQf5Wz8ChflpAMlQgpZgPer+yWiDVLTNn4L8tpGEr0TgOQZkpS2BpRnwEfe2tZ5WXpIU6awqJ2RXo" +
                "ua5wOqWauDsyWIFQqkyJF+cjUnCVNbW+JVutMqukQ/f84lwkDcjp5BgXJPvXS/MpBqEZBqgAPPoHdQex" +
                "yTkKTuijPmbiUtgbvS1AAe0+pHdjeAR3A0AABbUwYASHgPmbVT33kfVWWi0nBUU/SDLRgx7gooNBZ+eK" +
                "tq5kZcL2vGML489sW8V9kaZPYzh0zQwYCBMX1tzqnH0X6Sm4QVDeQk+stKuEXBWBTPafWXIkKD6SiHbr" +
                "5hnqD078db6L8LaZDQGxlwrFBYTIEEE4IqGKeldDlhkdXq5mVilyg1P4kRvwMlisGaw7Qv4A1DVZ3ViK" +
                "bC08dqsvas+QpkRtRr2RIVqg3rqVq1XJfsAtSKCg8mAXoTziNTNVt9GI6hTjod9AiKSQKbI5JLCpeIaO" +
                "+Q5EU4DHklSlSRtClyR/98Pl+TOKsCeYWB7egeuE/+USFQLjIeiOUzyI/hR9XkfRu9gxI7noGvq1GF96" +
                "47Arzwi7geLeKovqMZHZDRLcw+H/QXW3QXVpwS/O/3RQDdP/l4LqfTGV6yJc7u5rX8CstlexPmkJAsUJ" +
                "+O/a2E/EJhhkfj2U07sH68BJG1yeN4foViaqXipsTSzNRsQk+aHDA2OSGehy8iPw09gTXl+wVX/fwAJb" +
                "oQOwhl3qboj0yGwhUUIKhGNr+IvoiEmjSoV1IOhUXEmFJeoM0ICNFvAKWLUNsVrLDfAD6kDyYmBqGGXI" +
                "/NEdr4IzZJ7ga1hyiMY2xMK24lkYKihXoewGfLXVM52vu1Fy/J64oainx6DSYFKEMwMDEcImgduDVLyY" +
                "koEukSAy7lCsTVTEi4J/bcwQMyq/RZ+hb8iIgq3qCkKSzEHqPo0Ud/FXTC3FHzsRdatj26QNbtnqGM57" +
                "Msen31sFRSZ/kKDwa7kjWyWn4ckKAda1VWufnok1NwqJJBVz2JPBngSGXFnNKPHFoAHOLtiqn9I++3m7" +
                "oY7d3xapgShYPC1xQzAqQJ5CDxKIIffPkUibtY/cg9hFJ/Ge9pcPz2tv2Y4nne4RxSrJDXEfw6jPq4nq" +
                "m9iyp5Zz7NUTK2OnktpRkHhCJuDmEsoo4hSUhb6HX28iAnuwb+i2+HAeA90HdyIzNiaUGRJMm1bGdwdT" +
                "8FrYpVP1kHrlsbG3v23H0OXjdC3SEtnBEPK9ZJ1fxmMbzjp8i4l6qj7D7PbiQucSW6ThDAHRME02xy0A" +
                "eK6mEjIu8WlEjlHB5LyA4i9fcX4G6QFj6xfstbkCbTf2fV3cFriVQRoLwvCYUTYJCXfGLaNeVQpyoRqB" +
                "JYOhiEmlMFTiHpEc3NuHEHBxCvgPVTzHtawwVKZKaxoyC7/NYBiaYwSkUhk6c7sicFYVXN2GgwAPGgUJ" +
                "m8ZuMgSM3wjMqtMivI4vgSWI4NgDaaWzVFDex3R1TSoQQ6fipjLLaq91sLRgJx3/Tfs881ngkBsMU8oW" +
                "fJc5lHRkRHv9rLGlF2zA0+oZeUiqRNuBHF8C+BfYIvc2rPN2aRD6aqFYQzBeT6Sj2pG41JqUP8XBEmNW" +
                "UauHSWJKrnEL3KfdvG3le1eMRdCWrD7Ysl+nrXcaaD7bDvq6pw4tJ65MAYwIkDLs3ZYaGyp46oH+iLGl" +
                "aW/CGPatOvPW0xDXmzD2YkBoL5Wbr+2Mr2B66Ue27oWD3W2eoNmgULAsxl6/wiTBO7xI51BM/LkPTQtl" +
                "KycbwIaGbRDk1x6eIpBBDz88XyN6CNR9xOJgF8OzPHdd3Vo/bKUDDkr8OpNAd2+1aRykiOoOsgokQdfs" +
                "xNkfgbAnK8jvz87PTx8xpEvyvj1gU2tKbpxWt9qaqsTcGEtui5XXoYKqfQXOi6yEDqpqsHS3piQ6H3hg" +
                "lxcvX/94cfqZp2yxQF+GWW4VqaOGiHfAhLoLzfb3Uxyycl4UqAV5dEh98+bi1fnpcXTWLdjtEAnQEJzn" +
                "0huElztVCIc4I4gwlLll42qcUahpzSXsAGGBx3OmQJYBh4NPaZ1urhwwNPdoEotOGMnXC2VjLQD7wiNm" +
                "rnGuCeMP5js/7HSS/b/8R7x+8u3F02tsrv71xf4PM+jp+49pya9Se2VK8dE7OvBy2NDCUtcpbsBgugmi" +
                "VBZ7KDM+74stBj54An2h4721XORGxQOlLpARveEt2j6VDbo103gPIJ/EqADbtHvmky5CPihTp+3bq9ev" +
                "jjJThvbbL2cvvxO8RSrOokKDJ44W0alS0ZsH3rQtRp8JhNCTigvKNXS1RfpkWdT/MeYG0pwbNRIf/eMA" +
                "GX0wOniKKdH5k4OhOLDG1PBmXteL0dERlDCyAKbXB//8yBNpKdmqDHf/qnDnhKTosyIUUocPmHnq+gAW" +
                "6Ywq7hulfO99WoDpTnQBdVLaj6091cXTWOZjKLzPn7CS0C5IFzoCD5obZ6RmDfAKXR/3Ud2Izq0BR08w" +
                "PQvaaSQiF/glMgJerjNi9Pnfvnzsp2Cg5mYDTNxE+yBAu/r+OwHycwoPYqO8+sCvfi+ehyl+ewInDpYz" +
                "d/KFf4WH3yPx+eOTY36GBRanaMyWwxxIFZbG5uvvMblBggKUcJrvh0uTNwVOoEZDbRYHUcdR3R+q139f" +
                "hgE4nbP5TswdFJYL1LyhyFaQolPWl2Gj1XcrQ91kVTxsBjULXUpIjCYhX4DNMCBg/Cfb5Pz70RD+SxM6" +
                "MfpSPHn98+ln/vfVm+cXlxenx/7x6S/fvXh1fnF5ehJevH51cfo48aob/BZFIcTJz8L3SZiUawjHLlw3" +
                "aae2h33tjLAGW12IfndBZ9qIG8dY+dDdCGYCB3Rk111wXwftmgMOfonXURwFwglVLkJ+Hopf+Czg1y7O" +
                "yGSqvVQ1q2OzuueVsGMLrjPSB0xPW96Ofz591Hn6JfIan34FVndRYv57rKiDhmJHRwr/+lMFh2kSOxny" +
                "z0y3lbluEAVfLLEGpT25ji/Pzl/8cAX4dGEGIdOeKGA+3WSusOpQT4MakSGXpJMcD+pXISEhSUXbgezt" +
                "O35+8eKb59fiEPf2D4OWJr6K0uF4S9O8V6EFWxCHaAsDhodeL8Bh6jwcfujAuQ8KdiYD71h8vq7ZDvOp" +
                "qbi5EIZgfVsbrNsknhppS6V0yiajF60OEU9xPVasqO/NYujPyD7xTE3WLNHzL6rUGvGgXB1L3ZjcMgYn" +
                "PoyL26wXqHq1G4eYmKyuN9RIWpgw8DhfmUFudzqmqUi48xuvEnRa+p15uyJQV7Ed2mtyda/8SJZxn9wP" +
                "9HUfPgRhKRoCTwdVrD7RQUBVzsZntaxmkE/8veNhb2XRKCzUpnjFwHQuFQKNeEwKyY97+y5BGNd+AzqT" +
                "8nsl3nn4VmBYESq0G7x7RhMImy08p4sjvGhHrApkbGFZIOvAtUjxfYy3J4ynuhtTW3En2FINv/XmAJ+s" +
                "q6FvBrTNgthRkHfiE+wkfiKyP+CvXJyKRygsKUanoOBq+vbRO2xOxsfP8DGLj8f4mMfHk3fx/OLt43f0" +
                "7qEY8IFG4Npp6tbD1LUlQdHIeB9McB/AO3gYulzQzvUepb03oDSVg9EQ3w7DoQyMwoPMMlX4Qty9QymZ" +
                "/my+dPUuduE7sDiUqTu8fKTyNOShsXnivQFGv9CVoHv+Fhku3dqZN/mINSpTID3Zck/MbV4UA3I6L3tk" +
                "bV4hy5vQmoDYP8ZOEd4nfri7Ze//mmDz3Kx7Z4b6WOun2l5f03gNaT801zZmZnNdhA8VcOL6VY72XjE1" +
                "ff0lKZj1lRRz8OSnH/nyb6lvdGqNS42dHdXTj76up18dya+h1spuYCM6mb6CMIbHXbnJmjK6GNSd0pAa" +
                "xE7HxkUGX2f10RUcD5iStldNk/htct3eLooXBnZxaLb1Kw5fqoQgCRwAO4naTZHLf9QRPQ3NYU/TactD" +
                "VLjVOQZBHNft+vu/K4GCA9w1FohuVXIje8g98L7Z9QCuk3GBgxErtmDfdGxbxK2u9XFA0atiClART2fa" +
                "T0Fwdji2YqbQ1u0loZZTaexxbid3Slfunfi9UVb7Qw88nYIKFzjOvD6sPhuK6njAoPl+I7aTuMnlOXkv" +
                "9nzTENEz083vTQL8zhGbv6OAgLyCMjmABl28Z0Q6XeXKn9xRxxUIpI7wxICoYAUdUh6T2+zhS9P9JX3P" +
                "S1+PgddAxhJS5aKJ5yOyEmevzrud1o7e+S3GPXXAq/sbY0EL/gtWReqI17r6nzG00sjmKruhkylUbOza" +
                "5JGK+LwDxDsfYyX7dIQWAuZ7zsDDx1ztDZzeEXbI+uNXXTuig74R+wtU4GdkH6bCf2y2Axo6H5D1jyo1" +
                "ewu+P7kM5zwr/lBt6xlncAPBB/C0PJ7HGnv5zZOzMPLQN9MjQGbqybGw8dcs/prEX3IX3N789C7Z3/wS" +
                "a+NexNt34p5vqkhg/gND8qnda1LtIWULApvJe4lf4lWAH34CL4jNhrDfwx01vQc4dTpOzun7SHDlE0jO" +
                "+DIDhJ0hBmSYD+X/+r1OZgT1Q8IJPc3beqzt7wtznrTldBZbIDNdhV19fe03xCR7GwGBql0y7d9mFn2J" +
                "R192QYAACsPSQ2qYYn/lyGRZs4D4O8AwQh/p0htZZavAisN0Uh+lmCnoQg34SzbeCGE8LaRz3TSUOzvY" +
                "meLlrQtJqNB2+DUYqih+x1AOYuu6xG8XMEXkZZXJ2+oFReuX4SaeDAfJDnjZP2LNzkv5HiTfQacWZjrn" +
                "WzgypzxiaXUdFMcl2O/D6A72kiT/AsTdPurRPQAA";
                
    }
}

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
                "H4sIAAAAAAAACu1ba28bx7n+TsD/YRB9kNQwVGK5RY9OXUC25NhBfKmkNhfDIJa7Q3Kr5Q6zsxTFFOe/" +
                "n+d535nd5UVxWtQMCtQJbO7O7b1fZw9MmcyscWMzL5KyzMuJ8aktbc/XFR842usdmPGiKEzlRq42vk5q" +
                "27vi72v+1NdDfY2pN1PbbFrjt66aucwWeM69HmDwY+yqeI7uIZOG4cwky/I6d2VSmHGFVzLfZIt5kadJ" +
                "zUX12Bwt83pqKuvnNq1N7eTEBhVZd9ybWDezdbUazvzEn9xUSemx1QzQz+Y2e//BjPN7mw1l9rCOwx4w" +
                "CNpJUbilzUzqiiL3gMjMEkB93zvXgefx/Wt5HecPm/nDMB/EwZgp8vLWzIleOfG9b/H0Th8ACceGYWxt" +
                "vk+Twursa/6Mc+U9Z57XdZJOAaYb/R208P0OvM0re2dLIZFLazdL5gYU7aI1N2lSmmlyZw22xIirvEm8" +
                "d2kO7maBwLMBmAz+JVWVrAxJn49z8EeGem/lMBDFVQBSjx7qVoSTp68d2XsX2HVNufjOVUVmlvybs18U" +
                "ycTkZRZZvpxabFBtCBLAGuFHWdtqXlkCmgA6k+XjsdkUEA9ZME42UUkfOVdgkyFn9x71nv6b/zzqvb7+" +
                "+gzyf2fzWiWw1Z1Hoi/AAOLtkwkpU9ZJXnogQxkE0iBSMnKLuqNLomp9kw/sQOXdedEUT5XLa2/+7kAL" +
                "UKDMREh8z9vSu0pP/4aDqrkyr9VcGSEbE+grOCtyMFsUdT4vrLl4+wIctw2/MwMa2rWtX3Mu5nWOkOXD" +
                "zI2HG4c18rolpuYoiWORX6ICmNHQ4LgX1zfap3IHkYurOwoYdt6TRAWekq3O18XK5LO5q+qkrMWETcGX" +
                "QuwXsGlQHbmMOnQU4cHE0sn+aWGTatdkHASGJ0G2zs5SV9mzs45lHlmcZ81inimuea3A1x2RO96TAuwW" +
                "wUYBaE4aLRAJnLoi8waQJyRCZn1a5eAJ6SBCpLh7W4ujcdVPC1WfClwHiVQHBrBjwSc1iyxoqMPmqLJ3" +
                "rljwfWXmVe6pcekxocnsOC9J6tUZNjC/W9O06NviLgnMbmVmx/126p0tYDXr1fbUEy+TT/yxaGi7xI7B" +
                "rlqxJz3mcHWAIC+7G7yZcfWb44EgdtnighWLMgcVKHCZLWtV0hGeoFPiVJUQU5tklNWu3TZ1DiHGuctp" +
                "jh3b84Rq3iyh6pB5SFhms4GB59uag90BqYP+RDaK/VBNikt5hHCQAQLPVDQ2baDPPWBW4Y72R7wNfBhP" +
                "oBoJG+cJCLZGYQGGbBdcJw7hg+rzLLm1uijMB+6UMDfXKGNgvpvCO9rBZGBWblFFKypYlA4bBv50/GHQ" +
                "Jjvrc4k4TygrsW7ZqV7Szub1KkgjqafYKG87uPupW8D/KeUinXz+M8w9UAYhdZ+O1og3L8HzJU4Bmo0M" +
                "NGB2iCNxWQQahK7AlxqbKQcHvV7vpQqHykgvhGcwqiI/48Il9R+e4DEqQudVFPjOK6XAJ7QpdaYGRaGm" +
                "MYFVKbOkykDROhHjISY3n8CmflEg/imILOO+YFpWcyLe0nMCC14h7FqZhVcPlLrZDFSFv1CRXVuvUp+I" +
                "IObpooClTh1EPS85XUJK7k4aW/ClTK15dXEmMm7TRZ0DIChomVY28TTRry5MbwFWnT7mgt7BzdJ9QTc0" +
                "oYuKhzcmwt7DO3nCmXiaqd8pcgPsTYuLUyDgR/JuiEdYHBwCEOzcQQ+OAPm7VT0NjvUuqfJkBE3Dxogq" +
                "aUQPuejwuLMzwT6DNJQubq87tmf8mm3LZl/i9EXjEf1iAgJi4rxydzBiYr5EVGEJIb9FPqqSatUTayVH" +
                "9g5ekMaqRcIRWs51DY1Jhgb4ebYfJ7cdDFE+ryw5Blw0tIMpUr9EKQ0GR/SzMXuZnVQWFhgzx/iROdga" +
                "JmTwcm4ZowgguEjrBWw0prUHqnF9pd7E+8WMAk3RSaLPoOj6la/tTK2BnwtPIfVQjZgJ6ZqJrVufJKmJ" +
                "C6ffwlGK4zTpFMHrwLygeb4HdwrYLegGAnywNTgwBFI49q9XFy/Ez54yrjy6hwHF/8mSMkGvCPHxVgdp" +
                "VWn5OrLehU4JqSlWP6yll1kbx646I+4G2b2zFSVklKS3RHgNhv+61v261mUF0zj91a41Tv9Pcq0PeVbN" +
                "i7jcP1SpwKxOWWJj0hKxEifw342x74RMGFR6fTq79wDcTXRfRasXgs3GsoxsvbSsSSzdlt8UFtLmIVRN" +
                "Uhiz3t9AUled6vpCFfsvCyyoStqAyqlV3ReeAZxdWCYIhzi4gYJpzLHI1cwyJ4RkNSslx6TkAA1WWRB2" +
                "M4VDsl+bzIEkyAnFlkHh6GwkvqZRhlB2ycLXWHJEleszxy11Fh2GBC0S5sBiV/kkzzaNqZj/gF0fZbbH" +
                "EGwolsCsh4GL2CQS/HhgXo1FTZdESFQ8Jm5M2QJcEgXUzvUZWoUt1in6TlQpaizqIDW0BYwPIaW5b341" +
                "Yab5eU/cbgVtJ8PhyyvGKErBNbbz6adWTEnnj+HU/FruTWlpPxrMorP1bR67jtKocrcQKrCLguZZoWGV" +
                "gu43KScSB9OBwPBFpQ1T2ucwb18IqjHcxTswRJnU4teHdgF+8UTEkWH+r8NSNmsftTCxn7riAyUxRdma" +
                "jdeq06NOVUm8V6LV8ODVpOALarEAF8NoqTuHIr2QsilaSo0KcSgCAz9NkFgJpZArhuJ9vQ1F70BtRLfk" +
                "x2ly4AGsCvYThSLHiKxsWbpQLBzAeLFuZ2uEfjRRodB3sGu/WPbTtKJBoyGEHpD1Ngmlm8beRqg4SWE1" +
                "hJrd0lwsYrJUGjsHhMEt4IAPeDAqSwmie/NFA5iCwSC9QB6YrbQEgjBBIQ0L2pBBNhtqaZceZWxSxLJg" +
                "QYBKqjWIulOtHq1lp+CGJArKD3oiRVK80Ix7NKhg6+BAYN0syI58Xr1aWiD/pyup3EJ0Iexy3I9lMjmj" +
                "tClNebWS0yqLAI7LQhcgHEz2Yc9YT4az4GsX1VaymZvmJYhB6IbhiMiUpUWG34SrG8yA9xyb29Itm3JF" +
                "mL8ftdyhjuchDBRPmAl1mlpzzOlEbTajRsUWQh8wDTQ8EgGSvcDA1zj8FUrkQV+R8IZ1kdWoeahc0EmP" +
                "EjhjFwjUKFDo27BuNyml1KPIKA433IHbxJ3bIn4wuqwr7ojmo9rqslzEkTMhLLs6ed1eQyTAtSuAfzwm" +
                "Zdl2BgDvQCwxOgqnzHoXh1ixaqdtxhx+bZyihUk46bX10/Vd+QZzZzqwcx+OtVs8o36QCcyBWd5HEShE" +
                "CL7Frm9GocEj02KOqgEFkF+osoFjbU+URxx3YWMbTRCRkx5AkmMtdOcZc/2OYGz0T6WdIbFdZxJk9C53" +
                "C48o0N4jZiD4CDXVpYrNGfRGKwTx5xcXT7/kMVdiV9dOGleOZQUkXeVdXrlyxtiXNWsYiRWohNwcFSRV" +
                "BelG1dBnvyETeXasJ11dvn77t8unXwlO8zlNFUPYKM2h5hFsqwAd0sOP4RpDbl0U8QQXWiTfvbt8c/H0" +
                "cbDD7Zm7j5NT+jCMyyD5gdUS+x9xRuRbTGNnC19zRmHHtaaoLJHAoHlXkFYgbbQYrUFFtwSUzBREoc0p" +
                "AXw7R2kzRvjYE48MRuNEF4c/nV38uFmBefyn/5i3z765fH7D4uk/vzj8IX2e/3L3VeymNCXG4vaCLYMl" +
                "Y62KKSxiA6nUMXQEFy2657WbaEOvqR5oZwmiwv7dWmhxa5t2UfeEM3mj69vWjuSWIjEwWqXJRtHeY5e4" +
                "YTbqghLcrJTPvrl+++YEle1YU/vh/PW3RjdAe6eRYljaRgc6SSdtdaRKWzdU1x59ysBcSuzAptAW10WV" +
                "pKbj3C2illt7Zj77xyEpfHh2+JzxzcWzw745rJyr8WZa1/OzkxOkIkkBateH//eZogjHAXlHPCgFvTJe" +
                "GBHuhRiHzOlQgfFjXh9iUY6oH4pwa22oqI8LaOsoL5DuBA+1S2DZZlUixiT64pnKhmxCrKj64WQthVG4" +
                "FqATrZwWRqVez0JpQFa6jLLNmWkIIO9IArzbJMHZ7//nj090Br2v1gwwbxviw3DS9V++RVcVUQJbqw2f" +
                "1g6+/ql4GWfo3nKUOVxO/Okf9A172Wfm909OH8sjZlecgCDaLcMMeH5c4cg2XjNIISLxgNiW11Fc/lkU" +
                "HJc6Qe3mh1GgIdqfrmL/UMjAMO1CNXXkUBr2cwobbtWsEGNL6AaJk2syrDnGdAeSERvHkKxYa0ScM4qB" +
                "ADaj2adjF2XUCPrLPv5DUYCtnz+aZ2+/hzPT39fvXl5eXcLB6OPzH7599ebi8goGPbx4++by6ZOo8NFE" +
                "ia8hTGGWxmrRKqB7gvwiXBhpp7aNu3ZGXMNSFcHvLuhMO9PyL/MWueegRFCHTXLdR2N12K45VBcnlzJC" +
                "cgjEBVRNI77vmx+0ov9jF2YSWTInW04QMgaINs0Q86cGPxB90NJ2+D3ikvbph4bWfPqRvrwDktI/QCUV" +
                "MLKdlhP/hrY7DKjCCaNGa6x4o9WfLwhCyHdUgiIcuu/w6vzi1V+vGSd1zoxMlj3JYG1TKlVUdKQUIYXE" +
                "GCRKPyYc9aNJEHYMTFtBXNt3+PLy1dcvb8wR9w4Pxy1Oeq+kQ/EWp+lanhV1wRxRF471PJq6eI5iF87R" +
                "h845D53CymKknbIvpCi7z4TX1qpAHML6Ntrf1En2fvJKcmEpsKIDOW9lSGjK9cw6Ke+LeT90uj4PRI1K" +
                "ukHMRqQ2kGdU2mrq1uSWMJi4p0oYkwHNQqutbiSj0s1SmDCMQYKO6w0YErxT8URpW4u3zc2ATmG+M29/" +
                "OAKYpti3VqHqXuJBaSQ2QluMP1Ka3YcvYqLZeKAOtMwuaSmQbKsWordeThBN/G/H1N4lxQKCD8XnpQHX" +
                "uR8INNn1RODj33/o8ZCbsIG0mMJe8ZpmKOXFFTEVQxS4YMAkF32mW4kmyCm3QXTR3qgVEdlFtYgZgr4G" +
                "Lr1k8f5UQbX3QykO7glgydV3XwfQdjm0TpP+tijQVA6Se/M5y4Kfm/Rn/JWZp0bS7MScPYWk2/H7Lz+w" +
                "0tg8fsXHtHl8zMeseTz90PQi3j/5IO8+HQ0+Ut17tFHv2tkk3VgTJU4U2f9moDcGR67ktZODgWlv26Eu" +
                "yHSwUcr3/dhjwSgekjRFoVQTcf+BvHLrs/VWFW6th2ihc5b6N3vPq0UsUYTgtKmYBMtAlxgLEnJTH1fz" +
                "0NXwG+1ssRcbaA6Ae2/HRTC/fROMN1Tbl2tobd8RyxaxMoGAYMjyEK8Kf8rLY7/8VcCOTlj3RozUrzYb" +
                "1kFqB809o4NYUduamU5z5D/txM07kO3dYSnqhltQmPWnxExh2J9+FtLBZX6bDyrnB66anNTjz/5cj/90" +
                "kvwZKVh6i42k43wNx8buVeZSXAGKtobiM2Nk2Cl2bF1TCBnYOrhG3YNi0taiZZK+7TXkbGm2px7Yzm8y" +
                "Ygssuk0QAdrSyLj4Mv1Go7E4MiVYnFh2h4fArTT6RI7m7eIHPxFBHgKzzbzRr2ZasO5rpXtd8bqnbaJw" +
                "ybEGIlVhKTe29eBWztbPJ9ttMcaRhNGHjDS499iHUmLIvu3F2pZCg1Da3I3nWK7Te/S3UQkJrQz2m5Dy" +
                "gs5K4aPyK5Q8H8t1v3BxkfUkLXEFCj4IuV4hJGxNIrV9eqdfFq4c8Jwgl4oKgJA79QJGp4asx4tgjGC3" +
                "llICHjlwCAuk1/hY7OUatDI9pElKxZCcwVSQpALSbE4jHLqqpTl/c9GpsLaCFjYYdkWAV/K3hgLnfxM1" +
                "EhmkFm18ntCyAvX5FBcBhR22ZAkni2jEx71A3vmeCvBKXyw6yoea2PFjrPYuTbcJHQP/+FXW3rCQ77x+" +
                "NQ78EOyjOOjXYnvBoPMZWLS+bU2Z2OiXBjBMqoxsemPudtMyKn9UfZkT7n3L9ldfPzsPA5/+pnlz4iMl" +
                "Ku5pV82vSfNr1PxK9kPu7Y/oSPWtb6s27zZAO3d+JXXT+UZQbGn3qlPbhGz3ZzW5F1YEAdCH72D+WHCI" +
                "233C3tIvnK7ljtML+cYRRnyEYEyvJcDdIOb2BgtQANjdb5eiSOy4y7yd3erwYY3GRTsasKyDTPIy7hrS" +
                "67Ah4+pdGPw2dPvX6SXf18kHW/APQDIuPZLaKYssJy5NF3M432N6EfnEVt4kZYriqlLjaDCqTwYMEvIi" +
                "fqKmG0mfrkCDoRt5anmHFSpdvm5Frix6pfjWAmLKbxNmODdUsWf8HoEhoS4r8f1xk5uQu2EZNwlooNmR" +
                "w9D+3OTrulSvNOqlcqlmDpAD8kYN+kAMIpZVzgarzPU9lv7o3qkzj3r/D6VmcyGEPQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

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
            Name = "";
            RobotState = new RobotState();
            RobotModelName = "";
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
                "H4sIAAAAAAAAA+1b628ct7X/Pn8FEX2Q1GxWieUWrVoXkC05dhA/arnNwzAW3BnuLqPZ4Yac1Wpzcf/3" +
                "nt85JGf2oTgtrnRRoE5g7wwPyfN+kXOgGj03yk3UotZNY5upCqVpTBFajweMFsWBmizrWnk3dq0KrW5N" +
                "8Q6/r/BTXo/kNYG+n5m8aDuLw2ruKlPTsw2ygaIfE+fTPrIGA43inrqqbGtdo2s18fSK4VW1XNS21C0m" +
                "tRN1tLLtTHkTFqZsVet4x0wKzzsupsbNTevXo3mYhpP3XjeBlpoT9vOFqT58VBN7a6oRQ4/aNBwIByZb" +
                "17VbmUqVrq5tIIzUXBPWt8W5DDxL71/x6wQ/yvCjCE/MoTFV2+ZaLUBeMw3Ft/T0Vh4IE4yN4tgGfCh1" +
                "bQT6Cj8TLL8H5Hnb6nJGaLrxT8SLMOjhm1+ZG9Mwi1zZurleKOJon6yFKnWjZvrGKFqSRpwPSofgSkvS" +
                "rSKD50MSMslPe6/XCqy3E0vy4aHiDW/2DHMJSdl6JEsBT+y+sWXxNorrCnrxnfN1pVb4G9DPaz1VtqmS" +
                "yFczQwv4LUUitMb0o2mNX3gDRDVhpyo7mahtBQkOqsmLiKaPnatpkRGgi+LJ//Gf4tXV12ek/TfGtqJ/" +
                "neWwsRD6pNtBT8GWptW2CUQJFFBD+5Ueu2XbMyS2s4GyQzMUZXeBzSTA3mwb1E+OGEHkNxVrSCiCaYLz" +
                "svk3GBSzZbjObL+Rae1Mk7GSWFkJ5su6tYvaqIs3z0ncJgu7UsRAs7H0K8ASXG8Lnj6q3GS0tVlW1h0d" +
                "VUc6jSVhsf67puPBcZHmZ9MTpSN9S7N71hdXfiB1ijKFWF1o67Wy84XzrW5a9l8zkkvNzouoyaSOXQUD" +
                "Okr4EGDjeP2yNtrvA6aNSOA6qtbZWem8OTvrueWxof2MWi4qodW2gnzbU7njB9H+/QrY45TOJsDqN3N1" +
                "FRShrcGByoTS27FhJrAGCeHBtBxinP95KbbjSeTEHzGAIXmwGI3yJEMMlGF15M2Nq5d479XC2wBzK4+B" +
                "TWUmtgGf12e0gPrdhpmlqJZW0RUWmB8POtAbU5O/bNe7oCeBgU/CMZtnN8VMSFatUA9+LCjIEQa26S/w" +
                "eo7Zr4+HTNhlRwvNWDaWuABtq0zTioWO1+wPOJwKI2ZGV1DUvsdWrZ1DucgWbDnr7cdcC2pFdk4KT+pV" +
                "mWqoKObtwNDqhKkj40liZOchZpSmYguWIFID7DmM8tl0gMGGNkTNTs6H4wxFL+wAG2IxLrRvNznMyEDs" +
                "TOvU6Toa81xfG5kU4Yl2aJhbSH4xVN/NKC6a4XSo1m7pkwtlKhpHC0b59CJhNCUzH2AKh02y1BvTF6fE" +
                "RzNftOuojeCeUCOy7dEeZm5JkW+W1mA+BfsL+XoimRgp6/SshuN4QzJf0S5EZtaBjGaPOZyRJaSJ0Z7k" +
                "0tJiIsFhURQvRDlER4qYmJFHZf2Z1E63f3hMj8kQeq+SwvdeCQfuzaG0lXgTwZnIII/SVNpXxM5Ws+dg" +
                "Z2un5E2/qCntqUEp0r3oV9YLUN0xc0q+21O2tVbLILGndPM5sbRkPpK+bswXldeshbZc1hp5FOm5bQDO" +
                "mSRWB4MNCaUpjXp5ccYKbspla2/YVpvSGx3gnF9eqGJJcjp9hAnFwfuV+wIBaIrglDbP/sHcUlwKgQMT" +
                "fNTvhLghrQ1vS7uQdh/xuxE9kruhTQgFs3BkBEeE+dt1O4sh9UZ7q8c1Rz5KJuFBDzHp8Li3csNLN7px" +
                "aXlZsdvjtyzb5HVB0xc5FobllBhIgAvvbmwlvov1lNwgKW9tx177dcGuircsDp57diQQH0vEhm3zTLWF" +
                "5PW2eojwtpsEEbHvDMRFhOgUQSQiQUWjq2HLzA6vMlNvDLvBCf2oHHkZFGEOlUVKHoi6ZdkuPUe2bj9x" +
                "qy/byJDlHNoMvdEpWkBvwzq0Zi5+ICxYoKTyZBep+pE5U9N20YjLERd3v6YQySFTlTPKWYfqORzzLYmm" +
                "Jo+luQbTPoUuzf7u7+8unnOEPUU6eXRLrpP+1ysoBOIh6U4wMgh/Cp/XU/Q+dsJIKasGcS7iy8Y4rSoQ" +
                "aTVS3BvjoR5jXV6D4A0c/htUHzaorjz5xdlvDqoJ/D8pqN4VU6UcwvRwV3eCoHqtiC2gFQkUAPh3a+w7" +
                "ZhMNCr/uy+ndgXXipE8uL5pDditj064MmhArtxMxWX5weGRMuiRdLv5B/HT+VObXYtV/W9IE38ABeCcu" +
                "9WGIjMjsIVFTCoSxLfxVdsSsUXODIpB0Ks/kohI6QzSgp0JeATXbALVa5YgfVASyFyNTQ5Rh84c7Xidn" +
                "KDzBa5pyBGMboKhtBAqhgnMVzm7IV3s7tdW2G2XHH4kbqHbyiFSaTIpxls1IhLRI4vbxUL2csIGuQBAb" +
                "dyrWxibjxcG/dW6AjCouscnQt2xEyVZtQyFJVyT1mEaq2/wrp5bqlwcRdadj+6RNbtnbHM43ZI6nnzsF" +
                "BZM/SVD6tXogW2WnEclKATZ0VesmPWPvrg2IZBULaMagIYGQq5spJ74IGuTskq1GkO45wj0MdeL+9kiN" +
                "RCHi6YgbkFER8hx6QCBC7m8jkRfrHqUH8RDNwzsaXzE8b70VOx73Wkccq7T0u2MM45auZaqvU8bMneXY" +
                "hmc+5s4kN6Io66Q0IMw01VDMJqoJY3u+3cWiOBC/0O/rAYw3PCBPokuxI4gLtPKSjYsdwSE5LDTnTDvg" +
                "bnjq5h3sWy/19iRRy2RkRsgGVbHNKFk0nV7EzhJ3T2Ni2e+/pU4l+qHpbAA4uCUrHm1cmYmmPEt9kRET" +
                "NJCS11TyVWvJyigpEEzjhC5B4MVG0r9FFJmokjJXEkHEihNIyrFL6RJtFKIkDS4LRB6IPkIkR5451sik" +
                "0NIxaJBTM8R2qtslkpW148JUe7dkQ4irHA9SO4z3aEwJ9+3XvJs3tdSzsc8fN4b4aM3UNKYA8RNvsu61" +
                "BN/nl8QMYDeKWyShrAwV8zk53RIGRcyJum7cqum8KcM/hE3u2uJ5zPgG0kyYcGYQu8mpfGOb2U4QhVTS" +
                "+EhmZOARaw+vRdJ7RXu/bI+TsdoqzUtyXi+MKAWi8lgHrhCZO9l64rEM6ohpw/0coUVIeI8VsExauWvT" +
                "R2+LOmdP4p5sVqZZHz0DLGXfQV3/NCEx4MrVRH/apkRvdm7RMAkFexzBk6HepiG0pTqw7SQjbIyPhPG0" +
                "0ysTZpur4g3BzmVg7zoY65Z4CuOAEFDuooFvEPyjN8vUDdQ4HuEwWCpHJYkg4pdiaSSx7sgTWxz3ccMp" +
                "GRPCO91BJMY67M6rKvTVaPt4lA8sOJnrAZGO3li3DJT2mVvKFIC+bcU7i8MZFuM1peznFxdPviy4vQFr" +
                "2Nhp4t1cOqHNjfWumSPZRQ3tUUodGSrD1+Sa2BT4vKklYw5bOmGrY9np3eWrN/+4fPIV07RYwE8hZ20y" +
                "XdzeiI6VkQ6pdf7rtKYcWyYlOkkKHZFv316+vnjyKDrhbs/92/EuA/KKq6j5UdSc7B8BIsktVazzZWgB" +
                "UZtJK9UouiHkzYKrwStibfIYnTetTCBOVoIi8+YUCL5ZGJ9TelqTHpGAJkCXhu/LKX7aqRQH//If9ebp" +
                "N5fP3qND+q9Pjn/AnGe/frjKTpM7JBMOeNGRkRtDTwrVajDSQ0HGSCI0Hm2QqZzX5S6BnB2RnuB4biOp" +
                "uDb5QKi/wxm/kfldn8knhZpanNhX4+TsaZW0YDXuoxIDLLfJvrl68/qkdPPUO/vh/NW3ShYYqvOswuRm" +
                "swH0Skw46sSVrj8oQT0FlKG65KzBNnuEznbEvRvnrilfuTZn6rP/OQSHD88OnyGzuXh6OFCH3rmW3sza" +
                "dnF2ckLlh66J2+3h/34mJHrOmBonjbsmXQZh6cXsBsLpcQGZo20PaZItuVi+Nia2zSc1merY1jb1e8w+" +
                "fcUpqjAxlcwXT0U3eBFQBbuPO0vLC8q1JD7BxUkDlJvyaIhGYvkckZc5U5kB/A4soHfbLDj7/Z/++Fgg" +
                "EHqlQ0Bwuxgfxp2u/vatIrEFg8PTLKeNja9+rl8kCFmbt1KHq2k4/YO8wVH1mfr949NH/EjQHgAWaW6E" +
                "oLC/cr7aeo0MBYSkDdKpu4zOXbWsMc5dgdYtDpNCk2rfV1v+rmyBMLoQMx27W6oBF9C0gSrXlFpz0lai" +
                "Jxobi6nK8SafC5NapYYiZTjjlALQYnD4COlsiZI4fzmg/4YFH+78UT198z2FMfl99fbF5btLCi3y+OyH" +
                "b1++vrh8R648vnjz+vLJ42TtyT9xlAFOEUqytOQSLAXakC6DdKDduVwHkeagKwX0+xN6YGfS40W5wncY" +
                "hAkSqsGu2+SpDrs5hxLciqiaGCXCGVWpHr4fqB+kbf9jH2cwmQsm00zb3Ffe9kEomzJ9xPRhx9vR95SR" +
                "dE8/ZF7j6UdE8R5Kwv+IFTe7IHa4Tfo3HgAEZD/iVNgVC91eV3YJFGKZIxo03JDr6N35xcu/XyFD6u2Z" +
                "hMxrQsByEClcEdXh9gP3DFN6yIcucasflaaEY6i6ZuHGuqMXly+/fvFeHWHt+HDc0SR3Rnoc72iabZRX" +
                "yRbUEWzhWPaDn0v7CHVxH3no7XPXLmgiJt6J+GJxsn/PZ66RZkAaovldnr9tkzjgsZ5L4KGYjF10OsQ8" +
                "xXwUm9D35WIQj7M+j0wttiwx8i+r1BbxyEc7S90B7hgDwPtxcbtFABeffue8Ecnodu+LpYX0QMbldgu4" +
                "3WtuDlUhTdp86t/rvvfgHopA2+TO5UZLqn87R4uMN8n9RAv2/kMQSssUeHqoopyEg6DqWozPW91MKYP4" +
                "c8/D3uh6aVB/TXAbwPWu/BGNONGkZCd8+Fhgj/dxAT4+imula5excZdmpNrrGpfEGICx2cNzvuMhkx6I" +
                "VYmMPSxLZB2GDim5OvHhVPA0tyPuAz4ItlyX7z3kl0NwM4j1fVf/5yaBvlWfo/33uSp/ob8q9URxRa3V" +
                "2RNScDP58OVHdBTz41d4LPPjIzxW+fH0Yz5q+PD4I7+7LwZ8ooe31dfae+65NSUpGhvvvQnuE3gnD8P3" +
                "ADrY6FG6I35juezLhvhhkM5PaJQedFmaOlbb4SOk5Dah5X7Ux9wz7+0loczc4p4Q+hAxD81tkegNEP1S" +
                "14Fv23swXIet42n2EVtUDon0Ys+VrrB7pwsXTbuXG2Tt3vaqlqn9QLF/hB4Qbvze3zWwX7/Xv3vE1b/e" +
                "wh2q7QPoqK/DfGPoIPXMdiDLma3TJwMA3L510d3/5bZtvM9EUH/Rakae/MlnseZb2Ws79C4MnZ+etJPP" +
                "/tpO/nKi/0p1VnlNC/Eh8hWFMZxMVa5czrOLge7MHatB7mjs3DmIZdYmukrigVDSdZsZSN4W77uLQPls" +
                "/yHOt/Z+UhFLlRQkiQNkJ1m7OXIJXHY0DBIdTeqqU0i4sRUiIEZtN/nODzyo2CBXjeIwrOfSjx5II3vT" +
                "5Pq7bVNwibGMkRgvdxO7dm+nZJv7Q+amntCWwDG4/GEGgNMZkzCD1+3u8XQcGsbO5X46J3wfPqifl8bb" +
                "eFKBsySqa4nPwuGj5quBah7xrb14/xAdI2liRQ7eibncBARuuVra3b13FhavEGCfqJRCCiHBl+IZjV6L" +
                "uIknbNxEJeq4wzt2JCGawOeIj9hTbmDL4LEWEi7GCoz8BFjKKM0Xy3yyoRt1/vqi30DNihYXGPVVAHfq" +
                "d4aS5B/ehlgDcd9q8+OCTg7lzJTXfJQEVUaHpko0pMcHQLv3IVRxwAdeKTjedTSdPqLqbsX0j5ZTdp+/" +
                "pnoYEvjrrN9KAL7e+iQB8ROv+0e/9+XW5omiFc8glxlX6YxmLR+I7Z5DJoNP5s4wVT4sdf7d10/P48B9" +
                "3xDP+wk7Tx8pn39N869x/qUfgtG7X7wVB7ufQm3fUvjwUe39qIkFFb/nY8/Zv6vUnSh266M7XMQZUfLy" +
                "8B05O3QR0nL3dlD0K3tzB+P0gj9GJH89pqRLbhdQZBkg2BI8lfX7T865z5HOzhlu77lzvLIr+c+eo1S0" +
                "Nqa2SavGujkuiOR5HwH/H0z7t5nFH8Lxx1UUCojCNPWIG6Hom5y4slwuKMgeI2Dwh7D8RjflOrHiaDhu" +
                "T4ZIBmydviWThfjErdYh9NNL6dig4yTTNz3HOxPwQRYUFJ8SzI9zS3qOzweQ+sm0xlVdVQLRxmlYJJIR" +
                "KJ0hz/pLrsVlqlxFlGvg3JoczuRWjK44WVh52ybFCQX6eAjjsJbin27dJdApPQAA";
                
    }
}

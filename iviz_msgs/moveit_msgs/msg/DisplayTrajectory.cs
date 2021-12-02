/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class DisplayTrajectory : IDeserializable<DisplayTrajectory>, IMessage
    {
        // The model id for which this path has been generated
        [DataMember (Name = "model_id")] public string ModelId;
        // The representation of the path contains position values for all the joints that are moving along the path; a sequence of trajectories may be specified
        [DataMember (Name = "trajectory")] public RobotTrajectory[] Trajectory;
        // The robot state is used to obtain positions for all/some of the joints of the robot. 
        // It is used by the path display node to determine the positions of the joints that are not specified in the joint path message above. 
        // If the robot state message contains joint position information for joints that are also mentioned in the joint path message, the positions in the joint path message will overwrite the positions specified in the robot state message. 
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart;
    
        /// Constructor for empty message.
        public DisplayTrajectory()
        {
            ModelId = string.Empty;
            Trajectory = System.Array.Empty<RobotTrajectory>();
            TrajectoryStart = new RobotState();
        }
        
        /// Explicit constructor.
        public DisplayTrajectory(string ModelId, RobotTrajectory[] Trajectory, RobotState TrajectoryStart)
        {
            this.ModelId = ModelId;
            this.Trajectory = Trajectory;
            this.TrajectoryStart = TrajectoryStart;
        }
        
        /// Constructor with buffer.
        internal DisplayTrajectory(ref Buffer b)
        {
            ModelId = b.DeserializeString();
            Trajectory = b.DeserializeArray<RobotTrajectory>();
            for (int i = 0; i < Trajectory.Length; i++)
            {
                Trajectory[i] = new RobotTrajectory(ref b);
            }
            TrajectoryStart = new RobotState(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new DisplayTrajectory(ref b);
        
        DisplayTrajectory IDeserializable<DisplayTrajectory>.RosDeserialize(ref Buffer b) => new DisplayTrajectory(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ModelId);
            b.SerializeArray(Trajectory);
            TrajectoryStart.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ModelId is null) throw new System.NullReferenceException(nameof(ModelId));
            if (Trajectory is null) throw new System.NullReferenceException(nameof(Trajectory));
            for (int i = 0; i < Trajectory.Length; i++)
            {
                if (Trajectory[i] is null) throw new System.NullReferenceException($"{nameof(Trajectory)}[{i}]");
                Trajectory[i].RosValidate();
            }
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.GetStringSize(ModelId);
                size += BuiltIns.GetArraySize(Trajectory);
                size += TrajectoryStart.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/DisplayTrajectory";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c3c039261ab9e8a11457dac56b6316c8";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbW/bRhL+rl+xqD/YvipyG6dFT0U+JLHTJGji1HZf0iAQKHIlsaa4KklZVg/33+95" +
                "ZnZJipKbFHf24YDLBY1I7s7OzM7bM7u3Zy5n1sxdYjOTJmbiCrOapfHMVLO0NIuomplZVJqxtbmZ2twW" +
                "UWWTXlkVaT7VaaM06fX2hExhF4UtbV5FVepy4yagYpVI7PA2zUHSlal8vY6ypS1lxSjLZORvLs2rEj+j" +
                "ykQF2brmMlHm8N9A6lsTmdL+vrR5bGWJIvrNxpUrUlCbR2vwasqFjdNJCk7P3dhVl2HI+v2HZvy6Zptj" +
                "TAmurYHQy9ImpnLGjclxzXDN6lHp5rpyw7J/EkoDA7ovq5rUeN2oIUnLRQYec2iOayS2ssU8za0OqZfa" +
                "pF4rJCefQTYD5upBSn5uyzKaWhON3bVVPlqMeRHDoHpLPIGwMWkOQee6hRS5y0SUlQ5Ecg74My76HZlu" +
                "Z3eVwgDAcbEqUjC4OW1L3h2yQFTZ6At52ezwCKOKqtd7/B/+03t98d2Q5mnTajQvp+VRx856LR7k+ytK" +
                "3XxXLYyq2ye8XmZVenL2vDtxzvejxE1GWyTuSMyPiNJ7YaPEFmYm//jQAD9T9vIIW9TrTHnLRwxZiGXd" +
                "Fd9llSjDyiCcAeaRJ1GRwGyqKImqSOx7lk5ntniQ2WvEQBjMfAFbk6/VemHLgUQJODP+agTMsnUdJGI3" +
                "ny/zNBa7SyFrez5mwmYjGHtRpfEyiwqMd0WS5hw+KaAbUsffOqC9PBnSMUsbL6sUDK1BIS5sVDIQvjwx" +
                "vSVUdvyQE3p7lyv3AI92CvXXi6ujgll7w2hMPqNyiDX+psINQBvKsVglKc2BvBvhsTw0WAQs2IVD/D8A" +
                "52/X1cyp011HRRqNMwmQMTQAqvuctH/Yoky2hyaPchfIK8VmjU8hm9d0KdODGfYso/TlcgoFMisVyAtJ" +
                "E1njLEU0Mlk6LiJ6Embpkr2959Sx5jLZEfwblaWLU6YxxJ1qFnKZ7Ibksv+KF4lLQOTTiLm3cXdxkToG" +
                "YlPBMna7Do/v+wZWAoEqfMVDFMc2Y5bmxw8fQNFtjrYT2Hz1QSyfymutBXNG7rQ3tD2b0DKfIC438ddn" +
                "bOYAH4pLahcGDY5QJDTRXbyegzpSDiB6rzfJXFR9/UgCgGes9a4Rp/VyQ6zWe5Wmlyz1k1jMaFK4+d1G" +
                "/k8M1r6+0MjYLi1Up956OxHBwN41z9mJLSQoiEXv2LAgdtmJwdy5HcVYpMnjAZKH36cWrQNLy1Nzo5fw" +
                "W17WlcBhsE0d0ZRkLRKSxuHw2RLFTYTAVxQodbBYvc1HzeYebW7pnoYt1pq1YWU2n6JK8K9qai0T64uy" +
                "NiapNYZhdHfG165pCrFBb3e2umU37ydrfaJpbcSL7rbGUH4wsdZGmoMlsoMzXxvQO+xNLerYKqxzGUZp" +
                "iay/IaWYMDyeNBEdouCf61ZgCKaJGnwKp28Xr6a7zCotq00v31oCltOyjX9vnU0ru/dAcYuOQ0lR+6ga" +
                "cLNTY1utCLmqldsKEBJYJ4UlzIliVBC9n8QmjnV+JgL2flhiQpFT1sJpDLgfIT0zO0Sk7fBbh/8GLrkc" +
                "9c7cRoxMrpmJiUlaYCpkGKitQEnAF2llEgd9ABaBxjy6AkmL8oGzo8Uiq61fdcLXmHJgB9NBHzAX+pVR" +
                "gjHJhZR3aWxoXkknAApN44UDtJk89LCFPOti2EIQCdo+HBB8rd3SrCgQfhS+qpQ0G/iS6qdyrs/k4Els" +
                "KlRcvcZKAGwVojx23WdBc1P/Wte//riXrW5sbNduw0GByUP+2dhzPv3eGCiV/FGBwq/VPfkqA0gQK5TS" +
                "ZRP9NuUZF+4K5oSNoomVqEVzi2KV2SnKp1L5EwQATARf9UOaZz/u3sCqYOUg4FZPoN0EQDdhWXWhN7xv" +
                "AN/daltQ+pB6IT2kvCp79EpXtApfBeqadIUcs8CrVqOBjZxZdG01ty3gI8hZUno26YCBYIP0Rq7UJbpw" +
                "uV7sSVUhdYJK7LIsLaVEGjO5AqNE4RtLNTZ7KAU8vdHBYS/Mfxamn8lsphz/ZVRTHnnKXPd5Fk2h3YSQ" +
                "kYEHYUgqesEoZYwIxEioMYLYroDlEaugjmEYnEwEtcDRqAeJk9qOEiIyv4VX566sCCHnC1TJjEKCdwOg" +
                "kqoqiDp2CfHFQeAHA9ltIsTNaMo7BhNcSFUppjUcxgjKw2GrETOWOG2WC8Q9LojgK8xXLZM77I2dQ9lY" +
                "jijcnfUCdhpgS1NR7QJah7oM6DWE68SWcZEyaLPKFNFE8NJWUoS4AuhdfKfAlkM/6gDMbB4C1JOkytaK" +
                "5aCw1y4D1BKIVqQl3S0+JDeJnSA6EOASuANft91ss+45KKKEBOaH/WZoHaW2hh6VMvgIaJ/u2UxRJFU3" +
                "D5jRtnqMB2/mnP3mENgw1J76hdgmT6EFWlvC3qB4KAA64wHL6k0s1ABaqXuIb7Bu6DyH9URrpVmxOET+" +
                "Z2mfDASUdsdopwX9yHobJXioG4WpXKLGCVxTxegGwBKBv2zhJQYfwQuAG1yBPqRgCRF9U8PCjBQ0lHXq" +
                "Iho2uZDaRCb58ZCdFuYW3NEoG5ifWY6wMtFKwYdQkSJ3IOj3p9O9IK15X6oMVs/wVErdbKeCJjtfVGtv" +
                "jdSeSqN725K9nLllRjjlaYieyvQPxHqIzNaA0Gl5jRTjLIBWWAVi1jZQs9lBXjXTUHTBko69L9lB5P8u" +
                "iq3BmdjPdtdgu2mw3m4N3ENA2U47kOp8C35rDGh11EUXtYkldop6QgyPhUXisK+gM0FEcasQrqGSZVwt" +
                "4Q8Y1qynhqxFNFS/nDN3Sb8++CfbkOUapeNcNV8upD8nRW0DO2TO1FaN/4Nsg/WvEJQkSJl4hiphYJ7T" +
                "FW7QactgI+hzZlGOVOGDBTIWlv3x/OS5xLRjJvCDGxgr/kYrNhS004FmoX70+K/dt9zqQ8CTYBWgonPp" +
                "0RvfBQdwRKAGh8bBAv1lHMVXFHiDh/+HsfsNYyt2tGafHMbC8P+lMHZbFGu3mD6p97K7o1EJMNn89rOo" +
                "CR9VX/eDkHTRHcgP3CvEawBSHwETdiYbTpDEfP1pMEmINY9aa90HSLqlwPdO0XmrvYBxq0QWC8EBU3oT" +
                "6kFalhgq0UQ4duBvsQlgA9FjjcCk4Eash/OVswgHUaIm5D5/aM7vW7xpwdPGLxwmC+6hGwF6gsW5XZRV" +
                "SOY86ub0AZoeBCG2Qnhla8Ojlr1d9AKG0SqvFqNWhC6Q9LqKUqLh6N1X0IISfThv44yAyPTYGXnQ8+CW" +
                "YnhYGJVyhAxqHtSMKRtMhBnOzZK1lnRwReXUT2jcUoiNFKfqgXmMfMGTf+VKqk9ktlir4Y3TPOyGJGPd" +
                "D3awVEjpXs1JoxYFpH3jCQHOQu2oT7QbFmeoZ9iCKtxSHMFTOawP0GWN3MYMcOizc7UCXU05FPTNbb8w" +
                "tw80Azj+2Ck0rh+Au5FfImzKyuJEtE4Jnc0A7p+Yq9yt6vLLj78Pn9z2xSc+zkr7TDvENWoORZP4zPYR" +
                "CUWFxXsxvQIPxHqEFnbvNdZ+CbDvnbW56RL2GQfEahSEiuMIlYzz2qm9R/8dEYRMc6lbVRYV4ZIUSCZQ" +
                "btoRPtoSJO1Il8FndVoqtugP57oNAkmCra5JUMCFyyB/WCYmBp2DwWsoSyKO8imj3oZPLL+bYd1GZbnx" +
                "nXal/f3XtpxtUuUbjJ3rh510+K0h8ZTOwU1gkclGBU7MfWexdUekb8a+VSXDQhGojUgIv1RPw44liewF" +
                "oiuXOGzz9pZTKYisdIuQ/NZw9yRhMd0yDNU6R+Vyds3GjDSEW4Ngo9epW5YoNOwNkjrZR4tEk6kEnEFv" +
                "vEbx8eTk5PEXXOZcgurGSjzDUMSXX6eFy3kxB78rNMxB9wCXGgoct6srSF+tgjP787LmZk1yqCudn74+" +
                "++n08Zci02LBOEWIEKzZgwofWIXp+l7Pn8sa+vQ6KciJXWiEfPv29M3J44c+CDdr7l5OVukjKq685fut" +
                "lgODA44I+xbqxPmyrDgis5NKa0BiEESz0mXUlb9tthlN0fqBJhNlUXRzTAbPFuF0SjMuHlmzhYEufL6r" +
                "oPjxoNLb+8t/zNnTV6fPLnnN5K9P9n+onGd/3kSWoCntlYkkPB/IEMaIBHnihapAcDArRmyhLQg+ptqX" +
                "rGtz7ZHBTtiG3Cgqrmzd+GqvMJQ3Or9pUslplJgLIlZuknEI9qASCCbjNis+wQo4fXVx9uYId4ACYn33" +
                "5PX3RgmgUVWbMMJs7QCtYyoG6qCVBpVrUg8JZWBOpWpge2tr08WPBDE5d4V65coOzWf/2KeG94f7z1jZ" +
                "nDzd75v9wrkKb2ZVtRgeHaFXEmXQdrX/z8/q01cYOypBgcu5j4y6e7664ea0tMDKMa32MSnlDQUHfVl/" +
                "92iSwVXHaYZ+jE9Pu+yV3WJVYjh2O3mqtiFEKBX93q+sQJPGtYSeGOK07SA3m9iG8MJKv1TIDE2tAHlH" +
                "FeBdVwXDr/7+zSMdwdSrp4wYt83xvl/p4ofv0R9GicAmcb1PGwtf/J69CCOUtixl9lfT8vhrfcOW/NB8" +
                "9ej4oTxidMEBKJ/dyo9A2l+hc9l5zQqFgoQFwumCfsWd3GXG73KyWLnFfjBomPZdNcNuqxbA0Ym66dih" +
                "61IuaGl9E69RWkvRBnOzJlw08igHZhH63zCrAONR4YxDCQBiDPhM6eKJWjh/0cf/gL95Q+4b8/TsF6Qx" +
                "/X3x9sXp+SlSiz4+e/f9yzcnp+cI5f7F2ZvTx4+Ct4f4JFmGPPlRWqWFkID2MWBF666uDm36j82IMIcn" +
                "22S/PaE1bKidFcIVOatRJWiqprpuQqTab+bsa3KTgyWPCSG4sKro4Ze+eafNsl/bPFPJApj0io3nqBuD" +
                "CJtq+aD0QaPb0S+oSJqnd7Wu+fQrs3iLJdW/50oaLdx2hk38608PED2VT0Q0hmKVGycW6ZIseJijFhT4" +
                "ULqj8ycnL3+8YIXUWjNsstDkButtTtWKmo60H+TeQSgPpdXpl/rVRCg4Bqa5cLBBd/Ti9OV3Ly7NAWn7" +
                "h8NGJj0ba2m8kWm2Aa+CL5gD+sKhrsc4F9ZR6fw6+tBa57ZVeBEh6E63z4OT3WsiZWszIHzC/KbO7/ok" +
                "26ppIRBY7mPgMGXR2JDolPMJNmnvy0XfN5E/90oNTtpRZm1SHeFZjzaeujW4UQwH3k2I2wYBt1yyYzHa" +
                "7X3JbrE8aN2FE223Lkigr6kXPerTjdYNnta4+xJQr5dtt6Tap5DohYTThUbcj1zjuPsURGgZEk+LVcJJ" +
                "Bgiga3U+3DzOp6ggvm1FWH/FVa5e0tI3/t8LPEdAsVO+/9DjGpeegDRtPS0u0GrchRkBe6Hy4xU8yQXg" +
                "ZofO5SxLJ92TqoIYO1QWxEKVVzOl98/fHyuf9mYkfUB7H9wKLt95tKZHT/A0xfcN/q+bBNGN+Zztv89N" +
                "/Af+k5jHRhB1ZIaPYeB28v4LXpYe149f8jGuHx/yMakfjz8015gffZB3vd6/AETv5SVMNQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

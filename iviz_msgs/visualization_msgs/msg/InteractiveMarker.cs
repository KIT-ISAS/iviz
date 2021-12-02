/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class InteractiveMarker : IDeserializable<InteractiveMarker>, IMessage
    {
        // Time/frame info.
        // If header.time is set to 0, the marker will be retransformed into
        // its frame on each timestep. You will receive the pose feedback
        // in the same frame.
        // Otherwise, you might receive feedback in a different frame.
        // For rviz, this will be the current 'fixed frame' set by the user.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Initial pose. Also, defines the pivot point for rotations.
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        // Identifying string. Must be globally unique in
        // the topic that this message is sent through.
        [DataMember (Name = "name")] public string Name;
        // Short description (< 40 characters).
        [DataMember (Name = "description")] public string Description;
        // Scale to be used for default controls (default=1).
        [DataMember (Name = "scale")] public float Scale;
        // All menu and submenu entries associated with this marker.
        [DataMember (Name = "menu_entries")] public MenuEntry[] MenuEntries;
        // List of controls displayed for this marker.
        [DataMember (Name = "controls")] public InteractiveMarkerControl[] Controls;
    
        /// Constructor for empty message.
        public InteractiveMarker()
        {
            Name = string.Empty;
            Description = string.Empty;
            MenuEntries = System.Array.Empty<MenuEntry>();
            Controls = System.Array.Empty<InteractiveMarkerControl>();
        }
        
        /// Explicit constructor.
        public InteractiveMarker(in StdMsgs.Header Header, in GeometryMsgs.Pose Pose, string Name, string Description, float Scale, MenuEntry[] MenuEntries, InteractiveMarkerControl[] Controls)
        {
            this.Header = Header;
            this.Pose = Pose;
            this.Name = Name;
            this.Description = Description;
            this.Scale = Scale;
            this.MenuEntries = MenuEntries;
            this.Controls = Controls;
        }
        
        /// Constructor with buffer.
        internal InteractiveMarker(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
            Name = b.DeserializeString();
            Description = b.DeserializeString();
            Scale = b.Deserialize<float>();
            MenuEntries = b.DeserializeArray<MenuEntry>();
            for (int i = 0; i < MenuEntries.Length; i++)
            {
                MenuEntries[i] = new MenuEntry(ref b);
            }
            Controls = b.DeserializeArray<InteractiveMarkerControl>();
            for (int i = 0; i < Controls.Length; i++)
            {
                Controls[i] = new InteractiveMarkerControl(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new InteractiveMarker(ref b);
        
        InteractiveMarker IDeserializable<InteractiveMarker>.RosDeserialize(ref Buffer b) => new InteractiveMarker(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref Pose);
            b.Serialize(Name);
            b.Serialize(Description);
            b.Serialize(Scale);
            b.SerializeArray(MenuEntries);
            b.SerializeArray(Controls);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Description is null) throw new System.NullReferenceException(nameof(Description));
            if (MenuEntries is null) throw new System.NullReferenceException(nameof(MenuEntries));
            for (int i = 0; i < MenuEntries.Length; i++)
            {
                if (MenuEntries[i] is null) throw new System.NullReferenceException($"{nameof(MenuEntries)}[{i}]");
                MenuEntries[i].RosValidate();
            }
            if (Controls is null) throw new System.NullReferenceException(nameof(Controls));
            for (int i = 0; i < Controls.Length; i++)
            {
                if (Controls[i] is null) throw new System.NullReferenceException($"{nameof(Controls)}[{i}]");
                Controls[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 76;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetStringSize(Description);
                size += BuiltIns.GetArraySize(MenuEntries);
                size += BuiltIns.GetArraySize(Controls);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarker";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "dd86d22909d5a3364b384492e35c10af";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1aaXMbNxL9HP4KlFQpSxuKuhxvwl19oEXKZkXXSlSOSqVYIAckEQ8HY8yMKPrX7+sG" +
                "MAcpxamttZNUPJwBGn3364Z3xUgv1eHMyqUSOpmZTmtXDGdioWSkbCfHR6Ezkalc5EYctUW+UGIp7Qdl" +
                "xUrHsZgoYVVuZZLNjF2qCERyAxo6z4SjahKh5HQhiFaWq7QjfjOF22zVVOlHxURTkykxUyqayOkHIpDw" +
                "64xIMCHi7Aav7Epnqi3WILLU80VeUgmbaasUkZ7NlFVJXu2+MFbYR/2JpIBQgX86ZlpYXvtqpp8gBG95" +
                "xWJP1rygyKCO1ntWi9dOi1SV6FzLmLnviF6cmbaI1EwnKnNS6UeT4yu0IqAgYU0uc22SrNOaK7OE5tbj" +
                "ZTbPDm9JfKLCVCPwomdrncxFllv80RFXRQZmlJjHZiLjeC2KRH8syGjYQEflJtVTPMnciQdtZ3LuzYfj" +
                "84U1xXzRaTmKIoGIdNj9wtgcTGdTq1PiTez9W7w+EtOFtHKaK5vtl3tqq3jrVMZ0MPEFBUUsIsSXRZyL" +
                "qUlya+JM7Pk3Z8cgNIuNzE9PREZbiUYPRliqpBAyiURWTPgZ/FoNFcosM1Mtc5Be6XzhBWP367SusHKA" +
                "hevf/2AKY7+LqF5qaMvMKiYinaWxXHseG3SGCYSEpPChK3517jaBbNjeap39n/9pXd2/68K4kTO/cyxS" +
                "aQ49SBtBolxGMpfM7wKOruxBrB5VjE1ymUIQ/pqvUwVn2hUjEgn/zVUCadhByCIwztQsl3CWKdTog7C2" +
                "30WaFKm0uZ4WsbRYb2ykE1pehg7+yxTcLZkqMex3STGZmhakM5ykk6lVMiMPGfZFq4C3k4nVx9buaGUO" +
                "8FPNETbl4c5Lwax6Si3cFMzIrIsz/uGE64A2lKNwSgT/4Xdj/Mz2BQ4BCyo1yCh74Px2nS/gshQAj9Jq" +
                "OYFDgjC8KwbVV7Tp1X6NMrHdhe8nJpB3FKsz/g7ZpKRLMh0sYLOYg7WYQ4FYmFrzqCMs9eljGmuKwVhP" +
                "rLTrFudVPrK1e8FZkj2SLaK33D5EH1tjrKMv5Y3bGYniE/mVjAT2OXNRVHGuhpZmVkGMVE6Rj+Fl9Dry" +
                "3zWvpZg2iEm/tyNat5wKw4LWfwpIaROmW637WgKClRA5FOlSJz5rB/4hC0KDWW6I69LYm9fiqXxal0+f" +
                "vg77leqCDKWh4EENfTaZp18fK71T3e60PiNReFp9KdkedVbIWH9ifp2AZX6HfOVzKGvgeFcMCFdspe+y" +
                "8i0kK0JaK9dkyi0inDh7sD2iehoMHlZR/cnNXBHk8IUPZU5iBxWbQ1+q6M/wqHLUX6iZ8AWVWvKlNY6n" +
                "UDeWsRHcaRZL8jrmqiNgOQQ/2ya3xTQvsLpuSpdC5vqREgDjqLJAkl4gHjJuUiwnYJKiTYodpHJ8Rp7Y" +
                "ETOt4ogOMakvHmVlxTlgDyQMYRWuruVGcSaOsIs00diAugBI5ZKSw2eaapajXKK1FbRdSh62eDjnw4yE" +
                "CQq45VNBJpy0JKQj01QhlU4U3JM51ShMCx1HWOxs/4TsGSuqGgeCWT7Fo2gKwW9ynSN7n4mdWZHsVMtP" +
                "PrfcmonJaxteb204aW5I7Ult+fefWw5b4wk78PodvBc6dsZld4j1B1cVSELQBPPugdliSviBI8OjI8fw" +
                "sc9FJLgFAYgNzyHjejzpYWRlUhDwsKftMBl8OY5EAiOTPeAZocBTJeLTEDWcNllW94sBaDiuLfQMHYHj" +
                "ykcLWBi6hT6i3FK3Jm/4K/YTEq9USa1IyUX5uhUIHXpSrOlQPN0Pinc7L5acJD02IiF1EjFA4mjzL8eE" +
                "rcTeRMVmtR/I+G9E6Ly+jCPcVQ/+DSXgd0owiZKH5rBZqKRSjfd4lhjwYPpBRdykDAb9t73znwj/UDwn" +
                "2wnuIjQ6IdFx9JYQeE0q8h0bn8YvXwEd8gF3N/d3D9ddYC8CcIr8PLOIDEdFltpBzgG7Zdw6PXFCEXty" +
                "Yh7Vvqd22Xu4Pn/fJBjLIpku/ieaZNYfSjWcHfkXju2z4+qnO/fsxL+pW+0rVqmXugfSTa0eh5gqoa/r" +
                "mqq2pCw1TmWkpZKwb1Webw2rhsYfQWahFjuBm9AxQJN6TpXHh/qjjPH/4Bz4c6uBnlmzZBO9exhyhYDr" +
                "gmtiBg5d9sMiVZbQQ0Yr6hyIPdVByyrFpMhzfGP33t/oPEG4X+uUYwNwvdV7iD2rUC95SGCqOUEj4XAL" +
                "s6mrffKy1UIj7YGviaJzufmmXgNOxyMLUHZB13cdaterN19vdOcvwVQaSdRg1tJEqlv1nAuzaqAwdNSJ" +
                "Bx3D6/eDu+Goi5lEHG8sg3DP2B6ZYfjrAK3XT0qljfVuZEFe5YcRaCxySuM/Dwe/jC9658NruG4vJidY" +
                "H3wScLjE5wyAGoWA3Hvqkhet0HdisNKN1Sxvi0/dIi3D0fNLRVKECCV28CJEZO00rnX+dY3TMenHTU28" +
                "eF5nWy7sOs7rm+tBt4Ln5Fh4NAk6zqWSfqLSCMl/iYRyOsg/5ZwQSddXg+uHLiYCqKdEsU1eKSyNjg7Y" +
                "MV3RoLrDCqctbx9GoxvkyEGsKHGh70soWkkxbo9zm6ubnwfj3q/De3AZ/EnI2MDXnDs/HcgnnZVLby97" +
                "LFG5Fj7qFpZ2cTl11BsFwnfstMihpoDbPkvWre9SQZpwPFWHsa/X6AV7kiLqxiQd1Wzp5Gcz+u9BUsJZ" +
                "5abqGEJHITGXhzEIqi11n/D2DTjfOe3vVH7uHQFA1NgPYdYD3zfIM9/dvx9ejL47H91dwpvcx9M+jewy" +
                "YystnPbrmqV2hycTtJTbnppmaanX6/Y6gUIEB6Hj4btzfKGO1kGbch5Sk4epXRQYY7056N9cMMUI+ZNy" +
                "lGcntMJh+lc7wknhTwrW8fJAU/9sapXf/bCtU/fhxxBzNbX6mHNTXXQY3JyUOBx5w6UXPw2VGGCSoyOo" +
                "NEYe2OUxC+aOhaYYSTDOhEoaCQondFoTg/CUMcB/Ng7b6VhXFLfrHXqzKSAoFOUieGPMQEq+VJKHwwRh" +
                "lmkOQGeo7jA7G1NGwSMYzq2UOEyOPMxQnNqrIKKPYjefdd0YdYcTDTsBh23WnjYja0oW5aR4Y85dK1x+" +
                "1u3q0nb6JnEOHOBFX+qmyDxc8LP0ICKQMtAC17hwKBODarhyZSSVEJ8vis/y4GxB41KnEecXSSN1kzUd" +
                "3g7wgLyGADxN3COTvII+KPlWNwGE4J15kYjnpNoygKfQipUwsVq5OUqnYRGWEQ3GShSYhJF4MEqoNTW/" +
                "AjxXKeAwAX23d7xZh//GGJs0xECmgVUiozIyNUOWnSuAUObcdX+YWNXQQc9NVzFmr58zkTz2cmGymdGe" +
                "nZx/NXDqDE7aQUe5yPO0e3i4Wq06QOgdY+eHK/1BH9KVyGHfBeaIxsl+G6esv9w0KtD1AHGEHdm3p71v" +
                "T47eYhQ8xZ/3CwlqXKWX1MXT/ZJdepCTIJS5L6vfVbDfEOmQyXp3dze/lD3A+cPbQdkB3N/CTQYl/D//" +
                "7XJ43R/cnZ36F/g5GN+P7oa3Z6/rry6H96Oz72sU3Zs3DbLuXci+tzfD69H9WUi8o8Gvo3EtZs5+LKvo" +
                "/fvx3eD+5uHuHIwGtsFD7/rdpSd6fFwK1++Xol3d9IcXv5U/+4PLwagSzv3sXV5CuuYlFPX+z/2zG74z" +
                "tGre8XkblHA8e4mII3SNfa4yIsjhJPRARjOTPzE1A8VOp+MuGxC8iKo/0fqxjdmarl2n+wFCMaEHoRbA" +
                "oz7lCbXCUEF8881fcONPxdQBR86K+NlDiWpSss0OKBOd4noj59ETHDtSseIfFQeCkIP1fHAX/yInmLj6" +
                "Lr/BvA/8l3cdCRlFh0gMaOF4SMu72+KYbsmQ5HkGsd8WJ44/mglVi06rl0ib7m320j3iM4ff1qqEZ7u5" +
                "+We8MvbUXc5tbHZ3fY3d4riNfxmLZ2VB3isoCwFS0QeoU2SYNVtFAxR/13VuYmPv3r3lwStM0zyHv4rf" +
                "jzpHB8edoz9aUWFdxoj1DAaD2zyr2PeoIQy8a+z5yVUsedLFg0TXCErkLYoBus5Ze6XSmPTIy0JLMe1y" +
                "tcfduqDKA/Q/f7afY4Va7o9FOeStB24rKnsHV8Tbl+W1q3I6dF1elKOq3VCv40LLKd7NmlI11ZiaIDuj" +
                "YmawIMMiH05LdN+oZC5ntWtpsC0QqPtb/gIegAi4OGdf5sRdP4jk+1hY1494FSLVjxQJ1+N6ETjKA5pq" +
                "R2Ds+oZaHBmnCxlw6BoghVh9xrX47paOImjgtnLfWN5Suw7Rw5cwIsS7l9Y3UvvmRthrMQZ4NYXF3RB7" +
                "Db/C7rGCIBFuAoFbEA1ULL/O9ZCP5efuhsQjf2veCjHSGeYbHTZPh/xO/ksVSFHuJg8wznJMEWRlEEU2" +
                "oRGsRKON9QTUMXxKUxCTjVaIfd7NiNquweBVHJnERQBZ1BP5SU0FHIim8MKhl5mdOBDJPLvD3Aw79Fr7" +
                "HYpPwq6MWtewkb85Z8Qa+OIb3tzgr2/4DoP52IqU6m9VJAhRiSnAZy7Ovoyptx2+/IsVtnyal0+T8km2" +
                "Wv8F5XxfpvMjAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}

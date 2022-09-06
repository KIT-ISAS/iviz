/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract]
    public sealed class InteractiveMarker : IDeserializable<InteractiveMarker>, IHasSerializer<InteractiveMarker>, IMessage
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
    
        public InteractiveMarker()
        {
            Name = "";
            Description = "";
            MenuEntries = EmptyArray<MenuEntry>.Value;
            Controls = EmptyArray<InteractiveMarkerControl>.Value;
        }
        
        public InteractiveMarker(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
            b.DeserializeString(out Name);
            b.DeserializeString(out Description);
            b.Deserialize(out Scale);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<MenuEntry>.Value
                    : new MenuEntry[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new MenuEntry(ref b);
                }
                MenuEntries = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<InteractiveMarkerControl>.Value
                    : new InteractiveMarkerControl[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new InteractiveMarkerControl(ref b);
                }
                Controls = array;
            }
        }
        
        public InteractiveMarker(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align8();
            b.Deserialize(out Pose);
            b.DeserializeString(out Name);
            b.Align4();
            b.DeserializeString(out Description);
            b.Align4();
            b.Deserialize(out Scale);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<MenuEntry>.Value
                    : new MenuEntry[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new MenuEntry(ref b);
                }
                MenuEntries = array;
            }
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<InteractiveMarkerControl>.Value
                    : new InteractiveMarkerControl[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new InteractiveMarkerControl(ref b);
                }
                Controls = array;
            }
        }
        
        public InteractiveMarker RosDeserialize(ref ReadBuffer b) => new InteractiveMarker(ref b);
        
        public InteractiveMarker RosDeserialize(ref ReadBuffer2 b) => new InteractiveMarker(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Pose);
            b.Serialize(Name);
            b.Serialize(Description);
            b.Serialize(Scale);
            b.Serialize(MenuEntries.Length);
            foreach (var t in MenuEntries)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Controls.Length);
            foreach (var t in Controls)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Pose);
            b.Serialize(Name);
            b.Serialize(Description);
            b.Serialize(Scale);
            b.Serialize(MenuEntries.Length);
            foreach (var t in MenuEntries)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Controls.Length);
            foreach (var t in Controls)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Description is null) BuiltIns.ThrowNullReference();
            if (MenuEntries is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < MenuEntries.Length; i++)
            {
                if (MenuEntries[i] is null) BuiltIns.ThrowNullReference(nameof(MenuEntries), i);
                MenuEntries[i].RosValidate();
            }
            if (Controls is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Controls.Length; i++)
            {
                if (Controls[i] is null) BuiltIns.ThrowNullReference(nameof(Controls), i);
                Controls[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 76;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Description);
                foreach (var msg in MenuEntries) size += msg.RosMessageLength;
                foreach (var msg in Controls) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align8(size);
            size += 56; // Pose
            size = WriteBuffer2.AddLength(size, Name);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Description);
            size = WriteBuffer2.Align4(size);
            size += 4; // Scale
            size += 4; // MenuEntries.Length
            foreach (var msg in MenuEntries) size = msg.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Controls.Length
            foreach (var msg in Controls) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "visualization_msgs/InteractiveMarker";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "dd86d22909d5a3364b384492e35c10af";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71aW3PbNhZ+rn4Fxp5O7K1M39Js610/KJacaOrb2nIv0+loIBKS0FAEA5KWlV+/3zkA" +
                "KNKym87OJk5mTII4B+d+g7fFSC/U/tTKhRI6m5qosy2GUzFXMlE2KjUtF6JQpSiNOOiKcq7EQtoPyoql" +
                "TlMxUcKq0sqsmBq7UAmQlAY4dFkIh9VkQsl4LghXUao8Er+ZygFbFSv9oBhpbgolpkolExl/IAQZLxeE" +
                "ghERZddYsktdqK5YAclCz+ZljSUAE6gUiZ5OlVVZuYY+N1bYB/2JuABTgX46Jq4s73011Y9ggkFeMduT" +
                "FW+oCoij857F4qXTIVFlutQyZeoj0UsL0xWJmupMFY4r/WBKfNVEBx1vSllqkxVRZ6bMApJbjRfFrNi/" +
                "IfYJC2NNQIuernQ2E0Vp8SsSl1VRErWz1Exkmq5ElemPFSkNAHRUaXId40mWjj1Iu5Azr76MFq2pZvOo" +
                "4zCKDCzSYXdzY0sQXcRW50Sb2Pm3eH0g4rm0Mi6VLXZrmMYuBo1lSgcTXRBQwiyCfVmlpYhNVlqTFmLH" +
                "r5weAtE0NbI8PhIFgRKOHpSwUFklZJaIoprwM+i1GiKURWFiLUugXupy7hlj84s6l9g5wMbV738whrGH" +
                "IqwXGtIy0zURiS7yVK48jS08wwxMglPY0CUvnTkgoA3gnc7p//mnc3n37gTKTZz6nWGRSEvIQdoEHJUy" +
                "kaVkeucwdGX3UvWgUgDJRQ5G+Gu5ylVBxj0ilvB/pjJwwwZCGoFyYrNYwFhiiNE7YQPeeZoUubSljqtU" +
                "Wuw3NtEZba9dB/8LBXPLYiWG/RMSTKHiimSGk3QWWyULspBhX3QqWDupWH0U2+L3W1Mc/tHZHi3NHtbV" +
                "DP5TU+HMFVSrx9zCXkGVLE5w2D8clxEOgZQUjktgSLw2xmuxK3AaaFG5QWjZAQs3q3JuXMx4kFbLScqm" +
                "DzNLgfUVAb3abWDOGHUmMxPQO4zrM/4O2qzGSzztzaG8lL22mkGS2Jhb86ATbPVxJE41OWOqJ1baVYcD" +
                "LB/Z2T7ncMmmyarRG/Yf3JDVMtbJlzLLzdBEjopAS0oC+RzCyL04aENKU6vARi5jBGaYGy0n/rvmveTc" +
                "xuoAG4nODcfEsKHznwpc2ozxrvd9LQZBSnAhcnmpsyIkJR14lT6Mt9h18ezNa/FYP63qp09fh/y16AIP" +
                "taJgQS15tomnt49ruVMCjzqf4Sg8Lb8Ubw+6qGSqPzG9jsE60IO/+jnkt4jC/YAKjI04XqfAuWRBSGvl" +
                "ilS5gYQjaA+6h1fHQeFhFyWi0swU1R4+AyLfSUBQ1tn3OYt+h0dVIhFDzFRoUM4lW1rheHJ1Y7lIgjlN" +
                "U0lWx1RFAppTDEQZv4rLynKgqVXpQshMP1AA4IKqzpQkF7CH0JtViwmIJG+TYgsxHZ8RJ7bEVKs0oUNM" +
                "7rNInWItV0BAYaho4TRbA4pTcQCoy1ZOBgAShLI+KLlCTVPycpjrsm0pV2vOA4iv67ybETNBADd8KtCE" +
                "kxZU8sg8VwilEwXzVO4klCY6TbDZ6f4R0TNVlDX2BJN8jEfRZoJXSl0iep+KrWmVba23H31uuzUTUzYA" +
                "Xm8AHLUBcnvU2P7957ZD13gCBJbfwXohY6dcNodUf3BZgTgEThDvHpgsxoQXHBkeHTquI/ucRIJZUCXx" +
                "xHJIub6w9PXkWqVA4OufrivOYMtpIjIomfQBywiZnjIRnwav4bDJvLo3rkTDcV2hp2gNHFXeW0DC0G30" +
                "HuW2uj1ly14BTyX5WpTUk9RU1MudgGjfo2JJh+TpXsjf7axacJD0RRIxqbOEKyX2Nr84piJL7ExUapa7" +
                "AY3/RojOmtvYw1324HcIAe851UsUPDS7zVxla9F4i2eOUR7EH1TC3cpg0H/bO/uJ6h/y52wzwJ2HjicE" +
                "OvbeuhZekYh868an8eIrlIl8wO313e391QlqL6rkFNl5YeEZDouspYOYo7K13zo5cUARO3JiHtSux3bR" +
                "u786e99GmMoqi+f/E05S6w+1GE4P/IIj+/Rw/erOPT3yK02tfcUs9VIbQbJp5OPgU3Xp69qndX9Spxon" +
                "MpJSjdj3LM/3iOvOxh9BaqFeO1OuEUA1qWeUebyrP8i0UrVx4PdGJz21ZsEqenc/5AwB0wXVknNkUTfG" +
                "IleWqodCmKxFgdhREXpXKSZVWeIbm/fukxYUiPuNljk1KK43mhCxYxXyJU8LzHpg0Ao43Ms8ldUuWdly" +
                "rhH2QNdE0bnchVOvAaPj2UVKr5EjhFrVEy/ectV5qdZql6k0m2iUWQuTqJN18zk3y1YVhtY680XH8Or9" +
                "4HY4OhHnqD2ebANzz+gekWH46wA92E9K5a39bnZBVuWnEgWxCYCfh4Nfxue9s+EVTLeXkhGs9j4JGFzm" +
                "YwaKGgWH3Hk8IStaogHtitVJqqZlV3w6qfLaHT29lCRF8FAiBwvBIxunca7zyw1KxyQfNz7x7HmZbZiw" +
                "az2vrq8GJ+vynAxLk62lVMBJP1ppueS/RGZ4r3osOSCSrC8HV/cn4oLyKWHsklUKSzOkPTZMlzQo77DA" +
                "CeTt/Wh0jRg5SBUFLvR9GXkrCWavEasvr38ejHu/Du9AZbAnIVMDW3Pm/LgnH3VRb7256DFH9V7YqNtY" +
                "68XF1FFvFBDfstEihpoKZvssWrf/hBLShP1pfRjbegNf0CcJoqlMklFDl45/VqP/HjilOks0F90xVB2F" +
                "wFwfxkVQY6v7hNU3oHzruL+1tnNvCChEjf0Qhj6wfYM4893d++H56Luz0e0FrMl9PO7T7K4wdi2F435T" +
                "stTu8IiCtnLb05AsbfVy3dwnkIiqko+H7c6063hle6bY4IexnVdpKt7s9a/PGWOC+EkxypMTWuEwBmwc" +
                "4bjwJ0VNaR2Tc/2zLVVe+2FTpu7Dj8HnGmL1PufGu+gwVLdZhyNuuPDix6IyLTgtwan0JKUY4msWJWaV" +
                "Jh/JDLfCrQCFE6LOxBiCR/FfjAM4HXvp8T/Nd+jNYpSgEJTz4CdjBhLyhZIPfjKiFnm5IhywBybnybhR" +
                "8AiGYysFDlMiDnMpPqoH10XwYjeodd0YdYcTXdJQZiP3dLmypmBRj4yfDLwbicsPvV1e2gzfERMzdJ/d" +
                "HNkNF/xQPbDY5Qk657hwKCODaEonNCbq80nxWRqcLmhu6iTi7CJrhW7Spqu3Q3lAVkMFPI3eE5O9gjxk" +
                "VjauBKiCd+qVlGTWU1vYGaRiJVSslm6OErU0wjwWlCernLrZgpQSck3DrlCeqxzlMBX6Dnb8NA//jXk2" +
                "SWhZD8qD5SRGFaRqLlm2Lo2/mHDdXySa1UHPjVl13DpnInnslbVV7yPasyP0r1acXoba4Q4d5bws85P9" +
                "/eVyGaFCj4yd7S/1B71PdyP7feeYI5orezAOWX8JNKrQ9aDiCBDFt8e9b48O3spCx/h9N5fAxll6QV08" +
                "XTTZhS9yMrgy92XNSwu2G0IdIlnv9vb6l7oHOLt/O6g7gLsbmMmgLv/PfrsYXvUHt6fHfgGvg/Hd6HZ4" +
                "c/q6uXQxvBudft/A6FbetNC6tRB9b66HV6O70xB4R4NfR+OGz5z+WGfRu/fj28Hd9f3tGQgNZIOG3tW7" +
                "C4/08LBmrt+vWbu87g/Pf6tf+4OLwWjNnHvtXVyAu/ZtlHjhZzt859KqfdnndVCX48VLSByiK8C5zAgn" +
                "h5HEPiabyZ8qpkATRZG7dYDzwqv+ROvHOmZtunadLgqoigk9SMb3e1z1KY+oE4YK4ptv/oIaf+qwT0dO" +
                "q/TZQwlrVpPNBigznVcUNWn0BMNOVKpKZ4CBAkGVg/V0cBf/IiXbYuS7/Bbx3vFfhjoQMkn2ERjQwvGQ" +
                "lqG74pCuyxDkeQax2xVHjj5VNDcdrxcRNt1q8dKF4jOH3zSyhCe7Dfwzlow9drd0T4DdpV8LWhx28Y9r" +
                "8aJOyDsVRSGUVPQB4hTFxwoZiQYo/tLrzKTG3r57y4NXY5+cw1/F7wfRwd5hdPBHJ6msixipniq+N3lW" +
                "sO+RQ7jwbpDnJ1ep5EkXDxJdIygRt8gHYibUCZXGpAeeF9r6AEvg3ONuXZDlUfQ/f/YwDLxc2PTHTnwZ" +
                "sedAkdkjFT1za964M6dDV/WNObLadRYu9PR0PVsqchXrqcYqTbgLaJDLIu9OC3TfyGQuZnUbYbAr4Ki7" +
                "G/YCGlARcHIuvsyJ234QyRez0K4f8SrN8w4aKVJdrz5SFegLmjVEIOzqmlocmeZzGerQFYoUIvUZ0+JL" +
                "XDqqw30kgZqaMyabO0RfvoQRIdZe2t8K7U8Boa/5GMWrqWysnNXwEqDHCowkiQKBFFwoWX6d6yHvy8/d" +
                "DYkH/ta+FeJKZ1g+6bB5OuQh+a8rrLso4TLOsk9RycpFFOmERrASjTb2F5wvZJ4DmWy1QmzzbkbUdQ0G" +
                "72LPJCpCkUU9UbIuumtgKTxz6GWmR66IZJrdYW6GHXqt3Yj8k2pXrlrxYP0VuqlLetDFN7ylMd3QYTAd" +
                "G56y/vOKDC4qk89enH0ZVW8afP0XFrZ+mtVPk/pJdjr/Bcu9XyD8IwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<InteractiveMarker> CreateSerializer() => new Serializer();
        public Deserializer<InteractiveMarker> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<InteractiveMarker>
        {
            public override void RosSerialize(InteractiveMarker msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(InteractiveMarker msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(InteractiveMarker msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(InteractiveMarker msg) => msg.Ros2MessageLength;
            public override void RosValidate(InteractiveMarker msg) => msg.RosValidate();
        }
        sealed class Deserializer : Deserializer<InteractiveMarker>
        {
            public override void RosDeserialize(ref ReadBuffer b, out InteractiveMarker msg) => msg = new InteractiveMarker(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out InteractiveMarker msg) => msg = new InteractiveMarker(ref b);
        }
    }
}

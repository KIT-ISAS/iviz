/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = "visualization_msgs/InteractiveMarker")]
    public sealed class InteractiveMarker : IDeserializable<InteractiveMarker>, IMessage
    {
        // Time/frame info.
        // If header.time is set to 0, the marker will be retransformed into
        // its frame on each timestep. You will receive the pose feedback
        // in the same frame.
        // Otherwise, you might receive feedback in a different frame.
        // For rviz, this will be the current 'fixed frame' set by the user.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Initial pose. Also, defines the pivot point for rotations.
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; }
        // Identifying string. Must be globally unique in
        // the topic that this message is sent through.
        [DataMember (Name = "name")] public string Name { get; set; }
        // Short description (< 40 characters).
        [DataMember (Name = "description")] public string Description { get; set; }
        // Scale to be used for default controls (default=1).
        [DataMember (Name = "scale")] public float Scale { get; set; }
        // All menu and submenu entries associated with this marker.
        [DataMember (Name = "menu_entries")] public MenuEntry[] MenuEntries { get; set; }
        // List of controls displayed for this marker.
        [DataMember (Name = "controls")] public InteractiveMarkerControl[] Controls { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarker()
        {
            Header = new StdMsgs.Header();
            Name = "";
            Description = "";
            MenuEntries = System.Array.Empty<MenuEntry>();
            Controls = System.Array.Empty<InteractiveMarkerControl>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public InteractiveMarker(StdMsgs.Header Header, in GeometryMsgs.Pose Pose, string Name, string Description, float Scale, MenuEntry[] MenuEntries, InteractiveMarkerControl[] Controls)
        {
            this.Header = Header;
            this.Pose = Pose;
            this.Name = Name;
            this.Description = Description;
            this.Scale = Scale;
            this.MenuEntries = MenuEntries;
            this.Controls = Controls;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public InteractiveMarker(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Pose = new GeometryMsgs.Pose(ref b);
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarker(ref b);
        }
        
        InteractiveMarker IDeserializable<InteractiveMarker>.RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarker(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Pose.RosSerialize(ref b);
            b.Serialize(Name);
            b.Serialize(Description);
            b.Serialize(Scale);
            b.SerializeArray(MenuEntries, 0);
            b.SerializeArray(Controls, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Description);
                foreach (var i in MenuEntries)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Controls)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarker";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "dd86d22909d5a3364b384492e35c10af";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                "Wuw3NtEZba9dB/8LBXPLYiWG/RMSTKHiimSGk3QWWyULspBhX3QqWDupWH3sbI+WZg+vaga3qQ93Vgpi" +
                "1WNuYaYgRhYnOOMfjrkIuCEchVMS2A+vjfFa7AocAhJUbhBRdkD5zaqcGxcqHqTVcpKyxcO6UmB9RUCv" +
                "dhuYM0adycwE9A7j+oy/gzar8RJPe3PoLGVnrWYQIDbm1jzoBFt9+IhTTT6Y6omVdtXhuMpHdrbPOUqy" +
                "RbJG9IbZB+9jbYx18qWscTMikX8ivpKSQD5HLvIqjtWQ0tQqsJHLGPEYVkbLif+ueS/5tLE6wEaic8Oh" +
                "MGzo/KcClzZjvOt9X4tBkBI8hzxd6qwIuUgHXqWP3i12XRh781o81k+r+unT1yF/LbrAQ60oWFBLnm3i" +
                "6e3jWu6Ut6POZzgKT8svxduDLiqZ6k9Mr2Owju/gr34OaS2iKD+gumIjfNeZby5ZENJauSJVbiDhwNmD" +
                "7uHVcVB42EX5pzQzRSWHT3xIcxIQlGz2faqi3+FRlci/EDPVF5RqyZZWOJ5c3ViujWBO01SS1TFVkYDm" +
                "FANRoq/isrIcaGpVuhAy0w8UALiOqhMkyQXsIeJm1WICIsnbpNhCKMdnxIktMdUqTegQk/vkUWdWy4UP" +
                "UBiqVTi71oDiVBwA6rKVigGAvKCsD0quPtOUsxzmulpbytWa8wDiyznvZsRMEMANnwo04aQFVToyzxVC" +
                "6UTBPJU7CRWJThNsdrp/RPRMFWWNPcEkH+NRtJnglVKXiN6nYmtaZVvr7Uef227NxJQNgNcbAEdtgNwe" +
                "NbZ//7nt0DWeAIHld7BeyNgpl80h1R9cViAOgRPEuwcmizHhBUeGR4eOy8c+J5FgFlRAPLEcUq6vJ30Z" +
                "uVYpEPiyp+tqMthymogMSiZ9wDJCgqdMxKfBazhsMq/ujQvQcFxX6Ck6AkeV9xaQMHQbvUe5rW5P2bJX" +
                "wFMlvhYltSI1FfVyJyDa96hY0iF5uhfydzurFhwkfW1ETOos4QKJvc0vjqm2EjsTlZrlbkDjvxGis+Y2" +
                "9nCXPfgdQsB7TmUSBQ/NbjNX2Vo03uKZY5QH8QeVcJMyGPTf9s5+ovqH/DnbDHDnodEJgY69ty6BVyQi" +
                "37Hxabz4CtUhH3B7fXd7f3WC2osKOEV2Xlh4hsMia+kg5qhs7bdOThxQxI6cmAe167Fd9O6vzt63Eaay" +
                "yuL5/4ST1PpDLYbTA7/gyD49XL+6c0+P/EpTa18xS73UPZBsGvk4+FRd+rquad2W1KnGiYykVCP2rcrz" +
                "reG6ofFHkFqoxc6Uq/9RTeoZZR7v6g8yrVRtHPi90UBPrVmwit7dDzlDwHRBteQcWdT9sMiVpeqhECZr" +
                "USB2VISWVYpJVZb4xua9+6TzBOJ+o1NODYrrjd5D7FiFfMlDArOeE7QCDrcwT2W1S1a2nGuEPdA1UXQu" +
                "N9/Ua8DoeGSR0mvkCKEO9cSLt1x1Xqq12mUqjSQaZdbCJOpk3XPOzbJVhaGjznzRMbx6P7gdjk7EOWqP" +
                "J9vA3DO6R2QY/jpA6/WTUnlrvxtZkFX5YURBbALg5+Hgl/F572x4BdPtpWQEq71PAgaX+ZiBokbBIXce" +
                "T8iKlug7u2J1kqpp2RWfTqq8dkdPLyVJETyUyMFC8MjGaZzr/HKD0jHJx01NPHteZhsm7DrOq+urwcm6" +
                "PCfD0mRrKRVw0k9UWi75L5EZ3qseSw6IJOvLwdX9ibigfEoYu2SVwtLoaI8N0yUNyjsscAJ5ez8aXSNG" +
                "DlJFgQt9X0beSoLZa8Tqy+ufB+Per8M7UBnsScjUwNacOT/uyUdd1FtvLnrMUb0XNuo21npxMXXUGwXE" +
                "t2y0iKGmgtk+i9btP6GENGF/Wh/Gtt7AF/RJgmgqk2TU0KXjn9XovwdOqc4SzUV3DFVHITDXh3ER1Njq" +
                "PmH1DSjfOu5vre3cGwIKUWM/hFkPbN8gznx39354PvrubHR7AWtyH4/7NLIrjF1L4bjflCy1OzyZoK3c" +
                "9jQkS1u9XDf3CSSiquTjYbsz7Tpe2R4lNvhhbOdVmoo3e/3rc8aYIH5SjPLkhFY4TP8aRzgu/ElRU1rH" +
                "5Fz/bEuV137YlKn78GPwuYZYvc+5qS46DNVt1uGIGy68+GmoTAtOS3AqPUkphviaRYlZpclHMsOtcCtA" +
                "4YSoMzGG4FH8F+MATsdeevxP8x16sxglKATlPPjJmIGEfKHkg5+MqEVerggH7IHJeTJlFDyC4dhKgcOU" +
                "iMNcio/qeXURvNjNZ103Rt3hRJc0lNnIPV2urClY1JPiJ3PuRuLys26XlzbDd8TEDN1nNz52wwU/Sw8s" +
                "dnlwzjkuHMrIIJrSCY2J+nxSfJYGpwsalzqJOLvIWqGbtOnq7VAekNVQAU8T98RkryAPmZWNmwCq4J16" +
                "JSWZ9bAWdgapWAkVq6Wbo0QtjTCPBeXJKqdutiClhFzTsCuU5ypHOUyFvoMdP83Df2OMTRJa1vPxYDmJ" +
                "UQWpmkuWrUvj7yNc9xeJZnXQc9NVHbfOmUgee2Vt1fuI9uzk/KsVp5ehdrhDRzkvy/xkf3+5XEao0CNj" +
                "Z/tL/UHv05XIft855ojGyR6MQ9ZfAo0qdD2oOAJE8e1x79ujg7ey0DF+380lsHGWXlAXT/dLduGLnAyu" +
                "zH1Z866C7YZQh0jWu729/qXuAc7u3w7qDuDuBmYyqMv/s98uhlf9we3psV/A62B8N7od3py+bi5dDO9G" +
                "p983MLqVNy20bi1E35vr4dXo7jQE3tHg19G44TOnP9ZZ9O79+HZwd31/ewZCA9mgoXf17sIjPTysmev3" +
                "a9Yur/vD89/q1/7gYjBaM+deexcX4K59CSVe+NkO37m0at/xeR3U5XjxEhKH6ApwLjPCyWEksY/JZvKn" +
                "iinQRFHkLhvgvPCqP9H6sY5Zm65dp/sBqmJCD5LxtR5Xfcoj6oShgvjmm7+gxp867NOR0yp99lDCmtVk" +
                "swHKTOcVRU0aPcGwE5Wq0hlgoEBQ5WA9HdzFv0jJthj5Lr9FvHf8l6EOhEySfQQGtHA8pGXorjikWzIE" +
                "eZ5B7HbFkaNPFc1Nx+tFhE23Wrx0j/jM4TeNLOHJbgP/jCVjj93l3BNgd9fXghaHXfzjWryoE/JORVEI" +
                "JRV9gDhF8bFCRqIBir/rOjOpsbfv3vLg1dgn5/BX8ftBdLB3GB380Ukq6yJGqqeK702eFex75BAuvBvk" +
                "+clVKnnSxYNE1whKxC3ygZgJdUKlMemB54W2PsASOPe4WxdkeRT9z589DAMvFzb9sRNfRuw5UGT2SEXP" +
                "XJY3rsrp0FV9UY6sdp2Fezw9Xc+WilzFeqqxShPuAhrkssi70wLdNzKZi1ndRhjsCjjq7oa9gAZUBJyc" +
                "iy9z4rYfRPJ9LLTrR7xK87yDRopU16uPVAX6gmYNEQi7uqYWR6b5XIY6dIUihUh9xrT47paO6nAfSaCm" +
                "5ozJ5g7Rly9hRIi1l/a3QvtTQOhrPkbxaiobK2c1vATosQIjSaJAIAUXSpZf53rI+/Jzd0Pigb+1b4W4" +
                "0hmWTzpsng55SP6jCusuSriMs+xTVLJyEUU6oRGsRKON/QXnC5nnQCZbrRDbvJsRdV2DwbvYM4mKUGRR" +
                "T5Ssi+4aWArPHHqZ6ZErIplmd5ibYYdeazci/6TalatWPFh/c27qkh508Q1vaUw3dBhMx4anrP+qIoOL" +
                "yuSzF2dfRtWbBl//YYWtn2b106R+kp3OfwHlfF+m8yMAAA==";
                
    }
}

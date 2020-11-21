/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract (Name = "visualization_msgs/InteractiveMarker")]
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
                "H4sIAAAAAAAACr1a63PbNhL/XM/4f8Ak04l9teVXmmt15w+KJSeaOrbPVvqYTkcDiZCEhiIUkLSs/PX3" +
                "212ApCy7zdxckj5CgtjFvl/QczWwc3Mw8XpulM0mrrW99Vz1J2pmdGJ8q8BXZXOVm0IVTh3uqWJm1Fz7" +
                "D8arpU1TNTLKm8LrLJ84PzcJsBSOkNgiV4LXZcro8UwRsrwwi5b6zZUC7c3Y2DvDWBcuN2piTDLS4w+M" +
                "IeP1nHAwJibuCmt+aXOzp1ZAM7fTWVHhieAEq1ViJxPjTVY0wM+dV/7OfiJOwFjkgQ4al543v5jYezDC" +
                "MC+Y9dGKN5Q5RLK99ZZlE0S0vcUSy2xhdco8tFQnzd2eSszEZiYX3uydK/AVwlGQk/Ku0IV1WQ50U+Pm" +
                "kOBqOM+n+cE1SYHQBMQJKLKTlc2mKi88/mqpd2UOkoyapm6k03Slysx+LEl/BEHHFW5hx3jShXAJued6" +
                "GjQJEoqZd+V0hsMFp8rAqhx4O3O+AO352NsFkah2/q1eHqrxTHs9LozPd2uwxrYAPdYpHU/kQVoJMwtB" +
                "6DIt1NhlhXdprnbCyukR4ZqkThcnxyonWEHTgVLmJiuVzhKVlyN+BuHeQp46z93Y6gLYl7aYBQ7ZJIHt" +
                "Hbb2sHP1+x+MYhjABPGFhejcpCYlsfki1atA6TqqfgZ2wTMM6x2vnQkUMEd4wnr6f/4DHm7ftKHtRCxC" +
                "zI2lW0Ae2idgrNCJLjQTPYMHGL+fmjuTAkrPF+CGvxarhSELe64GxBj+nZoMLLHNkHagqLGbz2E/Y8gz" +
                "OGgDQfBCrRbaF3ZcptoDwPnEZrS/9ir6LzcwwmxsVL/bJgHlZlyS7HCYzcbe6JxMpt/F5hJ+QBo3HwE4" +
                "WLp9vJspfKqiQIwXFJv7hYf1giKdt+mYfwiPLaCHkAwOSmBRvDbEa76rcA6oMAuHmLMD8q9XxQx2TI5x" +
                "p73VI5goMMPcUqB9QUAvdpuoifQ2fCJzEb+grA/5HLxZjZjY2p9BeSn7cTmFHLFz4d2dTbA3xJdxask7" +
                "Uzvy2q+2tzj68qFAcs6xlC2UdWM3PKFyS9bL0CZf0Do3gxb7LQIxqQtccHgjV+OwDmlNvAEzCz1G4IbR" +
                "0XISvlveS77u4KoBtgU7ueaAGXdsb/2nBLM+Y8z1zq/IJsip3IligLZZCPCRC3AEd2G615gOce7VS3Vf" +
                "P0LH8fHTV+OiFmLFSqU12NSaaNd5oLePtQoo4cP7P4Oz+Lj8gkze2bzUqf3EdAunVS4gRquXmA2Z9Oeq" +
                "R6XJRqivUuZMs0y093pFyt1AIwG2A3OA24+jDcRtlLAKNzVUtIR0idSoCYSy00FIbvR3fDQFUjdkThUK" +
                "ZWmyrxUIoFDgPFdYMLFJqtkUmbCWgh4RHFhThS/HRYntTcVKkJnaO4oPXI1VOZWlAxYRmbNyPgKd5Ila" +
                "PUPQx3fEkWdqYk2a0CluERJNlY1xEAgkHI6qHU7JFaQ6VYcAI2msQSCFoDCTsCVlnuUMJ7irqm8JmVfc" +
                "R5hQFgb3I36iDK75WMITz5pTqaQXC4OAOzIwWCbWIovNbJpgdzSCewTZ1HCC2VdM9wk9q3VWZKmwBSL9" +
                "qXo2KbNnDYjjv4fwbuSKJszLTZjjBzALf9yE+P7vIaB9PBEQfXgDo4bUReFsIqn9IJmE+QViMBKemEBB" +
                "hzccXT0L0lCVdjn9RHuhKuSBTZHSQ5kaqtNa1YQhFFB7UuHBztNEZVA+qQkmU5UIksT4RHgVR1pmXN64" +
                "tI1H7ik7QdchlAVnAhl92Rg9TvbKpmLNmoGAav1asNTw1JRU60IQ4zoI2FjyVfoNbxwW/LScc1wNpRax" +
                "a7OE6y12ybA4pFpN7YxM6pa7FabwUXCdNXdyKJDUw+8QB94XVHVRmLHsXDOT1UKKbsG8o9IYfzCJdES9" +
                "Xvd15+wnqqbI8bPNaHge26oYFdnLq/J6RdIKLSKfx4svUHHKCTdXtzfvL9uo5agkNOQGuYfvCBpdyQjh" +
                "CRRX/i3S4tCjdvTI3ZndiO6i8/7y7O06xlSX2Xj2vyElHf9QSeL0MK4I5adHjXc5+vQ4LjUV+HXT21P9" +
                "CcuokdGjs1U1tfRndetTJSgRHUmrwhzaoae60bptCoewhqi/z2AzdBKKVDuljBXiwJ1O8f9oKfh7o3ef" +
                "eDdnbb1535e8AlMG5UQQDLxqxdXCeKpBctrRpEHtmBb6ZK1GZVHgG1v77ma3S8i7jS49dajdNxocteMN" +
                "ki2PKVw9qVgLR9IoPRTaLpndcmYRGUHcyNDZ3PlTNwMr5LEJUAdH7EpX3A5iLpCYn6rcHpa/PBhplG1z" +
                "l5h23eXO3HKtqkMzn8XKpX/5tnfTH7QxGUnTB/vA42OmgJjR/7WHLu8nYxZrADI5ISsLAxF0LwUH/J/7" +
                "vV+G552z/iWMuZOSSaz2PylYYBbCCWojA0/duW+TUS3R52LC007NpNhTn9rlovbTQDLlV1X5LlGElcpV" +
                "GwdKiowfGvQOSU5xghP4DNLbMOzY5l5eXfbadQdA5oZHl6HNnRsd5jtr3vovlVHwxwH3BQdNlvu73uX7" +
                "NgYSSMWEco+MVXmaZu2zvUqGoTzFwmeY1+8HgysE0l5qKLih1czIk0lGAhQs6d3Vz71h59f+LQiNNqZ0" +
                "6mB/YuP3+/re5vXe64sOc1VthuHKzkpJIfQOOoOI+oZNGaHWlTDmxxELQJuS14j9rD6OXaCBsFIviWNN" +
                "tySqpmpFDKLVsCMyTIVbDVefxZVWjOHVkVJONTbLNyy/Ig6enXSf1Q4QDAMlrvMf4ugJTuEQi767fds/" +
                "H3x3Nri5gH3Jx5MuTRRz5xviOOk2hUydFU9HaC93WE0h094g4s2NCqkL9kIEwJ6n+EKttJRG9VimwROj" +
                "Oy8xWHu13706Z5QJ4iyFsUBQbMLjcLJxhjASjqoUFViCuP75QLi8+MMjopUvP9be2BBv5Y1hBo1Ghpug" +
                "qtZHaJEQFOa2GnNWsn94m8XwhcBCzYPhaGnJeTKMXSGctSiGU8DCyMFzdYoWIx9WCORsyaabeRKt4BhF" +
                "LaQm/v1g2sEivzCa59lUBc0XBWpDR8mKaXowCFU8EZIwzJHFFQjaUupTOxd5DV4u82Rp/6ghHVkoDuXc" +
                "w3y1JyU7hZNquv1gPt/IdmFEL8lsM9gzT/tSRKMZlsk3zzjCJUDkE9U3ag3OjPFUxgYBcbrLmTGl/j6X" +
                "Pk6F6ISGuiKVyk6ytUhPypU6PpYXZEXUGdBlQeKyF5AKRen6HoNbA1E1QvaURFw59xjC8RrqNksZ6rTW" +
                "NMOcontZqpJGdMQltBPTU9PQUPObBcpraiEEevhICv+M4TvJiquhtYIncSZnvXPh8+wdylqmX/pNGqU1" +
                "youOjIFxQ9A8aaR5ICfe8zDoPTXx/3rFruifhYQOdlYUi/bBwXK5bKHybzk/PVjaD/aAbnYOuuKwA5p+" +
                "BziOa38JNCjRUaFiiRD5tyedb48PX2NqPcbftzMNbJzb5zRFoLsyPw9VUgYP58avedfCNkSo61jXubm5" +
                "+qVuLs7ev+7VrcXtNaymV/cVZ79d9C+7vZvTk7iC997wdnDTvz59ubZ20b8dnH7fRCtLr9Zxy2IVqq+v" +
                "+peD29MqSg96vw6GDV86/bHOv7dvhze926v3N2eguGIApHQu31wExEdHDU673ZrPd1fd/vlv9Xu3d9Eb" +
                "NDiV987FBbH64HqNZg+P/Xkev3OZtn6HGfRSl/v5U1gE0yUAJakiCsBy6IE06UZ/YqAHlK1WS+5L4Nzw" +
                "uD/RZ7LiWcUyKKD7DaqFYpdDLUYoIU1AtL0VRxrqm2/+gpxwLCYeOHNSpo+eSmizim42S53ZBW5oCp6I" +
                "wdwTkxp+qUlQVHb4SAgPDp4kBaPhMFhYJz9EhafBDpVOkgNEDTSKPE5m8D11RPd+SAc8+tjdU8dCIc2m" +
                "6k0n9SJCq6zmT96RPnL6dSOfRMLXoX/GmvMnct/4AFruL9fA1dEe/uHaPq8y+E5JAQolGX2ASFWOubg3" +
                "PLkJ13ZnLnX+5s1rngxDP+sH8Vf1+2HrcP+odfjH9lZSeokmqZ1AbbCeR4X7FrmG6/gGgWF8lmqet/GQ" +
                "U3pNjZhGvkA3UqsgWJriHgZuaCtGbiFFya0RqgJ0EY8fHkZpMfWHc5E3GXRfQFEItHAHvvmjgMYvAujU" +
                "VfV7AM59V9RAiZOJ9GXMtTBji2ENgjdyaw41cjEVHGuORh/pTgLZXiM+7im4LJTxyPUNKghO5PmXOvN5" +
                "GIryLTOUHIbQBl4bppvUIeCuFMVXqIFqiIq0yyvqmnS6mOlYxq5Q1BCxj9kYX0nTYVJHCDT3pNUtvHSf" +
                "seKJo0osPg2yFvc3YKG82RDlrys9XW+xDfEa4IcGHCW42kSxA/egvPrV7reCfz96uaXu+OP6tZaUR/3i" +
                "QR/Po6kAyj8mQeiSi0mUgJ79jIperr1IQTwV1mjnAUAFP0ZfiwWw6bUGi/1ABlR70q3wLnZXpiPWZtRq" +
                "hRFRXWoQUhUYRGs0OZYSlKmW08J8PfZwuy3yWqp9uerFgw+/DXBVYwDK+O66cPjVSuhVhJIN76l/SZLB" +
                "czVNGz7j+u8LqX3TDeofkyCoxcdp/TiqH3EFt731XxWdroT/JAAA";
                
    }
}

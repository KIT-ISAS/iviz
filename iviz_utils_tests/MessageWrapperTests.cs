using System;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.MsgsWrapper;
using NUnit.Framework;

namespace Iviz.UtilsTests
{
    [Category("MessageWrappers")]
    public class MessageWrapperTests
    {
        class MyTransformStamped : RosMessageWrapper<MyTransformStamped>
        {
            [MessageName] public const string RosMessageType = "iviz_msgs/TransformStamped";

            [RenameTo("header")] public Header Header { get; set; }
            [RenameTo("child_frame_id")] public string ChildFrameId { get; set; }
            [RenameTo("transform")] public Transform Transform { get; set; }
        }

        [Test]
        public void TestTransformStampedMessage()
        {
            Assert.True(BuiltIns.GetMessageType<MyTransformStamped>() == MyTransformStamped.RosMessageType);
            Assert.True(BuiltIns.GetMd5Sum<MyTransformStamped>() == BuiltIns.GetMd5Sum<TransformStamped>());

            time now = time.Now();
            TransformStamped real = new TransformStamped()
            {
                Header = new Header(1, now, "Frame"),
                ChildFrameId = "Abcd",
                Transform = Transform.Identity
            };

            MyTransformStamped wrapped = new MyTransformStamped()
            {
                Header = new Header(1, now, "Frame"),
                ChildFrameId = "Abcd",
                Transform = Transform.Identity
            };

            byte[] messageBytes = real.SerializeToArray();
            byte[] otherMessageBytes = wrapped.SerializeToArray();

            Assert.True(messageBytes.SequenceEqual(otherMessageBytes));

            var otherWrapped = wrapped.DeserializeFrom(messageBytes);
            Assert.True(wrapped.Header == real.Header);
            Assert.True(wrapped.ChildFrameId == real.ChildFrameId);
            Assert.True(wrapped.Transform == real.Transform);
        }

        class MyInteractiveMarker : RosMessageWrapper<MyInteractiveMarker>
        {
            [MessageName] public const string RosMessageType = "iviz_msgs/InteractiveMarker";

            [RenameTo("header")] public Header Header { get; set; }
            [RenameTo("pose")] public Pose Pose { get; set; }
            [RenameTo("name")] public string Name { get; set; }
            [RenameTo("description")] public string Description { get; set; }
            [RenameTo("scale")] public float Scale { get; set; }
            [RenameTo("menu_entries")] public MenuEntry[] MenuEntries { get; set; }
            [RenameTo("controls")] public InteractiveMarkerControl[] Controls { get; set; }
        }

        [Test]
        public void TestInteractiveMarkerMessage()
        {
            Assert.True(BuiltIns.GetMessageType(typeof(MyInteractiveMarker)) == MyInteractiveMarker.RosMessageType);
            Console.WriteLine(MyInteractiveMarker.RosDefinition);
            Assert.True(
                BuiltIns.GetMd5Sum(typeof(MyInteractiveMarker)) == BuiltIns.GetMd5Sum(typeof(InteractiveMarker)));

            time now = time.Now();
            var real = new InteractiveMarker()
            {
                Pose = Pose.Identity,
                Name = "Abcd",
                Description = "Efgh",
                MenuEntries = new MenuEntry[] {new(), new()},
                Controls = new InteractiveMarkerControl[] {new(), new(), new()}
            };

            var wrapped = new MyInteractiveMarker()
            {
                Pose = Pose.Identity,
                Name = "Abcd",
                Description = "Efgh",
                MenuEntries = new MenuEntry[] {new(), new()},
                Controls = new InteractiveMarkerControl[] {new(), new(), new()}
            };

            byte[] messageBytes = real.SerializeToArray();
            byte[] otherMessageBytes = wrapped.SerializeToArray();

            Assert.True(messageBytes.SequenceEqual(otherMessageBytes));
        }

        enum LevelType : byte
        {
            DEBUG = 1, //debug level
            INFO = 2, //general level
            WARN = 4, //warning level
            ERROR = 8, //error level
            FATAL = 16 //fatal/critical level            
        };

        class MyLog : RosMessageWrapper<MyLog>
        {
            [MessageName] public const string RosMessageType = "iviz_msgs/MyLog";

            public const byte DEBUG = 1; //debug level
            public const byte INFO = 2; //general level
            public const byte WARN = 4; //warning level
            public const byte ERROR = 8; //error level
            public const byte FATAL = 16; //fatal/critical level

            [DataMember(Name = "header")] public Header Header { get; set; }
            [DataMember(Name = "level")] public LevelType Level { get; set; }
            [DataMember(Name = "name")] public string Name { get; set; } = ""; // name of the node
            [DataMember(Name = "msg")] public string Msg { get; set; } = ""; // message 
            [DataMember(Name = "file")] public string File { get; set; } = ""; // file the message came from
            [DataMember(Name = "function")] public string Function { get; set; } = ""; // function the message came from
            [DataMember(Name = "line")] public uint Line { get; set; } // line the message came from

            [DataMember(Name = "topics")]
            public string[] Topics { get; set; } = Array.Empty<string>(); // topic names that the node publishes
        }

        [Test]
        public void TestLogMessage()
        {
            Assert.True(BuiltIns.GetMessageType(typeof(MyLog)) == MyLog.RosMessageType);
            Assert.True(BuiltIns.GetMd5Sum(typeof(MyLog)) == BuiltIns.GetMd5Sum(typeof(Log)));
        }
    }
}
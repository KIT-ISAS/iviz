using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using iviz_test;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.MsgsWrapper;
using Iviz.Roslib;
using Newtonsoft.Json;
using Buffer = Iviz.Msgs.Buffer;
using Byte = System.Byte;

class Program
{
    public class IvizDialog : RosMessageWrapper<IvizDialog>
    {
        [MessageName] public const string RosMessageType = "iviz_msgs/Dialog";

        public const byte ActionAdd = 0;
        public const byte ActionRemove = 1;
        public const byte ActionRemoveAll = 2;

        public byte Action { get; set; }
        public string Id { get; set; } = "";

        public const byte ModeOk = 0;
        public const byte ModeYesNo = 1;
        public const byte ModeOkCancel = 2;

        public const byte ModeInfo = 10;
        public const byte ModeWarn = 11;
        public const byte ModeError = 12;

        public const byte ModeMenu = 20;

        public byte Mode { get; set; }
        public string Title { get; set; } = "";
        public string Caption { get; set; } = "";

        public const byte BindNone = 0;
        public const byte BindToTf = 1;
        public const byte BindToUser = 2;

        public byte BindingType { get; set; }
        public string TfPivot { get; set; } = "";
        public Vector3f TfOffset { get; set; }
        public Vector3f DialogDisplacement { get; set; }
    }

    public class IvizDialogFeedback : RosMessageWrapper<IvizDialogFeedback>
    {
        [MessageName] public const string RosMessageType = "iviz_msgs/DialogFeedback";
        public string Id { get; set; } = "";
    }
    
    static async Task Main()
    {
        Uri masterUri = new Uri("http://141.3.59.5:11311");
        Uri callerUri = new Uri("http://141.3.59.19:7633");

        await using var client = await RosClient.CreateAsync(masterUri, "/iviz_gui", callerUri);
        await using var writer = await RosChannelWriter.CreateAsync<IvizDialog>(client, "/iviz_dialog", true);
        await using var reader = await RosChannelReader.CreateAsync<IvizDialogFeedback>(client, "/iviz_dialog/feedback");

        var dialog = new IvizDialog()
        {
            Action = IvizDialog.ActionAdd,
            BindingType = IvizDialog.BindToTf,
            Caption = "Das hier ist der Armar6",
            DialogDisplacement = new Vector3f(0, -1, 1),
            Id = "Armar6",
            TfPivot = "map",
            TfOffset = Vector3f.One,
            Title = "Roboter",
            Mode = IvizDialog.ModeInfo
        };
        
        writer.Write(dialog);
        
        await foreach (var msg in reader.ReadAllAsync())
        {
            Console.WriteLine(msg);
        }

        Console.WriteLine(IvizDialog.RosDefinition);
        

        /*
        IvizQrMarkerTf marker = new IvizQrMarkerTf()
        {
            Size = 175,
            Tf = "table1"
        };

        Console.WriteLine(JsonConvert.SerializeObject(marker));
        */
    }

    interface IIvizQrMarker
    {
        public int IvizT { get; }
    }

    [DataContract]
    class BaseIvizMarker : IIvizQrMarker
    {
        [DataMember] public int IvizT { get; set; }
    }

    [DataContract]
    class IvizQrMarkerTf : IIvizQrMarker
    {
        [DataMember] public int IvizT => 0;
        [DataMember] public int Size { get; set; }
        [DataMember] public string? Tf { get; set; }
    }
}
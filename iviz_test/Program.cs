using System;
using System.Diagnostics;
using System.Threading.Tasks;
using iviz_test;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.MsgsGen.Dynamic;
using Newtonsoft.Json;
using Buffer = Iviz.Msgs.Buffer;

class Program
{
    /*
    static async Task Main()
    {
        await TwistTest.TwistMarkerTest();
    }
    */

    static void Main()
    {
        /*
        TFMessage message = new TFMessage
        {
            Transforms = new[]
            {
                new TransformStamped
                {
                    ChildFrameId = "Abcd1",
                    Header =
                    {
                        FrameId = "Efgh1", Seq = 0, Stamp = time.Now()
                    }
                },
                new TransformStamped
                {
                    ChildFrameId = "Abcd2",
                    Header =
                    {
                        FrameId = "Efgh2", Seq = 0, Stamp = time.Now()
                    }
                },
                new TransformStamped(), 
                new TransformStamped(), 
                new TransformStamped(), 
                new TransformStamped(), 
                new TransformStamped(), 
                new TransformStamped(), 
                new TransformStamped(), 
            }

        };

        byte[] array = new byte[1000];
        Buffer.Serialize(message, array);

        Type msgType = typeof(TFMessage);
        DynamicMessage generator = DynamicMessage.CreateFromDependencyString(
            BuiltIns.GetMessageType(msgType), BuiltIns.DecompressDependencies(msgType));

        DynamicMessage reconstructed = Buffer.Deserialize(generator, array);
        */

        OccupancyGrid map = new OccupancyGrid
        {
            Data = new sbyte[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, }

        };
        
        Console.WriteLine(JsonConvert.SerializeObject(map, Formatting.Indented, new ClampJsonConverter()));



    }
}
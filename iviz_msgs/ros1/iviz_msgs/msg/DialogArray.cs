/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class DialogArray : IHasSerializer<DialogArray>, IMessage
    {
        [DataMember (Name = "dialogs")] public Dialog[] Dialogs;
    
        public DialogArray()
        {
            Dialogs = EmptyArray<Dialog>.Value;
        }
        
        public DialogArray(Dialog[] Dialogs)
        {
            this.Dialogs = Dialogs;
        }
        
        public DialogArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Dialog[] array;
                if (n == 0) array = EmptyArray<Dialog>.Value;
                else
                {
                    array = new Dialog[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Dialog(ref b);
                    }
                }
                Dialogs = array;
            }
        }
        
        public DialogArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Dialog[] array;
                if (n == 0) array = EmptyArray<Dialog>.Value;
                else
                {
                    array = new Dialog[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Dialog(ref b);
                    }
                }
                Dialogs = array;
            }
        }
        
        public DialogArray RosDeserialize(ref ReadBuffer b) => new DialogArray(ref b);
        
        public DialogArray RosDeserialize(ref ReadBuffer2 b) => new DialogArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Dialogs.Length);
            foreach (var t in Dialogs)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Dialogs.Length);
            foreach (var t in Dialogs)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Dialogs, nameof(Dialogs));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Dialogs) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Dialogs.Length
            foreach (var msg in Dialogs) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/DialogArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "60c2548344f1e7f5baf10c6df11f828d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VVTW/bOBC961cMkEOTReJtkrbbDdCDYzupUcfK2k6LoigMWqJkIhKpJSknyq/fR8qS" +
                "7cTF7mFjCDA1nHnz5lN9wTKV/vhJsT+YIAg+/c+/4GZ6fUFiJZ7muUnN733vKSiFtB+p25sNw/G82+/T" +
                "J3q7K5wMbsKvA8hP98m7oxGuzoL13ez77WB+O+oOx+SQiA6IrYOiB2GXZIXN+DFZ/miPickY11o9kEpo" +
                "UVqrpKFtpOnncDJzSKc1kslZlnH9EtFDOdBt63E4G/Yc87OXPJgkESm5pvDc8mYwviPn93yPJeVclo5x" +
                "XmZWFHCuCitAfRvh8m42C8dAeFcjWCErijIR3bMFLOLt7HuDYQ/qzuX7f0mao70/dU0Rat/TefiFdn6b" +
                "0jYa3wfTcbijcfoCo9cd9wajjcbZM42rcPKtO+lvYZzv17js9r54TSTlmUZ71WK8b4JxeUElx4NnIXh5" +
                "bxJOp7vEvbyNfEPXy1uuG5Jevoeal/eH3VF4vSa0Jb+7bfE/7OiH38Zr+R/b8uH4KlzLP27L4bPR/3Nb" +
                "PphMwsk6rrcvCU39xU7Ef90Npm4s3QWm0di4HvPPnMUYmKX/CxaVxahErluhooVMScRBXGrmRJSJhFuR" +
                "c9eBIiGpLD1xrdB5S75pyCyjBSfNc7XiaMLEAt8uhSFnGiSZYvbDOzIRy3jt0FbF+tQ0qn9xjbwh2lOZ" +
                "0pPryy4tWHSfalXKeB45YcPUT0HzErFiKwpsTjeUcy7xytf4CyFj3M29+5SrnFtd1c6+8sgqfU42mask" +
                "Mdz+4r6OeR4LU2Qs4vDxK00g7Wi91gJ/VtjggKYWu4DpGBmwLGaWUaJQcJEuuT7J+IpnMGJ5gVr5W5cO" +
                "04HhzNUMT8ol19irFZUGSlZRpPK8lCJirngo6o49LAX2JhVMWxGVGdPQVxqpduqJZjl36HgM/7vkMuI0" +
                "7F9ARxoelVaAUAWESHNmXCWH/Xrln585A3Tej4kypz+Dg9mDOoGcp66/GhboNGYda/5YaG4cYWYu4Oy3" +
                "OsoOnCBLHO5iQ4deNserOSJ4AxdeqGhJhwjhtrJLNL3r7RXTwu9lAEfuExPTG2f05mgLWXpoyaRq4GvE" +
                "jY//AitbXBfTyRLFy1waTJkik1AstFqJGKqLyoPgo4GGwmwuNNNV4OfTuwwOrlyy69nzpcE/M0ZFApWI" +
                "/aejGRdfljlm/bXbsh3jehGgqLo9pe1p0Z7YazHaO6ZN12vumgdpRcJo5e9cUyeaI7cFZrjj+nfoG01J" +
                "9GvOGWqA0Wgt/Sdac79LO0DlmmPu8JUWlmLFjduewMjZPSA5qu6sWVEADDOomTRZvXQhhskh76SdY3pY" +
                "cllruar5YfPjKSLSIhVxbQlHeWvMaB0c1nRyVu9nz7l2hhYCiFbWGxx1aJhQpUp6cAHhoNdbQbml3vDy" +
                "TWsVNn/piHuI3YTeKowm0mIMS9Hf0ljso07Qrv/H9lS1p6fgH6oUFn3oCgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<DialogArray> CreateSerializer() => new Serializer();
        public Deserializer<DialogArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<DialogArray>
        {
            public override void RosSerialize(DialogArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(DialogArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(DialogArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(DialogArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(DialogArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<DialogArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out DialogArray msg) => msg = new DialogArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out DialogArray msg) => msg = new DialogArray(ref b);
        }
    }
}

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class GetFramePose : IService
    {
        /// Request message.
        [DataMember] public GetFramePoseRequest Request;
        
        /// Response message.
        [DataMember] public GetFramePoseResponse Response;
        
        /// Empty constructor.
        public GetFramePose()
        {
            Request = new GetFramePoseRequest();
            Response = new GetFramePoseResponse();
        }
        
        /// Setter constructor.
        public GetFramePose(GetFramePoseRequest request)
        {
            Request = request;
            Response = new GetFramePoseResponse();
        }
        
        IRequest IService.Request
        {
            get => Request;
            set => Request = (GetFramePoseRequest)value;
        }
        
        IResponse IService.Response
        {
            get => Response;
            set => Response = (GetFramePoseResponse)value;
        }
        
        public const string ServiceType = "iviz_msgs/GetFramePose";
        
        public string RosServiceType => ServiceType;
        
        public string RosMd5Sum => "80ebc02508d7723ac1b22636270c4ba6";
        
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetFramePoseRequest : IRequest<GetFramePose, GetFramePoseResponse>, IDeserializable<GetFramePoseRequest>
    {
        // Gets the absolute pose of a TF frame w.r.t. the map frame
        /// <summary> Frame ids </summary>
        [DataMember (Name = "frames")] public string[] Frames;
    
        public GetFramePoseRequest()
        {
            Frames = EmptyArray<string>.Value;
        }
        
        public GetFramePoseRequest(string[] Frames)
        {
            this.Frames = Frames;
        }
        
        public GetFramePoseRequest(ref ReadBuffer b)
        {
            b.DeserializeStringArray(out Frames);
        }
        
        public GetFramePoseRequest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeStringArray(out Frames);
        }
        
        public GetFramePoseRequest RosDeserialize(ref ReadBuffer b) => new GetFramePoseRequest(ref b);
        
        public GetFramePoseRequest RosDeserialize(ref ReadBuffer2 b) => new GetFramePoseRequest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Frames);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(Frames);
        }
        
        public void RosValidate()
        {
            if (Frames is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Frames.Length; i++)
            {
                if (Frames[i] is null) BuiltIns.ThrowNullReference(nameof(Frames), i);
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += WriteBuffer.GetArraySize(Frames);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Frames);
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }

    [DataContract]
    public sealed class GetFramePoseResponse : IResponse, IDeserializable<GetFramePoseResponse>
    {
        /// <summary> Whether the entry is valid </summary>
        [DataMember (Name = "is_valid")] public bool[] IsValid;
        /// <summary> The absolute poses </summary>
        [DataMember (Name = "poses")] public GeometryMsgs.Pose[] Poses;
    
        public GetFramePoseResponse()
        {
            IsValid = EmptyArray<bool>.Value;
            Poses = EmptyArray<GeometryMsgs.Pose>.Value;
        }
        
        public GetFramePoseResponse(bool[] IsValid, GeometryMsgs.Pose[] Poses)
        {
            this.IsValid = IsValid;
            this.Poses = Poses;
        }
        
        public GetFramePoseResponse(ref ReadBuffer b)
        {
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<bool>.Value
                    : new bool[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 1);
                }
                IsValid = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Pose>.Value
                    : new GeometryMsgs.Pose[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Poses = array;
            }
        }
        
        public GetFramePoseResponse(ref ReadBuffer2 b)
        {
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<bool>.Value
                    : new bool[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 1);
                }
                IsValid = array;
            }
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Pose>.Value
                    : new GeometryMsgs.Pose[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Poses = array;
            }
        }
        
        public GetFramePoseResponse RosDeserialize(ref ReadBuffer b) => new GetFramePoseResponse(ref b);
        
        public GetFramePoseResponse RosDeserialize(ref ReadBuffer2 b) => new GetFramePoseResponse(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(IsValid);
            b.SerializeStructArray(Poses);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(IsValid);
            b.SerializeStructArray(Poses);
        }
        
        public void RosValidate()
        {
            if (IsValid is null) BuiltIns.ThrowNullReference();
            if (Poses is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += IsValid.Length;
                size += 56 * Poses.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // IsValid.Length
            size += 1 * IsValid.Length;
            size = WriteBuffer2.Align4(size);
            size += 4; // Poses.Length
            size = WriteBuffer2.Align8(size);
            size += 56 * Poses.Length;
            return size;
        }
    
        public override string ToString() => Extensions.ToString(this);
    }
}

/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciActionGoal : IHasSerializer<FibonacciActionGoal>, IMessage, IActionGoal<FibonacciGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public FibonacciGoal Goal { get; set; }
    
        public FibonacciActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new FibonacciGoal();
        }
        
        public FibonacciActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, FibonacciGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        public FibonacciActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new FibonacciGoal(ref b);
        }
        
        public FibonacciActionGoal(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new FibonacciGoal(ref b);
        }
        
        public FibonacciActionGoal RosDeserialize(ref ReadBuffer b) => new FibonacciActionGoal(ref b);
        
        public FibonacciActionGoal RosDeserialize(ref ReadBuffer2 b) => new FibonacciActionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
            GoalId.RosValidate();
            Goal.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = GoalId.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Goal
            return size;
        }
    
        public const string MessageType = "actionlib_tutorials/FibonacciActionGoal";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "006871c7fa1d0e3d5fe2226bf17b2a94";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUwWrcQAy9+ysEe0hSyIa2t0BvIckeCqXJrZSgndHaovaMOxrv1n/fN+PNNoEeemiM" +
                "wXgsPT3pPfle2Euirj4adllj6HX7NFhrV3eR+80NtXg8qW9udRsDO6flvJ42n/7z1Xx+uLsmy34hcL/Q" +
                "WtFD5uA5eRoks+fMtItgrW0n6bKXvfRI4mEUT/VrnkexNRIfOzXC3UqQxH0/02QIypFcHIYpqOMslHWQ" +
                "V/nI1EBMI6esbuo5IT4mr6GE7xIPUtBxm/ycJDihzc01YoKJm7KC0AwEl4RNQ4uP1Ewa8scPJYFW9O1r" +
                "tPffm9XjIV7iXFqIcGJBueNcWMuvMYkVwmzXKPZu6XKNIpiSoJw3Oq9nT3i1C0I1cJExuo7O0cKXOXcx" +
                "AFBoz0l520sBdhgFUM9K0tnFC+RQoQOH+Ay/IP6p8S+w4YRberrsIF5fxmBTi0kicExxrx6h27mCuF4l" +
                "ZILzEqe5KVlLyWZ1W4aNIGRVafBks+gUSng6aO4ay6mgV1mKUd/Iln/djuqxI1myLk69x0tMUvuqjUDL" +
                "Q6cQpDZR9oYObJSKcwxNFCdtqt7VmxgJh2MxiJz2sMahk0CaCY2KFffCFzKMmTBwZBdMW1xzEJQ+QdNW" +
                "doULk5OUGcoVRi/ne+Sv/lkTjBf05lLkNGfaifgtux9g5pEBU059xjKacStVBLJRnO7ULQ0eGdj6iF42" +
                "ZQkAqWGyDGaE9UPU+lm/otybS5cniKMY19Wr31nTLNuJHccP5zdE8N4yFgUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<FibonacciActionGoal> CreateSerializer() => new Serializer();
        public Deserializer<FibonacciActionGoal> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<FibonacciActionGoal>
        {
            public override void RosSerialize(FibonacciActionGoal msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(FibonacciActionGoal msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(FibonacciActionGoal msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(FibonacciActionGoal msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(FibonacciActionGoal msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<FibonacciActionGoal>
        {
            public override void RosDeserialize(ref ReadBuffer b, out FibonacciActionGoal msg) => msg = new FibonacciActionGoal(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out FibonacciActionGoal msg) => msg = new FibonacciActionGoal(ref b);
        }
    }
}

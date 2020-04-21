using System;

namespace Iviz.Bridge
{
    class TypeInfo
    {
        public readonly string rosType;
        public readonly Type msgType;
        public readonly PublishMessage generator;

        public TypeInfo(string rosType, Type msgType)
        {
            this.rosType = rosType;
            this.msgType = msgType;
            generator = PublishMessage.Instantiate(msgType);
        }
    }
}

using System;

namespace Iviz.Bridge
{
    [Serializable]
    public sealed class GenericMessage
    {
        public string op = "";
        public string id = "";
        public string topic = "";
        public string type = "";
    }
}

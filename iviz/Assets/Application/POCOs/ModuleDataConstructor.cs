#nullable enable

using System;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Roslib.Utils;

namespace Iviz.App
{
    [DataContract]
    public sealed class ModuleDataConstructor : JsonToString
    {
        [DataMember] public ModuleType ModuleType { get; }
        [DataMember] public string Topic { get; }
        [DataMember] public string Type { get; }
        [DataMember] public IConfiguration? Configuration { get; }

        public string? TryGetConfigurationTopic() =>
            Configuration is IConfigurationWithTopic hasTopic ? hasTopic.Topic : null;

        public ModuleDataConstructor(ModuleType moduleType, string topic, string type, IConfiguration? configuration)
        {
            ModuleType = moduleType;
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Configuration = configuration;
        }
    }
}
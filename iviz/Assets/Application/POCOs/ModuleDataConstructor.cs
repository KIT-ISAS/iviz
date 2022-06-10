#nullable enable

using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Core;
using Iviz.Core.Configurations;
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
            ThrowHelper.ThrowIfNull(topic, nameof(topic));
            ThrowHelper.ThrowIfNull(type, nameof(type));
            
            ModuleType = moduleType;
            Topic = topic;
            Type = type;
            Configuration = configuration;
        }
    }
}
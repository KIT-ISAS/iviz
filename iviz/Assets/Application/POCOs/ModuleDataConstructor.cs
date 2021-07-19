using System;
using System.Runtime.Serialization;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;

namespace Iviz.App
{
    [DataContract]
    public sealed class ModuleDataConstructor : JsonToString
    {
        [DataMember] public ModuleType ModuleType { get; }
        [DataMember, NotNull] public string Topic { get; }
        [DataMember, NotNull] public string Type { get; }
        [DataMember, CanBeNull] public IConfiguration Configuration { get; }

        [CanBeNull] public T GetConfiguration<T>() where T : class, IConfiguration => Configuration as T;

        public ModuleDataConstructor(ModuleType moduleType,
                                     [NotNull] string topic,
                                     [NotNull] string type,
                                     [CanBeNull] IConfiguration configuration)
        {
            ModuleType = moduleType;
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Configuration = configuration;
        }
    }
}

using System;
using Iviz.Controllers;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class ModuleDataConstructor
    {
        public Resource.ModuleType ModuleType { get; }
        [NotNull] public string Topic { get; }
        [NotNull] public string Type { get; }
        [CanBeNull] public IConfiguration Configuration { get; }

        [CanBeNull] public T GetConfiguration<T>() where T : class, IConfiguration => Configuration as T;

        public ModuleDataConstructor(Resource.ModuleType moduleType,
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

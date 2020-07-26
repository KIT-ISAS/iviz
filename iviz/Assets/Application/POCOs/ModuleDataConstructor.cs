using System;
using Iviz.Controllers;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class ModuleDataConstructor
    {
        public Resource.Module Module { get; }
        public ModuleListPanel ModuleList { get; }
        [NotNull] public string Topic { get; }
        [NotNull] public string Type { get; }
        public IConfiguration Configuration { get; }

        public T GetConfiguration<T>() where T : class, IConfiguration => Configuration as T;

        public ModuleDataConstructor(Resource.Module module,
                                     ModuleListPanel moduleList,
                                     string topic,
                                     string type,
                                     IConfiguration configuration)
        {
            Module = module;
            ModuleList = moduleList;
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Configuration = configuration;
        }
    }
}

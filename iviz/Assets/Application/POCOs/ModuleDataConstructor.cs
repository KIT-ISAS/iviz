using Iviz.Resources;

namespace Iviz.App
{
    public sealed class ModuleDataConstructor
    {
        public Resource.Module Module { get; }
        public DisplayListPanel DisplayList { get; }
        public string Topic { get; }
        public string Type { get; }
        public IConfiguration Configuration { get; }

        public T GetConfiguration<T>() where T : class, IConfiguration => Configuration as T;

        public ModuleDataConstructor(Resource.Module module,
                                     DisplayListPanel displayList,
                                     string topic,
                                     string type,
                                     IConfiguration configuration)
        {
            Module = module;
            DisplayList = displayList;
            Topic = topic;
            Type = type;
            Configuration = configuration;
        }
    }
}

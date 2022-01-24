using System.Text;
using Iviz.Msgs.IvizMsgs;
using Iviz.Roslib;

namespace Iviz.RemoteLib;

public sealed class RemoteController
{
    readonly RosClient client;
    readonly string ivizId;
    readonly StringBuilder configStr = new(256);
    readonly List<string> configFields = new(16);

    public RemoteController(RosClient client, string ivizId)
    {
        this.client = client;
        this.ivizId = ivizId;
    }

    public string AddModuleFromTopic(string topic, string? requestedId = null)
    {
        if (string.IsNullOrEmpty(topic))
        {
            throw new ArgumentNullException(nameof(topic));
        }
        
        if (requestedId == "")
        {
            throw new ArgumentException("Requested id name cannot be empty");
        }

        var addModuleResponse =
            client.CallService($"{ivizId}/add_module_from_topic", new AddModuleFromTopicRequest(topic, requestedId ?? topic));

        if (!addModuleResponse.Success)
        {
            throw new RemoteException(addModuleResponse.Message);
        }

        return addModuleResponse.Id;
    }

    public string AddModule(AddModuleType type, string requestedId)
    {
        if (requestedId == null)
        {
            throw new ArgumentNullException(nameof(requestedId));
        }

        var addModuleResponse =
            client.CallService($"{ivizId}/add_module", new AddModuleRequest(type.ToString(), requestedId));

        if (!addModuleResponse.Success)
        { 
            throw new RemoteException(addModuleResponse.Message);
        }

        return addModuleResponse.Id;
    }

    public void UpdateModule(string id, IConfiguration config)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }
        
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id));
        }

        configStr.Clear();
        configFields.Clear();
        using (var processor = new ConfigurationSerializer(configStr, configFields))
        {
            config.Serialize(processor);
        }

        var updateModuleResponse = client.CallService($"{ivizId}/update_module",
            new UpdateModuleRequest(id, configFields.ToArray(), configStr.ToString()));

        if (!updateModuleResponse.Success)
        {
            throw new RemoteException(updateModuleResponse.Message);
        }
    }
    
    public void SetVisible(string id, bool value)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id));
        }

        configStr.Clear();
        configFields.Clear();
        using (var processor = new ConfigurationSerializer(configStr, configFields))
        {
            new BasicConfiguration(value).Serialize(processor);
        }

        var updateModuleResponse = client.CallService($"{ivizId}/update_module",
            new UpdateModuleRequest(id, configFields.ToArray(), configStr.ToString()));

        if (!updateModuleResponse.Success)
        {
            throw new RemoteException(updateModuleResponse.Message);
        }
    }

    public string[] GetAllModuleIds()
    {
        return client.CallService($"{ivizId}/get_modules", GetModulesRequest.Singleton).Configs;
    }
}

public enum AddModuleType
{
    Grid = ModuleType.Grid,
    AugmentedReality = ModuleType.AugmentedReality,
    Joystick = ModuleType.Joystick,
    Robot = ModuleType.Robot,
}

public enum ModuleType
{
    Grid = 1,
    TF = 2,
    PointCloud = 3,
    Image = 4,
    Marker = 5,
    InteractiveMarker = 6,
    DepthCloud = 8,
    LaserScan = 9,
    AugmentedReality = 10,
    Magnitude = 11,
    OccupancyGrid = 12,
    Joystick = 13,
    Path = 14,
    GridMap = 15,
    Robot = 16,
    GuiWidget = 18,
    XR = 19,
    Camera = 20,
    TFPublisher = 21
}

public interface IConfiguration
{
    internal void Serialize(in ConfigurationSerializer serializer);
}

public class RemoteException : Exception
{
    public RemoteException(string message) : base(message)
    {
    }
}
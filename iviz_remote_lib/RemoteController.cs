using System.Collections;
using System.Text;
using Iviz.Msgs.IvizMsgs;
using Iviz.Roslib;

namespace Iviz.RemoteLib;

/// <summary>
/// Class that can be used to remotely control an instance of the iviz app.
/// For this class to work, the app must be already running and connected to a ROS master.
/// </summary>
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

    /// <summary>
    /// Creates a module that listens to the given topic, or returns the name of the module already listening to it.
    /// Note that only one module can listen to a given topic at the same time.
    /// </summary>
    /// <param name="topic">The topic to listen to.</param>
    /// <param name="requestedId">
    /// An identifier to refer to the created module. If null, the identifier will be set to the topic.
    /// This argument will be ignored if a module for that topic already exists.
    /// </param>
    /// <returns>
    /// If no module that listens to the topic already exists, the call will return <see cref="requestedId"/>.
    /// Otherwise, it will return the id of the existing module. 
    /// </returns>
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

    /// <summary>
    /// Adds a module of type <see cref="AddModuleType"/>. Some module types such as <see cref="AddModuleType.AugmentedReality"/>
    /// are unique, while others such as <see cref="AddModuleType.Robot"/> can be instanced multiple times.
    /// If a module type is unique and it already exists, the id of the existing module will be returned.
    /// Note that if the module should listen to a given ROS topic, <see cref="AddModuleFromTopic"/> must be used instead.
    /// </summary>
    /// <param name="type">The module type to create.</param>
    /// <param name="requestedId">The id to use if the call creates a new module.</param>
    /// <returns>
    /// The id of the module. Equal to <see cref="requestedId"/> if a new module was created, otherwise the call
    /// returns the id of the existing module.
    /// Applications are encouraged to use the returned value instead of assuming that <see cref="requestedId"/> was used. 
    /// </returns>
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
    
    /// <summary>
    /// Toggles the visibility of the given module, if supported.
    /// </summary>
    /// <param name="id">The id of the module.</param>
    /// <param name="value">The requested visibility.</param>
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
            processor.Serialize(value, "Visible");
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
    DepthCloud = ModuleType.DepthCloud,
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
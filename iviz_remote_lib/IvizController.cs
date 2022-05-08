using System.Collections;
using System.Text;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Roslib;

namespace Iviz.RemoteLib;

/// <summary>
/// Class that can be used to remotely control an instance of the iviz app.
/// For this class to work, the app must be already running and connected to a ROS master.
/// <example>
/// This initializes an iviz controller:
/// <code>
/// string masterUri = "http://localhost:11311";
/// string callerId = "my_ros_id";
/// string callerUri = "http://localhost:7615";
/// var client = new RosClient(masterUri, "my_ros_id", callerUri);
/// string ivizId = "iviz_wineditor";
/// var controller = new IvizController(client, ivizId);
/// </code>
/// </example>
/// </summary>
public sealed class IvizController
{
    readonly RosClient client;
    readonly string ivizId;
    readonly StringBuilder configStr = new(256);

    /// <summary>
    /// Creates a new iviz controller.
    /// </summary>
    /// <param name="client">An initialized ROS client.</param>
    /// <param name="ivizId">
    /// The id of the iviz instance, as found in the 'My ROS ID' field. For example: iviz_wineditor.
    /// The starting '/' should be removed.
    /// </param>
    public IvizController(RosClient client, string ivizId)
    {
        RosClient.ValidateResourceName(ivizId);
        this.client = client;
        this.ivizId = ivizId[0] == '/' ? ivizId[1..] : ivizId;
    }

    /// <summary>
    /// Tells iviz to create a module that listens to the given topic, or returns the name of the module already listening to it.
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
            BuiltIns.ThrowArgumentNull(nameof(topic));
        }

        if (requestedId == "")
        {
            throw new ArgumentException("Requested id name cannot be empty");
        }

        var addModuleResponse =
            client.CallService($"{ivizId}/add_module_from_topic",
                new AddModuleFromTopicRequest(topic, requestedId ?? topic));

        if (!addModuleResponse.Success)
        {
            throw new RemoteException(addModuleResponse.Message);
        }

        return addModuleResponse.Id;
    }

    /// <summary>
    /// Tells iviz to add a module of type <see cref="AddModuleType"/>. Some module types such as <see cref="AddModuleType.AugmentedReality"/>
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
            BuiltIns.ThrowArgumentNull(nameof(requestedId));
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
            BuiltIns.ThrowArgumentNull(nameof(config));
        }

        if (string.IsNullOrEmpty(id))
        {
            BuiltIns.ThrowArgumentNull(nameof(id));
        }

        configStr.Clear();
        using (var processor = new ConfigurationSerializer(configStr))
        {
            config.Serialize(processor);
        }

        var updateModuleResponse = client.CallService($"{ivizId}/update_module",
            new UpdateModuleRequest(id, Array.Empty<string>(), configStr.ToString()));

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
            BuiltIns.ThrowArgumentNull(nameof(id));
        }

        configStr.Clear();
        using (var processor = new ConfigurationSerializer(configStr))
        {
            processor.Serialize(value, "Visible");
        }

        var updateModuleResponse = client.CallService($"{ivizId}/update_module",
            new UpdateModuleRequest(id, Array.Empty<string>(), configStr.ToString()));

        if (!updateModuleResponse.Success)
        {
            throw new RemoteException(updateModuleResponse.Message);
        }
    }

    public string[] GetAllModuleIds()
    {
        return client.CallService($"{ivizId}/get_modules", GetModulesRequest.Singleton).Configs;
    }
    
    public void ResetModule(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            BuiltIns.ThrowArgumentNull(nameof(id));
        }

        var response = client.CallService($"{ivizId}/reset_module", new ResetModuleRequest(id));

        if (!response.Success)
        {
            throw new RemoteException(response.Message);
        }
    }
    
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
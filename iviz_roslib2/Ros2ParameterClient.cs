using Iviz.Msgs;
using Iviz.Msgs.RclInterfaces;
using Iviz.Roslib;
using Iviz.Tools;

using static Iviz.Roslib2.Ros2Parameters;

namespace Iviz.Roslib2;

public sealed class Ros2ParameterClient
{
    readonly Ros2Client backend;

    public Ros2ParameterClient(Ros2Client backend)
    {
        this.backend = backend;
    }

    public async ValueTask<RosValue> GetParameterAsync(string node, string key, CancellationToken token = default)
    {
        if (node.IsNullOrEmpty()) BuiltIns.ThrowArgumentNull(nameof(node));
        if (key.IsNullOrEmpty()) BuiltIns.ThrowArgumentNull(nameof(key));
        if (!RosNameUtils.IsValidResourceName(node) || node[0] is '/' or '~')
            BuiltIns.ThrowArgument(nameof(node), "Invalid node name");

        var service = new GetParameters
        {
            Request =
            {
                Names = new[] {key}
            }
        };
        
        await backend.CallServiceAsync(ServiceNameForGetParameters(node), service, token: token);

        var value = ValueFromParameter(service.Response.Values[0]);
        
        return !value.IsEmpty 
            ? value 
            : throw new RosParameterNotFoundException();
    }
    
    public async ValueTask<string[]> GetParameterNamesAsync(string node, CancellationToken token = default)
    {
        if (node.IsNullOrEmpty()) BuiltIns.ThrowArgumentNull(nameof(node));
        if (!RosNameUtils.IsValidResourceName(node) || node[0] is '/' or '~')
            BuiltIns.ThrowArgument(nameof(node), "Invalid node name");

        var service = new ListParameters();
        
        await backend.CallServiceAsync(ServiceNameForGetParameters(node), service, token: token);
        
        return service.Response.Result.Names;
    }
}
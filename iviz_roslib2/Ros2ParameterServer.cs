using Iviz.Msgs.RclInterfaces;
using Iviz.Tools;
using static Iviz.Roslib2.Ros2Parameters;

namespace Iviz.Roslib2;

public sealed class Ros2ParameterServer
{
    readonly ParameterValue emptyParameterValue = new();
    readonly SetParametersResult failedResultMissingKey = new(false, "Key does not exist");
    readonly SetParametersResult failedResultInvalidValue = new(false, "Invalid value");
    readonly SetParametersResult failedResultTypeMismatch = new(false, "Requested value has a different type");
    readonly SetParametersResult successResult = new(true, "");

    readonly Ros2Client backend;
    public Dictionary<string, RosValue> Parameters { get; } = new();

    public Ros2ParameterServer(Ros2Client backend)
    {
        this.backend = backend;
    }

    public async ValueTask RegisterServicesAsync(CancellationToken token = default)
    {
        string node = backend.CallerId;
        
        await backend.AdvertiseServiceAsync<DescribeParameters>(ServiceNameForDescribeParameters(node), Handle, token);
        await backend.AdvertiseServiceAsync<GetParameterTypes>(ServiceNameForGetParameterTypes(node), Handle, token);
        await backend.AdvertiseServiceAsync<GetParameters>(ServiceNameForGetParameters(node), Handle, token);
        await backend.AdvertiseServiceAsync<ListParameters>(ServiceNameForListParameters(node), Handle, token);
        await backend.AdvertiseServiceAsync<SetParameters>(ServiceNameForSetParameters(node), Handle, token);
        await backend.AdvertiseServiceAsync<SetParametersAtomically>(ServiceNameForSetParametersAtomically(node),
            Handle, token);
    }

    ValueTask Handle(DescribeParameters srv)
    {
        srv.Response.Descriptors = srv.Request.Names.Select(GetDescriptor).ToArray();
        return default;
    }

    ParameterDescriptor GetDescriptor(string parameter)
    {
        return Parameters.TryGetValue(parameter, out var value)
            ? new ParameterDescriptor
            {
                Name = parameter,
                Type = AsValueType(value.ValueType)
                // TODO: fill rest
            }
            : new ParameterDescriptor
            {
                Name = parameter,
                Type = ParameterType.PARAMETER_NOT_SET
            };
    }

    ValueTask Handle(GetParameterTypes srv)
    {
        srv.Response.Types = srv.Request.Names.Select(GetParameterType).ToArray();
        return default;
    }

    byte GetParameterType(string parameter)
    {
        return Parameters.TryGetValue(parameter, out var value)
            ? AsValueType(value.ValueType)
            : ParameterType.PARAMETER_NOT_SET;
    }


    ValueTask Handle(GetParameters srv)
    {
        srv.Response.Values = srv.Request.Names.Select(GetParameter).ToArray();
        return default;
    }

    ParameterValue GetParameter(string parameter)
    {
        if (!Parameters.TryGetValue(parameter, out var value))
        {
            return emptyParameterValue;
        }

        var result = new ParameterValue();
        switch (value.ValueType)
        {
            case RosValue.Type.Boolean:
                result.Type = ParameterType.PARAMETER_BOOL;
                value.TryGet(out result.BoolValue);
                break;
            case RosValue.Type.Long:
                result.Type = ParameterType.PARAMETER_INTEGER;
                value.TryGet(out result.IntegerValue);
                break;
            case RosValue.Type.Double:
                result.Type = ParameterType.PARAMETER_DOUBLE;
                value.TryGet(out result.DoubleValue);
                break;
            case RosValue.Type.String:
                result.Type = ParameterType.PARAMETER_STRING;
                value.TryGet(out result.StringValue);
                break;
            case RosValue.Type.ByteArray:
                result.Type = ParameterType.PARAMETER_BYTE_ARRAY;
                value.TryGet(out result.ByteArrayValue);
                break;
            case RosValue.Type.BooleanArray:
                result.Type = ParameterType.PARAMETER_BOOL_ARRAY;
                value.TryGet(out result.BoolArrayValue);
                break;
            case RosValue.Type.LongArray:
                result.Type = ParameterType.PARAMETER_INTEGER_ARRAY;
                value.TryGet(out result.IntegerArrayValue);
                break;
            case RosValue.Type.DoubleArray:
                result.Type = ParameterType.PARAMETER_DOUBLE_ARRAY;
                value.TryGet(out result.DoubleArrayValue);
                break;
            case RosValue.Type.StringArray:
                result.Type = ParameterType.PARAMETER_STRING_ARRAY;
                value.TryGet(out result.StringArrayValue);
                break;
        }

        return result;
    }

    ValueTask Handle(ListParameters srv)
    {
        string[] requestedPrefixes = srv.Request.Prefixes;
        var result = srv.Response.Result;

        if (requestedPrefixes.Length == 0)
        {
            result.Names = Parameters.Keys.ToArray();
            return default;
        }

        var prefixes = new List<string>();
        var parameters = new List<string>();

        foreach (string parameter in Parameters.Keys)
        {
            foreach (string prefix in requestedPrefixes)
            {
                if (!parameter.StartsWith(prefix)) continue;

                prefixes.Add(prefix);
                parameters.Add(parameter);
            }
        }

        result.Prefixes = prefixes.ToArray();
        result.Names = parameters.ToArray();
        return default;
    }

    ValueTask Handle(SetParameters srv)
    {
        srv.Response.Results = srv.Request.Parameters.Select(SetParameter).ToArray();
        return default;
    }

    SetParametersResult SetParameter(Parameter parameter)
    {
        if (!Parameters.TryGetValue(parameter.Name, out var currentValue))
        {
            return failedResultMissingKey;
        }

        var value = ValueFromParameter(parameter.Value);

        if (value.IsEmpty)
        {
            return failedResultInvalidValue;
        }

        if (value.ValueType != currentValue.ValueType)
        {
            return failedResultTypeMismatch;
        }

        Parameters[parameter.Name] = value;

        return successResult;
    }

    ValueTask Handle(SetParametersAtomically srv)
    {
        foreach (var parameter in srv.Request.Parameters)
        {
            if (!Parameters.TryGetValue(parameter.Name, out var currentValue))
            {
                srv.Response.Result =
                    new SetParametersResult(false, $"Key '{parameter.Name}' is missing");
                return default;
            }

            if (parameter.Value.Type == ParameterType.PARAMETER_NOT_SET)
            {
                srv.Response.Result =
                    new SetParametersResult(false, $"Key '{parameter.Name}' is not set");
                return default;
            }

            if (AsValueType(currentValue.ValueType) != parameter.Value.Type)
            {
                srv.Response.Result =
                    new SetParametersResult(false, $"Key '{parameter.Name}' has mismatched parameter type");
                return default;
            }
        }

        foreach (var parameter in srv.Request.Parameters)
        {
            SetParameter(parameter);
        }

        srv.Response.Result = successResult;
        return default;
    }
}
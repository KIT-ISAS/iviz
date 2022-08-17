using Iviz.Msgs.RclInterfaces;
using Iviz.Tools;

namespace Iviz.Roslib2;

public static class Ros2Parameters
{
    public static string ServiceNameForDescribeParameters(string node) => $"{node}/describe_parameters";
    public static string ServiceNameForGetParameterTypes(string node) => $"{node}/get_parameter_types";
    public static string ServiceNameForGetParameters(string node) => $"{node}/get_parameters";
    public static string ServiceNameForListParameters(string node) => $"{node}/list_parameters";
    public static string ServiceNameForSetParameters(string node) => $"{node}/set_parameters";
    public static string ServiceNameForSetParametersAtomically(string node) => $"{node}/set_parameters_atomically";
    
    public static byte AsValueType(RosValue.Type type)
    {
        return type switch
        {
            RosValue.Type.Boolean => ParameterType.PARAMETER_BOOL,
            RosValue.Type.Long => ParameterType.PARAMETER_INTEGER,
            RosValue.Type.Double => ParameterType.PARAMETER_DOUBLE,
            RosValue.Type.String => ParameterType.PARAMETER_STRING,
            RosValue.Type.ByteArray => ParameterType.PARAMETER_BYTE_ARRAY,
            RosValue.Type.BooleanArray => ParameterType.PARAMETER_BOOL_ARRAY,
            RosValue.Type.LongArray => ParameterType.PARAMETER_INTEGER_ARRAY,
            RosValue.Type.DoubleArray => ParameterType.PARAMETER_DOUBLE_ARRAY,
            RosValue.Type.StringArray => ParameterType.PARAMETER_STRING_ARRAY,
            _ => ParameterType.PARAMETER_NOT_SET
        };
    }

    public static RosValue ValueFromParameter(ParameterValue parameterValue)
    {
        return parameterValue.Type switch
        {
            ParameterType.PARAMETER_BOOL => new RosValue(parameterValue.BoolValue),
            ParameterType.PARAMETER_INTEGER => new RosValue(parameterValue.IntegerValue),
            ParameterType.PARAMETER_DOUBLE => new RosValue(parameterValue.DoubleValue),
            ParameterType.PARAMETER_STRING => new RosValue(parameterValue.StringValue),
            ParameterType.PARAMETER_BYTE_ARRAY => new RosValue(parameterValue.ByteArrayValue),
            ParameterType.PARAMETER_BOOL_ARRAY => new RosValue(parameterValue.BoolArrayValue),
            ParameterType.PARAMETER_INTEGER_ARRAY => new RosValue(parameterValue.IntegerArrayValue),
            ParameterType.PARAMETER_DOUBLE_ARRAY => new RosValue(parameterValue.DoubleArrayValue),
            ParameterType.PARAMETER_STRING_ARRAY => new RosValue(parameterValue.StringArrayValue),
            _ => default,
        };
    }
}
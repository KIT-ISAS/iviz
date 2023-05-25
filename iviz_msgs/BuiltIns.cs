using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;
using Iviz.Tools;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.Msgs;

public static class BuiltIns
{
    public static UTF8Encoding UTF8 => Defaults.UTF8;

    public static CultureInfo Culture => Defaults.Culture;

    public const string EmptyMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";

    public const string EmptyDependenciesBase64 = "H4sIAAAAAAAAE+MCAJMG1zIBAAAA";

    /// <summary>
    /// Returns the constant field <see cref="IMessage.RosMessageType"/> for the given type T. 
    /// </summary>
    public static string GetMessageType<T>() where T : IMessage, new()
    {
        return new T().RosMessageType;
    }

    /// <summary>
    /// Returns the constant field <see cref="IService.RosServiceType"/> for the given type T. 
    /// </summary>
    public static string GetServiceType<T>() where T : IService, new()
    {
        return new T().RosServiceType;
    }

    /// <summary>
    /// Returns the constant field <see cref="IService.RosMd5Sum"/> for the given type T. 
    /// </summary>
    public static string GetMd5Sum<T>() where T : IMessageRos1, new()
    {
        return new T().RosMd5Sum;
    }

    /// <summary>
    /// Decompresses the dependencies of an <see cref="IMessageRos1.RosDependenciesBase64"/> field. 
    /// </summary>
    public static string DecompressDependencies(string dependenciesBase64)
    {
        byte[] inputBytes = Convert.FromBase64String(dependenciesBase64);

        Span<byte> outputBytes = stackalloc byte[32];
        using var inputStream = new MemoryStream(inputBytes);
        using var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress);
        using var str = BuilderPool.Rent();

        int read;
        do
        {
            read = gZipStream.Read(outputBytes);
            str.Append(UTF8.GetString(outputBytes[..read]));
        } while (read != 0);

        return str.ToString();
    }

    static string RosNameToCs(string name)
    {
        if (name == null)
        {
            ThrowArgumentNull(nameof(name));
        }

        using var str = BuilderPool.Rent();
        str.Append(char.ToUpper(name[0], Culture));
        for (int i = 1; i < name.Length; i++)
        {
            switch (name[i])
            {
                case '_' when i != name.Length - 1:
                    str.Append(char.ToUpper(name[i + 1], Culture));
                    i++;
                    break;
                case '/':
                    str.Append('.');
                    break;
                default:
                    str.Append(name[i]);
                    break;
            }
        }

        return str.ToString();
    }

    /// Obtains the C# class type for a given ROS message, if it exists. 
    public static Type? TryGetTypeFromMessageName(string fullRosMessageName, string assemblyName = "Iviz.Msgs")
    {
        string guessName = $"{assemblyName}.{RosNameToCs(fullRosMessageName)}, {assemblyName}";
        var type = Type.GetType(guessName);
        if (type != null
            && typeof(IMessage).IsAssignableFrom(type)
            && (type.IsValueType || type.GetConstructor(Type.EmptyTypes) != null))
        {
            return type;
        }

        return null;
    }

    /// Obtains an instance of a C# ROS message, if it exists. 
    public static IMessage? TryGetGeneratorFromMessageName(string fullRosMessageName,
        string assemblyName = "Iviz.Msgs")
    {
        if (TryGetTypeFromMessageName(fullRosMessageName, assemblyName) is { } lookupMsgType
            && Activator.CreateInstance(lookupMsgType) is IMessage message)
        {
            return message;
        }

        return null;
    }

    public static string ToJsonString(this IMessage o, bool indented = true)
    {
        return ToJsonString((object)o, indented);
    }

    public static string ToJsonString(this ISerializable o, bool indented = true)
    {
        return ToJsonString((object)o, indented);
    }

    public static string ToJsonString(this IService o, bool indented = true)
    {
        return ToJsonString((object)o, indented);
    }

    public static string ToJsonString(this IRequest o, bool indented = true)
    {
        return ToJsonString((object)o, indented);
    }

    public static string ToJsonString(this IResponse o, bool indented = true)
    {
        return ToJsonString((object)o, indented);
    }

    public static string ToJsonString(object o, bool indented = true)
    {
        return JsonConvert.SerializeObject(o, indented ? Formatting.Indented : Formatting.None);
    }

    public static int GetRos2MessageLength(this ISerializable serializable)
    {
        return serializable.AddRos2MessageLength(0);
    }

    public static byte[] SerializeToArrayRos1(this IMessage o)
    {
        Serializer serializer = o.CreateSerializer();
        serializer.RosValidate(o);
        byte[] bytes = new byte[serializer.RosMessageLength(o)];
        WriteBuffer.Serialize(serializer, o, bytes);

        return bytes;
    }

    public static byte[] SerializeToArrayRos2(this IMessage o)
    {
        Serializer serializer = o.CreateSerializer();
        serializer.RosValidate(o);
        byte[] bytes = new byte[serializer.Ros2MessageLength(o)];
        WriteBuffer2.Serialize(serializer, o, bytes);

        return bytes;
    }

    public static T DeserializeRos1<T>(this T generator, Span<byte> src) where T : IMessage, IHasSerializer<T>
    {
        var deserializer = generator.CreateDeserializer();
        return ReadBuffer.Deserialize(deserializer, src);
    }

    public static T DeserializeRos2<T>(this T generator, Span<byte> src) where T : IMessage, IHasSerializer<T>
    {
        var deserializer = generator.CreateDeserializer();
        return ReadBuffer2.Deserialize(deserializer, src);
    }

    // we use here the fact that 99% of the strings we get are ascii and with length <= 64 (i.e., frames in headers)
    // so we do a simple check and if it's ascii, we do a quick conversion that gets auto-vectorized in il2cpp
    [MethodImpl(MethodImplOptions.AggressiveInlining), SkipLocalsInit]
    internal static unsafe string GetString(byte* srcPtr, int length)
    {
        if (length > 64 || StringHasUnicode(srcPtr, length))
        {
            return UTF8.GetString(srcPtr, length);
        }

        char* strPtr = stackalloc char[64];
        ConvertToChar(srcPtr, strPtr, length);
        return new string(strPtr, 0, length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static unsafe bool StringHasUnicode(byte* ptr, int size)
    {
        ulong result = 0;
        int remaining = size;

        ulong* ptrUlong = (ulong*)ptr;
        while (remaining >= 8)
        {
            result |= *ptrUlong++;
            remaining -= 8;
        }

        byte* ptrRemaining = (byte*)ptrUlong;
        switch (remaining)
        {
            case 7:
                result |= (ulong)*(uint*)ptrUlong
                          | *(ushort*)(ptrRemaining + 4)
                          | *(ptrRemaining + 6);
                break;
            case 6:
                result |= (ulong)*(uint*)ptrUlong
                          | *(ushort*)(ptrRemaining + 4);
                break;
            case 5:
                result |= (ulong)*(uint*)ptrUlong
                          | *(ptrRemaining + 4);
                break;
            case 4:
                result |= *(uint*)ptrUlong;
                break;
            case 3:
                result |= (ulong)*(ushort*)ptrRemaining
                          | *(ptrRemaining + 2);
                break;
            case 2:
                result |= *(ushort*)ptrRemaining;
                break;
            case 1:
                result |= *ptrRemaining;
                break;
            case 0:
                break;
        }

        return (result & 0x8080808080808080) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static unsafe void ConvertToChar(byte* srcPtr, char* strPtr, int size)
    {
        ulong* dstPtrUlong = (ulong*)strPtr;

        while (size >= 4)
        {
            ulong a0 = *srcPtr++;
            ulong a1 = *srcPtr++;
            ulong a2 = *srcPtr++;
            ulong a3 = *srcPtr++;

            *dstPtrUlong++ = (a3 << 48) | (a2 << 32) | (a1 << 16) | a0;
            size -= 4;
        }

        ushort* dstPtr = (ushort*)dstPtrUlong;
        switch (size)
        {
            case 3:
                *dstPtr++ = *srcPtr++;
                *dstPtr++ = *srcPtr++;
                *dstPtr = *srcPtr;
                return;
            case 2:
                *dstPtr++ = *srcPtr++;
                *dstPtr = *srcPtr;
                return;
            case 1:
                *dstPtr = *srcPtr;
                return;
            case 0:
                return;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SkipLocalsInit]
    internal static unsafe int GetByteCount(string str)
    {
        int length = str.Length;
        if (length <= 64)
        {
            fixed (char* strPtr = str)
            {
                if (!StringHasUnicode((ushort*)strPtr, length))
                {
                    return length;
                }
            }
        }

        return UTF8.GetByteCount(str);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static unsafe bool CanWriteStringSimple(char* strPtr, int length)
    {
        return length <= 64 && !StringHasUnicode((ushort*)strPtr, length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static unsafe void WriteStringSimple(char* strPtr, byte* dstPtr, int size)
    {
        ushort* srcPtr = (ushort*)strPtr;
        for (int i = 0; i < size; i++) dstPtr[i] = (byte)srcPtr[i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static unsafe bool StringHasUnicode(ushort* ptr, int size)
    {
        int halfSize = (size + 1) / 2;
        uint* halfPtr = (uint*)ptr;

        uint result = 0;
        for (int i = 0; i < halfSize; i++)
        {
            // overflows if size is odd!
            // but ptr (as all C# strings) has an extra ushort \0 at position [size] 
            result |= halfPtr[i];
        }

        return (result & 0xff80ff80) != 0;
    }

    public static Serializer<T> CreateSerializer<T>(this T t) where T : IMessage
    {
        return ((IHasSerializer<T>)t).CreateSerializer();
    }

    public static Deserializer<T> CreateDeserializer<T>(this T t) where T : IMessage
    {
        return ((IHasSerializer<T>)t).CreateDeserializer();
    }

    [AssertionMethod]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNull(object arg, string name)
    {
        if (arg is null) ThrowNullReference(name);
    }

    [AssertionMethod]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNull(object[] arg, string name)
    {
        if (arg is null) ThrowNullReference(name);

        int length = arg.Length;
        for (int i = 0; i < length; i++)
        {
            if (arg[i] is null) ThrowNullReference(name, i);
        }
    }

    [AssertionMethod]
    public static void ThrowIfWrongSize(Array arg, string name, int expectedLength)
    {
        int length = arg.Length;
        if (length != expectedLength) ThrowInvalidSizeForFixedArray(name, length, expectedLength);
    }

    [DoesNotReturn, AssertionMethod]
    public static void ThrowArgument([InvokerParameterName] string arg, string message) =>
        throw new ArgumentNullException(arg, message);

    [DoesNotReturn, AssertionMethod]
    public static void ThrowArgumentNull([InvokerParameterName] string arg) => throw new ArgumentNullException(arg);

    [DoesNotReturn, AssertionMethod]
    public static void ThrowArgumentNull([InvokerParameterName] string arg, string message) =>
        throw new ArgumentNullException(arg, message);

    [DoesNotReturn, AssertionMethod]
    public static void ThrowNullReference(string arg) => throw new NullReferenceException(arg);

    [DoesNotReturn, AssertionMethod]
    public static void ThrowNullReference(string arg, int i) =>
        throw new NullReferenceException($"{arg}[{i.ToString()}] cannot be null");

    [DoesNotReturn, AssertionMethod]
    public static void ThrowNullReference() => throw new NullReferenceException("Message fields cannot be null.");

    [DoesNotReturn, AssertionMethod]
    public static void ThrowBufferOverflow(int off, int remaining) =>
        throw new RosBufferException($"Requested {off.ToString()} bytes, but only {remaining.ToString()} remain!");

    [DoesNotReturn, AssertionMethod]
    public static void ThrowBufferOverflow() =>
        throw new RosBufferException($"Requested bytes beyond the buffer end!");

    [DoesNotReturn, AssertionMethod]
    public static void ThrowImplausibleBufferSize() =>
        throw new RosBufferException("Message requested an array with negative size or an " +
                                     "implausible amount of more than 1TB elements.");

    [DoesNotReturn, AssertionMethod]
    public static void ThrowArgumentOutOfRange() => throw new ArgumentOutOfRangeException();

    [DoesNotReturn, AssertionMethod]
    public static void ThrowArgumentOutOfRange(string arg) => throw new ArgumentOutOfRangeException(arg);

    [DoesNotReturn, AssertionMethod]
    public static void ThrowInvalidSizeForFixedArray(int size, int expected) =>
        throw new RosInvalidSizeForFixedArrayException(size, expected);

    [DoesNotReturn, AssertionMethod]
    public static void ThrowInvalidSizeForFixedArray(string arg, int size, int expected) =>
        throw new RosInvalidSizeForFixedArrayException(arg, size, expected);

    [DoesNotReturn]
    public static void ThrowObjectDisposed(string name) =>
        throw new ObjectDisposedException(name);

    [DoesNotReturn]
    public static void ThrowObjectDisposed(string name, string message) =>
        throw new ObjectDisposedException(name, message);

    [DoesNotReturn]
    public static IntPtr ThrowPointerDisposed(string name) =>
        throw new ObjectDisposedException(name);
}
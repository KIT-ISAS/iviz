using System;

namespace Iviz.Msgs;

public static class EmptyArray<T>
{
#if NETSTANDARD2_1
    public static readonly T[] Value = Array.Empty<T>(); // remove indirection for il2cpp
#else
    public static T[] Value => Array.Empty<T>();
#endif
}
using System;
using Unity.IL2CPP.CompilerServices;

namespace Iviz.Msgs;

[Il2CppEagerStaticClassConstruction]
public static class EmptyArray
{
#if NETSTANDARD2_1 // remove indirection for il2cpp
    public static readonly string String = "";
    public static readonly string[] StringArray = Array.Empty<string>();
#else
    public const string String = "";
    public static string[] StringArray => Array.Empty<string>();
#endif
}

[Il2CppEagerStaticClassConstruction]
public static class EmptyArray<T>
{
#if NETSTANDARD2_1 // remove indirection for il2cpp
    public static readonly T[] Value = Array.Empty<T>();
#else
    public static T[] Value => Array.Empty<T>();
#endif
}
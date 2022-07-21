﻿using Iviz.Msgs;

namespace Iviz.Roslib.Utils;

/// <summary>
///     Simple class that overrides the ToString() method to produce a JSON representation.
/// </summary>
public abstract class JsonToString
{
    public sealed override string ToString() => BuiltIns.ToJsonString(this);
}
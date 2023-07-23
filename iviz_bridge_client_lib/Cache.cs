using System;

namespace Iviz.Bridge.Client;

internal struct Cache<T> where T : class
{
    T? value;
    DateTime expiration;

    public T? TryGet() => DateTime.UtcNow <= expiration ? value : null;

    public bool TryGet(out T? t)
    {
        t = value;
        return DateTime.UtcNow <= expiration;
    }

    public T Value
    {
        set
        {
            this.value = value;
            expiration = DateTime.UtcNow + TimeSpan.FromSeconds(3);
        }
    }
}
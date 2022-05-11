#nullable enable
using System;

namespace Iviz.Core
{
    /// <summary>
    /// Thrown when an asset is missing but is expected to be there. Only used for internal iviz errors.
    /// </summary>s
    public class MissingAssetFieldException : Exception
    {
        public MissingAssetFieldException(string message) : base(message)
        {
        }
    }
}
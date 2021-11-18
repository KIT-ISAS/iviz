#nullable enable

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;

namespace Iviz.Core
{
    public interface IScreenCaptureManager
    {
        bool Started { get; }

        IEnumerable<(int width, int height)> GetResolutions();

        ValueTask StartAsync(int width, int height, bool withHolograms);
        ValueTask StopAsync();

        ValueTask<Screenshot?> CaptureColorAsync(int reuseCaptureAgeInMs = 0, CancellationToken token = default);
        ValueTask<Screenshot?> CaptureDepthAsync(int reuseCaptureAgeInMs = 0, CancellationToken token = default);
    }
}
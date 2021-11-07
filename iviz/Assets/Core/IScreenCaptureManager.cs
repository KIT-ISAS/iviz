#nullable enable

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.Core
{
    public interface IScreenCaptureManager
    {
        bool Started { get; }

        IEnumerable<(int width, int height)> GetResolutions();

        Task StartAsync(int width, int height, bool withHolograms);
        Task StopAsync();

        ValueTask<Screenshot?> CaptureColorAsync(int reuseCaptureAgeInMs = 0, CancellationToken token = default);
        ValueTask<Screenshot?> CaptureDepthAsync(int reuseCaptureAgeInMs = 0, CancellationToken token = default);
    }
}
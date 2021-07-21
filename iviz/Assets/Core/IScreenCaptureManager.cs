using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Iviz.Core
{
    public interface IScreenCaptureManager
    {
        bool Started { get; }

        [NotNull]
        IEnumerable<(int width, int height)> GetResolutions();

        Task StartAsync(int width, int height, bool withHolograms);
        Task StopAsync();

        [ItemCanBeNull]
        ValueTask<Screenshot> CaptureColorAsync(int reuseCaptureAgeInMs = 0, CancellationToken token = default);

        [ItemCanBeNull]
        ValueTask<Screenshot> CaptureDepthAsync(int reuseCaptureAgeInMs = 0, CancellationToken token = default);
    }
}
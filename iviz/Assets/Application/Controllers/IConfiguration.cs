using Iviz.Resources;
using System;

namespace Iviz.Controllers
{
    public interface IConfiguration
    {
        Guid Id { get; }
        Resource.Module Module { get; }
        bool Visible { get; set; }
    }
}

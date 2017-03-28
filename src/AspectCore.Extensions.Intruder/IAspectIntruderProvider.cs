using System;
using System.Threading.Tasks;
using AspectCore.Abstractions;

namespace AspectCore.Extensions.Intruder
{
    public interface IAspectIntruderProvider
    {
        Type IntruderType { get; }

        Task<IAspectIntruder> GetIntruderAsync(AspectContext context);
    }
}
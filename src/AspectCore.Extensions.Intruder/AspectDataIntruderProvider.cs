using System;
using System.Threading.Tasks;
using AspectCore.Abstractions;

namespace AspectCore.Extensions.Intruder
{
    public class AspectDataIntruderProvider : IAspectIntruderProvider
    {
        public Type IntruderType
        {
            get
            {
                return typeof(AspectDataIntruder);
            }
        }

        public Task<IAspectIntruder> GetIntruderAsync(AspectContext context)
        {
            return Task.FromResult<IAspectIntruder>(new AspectDataIntruder(context.Data));
        }
    }
}

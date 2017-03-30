using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.Abstractions;

namespace AspectCore.Extensions.Intruder
{
    public sealed class AspectIntruderAttribute : InterceptorAttribute
    {
        public override int Order { get; set; } = -300;

        public override bool AllowMultiple { get; } = false;

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var intruderProviders = (IEnumerable<IAspectIntruderProvider>)context.ServiceProvider.GetService(typeof(IEnumerable<IAspectIntruderProvider>));
            var intruderProvidersMap = intruderProviders.ToDictionary(x => x.IntruderType, x => x);
            foreach (var parameter in context.Parameters)
            {
                var paraType = parameter.ParameterType;
                IAspectIntruderProvider intruderProvider;
                if (intruderProvidersMap.TryGetValue(paraType, out intruderProvider))
                {
                    var intruder = await intruderProvider.GetIntruderAsync(context);
                    parameter.Value = intruder;
                    context.Data[$"Intruder_{paraType.Name}"] = intruder;
                }
            }
            await next(context);
        }
    }
}
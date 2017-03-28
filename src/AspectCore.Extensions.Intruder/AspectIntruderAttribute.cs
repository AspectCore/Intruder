using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.Abstractions;

namespace AspectCore.Extensions.Intruder
{
    public sealed class AspectIntruderAttribute : InterceptorAttribute
    {
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var intruders = (IEnumerable<IAspectIntruderProvider>)context.ServiceProvider.GetService(typeof(IEnumerable<IAspectIntruderProvider>));
            var intrudersMap = intruders.ToDictionary(x => x.IntruderType, x => x);
            foreach (var parameter in context.Parameters)
            {
                var paraType = parameter.ParameterType;
                IAspectIntruderProvider intruderProvider;
                if (intrudersMap.TryGetValue(paraType, out intruderProvider))
                {
                    var intruder = await intruderProvider.GetIntruderAsync(context);
                    parameter.Value = intruder;
                    context.Items[$"Intruder_{paraType.Name}"] = intruder;
                }
            }
            await next(context);
        }
    }
}
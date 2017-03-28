using AspectCore.Abstractions;

namespace AspectCore.Extensions.Intruder
{
    public class AspectDataIntruder : IAspectIntruder
    {
        public DynamicDictionary Data { get; }

        public AspectDataIntruder(DynamicDictionary data)
        {
            Data = data;
        }
    }
}
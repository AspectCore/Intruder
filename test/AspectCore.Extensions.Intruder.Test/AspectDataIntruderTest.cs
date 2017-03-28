using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using AspectCore.Extensions.DependencyInjection;

namespace AspectCore.Extensions.Intruder.Test
{
    public class AspectDataIntruderTest
    {
        [Fact]
        public void Test()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IAspectIntruderProvider, AspectDataIntruderProvider>();
            services.AddTransient<TestService>();
            var serviceProvider = services.BuildAspectCoreServiceProvider();
            var testService = serviceProvider.GetService<TestService>();
            testService.Foo();
        }
    }

    public class TestAttribute : InterceptorAttribute
    {
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            context.Data["key"] = "test";

            return base.Invoke(context, next);
        }
    }

    public class TestService
    {
        [Test]
        [AspectIntruder]
        public virtual void Foo(AspectDataIntruder intruder = null)
        {
            Assert.Equal(intruder.Data["key"], "test");
        }
    }
}

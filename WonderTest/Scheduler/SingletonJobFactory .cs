using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Oculus.Scheduler
{
    public class SingletonJobFactory: IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        [ExcludeFromCodeCoverage]
        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [ExcludeFromCodeCoverage]
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        [ExcludeFromCodeCoverage]
        public void ReturnJob(IJob job) { }
    }
}

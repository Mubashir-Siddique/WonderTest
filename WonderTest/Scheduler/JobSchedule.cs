using System;
using System.Diagnostics.CodeAnalysis;

namespace Oculus.Scheduler
{
    public class JobSchedule
    {
        [ExcludeFromCodeCoverage]
        public JobSchedule(Type jobType, string cronExpression)
        {
            JobType = jobType;
            CronExpression = cronExpression;
        }

        [ExcludeFromCodeCoverage]
        public Type JobType { get; }
        [ExcludeFromCodeCoverage]
        public string CronExpression { get; }
    }
}

using GameAnalytics.Core;
using GameAnalytics.Model;
using Quartz;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace WonderTest.Scheduler
{
    public class GameReportEmailScheduler : IJob
    {
        static IRepository<GamesAnalytics> _iGameRepository;

        GameCore gameCore = new GameCore(_iGameRepository);

        [ExcludeFromCodeCoverage]
        public Task Execute(IJobExecutionContext context)
        {
            gameCore.SendUserProgressEmail();   // our email Scheduler will run this method on monthly/daily/weekly basis. 
            return Task.CompletedTask;
        }
    }
}

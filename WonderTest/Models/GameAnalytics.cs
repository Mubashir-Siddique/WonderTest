using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameAnalytics.Model
{
    public class GamesAnalytics
    {
        [Key]
        public long Id { get; set; }
        public int Game_Metric_Score_1 { get; set; }
        public int Game_Metric_Score_2 { get; set; }
        public DateTime Time { get; set; }
        public Guid UserId { get; set; }

    }
}

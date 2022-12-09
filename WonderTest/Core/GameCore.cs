using System;
using System.Linq;
using GameAnalytics.Model;
using System.Net.Mail;
using WonderTest.Models;
using GameAnalytics.Enumerations;
using System.Collections.Generic;
using System.Globalization;

namespace GameAnalytics.Core
{
    public class GameCore
    {
        private readonly GameContext _gameEntities;
        private IRepository<GamesAnalytics> _iRepository;
        APIResponse apirespone = new APIResponse();

        public GameCore(IRepository<GamesAnalytics> iRepository)
        {
            _iRepository = iRepository;
        }

        #region UsingEFSQL
        public APIResponse CreateUpdateGameData(GamesAnalytics model)
        {
            GamesAnalytics gameObj = new GamesAnalytics();

            gameObj.Game_Metric_Score_1 = model.Game_Metric_Score_1;
            gameObj.Game_Metric_Score_2 = model.Game_Metric_Score_2;
            gameObj.Time = model.Time;
            gameObj.UserId = model.UserId;

            _gameEntities.GamesAnalytics.Add(gameObj);

            var report = _iRepository.ExecuteQuery<GamesAnalytics>(model, Constant.SP_CreateUpdateGameData).FirstOrDefault();
            apirespone.Response = report;
            return apirespone;

        }
        public APIResponse GetGameAnalytics(SearchGameAnalyticModel model)
        {
            apirespone.Response = GetDateRangeBasedOnEvent(model.StartDate, model.EndDate, (int)model.Event);
            return apirespone;
        }
        public APIResponse SendUserProgressEmail()
        {
            var users = _gameEntities.Users.AsQueryable().ToList<Users>();

            foreach (var item in users)
            {
                try
                {
                    var data = _gameEntities.GamesAnalytics
                        
                        .Where(x => x.UserId == item.UserID
                            && x.Time.Month == DateTime.Now.Month)    
                        .AsQueryable().ToList<GamesAnalytics>();

                    // TO DO
                    // Write logic for parsing the data and creating a progress report in the email

                    MailMessage newMail = new MailMessage();
                    SmtpClient client = new SmtpClient(Constant.SmtpHost);
                    newMail.From = new MailAddress(Constant.FromEmail, Constant.SenderName);
                    newMail.To.Add(item.Email);
                    newMail.Subject = String.Format("");

                    client.EnableSsl = true;
                    client.Port = 465;
                    client.Credentials = new System.Net.NetworkCredential(Constant.FromEmail, Constant.FromPassword);

                    client.Send(newMail);
                    Console.WriteLine(Constant.EmailSentResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error -" + ex);
                }
            }
            apirespone.Response = Constant.EmailResponse;
            return apirespone;
        } 
        #endregion

        #region UsingDapperSQL

        public APIResponse CreateUpdateGameData_Repository(GamesAnalytics model)
        {
            var report = _iRepository.ExecuteQuery<GamesAnalytics>(model, GameAnalytics.Constant.Constant.SP_CreateUpdateGameData).FirstOrDefault();
            apirespone.Response = report;
            return apirespone;
        }
        public APIResponse GetGameAnalytics_Repository(GamesAnalytics model)
        {
            var report = _iRepository.Search<GamesAnalytics>(model, GameAnalytics.Constant.Constant.SP_GetGameAnalytics).ToList();
            apirespone.Response = report;
            return apirespone;
        }

        #endregion

        public List<ResponseModel> GetDateRangeBasedOnEvent(DateTime start, DateTime end, int eventType)
        {
            List<ResponseModel> DayEventData = new List<ResponseModel>();
            try
            {
                var Data = _gameEntities.GamesAnalytics
                                .Where(x => x.Time >= start && x.Time <= end)
                                .AsQueryable();

                switch (eventType)
                {
                    case 1: // Day Case
                        {
                            DayEventData = Data.GroupBy(x => x.Time.Day)
                                .Select(c => new ResponseModel
                                {
                                    Time = c.First().Time,
                                    Game_Metric_Score_1 = c.Sum(s=> s.Game_Metric_Score_1),
                                    Game_Metric_Score_2 = c.Sum(s=> s.Game_Metric_Score_2),
                                    Count = c.Count()
                                }).ToList();

                            return DayEventData;
                        }                    
                    case 2: // Week Case
                        {

                            DayEventData = Data.GroupBy(i => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                        i.Time, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                                .Select(c => new ResponseModel
                                {
                                    Time = c.First().Time,
                                    Game_Metric_Score_1 = c.Sum(s => s.Game_Metric_Score_1),
                                    Game_Metric_Score_2 = c.Sum(s => s.Game_Metric_Score_2),
                                    Count = c.Count()
                                }).ToList();

                            return DayEventData;
                        }                    
                    case 3: // Month Case
                        {
                            DayEventData = Data.GroupBy(x => x.Time.Month)
                                .Select(c => new ResponseModel
                                {
                                    Time = c.First().Time,
                                    Game_Metric_Score_1 = c.Sum(s => s.Game_Metric_Score_1),
                                    Game_Metric_Score_2 = c.Sum(s => s.Game_Metric_Score_2),
                                    Count = c.Count()
                                }).ToList();

                            return DayEventData;
                        }                    
                    case 4:
                        {
                            DayEventData = Data.GroupBy(x => x.Time.Year)
                                .Select(c => new ResponseModel
                                {
                                    Time = c.First().Time,
                                    Game_Metric_Score_1 = c.Sum(s => s.Game_Metric_Score_1),
                                    Game_Metric_Score_2 = c.Sum(s => s.Game_Metric_Score_2),
                                    Count = c.Count()
                                }).ToList();

                            return DayEventData;
                        }
                    default:
                        return DayEventData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DayEventData;
            }
        }
    }
}

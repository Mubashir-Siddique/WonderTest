using GameAnalytics.Core;
using GameAnalytics.Enumerations;
using GameAnalytics.Model;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WonderTest.Core
{
    public class UserCore
    {
        private readonly GameContext _gameEntities;
        private IRepository<Users> _iRepository;
        APIResponse apirespone = new APIResponse();

        public UserCore(IRepository<Users> iRepository)
        {
            _iRepository = iRepository;
        }

        #region UsingEFSQL

        public async Task<APIResponse> CreateUpdateUser(Users model)
        {
            Users user = new Users();

            user.Full_Name = model.Full_Name;
            user.Email = model.Email;

            _gameEntities.Users.Add(user);
            await _gameEntities.SaveChangesAsync();

            var report = _iRepository.ExecuteQuery<GamesAnalytics>(model, Constant.SP_CreateUpdateGameData).FirstOrDefault();
            apirespone.Response = user;
            return apirespone;

        }
        public APIResponse GetAllUsers()
        {
            IQueryable<Users> criteria = _gameEntities.Users.AsQueryable();
            apirespone.Response = criteria.ToList();
            return apirespone;
        }

        #endregion


        #region UsingDapperSQL

        public APIResponse CreateUpdateUser_Dapper(Users model)
        {
            var report = _iRepository.ExecuteQuery<GamesAnalytics>(model, Constant.SP_CreateUpdateUsers).FirstOrDefault();
            apirespone.Response = report;
            return apirespone;
        }
        public APIResponse GetUsers_Dapper(GamesAnalytics model)
        {
            var report = _iRepository.Search<GamesAnalytics>(model, Constant.SP_GetAllUser).ToList();
            apirespone.Response = report;
            return apirespone;
        }

        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAnalytics.Model;
using GameAnalytics.Core;
using WonderTest.Models;
using WonderTest.Core;

namespace GameAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        GameCore gameCore;
        UserCore usercore;
        public GameController(IRepository<GamesAnalytics> irepository, IRepository<Users> iUserRepository)
        {
            gameCore = new GameCore(irepository);
            usercore = new UserCore(iUserRepository);
        }

        [HttpPost]
        [Route("CreateUpdateGameData")]
        public IActionResult CreateUpdateGameData(GamesAnalytics model)
        {
            return Ok(gameCore.CreateUpdateGameData(model));
        }

        [HttpGet]
        [Route("GetGameAnalytics")]
        public IActionResult GetGameAnalytics(SearchGameAnalyticModel model)
        {
            return Ok(gameCore.GetGameAnalytics(model));
        }

        [HttpPost]
        [Route("CreateUpdateUser")]
        public IActionResult CreateUpdateUser(Users model)
        {
            return Ok(usercore.CreateUpdateUser(model));
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(usercore.GetAllUsers());
        }
    }
}

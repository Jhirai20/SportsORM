using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsORM.Models;


namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            ViewBag.WomensLeague = _context.Leagues.Where(l=>l.Name.Contains("Women"));
            ViewBag.HockeyLeagues =_context.Leagues.Where(l => l.Sport.Contains("Hockey")); 
            ViewBag.NotFootballLeagues=_context.Leagues.Where(l =>l.Sport !="Football");
            ViewBag.ConferenceLeagues=_context.Leagues.Where(l => l.Name.Contains("Conference"));
            ViewBag.AtlanticLeagues=_context.Leagues.Where(l => l.Name.Contains("Atlantic"));
            ViewBag.DallasTeams=_context.Teams.Where(l =>l.Location.Contains("Dallas"));
            ViewBag.RaptorTeams=_context.Teams.Where(l =>l.TeamName.Contains("Raptor"));
            ViewBag.CityTeams=_context.Teams.Where(l =>l.Location.Contains("City"));
            ViewBag.TTeams=_context.Teams.Where(l => l.TeamName.StartsWith("T"));
            ViewBag.OrderedTeam=_context.Teams.OrderBy(l =>l.Location);
            ViewBag.RevOrdTeams=_context.Teams.OrderByDescending(l =>l.TeamName);
            ViewBag.CooperPlayer=_context.Players.Where(l =>l.LastName.Contains("Cooper"));
            ViewBag.JoshuaPlayers=_context.Players.Where(l => l.FirstName.Contains("Joshua"));
            ViewBag.CooperNoJPlayer=_context.Players.Where(l =>l.LastName=="Cooper" && l.FirstName != "Joshua");
            ViewBag.AlexWyattPlayers=_context.Players.Where(l => l.FirstName=="Alexander" || l.FirstName=="Wyatt");

            return View();
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}
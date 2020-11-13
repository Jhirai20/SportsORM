using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsORM.Models;

using Microsoft.EntityFrameworkCore;

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
            ViewBag.AllTeamAlanticSoccerConfrence=_context.Teams.Include(teams => teams.CurrLeague)
            .Where(teams => teams.CurrLeague.Name
            .Contains("Atlantic Soccer Conference")).ToList();

            ViewBag.BostonPenguinePlayers=_context.Players.Include(players => players.CurrentTeam)
            .Where(players=>players.CurrentTeam.TeamName=="Penguins"&&players.CurrentTeam.Location=="Boston").ToList();

            ViewBag.ICBCPlayers=_context.Players.Include(players=>players.CurrentTeam)
            .Where(players=>players.CurrentTeam.CurrLeague.Name== "International Collegiate Baseball Conference").ToList();

            ViewBag.ACAFPlayers=_context.Players.Include(p=>p.CurrentTeam)
            .Where(p=>p.CurrentTeam.CurrLeague.Name=="American Conference of Amateur Football" && p.LastName=="Davis").OrderBy(p=>p.LastName).ToList();

            ViewBag.AllFPlayers=_context.Players.Include(p=>p.CurrentTeam)
            .Where(p=>p.CurrentTeam.CurrLeague.Sport=="Football").ToList();

            ViewBag.SophiaTeams=_context.Teams.Include(t=>t.CurrentPlayers).Where(t=>t.CurrentPlayers.Any(p=>p.FirstName=="Sophia")).ToList();

            ViewBag.SophiaLList=_context.Teams.Include(t => t.CurrentPlayers)
                                      .Where(t => t.CurrentPlayers
                                      .Any(p => p.FirstName == "Sophia"))
                                      .ToList();

            ViewBag.AllFlores=_context.Players.Include(p =>p.CurrentTeam).Where(p =>p.CurrentTeam.TeamName != "Roughriders" && p.LastName =="Flores").ToList();
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            ViewBag.Evans=_context.Players.Include(p=>p.AllTeams).ThenInclude(t=>t.TeamOfPlayer).FirstOrDefault(p => p.FirstName == "Samuel" && p.LastName == "Evans");
            ViewBag.two=_context.Teams.Include(p=>p.AllPlayers).ThenInclude(p=>p.PlayerOnTeam).FirstOrDefault(t=>t.Location=="Manitoba" && t.TeamName=="Tiger-Cats");
            ViewBag.three=_context.PlayerTeams.Include(p=>p.PlayerOnTeam).ThenInclude(t=>t.CurrentTeam).Include(t=>t.TeamOfPlayer).Where(pt => pt.TeamOfPlayer.Location == "Wichita" && pt.TeamOfPlayer.TeamName == "Vikings") .Where(pt => pt.PlayerOnTeam.CurrentTeam.Location != "Wichita" && pt.PlayerOnTeam.CurrentTeam.TeamName != "Vikings");
            ViewBag.four=_context.PlayerTeams.Include(p=>p.PlayerOnTeam).ThenInclude(p=>p.CurrentTeam).Include(p=>p.TeamOfPlayer).Where(p=>p.PlayerOnTeam.FirstName== "Jacob" && p.PlayerOnTeam.LastName=="Gray").Where(p=>p.TeamOfPlayer.Location != "Oregon" &&p.TeamOfPlayer.TeamName!="Colts");
            ViewBag.five=_context.PlayerTeams.Include(t=>t.TeamOfPlayer).ThenInclude(p=>p.CurrLeague).Include(p=>p.PlayerOnTeam).Where(p=>p.PlayerOnTeam.FirstName== "Levi").Where(p=>p.TeamOfPlayer.CurrLeague.Name=="Atlantic Federation of Amateur Baseball Players").ToList();
            ViewBag.six=_context.Teams.Include(t=>t.CurrentPlayers).Include(p=>p.AllPlayers).Where(p => p.CurrentPlayers.Count+ p.AllPlayers.Count >=12).ToList();
            ViewBag.seven=_context.Players.Include(t=>t.AllTeams).OrderByDescending(p => p.AllTeams.Count).ToList();


            return View();
        }

    }
}
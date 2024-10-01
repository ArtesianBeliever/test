using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly DataContext _context;

        public TeamController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Team>>> GetTeams()
        {
            var teams = await _context.Teams.ToListAsync();
            
            return Ok(teams);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeamById(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
                return NotFound("Team not found");
            return Ok(team);
        }
        [HttpPost]
        public async Task<ActionResult<List<Team>>> AddTeam(Team team)
        {
            int maxId = _context.Teams.Max(x => x.Id);
            var id = Enumerable.Range(1, maxId + 1).Except(_context.Teams.Select(x => x.Id)).First();
            team.Id = id;
            _context.Teams.Add(team);
            
            await _context.SaveChangesAsync();
            return Ok(_context.Teams.ToList());
        }
        [HttpPut]
        public async Task<ActionResult<List<Team>>> UpdateTeam(Team team)
        {
            var dbTeam = await _context.Teams.FindAsync(team.Id);
            if (dbTeam == null)
                return NotFound("Team not found");
            
            dbTeam.Name = team.Name;
            dbTeam.Tag = team.Tag;
            dbTeam.Region = team.Region; 
            
            //Сделать методы для каждого столбца?
            
            await _context.SaveChangesAsync(); 
            
            return Ok(_context.Teams.ToList());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
                return NotFound("Team not found");
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return Ok(team);
        }
    }

};


using Kolokwium_2.Models;
using Kolokwium_2.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium_2.Services
{
    public class TeamService : ITeamService
    {
        private readonly KolokwiumContext _context;
        public TeamService(KolokwiumContext context)
        {
            _context = context;
        }

        public async Task AddMember(MemberPut member)
        {
            var newmember = new Models.Member
            {
                OrganizationID = member.OrganizationID,
                MemberName = member.MemberName,
                MemberSurname = member.MemberSurname,
                MemberNickName = member.MemberSurname
            };

            var entry = _context.Entry(newmember);
            entry.State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        async Task<List<TeamDTO>> ITeamService.GetTeam(int id)
        {
            return await _context.Team
                .Include(e => e.Memberships)
                .ThenInclude(e => e.Member)
                .Include(e => e.Organization)
                .Where(e => e.TeamID == id)
                .Select(e => new TeamDTO
                {
                    TeamName = e.TeamName,
                    TeamDescription = e.TeamDescription,
                    OrganizationName = e.Organization.OrganizationName,
                    Members = e.Memberships.Select(e => new Models.DTO.Member
                    {
                        MemberName = e.Member.MemberName,
                        MemberSurname = e.Member.MemberSurname,
                        MemberNickName = e.Member.MemberNickName
                    }).ToList()
                }).ToListAsync();
        }

        public IQueryable<Team> GetAId(int id)
        {
            return _context.Team.Where(e => e.TeamID == id);
        }

    }
}

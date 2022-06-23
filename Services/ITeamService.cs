using Kolokwium_2.Models;
using Kolokwium_2.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium_2.Services
{
    public interface ITeamService
    {
        public Task AddMember(MemberPut member);
        public Task<List<TeamDTO>> GetTeam(int id);
        IQueryable<Team> GetAId(int id);
    }
}

using Kolokwium_2.Models;
using Kolokwium_2.Models.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium_2.Services
{
    public interface IAlbumService
    {
        public Task DeleteMusician(int id);
        public Task<AlbumDTO> GetAlbums(int id);
        IQueryable<Album> GetAId(int id);
        IQueryable<Musician> GetId(int id);
    }
}

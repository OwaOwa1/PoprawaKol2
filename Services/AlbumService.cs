using Kolokwium_2.Models;
using Kolokwium_2.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium_2.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly KolokwiumContext _context;
        public AlbumService(KolokwiumContext context)
        {
            _context = context;
        }

        public async Task DeleteMusician(int id)
        {
            var del = await _context.Musician.FindAsync();
            _context.Remove(del);
            await _context.SaveChangesAsync();
        }

        async Task<AlbumDTO> IAlbumService.GetAlbums(int id)
        {
            var album = await _context.Album.FindAsync(id);

            AlbumDTO albums = await _context
                .Album
                .Where(e => e.IdAlbum == id)
                .Select(e => new AlbumDTO
                {
                    AlbumName = e.AlbumName,
                    PublishDate = e.PublishDate,
                    tracks = (System.Collections.Generic.List<TrackDTO>)e.Tracks.Select(e => new TrackDTO
                    {
                        TrackName = e.TrackName,
                        Duration = e.Duration
                    })
                }).FirstAsync();
            return albums;
        }

        public IQueryable<Album> GetAId(int id)
        {
            return _context.Album.Where(e => e.IdAlbum == id);
        }
        public IQueryable<Musician> GetId(int id)
        {
            return _context.Musician.Where(e => e.IdMusician == id);
        }
    }
}

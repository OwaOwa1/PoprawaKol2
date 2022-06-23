using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium_2.Models
{
    public class Album
    {
        public int IdAlbum { get; set; }
        public string AlbumName { get; set; }
        public DateTime PublishDate { get; set; }

        public virtual IEnumerable<Track> Tracks { get; set; }
        public virtual MusicLabel IdMusicLabel { get; set; }

    }
}

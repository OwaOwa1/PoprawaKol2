using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium_2.Models.DTO
{
    public class AlbumDTO
    {
        public string AlbumName { get; set; }
        public DateTime PublishDate { get; set; }
        public List<TrackDTO> tracks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium_2.Models
{
    public class Track
    {
        public int IdTrack { get; set; }
        public string TrackName { get; set; }
        public float Duration { get; set; }

        public virtual IEnumerable<Musician_Track> Musician_Tracks { get; set; }
        public virtual Album Album { get; set; }

        public static implicit operator Track(int v)
        {
            throw new NotImplementedException();
        }
    }
}

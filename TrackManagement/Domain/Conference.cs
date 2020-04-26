using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackManagement.Domain
{
    public class Conference
    {
        private List<Track> _Tracks;

        public Conference(List<Track> tracks)
        {
            _Tracks = tracks;
        }
        public List<Track> Tracks
        {
            get { return _Tracks; }
        }
    }
}

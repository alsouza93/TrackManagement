using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackManagement.Domain
{
    public class Track
    {
        private List<Talk> _MorningTalks;
        private List<Talk> _AfternoonTalks;

        public Track(string titleTrack)
        {
            Title = titleTrack;
            _MorningTalks = new List<Talk>();
            _AfternoonTalks = new List<Talk>();
        }
        public string Title { get; set; }
        public bool HasNetworking { get; set; }
        public List<Talk> MorningTalks
        {
            get { return _MorningTalks; }

        }
        public List<Talk> AfternoonTalks
        {
            get { return _AfternoonTalks; }
        }

        public void AdicionarTalk(Talk talk, SessionType sessionType)
        {

            if (sessionType == SessionType.MorningSession)
                _MorningTalks.Add(talk);
            else
                _AfternoonTalks.Add(talk);
        }
    }

    public enum SessionType
    {
        MorningSession,
        AfternoonSession
    }
}

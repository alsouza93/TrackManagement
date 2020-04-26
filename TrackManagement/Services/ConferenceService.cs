using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackManagement.Domain;
using TrackManagement.Services.Interfaces;

namespace TrackManagement.Services
{
    public class ConferenceService : IConferenceService
    {

        private DateTime FourPM = DateTime.Today.Add(new TimeSpan(16, 00, 00));
        private DateTime FivePM = DateTime.Today.Add(new TimeSpan(17, 00, 00));
        private const int SessionStartsAt = 9;
        private const int SessionEndsAt = 17;
        private const int LunchHour = 12;
        private const int MinutesPerHour = 60;
        private const int TotalMinPerTrack = (SessionEndsAt - SessionStartsAt - 1) * MinutesPerHour;
        private const int TotalMinInMorningSession = 60 * (LunchHour - SessionStartsAt);
        private const int TotalMinInAfterNoonSession = 60 * (SessionEndsAt - LunchHour - 1);

        private DateTime Hour;
        private string _Error;
        public string Error
        {
            get { return _Error; }
        }

        private void IncludeTalkText(StringBuilder texto, List<Talk> talks)
        {
            foreach (var talk in talks)
            {
                texto.AppendLine(string.Format("{0:hh:mm tt} {1} {2}", talk.Start, talk.Title, talk.DurationFormat));
            }


        }
        private List<Talk> AllocateSessions(List<Talk> talks, List<Track> tracks, int trackIndex, int totalNumOfMinutes, SessionType sessionType)
        {
            talks = talks.OrderBy(o => o.Duration).ToList();

            for (int i = talks.Count - 1; i >= 0; i--)
            {
                if (totalNumOfMinutes < talks[i].Duration)
                    break;

                talks[i].Start = Hour;
                totalNumOfMinutes = totalNumOfMinutes - talks[i].Duration;
                Hour = Hour.AddMinutes(talks[i].Duration);
                tracks[trackIndex].AdicionarTalk(talks[i], sessionType);
                talks.RemoveAt(i);

            }
            return talks;
        }
        public Conference ScheduleTalks(List<Talk> talks)
        {
            if (talks.Count() == 0)
            {
                _Error = "No talks scheduled.";
                return null;
            }

            var tracks = new List<Track>();
            Double totalDuration = talks.Sum(s => s.Duration);

            var numOfTracks = (totalDuration < TotalMinPerTrack) ? 1 : (int)Math.Ceiling(totalDuration / TotalMinPerTrack);

            for (int i = 0; i < numOfTracks; ++i)
            {
                Hour = DateTime.Today.Add(new TimeSpan(SessionStartsAt, 00, 00));
                tracks.Add(new Track(string.Format("Track {0}", i + 1)));
                talks = AllocateSessions(talks, tracks, i, TotalMinInMorningSession, SessionType.MorningSession);
                Hour = DateTime.Today.Add(new TimeSpan(LunchHour + 1, 00, 00));
                talks = AllocateSessions(talks, tracks, i, TotalMinInAfterNoonSession, SessionType.AfternoonSession);
                if (DateTime.Compare(Hour, FourPM) >= 0 &&
                    DateTime.Compare(Hour, FivePM) < 0)
                    tracks[i].HasNetworking = true;
            }
            var conference = new Conference(tracks);
            return conference;
        }
        public StringBuilder VisualizeSchedule(Conference conference)
        {
            if (conference == null)
            {
                _Error = "Inform a Conference.";
                return null;
            }

            if (conference.Tracks.Count == 0)
            {
                _Error = "This Conference has no Tracks to visualize.";
                return null;
            }
            var texto = new StringBuilder();
            foreach (var track in conference.Tracks)
            {
                texto.AppendLine(track.Title);
                texto.AppendLine();
                IncludeTalkText(texto, track.MorningTalks);
                var lunchHour = DateTime.Today.Add(new TimeSpan(LunchHour, 00, 00));
                texto.AppendLine(string.Format("{0:hh:mm tt} Lunch", lunchHour));
                IncludeTalkText(texto, track.AfternoonTalks);
                if (track.HasNetworking)
                    texto.AppendLine(string.Format("{0:hh:mm tt} Networking Event", FivePM));
                texto.AppendLine();
            }
            return texto;
        }

    }
}

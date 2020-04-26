using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackManagement.Domain;
using TrackManagement.Services;

namespace TrackManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inform a talk list:\n\n");
            string line;
            var talks = new List<Talk>();
            var conferenceService = new ConferenceService();
            var talkService = new TalkService();
            while ((line = Console.ReadLine()).Any())
            {
                var talk = talkService.IncludeTalk(line);
                if (talk == null)
                {
                    Console.WriteLine(talkService.Error);
                    continue;
                }
                talks.Add(talk);
            }
            var conference = conferenceService.ScheduleTalks(talks);
            if (conference == null)
            {
                Console.WriteLine(conferenceService.Error);
                return;
            }
            var schedule = conferenceService.VisualizeSchedule(conference);
            if (schedule == null)
                Console.WriteLine(conferenceService.Error);
            else
                Console.WriteLine(schedule.ToString());
        }
    }
}

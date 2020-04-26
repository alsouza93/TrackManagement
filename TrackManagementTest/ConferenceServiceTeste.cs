using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrackManagement.Domain;
using TrackManagement.Services;

namespace TrackManagementTest
{

    [TestClass]
    public class ConferenceServiceTeste
    {
        public List<Talk> DefaultTalks = new List<Talk>();
        [TestInitialize]
        public void Initialize()
        {
            var stringTalkList = new List<string>();
            stringTalkList.Add("Writing Fast Tests Against Enterprise Rails 60min");
            stringTalkList.Add("Overdoing it in Python 45min");
            stringTalkList.Add("Lua for the Masses 30min");
            stringTalkList.Add("Ruby Errors from Mismatched Gem Versions 45min");
            stringTalkList.Add("Common Ruby Errors 45min");
            stringTalkList.Add("Communicating Over Distance 60min");
            stringTalkList.Add("Rails for Python Developers lightning");
            stringTalkList.Add("Accounting - Driven Development 45min");
            stringTalkList.Add("Woah 30min");
            stringTalkList.Add("Sit Down and Write 30min");
            stringTalkList.Add("Pair Programming vs Noise 45min");
            stringTalkList.Add("Rails Magic 60min");
            stringTalkList.Add("Ruby on Rails: Why We Should Move On 60min");
            stringTalkList.Add("Clojure Ate Scala(on my project) 45min");
            stringTalkList.Add("Programming in the Boondocks of Seattle 30min");
            stringTalkList.Add("Ruby vs.Clojure for Back - End Development 30min");
            stringTalkList.Add("Ruby on Rails Legacy App Maintenance 60min");
            stringTalkList.Add("A World Without HackerNews 30min");
            stringTalkList.Add("User Interface CSS in Rails Apps 30min");
            var talkService = new TalkService();
            foreach (var item in stringTalkList)
            {
                DefaultTalks.Add(talkService.IncludeTalk(item));
            }

        }

        #region ScheduleTalks Tests
        [TestMethod]
        public void IncludeConferenceWithTwoTracksSuccessfully()
        {
            var service = new ConferenceService();
            var conference = service.ScheduleTalks(DefaultTalks);
            Assert.IsTrue(conference != null &&
                          conference.Tracks.Count == 2 &&
                          string.IsNullOrWhiteSpace(service.Error));
        }

        [TestMethod]
        public void IncludeConferenceWithOneTrackSuccessfully()
        {
            var stringTalkList = new List<string>();
            stringTalkList.Add("Rails Magic 60min");
            stringTalkList.Add("Ruby on Rails: Why We Should Move On 60min");
            stringTalkList.Add("Clojure Ate Scala(on my project) 45min");
            stringTalkList.Add("Programming in the Boondocks of Seattle 30min");
            stringTalkList.Add("Ruby vs.Clojure for Back - End Development 30min");
            stringTalkList.Add("Ruby on Rails Legacy App Maintenance 60min");
            stringTalkList.Add("A World Without HackerNews 30min");
            stringTalkList.Add("User Interface CSS in Rails Apps 30min");
            var talkService = new TalkService();
            var talks = new List<Talk>();
            foreach (var item in stringTalkList)
            {
                talks.Add(talkService.IncludeTalk(item));
            }
            var service = new ConferenceService();
            var conference = service.ScheduleTalks(talks);
            Assert.IsTrue(conference != null &&
                          conference.Tracks.Count == 1 &&
                          string.IsNullOrWhiteSpace(service.Error));
        }

        [TestMethod]
        public void IncludeConferenceWithOnlyMorningSessionTrackSuccessfully()
        {
            var stringTalkList = new List<string>();
            stringTalkList.Add("Clojure Ate Scala(on my project) 45min");
            stringTalkList.Add("Programming in the Boondocks of Seattle 30min");
            stringTalkList.Add("Ruby vs.Clojure for Back - End Development 30min");
            stringTalkList.Add("User Interface CSS in Rails Apps 30min");
            var talkService = new TalkService();
            var talks = new List<Talk>();
            foreach (var item in stringTalkList)
            {
                talks.Add(talkService.IncludeTalk(item));
            }
            var service = new ConferenceService();
            var conference = service.ScheduleTalks(talks);
            Assert.IsTrue(conference != null &&
                          conference.Tracks.Count == 1 &&
                          conference.Tracks[0].HasNetworking == false &&
                          conference.Tracks[0].MorningTalks.Count > 0 &&
                          conference.Tracks[0].AfternoonTalks.Count == 0 &&
                          string.IsNullOrWhiteSpace(service.Error));
        }

        [TestMethod]
        public void DontIncludeConferenceWithoutTalks()
        {
            var service = new ConferenceService();
            var talksCountZero = new List<Talk>();
            var conference = service.ScheduleTalks(talksCountZero);
            Assert.IsTrue(conference == null && !string.IsNullOrWhiteSpace(service.Error));
        }
        #endregion

        #region VisualizeSchedule Tests
        [TestMethod]
        public void VisualizeScheduleSuccessfully()
        {
            var service = new ConferenceService();
            var conference = service.ScheduleTalks(DefaultTalks);
            var schedule = service.VisualizeSchedule(conference);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(schedule.ToString()) && string.IsNullOrWhiteSpace(service.Error));
        }

        [TestMethod]
        public void VisualizeScheduleOfConferenceWithOnlyMorningSessionTrackSuccessfully()
        {
            var stringTalkList = new List<string>();
            stringTalkList.Add("Clojure Ate Scala(on my project) 45min");
            stringTalkList.Add("Programming in the Boondocks of Seattle 30min");
            stringTalkList.Add("Ruby vs.Clojure for Back - End Development 30min");
            stringTalkList.Add("User Interface CSS in Rails Apps 30min");
            var talkService = new TalkService();
            var talks = new List<Talk>();

            foreach (var item in stringTalkList)
            {
                talks.Add(talkService.IncludeTalk(item));
            }
            var service = new ConferenceService();
            var conference = service.ScheduleTalks(talks);
            var schedule = service.VisualizeSchedule(conference);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(schedule.ToString()) &&
                          string.IsNullOrWhiteSpace(service.Error));
        }

        [TestMethod]
        public void DontVisualizeScheduleWithNullConference()
        {
            var service = new ConferenceService();
            var schedule = service.VisualizeSchedule(null);
            Assert.IsTrue(schedule == null && !string.IsNullOrWhiteSpace(service.Error));
        }
        #endregion

    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrackManagement.Services;

namespace TrackManagementTest
{
    [TestClass]
    public class TalkServiceTeste
    {
        [TestMethod]
        public void IncludeTalkSuccessfully()
        {
            var talkService = new TalkService();
            var talk = talkService.IncludeTalk("Writing Fast Tests Against Enterprise Rails 60min");
            Assert.IsTrue(talk != null && string.IsNullOrWhiteSpace(talkService.Error));
        }

        [TestMethod]
        public void IncludeLightningSuccessfully()
        {
            var talkService = new TalkService();
            var lightning = talkService.IncludeTalk("Rails for Python Developers lightning");
            Assert.IsTrue(lightning != null && lightning.Duration == 5 && lightning.DurationFormat == "Lightning");
        }

        [TestMethod]
        public void DontIncludeTalkWithInvalidTitle()
        {
            var talkService = new TalkService();
            var talk = talkService.IncludeTalk("Java 1 60min");
            Assert.IsTrue(talk == null && !string.IsNullOrWhiteSpace(talkService.Error));
        }

        [TestMethod]
        public void DontIncludeTalkWithoutTitle()
        {
            var talkService = new TalkService();
            var talk = talkService.IncludeTalk(" ");
            Assert.IsTrue(talk == null && !string.IsNullOrWhiteSpace(talkService.Error));
        }

        [TestMethod]
        public void DontIncludeTalkWithTitleNull()
        {
            var talkService = new TalkService();
            var talk = talkService.IncludeTalk(null);
            Assert.IsTrue(talk == null && !string.IsNullOrWhiteSpace(talkService.Error));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackManagement.Domain;
using TrackManagement.Services.Interfaces;

namespace TrackManagement.Services
{
    public class TalkService : ITalkService
    {
        private const int Lightning = 5;
        private string _Error;
        public string Error
        {
            get { return _Error; }
        }
        private bool IsTalkTitleValid(string title)
        {
            return !System.Text.RegularExpressions.Regex.IsMatch(title, @"[0-9]+$");
        }

        private int GetDuration(string duration)
        {
            try
            {
                return Int32.Parse(duration.Substring(0, duration.IndexOf('m')));
            }
            catch { }
            return Lightning;
        }
        public Talk IncludeTalk(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                _Error = "Enter a title";
                return null;
            }
            var titleArray = line.Split(new char[] { ' ' });
            var durationTime = titleArray.Last();
            var title = string.Join(" ", titleArray.Take(titleArray.Count() - 1));
            if (!IsTalkTitleValid(title))
            {
                _Error = "This is a invalid title.";
                return null;
            }

            var talk = new Talk(title, GetDuration(durationTime.ToLower()));
            return talk;
        }

        Talk ITalkService.IncludeTalk(string line)
        {
            throw new NotImplementedException();
        }
    }
}

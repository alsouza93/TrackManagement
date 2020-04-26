using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackManagement.Domain
{
    public class Talk
    {
        public Talk(string title, int duration)
        {
            Title = title;
            Duration = duration;
        }

        private int GetDuration(string duration)
        {
            try
            {
                return Int32.Parse(duration.Substring(0, duration.IndexOf('m')));
            }
            catch { }
            return 5;
        }
        public string Title { get; set; }
        public int Duration { get; set; }
        public DateTime? Start { get; set; }

        public string DurationFormat
        {
            get
            {
                return Duration == 5 ? "Lightning" : Duration + "min";
            }
        }



    }
}

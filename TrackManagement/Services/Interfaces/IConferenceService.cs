using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackManagement.Domain;

namespace TrackManagement.Services.Interfaces
{
    public interface IConferenceService : IService
    {
        Conference ScheduleTalks(List<Talk> talks);
        StringBuilder VisualizeSchedule(Conference conference);
    }
}

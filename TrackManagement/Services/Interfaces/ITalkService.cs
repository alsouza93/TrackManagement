using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackManagement.Domain;

namespace TrackManagement.Services.Interfaces
{
    public interface ITalkService : IService
    {
        Talk IncludeTalk(string line);
    }
}

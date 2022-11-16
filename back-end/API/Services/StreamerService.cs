using Data.DatabaseLayer;
using Data.ModelLayer;

namespace API.Services
{
    public class StreamerService
    {
        private IStreamerAccess _steramerAccess;

        public StreamerService(IStreamerAccess streamerAccess)
        {
            _steramerAccess = streamerAccess;
        }

        public Streamer Get(int id)
        {
            return _steramerAccess.GetStreamerById(id);
        }
    }
}

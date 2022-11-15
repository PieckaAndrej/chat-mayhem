using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public class StreamerAccess : IStreamerAccess
    {
        private readonly string _connectionString;

        public StreamerAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Streamer GetStreamerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

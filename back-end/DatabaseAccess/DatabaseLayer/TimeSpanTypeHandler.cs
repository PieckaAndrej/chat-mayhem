using Dapper;
using System.Data;

namespace Data.DatabaseLayer
{
    public class TimeSpanTypeHandler : SqlMapper.TypeHandler<TimeSpan>
    {
        public override TimeSpan Parse(object value)
        {
            return TimeSpan.FromSeconds((long)value);
        }

        public override void SetValue(IDbDataParameter parameter, TimeSpan value)
        {
            throw new NotImplementedException();
        }
    }
}

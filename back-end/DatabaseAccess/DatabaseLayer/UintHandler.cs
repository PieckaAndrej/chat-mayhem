using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public class UintHandler : SqlMapper.TypeHandler<uint?>
    {
        /// <inheritdoc />
        public override uint? Parse(object value)
        {
            if (value == null)
            {
                return null;
            }

            return Convert.ToUInt32(value, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc />
        public override void SetValue(IDbDataParameter parameter, uint? value)
        {
            if (parameter == null)
            {
                return;
            }

            parameter.DbType = DbType.UInt32;
            parameter.Value = value;
        }
    }
}

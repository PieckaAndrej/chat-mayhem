using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public class GenericArrayHandler<T> : SqlMapper.TypeHandler<T[]>
    {
        public override void SetValue(IDbDataParameter parameter, T[] value)
        {
            parameter.Value = value;
        }

        public override T[] Parse(object value) => (T[])value;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAnalytics.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<Model> ExecuteQuery<Model>(object parameters, string query);
        IEnumerable<Model> Search<Model>(object parameters, string query);

    }
}

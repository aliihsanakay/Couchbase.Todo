using Couchbase.N1QL;
using System.Collections.Generic;

namespace Couchbase.Todo.CouchBase.RepositoryBase
{
    public interface IRepository<Type> where Type : class
    {
         void Insert(string key, Type model);
        IEnumerable<Type> ExecQueryReturnList(IQueryRequest queryRequest);
    }
}
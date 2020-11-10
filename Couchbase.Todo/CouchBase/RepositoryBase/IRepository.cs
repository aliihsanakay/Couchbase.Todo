using Couchbase.N1QL;
using System.Collections.Generic;

namespace Couchbase.Todo.CouchBase.RepositoryBase
{
    public interface IRepository<Type> where Type : class
    {
        void Insert(Type model, string key = null);
        IEnumerable<Type> ExecQueryReturnList(IQueryRequest queryRequest);
    }
}
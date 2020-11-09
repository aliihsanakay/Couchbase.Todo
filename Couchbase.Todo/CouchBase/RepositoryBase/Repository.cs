using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.N1QL;
using Couchbase.Todo.Models.DbModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Couchbase.Todo.CouchBase.RepositoryBase
{
    public class Repository<Type> : IRepository<Type> where Type : class
    {
        private readonly IBucket _bucket;
        public Repository( IBucketProvider bucketProvider)
        {
            string name = typeof(Type)
                     .GetAttributeValue((BucketNameAttribute dna) => dna.Name);
            if (string.IsNullOrEmpty(name))
                throw new Exception("BucketName Eklememiş");
            _bucket = bucketProvider.GetBucket(name);
        }
        [NonAction]
        public void Insert(string key, Type model)
        {
            _bucket.Insert(key, model);
          
        }
        [NonAction]
        public IEnumerable<Type> ExecQueryReturnList(IQueryRequest queryRequest)
        {
            var result = _bucket.Query<Type>(queryRequest);
            if(result.Errors.Count>0)
                throw new Exception(result.Errors[0].Message);
            return result.Rows;

        }

    }
}

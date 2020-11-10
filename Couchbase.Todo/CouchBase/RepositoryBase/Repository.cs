using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.Management;
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
        private readonly IClusterManager _clusterManager;
        public Repository( IBucketProvider bucketProvider, ICluster clustur)
        {
            string name = typeof(Type)
                     .GetAttributeValue((BucketNameAttribute dna) => dna.Name);
            if (string.IsNullOrEmpty(name))
                throw new BucketNotFoundException("BucketName Eklememiş");
          clustur.Authenticate("Administrator", "Qn4j123");
            _clusterManager = clustur.CreateManager(); //burda kaldım clustur oluşturmuyor
            var bucketList = _clusterManager.ListBuckets();

            if (bucketList.Success && !bucketList.Value.Any(x => x.Name == name))
            {
                var bucketResult = CreateBucket(name);
                if (!bucketResult.Item1)
                    throw new BucketNotFoundException($"Bucket Oluşturulken hata alındı {bucketResult.Item2}");
                else
                    ExecQueryReturnList($"CREATE PRIMARY INDEX `{name}_primary_index` ON `{name}`");
            }
               
            _bucket = bucketProvider.GetBucket(name);
        }
        private (bool,string) CreateBucket(string name)
        {
            var result = _clusterManager.CreateBucket(name);
            return  (result.Success,result.Message);   
            
        }
        [NonAction]
        public void Insert(Type model, string key = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                key = Guid.NewGuid().ToString();
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
        [NonAction]
        public IEnumerable<Type> ExecQueryReturnList(string queryRequest)
        {
          
            var query = QueryRequest.Create(queryRequest);
            var result = _bucket.Query<Type>(query);
            if (result.Errors.Count > 0)
                throw new Exception(result.Errors[0].Message);
            return result.Rows;

        }

    }
}

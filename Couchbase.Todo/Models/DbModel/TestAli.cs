using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Couchbase.Todo.Models.DbModel
{
    [BucketName("testAli")]
    public class TestAli
    {
        public int Type { get; set; }
    }
}

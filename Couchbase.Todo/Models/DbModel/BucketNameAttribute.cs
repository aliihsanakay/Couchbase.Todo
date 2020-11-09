using System;

namespace Couchbase.Todo.Models.DbModel
{
    internal class BucketNameAttribute : Attribute
    {
        public string Name;

        public BucketNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Couchbase.Todo.Models;
using Couchbase.Todo.Models.DbModel;
using Couchbase.Todo.CouchBase.RepositoryBase;
using Couchbase.N1QL;

namespace Couchbase.Todo.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<TestAli> _testRepository;


        public HomeController( IRepository<User> userRepository, IRepository<TestAli> testRepository)
        {
            _userRepository = userRepository;
            _testRepository = testRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            /* var key = Guid.NewGuid().ToString();
             var user = new User();
             user.Email = "ali1111111.akay@gmail.com";
             user.FirstName = "Ali";
             user.LastName = "Akat";
             user.TagLine = "120";

           _userRepository.Insert(user,key);*///Insert Komutu


            var n1ql = "SELECT d.* FROM default d";
            var query = QueryRequest.Create(n1ql);
            // query.AddNamedParameter("$email", email);
            var result = _userRepository.ExecQueryReturnList(query);
            var list= result;

            TestAli ali = new TestAli();
            ali.Type = 11;
            _testRepository.Insert(ali);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace MVCAPIDAPPERTEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingController : ControllerBase
    {
        private string _connection = @"Server=localhost; Database=cruddapper; Uid=root; pwd=root";

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Models.Thing> lst = null;
            using (var db = new MySqlConnection(_connection)) {
                var sql = "select id,name,description from thing";
                lst = db.Query<Models.Thing>(sql);
            }
        
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult Insert(Models.Thing data)
        {
            int Result = 0;
            using (var db = new MySqlConnection(_connection))
            {
                var sql = "insert into thing(name,description) "+
                          "values(@name, @description)";

                Result = db.Execute(sql, data);
                
            }
            return Ok(Result);
        }

        [HttpPut]
        public IActionResult Update(Models.Thing data)
        {
            int Result = 0;
            using (var db = new MySqlConnection(_connection))
            {
                var sql = "update thing set name=@name, description=@description "+
                          "where id=@id";

                Result = db.Execute(sql, data);

            }
            return Ok(Result);
        }

        [HttpDelete]
        public IActionResult Delete(Models.Thing data)
        {
            int Result = 0;
            using (var db = new MySqlConnection(_connection))
            {
                var sql = "delete from thing where id=@id";

                Result = db.Execute(sql, data);

            }
            return Ok(Result);
        }
    }
}
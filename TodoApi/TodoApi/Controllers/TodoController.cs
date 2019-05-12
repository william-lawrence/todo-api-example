using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Classes;
using TodoApi.Interfaces;

namespace TodoApi.Controllers
{
    [Route("api/v0/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoSqlDal todoDal;

        public TodoController(ITodoSqlDal todoDal)
        {
            this.todoDal = todoDal;
        }

        [HttpGet]
        public JsonResult Get()
        {
            IList<Todo> todoItems = new List<Todo>();

            todoItems = todoDal.GetAllTodoItems();

            JsonResult todoItemsAsJson = new JsonResult(todoItems);

            return todoItemsAsJson;
        }
    }

}
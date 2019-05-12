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
        /// <summary>
        /// DAL that is used to access todos
        /// </summary>
        private readonly ITodoSqlDal todoDal;

        /// <summary>
        /// Constructor that injects any DALs it needs.
        /// </summary>
        /// <param name="todoDal"></param>
        /// <remarks>
        /// 
        /// If you need to inject more services, do them here.
        /// 
        /// </remarks>
        public TodoController(ITodoSqlDal todoDal)
        {
            this.todoDal = todoDal;
        }

        /// <summary>
        /// Gets all the Todos in the database.
        /// </summary>
        /// <returns>All the Todos in the database as JSON.</returns>
        [HttpGet]
        public IActionResult GetAllTodoItems()
        {
            IList<Todo> todoItems = todoDal.GetAllTodoItems();

            if (todoItems == null)
            {
                return NotFound();
            }

            return new JsonResult(todoItems);
        }

        /// <summary>
        /// Gets the todo with a specific ID.
        /// </summary>
        /// <param name="id">The ID of the todo to get.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetTodoItemByID(int id)
        {
            Todo todo = todoDal.GetTodoItemById(id);

            if (todo.Id == 0 || todo.TodoText == null)
            {
                return NotFound();
            }

            return new JsonResult(todo);
        }
    }

}
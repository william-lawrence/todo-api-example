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

            if (NoTodoFound(todo))
            {
                return NotFound();
            }

            return new JsonResult(todo);
        }

        /// <summary>
        /// Create a new todo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>The newly created todo object with its newly generated id</returns>
        [HttpPost]
        public IActionResult CreateTodoItem(Todo todo)
        {
            todoDal.CreateTodoItem(todo);

            return new JsonResult(todo);
        }

        /// <summary>
        /// If no todo is found, 404
        /// </summary>
        /// <param name="todo">the todo to validate.</param>
        /// <returns>True if the todo is valid, false if it is not.</returns>
        /// <remarks>
        /// 
        /// If no todo is found, the text is NULL and the Id is zero,
        /// but either comdition is sufficient.
        /// 
        /// </remarks>
        private static bool NoTodoFound(Todo todo)
        {
            return todo.Id == 0 || todo.TodoText == null;
        }
    }

}
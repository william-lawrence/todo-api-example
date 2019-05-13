using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TodoApi.Classes;
using TodoApi.DAL;
using System.Data.SqlClient;
using TodoApi.Interfaces;

namespace TodoApi.Tests
{
    [TestClass]
    public class TodoSqlDalTests : DatabaseTests
    {
        private readonly string connectionString = $@"Data Source=DESKTOP-99R8GDK\SQLEXPRESS;Initial Catalog=Todo;Integrated Security=True";

        [TestCategory("TodoSqlDal")]
        [TestMethod]
        public void TestGetAllTodosId()
        {
            TodoSqlDal todoSqlDal = new TodoSqlDal(connectionString);
            IList<Todo> todoItems = todoSqlDal.GetAllTodoItems();
            Assert.AreEqual(1, todoItems[0].Id);
        }

        [TestCategory("TodoSqlDal")]
        [TestMethod]
        public void TestGetAllTodosText()
        {
            TodoSqlDal todoSqlDal = new TodoSqlDal(connectionString);
            IList<Todo> todoItems = todoSqlDal.GetAllTodoItems();
            Assert.AreEqual("test1", todoItems[0].TodoText);
        }

        [TestCategory("TodoSqlDal")]
        [TestMethod]
        public void TestGetAllTodosIsCompleted()
        {
            TodoSqlDal todoSqlDal = new TodoSqlDal(connectionString);
            IList<Todo> todoItems = todoSqlDal.GetAllTodoItems();
            Assert.AreEqual(false, todoItems[0].IsCompleted);
        }

        [TestCategory("TodoSqlDal")]
        [TestMethod]
        public void TestGetAllTodosIsDeleted()
        {
            TodoSqlDal todoSqlDal = new TodoSqlDal(connectionString);
            IList<Todo> todoItems = todoSqlDal.GetAllTodoItems();
            Assert.AreEqual(false, todoItems[0].IsDeleted);
        }

        [TestCategory("TodoSqlDal")]
        [TestMethod]
        public void TestGetTodoItemById()
        {
            TodoSqlDal todoSqlDal = new TodoSqlDal(connectionString);
            Todo todo = todoSqlDal.GetTodoItemById(1);
            Assert.AreEqual(1, todo.Id);
        }

        [TestCategory("TodoSqlDal")]
        [TestMethod]
        public void TestCreateTodoItem()
        {
            TodoSqlDal todoSqlDal = new TodoSqlDal(connectionString);
            Todo todo = new Todo
            {
                TodoText = "test5",
                IsCompleted = true,
                IsDeleted = true
            };

            _ = todoSqlDal.CreateTodoItem(todo);

            Assert.AreEqual(5, GetRowCount("TodoItems"));
        }

        [TestCategory("TodoSqlDal")]
        [TestMethod]
        public void TestUpdateTodoItem()
        {
            TodoSqlDal todoSqlDal = new TodoSqlDal(connectionString);
            Todo todo = todoSqlDal.GetTodoItemById(1);
            
            // making sure we have the correct original value in the database.
            Assert.AreEqual("test1", todo.TodoText);

            todo.TodoText = "updated test1";

            // getting the new value, but we don't need it so we can discard.
            _ = todoSqlDal.UpdateTodoItem(todo);

            todo = todoSqlDal.GetTodoItemById(1);

            // making sure that we have to updated value in the database.
            Assert.AreEqual("updated test1", todo.TodoText);
        }

        [TestCategory("TodoSqlDal")]
        [TestMethod]
        public void TestDeleteTodoItemByRowsDeleted()
        {
            TodoSqlDal todoSqlDal = new TodoSqlDal(connectionString);
            int rowsDeleted = todoSqlDal.DeleteTodoItem(1);

            Assert.AreEqual(1, rowsDeleted);
        }

        [TestCategory("TodoSqlDal")]
        [TestMethod]
        public void TestDeleteTodoItemByRowsRemaining()
        {
            TodoSqlDal todoSqlDal = new TodoSqlDal(connectionString);

            _ = todoSqlDal.DeleteTodoItem(1);
            int rowsRemaining = GetRowCount("TodoItems");

            Assert.AreEqual(3, rowsRemaining);
        }
    }
}

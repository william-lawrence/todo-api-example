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

            IList<Todo> todoItems = new List<Todo>();

            todoItems = todoSqlDal.GetAllTodoItems();

            Assert.AreEqual(1, todoItems[0].Id);
        }

        [TestCategory("TodoSqlDal")]
        [TestMethod]
        public void TestGetAllTodosText()
        {
            TodoSqlDal todoSqlDal = new TodoSqlDal(connectionString);

            IList<Todo> todoItems = new List<Todo>();

            todoItems = todoSqlDal.GetAllTodoItems();

            Assert.AreEqual("test1", todoItems[0].TodoText);
        }

        [TestCategory("TodoSqlDal")]
        [TestMethod]
        public void TestGetAllTodosIsCompleted()
        {
            TodoSqlDal todoSqlDal = new TodoSqlDal(connectionString);

            IList<Todo> todoItems = new List<Todo>();

            todoItems = todoSqlDal.GetAllTodoItems();

            Assert.AreEqual(false, todoItems[0].IsCompleted);
        }

        [TestCategory("TodoSqlDal")]
        [TestMethod]
        public void TestGetAllTodosIsDeleted()
        {
            TodoSqlDal todoSqlDal = new TodoSqlDal(connectionString);

            IList<Todo> todoItems = new List<Todo>();

            todoItems = todoSqlDal.GetAllTodoItems();

            Assert.AreEqual(false, todoItems[0].IsDeleted);
        }
    }
}

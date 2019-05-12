using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Classes;
using TodoApi.Interfaces;

namespace TodoApi.DAL
{
    public class TodoSqlDal : ITodoSqlDal
    {
        private readonly string connectionString;

        public TodoSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Todo> GetAllTodoItems()
        {
            IList<Todo> todoItems = new List<Todo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $@"SELECT * FROM TodoItems;";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        todoItems.Add(MapRowToTodoItem(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return todoItems;
        }

        private Todo MapRowToTodoItem(SqlDataReader reader)
        {
            Todo todo = new Todo();

            todo.Id = Convert.ToInt32(reader["Id"]);
            todo.TodoText = Convert.ToString(reader["TodoText"]);
            todo.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
            todo.IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);

            return todo;
        }
    }
}

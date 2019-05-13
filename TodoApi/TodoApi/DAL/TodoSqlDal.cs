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

        public Todo CreateTodoItem(Todo todo)
        {
            Todo newTodo = new Todo
            {
                Id = 0,
                TodoText = todo.TodoText,
                IsCompleted = todo.IsCompleted,
                IsDeleted = todo.IsDeleted
            };


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $@"INSERT INTO TodoItems ([TodoText], [IsCompleted],[IsDeleted])
                                    OUTPUT INSERTED.Id
                                    VALUES(@passedTodoText, @passedIsCompleted, @passedIsDeleted)";

                    SqlCommand cmd = new SqlCommand(sql, connection);

                    cmd.Parameters.AddWithValue("@passedTodoText", todo.TodoText);
                    cmd.Parameters.AddWithValue("@passedIsCompleted", todo.IsCompleted);
                    cmd.Parameters.AddWithValue("@passedIsDeleted", todo.IsDeleted);

                    newTodo.Id = (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if(newTodo.Id ==0)
            {
                return null;
            }
            else
            {
                return newTodo;
            }
        }

        public int DeleteTodoItem(int id)
        {
            int rowsDeleted = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $@"DELETE FROM TodoItems
                                    WHERE Id = @passedId;";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@passedId", id);

                    rowsDeleted = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsDeleted;
        }

        public IList<Todo> GetAllTodoItems()
        {
            IList<Todo> todoItems = new List<Todo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $@"SELECT [Id],
                                    [TodoText],
                                    [IsCompleted],
                                    [IsDeleted]
                                    FROM TodoItems;";

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

        public Todo GetTodoItemById(int id)
        {
            Todo todo = new Todo();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $@"SELECT [Id],
                                    [TodoText],
                                    [IsCompleted],
                                    [IsDeleted]
                                    FROM TodoItems
                                    WHERE Id = @passedId;";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@passedId", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        todo = MapRowToTodoItem(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return todo;
        }

        public Todo UpdateTodoItem(Todo todo)
        {
            Todo updatedTodo = new Todo
            {
                // Generating the updated todo.
                Id = todo.Id,
                TodoText = todo.TodoText,
                IsCompleted = todo.IsCompleted,
                IsDeleted = todo.IsDeleted
            };

            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $@"UPDATE TodoItems
                                    SET TodoText = @passedTodoText, IsCompleted = @passedIsCompleted, IsDeleted = @passedIsDeleted
                                    WHERE Id = @passedId;";

                    SqlCommand cmd = new SqlCommand(sql, connection);

                    cmd.Parameters.AddWithValue("@passedId", todo.Id);
                    cmd.Parameters.AddWithValue("@passedTodoText", todo.TodoText);
                    cmd.Parameters.AddWithValue("@passedIsCompleted", todo.IsCompleted);
                    cmd.Parameters.AddWithValue("@passedIsDeleted", todo.IsDeleted);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (rowsAffected != 1)
            {
                return null;
            }
            else
            {
                return updatedTodo;
            }

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

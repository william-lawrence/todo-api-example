﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Classes;

namespace TodoApi.Interfaces
{
    public interface ITodoSqlDal
    {
        IList<Todo> GetAllTodoItems();
        Todo GetTodoItemById(int id);
        Todo CreateTodoItem(Todo todo);
        Todo UpdateTodoItem(Todo todo);
        int DeleteTodoItem(int id);
    }
}

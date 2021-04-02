using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotesApp.Models;

namespace NotesApp.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        public Todo GetTodo(int id)
        {
            using (var dbContext = new NotesContext())
            {
                var todo = dbContext.Todos.Find(id);
                return todo;
            }
        }

        public IEnumerable<Todo> GetAllTodos()
        {
            using (var dbContext = new NotesContext())
            {
                return dbContext.Todos.ToList();
            }
        }

        public void SaveTodo(Todo todo)
        {
            using (var dbContext = new NotesContext())
            {
                if (todo.Id == 0)
                {
                    dbContext.Todos.Add(todo);
                }
                else
                {
                    dbContext.Todos.Update(todo);
                }

                dbContext.SaveChanges();
            }
        }

        public void DeleteTodo(Todo todo)
        {
            using (var dbContext = new NotesContext())
            {
                dbContext.Remove(todo);
                dbContext.SaveChanges();
            }
        }
    }
}
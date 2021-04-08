using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        public async Task<Todo> GetTodoAsync(int id)
        {
            using (var dbContext = new NotesContext())
            {
                var todo = await dbContext.Todos.FindAsync(id).ConfigureAwait(false);
                return todo;
            }
        }

        public async Task<IEnumerable<Todo>> GetAllTodosAsync()
        {
            using (var dbContext = new NotesContext())
            {
                var todos = await dbContext.Todos.ToListAsync();
                return todos;
            }
        }

        public async Task SaveTodoAsync(Todo todo)
        {
            using (var dbContext = new NotesContext())
            {
                if (todo.Id == 0)
                {
                    await dbContext.Todos.AddAsync(todo).ConfigureAwait(false);
                }
                else
                {
                    dbContext.Todos.Update(todo);
                }

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteTodoAsync(Todo todo)
        {
            using (var dbContext = new NotesContext())
            {
                dbContext.Remove(todo);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApp.Models;

namespace NotesApp.Repositories
{
    public interface ITodoRepository
    {
        Task<Todo> GetTodoAsync(int id);

        Task<IEnumerable<Todo>> GetAllTodosAsync();

        Task SaveTodoAsync(Todo todo);

        Task DeleteTodoAsync(Todo todo);
    }
}
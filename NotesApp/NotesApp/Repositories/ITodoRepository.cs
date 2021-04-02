using System.Collections.Generic;
using NotesApp.Models;

namespace NotesApp.Repositories
{
    public interface ITodoRepository
    {
        Todo GetTodo(int id);

        IEnumerable<Todo> GetAllTodos();

        void SaveTodo(Todo todo);

        void DeleteTodo(Todo todo);
    }
}
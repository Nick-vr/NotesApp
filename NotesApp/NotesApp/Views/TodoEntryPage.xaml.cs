using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.Models;
using NotesApp.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesApp.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class TodoEntryPage : ContentPage
    {
        private readonly ITodoRepository _todoRepository;
        public int ItemId { set => _ = LoadTodo(value); }

        public TodoEntryPage()
        {
            InitializeComponent();
            _todoRepository = new TodoRepository();
            BindingContext = new Todo();
        }

        private async Task LoadTodo(int id)
        {
            BindingContext = await _todoRepository.GetTodoAsync(id).ConfigureAwait(false);
        }

        public async Task SaveTodo(Todo todo)
        {
            await _todoRepository.SaveTodoAsync(todo).ConfigureAwait(false);
        }

        public async Task DeleteTodo(Todo todo)
        {
            await _todoRepository.DeleteTodoAsync(todo).ConfigureAwait(false);
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (BindingContext is Todo todo)
            {
                todo.Date = DateTime.Now;
                await SaveTodo(todo).ConfigureAwait(false);
            }

            // Navigate backwards -> Go back to previous screen
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var todo = BindingContext as Todo;

            await DeleteTodo(todo).ConfigureAwait(false);

            // Navigate backwards -> Go back to previous screen
            await Shell.Current.GoToAsync("..");
        }
    }
}
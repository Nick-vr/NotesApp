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
        public int ItemId { set => LoadTodo(value); }

        public TodoEntryPage()
        {
            InitializeComponent();
            _todoRepository = new TodoRepository();
            BindingContext = new Todo();
        }

        private void LoadTodo(int id)
        {
            BindingContext = _todoRepository.GetTodo(id);
        }

        public void SaveTodo(Todo todo)
        {
            _todoRepository.SaveTodo(todo);
        }

        public void DeleteTodo(Todo todo)
        {
            _todoRepository.DeleteTodo(todo);
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (BindingContext is Todo todo)
            {
                todo.Date = DateTime.Now;
                SaveTodo(todo);
            }

            // Navigate backwards -> Go back to previous screen
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var todo = BindingContext as Todo;

            DeleteTodo(todo);

            // Navigate backwards -> Go back to previous screen
            await Shell.Current.GoToAsync("..");
        }
    }
}
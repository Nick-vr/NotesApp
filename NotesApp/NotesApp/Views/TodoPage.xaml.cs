using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoPage : ContentPage
    {
        private readonly ITodoRepository _todoRepository;

        public TodoPage()
        {
            InitializeComponent();
            _todoRepository = new TodoRepository();
            BindingContext = new Todo();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var todos = _todoRepository.GetAllTodosAsync();

            MyTodos.ItemsSource = await todos;
        }

        private async Task<IEnumerable<Todo>> GetAllTodos()
        {
            var todos = _todoRepository.GetAllTodosAsync();

            return await todos;
        }

        private async void MyTodos_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Todo todo)
                await Shell.Current.GoToAsync($"{nameof(TodoEntryPage)}?{nameof(TodoEntryPage.ItemId)}={todo.Id}");
        }

        private async void AddTodoClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(TodoEntryPage));
        }

        private void OnCheckboxChecked(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkbox && checkbox.BindingContext is Todo checklist)
            {
                _todoRepository.SaveTodoAsync(checklist);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoPage : ContentPage
    {
        public TodoPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var todos = new List<Todo>();

            var files = Directory.EnumerateFiles(App.FolderPath, "*.todos.txt");

            foreach (var file in files)
            {
                todos.Add(new Todo
                {
                    FileName = file,
                    Text = File.ReadAllText(file),
                    Date = File.GetCreationTime(file),
                    Deadline = File.GetCreationTime(file).AddDays(7),
                    Completed = false,
                    Priority = 1,
                });
            }

            MyTodos.ItemsSource = todos;
        }

        private async void MyTodos_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var todo = e.CurrentSelection.FirstOrDefault() as Todo;
            await Shell.Current.GoToAsync($"{nameof(TodoEntryPage)}?{nameof(TodoEntryPage.ItemId)}={todo.FileName}");
        }

        private void AddTodoClicked(object sender, EventArgs e)
        {
        }
    }
}
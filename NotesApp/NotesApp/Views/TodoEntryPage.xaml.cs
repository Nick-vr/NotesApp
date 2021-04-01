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
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class TodoEntryPage : ContentPage
    {
        public string ItemId { set => LoadTodo(value); }

        public TodoEntryPage()
        {
            InitializeComponent();
            BindingContext = new Todo();
        }

        private void LoadTodo(string fileName)
        {
            var todo = new Todo
            {
                FileName = fileName,
                Text = File.ReadAllText(fileName),
                Date = File.GetCreationTime(fileName),
                Deadline = File.GetCreationTime(fileName).AddDays(7),
                Completed = false,
                Priority = 1,
            };

            BindingContext = todo;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (BindingContext is Todo todo)
            {
                // Check if file already exists
                if (string.IsNullOrWhiteSpace(todo.FileName))
                {
                    // Access a file in an app's sandbox
                    var _fileName = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                    File.WriteAllText(_fileName, Editor.Text);
                }
                else
                {
                    // Update existing file
                    File.WriteAllText(todo.FileName, Editor.Text);
                }
            }

            // Navigate backwards -> Go back to previous screen
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (BindingContext is Todo todo)
            {
                if (File.Exists(todo.FileName))
                {
                    File.Delete(todo.FileName);
                }
            }

            // Navigate backwards -> Go back to previous screen
            await Shell.Current.GoToAsync("..");
        }
    }
}
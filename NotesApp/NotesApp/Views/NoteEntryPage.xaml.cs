using NotesApp.Models;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesApp.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NoteEntryPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }


        public NoteEntryPage()
        {
            InitializeComponent();
            BindingContext = new Note();
        }

        private void LoadNote(string fileName)
        {
            var note = new Note
            {
                FileName = fileName,
                Text = File.ReadAllText(fileName),
                Date = File.GetCreationTime(fileName)
            };

            BindingContext = note;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = BindingContext as Note;

            if (note != null)
            {
                // Check if file already exists
                if (string.IsNullOrWhiteSpace(note.FileName))
                {
                    // Access a file in an app's sandbox
                    string _fileName = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                    File.WriteAllText(_fileName, editor.Text);
                }
                else
                {
                    // Update existing file
                    File.WriteAllText(note.FileName, editor.Text);
                }
            }

            // Navigate backwards -> Go back to previous screen
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = BindingContext as Note;

            if (note != null)
            {
                if (File.Exists(note.FileName))
                {
                    File.Delete(note.FileName);
                }
            }

            // Navigate backwards -> Go back to previous screen
            await Shell.Current.GoToAsync("..");
        }
    }
}
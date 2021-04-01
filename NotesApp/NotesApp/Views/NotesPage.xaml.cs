using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
       
        public NotesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var notes = new List<Note>();

            // Create a Note object from each file
            var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");

            foreach (var file in files)
            {
                notes.Add(new Note
                {
                    FileName = file,
                    Text = File.ReadAllText(file),
                    Date = File.GetCreationTime(file),
                });
            }

            // Bind a list of items to a collectionView
            myNotes.ItemsSource = notes;
        }

        private async void myNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the first selected element from a collectionview
            Note note = (Note)e.CurrentSelection.FirstOrDefault();
            await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.FileName}");
        }

        private async void AddNoteClicked(object sender, EventArgs e)
        {
            // nameof -> Returns name of object in string format
            // Much, much safer than using "NoteEntryPage" -> A danger to refactoring
            await Shell.Current.GoToAsync(nameof(NoteEntryPage));
        }
    }
}
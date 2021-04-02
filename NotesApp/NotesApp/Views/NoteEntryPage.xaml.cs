using NotesApp.Models;
using System;
using System.IO;
using NotesApp.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesApp.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NoteEntryPage : ContentPage
    {
        private readonly INoteRepository _noteRepository;
        public int ItemId { set => LoadNote(value); }

        public NoteEntryPage()
        {
            InitializeComponent();
            _noteRepository = new NoteRepository();
            BindingContext = new Note();
        }

        private void LoadNote(int id)
        {
            BindingContext = _noteRepository.GetNote(id);
        }

        public void SaveNote(Note note)
        {
            _noteRepository.SaveNote(note);
        }

        private void DeleteNote(Note note)
        {
            _noteRepository.DeleteNote(note);
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (BindingContext is Note note)
            {
                note.Date = DateTime.Now;
                SaveNote(note);
            }

            // Navigate backwards -> Go back to previous screen
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = BindingContext as Note;

            DeleteNote(note);

            // Navigate backwards -> Go back to previous screen
            await Shell.Current.GoToAsync("..");
        }
    }
}
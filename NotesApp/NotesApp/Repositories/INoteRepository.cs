using System.Collections.Generic;
using NotesApp.Models;

namespace NotesApp.Repositories
{
    public interface INoteRepository
    {
        Note GetNote(int id);

        IEnumerable<Note> GetAllNotes();

        void SaveNote(Note note);

        void DeleteNote(Note note);
    }
}
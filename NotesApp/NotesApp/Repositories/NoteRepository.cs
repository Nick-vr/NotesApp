using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotesApp.Models;

namespace NotesApp.Repositories
{
    public class NoteRepository : INoteRepository
    {
        public Note GetNote(int id)
        {
            using (var dbContext = new NotesContext())
            {
                var note = dbContext.Notes.Find(id);
                return note;
            }
        }

        public IEnumerable<Note> GetAllNotes()
        {
            using (var dbContext = new NotesContext())
            {
                return dbContext.Notes.ToList();
            }
        }

        public void SaveNote(Note note)
        {
            using (var dbContext = new NotesContext())
            {
                // Check if item is new or an existing note has to be updated
                if (note.Id == 0)
                {
                    // Item is new. Add new item
                    dbContext.Notes.Add(note);
                }
                else
                {
                    // Update existing note
                    dbContext.Notes.Update(note);
                }

                dbContext.SaveChanges();
            }
        }

        public void DeleteNote(Note note)
        {
            using (var dbContext = new NotesContext())
            {
                dbContext.Remove(note);
                dbContext.SaveChanges();
            }
        }
    }
}
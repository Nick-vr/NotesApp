using System;
using System.Collections.Generic;
using System.Text;

namespace NotesApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set; }
        public bool Completed { get; set; }
        public int Priority { get; set; }
    }
}
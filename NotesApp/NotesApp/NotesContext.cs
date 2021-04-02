using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NotesApp.Models;
using Xamarin.Essentials;

namespace NotesApp
{
    public sealed class NotesContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Todo> Todos { get; set; }

        public NotesContext()
        {
            // Database is automatically generated on first run
            // No migrations needed
            // BUT -> DB cannot be updated with migrations. IF db has to be updated, it has to be recreated from scratch.
            // So, while dev -> use SQL server with migrations. When publishing -> convert to SQLITE
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.sqlite");
            optionsBuilder.UseSqlite($"FileName = {dbPath}");
        }
    }
}
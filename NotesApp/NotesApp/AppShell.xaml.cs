using NotesApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NotesApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // App shell contains all navigation routes in app.
            Routing.RegisterRoute(nameof(NoteEntryPage), typeof(NoteEntryPage));
            Routing.RegisterRoute(nameof(TodoEntryPage), typeof(TodoEntryPage));
        }
    }
}
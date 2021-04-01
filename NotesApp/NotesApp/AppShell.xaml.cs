using NotesApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NotesApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // App shell contains all navigation routes in app.
            Routing.RegisterRoute(nameof(NoteEntryPage), typeof(NoteEntryPage));
        }

    }
}

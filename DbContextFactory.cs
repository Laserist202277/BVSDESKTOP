using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using WPFDEMONSTRATIONAPP;

namespace MVVM
{
    public class DbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public DbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public NotesDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<NotesDbContext> options = new DbContextOptionsBuilder<NotesDbContext>();

            _configureDbContext(options);

            return new NotesDbContext();
        }
    }
}

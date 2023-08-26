using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Notebook.Authorization;
using Notebook.Models;

namespace Notebook.Data
{
    public partial class NotebookContext : IdentityDbContext<User>
    {
        public NotebookContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }


    }

}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Notebook.Models;

namespace Notebook.Data
{
    public partial class NotebookContext : DbContext
    {
        public NotebookContext()
        {
        }

        public NotebookContext(DbContextOptions<NotebookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Notebook;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Clients__3213E83F01BB4112");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Address)
                    .HasMaxLength(20)
                    .HasColumnName("address");
                entity.Property(e => e.Description)
                    .HasMaxLength(20)
                    .HasColumnName("description");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .HasColumnName("firstName");
                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .HasColumnName("lastName");
                entity.Property(e => e.Patronymic)
                    .HasMaxLength(20)
                    .HasColumnName("patronymic");
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phoneNumber");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}

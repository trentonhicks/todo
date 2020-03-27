using System;
using Microsoft.EntityFrameworkCore;
using Todo.Domain;

namespace Todo.Infrastructure
{
    public class TodoDatabaseContext : DbContext
    {
        public TodoDatabaseContext()
        {
        }

        public TodoDatabaseContext(DbContextOptions<TodoDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<TodoList> TodoLists { get; set; }
        public virtual DbSet<TodoListItem> TodoListItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__Accounts__C9F2845640260CAD")
                    .IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TodoList>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ListTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Lists)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lists__AccountID__3A81B327");
            });

            modelBuilder.Entity<TodoListItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ListId).HasColumnName("ListID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.ToDoName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.List)
                    .WithMany(p => p.ToDos)
                    .HasForeignKey(d => d.ListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ToDos__ListID__3D5E1FD2");
            });
        }
    }
}

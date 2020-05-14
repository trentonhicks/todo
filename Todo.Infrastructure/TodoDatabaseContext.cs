﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Domain;
using Todo.Domain.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Todo.Infrastructure
{
    public class TodoDatabaseContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public TodoDatabaseContext()
        {
        }
        public TodoDatabaseContext(DbContextOptions<TodoDatabaseContext> options)
           : base(options)
        {
        }

        public TodoDatabaseContext(DbContextOptions<TodoDatabaseContext> options, IMediator mediator)
           : base(options)
        {
            _mediator = mediator;
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<TodoList> TodoLists { get; set; }
        public virtual DbSet<TodoListItem> TodoListItems { get; set; }
        public virtual DbSet<TodoListLayout> TodoListLayouts { get; set; }
        public virtual DbSet<SubItem> SubItems { get; set; }
        public virtual DbSet<SubItemLayout> SubItemLayouts { get; set; }
        public virtual DbSet<AccountLists> AccountLists { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var saveChanges = await base.SaveChangesAsync(cancellationToken);

            var domainEntities = ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents.Count > 0).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);

            return saveChanges;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {

                entity
                    .Property(e => e.Id).HasColumnName("ID");

                entity
                    .Property(e => e.FullName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity
                    .Property(e => e.Contributors).HasColumnName("Contributors")
                    .HasConversion(
                        v => JsonConvert.SerializeObject(v),
                        v => JsonConvert.DeserializeObject<List<string>>(v));
            });

            modelBuilder.Entity<TodoList>(entity =>
            {
                entity
                    .Property(e => e.Id)
                    .HasColumnName("ID");

                entity
                    .Property(e => e.ListTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity
                    .Property(e => e.Contributors).HasColumnName("Contributors")
                    .HasConversion(
                        v => JsonConvert.SerializeObject(v),
                        v => JsonConvert.DeserializeObject<List<string>>(v));
            });

            modelBuilder.Entity<TodoListItem>(entity =>
            {
                entity
                    .Property(e => e.Id)
                    .HasColumnName("ID");

                entity
                    .Property(e => e.ListId)
                    .HasColumnName("ListID");

                entity
                    .Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity
                    .Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity
                    .Property(e => e.DueDate)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TodoListLayout>(entity =>
            {
                entity
                    .Property(e => e.Id)
                    .HasColumnName("ID");

                entity
                    .Property(e => e.ListId)
                    .HasColumnName("ListID");

                entity
                    .Property(e => e.Layout)
                    .HasColumnName("Layout")
                    .HasConversion(
                        v => JsonConvert.SerializeObject(v),
                        v => JsonConvert.DeserializeObject<List<Guid>>(v)
                    );
            });

            modelBuilder.Entity<SubItemLayout>(entity =>
            {
                entity
                    .Property(e => e.Id)
                    .HasColumnName("ID");

                entity
                    .Property(e => e.ItemId);

                entity
                    .Property(e => e.Layout).HasColumnName("Layout")
                    .HasConversion(
                        v => JsonConvert.SerializeObject(v),
                        v => JsonConvert.DeserializeObject<List<Guid>>(v)
                    );
            });

            modelBuilder.Entity<SubItem>(entity =>
            {
                entity
                    .Property(e => e.Id)
                    .HasColumnName("ID");

                entity
                    .Property(e => e.ListItemId)
                    .HasColumnName("ListItemID");

                entity
                    .Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity
                    .Property(e => e.Completed)
                    .HasColumnName("Completed");
            });

            modelBuilder.Entity<AccountLists>(entity =>
            {
                entity
                    .HasKey(o => new { o.AccountId, o.ListId });
                entity
                    .Property(e => e.Role).HasColumnName("Role");
            });
        }
    }
}

namespace SwClub.Repositories.UoW
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SwClub.Common.Constants;
    using SwClub.Common.Helpers;
    using SwClub.DataAccess.Contexts;
    using SwClub.Entities.IModels;
    using SwClub.Entities.Models;
    using SwClub.Repositories.Interfaces;
    using SwClub.Repositories.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SwClubDbContext _context;

        public UnitOfWork(SwClubDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.Users = new UserRepository(this._context);
            this.Clubs = new ClubRepository(this._context);
        }

        public IUserRepository Users { get; private set; }
        public IClubRepository Clubs { get; private set; }

        public async Task<int> SaveChanges()
        {
            this.SaveChangesInternal();

            return await this._context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this._context?.Dispose();
            GC.SuppressFinalize(this);
        }

        private void SaveChangesInternal()
        {
            var entries = this._context.ChangeTracker.Entries()
                .Where(x => (x.State == EntityState.Added)
                    || (x.State == EntityState.Modified));

            this.SaveChangesInternal(entries, EntityState.Added);
            this.SaveChangesInternal(entries, EntityState.Modified);

            var deletedEntries = this._context.ChangeTracker.Entries()
                .Where(x => (x.State == EntityState.Deleted));
            this.SaveChangesSoftDelete(deletedEntries);
        }
         
        private void SaveChangesInternal(IEnumerable<EntityEntry> entries, EntityState state)
        {
            PropertyEntry prop;

            // Enforce type defaults for all entities
            foreach (var item in entries)
            {
                foreach (var p in item.Properties)
                {
                    if (p.CurrentValue == null)
                    {
                        continue;
                    }

                    switch (p.Metadata.ClrType.Name)
                    {
                        case "String": // Replace all empty strings with null
                            var emptyString = string.IsNullOrWhiteSpace(p.CurrentValue.ToString());
                            p.CurrentValue = emptyString ? null : p.CurrentValue;
                            break;
                    }
                }
            }

            foreach (var item in entries.Where(t => t.State == state))
            {
                if (state == EntityState.Added)
                {
                    // CreatedDate
                    prop = item.Properties.FirstOrDefault(p => p.Metadata.Name == ColumnName.CreatedAt);
                    if (prop != null)
                    {
                        prop.CurrentValue = DateTime.Now;
                    }
                }

                // UpdatedAt
                prop = item.Properties.FirstOrDefault(p => p.Metadata.Name == ColumnName.UpdatedAt);
                if (prop != null)
                {
                    prop.CurrentValue = DateTime.Now;
                }

                // Make email address and user name to lowercase and none whitespace
                prop = item.Properties.FirstOrDefault(p => p.Metadata.Name == ColumnName.Email
                                                        || p.Metadata.Name == ColumnName.UserName);
                if (prop != null && prop.CurrentValue != null)
                {
                    prop.CurrentValue = prop.CurrentValue.ToString()?.RemoveWhiteSpaces().ToLower();
                }

                // Trim String Entries Before Saving
                var propertyValues = item.Properties.Where(p => (p.CurrentValue != null) && p.CurrentValue.GetType() == typeof(string) && !string.IsNullOrEmpty(Convert.ToString(p.CurrentValue)));
                foreach (PropertyEntry propertyValue in propertyValues)
                {
                    propertyValue.CurrentValue = propertyValue.CurrentValue.ToString().Trim();
                }
            }
        }

        private void SaveChangesSoftDelete(IEnumerable<EntityEntry> entries)
        {
            foreach (var item in entries)
            {
                if (item.Entity is IIsDeleted entity)
                {
                    // Set the entity to unchanged (if we mark the whole entity as Modified, every field gets sent to Db as an update)
                    item.State = EntityState.Unchanged;

                    // Only update the IsDeleted flag - only this will get sent to the Db
                    entity.IsDeleted = true;

                    // UpdatedAt
                    if (item.Entity is BaseModel baseModel)
                    {
                        baseModel.UpdatedAt = DateTime.Now;
                    }
                }
            }
        }
    }
}

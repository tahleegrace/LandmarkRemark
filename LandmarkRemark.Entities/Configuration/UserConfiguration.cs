using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LandmarkRemark.Entities.Configuration
{
    /// <summary>
    /// Sets up seed data and computed columns for the users table.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        // Normally, this file will set up seed users for administering the application (e.g. a system administrator account).
        // Since there is no registration functionality, this file sets up a few test users.
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureComputedColumns(builder);
            ConfigureSeedData(builder);
        }

        private void ConfigureComputedColumns(EntityTypeBuilder<User> builder)
        {
            builder.Property(nameof(User.FullName))
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]", true);
        }

        private void ConfigureSeedData(EntityTypeBuilder<User> builder)
        {
            // In real life the passwords would be hashed with an appropriate salt and not stored in the codebase.
            DateTime creationDate = new DateTime(2022, 10, 18, 12, 45, 0); // EF Core will regenerate all migrations if you don't use a fixed creation date.

            // Note: The User IDs need to be specified here as Entity Framework doesn't support generating them automatically (see: https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding#limitations-of-model-seed-data).

            // PM.
            builder.HasData
            (
                new User()
                {
                    Id = 1,
                    FirstName = "Anthony",
                    LastName = "Albanese",
                    EmailAddress = "anthony.albanese@example.com",
                    Password = "anthonyalbanese",
                    Created = creationDate,
                    Updated = creationDate
                }
            );

            // Deputy PM.
            builder.HasData
            (
                new User()
                {
                    Id = 2,
                    FirstName = "Richard",
                    LastName = "Marles",
                    EmailAddress = "richard.marles@example.com",
                    Password = "richardmarles",
                    Created = creationDate,
                    Updated = creationDate
                }
            );

            // Treasurer.
            builder.HasData
            (
                new User()
                {
                    Id = 3,
                    FirstName = "Jim",
                    LastName = "Chalmers",
                    EmailAddress = "jim.chalmers@example.com",
                    Password = "jimchalmers",
                    Created = creationDate,
                    Updated = creationDate
                }
            );

            // Foreign Minister.
            builder.HasData
            (
                new User()
                {
                    Id = 4,
                    FirstName = "Penny",
                    LastName = "Wong",
                    EmailAddress = "penny.wong@example.com",
                    Password = "pennywong",
                    Created = creationDate,
                    Updated = creationDate
                }
            );

            // Health Minister.
            builder.HasData
            (
                new User()
                {
                    Id = 5,
                    FirstName = "Mark",
                    LastName = "Butler",
                    EmailAddress = "mark.butler@example.com",
                    Password = "markbulter",
                    Created = creationDate,
                    Updated = creationDate
                }
            );
        }
    }
}
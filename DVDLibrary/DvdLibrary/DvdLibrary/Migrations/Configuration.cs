namespace DvdLibrary.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DvdLibrary.Models.EF.DvdLibraryEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DvdLibrary.Models.EF.DvdLibraryEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Ratings.AddOrUpdate(
                    r => r.RatingName,
                    new Models.EF.Rating { RatingName = "G" },
                    new Models.EF.Rating { RatingName = "PG" },
                    new Models.EF.Rating { RatingName = "PG13" },
                    new Models.EF.Rating { RatingName = "R" }
                );

            context.Directors.AddOrUpdate(
                    d => d.DirectorName,
                    new Models.EF.Director { DirectorName = "Palo Glob" },
                    new Models.EF.Director { DirectorName = "Cole Smith" },
                    new Models.EF.Director { DirectorName = "Grorff Glob" },
                    new Models.EF.Director { DirectorName = "Dalob Mollfi" }
                );

            context.Releases.AddOrUpdate(
                    r => r.ReleaseYear,
                    new Models.EF.Release { ReleaseYear = 1994 },
                    new Models.EF.Release { ReleaseYear = 1997 },
                    new Models.EF.Release { ReleaseYear = 1984 },
                    new Models.EF.Release { ReleaseYear = 2004 }
                );

            context.Dvds.AddOrUpdate(
                    d => d.DvdTitle,
                    new Models.EF.DvdInfo
                    {
                        DvdTitle = "Howl's Moving Castel",
                        RatingID = context.Ratings.First(r => r.RatingName == "PG").RatingID,
                        DirectorID = context.Directors.First(d => d.DirectorName == "Cole Smith").DirectorID,
                        ReleaseYearID = context.Releases.First(r => r.ReleaseYear == 1994).ReleaseYearID,
                        Notes = "Such a movie!"

                    }
                );
        }
    }
}

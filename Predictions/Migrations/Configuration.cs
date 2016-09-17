namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;
    using Predictions.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Predictions.DAL.PredictionsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Predictions.DAL.PredictionsContext context)
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

            var experts = new List<Expert>
            {
                new Expert { Nickname = "cherocky" },
                new Expert { Nickname = "fuliver" },
                new Expert { Nickname = "Gwynbleidd " },
                new Expert { Nickname = "Chester"},
                new Expert { Nickname = "Polidevk"},
                new Expert { Nickname = "Андреа БазиЛеоник"},
                new Expert { Nickname = "поручик Киже"},
                new Expert { Nickname = "Nightmare"},
                new Expert { Nickname = "Iv1oWitch"},
                new Expert { Nickname = "Ibrahim Loza"},
                new Expert { Nickname = "EnotSty"},
                new Expert { Nickname = "Pinkman"},
                new Expert { Nickname = "SUBIC"},
                new Expert { Nickname = "Alex McLydy"},
                new Expert { Nickname = "Венцеслава"}
            };
            experts.ForEach(x => context.Experts.AddOrUpdate(z => z.Nickname, x));

            var teams = new List<Team>
            {
                new Team { Title = "Спартак" },
                new Team { Title = "Цска" }
            };
            teams.ForEach(x => context.Teams.AddOrUpdate(z => z.Title, x));

            context.SaveChanges();

        }
    }
}

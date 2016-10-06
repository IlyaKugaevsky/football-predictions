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
                new Expert { Nickname = "Венцеслава"},
                new Expert { Nickname = "SumarokovNC-17"},
                new Expert { Nickname = "ginger-ti"}
            };
            experts.ForEach(x => context.Experts.AddOrUpdate(z => z.Nickname, x));

            var teams = new List<Team>
            {
                new Team { Title = "Манчестер Сити" },
                new Team { Title = "Боруссия М" },
                new Team { Title = "Барселона" },
                new Team { Title = "Селтик" },
                new Team { Title = "Бенфика" },
                new Team { Title = "Бешикташ" },
                new Team { Title = "Динамо Киев" },
                new Team { Title = "Наполи" },
                new Team { Title = "ПСЖ" },
                new Team { Title = "Арсенал" },
                new Team { Title = "Базель" },
                new Team { Title = "Лудогорец" },
                new Team { Title = "Бавария" },
                new Team { Title = "Ростов" },
                new Team { Title = "ПСВ" },
                new Team { Title = "Атлетико" },
                new Team { Title = "Порту" },
                new Team { Title = "Копенгаген" },
                new Team { Title = "Брюгге" },
                new Team { Title = "Лестер" },
                new Team { Title = "Тоттенхэм" },
                new Team { Title = "Монако" },
                new Team { Title = "Байер" },
                new Team { Title = "ЦСКА" },
                new Team { Title = "Ювенус" },
                new Team { Title = "Севилья" },
                new Team { Title = "Лион" },
                new Team { Title = "Динамо Загреб" },
                new Team { Title = "Реал Мадрид" },
                new Team { Title = "Легия" },
                new Team { Title = "Боруссия Д" },
            };
            teams.ForEach(x => context.Teams.AddOrUpdate(z => z.Title, x));

            context.SaveChanges();

        }
    }
}

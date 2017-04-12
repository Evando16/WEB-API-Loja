namespace Loja.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Loja.Repository.RepositoryLojaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //NÃO Permite a perda de dados no banco (necessário para fazer a remoção de colunas)
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Loja.Repository.RepositoryLojaContext context)
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
        }
    }
}

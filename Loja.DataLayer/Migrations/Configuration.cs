using System.Data.Entity.Migrations;

namespace Loja.DataLayer.Migrations
{
    

    public sealed class Configuration : DbMigrationsConfiguration<LojaModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //Permite a perda de dados no banco (necessário para fazer a remoção de colunas)
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(LojaModelContext context)
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

namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
          Sql("INSERT INTO Genres (Name) VALUES ('Action')");
          Sql("INSERT INTO Genres (Name) VALUES ('Comedy')");
          Sql("INSERT INTO Genres (Name) VALUES ('Drama')");
          Sql("INSERT INTO Genres (Name) VALUES ('Thriller')");
          Sql("INSERT INTO Genres (Name) VALUES ('Horror')");
        }
        
        public override void Down()
        {
        }
    }
}

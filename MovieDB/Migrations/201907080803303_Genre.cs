namespace MovieDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Genre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movie", "overview", c => c.String());
            AddColumn("dbo.Movie", "posterpath", c => c.String());
            AddColumn("dbo.Movie", "releasedate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movie", "releasedate");
            DropColumn("dbo.Movie", "posterpath");
            DropColumn("dbo.Movie", "overview");
        }
    }
}

namespace RepositoryLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuoteID = c.Int(nullable: false),
                        QuoteType = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Premium = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sales = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuoteID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        UserPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Quotes");
        }
    }
}

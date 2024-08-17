namespace RepositoryLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondCreation : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Quotes");
            DropColumn("dbo.Quotes", "ID");
            AddColumn("dbo.Quotes", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Quotes", "QuoteID", c => c.Int(nullable: false, identity: false));
            AddPrimaryKey("dbo.Quotes", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Quotes");
            AlterColumn("dbo.Quotes", "QuoteID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Quotes", "ID");
            AddPrimaryKey("dbo.Quotes", "QuoteID");
        }
    }
}

namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientBor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "Client_Id", c => c.Int());
            CreateIndex("dbo.Books", "Client_Id");
            AddForeignKey("dbo.Books", "Client_Id", "dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Client_Id", "dbo.Clients");
            DropIndex("dbo.Books", new[] { "Client_Id" });
            DropColumn("dbo.Books", "Client_Id");
            DropTable("dbo.Clients");
        }
    }
}

namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryBor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BorrowedHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BorrowDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        Book_BookId = c.Guid(),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_BookId)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Book_BookId)
                .Index(t => t.Client_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowedHistories", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.BorrowedHistories", "Book_BookId", "dbo.Books");
            DropIndex("dbo.BorrowedHistories", new[] { "Client_Id" });
            DropIndex("dbo.BorrowedHistories", new[] { "Book_BookId" });
            DropTable("dbo.BorrowedHistories");
        }
    }
}

namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        MiddleName = c.String(maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Birthday = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuthorId)
                .Index(t => t.FirstName)
                .Index(t => t.LastName);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Year = c.Int(nullable: false),
                        Genre = c.Int(nullable: false),
                        Author_AuthorId = c.Int(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Authors", t => t.Author_AuthorId)
                .Index(t => t.Title)
                .Index(t => t.Genre)
                .Index(t => t.Author_AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Author_AuthorId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "Author_AuthorId" });
            DropIndex("dbo.Books", new[] { "Genre" });
            DropIndex("dbo.Books", new[] { "Title" });
            DropIndex("dbo.Authors", new[] { "LastName" });
            DropIndex("dbo.Authors", new[] { "FirstName" });
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}

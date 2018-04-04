namespace DvdLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        DirectorID = c.Int(nullable: false, identity: true),
                        DirectorName = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.DirectorID);
            
            CreateTable(
                "dbo.DvdInfoes",
                c => new
                    {
                        DvdID = c.Int(nullable: false, identity: true),
                        DvdTitle = c.String(maxLength: 70),
                        ReleaseYearID = c.Int(nullable: false),
                        RatingID = c.Int(nullable: false),
                        DirectorID = c.Int(nullable: false),
                        Notes = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.DvdID)
                .ForeignKey("dbo.Directors", t => t.DirectorID)
                .ForeignKey("dbo.Ratings", t => t.RatingID)
                .ForeignKey("dbo.Releases", t => t.ReleaseYearID)
                .Index(t => t.ReleaseYearID)
                .Index(t => t.RatingID)
                .Index(t => t.DirectorID);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingID = c.Int(nullable: false, identity: true),
                        RatingName = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.RatingID);
            
            CreateTable(
                "dbo.Releases",
                c => new
                    {
                        ReleaseYearID = c.Int(nullable: false, identity: true),
                        ReleaseYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReleaseYearID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DvdInfoes", "ReleaseYearID", "dbo.Releases");
            DropForeignKey("dbo.DvdInfoes", "RatingID", "dbo.Ratings");
            DropForeignKey("dbo.DvdInfoes", "DirectorID", "dbo.Directors");
            DropIndex("dbo.DvdInfoes", new[] { "DirectorID" });
            DropIndex("dbo.DvdInfoes", new[] { "RatingID" });
            DropIndex("dbo.DvdInfoes", new[] { "ReleaseYearID" });
            DropTable("dbo.Releases");
            DropTable("dbo.Ratings");
            DropTable("dbo.DvdInfoes");
            DropTable("dbo.Directors");
        }
    }
}

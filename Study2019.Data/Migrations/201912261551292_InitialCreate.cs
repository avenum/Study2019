namespace Study2019.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avatars",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ImageId })
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MimeType = c.String(),
                        Created = c.DateTime(nullable: false),
                        UserName = c.String(),
                        BlobId = c.Guid(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.PostImages",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                        OrderNum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.ImageId })
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginName = c.String(maxLength: 100),
                        PasswordHash = c.String(),
                        Salt = c.String(maxLength: 50),
                        Nickname = c.String(maxLength: 100),
                        RegDate = c.DateTime(nullable: false),
                        BirtDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        IsShared = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Images", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Avatars", "UserId", "dbo.Users");
            DropForeignKey("dbo.PostImages", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostImages", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Avatars", "ImageId", "dbo.Images");
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.PostImages", new[] { "ImageId" });
            DropIndex("dbo.PostImages", new[] { "PostId" });
            DropIndex("dbo.Images", new[] { "User_Id" });
            DropIndex("dbo.Avatars", new[] { "ImageId" });
            DropIndex("dbo.Avatars", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Posts");
            DropTable("dbo.PostImages");
            DropTable("dbo.Images");
            DropTable("dbo.Avatars");
        }
    }
}

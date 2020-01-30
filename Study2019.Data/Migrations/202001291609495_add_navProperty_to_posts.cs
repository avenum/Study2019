namespace Study2019.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_navProperty_to_posts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "User_Id", "dbo.Users");
            DropIndex("dbo.Images", new[] { "User_Id" });
            DropColumn("dbo.Images", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "User_Id", c => c.Int());
            CreateIndex("dbo.Images", "User_Id");
            AddForeignKey("dbo.Images", "User_Id", "dbo.Users", "Id");
        }
    }
}

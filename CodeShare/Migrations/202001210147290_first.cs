namespace CodeShare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrivateFileModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Location = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Owner = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.UserUploadModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserUploadModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.PrivateFileModels");
        }
    }
}

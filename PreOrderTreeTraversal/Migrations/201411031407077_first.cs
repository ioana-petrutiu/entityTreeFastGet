namespace PreOrderTreeTraversal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        RootId = c.Int(),
                        lft = c.Int(),
                        rgt = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskModels", t => t.ParentId)
                .ForeignKey("dbo.TaskModels", t => t.RootId)
                .Index(t => t.ParentId)
                .Index(t => t.RootId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskModels", "RootId", "dbo.TaskModels");
            DropForeignKey("dbo.TaskModels", "ParentId", "dbo.TaskModels");
            DropIndex("dbo.TaskModels", new[] { "RootId" });
            DropIndex("dbo.TaskModels", new[] { "ParentId" });
            DropTable("dbo.TaskModels");
        }
    }
}

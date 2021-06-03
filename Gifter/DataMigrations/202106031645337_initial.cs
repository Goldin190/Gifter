namespace Gifter.DataMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropColumn("public.Categories", "UserId");
            DropColumn("public.PersonsProperties", "CategoryId");
            DropTable("public.PropertiesCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "public.PropertiesCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("public.PersonsProperties", "CategoryId", c => c.Int(nullable: false));
            AddColumn("public.Categories", "UserId", c => c.String());
        }
    }
}

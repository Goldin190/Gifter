namespace Gifter.DataMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3 : DbMigration
    {
        public override void Up()
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
        }
        
        public override void Down()
        {
            DropColumn("public.PersonsProperties", "CategoryId");
            DropTable("public.PropertiesCategories");
        }
    }
}

namespace Gifter.DataMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Categories", "UserId", c => c.String());
            AlterColumn("public.Persons", "FirstName", c => c.String(nullable: false));
            AlterColumn("public.Persons", "SirName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("public.Persons", "SirName", c => c.String());
            AlterColumn("public.Persons", "FirstName", c => c.String());
            DropColumn("public.Categories", "UserId");
        }
    }
}

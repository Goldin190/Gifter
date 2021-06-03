namespace Gifter.DataMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Categories", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("public.Categories", "UserId");
        }
    }
}

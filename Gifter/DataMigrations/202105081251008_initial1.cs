namespace Gifter.DataMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Categories", "UserId", c => c.String());
            DropColumn("public.Categories", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("public.Categories", "User_Id", c => c.String());
            DropColumn("public.Categories", "UserId");
        }
    }
}

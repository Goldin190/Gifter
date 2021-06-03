namespace Gifter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "public.AspNetRoles", newName: "Categories");
            DropForeignKey("public.AspNetUserRoles", "RoleId", "public.AspNetRoles");
            DropForeignKey("public.AspNetUserClaims", "UserId", "public.AspNetUsers");
            DropForeignKey("public.AspNetUserLogins", "UserId", "public.AspNetUsers");
            DropForeignKey("public.AspNetUserRoles", "UserId", "public.AspNetUsers");
            DropIndex("public.Categories", "RoleNameIndex");
            DropIndex("public.AspNetUserRoles", new[] { "UserId" });
            DropIndex("public.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("public.AspNetUsers", "UserNameIndex");
            DropIndex("public.AspNetUserClaims", new[] { "UserId" });
            DropIndex("public.AspNetUserLogins", new[] { "UserId" });
            DropPrimaryKey("public.Categories");
            CreateTable(
                "public.PersonsDislikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PersonId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.PersonsLikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PersonId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.PersonsProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Persons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        AddInfo = c.String(),
                        BirthDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Presents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        LinkToProduct = c.String(),
                        IsDone = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("public.Categories", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("public.Categories", "Name", c => c.String());
            AddPrimaryKey("public.Categories", "Id");
            DropTable("public.AspNetUserRoles");
            DropTable("public.AspNetUsers");
            DropTable("public.AspNetUserClaims");
            DropTable("public.AspNetUserLogins");
        }
        
        public override void Down()
        {
            CreateTable(
                "public.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
            CreateTable(
                "public.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            DropPrimaryKey("public.Categories");
            AlterColumn("public.Categories", "Name", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("public.Categories", "Id", c => c.String(nullable: false, maxLength: 128));
            DropTable("public.Presents");
            DropTable("public.Persons");
            DropTable("public.PersonsProperties");
            DropTable("public.PersonsLikes");
            DropTable("public.PersonsDislikes");
            AddPrimaryKey("public.Categories", "Id");
            CreateIndex("public.AspNetUserLogins", "UserId");
            CreateIndex("public.AspNetUserClaims", "UserId");
            CreateIndex("public.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("public.AspNetUserRoles", "RoleId");
            CreateIndex("public.AspNetUserRoles", "UserId");
            CreateIndex("public.Categories", "Name", unique: true, name: "RoleNameIndex");
            AddForeignKey("public.AspNetUserRoles", "UserId", "public.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("public.AspNetUserLogins", "UserId", "public.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("public.AspNetUserClaims", "UserId", "public.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("public.AspNetUserRoles", "RoleId", "public.AspNetRoles", "Id", cascadeDelete: true);
            RenameTable(name: "public.Categories", newName: "AspNetRoles");
        }
    }
}

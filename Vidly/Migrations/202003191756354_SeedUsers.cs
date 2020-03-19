namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            //giving allivated user power: note need to modify register method first
            //and copy scripts from data base of aspnetusers for registed users
            //aspnetroles to grab added alivated modification
            //aspnetuserroles to give rights
            //note: delete created role since it will be re added in this migration
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'3e86773f-54d2-4318-949a-8b0fae61a2c4', N'guest@test.com', 0, N'ANF6GtacEmkmWcKVQTRezlZJfPHkOmNTzxDq4To/6hiMT9i7it3a/Ry+MnvNEhe9Lg==', N'e428db8c-20d6-409c-9df3-c3ead108d968', NULL, 0, 0, NULL, 1, 0, N'guest@test.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd94ec84a-c9cf-408e-942b-24ba303815a8', N'admin@test.com', 0, N'AGnUyZ7fcwP4SsECiq9qqjYKHUAMQsYxMqWfQJEqqn/PXwqLvgtB29fVzph6D//97A==', N'b5b7e797-a1fb-45c2-8eac-042f9719c28b', NULL, 0, 0, NULL, 1, 0, N'admin@test.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'36bc16e6-256e-4716-a272-f082a28c4728', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd94ec84a-c9cf-408e-942b-24ba303815a8', N'36bc16e6-256e-4716-a272-f082a28c4728')

");
        }
        
        public override void Down()
        {
        }
    }
}

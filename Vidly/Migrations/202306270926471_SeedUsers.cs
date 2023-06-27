namespace Vidly.Migrations
{
	using System;
    using System.Data.Entity.Migrations;
	public partial class SeedUsers : DbMigration
	{
		public override void Up()
		{
			Sql(@"  INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'350e65ad-c7e3-412f-ae41-e62945f7cb4d', N'admin@vidly.com', 0, N'AC7115u2wRD7n3+IYuvgSuKC3vZ9v0PCut2Q6t+UIRur+kjOrLvHgP+sQJhihCcZNg==', N'54f36dc2-4b5a-452f-945f-6f7d0a574fb4', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

			INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c0993942-31e8-4046-b11c-2bc4f6f92bfd', N'guest@vidly.com', 0, N'AAGxJs2lQVY9DExBY5hZ00jc56GT9peojMnic3TyaTFBB6taiPK6LuhyGtPV+o2MDg==', N'92ed50a0-3379-4da9-9cbc-2876e73422af', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

			INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'cffba3f8-b03c-42ee-9f8c-9671dafcef88', N'CanManageMovies')

			INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'350e65ad-c7e3-412f-ae41-e62945f7cb4d', N'cffba3f8-b03c-42ee-9f8c-9671dafcef88')

			");
        



				}
		public override void Down()
		{
		}
    }
}

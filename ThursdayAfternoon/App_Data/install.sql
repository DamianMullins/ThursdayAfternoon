SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([Id], [UserName], [Identifier], [FirstName], [LastName], [PasswordHash], [PasswordSalt], [Active], [LastLoginDate], [CreatedOn], [ModifiedOn]) 
VALUES (1, N'damian@lowflyingowls.co.uk', '{8DB5F881-5A47-4FF2-A518-CB8B6559EE59}', N'Damian', N'Mullins', N'1821A12F8B7FFBC87D66C3C18B812B9484667737', N'JZE+JGA=', 1, NULL, GETDATE(), NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO

SET IDENTITY_INSERT [dbo].[UserRoles] ON
INSERT [dbo].[UserRoles] ([Id], [Name], [Active], [CreatedOn]) VALUES (1, N'Admin', 1, GETDATE() )
INSERT [dbo].[UserRoles] ([Id], [Name], [Active], [CreatedOn]) VALUES (2, N'User', 1, GETDATE())
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO

INSERT [dbo].[User_UserRole] ([User_Id], [UserRole_Id]) VALUES ( 1, 1 )
INSERT [dbo].[User_UserRole] ([User_Id], [UserRole_Id]) VALUES ( 1, 2 )
GO

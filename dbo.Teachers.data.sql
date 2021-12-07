SET IDENTITY_INSERT [dbo].[Teachers] ON
INSERT INTO [dbo].[Teachers] ([ID], [StudentClassID], [name], [PhoneNumber], [Email], [PersonalId]) VALUES (1, null, 'Bob', '+361234567', 'bob@builder.com', '111111')
SET IDENTITY_INSERT [dbo].[Teachers] OFF

/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4259)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [recipe]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 12/6/2020 2:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeasurementTypes]    Script Date: 12/6/2020 2:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeasurementTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](25) NOT NULL,
	[Abbreviation] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeIngredients]    Script Date: 12/6/2020 2:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeIngredients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[IngredientId] [int] NOT NULL,
	[Amount] [float] NOT NULL,
	[MeasurementTypeId] [int] NOT NULL,
 CONSTRAINT [PK_RecipeIngredients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipes]    Script Date: 12/6/2020 2:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Feeds] [int] NOT NULL,
	[Time] [varchar](15) NOT NULL,
 CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeSteps]    Script Date: 12/6/2020 2:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeSteps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[Text] [text] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Ingredients] ON 

INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (1, N'Broccolini')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (2, N'Olive Oil')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (3, N'Garlic Cloves, Thinly Sliced')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (4, N'Grated or Shaved Parmesan Cheese')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (5, N'Mayonnaise')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (6, N'Lemon Juice')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (7, N'Salt')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (8, N'Bread Crumbs, Preferably Panko')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (9, N'Zest from Small Lemon')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (10, N'Chopped Fresh Dill')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (11, N'Center-Cut Salmon Fillets')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (12, N'Lemon Wedges (For Serving)')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (13, N'Garlic Clove, Thinly Sliced')
INSERT [dbo].[Ingredients] ([Id], [Name]) VALUES (14, N'Sugar')
SET IDENTITY_INSERT [dbo].[Ingredients] OFF
SET IDENTITY_INSERT [dbo].[MeasurementTypes] ON 

INSERT [dbo].[MeasurementTypes] ([Id], [Name], [Abbreviation]) VALUES (1, N'Pound(s)', N'Lbs')
INSERT [dbo].[MeasurementTypes] ([Id], [Name], [Abbreviation]) VALUES (2, N'To Taste', N'taste')
INSERT [dbo].[MeasurementTypes] ([Id], [Name], [Abbreviation]) VALUES (3, N'Count', N'count')
INSERT [dbo].[MeasurementTypes] ([Id], [Name], [Abbreviation]) VALUES (4, N'Cup(s)', N'C')
INSERT [dbo].[MeasurementTypes] ([Id], [Name], [Abbreviation]) VALUES (5, N'Tablespoon(s)', N'tbsp')
SET IDENTITY_INSERT [dbo].[MeasurementTypes] OFF
SET IDENTITY_INSERT [dbo].[RecipeIngredients] ON 

INSERT [dbo].[RecipeIngredients] ([Id], [RecipeId], [IngredientId], [Amount], [MeasurementTypeId]) VALUES (13, 1, 1, 1, 1)
INSERT [dbo].[RecipeIngredients] ([Id], [RecipeId], [IngredientId], [Amount], [MeasurementTypeId]) VALUES (14, 1, 2, 1, 2)
INSERT [dbo].[RecipeIngredients] ([Id], [RecipeId], [IngredientId], [Amount], [MeasurementTypeId]) VALUES (15, 1, 13, 2, 3)
INSERT [dbo].[RecipeIngredients] ([Id], [RecipeId], [IngredientId], [Amount], [MeasurementTypeId]) VALUES (16, 1, 4, 0.25, 3)
INSERT [dbo].[RecipeIngredients] ([Id], [RecipeId], [IngredientId], [Amount], [MeasurementTypeId]) VALUES (17, 1, 5, 3, 5)
INSERT [dbo].[RecipeIngredients] ([Id], [RecipeId], [IngredientId], [Amount], [MeasurementTypeId]) VALUES (18, 1, 6, 1, 5)
INSERT [dbo].[RecipeIngredients] ([Id], [RecipeId], [IngredientId], [Amount], [MeasurementTypeId]) VALUES (19, 1, 7, 1, 2)
INSERT [dbo].[RecipeIngredients] ([Id], [RecipeId], [IngredientId], [Amount], [MeasurementTypeId]) VALUES (20, 1, 8, 0.5, 4)
INSERT [dbo].[RecipeIngredients] ([Id], [RecipeId], [IngredientId], [Amount], [MeasurementTypeId]) VALUES (21, 1, 9, 1, 3)
INSERT [dbo].[RecipeIngredients] ([Id], [RecipeId], [IngredientId], [Amount], [MeasurementTypeId]) VALUES (22, 1, 10, 3, 5)
INSERT [dbo].[RecipeIngredients] ([Id], [RecipeId], [IngredientId], [Amount], [MeasurementTypeId]) VALUES (23, 1, 11, 4, 3)
INSERT [dbo].[RecipeIngredients] ([Id], [RecipeId], [IngredientId], [Amount], [MeasurementTypeId]) VALUES (24, 1, 12, 4, 3)
SET IDENTITY_INSERT [dbo].[RecipeIngredients] OFF
SET IDENTITY_INSERT [dbo].[Recipes] ON 

INSERT [dbo].[Recipes] ([Id], [Name], [Feeds], [Time]) VALUES (1, N'Sheet Pan Salmon with Broccolini', 4, N'35 Minutes')
SET IDENTITY_INSERT [dbo].[Recipes] OFF
SET IDENTITY_INSERT [dbo].[RecipeSteps] ON 

INSERT [dbo].[RecipeSteps] ([Id], [RecipeId], [Text]) VALUES (1, 1, N'Heat the oven to 425 degrees. Line a rimmed baking sheet with foil and lightly grease the foil.')
INSERT [dbo].[RecipeSteps] ([Id], [RecipeId], [Text]) VALUES (2, 1, N'Place the broccolini in the sheet pan and drizzle over 3 tablespoons of olive oil. Add the sliced garlic and parmesan and toss to combine. Spread the broccolini towards the outer parts of the sheet to make room for the salmon.')
INSERT [dbo].[RecipeSteps] ([Id], [RecipeId], [Text]) VALUES (3, 1, N'In a small bowl, combine the mayonnaise with the lemon juice and 1/4 teaspoon salt. In a separate small bowl, combine the breadcrumbs with the lemon zest, dill and 1 1/2 tablespoons olive oil until evenly moistened.')
INSERT [dbo].[RecipeSteps] ([Id], [RecipeId], [Text]) VALUES (4, 1, N'Brush a thin coating of the mayonnaise over the fillets, and sprinkle the breadcrumbs evenly over. Place the fillets in the sheet pan, leaving space in between each for even cooking.')
INSERT [dbo].[RecipeSteps] ([Id], [RecipeId], [Text]) VALUES (5, 1, N'Place the sheet pan on a rack in the center of the oven and bake until the salmon fillets are cooked through and the broccolini is tender, about 15 minutes. Remove from heat. Serve with lemon wedges.')
SET IDENTITY_INSERT [dbo].[RecipeSteps] OFF
ALTER TABLE [dbo].[RecipeIngredients]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredients_Ingredients] FOREIGN KEY([IngredientId])
REFERENCES [dbo].[Ingredients] ([Id])
GO
ALTER TABLE [dbo].[RecipeIngredients] CHECK CONSTRAINT [FK_RecipeIngredients_Ingredients]
GO
ALTER TABLE [dbo].[RecipeIngredients]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredients_MeasurementTypes] FOREIGN KEY([MeasurementTypeId])
REFERENCES [dbo].[MeasurementTypes] ([Id])
GO
ALTER TABLE [dbo].[RecipeIngredients] CHECK CONSTRAINT [FK_RecipeIngredients_MeasurementTypes]
GO
ALTER TABLE [dbo].[RecipeIngredients]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredients_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RecipeIngredients] CHECK CONSTRAINT [FK_RecipeIngredients_Recipe]
GO
ALTER TABLE [dbo].[RecipeIngredients]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredients_RecipeIngredients] FOREIGN KEY([Id])
REFERENCES [dbo].[RecipeIngredients] ([Id])
GO
ALTER TABLE [dbo].[RecipeIngredients] CHECK CONSTRAINT [FK_RecipeIngredients_RecipeIngredients]
GO
ALTER TABLE [dbo].[RecipeSteps]  WITH CHECK ADD  CONSTRAINT [FK_RecipeSteps_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RecipeSteps] CHECK CONSTRAINT [FK_RecipeSteps_Recipe]
GO

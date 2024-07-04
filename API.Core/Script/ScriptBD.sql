USE [BdUsuarios]
GO
/****** Object:  Table [dbo].[TbCiudades]    Script Date: 3/07/2024 11:20:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TbCiudades](
	[IdCiudad] [int] IDENTITY(1,1) NOT NULL,
	[NombreCiudad] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TbCiudades] PRIMARY KEY CLUSTERED 
(
	[IdCiudad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TbUsuarios]    Script Date: 3/07/2024 11:20:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TbUsuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Cedula] [varchar](20) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Apellidos] [varchar](200) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Ciudad] [int] NOT NULL,
 CONSTRAINT [PK_TbUsuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TbCiudades] ON 
GO
INSERT [dbo].[TbCiudades] ([IdCiudad], [NombreCiudad]) VALUES (1, N'ciudad1')
GO
SET IDENTITY_INSERT [dbo].[TbCiudades] OFF
GO
SET IDENTITY_INSERT [dbo].[TbUsuarios] ON 
GO
INSERT [dbo].[TbUsuarios] ([IdUsuario], [Cedula], [Nombre], [Apellidos], [Email], [Ciudad]) VALUES (4, N'123', N'usuario1', N'apellido1', N'usuario1@hotmail.com', 1)
GO
SET IDENTITY_INSERT [dbo].[TbUsuarios] OFF
GO
ALTER TABLE [dbo].[TbUsuarios]  WITH CHECK ADD  CONSTRAINT [FK_TbUsuarios_TbCiudades] FOREIGN KEY([Ciudad])
REFERENCES [dbo].[TbCiudades] ([IdCiudad])
GO
ALTER TABLE [dbo].[TbUsuarios] CHECK CONSTRAINT [FK_TbUsuarios_TbCiudades]
GO
/****** Object:  StoredProcedure [dbo].[SP_CrearUsuario]    Script Date: 3/07/2024 11:20:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CrearUsuario]
	   @Cedula varchar(20),
	   @Nombre varchar(200),
	   @Apellidos varchar(200),
	   @Email varchar(100),
	   @Ciudad int	   
AS
BEGIN 
INSERT INTO [dbo].[TbUsuarios]
           ([Cedula]
           ,[Nombre]
           ,[Apellidos]
           ,[Email]
           ,[Ciudad])
     VALUES
           (@Cedula
           ,@Nombre
           ,@Apellidos
           ,@Email
           ,@Ciudad)

		   SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ObtenerUsuario]    Script Date: 3/07/2024 11:20:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ObtenerUsuario]
	   @cedula	varchar(20)	 
AS
BEGIN 
SELECT *
FROM TbUsuarios 
WHERE [Cedula] = @cedula
END
GO

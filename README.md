# Hyperativa

Para executar a aplicação é necessario os seguintes ajustes:
- Ajustar a stringconnection localizada na pasta App_Start, classe RouteConfig.cs, dentro da tag <connectionStrings>. Deve ser ajustado as informações de "data source" com o caminho do servidor de SQL, "initial catalog" com o banco de dados de sua escolha, "id" com o usuario do banco de dados e "password" com a senha do mesmo.
- Na mesma classe deve ser ajustada a tag <log4net> onde dentro da mesma existe a tag <file value>, deve ser informado um caminho para que a aplicação possa gerar o arquivo de log, não se esquecendo de utilizar \\ no caminho para que o sistema possa entender.
- Apos escolher sua base de dados, deve ser gerada as tabelas conforme script a seguir:
  CREATE TABLE [dbo].[Cartoes]([NrCartao] [decimal](19, 0) NOT NULL, [NrLote] [int] NULL, CONSTRAINT [PK_Cartoes] PRIMARY KEY CLUSTERED 
([NrCartao])
  CREATE TABLE [dbo].[Usuarios]([IdUsuario] [int] IDENTITY(1,1) NOT NULL, [Nome] [varchar](50) NULL, [Senha] [varchar](50) NULL,  [Email] [varchar](100) NULL, CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([IdUsuario])
- Apos criação das tabelas deve ser incluido o usuario admin para que possa ser feito o primeiro login, segue script:
  INSERT INTO dbo.Usuarios (Nome, Senha, Email) VALUES ('admin', 'password', NULL)

Apos isso, é so compilar o sistema.

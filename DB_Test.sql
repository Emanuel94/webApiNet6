/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [userId]
      ,[UserName]
      ,[PasswordHash]
      ,[PasswordSalt]
      ,[DateCreated]
      ,[DateModified]
      ,[Email]
      ,[Password]
  FROM [accountowner].[dbo].[userAuth]


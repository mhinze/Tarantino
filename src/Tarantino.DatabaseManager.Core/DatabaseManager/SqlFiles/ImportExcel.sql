
USE [master]
go

if exists(select name from sys.servers where name = '||DATABASE||_LinkedServer') begin
  exec sp_dropserver @server='||DATABASE||_LinkedServer', @droplogins='droplogins'
end
go

EXEC master.dbo.sp_addlinkedserver @server = N'||DATABASE||_LinkedServer', @srvproduct=N'Excel', @provider=N'Microsoft.Jet.OLEDB.4.0', @datasrc=N'||EXCEL_FILE||', @provstr=N'Excel 8.0'
go
EXEC master.dbo.sp_serveroption @server=N'||DATABASE||_LinkedServer', @optname=N'collation compatible', @optvalue=N'false'
go
EXEC master.dbo.sp_serveroption @server=N'||DATABASE||_LinkedServer', @optname=N'data access', @optvalue=N'true'
go
EXEC master.dbo.sp_serveroption @server=N'||DATABASE||_LinkedServer', @optname=N'rpc', @optvalue=N'false'
go
EXEC master.dbo.sp_serveroption @server=N'||DATABASE||_LinkedServer', @optname=N'rpc out', @optvalue=N'false'
go
EXEC master.dbo.sp_serveroption @server=N'||DATABASE||_LinkedServer', @optname=N'connect timeout', @optvalue=N'0'
go
EXEC master.dbo.sp_serveroption @server=N'||DATABASE||_LinkedServer', @optname=N'collation name', @optvalue=null
go
EXEC master.dbo.sp_serveroption @server=N'||DATABASE||_LinkedServer', @optname=N'query timeout', @optvalue=N'0'
go
EXEC master.dbo.sp_serveroption @server=N'||DATABASE||_LinkedServer', @optname=N'use remote collation', @optvalue=N'true'
go

USE [||DATABASE||]
go

CREATE TABLE TarantinoExcelTables (C1 varchar(255), C2 varchar(255), ExcelTableName varchar(255), C3 varchar(255), C4 varchar(255))
go
 
INSERT TarantinoExcelTables (C1, C2, ExcelTableName, C3, C4)
EXEC sp_tables_ex '||DATABASE||_LinkedServer'
go

    DECLARE @excelTable varchar(255)
    DECLARE @getExcelTables CURSOR
    DECLARE @sql nvarchar(255)

    SET @getExcelTables = CURSOR FOR
         SELECT ExcelTableName FROM TarantinoExcelTables where ExcelTableName like '%$'

    OPEN @getExcelTables

    FETCH NEXT FROM @getExcelTables INTO @excelTable

    WHILE (@@FETCH_STATUS = 0) 
    BEGIN
        set @sql = 'SELECT * INTO [' + @excelTable + '] FROM ||DATABASE||_LinkedServer...[' + @excelTable + ']'
		exec sp_executesql @sql
         FETCH NEXT FROM @getExcelTables INTO @excelTable
    END

    CLOSE @getExcelTables
    DEALLOCATE @getExcelTables
go

drop table TarantinoExcelTables
go
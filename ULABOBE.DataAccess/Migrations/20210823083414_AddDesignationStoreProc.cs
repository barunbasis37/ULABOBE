using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class AddDesignationStoreProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Create PROC usp_GetDesignations 
                                    As 
                                        BEGIN
                                            Select * From dbo.Designations
                                        END");
            migrationBuilder.Sql(@"CREATE PROC usp_GetDesignation 
                                    @Id int 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.CoverTypes  WHERE  (Id = @Id) 
                                    END ");

            migrationBuilder.Sql(@"CREATE PROC usp_UpdateDesignation
	                                @Id int,
	                                @Name varchar(100)
                                    AS 
                                    BEGIN 
                                     UPDATE dbo.Designations
                                     SET  Name = @Name
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_DeleteDesignation
	                                @Id int
                                    AS 
                                    BEGIN 
                                     DELETE FROM dbo.Designations
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_CreateDesignation
                                   @Name varchar(150),
									@Priority int,
									@IsActive bit,
									@QueryId uniqueidentifier,
									@CreatedDate datetime,
									@CreatedBy varchar(50),
									@CreatedIp varchar(50),
									@UpdatedDate datetime,
									@UpdatedBy varchar(50),
									@UpdatedIp varchar(50),
									@IsDeleted bit
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.Designations(Name,Priority,IsActive,QueryId,CreatedDate,CreatedBy,CreatedIp,UpdatedDate,UpdatedBy,UpdatedIp,IsDeleted)
                                    VALUES (@Name,@Priority,@IsActive,@QueryId,@CreatedDate,@CreatedBy,@CreatedIp,@UpdatedDate,@UpdatedBy,@UpdatedIp,@IsDeleted)
									END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetDesignations");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetDesignation");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdateDesignation");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_DeleteDesignation");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CreateDesignation");
        }
    }
}

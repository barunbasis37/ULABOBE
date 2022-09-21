using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifyDesignationUpdateProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER PROC usp_UpdateDesignation
	                                @Id int,
	                                @Name varchar(100),
                                    @Priority int,
									@IsActive bit,
									@QueryId uniqueidentifier,									
									@UpdatedDate datetime,
									@UpdatedBy varchar(50),
									@UpdatedIp varchar(50),
									@IsDeleted bit
                                    AS 
                                    BEGIN 
                                     UPDATE dbo.Designations
                                     SET  Name = @Name,
                                    Priority=@Priority,
									IsActive=@IsActive,
									QueryId=@QueryId,									
									UpdatedDate=@UpdatedDate,
									UpdatedBy=@UpdatedBy,
									UpdatedIp=@UpdatedIp,
									IsDeleted=@IsDeleted
                                     WHERE  Id = @Id
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdateDesignation");
        }
    }
}

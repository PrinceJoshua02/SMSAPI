using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMSAPI.Migrations
{
    public partial class sp_getstudentbyid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE SP_GetStudentById
                        @Id INT
                    AS
                    BEGIN
                        SELECT *
                        FROM Students
                        WHERE Id = @Id;
                    END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE SP_PutStudent";
            migrationBuilder.Sql(sp);
        }
    }
}



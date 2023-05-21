using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMSAPI.Migrations
{
    public partial class sp_getallstudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @" CREATE PROCEDURE SP_GetAllStudent
            AS
            BEGIN
                SELECT* from Students ;
            END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE SP_GetAllStudents";
            migrationBuilder.Sql(sp);
        }
    }
}

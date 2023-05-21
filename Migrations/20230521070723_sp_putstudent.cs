using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMSAPI.Migrations
{
    public partial class sp_putstudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE SP_PutStudent
                       
                            @Id INT,
                            @First_Name VARCHAR(100),
                            @Last_Name VARCHAR(100),
                            @Middle_Name VARCHAR(250),
                            @Address VARCHAR(100),
                            @DOB VARCHAR(100)
                    AS
                    BEGIN
                        UPDATE Students
                        SET First_Name = @First_Name, Last_Name = @Last_Name, Middle_Name = @Middle_Name, Address = @Address, DOB = @DOB
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


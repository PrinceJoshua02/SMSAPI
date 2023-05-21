using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMSAPI.Migrations
{
    public partial class sp_addstudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE SP_AddStudent
                       
                            @First_Name VARCHAR(100),
                            @Last_Name VARCHAR(100),
                            @Middle_Name VARCHAR(250),
                            @Address VARCHAR(100),
                            @DOB VARCHAR(100)
                      
                        AS
                        BEGIN
                            INSERT INTO Students (First_Name,Last_Name, Middle_Name, Address, DOB)
                            VALUES (@First_Name, @Last_Name, @Middle_Name, @Address, @DOB);
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE SP_AddStudent";
            migrationBuilder.Sql(sp);
        }
    }
}


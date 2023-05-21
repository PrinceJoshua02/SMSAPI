using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMSAPI.Migrations
{
    public partial class sp_addapplicationuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
        CREATE PROCEDURE SP_AddApplicationUser
            @Id nvarchar(100),
            @FirstName nvarchar(100),
            @LastName nvarchar(100),
            @Address nvarchar(100),
             @UserName nvarchar(100),
              @Password nvarchar(100)

          AS
        BEGIN
            INSERT INTO AspNetUsers (
Id,
First_Name,
Last_Name,
Address,
UserName,
NormalizedUserName,
Email,
NormalizedEmail,
EmailConfirmed,
PasswordHash,
AccessFailedCount,
PhoneNumberConfirmed,
TwoFactorEnabled,
LockoutEnd,
LockoutEnabled

 )

            VALUES (
           @Id,
            @FirstName ,
            @LastName,
            @Address,
             @UserName,
              UPPER (@UserName),
                @UserName,
               UPPER(@UserName),               
                0,
                @Password,
                0,
                'false',
                   'false',
                      '',
                    'false'
                      


                 
           );
        END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}


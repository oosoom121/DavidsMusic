using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DavidsMusic.Migrations
{
    public partial class Register : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConfirmPassword = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    RegAddress1 = table.Column<string>(nullable: true),
                    RegAddress2 = table.Column<string>(nullable: true),
                    RegCellPhone = table.Column<string>(nullable: true),
                    RegCity = table.Column<string>(nullable: true),
                    RegEmail = table.Column<string>(nullable: false),
                    RegFirstName = table.Column<string>(nullable: false),
                    RegHomePhone = table.Column<string>(nullable: true),
                    RegLastName = table.Column<string>(nullable: false),
                    RegPostal = table.Column<string>(nullable: true),
                    RegState = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Register");
        }
    }
}

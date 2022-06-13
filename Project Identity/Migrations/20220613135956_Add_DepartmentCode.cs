using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Identity.Migrations
{
    public partial class Add_DepartmentCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department_Code",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department_Code",
                table: "AspNetUsers");
        }
    }
}

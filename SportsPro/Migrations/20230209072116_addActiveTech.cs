using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsPro.Migrations
{
    public partial class addActiveTech : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Technicians",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Technicians",
                keyColumn: "TechnicianID",
                keyValue: 11,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Technicians",
                keyColumn: "TechnicianID",
                keyValue: 12,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Technicians",
                keyColumn: "TechnicianID",
                keyValue: 13,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Technicians",
                keyColumn: "TechnicianID",
                keyValue: 14,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Technicians",
                keyColumn: "TechnicianID",
                keyValue: 15,
                column: "Active",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Technicians");
        }
    }
}

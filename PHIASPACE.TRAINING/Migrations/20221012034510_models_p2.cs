using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHIASPACE.TRAINING.Migrations
{
    public partial class models_p2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_FacilityType",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_FacilityType", x => x.TypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Facility_TypeId",
                table: "Tbl_Facility",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Facility_Tbl_FacilityType_TypeId",
                table: "Tbl_Facility",
                column: "TypeId",
                principalTable: "Tbl_FacilityType",
                principalColumn: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Facility_Tbl_FacilityType_TypeId",
                table: "Tbl_Facility");

            migrationBuilder.DropTable(
                name: "Tbl_FacilityType");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Facility_TypeId",
                table: "Tbl_Facility");
        }
    }
}

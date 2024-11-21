using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHIASPACE.TRAINING.Migrations
{
    public partial class models_p5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Facility_Tbl_FacilityClassification_ClassificationId",
                table: "Tbl_Facility");

            migrationBuilder.DropColumn(
                name: "UsClassificationId",
                table: "Tbl_Facility");

            migrationBuilder.AlterColumn<int>(
                name: "ClassificationId",
                table: "Tbl_Facility",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Facility_Tbl_FacilityClassification_ClassificationId",
                table: "Tbl_Facility",
                column: "ClassificationId",
                principalTable: "Tbl_FacilityClassification",
                principalColumn: "ClassificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Facility_Tbl_FacilityClassification_ClassificationId",
                table: "Tbl_Facility");

            migrationBuilder.AlterColumn<int>(
                name: "ClassificationId",
                table: "Tbl_Facility",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsClassificationId",
                table: "Tbl_Facility",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Facility_Tbl_FacilityClassification_ClassificationId",
                table: "Tbl_Facility",
                column: "ClassificationId",
                principalTable: "Tbl_FacilityClassification",
                principalColumn: "ClassificationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

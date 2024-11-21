using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHIASPACE.TRAINING.Migrations
{
    public partial class models_p3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Facility_Tbl_District_DistrictId1",
                table: "Tbl_Facility");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Facility_Tbl_Province_ProvinceId1",
                table: "Tbl_Facility");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Facility_DistrictId1",
                table: "Tbl_Facility");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Facility_ProvinceId1",
                table: "Tbl_Facility");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Tbl_Province");

            migrationBuilder.DropColumn(
                name: "DistrictId1",
                table: "Tbl_Facility");

            migrationBuilder.DropColumn(
                name: "ProvinceId1",
                table: "Tbl_Facility");

            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                table: "Tbl_Province",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProvinceId",
                table: "Tbl_Facility",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "Tbl_Facility",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tbl_Facility",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Tbl_Facility",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                table: "Tbl_Facility",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Tbl_Facility",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Tbl_Facility",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Facility_DistrictId",
                table: "Tbl_Facility",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Facility_ProvinceId",
                table: "Tbl_Facility",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Facility_Tbl_District_DistrictId",
                table: "Tbl_Facility",
                column: "DistrictId",
                principalTable: "Tbl_District",
                principalColumn: "DistrictId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Facility_Tbl_Province_ProvinceId",
                table: "Tbl_Facility",
                column: "ProvinceId",
                principalTable: "Tbl_Province",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Facility_Tbl_District_DistrictId",
                table: "Tbl_Facility");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Facility_Tbl_Province_ProvinceId",
                table: "Tbl_Facility");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Facility_DistrictId",
                table: "Tbl_Facility");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Facility_ProvinceId",
                table: "Tbl_Facility");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Tbl_Province");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tbl_Facility");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tbl_Facility");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Tbl_Facility");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Tbl_Facility");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Tbl_Facility");

            migrationBuilder.AddColumn<string>(
                name: "DeletedAt",
                table: "Tbl_Province",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProvinceId",
                table: "Tbl_Facility",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DistrictId",
                table: "Tbl_Facility",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId1",
                table: "Tbl_Facility",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId1",
                table: "Tbl_Facility",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Facility_DistrictId1",
                table: "Tbl_Facility",
                column: "DistrictId1");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Facility_ProvinceId1",
                table: "Tbl_Facility",
                column: "ProvinceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Facility_Tbl_District_DistrictId1",
                table: "Tbl_Facility",
                column: "DistrictId1",
                principalTable: "Tbl_District",
                principalColumn: "DistrictId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Facility_Tbl_Province_ProvinceId1",
                table: "Tbl_Facility",
                column: "ProvinceId1",
                principalTable: "Tbl_Province",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHIASPACE.TRAINING.Data.Migrations
{
    public partial class TrainingDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Course",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertificateType = table.Column<int>(type: "int", nullable: true),
                    CmeUnits = table.Column<int>(type: "int", nullable: true),
                    ValidityPeriodMonths = table.Column<int>(type: "int", nullable: true),
                    ThematicArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationDays = table.Column<int>(type: "int", nullable: true),
                    PassScore = table.Column<double>(type: "float", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_FacilityClassification",
                columns: table => new
                {
                    ClassificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassificationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_FacilityClassification", x => x.ClassificationId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_FacilityLevel",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_FacilityLevel", x => x.LevelId);
                });

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

            migrationBuilder.CreateTable(
                name: "Tbl_Lookup",
                columns: table => new
                {
                    LookupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LookupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Lookup", x => x.LookupId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Profession",
                columns: table => new
                {
                    ProfessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Profession", x => x.ProfessionId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Province",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Province", x => x.ProvinceId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_District",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_District", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_Tbl_District_Tbl_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Tbl_Province",
                        principalColumn: "ProvinceId");
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Facility",
                columns: table => new
                {
                    FacilityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassificationId = table.Column<int>(type: "int", nullable: true),
                    UsCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    LevelId = table.Column<int>(type: "int", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "(N'')"),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "(N'')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Facility", x => x.FacilityId);
                    table.ForeignKey(
                        name: "FK_Tbl_Facility_Tbl_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Tbl_District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_Facility_Tbl_FacilityClassification_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Tbl_FacilityClassification",
                        principalColumn: "ClassificationId");
                    table.ForeignKey(
                        name: "FK_Tbl_Facility_Tbl_FacilityLevel_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Tbl_FacilityLevel",
                        principalColumn: "LevelId");
                    table.ForeignKey(
                        name: "FK_Tbl_Facility_Tbl_FacilityType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Tbl_FacilityType",
                        principalColumn: "TypeId");
                    table.ForeignKey(
                        name: "FK_Tbl_Facility_Tbl_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Tbl_Province",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Venue",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Venue", x => x.VenueId);
                    table.ForeignKey(
                        name: "FK_Tbl_Venue_Tbl_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Tbl_District",
                        principalColumn: "DistrictId");
                    table.ForeignKey(
                        name: "FK_Tbl_Venue_Tbl_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Tbl_Province",
                        principalColumn: "ProvinceId");
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Persons",
                columns: table => new
                {
                    IdentificationCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<int>(type: "int", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    FacilityId = table.Column<int>(type: "int", nullable: true),
                    TblFacilityFacilityId = table.Column<int>(type: "int", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessionId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Persons", x => x.IdentificationCode);
                    table.ForeignKey(
                        name: "FK_Tbl_Persons_Tbl_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Tbl_District",
                        principalColumn: "DistrictId");
                    table.ForeignKey(
                        name: "FK_Tbl_Persons_Tbl_Facility_TblFacilityFacilityId",
                        column: x => x.TblFacilityFacilityId,
                        principalTable: "Tbl_Facility",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_Persons_Tbl_Profession_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Tbl_Profession",
                        principalColumn: "ProfessionId");
                    table.ForeignKey(
                        name: "FK_Tbl_Persons_Tbl_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Tbl_Province",
                        principalColumn: "ProvinceId");
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Training",
                columns: table => new
                {
                    TrainingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    VenueId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedParticipants = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trainingstatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Statusmessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Training", x => x.TrainingId);
                    table.ForeignKey(
                        name: "FK_Tbl_Training_Tbl_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Tbl_Course",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Tbl_Training_Tbl_Venue_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Tbl_Venue",
                        principalColumn: "VenueId");
                });

            migrationBuilder.CreateTable(
                name: "Tbl_FacilityTransfers",
                columns: table => new
                {
                    TransferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonIdentificationCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FacilityId = table.Column<int>(type: "int", nullable: false),
                    TransferIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_FacilityTransfers", x => x.TransferId);
                    table.ForeignKey(
                        name: "FK_Tbl_FacilityTransfers_Tbl_Facility_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Tbl_Facility",
                        principalColumn: "FacilityId");
                    table.ForeignKey(
                        name: "FK_Tbl_FacilityTransfers_Tbl_Persons_PersonIdentificationCode",
                        column: x => x.PersonIdentificationCode,
                        principalTable: "Tbl_Persons",
                        principalColumn: "IdentificationCode");
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Certificate",
                columns: table => new
                {
                    CertificateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    IdentificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonIdentificationCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    CertStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Certificate", x => x.CertificateId);
                    table.ForeignKey(
                        name: "FK_Tbl_Certificate_Tbl_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Tbl_Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_Certificate_Tbl_Persons_PersonIdentificationCode",
                        column: x => x.PersonIdentificationCode,
                        principalTable: "Tbl_Persons",
                        principalColumn: "IdentificationCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_Certificate_Tbl_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Tbl_Training",
                        principalColumn: "TrainingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_TrainingFacilitator",
                columns: table => new
                {
                    FacilitatorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegisteredBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    PersonIdentificationCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_TrainingFacilitator", x => x.FacilitatorId);
                    table.ForeignKey(
                        name: "FK_Tbl_TrainingFacilitator_Tbl_Persons_PersonIdentificationCode",
                        column: x => x.PersonIdentificationCode,
                        principalTable: "Tbl_Persons",
                        principalColumn: "IdentificationCode");
                    table.ForeignKey(
                        name: "FK_Tbl_TrainingFacilitator_Tbl_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Tbl_Training",
                        principalColumn: "TrainingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_TrainingParticipant",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreTestScore = table.Column<double>(type: "float", nullable: true),
                    PostTestScore = table.Column<double>(type: "float", nullable: true),
                    FinalScore = table.Column<double>(type: "float", nullable: true),
                    CompletionStatus = table.Column<int>(type: "int", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegisteredBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    PersonIdentificationCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_TrainingParticipant", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_Tbl_TrainingParticipant_Tbl_Persons_PersonIdentificationCode",
                        column: x => x.PersonIdentificationCode,
                        principalTable: "Tbl_Persons",
                        principalColumn: "IdentificationCode");
                    table.ForeignKey(
                        name: "FK_Tbl_TrainingParticipant_Tbl_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Tbl_Training",
                        principalColumn: "TrainingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Certificate_CourseId",
                table: "Tbl_Certificate",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Certificate_PersonIdentificationCode",
                table: "Tbl_Certificate",
                column: "PersonIdentificationCode");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Certificate_TrainingId",
                table: "Tbl_Certificate",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_District_ProvinceId",
                table: "Tbl_District",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Facility_ClassificationId",
                table: "Tbl_Facility",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Facility_DistrictId",
                table: "Tbl_Facility",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Facility_LevelId",
                table: "Tbl_Facility",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Facility_ProvinceId",
                table: "Tbl_Facility",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Facility_TypeId",
                table: "Tbl_Facility",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_FacilityTransfers_FacilityId",
                table: "Tbl_FacilityTransfers",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_FacilityTransfers_PersonIdentificationCode",
                table: "Tbl_FacilityTransfers",
                column: "PersonIdentificationCode");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Persons_DistrictId",
                table: "Tbl_Persons",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Persons_ProfessionId",
                table: "Tbl_Persons",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Persons_ProvinceId",
                table: "Tbl_Persons",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Persons_TblFacilityFacilityId",
                table: "Tbl_Persons",
                column: "TblFacilityFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Training_CourseId",
                table: "Tbl_Training",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Training_VenueId",
                table: "Tbl_Training",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_TrainingFacilitator_PersonIdentificationCode",
                table: "Tbl_TrainingFacilitator",
                column: "PersonIdentificationCode");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_TrainingFacilitator_TrainingId",
                table: "Tbl_TrainingFacilitator",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_TrainingParticipant_PersonIdentificationCode",
                table: "Tbl_TrainingParticipant",
                column: "PersonIdentificationCode");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_TrainingParticipant_TrainingId",
                table: "Tbl_TrainingParticipant",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Venue_DistrictId",
                table: "Tbl_Venue",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Venue_ProvinceId",
                table: "Tbl_Venue",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Certificate");

            migrationBuilder.DropTable(
                name: "Tbl_FacilityTransfers");

            migrationBuilder.DropTable(
                name: "Tbl_Lookup");

            migrationBuilder.DropTable(
                name: "Tbl_TrainingFacilitator");

            migrationBuilder.DropTable(
                name: "Tbl_TrainingParticipant");

            migrationBuilder.DropTable(
                name: "Tbl_Persons");

            migrationBuilder.DropTable(
                name: "Tbl_Training");

            migrationBuilder.DropTable(
                name: "Tbl_Facility");

            migrationBuilder.DropTable(
                name: "Tbl_Profession");

            migrationBuilder.DropTable(
                name: "Tbl_Course");

            migrationBuilder.DropTable(
                name: "Tbl_Venue");

            migrationBuilder.DropTable(
                name: "Tbl_FacilityClassification");

            migrationBuilder.DropTable(
                name: "Tbl_FacilityLevel");

            migrationBuilder.DropTable(
                name: "Tbl_FacilityType");

            migrationBuilder.DropTable(
                name: "Tbl_District");

            migrationBuilder.DropTable(
                name: "Tbl_Province");
        }
    }
}

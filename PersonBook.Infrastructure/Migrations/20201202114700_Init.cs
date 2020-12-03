using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonBook.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PersonBook");

            migrationBuilder.EnsureSchema(
                name: "PersonBookReadModel");

            migrationBuilder.CreateTable(
                name: "City",
                schema: "PersonBook",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "PersonBook",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Gender = table.Column<byte>(nullable: false),
                    PersonalNumber = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTimeOffset>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    CityName = table.Column<string>(nullable: true),
                    PhotoId = table.Column<int>(nullable: true),
                    Photo_PhotoId = table.Column<int>(nullable: true),
                    Photo_Url = table.Column<string>(nullable: true),
                    Photo_Width = table.Column<int>(nullable: true),
                    Photo_Height = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonRelationship",
                schema: "PersonBook",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    PersonRelationshipType = table.Column<byte>(nullable: false),
                    FirstPersonId = table.Column<int>(nullable: false),
                    SecondPersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRelationship", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                schema: "PersonBook",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityReadModel",
                schema: "PersonBookReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AggregateRootId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonReadModel",
                schema: "PersonBookReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AggregateRootId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Gender = table.Column<byte>(nullable: false),
                    PersonalNumber = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTimeOffset>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    CityName = table.Column<string>(nullable: true),
                    MobilePhoneNumber = table.Column<string>(nullable: true),
                    HomePhoneNumber = table.Column<string>(nullable: true),
                    OfficePhoneNumber = table.Column<string>(nullable: true),
                    PhotoId = table.Column<int>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    PhotoWidth = table.Column<int>(nullable: true),
                    PhotoHeight = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonRelationShipReadModel",
                schema: "PersonBookReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AggregateRootId = table.Column<int>(nullable: false),
                    PersonRelationshipType = table.Column<byte>(nullable: false),
                    FirstPersonId = table.Column<int>(nullable: false),
                    SecondPersonId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRelationShipReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumber",
                schema: "PersonBook",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumber_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "PersonBook",
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_PersonalNumber",
                schema: "PersonBook",
                table: "Person",
                column: "PersonalNumber",
                unique: true,
                filter: "[PersonalNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumber_PersonId",
                schema: "PersonBook",
                table: "PhoneNumber",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "City",
                schema: "PersonBook");

            migrationBuilder.DropTable(
                name: "PersonRelationship",
                schema: "PersonBook");

            migrationBuilder.DropTable(
                name: "PhoneNumber",
                schema: "PersonBook");

            migrationBuilder.DropTable(
                name: "Photo",
                schema: "PersonBook");

            migrationBuilder.DropTable(
                name: "CityReadModel",
                schema: "PersonBookReadModel");

            migrationBuilder.DropTable(
                name: "PersonReadModel",
                schema: "PersonBookReadModel");

            migrationBuilder.DropTable(
                name: "PersonRelationShipReadModel",
                schema: "PersonBookReadModel");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "PersonBook");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class Talks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Talk",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Abstract = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talk", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Session_Persons",
                columns: table => new
                {
                    TalkID = table.Column<int>(nullable: false),
                    PersonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session_Persons", x => new { x.PersonID, x.TalkID });
                    table.ForeignKey(
                        name: "FK_Session_Persons_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Session_Persons_Talk_TalkID",
                        column: x => x.TalkID,
                        principalTable: "Talk",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Session_Talks",
                columns: table => new
                {
                    SessionID = table.Column<int>(nullable: false),
                    TalkID = table.Column<int>(nullable: false),
                    Hour = table.Column<DateTime>(nullable: false),
                    Highlight = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session_Talks", x => new { x.SessionID, x.TalkID });
                    table.ForeignKey(
                        name: "FK_Session_Talks_Session_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Session",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Session_Talks_Talk_TalkID",
                        column: x => x.TalkID,
                        principalTable: "Talk",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Session_Persons_TalkID",
                table: "Session_Persons",
                column: "TalkID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_Talks_TalkID",
                table: "Session_Talks",
                column: "TalkID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Session_Persons");

            migrationBuilder.DropTable(
                name: "Session_Talks");

            migrationBuilder.DropTable(
                name: "Talk");
        }
    }
}

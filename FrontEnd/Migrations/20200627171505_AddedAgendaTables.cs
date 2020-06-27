using Microsoft.EntityFrameworkCore.Migrations;

namespace FrontEnd.Migrations
{
    public partial class AddedAgendaTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAgenda",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ConferenceId = table.Column<int>(nullable: false),
                    SessionId = table.Column<int>(nullable: false),
                    TalkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAgenda", x => new { x.UserId, x.ConferenceId, x.SessionId, x.TalkId });
                    table.ForeignKey(
                        name: "FK_UserAgenda_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOwnership",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ConferenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOwnership", x => new { x.UserId, x.ConferenceId });
                    table.ForeignKey(
                        name: "FK_UserOwnership_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAgenda");

            migrationBuilder.DropTable(
                name: "UserOwnership");
        }
    }
}

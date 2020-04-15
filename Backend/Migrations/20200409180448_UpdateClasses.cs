using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class UpdateClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_Persons_Person_PersonID",
                table: "Talk_Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Persons_Talk_TalkID",
                table: "Talk_Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Session_Persons",
                table: "Talk_Persons");

            migrationBuilder.DropIndex(
                name: "IX_Session_Persons_TalkID",
                table: "Talk_Persons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Talk_Persons",
                table: "Talk_Persons",
                columns: new[] { "TalkID", "PersonID" });

            migrationBuilder.CreateIndex(
                name: "IX_Talk_Persons_PersonID",
                table: "Talk_Persons",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Talk_Persons_Person_PersonID",
                table: "Talk_Persons",
                column: "PersonID",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Talk_Persons_Talk_TalkID",
                table: "Talk_Persons",
                column: "TalkID",
                principalTable: "Talk",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Talk_Persons_Person_PersonID",
                table: "Talk_Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Talk_Persons_Talk_TalkID",
                table: "Talk_Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Talk_Persons",
                table: "Talk_Persons");

            migrationBuilder.DropIndex(
                name: "IX_Talk_Persons_PersonID",
                table: "Talk_Persons");


            migrationBuilder.AddPrimaryKey(
                name: "PK_Session_Persons",
                table: "Session_Persons",
                columns: new[] { "PersonID", "TalkID" });

            migrationBuilder.CreateIndex(
                name: "IX_Session_Persons_TalkID",
                table: "Session_Persons",
                column: "TalkID");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Persons_Person_PersonID",
                table: "Session_Persons",
                column: "PersonID",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Persons_Talk_TalkID",
                table: "Session_Persons",
                column: "TalkID",
                principalTable: "Talk",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

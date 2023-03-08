using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment3_V2.Data.Migrations
{
    public partial class addedFinalActor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IMBDLink",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "MoviePoster",
                table: "Movie",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ActorPhoto",
                table: "Actor",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IMBDLink",
                table: "Actor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Actor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actor_MovieId",
                table: "Actor",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Movie_MovieId",
                table: "Actor",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movie_MovieId",
                table: "Actor");

            migrationBuilder.DropIndex(
                name: "IX_Actor_MovieId",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "IMBDLink",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "MoviePoster",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ActorPhoto",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "IMBDLink",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Actor");
        }
    }
}

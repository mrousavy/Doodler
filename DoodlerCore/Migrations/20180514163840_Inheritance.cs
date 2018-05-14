using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DoodlerCore.Migrations
{
    public partial class Inheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Polls",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Answers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Answers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Answers");
        }
    }
}

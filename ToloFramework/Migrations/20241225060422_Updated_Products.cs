﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToloFramework.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppProducts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppProducts",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppProducts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppProducts");
        }
    }
}

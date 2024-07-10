﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNavigationToAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReturningRequests_AssignmentId",
                table: "ReturningRequests");

            migrationBuilder.CreateIndex(
                name: "IX_ReturningRequests_AssignmentId",
                table: "ReturningRequests",
                column: "AssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReturningRequests_AssignmentId",
                table: "ReturningRequests");

            migrationBuilder.CreateIndex(
                name: "IX_ReturningRequests_AssignmentId",
                table: "ReturningRequests",
                column: "AssignmentId",
                unique: true);
        }
    }
}
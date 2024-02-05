using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workout_Builder.Migrations
{
    /// <inheritdoc />
    public partial class addedlastUpdatedfieldtoexercisetypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "ExerciseTypes",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "ExerciseTypes");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workout_Builder.Migrations
{
    /// <inheritdoc />
    public partial class Addedrepsandweightfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomExercise",
                table: "Exercises",
                newName: "CustomExerciseName");

            migrationBuilder.AddColumn<bool>(
                name: "CustomSets",
                table: "Exercises",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MasterReps",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MasterWeight",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomSets",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MasterReps",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MasterWeight",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "CustomExerciseName",
                table: "Exercises",
                newName: "CustomExercise");
        }
    }
}

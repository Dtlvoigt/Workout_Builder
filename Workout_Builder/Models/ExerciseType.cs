using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WorkoutManager.Data.Models
{
    public class ExerciseType
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Force {  get; set; }
        public string? Level { get; set; }
        public string? Mechanic { get; set; }
        public string? Equipment { get; set; }
        public string[]? PrimaryMuscles { get; set; }
        public string[]? SecondaryMuscles { get; set; }
        public string[]? Instructions {  get; set; }
        public string? Category { get; set; }
    }
}

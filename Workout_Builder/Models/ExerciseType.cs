using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Workout_Builder.Models
{
    public class ExerciseType
    {
        [Key]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Exercise name is required")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [JsonPropertyName("force")]
        public string? Force {  get; set; }
        [JsonPropertyName("level")]
        public string? Level { get; set; }
        [JsonPropertyName("mechanic")]
        public string? Mechanic { get; set; }
        [JsonPropertyName("equipment")]
        public string? Equipment { get; set; }
        [JsonPropertyName("primaryMuscles")]
        public string[]? PrimaryMuscles { get; set; }
        [JsonPropertyName("secondaryMuscles")]
        public string[]? SecondaryMuscles { get; set; }
        [JsonPropertyName("instructions")]
        public string[]? Instructions {  get; set; }
        [JsonPropertyName("category")]
        public string? Category { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}

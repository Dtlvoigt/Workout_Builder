using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Workout_Builder.Models
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public Boolean IsTemplate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateAttempted { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Boolean Completed { get; set; } = false;
        public Boolean Paused { get; set; } = false;
    }
}

using System.ComponentModel.DataAnnotations;

namespace Workout_Builder.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        public Workout? Workout { get; set; }
        public ExerciseType? ExerciseType { get; set; }
        public int Order { get; set; }
        public Boolean Completed { get; set; } = false;
    }
}

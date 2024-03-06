using System.ComponentModel.DataAnnotations;

namespace Workout_Builder.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        public Workout? Workout { get; set; }
        public ExerciseType? ExerciseType { get; set; }
        public String? CustomExerciseName { get; set; }
        public int Order { get; set; }
        public Boolean Completed { get; set; } = false;
        public Boolean IsPounds { get; set; } = true;
        public int NumSets {  get; set; }
        public int MasterReps {  get; set; }
        public int MasterWeight { get; set; }
        public string? SetsJsonString { get; set; }
        public Boolean CustomSets { get; set; }
    }
}

using Workout_Builder.Models;

namespace Workout_Builder.ViewModels
{
    public class WorkoutVM
    {
        public Workout Workout { get; set; }
        public List<Workout> CurrentTemplates { get; set; }
        public List<ExerciseVM> ExerciseModels { get; set; }
        public int MaxNumExercises { get; set; }
        public int MaxNumSets { get; set; }
        public int NumExercises { get; set; }
        public Boolean IsPounds { get; set; } = true;
    }
}

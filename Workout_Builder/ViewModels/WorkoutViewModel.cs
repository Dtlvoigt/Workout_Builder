using Workout_Builder.Models;

namespace Workout_Builder.ViewModels
{
    public class WorkoutVM
    {
        public Workout? Workout { get; set; }
        public List<Workout> CurrentTemplates { get; set; }
        public List<ExerciseVM> ExerciseModels { get; set; }
        //public List<Exercise> Exercises { get; set; }
        //public Exercise NewExercise { get; set; }
        //public Boolean AddExercise { get; set; }
        public int MaxNumExercises { get; set; }
        public int MaxNumSets { get; set; }
        public int NumExercises { get; set; }
        public Boolean IsPounds { get; set; } = true;
        //public List<Set> Sets { get; set; }
        //public Exercise Exercise { get; set; }
        //public ExerciseType ExerciseType { get; set; }
        //public Set Set { get; set; }
    }
}

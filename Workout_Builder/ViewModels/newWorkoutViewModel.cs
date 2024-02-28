using Workout_Builder.Models;

namespace Workout_Builder.ViewModels
{
    public class NewWorkoutVM
    {
        public Workout Workout { get; set; }
        public List<Workout> CurrentTemplates { get; set; }
        public List<NewExerciseViewModel> ExerciseModels { get; set; }
        public List<Exercise> Exercises { get; set; }
        public Exercise NewExercise { get; set; }
        public Boolean AddExercise { get; set; }
        public int MaxNumExercises { get; set; }
        public int MaxNumSets { get; set; }
        //public List<Set> Sets { get; set; }
        //public Exercise Exercise { get; set; }
        //public ExerciseType ExerciseType { get; set; }
        //public Set Set { get; set; }
    }
}

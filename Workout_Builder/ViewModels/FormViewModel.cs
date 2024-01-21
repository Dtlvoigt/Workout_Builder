using Workout_Builder.Models;

namespace Workout_Builder.ViewModels
{
    public class FormViewModel
    {
        public Workout Workout { get; set; }
        public Exercise Exercise { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public Set Set { get; set; }
    }
}

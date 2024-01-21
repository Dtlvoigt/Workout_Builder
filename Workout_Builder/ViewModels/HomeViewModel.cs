using Workout_Builder.Models;

namespace Workout_Builder.ViewModels
{
    public class HomeViewModel
    {
        public List<Workout> WorkoutTemplates { get; set; }
        public List<Workout> RecentWorkouts { get; set; }
    }
}

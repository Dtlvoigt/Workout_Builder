using Microsoft.AspNetCore.Mvc.Rendering;
using Workout_Builder.Models;

namespace Workout_Builder.ViewModels
{
    public class NewExerciseViewModel
    {
        public Exercise Exercise { get; set; }
        public List<SelectListItem> ExerciseList { get; set; }
        public String? SelectedExercise {  get; set; }
        public Boolean Deleted { get; set; }
    }
}

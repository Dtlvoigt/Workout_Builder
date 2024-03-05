using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Workout_Builder.Models;

namespace Workout_Builder.ViewModels
{
    public class NewExerciseViewModel
    {
        //public Exercise Exercise { get; set; }
        [Required(ErrorMessage = "Exercise name is required")]
        public string Name { get; set; }
        public List<SelectListItem>? ExerciseList { get; set; }
        //public String? SelectedExercise { get; set; }
        //public Boolean Deleted { get; set; }
        public int MaxNumExercises { get; set; }
        public int Order { get; set; }
        public int NumSets { get; set; }
        public int MasterReps { get; set; }
        public int MasterWeight { get; set; }
        public List<Set>? SetsList { get; set; }
    }
}

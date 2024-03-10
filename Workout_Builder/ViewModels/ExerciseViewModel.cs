using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using Workout_Builder.Models;

namespace Workout_Builder.ViewModels
{
    public class ExerciseVM
    {
        public ExerciseVM(int order)
        {
            Name = "";
            Order = order;
            NumSets = 1;
            SetsList = new List<Set>();
            CustomSets = false;
        }

        public ExerciseVM(Exercise exercise)
        {
            if(String.IsNullOrEmpty(exercise.CustomExerciseName))
            {
                if (exercise.ExerciseType == null)
                {
                    throw new Exception("Exercise type is null on exercise: " + exercise.Id);
                }
                Name = exercise.ExerciseType.Name;
            }
            else
            {
                Name = exercise.CustomExerciseName;
            }
            
            Order = exercise.Order;
            NumSets = exercise.NumSets;
            MasterReps = exercise.MasterReps;
            MasterWeight = exercise.MasterWeight;
            SetsList = new List<Set>();
            CustomSets = exercise.CustomSets;
        }

        //public Exercise Exercise { get; set; }
        [Required(ErrorMessage = "Exercise name is required")]
        public string Name { get; set; }
        public List<SelectListItem>? ExerciseList { get; set; }
        public int Order { get; set; }
        public int NumSets { get; set; }
        public int MasterReps { get; set; }
        public int MasterWeight { get; set; }
        public List<Set> SetsList { get; set; }
        public Boolean CustomSets { get; set; } = false;
    }
}

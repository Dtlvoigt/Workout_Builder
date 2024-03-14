using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Workout_Builder.Models;

namespace Workout_Builder.ViewModels
{
    public class ExerciseVM
    {
        public ExerciseVM() { }
        public ExerciseVM(int order, int maxSets)
        {
            Name = "";
            Order = order;
            NumSets = 1;
            SetsList = new List<Set>();
            CustomSets = false;

            //create sets
            for (int i = 0; i < maxSets; i++)
            {
                SetsList.Add(new Set());
            }
        }

        public ExerciseVM(Exercise exercise, int maxSets)
        {
            //get exercise name
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

            //get sets for existing exercise
            if (!String.IsNullOrEmpty(exercise.SetsJsonString))
            {
                SetsList = GetSetsFromString(exercise.SetsJsonString);
            }
            else
            {
                for (int i = 0; i < maxSets; i++)
                {
                    SetsList.Add(new Set());
                }
            }
        }

        //local function
        List<Set> GetSetsFromString(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                throw new Exception("Set string is empty");
            }
            var sets = JsonSerializer.Deserialize<List<Set>>(jsonString);
            return sets;
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

using Microsoft.AspNetCore.Mvc.Rendering;
using Workout_Builder.Models;

namespace Workout_Builder.Services
{
    public interface IWorkoutService
    {
        //search workouts
        Task<List<Workout>> GetUserTemplates(string userID);
        Task<List<Exercise>> GetUserExercises(string userID);


        //updating workouts
        Task<int> AddWorkout(Workout workout);
        Task AddExerciseTypes(List<ExerciseType> exerciseTypes);

        //non specific workout info
        Task<List<ExerciseType>> AutofillExerciseTypes(string input);
        Task<List<SelectListItem>> GetExerciseSelectList();

        //user info

        //file loading
        Task LoadExerciseTypes();
    }
}

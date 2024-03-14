using Microsoft.AspNetCore.Mvc.Rendering;
using Workout_Builder.Models;

namespace Workout_Builder.Services
{
    public interface IWorkoutService
    {
        //search workouts
        Task<List<Workout>> GetUserTemplates(string userID);
        Task<List<Exercise>> GetUserExercises(string userID);
        Task<Workout> GetWorkout(int workoutID);
        Task<List<Exercise>> GetExercises(int workoutID);


        //updating workouts
        Task<int> AddExercise(Exercise exercise);
        Task<int> AddWorkout(Workout workout);
        Task AddExerciseTypes(List<ExerciseType> exerciseTypes);

        //non specific workout info
        Task<List<ExerciseType>> AutofillExerciseTypes(string input);
        Task<List<SelectListItem>> GetExerciseSelectList();
        Task<ExerciseType> GetExerciseByName(string name);

        //user info

        //file loading
        Task LoadExerciseTypes();

        string CreateSetJsonString(List<Set> sets);
        List<Set> GetSetsFromString(string jsonString);
    }
}

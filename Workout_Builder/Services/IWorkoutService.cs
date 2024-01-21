using Workout_Builder.Models;

namespace Workout_Builder.Services
{
    public interface IWorkoutService
    {
        //search workouts
        Task<List<Workout>> GetWorkoutTemplates(string userID);
        Task<List<Exercise>> GetUserExercises(string userID);


        //updating workouts

        //non specific workout info

        //user info
    }
}

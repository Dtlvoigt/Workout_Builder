using Workout_Builder.Models;

namespace Workout_Builder.Services
{
    public interface IWorkoutService
    {
        //search workouts
        Task<List<Workout>> GetUserTemplates(string userID);
        Task<List<Exercise>> GetUserExercises(string userID);


        //updating workouts
        System.Threading.Tasks.Task AddWorkout(Workout workout);

        //non specific workout info

        //user info
    }
}

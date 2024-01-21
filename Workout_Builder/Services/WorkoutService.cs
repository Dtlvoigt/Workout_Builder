using Microsoft.EntityFrameworkCore;
using Workout_Builder.Data;
using Workout_Builder.Models;

namespace Workout_Builder.Services
{
    public class WorkoutService : IWorkoutService
    {
        private IDbContextFactory<WorkoutContext> _dbContextFactory;

        public WorkoutService(IDbContextFactory<WorkoutContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void AddExercise(Exercise exercise)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Exercises.Add(exercise);
                context.SaveChanges();
            }
        }

        public void UpdateExercise(Exercise exercise)
        {
            if (exercise == null)
            {
                throw new Exception("Exercise " + exercise.Id + " not found.");
            }
            else
            {
                using (var context = _dbContextFactory.CreateDbContext())
                {
                    context.Update(exercise);
                    context.SaveChanges();
                }
            }
        }

        /////////////////////
        // search workouts //
        /////////////////////

        public async Task<List<Workout>> GetWorkoutTemplates(string userID)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                IQueryable<Workout> workoutQuery;
                workoutQuery = context.Workouts.OrderBy(i => i.DateCreated)
                                               .ThenBy(i => i.Name);
                return await workoutQuery.ToListAsync().ConfigureAwait(false);
            }            
        }

        public Task<List<Exercise>> GetUserExercises(string userID)
        {
            throw new NotImplementedException();
        }

        ///////////////////////
        // updating workouts //
        ///////////////////////


        ///////////////////////////////
        // non specific workout info //
        ///////////////////////////////
        public ExerciseType GetExerciseByName(string name)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var exerciseType = context.ExerciseTypes.FirstOrDefault(x => x.Name == name);
                return exerciseType ?? throw new Exception("Exercise " + name + " not found.");
            }
        }


        ///////////////
        // user info //
        ///////////////
    }
}

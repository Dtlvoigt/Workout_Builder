using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Workout_Builder.Data;
using Workout_Builder.Models;

namespace Workout_Builder.Services
{
    public class WorkoutService : IWorkoutService
    {
        private IWorkoutContext _dbContext;
        //private IDbContextFactory<WorkoutContext> _dbContext;

        public WorkoutService(IWorkoutContext workoutContext/*IDbContextFactory<WorkoutContext> dbContextFactory*/)
        {
            _dbContext = workoutContext;
        }

        public void AddExercise(Exercise exercise)
        {
            //using (var context = _dbContext.CreateDbContext())
            //{
            //    context.Exercises.Add(exercise);
            //    context.SaveChanges();
            //}
        }

        public void UpdateExercise(Exercise exercise)
        {
            //if (exercise == null)
            //{
            //    throw new Exception("Exercise " + exercise.Id + " not found.");
            //}
            //else
            //{
            //    using (var context = _dbContext.CreateDbContext())
            //    {
            //        context.Update(exercise);
            //        context.SaveChanges();
            //    }
            //}
        }

        /////////////////////
        // search workouts //
        /////////////////////

        public async Task<List<Workout>> GetUserTemplates(string userID)
        {
            IQueryable<Workout> workoutQuery;
            workoutQuery = _dbContext.Workouts.OrderBy(i => i.DateCreated)
                                            .ThenBy(i => i.Name);
            return await workoutQuery.ToListAsync().ConfigureAwait(false);
        }

        public Task<List<Exercise>> GetUserExercises(string userID)
        {
            throw new NotImplementedException();
        }

        ///////////////////////
        // updating workouts //
        ///////////////////////

        public async Task AddWorkout(Workout workout)
        {
            if (workout == null)
            {
                throw new Exception("workout is null");
            }
            if (String.IsNullOrWhiteSpace(workout.UserId))
            {
                throw new Exception("no userID");
            }

            workout.DateCreated = DateTime.Now;
            workout.IsTemplate = true;

            await _dbContext.Workouts.AddAsync(workout).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);
        }

        ///////////////////////////////
        // non specific workout info //
        ///////////////////////////////
        public ExerciseType GetExerciseByName(string name)
        {
            var exerciseType = _dbContext.ExerciseTypes.FirstOrDefault(x => x.Name == name) 
                ?? throw new Exception("Exercise " + name + " not found.");
            return exerciseType;
        }


        ///////////////
        // user info //
        ///////////////
    }
}

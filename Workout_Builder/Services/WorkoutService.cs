using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.IO;
using System.Web;
using Workout_Builder.Data;
using Workout_Builder.Models;
using Workout_Builder.Properties;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Humanizer.Localisation;

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

        public void AddExercise(Exercise exercise)
        {
            //using (var context = _dbContext.CreateDbContext())
            //{
            //    context.Exercises.Add(exercise);
            //    context.SaveChanges();
            //}
        }

        public async Task<int> AddWorkout(Workout workout)
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

            return workout.Id;
        }

        public async Task AddExerciseTypes(List<ExerciseType> exerciseTypes)
        {
            if (exerciseTypes == null)
            {
                throw new Exception("Exercise type list is empty.");
            }

            foreach(var exerciseType in exerciseTypes)
            {
                exerciseType.LastUpdated = DateTime.Now;
            }

            await _dbContext.ExerciseTypes.AddRangeAsync(exerciseTypes).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);
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

        ///////////////////////////////
        // non specific workout info //
        ///////////////////////////////
        public async Task<ExerciseType> GetExerciseByName(string name)
        {
            var exerciseType = await _dbContext.ExerciseTypes.FirstOrDefaultAsync(x => x.Name == name).ConfigureAwait(false);
                //?? throw new Exception("Exercise " + name + " not found.");
            return exerciseType;
        }

        public async Task<List<SelectListItem>> GetExerciseSelectList()
        {
            var selectList = _dbContext.ExerciseTypes.Select(e =>
                                                         new SelectListItem
                                                         {
                                                             Value = e.Id.ToString(),
                                                             Text = e.Name,
                                                         }).ToListAsync().ConfigureAwait(false);
            return await selectList;
        }

        public async Task<List<ExerciseType>> AutofillExerciseTypes(string input)
        {
            var exerciseTypes = await _dbContext.ExerciseTypes.Where(e => (!String.IsNullOrEmpty(e.Name)) 
                                                               && (e.Name.Contains(input)))
                                                              .OrderBy(e => e.Name)
                                                              .ToListAsync()
                                                              .ConfigureAwait(false);

            return exerciseTypes;
        }


        ///////////////
        // user info //
        ///////////////

        //////////////////
        // file loading //
        //////////////////

        public async Task LoadExerciseTypes()
        {
            string filename = "..\\Workout_Builder\\Data\\JSON\\exerciseTypes.json";
            List<ExerciseType>? exerciseTypes;

            using (StreamReader file = new StreamReader(filename))
            {
                string json = file.ReadToEnd();
                JsonNode? node = JsonNode.Parse(json);
                JsonNode root = node.Root;
                JsonArray exerciseArray = root["exercises"]!.AsArray();
                int count = exerciseArray.Count;
                exerciseTypes = JsonSerializer.Deserialize<List<ExerciseType>>(root["exercises"]);
            }

            if(exerciseTypes != null && exerciseTypes.Count > 0)
            {
                await AddExerciseTypes(exerciseTypes);
            }

            return;
        }



    }
}

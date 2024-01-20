using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data.Models;

namespace Workout_Builder.Data
{
    public class WorkoutContext : DbContext
    {
        public WorkoutContext(DbContextOptions<WorkoutContext> options) : base(options) { }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }

    }
}

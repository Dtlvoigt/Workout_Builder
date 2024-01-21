using Microsoft.EntityFrameworkCore;
using Workout_Builder.Models;

namespace Workout_Builder.Data
{
    public class WorkoutContext : DbContext, IWorkoutContext
    {
        public WorkoutContext(DbContextOptions<WorkoutContext> options) : base(options) { }

        public virtual DbSet<Workout> Workouts { get; set; }
        public virtual DbSet<ExerciseType> ExerciseTypes { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
    }
}

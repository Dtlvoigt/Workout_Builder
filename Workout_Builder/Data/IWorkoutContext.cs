using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Workout_Builder.Models;

namespace Workout_Builder.Data
{
    public interface IWorkoutContext :IDisposable
    {
        DbSet<Workout> Workouts { get; set; }
        DbSet<ExerciseType> ExerciseTypes { get; set; }
        DbSet<Exercise> Exercises { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

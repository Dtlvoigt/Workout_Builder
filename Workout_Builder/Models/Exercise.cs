using System.ComponentModel.DataAnnotations;

namespace WorkoutManager.Data.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        public Workout? Workout { get; set; }
        public DateTime Date { get; set; }

    }
}

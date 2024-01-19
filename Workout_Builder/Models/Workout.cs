using System.ComponentModel.DataAnnotations;

namespace WorkoutManager.Data.Models
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }
        Boolean IsTemplate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime Date { get; set; }


    }
}

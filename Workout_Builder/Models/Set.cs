namespace WorkoutManager.Data.Models
{
    public class Set
    {
        public Exercise? Exercise {  get; set; }
        public int Order { get; set; }
        public Boolean ForTime { get; set; }
        public int Duration { get; set; }
        public int Reps { get; set; }
        public Boolean Completed { get; set; } = false;
    }
}

﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Workout_Builder.Models
{
    public class Set
    {
        //[Key]
        //public int Id { get; set; }
        //public Exercise? Exercise {  get; set; }
        //public int Order { get; set; }
        //public Boolean ForTime { get; set; }
        //public int Duration { get; set; }
        //public int Reps { get; set; }
        //public Boolean Completed { get; set; } = false;

        public int Reps { get; set; }
        public int Weight { get; set; }
    }
}

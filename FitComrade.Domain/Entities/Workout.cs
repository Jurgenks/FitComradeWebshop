using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitComrade.Domain.Entities
{
    public class Workout
    {
        public int WorkoutID { get; set; }
        public string WorkoutName { get; set; }
        public string Discription { get; set; }
        public bool Confirmed { get; set; }
        public string WorkoutImage { get; set; }
        public string WorkoutVideo { get; set; }
    }
}

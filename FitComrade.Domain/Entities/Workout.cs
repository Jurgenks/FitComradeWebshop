using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitComrade.Domain.Entities
{
    public class Workout
    {
        public int WorkoutID { get; set; }

        [Required]
        public string WorkoutName { get; set; }

        [Required]
        public string Discription { get; set; }

        public bool Confirmed { get; set; }

        [DataType(DataType.ImageUrl)]
        public string WorkoutImage { get; set; }

        [DataType(DataType.ImageUrl)]
        public string WorkoutVideo { get; set; }        
    }
}

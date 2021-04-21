using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitComrade.Models
{
    public class Blog
    {
        public int BlogID { get; set; }
        public string BlogName { get; set; }
        public List<Workout> Workouts { get; set; }
        public int ProfileID { get; set; }
    }
}

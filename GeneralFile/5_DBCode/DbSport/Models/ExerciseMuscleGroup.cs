using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbSport.Models
{
    public class ExerciseMuscleGroup
    {
        public Guid ExerciseId { get; set; }
        public Guid MuscleGroupId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbSport.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public Guid ExerciseId { get; set; }
        public DateTime ActivityDate { get; set; }
        public int DurationMinutes { get; set; }
        public string Notes { get; set; }
        public decimal? CaloriesBurned { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static IUE7VU_HFT_2022231.Models.Enum;

namespace IUE7VU_HFT_2022231.Models
{
    public class Workout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkoutId { get; set; }
        public MuscleTypes MuscleTypes { get; set; }
        public double WorkoutTime_Weights { get; set; }
        public double WorkoutTime_Cardio { get; set; }
        public WorkoutDifficulty WorkoutDifficulty { get; set; }
        public DateTime WorkoutDay { get; set; }
        public int PersonId { get; set; }

        [JsonIgnore]
        public virtual Person Person { get; set;}
        public Workout()
        {
        }

        public Workout(string line)
        {
            string[] split = line.Split("#");
            WorkoutId = int.Parse(split[0]);
            MuscleTypes = MuscleTypes.Parse<MuscleTypes>(split[1]);
            WorkoutTime_Weights = double.Parse(split[2]);
            WorkoutTime_Cardio = double.Parse(split[3]);
            WorkoutDifficulty = WorkoutDifficulty.Parse<WorkoutDifficulty>(split[4]);
            WorkoutDay = DateTime.Parse(split[5].Replace('*', '.'));
            PersonId = int.Parse(split[6]);
        }
        public override string ToString()
        {
            return @$"
            WorkoutId: {this.WorkoutId}
            MuscleTypes: {this.MuscleTypes}
            WorkoutTime_Weights: {this.WorkoutTime_Weights}
            WorkoutTime_Cardio: {this.WorkoutTime_Cardio}
            WorkoutDifficulty: {this.WorkoutDifficulty}
            WorkoutDay: {this.WorkoutDay}
            PersonId: {this.PersonId}";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE7VU_HFT_2022231.Models
{
    public class Enum
    {
        public enum Gender
        {
            Male, Female
        };
        public enum MuscleTypes
        {
            Chest, Back, Shoulders, Legs, Biceps, Triceps
        };
        public enum MembershipTypes
        {
            Daily, Weekly, Monthly
        };
        public enum MembershipTicketColours
        {
            Red, Green, Blue, Pink
        };
        public enum WorkoutDifficulty
        {
            Easy, Medium, Hard, Extreme
        };
    }
}

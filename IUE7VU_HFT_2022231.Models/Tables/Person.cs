using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static IUE7VU_HFT_2022231.Models.Enum;

namespace IUE7VU_HFT_2022231.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }
        [StringLength(20, MinimumLength = 3)]
        public string PersonName { get; set; }
        public int PersonAge { get; set; }
        public Gender PersonGender { get; set; }
        public int? TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; }
        public virtual Membership Memberships { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }
        public Person()
        {
        }
        
        public Person(string line)
        {
            string[] split = line.Split("#");
            PersonId = int.Parse(split[0]);
            PersonName = split[1];
            PersonAge = int.Parse(split[2]);
            PersonGender = Gender.Parse<Gender>(split[3]);
            TrainerId = split[4] == "null" ? null : int.Parse(split[4]);
        }
    }
}

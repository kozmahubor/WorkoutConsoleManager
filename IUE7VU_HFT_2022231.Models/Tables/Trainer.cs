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
    public class Trainer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainerId { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string TrainerName { get; set; }
        public int TrainerAge { get; set; }
        public Gender TrainerGender { get; set; }
        [JsonIgnore]
        public virtual ICollection<Person> Clients { get; set; }

        public Trainer()
        {

        }
        public Trainer(string line)
        {
            string[] split = line.Split("#");
            TrainerId = int.Parse(split[0]);
            TrainerName = split[1];
            TrainerAge = int.Parse(split[2]);
            TrainerGender = Gender.Parse<Gender>(split[3]);
        }
    }
}

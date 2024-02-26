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
    public class Membership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MembershipId { get; set; }
        public MembershipTypes MembershipType { get; set; }
        public DateTime MembershipDurationBegin { get; set; }
        public DateTime MembershipDurationEnd { get; set; }
        public MembershipTicketColours MembershipTicketColour { get; set; }
        public int PersonId { get; set; }
        [JsonIgnore]
        public virtual Person Person { get; set; }

        public Membership()
        {
        }

        public Membership(string line)
        {
            string[] split = line.Split("#");
            MembershipId = int.Parse(split[0]);
            MembershipType = MembershipTypes.Parse<MembershipTypes>(split[1]);
            MembershipDurationBegin = DateTime.Parse(split[2].Replace("*", "."));
            MembershipDurationEnd = DateTime.Parse(split[3].Replace("*", "."));
            MembershipTicketColour = MembershipTicketColours.Parse<MembershipTicketColours>(split[4]);
            PersonId = int.Parse(split[5]);
        }
    }
}

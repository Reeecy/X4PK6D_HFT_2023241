using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4PK6D_HFT_2023241.Models
{
    public class Pass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(60)]
        public string PassType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; }
        public bool CrossfitGymUsage { get; set; }
        public bool GroupTrainingUsage { get; set; }
        public bool PoolUsage { get; set; }
        public bool SaunaUsage { get; set; }
        public bool MassageUsage { get; set; }
        [NotMapped]
        public virtual ICollection<Person> Persons { get; set; }
    }
}

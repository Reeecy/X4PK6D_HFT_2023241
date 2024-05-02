using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace X4PK6D_HFT_2023241.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(60)]
        public string FirstName { get; set; }
        [StringLength(60)]
        public string LastName { get; set;}
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        [StringLength(30)]
        public string PhoneNumber { get; set; }
        [StringLength(240)]
        public string Email { get; set; }
        public bool IsStudent { get; set; }
        public bool IsRetired { get; set; }

        public int PassId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Pass Pass { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<EntriesExits> EntriesExits { get; set; }

        public Person()
        {
            EntriesExits = new HashSet<EntriesExits>();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        [StringLength(240)]
        public string Email { get; set; }
        public bool isStudent { get; set; }
        public bool isRetired { get; set; }

        public int PassId { get; set; }
        public int EntriesExitsId { get; set; }
        [NotMapped]
        public virtual Pass Pass { get; set; }
        [NotMapped]
        public virtual ICollection<EntriesExits> EntriesExits { get; set; }
    }
}

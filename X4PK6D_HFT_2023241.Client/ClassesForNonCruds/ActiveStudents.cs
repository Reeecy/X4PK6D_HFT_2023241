using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4PK6D_HFT_2023241.Client
{
    public class ActiveStudents
    {
        public string FullName { get; set; }
        public string PassType { get; set; }
        public string EndDate { get; set; }
        public List<string> Entries { get; set; }
        public List<string> Exits { get; set; }
    }
}
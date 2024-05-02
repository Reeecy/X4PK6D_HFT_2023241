using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4PK6D_HFT_2023241.WPFClient
{
    public class ActiveStudents
    {
        public string FullName { get; set; }
        public string PassType { get; set; }
        public string EndDate { get; set; }
        public List<string> Entries { get; set; }
        public List<string> Exits { get; set; }

        public override string ToString()
        {
            var entries = string.Join(", ", Entries);
            var exits = string.Join(", ", Exits);

            return $"Full Name: {FullName}, Pass Type: {PassType}, End Date: {EndDate}, Entries: {entries}, Exits: {exits}";
        }
    }
}
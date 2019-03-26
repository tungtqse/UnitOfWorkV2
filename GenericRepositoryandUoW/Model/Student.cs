using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryandUoW.Model
{
    public class Student : EntityBase
    {
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
    }
}
